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
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using SqlSugar;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 字典数据服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class HbtDictDataService : IHbtDictDataService
    {
        private readonly IHbtRepository<HbtDictData> _dictDataRepository;
        private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictDataRepository">字典数据仓储</param>
        /// <param name="dictTypeRepository">字典类型仓储</param>
        /// <param name="logger">日志接口</param>
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
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtDictDataDto>> GetPagedListAsync(HbtDictDataQueryDto query)
        {
            var exp = Expressionable.Create<HbtDictData>();

            if (!string.IsNullOrEmpty(query.DictType))
                exp.And(x => x.DictType == query.DictType);

            if (!string.IsNullOrEmpty(query.DictLabel))
                exp.And(x => x.DictLabel.Contains(query.DictLabel));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var (list, total) = await _dictDataRepository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);
            return new HbtPagedResult<HbtDictDataDto>
            {
                Rows = list.Adapt<List<HbtDictDataDto>>(),
                TotalNum = total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取字典数据详情
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>返回字典数据详情</returns>
        public async Task<HbtDictDataDto> GetAsync(long dictDataId)
        {
            var dictData = await _dictDataRepository.GetByIdAsync(dictDataId);
            if (dictData == null)
                throw new HbtException("字典数据不存在");

            return dictData.Adapt<HbtDictDataDto>();
        }

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="input">字典数据创建信息</param>
        /// <returns>返回新创建的字典数据ID</returns>
        public async Task<long> InsertAsync(HbtDictDataCreateDto input)
        {
            var dictData = input.Adapt<HbtDictData>();
            dictData.Status = 0; // 0表示正常状态

            var result = await _dictDataRepository.InsertAsync(dictData);
            return result > 0 ? dictData.Id : 0;
        }

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="input">字典数据更新信息</param>
        /// <returns>返回是否更新成功</returns>
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
        /// <returns>返回是否删除成功</returns>
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
        /// <param name="dictDataIds">字典数据ID集合</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] dictDataIds)
        {
            if (dictDataIds == null || dictDataIds.Length == 0)
                return false;

            var entities = await _dictDataRepository.GetListAsync(x => dictDataIds.Contains(x.Id));
            return await _dictDataRepository.DeleteRangeAsync(entities) > 0;
        }

        /// <summary>
        /// 导入字典数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
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
                        await _dictDataRepository.InsertAsync(dictData);
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
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
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
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
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
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否更新成功</returns>
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
        /// <param name="dictType">字典类型</param>
        /// <returns>返回字典数据选项列表</returns>
        public async Task<List<HbtDictDataDto>> GetOptionsAsync(string dictType)
        {
            var list = await _dictDataRepository.GetListAsync(x => x.DictType == dictType && x.Status == 0); // 0表示正常状态
            return list.OrderBy(x => x.OrderNum).Adapt<List<HbtDictDataDto>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dictType"></param>
        /// <param name="dictValue"></param>
        /// <returns></returns>
        public async Task<bool> CheckDictDataExists(string dictType, string dictValue)
        {
            var query = from d in _dictDataRepository.GetListAsync().Result
                        where d.DictType == dictType
                        && d.DictValue == dictValue
                        && d.Status == 0 // 0表示正常状态
                        select d;
            return await Task.FromResult(query.Any());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CheckBuiltinData(long id)
        {
            var dictData = await _dictDataRepository.GetByIdAsync(id);
            if (dictData == null) return false;

            var dictType = await _dictTypeRepository.FirstOrDefaultAsync(x => x.DictType == dictData.DictType);
            return dictType != null && dictType.DictBuiltin == 1; // 1表示内置
        }

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
        /// 获取字典数据列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<HbtDictDataDto>> GetListAsync()
        {
            var list = await _dictDataRepository.GetListAsync();
            return list.Select(x => new HbtDictDataDto
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
        /// <param name="dictType">字典类型</param>
        /// <returns>字典数据列表</returns>
        public async Task<List<HbtDictDataDto>> GetListByDictTypeAsync(string dictType)
        {
            if (string.IsNullOrEmpty(dictType))
                throw new HbtException("字典类型不能为空");

            var list = await _dictDataRepository.GetListAsync(x => x.DictType == dictType && x.Status == 0);
            return list.Select(x => new HbtDictDataDto
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