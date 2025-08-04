#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtApproverResolver.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流审批人解析器实现
//===================================================================

using Hbt.Cur.Domain.IServices;
using System.Text.Json;

namespace Hbt.Cur.Application.Services.Workflow.Engine.Resolvers
{
    /// <summary>
    /// 工作流审批人解析器实现
    /// </summary>
    public class HbtApproverResolver : IHbtApproverResolver
    {
        private readonly IHbtLogger _logger;
        private readonly IHbtCurrentUser _currentUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="currentUser"></param>
        public HbtApproverResolver(IHbtLogger logger, IHbtCurrentUser currentUser)
        {
            _logger = logger;
            _currentUser = currentUser;
        }

        /// <inheritdoc/>
        public async Task<List<long>> ResolveApproversAsync(
            long instanceId,
            string nodeId,
            int approverType,
            string? approverConfig,
            Dictionary<string, object>? variables = null)
        {
            try
            {
                switch (approverType)
                {
                    case 1: // 指定用户
                        return ResolveSpecifiedUsersAsync(approverConfig);
                    case 2: // 角色
                        return ResolveByRoleAsync(approverConfig);
                    case 3: // 部门
                        return ResolveByDepartmentAsync(approverConfig);
                    case 4: // 动态
                        return ResolveDynamicAsync(instanceId, nodeId, approverConfig, variables);
                    default:
                        _logger.Warn($"不支持的审批人类型: {approverType}");
                        return new List<long>();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"解析审批人失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        /// <inheritdoc/>
        public bool ValidateConfig(int approverType, string? approverConfig)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return false;

            try
            {
                switch (approverType)
                {
                    case 1: // 指定用户
                        var userConfig = JsonSerializer.Deserialize<UserApproverConfig>(approverConfig);
                        return userConfig?.UserIds != null && userConfig.UserIds.Any();
                    case 2: // 角色
                        var roleConfig = JsonSerializer.Deserialize<RoleApproverConfig>(approverConfig);
                        return !string.IsNullOrEmpty(roleConfig?.RoleCode);
                    case 3: // 部门
                        var deptConfig = JsonSerializer.Deserialize<DepartmentApproverConfig>(approverConfig);
                        return !string.IsNullOrEmpty(deptConfig?.DepartmentCode);
                    case 4: // 动态
                        var dynamicConfig = JsonSerializer.Deserialize<DynamicApproverConfig>(approverConfig);
                        return !string.IsNullOrEmpty(dynamicConfig?.Expression);
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public string GetApproverTypeDescription(int approverType)
        {
            return approverType switch
            {
                1 => "指定用户",
                2 => "角色",
                3 => "部门",
                4 => "动态",
                _ => "未知类型"
            };
        }

        #region 私有方法

        /// <summary>
        /// 解析指定用户
        /// </summary>
        private List<long> ResolveSpecifiedUsersAsync(string? approverConfig)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return new List<long>();

            try
            {
                var config = JsonSerializer.Deserialize<UserApproverConfig>(approverConfig);
                return config?.UserIds ?? new List<long>();
            }
            catch (Exception ex)
            {
                _logger.Error($"解析指定用户配置失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        /// <summary>
        /// 根据角色解析审批人
        /// </summary>
        private List<long> ResolveByRoleAsync(string? approverConfig)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return new List<long>();

            try
            {
                var config = JsonSerializer.Deserialize<RoleApproverConfig>(approverConfig);
                if (config == null || string.IsNullOrEmpty(config.RoleCode))
                    return new List<long>();

                // 这里需要调用用户服务获取角色下的用户
                // 简化实现，返回空列表
                _logger.Info($"根据角色[{config.RoleCode}]解析审批人");
                return new List<long>();
            }
            catch (Exception ex)
            {
                _logger.Error($"根据角色解析审批人失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        /// <summary>
        /// 根据部门解析审批人
        /// </summary>
        private List<long> ResolveByDepartmentAsync(string? approverConfig)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return new List<long>();

            try
            {
                var config = JsonSerializer.Deserialize<DepartmentApproverConfig>(approverConfig);
                if (config == null || string.IsNullOrEmpty(config.DepartmentCode))
                    return new List<long>();

                // 这里需要调用用户服务获取部门下的用户
                // 简化实现，返回空列表
                _logger.Info($"根据部门[{config.DepartmentCode}]解析审批人");
                return new List<long>();
            }
            catch (Exception ex)
            {
                _logger.Error($"根据部门解析审批人失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        /// <summary>
        /// 动态解析审批人
        /// </summary>
        private List<long> ResolveDynamicAsync(
            long instanceId,
            string nodeId,
            string? approverConfig,
            Dictionary<string, object>? variables)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return new List<long>();

            try
            {
                var config = JsonSerializer.Deserialize<DynamicApproverConfig>(approverConfig);
                if (config == null || string.IsNullOrEmpty(config.Expression))
                    return new List<long>();

                // 这里需要执行动态表达式来获取审批人
                // 简化实现，返回空列表
                _logger.Info($"执行动态表达式[{config.Expression}]解析审批人");
                return new List<long>();
            }
            catch (Exception ex)
            {
                _logger.Error($"动态解析审批人失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        #endregion

        #region 配置类

        /// <summary>
        /// 指定用户审批人配置
        /// </summary>
        private class UserApproverConfig
        {
            public List<long> UserIds { get; set; } = new();
        }

        /// <summary>
        /// 角色审批人配置
        /// </summary>
        private class RoleApproverConfig
        {
            public string RoleCode { get; set; } = string.Empty;
            public bool IncludeSubRoles { get; set; } = false;
        }

        /// <summary>
        /// 部门审批人配置
        /// </summary>
        private class DepartmentApproverConfig
        {
            public string DepartmentCode { get; set; } = string.Empty;
            public bool IncludeSubDepartments { get; set; } = false;
            public string? Position { get; set; }
        }

        /// <summary>
        /// 动态审批人配置
        /// </summary>
        private class DynamicApproverConfig
        {
            public string Expression { get; set; } = string.Empty;
            public Dictionary<string, object>? Parameters { get; set; }
        }

        #endregion
    }
} 
