//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDictDataDto.cs
// 创建者 : Lean365
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;
using Lean.Hbt.Common.Enums;

namespace Lean.Hbt.Application.Dtos.Admin
{
    /// <summary>
    /// 字典数据传输对象
    /// </summary>
    public class HbtDictDataDto
    {
        /// <summary>
        /// 字典数据ID
        /// </summary>
        public long Id { get; set; }

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
        /// 字典排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 样式属性
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// 表格回显样式
        /// </summary>
        public string? ListClass { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            ExtLabel = string.Empty;
            ExtValue = string.Empty;
            TransKey = string.Empty;
            CssClass = string.Empty;
            ListClass = string.Empty;
            Remark = string.Empty;
        }
    }

    /// <summary>
    /// 字典数据查询对象
    /// </summary>
    public class HbtDictDataQueryDto : HbtPagedQuery
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
        /// 状态
        /// </summary>
        public HbtStatus? Status { get; set; }
    }

    /// <summary>
    /// 字典数据创建对象
    /// </summary>
    public class HbtDictDataCreateDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [Required(ErrorMessage = "字典类型不能为空")]
        [MaxLength(100, ErrorMessage = "字典类型长度不能超过100个字符")]
        public string DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        [Required(ErrorMessage = "字典标签不能为空")]
        [MaxLength(100, ErrorMessage = "字典标签长度不能超过100个字符")]
        public string DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        [Required(ErrorMessage = "字典键值不能为空")]
        [MaxLength(100, ErrorMessage = "字典键值长度不能超过100个字符")]
        public string DictValue { get; set; }

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
        /// 字典排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 样式属性
        /// </summary>
        [MaxLength(100, ErrorMessage = "样式属性长度不能超过100个字符")]
        public string? CssClass { get; set; }

        /// <summary>
        /// 表格回显样式
        /// </summary>
        [MaxLength(100, ErrorMessage = "表格回显样式长度不能超过100个字符")]
        public string? ListClass { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataCreateDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            ExtLabel = string.Empty;
            ExtValue = string.Empty;
            TransKey = string.Empty;
            CssClass = string.Empty;
            ListClass = string.Empty;
            Remark = string.Empty;
        }
    }

    /// <summary>
    /// 字典数据更新对象
    /// </summary>
    public class HbtDictDataUpdateDto
    {
        /// <summary>
        /// 字典数据ID
        /// </summary>
        [Required(ErrorMessage = "字典数据ID不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [Required(ErrorMessage = "字典类型不能为空")]
        [MaxLength(100, ErrorMessage = "字典类型长度不能超过100个字符")]
        public string DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        [Required(ErrorMessage = "字典标签不能为空")]
        [MaxLength(100, ErrorMessage = "字典标签长度不能超过100个字符")]
        public string DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        [Required(ErrorMessage = "字典键值不能为空")]
        [MaxLength(100, ErrorMessage = "字典键值长度不能超过100个字符")]
        public string DictValue { get; set; }

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
        /// 字典排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 样式属性
        /// </summary>
        [MaxLength(100, ErrorMessage = "样式属性长度不能超过100个字符")]
        public string? CssClass { get; set; }

        /// <summary>
        /// 表格回显样式
        /// </summary>
        [MaxLength(100, ErrorMessage = "表格回显样式长度不能超过100个字符")]
        public string? ListClass { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataUpdateDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            ExtLabel = string.Empty;
            ExtValue = string.Empty;
            TransKey = string.Empty;
            CssClass = string.Empty;
            ListClass = string.Empty;
            Remark = string.Empty;
        }
    }

    /// <summary>
    /// 字典数据导入对象
    /// </summary>
    public class HbtDictDataImportDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [Required(ErrorMessage = "字典类型不能为空")]
        [MaxLength(100, ErrorMessage = "字典类型长度不能超过100个字符")]
        public string DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        [Required(ErrorMessage = "字典标签不能为空")]
        [MaxLength(100, ErrorMessage = "字典标签长度不能超过100个字符")]
        public string DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        [Required(ErrorMessage = "字典键值不能为空")]
        [MaxLength(100, ErrorMessage = "字典键值长度不能超过100个字符")]
        public string DictValue { get; set; }

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
        /// 字典排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 样式属性
        /// </summary>
        [MaxLength(100, ErrorMessage = "样式属性长度不能超过100个字符")]
        public string? CssClass { get; set; }

        /// <summary>
        /// 表格回显样式
        /// </summary>
        [MaxLength(100, ErrorMessage = "表格回显样式长度不能超过100个字符")]
        public string? ListClass { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataImportDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            ExtLabel = string.Empty;
            ExtValue = string.Empty;
            TransKey = string.Empty;
            CssClass = string.Empty;
            ListClass = string.Empty;
            Remark = string.Empty;
        }
    }

    /// <summary>
    /// 字典数据导出对象
    /// </summary>
    public class HbtDictDataExportDto
    {
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
        /// 字典排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 样式属性
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// 表格回显样式
        /// </summary>
        public string? ListClass { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public HbtStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataExportDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            ExtLabel = string.Empty;
            ExtValue = string.Empty;
            TransKey = string.Empty;
            CssClass = string.Empty;
            ListClass = string.Empty;
            Remark = string.Empty;
        }
    }

    /// <summary>
    /// 字典数据导入模板对象
    /// </summary>
    public class HbtDictDataTemplateDto
    {
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
        /// 字典排序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 样式属性
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// 表格回显样式
        /// </summary>
        public string? ListClass { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HbtDictDataTemplateDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            ExtLabel = string.Empty;
            ExtValue = string.Empty;
            TransKey = string.Empty;
            CssClass = string.Empty;
            ListClass = string.Empty;
            Remark = string.Empty;
        }
    }

    /// <summary>
    /// 字典数据状态更新对象
    /// </summary>
    public class HbtDictDataStatusDto
    {
        /// <summary>
        /// 字典数据ID
        /// </summary>
        [Required(ErrorMessage = "字典数据ID不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public HbtStatus Status { get; set; }
    }
}