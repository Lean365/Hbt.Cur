//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedTranslation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 翻译数据初始化类
//===================================================================

using Lean.Hbt.Common.Enums;
using Lean.Hbt.Domain.Entities.Admin;
using Lean.Hbt.Domain.IServices;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 翻译数据初始化类
/// </summary>
public class HbtDbSeedTranslation
{
    private readonly IHbtRepository<HbtTranslation> _translationRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationRepository">翻译仓储</param>
    /// <param name="logger">日志记录器</param>
    public HbtDbSeedTranslation(IHbtRepository<HbtTranslation> translationRepository, IHbtLogger logger)
    {
        _translationRepository = translationRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化翻译数据
    /// </summary>
    public async Task<(int, int)> InitializeTranslationAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        var defaultTranslations = new List<HbtTranslation>
        {
          CreateTranslation("zh-CN", "Common.OperationSuccess", "操作成功", "Common"),
          CreateTranslation("zh-CN", "Common.OperationFailed", "操作失败", "Common"),
          CreateTranslation("zh-CN", "Common.SaveSuccess", "保存成功", "Common"),
          CreateTranslation("zh-CN", "Common.SaveFailed", "保存失败", "Common"),
          CreateTranslation("zh-CN", "Common.DeleteSuccess", "删除成功", "Common"),
          CreateTranslation("zh-CN", "Common.DeleteFailed", "删除失败", "Common"),
          CreateTranslation("zh-CN", "Common.UpdateSuccess", "更新成功", "Common"),
          CreateTranslation("zh-CN", "Common.UpdateFailed", "更新失败", "Common"),
          CreateTranslation("zh-CN", "Common.QuerySuccess", "查询成功", "Common"),
          CreateTranslation("zh-CN", "Common.QueryFailed", "查询失败", "Common"),
          CreateTranslation("zh-CN", "Common.SubmitSuccess", "提交成功", "Common"),
          CreateTranslation("zh-CN", "Common.SubmitFailed", "提交失败", "Common"),
          CreateTranslation("zh-CN", "Common.InvalidRequest", "无效的请求", "Common"),
          CreateTranslation("zh-CN", "Common.DataNotFound", "数据不存在", "Common"),
          CreateTranslation("zh-CN", "Common.DataExists", "数据已存在", "Common"),
          CreateTranslation("zh-CN", "Common.ServerError", "服务器错误", "Common"),
          CreateTranslation("en-US", "Common.OperationSuccess", "Operation successful", "Common"),
          CreateTranslation("en-US", "Common.OperationFailed", "Operation failed", "Common"),
          CreateTranslation("en-US", "Common.SaveSuccess", "Save successful", "Common"),
          CreateTranslation("en-US", "Common.SaveFailed", "Save failed", "Common"),
          CreateTranslation("en-US", "Common.DeleteSuccess", "Delete successful", "Common"),
          CreateTranslation("en-US", "Common.DeleteFailed", "Delete failed", "Common"),
          CreateTranslation("en-US", "Common.UpdateSuccess", "Update successful", "Common"),
          CreateTranslation("en-US", "Common.UpdateFailed", "Update failed", "Common"),
          CreateTranslation("en-US", "Common.QuerySuccess", "Query successful", "Common"),
          CreateTranslation("en-US", "Common.QueryFailed", "Query failed", "Common"),
          CreateTranslation("en-US", "Common.SubmitSuccess", "Submit successful", "Common"),
          CreateTranslation("en-US", "Common.SubmitFailed", "Submit failed", "Common"),
          CreateTranslation("en-US", "Common.InvalidRequest", "Invalid request", "Common"),
          CreateTranslation("en-US", "Common.DataNotFound", "Data not found", "Common"),
          CreateTranslation("en-US", "Common.DataExists", "Data already exists", "Common"),
          CreateTranslation("en-US", "Common.ServerError", "Server error", "Common"),
          CreateTranslation("zh-CN", "System.Login", "登录", "System"),
          CreateTranslation("zh-CN", "System.Logout", "退出登录", "System"),
          CreateTranslation("zh-CN", "System.Username", "用户名", "System"),
          CreateTranslation("zh-CN", "System.Password", "密码", "System"),
          CreateTranslation("zh-CN", "System.RememberMe", "记住我", "System"),
          CreateTranslation("zh-CN", "System.ForgotPassword", "忘记密码", "System"),
          CreateTranslation("zh-CN", "System.LoginFailed", "登录失败", "System"),
          CreateTranslation("zh-CN", "System.AccountLocked", "账号已锁定", "System"),
          CreateTranslation("zh-CN", "System.AccountDisabled", "账号已禁用", "System"),
          CreateTranslation("zh-CN", "System.PasswordExpired", "密码已过期", "System"),
          CreateTranslation("en-US", "System.Login", "Login", "System"),
          CreateTranslation("en-US", "System.Logout", "Logout", "System"),
          CreateTranslation("en-US", "System.Username", "Username", "System"),
          CreateTranslation("en-US", "System.Password", "Password", "System"),
          CreateTranslation("en-US", "System.RememberMe", "Remember me", "System"),
          CreateTranslation("en-US", "System.ForgotPassword", "Forgot password", "System"),
          CreateTranslation("en-US", "System.LoginFailed", "Login failed", "System"),
          CreateTranslation("en-US", "System.AccountLocked", "Account locked", "System"),
          CreateTranslation("en-US", "System.AccountDisabled", "Account disabled", "System"),
          CreateTranslation("en-US", "System.PasswordExpired", "Password expired", "System"),
          CreateTranslation("zh-CN", "User.UserManagement", "用户管理", "User"),
          CreateTranslation("zh-CN", "User.UserList", "用户列表", "User"),
          CreateTranslation("zh-CN", "User.AddUser", "添加用户", "User"),
          CreateTranslation("zh-CN", "User.EditUser", "编辑用户", "User"),
          CreateTranslation("zh-CN", "User.DeleteUser", "删除用户", "User"),
          CreateTranslation("zh-CN", "User.ResetPassword", "重置密码", "User"),
          CreateTranslation("zh-CN", "User.AssignRoles", "分配角色", "User"),
          CreateTranslation("zh-CN", "User.UserProfile", "用户资料", "User"),
          CreateTranslation("zh-CN", "User.ChangePassword", "修改密码", "User"),
          CreateTranslation("en-US", "User.UserManagement", "User Management", "User"),
          CreateTranslation("en-US", "User.UserList", "User List", "User"),
          CreateTranslation("en-US", "User.AddUser", "Add User", "User"),
          CreateTranslation("en-US", "User.EditUser", "Edit User", "User"),
          CreateTranslation("en-US", "User.DeleteUser", "Delete User", "User"),
          CreateTranslation("en-US", "User.ResetPassword", "Reset Password", "User"),
          CreateTranslation("en-US", "User.AssignRoles", "Assign Roles", "User"),
          CreateTranslation("en-US", "User.UserProfile", "User Profile", "User"),
          CreateTranslation("en-US", "User.ChangePassword", "Change Password", "User"),
          CreateTranslation("zh-CN", "Role.RoleManagement", "角色管理", "Role"),
          CreateTranslation("zh-CN", "Role.RoleList", "角色列表", "Role"),
          CreateTranslation("zh-CN", "Role.AddRole", "添加角色", "Role"),
          CreateTranslation("zh-CN", "Role.EditRole", "编辑角色", "Role"),
          CreateTranslation("zh-CN", "Role.DeleteRole", "删除角色", "Role"),
          CreateTranslation("zh-CN", "Role.AssignPermissions", "分配权限", "Role"),
          CreateTranslation("en-US", "Role.RoleManagement", "Role Management", "Role"),
          CreateTranslation("en-US", "Role.RoleList", "Role List", "Role"),
          CreateTranslation("en-US", "Role.AddRole", "Add Role", "Role"),
          CreateTranslation("en-US", "Role.EditRole", "Edit Role", "Role"),
          CreateTranslation("en-US", "Role.DeleteRole", "Delete Role", "Role"),
          CreateTranslation("en-US", "Role.AssignPermissions", "Assign Permissions", "Role"),
          CreateTranslation("zh-CN", "Post.Management", "岗位管理", "Post"),
          CreateTranslation("zh-CN", "Post.List", "岗位列表", "Post"),
          CreateTranslation("zh-CN", "Post.Code", "岗位编码", "Post"),
          CreateTranslation("zh-CN", "Post.Name", "岗位名称", "Post"),
          CreateTranslation("zh-CN", "Post.OrderNum", "显示顺序", "Post"),
          CreateTranslation("zh-CN", "Post.Status", "状态", "Post"),
          CreateTranslation("zh-CN", "Post.Remark", "备注", "Post"),
          CreateTranslation("zh-CN", "Post.AddSuccess", "添加岗位成功", "Post"),
          CreateTranslation("zh-CN", "Post.UpdateSuccess", "更新岗位成功", "Post"),
          CreateTranslation("zh-CN", "Post.DeleteSuccess", "删除岗位成功", "Post"),
          CreateTranslation("zh-CN", "Post.NotExists", "岗位不存在", "Post"),
          CreateTranslation("zh-CN", "Post.CodeExists", "岗位编码已存在", "Post"),
          CreateTranslation("zh-CN", "Post.NameExists", "岗位名称已存在", "Post"),
          CreateTranslation("en-US", "Post.Management", "Post Management", "Post"),
          CreateTranslation("en-US", "Post.List", "Post List", "Post"),
          CreateTranslation("en-US", "Post.Code", "Post Code", "Post"),
          CreateTranslation("en-US", "Post.Name", "Post Name", "Post"),
          CreateTranslation("en-US", "Post.OrderNum", "Display Order", "Post"),
          CreateTranslation("en-US", "Post.Status", "Status", "Post"),
          CreateTranslation("en-US", "Post.Remark", "Remark", "Post"),
          CreateTranslation("en-US", "Post.AddSuccess", "Add post successfully", "Post"),
          CreateTranslation("en-US", "Post.UpdateSuccess", "Update post successfully", "Post"),
          CreateTranslation("en-US", "Post.DeleteSuccess", "Delete post successfully", "Post"),
          CreateTranslation("en-US", "Post.NotExists", "Post does not exist", "Post"),
          CreateTranslation("en-US", "Post.CodeExists", "Post code already exists", "Post"),
          CreateTranslation("en-US", "Post.NameExists", "Post name already exists", "Post"),

          // 按钮通用翻译
          CreateTranslation("zh-CN", "button.query", "查询", "Button"),
          CreateTranslation("zh-CN", "button.add", "新增", "Button"),
          CreateTranslation("zh-CN", "button.edit", "修改", "Button"),
          CreateTranslation("zh-CN", "button.delete", "删除", "Button"),
          CreateTranslation("zh-CN", "button.preview", "预览", "Button"),
          CreateTranslation("zh-CN", "button.import", "导入", "Button"),
          CreateTranslation("zh-CN", "button.export", "导出", "Button"),
          CreateTranslation("en-US", "button.query", "Query", "Button"),
          CreateTranslation("en-US", "button.add", "Add", "Button"),
          CreateTranslation("en-US", "button.edit", "Edit", "Button"),
          CreateTranslation("en-US", "button.delete", "Delete", "Button"),
          CreateTranslation("en-US", "button.preview", "Preview", "Button"),
          CreateTranslation("en-US", "button.import", "Import", "Button"),
          CreateTranslation("en-US", "button.export", "Export", "Button"),

          // 菜单翻译
          CreateTranslation("zh-CN", "menu.admin.dicttype", "字典类型", "Menu"),
          CreateTranslation("zh-CN", "menu.admin.dictdata", "字典数据", "Menu"),
          CreateTranslation("zh-CN", "menu.identity.loginpolicy", "登录策略", "Menu"),
          CreateTranslation("zh-CN", "menu.identity.loginextend", "登录扩展", "Menu"),
          CreateTranslation("zh-CN", "menu.identity.deviceextend", "设备扩展", "Menu"),
          CreateTranslation("zh-CN", "menu.audit.operlog", "操作日志", "Menu"),
          CreateTranslation("zh-CN", "menu.audit.loginlog", "登录日志", "Menu"),
          CreateTranslation("zh-CN", "menu.audit.auditlog", "审计日志", "Menu"),
          CreateTranslation("zh-CN", "menu.audit.dbdifflog", "数据变更日志", "Menu"),
          CreateTranslation("zh-CN", "menu.audit.exceptionlog", "异常日志", "Menu"),
          CreateTranslation("en-US", "menu.admin.dicttype", "Dictionary Type", "Menu"),
          CreateTranslation("en-US", "menu.admin.dictdata", "Dictionary Data", "Menu"),
          CreateTranslation("en-US", "menu.identity.loginpolicy", "Login Policy", "Menu"),
          CreateTranslation("en-US", "menu.identity.loginextend", "Login Extension", "Menu"),
          CreateTranslation("en-US", "menu.identity.deviceextend", "Device Extension", "Menu"),
          CreateTranslation("en-US", "menu.audit.operlog", "Operation Log", "Menu"),
          CreateTranslation("en-US", "menu.audit.loginlog", "Login Log", "Menu"),
          CreateTranslation("en-US", "menu.audit.auditlog", "Audit Log", "Menu"),
          CreateTranslation("en-US", "menu.audit.dbdifflog", "Data Change Log", "Menu"),
          CreateTranslation("en-US", "menu.audit.exceptionlog", "Exception Log", "Menu"),
        };

        foreach (var translation in defaultTranslations)
        {
            var existingTranslation = await _translationRepository.FirstOrDefaultAsync(t =>
                t.LangCode == translation.LangCode &&
                t.TransKey == translation.TransKey);

            if (existingTranslation == null)
            {
                await _translationRepository.InsertAsync(translation);
                insertCount++;
                _logger.Info($"[创建] 翻译 '{translation.TransKey}' ({translation.LangCode}) 创建成功");
            }
            else
            {
                existingTranslation.LangCode = translation.LangCode;
                existingTranslation.TransKey = translation.TransKey;
                existingTranslation.TransValue = translation.TransValue;
                existingTranslation.ModuleName = translation.ModuleName;
                existingTranslation.Status = translation.Status;
                existingTranslation.TransBuiltin = translation.TransBuiltin;
                existingTranslation.TenantId = translation.TenantId;
                existingTranslation.Remark = translation.Remark;
                existingTranslation.CreateBy = translation.CreateBy;
                existingTranslation.CreateTime = translation.CreateTime;
                existingTranslation.UpdateBy = "system";
                existingTranslation.UpdateTime = DateTime.Now;

                await _translationRepository.UpdateAsync(existingTranslation);
                updateCount++;
                _logger.Info($"[更新] 翻译 '{existingTranslation.TransKey}' ({existingTranslation.LangCode}) 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    private HbtTranslation CreateTranslation(string langCode, string transKey, string transValue, string moduleName)
    {
        return new HbtTranslation
        {
            LangCode = langCode,
            TransKey = transKey,
            TransValue = transValue,
            ModuleName = moduleName,
            Status = HbtStatus.Normal,
            TransBuiltin = 1,
            TenantId = 0,
            CreateBy = "system",
            CreateTime = DateTime.Now,
            UpdateBy = "system",
            UpdateTime = DateTime.Now
        };
    }
}