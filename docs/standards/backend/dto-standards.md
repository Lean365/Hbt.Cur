# DTO规范

## 1. 目录规范

- 所有DTO必须放在Application层下的Dtos目录中
- 按业务模块分子目录,例如:
  - `Lean.Hbt.Application.Dtos.Identity` (身份认证模块)
  - `Lean.Hbt.Application.Dtos.System` (系统管理模块)
  - `Lean.Hbt.Application.Dtos.Monitor` (监控模块)

## 2. 命名规范

- 所有DTO类必须以`Hbt`开头,以`Dto`结尾
- 按用途分类命名:
  - 基础DTO: `HbtXxxDto`
  - 查询DTO: `HbtXxxQueryDto`
  - 创建DTO: `HbtXxxCreateDto`
  - 更新DTO: `HbtXxxUpdateDto`
  - 导入DTO: `HbtXxxImportDto`
  - 导出DTO: `HbtXxxExportDto`
  - 模板DTO: `HbtXxxTemplateDto`

## 3. 文件结构规范

```csharp
//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtXxxDto.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 21:50
// 版本号 : V1.0.0
// 描述    : XXX数据传输对象
//===================================================================

namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// XXX数据传输对象
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class HbtXxxDto
    {
        // 属性定义
    }
}
```

## 4. 标准示例：用户DTO

```csharp
namespace Lean.Hbt.Application.Dtos.Identity
{
    /// <summary>
    /// 用户基础DTO
    /// </summary>
    public class HbtUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public YesNo UserType { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CommonStatus Status { get; set; }
    }

    /// <summary>
    /// 用户查询DTO
    /// </summary>
    public class HbtUserQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CommonStatus? Status { get; set; }
    }

    /// <summary>
    /// 用户创建DTO
    /// </summary>
    public class HbtUserCreateDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public List<long> RoleIds { get; set; }

        /// <summary>
        /// 岗位ID列表
        /// </summary>
        public List<long> PostIds { get; set; }
    }

    /// <summary>
    /// 用户更新DTO
    /// </summary>
    public class HbtUserUpdateDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public List<long> RoleIds { get; set; }

        /// <summary>
        /// 岗位ID列表
        /// </summary>
        public List<long> PostIds { get; set; }
    }

    /// <summary>
    /// 用户导入DTO
    /// </summary>
    public class HbtUserImportDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别(0男 1女 2未知)
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表(逗号分隔)
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表(逗号分隔)
        /// </summary>
        public string PostNames { get; set; }
    }

    /// <summary>
    /// 用户导出DTO
    /// </summary>
    public class HbtUserExportDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string GenderName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表
        /// </summary>
        public string PostNames { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 用户导入模板DTO
    /// </summary>
    public class HbtUserTemplateDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别(0男 1女 2未知)
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表(多个逗号分隔)
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表(多个逗号分隔)
        /// </summary>
        public string PostNames { get; set; }
    }
}
```

## 5. 规范说明

1. 基础规范
   - 所有DTO类必须有完整的XML注释
   - 所有属性必须有注释说明
   - 枚举类型必须说明每个值的含义
   - 不要在DTO中包含业务逻辑

2. 属性规范
   - 属性名采用PascalCase命名法
   - 属性类型要与实体对应
   - 可以省略不需要的字段
   - 可以添加展示需要的字段
   - 敏感字段不要返回(如密码)

3. 分类规范
   - 基础DTO: 用于返回数据,包含基本字段
   - 查询DTO: 继承分页基类,包含查询条件
   - 创建DTO: 包含创建实体需要的字段
   - 更新DTO: 包含更新实体需要的字段
   - 导入DTO: 用于Excel导入,字段类型简单化
   - 导出DTO: 用于Excel导出,转换枚举显示值
   - 模板DTO: 用于生成导入模板,包含示例值

4. 验证规范
   - 必填字段使用特性验证
   - 长度限制使用特性验证
   - 格式验证使用特性验证
   - 自定义验证写在Validator中

5. 映射规范
   - 使用AutoMapper进行DTO和实体映射
   - 在Profile中配置映射关系
   - 特殊字段转换写在Resolver中

6. 安全规范
   - 密码等敏感字段不返回
   - 用户信息脱敏处理
   - 权限字段做好控制 