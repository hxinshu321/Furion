﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions.Sql
{
    /// <summary>
    /// Sql DataSet 拓展类
    /// </summary>
    internal static class SqlDataSetExtensions
    {
        #region Sql 查询返回 DataSet + internal static DataSet SqlDataSet(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回 <see cref="DataSet"/>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="Microsoft.Data.SqlClient.SqlParameter"/></param>
        /// <returns><see cref="DataSet"/></returns>
        internal static DataSet SqlDataSet(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return databaseFacade.SqlDataAdapterFill(sql, commandType, parameters);
        }

        #endregion Sql 查询返回 DataSet + internal static DataSet SqlDataSet(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回 DataSet + internal static async Task<DataSet> SqlDataSetAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回 <see cref="DataSet"/>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="Microsoft.Data.SqlClient.SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<DataSet> SqlDataSetAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return databaseFacade.SqlDataAdapterFillAsync(sql, commandType, parameters);
        }

        #endregion Sql 查询返回 DataSet + internal static async Task<DataSet> SqlDataSetAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回一个结果集 + internal static IEnumerable<T1> SqlDataSet<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/></param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        internal static IEnumerable<T1> SqlDataSet<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);

            if (dataset.Tables.Count == 0) return default;
            return dataset.Tables[0].ToList<T1>();
        }

        #endregion Sql 查询返回一个结果集 + internal static IEnumerable<T1> SqlDataSet<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回两个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSet<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSet<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>());
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default);
            }
            return default;
        }

        #endregion Sql 查询返回两个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSet<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回三个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSet<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSet<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>());
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default);
            }
            return default;
        }

        #endregion Sql 查询返回三个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSet<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回四个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSet<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSet<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>());
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回四个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSet<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回五个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSet<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSet<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>());
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回五个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSet<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回六个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSet<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSet<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>());
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回六个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSet<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回七个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSet<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSet<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 7)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>());
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回七个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSet<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回八个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>(), dataset.Tables[7].ToList<T8>());
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>(), default);
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), default, default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default, default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回八个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回一个结果集 + internal static async Task<IEnumerable<T1>> SqlDataSetAsync<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<IEnumerable<T1>> SqlDataSetAsync<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count == 0) return default;
            return dataset.Tables[0].ToList<T1>();
        }

        #endregion Sql 查询返回一个结果集 + internal static async Task<IEnumerable<T1>> SqlDataSetAsync<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回两个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetAsync<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetAsync<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>());
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default);
            }
            return default;
        }

        #endregion Sql 查询返回两个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetAsync<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回三个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>());
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default);
            }
            return default;
        }

        #endregion Sql 查询返回三个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回四个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>());
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回四个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回五个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>());
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回五个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回六个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>());
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回六个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回七个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 7)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>());
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回七个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回八个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>(), dataset.Tables[7].ToList<T8>());
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>(), default);
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), default, default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default, default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default, default, default);
            }
            return default;
        }

        #endregion Sql 查询返回八个结果集 + internal static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回特定个数结果集 + internal static object SqlDataSet(this DatabaseFacade databaseFacade, string sql, Type[] returnTypes, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回特定个数结果集
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        internal static object SqlDataSet(this DatabaseFacade databaseFacade, string sql, Type[] returnTypes, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSet(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]), dataset.Tables[6].ToList(returnTypes[6]), dataset.Tables[7].ToList(returnTypes[7]));
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]), dataset.Tables[6].ToList(returnTypes[6]));
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]));
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]));
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]));
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]));
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]));
            }
            else if (dataset.Tables.Count == 1)
            {
                return dataset.Tables[0].ToList(returnTypes[0]);
            }
            return default;
        }

        #endregion Sql 查询返回特定个数结果集 + internal static object SqlDataSet(this DatabaseFacade databaseFacade, string sql, Type[] returnTypes, CommandType commandType = CommandType.Text, params object[] parameters)

        #region Sql 查询返回特定个数结果集 + internal static async Task<object> SqlDataSetAsync(this DatabaseFacade databaseFacade, string sql, Type[] returnTypes, CommandType commandType = CommandType.Text, params object[] parameters)

        /// <summary>
        /// Sql 查询返回特定个数结果集
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<object> SqlDataSetAsync(this DatabaseFacade databaseFacade, string sql, Type[] returnTypes, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]), dataset.Tables[6].ToList(returnTypes[6]), dataset.Tables[7].ToList(returnTypes[7]));
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]), dataset.Tables[6].ToList(returnTypes[6]));
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]), dataset.Tables[5].ToList(returnTypes[5]));
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]), dataset.Tables[4].ToList(returnTypes[4]));
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]), dataset.Tables[3].ToList(returnTypes[3]));
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]), dataset.Tables[2].ToList(returnTypes[2]));
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList(returnTypes[0]), dataset.Tables[1].ToList(returnTypes[1]));
            }
            else if (dataset.Tables.Count == 1)
            {
                return dataset.Tables[0].ToList(returnTypes[0]);
            }
            return default;
        }

        #endregion Sql 查询返回特定个数结果集 + internal static async Task<object> SqlDataSetAsync(this DatabaseFacade databaseFacade, string sql, Type[] returnTypes, CommandType commandType = CommandType.Text, params object[] parameters)
    }
}