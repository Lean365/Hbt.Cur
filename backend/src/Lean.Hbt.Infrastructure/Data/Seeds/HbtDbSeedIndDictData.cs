//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedIndDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 国民经济行业分类字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 国民经济行业分类字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedIndDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedIndDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化国民经济行业分类字典数据
    /// </summary>
    public async Task<(int, int)> InitializeIndDictDataAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            // A 农、林、牧、渔业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "农、林、牧、渔业",
                DictValue = "A",
                OrderNum = 1,
                Status = 0,
                
                CssClass = 1,
                ListClass = 1,
                Remark = "农、林、牧、渔业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "农业",
                DictValue = "A01",
                OrderNum = 2,
                Status = 0,
                
                CssClass = 2,
                ListClass = 2,
                Remark = "农业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "林业",
                DictValue = "A02",
                OrderNum = 3,
                Status = 0,
                
                CssClass = 3,
                ListClass = 3,
                Remark = "林业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "畜牧业",
                DictValue = "A03",
                OrderNum = 4,
                Status = 0,
                
                CssClass = 4,
                ListClass = 4,
                Remark = "畜牧业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "渔业",
                DictValue = "A04",
                OrderNum = 5,
                Status = 0,
                
                CssClass = 5,
                ListClass = 5,
                Remark = "渔业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "农、林、牧、渔专业及辅助性活动",
                DictValue = "A05",
                OrderNum = 6,
                Status = 0,
                
                CssClass = 6,
                ListClass = 6,
                Remark = "农、林、牧、渔专业及辅助性活动",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // B 采矿业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "采矿业",
                DictValue = "B",
                OrderNum = 7,
                Status = 0,
                
                CssClass = 7,
                ListClass = 7,
                Remark = "采矿业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "煤炭开采和洗选业",
                DictValue = "B06",
                OrderNum = 8,
                Status = 0,
                
                CssClass = 8,
                ListClass = 8,
                Remark = "煤炭开采和洗选业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "石油和天然气开采业",
                DictValue = "B07",
                OrderNum = 9,
                Status = 0,
                
                CssClass = 9,
                ListClass = 9,
                Remark = "石油和天然气开采业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "黑色金属矿采选业",
                DictValue = "B08",
                OrderNum = 10,
                Status = 0,
                
                CssClass = 10,
                ListClass = 10,
                Remark = "黑色金属矿采选业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "有色金属矿采选业",
                DictValue = "B09",
                OrderNum = 11,
                Status = 0,
                
                CssClass = 11,
                ListClass = 11,
                Remark = "有色金属矿采选业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "非金属矿采选业",
                DictValue = "B10",
                OrderNum = 12,
                Status = 0,
                
                CssClass = 12,
                ListClass = 12,
                Remark = "非金属矿采选业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "开采专业及辅助性活动",
                DictValue = "B11",
                OrderNum = 13,
                Status = 0,
                
                CssClass = 13,
                ListClass = 13,
                Remark = "开采专业及辅助性活动",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "其他采矿业",
                DictValue = "B12",
                OrderNum = 14,
                Status = 0,
                
                CssClass = 14,
                ListClass = 14,
                Remark = "其他采矿业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // C 制造业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "制造业",
                DictValue = "C",
                OrderNum = 15,
                Status = 0,
                
                CssClass = 15,
                ListClass = 15,
                Remark = "制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "农副食品加工业",
                DictValue = "C13",
                OrderNum = 16,
                Status = 0,
                
                CssClass = 16,
                ListClass = 16,
                Remark = "农副食品加工业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "食品制造业",
                DictValue = "C14",
                OrderNum = 17,
                Status = 0,
                
                CssClass = 17,
                ListClass = 17,
                Remark = "食品制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "酒、饮料和精制茶制造业",
                DictValue = "C15",
                OrderNum = 18,
                Status = 0,
                
                CssClass = 18,
                ListClass = 18,
                Remark = "酒、饮料和精制茶制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "烟草制品业",
                DictValue = "C16",
                OrderNum = 19,
                Status = 0,
                
                CssClass = 19,
                ListClass = 19,
                Remark = "烟草制品业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "纺织业",
                DictValue = "C17",
                OrderNum = 20,
                Status = 0,
                
                CssClass = 20,
                ListClass = 20,
                Remark = "纺织业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "纺织服装、服饰业",
                DictValue = "C18",
                OrderNum = 21,
                Status = 0,
                
                CssClass = 21,
                ListClass = 21,
                Remark = "纺织服装、服饰业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "皮革、毛皮、羽毛及其制品和制鞋业",
                DictValue = "C19",
                OrderNum = 22,
                Status = 0,
                
                CssClass = 22,
                ListClass = 22,
                Remark = "皮革、毛皮、羽毛及其制品和制鞋业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "木材加工和木、竹、藤、棕、草制品业",
                DictValue = "C20",
                OrderNum = 23,
                Status = 0,
                
                CssClass = 23,
                ListClass = 23,
                Remark = "木材加工和木、竹、藤、棕、草制品业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "家具制造业",
                DictValue = "C21",
                OrderNum = 24,
                Status = 0,
                
                CssClass = 24,
                ListClass = 24,
                Remark = "家具制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "造纸和纸制品业",
                DictValue = "C22",
                OrderNum = 25,
                Status = 0,
                
                CssClass = 25,
                ListClass = 25,
                Remark = "造纸和纸制品业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "印刷和记录媒介复制业",
                DictValue = "C23",
                OrderNum = 26,
                Status = 0,
                
                CssClass = 26,
                ListClass = 26,
                Remark = "印刷和记录媒介复制业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "文教、工美、体育和娱乐用品制造业",
                DictValue = "C24",
                OrderNum = 27,
                Status = 0,
                
                CssClass = 27,
                ListClass = 27,
                Remark = "文教、工美、体育和娱乐用品制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "石油、煤炭及其他燃料加工业",
                DictValue = "C25",
                OrderNum = 28,
                Status = 0,
                
                CssClass = 28,
                ListClass = 28,
                Remark = "石油、煤炭及其他燃料加工业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "化学原料和化学制品制造业",
                DictValue = "C26",
                OrderNum = 29,
                Status = 0,
                
                CssClass = 29,
                ListClass = 29,
                Remark = "化学原料和化学制品制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "医药制造业",
                DictValue = "C27",
                OrderNum = 30,
                Status = 0,
                
                CssClass = 30,
                ListClass = 30,
                Remark = "医药制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "化学纤维制造业",
                DictValue = "C28",
                OrderNum = 31,
                Status = 0,
                
                CssClass = 31,
                ListClass = 31,
                Remark = "化学纤维制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "橡胶和塑料制品业",
                DictValue = "C29",
                OrderNum = 32,
                Status = 0,
                
                CssClass = 32,
                ListClass = 32,
                Remark = "橡胶和塑料制品业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "非金属矿物制品业",
                DictValue = "C30",
                OrderNum = 33,
                Status = 0,
                
                CssClass = 33,
                ListClass = 33,
                Remark = "非金属矿物制品业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "黑色金属冶炼和压延加工业",
                DictValue = "C31",
                OrderNum = 34,
                Status = 0,
                
                CssClass = 34,
                ListClass = 34,
                Remark = "黑色金属冶炼和压延加工业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "有色金属冶炼和压延加工业",
                DictValue = "C32",
                OrderNum = 35,
                Status = 0,
                
                CssClass = 35,
                ListClass = 35,
                Remark = "有色金属冶炼和压延加工业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "金属制品业",
                DictValue = "C33",
                OrderNum = 36,
                Status = 0,
                
                CssClass = 36,
                ListClass = 36,
                Remark = "金属制品业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "通用设备制造业",
                DictValue = "C34",
                OrderNum = 37,
                Status = 0,
                
                CssClass = 37,
                ListClass = 37,
                Remark = "通用设备制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "专用设备制造业",
                DictValue = "C35",
                OrderNum = 38,
                Status = 0,
                
                CssClass = 38,
                ListClass = 38,
                Remark = "专用设备制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "汽车制造业",
                DictValue = "C36",
                OrderNum = 39,
                Status = 0,
                
                CssClass = 39,
                ListClass = 39,
                Remark = "汽车制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "铁路、船舶、航空航天和其他运输设备制造业",
                DictValue = "C37",
                OrderNum = 40,
                Status = 0,
                
                CssClass = 40,
                ListClass = 40,
                Remark = "铁路、船舶、航空航天和其他运输设备制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "电气机械和器材制造业",
                DictValue = "C38",
                OrderNum = 41,
                Status = 0,
                
                CssClass = 41,
                ListClass = 41,
                Remark = "电气机械和器材制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "计算机、通信和其他电子设备制造业",
                DictValue = "C39",
                OrderNum = 42,
                Status = 0,
                
                CssClass = 42,
                ListClass = 42,
                Remark = "计算机、通信和其他电子设备制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "仪器仪表制造业",
                DictValue = "C40",
                OrderNum = 43,
                Status = 0,
                
                CssClass = 43,
                ListClass = 43,
                Remark = "仪器仪表制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "其他制造业",
                DictValue = "C41",
                OrderNum = 44,
                Status = 0,
                
                CssClass = 44,
                ListClass = 44,
                Remark = "其他制造业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "废弃资源综合利用业",
                DictValue = "C42",
                OrderNum = 45,
                Status = 0,
                
                CssClass = 45,
                ListClass = 45,
                Remark = "废弃资源综合利用业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "金属制品、机械和设备修理业",
                DictValue = "C43",
                OrderNum = 46,
                Status = 0,
                
                CssClass = 46,
                ListClass = 46,
                Remark = "金属制品、机械和设备修理业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // D 电力、热力、燃气及水生产和供应业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "电力、热力、燃气及水生产和供应业",
                DictValue = "D",
                OrderNum = 47,
                Status = 0,
                
                CssClass = 47,
                ListClass = 47,
                Remark = "电力、热力、燃气及水生产和供应业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "电力、热力生产和供应业",
                DictValue = "D44",
                OrderNum = 48,
                Status = 0,
                
                CssClass = 48,
                ListClass = 48,
                Remark = "电力、热力生产和供应业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "燃气生产和供应业",
                DictValue = "D45",
                OrderNum = 49,
                Status = 0,
                
                CssClass = 49,
                ListClass = 49,
                Remark = "燃气生产和供应业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "水的生产和供应业",
                DictValue = "D46",
                OrderNum = 50,
                Status = 0,
                
                CssClass = 50,
                ListClass = 50,
                Remark = "水的生产和供应业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // E 建筑业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "建筑业",
                DictValue = "E",
                OrderNum = 51,
                Status = 0,
                
                CssClass = 51,
                ListClass = 51,
                Remark = "建筑业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "房屋建筑业",
                DictValue = "E47",
                OrderNum = 52,
                Status = 0,
                
                CssClass = 52,
                ListClass = 52,
                Remark = "房屋建筑业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "土木工程建筑业",
                DictValue = "E48",
                OrderNum = 53,
                Status = 0,
                
                CssClass = 53,
                ListClass = 53,
                Remark = "土木工程建筑业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "建筑安装业",
                DictValue = "E49",
                OrderNum = 54,
                Status = 0,
                
                CssClass = 54,
                ListClass = 54,
                Remark = "建筑安装业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "建筑装饰、装修和其他建筑业",
                DictValue = "E50",
                OrderNum = 55,
                Status = 0,
                
                CssClass = 55,
                ListClass = 55,
                Remark = "建筑装饰、装修和其他建筑业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // F 批发和零售业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "批发和零售业",
                DictValue = "F",
                OrderNum = 56,
                Status = 0,
                
                CssClass = 56,
                ListClass = 56,
                Remark = "批发和零售业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "批发业",
                DictValue = "F51",
                OrderNum = 57,
                Status = 0,
                
                CssClass = 57,
                ListClass = 57,
                Remark = "批发业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "零售业",
                DictValue = "F52",
                OrderNum = 58,
                Status = 0,
                
                CssClass = 58,
                ListClass = 58,
                Remark = "零售业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // G 交通运输、仓储和邮政业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "交通运输、仓储和邮政业",
                DictValue = "G",
                OrderNum = 59,
                Status = 0,
                
                CssClass = 59,
                ListClass = 59,
                Remark = "交通运输、仓储和邮政业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "铁路运输业",
                DictValue = "G53",
                OrderNum = 60,
                Status = 0,
                
                CssClass = 60,
                ListClass = 60,
                Remark = "铁路运输业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "道路运输业",
                DictValue = "G54",
                OrderNum = 61,
                Status = 0,
                
                CssClass = 61,
                ListClass = 61,
                Remark = "道路运输业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "水上运输业",
                DictValue = "G55",
                OrderNum = 62,
                Status = 0,
                
                CssClass = 62,
                ListClass = 62,
                Remark = "水上运输业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "航空运输业",
                DictValue = "G56",
                OrderNum = 63,
                Status = 0,
                
                CssClass = 63,
                ListClass = 63,
                Remark = "航空运输业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "管道运输业",
                DictValue = "G57",
                OrderNum = 64,
                Status = 0,
                
                CssClass = 64,
                ListClass = 64,
                Remark = "管道运输业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "多式联运和运输代理业",
                DictValue = "G58",
                OrderNum = 65,
                Status = 0,
                
                CssClass = 65,
                ListClass = 65,
                Remark = "多式联运和运输代理业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "装卸搬运和仓储业",
                DictValue = "G59",
                OrderNum = 66,
                Status = 0,
                
                CssClass = 66,
                ListClass = 66,
                Remark = "装卸搬运和仓储业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "邮政业",
                DictValue = "G60",
                OrderNum = 67,
                Status = 0,
                
                CssClass = 67,
                ListClass = 67,
                Remark = "邮政业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // H 住宿和餐饮业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "住宿和餐饮业",
                DictValue = "H",
                OrderNum = 68,
                Status = 0,
                
                CssClass = 68,
                ListClass = 68,
                Remark = "住宿和餐饮业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "住宿业",
                DictValue = "H61",
                OrderNum = 69,
                Status = 0,
                
                CssClass = 69,
                ListClass = 69,
                Remark = "住宿业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "餐饮业",
                DictValue = "H62",
                OrderNum = 70,
                Status = 0,
                
                CssClass = 70,
                ListClass = 70,
                Remark = "餐饮业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // I 信息传输、软件和信息技术服务业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "信息传输、软件和信息技术服务业",
                DictValue = "I",
                OrderNum = 71,
                Status = 0,
                
                CssClass = 71,
                ListClass = 71,
                Remark = "信息传输、软件和信息技术服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "电信、广播电视和卫星传输服务",
                DictValue = "I63",
                OrderNum = 72,
                Status = 0,
                
                CssClass = 72,
                ListClass = 72,
                Remark = "电信、广播电视和卫星传输服务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "互联网和相关服务",
                DictValue = "I64",
                OrderNum = 73,
                Status = 0,
                
                CssClass = 73,
                ListClass = 73,
                Remark = "互联网和相关服务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "软件和信息技术服务业",
                DictValue = "I65",
                OrderNum = 74,
                Status = 0,
                
                CssClass = 74,
                ListClass = 74,
                Remark = "软件和信息技术服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // J 金融业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "金融业",
                DictValue = "J",
                OrderNum = 75,
                Status = 0,
                
                CssClass = 75,
                ListClass = 75,
                Remark = "金融业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "货币金融服务",
                DictValue = "J66",
                OrderNum = 76,
                Status = 0,
                
                CssClass = 76,
                ListClass = 76,
                Remark = "货币金融服务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "资本市场服务",
                DictValue = "J67",
                OrderNum = 77,
                Status = 0,
                
                CssClass = 77,
                ListClass = 77,
                Remark = "资本市场服务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "保险业",
                DictValue = "J68",
                OrderNum = 78,
                Status = 0,
                
                CssClass = 78,
                ListClass = 78,
                Remark = "保险业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "其他金融业",
                DictValue = "J69",
                OrderNum = 79,
                Status = 0,
                
                CssClass = 79,
                ListClass = 79,
                Remark = "其他金融业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // K 房地产业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "房地产业",
                DictValue = "K",
                OrderNum = 80,
                Status = 0,
                
                CssClass = 80,
                ListClass = 80,
                Remark = "房地产业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "房地产业",
                DictValue = "K70",
                OrderNum = 81,
                Status = 0,
                
                CssClass = 81,
                ListClass = 81,
                Remark = "房地产业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // L 租赁和商务服务业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "租赁和商务服务业",
                DictValue = "L",
                OrderNum = 82,
                Status = 0,
                
                CssClass = 82,
                ListClass = 82,
                Remark = "租赁和商务服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "租赁业",
                DictValue = "L71",
                OrderNum = 83,
                Status = 0,
                
                CssClass = 83,
                ListClass = 83,
                Remark = "租赁业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "商务服务业",
                DictValue = "L72",
                OrderNum = 84,
                Status = 0,
                
                CssClass = 84,
                ListClass = 84,
                Remark = "商务服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // M 科学研究和技术服务业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "科学研究和技术服务业",
                DictValue = "M",
                OrderNum = 85,
                Status = 0,
                
                CssClass = 85,
                ListClass = 85,
                Remark = "科学研究和技术服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "研究和试验发展",
                DictValue = "M73",
                OrderNum = 86,
                Status = 0,
                
                CssClass = 86,
                ListClass = 86,
                Remark = "研究和试验发展",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "专业技术服务业",
                DictValue = "M74",
                OrderNum = 87,
                Status = 0,
                
                CssClass = 87,
                ListClass = 87,
                Remark = "专业技术服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "科技推广和应用服务业",
                DictValue = "M75",
                OrderNum = 88,
                Status = 0,
                
                CssClass = 88,
                ListClass = 88,
                Remark = "科技推广和应用服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // N 水利、环境和公共设施管理业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "水利、环境和公共设施管理业",
                DictValue = "N",
                OrderNum = 89,
                Status = 0,
                
                CssClass = 89,
                ListClass = 89,
                Remark = "水利、环境和公共设施管理业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "水利管理业",
                DictValue = "N76",
                OrderNum = 90,
                Status = 0,
                
                CssClass = 90,
                ListClass = 90,
                Remark = "水利管理业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "生态保护和环境治理业",
                DictValue = "N77",
                OrderNum = 91,
                Status = 0,
                
                CssClass = 91,
                ListClass = 91,
                Remark = "生态保护和环境治理业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "公共设施管理业",
                DictValue = "N78",
                OrderNum = 92,
                Status = 0,
                
                CssClass = 92,
                ListClass = 92,
                Remark = "公共设施管理业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "土地管理业",
                DictValue = "N79",
                OrderNum = 93,
                Status = 0,
                
                CssClass = 93,
                ListClass = 93,
                Remark = "土地管理业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // O 居民服务、修理和其他服务业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "居民服务、修理和其他服务业",
                DictValue = "O",
                OrderNum = 94,
                Status = 0,
                
                CssClass = 94,
                ListClass = 94,
                Remark = "居民服务、修理和其他服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "居民服务业",
                DictValue = "O80",
                OrderNum = 95,
                Status = 0,
                
                CssClass = 95,
                ListClass = 95,
                Remark = "居民服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "机动车、电子产品和日用产品修理业",
                DictValue = "O81",
                OrderNum = 96,
                Status = 0,
                
                CssClass = 96,
                ListClass = 96,
                Remark = "机动车、电子产品和日用产品修理业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "其他服务业",
                DictValue = "O82",
                OrderNum = 97,
                Status = 0,
                
                CssClass = 97,
                ListClass = 97,
                Remark = "其他服务业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // P 教育
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "教育",
                DictValue = "P",
                OrderNum = 98,
                Status = 0,
                
                CssClass = 98,
                ListClass = 98,
                Remark = "教育",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "教育",
                DictValue = "P83",
                OrderNum = 99,
                Status = 0,
                
                CssClass = 99,
                ListClass = 99,
                Remark = "教育",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // Q 卫生和社会工作
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "卫生和社会工作",
                DictValue = "Q",
                OrderNum = 100,
                Status = 0,
                
                CssClass = 100,
                ListClass = 100,
                Remark = "卫生和社会工作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "卫生",
                DictValue = "Q84",
                OrderNum = 101,
                Status = 0,
                
                CssClass = 101,
                ListClass = 101,
                Remark = "卫生",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "社会工作",
                DictValue = "Q85",
                OrderNum = 102,
                Status = 0,
                
                CssClass = 102,
                ListClass = 102,
                Remark = "社会工作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // R 文化、体育和娱乐业
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "文化、体育和娱乐业",
                DictValue = "R",
                OrderNum = 103,
                Status = 0,
                
                CssClass = 103,
                ListClass = 103,
                Remark = "文化、体育和娱乐业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "新闻和出版业",
                DictValue = "R86",
                OrderNum = 104,
                Status = 0,
                
                CssClass = 104,
                ListClass = 104,
                Remark = "新闻和出版业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "广播、电视、电影和录音制作业",
                DictValue = "R87",
                OrderNum = 105,
                Status = 0,
                
                CssClass = 105,
                ListClass = 105,
                Remark = "广播、电视、电影和录音制作业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "文化艺术业",
                DictValue = "R88",
                OrderNum = 106,
                Status = 0,
                
                CssClass = 106,
                ListClass = 106,
                Remark = "文化艺术业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "体育",
                DictValue = "R89",
                OrderNum = 107,
                Status = 0,
                
                CssClass = 107,
                ListClass = 107,
                Remark = "体育",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "娱乐业",
                DictValue = "R90",
                OrderNum = 108,
                Status = 0,
                
                CssClass = 108,
                ListClass = 108,
                Remark = "娱乐业",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // S 公共管理、社会保障和社会组织
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "公共管理、社会保障和社会组织",
                DictValue = "S",
                OrderNum = 109,
                Status = 0,
                
                CssClass = 109,
                ListClass = 109,
                Remark = "公共管理、社会保障和社会组织",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "中国共产党机关",
                DictValue = "S91",
                OrderNum = 110,
                Status = 0,
                
                CssClass = 110,
                ListClass = 110,
                Remark = "中国共产党机关",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "国家机构",
                DictValue = "S92",
                OrderNum = 111,
                Status = 0,
                
                CssClass = 111,
                ListClass = 111,
                Remark = "国家机构",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "人民政协、民主党派",
                DictValue = "S93",
                OrderNum = 112,
                Status = 0,
                
                CssClass = 112,
                ListClass = 112,
                Remark = "人民政协、民主党派",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "社会保障",
                DictValue = "S94",
                OrderNum = 113,
                Status = 0,
                
                CssClass = 113,
                ListClass = 113,
                Remark = "社会保障",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "群众团体、社会团体和其他成员组织",
                DictValue = "S95",
                OrderNum = 114,
                Status = 0,
                
                CssClass = 114,
                ListClass = 114,
                Remark = "群众团体、社会团体和其他成员组织",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "基层群众自治组织",
                DictValue = "S96",
                OrderNum = 115,
                Status = 0,
                
                CssClass = 115,
                ListClass = 115,
                Remark = "基层群众自治组织",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // T 国际组织
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "国际组织",
                DictValue = "T",
                OrderNum = 116,
                Status = 0,
                
                CssClass = 116,
                ListClass = 116,
                Remark = "国际组织",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_industry_type",
                DictLabel = "国际组织",
                DictValue = "T97",
                OrderNum = 117,
                Status = 0,
                
                CssClass = 117,
                ListClass = 117,
                Remark = "国际组织",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictData in defaultDictData)
        {
            var existingDictData = await _dictDataRepository.GetFirstAsync(d => d.DictType == dictData.DictType && d.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                
                dictData.CreateBy = "Hbt365";
                dictData.CreateTime = DateTime.Now;
                dictData.UpdateBy = "Hbt365";
                dictData.UpdateTime = DateTime.Now;
                await _dictDataRepository.CreateAsync(dictData);
                insertCount++;
                _logger.Info($"[创建] 国民经济行业分类字典数据 '{dictData.DictLabel}' 创建成功");
            }
            else
            {
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.DictValue = dictData.DictValue;
                existingDictData.DictType = dictData.DictType;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.CssClass = dictData.CssClass;
                existingDictData.ListClass = dictData.ListClass;
                existingDictData.Status = dictData.Status;
                
                existingDictData.Remark = dictData.Remark;
                existingDictData.CreateBy = dictData.CreateBy;
                existingDictData.CreateTime = dictData.CreateTime;
                existingDictData.UpdateBy = "Hbt365";
                existingDictData.UpdateTime = DateTime.Now;

                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
                _logger.Info($"[更新] 国民经济行业分类字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
} 