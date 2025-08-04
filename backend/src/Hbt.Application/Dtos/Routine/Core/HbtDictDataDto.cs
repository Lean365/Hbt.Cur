//===================================================================
// 项目名 : Hbt.Application
// 文件名 : HbtDictDataDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Hbt.Application.Dtos.Routine.Core
{
    /// <summary>
    /// 字典数据DTO
    /// </summary>
    public class HbtDictDataDto 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            Label = string.Empty;
            Value = string.Empty;
            ExtLabel = string.Empty;
            ExtValue = string.Empty;
            TransKey = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long DictDataId { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        public string DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 显示标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 键值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public string ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
                /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 字典数据查询DTO
    /// </summary>
    public class HbtDictDataQueryDto : HbtPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataQueryDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
        }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string? DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        public string? DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        public string? DictValue { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 字典数据创建DTO
    /// </summary>
    public class HbtDictDataCreateDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [Required(ErrorMessage = "字典类型不能为空")]
        [MaxLength(100, ErrorMessage = "字典类型长度不能超过100个字符")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典标签
        /// </summary>
        [Required(ErrorMessage = "字典标签不能为空")]
        [MaxLength(100, ErrorMessage = "字典标签长度不能超过100个字符")]
        public string DictLabel { get; set; } = string.Empty;

        /// <summary>
        /// 字典键值
        /// </summary>
        [Required(ErrorMessage = "字典键值不能为空")]
        [MaxLength(100, ErrorMessage = "字典键值长度不能超过100个字符")]
        public string DictValue { get; set; } = string.Empty;

        /// <summary>
        /// 扩展标签
        /// </summary>
        [MaxLength(100, ErrorMessage = "扩展标签长度不能超过100个字符")]
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        [MaxLength(100, ErrorMessage = "扩展键值长度不能超过100个字符")]
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        [MaxLength(100, ErrorMessage = "翻译键长度不能超过100个字符")]
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }        
    }

    /// <summary>
    /// 字典数据更新DTO
    /// </summary>
    public class HbtDictDataUpdateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        ///
        [Required(ErrorMessage = "字典数据ID不能为空")]
        [AdaptMember("Id")]
        public long DictDataId { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [Required(ErrorMessage = "字典类型不能为空")]
        [MaxLength(100, ErrorMessage = "字典类型长度不能超过100个字符")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典标签
        /// </summary>
        [Required(ErrorMessage = "字典标签不能为空")]
        [MaxLength(100, ErrorMessage = "字典标签长度不能超过100个字符")]
        public string DictLabel { get; set; } = string.Empty;

        /// <summary>
        /// 字典键值
        /// </summary>
        [Required(ErrorMessage = "字典键值不能为空")]
        [MaxLength(100, ErrorMessage = "字典键值长度不能超过100个字符")]
        public string DictValue { get; set; } = string.Empty;

        /// <summary>
        /// 扩展标签
        /// </summary>
        [MaxLength(100, ErrorMessage = "扩展标签长度不能超过100个字符")]
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        [MaxLength(100, ErrorMessage = "扩展键值长度不能超过100个字符")]
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        [MaxLength(100, ErrorMessage = "翻译键长度不能超过100个字符")]
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典数据导入DTO
    /// </summary>
    public class HbtDictDataImportDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [Required(ErrorMessage = "字典类型不能为空")]
        [MaxLength(100, ErrorMessage = "字典类型长度不能超过100个字符")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典标签
        /// </summary>
        [Required(ErrorMessage = "字典标签不能为空")]
        [MaxLength(100, ErrorMessage = "字典标签长度不能超过100个字符")]
        public string DictLabel { get; set; } = string.Empty;

        /// <summary>
        /// 字典键值
        /// </summary>
        [Required(ErrorMessage = "字典键值不能为空")]
        [MaxLength(100, ErrorMessage = "字典键值长度不能超过100个字符")]
        public string DictValue { get; set; } = string.Empty;

        /// <summary>
        /// 扩展标签
        /// </summary>
        [MaxLength(100, ErrorMessage = "扩展标签长度不能超过100个字符")]
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        [MaxLength(100, ErrorMessage = "扩展键值长度不能超过100个字符")]
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        [MaxLength(100, ErrorMessage = "翻译键长度不能超过100个字符")]
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典数据导出DTO
    /// </summary>
    public class HbtDictDataExportDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string? DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        public string? DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        public string? DictValue { get; set; }

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典数据模板DTO
    /// </summary>
    public class HbtDictDataTemplateDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string? DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        public string? DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        public string? DictValue { get; set; }

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典数据状态DTO
    /// </summary>
    public class HbtDictDataStatusDto
    {
        /// <summary>
        /// 字典数据ID
        /// </summary>
        [AdaptMember("Id")]
        public long DictDataId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}