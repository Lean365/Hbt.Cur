//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtNumberGeneratorService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 通用单号生成器服务接口
//===================================================================

using System.Threading.Tasks;
using Hbt.Cur.Application.Dtos.Routine.Core;

namespace Hbt.Cur.Application.Services.Core
{
    /// <summary>
    /// 通用单号生成器服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtNumberGeneratorService
    {
        /// <summary>
        /// 根据规则生成单号
        /// </summary>
        /// <param name="numberRuleCode">规则编号</param>
        /// <param name="companyCode">公司代码</param>
        /// <returns>生成的单号</returns>
        Task<string> GenerateNumberAsync(string numberRuleCode, string companyCode);

        /// <summary>
        /// 根据规则生成单号（带自定义参数）
        /// </summary>
        /// <param name="numberRuleCode">规则编号</param>
        /// <param name="companyCode">公司代码</param>
        /// <param name="customParams">自定义参数</param>
        /// <returns>生成的单号</returns>
        Task<string> GenerateNumberAsync(string numberRuleCode, string companyCode, HbtNumberGeneratorParams customParams);

        /// <summary>
        /// 验证单号是否已存在
        /// </summary>
        /// <param name="number">单号</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <returns>是否存在</returns>
        Task<bool> IsNumberExistsAsync(string number, string tableName, string columnName);

        /// <summary>
        /// 获取规则信息
        /// </summary>
        /// <param name="numberRuleCode">规则编号</param>
        /// <param name="companyCode">公司代码</param>
        /// <returns>规则信息</returns>
        Task<HbtNumberRuleDto> GetRuleAsync(string numberRuleCode, string companyCode);
    }

    /// <summary>
    /// 单号生成器参数
    /// </summary>
    public class HbtNumberGeneratorParams
    {
        /// <summary>部门代码</summary>
        public string? DepartmentCode { get; set; }

        /// <summary>用户代码</summary>
        public string? UserCode { get; set; }

        /// <summary>自定义前缀</summary>
        public string? CustomPrefix { get; set; }

        /// <summary>自定义后缀</summary>
        public string? CustomSuffix { get; set; }

        /// <summary>自定义日期</summary>
        public DateTime? CustomDate { get; set; }

        /// <summary>自定义序列号</summary>
        public int? CustomSequence { get; set; }

        /// <summary>自定义随机数</summary>
        public string? CustomRandom { get; set; }
    }
} 