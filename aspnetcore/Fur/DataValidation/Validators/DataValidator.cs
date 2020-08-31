﻿using Fur.Extensions;
using Fur.FriendlyException;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Fur.DataValidation
{
    /// <summary>
    /// 数据验证器
    /// </summary>
    public static class DataValidator
    {
        /// <summary>
        /// 所有验证类型
        /// </summary>
        private static readonly IEnumerable<Type> ValidationTypes;

        /// <summary>
        /// 所有验证类型
        /// </summary>
        private static readonly IEnumerable<Type> ValidationMessageTypes;

        /// <summary>
        /// 验证类型正则表达式
        /// </summary>
        private static readonly Dictionary<string, ValidationItemMetadataAttribute> ValidationItemMetadatas;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DataValidator()
        {
            // 获取所有验证类型
            ValidationTypes = GetValidationTypes();

            // 获取所有验证消息类型
            ValidationMessageTypes = GetValidationMessageTypes();

            // 获取所有验证类型正则表达式
            ValidationItemMetadatas = GetValidationValidationItemMetadatas();

            // 缓存所有正则表达式
            GetValidationTypeValidationItemMetadataCached = new ConcurrentDictionary<object, (string, ValidationItemMetadataAttribute)>();
        }

        /// <summary>
        /// 验证类类型对象
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="validateAllProperties">是否验证所有属性</param>
        /// <returns>验证结果</returns>
        public static DataValidationResult TryValidateObject(object obj, bool validateAllProperties = true)
        {
            // 如果该类型贴有 [NonValidate] 特性，则跳过验证
            if (obj.GetType().IsDefined(typeof(NonValidationAttribute), true))
                return new DataValidationResult
                {
                    IsValid = true
                };

            // 存储验证结果
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), results, validateAllProperties);

            // 返回验证结果
            return new DataValidationResult
            {
                IsValid = isValid,
                ValidationResults = results
            };
        }

        /// <summary>
        /// 验证单个值
        /// </summary>
        /// <param name="value">单个值</param>
        /// <param name="validationAttributes">验证特性</param>
        /// <returns></returns>
        public static DataValidationResult TryValidateValue(object value, params ValidationAttribute[] validationAttributes)
        {
            // 存储验证结果
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var isValid = Validator.TryValidateValue(value, new ValidationContext(value), results, validationAttributes);

            // 返回验证结果
            return new DataValidationResult
            {
                IsValid = isValid,
                ValidationResults = results
            };
        }

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="regexOptions">正则表达式选项</param>
        /// <returns></returns>
        public static bool TryValidateValue(object value, string regexPattern, RegexOptions regexOptions = RegexOptions.None)
        {
            if (value == null) Oops.Oh($"Value cannot be null.", typeof(ArgumentNullException));
            return Regex.IsMatch(value.ToString(), regexPattern, regexOptions);
        }

        /// <summary>
        /// 验证类型验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationTypes"></param>
        /// <returns></returns>
        public static DataValidationResult TryValidateValue(object value, params object[] validationTypes)
        {
            return TryValidateValue(value, ValidationOptions.AllOfThem, validationTypes);
        }

        /// <summary>
        /// 验证类型验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationOptionss">验证方式</param>
        /// <param name="validationTypes"></param>
        /// <returns></returns>
        public static DataValidationResult TryValidateValue(object value, ValidationOptions validationOptionss, params object[] validationTypes)
        {
            // 存储验证结果
            ICollection<ValidationResult> results = new List<ValidationResult>();

            // 如果值未null，验证失败
            if (value == null)
            {
                results.Add(new ValidationResult("The Value is required."));

                // 返回验证结果
                return new DataValidationResult
                {
                    IsValid = false,
                    ValidationResults = results
                };
            }

            // 验证标识
            bool? isValid = null;
            foreach (var validationType in validationTypes)
            {
                // 解析名称和正则表达式
                var (validationName, validationItemMetadata) = GetValidationTypeValidationItemMetadata(validationType);

                // 验证结果
                var validResult = TryValidateValue(value, validationItemMetadata.RegularExpression, validationItemMetadata.RegexOptions);

                // 判断是否需要同时验证通过才通过
                if (validationOptionss == ValidationOptions.AtLeastOne)
                {
                    // 只要有一个验证通过，则跳出
                    if (validResult)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (!validResult)
                {
                    if (isValid != false) isValid = false;
                    // 添加错误消息
                    results.Add(new ValidationResult(
                        string.Format(validationItemMetadata.DefaultErrorMessage, value, validationName)));
                }
            }

            // 返回验证结果
            return new DataValidationResult
            {
                IsValid = isValid ?? true,
                ValidationResults = results
            };
        }

        /// <summary>
        /// <see cref="GetValidationValidationItemMetadata"/> 缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<object, (string, ValidationItemMetadataAttribute)> GetValidationTypeValidationItemMetadataCached;

        /// <summary>
        /// 获取验证类型正则表达式（需要缓存）
        /// </summary>
        /// <param name="validationType"></param>
        /// <returns></returns>
        private static (string ValidationName, ValidationItemMetadataAttribute ValidationItemMetadata) GetValidationTypeValidationItemMetadata(object validationType)
        {
            var isCached = GetValidationTypeValidationItemMetadataCached.TryGetValue(validationType, out (string, ValidationItemMetadataAttribute) validation);
            if (isCached) return validation;

            // 本地函数
            static (string, ValidationItemMetadataAttribute) Function(object validationType)
            {
                // 获取验证类型
                var type = validationType.GetType();

                // 判断是否是有效的验证类型
                if (!ValidationTypes.Any(u => u == type))
                    Oops.Oh($"{type.Name} is not a valid validation type.", typeof(InvalidOperationException));

                // 获取对应的枚举名称
                var validationName = Enum.GetName(type, validationType);

                // 判断是否配置验证正则表达式
                if (!ValidationItemMetadatas.ContainsKey(validationName))
                    Oops.Oh($"No ${validationName} validation type metadata exists.", typeof(InvalidOperationException));

                // 获取对应的验证选项
                var validationItemMetadataAttribute = ValidationItemMetadatas[validationName];

                return (validationName, validationItemMetadataAttribute);
            }

            // 调用本地函数
            validation = Function(validationType);
            GetValidationTypeValidationItemMetadataCached.TryAdd(validationType, validation);
            return validation;
        }

        /// <summary>
        /// 获取所有验证类型
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> GetValidationTypes()
        {
            // 扫描所有公开的的枚举且贴有 [ValidationType] 特性
            var validationTypes = App.Assemblies.SelectMany(a => a.GetTypes()
                .Where(u => u.IsPublic && u.IsEnum && u.IsDefined(typeof(ValidationTypeAttribute), true)));
            return validationTypes;
        }

        /// <summary>
        /// 获取所有验证消息类型
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> GetValidationMessageTypes()
        {
            // 扫描所有公开的的枚举且贴有 [ValidationMessageType] 特性
            var validationMessageTypes = App.Assemblies.SelectMany(a => a.GetTypes()
                    .Where(u => u.IsPublic && u.IsEnum && u.IsDefined(typeof(ValidationMessageTypeAttribute), true)))
                .ToList();

            // 加载自定义验证消息类型提供器
            var validationMessageTypeProvider = App.ServiceProvider.GetService<IValidationMessageTypeProvider>();
            if (validationMessageTypeProvider is { Definitions: not null }) validationMessageTypes.AddRange(validationMessageTypeProvider.Definitions);

            return validationMessageTypes.Distinct();
        }

        /// <summary>
        /// 获取验证类型所有有效的正则表达式
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, ValidationItemMetadataAttribute> GetValidationValidationItemMetadatas()
        {
            // 查找所有 [ValidationMessageType] 类型中的 [ValidationMessage] 消息定义
            var customErrorMessages = ValidationMessageTypes.SelectMany(u => u.GetFields()
                    .Where(u => u.IsDefined(typeof(ValidationMessageAttribute))))
                .ToDictionary(u => u.Name, u => u.GetCustomAttribute<ValidationMessageAttribute>().ErrorMessage);

            // 加载配置文件配置
            var validationTypeMessageSettings = App.GetOptions<ValidationTypeMessageSettingsOptions>();
            if (validationTypeMessageSettings is { Definitions: not null })
            {
                // 获取所有参数大于1的配置
                var settingsErrorMessages = validationTypeMessageSettings.Definitions
                    .Where(u => u.Length > 1)
                    .ToDictionary(u => u[0].ToString(), u => u[1].ToString());

                // 合并自定义验证消息
                customErrorMessages = customErrorMessages.AddOrUpdate(settingsErrorMessages);
            }

            // 获取所有验证属性
            var validationFields = ValidationTypes.SelectMany(u => u.GetFields()
                .Where(u => u.IsDefined(typeof(ValidationItemMetadataAttribute))))
                .ToDictionary(u => u.Name, u => ReplaceValidateErrorMessage(u.Name, u, customErrorMessages));

            return validationFields;
        }

        /// <summary>
        /// 替换默认验证失败消息
        /// </summary>
        /// <param name="name">验证唯一名称</param>
        /// <param name="type"></param>
        /// <param name="customErrorMessages"></param>
        private static ValidationItemMetadataAttribute ReplaceValidateErrorMessage(string name, FieldInfo field, Dictionary<string, string> customErrorMessages)
        {
            var validationValidationItemMetadata = field.GetCustomAttribute<ValidationItemMetadataAttribute>();
            if (customErrorMessages.ContainsKey(name))
            {
                validationValidationItemMetadata.DefaultErrorMessage = customErrorMessages[name];
            }

            return validationValidationItemMetadata;
        }
    }
}