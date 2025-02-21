-- ===================================================================
-- 项目名 : Lean.Hbt
-- 文件名 : init-seed-data.sql
-- 创建者 : Claude
-- 创建时间: 2024-02-19
-- 版本号 : V0.0.1
-- 描述   : 数据库种子数据初始化脚本
-- ===================================================================

-- 1. 初始化租户数据
PRINT N'开始初始化租户数据...'
GO

-- 检查是否存在租户数据
IF NOT EXISTS (SELECT 1 FROM hbt_id_tenant)
BEGIN
    INSERT INTO hbt_id_tenant (
        tenant_name, tenant_code, contact_person, contact_phone, contact_email, 
        address, domain, logo_url, db_connection, theme, 
        license_start_time, license_end_time, max_user_count, status, 
        create_time, create_by, update_time, update_by,
        delete_time, delete_by, is_deleted
    ) VALUES (
        N'默认租户', 'default', N'管理员', '13800138000', 'admin@lean365.com',
        N'默认地址', 'localhost', '/logo.png',
        'Server=localhost;Database=LeanHbt_Dev;Trusted_Connection=True;MultipleActiveResultSets=true',
        'default',
        GETDATE(), DATEADD(YEAR, 1, GETDATE()), 100, 1,
        GETDATE(), 'system', GETDATE(), 'system',
        NULL, NULL, 0
    );
    PRINT N'租户数据初始化完成。'
END
ELSE
BEGIN
    UPDATE hbt_id_tenant SET
        tenant_name = N'默认租户',
        tenant_code = 'default',
        contact_person = N'管理员',
        contact_phone = '13800138000',
        contact_email = 'admin@lean365.com',
        address = N'默认地址',
        domain = 'localhost',
        logo_url = '/logo.png',
        db_connection = 'Server=localhost;Database=LeanHbt_Dev;Trusted_Connection=True;MultipleActiveResultSets=true',
        theme = 'default',
        license_start_time = GETDATE(),
        license_end_time = DATEADD(YEAR, 1, GETDATE()),
        max_user_count = 100,
        status = 1,
        update_time = GETDATE(),
        update_by = 'system',
        is_deleted = 0
    WHERE tenant_code = 'default';
    PRINT N'租户数据更新完成。'
END
GO

-- 2. 初始化角色数据
PRINT N'开始初始化角色数据...'
GO

-- 检查是否存在角色数据
IF NOT EXISTS (SELECT 1 FROM hbt_id_role WHERE role_key = 'admin')
BEGIN
    INSERT INTO hbt_id_role (
        role_name, role_key, status, tenant_id,
        create_time, create_by, update_time, update_by,
        delete_time, delete_by, is_deleted
    ) VALUES (
        N'超级管理员', 'admin', 1, 0,
        GETDATE(), 'system', GETDATE(), 'system',
        NULL, NULL, 0
    );
    PRINT N'角色数据初始化完成。'
END
ELSE
BEGIN
    UPDATE hbt_id_role SET
        role_name = N'超级管理员',
        status = 1,
        tenant_id = 0,
        update_time = GETDATE(),
        update_by = 'system',
        is_deleted = 0
    WHERE role_key = 'admin';
    PRINT N'角色数据更新完成。'
END
GO

-- 3. 初始化用户数据
PRINT N'开始初始化用户数据...'
GO

-- 注意：密码需要通过代码生成哈希值，这里使用占位符
-- 默认密码为：123456
DECLARE @PasswordHash NVARCHAR(MAX) = ''; -- 需要通过代码设置正确的哈希值
DECLARE @Salt NVARCHAR(MAX) = '';        -- 需要通过代码设置正确的盐值

-- 检查是否存在管理员用户
IF NOT EXISTS (SELECT 1 FROM hbt_id_user WHERE user_name = 'admin')
BEGIN
    INSERT INTO hbt_id_user (
        user_name, nick_name, english_name, user_type, password, salt,
        email, phone_number, gender, avatar, status, tenant_id,
        last_password_change_time, create_time, create_by, update_time, update_by,
        delete_time, delete_by, is_deleted
    ) VALUES (
        'admin', N'超级管理员', 'Administrator', 1, @PasswordHash, @Salt,
        'admin@lean365.com', '13800138000', 0, '/avatar/default.png', 1, 0,
        GETDATE(), GETDATE(), 'system', GETDATE(), 'system',
        NULL, NULL, 0
    );
    PRINT N'用户数据初始化完成。'
END
ELSE
BEGIN
    UPDATE hbt_id_user SET
        nick_name = N'超级管理员',
        english_name = 'Administrator',
        user_type = 1,
        email = 'admin@lean365.com',
        phone_number = '13800138000',
        status = 1,
        tenant_id = 0,
        update_time = GETDATE(),
        update_by = 'system',
        is_deleted = 0
    WHERE user_name = 'admin';
    PRINT N'用户数据更新完成。'
END
GO

-- 4. 初始化部门数据
PRINT N'开始初始化部门数据...'
GO

-- 检查是否存在总公司部门
IF NOT EXISTS (SELECT 1 FROM hbt_id_dept WHERE dept_name = N'总公司')
BEGIN
    INSERT INTO hbt_id_dept (
        dept_name, parent_id, order_num, leader, phone, email, 
        status, tenant_id, create_time, create_by, update_time, update_by,
        delete_time, delete_by, is_deleted
    ) VALUES (
        N'总公司', 0, 1, N'管理员', '13800138000', 'admin@lean365.com',
        1, 0, GETDATE(), 'system', GETDATE(), 'system',
        NULL, NULL, 0
    );
    PRINT N'部门数据初始化完成。'
END
ELSE
BEGIN
    UPDATE hbt_id_dept SET
        parent_id = 0,
        order_num = 1,
        leader = N'管理员',
        phone = '13800138000',
        email = 'admin@lean365.com',
        status = 1,
        tenant_id = 0,
        update_time = GETDATE(),
        update_by = 'system',
        is_deleted = 0
    WHERE dept_name = N'总公司';
    PRINT N'部门数据更新完成。'
END
GO

-- 5. 初始化岗位数据
PRINT N'开始初始化岗位数据...'
GO

-- 检查并更新或插入总经理岗位
IF NOT EXISTS (SELECT 1 FROM hbt_id_post WHERE post_code = 'GM')
BEGIN
    INSERT INTO hbt_id_post (
        post_name, post_code, order_num, status, tenant_id,
        create_time, create_by, update_time, update_by,
        delete_time, delete_by, is_deleted
    ) VALUES (
        N'总经理', 'GM', 1, 1, 0,
        GETDATE(), 'system', GETDATE(), 'system',
        NULL, NULL, 0
    );
END
ELSE
BEGIN
    UPDATE hbt_id_post SET
        post_name = N'总经理',
        order_num = 1,
        status = 1,
        tenant_id = 0,
        update_time = GETDATE(),
        update_by = 'system',
        is_deleted = 0
    WHERE post_code = 'GM';
END

-- 检查并更新或插入项目经理岗位
IF NOT EXISTS (SELECT 1 FROM hbt_id_post WHERE post_code = 'PM')
BEGIN
    INSERT INTO hbt_id_post (
        post_name, post_code, order_num, status, tenant_id,
        create_time, create_by, update_time, update_by,
        delete_time, delete_by, is_deleted
    ) VALUES (
        N'项目经理', 'PM', 2, 1, 0,
        GETDATE(), 'system', GETDATE(), 'system',
        NULL, NULL, 0
    );
END
ELSE
BEGIN
    UPDATE hbt_id_post SET
        post_name = N'项目经理',
        order_num = 2,
        status = 1,
        tenant_id = 0,
        update_time = GETDATE(),
        update_by = 'system',
        is_deleted = 0
    WHERE post_code = 'PM';
END

-- 检查并更新或插入开发工程师岗位
IF NOT EXISTS (SELECT 1 FROM hbt_id_post WHERE post_code = 'DEV')
BEGIN
    INSERT INTO hbt_id_post (
        post_name, post_code, order_num, status, tenant_id,
        create_time, create_by, update_time, update_by,
        delete_time, delete_by, is_deleted
    ) VALUES (
        N'开发工程师', 'DEV', 3, 1, 0,
        GETDATE(), 'system', GETDATE(), 'system',
        NULL, NULL, 0
    );
END
ELSE
BEGIN
    UPDATE hbt_id_post SET
        post_name = N'开发工程师',
        order_num = 3,
        status = 1,
        tenant_id = 0,
        update_time = GETDATE(),
        update_by = 'system',
        is_deleted = 0
    WHERE post_code = 'DEV';
END

PRINT N'岗位数据初始化完成。'
GO

-- 6. 初始化系统配置数据
PRINT N'开始初始化系统配置数据...'
GO

-- 创建临时表存储配置数据
CREATE TABLE #TempSysConfig (
    config_name NVARCHAR(100),
    config_key NVARCHAR(100),
    config_value NVARCHAR(500),
    config_type INT,
    order_num INT,
    status INT,
    tenant_id INT,
    remark NVARCHAR(500)
)

-- 插入缓存配置数据
INSERT INTO #TempSysConfig VALUES
(N'缓存提供程序', 'Cache:Provider', 'Memory', 1, 50, 1, 0, N'缓存提供程序类型(Memory/Redis)'),
(N'默认过期时间(分钟)', 'Cache:DefaultExpirationMinutes', '30', 1, 51, 1, 0, N'缓存默认过期时间(分钟)'),
(N'启用滑动过期', 'Cache:EnableSlidingExpiration', 'true', 1, 52, 1, 0, N'是否启用滑动过期'),
(N'启用多级缓存', 'Cache:EnableMultiLevelCache', 'false', 1, 53, 1, 0, N'是否启用多级缓存'),
(N'内存缓存大小限制', 'Cache:Memory:SizeLimit', '104857600', 1, 54, 1, 0, N'内存缓存大小限制(字节)'),
(N'内存缓存压缩阈值', 'Cache:Memory:CompactionThreshold', '1048576', 1, 55, 1, 0, N'内存缓存压缩阈值(字节)'),
(N'过期扫描频率', 'Cache:Memory:ExpirationScanFrequency', '60', 1, 56, 1, 0, N'过期扫描频率(秒)'),
(N'Redis实例名称', 'Cache:Redis:InstanceName', 'Lean.Hbt', 1, 57, 1, 0, N'Redis实例名称'),
(N'Redis数据库', 'Cache:Redis:DefaultDatabase', '0', 1, 58, 1, 0, N'Redis默认数据库编号'),
(N'Redis启用压缩', 'Cache:Redis:EnableCompression', 'true', 1, 59, 1, 0, N'是否启用Redis数据压缩'),
(N'Redis压缩阈值', 'Cache:Redis:CompressionThreshold', '1024', 1, 60, 1, 0, N'Redis数据压缩阈值(字节)');

-- 将临时表数据插入或更新到系统配置表
MERGE INTO hbt_admin_config AS target
USING #TempSysConfig AS source
ON target.config_key = source.config_key
WHEN MATCHED THEN
    UPDATE SET
        config_name = source.config_name,
        config_value = source.config_value,
        config_type = source.config_type,
        order_num = source.order_num,
        status = source.status,
        tenant_id = source.tenant_id,
        remark = source.remark,
            update_time = GETDATE(),
            update_by = 'system'
WHEN NOT MATCHED THEN
    INSERT (
        config_name, config_key, config_value, config_type,
        order_num, status, tenant_id, remark,
        create_time, create_by, update_time, update_by,
        delete_time, delete_by, is_deleted
    )
    VALUES (
        source.config_name, source.config_key, source.config_value, source.config_type,
        source.order_num, source.status, source.tenant_id, source.remark,
        GETDATE(), 'system', GETDATE(), 'system',
        NULL, NULL, 0
    );

DROP TABLE #TempSysConfig;
PRINT N'系统配置数据初始化完成。'
GO

-- 7. 初始化验证码配置数据
PRINT N'开始初始化验证码配置数据...'
GO

-- 创建临时表存储验证码配置数据
CREATE TABLE #TempCaptchaConfig (
    ConfigName NVARCHAR(100),
    ConfigKey NVARCHAR(100),
    ConfigValue NVARCHAR(500),
    ConfigType INT,
    OrderNum INT,
    Status INT,
    Remark NVARCHAR(500)
)

-- 插入验证码配置数据
INSERT INTO #TempCaptchaConfig VALUES
(N'验证码类型', 'Captcha:Type', 'Slider', 1, 140, 1, N'验证码类型(Slider/Behavior)'),
(N'滑块验证码宽度', 'Captcha:Slider:Width', '300', 2, 141, 1, N'滑块验证码背景图片宽度(像素)'),
(N'滑块验证码高度', 'Captcha:Slider:Height', '150', 2, 142, 1, N'滑块验证码背景图片高度(像素)'),
(N'滑块宽度', 'Captcha:Slider:SliderWidth', '50', 2, 143, 1, N'滑块宽度(像素)'),
(N'滑块验证容差', 'Captcha:Slider:Tolerance', '5', 2, 144, 1, N'滑块验证允许的误差像素'),
(N'滑块验证过期时间', 'Captcha:Slider:ExpirationMinutes', '5', 2, 145, 1, N'滑块验证码的过期时间(分钟)'),
(N'行为验证分数阈值', 'Captcha:Behavior:ScoreThreshold', '0.8', 2, 146, 1, N'行为验证通过的最低分数'),
(N'行为数据过期时间', 'Captcha:Behavior:DataExpirationMinutes', '30', 2, 147, 1, N'行为数据的缓存时间(分钟)'),
(N'启用机器学习', 'Captcha:Behavior:EnableMachineLearning', 'false', 3, 148, 1, N'是否启用机器学习模型进行行为分析')

-- 遍历临时表中的配置数据进行更新或插入
DECLARE @CaptchaConfigName NVARCHAR(100)
DECLARE @CaptchaConfigKey NVARCHAR(100)
DECLARE @CaptchaConfigValue NVARCHAR(500)
DECLARE @CaptchaConfigType INT
DECLARE @CaptchaOrderNum INT
DECLARE @CaptchaStatus INT
DECLARE @CaptchaRemark NVARCHAR(500)

DECLARE captcha_cursor CURSOR FOR 
SELECT ConfigName, ConfigKey, ConfigValue, ConfigType, OrderNum, Status, Remark
FROM #TempCaptchaConfig

OPEN captcha_cursor
FETCH NEXT FROM captcha_cursor INTO @CaptchaConfigName, @CaptchaConfigKey, @CaptchaConfigValue, @CaptchaConfigType, @CaptchaOrderNum, @CaptchaStatus, @CaptchaRemark

WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (SELECT 1 FROM hbt_sys_config WHERE config_key = @CaptchaConfigKey)
    BEGIN
        INSERT INTO hbt_sys_config (
            config_name, config_key, config_value, config_type,
            order_num, status, remark, create_time, create_by
        ) VALUES (
            @CaptchaConfigName, @CaptchaConfigKey, @CaptchaConfigValue, @CaptchaConfigType,
            @CaptchaOrderNum, @CaptchaStatus, @CaptchaRemark, GETDATE(), 'system'
        )
    END
    ELSE
    BEGIN
        UPDATE hbt_sys_config SET
            config_name = @CaptchaConfigName,
            config_value = @CaptchaConfigValue,
            config_type = @CaptchaConfigType,
            order_num = @CaptchaOrderNum,
            status = @CaptchaStatus,
            remark = @CaptchaRemark,
            update_time = GETDATE(),
            update_by = 'system'
        WHERE config_key = @CaptchaConfigKey
    END
    
    FETCH NEXT FROM captcha_cursor INTO @CaptchaConfigName, @CaptchaConfigKey, @CaptchaConfigValue, @CaptchaConfigType, @CaptchaOrderNum, @CaptchaStatus, @CaptchaRemark
END

CLOSE captcha_cursor
DEALLOCATE captcha_cursor

-- 删除临时表
DROP TABLE #TempCaptchaConfig

PRINT N'验证码配置数据初始化完成。'
GO

-- 8. 初始化语言配置数据
INSERT INTO hbt_language 
    (lang_code, lang_name, lang_icon, order_num, status, create_time, create_by) 
VALUES
    ('zh-CN', '简体中文', 'cn', 1, 1, GETDATE(), 'system'),
    ('en-US', 'English', 'us', 2, 1, GETDATE(), 'system');

-- 9. 初始化翻译数据
INSERT INTO hbt_translation 
    (lang_code, lang_key, lang_value, status, remark, create_time) 
VALUES
    -- JWT相关翻译
    ('zh-CN', 'Jwt.SecretKeyNotConfigured', 'JWT密钥未配置', 1, 'JWT配置错误提示', GETDATE()),
    ('zh-CN', 'Jwt.IssuerNotConfigured', 'JWT发行者未配置', 1, 'JWT配置错误提示', GETDATE()),
    ('zh-CN', 'Jwt.AudienceNotConfigured', 'JWT受众未配置', 1, 'JWT配置错误提示', GETDATE()),
    ('zh-CN', 'Jwt.RefreshSecretKeyNotConfigured', '刷新令牌密钥未配置', 1, 'JWT配置错误提示', GETDATE()),
    ('en-US', 'Jwt.SecretKeyNotConfigured', 'JWT secret key is not configured', 1, 'JWT configuration error message', GETDATE()),
    ('en-US', 'Jwt.IssuerNotConfigured', 'JWT issuer is not configured', 1, 'JWT configuration error message', GETDATE()),
    ('en-US', 'Jwt.AudienceNotConfigured', 'JWT audience is not configured', 1, 'JWT configuration error message', GETDATE()),
    ('en-US', 'Jwt.RefreshSecretKeyNotConfigured', 'Refresh token secret key is not configured', 1, 'JWT configuration error message', GETDATE()),
    
    -- 通用验证翻译
    ('zh-CN', 'Common.FieldExists', '{0}已存在：{1}', 1, '字段验证错误提示', GETDATE()),
    ('en-US', 'Common.FieldExists', '{0} already exists: {1}', 1, 'Field validation error message', GETDATE()),

    -- 系统相关翻译
    ('zh-CN', 'System.Success', '操作成功', 1, '操作成功提示', GETDATE()),
    ('zh-CN', 'System.Error', '操作失败', 1, '操作失败提示', GETDATE()),
    ('zh-CN', 'System.UnknownError', '未知错误', 1, '未知错误提示', GETDATE()),
    ('en-US', 'System.Success', 'Operation successful', 1, 'Operation success message', GETDATE()),
    ('en-US', 'System.Error', 'Operation failed', 1, 'Operation error message', GETDATE()),
    ('en-US', 'System.UnknownError', 'Unknown error', 1, 'Unknown error message', GETDATE()),

    -- 用户相关翻译
    ('zh-CN', 'User.NotFound', '用户不存在', 1, '用户不存在提示', GETDATE()),
    ('zh-CN', 'User.Disabled', '用户已禁用', 1, '用户禁用提示', GETDATE()),
    ('zh-CN', 'User.Locked', '用户已锁定', 1, '用户锁定提示', GETDATE()),
    ('zh-CN', 'User.PasswordError', '密码错误', 1, '密码错误提示', GETDATE()),
    ('zh-CN', 'User.OldPasswordError', '原密码错误', 1, '原密码错误提示', GETDATE()),
    ('zh-CN', 'User.PasswordExpired', '密码已过期', 1, '密码过期提示', GETDATE()),
    ('en-US', 'User.NotFound', 'User does not exist', 1, 'User not found message', GETDATE()),
    ('en-US', 'User.Disabled', 'User is disabled', 1, 'User disabled message', GETDATE()),
    ('en-US', 'User.Locked', 'User is locked', 1, 'User locked message', GETDATE()),
    ('en-US', 'User.PasswordError', 'Password is incorrect', 1, 'Password error message', GETDATE()),
    ('en-US', 'User.OldPasswordError', 'Old password is incorrect', 1, 'Old password error message', GETDATE()),
    ('en-US', 'User.PasswordExpired', 'Password has expired', 1, 'Password expired message', GETDATE()),

    -- 角色相关翻译
    ('zh-CN', 'Role.NotFound', '角色不存在', 1, '角色不存在提示', GETDATE()),
    ('zh-CN', 'Role.Disabled', '角色已禁用', 1, '角色禁用提示', GETDATE()),
    ('zh-CN', 'Role.HasUsers', '角色下存在用户', 1, '角色删除限制提示', GETDATE()),
    ('en-US', 'Role.NotFound', 'Role does not exist', 1, 'Role not found message', GETDATE()),
    ('en-US', 'Role.Disabled', 'Role is disabled', 1, 'Role disabled message', GETDATE()),
    ('en-US', 'Role.HasUsers', 'Role has associated users', 1, 'Role deletion restriction message', GETDATE()),

    -- 部门相关翻译
    ('zh-CN', 'Dept.NotFound', '部门不存在', 1, '部门不存在提示', GETDATE()),
    ('zh-CN', 'Dept.Disabled', '部门已禁用', 1, '部门禁用提示', GETDATE()),
    ('zh-CN', 'Dept.HasChildren', '部门下存在子部门', 1, '部门删除限制提示', GETDATE()),
    ('zh-CN', 'Dept.HasUsers', '部门下存在用户', 1, '部门删除限制提示', GETDATE()),
    ('en-US', 'Dept.NotFound', 'Department does not exist', 1, 'Department not found message', GETDATE()),
    ('en-US', 'Dept.Disabled', 'Department is disabled', 1, 'Department disabled message', GETDATE()),
    ('en-US', 'Dept.HasChildren', 'Department has sub-departments', 1, 'Department deletion restriction message', GETDATE()),
    ('en-US', 'Dept.HasUsers', 'Department has users', 1, 'Department deletion restriction message', GETDATE()),

    -- 岗位相关翻译
    ('zh-CN', 'Post.NotFound', '岗位不存在', 1, '岗位不存在提示', GETDATE()),
    ('zh-CN', 'Post.Disabled', '岗位已禁用', 1, '岗位禁用提示', GETDATE()),
    ('zh-CN', 'Post.HasUsers', '岗位下存在用户', 1, '岗位删除限制提示', GETDATE()),
    ('en-US', 'Post.NotFound', 'Post does not exist', 1, 'Post not found message', GETDATE()),
    ('en-US', 'Post.Disabled', 'Post is disabled', 1, 'Post disabled message', GETDATE()),
    ('en-US', 'Post.HasUsers', 'Post has associated users', 1, 'Post deletion restriction message', GETDATE()),

    -- 租户相关翻译
    ('zh-CN', 'Tenant.NotFound', '租户不存在', 1, '租户不存在提示', GETDATE()),
    ('zh-CN', 'Tenant.Disabled', '租户已禁用', 1, '租户禁用提示', GETDATE()),
    ('zh-CN', 'Tenant.Expired', '租户已过期', 1, '租户过期提示', GETDATE()),
    ('zh-CN', 'Tenant.UserLimitExceeded', '租户用户数量超限', 1, '租户用户限制提示', GETDATE()),
    ('en-US', 'Tenant.NotFound', 'Tenant does not exist', 1, 'Tenant not found message', GETDATE()),
    ('en-US', 'Tenant.Disabled', 'Tenant is disabled', 1, 'Tenant disabled message', GETDATE()),
    ('en-US', 'Tenant.Expired', 'Tenant has expired', 1, 'Tenant expired message', GETDATE()),
    ('en-US', 'Tenant.UserLimitExceeded', 'Tenant user limit exceeded', 1, 'Tenant user limit message', GETDATE()),

    -- 验证码相关翻译
    ('zh-CN', 'Captcha.Invalid', '验证码无效', 1, '验证码无效提示', GETDATE()),
    ('zh-CN', 'Captcha.Expired', '验证码已过期', 1, '验证码过期提示', GETDATE()),
    ('zh-CN', 'Captcha.TooManyAttempts', '验证尝试次数过多', 1, '验证码尝试限制提示', GETDATE()),
    ('en-US', 'Captcha.Invalid', 'Invalid captcha', 1, 'Invalid captcha message', GETDATE()),
    ('en-US', 'Captcha.Expired', 'Captcha has expired', 1, 'Captcha expired message', GETDATE()),
    ('en-US', 'Captcha.TooManyAttempts', 'Too many captcha attempts', 1, 'Captcha attempt limit message', GETDATE()),

    -- 文件相关翻译
    ('zh-CN', 'File.NotFound', '文件不存在', 1, '文件不存在提示', GETDATE()),
    ('zh-CN', 'File.SizeExceeded', '文件大小超限', 1, '文件大小限制提示', GETDATE()),
    ('zh-CN', 'File.TypeNotAllowed', '文件类型不允许', 1, '文件类型限制提示', GETDATE()),
    ('zh-CN', 'File.UploadFailed', '文件上传失败', 1, '文件上传失败提示', GETDATE()),
    ('en-US', 'File.NotFound', 'File does not exist', 1, 'File not found message', GETDATE()),
    ('en-US', 'File.SizeExceeded', 'File size exceeded', 1, 'File size limit message', GETDATE()),
    ('en-US', 'File.TypeNotAllowed', 'File type not allowed', 1, 'File type restriction message', GETDATE()),
    ('en-US', 'File.UploadFailed', 'File upload failed', 1, 'File upload failure message', GETDATE()); 

-- 10. 初始化菜单数据
PRINT N'开始初始化菜单数据...'
GO

-- 声明变量存储父菜单ID
DECLARE @AdminMenuId bigint;
DECLARE @IdentityMenuId bigint;
DECLARE @AuditMenuId bigint;
DECLARE @WorkflowMenuId bigint;
DECLARE @RealtimeMenuId bigint;
DECLARE @SecurityMenuId bigint;

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
('系统管理', 'menu.admin._self', NULL, 1,
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

('用户管理', 'menu.identity.user', @IdentityMenuId, 2,
 'user', 'identity/user/index', NULL, 0,
 0, 1, 0, 0,
 'identity:user:list', 'UserOutlined', 0, '用户管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('角色管理', 'menu.identity.role', @IdentityMenuId, 3,
 'role', 'identity/role/index', NULL, 0,
 0, 1, 0, 0,
 'identity:role:list', 'UserSwitchOutlined', 0, '角色管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('菜单管理', 'menu.identity.menu', @IdentityMenuId, 4,
 'menu', 'identity/menu/index', NULL, 0,
 0, 1, 0, 0,
 'identity:menu:list', 'MenuOutlined', 0, '菜单管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('部门管理', 'menu.identity.dept', @IdentityMenuId, 5,
 'dept', 'identity/dept/index', NULL, 0,
 0, 1, 0, 0,
 'identity:dept:list', 'ApartmentOutlined', 0, '部门管理菜单',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),

('岗位管理', 'menu.identity.post', @IdentityMenuId, 6,
 'post', 'identity/post/index', NULL, 0,
 0, 1, 0, 0,
 'identity:post:list', 'IdcardOutlined', 0, '岗位管理菜单',
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

-- 获取子菜单ID用于添加按钮
DECLARE @ConfigMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '系统配置');
DECLARE @LanguageMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '语言管理');
DECLARE @DictMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '字典管理');
DECLARE @TenantMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '租户管理');
DECLARE @UserMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '用户管理');
DECLARE @RoleMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '角色管理');
DECLARE @MenuMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '菜单管理');
DECLARE @DeptMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '部门管理');
DECLARE @PostMenuId bigint = (SELECT id FROM hbt_id_menu WHERE menu_name = '岗位管理');

-- 为每个菜单添加按钮
-- 系统配置按钮
INSERT INTO hbt_id_menu (
    menu_name, trans_key, parent_id, order_num,
    path, component, query_params, is_frame,
    is_cache, menu_type, visible, status,
    perms, icon, tenant_id, remark,
    create_by, create_time, update_by, update_time,
    delete_by, delete_time, is_deleted
) VALUES
('查询', 'menu.admin.config.query', @ConfigMenuId, 1,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:query', NULL, 0, '系统配置查询按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('新增', 'menu.admin.config.add', @ConfigMenuId, 2,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:add', NULL, 0, '系统配置新增按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('修改', 'menu.admin.config.edit', @ConfigMenuId, 3,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:edit', NULL, 0, '系统配置修改按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0),
('删除', 'menu.admin.config.delete', @ConfigMenuId, 4,
 '', NULL, NULL, 0,
 0, 2, 0, 0,
 'admin:config:delete', NULL, 0, '系统配置删除按钮',
 'admin', GETDATE(), 'admin', GETDATE(),
 NULL, NULL, 0);

-- 继续添加其他菜单的按钮...

PRINT N'菜单数据初始化完成。'
GO 