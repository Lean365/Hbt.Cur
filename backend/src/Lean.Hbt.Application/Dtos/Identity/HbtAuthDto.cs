//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtAuthDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录数据传输对象
//===================================================================

#nullable enable

using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Identity;

/// <summary>
/// 登录请求DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtAuthDto
{

    /// <summary>
    /// 用户名
    /// </summary>

    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>

    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 验证码Token
    /// </summary>
    public string? CaptchaToken { get; set; }

    /// <summary>
    /// 验证码偏移量
    /// </summary>
    public int CaptchaOffset { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// 用户代理
    /// </summary>
    public string UserAgent { get; set; } = string.Empty;

    /// <summary>
    /// 登录类型
    /// </summary>
    public HbtLoginType LoginType { get; set; } = HbtLoginType.Password;

    /// <summary>
    /// 登录来源
    /// </summary>

    public int LoginSource { get; set; }

    /// <summary>
    /// 设备信息
    /// </summary>
    public HbtSignalRDevice? DeviceInfo { get; set; }

    /// <summary>
    /// 环境信息
    /// </summary>
    public HbtSignalREnvironment? EnvironmentInfo { get; set; }
}

/// <summary>
/// 登录结果传输对象
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtLoginResultDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtLoginResultDto()
    {
        AccessToken = string.Empty;
        RefreshToken = string.Empty;
        UserInfo = new HbtUserInfoDto();
    }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// 过期时间(秒)
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// 用户信息
    /// </summary>
    public HbtUserInfoDto UserInfo { get; set; }
}

/// <summary>
/// 用户信息DTO
/// </summary>
/// <remarks>
/// 创建者: Lean365
/// 创建时间: 2024-01-22
/// </remarks>
public class HbtUserInfoDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtUserInfoDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        FullName = string.Empty;
        RealName = string.Empty;
        EnglishName = string.Empty;
        Avatar = string.Empty;
        Roles = new List<string>();
        Permissions = new List<string>();
    }



    /// <summary>
    /// 用户ID
    /// </summary>
    [AdaptMember("Id")]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 全名
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 英文名称
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 用户类型
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 角色列表
    /// </summary>
    public List<string> Roles { get; set; }

    /// <summary>
    /// 权限列表
    /// </summary>
    public List<string> Permissions { get; set; }
}
