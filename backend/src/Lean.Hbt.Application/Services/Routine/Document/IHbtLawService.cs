//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtLawService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 法律法规服务接口
//===================================================================

namespace Lean.Hbt.Application.Services.Routine.Document
{
    /// <summary>
    /// 法律法规服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtLawService
    {
        /// <summary>
        /// 获取法律法规分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>法律法规分页列表</returns>
        Task<HbtPagedResult<HbtLawDto>> GetListAsync(HbtLawQueryDto? query);

        /// <summary>
        /// 获取法律法规详情
        /// </summary>
        /// <param name="lawId">法律法规ID</param>
        /// <returns>法律法规详情</returns>
        Task<HbtLawDto> GetByIdAsync(long lawId);

        /// <summary>
        /// 创建法律法规
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>法律法规ID</returns>
        Task<long> CreateAsync(HbtLawCreateDto input);

        /// <summary>
        /// 更新法律法规
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtLawUpdateDto input);

        /// <summary>
        /// 删除法律法规
        /// </summary>
        /// <param name="lawId">法律法规ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long lawId);

        /// <summary>
        /// 批量删除法律法规
        /// </summary>
        /// <param name="lawIds">法律法规ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] lawIds);

        /// <summary>
        /// 获取法律法规树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        Task<List<HbtLawDto>> GetTreeAsync(long? parentId = null);

        /// <summary>
        /// 导入法律法规数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtLaw");

        /// <summary>
        /// 导出法律法规数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtLawQueryDto query, string sheetName = "HbtLaw");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtLaw");

        /// <summary>
        /// 更新法律法规状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(HbtLawStatusDto input);

        /// <summary>
        /// 从国家行政机关下载法律法规
        /// </summary>
        /// <param name="lawCode">法规编号</param>
        /// <returns>下载结果</returns>
        Task<HbtApiResult<bool>> DownloadFromGovernmentAsync(string lawCode);

        /// <summary>
        /// 批量下载法律法规
        /// </summary>
        /// <param name="lawCodes">法规编号集合</param>
        /// <returns>下载结果</returns>
        Task<HbtApiResult<(int success, int fail)>> BatchDownloadAsync(string[] lawCodes);
    }
} 