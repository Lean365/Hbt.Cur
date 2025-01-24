-- ===================================================================
-- 项目名 : Lean.Hbt
-- 文件名 : init-seed-data.sql
-- 创建者 : Lean365
-- 创建时间: 2024-01-22
-- 版本号 : V0.0.1
-- 描述   : 数据库种子数据初始化脚本
-- ===================================================================

-- 1. 初始化租户数据
PRINT N'开始初始化租户数据...'
GO

-- 检查是否存在租户数据
IF NOT EXISTS (SELECT 1 FROM hbt_tenant)
BEGIN
    INSERT INTO hbt_tenant (
        tenant_name, tenant_code, contact_person, contact_phone, contact_email, 
        address, domain, logo_url, db_connection, theme, 
        license_start_time, license_end_time, max_user_count, status, 
        create_time, create_by
    ) VALUES (
        N'默认租户', 'default', N'管理员', '13800138000', 'admin@lean365.com',
        N'默认地址', 'localhost', '/logo.png',
        'Server=localhost;Database=LeanHbt_Dev;Trusted_Connection=True;MultipleActiveResultSets=true',
        'default',
        GETDATE(), DATEADD(YEAR, 1, GETDATE()), 100, 1,
        GETDATE(), 'system'
    );
    PRINT N'租户数据初始化完成。'
END
ELSE
BEGIN
    UPDATE hbt_tenant SET
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
        update_by = 'system'
    WHERE tenant_code = 'default';
    PRINT N'租户数据更新完成。'
END
GO

-- 2. 初始化角色数据
PRINT N'开始初始化角色数据...'
GO

-- 检查是否存在角色数据
IF NOT EXISTS (SELECT 1 FROM hbt_role WHERE role_key = 'admin')
BEGIN
    INSERT INTO hbt_role (
        role_name, role_key, status, create_time, create_by
    ) VALUES (
        N'超级管理员', 'admin', 1, GETDATE(), 'system'
    );
    PRINT N'角色数据初始化完成。'
END
ELSE
BEGIN
    UPDATE hbt_role SET
        role_name = N'超级管理员',
        status = 1,
        update_time = GETDATE(),
        update_by = 'system'
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
IF NOT EXISTS (SELECT 1 FROM hbt_user WHERE user_name = 'admin')
BEGIN
    INSERT INTO hbt_user (
        user_name, nick_name, english_name, user_type, password, salt,
        email, phone_number, gender, avatar, status,
        last_password_change_time, create_time, create_by, tenant_id
    ) VALUES (
        'admin', N'超级管理员', 'Administrator', 1, @PasswordHash, @Salt,
        'admin@lean365.com', '13800138000', 0, '/avatar/default.png', 1,
        GETDATE(), GETDATE(), 'system', 0
    );
    PRINT N'用户数据初始化完成。'
END
ELSE
BEGIN
    UPDATE hbt_user SET
        nick_name = N'超级管理员',
        english_name = 'Administrator',
        user_type = 1,
        email = 'admin@lean365.com',
        phone_number = '13800138000',
        status = 1,
        update_time = GETDATE(),
        update_by = 'system'
    WHERE user_name = 'admin';
    PRINT N'用户数据更新完成。'
END
GO

-- 4. 初始化部门数据
PRINT N'开始初始化部门数据...'
GO

-- 检查是否存在总公司部门
IF NOT EXISTS (SELECT 1 FROM hbt_dept WHERE dept_name = N'总公司')
BEGIN
    INSERT INTO hbt_dept (
        dept_name, parent_id, order_num, leader, phone, email, 
        status, create_time, create_by
    ) VALUES (
        N'总公司', 0, 1, N'管理员', '13800138000', 'admin@lean365.com',
        1, GETDATE(), 'system'
    );
    PRINT N'部门数据初始化完成。'
END
ELSE
BEGIN
    UPDATE hbt_dept SET
        parent_id = 0,
        order_num = 1,
        leader = N'管理员',
        phone = '13800138000',
        email = 'admin@lean365.com',
        status = 1,
        update_time = GETDATE(),
        update_by = 'system'
    WHERE dept_name = N'总公司';
    PRINT N'部门数据更新完成。'
END
GO

-- 5. 初始化岗位数据
PRINT N'开始初始化岗位数据...'
GO

-- 检查并更新或插入总经理岗位
IF NOT EXISTS (SELECT 1 FROM hbt_post WHERE post_code = 'GM')
BEGIN
    INSERT INTO hbt_post (
        post_name, post_code, order_num, status, create_time, create_by
    ) VALUES (
        N'总经理', 'GM', 1, 1, GETDATE(), 'system'
    );
END
ELSE
BEGIN
    UPDATE hbt_post SET
        post_name = N'总经理',
        order_num = 1,
        status = 1,
        update_time = GETDATE(),
        update_by = 'system'
    WHERE post_code = 'GM';
END

-- 检查并更新或插入项目经理岗位
IF NOT EXISTS (SELECT 1 FROM hbt_post WHERE post_code = 'PM')
BEGIN
    INSERT INTO hbt_post (
        post_name, post_code, order_num, status, create_time, create_by
    ) VALUES (
        N'项目经理', 'PM', 2, 1, GETDATE(), 'system'
    );
END
ELSE
BEGIN
    UPDATE hbt_post SET
        post_name = N'项目经理',
        order_num = 2,
        status = 1,
        update_time = GETDATE(),
        update_by = 'system'
    WHERE post_code = 'PM';
END

-- 检查并更新或插入开发工程师岗位
IF NOT EXISTS (SELECT 1 FROM hbt_post WHERE post_code = 'DEV')
BEGIN
    INSERT INTO hbt_post (
        post_name, post_code, order_num, status, create_time, create_by
    ) VALUES (
        N'开发工程师', 'DEV', 3, 1, GETDATE(), 'system'
    );
END
ELSE
BEGIN
    UPDATE hbt_post SET
        post_name = N'开发工程师',
        order_num = 3,
        status = 1,
        update_time = GETDATE(),
        update_by = 'system'
    WHERE post_code = 'DEV';
END

PRINT N'岗位数据初始化完成。'
GO

-- 6. 初始化系统配置数据
PRINT N'开始初始化系统配置数据...'
GO

-- 创建临时表存储配置数据
CREATE TABLE #TempSysConfig (
    ConfigName NVARCHAR(100),
    ConfigKey NVARCHAR(100),
    ConfigValue NVARCHAR(500),
    ConfigType INT,
    OrderNum INT,
    Status INT,
    Remark NVARCHAR(500)
)

-- 插入缓存配置数据
INSERT INTO #TempSysConfig VALUES
(N'缓存提供程序', 'Cache:Provider', 'Memory', 1, 50, 1, N'缓存提供程序类型(Memory/Redis)'),
(N'默认过期时间(分钟)', 'Cache:DefaultExpirationMinutes', '30', 1, 51, 1, N'缓存默认过期时间(分钟)'),
(N'启用滑动过期', 'Cache:EnableSlidingExpiration', 'true', 1, 52, 1, N'是否启用滑动过期'),
(N'启用多级缓存', 'Cache:EnableMultiLevelCache', 'false', 1, 53, 1, N'是否启用多级缓存'),
(N'内存缓存大小限制', 'Cache:Memory:SizeLimit', '104857600', 1, 54, 1, N'内存缓存大小限制(字节)'),
(N'内存缓存压缩阈值', 'Cache:Memory:CompactionThreshold', '1048576', 1, 55, 1, N'内存缓存压缩阈值(字节)'),
(N'过期扫描频率', 'Cache:Memory:ExpirationScanFrequency', '60', 1, 56, 1, N'过期扫描频率(秒)'),
(N'Redis实例名称', 'Cache:Redis:InstanceName', 'Lean.Hbt', 1, 57, 1, N'Redis实例名称'),
(N'Redis数据库', 'Cache:Redis:DefaultDatabase', '0', 1, 58, 1, N'Redis默认数据库编号'),
(N'Redis启用压缩', 'Cache:Redis:EnableCompression', 'true', 1, 59, 1, N'是否启用Redis数据压缩'),
(N'Redis压缩阈值', 'Cache:Redis:CompressionThreshold', '1024', 1, 60, 1, N'Redis数据压缩阈值(字节)'),

-- 插入OAuth配置数据
(N'OAuth启用状态', 'Security:OAuth:Enabled', 'true', 1, 70, 1, N'是否启用OAuth认证'),
(N'GitHub客户端ID', 'Security:OAuth:Providers:GitHub:ClientId', '', 1, 71, 1, N'GitHub OAuth应用的客户端ID'),
(N'GitHub客户端密钥', 'Security:OAuth:Providers:GitHub:ClientSecret', '', 1, 72, 1, N'GitHub OAuth应用的客户端密钥'),
(N'GitHub授权端点', 'Security:OAuth:Providers:GitHub:AuthorizationEndpoint', 'https://github.com/login/oauth/authorize', 1, 73, 1, N'GitHub OAuth授权端点URL'),
(N'GitHub令牌端点', 'Security:OAuth:Providers:GitHub:TokenEndpoint', 'https://github.com/login/oauth/access_token', 1, 74, 1, N'GitHub OAuth令牌端点URL'),
(N'GitHub用户信息端点', 'Security:OAuth:Providers:GitHub:UserInfoEndpoint', 'https://api.github.com/user', 1, 75, 1, N'GitHub OAuth用户信息端点URL'),
(N'GitHub回调地址', 'Security:OAuth:Providers:GitHub:RedirectUri', 'https://localhost:5001/oauth/callback/github', 1, 76, 1, N'GitHub OAuth回调地址'),
(N'GitHub权限范围', 'Security:OAuth:Providers:GitHub:Scope', 'read:user user:email', 1, 77, 1, N'GitHub OAuth所需权限范围'),
(N'Google客户端ID', 'Security:OAuth:Providers:Google:ClientId', '', 1, 78, 1, N'Google OAuth应用的客户端ID'),
(N'Google客户端密钥', 'Security:OAuth:Providers:Google:ClientSecret', '', 1, 79, 1, N'Google OAuth应用的客户端密钥'),

-- 插入日志清理配置数据
(N'日志清理启用状态', 'LogCleanup:Enabled', 'true', 1, 90, 1, N'是否启用日志自动清理'),
(N'日志保留天数', 'LogCleanup:RetentionDays', '30', 1, 91, 1, N'日志保留天数，超过该天数的日志将被清理'),
(N'日志清理执行时间', 'LogCleanup:ExecutionTime', '02:00:00', 1, 92, 1, N'日志清理的执行时间（24小时制）'),
(N'批次清理数量', 'LogCleanup:BatchSize', '1000', 1, 93, 1, N'每次清理的日志数量'),
(N'日志类型', 'LogCleanup:LogTypes', 'Info,Debug,Warning', 1, 94, 1, N'需要清理的日志类型，多个类型用逗号分隔'),

-- 插入日志归档配置数据
(N'日志归档启用状态', 'LogArchive:Enabled', 'true', 1, 100, 1, N'是否启用日志自动归档'),
(N'归档触发天数', 'LogArchive:TriggerDays', '90', 1, 101, 1, N'超过多少天的日志将被归档'),
(N'归档执行时间', 'LogArchive:ExecutionTime', '03:00:00', 1, 102, 1, N'日志归档的执行时间（24小时制）'),
(N'归档批次大小', 'LogArchive:BatchSize', '1000', 1, 103, 1, N'每次归档的日志数量'),
(N'归档存储路径', 'LogArchive:StoragePath', 'Archive/Logs', 1, 104, 1, N'日志归档文件的存储路径'),
(N'归档文件格式', 'LogArchive:FileFormat', 'json', 1, 105, 1, N'归档文件的格式(json/csv)'),
(N'归档压缩启用', 'LogArchive:Compression:Enabled', 'true', 1, 106, 1, N'是否启用归档文件压缩'),
(N'归档压缩格式', 'LogArchive:Compression:Format', 'gzip', 1, 107, 1, N'归档文件的压缩格式(gzip/zip)'),

-- 插入安全配置数据
(N'密码最小长度', 'Security:Password:MinLength', '8', 1, 110, 1, N'密码最小长度要求'),
(N'密码复杂度要求', 'Security:Password:RequireComplexity', 'true', 1, 111, 1, N'是否要求密码包含大小写字母、数字和特殊字符'),
(N'密码过期天数', 'Security:Password:ExpirationDays', '90', 1, 112, 1, N'密码过期天数，0表示永不过期'),
(N'密码历史记录数', 'Security:Password:HistoryCount', '3', 1, 113, 1, N'记住多少个历史密码，防止重复使用'),
(N'登录失败锁定次数', 'Security:Lockout:MaxFailedAttempts', '5', 1, 114, 1, N'允许的最大登录失败次数'),
(N'锁定时间(分钟)', 'Security:Lockout:DurationMinutes', '30', 1, 115, 1, N'账户锁定持续时间(分钟)'),
(N'会话超时时间(分钟)', 'Security:Session:TimeoutMinutes', '30', 1, 116, 1, N'用户会话超时时间(分钟)'),
(N'允许多端登录', 'Security:Session:AllowMultipleLogin', 'false', 1, 117, 1, N'是否允许同一账户多个终端同时登录'),
(N'JWT密钥', 'Security:Jwt:SecretKey', 'your-secret-key-here', 1, 118, 1, N'JWT令牌加密密钥'),
(N'JWT过期时间(分钟)', 'Security:Jwt:ExpirationMinutes', '120', 1, 119, 1, N'JWT令牌过期时间(分钟)'),
(N'启用CORS', 'Security:Cors:Enabled', 'true', 1, 120, 1, N'是否启用跨域资源共享'),
(N'允许的来源', 'Security:Cors:AllowedOrigins', '*', 1, 121, 1, N'允许的跨域来源，多个用逗号分隔，*表示允许所有'),

-- 插入数据库配置数据
(N'数据库类型', 'Database:Type', 'SqlServer', 1, 130, 1, N'数据库类型(SqlServer/MySql/PostgreSQL/Oracle/Sqlite)'),
(N'数据库连接字符串', 'Database:ConnectionString', '', 1, 131, 1, N'数据库连接字符串(已加密)'),
(N'最大连接池大小', 'Database:MaxPoolSize', '100', 1, 132, 1, N'数据库最大连接池大小'),
(N'最小连接池大小', 'Database:MinPoolSize', '5', 1, 133, 1, N'数据库最小连接池大小'),
(N'连接超时时间', 'Database:ConnectionTimeout', '30', 1, 134, 1, N'数据库连接超时时间(秒)'),
(N'命令超时时间', 'Database:CommandTimeout', '30', 1, 135, 1, N'数据库命令执行超时时间(秒)'),
(N'启用读写分离', 'Database:EnableReadWriteSeparation', 'false', 1, 136, 1, N'是否启用读写分离'),
(N'只读连接字符串', 'Database:ReadOnlyConnectionString', '', 1, 137, 1, N'只读数据库连接字符串(读写分离时使用，需加密)')

-- 遍历临时表中的配置数据进行更新或插入
DECLARE @ConfigName NVARCHAR(100)
DECLARE @ConfigKey NVARCHAR(100)
DECLARE @ConfigValue NVARCHAR(500)
DECLARE @ConfigType INT
DECLARE @OrderNum INT
DECLARE @Status INT
DECLARE @Remark NVARCHAR(500)

DECLARE config_cursor CURSOR FOR 
SELECT ConfigName, ConfigKey, ConfigValue, ConfigType, OrderNum, Status, Remark
FROM #TempSysConfig

OPEN config_cursor
FETCH NEXT FROM config_cursor INTO @ConfigName, @ConfigKey, @ConfigValue, @ConfigType, @OrderNum, @Status, @Remark

WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (SELECT 1 FROM hbt_sys_config WHERE config_key = @ConfigKey)
    BEGIN
        INSERT INTO hbt_sys_config (
            config_name, config_key, config_value, config_type,
            order_num, status, remark, create_time, create_by
        ) VALUES (
            @ConfigName, @ConfigKey, @ConfigValue, @ConfigType,
            @OrderNum, @Status, @Remark, GETDATE(), 'system'
        )
    END
    ELSE
    BEGIN
        UPDATE hbt_sys_config SET
            config_name = @ConfigName,
            config_value = @ConfigValue,
            config_type = @ConfigType,
            order_num = @OrderNum,
            status = @Status,
            remark = @Remark,
            update_time = GETDATE(),
            update_by = 'system'
        WHERE config_key = @ConfigKey
    END
    
    FETCH NEXT FROM config_cursor INTO @ConfigName, @ConfigKey, @ConfigValue, @ConfigType, @OrderNum, @Status, @Remark
END

CLOSE config_cursor
DEALLOCATE config_cursor

-- 删除临时表
DROP TABLE #TempSysConfig

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

-- 系统管理菜单
IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'系统管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'系统管理', 0, 1, 'system', NULL, '', 1,
        0, 'M', '0', 1, 'system', 'system', GETDATE(), 'system'
    );
END

DECLARE @SystemManageId BIGINT = (SELECT menu_id FROM hbt_menu WHERE menu_name = N'系统管理');

-- 系统管理子菜单
IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'系统配置')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'系统配置', @SystemManageId, 1, 'config', 'system/config/index', '', 1,
        0, 'C', '0', 1, 'system:config:list', 'config', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'语言管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'语言管理', @SystemManageId, 2, 'language', 'system/language/index', '', 1,
        0, 'C', '0', 1, 'system:language:list', 'language', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'翻译管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'翻译管理', @SystemManageId, 3, 'translation', 'system/translation/index', '', 1,
        0, 'C', '0', 1, 'system:translation:list', 'translation', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'字典类型')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'字典类型', @SystemManageId, 4, 'dict-type', 'system/dict-type/index', '', 1,
        0, 'C', '0', 1, 'system:dict-type:list', 'dict-type', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'数据字典')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'数据字典', @SystemManageId, 5, 'dict-data', 'system/dict-data/index', '', 1,
        0, 'C', '0', 1, 'system:dict-data:list', 'dict-data', GETDATE(), 'system'
    );
END

-- 认证管理菜单
IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'认证管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'认证管理', 0, 2, 'auth', NULL, '', 1,
        0, 'M', '0', 1, 'auth', 'auth', GETDATE(), 'system'
    );
END

DECLARE @AuthManageId BIGINT = (SELECT menu_id FROM hbt_menu WHERE menu_name = N'认证管理');

-- 认证管理子菜单
IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'租户管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'租户管理', @AuthManageId, 1, 'tenant', 'auth/tenant/index', '', 1,
        0, 'C', '0', 1, 'auth:tenant:list', 'tenant', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'用户管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'用户管理', @AuthManageId, 2, 'user', 'auth/user/index', '', 1,
        0, 'C', '0', 1, 'auth:user:list', 'user', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'菜单管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'菜单管理', @AuthManageId, 3, 'menu', 'auth/menu/index', '', 1,
        0, 'C', '0', 1, 'auth:menu:list', 'menu', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'部门管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'部门管理', @AuthManageId, 4, 'dept', 'auth/dept/index', '', 1,
        0, 'C', '0', 1, 'auth:dept:list', 'dept', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'角色管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'角色管理', @AuthManageId, 5, 'role', 'auth/role/index', '', 1,
        0, 'C', '0', 1, 'auth:role:list', 'role', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'岗位管理')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'岗位管理', @AuthManageId, 6, 'post', 'auth/post/index', '', 1,
        0, 'C', '0', 1, 'auth:post:list', 'post', GETDATE(), 'system'
    );
END

-- 系统监控菜单
IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'系统监控')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'系统监控', 0, 3, 'monitor', NULL, '', 1,
        0, 'M', '0', 1, 'monitor', 'monitor', GETDATE(), 'system'
    );
END

DECLARE @SystemMonitorId BIGINT = (SELECT menu_id FROM hbt_menu WHERE menu_name = N'系统监控');

-- 系统监控子菜单
IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'登录日志')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'登录日志', @SystemMonitorId, 1, 'loginlog', 'monitor/loginlog/index', '', 1,
        0, 'C', '0', 1, 'monitor:loginlog:list', 'loginlog', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'审计日志')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'审计日志', @SystemMonitorId, 2, 'auditlog', 'monitor/auditlog/index', '', 1,
        0, 'C', '0', 1, 'monitor:auditlog:list', 'auditlog', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'操作日志')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'操作日志', @SystemMonitorId, 3, 'operlog', 'monitor/operlog/index', '', 1,
        0, 'C', '0', 1, 'monitor:operlog:list', 'operlog', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'差异日志')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'差异日志', @SystemMonitorId, 4, 'difflog', 'monitor/difflog/index', '', 1,
        0, 'C', '0', 1, 'monitor:difflog:list', 'difflog', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'异常日志')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'异常日志', @SystemMonitorId, 5, 'exceptionlog', 'monitor/exceptionlog/index', '', 1,
        0, 'C', '0', 1, 'monitor:exceptionlog:list', 'exceptionlog', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'服务监控')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'服务监控', @SystemMonitorId, 6, 'server', 'monitor/server/index', '', 1,
        0, 'C', '0', 1, 'monitor:server:list', 'server', GETDATE(), 'system'
    );
END

-- 实时在线菜单
IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'实时在线')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'实时在线', 0, 4, 'online', NULL, '', 1,
        0, 'M', '0', 1, 'online', 'online', GETDATE(), 'system'
    );
END

DECLARE @OnlineId BIGINT = (SELECT menu_id FROM hbt_menu WHERE menu_name = N'实时在线');

-- 实时在线子菜单
IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'在线用户')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'在线用户', @OnlineId, 1, 'user', 'online/user/index', '', 1,
        0, 'C', '0', 1, 'online:user:list', 'online-user', GETDATE(), 'system'
    );
END

IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = N'在线消息')
BEGIN
    INSERT INTO hbt_menu (
        menu_name, parent_id, order_num, path, component, query, is_frame,
        is_cache, menu_type, visible, status, perms, icon, create_time, create_by
    ) VALUES (
        N'在线消息', @OnlineId, 2, 'message', 'online/message/index', '', 1,
        0, 'C', '0', 1, 'online:message:list', 'online-message', GETDATE(), 'system'
    );
END

-- 为每个菜单添加按钮
DECLARE @MenuId BIGINT
DECLARE @MenuName NVARCHAR(50)
DECLARE @MenuPerms NVARCHAR(100)

DECLARE menu_cursor CURSOR FOR 
SELECT menu_id, menu_name, perms 
FROM hbt_menu 
WHERE menu_type = 'C'

OPEN menu_cursor
FETCH NEXT FROM menu_cursor INTO @MenuId, @MenuName, @MenuPerms

WHILE @@FETCH_STATUS = 0
BEGIN
    -- 查询按钮
    IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = @MenuName + N'查询' AND parent_id = @MenuId)
    BEGIN
        INSERT INTO hbt_menu (
            menu_name, parent_id, order_num, path, component, query, is_frame,
            is_cache, menu_type, visible, status, perms, icon, create_time, create_by
        ) VALUES (
            @MenuName + N'查询', @MenuId, 1, NULL, NULL, '', 1,
            0, 'F', '0', 1, @MenuPerms + ':query', NULL, GETDATE(), 'system'
        );
    END

    -- 新增按钮
    IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = @MenuName + N'新增' AND parent_id = @MenuId)
    BEGIN
        INSERT INTO hbt_menu (
            menu_name, parent_id, order_num, path, component, query, is_frame,
            is_cache, menu_type, visible, status, perms, icon, create_time, create_by
        ) VALUES (
            @MenuName + N'新增', @MenuId, 2, NULL, NULL, '', 1,
            0, 'F', '0', 1, @MenuPerms + ':add', NULL, GETDATE(), 'system'
        );
    END

    -- 修改按钮
    IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = @MenuName + N'修改' AND parent_id = @MenuId)
    BEGIN
        INSERT INTO hbt_menu (
            menu_name, parent_id, order_num, path, component, query, is_frame,
            is_cache, menu_type, visible, status, perms, icon, create_time, create_by
        ) VALUES (
            @MenuName + N'修改', @MenuId, 3, NULL, NULL, '', 1,
            0, 'F', '0', 1, @MenuPerms + ':edit', NULL, GETDATE(), 'system'
        );
    END

    -- 删除按钮
    IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = @MenuName + N'删除' AND parent_id = @MenuId)
    BEGIN
        INSERT INTO hbt_menu (
            menu_name, parent_id, order_num, path, component, query, is_frame,
            is_cache, menu_type, visible, status, perms, icon, create_time, create_by
        ) VALUES (
            @MenuName + N'删除', @MenuId, 4, NULL, NULL, '', 1,
            0, 'F', '0', 1, @MenuPerms + ':remove', NULL, GETDATE(), 'system'
        );
    END

    -- 导出按钮 (排除服务监控和在线消息)
    IF @MenuName NOT IN (N'服务监控', N'在线消息')
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = @MenuName + N'导出' AND parent_id = @MenuId)
        BEGIN
            INSERT INTO hbt_menu (
                menu_name, parent_id, order_num, path, component, query, is_frame,
                is_cache, menu_type, visible, status, perms, icon, create_time, create_by
            ) VALUES (
                @MenuName + N'导出', @MenuId, 5, NULL, NULL, '', 1,
                0, 'F', '0', 1, @MenuPerms + ':export', NULL, GETDATE(), 'system'
            );
        END
    END

    -- 导入按钮 (仅适用于部分菜单)
    IF @MenuName IN (N'租户管理', N'用户管理', N'部门管理', N'岗位管理', N'字典类型', N'数据字典')
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM hbt_menu WHERE menu_name = @MenuName + N'导入' AND parent_id = @MenuId)
        BEGIN
            INSERT INTO hbt_menu (
                menu_name, parent_id, order_num, path, component, query, is_frame,
                is_cache, menu_type, visible, status, perms, icon, create_time, create_by
            ) VALUES (
                @MenuName + N'导入', @MenuId, 6, NULL, NULL, '', 1,
                0, 'F', '0', 1, @MenuPerms + ':import', NULL, GETDATE(), 'system'
            );
        END
    END

    FETCH NEXT FROM menu_cursor INTO @MenuId, @MenuName, @MenuPerms
END

CLOSE menu_cursor
DEALLOCATE menu_cursor

PRINT N'菜单数据初始化完成。'
GO 