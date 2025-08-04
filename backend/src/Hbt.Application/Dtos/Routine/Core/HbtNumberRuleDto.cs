#nullable enable

using System.ComponentModel.DataAnnotations;
using System.IO;
using Hbt.Common.Models;

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNumberRuleDto.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 单号规则数据传输对象
// 版权    : Copyright © 2024 Lean365. All rights reserved.
//===================================================================

namespace Hbt.Application.Dtos.Routine.Core
{
    /// <summary>
    /// 单号规则基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// 说明: 单号规则基础数据传输对象
    /// </remarks>
    public class HbtNumberRuleDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long NumberRuleId { get; set; }

        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>规则编号</summary>
        public string NumberRuleCode { get; set; } = string.Empty;

        /// <summary>规则名称</summary>
        public string NumberRuleName { get; set; } = string.Empty;

        /// <summary>规则类型(1=采购单 2=销售单 3=生产单 4=入库单 5=出库单 6=调拨单 7=盘点单 8=报损单 9=其他)</summary>
        public int NumberRuleType { get; set; } = 1;

        /// <summary>规则描述</summary>
        public string? NumberRuleDescription { get; set; }

        /// <summary>前缀</summary>
        public string? NumberPrefix { get; set; }

        /// <summary>后缀</summary>
        public string? NumberSuffix { get; set; }

        /// <summary>日期格式</summary>
        public string DateFormat { get; set; } = "yyyyMMdd";

        /// <summary>序列号长度</summary>
        public int SequenceLength { get; set; } = 4;

        /// <summary>序列号起始值</summary>
        public int SequenceStart { get; set; } = 1;

        /// <summary>序列号步长</summary>
        public int SequenceStep { get; set; } = 1;

        /// <summary>当前序列号</summary>
        public int CurrentSequence { get; set; } = 0;

        /// <summary>序列号重置规则(1=不重置 2=按年重置 3=按月重置 4=按日重置)</summary>
        public int SequenceResetRule { get; set; } = 1;

        /// <summary>最后重置日期</summary>
        public DateTime? LastResetDate { get; set; }

        /// <summary>格式模板</summary>
        public string NumberFormatTemplate { get; set; } = "{PREFIX}{DATE}{SEQUENCE}{SUFFIX}";

        /// <summary>示例</summary>
        public string? NumberExample { get; set; }

        /// <summary>是否包含公司代码(0=否 1=是)</summary>
        public int IncludeCompanyCode { get; set; } = 0;

        /// <summary>是否包含部门代码(0=否 1=是)</summary>
        public int IncludeDepartmentCode { get; set; } = 0;

        /// <summary>是否包含用户代码(0=否 1=是)</summary>
        public int IncludeUserCode { get; set; } = 0;

        /// <summary>是否包含年份(0=否 1=是)</summary>
        public int IncludeYear { get; set; } = 1;

        /// <summary>是否包含月份(0=否 1=是)</summary>
        public int IncludeMonth { get; set; } = 1;

        /// <summary>是否包含日期(0=否 1=是)</summary>
        public int IncludeDay { get; set; } = 1;

        /// <summary>是否包含小时(0=否 1=是)</summary>
        public int IncludeHour { get; set; } = 0;

        /// <summary>是否包含分钟(0=否 1=是)</summary>
        public int IncludeMinute { get; set; } = 0;

        /// <summary>是否包含秒(0=否 1=是)</summary>
        public int IncludeSecond { get; set; } = 0;

        /// <summary>是否包含毫秒(0=否 1=是)</summary>
        public int IncludeMillisecond { get; set; } = 0;

        /// <summary>是否包含随机数(0=否 1=是)</summary>
        public int IncludeRandom { get; set; } = 0;

        /// <summary>随机数长度</summary>
        public int RandomLength { get; set; } = 2;

        /// <summary>是否包含校验位(0=否 1=是)</summary>
        public int IncludeCheckDigit { get; set; } = 0;

        /// <summary>校验位算法(1=简单求和 2=加权求和 3=模运算)</summary>
        public int CheckDigitAlgorithm { get; set; } = 1;

        /// <summary>是否允许重复(0=否 1=是)</summary>
        public int AllowDuplicate { get; set; } = 0;

        /// <summary>重复检查范围(1=全局 2=按年 3=按月 4=按日)</summary>
        public int DuplicateCheckScope { get; set; } = 1;

        /// <summary>最后使用时间</summary>
        public DateTime? LastUsedTime { get; set; }

        /// <summary>使用次数</summary>
        public int UsageCount { get; set; } = 0;

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规则状态(0=正常 1=停用 2=维护中)</summary>
        public int Status { get; set; } = 0;

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>创建人</summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>更新时间</summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>更新人</summary>
        public string? UpdateBy { get; set; }

        /// <summary>是否删除</summary>
        public int IsDeleted { get; set; } = 0;
    }

    /// <summary>
    /// 单号规则查询DTO
    /// </summary>
    public class HbtNumberRuleQueryDto : HbtPagedQuery
    {
        /// <summary>公司代码</summary>
        public string? CompanyCode { get; set; }

        /// <summary>规则编号</summary>
        public string? NumberRuleCode { get; set; }

        /// <summary>规则名称</summary>
        public string? NumberRuleName { get; set; }

        /// <summary>规则类型(1=采购单 2=销售单 3=生产单 4=入库单 5=出库单 6=调拨单 7=盘点单 8=报损单 9=其他)</summary>
        public int? NumberRuleType { get; set; }

        /// <summary>规则描述</summary>
        public string? NumberRuleDescription { get; set; }

        /// <summary>前缀</summary>
        public string? NumberPrefix { get; set; }

        /// <summary>后缀</summary>
        public string? NumberSuffix { get; set; }

        /// <summary>日期格式</summary>
        public string? DateFormat { get; set; }

        /// <summary>序列号长度</summary>
        public int? SequenceLength { get; set; }

        /// <summary>序列号起始值</summary>
        public int? SequenceStart { get; set; }

        /// <summary>序列号步长</summary>
        public int? SequenceStep { get; set; }

        /// <summary>当前序列号</summary>
        public int? CurrentSequence { get; set; }

        /// <summary>序列号重置规则(1=不重置 2=按年重置 3=按月重置 4=按日重置)</summary>
        public int? SequenceResetRule { get; set; }

        /// <summary>最后重置日期</summary>
        public DateTime? LastResetDate { get; set; }

        /// <summary>格式模板</summary>
        public string? NumberFormatTemplate { get; set; }

        /// <summary>示例</summary>
        public string? NumberExample { get; set; }

        /// <summary>是否包含公司代码(0=否 1=是)</summary>
        public int? IncludeCompanyCode { get; set; }

        /// <summary>是否包含部门代码(0=否 1=是)</summary>
        public int? IncludeDepartmentCode { get; set; }

        /// <summary>是否包含用户代码(0=否 1=是)</summary>
        public int? IncludeUserCode { get; set; }

        /// <summary>是否包含年份(0=否 1=是)</summary>
        public int? IncludeYear { get; set; }

        /// <summary>是否包含月份(0=否 1=是)</summary>
        public int? IncludeMonth { get; set; }

        /// <summary>是否包含日期(0=否 1=是)</summary>
        public int? IncludeDay { get; set; }

        /// <summary>是否包含小时(0=否 1=是)</summary>
        public int? IncludeHour { get; set; }

        /// <summary>是否包含分钟(0=否 1=是)</summary>
        public int? IncludeMinute { get; set; }

        /// <summary>是否包含秒(0=否 1=是)</summary>
        public int? IncludeSecond { get; set; }

        /// <summary>是否包含毫秒(0=否 1=是)</summary>
        public int? IncludeMillisecond { get; set; }

        /// <summary>是否包含随机数(0=否 1=是)</summary>
        public int? IncludeRandom { get; set; }

        /// <summary>随机数长度</summary>
        public int? RandomLength { get; set; }

        /// <summary>是否包含校验位(0=否 1=是)</summary>
        public int? IncludeCheckDigit { get; set; }

        /// <summary>校验位算法(1=简单求和 2=加权求和 3=模运算)</summary>
        public int? CheckDigitAlgorithm { get; set; }

        /// <summary>是否允许重复(0=否 1=是)</summary>
        public int? AllowDuplicate { get; set; }

        /// <summary>重复检查范围(1=全局 2=按年 3=按月 4=按日)</summary>
        public int? DuplicateCheckScope { get; set; }

        /// <summary>最后使用时间</summary>
        public DateTime? LastUsedTime { get; set; }

        /// <summary>使用次数</summary>
        public int? UsageCount { get; set; }

        /// <summary>排序号</summary>
        public int? OrderNum { get; set; }

        /// <summary>规则状态(0=正常 1=停用 2=维护中)</summary>
        public int? Status { get; set; }

        /// <summary>开始时间</summary>
        public DateTime? StartTime { get; set; }

        /// <summary>结束时间</summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 单号规则新增DTO
    /// </summary>
    public class HbtNumberRuleCreateDto
    {
        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>规则编号</summary>
        public string NumberRuleCode { get; set; } = string.Empty;

        /// <summary>规则名称</summary>
        public string NumberRuleName { get; set; } = string.Empty;

        /// <summary>规则类型(1=采购单 2=销售单 3=生产单 4=入库单 5=出库单 6=调拨单 7=盘点单 8=报损单 9=其他)</summary>
        public int NumberRuleType { get; set; } = 1;

        /// <summary>规则描述</summary>
        public string? NumberRuleDescription { get; set; }

        /// <summary>前缀</summary>
        public string? NumberPrefix { get; set; }

        /// <summary>后缀</summary>
        public string? NumberSuffix { get; set; }

        /// <summary>日期格式</summary>
        public string DateFormat { get; set; } = "yyyyMMdd";

        /// <summary>序列号长度</summary>
        public int SequenceLength { get; set; } = 4;

        /// <summary>序列号起始值</summary>
        public int SequenceStart { get; set; } = 1;

        /// <summary>序列号步长</summary>
        public int SequenceStep { get; set; } = 1;

        /// <summary>当前序列号</summary>
        public int CurrentSequence { get; set; } = 0;

        /// <summary>序列号重置规则(1=不重置 2=按年重置 3=按月重置 4=按日重置)</summary>
        public int SequenceResetRule { get; set; } = 1;

        /// <summary>最后重置日期</summary>
        public DateTime? LastResetDate { get; set; }

        /// <summary>格式模板</summary>
        public string NumberFormatTemplate { get; set; } = "{PREFIX}{DATE}{SEQUENCE}{SUFFIX}";

        /// <summary>示例</summary>
        public string? NumberExample { get; set; }

        /// <summary>是否包含公司代码(0=否 1=是)</summary>
        public int IncludeCompanyCode { get; set; } = 0;

        /// <summary>是否包含部门代码(0=否 1=是)</summary>
        public int IncludeDepartmentCode { get; set; } = 0;

        /// <summary>是否包含用户代码(0=否 1=是)</summary>
        public int IncludeUserCode { get; set; } = 0;

        /// <summary>是否包含年份(0=否 1=是)</summary>
        public int IncludeYear { get; set; } = 1;

        /// <summary>是否包含月份(0=否 1=是)</summary>
        public int IncludeMonth { get; set; } = 1;

        /// <summary>是否包含日期(0=否 1=是)</summary>
        public int IncludeDay { get; set; } = 1;

        /// <summary>是否包含小时(0=否 1=是)</summary>
        public int IncludeHour { get; set; } = 0;

        /// <summary>是否包含分钟(0=否 1=是)</summary>
        public int IncludeMinute { get; set; } = 0;

        /// <summary>是否包含秒(0=否 1=是)</summary>
        public int IncludeSecond { get; set; } = 0;

        /// <summary>是否包含毫秒(0=否 1=是)</summary>
        public int IncludeMillisecond { get; set; } = 0;

        /// <summary>是否包含随机数(0=否 1=是)</summary>
        public int IncludeRandom { get; set; } = 0;

        /// <summary>随机数长度</summary>
        public int RandomLength { get; set; } = 2;

        /// <summary>是否包含校验位(0=否 1=是)</summary>
        public int IncludeCheckDigit { get; set; } = 0;

        /// <summary>校验位算法(1=简单求和 2=加权求和 3=模运算)</summary>
        public int CheckDigitAlgorithm { get; set; } = 1;

        /// <summary>是否允许重复(0=否 1=是)</summary>
        public int AllowDuplicate { get; set; } = 0;

        /// <summary>重复检查范围(1=全局 2=按年 3=按月 4=按日)</summary>
        public int DuplicateCheckScope { get; set; } = 1;

        /// <summary>最后使用时间</summary>
        public DateTime? LastUsedTime { get; set; }

        /// <summary>使用次数</summary>
        public int UsageCount { get; set; } = 0;

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规则状态(0=正常 1=停用 2=维护中)</summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 单号规则更新DTO
    /// </summary>
    public class HbtNumberRuleUpdateDto : HbtNumberRuleCreateDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long NumberRuleId { get; set; }
    }

    /// <summary>
    /// 单号规则删除DTO
    /// </summary>
    public class HbtNumberRuleDeleteDto
    {
        /// <summary>主键ID</summary>
          [AdaptMember("Id")]
        public long NumberRuleId { get; set; }
    }

    /// <summary>
    /// 单号规则批量删除DTO
    /// </summary>
    public class HbtNumberRuleBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> Ids { get; set; } = new List<long>();
    }

    /// <summary>
    /// 单号规则模板DTO
    /// </summary>
    public class HbtNumberRuleTemplateDto
    {
        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>规则编号</summary>
        public string NumberRuleCode { get; set; } = string.Empty;

        /// <summary>规则名称</summary>
        public string NumberRuleName { get; set; } = string.Empty;

        /// <summary>规则类型(1=采购单 2=销售单 3=生产单 4=入库单 5=出库单 6=调拨单 7=盘点单 8=报损单 9=其他)</summary>
        public int NumberRuleType { get; set; } = 1;

        /// <summary>规则描述</summary>
        public string? NumberRuleDescription { get; set; }

        /// <summary>前缀</summary>
        public string? NumberPrefix { get; set; }

        /// <summary>后缀</summary>
        public string? NumberSuffix { get; set; }

        /// <summary>日期格式</summary>
        public string DateFormat { get; set; } = "yyyyMMdd";

        /// <summary>序列号长度</summary>
        public int SequenceLength { get; set; } = 4;

        /// <summary>序列号起始值</summary>
        public int SequenceStart { get; set; } = 1;

        /// <summary>序列号步长</summary>
        public int SequenceStep { get; set; } = 1;

        /// <summary>当前序列号</summary>
        public int CurrentSequence { get; set; } = 0;

        /// <summary>序列号重置规则(1=不重置 2=按年重置 3=按月重置 4=按日重置)</summary>
        public int SequenceResetRule { get; set; } = 1;

        /// <summary>最后重置日期</summary>
        public DateTime? LastResetDate { get; set; }

        /// <summary>格式模板</summary>
        public string NumberFormatTemplate { get; set; } = "{PREFIX}{DATE}{SEQUENCE}{SUFFIX}";

        /// <summary>示例</summary>
        public string? NumberExample { get; set; }

        /// <summary>是否包含公司代码(0=否 1=是)</summary>
        public int IncludeCompanyCode { get; set; } = 0;

        /// <summary>是否包含部门代码(0=否 1=是)</summary>
        public int IncludeDepartmentCode { get; set; } = 0;

        /// <summary>是否包含用户代码(0=否 1=是)</summary>
        public int IncludeUserCode { get; set; } = 0;

        /// <summary>是否包含年份(0=否 1=是)</summary>
        public int IncludeYear { get; set; } = 1;

        /// <summary>是否包含月份(0=否 1=是)</summary>
        public int IncludeMonth { get; set; } = 1;

        /// <summary>是否包含日期(0=否 1=是)</summary>
        public int IncludeDay { get; set; } = 1;

        /// <summary>是否包含小时(0=否 1=是)</summary>
        public int IncludeHour { get; set; } = 0;

        /// <summary>是否包含分钟(0=否 1=是)</summary>
        public int IncludeMinute { get; set; } = 0;

        /// <summary>是否包含秒(0=否 1=是)</summary>
        public int IncludeSecond { get; set; } = 0;

        /// <summary>是否包含毫秒(0=否 1=是)</summary>
        public int IncludeMillisecond { get; set; } = 0;

        /// <summary>是否包含随机数(0=否 1=是)</summary>
        public int IncludeRandom { get; set; } = 0;

        /// <summary>随机数长度</summary>
        public int RandomLength { get; set; } = 2;

        /// <summary>是否包含校验位(0=否 1=是)</summary>
        public int IncludeCheckDigit { get; set; } = 0;

        /// <summary>校验位算法(1=简单求和 2=加权求和 3=模运算)</summary>
        public int CheckDigitAlgorithm { get; set; } = 1;

        /// <summary>是否允许重复(0=否 1=是)</summary>
        public int AllowDuplicate { get; set; } = 0;

        /// <summary>重复检查范围(1=全局 2=按年 3=按月 4=按日)</summary>
        public int DuplicateCheckScope { get; set; } = 1;

        /// <summary>最后使用时间</summary>
        public DateTime? LastUsedTime { get; set; }

        /// <summary>使用次数</summary>
        public int UsageCount { get; set; } = 0;

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规则状态(0=正常 1=停用 2=维护中)</summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 单号规则导入DTO
    /// </summary>
    public class HbtNumberRuleImportDto
    {
        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>规则编号</summary>
        public string NumberRuleCode { get; set; } = string.Empty;

        /// <summary>规则名称</summary>
        public string NumberRuleName { get; set; } = string.Empty;

        /// <summary>规则类型(1=采购单 2=销售单 3=生产单 4=入库单 5=出库单 6=调拨单 7=盘点单 8=报损单 9=其他)</summary>
        public int NumberRuleType { get; set; } = 1;

        /// <summary>规则描述</summary>
        public string? NumberRuleDescription { get; set; }

        /// <summary>前缀</summary>
        public string? NumberPrefix { get; set; }

        /// <summary>后缀</summary>
        public string? NumberSuffix { get; set; }

        /// <summary>日期格式</summary>
        public string DateFormat { get; set; } = "yyyyMMdd";

        /// <summary>序列号长度</summary>
        public int SequenceLength { get; set; } = 4;

        /// <summary>序列号起始值</summary>
        public int SequenceStart { get; set; } = 1;

        /// <summary>序列号步长</summary>
        public int SequenceStep { get; set; } = 1;

        /// <summary>当前序列号</summary>
        public int CurrentSequence { get; set; } = 0;

        /// <summary>序列号重置规则(1=不重置 2=按年重置 3=按月重置 4=按日重置)</summary>
        public int SequenceResetRule { get; set; } = 1;

        /// <summary>最后重置日期</summary>
        public DateTime? LastResetDate { get; set; }

        /// <summary>格式模板</summary>
        public string NumberFormatTemplate { get; set; } = "{PREFIX}{DATE}{SEQUENCE}{SUFFIX}";

        /// <summary>示例</summary>
        public string? NumberExample { get; set; }

        /// <summary>是否包含公司代码(0=否 1=是)</summary>
        public int IncludeCompanyCode { get; set; } = 0;

        /// <summary>是否包含部门代码(0=否 1=是)</summary>
        public int IncludeDepartmentCode { get; set; } = 0;

        /// <summary>是否包含用户代码(0=否 1=是)</summary>
        public int IncludeUserCode { get; set; } = 0;

        /// <summary>是否包含年份(0=否 1=是)</summary>
        public int IncludeYear { get; set; } = 1;

        /// <summary>是否包含月份(0=否 1=是)</summary>
        public int IncludeMonth { get; set; } = 1;

        /// <summary>是否包含日期(0=否 1=是)</summary>
        public int IncludeDay { get; set; } = 1;

        /// <summary>是否包含小时(0=否 1=是)</summary>
        public int IncludeHour { get; set; } = 0;

        /// <summary>是否包含分钟(0=否 1=是)</summary>
        public int IncludeMinute { get; set; } = 0;

        /// <summary>是否包含秒(0=否 1=是)</summary>
        public int IncludeSecond { get; set; } = 0;

        /// <summary>是否包含毫秒(0=否 1=是)</summary>
        public int IncludeMillisecond { get; set; } = 0;

        /// <summary>是否包含随机数(0=否 1=是)</summary>
        public int IncludeRandom { get; set; } = 0;

        /// <summary>随机数长度</summary>
        public int RandomLength { get; set; } = 2;

        /// <summary>是否包含校验位(0=否 1=是)</summary>
        public int IncludeCheckDigit { get; set; } = 0;

        /// <summary>校验位算法(1=简单求和 2=加权求和 3=模运算)</summary>
        public int CheckDigitAlgorithm { get; set; } = 1;

        /// <summary>是否允许重复(0=否 1=是)</summary>
        public int AllowDuplicate { get; set; } = 0;

        /// <summary>重复检查范围(1=全局 2=按年 3=按月 4=按日)</summary>
        public int DuplicateCheckScope { get; set; } = 1;

        /// <summary>最后使用时间</summary>
        public DateTime? LastUsedTime { get; set; }

        /// <summary>使用次数</summary>
        public int UsageCount { get; set; } = 0;

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规则状态(0=正常 1=停用 2=维护中)</summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 单号规则导出DTO
    /// </summary>
    public class HbtNumberRuleExportDto
    {
        /// <summary>主键ID</summary>
        public long Id { get; set; }

        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>规则编号</summary>
        public string NumberRuleCode { get; set; } = string.Empty;

        /// <summary>规则名称</summary>
        public string NumberRuleName { get; set; } = string.Empty;

        /// <summary>规则类型(1=采购单 2=销售单 3=生产单 4=入库单 5=出库单 6=调拨单 7=盘点单 8=报损单 9=其他)</summary>
        public int NumberRuleType { get; set; } = 1;

        /// <summary>规则描述</summary>
        public string? NumberRuleDescription { get; set; }

        /// <summary>前缀</summary>
        public string? NumberPrefix { get; set; }

        /// <summary>后缀</summary>
        public string? NumberSuffix { get; set; }

        /// <summary>日期格式</summary>
        public string DateFormat { get; set; } = "yyyyMMdd";

        /// <summary>序列号长度</summary>
        public int SequenceLength { get; set; } = 4;

        /// <summary>序列号起始值</summary>
        public int SequenceStart { get; set; } = 1;

        /// <summary>序列号步长</summary>
        public int SequenceStep { get; set; } = 1;

        /// <summary>当前序列号</summary>
        public int CurrentSequence { get; set; } = 0;

        /// <summary>序列号重置规则(1=不重置 2=按年重置 3=按月重置 4=按日重置)</summary>
        public int SequenceResetRule { get; set; } = 1;

        /// <summary>最后重置日期</summary>
        public DateTime? LastResetDate { get; set; }

        /// <summary>格式模板</summary>
        public string NumberFormatTemplate { get; set; } = "{PREFIX}{DATE}{SEQUENCE}{SUFFIX}";

        /// <summary>示例</summary>
        public string? NumberExample { get; set; }

        /// <summary>是否包含公司代码(0=否 1=是)</summary>
        public int IncludeCompanyCode { get; set; } = 0;

        /// <summary>是否包含部门代码(0=否 1=是)</summary>
        public int IncludeDepartmentCode { get; set; } = 0;

        /// <summary>是否包含用户代码(0=否 1=是)</summary>
        public int IncludeUserCode { get; set; } = 0;

        /// <summary>是否包含年份(0=否 1=是)</summary>
        public int IncludeYear { get; set; } = 1;

        /// <summary>是否包含月份(0=否 1=是)</summary>
        public int IncludeMonth { get; set; } = 1;

        /// <summary>是否包含日期(0=否 1=是)</summary>
        public int IncludeDay { get; set; } = 1;

        /// <summary>是否包含小时(0=否 1=是)</summary>
        public int IncludeHour { get; set; } = 0;

        /// <summary>是否包含分钟(0=否 1=是)</summary>
        public int IncludeMinute { get; set; } = 0;

        /// <summary>是否包含秒(0=否 1=是)</summary>
        public int IncludeSecond { get; set; } = 0;

        /// <summary>是否包含毫秒(0=否 1=是)</summary>
        public int IncludeMillisecond { get; set; } = 0;

        /// <summary>是否包含随机数(0=否 1=是)</summary>
        public int IncludeRandom { get; set; } = 0;

        /// <summary>随机数长度</summary>
        public int RandomLength { get; set; } = 2;

        /// <summary>是否包含校验位(0=否 1=是)</summary>
        public int IncludeCheckDigit { get; set; } = 0;

        /// <summary>校验位算法(1=简单求和 2=加权求和 3=模运算)</summary>
        public int CheckDigitAlgorithm { get; set; } = 1;

        /// <summary>是否允许重复(0=否 1=是)</summary>
        public int AllowDuplicate { get; set; } = 0;

        /// <summary>重复检查范围(1=全局 2=按年 3=按月 4=按日)</summary>
        public int DuplicateCheckScope { get; set; } = 1;

        /// <summary>最后使用时间</summary>
        public DateTime? LastUsedTime { get; set; }

        /// <summary>使用次数</summary>
        public int UsageCount { get; set; } = 0;

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规则状态(0=正常 1=停用 2=维护中)</summary>
        public int Status { get; set; } = 0;

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>创建人</summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>更新时间</summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>更新人</summary>
        public string? UpdateBy { get; set; }
    }

    /// <summary>
    /// 单号规则状态DTO
    /// </summary>
    public class HbtNumberRuleStatusDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long NumberRuleId { get; set; }

        /// <summary>规则状态(0=正常 1=停用 2=维护中)</summary>
        public int Status { get; set; } = 0;
    }


} 