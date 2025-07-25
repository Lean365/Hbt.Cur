#nullable enable

using SqlSugar;
using Lean.Hbt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtProjectContract.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目合同实体类
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Lean.Hbt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目合同实体类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目合同的相关信息，包括合同信息、条款、付款等
    /// </remarks>
    [SugarTable("hbt_routine_project_contract", "项目合同表")]
    [SugarIndex("ix_project_contract_code", nameof(ProjectContractCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_contract", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectContractCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_contract_status", nameof(ProjectContractStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_contract_type", nameof(ProjectContractType), OrderByType.Asc, false)]
    public class HbtProjectContract : HbtBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目合同编号</summary>
        [SugarColumn(ColumnName = "project_contract_code", ColumnDescription = "项目合同编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectContractCode { get; set; } = string.Empty;

        /// <summary>合同名称</summary>
        [SugarColumn(ColumnName = "contract_name", ColumnDescription = "合同名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractName { get; set; } = string.Empty;

        /// <summary>合同类型(1=总承包合同 2=分包合同 3=采购合同 4=服务合同 5=咨询合同 6=其他合同)</summary>
        [SugarColumn(ColumnName = "project_contract_type", ColumnDescription = "合同类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectContractType { get; set; } = 1;

        /// <summary>关联项目编号</summary>
        [SugarColumn(ColumnName = "related_project_code", ColumnDescription = "关联项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectCode { get; set; } = string.Empty;

        /// <summary>关联项目名称</summary>
        [SugarColumn(ColumnName = "related_project_name", ColumnDescription = "关联项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectName { get; set; } = string.Empty;

        /// <summary>合同描述</summary>
        [SugarColumn(ColumnName = "contract_description", ColumnDescription = "合同描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractDescription { get; set; }

        /// <summary>合同范围</summary>
        [SugarColumn(ColumnName = "contract_scope", ColumnDescription = "合同范围", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractScope { get; set; }

        /// <summary>合同金额</summary>
        [SugarColumn(ColumnName = "contract_amount", ColumnDescription = "合同金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ContractAmount { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>合同签订日期</summary>
        [SugarColumn(ColumnName = "contract_signing_date", ColumnDescription = "合同签订日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractSigningDate { get; set; }

        /// <summary>合同生效日期</summary>
        [SugarColumn(ColumnName = "contract_effective_date", ColumnDescription = "合同生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractEffectiveDate { get; set; }

        /// <summary>合同开始日期</summary>
        [SugarColumn(ColumnName = "contract_start_date", ColumnDescription = "合同开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractStartDate { get; set; }

        /// <summary>合同结束日期</summary>
        [SugarColumn(ColumnName = "contract_end_date", ColumnDescription = "合同结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractEndDate { get; set; }

        /// <summary>合同期限(天)</summary>
        [SugarColumn(ColumnName = "contract_duration", ColumnDescription = "合同期限(天)", ColumnDataType = "int", IsNullable = true)]
        public int? ContractDuration { get; set; }

        /// <summary>甲方名称</summary>
        [SugarColumn(ColumnName = "party_a_name", ColumnDescription = "甲方名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyAName { get; set; }

        /// <summary>甲方联系人</summary>
        [SugarColumn(ColumnName = "party_a_contact", ColumnDescription = "甲方联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyAContact { get; set; }

        /// <summary>甲方联系电话</summary>
        [SugarColumn(ColumnName = "party_a_phone", ColumnDescription = "甲方联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyAPhone { get; set; }

        /// <summary>甲方联系邮箱</summary>
        [SugarColumn(ColumnName = "party_a_email", ColumnDescription = "甲方联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyAEmail { get; set; }

        /// <summary>甲方地址</summary>
        [SugarColumn(ColumnName = "party_a_address", ColumnDescription = "甲方地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyAAddress { get; set; }

        /// <summary>乙方名称</summary>
        [SugarColumn(ColumnName = "party_b_name", ColumnDescription = "乙方名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyBName { get; set; }

        /// <summary>乙方联系人</summary>
        [SugarColumn(ColumnName = "party_b_contact", ColumnDescription = "乙方联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyBContact { get; set; }

        /// <summary>乙方联系电话</summary>
        [SugarColumn(ColumnName = "party_b_phone", ColumnDescription = "乙方联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyBPhone { get; set; }

        /// <summary>乙方联系邮箱</summary>
        [SugarColumn(ColumnName = "party_b_email", ColumnDescription = "乙方联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyBEmail { get; set; }

        /// <summary>乙方地址</summary>
        [SugarColumn(ColumnName = "party_b_address", ColumnDescription = "乙方地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartyBAddress { get; set; }

        /// <summary>合同负责人</summary>
        [SugarColumn(ColumnName = "contract_manager", ColumnDescription = "合同负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractManager { get; set; }

        /// <summary>合同负责人电话</summary>
        [SugarColumn(ColumnName = "contract_manager_phone", ColumnDescription = "合同负责人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractManagerPhone { get; set; }

        /// <summary>合同负责人邮箱</summary>
        [SugarColumn(ColumnName = "contract_manager_email", ColumnDescription = "合同负责人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractManagerEmail { get; set; }

        /// <summary>付款方式(1=一次性付款 2=分期付款 3=按进度付款 4=其他)</summary>
        [SugarColumn(ColumnName = "payment_method", ColumnDescription = "付款方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int PaymentMethod { get; set; } = 3;

        /// <summary>付款条件</summary>
        [SugarColumn(ColumnName = "payment_terms", ColumnDescription = "付款条件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentTerms { get; set; }

        /// <summary>已付款金额</summary>
        [SugarColumn(ColumnName = "paid_amount", ColumnDescription = "已付款金额", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PaidAmount { get; set; } = 0;

        /// <summary>未付款金额</summary>
        [SugarColumn(ColumnName = "unpaid_amount", ColumnDescription = "未付款金额", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnpaidAmount { get; set; } = 0;

        /// <summary>付款进度(%)</summary>
        [SugarColumn(ColumnName = "payment_progress", ColumnDescription = "付款进度(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PaymentProgress { get; set; } = 0;

        /// <summary>合同条款</summary>
        [SugarColumn(ColumnName = "contract_terms", ColumnDescription = "合同条款", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractTerms { get; set; }

        /// <summary>违约责任</summary>
        [SugarColumn(ColumnName = "breach_liability", ColumnDescription = "违约责任", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BreachLiability { get; set; }

        /// <summary>争议解决方式</summary>
        [SugarColumn(ColumnName = "dispute_resolution", ColumnDescription = "争议解决方式", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DisputeResolution { get; set; }

        /// <summary>合同附件</summary>
        [SugarColumn(ColumnName = "contract_attachments", ColumnDescription = "合同附件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractAttachments { get; set; }

        /// <summary>合同状态(0=草稿 1=待签订 2=已签订 3=执行中 4=已完成 5=已终止 6=已作废)</summary>
        [SugarColumn(ColumnName = "project_contract_status", ColumnDescription = "合同状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectContractStatus { get; set; } = 0;

        /// <summary>合同风险等级(1=低 2=中 3=高 4=极高)</summary>
        [SugarColumn(ColumnName = "contract_risk_level", ColumnDescription = "合同风险等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ContractRiskLevel { get; set; } = 2;

        /// <summary>合同备注</summary>
        [SugarColumn(ColumnName = "contract_remark", ColumnDescription = "合同备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 