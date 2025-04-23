//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowHistoryService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流历史服务实现类
//===================================================================

#nullable enable

using System.Linq.Expressions;
using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.IServices.Extensions;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流历史服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowHistoryService : IHbtWorkflowHistoryService
    {
        private readonly IHbtRepository<HbtWorkflowHistory> _historyRepository;
        private readonly IHbtLogger _logger;
        private readonly IHbtLocalizationService _localization;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowHistoryService(
            IHbtRepository<HbtWorkflowHistory> historyRepository,
            IHbtLogger logger,
            IHbtLocalizationService localization)
        {
            _historyRepository = historyRepository;
            _logger = logger;
            _localization = localization;
        }

        /// <summary>
        /// 获取工作流历史分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        public async Task<HbtPagedResult<HbtWorkflowHistoryDto>> GetListAsync(HbtWorkflowHistoryQueryDto query)
        {
            var exp = Expressionable.Create<HbtWorkflowHistory>();

            if (query?.WorkflowInstanceId.HasValue == true)
                exp.And(x => x.WorkflowInstanceId == query.WorkflowInstanceId.Value);

            if (query?.WorkflowNodeId.HasValue == true)
                exp.And(x => x.NodeId == query.WorkflowNodeId.Value);

            if (query?.OperatorId.HasValue == true)
                exp.And(x => x.OperatorId == query.OperatorId.Value);

            var result = await _historyRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10, x => x.Id, OrderByType.Asc);

            return new HbtPagedResult<HbtWorkflowHistoryDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtWorkflowHistoryDto>>()
            };
        }

        /// <summary>
        /// 获取工作流历史详情
        /// </summary>
        /// <param name="id">工作流历史ID</param>
        /// <returns>工作流历史详情</returns>
        public async Task<HbtWorkflowHistoryDto> GetByIdAsync(long id)
        {
            var history = await _historyRepository.GetByIdAsync(id);
            if (history == null)
                throw new HbtException(_localization.L("WorkflowHistory.NotFound"));

            return history.Adapt<HbtWorkflowHistoryDto>();
        }

        /// <summary>
        /// 创建工作流历史
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流历史ID</returns>
        public async Task<long> CreateAsync(HbtWorkflowHistoryCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var history = new HbtWorkflowHistory
            {
                WorkflowInstanceId = input.WorkflowInstanceId,
                NodeId = input.WorkflowNodeId,
                OperatorId = input.OperatorId,
                OperationType = (int)input.OperationType,
                OperationTime = DateTime.Now,
                OperationResult = input.OperationResult != null ? (int?)Enum.Parse<int>(input.OperationResult) : null,
                OperationComment = input.OperationComment ?? string.Empty,
                Remark = input.Remark ?? string.Empty
            };

            var result = await _historyRepository.CreateAsync(history);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowHistory.Create.Failed"));

            _logger.Info(_localization.L("WorkflowHistory.Created.Success", history.Id));
            return history.Id;
        }

        /// <summary>
        /// 更新工作流历史
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtWorkflowHistoryUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var history = await _historyRepository.GetByIdAsync(input.WorkflowHistoryId);
            if (history == null)
                throw new HbtException(_localization.L("WorkflowHistory.NotFound"));

            history.OperationResult = input.OperationResult != null ? (int?)Enum.Parse<int>(input.OperationResult) : null;
            history.OperationComment = input.OperationComment ?? string.Empty;
            history.Remark = input.Remark ?? string.Empty;

            var result = await _historyRepository.UpdateAsync(history);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowHistory.Update.Failed"));

            _logger.Info(_localization.L("WorkflowHistory.Updated.Success", history.Id));
            return true;
        }

        /// <summary>
        /// 删除工作流历史
        /// </summary>
        /// <param name="id">工作流历史ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var history = await _historyRepository.GetByIdAsync(id);
            if (history == null)
                throw new HbtException(_localization.L("WorkflowHistory.NotFound"));

            var result = await _historyRepository.DeleteAsync(history);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowHistory.Delete.Failed"));

            _logger.Info(_localization.L("WorkflowHistory.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除工作流历史
        /// </summary>
        /// <param name="ids">工作流历史ID数组</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            Expression<Func<HbtWorkflowHistory, bool>> predicate = x => ids.Contains(x.Id);
            var result = await _historyRepository.DeleteAsync(predicate);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowHistory.BatchDelete.Failed"));

            _logger.Info(_localization.L("WorkflowHistory.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流历史
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<List<HbtWorkflowHistoryDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                // 使用 HbtExcelHelper 导入数据
                var importHistories = await HbtExcelHelper.ImportAsync<HbtWorkflowHistoryImportDto>(fileStream, sheetName);
                var result = new List<HbtWorkflowHistoryDto>();

                foreach (var item in importHistories)
                {
                    try
                    {
                        var history = item.Adapt<HbtWorkflowHistory>();
                        var insertResult = await _historyRepository.CreateAsync(history);
                        if (insertResult > 0)
                        {
                            result.Add(history.Adapt<HbtWorkflowHistoryDto>());
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(_localization.L("WorkflowHistory.Import.Failed"), ex);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowHistory.Import.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowHistory.Import.Failed"), ex);
            }
        }

        /// <summary>
        /// 导出工作流历史记录
        /// </summary>
        /// <param name="data">数据列表</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出数据</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(IEnumerable<HbtWorkflowHistoryDto> data, string sheetName = "Sheet1")
        {
            try
            {
                return await HbtExcelHelper.ExportAsync(data, sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowHistory.Export.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowHistory.Export.Failed"), ex);
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtWorkflowHistoryImportDto>(sheetName);
        }

        /// <summary>
        /// 获取工作流实例的历史记录
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <returns>历史记录列表</returns>
        public async Task<List<HbtWorkflowHistoryDto>> GetHistoriesByWorkflowInstanceAsync(long workflowInstanceId)
        {
            var list = await _historyRepository.GetListAsync(x => x.WorkflowInstanceId == workflowInstanceId);
            return list.Adapt<List<HbtWorkflowHistoryDto>>();
        }

        /// <summary>
        /// 获取工作流节点的历史记录
        /// </summary>
        /// <param name="workflowNodeId">工作流节点ID</param>
        /// <returns>历史记录列表</returns>
        public async Task<List<HbtWorkflowHistoryDto>> GetHistoriesByWorkflowNodeAsync(long workflowNodeId)
        {
            var list = await _historyRepository.GetListAsync(x => x.NodeId == workflowNodeId);
            return list.Adapt<List<HbtWorkflowHistoryDto>>();
        }

        /// <summary>
        /// 获取用户的操作历史记录
        /// </summary>
        /// <param name="operatorId">操作人ID</param>
        /// <returns>历史记录列表</returns>
        public async Task<List<HbtWorkflowHistoryDto>> GetHistoriesByOperatorAsync(long operatorId)
        {
            var list = await _historyRepository.GetListAsync(x => x.OperatorId == operatorId);
            return list.Adapt<List<HbtWorkflowHistoryDto>>();
        }

        /// <summary>
        /// 清理历史记录
        /// </summary>
        /// <param name="days">保留天数</param>
        /// <returns>清理数量</returns>
        public async Task<int> CleanupHistoriesAsync(int days)
        {
            if (days <= 0)
                throw new ArgumentException("Days must be greater than 0", nameof(days));

            var cutoffDate = DateTime.Now.AddDays(-days);
            Expression<Func<HbtWorkflowHistory, bool>> predicate = x => x.OperationTime < cutoffDate;
            var result = await _historyRepository.DeleteAsync(predicate);

            _logger.Info(_localization.L("WorkflowHistory.Cleanup.Success", result));
            return result;
        }
    }
}