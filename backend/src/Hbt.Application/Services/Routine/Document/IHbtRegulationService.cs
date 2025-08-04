//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtRegulationService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 规章制度服务接口
//===================================================================

namespace Hbt.Application.Services.Routine.Document.Regulations
{
    /// <summary>
    /// 规章制度服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface IHbtRegulationService
    {
        /// <summary>
        /// 获取规章制度分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>规章制度分页列表</returns>
        Task<HbtPagedResult<HbtRegulationDto>> GetListAsync(HbtRegulationQueryDto query);

        /// <summary>
        /// 获取规章制度详情
        /// </summary>
        /// <param name="id">规章制度ID</param>
        /// <returns>规章制度详情</returns>
        Task<HbtRegulationDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建规章制度
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>规章制度ID</returns>
        Task<long> CreateAsync(HbtRegulationCreateDto input);

        /// <summary>
        /// 更新规章制度
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtRegulationUpdateDto input);

        /// <summary>
        /// 删除规章制度
        /// </summary>
        /// <param name="id">规章制度ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除规章制度
        /// </summary>
        /// <param name="regulationIds">规章制度ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] regulationIds);

        /// <summary>
        /// 获取规章制度树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        Task<List<HbtRegulationDto>> GetTreeAsync(long? parentId = null);

        /// <summary>
        /// 导入规章制度数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出规章制度数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtRegulationQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);
    }
} 