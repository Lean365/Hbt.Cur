//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 字典数据种子数据初始化类 - 使用仓储工厂模式
//===================================================================

using Hbt.Cur.Domain.Entities.Routine.Core;
using Hbt.Cur.Domain.Repositories;

namespace Hbt.Cur.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 字典数据种子数据提供类
/// </summary>
public class HbtDbSeedDictData
{
    /// <summary>
    /// 获取默认字典数据
    /// </summary>
    /// <returns>字典数据列表</returns>
    public List<HbtDictData> GetDefaultDictData()
    {
        return new List<HbtDictData>
        {
            // 系统状态
            new HbtDictData
            {
                DictType = "sys_normal_disable",
                DictLabel = "正常",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,
                CssClass = 2,
                ListClass = 2,
                Remark = "正常状态",

            },
            new HbtDictData
            {
                DictType = "sys_normal_disable",
                DictLabel = "停用",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,
                CssClass = 5,
                ListClass = 5,
                Remark = "停用状态",
            },

            // 是否选项
            new HbtDictData
            {
                DictType = "sys_yes_no",
                DictLabel = "是",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,
                CssClass = 1,
                ListClass = 1,
                Remark = "是",

            },
            new HbtDictData
            {
                DictType = "sys_yes_no",
                DictLabel = "否",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,
                CssClass = 3,
                ListClass = 3,
                Remark = "否",
            },

            // 性别类型
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "男",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "男性",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "女",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "女性",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_gender",
                DictLabel = "未知",
                DictValue = "0",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "未知性别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 3,
                ListClass = 3,
                Remark = "通知类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notice_type",
                DictLabel = "公告",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "公告类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 2,
                ListClass = 2,
                Remark = "正常状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notice_status",
                DictLabel = "关闭",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "关闭状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 3,
                ListClass = 3,
                Remark = "其他操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "查询",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "查询操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "新增",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "新增操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "修改",
                DictValue = "3",
                OrderNum = 4,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "修改操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "删除",
                DictValue = "4",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "删除操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "导出",
                DictValue = "5",
                OrderNum = 6,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "导出操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_oper_type",
                DictLabel = "导入",
                DictValue = "6",
                OrderNum = 7,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "导入操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 3,
                ListClass = 3,
                Remark = "调试级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "信息",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "信息级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "警告",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "警告级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "错误",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "错误级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_log_level",
                DictLabel = "致命",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,

                CssClass = 6,
                ListClass = 6,
                Remark = "致命级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "系统用户",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_user_type",
                DictLabel = "普通用户",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "普通用户",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
             new HbtDictData
            {
                DictType = "sys_user_type",
                DictLabel = "管理员",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "普通用户",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_user_type",
                DictLabel = "OAuth用户",
                DictValue = "3",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "OAuth用户",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 是否默认
            new HbtDictData
            {
                DictType = "sys_is_default",
                DictLabel = "是",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "是",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_default",
                DictLabel = "否",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "否",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "全部数据权限",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "本部门及以下数据",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "本部门及以下数据权限",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "本部门数据",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "本部门数据权限",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "仅本人数据",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "仅本人数据权限",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_data_scope",
                DictLabel = "自定义数据",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "自定义数据权限",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 是否为外链
            new HbtDictData
            {
                DictType = "sys_IsExternal",
                DictLabel = "是",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "是",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_IsExternal",
                DictLabel = "否",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "否",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 是否缓存
            new HbtDictData
            {
                DictType = "sys_is_cache",
                DictLabel = "是",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "是",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_cache",
                DictLabel = "否",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "否",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "目录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_menu_type",
                DictLabel = "菜单",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "菜单",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_menu_type",
                DictLabel = "按钮",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "按钮",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 显示状态
            new HbtDictData
            {
                DictType = "sys_is_visible",
                DictLabel = "显示",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "显示",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_visible",
                DictLabel = "隐藏",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "隐藏",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "账号密码登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_type",
                DictLabel = "手机验证码",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "手机验证码登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_type",
                DictLabel = "邮箱验证码",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "邮箱验证码登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_type",
                DictLabel = "第三方登录",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "第三方登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "PC端登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_source",
                DictLabel = "移动端",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "移动端登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_source",
                DictLabel = "小程序",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "小程序登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 2,
                ListClass = 2,
                Remark = "登录成功",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_status",
                DictLabel = "失败",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "登录失败",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "系统登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_provider",
                DictLabel = "微信",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "微信登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_provider",
                DictLabel = "QQ",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "QQ登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_login_provider",
                DictLabel = "钉钉",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "钉钉登录",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "电脑设备",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_device_type",
                DictLabel = "平板",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "平板设备",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_device_type",
                DictLabel = "手机",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "手机设备",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "Chrome浏览器",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_browser_typpe",
                DictLabel = "Firefox",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "Firefox浏览器",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_browser_typpe",
                DictLabel = "Edge",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "Edge浏览器",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_browser_typpe",
                DictLabel = "Safari",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "Safari浏览器",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "Windows操作系统",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "Linux",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "Linux操作系统",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "MacOS",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "MacOS操作系统",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "Android",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "Android操作系统",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_Os_type",
                DictLabel = "iOS",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "iOS操作系统",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 2,
                ListClass = 2,
                Remark = "设备在线",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_device_status",
                DictLabel = "离线",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "设备离线",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "系统内置",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_Builtin",
                DictLabel = "否",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "非系统内置",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "主要样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "成功",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "成功样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "信息",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "信息样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "警告",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "警告样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "危险",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "危险样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "暗黑",
                DictValue = "6",
                OrderNum = 6,
                Status = 0,

                CssClass = 6,
                ListClass = 6,
                Remark = "暗黑样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 流程状态样式
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程草稿",
                DictValue = "10",
                OrderNum = 10,
                Status = 0,

                CssClass = 10,
                ListClass = 10,
                Remark = "流程草稿样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程待处理",
                DictValue = "11",
                OrderNum = 11,
                Status = 0,

                CssClass = 11,
                ListClass = 11,
                Remark = "流程待处理样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程进行中",
                DictValue = "12",
                OrderNum = 12,
                Status = 0,

                CssClass = 12,
                ListClass = 12,
                Remark = "流程进行中样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程已完成",
                DictValue = "13",
                OrderNum = 13,
                Status = 0,

                CssClass = 13,
                ListClass = 13,
                Remark = "流程已完成样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程已驳回",
                DictValue = "14",
                OrderNum = 14,
                Status = 0,

                CssClass = 14,
                ListClass = 14,
                Remark = "流程已驳回样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程已取消",
                DictValue = "15",
                OrderNum = 15,
                Status = 0,

                CssClass = 15,
                ListClass = 15,
                Remark = "流程已取消样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程已暂停",
                DictValue = "16",
                OrderNum = 16,
                Status = 0,

                CssClass = 16,
                ListClass = 16,
                Remark = "流程已暂停样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程已终止",
                DictValue = "17",
                OrderNum = 17,
                Status = 0,

                CssClass = 17,
                ListClass = 17,
                Remark = "流程已终止样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程已过期",
                DictValue = "18",
                OrderNum = 18,
                Status = 0,

                CssClass = 18,
                ListClass = 18,
                Remark = "流程已过期样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "流程已归档",
                DictValue = "19",
                OrderNum = 19,
                Status = 0,

                CssClass = 19,
                ListClass = 19,
                Remark = "流程已归档样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 邮件状态样式
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件未读",
                DictValue = "20",
                OrderNum = 20,
                Status = 0,

                CssClass = 20,
                ListClass = 20,
                Remark = "邮件未读样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件已读",
                DictValue = "21",
                OrderNum = 21,
                Status = 0,

                CssClass = 21,
                ListClass = 21,
                Remark = "邮件已读样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件已回复",
                DictValue = "22",
                OrderNum = 22,
                Status = 0,

                CssClass = 22,
                ListClass = 22,
                Remark = "邮件已回复样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件已转发",
                DictValue = "23",
                OrderNum = 23,
                Status = 0,

                CssClass = 23,
                ListClass = 23,
                Remark = "邮件已转发样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件已标星",
                DictValue = "24",
                OrderNum = 24,
                Status = 0,

                CssClass = 24,
                ListClass = 24,
                Remark = "邮件已标星样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "垃圾邮件",
                DictValue = "25",
                OrderNum = 25,
                Status = 0,

                CssClass = 25,
                ListClass = 25,
                Remark = "垃圾邮件样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件已删除",
                DictValue = "26",
                OrderNum = 26,
                Status = 0,

                CssClass = 26,
                ListClass = 26,
                Remark = "邮件已删除样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件草稿",
                DictValue = "27",
                OrderNum = 27,
                Status = 0,

                CssClass = 27,
                ListClass = 27,
                Remark = "邮件草稿样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件已发送",
                DictValue = "28",
                OrderNum = 28,
                Status = 0,

                CssClass = 28,
                ListClass = 28,
                Remark = "邮件已发送样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "邮件发送失败",
                DictValue = "29",
                OrderNum = 29,
                Status = 0,

                CssClass = 29,
                ListClass = 29,
                Remark = "邮件发送失败样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 通知状态样式
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "通知未读",
                DictValue = "30",
                OrderNum = 30,
                Status = 0,

                CssClass = 30,
                ListClass = 30,
                Remark = "通知未读样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "通知已读",
                DictValue = "31",
                OrderNum = 31,
                Status = 0,

                CssClass = 31,
                ListClass = 31,
                Remark = "通知已读样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "通知紧急",
                DictValue = "32",
                OrderNum = 32,
                Status = 0,

                CssClass = 32,
                ListClass = 32,
                Remark = "通知紧急样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "通知重要",
                DictValue = "33",
                OrderNum = 33,
                Status = 0,

                CssClass = 33,
                ListClass = 33,
                Remark = "通知重要样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "通知普通",
                DictValue = "34",
                OrderNum = 34,
                Status = 0,

                CssClass = 34,
                ListClass = 34,
                Remark = "通知普通样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "系统通知",
                DictValue = "35",
                OrderNum = 35,
                Status = 0,

                CssClass = 35,
                ListClass = 35,
                Remark = "系统通知样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "业务通知",
                DictValue = "36",
                OrderNum = 36,
                Status = 0,

                CssClass = 36,
                ListClass = 36,
                Remark = "业务通知样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "通知已过期",
                DictValue = "37",
                OrderNum = 37,
                Status = 0,

                CssClass = 37,
                ListClass = 37,
                Remark = "通知已过期样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "通知处理中",
                DictValue = "38",
                OrderNum = 38,
                Status = 0,

                CssClass = 38,
                ListClass = 38,
                Remark = "通知处理中样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "通知已处理",
                DictValue = "39",
                OrderNum = 39,
                Status = 0,

                CssClass = 39,
                ListClass = 39,
                Remark = "通知已处理样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 审批状态样式
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "待审批",
                DictValue = "40",
                OrderNum = 40,
                Status = 0,

                CssClass = 40,
                ListClass = 40,
                Remark = "待审批样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "已通过",
                DictValue = "41",
                OrderNum = 41,
                Status = 0,

                CssClass = 41,
                ListClass = 41,
                Remark = "已通过样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "已拒绝",
                DictValue = "42",
                OrderNum = 42,
                Status = 0,

                CssClass = 42,
                ListClass = 42,
                Remark = "已拒绝样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "审核中",
                DictValue = "43",
                OrderNum = 43,
                Status = 0,

                CssClass = 43,
                ListClass = 43,
                Remark = "审核中样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "已撤回",
                DictValue = "44",
                OrderNum = 44,
                Status = 0,

                CssClass = 44,
                ListClass = 44,
                Remark = "已撤回样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "已转交",
                DictValue = "45",
                OrderNum = 45,
                Status = 0,

                CssClass = 45,
                ListClass = 45,
                Remark = "已转交样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "会签中",
                DictValue = "46",
                OrderNum = 46,
                Status = 0,

                CssClass = 46,
                ListClass = 46,
                Remark = "会签中样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "已退回",
                DictValue = "47",
                OrderNum = 47,
                Status = 0,

                CssClass = 47,
                ListClass = 47,
                Remark = "已退回样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "已暂停",
                DictValue = "48",
                OrderNum = 48,
                Status = 0,

                CssClass = 48,
                ListClass = 48,
                Remark = "已暂停样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "已终止",
                DictValue = "49",
                OrderNum = 49,
                Status = 0,

                CssClass = 49,
                ListClass = 49,
                Remark = "已终止样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 定时任务样式
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务正常",
                DictValue = "50",
                OrderNum = 50,
                Status = 0,

                CssClass = 50,
                ListClass = 50,
                Remark = "任务正常样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务暂停",
                DictValue = "51",
                OrderNum = 51,
                Status = 0,

                CssClass = 51,
                ListClass = 51,
                Remark = "任务暂停样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务运行",
                DictValue = "52",
                OrderNum = 52,
                Status = 0,

                CssClass = 52,
                ListClass = 52,
                Remark = "任务运行样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务错误",
                DictValue = "53",
                OrderNum = 53,
                Status = 0,

                CssClass = 53,
                ListClass = 53,
                Remark = "任务错误样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务阻塞",
                DictValue = "54",
                OrderNum = 54,
                Status = 0,

                CssClass = 54,
                ListClass = 54,
                Remark = "任务阻塞样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务过期",
                DictValue = "55",
                OrderNum = 55,
                Status = 0,

                CssClass = 55,
                ListClass = 55,
                Remark = "任务过期样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务超时",
                DictValue = "56",
                OrderNum = 56,
                Status = 0,

                CssClass = 56,
                ListClass = 56,
                Remark = "任务超时样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务等待",
                DictValue = "57",
                OrderNum = 57,
                Status = 0,

                CssClass = 57,
                ListClass = 57,
                Remark = "任务等待样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务禁用",
                DictValue = "58",
                OrderNum = 58,
                Status = 0,

                CssClass = 58,
                ListClass = 58,
                Remark = "任务禁用样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_css_type",
                DictLabel = "任务删除",
                DictValue = "59",
                OrderNum = 59,
                Status = 0,

                CssClass = 59,
                ListClass = 59,
                Remark = "任务删除样式",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 字典类别
            new HbtDictData
            {
                DictType = "sys_dict_category",
                DictLabel = "系统类",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "系统类字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_dict_category",
                DictLabel = "业务类",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "业务类字典",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 2,
                ListClass = 2,
                Remark = "已读",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_is_read",
                DictLabel = "未读",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "未读",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "系统消息",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_message_type",
                DictLabel = "通知公告",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "通知公告",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_message_type",
                DictLabel = "待办事项",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "待办事项",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "开始活动",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_activity_type",
                DictLabel = "审批",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "审批活动",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_activity_type",
                DictLabel = "办理",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "办理活动",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_activity_type",
                DictLabel = "结束",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "结束活动",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "行政审批流程",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_workflow_category",
                DictLabel = "财务审批",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "财务审批流程",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_workflow_category",
                DictLabel = "人事审批",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "人事审批流程",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 2,
                ListClass = 2,
                Remark = "同意",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_result",
                DictLabel = "驳回",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "驳回",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_result",
                DictLabel = "退回",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "退回",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "提交操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_type",
                DictLabel = "审批",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "审批操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_type",
                DictLabel = "撤回",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "撤回操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_oper_type",
                DictLabel = "转办",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "转办操作",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "开始节点",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_node_type",
                DictLabel = "审批节点",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "审批节点",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_node_type",
                DictLabel = "分支节点",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "分支节点",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_node_type",
                DictLabel = "结束节点",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "结束节点",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 2,
                ListClass = 2,
                Remark = "已完成",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_is_completed",
                DictLabel = "未完成",
                DictValue = "0",
                OrderNum = 2,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "未完成",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "审批任务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_type",
                DictLabel = "办理任务",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "办理任务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_type",
                DictLabel = "传阅任务",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "传阅任务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 2,
                ListClass = 2,
                Remark = "同意",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_is_result",
                DictLabel = "不同意",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "不同意",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_is_result",
                DictLabel = "退回",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "退回",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "普通优先级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_priority_type",
                DictLabel = "重要",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "重要优先级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_priority_type",
                DictLabel = "紧急",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "紧急优先级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 1,
                ListClass = 1,
                Remark = "字符串类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "数字",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "数字类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "布尔",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "布尔类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "日期",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "日期类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_variable_type",
                DictLabel = "数组",
                DictValue = "5",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "数组类型",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
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

                CssClass = 3,
                ListClass = 3,
                Remark = "待处理状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "处理中",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "处理中状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "已完成",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "已完成状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "已取消",
                DictValue = "3",
                OrderNum = 4,
                Status = 0,

                CssClass = 4,
                ListClass = 4,
                Remark = "已取消状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "wfs_task_status",
                DictLabel = "已失败",
                DictValue = "4",
                OrderNum = 5,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "已失败状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 邮件状态
            new HbtDictData
            {
                DictType = "sys_mail_status",
                DictLabel = "未读",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 20,
                ListClass = 20,
                Remark = "未读状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_mail_status",
                DictLabel = "已读",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 21,
                ListClass = 21,
                Remark = "已读状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_mail_status",
                DictLabel = "已回复",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,

                CssClass = 22,
                ListClass = 22,
                Remark = "已回复状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_mail_status",
                DictLabel = "已删除",
                DictValue = "3",
                OrderNum = 4,
                Status = 0,

                CssClass = 26,
                ListClass = 26,
                Remark = "已删除状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 邮件类型
            new HbtDictData
            {
                DictType = "sys_mail_type",
                DictLabel = "收件箱",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "收件箱",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_mail_type",
                DictLabel = "发件箱",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "发件箱",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_mail_type",
                DictLabel = "草稿箱",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 27,
                ListClass = 27,
                Remark = "草稿箱",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_mail_type",
                DictLabel = "垃圾箱",
                DictValue = "4",
                OrderNum = 4,
                Status = 0,

                CssClass = 25,
                ListClass = 25,
                Remark = "垃圾箱",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 邮件优先级
            new HbtDictData
            {
                DictType = "sys_mail_priority",
                DictLabel = "普通",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "普通优先级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_mail_priority",
                DictLabel = "重要",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "重要优先级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_mail_priority",
                DictLabel = "紧急",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "紧急优先级",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 通知状态
            new HbtDictData
            {
                DictType = "sys_notify_status",
                DictLabel = "未读",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 30,
                ListClass = 30,
                Remark = "未读状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notify_status",
                DictLabel = "已读",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 31,
                ListClass = 31,
                Remark = "已读状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notify_status",
                DictLabel = "处理中",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,

                CssClass = 38,
                ListClass = 38,
                Remark = "处理中状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notify_status",
                DictLabel = "已处理",
                DictValue = "3",
                OrderNum = 4,
                Status = 0,

                CssClass = 39,
                ListClass = 39,
                Remark = "已处理状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 通知类型
            new HbtDictData
            {
                DictType = "sys_notify_type",
                DictLabel = "系统通知",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,

                CssClass = 35,
                ListClass = 35,
                Remark = "系统通知",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notify_type",
                DictLabel = "业务通知",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 36,
                ListClass = 36,
                Remark = "业务通知",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 通知级别
            new HbtDictData
            {
                DictType = "sys_notify_level",
                DictLabel = "普通",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 34,
                ListClass = 34,
                Remark = "普通级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notify_level",
                DictLabel = "重要",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 33,
                ListClass = 33,
                Remark = "重要级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_notify_level",
                DictLabel = "紧急",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,

                CssClass = 32,
                ListClass = 32,
                Remark = "紧急级别",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 任务类型
            new HbtDictData
            {
                DictType = "sys_task_type",
                DictLabel = "系统任务",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "系统任务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_type",
                DictLabel = "业务任务",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "业务任务",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 任务执行策略
            new HbtDictData
            {
                DictType = "sys_task_policy",
                DictLabel = "立即执行",
                DictValue = "1",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "立即执行",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_policy",
                DictLabel = "执行一次",
                DictValue = "2",
                OrderNum = 2,
                Status = 0,

                CssClass = 2,
                ListClass = 2,
                Remark = "执行一次",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_policy",
                DictLabel = "重复执行",
                DictValue = "3",
                OrderNum = 3,
                Status = 0,

                CssClass = 3,
                ListClass = 3,
                Remark = "重复执行",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 任务并发策略
            new HbtDictData
            {
                DictType = "sys_task_concurrent",
                DictLabel = "允许",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 1,
                ListClass = 1,
                Remark = "允许并发执行",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_concurrent",
                DictLabel = "禁止",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 5,
                ListClass = 5,
                Remark = "禁止并发执行",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 任务状态
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "正常",
                DictValue = "0",
                OrderNum = 1,
                Status = 0,

                CssClass = 50,
                ListClass = 50,
                Remark = "正常状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "暂停",
                DictValue = "1",
                OrderNum = 2,
                Status = 0,

                CssClass = 51,
                ListClass = 51,
                Remark = "暂停状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "运行中",
                DictValue = "2",
                OrderNum = 3,
                Status = 0,

                CssClass = 52,
                ListClass = 52,
                Remark = "运行中状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "错误",
                DictValue = "3",
                OrderNum = 4,
                Status = 0,

                CssClass = 53,
                ListClass = 53,
                Remark = "错误状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "阻塞",
                DictValue = "4",
                OrderNum = 5,
                Status = 0,

                CssClass = 54,
                ListClass = 54,
                Remark = "阻塞状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "过期",
                DictValue = "5",
                OrderNum = 6,
                Status = 0,

                CssClass = 55,
                ListClass = 55,
                Remark = "过期状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "超时",
                DictValue = "6",
                OrderNum = 7,
                Status = 0,

                CssClass = 56,
                ListClass = 56,
                Remark = "超时状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "等待中",
                DictValue = "7",
                OrderNum = 8,
                Status = 0,

                CssClass = 57,
                ListClass = 57,
                Remark = "等待中状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "已禁用",
                DictValue = "8",
                OrderNum = 9,
                Status = 0,

                CssClass = 58,
                ListClass = 58,
                Remark = "已禁用状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictType = "sys_task_status",
                DictLabel = "已删除",
                DictValue = "9",
                OrderNum = 10,
                Status = 0,

                CssClass = 59,
                ListClass = 59,
                Remark = "已删除状态",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

            // 翻译模块类型
            new HbtDictData
            {
                DictLabel = "前端",
                DictValue = "frontend",
                DictType = "sys_translation_module",
                OrderNum = 1,
                CssClass = 60,
                ListClass = 60,
                Status = 0,
                Remark = "前端模块",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtDictData
            {
                DictLabel = "后端",
                DictValue = "backend",
                DictType = "sys_translation_module",
                OrderNum = 2,
                CssClass = 61,
                ListClass = 61,
                Status = 0,
                Remark = "后端模块",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },

        };

    }
}