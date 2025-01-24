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
    public class WorkflowApproverResolver : IWorkflowApproverResolver
    {
        private readonly IHbtRepository<HbtUser> _userRepository;
        private readonly IHbtRepository<HbtRole> _roleRepository;
        private readonly IHbtRepository<HbtDept> _deptRepository;
        private readonly IHbtRepository<HbtUserRole> _userRoleRepository;
        private readonly IHbtRepository<HbtUserDept> _userDeptRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowApproverResolver(
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
                case HbtWorkflowApproverType.Specified:
                    if (config.ApproverIds?.Any() == true)
                    {
                        approvers.AddRange(config.ApproverIds);
                    }
                    break;

                case HbtWorkflowApproverType.Role:
                    if (config.RoleIds?.Any() == true)
                    {
                        foreach (var roleId in config.RoleIds)
                        {
                            var userRoles = await _userRoleRepository.GetListAsync(ur => ur.RoleId == roleId);
                            approvers.AddRange(userRoles.Select(ur => ur.UserId));
                        }
                    }
                    break;

                case HbtWorkflowApproverType.Department:
                    if (config.DepartmentIds?.Any() == true)
                    {
                        foreach (var deptId in config.DepartmentIds)
                        {
                            var userDepts = await _userDeptRepository.GetListAsync(ud => ud.DeptId == deptId);
                            approvers.AddRange(userDepts.Select(ud => ud.UserId));
                        }
                    }
                    break;

                case HbtWorkflowApproverType.Initiator:
                    approvers.Add(instance.InitiatorId);
                    break;

                case HbtWorkflowApproverType.InitiatorSuperior:
                    var initiator = await _userRepository.FirstOrDefaultAsync(u => u.Id == instance.InitiatorId);
                    if (initiator != null)
                    {
                        var initiatorDepts = await _userDeptRepository.GetListAsync(ud => ud.UserId == initiator.Id);
                        foreach (var dept in initiatorDepts)
                        {
                            var deptInfo = await _deptRepository.FirstOrDefaultAsync(d => d.Id == dept.DeptId);
                            if (deptInfo?.Leader != null)
                            {
                                var leader = await _userRepository.FirstOrDefaultAsync(u => u.UserName == deptInfo.Leader);
                                if (leader != null)
                                {
                                    approvers.Add(leader.Id);
                                }
                            }
                        }
                    }
                    break;

                case HbtWorkflowApproverType.InitiatorDeptManager:
                    var initiatorForDept = await _userRepository.FirstOrDefaultAsync(u => u.Id == instance.InitiatorId);
                    if (initiatorForDept != null)
                    {
                        var initiatorDeptIds = await _userDeptRepository.GetListAsync(ud => ud.UserId == initiatorForDept.Id);
                        foreach (var deptId in initiatorDeptIds.Select(d => d.DeptId))
                        {
                            var dept = await _deptRepository.FirstOrDefaultAsync(d => d.Id == deptId);
                            if (dept?.Leader != null)
                            {
                                var deptManager = await _userRepository.FirstOrDefaultAsync(u => u.UserName == dept.Leader);
                                if (deptManager != null)
                                {
                                    approvers.Add(deptManager.Id);
                                }
                            }
                        }
                    }
                    break;

                default:
                    throw new ArgumentException($"不支持的审批人类型: {config.ApproverType}");
            }

            return approvers.Distinct().ToList();
        }
    }
} 
