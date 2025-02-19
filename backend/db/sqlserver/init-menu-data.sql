--===================================================================
-- 项目名 : Lean.Hbt
-- 文件名 : init-menu-data.sql 
-- 创建者 : Claude
-- 创建时间: 2024-02-19
-- 版本号 : V0.0.1
-- 描述   : 菜单表初始化数据
--===================================================================

-- 清空现有菜单数据
DELETE FROM hbt_id_menu;

-- 重置自增ID
DBCC CHECKIDENT ('hbt_id_menu', RESEED, 0);

-- 插入顶级菜单
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num, 
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES 
-- 系统管理
('系统管理', 'menu.admin', NULL, 1,
 '/admin', '', NULL, 0,
 0, 0, 0, 0,
 '', 'SettingOutlined', 0, '系统管理目录',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

-- 身份认证 
('身份认证', 'menu.identity', NULL, 2,
 '/identity', '', NULL, 0,
 0, 0, 0, 0,
 '', 'UserOutlined', 0, '身份认证目录',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

-- 审计日志
('审计日志', 'menu.audit', NULL, 3,
 '/audit', '', NULL, 0,
 0, 0, 0, 0,
 '', 'AuditOutlined', 0, '审计日志目录',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

-- 工作流程
('工作流程', 'menu.workflow', NULL, 4,
 '/workflow', '', NULL, 0,
 0, 0, 0, 0,
 '', 'DeploymentUnitOutlined', 0, '工作流程目录',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

-- 实时监控
('实时监控', 'menu.realtime', NULL, 5,
 '/realtime', '', NULL, 0,
 0, 0, 0, 0,
 '', 'DashboardOutlined', 0, '实时监控目录',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

-- 安全管理
('安全管理', 'menu.security', NULL, 6,
 '/security', '', NULL, 0,
 0, 0, 0, 0,
 '', 'SafetyCertificateOutlined', 0, '安全管理目录',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 声明变量存储父菜单ID
DECLARE @AdminMenuId bigint;
DECLARE @IdentityMenuId bigint;
DECLARE @AuditMenuId bigint;
DECLARE @WorkflowMenuId bigint;
DECLARE @RealtimeMenuId bigint;
DECLARE @SecurityMenuId bigint;

-- 获取父菜单ID
SELECT @AdminMenuId = id FROM hbt_id_menu WHERE menu_name = '系统管理';
SELECT @IdentityMenuId = id FROM hbt_id_menu WHERE menu_name = '身份认证';
SELECT @AuditMenuId = id FROM hbt_id_menu WHERE menu_name = '审计日志';
SELECT @WorkflowMenuId = id FROM hbt_id_menu WHERE menu_name = '工作流程';
SELECT @RealtimeMenuId = id FROM hbt_id_menu WHERE menu_name = '实时监控';
SELECT @SecurityMenuId = id FROM hbt_id_menu WHERE menu_name = '安全管理';

-- 插入系统管理子菜单
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('系统配置', 'menu.admin.config', @AdminMenuId, 1,
 'config', 'admin/config/index', NULL, 0,
 0, 1, 0, 0,
 'admin:config:list', 'ToolOutlined', 0, '系统配置菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('语言管理', 'menu.admin.language', @AdminMenuId, 2,
 'language', 'admin/language/index', NULL, 0,
 0, 1, 0, 0,
 'admin:language:list', 'TranslationOutlined', 0, '语言管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('字典管理', 'menu.admin.dict', @AdminMenuId, 3,
 'dict', 'admin/dict/index', NULL, 0,
 0, 1, 0, 0,
 'admin:dict:list', 'OrderedListOutlined', 0, '字典管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('翻译管理', 'menu.admin.translation', @AdminMenuId, 4,
 'translation', 'admin/translation/index', NULL, 0,
 0, 1, 0, 0,
 'admin:translation:list', 'GlobalOutlined', 0, '翻译管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 插入身份认证子菜单
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('租户管理', 'menu.identity.tenant', @IdentityMenuId, 1,
 'tenant', 'identity/tenant/index', NULL, 0,
 0, 1, 0, 0,
 'identity:tenant:list', 'TeamOutlined', 0, '租户管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('菜单管理', 'menu.identity.menu', @IdentityMenuId, 2,
 'menu', 'identity/menu/index', NULL, 0,
 0, 1, 0, 0,
 'identity:menu:list', 'MenuOutlined', 0, '菜单管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('部门管理', 'menu.identity.dept', @IdentityMenuId, 3,
 'dept', 'identity/dept/index', NULL, 0,
 0, 1, 0, 0,
 'identity:dept:list', 'ApartmentOutlined', 0, '部门管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('岗位管理', 'menu.identity.post', @IdentityMenuId, 4,
 'post', 'identity/post/index', NULL, 0,
 0, 1, 0, 0,
 'identity:post:list', 'IdcardOutlined', 0, '岗位管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('角色管理', 'menu.identity.role', @IdentityMenuId, 5,
 'role', 'identity/role/index', NULL, 0,
 0, 1, 0, 0,
 'identity:role:list', 'UserSwitchOutlined', 0, '角色管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('用户管理', 'menu.identity.user', @IdentityMenuId, 6,
 'user', 'identity/user/index', NULL, 0,
 0, 1, 0, 0,
 'identity:user:list', 'UsergroupAddOutlined', 0, '用户管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 插入审计日志子菜单
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('服务器监控', 'menu.audit.server', @AuditMenuId, 1,
 'server', 'audit/server/index', NULL, 0,
 0, 1, 0, 0,
 'audit:server:list', 'DesktopOutlined', 0, '服务器监控菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('登录日志', 'menu.audit.loginlog', @AuditMenuId, 2,
 'loginlog', 'audit/loginlog/index', NULL, 0,
 0, 1, 0, 0,
 'audit:loginlog:list', 'LoginOutlined', 0, '登录日志菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('操作日志', 'menu.audit.operlog', @AuditMenuId, 3,
 'operlog', 'audit/operlog/index', NULL, 0,
 0, 1, 0, 0,
 'audit:operlog:list', 'HistoryOutlined', 0, '操作日志菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 插入工作流程子菜单
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('流程定义', 'menu.workflow.definition', @WorkflowMenuId, 1,
 'definition', 'workflow/definition/index', NULL, 0,
 0, 1, 0, 0,
 'workflow:definition:list', 'ProfileOutlined', 0, '流程定义菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('流程实例', 'menu.workflow.instance', @WorkflowMenuId, 2,
 'instance', 'workflow/instance/index', NULL, 0,
 0, 1, 0, 0,
 'workflow:instance:list', 'ApartmentOutlined', 0, '流程实例菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('任务管理', 'menu.workflow.task', @WorkflowMenuId, 3,
 'task', 'workflow/task/index', NULL, 0,
 0, 1, 0, 0,
 'workflow:task:list', 'CarryOutOutlined', 0, '任务管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 插入实时监控子菜单
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('在线用户', 'menu.realtime.online', @RealtimeMenuId, 1,
 'online', 'realtime/online/index', NULL, 0,
 0, 1, 0, 0,
 'realtime:online:list', 'TeamOutlined', 0, '在线用户菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('消息管理', 'menu.realtime.message', @RealtimeMenuId, 2,
 'message', 'realtime/message/index', NULL, 0,
 0, 1, 0, 0,
 'realtime:message:list', 'MessageOutlined', 0, '消息管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 插入安全管理子菜单
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('验证码', 'menu.security.captcha', @SecurityMenuId, 1,
 'captcha', 'security/captcha/index', NULL, 0,
 0, 1, 0, 0,
 'security:captcha:list', 'SafetyCertificateOutlined', 0, '验证码菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 插入系统配置按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('查询', 'menu.admin.config.query', @AdminMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:query', NULL, 0, '系统配置查询按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('新增', 'menu.admin.config.add', @AdminMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:add', NULL, 0, '系统配置新增按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('修改', 'menu.admin.config.edit', @AdminMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:edit', NULL, 0, '系统配置修改按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('删除', 'menu.admin.config.delete', @AdminMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:delete', NULL, 0, '系统配置删除按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('导出', 'menu.admin.config.export', @AdminMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:export', NULL, 0, '系统配置导出按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 插入用户管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('查询', 'menu.identity.user.query', @IdentityMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:user:query', NULL, 0, '用户管理查询按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('新增', 'menu.identity.user.add', @IdentityMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:user:add', NULL, 0, '用户管理新增按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('修改', 'menu.identity.user.edit', @IdentityMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:user:edit', NULL, 0, '用户管理修改按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('删除', 'menu.identity.user.delete', @IdentityMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:user:delete', NULL, 0, '用户管理删除按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('导出', 'menu.identity.user.export', @IdentityMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:user:export', NULL, 0, '用户管理导出按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('重置密码', 'menu.identity.user.resetPwd', @IdentityMenuId, 6,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:user:resetPwd', NULL, 0, '重置密码按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 插入角色管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('查询', 'menu.identity.role.query', @IdentityMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:role:query', NULL, 0, '角色管理查询按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('新增', 'menu.identity.role.add', @IdentityMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:role:add', NULL, 0, '角色管理新增按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('修改', 'menu.identity.role.edit', @IdentityMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:role:edit', NULL, 0, '角色管理修改按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('删除', 'menu.identity.role.delete', @IdentityMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:role:delete', NULL, 0, '角色管理删除按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
 GETDATE(), GETDATE()),
('导出', 'menu.identity.role.export', @IdentityMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:role:export', NULL, 0, '角色管理导出按钮',
 GETDATE(), GETDATE());

-- 插入菜单管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.identity.menu.query', @IdentityMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:menu:query', NULL, 0, '菜单管理查询按钮',
 GETDATE(), GETDATE()),
('新增', 'menu.identity.menu.add', @IdentityMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:menu:add', NULL, 0, '菜单管理新增按钮',
 GETDATE(), GETDATE()),
('修改', 'menu.identity.menu.edit', @IdentityMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:menu:edit', NULL, 0, '菜单管理修改按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.identity.menu.delete', @IdentityMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:menu:delete', NULL, 0, '菜单管理删除按钮',
 GETDATE(), GETDATE());

-- 插入部门管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.identity.dept.query', @IdentityMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:dept:query', NULL, 0, '部门管理查询按钮',
 GETDATE(), GETDATE()),
('新增', 'menu.identity.dept.add', @IdentityMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:dept:add', NULL, 0, '部门管理新增按钮',
 GETDATE(), GETDATE()),
('修改', 'menu.identity.dept.edit', @IdentityMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:dept:edit', NULL, 0, '部门管理修改按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.identity.dept.delete', @IdentityMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:dept:delete', NULL, 0, '部门管理删除按钮',
 GETDATE(), GETDATE());

-- 插入岗位管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.identity.post.query', @IdentityMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:post:query', NULL, 0, '岗位管理查询按钮',
 GETDATE(), GETDATE()),
('新增', 'menu.identity.post.add', @IdentityMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:post:add', NULL, 0, '岗位管理新增按钮',
 GETDATE(), GETDATE()),
('修改', 'menu.identity.post.edit', @IdentityMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:post:edit', NULL, 0, '岗位管理修改按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.identity.post.delete', @IdentityMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:post:delete', NULL, 0, '岗位管理删除按钮',
 GETDATE(), GETDATE()),
('导出', 'menu.identity.post.export', @IdentityMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:post:export', NULL, 0, '岗位管理导出按钮',
 GETDATE(), GETDATE());

-- 插入字典管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.admin.dict.query', @AdminMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:dict:query', NULL, 0, '字典管理查询按钮',
 GETDATE(), GETDATE()),
('新增', 'menu.admin.dict.add', @AdminMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:dict:add', NULL, 0, '字典管理新增按钮',
 GETDATE(), GETDATE()),
('修改', 'menu.admin.dict.edit', @AdminMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:dict:edit', NULL, 0, '字典管理修改按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.admin.dict.delete', @AdminMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:dict:delete', NULL, 0, '字典管理删除按钮',
 GETDATE(), GETDATE()),
('导出', 'menu.admin.dict.export', @AdminMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:dict:export', NULL, 0, '字典管理导出按钮',
 GETDATE(), GETDATE());

-- 插入语言管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.admin.language.query', @AdminMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:language:query', NULL, 0, '语言管理查询按钮',
 GETDATE(), GETDATE()),
('新增', 'menu.admin.language.add', @AdminMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:language:add', NULL, 0, '语言管理新增按钮',
 GETDATE(), GETDATE()),
('修改', 'menu.admin.language.edit', @AdminMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:language:edit', NULL, 0, '语言管理修改按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.admin.language.delete', @AdminMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:language:delete', NULL, 0, '语言管理删除按钮',
 GETDATE(), GETDATE()),
('导出', 'menu.admin.language.export', @AdminMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:language:export', NULL, 0, '语言管理导出按钮',
 GETDATE(), GETDATE());

-- 插入租户管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.identity.tenant.query', @IdentityMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:tenant:query', NULL, 0, '租户管理查询按钮',
 GETDATE(), GETDATE()),
('新增', 'menu.identity.tenant.add', @IdentityMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:tenant:add', NULL, 0, '租户管理新增按钮',
 GETDATE(), GETDATE()),
('修改', 'menu.identity.tenant.edit', @IdentityMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:tenant:edit', NULL, 0, '租户管理修改按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.identity.tenant.delete', @IdentityMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:tenant:delete', NULL, 0, '租户管理删除按钮',
 GETDATE(), GETDATE()),
('导出', 'menu.identity.tenant.export', @IdentityMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'identity:tenant:export', NULL, 0, '租户管理导出按钮',
 GETDATE(), GETDATE());

-- 插入操作日志按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.audit.operlog.query', @AuditMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:operlog:query', NULL, 0, '操作日志查询按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.audit.operlog.delete', @AuditMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:operlog:delete', NULL, 0, '操作日志删除按钮',
 GETDATE(), GETDATE()),
('清空', 'menu.audit.operlog.clear', @AuditMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:operlog:clear', NULL, 0, '操作日志清空按钮',
 GETDATE(), GETDATE()),
('导出', 'menu.audit.operlog.export', @AuditMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:operlog:export', NULL, 0, '操作日志导出按钮',
 GETDATE(), GETDATE());

-- 插入登录日志按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.audit.loginlog.query', @AuditMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:loginlog:query', NULL, 0, '登录日志查询按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.audit.loginlog.delete', @AuditMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:loginlog:delete', NULL, 0, '登录日志删除按钮',
 GETDATE(), GETDATE()),
('清空', 'menu.audit.loginlog.clear', @AuditMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:loginlog:clear', NULL, 0, '登录日志清空按钮',
 GETDATE(), GETDATE()),
('导出', 'menu.audit.loginlog.export', @AuditMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:loginlog:export', NULL, 0, '登录日志导出按钮',
 GETDATE(), GETDATE());

-- 插入在线用户按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.realtime.online.query', @RealtimeMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'realtime:online:query', NULL, 0, '在线用户查询按钮',
 GETDATE(), GETDATE()),
('强退', 'menu.realtime.online.forceLogout', @RealtimeMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'realtime:online:forceLogout', NULL, 0, '在线用户强退按钮',
 GETDATE(), GETDATE());

-- 插入服务器监控按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.audit.server.query', @AuditMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'audit:server:query', NULL, 0, '服务器监控查询按钮',
 GETDATE(), GETDATE());

-- 插入消息管理按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
('查询', 'menu.realtime.message.query', @RealtimeMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'realtime:message:query', NULL, 0, '消息管理查询按钮',
 GETDATE(), GETDATE()),
('新增', 'menu.realtime.message.add', @RealtimeMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'realtime:message:add', NULL, 0, '消息管理新增按钮',
 GETDATE(), GETDATE()),
('修改', 'menu.realtime.message.edit', @RealtimeMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'realtime:message:edit', NULL, 0, '消息管理修改按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.realtime.message.delete', @RealtimeMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'realtime:message:delete', NULL, 0, '消息管理删除按钮',
 GETDATE(), GETDATE());

-- 插入工作流程按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_time, update_time
) VALUES
-- 流程定义按钮
('查询', 'menu.workflow.definition.query', @WorkflowMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:definition:query', NULL, 0, '流程定义查询按钮',
 GETDATE(), GETDATE()),
('新增', 'menu.workflow.definition.add', @WorkflowMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:definition:add', NULL, 0, '流程定义新增按钮',
 GETDATE(), GETDATE()),
('修改', 'menu.workflow.definition.edit', @WorkflowMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:definition:edit', NULL, 0, '流程定义修改按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.workflow.definition.delete', @WorkflowMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:definition:delete', NULL, 0, '流程定义删除按钮',
 GETDATE(), GETDATE()),
('导出', 'menu.workflow.definition.export', @WorkflowMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:definition:export', NULL, 0, '流程定义导出按钮',
 GETDATE(), GETDATE()),

-- 流程实例按钮
('查询', 'menu.workflow.instance.query', @WorkflowMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:instance:query', NULL, 0, '流程实例查询按钮',
 GETDATE(), GETDATE()),
('启动', 'menu.workflow.instance.start', @WorkflowMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:instance:start', NULL, 0, '流程实例启动按钮',
 GETDATE(), GETDATE()),
('挂起', 'menu.workflow.instance.suspend', @WorkflowMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:instance:suspend', NULL, 0, '流程实例挂起按钮',
 GETDATE(), GETDATE()),
('激活', 'menu.workflow.instance.activate', @WorkflowMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:instance:activate', NULL, 0, '流程实例激活按钮',
 GETDATE(), GETDATE()),
('终止', 'menu.workflow.instance.terminate', @WorkflowMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:instance:terminate', NULL, 0, '流程实例终止按钮',
 GETDATE(), GETDATE()),
('删除', 'menu.workflow.instance.delete', @WorkflowMenuId, 6,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:instance:delete', NULL, 0, '流程实例删除按钮',
 GETDATE(), GETDATE()),

-- 任务管理按钮
('查询', 'menu.workflow.task.query', @WorkflowMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:task:query', NULL, 0, '任务管理查询按钮',
 GETDATE(), GETDATE()),
('审批', 'menu.workflow.task.approve', @WorkflowMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:task:approve', NULL, 0, '任务管理审批按钮',
 GETDATE(), GETDATE()),
('驳回', 'menu.workflow.task.reject', @WorkflowMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:task:reject', NULL, 0, '任务管理驳回按钮',
 GETDATE(), GETDATE()),
('转办', 'menu.workflow.task.transfer', @WorkflowMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:task:transfer', NULL, 0, '任务管理转办按钮',
 GETDATE(), GETDATE()),
('委派', 'menu.workflow.task.delegate', @WorkflowMenuId, 5,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:task:delegate', NULL, 0, '任务管理委派按钮',
 GETDATE(), GETDATE()),
('退回', 'menu.workflow.task.return', @WorkflowMenuId, 6,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'workflow:task:return', NULL, 0, '任务管理退回按钮',
 GETDATE(), GETDATE()); 