#nullable enable

using System.ComponentModel.DataAnnotations;
using SqlSugar;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtOAuth.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V.0.0.1
// 描述    : OAuth第三方登录账号实体类
//===================================================================
namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// OAuth第三方登录账号实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    [SugarTable("hbt_identity_oauth", TableDescription = "OAuth账号")]
    [SugarIndex("ix_user_provider", nameof(UserId),  OrderByType.Asc, nameof(Provider), OrderByType.Asc, true)]
    [SugarIndex("ix_provider_userid", nameof(Provider), OrderByType.Asc,  nameof(OAuthUserId), OrderByType.Asc, true)]
    public class HbtOAuth : HbtBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 第三方登录提供商
        /// </summary>
        [SugarColumn(ColumnName = "provider", ColumnDescription = "OAuth提供商", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户ID
        /// </summary>
        [SugarColumn(ColumnName = "oauth_user_id", ColumnDescription = "OAuth用户ID", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OAuthUserId { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户名
        /// </summary>
        [SugarColumn(ColumnName = "oauth_user_name", ColumnDescription = "OAuth用户名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OAuthUserName { get; set; } = string.Empty;

        /// <summary>
        /// OAuth用户昵称
        /// </summary>
        [SugarColumn(ColumnName = "oauth_nick_name", ColumnDescription = "OAuth用户昵称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OAuthNickName { get; set; }

        /// <summary>
        /// OAuth用户邮箱
        /// </summary>
        [SugarColumn(ColumnName = "oauth_email", ColumnDescription = "OAuth用户邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OAuthEmail { get; set; }

        /// <summary>
        /// OAuth用户头像
        /// </summary>
        [SugarColumn(ColumnName = "oauth_avatar", ColumnDescription = "OAuth用户头像", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OAuthAvatar { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        [SugarColumn(ColumnName = "bind_time", ColumnDescription = "绑定时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime BindTime { get; set; }

        /// <summary>
        /// 是否为主要账号（0否 1是）
        /// </summary>
        [SugarColumn(ColumnName = "is_primary", ColumnDescription = "是否为主要账号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPrimary { get; set; } = 0;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [SugarColumn(ColumnName = "last_login_time", ColumnDescription = "最后登录时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [SugarColumn(ColumnName = "login_count", ColumnDescription = "登录次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginCount { get; set; } = 0;

        /// <summary>
        /// 关联用户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public HbtUser? User { get; set; }
    }
} 