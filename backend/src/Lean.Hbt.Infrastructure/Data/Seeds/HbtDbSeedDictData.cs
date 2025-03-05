//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 字典数据种子数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 字典数据种子数据初始化类
/// </summary>
public class HbtDbSeedDictData
{
    private readonly IHbtRepository<HbtDictData> _dictDataRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataRepository">字典数据仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedDictData(IHbtRepository<HbtDictData> dictDataRepository, IHbtLogger logger)
    {
        _dictDataRepository = dictDataRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化字典数据
    /// </summary>
    public async Task<(int, int)> InitializeDictDataAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultDictData = new List<HbtDictData>
        {
            // 系统状态
            new HbtDictData
            {
                DictType = "sys_status",
                DictLabel = "正常",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "正常状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_status",
                DictLabel = "停用",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "停用状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 是否选项
            new HbtDictData
            {
                DictType = "sys_yes_no",
                DictLabel = "是",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "是",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_yes_no",
                DictLabel = "否",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "否",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 性别类型
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "男",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "男性",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "女",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "女性",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "未知",
                DictValue = "0",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "未知性别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 通知类型
            new HbtDictData
            {
                DictType = "sys_notice_type",
                DictLabel = "通知",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "通知类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notice_type",
                DictLabel = "公告",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "公告类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 通知状态
            new HbtDictData
            {
                DictType = "sys_notice_status",
                DictLabel = "正常",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "正常状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notice_status",
                DictLabel = "关闭",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "关闭状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 操作类型
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "其他",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "其他操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "查询",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "查询操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "新增",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "新增操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "修改",
                DictValue = "3",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "修改操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "删除",
                DictValue = "4",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "删除操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "导出",
                DictValue = "5",
                OrderNum = 6,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "导出操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "导入",
                DictValue = "6",
                OrderNum = 7,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "导入操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 日志级别
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "调试",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "调试级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "信息",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "信息级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "警告",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "警告级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "错误",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "错误级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "致命",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 6,
                ListClass = 6,
                Remark = "致命级别",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 用户类型
            new HbtDictData
            {
                DictType = "sys_user_type",
                DictLabel = "系统用户",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "系统用户",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_user_type",
                DictLabel = "普通用户",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "普通用户",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 是否默认
            new HbtDictData
            {
                DictType = "sys_is_default",
                DictLabel = "是",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "是",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_default",
                DictLabel = "否",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "否",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 数据范围
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "全部数据",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "全部数据权限",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "本部门及以下数据",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "本部门及以下数据权限",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "本部门数据",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "本部门数据权限",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "仅本人数据",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "仅本人数据权限",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "自定义数据",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "自定义数据权限",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 是否为外链
            new HbtDictData
            {
                DictType = "sys_IsExternal",
                DictLabel = "是",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "是",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_IsExternal",
                DictLabel = "否",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "否",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 是否缓存
            new HbtDictData
            {
                DictType = "sys_is_cache",
                DictLabel = "是",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "是",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_cache",
                DictLabel = "否",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "否",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 菜单类型
            new HbtDictData
            {
                DictType = "sys_menu_type",
                DictLabel = "目录",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "目录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_menu_type",
                DictLabel = "菜单",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "菜单",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_menu_type",
                DictLabel = "按钮",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "按钮",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 显示状态
            new HbtDictData
            {
                DictType = "sys_is_visible",
                DictLabel = "显示",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "显示",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_visible",
                DictLabel = "隐藏",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "隐藏",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 登录类型
            new HbtDictData
            {
                DictType = "sys_login_type",
                DictLabel = "账号密码",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "账号密码登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_type",
                DictLabel = "手机验证码",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "手机验证码登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_type",
                DictLabel = "邮箱验证码",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "邮箱验证码登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_type",
                DictLabel = "第三方登录",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "第三方登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 登录来源
            new HbtDictData
            {
                DictType = "sys_login_source",
                DictLabel = "PC端",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "PC端登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_source",
                DictLabel = "移动端",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "移动端登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_source",
                DictLabel = "小程序",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "小程序登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 登录状态
            new HbtDictData
            {
                DictType = "sys_login_status",
                DictLabel = "成功",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "登录成功",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_status",
                DictLabel = "失败",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "登录失败",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 登录提供者
            new HbtDictData
            {
                DictType = "sys_login_provider",
                DictLabel = "系统",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "系统登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_provider",
                DictLabel = "微信",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "微信登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_provider",
                DictLabel = "QQ",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "QQ登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_provider",
                DictLabel = "钉钉",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "钉钉登录",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 设备类型
            new HbtDictData
            {
                DictType = "sys_device_type",
                DictLabel = "电脑",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "电脑设备",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_device_type",
                DictLabel = "平板",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "平板设备",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_device_type",
                DictLabel = "手机",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "手机设备",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 浏览器类型
            new HbtDictData
            {
                DictType = "sys_browser_typpe",
                DictLabel = "Chrome",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "Chrome浏览器",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_browser_typpe",
                DictLabel = "Firefox",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "Firefox浏览器",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_browser_typpe",
                DictLabel = "Edge",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "Edge浏览器",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_browser_typpe",
                DictLabel = "Safari",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "Safari浏览器",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 操作系统类型
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "Windows",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "Windows操作系统",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "Linux",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "Linux操作系统",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "MacOS",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "MacOS操作系统",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "Android",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "Android操作系统",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "iOS",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "iOS操作系统",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 设备状态
            new HbtDictData
            {
                DictType = "sys_device_status",
                DictLabel = "在线",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "设备在线",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_device_status",
                DictLabel = "离线",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "设备离线",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 系统内置
            new HbtDictData
            {
                DictType = "sys_is_Builtin",
                DictLabel = "是",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "系统内置",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_Builtin",
                DictLabel = "否",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "非系统内置",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 样式类别
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "主要",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "主要样式",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "成功",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "成功样式",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "信息",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "信息样式",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "警告",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "警告样式",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "危险",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "危险样式",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "暗黑",
                DictValue = "6",
                OrderNum = 6,
                Status = 0,
                TenantId = 0,
                CssClass = 6,
                ListClass = 6,
                Remark = "暗黑样式",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 字典类别
            new HbtDictData
            {
                DictType = "sys_dict_category",
                DictLabel = "系统类",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "系统类字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_dict_category",
                DictLabel = "业务类",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "业务类字典",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 是否已读
            new HbtDictData
            {
                DictType = "sys_is_read",
                DictLabel = "已读",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "已读",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_read",
                DictLabel = "未读",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "未读",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 消息类型
            new HbtDictData
            {
                DictType = "sys_message_type",
                DictLabel = "系统消息",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "系统消息",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_message_type",
                DictLabel = "通知公告",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "通知公告",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_message_type",
                DictLabel = "待办事项",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "待办事项",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流活动类型
            new HbtDictData
            {
                DictType = "wfs_activity_type",
                DictLabel = "开始",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "开始活动",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_activity_type",
                DictLabel = "审批",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "审批活动",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_activity_type",
                DictLabel = "办理",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "办理活动",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_activity_type",
                DictLabel = "结束",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "结束活动",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流程分类
            new HbtDictData
            {
                DictType = "wfs_workflow_category",
                DictLabel = "行政审批",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "行政审批流程",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_workflow_category",
                DictLabel = "财务审批",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "财务审批流程",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_workflow_category",
                DictLabel = "人事审批",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "人事审批流程",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流操作结果
            new HbtDictData
            {
                DictType = "wfs_oper_result",
                DictLabel = "同意",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "同意",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_result",
                DictLabel = "驳回",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "驳回",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_result",
                DictLabel = "退回",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "退回",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流操作类型
            new HbtDictData
            {
                DictType = "wfs_oper_type",
                DictLabel = "提交",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "提交操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_type",
                DictLabel = "审批",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "审批操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_type",
                DictLabel = "撤回",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "撤回操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_type",
                DictLabel = "转办",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "转办操作",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流节点类型
            new HbtDictData
            {
                DictType = "wfs_node_type",
                DictLabel = "开始节点",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "开始节点",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_node_type",
                DictLabel = "审批节点",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "审批节点",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_node_type",
                DictLabel = "分支节点",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "分支节点",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_node_type",
                DictLabel = "结束节点",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "结束节点",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流是否完成
            new HbtDictData
            {
                DictType = "wfs_is_completed",
                DictLabel = "已完成",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "已完成",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_is_completed",
                DictLabel = "未完成",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "未完成",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流计划任务类型
            new HbtDictData
            {
                DictType = "wfs_task_type",
                DictLabel = "审批任务",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "审批任务",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_type",
                DictLabel = "办理任务",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "办理任务",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_type",
                DictLabel = "传阅任务",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "传阅任务",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流处理结果
            new HbtDictData
            {
                DictType = "wfs_is_result",
                DictLabel = "同意",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "同意",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_is_result",
                DictLabel = "不同意",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "不同意",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_is_result",
                DictLabel = "退回",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "退回",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流优先级
            new HbtDictData
            {
                DictType = "wfs_priority_type",
                DictLabel = "普通",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "普通优先级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_priority_type",
                DictLabel = "重要",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "重要优先级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_priority_type",
                DictLabel = "紧急",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "紧急优先级",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流变量类型
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "字符串",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "字符串类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "数字",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "数字类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "布尔",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "布尔类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "日期",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "日期类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "数组",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "数组类型",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },

            // 工作流计划任务状态
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "待处理",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,
                TenantId = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "待处理状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "处理中",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,
                TenantId = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "处理中状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "已完成",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,
                TenantId = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "已完成状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "已取消",
                DictValue = "3",
                OrderNum = 4,
                Status = 0,
                TenantId = 0,
                CssClass = 4,
                ListClass = 4,
                Remark = "已取消状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "已失败",
                DictValue = "4",
                OrderNum = 5,
                Status = 0,
                TenantId = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "已失败状态",
                CreateBy = "system",
                CreateTime = DateTime.Now,
                UpdateBy = "system",
                UpdateTime = DateTime.Now
            },
        };

        foreach (var dictData in defaultDictData)
        {
            var existingDictData = await _dictDataRepository.FirstOrDefaultAsync(d => d.DictType == dictData.DictType && d.DictValue == dictData.DictValue);
            if (existingDictData == null)
            {
                await _dictDataRepository.InsertAsync(dictData);
                insertCount++;
                _logger.Info($"[创建] 字典数据 '{dictData.DictLabel}' 创建成功");
            }
            else
            {
                // 更新所有字段
                existingDictData.DictLabel = dictData.DictLabel;
                existingDictData.DictValue = dictData.DictValue;
                existingDictData.DictType = dictData.DictType;
                existingDictData.OrderNum = dictData.OrderNum;
                existingDictData.CssClass = dictData.CssClass;
                existingDictData.ListClass = dictData.ListClass;
                existingDictData.Status = dictData.Status;
                existingDictData.TenantId = dictData.TenantId;
                existingDictData.Remark = dictData.Remark;
                existingDictData.CreateBy = dictData.CreateBy;
                existingDictData.CreateTime = dictData.CreateTime;
                existingDictData.UpdateBy = "system";
                existingDictData.UpdateTime = DateTime.Now;

                await _dictDataRepository.UpdateAsync(existingDictData);
                updateCount++;
                _logger.Info($"[更新] 字典数据 '{existingDictData.DictLabel}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }
}