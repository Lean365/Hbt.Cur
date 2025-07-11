//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtContractService.cs
// 创建者 : Lean365
// 创建时间: 2024-03-07 16:30
// 版本号 : V1.0.0
// 描述   : 合同服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Routine.Contract;
using Lean.Hbt.Application.Dtos.Routine.Contract;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Common.Utils;
using Lean.Hbt.Domain.Utils;
using Lean.Hbt.Common.Constants;

namespace Lean.Hbt.Application.Services.Routine.Contract
{
    /// <summary>
    /// 合同服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class HbtContractService : HbtBaseService, IHbtContractService
    {
        private readonly IHbtRepository<HbtContract> _contractRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="contractRepository">合同仓储</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtContractService(
            IHbtLogger logger,
            IHbtRepository<HbtContract> contractRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _contractRepository = contractRepository;
        }

        /// <summary>
        /// 获取合同分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<HbtPagedResult<HbtContractDto>> GetListAsync(HbtContractQueryDto query)
        {
            _logger.Info("开始查询合同列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Info("生成的查询表达式：{@Predicate}", predicate);

            var result = await _contractRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                _logger.Info("第一条数据：{@FirstRow}", result.Rows.First());
            }

            var dtoResult = new HbtPagedResult<HbtContractDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtContractDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取合同详情
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <returns>返回合同详情</returns>
        public async Task<HbtContractDto> GetByIdAsync(long contractId)
        {
            var contract = await _contractRepository.GetByIdAsync(contractId);
            if (contract == null)
                throw new HbtException(L("Contract.NotFound", contractId));

            return contract.Adapt<HbtContractDto>();
        }

        /// <summary>
        /// 创建合同
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回合同ID</returns>
        public async Task<long> CreateAsync(HbtContractCreateDto input)
        {
            var contract = input.Adapt<HbtContract>();
            var result = await _contractRepository.CreateAsync(contract);
            return result;
        }

        /// <summary>
        /// 更新合同
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(HbtContractUpdateDto input)
        {
            var contract = await _contractRepository.GetByIdAsync(input.ContractId);
            if (contract == null)
                throw new HbtException(L("Contract.NotFound", input.ContractId));

            input.Adapt(contract);
            var result = await _contractRepository.UpdateAsync(contract);
            return result > 0;
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long contractId)
        {
            var contract = await _contractRepository.GetByIdAsync(contractId);
            if (contract == null)
                throw new HbtException(L("Contract.NotFound", contractId));

            var result = await _contractRepository.DeleteAsync(contract);
            return result > 0;
        }

        /// <summary>
        /// 批量删除合同
        /// </summary>
        /// <param name="contractIds">合同ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] contractIds)
        {
            if (contractIds == null || contractIds.Length == 0)
                throw new HbtException(L("Contract.SelectToDelete"));

            var contracts = await _contractRepository.GetListAsync(x => contractIds.Contains(x.Id));
            if (!contracts.Any())
                throw new HbtException(L("Contract.NotFound"));

            var result = await _contractRepository.DeleteAsync(contracts);
            return result > 0;
        }

        /// <summary>
        /// 导入合同数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "合同信息")
        {
            var importDtos = await HbtExcelHelper.ImportAsync<HbtContractImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var contract = dto.Adapt<HbtContract>();
                    await _contractRepository.CreateAsync(contract);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Contract.ImportFailed", dto.ContractName), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出合同数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtContractQueryDto query, string sheetName = "合同信息")
        {
            var predicate = QueryExpression(query);

            var contracts = await _contractRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = contracts.Adapt<List<HbtContractExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "合同信息")
        {
            var template = new List<HbtContractTemplateDto>();
            return await HbtExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtContract, bool>> QueryExpression(HbtContractQueryDto query)
        {
            var exp = Expressionable.Create<HbtContract>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.ContractCode))
                    exp = exp.And(x => x.ContractCode.Contains(query.ContractCode));

                if (!string.IsNullOrEmpty(query.ContractName))
                    exp = exp.And(x => x.ContractName.Contains(query.ContractName));

                if (query.ContractType.HasValue)
                    exp = exp.And(x => x.ContractType == query.ContractType.Value);

                if (!string.IsNullOrEmpty(query.ContractCategory))
                    exp = exp.And(x => x.ContractCategory.Contains(query.ContractCategory));

                if (!string.IsNullOrEmpty(query.ContractParty))
                    exp = exp.And(x => x.ContractParty.Contains(query.ContractParty));

                if (query.ContractStatus.HasValue)
                    exp = exp.And(x => x.ContractStatus == query.ContractStatus.Value);

                if (query.StartTime.HasValue)
                    exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

                if (query.EndTime.HasValue)
                    exp = exp.And(x => x.CreateTime <= query.EndTime.Value);
            }

            return exp.ToExpression();
        }
    }
} 