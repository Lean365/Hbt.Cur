//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtInstanceService.cs
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述   : 工作流实例服务实现类
//===================================================================

#nullable enable

using Microsoft.AspNetCore.Http;
using System.IO;
using Lean.Hbt.Common.Enums;
using System.Linq.Expressions;

namespace Lean.Hbt.Application.Services.Workflow
{
    /// <summary>
    /// 工作流实例服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 提供工作流实例的增删改查服务
    /// 2. 支持工作流实例的导入导出功能
    /// 3. 实现工作流实例的状态管理
    /// 4. 提供工作流实例的提交、撤回、终止等操作
    /// </remarks>
    public class HbtInstanceService : HbtBaseService, IHbtInstanceService
    {
        private readonly IHbtRepository<HbtInstance> _instanceRepository;

        /// <summary>
        /// 构造函数，注入所需依赖
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="instanceRepository">工作流实例仓储接口</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="currentTenant">当前租户服务</param>
        /// <param name="localization">本地化服务</param>
        public HbtInstanceService(
            IHbtLogger logger,
            IHbtRepository<HbtInstance> instanceRepository,
            IHttpContextAccessor httpContextAccessor,
            IHbtCurrentUser currentUser,
            IHbtCurrentTenant currentTenant,
            IHbtLocalizationService localization) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
        {
            _instanceRepository = instanceRepository ?? throw new ArgumentNullException(nameof(instanceRepository));
        }

        /// <summary>
        /// 获取工作流实例分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页后的工作流实例列表</returns>
        public async Task<HbtPagedResult<HbtInstanceDto>> GetListAsync(HbtInstanceQueryDto query)
        {
            query ??= new HbtInstanceQueryDto();

            var exp = QueryExpression(query);

            var result = await _instanceRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new HbtPagedResult<HbtInstanceDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<HbtInstanceDto>>()
            };
        }

        /// <summary>
        /// 根据ID获取工作流实例详情
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>工作流实例详情DTO</returns>
        public async Task<HbtInstanceDto> GetByIdAsync(long id)
        {
            var instance = await _instanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            return instance.Adapt<HbtInstanceDto>();
        }

        /// <summary>
        /// 创建新的工作流实例
        /// </summary>
        /// <param name="input">工作流实例创建DTO</param>
        /// <returns>新创建的工作流实例ID</returns>
        public async Task<long> CreateAsync(HbtInstanceCreateDto input)
        {
            var instance = input.Adapt<HbtInstance>();
            instance.Status = 0; // 0 表示草稿状态

            var result = await _instanceRepository.CreateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Create.Failed"));

            _logger.Info(L("WorkflowInstance.Created.Success", instance.Id));
            return instance.Id;
        }

        /// <summary>
        /// 更新工作流实例信息
        /// </summary>
        /// <param name="input">工作流实例更新DTO</param>
        /// <returns>更新是否成功</returns>
        public async Task<bool> UpdateAsync(HbtInstanceUpdateDto input)
        {
            var instance = await _instanceRepository.GetByIdAsync(input.InstanceId)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许更新
            if (instance.Status != 0) // 0 表示草稿状态
                throw new HbtException(L("WorkflowInstance.CannotUpdateNonDraft"));

            input.Adapt(instance);
            var result = await _instanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Update.Failed"));

            _logger.Info(L("WorkflowInstance.Updated.Success", instance.Id));
            return true;
        }

        /// <summary>
        /// 删除指定工作流实例
        /// </summary>
        /// <param name="id">要删除的工作流实例ID</param>
        /// <returns>删除是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var instance = await _instanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许删除
            if (instance.Status != 0 && // 0 表示草稿状态
                instance.Status != 4) // 4 表示已终止状态
                throw new HbtException(L("WorkflowInstance.CannotDeleteActive"));

            return await _instanceRepository.DeleteAsync(id) > 0;
        }

        /// <summary>
        /// 批量删除工作流实例
        /// </summary>
        /// <param name="ids">要删除的工作流实例ID数组</param>
        /// <returns>删除是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0) return false;

            // 检查是否有活动状态的实例
            var activeInstances = await _instanceRepository.GetListAsync(x =>
                ids.Contains(x.Id) &&
                x.Status != 0 && // 0 表示草稿状态
                x.Status != 4); // 4 表示已终止状态

            if (activeInstances.Any())
                throw new HbtException(L("WorkflowInstance.CannotDeleteActive"));

            return await _instanceRepository.DeleteRangeAsync(ids.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入工作流实例数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回成功和失败的记录数</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "HbtInstance")
        {
            var instances = await HbtExcelHelper.ImportAsync<HbtInstanceDto>(fileStream, sheetName);
            if (instances == null || !instances.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var instance in instances)
            {
                try
                {
                    // 验证字段是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_instanceRepository, "InstanceName", instance.InstanceName);

                    var entity = instance.Adapt<HbtInstance>();
                    entity.Status = 0; // 0 表示草稿状态
                    entity.CreateBy = _currentUser.UserName;
                    entity.CreateTime = DateTime.Now;

                    await _instanceRepository.CreateAsync(entity);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("WorkflowInstance.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出工作流实例数据
        /// </summary>
        /// <param name="query">导出查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(HbtInstanceQueryDto query, string sheetName = "Instance")
        {
            try
            {
                var exp = QueryExpression(query);

                var list = await _instanceRepository.GetListAsync(exp);
                return await HbtExcelHelper.ExportAsync(list.Adapt<List<HbtInstanceDto>>(), sheetName, L("WorkflowInstance.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("WorkflowInstance.ExportFailed", ex.Message), ex);
                throw new HbtException(L("WorkflowInstance.ExportFailed"));
            }
        }

        /// <summary>
        /// 获取工作流实例导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Instance")
        {
            var template = new HbtInstanceTemplateDto
            {
                InstanceName = "示例工作流实例",
                BusinessKey = "示例业务键",
                DefinitionId = 1,
                CurrentNodeId = 1,
                InitiatorId = 1,
                FormData = "示例表单数据",
                Remark = "请填写备注信息"
            };

            return await HbtExcelHelper.ExportAsync(new List<HbtInstanceTemplateDto> { template }, sheetName, L("WorkflowInstance.TemplateTitle"));
        }

        /// <summary>
        /// 更新工作流实例状态
        /// </summary>
        /// <param name="input">状态更新信息</param>
        /// <returns>更新是否成功</returns>
        public async Task<bool> UpdateStatusAsync(HbtInstanceStatusDto input)
        {
            var instance = await _instanceRepository.GetByIdAsync(input.InstanceId)
                ?? throw new HbtException(L("WorkflowInstance.NotFound", input.InstanceId));

            instance.Status = input.Status;
            return await _instanceRepository.UpdateAsync(instance) > 0;
        }

        /// <summary>
        /// 提交工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>提交是否成功</returns>
        public async Task<bool> SubmitAsync(long id)
        {
            var instance = await _instanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许提交
            if (instance.Status != 0) // 0 表示草稿状态
                throw new HbtException(L("WorkflowInstance.CannotSubmitNonDraft"));

            instance.Status = 1; // 1 表示运行中状态
            var result = await _instanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Submit.Failed"));

            _logger.Info(L("WorkflowInstance.Submitted.Success", id));
            return true;
        }

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <returns>撤回是否成功</returns>
        public async Task<bool> WithdrawAsync(long id)
        {
            var instance = await _instanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许撤回
            if (instance.Status != 1) // 1 表示运行中状态
                throw new HbtException(L("WorkflowInstance.CannotWithdrawNonRunning"));

            instance.Status = 3; // 3 表示已撤回状态
            var result = await _instanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Withdraw.Failed"));

            _logger.Info(L("WorkflowInstance.Withdrawn.Success", id));
            return true;
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="id">工作流实例ID</param>
        /// <param name="reason">终止原因</param>
        /// <returns>终止是否成功</returns>
        public async Task<bool> TerminateAsync(long id, string reason)
        {
            var instance = await _instanceRepository.GetByIdAsync(id)
                ?? throw new HbtException(L("WorkflowInstance.NotFound"));

            // 检查实例状态是否允许终止
            if (instance.Status != 1) // 1 表示运行中状态
                throw new HbtException(L("WorkflowInstance.CannotTerminateNonRunning"));

            instance.Status = 4; // 4 表示已终止状态
            instance.EndTime = DateTime.Now;
            instance.Remark = reason;
            var result = await _instanceRepository.UpdateAsync(instance);
            if (result <= 0)
                throw new HbtException(L("WorkflowInstance.Terminate.Failed"));

            _logger.Info(L("WorkflowInstance.Terminated.Success", id));
            return true;
        }

        #region 查询表达式

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<HbtInstance, bool>> QueryExpression(HbtInstanceQueryDto query)
        {
            return Expressionable.Create<HbtInstance>()
                .AndIF(!string.IsNullOrEmpty(query.InstanceName), x => x.InstanceName.Contains(query.InstanceName))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }

        #endregion
    }
}