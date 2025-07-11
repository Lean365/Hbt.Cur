//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : IHbtTenantService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-17 19:15
// 版本号 : V0.0.1
// 描述   : 租户服务接口
//===================================================================

namespace Lean.Hbt.Application.Services.Identity;

/// <summary>
/// 租户服务接口
/// </summary>
public interface IHbtTenantService
{
    /// <summary>
    /// 获取租户列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>租户列表</returns>
    Task<HbtPagedResult<HbtTenantDto>> GetListAsync(HbtTenantQueryDto query);

    /// <summary>
    /// 获取租户详情
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>租户详情</returns>
    Task<HbtTenantDto> GetByIdAsync(long id);

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>租户ID</returns>
    Task<long> CreateAsync(HbtTenantCreateDto input);

    /// <summary>
    /// 更新租户
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(HbtTenantUpdateDto input);

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除租户
    /// </summary>
    /// <param name="tenantIds">租户ID集合</param>
    /// <returns>是否成功</returns>
    Task<bool> BatchDeleteAsync(long[] tenantIds);

    /// <summary>
    /// 获取租户选项列表
    /// </summary>
    /// <returns>租户选项列表</returns>
    Task<List<HbtSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 根据用户名获取租户选项列表
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>租户选项列表</returns>
    Task<List<HbtSelectOption>> GetOptionsByUserNameAsync(string userName);

    /// <summary>
    /// 根据用户ID获取租户选项列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>租户选项列表</returns>
    Task<List<HbtSelectOption>> GetOptionsByUserIdAsync(long userId);

    /// <summary>
    /// 导入租户数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
    Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1");

    /// <summary>
    /// 导出租户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    Task<(string fileName, byte[] content)> ExportAsync(HbtTenantQueryDto query, string sheetName = "Sheet1");

    /// <summary>
    /// 获取租户导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件字节数组</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1");

    /// <summary>
    /// 更新租户状态
    /// </summary>
    /// <param name="id">租户ID</param>
    /// <param name="status">状态</param>
    /// <returns>更新后的租户状态信息</returns>
    Task<HbtTenantStatusDto> UpdateStatusAsync(long id, int status);

    /// <summary>
    /// 获取用户关联的租户列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户租户列表</returns>
    Task<List<HbtUserTenantDto>> GetUserTenantsAsync(long userId);
}