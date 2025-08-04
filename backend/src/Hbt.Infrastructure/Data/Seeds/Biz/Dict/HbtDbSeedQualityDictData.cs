//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedQualityDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 质量相关字典数据种子数据初始化类
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 质量相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedQualityDictData
{
    /// <summary>
    /// 获取质量相关字典数据
    /// </summary>
    /// <returns>质量相关字典数据列表</returns>
    public List<HbtDictData> GetQualityDictData()
    {
        return new List<HbtDictData>
        {
            // 检验类型
            new HbtDictData
            {
                DictType = "sys_inspection_type",
                DictLabel = "进货检验",
                DictValue = "INCOMING",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "原材料进货检验",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_inspection_type",
                DictLabel = "过程检验",
                DictValue = "PROCESS",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "生产过程检验",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_inspection_type",
                DictLabel = "成品检验",
                DictValue = "FINAL",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "成品出厂检验",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_inspection_type",
                DictLabel = "定期检验",
                DictValue = "PERIODIC",
                OrderNum = 4,
                Status = 0,
                
                CssClass = 4,
                ListClass = 4,
                Remark = "定期质量检验",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 检验状态
            new HbtDictData
            {
                DictType = "sys_inspection_status",
                DictLabel = "待检验",
                DictValue = "PENDING",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "待检验状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_inspection_status",
                DictLabel = "检验中",
                DictValue = "INSPECTING",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "检验中状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_inspection_status",
                DictLabel = "已检验",
                DictValue = "INSPECTED",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "已检验状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_inspection_status",
                DictLabel = "已关闭",
                DictValue = "CLOSED",
                OrderNum = 4,
                Status = 0,
                
                CssClass = 4,
                ListClass = 4,
                Remark = "已关闭状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 检验结果
            new HbtDictData
            {
                DictType = "sys_inspection_result",
                DictLabel = "合格",
                DictValue = "PASS",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "检验合格",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_inspection_result",
                DictLabel = "不合格",
                DictValue = "FAIL",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "检验不合格",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_inspection_result",
                DictLabel = "让步接收",
                DictValue = "CONCESSION",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "让步接收",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 不合格类型
            new HbtDictData
            {
                DictType = "sys_defect_type",
                DictLabel = "外观缺陷",
                DictValue = "APPEARANCE",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "外观质量缺陷",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_defect_type",
                DictLabel = "尺寸缺陷",
                DictValue = "DIMENSION",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "尺寸规格缺陷",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_defect_type",
                DictLabel = "功能缺陷",
                DictValue = "FUNCTION",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "功能性能缺陷",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_defect_type",
                DictLabel = "材质缺陷",
                DictValue = "MATERIAL",
                OrderNum = 4,
                Status = 0,
                
                CssClass = 4,
                ListClass = 4,
                Remark = "材质材料缺陷",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 不合格等级
            new HbtDictData
            {
                DictType = "sys_defect_level",
                DictLabel = "致命缺陷",
                DictValue = "CRITICAL",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "致命质量缺陷",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_defect_level",
                DictLabel = "严重缺陷",
                DictValue = "MAJOR",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "严重质量缺陷",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_defect_level",
                DictLabel = "轻微缺陷",
                DictValue = "MINOR",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "轻微质量缺陷",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 不合格处理方式
            new HbtDictData
            {
                DictType = "sys_defect_disposition",
                DictLabel = "返工",
                DictValue = "REWORK",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "返工处理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_defect_disposition",
                DictLabel = "返修",
                DictValue = "REPAIR",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "返修处理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_defect_disposition",
                DictLabel = "报废",
                DictValue = "SCRAP",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "报废处理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_defect_disposition",
                DictLabel = "让步接收",
                DictValue = "CONCESSION",
                OrderNum = 4,
                Status = 0,
                
                CssClass = 4,
                ListClass = 4,
                Remark = "让步接收处理",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 质量等级
            new HbtDictData
            {
                DictType = "sys_quality_level",
                DictLabel = "优等品",
                DictValue = "EXCELLENT",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "优等质量等级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_level",
                DictLabel = "一等品",
                DictValue = "FIRST",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "一等质量等级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_level",
                DictLabel = "合格品",
                DictValue = "QUALIFIED",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "合格质量等级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 质量特性
            new HbtDictData
            {
                DictType = "sys_quality_characteristic",
                DictLabel = "物理特性",
                DictValue = "PHYSICAL",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "物理质量特性",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_characteristic",
                DictLabel = "化学特性",
                DictValue = "CHEMICAL",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "化学质量特性",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_characteristic",
                DictLabel = "机械特性",
                DictValue = "MECHANICAL",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "机械质量特性",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 质量工具
            new HbtDictData
            {
                DictType = "sys_quality_tool",
                DictLabel = "控制图",
                DictValue = "CONTROL_CHART",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "统计过程控制图",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_tool",
                DictLabel = "直方图",
                DictValue = "HISTOGRAM",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "质量数据直方图",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_tool",
                DictLabel = "帕累托图",
                DictValue = "PARETO",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "质量分析帕累托图",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 质量成本类型
            new HbtDictData
            {
                DictType = "sys_quality_cost_type",
                DictLabel = "预防成本",
                DictValue = "PREVENTION",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "质量预防成本",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_cost_type",
                DictLabel = "鉴定成本",
                DictValue = "APPRAISAL",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "质量鉴定成本",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_cost_type",
                DictLabel = "内部损失",
                DictValue = "INTERNAL",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "质量内部损失",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_quality_cost_type",
                DictLabel = "外部损失",
                DictValue = "EXTERNAL",
                OrderNum = 4,
                Status = 0,
                
                CssClass = 4,
                ListClass = 4,
                Remark = "质量外部损失",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
} 