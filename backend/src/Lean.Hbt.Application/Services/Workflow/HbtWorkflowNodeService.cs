//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtWorkflowNodeService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流节点服务实现类
//===================================================================

#nullable enable

using Lean.Hbt.Application.Dtos.Workflow;
using Lean.Hbt.Common.Exceptions;
using Lean.Hbt.Common.Helpers;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.IServices.Admin;
using Lean.Hbt.Domain.Repositories;
using SqlSugar;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流节点服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供工作流节点的增删改查服务
    /// 2. 支持节点导入导出功能
    /// 3. 实现节点之间的父子关系管理
    /// 4. 提供节点排序功能
    /// </remarks>
    public class HbtWorkflowNodeService : IHbtWorkflowNodeService
    {
        private readonly IHbtRepository<HbtWorkflowNode> _nodeRepository;
        private readonly IHbtLogger _logger;
        private readonly IHbtLocalizationService _localization;

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="nodeRepository">节点仓储接口</param>
        /// <param name="logger">日志服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtWorkflowNodeService(
            IHbtRepository<HbtWorkflowNode> nodeRepository,
            IHbtLogger logger,
            IHbtLocalizationService localization)
        {
            _nodeRepository = nodeRepository;
            _logger = logger;
            _localization = localization;
        }

        /// <summary>
        /// 获取工作流节点分页列表
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// 1. NodeName - 节点名称（模糊查询）
        /// 2. NodeType - 节点类型
        /// 3. WorkflowDefinitionId - 工作流定义ID
        /// 4. PageIndex - 页码
        /// 5. PageSize - 每页记录数</param>
        /// <returns>返回分页后的节点列表</returns>
        public async Task<HbtPagedResult<HbtWorkflowNodeDto>> GetPagedListAsync(HbtWorkflowNodeQueryDto query)
        {
            var exp = Expressionable.Create<HbtWorkflowNode>();

            if (!string.IsNullOrEmpty(query?.NodeName))
                exp = exp.And(x => x.NodeName.Contains(query.NodeName));

            if (query?.NodeType.HasValue == true)
                exp = exp.And(x => x.NodeType == query.NodeType.Value);

            if (query?.WorkflowDefinitionId.HasValue == true)
                exp = exp.And(x => x.WorkflowDefinitionId == query.WorkflowDefinitionId.Value);

            var result = await _nodeRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10);

            return new HbtPagedResult<HbtWorkflowNodeDto>
            {
                TotalNum = result.total,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.list.Adapt<List<HbtWorkflowNodeDto>>()
            };
        }

        /// <summary>
        /// 根据ID获取工作流节点详情
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <returns>节点详情DTO</returns>
        /// <exception cref="HbtException">当节点不存在时抛出异常</exception>
        public async Task<HbtWorkflowNodeDto> GetAsync(long id)
        {
            var node = await _nodeRepository.GetByIdAsync(id);
            if (node == null)
                throw new HbtException(_localization.L("WorkflowNode.NotFound"));

            return node.Adapt<HbtWorkflowNodeDto>();
        }

        /// <summary>
        /// 创建新的工作流节点
        /// </summary>
        /// <param name="input">节点创建DTO，包含节点的基本信息</param>
        /// <returns>新创建的节点ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当节点创建失败时抛出异常</exception>
        public async Task<long> InsertAsync(HbtWorkflowNodeCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var node = input.Adapt<HbtWorkflowNode>();
            var result = await _nodeRepository.InsertAsync(node);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowNode.Create.Failed"));

            _logger.Info(_localization.L("WorkflowNode.Created.Success", node.Id));
            return node.Id;
        }

        /// <summary>
        /// 更新工作流节点信息
        /// </summary>
        /// <param name="input">节点更新DTO，包含需要更新的节点信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当节点不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtWorkflowNodeUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var node = await _nodeRepository.GetByIdAsync(input.WorkflowNodeId);
            if (node == null)
                throw new HbtException(_localization.L("WorkflowNode.NotFound"));

            input.Adapt(node);
            var result = await _nodeRepository.UpdateAsync(node);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowNode.Update.Failed"));

            _logger.Info(_localization.L("WorkflowNode.Updated.Success", node.Id));
            return true;
        }

        /// <summary>
        /// 删除指定工作流节点
        /// </summary>
        /// <param name="id">要删除的节点ID</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="HbtException">当节点不存在或删除失败时抛出异常</exception>
        public async Task<bool> DeleteAsync(long id)
        {
            var node = await _nodeRepository.GetByIdAsync(id);
            if (node == null)
                throw new HbtException(_localization.L("WorkflowNode.NotFound"));

            var result = await _nodeRepository.DeleteAsync(node);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowNode.Delete.Failed"));

            _logger.Info(_localization.L("WorkflowNode.Deleted.Success", id));
            return true;
        }

        /// <summary>
        /// 批量删除工作流节点
        /// </summary>
        /// <param name="ids">要删除的节点ID数组</param>
        /// <returns>删除是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入ID数组为空时抛出异常</exception>
        /// <exception cref="HbtException">当批量删除失败时抛出异常</exception>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException(nameof(ids));

            var exp = Expressionable.Create<HbtWorkflowNode>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await _nodeRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowNode.BatchDelete.Failed"));

            _logger.Info(_localization.L("WorkflowNode.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流节点
        /// </summary>
        public async Task<List<HbtWorkflowNodeDto>> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                // 使用 HbtExcelHelper 导入数据
                var importNodes = await HbtExcelHelper.ImportAsync<HbtWorkflowNodeImportDto>(fileStream, sheetName);
                var result = new List<HbtWorkflowNodeDto>();

                foreach (var item in importNodes)
                {
                    try
                    {
                        var node = item.Adapt<HbtWorkflowNode>();
                        var insertResult = await _nodeRepository.InsertAsync(node);
                        if (insertResult > 0)
                        {
                            result.Add(node.Adapt<HbtWorkflowNodeDto>());
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(_localization.L("WorkflowNode.Import.Failed", item.NodeName), ex);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowNode.Import.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowNode.Import.Failed"), ex);
            }
        }

        /// <summary>
        /// 导出工作流节点
        /// </summary>
        public async Task<byte[]> ExportAsync(IEnumerable<HbtWorkflowNodeDto> data, string sheetName = "Sheet1")
        {
            try
            {
                return await HbtExcelHelper.ExportAsync(data, sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowNode.Export.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowNode.Export.Failed"), ex);
            }
        }

        /// <summary>
        /// 获取工作流节点导入模板
        /// </summary>
        public async Task<byte[]> GetTemplateAsync(string sheetName = "Sheet1")
        {
            try
            {
                return await HbtExcelHelper.GenerateTemplateAsync<HbtWorkflowNodeImportDto>(sheetName);
            }
            catch (Exception ex)
            {
                _logger.Error(_localization.L("WorkflowNode.Template.Failed"), ex);
                throw new HbtException(_localization.L("WorkflowNode.Template.Failed"), ex);
            }
        }

        /// <summary>
        /// 获取指定工作流定义下的所有节点
        /// </summary>
        /// <param name="workflowDefinitionId">工作流定义ID</param>
        /// <returns>节点列表</returns>
        public async Task<List<HbtWorkflowNodeDto>> GetNodesByWorkflowDefinitionAsync(long workflowDefinitionId)
        {
            var exp = Expressionable.Create<HbtWorkflowNode>();
            exp = exp.And(x => x.WorkflowDefinitionId == workflowDefinitionId);

            var list = await _nodeRepository.GetListAsync(exp.ToExpression());
            return list.Adapt<List<HbtWorkflowNodeDto>>();
        }

        /// <summary>
        /// 获取指定节点的子节点列表
        /// </summary>
        /// <param name="nodeId">父节点ID</param>
        /// <returns>子节点列表</returns>
        public async Task<List<HbtWorkflowNodeDto>> GetChildNodesAsync(long nodeId)
        {
            var exp = Expressionable.Create<HbtWorkflowNode>();
            exp = exp.And(x => x.ParentNodeId == nodeId);

            var list = await _nodeRepository.GetListAsync(exp.ToExpression());
            return list.Adapt<List<HbtWorkflowNodeDto>>();
        }

        /// <summary>
        /// 更新节点排序号
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <param name="sort">新的排序号</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="HbtException">当节点不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateSortAsync(long id, int sort)
        {
            var node = await _nodeRepository.GetByIdAsync(id);
            if (node == null)
                throw new HbtException(_localization.L("WorkflowNode.NotFound"));

            node.OrderNum = sort;
            var result = await _nodeRepository.UpdateAsync(node);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowNode.UpdateSort.Failed"));

            _logger.Info(_localization.L("WorkflowNode.UpdatedSort.Success", id));
            return true;
        }
    }
}