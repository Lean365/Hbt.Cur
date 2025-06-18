//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtActivityService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流表单服务接口
//===================================================================
using System.IO;
using Lean.Hbt.Application.Dtos.Workflow;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流表单服务接口
    /// </summary>
    public interface IHbtFormService
    {
        /// <summary>
        /// 获取工作流表单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<HbtPagedResult<HbtFormDto>> GetListAsync(HbtFormQueryDto query);

        /// <summary>
        /// 获取工作流表单详情
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <returns>表单详情</returns>
        Task<HbtFormDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建工作流表单
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的表单ID</returns>
        Task<long> CreateAsync(HbtFormCreateDto input);

        /// <summary>
        /// 更新工作流表单
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtFormUpdateDto input);

        /// <summary>
        /// 删除工作流表单
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工作流表单
        /// </summary>
        /// <param name="ids">表单ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入工作流表单数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

        /// <summary>
        /// 导出工作流表单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtFormQueryDto query, string sheetName = "Sheet1");

        /// <summary>
        /// 获取工作流表单导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

        /// <summary>
        /// 获取指定工作流定义下的所有表单
        /// </summary>
        /// <param name="definitionId">工作流定义ID</param>
        /// <returns>表单列表</returns>
        Task<List<HbtFormDto>> GetFormsByWorkflowDefinitionAsync(long definitionId);

        /// <summary>
        /// 修改表单状态
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <param name="status">新状态</param>
        /// <returns>是否成功</returns>
        Task<bool> ChangeStatusAsync(long id, int status);

        /// <summary>
        /// 获取表单选项列表
        /// </summary>
        /// <returns>表单选项列表</returns>
        Task<List<HbtSelectOption>> GetOptionsAsync();
    }
}