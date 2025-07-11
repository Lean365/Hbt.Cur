//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTranslation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 翻译数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTranslation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 翻译数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTranslation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 翻译数据初始化类
//===================================================================


//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTranslation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 翻译数据初始化类
//===================================================================

using Lean.Hbt.Domain.Entities.Routine.Core;

namespace Lean.Hbt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 翻译数据提供类
/// </summary>
public class HbtDbSeedTranslation
{

    /// <summary>
    /// 获取基础翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<HbtTranslation> GetDefaultTranslations()
    {
        return new List<HbtTranslation>
        {
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.OperationSuccess", TransValue = "操作成功", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.OperationFailed", TransValue = "操作失败", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.SaveSuccess", TransValue = "保存成功", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.SaveFailed", TransValue = "保存失败", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.DeleteSuccess", TransValue = "删除成功", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.DeleteFailed", TransValue = "删除失败", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.UpdateSuccess", TransValue = "更新成功", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.UpdateFailed", TransValue = "更新失败", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.QuerySuccess", TransValue = "查询成功", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.QueryFailed", TransValue = "查询失败", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.SubmitSuccess", TransValue = "提交成功", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.SubmitFailed", TransValue = "提交失败", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.InvalidRequest", TransValue = "无效的请求", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.DataNotFound", TransValue = "数据不存在", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.DataExists", TransValue = "数据已存在", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Common.ServerError", TransValue = "服务器错误", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.OperationSuccess", TransValue = "Operation successful", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.OperationFailed", TransValue = "Operation failed", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.SaveSuccess", TransValue = "Save successful", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.SaveFailed", TransValue = "Save failed", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.DeleteSuccess", TransValue = "Delete successful", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.DeleteFailed", TransValue = "Delete failed", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.UpdateSuccess", TransValue = "Update successful", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.UpdateFailed", TransValue = "Update failed", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.QuerySuccess", TransValue = "Query successful", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.QueryFailed", TransValue = "Query failed", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.SubmitSuccess", TransValue = "Submit successful", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.SubmitFailed", TransValue = "Submit failed", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.InvalidRequest", TransValue = "Invalid request", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.DataNotFound", TransValue = "Data not found", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.DataExists", TransValue = "Data already exists", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Common.ServerError", TransValue = "Server error", ModuleName = "Common", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.Login", TransValue = "登录", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.Logout", TransValue = "退出登录", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.Username", TransValue = "用户名", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.Password", TransValue = "密码", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.RememberMe", TransValue = "记住我", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.ForgotPassword", TransValue = "忘记密码", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.LoginFailed", TransValue = "登录失败", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.AccountLocked", TransValue = "账号已锁定", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.AccountDisabled", TransValue = "账号已禁用", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "System.PasswordExpired", TransValue = "密码已过期", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.Login", TransValue = "Login", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.Logout", TransValue = "Logout", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.Username", TransValue = "Username", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.Password", TransValue = "Password", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.RememberMe", TransValue = "Remember me", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.ForgotPassword", TransValue = "Forgot password", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.LoginFailed", TransValue = "Login failed", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.AccountLocked", TransValue = "Account locked", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.AccountDisabled", TransValue = "Account disabled", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "System.PasswordExpired", TransValue = "Password expired", ModuleName = "System", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.UserManagement", TransValue = "用户管理", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.UserList", TransValue = "用户列表", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.AddUser", TransValue = "添加用户", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.EditUser", TransValue = "编辑用户", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.DeleteUser", TransValue = "删除用户", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.ResetPassword", TransValue = "重置密码", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.AssignRoles", TransValue = "分配角色", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.UserProfile", TransValue = "用户资料", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "User.ChangePassword", TransValue = "修改密码", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.UserManagement", TransValue = "User Management", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.UserList", TransValue = "User List", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.AddUser", TransValue = "Add User", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.EditUser", TransValue = "Edit User", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.DeleteUser", TransValue = "Delete User", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.ResetPassword", TransValue = "Reset Password", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.AssignRoles", TransValue = "Assign Roles", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.UserProfile", TransValue = "User Profile", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "User.ChangePassword", TransValue = "Change Password", ModuleName = "User", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Role.RoleManagement", TransValue = "角色管理", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Role.RoleList", TransValue = "角色列表", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Role.AddRole", TransValue = "添加角色", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Role.EditRole", TransValue = "编辑角色", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Role.DeleteRole", TransValue = "删除角色", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Role.AssignPermissions", TransValue = "分配权限", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Role.RoleManagement", TransValue = "Role Management", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Role.RoleList", TransValue = "Role List", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Role.AddRole", TransValue = "Add Role", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Role.EditRole", TransValue = "Edit Role", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Role.DeleteRole", TransValue = "Delete Role", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Role.AssignPermissions", TransValue = "Assign Permissions", ModuleName = "Role", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Management", TransValue = "岗位管理", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.List", TransValue = "岗位列表", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Code", TransValue = "岗位编码", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Name", TransValue = "岗位名称", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.OrderNum", TransValue = "显示顺序", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Status", TransValue = "状态", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.Remark", TransValue = "备注", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.AddSuccess", TransValue = "添加岗位成功", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.UpdateSuccess", TransValue = "更新岗位成功", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.DeleteSuccess", TransValue = "删除岗位成功", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.NotExists", TransValue = "岗位不存在", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.CodeExists", TransValue = "岗位编码已存在", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.NameExists", TransValue = "岗位名称已存在", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Management", TransValue = "Post Management", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "Post.List", TransValue = "Post List", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Code", TransValue = "Post Code", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Name", TransValue = "Post Name", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.OrderNum", TransValue = "Display Order", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Status", TransValue = "Status", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.Remark", TransValue = "Remark", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.AddSuccess", TransValue = "Add post successfully", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.UpdateSuccess", TransValue = "Update post successfully", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.DeleteSuccess", TransValue = "Delete post successfully", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.NotExists", TransValue = "Post does not exist", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.CodeExists", TransValue = "Post code already exists", ModuleName = "Post", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "Post.NameExists", TransValue = "Post name already exists", ModuleName = "Post", Status = 0 },

            // 按钮通用翻译
            new HbtTranslation { LangCode = "zh-CN", TransKey = "button.query", TransValue = "查询", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "button.add", TransValue = "新增", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "button.edit", TransValue = "修改", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "button.delete", TransValue = "删除", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "button.preview", TransValue = "预览", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "button.import", TransValue = "导入", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "button.export", TransValue = "导出", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "button.query", TransValue = "Query", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "button.add", TransValue = "Add", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "button.edit", TransValue = "Edit", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "button.delete", TransValue = "Delete", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "button.preview", TransValue = "Preview", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "button.import", TransValue = "Import", ModuleName = "Button", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "button.export", TransValue = "Export", ModuleName = "Button", Status = 0 },

            // 菜单翻译
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.admin.dicttype", TransValue = "字典类型", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.admin.dictdata", TransValue = "字典数据", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.identity.loginpolicy", TransValue = "登录策略", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.identity.loginextend", TransValue = "登录环境日志", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.identity.deviceextend", TransValue = "登录设备", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.audit.operlog", TransValue = "操作日志", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.audit.loginlog", TransValue = "登录日志", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.audit.auditlog", TransValue = "审计日志", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.audit.dbdifflog", TransValue = "数据变更日志", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.audit.exceptionlog", TransValue = "异常日志", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.admin.dicttype", TransValue = "Dictionary Type", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "zh-CN", TransKey = "menu.admin.dictdata", TransValue = "Dictionary Data", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.identity.loginpolicy", TransValue = "Login Policy", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.identity.loginextend", TransValue = "Login Extension", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.identity.deviceextend", TransValue = "Device Extension", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.audit.operlog", TransValue = "Operation Log", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.audit.loginlog", TransValue = "Login Log", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.audit.auditlog", TransValue = "Audit Log", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.audit.dbdifflog", TransValue = "Data Change Log", ModuleName = "Menu", Status = 0 },
            new HbtTranslation { LangCode = "en-US", TransKey = "menu.audit.exceptionlog", TransValue = "Exception Log", ModuleName = "Menu", Status = 0 }
        };

    }
}