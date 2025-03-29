#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictTypeService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典类型服务实现
//===================================================================

using System.Data;
using Lean.Hbt.Application.Dtos.Admin;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Domain.Data;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using SqlSugar;

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
        /// <param name="query">查询条件</param>
        /// <returns>字典类型分页列表</returns>
        public async Task<HbtPagedResult<HbtDictTypeDto>> GetListAsync(HbtDictTypeQueryDto query)
        {
            var exp = Expressionable.Create<HbtDictType>();

            // 构建查询条件
            if (!string.IsNullOrEmpty(query.DictName))
                exp.And(x => x.DictName.Contains(query.DictName));
            if (!string.IsNullOrEmpty(query.DictType))
                exp.And(x => x.DictType.Contains(query.DictType));
            if (query.DictCategory.HasValue)
                exp.And(x => x.DictCategory == query.DictCategory.Value);
            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            // 执行分页查询
            var result = await _dictTypeRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            // 返回分页结果
            return new HbtPagedResult<HbtDictTypeDto>
            {
                Rows = result.Rows.Adapt<List<HbtDictTypeDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
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
            if (dictType == null)
                throw new HbtException($"字典类型[{id}]不存在");

            return dictType.Adapt<HbtDictTypeDto>();
        }

        /// <summary>
        /// 根据字典类型获取详情
        /// </summary>
        /// <param name="type">字典类型</param>
        /// <returns>字典类型详情</returns>
        public async Task<HbtDictTypeDto> GetByTypeAsync(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new HbtException("字典类型不能为空");

            var dictType = await _dictTypeRepository.GetInfoAsync(x => x.DictType == type);
            if (dictType == null)
                throw new HbtException($"字典类型[{type}]不存在");

            return dictType.Adapt<HbtDictTypeDto>();
        }

        /// <summary>
        /// 创建字典类型
        /// </summary>
        public async Task<long> CreateAsync(HbtDictTypeCreateDto input)
        {
            await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictName", input.DictName);
            await HbtValidateUtils.ValidateFieldExistsAsync(_dictTypeRepository, "DictType", input.DictType);

            var dictType = input.Adapt<HbtDictType>();
            var result = await _dictTypeRepository.CreateAsync(dictType);
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
        /// <param name="id">字典类型ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var dictType = await _dictTypeRepository.GetByIdAsync(id);
            if (dictType == null)
            {
                throw new HbtException($"字典类型[{id}]不存在");
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
                    await _dictTypeRepository.CreateAsync(dictType);
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
                Status = 0
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

        /// <summary>
        /// 执行字典SQL脚本
        /// </summary>
        /// <param name="sqlScript">SQL脚本</param>
        /// <returns>字典数据列表</returns>
        public async Task<List<HbtDictDataDto>> ExecuteDictSqlAsync(string sqlScript)
        {
            try
            {
                // 检查SQL脚本安全性
                if (!IsSafeSqlScript(sqlScript))
                {
                    throw new HbtException("SQL脚本包含不安全的操作");
                }

                // 使用SqlSugar执行SQL查询
                var result = await _dbContext.Client.Ado.SqlQueryAsync<dynamic>(sqlScript);
                if (result == null)
                {
                    return new List<HbtDictDataDto>();
                }

                // 转换为标准字典数据格式
                var dtoList = new List<HbtDictDataDto>();
                foreach (var row in result)
                {
                    dtoList.Add(ConvertToDto(row));
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                _logger.Error($"执行SQL脚本失败：{ex.Message}", ex);
                throw new HbtException($"执行SQL脚本失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 检查SQL脚本安全性
        /// </summary>
        /// <param name="sqlScript">SQL脚本</param>
        /// <returns>是否安全</returns>
        private bool IsSafeSqlScript(string sqlScript)
        {
            if (string.IsNullOrEmpty(sqlScript))
                return false;

            // 转换为大写进行检查
            var upperSql = sqlScript.ToUpper();

            // 禁止的关键字列表
            var forbiddenKeywords = new[]
            {
                "DROP ", "DELETE ", "TRUNCATE ", "UPDATE ", "INSERT ", "ALTER ", "CREATE ", "EXEC ",
                "SP_", "XP_", "SYSTEM_USER", "IS_SRVROLEMEMBER"
            };

            // 检查是否包含禁止的关键字
            return !forbiddenKeywords.Any(keyword => upperSql.Contains(keyword));
        }

        /// <summary>
        /// 转换SQL查询结果为字典数据DTO
        /// </summary>
        private HbtDictDataDto ConvertToDto(dynamic row)
        {
            return new HbtDictDataDto
            {
                Label = row.label,
                Value = row.value,
                Status = row.status,
                OrderNum = row.order_num,
                CssClass = row.css_class,
                ListClass = row.list_class,
                ExtLabel = row.ext_label,
                ExtValue = row.ext_value,
                TransKey = row.trans_key,
                Remark = row.remark
            };
        }
    }
}