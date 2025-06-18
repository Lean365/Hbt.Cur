//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDefinition.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 工作流定义种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流定义种子数据初始化类
/// </summary>
public class HbtDbSeedDefinition
{
    private readonly IHbtRepository<HbtDefinition> _definitionRepository;
    private readonly IHbtRepository<HbtForm> _formRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="definitionRepository">工作流定义仓储</param>
    /// <param name="formRepository">表单仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedDefinition(IHbtRepository<HbtDefinition> definitionRepository, IHbtRepository<HbtForm> formRepository, IHbtLogger logger)
    {
        _definitionRepository = definitionRepository;
        _formRepository = formRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化工作流定义数据
    /// </summary>
    public async Task<(int, int)> InitializeDefinitionAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 定义流程与表单的映射
        var defaultDefinitions = new List<(string WorkflowName, string WorkflowCategory, string WorkflowVersion, string FormName, string WorkflowConfig, int Status, string Remark)>
        {
            ("员工请假流程", "leave", "A", "请假申请表单", "{\"nodes\":[{\"id\":\"start\",\"type\":\"start\",\"name\":\"开始\"},{\"id\":\"approval\",\"type\":\"approval\",\"name\":\"部门审批\"},{\"id\":\"end\",\"type\":\"end\",\"name\":\"结束\"}],\"edges\":[{\"source\":\"start\",\"target\":\"approval\"},{\"source\":\"approval\",\"target\":\"end\"}]}", 0, "员工请假审批流程定义"),
            ("费用报销流程", "expense", "A", "报销申请表单", "{\"nodes\":[{\"id\":\"start\",\"type\":\"start\",\"name\":\"开始\"},{\"id\":\"approval1\",\"type\":\"approval\",\"name\":\"部门审批\"},{\"id\":\"approval2\",\"type\":\"approval\",\"name\":\"财务审批\"},{\"id\":\"end\",\"type\":\"end\",\"name\":\"结束\"}],\"edges\":[{\"source\":\"start\",\"target\":\"approval1\"},{\"source\":\"approval1\",\"target\":\"approval2\"},{\"source\":\"approval2\",\"target\":\"end\"}]}", 0, "费用报销审批流程定义"),
            ("采购申请流程", "purchase", "A", "采购申请流程表单", "{\"nodes\":[{\"id\":\"start\",\"type\":\"start\",\"name\":\"开始\"},{\"id\":\"approval1\",\"type\":\"approval\",\"name\":\"部门审批\"},{\"id\":\"approval2\",\"type\":\"approval\",\"name\":\"采购审批\"},{\"id\":\"approval3\",\"type\":\"approval\",\"name\":\"财务审批\"},{\"id\":\"end\",\"type\":\"end\",\"name\":\"结束\"}],\"edges\":[{\"source\":\"start\",\"target\":\"approval1\"},{\"source\":\"approval1\",\"target\":\"approval2\"},{\"source\":\"approval2\",\"target\":\"approval3\"},{\"source\":\"approval3\",\"target\":\"end\"}]}", 0, "采购申请审批流程定义")
        };

        foreach (var def in defaultDefinitions)
        {
            // 查找表单ID
            var form = await _formRepository.GetFirstAsync(f => f.FormName == def.FormName);
            long? formId = form?.Id;

            var existingDefinition = await _definitionRepository.GetFirstAsync(d =>
                d.WorkflowName == def.WorkflowName &&
                d.WorkflowVersion == def.WorkflowVersion);

            if (existingDefinition == null)
            {
                var definition = new HbtDefinition
                {
                    WorkflowName = def.WorkflowName,
                    WorkflowCategory = def.WorkflowCategory,
                    WorkflowVersion = def.WorkflowVersion,
                    FormId = formId,
                    WorkflowConfig = def.WorkflowConfig,
                    Status = def.Status,
                    Remark = def.Remark,
                    CreateBy = "Hbt365",
                    CreateTime = DateTime.Now,
                    UpdateBy = "Hbt365",
                    UpdateTime = DateTime.Now
                };
                await _definitionRepository.CreateAsync(definition);
                insertCount++;
                _logger.Info($"已添加工作流定义: {def.WorkflowName}");
            }
            else
            {
                existingDefinition.FormId = formId;
                existingDefinition.WorkflowConfig = def.WorkflowConfig;
                existingDefinition.Status = def.Status;
                existingDefinition.Remark = def.Remark;
                existingDefinition.UpdateBy = "Hbt365";
                existingDefinition.UpdateTime = DateTime.Now;

                await _definitionRepository.UpdateAsync(existingDefinition);
                updateCount++;
                _logger.Info($"已更新工作流定义: {def.WorkflowName}");
            }
        }

        return (insertCount, updateCount);
    }
} 