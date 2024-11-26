﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// FileName:SelectCompareEnum
// Guid:ed708176-466d-46db-8b73-3fff5f658c33
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/11/27 6:34:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Framework.Utils.DataFilter.Enums;

/// <summary>
/// 选择比较枚举
/// </summary>
public enum SelectCompareEnum
{
    /// <summary>
    /// 包含
    /// </summary>
    [Description("包含")]
    Contains,

    /// <summary>
    /// 等于
    /// </summary>
    [Description("等于")]
    Equal,

    /// <summary>
    /// 大于
    /// </summary>
    [Description("大于")]
    Greater,

    /// <summary>
    /// 大于等于
    /// </summary>
    [Description("大于等于")]
    GreaterEqual,

    /// <summary>
    /// 小于
    /// </summary>
    [Description("小于")]
    Less,

    /// <summary>
    /// 小于等于
    /// </summary>
    [Description("小于等于")]
    LessEqual,

    /// <summary>
    /// 不等于
    /// </summary>
    [Description("不等于")]
    NotEqual,

    /// <summary>
    /// 多个值执行包含比较
    /// </summary>
    [Description("多个值执行包含比较")]
    InWithContains,

    /// <summary>
    /// 多个值执行等于比较
    /// </summary>
    [Description("多个值执行等于比较")]
    InWithEqual,

    /// <summary>
    /// 在...之间
    /// </summary>
    [Description("在...之间")]
    Between,
}
