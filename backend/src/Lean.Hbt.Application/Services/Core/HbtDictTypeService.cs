#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictTypeService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述   : 字典类型服务实现类
//===================================================================

using Lean.Hbt.Application.Dtos.Core;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Entities.Core;
using System.Linq.Expressions;
using System.IO;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 字典类型服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-18
    /// </remarks>
    public class HbtDictTypeService : HbtBaseService, IHbtDictTypeService
    {
        private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
        private readonly IHbtRepository<HbtDictData> _dictDataRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictTypeService(
            IHbtRepository<HbtDictType> dictTypeRepository,
            IHbtRepository<HbtDictData> dictDataRepository,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _dictTypeRepository = dictTypeRepository ?? throw new ArgumentNullException(nameof(dictTypeRepository));
            _dictDataRepository = dictDataRepository ?? throw new ArgumentNullException(nameof(dictDataRepository));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtDictType, bool>> KpDictTypeQueryExpression(HbtDictTypeQueryDto query)
        {
            return Expressionable.Create<HbtDictType>()
                .AndIF(!string.IsNullOrEmpty(query.DictName), x => x.DictName!.Contains(query.DictName!))
                .AndIF(!string.IsNullOrEmpty(query.DictType), x => x.DictType!.Contains(query.DictType!))
                .AndIF(query.Status != -1, x => x.Status == query.Status)
                .AndIF(query.IsBuiltin != -1, x => x.IsBuiltin == query.IsBuiltin)
                .AndIF(query.DictCategory != -1, x => x.DictCategory == query.DictCategory)
                .ToExpression();
        }

        /// <summary>
        /// 获取字典类型分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典类型分页列表</returns>
        public async Task<HbtPagedResult<HbtDictTypeDto>> GetListAsync(HbtDictTypeQueryDto? query)
        {
            query ??= new HbtDictTypeQueryDto();

            var result = await _dictTypeRepository.GetPagedListAsync(
                KpDictTypeQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtDictTypeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtDictTypeDto>>()
            };
        }

        /// <summary>
        /// 获取字典类型详情
        /// </summary>
        /// <param name="id">字典类型ID</param>
        /// <returns>字典类型详情</returns>
        public async Task<HbtDictTypeDto> GetByIdAsync(long id)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(id);
            return dictType == null ? throw new HbtException(L("Core.DictType.NotFound", id)) : dictType.Adapt<HbtDictTypeDto>();
        }

        /// <summary>
        /// 根据字典类型获取详情
        /// </summary>
        /// <param name="type">字典类型</param>
        /// <returns>字典类型详情</returns>
        public async Task<HbtDictTypeDto> GetByTypeAsync(string type)
        {
            var dictType = await _dictTypeRepository.GetFirstAsync(x => x.DictType == type);
            return dictType == null ? throw new HbtException(L("Core.DictType.NotFound", type)) : dictType.Adapt<HbtDictTypeDto>();
        }

        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典类型ID</returns>
        public async Task<long> CreateAsync(HbtDictTypeCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictName", input.DictName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictType", input.DictType);

            var dictType = input.Adapt<HbtDictType>();
            return await _dictTypeRepository.CreateAsync(dictType) > 0 ? dictType.Id : throw new HbtException(L("Core.DictType.CreateFailed"));
        }

        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtDictTypeUpdateDto input)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(input.DictTypeId) 
                ?? throw new HbtException(L("Core.DictType.NotFound", input.DictTypeId));

            // 验证字段是否已存在
            if (dictType.DictName != input.DictName)
                await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictName", input.DictName, input.DictTypeId);
            if (dictType.DictType != input.DictType)
                await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictType", input.DictType, input.DictTypeId);

            input.Adapt(dictType);
            return await _dictTypeRepository.UpdateAsync(dictType) > 0;
        }

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long dictTypeId)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(dictTypeId) 
                ?? throw new HbtException(L("Core.DictType.NotFound", dictTypeId));

            if (dictType.IsBuiltin == 1)
                throw new HbtException(L("Core.DictType.CannotDeleteBuiltin"));

            return await _dictTypeRepository.DeleteAsync(dictTypeId) > 0;
        }

        /// <summary>
        /// 批量删除字典类型
        /// </summary>
        /// <param name="dictTypeIds">字典类型ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] dictTypeIds)
        {
            if (dictTypeIds == null || dictTypeIds.Length == 0) return false;
            return await _dictTypeRepository.DeleteRangeAsync(dictTypeIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入字典类型数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtDictType")
        {
            var dictTypes = await HbtExcelHelper.ImportAsync<HbtDictTypeImportDto>(fileStream, sheetName);
            if (dictTypes == null || !dictTypes.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var dictType in dictTypes)
            {
                try
                {
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictName", dictType.DictName);
                    await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictType", dictType.DictType);

                    await _dictTypeRepository.CreateAsync(dictType.Adapt<HbtDictType>());
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Core.DictType.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出字典类型数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtDictTypeQueryDto query, string sheetName)
        {
            var list = await _dictTypeRepository.GetListAsync(KpDictTypeQueryExpression(query));
            var exportList = list.Adapt<List<HbtDictTypeExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, L("Core.DictType.ExportTitle"));
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtDictTypeTemplateDto>(sheetName);
        }

        /// <summary>
        /// 执行字典SQL脚本
        /// </summary>
        /// <param name="sqlScript">SQL脚本</param>
        /// <returns>字典数据列表</returns>
        public async Task<List<HbtDictDataDto>> ExecuteDictSqlAsync(string sqlScript)
        {
            var result = await _dictDataRepository.GetListAsync(x => true);
            return await Task.FromResult(result.Adapt<List<HbtDictDataDto>>());
        }

        /// <summary>
        /// 更新字典类型状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtDictTypeStatusDto input)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(input.DictTypeId) 
                ?? throw new HbtException(L("Core.DictType.NotFound", input.DictTypeId));

            dictType.Status = input.Status;
            return await _dictTypeRepository.UpdateAsync(dictType) > 0;
        }
    }
}
