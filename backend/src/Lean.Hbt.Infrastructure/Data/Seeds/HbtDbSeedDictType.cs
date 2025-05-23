//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 字典类型种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 字典类型种子数据初始化类
/// </summary>
public class HbtDbSeedDictType
{
    private readonly IHbtRepository<HbtDictType> _dictTypeRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedDictType(IHbtRepository<HbtDictType> dictTypeRepository, IHbtLogger logger)
    {
        _dictTypeRepository = dictTypeRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化字典类型数据
    /// </summary>
    public async Task<(int, int)> InitializeDictTypeAsync(long tenantId)
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictTypes = new List<HbtDictType>
        {
            new HbtDictType
            {
                DictName = "系统状态",
                DictType = "sys_normal_disable",
                OrderNum = 1,
                Status = 0,
                
                Remark = "系统状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "是否选项",
                DictType = "sys_yes_no",
                OrderNum = 2,
                Status = 0,
                
                Remark = "是否选项字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "性别类型",
                DictType = "sys_gender",
                OrderNum = 3,
                Status = 0,
                
                Remark = "性别类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "通知类型",
                DictType = "sys_notice_type",
                OrderNum = 4,
                Status = 0,
                
                Remark = "通知类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "通知状态",
                DictType = "sys_notice_status",
                OrderNum = 5,
                Status = 0,
                
                Remark = "通知状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "操作类型",
                DictType = "sys_oper_type",
                OrderNum = 6,
                Status = 0,
                
                Remark = "操作类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "用户类型",
                DictType = "sys_user_type",
                OrderNum = 7,
                Status = 0,
                
                Remark = "用户类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "是否默认",
                DictType = "sys_is_default",
                OrderNum = 8,
                Status = 0,
                
                Remark = "是否默认字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "数据范围",
                DictType = "sys_data_scope",
                OrderNum = 9,
                Status = 0,
                
                Remark = "数据范围字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "是否为外链",
                DictType = "sys_IsExternal",
                OrderNum = 10,
                Status = 0,
                
                Remark = "是否为外链字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "是否缓存",
                DictType = "sys_is_cache",
                OrderNum = 11,
                Status = 0,
                
                Remark = "是否缓存字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "菜单类型",
                DictType = "sys_menu_type",
                OrderNum = 12,
                Status = 0,
                
                Remark = "菜单类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "显示状态",
                DictType = "sys_is_visible",
                OrderNum = 13,
                Status = 0,
                
                Remark = "显示状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "登录类型",
                DictType = "sys_login_type",
                OrderNum = 14,
                Status = 0,
                
                Remark = "登录类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "登录来源",
                DictType = "sys_login_source",
                OrderNum = 15,
                Status = 0,
                
                Remark = "登录来源字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "登录状态",
                DictType = "sys_login_status",
                OrderNum = 16,
                Status = 0,
                
                Remark = "登录状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "登录提供者",
                DictType = "sys_login_provider",
                OrderNum = 17,
                Status = 0,
                
                Remark = "登录提供者字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备类型",
                DictType = "sys_device_type",
                OrderNum = 18,
                Status = 0,
                
                Remark = "设备类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "浏览器类型",
                DictType = "sys_browser_typpe",
                OrderNum = 19,
                Status = 0,
                
                Remark = "浏览器类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "操作系统类型",
                DictType = "sys_Os_type",
                OrderNum = 20,
                Status = 0,
                
                Remark = "操作系统类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "设备状态",
                DictType = "sys_device_status",
                OrderNum = 21,
                Status = 0,
                
                Remark = "设备状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "日志级别",
                DictType = "sys_log_level",
                OrderNum = 22,
                Status = 0,
                
                Remark = "日志级别字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "系统内置",
                DictType = "sys_is_Builtin",
                OrderNum = 23,
                Status = 0,
                
                Remark = "系统内置字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "样式类别",
                DictType = "sys_css_type",
                OrderNum = 24,
                Status = 0,
                
                Remark = "样式类别字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "字典类别",
                DictType = "sys_dict_category",
                OrderNum = 25,
                Status = 0,
                
                Remark = "字典类别字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "是否已读",
                DictType = "sys_is_read",
                OrderNum = 26,
                Status = 0,
                
                Remark = "是否已读字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "消息类型",
                DictType = "sys_message_type",
                OrderNum = 27,
                Status = 0,
                
                Remark = "消息类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流活动类型",
                DictType = "wfs_activity_type",
                OrderNum = 28,
                Status = 0,
                
                Remark = "工作流活动类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流程分类",
                DictType = "wfs_workflow_category",
                OrderNum = 29,
                Status = 0,
                
                Remark = "工作流程分类字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流操作结果",
                DictType = "wfs_oper_result",
                OrderNum = 30,
                Status = 0,
                
                Remark = "工作流操作结果字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流操作类型",
                DictType = "wfs_oper_type",
                OrderNum = 31,
                Status = 0,
                
                Remark = "工作流操作类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流节点类型",
                DictType = "wfs_node_type",
                OrderNum = 32,
                Status = 0,
                
                Remark = "工作流节点类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流是否完成",
                DictType = "wfs_is_completed",
                OrderNum = 33,
                Status = 0,
                
                Remark = "工作流是否完成字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流计划任务类型",
                DictType = "wfs_task_type",
                OrderNum = 34,
                Status = 0,
                
                Remark = "工作流计划任务类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流处理结果",
                DictType = "wfs_is_result",
                OrderNum = 35,
                Status = 0,
                
                Remark = "工作流处理结果字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流优先级",
                DictType = "wfs_priority_type",
                OrderNum = 36,
                Status = 0,
                
                Remark = "工作流优先级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流变量类型",
                DictType = "wfs_variable_type",
                OrderNum = 37,
                Status = 0,
                
                Remark = "工作流变量类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "工作流计划任务状态",
                DictType = "wfs_task_status",
                OrderNum = 38,
                Status = 0,
                
                Remark = "工作流计划任务状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "邮件状态",
                DictType = "sys_mail_status",
                OrderNum = 39,
                Status = 0,
                
                Remark = "邮件状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "邮件类型",
                DictType = "sys_mail_type",
                OrderNum = 40,
                Status = 0,
                
                Remark = "邮件类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "邮件优先级",
                DictType = "sys_mail_priority",
                OrderNum = 41,
                Status = 0,
                
                Remark = "邮件优先级字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "通知状态",
                DictType = "sys_notify_status",
                OrderNum = 42,
                Status = 0,
                
                Remark = "通知状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "通知类型",
                DictType = "sys_notify_type",
                OrderNum = 43,
                Status = 0,
                
                Remark = "通知类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "通知级别",
                DictType = "sys_notify_level",
                OrderNum = 44,
                Status = 0,
                
                Remark = "通知级别字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "任务状态",
                DictType = "sys_task_status",
                OrderNum = 45,
                Status = 0,
                
                Remark = "任务状态字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "任务类型",
                DictType = "sys_task_type",
                OrderNum = 46,
                Status = 0,
                
                Remark = "任务类型字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "任务执行策略",
                DictType = "sys_task_policy",
                OrderNum = 47,
                Status = 0,
                
                Remark = "任务执行策略字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictType
            {
                DictName = "任务并发策略",
                DictType = "sys_task_concurrent",
                OrderNum = 48,
                Status = 0,
                
                Remark = "任务并发策略字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };

        foreach (var dictType in defaultDictTypes)
        {
            var existingDictType = await _dictTypeRepository.GetFirstAsync(d => d.DictType == dictType.DictType);
            if (existingDictType == null)
            {
                dictType.TenantId = tenantId;
                dictType.CreateBy = "Hbt365";
                dictType.CreateTime = DateTime.Now;
                dictType.UpdateBy = "Hbt365";
                dictType.UpdateTime = DateTime.Now;
                await _dictTypeRepository.CreateAsync(dictType);
                insertCount++;
                _logger.Info($"[创建] 字典类型 '{dictType.DictName}' 创建成功");
            }
            else
            {
                existingDictType.DictName = dictType.DictName;
                existingDictType.DictType = dictType.DictType;
                existingDictType.IsBuiltin = dictType.IsBuiltin;
                existingDictType.OrderNum = dictType.OrderNum;
                existingDictType.Status = dictType.Status;
                existingDictType.TenantId = tenantId;
                existingDictType.Remark = dictType.Remark;
                existingDictType.CreateBy = dictType.CreateBy;
                existingDictType.CreateTime = dictType.CreateTime;
                existingDictType.UpdateBy = "Hbt365";
                existingDictType.UpdateTime = DateTime.Now;

                await _dictTypeRepository.UpdateAsync(existingDictType);
                updateCount++;
                _logger.Info($"[更新] 字典类型 '{existingDictType.DictName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}