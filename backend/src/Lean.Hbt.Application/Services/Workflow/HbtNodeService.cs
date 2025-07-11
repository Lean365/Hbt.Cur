//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtNodeService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流节点服务实现类 - 使用仓储工厂模式
//===================================================================

#nullable enable

using Lean.Hbt.Application.Dtos.Workflow;
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
    /// 更新: 2024-12-19 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class HbtNodeService : HbtBaseService, IHbtNodeService
    {
        private readonly IHbtRepositoryFactory _repositoryFactory;

        private IHbtRepository<HbtNode> NodeRepository => _repositoryFactory.GetWorkflowRepository<HbtNode>();
        private IHbtRepository<HbtDefinition> DefinitionRepository => _repositoryFactory.GetWorkflowRepository<HbtDefinition>();
        private IHbtRepository<HbtNodeTemplate> NodeTemplateRepository => _repositoryFactory.GetWorkflowRepository<HbtNodeTemplate>();

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtNodeService(
            IHbtLogger logger,
            IHbtRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取工作流节点分页列表
        /// </summary>
        /// <param name="query">查询条件，包含：
        /// 1. NodeName - 节点名称（模糊查询）
        /// 2. NodeType - 节点类型
        /// 3. DefinitionId - 工作流定义ID
        /// 4. PageIndex - 页码
        /// 5. PageSize - 每页记录数</param>
        /// <returns>返回分页后的节点列表</returns>
        public async Task<HbtPagedResult<HbtNodeDto>> GetListAsync(HbtNodeQueryDto query)
        {
            var exp = Expressionable.Create<HbtNode>();

            // 根据实例ID查询（因为HbtNode是实例级别的节点）
            if (query?.DefinitionId.HasValue == true)
            {
                // 这里需要通过实例来查询，但HbtNode实体没有直接的DefinitionId
                // 需要通过NodeTemplate来关联查询
                var templates = await NodeTemplateRepository.GetListAsync(x => x.DefinitionId == query.DefinitionId.Value);
                var templateIds = templates.Select(t => t.Id).ToList();
                exp = exp.And(x => templateIds.Contains(x.NodeTemplateId));
            }

            var result = await NodeRepository.GetPagedListAsync(exp.ToExpression(), query?.PageIndex ?? 1, query?.PageSize ?? 10, x => x.Id, OrderByType.Asc);

            // 获取节点列表
            var nodes = result.Rows.Adapt<List<HbtNodeDto>>();

            // 获取所有相关的ID
            var nodeTemplateIds = result.Rows.Select(n => n.NodeTemplateId).Distinct().ToList();
            var parentNodeIds = result.Rows.Select(n => n.ParentNodeId).Where(id => id.HasValue).Distinct().ToList();

            // 批量查询关联数据
            var nodeTemplates = await NodeTemplateRepository.GetListAsync(x => nodeTemplateIds.Contains(x.Id));
            var parentNodes = await NodeRepository.GetListAsync(x => parentNodeIds.Contains(x.Id));
            var parentNodeTemplates = await NodeTemplateRepository.GetListAsync(x => parentNodes.Select(p => p.NodeTemplateId).Contains(x.Id));

            // 填充名称信息
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                var nodeEntity = result.Rows[i];

                // 填充节点名称（从节点模板获取）
                var nodeTemplate = nodeTemplates.FirstOrDefault(t => t.Id == nodeEntity.NodeTemplateId);
                node.NodeName = nodeTemplate?.NodeName ?? $"节点模板{nodeEntity.NodeTemplateId}";
                node.NodeType = nodeTemplate?.NodeType ?? 0;
                node.DefinitionId = nodeTemplate?.DefinitionId ?? 0;

                // 填充父节点名称
                if (nodeEntity.ParentNodeId.HasValue)
                {
                    var parentNode = parentNodes.FirstOrDefault(p => p.Id == nodeEntity.ParentNodeId.Value);
                    if (parentNode != null)
                    {
                        var parentNodeTemplate = parentNodeTemplates.FirstOrDefault(t => t.Id == parentNode.NodeTemplateId);
                        node.ParentNodeName = parentNodeTemplate?.NodeName ?? $"节点模板{parentNode.NodeTemplateId}";
                    }
                }
            }

            return new HbtPagedResult<HbtNodeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = nodes
            };
        }

        /// <summary>
        /// 根据ID获取工作流节点详情
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <returns>节点详情DTO</returns>
        /// <exception cref="HbtException">当节点不存在时抛出异常</exception>
        public async Task<HbtNodeDto> GetByIdAsync(long id)
        {
            var node = await NodeRepository.GetByIdAsync(id);
            if (node == null)
                throw new HbtException(L("WorkflowNode.NotFound"));

            var nodeDto = node.Adapt<HbtNodeDto>();

            // 填充节点模板信息
            if (node.NodeTemplateId > 0)
            {
                var nodeTemplate = await NodeTemplateRepository.GetByIdAsync(node.NodeTemplateId);
                if (nodeTemplate != null)
                {
                    nodeDto.NodeName = nodeTemplate.NodeName ?? $"节点模板{node.NodeTemplateId}";
                    nodeDto.NodeType = nodeTemplate.NodeType;
                    nodeDto.DefinitionId = nodeTemplate.DefinitionId;
                    nodeDto.NodeConfig = nodeTemplate.NodeConfig ?? string.Empty;
                    nodeDto.OrderNum = nodeTemplate.OrderNum;
                }
            }

            // 填充父节点信息
            if (node.ParentNodeId.HasValue)
            {
                var parentNode = await NodeRepository.GetByIdAsync(node.ParentNodeId.Value);
                if (parentNode != null)
                {
                    var parentTemplate = await NodeTemplateRepository.GetByIdAsync(parentNode.NodeTemplateId);
                    nodeDto.ParentNodeName = parentTemplate?.NodeName ?? $"节点模板{parentNode.NodeTemplateId}";
                }
            }

            return nodeDto;
        }

        /// <summary>
        /// 创建新的工作流节点
        /// </summary>
        /// <param name="input">节点创建DTO，包含节点的基本信息</param>
        /// <returns>新创建的节点ID</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当节点创建失败时抛出异常</exception>
        public async Task<long> CreateAsync(HbtNodeCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var node = input.Adapt<HbtNode>();
            var result = await NodeRepository.CreateAsync(node);
            if (result <= 0)
                throw new HbtException(L("WorkflowNode.Create.Failed"));

            _logger.Info(L("WorkflowNode.Created.Success", node.Id));
            return node.Id;
        }

        /// <summary>
        /// 更新工作流节点信息
        /// </summary>
        /// <param name="input">节点更新DTO，包含需要更新的节点信息</param>
        /// <returns>更新是否成功</returns>
        /// <exception cref="ArgumentNullException">当输入参数为空时抛出异常</exception>
        /// <exception cref="HbtException">当节点不存在或更新失败时抛出异常</exception>
        public async Task<bool> UpdateAsync(HbtNodeUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var node = await NodeRepository.GetByIdAsync(input.NodeId);
            if (node == null)
                throw new HbtException(_localization.L("WorkflowNode.NotFound"));

            input.Adapt(node);
            var result = await NodeRepository.UpdateAsync(node);
            if (result <= 0)
                throw new HbtException(_localization.L("WorkflowNode.Update.Failed"));

            _logger.Info(L("WorkflowNode.Updated.Success", node.Id));
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
            var node = await NodeRepository.GetByIdAsync(id);
            if (node == null)
                throw new HbtException(L("WorkflowNode.NotFound"));

            var result = await NodeRepository.DeleteAsync(node);
            if (result <= 0)
                throw new HbtException(L("WorkflowNode.Delete.Failed"));

            _logger.Info(L("WorkflowNode.Deleted.Success", id));
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

            var exp = Expressionable.Create<HbtNode>();
            exp = exp.And(x => ids.Contains(x.Id));

            var result = await NodeRepository.DeleteAsync(exp.ToExpression());
            if (result <= 0)
                throw new HbtException(L("WorkflowNode.BatchDelete.Failed"));

            _logger.Info(L("WorkflowNode.BatchDeleted.Success", string.Join(",", ids)));
            return true;
        }

        /// <summary>
        /// 导入工作流节点数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "Sheet1")
        {
            try
            {
                var importNodes = await HbtExcelHelper.ImportAsync<HbtNodeImportDto>(fileStream, sheetName);
                int success = 0, fail = 0;

                foreach (var item in importNodes)
                {
                    try
                    {
                        var node = item.Adapt<HbtNode>();
                        node.CreateTime = DateTime.Now;
                        node.CreateBy = _currentUser.UserName;

                        var result = await NodeRepository.CreateAsync(node);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入工作流节点失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入工作流节点数据失败", ex);
                throw new HbtException("导入工作流节点数据失败");
            }
        }

        /// <summary>
        /// 导出工作流节点数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtNodeQueryDto query, string sheetName = "Sheet1")
        {
            var exp = Expressionable.Create<HbtNode>();

            // 根据工作流定义ID查询
            if (query?.DefinitionId.HasValue == true)
            {
                var templates = await NodeTemplateRepository.GetListAsync(x => x.DefinitionId == query.DefinitionId.Value);
                var templateIds = templates.Select(t => t.Id).ToList();
                exp = exp.And(x => templateIds.Contains(x.NodeTemplateId));
            }

            var nodes = await NodeRepository.GetListAsync(exp.ToExpression());
            var exportList = nodes.Adapt<List<HbtNodeExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "工作流节点数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
        {
            return await HbtExcelHelper.GenerateTemplateAsync<HbtNodeImportDto>(sheetName);
        }

        /// <summary>
        /// 根据工作流定义获取节点列表
        /// </summary>
        /// <param name="DefinitionId">工作流定义ID</param>
        /// <returns>节点列表</returns>
        public async Task<List<HbtNodeDto>> GetNodesByWorkflowDefinitionAsync(long DefinitionId)
        {
            // 先获取该定义下的所有节点模板
            var nodeTemplates = await NodeTemplateRepository.GetListAsync(x => x.DefinitionId == DefinitionId);
            var templateIds = nodeTemplates.Select(t => t.Id).ToList();

            if (!templateIds.Any())
                return new List<HbtNodeDto>();

            // 根据节点模板ID查询节点
            var nodes = await NodeRepository.GetListAsync(x => templateIds.Contains(x.NodeTemplateId));
            var nodeDtos = nodes.Adapt<List<HbtNodeDto>>();

            // 填充节点名称和类型信息
            foreach (var nodeDto in nodeDtos)
            {
                var nodeTemplate = nodeTemplates.FirstOrDefault(t => t.Id == nodeDto.NodeTemplateId);
                if (nodeTemplate != null)
                {
                    nodeDto.NodeName = nodeTemplate.NodeName ?? $"节点模板{nodeTemplate.Id}";
                    nodeDto.NodeType = nodeTemplate.NodeType;
                    nodeDto.DefinitionId = nodeTemplate.DefinitionId;
                    nodeDto.NodeConfig = nodeTemplate.NodeConfig ?? string.Empty;
                    nodeDto.OrderNum = nodeTemplate.OrderNum;
                }
            }

            return nodeDtos;
        }

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="nodeId">父节点ID</param>
        /// <returns>子节点列表</returns>
        public async Task<List<HbtNodeDto>> GetChildNodesAsync(long nodeId)
        {
            var childNodes = await NodeRepository.GetListAsync(x => x.ParentNodeId == nodeId);
            var nodeDtos = childNodes.Adapt<List<HbtNodeDto>>();

            // 获取节点模板信息
            var templateIds = childNodes.Select(n => n.NodeTemplateId).Distinct().ToList();
            var nodeTemplates = await NodeTemplateRepository.GetListAsync(x => templateIds.Contains(x.Id));

            // 填充节点名称和类型信息
            foreach (var nodeDto in nodeDtos)
            {
                var nodeTemplate = nodeTemplates.FirstOrDefault(t => t.Id == nodeDto.NodeTemplateId);
                if (nodeTemplate != null)
                {
                    nodeDto.NodeName = nodeTemplate.NodeName ?? $"节点模板{nodeTemplate.Id}";
                    nodeDto.NodeType = nodeTemplate.NodeType;
                    nodeDto.DefinitionId = nodeTemplate.DefinitionId;
                    nodeDto.NodeConfig = nodeTemplate.NodeConfig ?? string.Empty;
                    nodeDto.OrderNum = nodeTemplate.OrderNum;
                }
            }

            return nodeDtos;
        }

        /// <summary>
        /// 更新节点排序
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <param name="sort">新的排序值</param>
        /// <returns>更新是否成功</returns>
        public async Task<bool> UpdateSortAsync(long id, int sort)
        {
            var node = await NodeRepository.GetByIdAsync(id);
            if (node == null)
                throw new HbtException(L("WorkflowNode.NotFound"));

            node.OrderNum = sort;
            node.UpdateTime = DateTime.Now;
            node.UpdateBy = _currentUser.UserName;

            var result = await NodeRepository.UpdateAsync(node);
            if (result <= 0)
                throw new HbtException(L("WorkflowNode.UpdateSort.Failed"));

            _logger.Info(L("WorkflowNode.SortUpdated.Success", id, sort));
            return true;
        }
    }
}