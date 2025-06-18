//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedVariable.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 工作流变量种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流变量种子数据初始化类
/// </summary>
public class HbtDbSeedVariable
{
    private readonly IHbtRepository<HbtVariable> _variableRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="variableRepository">工作流变量仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedVariable(IHbtRepository<HbtVariable> variableRepository, IHbtLogger logger)
    {
        _variableRepository = variableRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化工作流变量数据
    /// </summary>
    public async Task<(int, int)> InitializeVariableAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultVariables = new List<HbtVariable>
        {
            // 请假流程变量
            new HbtVariable
            {
                InstanceId = 1,
                VariableName = "leaveType",
                VariableValue = "sick",
                VariableType = "string",
                Remark = "请假类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = 1,
                VariableName = "startTime",
                VariableValue = "2024-03-01 09:00:00",
                VariableType = "datetime",
                Remark = "开始时间",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = 1,
                VariableName = "endTime",
                VariableValue = "2024-03-03 18:00:00",
                VariableType = "datetime",
                Remark = "结束时间",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = 1,
                VariableName = "reason",
                VariableValue = "感冒发烧，需要休息",
                VariableType = "string",
                Remark = "请假原因",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 报销流程变量
            new HbtVariable
            {
                InstanceId = 2,
                VariableName = "expenseType",
                VariableValue = "travel",
                VariableType = "string",
                Remark = "报销类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = 2,
                VariableName = "amount",
                VariableValue = "5000",
                VariableType = "decimal",
                Remark = "报销金额",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = 2,
                VariableName = "description",
                VariableValue = "出差交通费报销",
                VariableType = "string",
                Remark = "报销说明",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 采购流程变量
            new HbtVariable
            {
                InstanceId = 3,
                VariableName = "purchaseType",
                VariableValue = "equipment",
                VariableType = "string",
                Remark = "采购类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = 3,
                VariableName = "amount",
                VariableValue = "10000",
                VariableType = "decimal",
                Remark = "采购金额",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtVariable
            {
                InstanceId = 3,
                VariableName = "description",
                VariableValue = "采购办公电脑",
                VariableType = "string",
                Remark = "采购说明",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var variable in defaultVariables)
        {
            var existingVariable = await _variableRepository.GetFirstAsync(v => 
                v.InstanceId == variable.InstanceId && 
                v.VariableName == variable.VariableName);

            if (existingVariable == null)
            {
                await _variableRepository.CreateAsync(variable);
                insertCount++;
                _logger.Info($"已添加工作流变量: {variable.VariableName}");
            }
            else
            {
                existingVariable.VariableValue = variable.VariableValue;
                existingVariable.VariableType = variable.VariableType;
                existingVariable.Remark = variable.Remark;
                existingVariable.UpdateBy = variable.UpdateBy;
                existingVariable.UpdateTime = DateTime.Now;

                await _variableRepository.UpdateAsync(existingVariable);
                updateCount++;
                _logger.Info($"已更新工作流变量: {variable.VariableName}");
            }
        }

        return (insertCount, updateCount);
    }
} 