//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTask.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 工作流任务种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流任务种子数据初始化类
/// </summary>
public class HbtDbSeedTask
{
    private readonly IHbtRepository<HbtProcessTask> _taskRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="taskRepository">工作流任务仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedTask(IHbtRepository<HbtProcessTask> taskRepository, IHbtLogger logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化工作流任务数据
    /// </summary>
    public async Task<(int, int)> InitializeTaskAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultTasks = new List<HbtProcessTask>
        {
            // 请假流程任务
            new HbtProcessTask
            {
                TaskName = "部门经理审批",
                InstanceId = 1,
                NodeId = 2,
                TaskType = 1,
                Status = 0,
                AssigneeId = 1,
                Priority = 1,
                Remark = "部门经理审批任务",
                CreateBy = "zhangsan",
                CreateTime = DateTime.Now,
                UpdateBy = "zhangsan",
                UpdateTime = DateTime.Now
            },

            // 报销流程任务
            new HbtProcessTask
            {
                TaskName = "部门经理审批",
                InstanceId = 2,
                NodeId = 2,
                TaskType = 1,
                Status = 0,
                AssigneeId = 2,
                Priority = 1,
                Remark = "部门经理审批任务",
                CreateBy = "lisi",
                CreateTime = DateTime.Now,
                UpdateBy = "lisi",
                UpdateTime = DateTime.Now
            },
            new HbtProcessTask
            {
                TaskName = "财务审批",
                InstanceId = 2,
                NodeId = 3,
                TaskType = 1,
                Status = 0,
                AssigneeId = 3,
                Priority = 1,
                Remark = "财务审批任务",
                CreateBy = "lisi",
                CreateTime = DateTime.Now,
                UpdateBy = "lisi",
                UpdateTime = DateTime.Now
            },

            // 采购流程任务
            new HbtProcessTask
            {
                TaskName = "部门经理审批",
                InstanceId = 3,
                NodeId = 2,
                TaskType = 1,
                Status = 0,
                AssigneeId = 4,
                Priority = 1,
                Remark = "部门经理审批任务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtProcessTask
            {
                TaskName = "采购审批",
                InstanceId = 3,
                NodeId = 3,
                TaskType = 1,
                Status = 0,
                AssigneeId = 5,
                Priority = 1,
                Remark = "采购审批任务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtProcessTask
            {
                TaskName = "财务审批",
                InstanceId = 3,
                NodeId = 4,
                TaskType = 1,
                Status = 0,
                AssigneeId = 6,
                Priority = 1,
                Remark = "财务审批任务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var task in defaultTasks)
        {
            var existingTask = await _taskRepository.GetFirstAsync(t => 
                t.InstanceId == task.InstanceId && 
                t.NodeId == task.NodeId);

            if (existingTask == null)
            {
                await _taskRepository.CreateAsync(task);
                insertCount++;
                _logger.Info($"已添加工作流任务: {task.TaskName}");
            }
            else
            {
                existingTask.Status = task.Status;
                existingTask.Remark = task.Remark;
                existingTask.UpdateBy = task.UpdateBy;
                existingTask.UpdateTime = DateTime.Now;

                await _taskRepository.UpdateAsync(existingTask);
                updateCount++;
                _logger.Info($"已更新工作流任务: {task.TaskName}");
            }
        }

        return (insertCount, updateCount);
    }
} 