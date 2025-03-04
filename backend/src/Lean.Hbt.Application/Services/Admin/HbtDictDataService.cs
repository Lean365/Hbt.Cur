//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictDataService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据服务实现类
//===================================================================

#nullable enable

using System;
using System.IO;
using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Common.Helpers;
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
        /// <param name="id">字典数据ID</param>
        /// <returns>返回字典数据详情</returns>
        public async Task<HbtDictDataDto> GetAsync(long id)
        {
            var dictData = await _dictDataRepository.GetByIdAsync(id);
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
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            var success = 0;
            var fail = 0;

            try
            {
                // 1.从Excel导入数据
                var dictDatas = await HbtExcelHelper.ImportAsync<HbtDictDataDto>(fileStream, sheetName);
                if (dictDatas == null || !dictDatas.Any())
                    return (0, 0);

                // 2.保存数据
                foreach (var dictData in dictDatas)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(dictData.DictLabel) || string.IsNullOrEmpty(dictData.DictValue))
                        {
                            _logger.Warn("导入字典数据失败：标签或值为空");
                            fail++;
                            continue;
                        }

                        // 验证字段是否已存在
                        try
                        {
                            await HbtValidateUtils.ValidateFieldExistsAsync(_dictDataRepository, "DictLabel", dictData.DictLabel);
                            await HbtValidateUtils.ValidateFieldExistsAsync(_dictDataRepository, "DictValue", dictData.DictValue);
                        }
                        catch (HbtException ex)
                        {
                            _logger.Warn($"导入字典数据失败：{ex.Message}");
                            fail++;
                            continue;
                        }

                        // 创建字典数据
                        var newDictData = dictData.Adapt<HbtDictData>();
                        await _dictDataRepository.InsertAsync(newDictData);
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
                return (0, 0);
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> ExportAsync(HbtDictDataQueryDto query, string sheetName)
        {
            var exp = Expressionable.Create<HbtDictData>();

            if (!string.IsNullOrEmpty(query?.DictType))
                exp.And(x => x.DictType == query.DictType);

            if (!string.IsNullOrEmpty(query?.DictLabel))
                exp.And(x => x.DictLabel.Contains(query.DictLabel));

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            var list = await _dictDataRepository.GetListAsync(exp.ToExpression());
            var dtos = list.Adapt<List<HbtDictDataDto>>();

            try
            {
                return await HbtExcelHelper.ExportAsync(dtos, sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error($"导出字典数据失败：{ex.Message}", ex);
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<byte[]> GetTemplateAsync(string sheetName)
        {
            var template = new List<HbtDictDataDto>
            {
                new HbtDictDataDto
                {
                    DictType = "sys_user_sex",
                    DictLabel = "男",
                    DictValue = "1",
                    Status = HbtStatus.Normal
                }
            };

            try
            {
                return await HbtExcelHelper.ExportAsync(template, sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error($"获取字典数据导入模板失败：{ex.Message}", ex);
                return Array.Empty<byte>();
            }
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