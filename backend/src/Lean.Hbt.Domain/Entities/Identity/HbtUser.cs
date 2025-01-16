//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtUser.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 11:30
// 版本号 : V0.0.1
// 描述    : 用户实体
//===================================================================

using SqlSugar;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Domain.Entities.Identity
{
    /// <summary>
    /// 用户实体
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("hbt_user", "用户表")]
    [SugarIndex("ix_user_name", nameof(UserName), OrderByType.Asc, true)]
    [SugarIndex("ix_email", nameof(Email), OrderByType.Asc, true)]
    [SugarIndex("ix_phone", nameof(PhoneNumber), OrderByType.Asc, true)]
    [SugarIndex("ix_tenant_user", $"{nameof(TenantId)},{nameof(UserName)}", OrderByType.Asc, true)]
    public class HbtUser : HbtBaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(ColumnName = "nick_name", ColumnDescription = "昵称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型（0系统用户 1普通用户）
        /// </summary>
        [SugarColumn(ColumnName = "user_type", ColumnDescription = "用户类型（0系统用户 1普通用户）", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public YesNo UserType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnName = "password", ColumnDescription = "密码", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Password { get; set; }

        /// <summary>
        /// 盐值
        /// </summary>
        [SugarColumn(ColumnName = "salt", ColumnDescription = "盐值", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Salt { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [SugarColumn(ColumnName = "phone_number", ColumnDescription = "手机号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 性别（0未知 1男 2女）
        /// </summary>
        [SugarColumn(ColumnName = "gender", ColumnDescription = "性别（0未知 1男 2女）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public Gender Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(ColumnName = "avatar", ColumnDescription = "头像", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string Avatar { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0正常 1停用）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public CommonStatus Status { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 用户角色关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtUserRole.UserId))]
        public List<HbtUserRole> UserRoles { get; set; }

        /// <summary>
        /// 用户部门关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtUserDept.UserId))]
        public List<HbtUserDept> UserDepts { get; set; }

        /// <summary>
        /// 用户岗位关联
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(HbtUserPost.UserId))]
        public List<HbtUserPost> UserPosts { get; set; }
    }
} 