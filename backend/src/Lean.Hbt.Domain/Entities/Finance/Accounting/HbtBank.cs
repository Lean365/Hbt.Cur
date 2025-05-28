#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtBank.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述    : 银行实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Finance.Accounting
{
    /// <summary>
    /// 银行实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("hbt_bank", "银行表")]
    [SugarIndex("ix_tenant_bank", nameof(TenantId), OrderByType.Asc, nameof(BankCode), OrderByType.Asc, true)]
    public class HbtBank : HbtBaseEntity
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TenantId))]
        public HbtTenant? Tenant { get; set; }

        /// <summary>
        /// 银行编码
        /// </summary>
        [SugarColumn(ColumnName = "bank_code", ColumnDescription = "银行编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? BankCode { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        [SugarColumn(ColumnName = "bank_name", ColumnDescription = "银行名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? BankName { get; set; }

        /// <summary>
        /// 银行简称
        /// </summary>
        [SugarColumn(ColumnName = "bank_short_name", ColumnDescription = "银行简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankShortName { get; set; }

        /// <summary>
        /// 银行类型
        /// </summary>
        [SugarColumn(ColumnName = "bank_type", ColumnDescription = "银行类型", ColumnDataType = "int", IsNullable = false)]
        public int BankType { get; set; }

        /// <summary>
        /// 银行级别
        /// </summary>
        [SugarColumn(ColumnName = "bank_level", ColumnDescription = "银行级别", ColumnDataType = "int", IsNullable = false)]
        public int BankLevel { get; set; }

        /// <summary>
        /// 上级银行ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "上级银行ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 上级银行
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public HbtBank? Parent { get; set; }

        /// <summary>
        /// 银行地址
        /// </summary>
        [SugarColumn(ColumnName = "bank_address", ColumnDescription = "银行地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankAddress { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [SugarColumn(ColumnName = "email", ColumnDescription = "电子邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Email { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=停用 1=正常)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Remark { get; set; }
    }
} 