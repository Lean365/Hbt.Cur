//===================================================================
// 项目名 : Lean.Hbt.Application
// 文件名 : HbtUserTenantDto.cs
// 创建者 : Lean365
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述   : 用户租户关联DTO
//===================================================================

using System;
using System.Collections.Generic;

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 用户租户关联DTO
    /// </summary>
    public class HbtUserTenantDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long UserTenantId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 租户配置ID
        /// </summary>
        public string? ConfigId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 用户名称（导航属性）
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 租户名称（导航属性）
        /// </summary>
        public string? TenantName { get; set; }

        /// <summary>
        /// 租户配置ID集合（用于批量操作）
        /// </summary>
        public string[] ConfigIds { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 已分配租户列表
        /// </summary>
        public List<HbtTenantDto> AssignedTenants { get; set; } = new();

        /// <summary>
        /// 可选租户列表
        /// </summary>
        public List<HbtTenantDto> OptionalTenants { get; set; } = new();
    }
} 