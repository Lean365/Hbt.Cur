//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedInstance.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 工作流实例种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流实例种子数据初始化类
/// </summary>
public class HbtDbSeedInstance
{
    private readonly IHbtRepository<HbtInstance> _instanceRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="instanceRepository">工作流实例仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedInstance(IHbtRepository<HbtInstance> instanceRepository, IHbtLogger logger)
    {
        _instanceRepository = instanceRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化工作流实例数据
    /// </summary>
    public async Task<(int, int)> InitializeInstanceAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultInstances = new List<HbtInstance>
        {
            // 请假流程实例
            new HbtInstance
            {
                InstanceName = "张三的请假申请",
                DefinitionId = 1,
                CurrentNodeId = 1,
                InitiatorId = 1,
                BusinessKey = "LEAVE-2024-001",
                FormData = "{\"leaveType\":\"sick\",\"startTime\":\"2024-03-01 09:00:00\",\"endTime\":\"2024-03-03 18:00:00\",\"reason\":\"感冒发烧，需要休息\"}",
                Status = 1,
                StartTime = DateTime.Now,
                EndTime = null,
                Remark = "张三的请假申请实例",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 报销流程实例
            new HbtInstance
            {
                InstanceName = "李四的报销申请",
                DefinitionId = 2,
                CurrentNodeId = 1,
                InitiatorId = 2,
                BusinessKey = "EXPENSE-2024-001",
                FormData = "{\"expenseType\":\"travel\",\"amount\":5000,\"description\":\"出差交通费报销\"}",
                Status = 1,
                StartTime = DateTime.Now,
                EndTime = null,
                Remark = "李四的报销申请实例",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 采购流程实例
            new HbtInstance
            {
                InstanceName = "王五的采购申请",
                DefinitionId = 3,
                CurrentNodeId = 1,
                InitiatorId = 3,
                BusinessKey = "PURCHASE-2024-001",
                FormData = "{\"purchaseType\":\"equipment\",\"amount\":10000,\"description\":\"采购办公电脑\"}",
                Status = 1,
                StartTime = DateTime.Now,
                EndTime = null,
                Remark = "王五的采购申请实例",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var instance in defaultInstances)
        {
            var existingInstance = await _instanceRepository.GetFirstAsync(i => 
                i.BusinessKey == instance.BusinessKey);

            if (existingInstance == null)
            {
                await _instanceRepository.CreateAsync(instance);
                insertCount++;
                _logger.Info($"已添加工作流实例: {instance.InstanceName}");
            }
            else
            {
                existingInstance.FormData = instance.FormData;
                existingInstance.Status = instance.Status;
                existingInstance.EndTime = instance.EndTime;
                existingInstance.Remark = instance.Remark;
                existingInstance.UpdateBy = instance.UpdateBy;
                existingInstance.UpdateTime = DateTime.Now;

                await _instanceRepository.UpdateAsync(existingInstance);
                updateCount++;
                _logger.Info($"已更新工作流实例: {instance.InstanceName}");
            }
        }

        return (insertCount, updateCount);
    }
} 