﻿// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可操作性仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IOperableRepository<TEntity>
        where TEntity : class, IEntityBase, new()
    {
    }
}