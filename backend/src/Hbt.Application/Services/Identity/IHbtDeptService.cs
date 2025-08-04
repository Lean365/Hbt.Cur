//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtDeptService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 部门服务接口
//===================================================================

using Hbt.Application.Dtos.Identity;

namespace Hbt.Application.Services.Identity
{
    /// <summary>
    /// 部门服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface IHbtDeptService
    {
        /// <summary>
        /// 获取部门分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>部门分页列表</returns>
        Task<HbtPagedResult<HbtDeptDto>> GetListAsync(HbtDeptQueryDto query);

        /// <summary>
        /// 获取部门树形结构
        /// </summary>
        /// <param name="query">状态</param>
        /// <returns>部门树形结构</returns>
        Task<List<HbtDeptDto>> GetTreeAsync(HbtDeptQueryDto query);

        /// <summary>
        /// 获取部门详情
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>部门详情</returns>
        Task<HbtDeptDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>部门ID</returns>
        Task<long> CreateAsync(HbtDeptCreateDto input);

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(HbtDeptUpdateDto input);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除部门
        /// </summary>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] deptIds);

        /// <summary>
        /// 导入部门数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtDept");

        /// <summary>
        /// 导出部门数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(HbtDeptQueryDto query, string sheetName = "HbtDept");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "HbtDept");

        /// <summary>
        /// 修改部门状态
        /// </summary>
        Task<bool> UpdateStatusAsync(long deptId, int status);

        /// <summary>
        /// 获取部门选项列表
        /// </summary>
        /// <returns>部门选项列表</returns>
        Task<List<HbtSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 生成导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> GenerateTemplateAsync(string sheetName = "HbtDept");
    }
}