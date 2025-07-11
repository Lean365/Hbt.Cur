//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtIsoDocumentService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述   : ISO文档服务接口
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtIsoDocumentService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V1.0.0
// 描述   : ISO文档服务接口
//===================================================================

using Lean.Hbt.Application.Dtos.Routine.Document.Iso;

namespace Lean.Hbt.Application.Services.Routine.Document.Regulations
{
    /// <summary>
    /// ISO文档服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtIsoDocumentService
    {
        /// <summary>
        /// 获取ISO文档分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>ISO文档分页列表</returns>
        Task<HbtPagedResult<HbtIsoDocumentDto>> GetListAsync(HbtIsoDocumentQueryDto query);

        /// <summary>
        /// 获取ISO文档详情
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>ISO文档详情</returns>
        Task<HbtIsoDocumentDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建ISO文档
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文档ID</returns>
        Task<long> CreateAsync(HbtIsoDocumentCreateDto input);

        /// <summary>
        /// 更新ISO文档
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtIsoDocumentUpdateDto input);

        /// <summary>
        /// 删除ISO文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除ISO文档
        /// </summary>
        /// <param name="documentIds">文档ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] documentIds);

        /// <summary>
        /// 获取ISO文档树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        Task<List<HbtIsoDocumentDto>> GetTreeAsync(long? parentId = null);

        /// <summary>
        /// 导入ISO文档数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出ISO文档数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtIsoDocumentQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 处理ISO文档请求审批完成后的自动更新
        /// </summary>
        /// <param name="requestId">请求ID</param>
        /// <returns>更新结果</returns>
        Task<HbtApiResult<bool>> ProcessRequestCompletionAsync(long requestId);
    }
} 