﻿using Fur.DatabaseAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 查询操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 查询单条 + TEntity Find(object id)

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        TEntity Find(object id);

        #endregion 查询单条 + TEntity Find(object id)

        #region 查询单条 + ValueTask<TEntity> FindAsync(object id)

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        ValueTask<TEntity> FindAsync(object id);

        #endregion 查询单条 + ValueTask<TEntity> FindAsync(object id)

        #region 查询单条 + TEntity Single()

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        TEntity Single();

        #endregion 查询单条 + TEntity Single()

        #region 查询单条 + Task<TEntity> SingleAsync()

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        Task<TEntity> SingleAsync();

        #endregion 查询单条 + Task<TEntity> SingleAsync()

        #region 查询单条 + TEntity Single(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        TEntity Single(Expression<Func<TEntity, bool>> expression);

        #endregion 查询单条 + TEntity Single(Expression<Func<TEntity, bool>> expression)

        #region 查询单条 + Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);

        #endregion 查询单条 + Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)

        #region 查询单条 + TEntity Single(bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity Single(bool noTracking);

        #endregion 查询单条 + TEntity Single(bool noTracking)

        #region 查询单条 + Task<TEntity> SingleAsync(bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleAsync(bool noTracking);

        #endregion 查询单条 + Task<TEntity> SingleAsync(bool noTracking)

        #region 查询单条 + TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking);

        #endregion 查询单条 + TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询单条 + Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        #endregion 查询单条 + Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询单条 + TEntity SingleOrDefault()

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        TEntity SingleOrDefault();

        #endregion 查询单条 + TEntity SingleOrDefault()

        #region 查询单条 + Task<TEntity> SingleOrDefaultAsync()

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        Task<TEntity> SingleOrDefaultAsync();

        #endregion 查询单条 + Task<TEntity> SingleOrDefaultAsync()

        #region 查询单条 + TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression);

        #endregion 查询单条 + TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)

        #region 查询单条 + Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        #endregion 查询单条 + Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)

        #region 查询单条 + TEntity SingleOrDefault(bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity SingleOrDefault(bool noTracking);

        #endregion 查询单条 + TEntity SingleOrDefault(bool noTracking)

        #region 查询单条 + Task<TEntity> SingleOrDefaultAsync(bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleOrDefaultAsync(bool noTracking);

        #endregion 查询单条 + Task<TEntity> SingleOrDefaultAsync(bool noTracking)

        #region 查询单条 + TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking);

        #endregion 查询单条 + TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询单条 + Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        #endregion 查询单条 + Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询一条 + TEntity First()

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <returns>实体</returns>
        TEntity First();

        #endregion 查询一条 + TEntity First()

        #region 查询一条 + Task<TEntity> FirstAsync()

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <returns>实体</returns>
        Task<TEntity> FirstAsync();

        #endregion 查询一条 + Task<TEntity> FirstAsync()

        #region 查询一条 + TEntity First(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        TEntity First(Expression<Func<TEntity, bool>> expression);

        #endregion 查询一条 + TEntity First(Expression<Func<TEntity, bool>> expression)

        #region 查询一条 + Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);

        #endregion 查询一条 + Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)

        #region 查询一条 + TEntity First(bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity First(bool noTracking);

        #endregion 查询一条 + TEntity First(bool noTracking)

        #region 查询一条 + Task<TEntity> FirstAsync(bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstAsync(bool noTracking);

        #endregion 查询一条 + Task<TEntity> FirstAsync(bool noTracking)

        #region 查询一条 + TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns></returns>
        TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking);

        #endregion 查询一条 + TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询一条 + Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        #endregion 查询一条 + Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询一条 + TEntity FirstOrDefault()

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <returns>实体</returns>
        TEntity FirstOrDefault();

        #endregion 查询一条 + TEntity FirstOrDefault()

        #region 查询一条 + Task<TEntity> FirstOrDefaultAsync()

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <returns>实体</returns>
        Task<TEntity> FirstOrDefaultAsync();

        #endregion 查询一条 + Task<TEntity> FirstOrDefaultAsync()

        #region 查询一条 + TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);

        #endregion 查询一条 + TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)

        #region 查询一条 + Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        #endregion 查询一条 + Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)

        #region 查询一条 + TEntity FirstOrDefault(bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity FirstOrDefault(bool noTracking);

        #endregion 查询一条 + TEntity FirstOrDefault(bool noTracking)

        #region 查询一条 + Task<TEntity> FirstOrDefaultAsync(bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstOrDefaultAsync(bool noTracking);

        #endregion 查询一条 + Task<TEntity> FirstOrDefaultAsync(bool noTracking)

        #region 查询一条 + TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking);

        #endregion 查询一条 + TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询一条 + Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        #endregion 查询一条 + Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询多条 + IQueryable<TEntity> All(bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        IQueryable<TEntity> All(bool noTracking = true, bool ignoreQueryFilters = false);

        #endregion 查询多条 + IQueryable<TEntity> All(bool noTracking = true, bool ignoreQueryFilters = false)

        #region 查询多条 + Task<List<TEntity>> AllAsync(bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        Task<List<TEntity>> AllAsync(bool noTracking = true, bool ignoreQueryFilters = false);

        #endregion 查询多条 + Task<List<TEntity>> AllAsync(bool noTracking = true, bool ignoreQueryFilters = false)

        #region 查询多条 + IQueryable<TEntity> All(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        IQueryable<TEntity> All(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false);

        #endregion 查询多条 + IQueryable<TEntity> All(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 查询多条 + Task<List<TEntity>> AllAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        Task<List<TEntity>> AllAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false);

        #endregion 查询多条 + Task<List<TEntity>> AllAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 分页查询多条 + PagedListOfT<TEntity> PagedAll(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns><see cref="PagedListOfT{T}"/></returns>
        PagedListOfT<TEntity> PagedAll(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false);

        #endregion 分页查询多条 + PagedListOfT<TEntity> PagedAll(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 分页查询多条 + Task<PagedListOfT<TEntity>> PagedAllAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        Task<PagedListOfT<TEntity>> PagedAllAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false);

        #endregion 分页查询多条 + Task<PagedListOfT<TEntity>> PagedAllAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 分页查询多条 + PagedListOfT<TEntity> PagedAll(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        PagedListOfT<TEntity> PagedAll(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false);

        #endregion 分页查询多条 + PagedListOfT<TEntity> PagedAll(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 分页查询多条 + Task<PagedListOfT<TEntity>> PagedAllAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        Task<PagedListOfT<TEntity>> PagedAllAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false);

        #endregion 分页查询多条 + Task<PagedListOfT<TEntity>> PagedAllAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 判断记录是否存在 + bool Any(Expression<Func<TEntity, bool>> expression = null)

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>存在与否</returns>
        bool Any(Expression<Func<TEntity, bool>> expression = null);

        #endregion 判断记录是否存在 + bool Any(Expression<Func<TEntity, bool>> expression = null)

        #region 判断记录是否存在 + Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null);

        #endregion 判断记录是否存在 + Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)

        #region 获取记录条数 + int Count(Expression<Func<TEntity, bool>> expression = null)

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>

        int Count(Expression<Func<TEntity, bool>> expression = null);

        #endregion 获取记录条数 + int Count(Expression<Func<TEntity, bool>> expression = null)

        #region 获取记录条数 + Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null);

        #endregion 获取记录条数 + Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)

        #region 获取实体队列中最大实体 + TEntity Max()

        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="TEntity"/></returns>
        TEntity Max();

        #endregion 获取实体队列中最大实体 + TEntity Max()

        #region 获取实体队列中最大实体 + Task<TEntity> MaxAsync()

        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TEntity> MaxAsync();

        #endregion 获取实体队列中最大实体 + Task<TEntity> MaxAsync()

        #region 获取最大值 + TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>最大值</returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion 获取最大值 + TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)

        #region 获取最大值 + Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion 获取最大值 + Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)

        #region 获取实体队列中最小实体 + TEntity Min()

        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns>实体</returns>
        TEntity Min();

        #endregion 获取实体队列中最小实体 + TEntity Min()

        #region 获取实体队列中最小实体 + Task<TEntity> MinAsync()

        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TEntity> MinAsync();

        #endregion 获取实体队列中最小实体 + Task<TEntity> MinAsync()

        #region 获取最小值 + TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>最大值</returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion 获取最小值 + TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)

        #region 获取最小值 + Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        #endregion 获取最小值 + Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
    }
}