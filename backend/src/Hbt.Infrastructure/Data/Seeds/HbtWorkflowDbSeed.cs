//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowDbSeed.cs
// 创建者 : Lean365
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 工作流数据库种子数据初始化类
//===================================================================

using Hbt.Common.Exceptions;
using Hbt.Infrastructure.Data.Contexts;
using Hbt.Infrastructure.Data.Seeds.Workflow;

namespace Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流数据库种子数据初始化类
/// </summary>
public class HbtWorkflowDbSeed
{
    private readonly HbtWorkflowDbContext _context;
    private readonly IHbtLogger _logger;
    private readonly HbtDbSeedWorkflowCoordinator _workflowCoordinator;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtWorkflowDbSeed(
        HbtWorkflowDbContext context,
        IHbtLogger logger,
        HbtDbSeedWorkflowCoordinator workflowCoordinator)
    {
        _context = context;
        _logger = logger;
        _workflowCoordinator = workflowCoordinator;
    }

    /// <summary>
    /// 初始化工作流数据库种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化工作流数据库种子数据...");

            // 使用工作流协调器初始化所有工作流相关数据
            await _workflowCoordinator.InitializeLeaveWorkflowAsync();
            _logger.Info("工作流数据初始化完成");

            _logger.Info("工作流数据库种子数据初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error($"工作流数据库种子数据初始化失败: {ex.Message}", ex);
            throw new HbtException("工作流数据库种子数据初始化失败", ex);
        }
    }
}