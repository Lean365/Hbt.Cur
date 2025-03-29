#nullable enable

//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : WorkflowApproverResolver.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-23 12:00
// 版本号 : V1.0.0
// 描述    : 工作流审批人解析器
//===================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lean.Hbt.Domain.Entities.Identity;
using Lean.Hbt.Domain.Entities.Workflow;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Models.Workflow;
namespace Lean.Hbt.Application.Services.Workflow.Engine.Resolvers
{
    /// <summary>
    /// 工作流审批人解析器
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-23
    /// </remarks>
    public class HbtWorkflowApproverResolver : IHbtWorkflowApproverResolver
    {
        private readonly IHbtRepository<HbtUser> _userRepository;
        private readonly IHbtRepository<HbtRole> _roleRepository;
        private readonly IHbtRepository<HbtDept> _deptRepository;
        private readonly IHbtRepository<HbtUserRole> _userRoleRepository;
        private readonly IHbtRepository<HbtUserDept> _userDeptRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtWorkflowApproverResolver(
            IHbtRepository<HbtUser> userRepository,
            IHbtRepository<HbtRole> roleRepository,
            IHbtRepository<HbtDept> deptRepository,
            IHbtRepository<HbtUserRole> userRoleRepository,
            IHbtRepository<HbtUserDept> userDeptRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _deptRepository = deptRepository;
            _userRoleRepository = userRoleRepository;
            _userDeptRepository = userDeptRepository;
        }

        /// <summary>
        /// 解析审批人
        /// </summary>
        public async Task<List<long>> ResolveApproversAsync(
            HbtWorkflowInstance instance,
            HbtWorkflowNode node,
            HbtWorkflowNodeConfig config)
        {
            var approvers = new List<long>();

            switch (config.ApproverType)
            {
                case 0: // 0 表示指定用户
                    if (config.ApproverIds?.Any() == true)
                    {
                        approvers.AddRange(config.ApproverIds);
                    }
                    break;

                case 1: // 1 表示指定角色
                    if (config.RoleIds?.Any() == true)
                    {
                        foreach (var roleId in config.RoleIds)
                        {
                            var userRoles = await _userRoleRepository.GetListAsync(ur => ur.RoleId == roleId);
                            approvers.AddRange(userRoles.Select(ur => ur.UserId));
                        }
                    }
                    break;

                case 2: // 2 表示指定部门
                    if (config.DepartmentIds?.Any() == true)
                    {
                        foreach (var deptId in config.DepartmentIds)
                        {
                            var userDepts = await _userDeptRepository.GetListAsync(ud => ud.DeptId == deptId);
                            approvers.AddRange(userDepts.Select(ud => ud.UserId));
                        }
                    }
                    break;

                case 3: // 3 表示发起人自己
                    approvers.Add(instance.InitiatorId);
                    break;

                case 4: // 4 表示发起人上级
                    var initiator = await GetUserByIdAsync(instance.InitiatorId);
                    if (initiator != null)
                    {
                        var initiatorDepts = await _userDeptRepository.GetListAsync(ud => ud.UserId == initiator.Id);
                        foreach (var dept in initiatorDepts)
                        {
                            var deptInfo = await GetDeptByIdAsync(dept.DeptId);
                            if (deptInfo?.Leader != null)
                            {
                                var leader = await GetDeptLeaderAsync(deptInfo.Id);
                                if (leader != null)
                                {
                                    approvers.Add(leader.Id);
                                }
                            }
                        }
                    }
                    break;

                case 5: // 5 表示发起人部门主管
                    var initiatorDepartments = await _userDeptRepository.GetListAsync(ud => ud.UserId == instance.InitiatorId);
                    foreach (var dept in initiatorDepartments)
                    {
                        var deptInfo = await GetDeptByIdAsync(dept.DeptId);
                        if (deptInfo?.Leader != null)
                        {
                            var leader = await GetDeptLeaderAsync(deptInfo.Id);
                            if (leader != null)
                            {
                                approvers.Add(leader.Id);
                            }
                        }
                    }
                    break;

                default:
                    throw new InvalidOperationException($"不支持的审批人类型: {config.ApproverType}");
            }

            return approvers.Distinct().ToList();
        }

        private async Task<HbtUser?> GetUserByIdAsync(long userId)
        {
            return await _userRepository.GetInfoAsync(x => x.Id == userId);
        }

        private async Task<HbtDept?> GetDeptByIdAsync(long deptId)
        {
            return await _deptRepository.GetInfoAsync(x => x.Id == deptId);
        }

        private async Task<HbtUser?> GetDeptLeaderAsync(long deptId)
        {
            var dept = await _deptRepository.GetInfoAsync(x => x.Id == deptId);
            if (string.IsNullOrEmpty(dept?.Leader)) return null;
            return await _userRepository.GetInfoAsync(x => x.UserName == dept.Leader);
        }

        private async Task<HbtUser?> GetParentDeptLeaderAsync(long deptId)
        {
            var dept = await _deptRepository.GetInfoAsync(x => x.Id == deptId);
            if (dept?.ParentId == 0) return null;
            
            var parentDept = await _deptRepository.GetInfoAsync(x => x.Id == dept.ParentId);
            if (string.IsNullOrEmpty(parentDept?.Leader)) return null;
            
            return await _userRepository.GetInfoAsync(x => x.UserName == parentDept.Leader);
        }
    }
} 
