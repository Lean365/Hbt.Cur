//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtBusinessTypeEnum.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 14:30
// 版本号 : V0.0.1
// 描述    : 业务操作类型枚举定义
//===================================================================

namespace Hbt.Common.Enums
{
    /// <summary>
    /// 业务操作类型枚举
    /// </summary>
    /// <remarks>
    /// 创建者: Lean365
    /// 创建时间: 2024-01-16
    /// </remarks>
    public enum HbtBusinessType
    {
        /// <summary>
        /// 其他
        /// </summary>
        Other = 0,

        /// <summary>
        /// 新增
        /// </summary>
        Insert = 1,

        /// <summary>
        /// 修改
        /// </summary>
        Update = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,

        /// <summary>
        /// 授权
        /// </summary>
        Grant = 4,

        /// <summary>
        /// 导出
        /// </summary>
        Export = 5,

        /// <summary>
        /// 导入
        /// </summary>
        Import = 6,

        /// <summary>
        /// 强退
        /// </summary>
        ForceLogout = 7,

        /// <summary>
        /// 生成代码
        /// </summary>
        GenCode = 8,

        /// <summary>
        /// 清空数据
        /// </summary>
        Clean = 9,

        /// <summary>
        /// 审核
        /// </summary>
        Audit = 10,

        /// <summary>
        /// 发布
        /// </summary>
        Publish = 11,

        /// <summary>
        /// 撤销
        /// </summary>
        Revoke = 12
    }
} 