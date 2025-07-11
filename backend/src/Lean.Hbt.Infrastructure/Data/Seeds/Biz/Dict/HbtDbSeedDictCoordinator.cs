//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDictCoordinator.cs
// 创建者 : Lean365
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述   : 字典种子数据协调器 - 使用仓储工厂模式
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;
using Lean.Hbt.Domain.Repositories;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 字典种子数据协调器
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-12-19
/// 功能说明:
/// 1. 统一管理字典类型和字典数据的初始化
/// 2. 使用仓储工厂模式支持多库架构
/// 3. 提供批量初始化功能
/// 4. 支持字典数据的增量更新
/// </remarks>
public class HbtDbSeedDictCoordinator
{
    private readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly IHbtLogger _logger;

    private IHbtRepository<HbtDictType> DictTypeRepository => _repositoryFactory.GetBusinessRepository<HbtDictType>();
    private IHbtRepository<HbtDictData> DictDataRepository => _repositoryFactory.GetBusinessRepository<HbtDictData>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    public HbtDbSeedDictCoordinator(IHbtRepositoryFactory repositoryFactory, IHbtLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 初始化所有字典数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<DictionarySeedResult> InitializeAllDictDataAsync()
    {
        try
        {
            _logger.Info("开始初始化所有字典数据...");

            var result = new DictionarySeedResult();

            // 1. 初始化系统基础字典类型
            var systemDictTypes = await InitializeSystemDictTypesAsync();
            result.DictTypeResults.Add("System", systemDictTypes);

            // 2. 初始化业务字典类型
            var businessDictTypes = await InitializeBusinessDictTypesAsync();
            result.DictTypeResults.Add("Business", businessDictTypes);

            // 3. 初始化系统基础字典数据
            var systemDictData = await InitializeSystemDictDataAsync();
            result.DictDataResults.Add("System", systemDictData);

            // 4. 初始化业务字典数据
            var businessDictData = await InitializeBusinessDictDataAsync();
            result.DictDataResults.Add("Business", businessDictData);

            _logger.Info($"字典数据初始化完成！字典类型: {result.GetTotalDictTypeCount()} 个, 字典数据: {result.GetTotalDictDataCount()} 条");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化字典数据失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化系统基础字典类型
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeSystemDictTypesAsync()
    {
        var systemSeed = new HbtDbSeedDictType();
        var dictTypes = systemSeed.GetDefaultDictTypes();
        return await InitializeDictTypesAsync(dictTypes, "系统基础字典类型");
    }

    /// <summary>
    /// 初始化业务字典类型
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeBusinessDictTypesAsync()
    {
        var allDictTypes = new List<HbtDictType>();

        // 客户服务字典类型
        var csDictTypes = new HbtDbSeedCsDictType();
        allDictTypes.AddRange(csDictTypes.GetCustomerServiceDictTypes());

        // 人力资源字典类型
        var hrDictTypes = new HbtDbSeedHrDictType();
        allDictTypes.AddRange(hrDictTypes.GetHrDictTypes());

        // 财务字典类型
        var financeDictTypes = new HbtDbSeedFinanceDictType();
        allDictTypes.AddRange(financeDictTypes.GetFinanceDictTypes());

        // 生产字典类型
        var productionDictTypes = new HbtDbSeedProductionDictType();
        allDictTypes.AddRange(productionDictTypes.GetProductionDictTypes());

        // 质量字典类型
        var qualityDictTypes = new HbtDbSeedQualityDictType();
        allDictTypes.AddRange(qualityDictTypes.GetQualityDictTypes());

        // 采购字典类型
        var purchaseDictTypes = new HbtDbSeedPurchaseDictType();
        allDictTypes.AddRange(purchaseDictTypes.GetPurchaseDictTypes());

        // 销售字典类型
        var salesDictTypes = new HbtDbSeedSalesDictType();
        allDictTypes.AddRange(salesDictTypes.GetSalesDictTypes());

        // 代码生成字典类型
        var generatorDictTypes = new HbtDbSeedGeneratorDictType();
        allDictTypes.AddRange(generatorDictTypes.GetGeneratorDictTypes());

        // 工作流字典类型
        var workflowDictTypes = new HbtDbSeedWorkflowDictType();
        allDictTypes.AddRange(workflowDictTypes.GetWorkflowDictTypes());

        // 设备字典类型
        var equipmentDictTypes = new HbtDbSeedEquipmentDictType();
        allDictTypes.AddRange(equipmentDictTypes.GetEquipmentDictTypes());

        // 文件字典类型
        var fileDictTypes = new HbtDbSeedFileDictType();
        allDictTypes.AddRange(fileDictTypes.GetFileDictTypes());

        // 工业字典类型
        var indDictTypes = new HbtDbSeedIndDictType();
        allDictTypes.AddRange(indDictTypes.GetIndDictTypes());

        // 物料字典类型
        var materialDictTypes = new HbtDbSeedMaterialDictType();
        allDictTypes.AddRange(materialDictTypes.GetMaterialDictTypes());

        // 联合国字典类型
        var unDictTypes = new HbtDbSeedUnDictType();
        allDictTypes.AddRange(unDictTypes.GetUnDictTypes());

        // 自然字典类型
        var natureDictTypes = new HbtDbSeedNatureDictType();
        allDictTypes.AddRange(natureDictTypes.GetNatureDictTypes());

        // OA字典类型
        var oaDictTypes = new HbtDbSeedOADictType();
        allDictTypes.AddRange(oaDictTypes.GetOADictTypes());

        return await InitializeDictTypesAsync(allDictTypes, "业务字典类型");
    }

    /// <summary>
    /// 初始化系统基础字典数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeSystemDictDataAsync()
    {
        var systemSeed = new HbtDbSeedDictData();
        var dictData = systemSeed.GetDefaultDictData();
        return await InitializeDictDataAsync(dictData, "系统基础字典数据");
    }

    /// <summary>
    /// 初始化业务字典数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeBusinessDictDataAsync()
    {
        var allDictData = new List<HbtDictData>();

        // 客户服务字典数据
        var csDictData = new HbtDbSeedCsDictData();
        allDictData.AddRange(csDictData.GetCustomerServiceDictData());

        // 人力资源字典数据
        var hrDictData = new HbtDbSeedHrDictData();
        allDictData.AddRange(hrDictData.GetHrDictData());

        // 财务字典数据
        var financeDictData = new HbtDbSeedFinanceDictData();
        allDictData.AddRange(financeDictData.GetFinanceDictData());

        // 生产字典数据
        var productionDictData = new HbtDbSeedProductionDictData();
        allDictData.AddRange(productionDictData.GetProductionDictData());

        // 质量字典数据
        var qualityDictData = new HbtDbSeedQualityDictData();
        allDictData.AddRange(qualityDictData.GetQualityDictData());

        // 采购字典数据
        var purchaseDictData = new HbtDbSeedPurchaseDictData();
        allDictData.AddRange(purchaseDictData.GetPurchaseDictData());

        // 销售字典数据
        var salesDictData = new HbtDbSeedSalesDictData();
        allDictData.AddRange(salesDictData.GetSalesDictData());

        // 代码生成字典数据
        var generatorDictData = new HbtDbSeedGeneratorDictData();
        allDictData.AddRange(generatorDictData.GetGeneratorDictData());

        // 工作流字典数据
        var workflowDictData = new HbtDbSeedWorkflowDictData();
        allDictData.AddRange(workflowDictData.GetWorkflowDictData());

        // 设备字典数据
        var equipmentDictData = new HbtDbSeedEquipmentDictData();
        allDictData.AddRange(equipmentDictData.GetEquipmentDictData());

        // 文件字典数据
        var fileDictData = new HbtDbSeedFileDictData();
        allDictData.AddRange(fileDictData.GetFileDictData());

        // 工业字典数据
        var indDictData = new HbtDbSeedIndDictData();
        allDictData.AddRange(indDictData.GetIndDictData());

        // 物料字典数据
        var materialDictData = new HbtDbSeedMaterialDictData();
        allDictData.AddRange(materialDictData.GetMaterialDictData());

        // 联合国字典数据
        var unDictData = new HbtDbSeedUnDictData();
        allDictData.AddRange(unDictData.GetUnDictData());

        // 自然字典数据
        var natureDictData = new HbtDbSeedNatureDictData();
        allDictData.AddRange(natureDictData.GetNatureDictData());

        // OA字典数据
        var oaDictData = new HbtDbSeedOADictData();
        allDictData.AddRange(oaDictData.GetOADictData());

        return await InitializeDictDataAsync(allDictData, "业务字典数据");
    }

    /// <summary>
    /// 初始化字典类型
    /// </summary>
    /// <param name="dictTypes">字典类型列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeDictTypesAsync(List<HbtDictType> dictTypes, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var dictType in dictTypes)
        {
            var existingDictType = await DictTypeRepository.GetFirstAsync(d => d.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                dictType.CreateBy = "Hbt365";
                dictType.CreateTime = DateTime.Now;
                dictType.UpdateBy = "Hbt365";
                dictType.UpdateTime = DateTime.Now;
                await DictTypeRepository.CreateAsync(dictType);
                insertCount++;
                _logger.Info($"[创建] {category} '{dictType.DictName}' 创建成功");
            }
            else
            {
                existingDictType.DictName = dictType.DictName;
                existingDictType.OrderNum = dictType.OrderNum;
                existingDictType.Status = dictType.Status;
                existingDictType.Remark = dictType.Remark;
                existingDictType.UpdateBy = "Hbt365";
                existingDictType.UpdateTime = DateTime.Now;

                await DictTypeRepository.UpdateAsync(existingDictType);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingDictType.DictName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化字典数据
    /// </summary>
    /// <param name="dictData">字典数据列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeDictDataAsync(List<HbtDictData> dictData, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var data in dictData)
        {
            var existingDictData = await DictDataRepository.GetFirstAsync(d => d.DictType == data.DictType && d.DictValue == data.DictValue);
            if (existingDictData == null)
            {
                data.CreateBy = "Hbt365";
                data.CreateTime = DateTime.Now;
                data.UpdateBy = "Hbt365";
                data.UpdateTime = DateTime.Now;
                await DictDataRepository.CreateAsync(data);
                insertCount++;
                _logger.Info($"[创建] {category} '{data.DictLabel}' 创建成功");
            }
            else
            {
                existingDictData.DictLabel = data.DictLabel;
                existingDictData.DictValue = data.DictValue;
                existingDictData.DictType = data.DictType;
                existingDictData.OrderNum = data.OrderNum;
                existingDictData.CssClass = data.CssClass;
                existingDictData.ListClass = data.ListClass;
                existingDictData.Status = data.Status;
                existingDictData.Remark = data.Remark;
                existingDictData.CreateBy = "Hbt365";
                existingDictData.CreateTime = DateTime.Now;
                existingDictData.UpdateBy = "Hbt365";
                existingDictData.UpdateTime = DateTime.Now;

                await DictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 根据字典类型获取字典数据
    /// </summary>
    /// <param name="dictType">字典类型</param>
    /// <returns>字典数据列表</returns>
    public async Task<List<HbtDictData>> GetDictDataByTypeAsync(string dictType)
    {
        var dictData = await DictDataRepository.GetListAsync(d => d.DictType == dictType && d.Status == 0);
        return dictData.OrderBy(d => d.OrderNum).ToList();
    }

    /// <summary>
    /// 获取所有字典类型
    /// </summary>
    /// <returns>字典类型列表</returns>
    public async Task<List<HbtDictType>> GetAllDictTypesAsync()
    {
        var dictTypes = await DictTypeRepository.GetListAsync(d => d.Status == 0);
        return dictTypes.OrderBy(d => d.OrderNum).ToList();
    }

    /// <summary>
    /// 清理字典数据
    /// </summary>
    /// <param name="dictType">字典类型</param>
    /// <returns>清理结果</returns>
    public async Task<bool> CleanDictDataAsync(string dictType)
    {
        try
        {
            var result = await DictDataRepository.DeleteAsync(d => d.DictType == dictType);
            _logger.Info($"清理字典类型 '{dictType}' 的数据，共删除 {result} 条记录");
            return result > 0;
        }
        catch (Exception ex)
        {
            _logger.Error($"清理字典数据失败: {ex.Message}", ex);
            return false;
        }
    }
}

/// <summary>
/// 字典种子初始化结果
/// </summary>
public class DictionarySeedResult
{
    /// <summary>
    /// 字典类型初始化结果
    /// </summary>
    public Dictionary<string, (int insertCount, int updateCount)> DictTypeResults { get; set; } = new();

    /// <summary>
    /// 字典数据初始化结果
    /// </summary>
    public Dictionary<string, (int insertCount, int updateCount)> DictDataResults { get; set; } = new();

    /// <summary>
    /// 获取字典类型总数
    /// </summary>
    /// <returns>总数</returns>
    public int GetTotalDictTypeCount()
    {
        return DictTypeResults.Values.Sum(x => x.insertCount + x.updateCount);
    }

    /// <summary>
    /// 获取字典数据总数
    /// </summary>
    /// <returns>总数</returns>
    public int GetTotalDictDataCount()
    {
        return DictDataResults.Values.Sum(x => x.insertCount + x.updateCount);
    }

    /// <summary>
    /// 获取总插入数
    /// </summary>
    /// <returns>插入数</returns>
    public int GetTotalInsertCount()
    {
        var typeInsert = DictTypeResults.Values.Sum(x => x.insertCount);
        var dataInsert = DictDataResults.Values.Sum(x => x.insertCount);
        return typeInsert + dataInsert;
    }

    /// <summary>
    /// 获取总更新数
    /// </summary>
    /// <returns>更新数</returns>
    public int GetTotalUpdateCount()
    {
        var typeUpdate = DictTypeResults.Values.Sum(x => x.updateCount);
        var dataUpdate = DictDataResults.Values.Sum(x => x.updateCount);
        return typeUpdate + dataUpdate;
    }
} 