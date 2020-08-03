﻿using Fur.AppCore.Attributes;
using Fur.AppCore.Inflations;
using Fur.AppCore.Options;
using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Entities;
using Fur.DatabaseAccessor.Entities.Configurations;
using Fur.DatabaseAccessor.MultipleTenants.Entities;
using Fur.DatabaseAccessor.MultipleTenants.Options;
using Fur.Linq.Extensions;
using Fur.MirrorController.Attributes;
using Fur.MirrorController.Dependencies;
using Fur.TypeExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Fur.AppCore
{
    /// <summary>
    /// 应用核心类
    /// </summary>
    [NonInflated]
    public static class App
    {
        /// <summary>
        /// 应用包装器
        /// </summary>
        public static ApplicationInflation Wrapper = null;

        /// <summary>
        /// Fur 框架配置选项
        /// </summary>
        public static FurOptions FurOptions = null;

        /// <summary>
        /// 多租户配置选项
        /// </summary>
        public static FurMultipleTenantOptions MultipleTenantOptions { get; internal set; } = FurMultipleTenantOptions.None;

        /// <summary>
        /// 是否支持多租户
        /// </summary>
        public static bool SupportedMultipleTenant { get; internal set; } = false;

        /// <summary>
        /// 是否支持性能分析
        /// </summary>
        public static bool SupportedMiniProfiler { get; internal set; } = false;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static App()
        {
            Wrapper ??= GetApplicationWrappers();
        }

        /// <summary>
        /// 获取类型的包装类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns><see cref="TypeInflation"/></returns>
        public static TypeInflation GetTypeWrapper(Type type)
            => Wrapper.PublicClassTypeWrappers.FirstOrDefault(u => u.ThisType == type);

        /// <summary>
        /// 获取方法的包装类型
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns><see cref="MethodInflation"/></returns>
        public static MethodInflation GetMethodWrapper(MethodInfo method)
            => Wrapper.PublicMethodWrappers.FirstOrDefault(u => u.ThisMethod == method);

        /// <summary>
        /// 获取公开类型自定义特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>{TAttribute}</returns>
        public static TAttribute GetPublicClassTypeCustomAttribute<TAttribute>(Type type) where TAttribute : Attribute
            => GetTypeWrapper(type).CustomAttributes.FirstOrDefault(u => u is TAttribute) as TAttribute;

        /// <summary>
        /// 获取公开方法自定义特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="method">方法</param>
        /// <returns>{TAttribute}</returns>
        public static TAttribute GetPublicMethodCustomAttribute<TAttribute>(MethodInfo method) where TAttribute : Attribute
            => GetMethodWrapper(method).CustomAttributes.FirstOrDefault(u => u is TAttribute) as TAttribute;

        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="typeInfo">类型</param>
        /// <param name="exceptControllerBase">是否排除 MVC <see cref="ControllerBase"/> 类型</param>
        /// <returns>bool</returns>
        internal static bool IsControllerType(TypeInfo typeInfo, bool exceptControllerBase = false)
        {
            // 必须是公开非抽象类、非泛型类、非接口类型
            if (!typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType || typeInfo.IsInterface) return false;

            // 判断是否是控制器类型，且 [ApiExplorerSettings].IgnoreApi!=true
            if (!exceptControllerBase)
            {
                var apiExplorerSettingsAttribute = typeInfo.GetDeepAttribute<ApiExplorerSettingsAttribute>();
                if (typeof(ControllerBase).IsAssignableFrom(typeInfo) && (apiExplorerSettingsAttribute == null || apiExplorerSettingsAttribute.IgnoreApi != true)) return true;
            }

            // 定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (typeInfo.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && typeInfo.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            // 是否是镜面控制器类型，且 [AttachController].Attach!=false，且继承 IAttachControllerDependency 接口
            var mirrorControllerAttribute = typeInfo.GetDeepAttribute<MirrorControllerAttribute>();
            if (mirrorControllerAttribute != null && mirrorControllerAttribute.Enabled != false && typeof(IMirrorControllerDependency).IsAssignableFrom(typeInfo)) return true;

            return false;
        }

        /// <summary>
        /// 判断是否是控制器类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="exceptControllerBase">除 MVC <see cref="ControllerBase"/> 类型</param>
        /// <returns>bool</returns>
        internal static bool IsControllerType(Type type, bool exceptControllerBase = false)
            => IsControllerType(type.GetTypeInfo(), exceptControllerBase);

        /// <summary>
        /// 判断是否是控制器 Action 类型
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>bool</returns>
        internal static bool IsControllerActionType(MethodInfo method)
        {
            // 方法所在类必须是一个控制器类型
            if (!IsControllerType(method.DeclaringType)) return false;

            // 必须是公开的，非抽象类，非静态方法，非泛型方法
            if (!method.IsPublic || method.IsAbstract || method.IsStatic || method.IsGenericMethod) return false;

            // 定义了 [ApiExplorerSettings] 特性，但特性 IgnoreApi 为 false
            if (method.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && method.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            return true;
        }

        /// <summary>
        /// 获取应用所有程序集
        /// <para>不包括Nuget/MyGet等第三方安装的包</para>
        /// </summary>
        /// <param name="namespacePrefix">命名空间前缀，默认为：<see cref="Fur"/></param>
        /// <returns> IEnumerable<Assembly></returns>
        private static IEnumerable<Assembly> GetApplicationAssembliesWithoutNuget(string namespacePrefix = nameof(Fur))
        {
            var dependencyConext = DependencyContext.Default;

            // 只包含项目程序集或Fur官方发布的Nuget包
            return dependencyConext.CompileLibraries
                .Where(u => !u.Serviceable && u.Name != "Fur.Database.Migrations" && (u.Type != "package" || u.Name.StartsWith(nameof(Fur))))
                .WhereIf(namespacePrefix.HasValue(), u => u.Name.StartsWith(namespacePrefix))
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)));
        }

        /// <summary>
        /// 获取应用解决方案中所有的包装器集合
        /// </summary>
        /// <returns><see cref="Wrapper"/></returns>
        private static ApplicationInflation GetApplicationWrappers()
        {
            // 避免重复读取
            if (Wrapper != null) return Wrapper;

            var applicationAssemblies = GetApplicationAssembliesWithoutNuget();

            // 组装应用包装器
            var applicationWrapper = new ApplicationInflation
            {
                // 创建程序集包装器
                AssemblyWrappers = applicationAssemblies
                .Select(a => new AssemblyInflation()
                {
                    ThisAssembly = a,
                    Name = a.GetName().Name,
                    FullName = a.FullName,

                    // 创建类型包装器
                    SubClassTypes = a.GetTypes()
                    .Where(t => t.IsPublic && !t.IsInterface && !t.IsEnum && !t.IsDefined(typeof(NonInflatedAttribute), false))
                    .Select(t => new TypeInflation()
                    {
                        ThisAssembly = a,
                        ThisType = t,
                        IsGenericType = t.IsGenericType,
                        IsControllerType = IsControllerType(t),
                        GenericArgumentTypes = t.IsGenericType ? t.GetGenericArguments() : null,
                        CustomAttributes = t.GetCustomAttributes(),
                        SwaggerGroups = GetControllerTypeSwaggerGroups(t),
                        IsStaticType = t.IsAbstract && t.IsSealed,
                        CanBeNew = !t.IsAbstract,
                        IsDbEntityRelevanceType = !t.IsAbstract && (typeof(IDbEntityBase).IsAssignableFrom(t) || typeof(IDbEntityConfigure).IsAssignableFrom(t)),
                        IsTenantType = t == typeof(Tenant),

                        // 创建包装属性器
                        SubPropertis = t.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                        .Where(p => p.DeclaringType == t && !p.IsDefined(typeof(NonInflatedAttribute), false))
                        .Select(p => new PropertyInflation()
                        {
                            Name = p.Name,
                            ThisAssembly = a,
                            ThisDeclareType = t,
                            PropertyType = p.PropertyType,
                            CustomAttributes = p.GetCustomAttributes()
                        }),

                        // 创建方法包装器
                        SubMethods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static)
                        .Where(m => m.DeclaringType == t && !m.IsDefined(typeof(NonInflatedAttribute), false))
                        .Select(m => new MethodInflation()
                        {
                            ThisAssembly = a,
                            ThisDeclareType = t,
                            ThisMethod = m,
                            Name = m.Name,
                            CustomAttributes = m.GetCustomAttributes(),
                            ReturnType = m.ReturnType,
                            IsControllerActionType = IsControllerActionType(m),
                            SwaggerGroups = GetControllerActionSwaggerGroups(m),

                            // 创建参数包装器
                            SubParameters = m.GetParameters(),
                            IsStaticMethod = m.IsStatic,
                            IsDbFunctionMethod = t.IsAbstract && t.IsSealed && m.IsStatic && m.IsDefined(typeof(DbFunctionAttribute), false)
                        })
                    })
                })
            };

            // 读取所有程序集下的公开类型包装器集合
            applicationWrapper.PublicClassTypeWrappers = applicationWrapper.AssemblyWrappers.SelectMany(u => u.SubClassTypes);

            // 读取所有程序集下的所有方法包装器集合
            applicationWrapper.PublicMethodWrappers = applicationWrapper.PublicClassTypeWrappers.SelectMany(u => u.SubMethods);

            return applicationWrapper;
        }

        /// <summary>
        /// 获取控制器类型 Swagger 接口文档分组
        /// </summary>
        /// <param name="controllerType">控制器类型</param>
        /// <returns>string[]</returns>
        private static string[] GetControllerTypeSwaggerGroups(Type controllerType)
        {
            // 如果不是控制器类型，返回 null
            if (!IsControllerType(controllerType)) return default;

            var defaultSwaggerGroups = new string[] { "Default" };

            if (!controllerType.IsDefined(typeof(MirrorControllerAttribute), true)) return defaultSwaggerGroups;

            var mirrorControllerAttribute = controllerType.GetDeepAttribute<MirrorControllerAttribute>();
            if (mirrorControllerAttribute.SwaggerGroups == null || !mirrorControllerAttribute.SwaggerGroups.Any()) return defaultSwaggerGroups;

            return mirrorControllerAttribute.SwaggerGroups;
        }

        /// <summary>
        /// 获取控制器 Action Swagger 接口文档分组
        /// </summary>
        /// <param name="controllerAction">控制器Action</param>
        /// <returns>string[]</returns>
        private static string[] GetControllerActionSwaggerGroups(MethodInfo controllerAction)
        {
            // 如果不是控制器Action类型，返回 null
            if (!IsControllerActionType(controllerAction)) return null;

            if (!controllerAction.IsDefined(typeof(MirrorActionAttribute), true)) return GetControllerTypeSwaggerGroups(controllerAction.DeclaringType);

            var attachActionAttribute = controllerAction.GetCustomAttribute<MirrorActionAttribute>();
            if (attachActionAttribute.SwaggerGroups == null || !attachActionAttribute.SwaggerGroups.Any()) return GetControllerTypeSwaggerGroups(controllerAction.DeclaringType);

            return attachActionAttribute.SwaggerGroups;
        }
    }
}