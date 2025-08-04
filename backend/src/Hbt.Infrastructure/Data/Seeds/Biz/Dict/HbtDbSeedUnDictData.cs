//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedUnDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 通用单位相关字典数据种子数据初始化类
//===================================================================

using Hbt.Domain.Entities.Routine.Core;

namespace Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 通用单位相关字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedUnDictData
{
    /// <summary>
    /// 获取通用单位相关字典数据
    /// </summary>
    /// <returns>通用单位相关字典数据列表</returns>
    public List<HbtDictData> GetUnDictData()
    {
        return new List<HbtDictData>
        {
            // 使用单位
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "个",
                DictValue = "PCS",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "个",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "千克",
                DictValue = "KG",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "千克",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "克",
                DictValue = "G",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "克",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "吨",
                DictValue = "T",
                OrderNum = 4,
                Status = 0,
                
                CssClass = 4,
                ListClass = 4,
                Remark = "吨",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "米",
                DictValue = "M",
                OrderNum = 5,
                Status = 0,
                
                CssClass = 5,
                ListClass = 5,
                Remark = "米",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "厘米",
                DictValue = "CM",
                OrderNum = 6,
                Status = 0,
                
                CssClass = 6,
                ListClass = 6,
                Remark = "厘米",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "毫米",
                DictValue = "MM",
                OrderNum = 7,
                Status = 0,
                
                CssClass = 7,
                ListClass = 7,
                Remark = "毫米",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "平方米",
                DictValue = "M2",
                OrderNum = 8,
                Status = 0,
                
                CssClass = 8,
                ListClass = 8,
                Remark = "平方米",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "立方米",
                DictValue = "M3",
                OrderNum = 9,
                Status = 0,
                
                CssClass = 9,
                ListClass = 9,
                Remark = "立方米",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "升",
                DictValue = "L",
                OrderNum = 10,
                Status = 0,
                
                CssClass = 10,
                ListClass = 10,
                Remark = "升",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "毫升",
                DictValue = "ML",
                OrderNum = 11,
                Status = 0,
                
                CssClass = 11,
                ListClass = 11,
                Remark = "毫升",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "箱",
                DictValue = "BOX",
                OrderNum = 12,
                Status = 0,
                
                CssClass = 12,
                ListClass = 12,
                Remark = "箱",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "包",
                DictValue = "PKG",
                OrderNum = 13,
                Status = 0,
                
                CssClass = 13,
                ListClass = 13,
                Remark = "包",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "卷",
                DictValue = "ROLL",
                OrderNum = 14,
                Status = 0,
                
                CssClass = 14,
                ListClass = 14,
                Remark = "卷",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "件",
                DictValue = "PCE",
                OrderNum = 15,
                Status = 0,
                
                CssClass = 15,
                ListClass = 15,
                Remark = "件",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "套",
                DictValue = "SET",
                OrderNum = 16,
                Status = 0,
                
                CssClass = 16,
                ListClass = 16,
                Remark = "套",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "台",
                DictValue = "UNIT",
                OrderNum = 17,
                Status = 0,
                
                CssClass = 17,
                ListClass = 17,
                Remark = "台",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "块",
                DictValue = "BLK",
                OrderNum = 18,
                Status = 0,
                
                CssClass = 18,
                ListClass = 18,
                Remark = "块",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "张",
                DictValue = "SHT",
                OrderNum = 19,
                Status = 0,
                
                CssClass = 19,
                ListClass = 19,
                Remark = "张",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "片",
                DictValue = "SLICE",
                OrderNum = 20,
                Status = 0,
                
                CssClass = 20,
                ListClass = 20,
                Remark = "片",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "小时",
                DictValue = "HR",
                OrderNum = 21,
                Status = 0,
                
                CssClass = 21,
                ListClass = 21,
                Remark = "小时",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "分钟",
                DictValue = "MIN",
                OrderNum = 22,
                Status = 0,
                
                CssClass = 22,
                ListClass = 22,
                Remark = "分钟",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "千瓦时",
                DictValue = "KWH",
                OrderNum = 23,
                Status = 0,
                
                CssClass = 23,
                ListClass = 23,
                Remark = "千瓦时",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "千瓦",
                DictValue = "KW",
                OrderNum = 24,
                Status = 0,
                
                CssClass = 24,
                ListClass = 24,
                Remark = "千瓦",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "伏特",
                DictValue = "V",
                OrderNum = 25,
                Status = 0,
                
                CssClass = 25,
                ListClass = 25,
                Remark = "伏特",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "安培",
                DictValue = "A",
                OrderNum = 26,
                Status = 0,
                
                CssClass = 26,
                ListClass = 26,
                Remark = "安培",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "欧姆",
                DictValue = "OHM",
                OrderNum = 27,
                Status = 0,
                
                CssClass = 27,
                ListClass = 27,
                Remark = "欧姆",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "赫兹",
                DictValue = "HZ",
                OrderNum = 28,
                Status = 0,
                
                CssClass = 28,
                ListClass = 28,
                Remark = "赫兹",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "摄氏度",
                DictValue = "C",
                OrderNum = 29,
                Status = 0,
                
                CssClass = 29,
                ListClass = 29,
                Remark = "摄氏度",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "帕斯卡",
                DictValue = "PA",
                OrderNum = 30,
                Status = 0,
                
                CssClass = 30,
                ListClass = 30,
                Remark = "帕斯卡",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "牛顿",
                DictValue = "N",
                OrderNum = 31,
                Status = 0,
                
                CssClass = 31,
                ListClass = 31,
                Remark = "牛顿",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "焦耳",
                DictValue = "J",
                OrderNum = 32,
                Status = 0,
                
                CssClass = 32,
                ListClass = 32,
                Remark = "焦耳",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "瓦特",
                DictValue = "W",
                OrderNum = 33,
                Status = 0,
                
                CssClass = 33,
                ListClass = 33,
                Remark = "瓦特",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "坎德拉",
                DictValue = "CD",
                OrderNum = 34,
                Status = 0,
                
                CssClass = 34,
                ListClass = 34,
                Remark = "坎德拉",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "摩尔",
                DictValue = "MOL",
                OrderNum = 35,
                Status = 0,
                
                CssClass = 35,
                ListClass = 35,
                Remark = "摩尔",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_unit_type",
                DictLabel = "其他",
                DictValue = "OTHER",
                OrderNum = 36,
                Status = 0,
                
                CssClass = 36,
                ListClass = 36,
                Remark = "其他计量单位",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}