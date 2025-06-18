//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedNode.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 工作流节点种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流节点种子数据初始化类
/// </summary>
public class HbtDbSeedNode
{
    private readonly IHbtRepository<HbtNode> _nodeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="nodeRepository">工作流节点仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedNode(IHbtRepository<HbtNode> nodeRepository, IHbtLogger logger)
    {
        _nodeRepository = nodeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化工作流节点数据
    /// </summary>
    public async Task<(int, int)> InitializeNodeAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultNodes = new List<HbtNode>
        {
            // 请假流程节点
            new HbtNode
            {
                InstanceId = 1,
                NodeName = "开始",
                NodeType = 1,
                NodeConfig = "{}",
                Status = 2,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(1),
                Remark = "开始节点",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 1,
                NodeName = "部门审批",
                NodeType = 2,
                NodeConfig = "{\"assigneeType\":\"user\",\"assignee\":\"manager1\"}",
                Status = 1,
                StartTime = DateTime.Now.AddMinutes(1),
                EndTime = null,
                Remark = "部门审批节点",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 1,
                NodeName = "结束",
                NodeType = 3,
                NodeConfig = "{}",
                Status = 0,
                StartTime = null,
                EndTime = null,
                Remark = "结束节点",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 报销流程节点
            new HbtNode
            {
                InstanceId = 2,
                NodeName = "开始",
                NodeType = 1,
                NodeConfig = "{}",
                Status = 2,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(1),
                Remark = "开始节点",
                CreateBy = "lisi",
                CreateTime = DateTime.Now,
                UpdateBy = "lisi",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 2,
                NodeName = "部门审批",
                NodeType = 2,
                NodeConfig = "{\"assigneeType\":\"user\",\"assignee\":\"manager2\"}",
                Status = 1,
                StartTime = DateTime.Now.AddMinutes(1),
                EndTime = null,
                Remark = "部门审批节点",
                CreateBy = "lisi",
                CreateTime = DateTime.Now,
                UpdateBy = "lisi",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 2,
                NodeName = "财务审批",
                NodeType = 2,
                NodeConfig = "{\"assigneeType\":\"role\",\"assignee\":\"finance\"}",
                Status = 0,
                StartTime = null,
                EndTime = null,
                Remark = "财务审批节点",
                CreateBy = "lisi",
                CreateTime = DateTime.Now,
                UpdateBy = "lisi",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 2,
                NodeName = "结束",
                NodeType = 3,
                NodeConfig = "{}",
                Status = 0,
                StartTime = null,
                EndTime = null,
                Remark = "结束节点",
                CreateBy = "lisi",
                CreateTime = DateTime.Now,
                UpdateBy = "lisi",
                UpdateTime = DateTime.Now
            },

            // 采购流程节点
            new HbtNode
            {
                InstanceId = 3,
                NodeName = "开始",
                NodeType = 1,
                NodeConfig = "{}",
                Status = 2,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(1),
                Remark = "开始节点",
                CreateBy = "wangwu",
                CreateTime = DateTime.Now,
                UpdateBy = "wangwu",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 3,
                NodeName = "部门审批",
                NodeType = 2,
                NodeConfig = "{\"assigneeType\":\"user\",\"assignee\":\"manager3\"}",
                Status = 1,
                StartTime = DateTime.Now.AddMinutes(1),
                EndTime = null,
                Remark = "部门审批节点",
                CreateBy = "wangwu",
                CreateTime = DateTime.Now,
                UpdateBy = "wangwu",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 3,
                NodeName = "采购审批",
                NodeType = 2,
                NodeConfig = "{\"assigneeType\":\"role\",\"assignee\":\"purchase\"}",
                Status = 0,
                StartTime = null,
                EndTime = null,
                Remark = "采购审批节点",
                CreateBy = "wangwu",
                CreateTime = DateTime.Now,
                UpdateBy = "wangwu",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 3,
                NodeName = "财务审批",
                NodeType = 2,
                NodeConfig = "{\"assigneeType\":\"role\",\"assignee\":\"finance\"}",
                Status = 0,
                StartTime = null,
                EndTime = null,
                Remark = "财务审批节点",
                CreateBy = "wangwu",
                CreateTime = DateTime.Now,
                UpdateBy = "wangwu",
                UpdateTime = DateTime.Now
            },
            new HbtNode
            {
                InstanceId = 3,
                NodeName = "结束",
                NodeType = 3,
                NodeConfig = "{}",
                Status = 0,
                StartTime = null,
                EndTime = null,
                Remark = "结束节点",
                CreateBy = "wangwu",
                CreateTime = DateTime.Now,
                UpdateBy = "wangwu",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var node in defaultNodes)
        {
            var existingNode = await _nodeRepository.GetFirstAsync(n => 
                n.InstanceId == node.InstanceId && 
                n.NodeName == node.NodeName);

            if (existingNode == null)
            {
                await _nodeRepository.CreateAsync(node);
                insertCount++;
                _logger.Info($"已添加工作流节点: {node.NodeName}");
            }
            else
            {
                existingNode.Status = node.Status;
                existingNode.StartTime = node.StartTime;
                existingNode.EndTime = node.EndTime;
                existingNode.Remark = node.Remark;
                existingNode.UpdateBy = node.UpdateBy;
                existingNode.UpdateTime = DateTime.Now;

                await _nodeRepository.UpdateAsync(existingNode);
                updateCount++;
                _logger.Info($"已更新工作流节点: {node.NodeName}");
            }
        }

        return (insertCount, updateCount);
    }
} 