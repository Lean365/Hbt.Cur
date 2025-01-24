//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictTypeService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典类型服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using Mapster;
using SqlSugar;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Services.Admin
{
    /// <summary>
    /// 字典类型服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-18
    /// </remarks>
    public class HbtDictTypeService : IHbtDictTypeService
    {
        private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
        private readonly IHbtRepository<HbtDictData> _dictDataRepository;
        private readonly IHbtDbContext _dbContext;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictTypeRepository">字典类型仓储</param>
        /// <param name="dictDataRepository">字典数据仓储</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="logger">日志接口</param>
        public HbtDictTypeService(
            IHbtRepository<HbtDictType> dictTypeRepository,
            IHbtRepository<HbtDictData> dictDataRepository,
            IHbtDbContext dbContext,
            IHbtLogger logger)
        {
            _dictTypeRepository = dictTypeRepository;
            _dictDataRepository = dictDataRepository;
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 获取字典类型分页列表
        /// </summary>
        public async Task<HbtPagedResult<HbtDictTypeDto>> GetPagedListAsync(HbtDictTypeQueryDto query)
        {
            var exp = Expressionable.Create<HbtDictType>();

            if (!string.IsNullOrEmpty(query.DictName))
                exp.And(x => x.DictName.Contains(query.DictName));

            if (!string.IsNullOrEmpty(query.DictType))
                exp.And(x => x.DictType.Contains(query.DictType));

            if (query.DictCategory.HasValue)
                exp.And(x => x.DictCategory == query.DictCategory.Value);

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var (list, total) = await _dictTypeRepository.GetPagedListAsync(exp.ToExpression(), query.PageIndex, query.PageSize);
            return new HbtPagedResult<HbtDictTypeDto>
            {
                Rows = list.Adapt<List<HbtDictTypeDto>>(),
                TotalNum = total,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取字典类型详情
        /// </summary>
        public async Task<HbtDictTypeDto> GetAsync(long dictTypeId)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(dictTypeId);
            if (dictType == null)
                throw new HbtException($"字典类型[{dictTypeId}]不存在");

            return dictType.Adapt<HbtDictTypeDto>();
        }

        /// <summary>
        /// 创建字典类型
        /// </summary>
        public async Task<long> InsertAsync(HbtDictTypeCreateDto input)
        {
            await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictName", input.DictName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictType", input.DictType);

            var dictType = input.Adapt<HbtDictType>();
            var result = await _dictTypeRepository.InsertAsync(dictType);
            return result > 0 ? dictType.Id : 0;
        }

        /// <summary>
        /// 更新字典类型
        /// </summary>
        public async Task<bool> UpdateAsync(HbtDictTypeUpdateDto input)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(input.DictTypeId);
            if (dictType == null)
                throw new HbtException($"字典类型[{input.DictTypeId}]不存在");

            if (dictType.DictName != input.DictName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictName", input.DictName, input.DictTypeId);

            if (dictType.DictType != input.DictType)
                await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictType", input.DictType, input.DictTypeId);

            dictType.DictName = input.DictName;
            dictType.DictType = input.DictType;
            dictType.DictCategory = input.DictCategory;
            dictType.SqlScript = input.SqlScript;
            dictType.OrderNum = input.OrderNum;
            dictType.Status = input.Status;

            return await _dictTypeRepository.UpdateAsync(dictType) > 0;
        }

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long dictTypeId)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(dictTypeId);
            if (dictType == null)
            {
                throw new HbtException($"字典类型[{dictTypeId}]不存在");
            }

            try
            {
                _dbContext.BeginTran();

                // 删除字典类型
                await _dictTypeRepository.DeleteAsync(dictType);

                // 删除关联的字典数据
                var dictDataList = await _dictDataRepository.GetListAsync(x => x.DictType == dictType.DictType);
                if (dictDataList?.Count > 0)
                {
                    await _dictDataRepository.DeleteRangeAsync(dictDataList);
                }

                _dbContext.CommitTran();
                return true;
            }
            catch (Exception)
            {
                _dbContext.RollbackTran();
                throw;
            }
        }

        /// <summary>
        /// 批量删除字典类型
        /// </summary>
        /// <param name="dictTypeIds">字典类型ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] dictTypeIds)
        {
            if (dictTypeIds == null || dictTypeIds.Length == 0)
            {
                throw new HbtException("请选择要删除的字典类型");
            }

            try
            {
                _dbContext.BeginTran();

                foreach (var dictTypeId in dictTypeIds)
                {
                    var dictType = await _dictTypeRepository.GetByIdAsync(dictTypeId);
                    if (dictType == null)
                    {
                        continue;
                    }

                    // 删除字典类型
                    await _dictTypeRepository.DeleteAsync(dictType);

                    // 删除关联的字典数据
                    var dictDataList = await _dictDataRepository.GetListAsync(x => x.DictType == dictType.DictType);
                    if (dictDataList?.Count > 0)
                    {
                        await _dictDataRepository.DeleteRangeAsync(dictDataList);
                    }
                }

                _dbContext.CommitTran();
                return true;
            }
            catch (Exception)
            {
                _dbContext.RollbackTran();
                throw;
            }
        }

        /// <summary>
        /// 导入字典类型数据
        /// </summary>
        public async Task<(int success, int fail)> ImportAsync(List<HbtDictTypeImportDto> dictTypes)
        {
            if (dictTypes == null || !dictTypes.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var item in dictTypes)
            {
                try
                {
                    var dictType = item.Adapt<HbtDictType>();
                    await _dictTypeRepository.InsertAsync(dictType);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error($"导入字典类型失败：{ex.Message}", ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出字典类型数据
        /// </summary>
        public async Task<List<HbtDictTypeExportDto>> ExportAsync(HbtDictTypeQueryDto query)
        {
            var exp = Expressionable.Create<HbtDictType>();

            if (!string.IsNullOrEmpty(query.DictName))
                exp.And(x => x.DictName.Contains(query.DictName));

            if (!string.IsNullOrEmpty(query.DictType))
                exp.And(x => x.DictType.Contains(query.DictType));

            if (query.DictCategory.HasValue)
                exp.And(x => x.DictCategory == query.DictCategory.Value);

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            var list = await _dictTypeRepository.GetListAsync(exp.ToExpression());
            return list.Adapt<List<HbtDictTypeExportDto>>();
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>模板数据</returns>
        public async Task<HbtDictTypeTemplateDto> GetTemplateAsync()
        {
            return await Task.FromResult(new HbtDictTypeTemplateDto
            {
                DictName = "示例字典",
                DictType = "sys_example",
                DictCategory = 0,
                SqlScript = "",
                OrderNum = 0,
                Status = HbtStatus.Normal.ToString()
            });
        }

        /// <summary>
        /// 更新字典类型状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtDictTypeStatusDto input)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(input.DictTypeId);
            if (dictType == null)
                throw new HbtException($"字典类型[{input.DictTypeId}]不存在");

            dictType.Status = input.Status;
            return await _dictTypeRepository.UpdateAsync(dictType) > 0;
        }
    }
}