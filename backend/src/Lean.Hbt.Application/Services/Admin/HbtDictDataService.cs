//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictDataService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据服务实现类
//===================================================================

#nullable enable

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Domain.Entities.Admin;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 字典数据服务实现类
    /// </summary>
    /// <remarks>
    /// 此服务类提供字典数据的增删改查等基础操作，包括：
    /// 1. 字典数据的分页查询
    /// 2. 字典数据的创建、更新、删除
    /// 3. 字典数据的导入导出
    /// 4. 字典数据状态管理
    /// 5. 字典数据选项列表获取
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class HbtDictDataService : IHbtDictDataService
    {
        private readonly IHbtRepository<HbtDictData> _dictDataRepository;
        private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="dictDataRepository">字典数据仓储接口</param>
        /// <param name="dictTypeRepository">字典类型仓储接口</param>
        /// <param name="logger">日志记录接口</param>
        public HbtDictDataService(
            IHbtRepository<HbtDictData> dictDataRepository,
            IHbtRepository<HbtDictType> dictTypeRepository,
            IHbtLogger logger)
        {
            _dictDataRepository = dictDataRepository;
            _dictTypeRepository = dictTypeRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取字典数据分页列表
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// - DictType：字典类型
        /// - DictLabel：字典标签
        /// - DictValue：字典值
        /// - Status：状态
        /// - PageIndex：页码
        /// - PageSize：每页记录数</param>
        /// <returns>返回分页结果，包含：
        /// - Rows：当前页数据列表
        /// - TotalNum：总记录数
        /// - PageIndex：当前页码
        /// - PageSize：每页记录数</returns>
        public async Task<HbtPagedResult<HbtDictDataDto>> GetListAsync(HbtDictDataQueryDto query)
        {
            // 1.构建查询条件
            var exp = Expressionable.Create<HbtDictData>();

            if (!string.IsNullOrEmpty(query.DictType))
                exp = exp.And(d => d.DictType.Contains(query.DictType));

            if (!string.IsNullOrEmpty(query.DictLabel))
                exp = exp.And(d => d.DictLabel.Contains(query.DictLabel));

            if (!string.IsNullOrEmpty(query.DictValue))
                exp = exp.And(d => d.DictValue.Contains(query.DictValue));

            if (query.Status.HasValue)
                exp = exp.And(d => d.Status == query.Status.Value);

            // 2.查询数据
            var result = await _dictDataRepository.GetPagedListAsync(
                exp.ToExpression(),
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.OrderNum,
                OrderByType.Asc);

            // 3.转换数据
            return new HbtPagedResult<HbtDictDataDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Select(x => x.Adapt<HbtDictDataDto>()).ToList()
            };
        }

        /// <summary>
        /// 根据ID获取字典数据详情
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>返回字典数据详情DTO</returns>
        /// <exception cref="HbtException">当字典数据不存在时抛出异常</exception>
        public async Task<HbtDictDataDto> GetByIdAsync(long dictDataId)
        {
            var dictData = await _dictDataRepository.GetByIdAsync(dictDataId);
            if (dictData == null)
                throw new HbtException("字典数据不存在");

            return dictData.Adapt<HbtDictDataDto>();
        }

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="input">字典数据创建DTO，包含：
        /// - DictType：字典类型
        /// - DictLabel：字典标签
        /// - DictValue：字典值
        /// - OrderNum：排序号
        /// - CssClass：CSS类名
        /// - ListClass：列表类名
        /// - Remark：备注</param>
        /// <returns>返回新创建的字典数据ID，创建失败返回0</returns>
        public async Task<long> CreateAsync(HbtDictDataCreateDto input)
        {
            var dictData = input.Adapt<HbtDictData>();
            dictData.Status = 0; // 0表示正常状态

            var result = await _dictDataRepository.CreateAsync(dictData);
            return result > 0 ? dictData.Id : 0;
        }

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="input">字典数据更新DTO，包含：
        /// - DictDataId：字典数据ID
        /// - DictType：字典类型
        /// - DictLabel：字典标签
        /// - DictValue：字典值
        /// - OrderNum：排序号
        /// - CssClass：CSS类名
        /// - ListClass：列表类名
        /// - Status：状态
        /// - Remark：备注</param>
        /// <returns>返回更新是否成功</returns>
        /// <exception cref="HbtException">当字典数据不存在时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtDictDataUpdateDto input)
        {
            var dictData = await _dictDataRepository.GetByIdAsync(input.DictDataId);
            if (dictData == null)
                throw new HbtException("字典数据不存在");

            if (dictData.DictType != input.DictType)
                await HbtValidateUtils.ValidateFieldExistsAsync(_dictDataRepository, "DictType", input.DictType, input.DictDataId);

            dictData.DictType = input.DictType;
            dictData.DictLabel = input.DictLabel;
            dictData.DictValue = input.DictValue;
            dictData.OrderNum = input.OrderNum;
            dictData.CssClass = input.CssClass;
            dictData.ListClass = input.ListClass;
            dictData.Status = input.Status;
            dictData.Remark = input.Remark;

            return await _dictDataRepository.UpdateAsync(dictData) > 0;
        }

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>返回删除是否成功</returns>
        /// <exception cref="HbtException">当字典数据不存在时抛出异常</exception>
        public async Task<bool> DeleteAsync(long dictDataId)
        {
            var dictData = await _dictDataRepository.GetByIdAsync(dictDataId);
            if (dictData == null)
                throw new HbtException("字典数据不存在");

            return await _dictDataRepository.DeleteAsync(dictData) > 0;
        }

        /// <summary>
        /// 批量删除字典数据
        /// </summary>
        /// <param name="dictDataIds">字典数据ID数组</param>
        /// <returns>返回删除是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] dictDataIds)
        {
            if (dictDataIds == null || dictDataIds.Length == 0)
                return false;

            var result = await _dictDataRepository.GetListAsync(x => dictDataIds.Contains(x.Id));
            return await _dictDataRepository.DeleteRangeAsync(result) > 0;
        }

        /// <summary>
        /// 导入字典数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称，默认为"字典数据"</param>
        /// <returns>返回导入结果元组：(成功数量, 失败数量)</returns>
        /// <exception cref="HbtException">当导入过程出现错误时抛出异常</exception>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "字典数据")
        {
            var success = 0;
            var fail = 0;

            try
            {
                // 1.从Excel导入数据
                var dictDataList = await HbtExcelHelper.ImportAsync<HbtDictDataDto>(fileStream, sheetName);
                if (dictDataList == null || !dictDataList.Any())
                    return (0, 0);

                // 2.保存数据
                foreach (var item in dictDataList)
                {
                    try
                    {
                        var dictData = item.Adapt<HbtDictData>();
                        await _dictDataRepository.CreateAsync(dictData);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"导入字典数据失败：{ex.Message}", ex);
                        fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"导入字典数据失败：{ex.Message}", ex);
                throw new HbtException("导入字典数据失败");
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// - DictType：字典类型
        /// - DictLabel：字典标签
        /// - Status：状态</param>
        /// <param name="sheetName">工作表名称，默认为"字典数据"</param>
        /// <returns>返回Excel文件的字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtDictDataQueryDto query, string sheetName = "字典数据")
        {
            var exp = Expressionable.Create<HbtDictData>();

            if (!string.IsNullOrEmpty(query.DictType))
                exp.And(x => x.DictType == query.DictType);

            if (!string.IsNullOrEmpty(query.DictLabel))
                exp.And(x => x.DictLabel.Contains(query.DictLabel));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var list = await _dictDataRepository.GetListAsync(exp.ToExpression());
            var dtos = list.Adapt<List<HbtDictDataDto>>();

            return await HbtExcelHelper.ExportAsync(dtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称，默认为"字典数据"</param>
        /// <returns>返回Excel模板文件的字节数组</returns>
        public async Task<byte[]> GetTemplateAsync(string sheetName = "字典数据")
        {
            var template = new List<HbtDictDataDto>
            {
                new HbtDictDataDto
                {
                    DictType = "sys_user_sex",
                    DictLabel = "男",
                    DictValue = "1",
                    OrderNum = 1,
                    Status = 0, // 0表示正常状态
                    Remark = "性别男"
                }
            };

            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 更新字典数据状态
        /// </summary>
        /// <param name="input">状态更新DTO，包含：
        /// - DictDataId：字典数据ID
        /// - Status：状态值</param>
        /// <returns>返回更新是否成功</returns>
        /// <exception cref="HbtException">当字典数据不存在时抛出异常</exception>
        public async Task<bool> UpdateStatusAsync(HbtDictDataStatusDto input)
        {
            var dictData = await _dictDataRepository.GetByIdAsync(input.DictDataId);
            if (dictData == null)
                throw new HbtException("字典数据不存在");

            dictData.Status = input.Status;
            return await _dictDataRepository.UpdateAsync(dictData) > 0;
        }

        /// <summary>
        /// 获取字典数据选项列表
        /// </summary>
        /// <param name="dictType">字典类型编码</param>
        /// <returns>返回字典数据选项列表，按排序号升序排列</returns>
        public async Task<List<HbtDictDataDto>> GetOptionsAsync(string dictType)
        {
            var result = await _dictDataRepository.GetListAsync(x => x.DictType == dictType && x.Status == 0);
            return result.OrderBy(x => x.OrderNum).Adapt<List<HbtDictDataDto>>();
        }

        /// <summary>
        /// 检查字典数据是否存在
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <param name="dictValue">字典值</param>
        /// <param name="excludeId">排除的字典数据ID</param>
        /// <returns>返回是否存在</returns>
        public async Task<bool> CheckDictDataExists(string dictType, string dictValue, long? excludeId = null)
        {
            var result = await _dictDataRepository.GetListAsync(x => x.DictType == dictType && x.Status == 0);
            return result.Any(x => x.DictValue == dictValue && x.Id != (excludeId ?? 0));
        }

        /// <summary>
        /// 检查是否为内置字典数据
        /// </summary>
        /// <param name="id">字典数据ID</param>
        /// <returns>返回是否为内置数据</returns>
        public async Task<bool> CheckBuiltinData(long id)
        {
            var dictData = await _dictDataRepository.GetByIdAsync(id);
            if (dictData == null) return false;

            var dictType = await _dictTypeRepository.GetFirstAsync(x => x.DictType == dictData.DictType);
            return dictType != null && dictType.DictBuiltin == 1; // 1表示内置
        }

        /// <summary>
        /// 将动态对象列表转换为DTO列表
        /// </summary>
        /// <param name="dynamicList">动态对象列表</param>
        /// <returns>返回字典数据DTO列表</returns>
        private List<HbtDictDataDto> ConvertToDtoList(List<dynamic> dynamicList)
        {
            var dtoList = new List<HbtDictDataDto>();
            foreach (var item in dynamicList)
            {
                var dto = ConvertToDto(item);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        /// <summary>
        /// 将动态对象转换为DTO对象
        /// </summary>
        /// <param name="item">动态对象</param>
        /// <returns>返回字典数据DTO</returns>
        private HbtDictDataDto ConvertToDto(dynamic item)
        {
            return new HbtDictDataDto
            {
                DictDataId = Convert.ToInt64(item.id),
                DictType = Convert.ToString(item.dict_type) ?? string.Empty,
                DictLabel = Convert.ToString(item.dict_label) ?? string.Empty,
                DictValue = Convert.ToString(item.dict_value) ?? string.Empty,
                Status = Convert.ToInt32(item.status),
                OrderNum = Convert.ToInt32(item.order_num),
                CssClass = item.css_class != null ? Convert.ToInt32(item.css_class) : null,
                ListClass = item.list_class != null ? Convert.ToInt32(item.list_class) : null,
                ExtLabel = Convert.ToString(item.ext_label),
                ExtValue = Convert.ToString(item.ext_value),
                TransKey = Convert.ToString(item.trans_key),
                Remark = Convert.ToString(item.remark)
            };
        }

        /// <summary>
        /// 获取所有字典数据列表
        /// </summary>
        /// <returns>返回字典数据DTO列表</returns>
        /// <remarks>
        /// 此方法会返回所有字典数据，不包含分页。
        /// 返回的数据包含：
        /// - 字典数据ID
        /// - 字典类型
        /// - 字典标签
        /// - 字典值
        /// - 排序号
        /// - 状态
        /// - 备注
        /// </remarks>
        public async Task<List<HbtDictDataDto>> GetListAsync()
        {
            var result = await _dictDataRepository.GetListAsync();
            return result.Select(x => new HbtDictDataDto
            {
                DictDataId = x.Id,
                DictType = x.DictType,
                DictLabel = x.DictLabel,
                DictValue = x.DictValue,
                OrderNum = x.OrderNum,
                Status = x.Status,
                Remark = x.Remark
            }).ToList();
        }

        /// <summary>
        /// 根据字典类型获取字典数据列表
        /// </summary>
        /// <param name="dictType">字典类型编码</param>
        /// <returns>返回指定类型的字典数据DTO列表</returns>
        /// <remarks>
        /// 此方法会返回指定字典类型的所有有效数据（状态为0的记录）。
        /// 返回的数据包含：
        /// - 字典数据ID
        /// - 字典类型
        /// - 字典标签
        /// - 字典值
        /// - 显示标签
        /// - 实际值
        /// - CSS类名
        /// - 列表类名
        /// - 状态
        /// - 扩展标签
        /// - 扩展值
        /// - 翻译键
        /// - 排序号
        /// - 备注
        /// 返回结果会按照排序号（OrderNum）升序排列
        /// </remarks>
        /// <exception cref="HbtException">当字典类型为空时抛出异常</exception>
        public async Task<List<HbtDictDataDto>> GetListByDictTypeAsync(string dictType)
        {
            if (string.IsNullOrEmpty(dictType))
                throw new HbtException("字典类型不能为空");

            var result = await _dictDataRepository.GetListAsync(x => x.DictType == dictType && x.Status == 0);
            return result.Select(x => new HbtDictDataDto
            {
                DictDataId = x.Id,
                DictType = x.DictType,
                DictLabel = x.DictLabel,
                DictValue = x.DictValue,
                Label = x.DictLabel,
                Value = x.DictValue,
                CssClass = x.CssClass,
                ListClass = x.ListClass,
                Status = x.Status,
                ExtLabel = x.ExtLabel,
                ExtValue = x.ExtValue,
                TransKey = x.TransKey,
                OrderNum = x.OrderNum,
                Remark = x.Remark
            }).OrderBy(x => x.OrderNum).ToList();
        }
    }
}