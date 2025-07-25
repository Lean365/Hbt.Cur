#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictDataService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 字典数据服务实现类
//===================================================================

using System.Linq.Expressions;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.Entities.Routine.Core;
using Lean.Hbt.Application.Dtos.Routine.Core;

namespace Lean.Hbt.Application.Services.Core
{
    /// <summary>
    /// 字典数据服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class HbtDictDataService : HbtBaseService, IHbtDictDataService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly IHbtRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取字典数据仓储
        /// </summary>
        private IHbtRepository<HbtDictData> DictDataRepository => _repositoryFactory.GetBusinessRepository<HbtDictData>();

        /// <summary>
        /// 获取字典类型仓储
        /// </summary>
        private IHbtRepository<HbtDictType> DictTypeRepository => _repositoryFactory.GetBusinessRepository<HbtDictType>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtDictDataService(
            IHbtRepositoryFactory repositoryFactory,
            IHbtLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtDictData, bool>> KpDictDataQueryExpression(HbtDictDataQueryDto query)
        {
            return Expressionable.Create<HbtDictData>()
                .AndIF(!string.IsNullOrEmpty(query.DictType), x => x.DictType.Contains(query.DictType!))
                .AndIF(!string.IsNullOrEmpty(query.DictLabel), x => x.DictLabel.Contains(query.DictLabel!))
                .AndIF(!string.IsNullOrEmpty(query.DictValue), x => x.DictValue.Contains(query.DictValue!))
                .AndIF(query.Status != -1, x => x.Status == query.Status)
                .ToExpression();
        }

        /// <summary>
        /// 获取字典数据分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典数据分页列表</returns>
        public async Task<HbtPagedResult<HbtDictDataDto>> GetListAsync(HbtDictDataQueryDto query)
        {
            query ??= new HbtDictDataQueryDto();

            var result = await DictDataRepository.GetPagedListAsync(
                KpDictDataQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new HbtPagedResult<HbtDictDataDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtDictDataDto>>()
            };
        }

        /// <summary>
        /// 获取字典数据详情
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>字典数据详情</returns>
        public async Task<HbtDictDataDto> GetByIdAsync(long dictDataId)
        {
            var dictData = await DictDataRepository.GetByIdAsync(dictDataId);
            return dictData == null ? throw new HbtException(L("Core.DictData.NotFound", dictDataId)) : dictData.Adapt<HbtDictDataDto>();
        }

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典数据ID</returns>
        public async Task<long> CreateAsync(HbtDictDataCreateDto input)
        {
            // 验证字段是否已存在
            await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictType", input.DictType);
            await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictLabel", input.DictLabel);
            await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictValue", input.DictValue);

            var dictData = input.Adapt<HbtDictData>();
            return await DictDataRepository.CreateAsync(dictData) > 0 ? dictData.Id : throw new HbtException(L("Core.DictData.CreateFailed"));
        }

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtDictDataUpdateDto input)
        {
            var dictData = await DictDataRepository.GetByIdAsync(input.DictDataId)
                ?? throw new HbtException(L("Core.DictData.NotFound", input.DictDataId));

            // 验证字段是否已存在
            if (dictData.DictType != input.DictType)
                await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictType", input.DictType, input.DictDataId);
            if (dictData.DictLabel != input.DictLabel)
                await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictLabel", input.DictLabel, input.DictDataId);
            if (dictData.DictValue != input.DictValue)
                await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictValue", input.DictValue, input.DictDataId);

            input.Adapt(dictData);
            return await DictDataRepository.UpdateAsync(dictData) > 0;
        }

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long dictDataId)
        {
            var dictData = await DictDataRepository.GetByIdAsync(dictDataId)
                ?? throw new HbtException(L("Core.DictData.NotFound", dictDataId));

            if (await CheckBuiltinData(dictDataId))
                throw new HbtException(L("Core.DictData.CannotDeleteBuiltin"));

            return await DictDataRepository.DeleteAsync(dictDataId) > 0;
        }

        /// <summary>
        /// 批量删除字典数据
        /// </summary>
        /// <param name="dictDataIds">字典数据ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] dictDataIds)
        {
            if (dictDataIds == null || dictDataIds.Length == 0) return false;
            return await DictDataRepository.DeleteRangeAsync(dictDataIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入字典数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtDictData")
        {
            var dictDatas = await HbtExcelHelper.ImportAsync<HbtDictDataDto>(fileStream, sheetName);
            if (dictDatas == null || !dictDatas.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var dictData in dictDatas)
            {
                try
                {
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictType", dictData.DictType);
                    await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictLabel", dictData.DictLabel);
                    await HbtValidateUtils.ValidateFieldExistsAsync(DictDataRepository, "DictValue", dictData.DictValue);

                    await DictDataRepository.CreateAsync(dictData.Adapt<HbtDictData>());
                    success++;
                }
                catch
                {
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtDictDataQueryDto query, string sheetName = "DictData")
        {
            try
            {
                var list = await DictDataRepository.GetListAsync(KpDictDataQueryExpression(query));
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtDictDataDto>>(), sheetName, L("Core.DictData.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Core.DictData.ExportFailed", ex.Message), ex);
                throw new HbtException(L("Core.DictData.ExportFailed"));
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtDictDataDto>(sheetName);
        }

        /// <summary>
        /// 更新字典数据状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtDictDataStatusDto input)
        {
            var dictData = await DictDataRepository.GetByIdAsync(input.DictDataId)
                ?? throw new HbtException(L("Core.DictData.NotFound", input.DictDataId));

            dictData.Status = input.Status;
            return await DictDataRepository.UpdateAsync(dictData) > 0;
        }

        /// <summary>
        /// 获取字典数据选项列表
        /// </summary>
        /// <param name="dictType">字典类型编码</param>
        /// <returns>字典数据选项列表</returns>
        public async Task<List<HbtDictDataDto>> GetOptionsAsync(string dictType)
        {
            var result = await DictDataRepository.GetListAsync(x => x.DictType == dictType && x.Status == 0);
            return result.OrderBy(x => x.OrderNum).Adapt<List<HbtDictDataDto>>();
        }

        /// <summary>
        /// 检查字典数据是否存在
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <param name="dictValue">字典值</param>
        /// <param name="excludeId">排除的字典数据ID</param>
        /// <returns>是否存在</returns>
        public async Task<bool> CheckDictDataExists(string dictType, string dictValue, long? excludeId = null)
        {
            var result = await DictDataRepository.GetListAsync(x => x.DictType == dictType && x.Status == 0);
            return result.Any(x => x.DictValue == dictValue && x.Id != (excludeId ?? 0));
        }

        /// <summary>
        /// 检查是否为内置字典数据
        /// </summary>
        /// <param name="id">字典数据ID</param>
        /// <returns>是否为内置数据</returns>
        public async Task<bool> CheckBuiltinData(long id)
        {
            var dictData = await DictDataRepository.GetByIdAsync(id);
            if (dictData == null) return false;

            var dictType = await DictTypeRepository.GetFirstAsync(x => x.DictType == dictData.DictType);
            return dictType != null && dictType.IsBuiltin == 1; // 1表示内置
        }

        /// <summary>
        /// 根据字典类型获取字典数据列表
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns>字典数据列表</returns>
        public async Task<List<HbtDictDataDto>> GetListByDictTypeAsync(string dictType)
        {
            var result = await DictDataRepository.GetListAsync(x => x.DictType == dictType && x.Status == 0);
            return result.OrderBy(x => x.OrderNum).Adapt<List<HbtDictDataDto>>();
        }

        /// <summary>
        /// 获取字典数据列表
        /// </summary>
        /// <returns>字典数据列表</returns>
        public async Task<List<HbtDictDataDto>> GetListAsync()
        {
            var result = await DictDataRepository.GetListAsync(x => x.Status == 0);
            return result.OrderBy(x => x.OrderNum).Adapt<List<HbtDictDataDto>>();
        }
    }
}