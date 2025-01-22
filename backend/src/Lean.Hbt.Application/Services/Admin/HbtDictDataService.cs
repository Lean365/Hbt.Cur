//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictDataService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据服务实现类
//===================================================================

#nullable enable

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.IServices;
using Mapster;
using SqlSugar;
using Lean.Hbt.Domain.Utils;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 字典数据服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-18
    /// </remarks>
    public class HbtDictDataService : IHbtDictDataService
    {
        private readonly IHbtRepository<HbtDictData> _dictDataRepository;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataService(
            IHbtRepository<HbtDictData> dictDataRepository,
            IHbtLogger logger)
        {
            _dictDataRepository = dictDataRepository;
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
            await HbtValidateUtils.ValidateFieldExistsAsync(_dictDataRepository, "DictLabel", input.DictLabel);
            await HbtValidateUtils.ValidateFieldExistsAsync(_dictDataRepository, "DictValue", input.DictValue);

            var dictData = input.Adapt<HbtDictData>();
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

            if (dictData.DictLabel != input.DictLabel)
                await HbtValidateUtils.ValidateFieldExistsAsync(_dictDataRepository, "DictLabel", input.DictLabel, input.DictDataId);

            if (dictData.DictValue != input.DictValue)
                await HbtValidateUtils.ValidateFieldExistsAsync(_dictDataRepository, "DictValue", input.DictValue, input.DictDataId);

            dictData.DictLabel = input.DictLabel;
            dictData.DictValue = input.DictValue;
            dictData.DictType = input.DictType;
            dictData.CssClass = input.CssClass;
            dictData.ListClass = input.ListClass;
            dictData.OrderNum = input.OrderNum;
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
        /// <param name="dictDatas">字典数据列表</param>
        /// <returns>返回导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(List<HbtDictDataImportDto> dictDatas)
        {
            if (dictDatas == null || !dictDatas.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var item in dictDatas)
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

            return (success, fail);
        }

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回导出数据列表</returns>
        public async Task<List<HbtDictDataExportDto>> ExportAsync(HbtDictDataQueryDto query)
        {
            var exp = Expressionable.Create<HbtDictData>();

            if (!string.IsNullOrEmpty(query.DictType))
                exp.And(x => x.DictType == query.DictType);

            if (!string.IsNullOrEmpty(query.DictLabel))
                exp.And(x => x.DictLabel.Contains(query.DictLabel));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var list = await _dictDataRepository.GetListAsync(exp.ToExpression());
            return list.Adapt<List<HbtDictDataExportDto>>();
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>返回模板数据</returns>
        public async Task<HbtDictDataTemplateDto> GetTemplateAsync()
        {
            return await Task.FromResult(new HbtDictDataTemplateDto());
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
    }
} 