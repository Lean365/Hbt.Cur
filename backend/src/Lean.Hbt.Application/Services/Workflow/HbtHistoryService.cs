//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtHistoryService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流历史服务实现类 - 使用仓储工厂模式
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
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流历史服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtHistoryService : HbtBaseService, IHbtHistoryService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtHistory> HistoryRepository => _repositoryFactory.GetWorkflowRepository<HbtHistory>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtHistoryService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取工作流历史分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        public async Task<HbtPagedResult<HbtHistoryDto>> GetListAsync(HbtHistoryQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await HistoryRepository.GetPagedListAsync(exp, query?.PageIndex ?? 1, query?.PageSize ?? 10, x => x.Id, OrderByType.Asc);

            return new HbtPagedResult<HbtHistoryDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<HbtHistoryDto>>()
            };
        }

        /// <summary>
        /// 获取工作流历史详情
        /// </summary>
        /// <param name="id">工作流历史ID</param>
        /// <returns>工作流历史详情</returns>
        public async Task<HbtHistoryDto> GetByIdAsync(long id)
        {
            var history = await HistoryRepository.GetByIdAsync(id);
            if (history == null)
                throw new HbtException(L("WorkflowHistory.NotFound"));

            return history.Adapt<HbtHistoryDto>();
        }

        /// <summary>
        /// 创建工作流历史
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>新创建的工作流历史ID</returns>
        public async Task<long> CreateAsync(HbtHistoryCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var history = new HbtHistory
            {
                InstanceId = input.InstanceId,
                NodeId = input.NodeId,
                OperationType = (int)input.OperationType,
                OperationResult = input.OperationResult != null ? (int?)Enum.Parse<int>(input.OperationResult) : null,
                OperationComment = input.OperationComment ?? string.Empty,
                Remark = input.Remark ?? string.Empty,
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            };

            var result = await HistoryRepository.CreateAsync(history);
            if (result <= 0)
                throw new HbtException(L("WorkflowHistory.Create.Failed"));

            _logger.Info(L("WorkflowHistory.Created.Success", history.Id));
            return history.Id;
        }

        /// <summary>
        /// 更新工作流历史
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(HbtHistoryUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var history = await HistoryRepository.GetByIdAsync(input.HistoryId);
            if (history == null)
                throw new HbtException(L("WorkflowHistory.NotFound"));

            history.OperationResult = input.OperationResult != null ? (int?)Enum.Parse<int>(input.OperationResult) : null;
            history.OperationComment = input.OperationComment ?? string.Empty;
            history.Remark = input.Remark ?? string.Empty;

            var result = await HistoryRepository.UpdateAsync(history);
            if (result <= 0)
                throw new HbtException(L("WorkflowHistory.Update.Failed"));

            _logger.Info(L("WorkflowHistory.Updated.Success", history.Id));
            return true;
        }

        /// <summary>
        /// 删除工作流历史
        /// </summary>
        /// <param name="id">工作流历史ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var history = await HistoryRepository.GetByIdAsync(id);
            if (history == null)
                throw new HbtException(L("WorkflowHistory.NotFound"));

            var result = await HistoryRepository.DeleteAsync(history);
            if (result <= 0)
                throw new HbtException(L("WorkflowHistory.Delete.Failed"));

            _logger.Info(L("WorkflowHistory.Deleted.Success", id));
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

            Expression<Func<HbtHistory, bool>> predicate = x => ids.Contains(x.Id);
            var result = await HistoryRepository.DeleteAsync(predicate);
            if (result <= 0)
                throw new HbtException(L("WorkflowHistory.BatchDelete.Failed"));

            _logger.Info(L("WorkflowHistory.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流历史
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importHistories = await HbtExcelHelper.ImportAsync<HbtHistoryImportDto>(fileStream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importHistories)
                {
                    try
                    {
                        var history = item.Adapt<HbtHistory>();
                        history.CreateTime = DateTime.Now;
                        history.CreateBy = _currentUser.UserName;

                        var result = await HistoryRepository.CreateAsync(history);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入工作流历史失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入工作流历史数据失败", ex);
                throw new HbtException("导入工作流历史数据失败");
            }
        }

        /// <summary>
        /// 导出工作流历史数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtHistoryQueryDto query, string sheetName = "Sheet1")
        {
            var exp = QueryExpression(query);
            var histories = await HistoryRepository.GetListAsync(exp);
            var exportList = histories.Adapt<List<HbtHistoryExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流历史数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtHistoryImportDto>(sheetName);
        }

        /// <summary>
        /// 根据工作流实例获取历史记录
        /// </summary>
        /// <param name="InstanceId">工作流实例ID</param>
        /// <returns>历史记录列表</returns>
        public async Task<List<HbtHistoryDto>> GetHistoriesByWorkflowInstanceAsync(long InstanceId)
        {
            var exp = Expressionable.Create<HbtHistory>();
            exp = exp.And(x => x.InstanceId == InstanceId);

            var histories = await HistoryRepository.GetListAsync(exp.ToExpression());
            return histories.Adapt<List<HbtHistoryDto>>();
        }

        /// <summary>
        /// 根据工作流节点获取历史记录
        /// </summary>
        /// <param name="NodeId">工作流节点ID</param>
        /// <returns>历史记录列表</returns>
        public async Task<List<HbtHistoryDto>> GetHistoriesByWorkflowNodeAsync(long NodeId)
        {
            var exp = Expressionable.Create<HbtHistory>();
            exp = exp.And(x => x.NodeId == NodeId);

            var histories = await HistoryRepository.GetListAsync(exp.ToExpression());
            return histories.Adapt<List<HbtHistoryDto>>();
        }

        /// <summary>
        /// 根据操作人获取历史记录
        /// </summary>
        /// <param name="CreateBy">操作人ID</param>
        /// <returns>历史记录列表</returns>
        public async Task<List<HbtHistoryDto>> GetHistoriesByOperatorAsync(long CreateBy)
        {
            var exp = Expressionable.Create<HbtHistory>();
            exp = exp.And(x => x.CreateBy == CreateBy.ToString());

            var histories = await HistoryRepository.GetListAsync(exp.ToExpression());
            return histories.Adapt<List<HbtHistoryDto>>();
        }

        /// <summary>
        /// 清理历史记录
        /// </summary>
        /// <param name="days">保留天数</param>
        /// <returns>清理的记录数</returns>
        public async Task<int> CleanupHistoriesAsync(int days)
        {
            var cutoffDate = DateTime.Now.AddDays(-days);
            var exp = Expressionable.Create<HbtHistory>();
            exp = exp.And(x => x.CreateTime < cutoffDate);

            var result = await HistoryRepository.DeleteAsync(exp.ToExpression());
            _logger.Info(L("WorkflowHistory.Cleanup.Success", result, days));
            return result;
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<HbtHistory, bool>> QueryExpression(HbtHistoryQueryDto query)
        {
            var exp = Expressionable.Create<HbtHistory>();

            if (query?.InstanceId.HasValue == true)
                exp = exp.And(x => x.InstanceId == query.InstanceId.Value);

            if (query?.NodeId.HasValue == true)
                exp = exp.And(x => x.NodeId == query.NodeId.Value);

            if (query?.OperationType.HasValue == true)
                exp = exp.And(x => x.OperationType == (int)query.OperationType.Value);

            if (!string.IsNullOrEmpty(query?.CreateBy))
                exp = exp.And(x => x.CreateBy == query.CreateBy);

            if (query?.StartTime.HasValue == true)
                exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp = exp.And(x => x.CreateTime <= query.EndTime.Value);

            return exp.ToExpression();
        }
    }
}