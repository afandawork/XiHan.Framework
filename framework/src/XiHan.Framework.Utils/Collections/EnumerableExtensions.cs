﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// FileName:EnumerableExtensions
// Guid:3d50f5ab-2bbb-4643-a8bc-03e137b0428f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/22 2:33:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Framework.Utils.DataFilter;
using XiHan.Framework.Utils.DataFilter.Dtos;
using XiHan.Framework.Utils.DataFilter.Enums;

namespace XiHan.Framework.Utils.Collections;

/// <summary>
/// 可列举扩展方法
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// 使用指定的分隔符连接构造的 <see cref="IEnumerable{T}"/> 集合（类型为 System.String）的成员
    /// 这是 string.Join(...) 的快捷方式
    /// </summary>
    /// <param name="source">包含要连接的字符串的集合</param>
    /// <param name="separator">要用作分隔符的字符串只有当 values 有多个元素时，separator 才会包含在返回的字符串中</param>
    /// <returns>由 values 的成员组成的字符串，这些成员由 separator 字符串分隔。如果 values 没有成员，则方法返回 System.String.Empty</returns>
    public static string JoinAsString(this IEnumerable<string> source, string separator)
    {
        return string.Join(separator, source);
    }

    /// <summary>
    /// 使用指定的分隔符连接集合的成员
    /// 这是 string.Join(...) 的快捷方式
    /// </summary>
    /// <param name="source">包含要连接的对象的集合</param>
    /// <param name="separator">要用作分隔符的字符串只有当 values 有多个元素时，separator 才会包含在返回的字符串中</param>
    /// <typeparam name="T">values 成员的类型</typeparam>
    /// <returns>由 values 的成员组成的字符串，这些成员由 separator 字符串分隔如果 values 没有成员，则方法返回 System.String.Empty</returns>
    public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
    {
        return string.Join(separator, source);
    }

    #region 筛选

    /// <summary>
    /// 如果给定的条件为真，则使用给定的谓词对 <see cref="IEnumerable{T}"/> 进行过滤
    /// </summary>
    /// <param name="source">要应用过滤的枚举对象</param>
    /// <param name="condition">第三方条件</param>
    /// <param name="predicate">用于过滤枚举对象的谓词</param>
    /// <returns>基于 <paramref name="condition"/> 的过滤或未过滤的枚举对象</returns>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    /// <summary>
    /// 如果给定的条件为真，则使用给定的谓词对 <see cref="IEnumerable{T}"/> 进行过滤
    /// </summary>
    /// <param name="source">要应用过滤的枚举对象</param>
    /// <param name="condition">第三方条件</param>
    /// <param name="predicate">用于过滤枚举对象的谓词，包含索引</param>
    /// <returns>基于 <paramref name="condition"/> 的过滤或未过滤的枚举对象</returns>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    #endregion

    #region 排序

    /// <summary>
    /// 对 <see cref="IEnumerable{T}"/> 进行排序
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">原始数据源</param>
    /// <param name="sortField">排序字段</param>
    /// <param name="sortDirection">排序方向</param>
    /// <returns>排序后的数据</returns>
    public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string sortField, SortDirectionEnum sortDirection)
    {
        return CollectionPropertySorter<T>.OrderBy(source, sortField, sortDirection);
    }

    /// <summary>
    /// 对 <see cref="IEnumerable{T}"/> 进行排序
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">原始数据源</param>
    /// <param name="sortCondition">排序条件</param>
    /// <returns>排序后的数据</returns>
    public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, SortConditionDto sortCondition)
    {
        return CollectionPropertySorter<T>.OrderBy(source, sortCondition);
    }

    /// <summary>
    /// 对 <see cref="IEnumerable{T}"/> 进行排序
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">原始数据源</param>
    /// <param name="sortCondition">排序条件</param>
    /// <returns>排序后的数据</returns>
    public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, SortConditionDto<T> sortCondition)
    {
        return CollectionPropertySorter<T>.OrderBy(source, sortCondition);
    }

    /// <summary>
    /// 对 <see cref="IEnumerable{T}"/> 进行后续排序
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">原始数据源</param>
    /// <param name="sortField">排序字段</param>
    /// <param name="sortDirection">排序方向</param>
    /// <returns>排序后的数据</returns>
    public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source, string sortField, SortDirectionEnum sortDirection)
    {
        return CollectionPropertySorter<T>.ThenBy(source, sortField, sortDirection);
    }

    /// <summary>
    /// 对 <see cref="IEnumerable{T}"/> 进行后续排序
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">原始数据源</param>
    /// <param name="sortCondition">排序条件</param>
    /// <returns>排序后的数据</returns>
    public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source, SortConditionDto sortCondition)
    {
        return CollectionPropertySorter<T>.ThenBy(source, sortCondition.SortField, sortCondition.SortDirection);
    }

    /// <summary>
    /// 对 <see cref="IEnumerable{T}"/> 进行后续排序
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">原始数据源</param>
    /// <param name="sortCondition">排序条件</param>
    /// <returns>排序后的数据</returns>
    public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source, SortConditionDto<T> sortCondition)
    {
        return CollectionPropertySorter<T>.ThenBy(source, sortCondition.SortField, sortCondition.SortDirection);
    }

    /// <summary>
    /// 对 <see cref="IEnumerable{T}"/> 进行多条件排序
    /// </summary>
    /// <typeparam name="T">集合中的元素类型</typeparam>
    /// <param name="source">要排序的集合</param>
    /// <param name="sortConditions">排序条件集合</param>
    /// <returns>排序后的数据</returns>
    public static IOrderedEnumerable<T> OrderByMultiple<T>(this IEnumerable<T> source, IEnumerable<SortConditionDto> sortConditions)
    {
        return CollectionPropertySorter<T>.OrderBy(source, sortConditions);
    }

    /// <summary>
    /// 对 <see cref="IEnumerable{T}"/> 进行多条件排序
    /// </summary>
    /// <typeparam name="T">集合中的元素类型</typeparam>
    /// <param name="source">要排序的集合</param>
    /// <param name="sortConditions">排序条件集合</param>
    /// <returns>排序后的数据</returns>
    public static IOrderedEnumerable<T> OrderByMultiple<T>(this IEnumerable<T> source, IEnumerable<SortConditionDto<T>> sortConditions)
    {
        return CollectionPropertySorter<T>.OrderBy(source, sortConditions);
    }

    #endregion
}
