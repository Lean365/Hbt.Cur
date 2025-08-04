# 命名规范

## 1. 项目命名规范

- 所有项目必须以`Hbt.Cur`开头
- 按照DDD分层命名:
  - `Hbt.Cur.Domain` - 领域层
  - `Hbt.Cur.Application` - 应用层
  - `Hbt.Cur.Infrastructure` - 基础设施层
  - `Hbt.Cur.Common` - 公共层
  - `Hbt.Cur.WebApi` - 接口层

## 2. 目录命名规范

- 按业务模块分目录,例如:
  - `Identity` - 身份认证模块
  - `System` - 系统管理模块
  - `Monitor` - 监控模块
- 按功能分目录,例如:
  - `Controllers` - 控制器
  - `Services` - 服务
  - `Repositories` - 仓储
  - `Entities` - 实体
  - `Dtos` - 数据传输对象
  - `Models` - 模型
  - `Enums` - 枚举
  - `Constants` - 常量
  - `Extensions` - 扩展方法
  - `Helpers` - 帮助类
  - `Middlewares` - 中间件

## 3. 文件命名规范

- 所有文件名必须以`Hbt`开头
- 按类型添加后缀:
  - 实体类: `HbtUser.cs`
  - DTO类: `HbtUserDto.cs`
  - 控制器类: `HbtUserController.cs`
  - 服务接口: `IHbtUserService.cs`
  - 服务实现: `HbtUserService.cs`
  - 枚举类: `HbtUserTypeEnum.cs`
  - 常量类: `HbtUserConstants.cs`
  - 扩展类: `HbtUserExtensions.cs`
  - 帮助类: `HbtUserHelper.cs`
  - 中间件类: `HbtExceptionMiddleware.cs`

## 4. 类命名规范

- 所有类必须以`Hbt`开头
- 使用PascalCase命名法
- 按类型添加后缀:
  - 实体类: `public class HbtUser`
  - DTO类: `public class HbtUserDto`
  - 控制器类: `public class HbtUserController`
  - 服务接口: `public interface IHbtUserService`
  - 服务实现: `public class HbtUserService`
  - 枚举类: `public enum HbtUserType`
  - 常量类: `public class HbtUserConstants`
  - 扩展类: `public static class HbtUserExtensions`
  - 帮助类: `public class HbtUserHelper`
  - 中间件类: `public class HbtExceptionMiddleware`

## 5. 方法命名规范

- 使用PascalCase命名法
- 动词+名词形式
- 常用动词:
  - 获取: Get
  - 查询: Query
  - 创建: Create
  - 更新: Update
  - 删除: Delete
  - 导入: Import
  - 导出: Export
  - 验证: Validate
  - 转换: Convert
  - 计算: Calculate
  - 处理: Handle
  - 发送: Send
  - 接收: Receive

## 6. 属性命名规范

- 使用PascalCase命名法
- 名词形式
- 布尔类型使用:
  - Is前缀: IsDeleted
  - Has前缀: HasChildren
  - Can前缀: CanEdit
  - Should前缀: ShouldUpdate

## 7. 变量命名规范

- 私有字段使用_camelCase
- 局部变量使用camelCase
- 参数使用camelCase
- 常量使用UPPER_CASE
- 避免使用缩写(除非是约定俗成的)

## 8. 接口命名规范

- 所有接口必须以I开头
- 接口必须以`Hbt`开头
- 使用PascalCase命名法
- 按功能命名:
  - 服务接口: `IHbtUserService`
  - 仓储接口: `IHbtRepository<T>`
  - 缓存接口: `IHbtCache`
  - 日志接口: `IHbtLogger`

## 9. 特性命名规范

- 所有特性必须以`Hbt`开头
- 使用PascalCase命名法
- 必须以Attribute结尾
- 例如:
  - `HbtAuthAttribute`
  - `HbtLogAttribute`
  - `HbtPermissionAttribute`
  - `HbtValidationAttribute` 

## 10. SqlSugar CodeFirst命名规范

### 表命名规范
- 所有表名必须以`hbt_`开头
- 使用小写下划线命名法
- 示例：
  - `hbt_user` - 用户表
  - `hbt_role` - 角色表
  - `hbt_menu` - 菜单表

### 列命名规范
- 使用小写下划线命名法
- 主键统一命名为`id`
- 创建时间统一命名为`create_time`
- 创建者统一命名为`create_by`
- 更新时间统一命名为`update_time`
- 更新者统一命名为`update_by`
- 删除标记统一命名为`is_deleted`
- 备注统一命名为`remark`
- 租户ID统一命名为`tenant_id`
- 示例：
  - `user_name` - 用户名
  - `nick_name` - 昵称
  - `phone_number` - 手机号

### 索引命名规范
- 唯一索引必须以`ix_`开头
- 普通索引必须以`idx_`开头
- 使用小写下划线命名法
- 示例：
  - `ix_user_name` - 用户名唯一索引
  - `idx_create_time` - 创建时间普通索引

### 特性配置规范
- 表配置：
  ```csharp
  [SugarTable("hbt_user", "用户表")]
  ```
- 列配置：
  ```csharp
  [SugarColumn(ColumnName = "user_name", 
               ColumnDescription = "用户名", 
               Length = 50, 
               ColumnDataType = "nvarchar", 
               IsNullable = false)]
  ```
- 索引配置：
  ```csharp
  [SugarIndex("ix_user_name", nameof(UserName), OrderByType.Asc, true)]
  ```
- 导航属性配置：
  ```csharp
  [Navigate(NavigateType.OneToMany, nameof(HbtUserRole.UserId))]
  ```

### 数据类型规范
- 字符串类型：
  - 默认使用`nvarchar`
  - 必须指定Length属性
  - 超长文本使用`text`
- 数值类型：
  - 主键使用`bigint`
  - 普通整数使用`int`
  - 状态值使用`int`
  - 金额使用`decimal(18,5)`
- 日期类型：
  - 默认使用`datetime`
  - 仅日期使用`date`
- 布尔类型：
  - 统一使用`int`
  - 值为0或1（0:否/禁用/隐藏，1:是/启用/显示）

## 11. Helper和Utils区分规范

### 帮助类(Helper)
- 目的是帮助减少重复代码
- 封装特定业务场景的通用操作
- 必须以`Helper`结尾命名
- 示例：
  - `HbtExcelHelper` - Excel导入导出帮助类，减少各业务模块重复编写导入导出代码
  - `HbtCacheHelper` - 缓存操作帮助类，减少缓存操作重复代码
  - `HbtValidationHelper` - 验证帮助类，减少验证逻辑重复代码

### 工具类(Utils)
- 提供基础的工具方法
- 不涉及具体业务逻辑
- 必须以`Utils`结尾命名
- 示例：
  - `HbtStringUtils` - 字符串处理工具类
  - `HbtDateUtils` - 日期转换工具类
  - `HbtEncryptUtils` - 加密解密工具类