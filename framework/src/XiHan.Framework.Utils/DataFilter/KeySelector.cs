﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// FileName:KeySelector
// Guid:03f8c708-54fe-4b89-9368-97ddbbda5b7a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/11/28 4:40:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using XiHan.Framework.Utils.Text;

namespace XiHan.Framework.Utils.DataFilter;

/// <summary>
/// 键选择器
/// </summary>
public static class KeySelector<T>
{
    /// <summary>
    /// 键选择器缓存
    /// </summary>
    private static readonly ConcurrentDictionary<string, LambdaExpression> KeySelectorCache = new();

    /// <summary>
    /// 获取键选择器
    /// </summary>
    /// <param name="keyName">键名称</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static LambdaExpression GetKeySelector(string keyName)
    {
        var type = typeof(T);
        var key = $"{type.FullName}.{keyName}";
        if (KeySelectorCache.TryGetValue(key, out var keySelector))
        {
            return keySelector;
        }

        var param = Expression.Parameter(type);
        var propertyNames = keyName.Split('.');
        Expression propertyAccess = param;
        foreach (var propertyName in propertyNames)
        {
            var property = FindProperty(type, propertyName);
            type = property.PropertyType;
            propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
        }

        keySelector = Expression.Lambda(propertyAccess, param);
        _ = KeySelectorCache.TryAdd(key, keySelector);
        return keySelector;
    }

    /// <summary>
    /// 从泛型委托获取属性名
    /// </summary>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string GetPropertyName(Expression<Func<T, object>> keySelector)
    {
        if (keySelector.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        if (keySelector.Body is UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MemberExpression operand)
            {
                return operand.Member.Name;
            }
        }
        throw new InvalidOperationException("无法从键选择器中获取属性名称。");
    }

    #region 私有方法

    /// <summary>
    /// 获取属性
    /// </summary>
    /// <param name="type"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    private static PropertyInfo FindProperty(Type type, string propertyName)
    {
        var strategies = new Func<string, string>[]
        {
            // 原始名称
            name => name,
            // 驼峰命名
            name => name.ToPascalCase(),
            // 蛇形命名
            name => name.ToSnakeCase(),
            // 短横线命名
            name => name.ToKebabCase()
        };

        foreach (var strategy in strategies)
        {
            var transformedName = strategy(propertyName);
            var property = type.GetProperty(transformedName);
            if (property != null)
            {
                return property;
            }
        }

        throw new ArgumentException($"在类型 {type.Name} 中没有发现属性 {propertyName}。");
    }

    #endregion
}
