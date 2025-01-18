//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtExcelHelper.cs
// 创建者 : Lean365
// 创建时间: 2024-01-16 21:50
// 版本号 : V.0.0.1
// 描述    : Excel导入导出帮助类
//===================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lean.Hbt.Common.Options;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Lean.Hbt.Common.Helpers
{
    /// <summary>
    /// Excel导入导出帮助类
    /// </summary>
    public class HbtExcelHelper
    {
        private static HbtExcelOptions? _options;

        /// <summary>
        /// 设置Excel配置
        /// </summary>
        /// <param name="options">Excel配置选项</param>
        public static void Configure(IOptions<HbtExcelOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// 设置工作簿属性
        /// </summary>
        private static void SetWorkbookProperties(ExcelWorkbook workbook)
        {
            if (_options == null) return;

            var props = workbook.Properties;
            props.Author = _options.Author;
            props.Title = _options.Title;
            props.Subject = _options.Subject;
            props.Category = _options.Category;
            props.Keywords = _options.Keywords;
            props.Comments = _options.Comments;
            props.Status = _options.Status;
            props.Application = _options.Application;
            props.Company = _options.Company;
            props.Manager = _options.Manager;
            props.Created = DateTime.Now;
            props.Modified = DateTime.Now;
            props.LastModifiedBy = _options.Author;
        }

        #region 单Sheet导入导出

        /// <summary>
        /// 导出Excel(单个Sheet)
        /// </summary>
        /// <typeparam name="T">要导出的数据类型</typeparam>
        /// <param name="data">数据集合</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public static Task<byte[]> ExportAsync<T>(IEnumerable<T> data, string sheetName = "Sheet1") where T : class
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            SetWorkbookProperties(package.Workbook);
            ExportToSheetAsync(package, data, sheetName);
            return package.GetAsByteArrayAsync();
        }

        /// <summary>
        /// 导入Excel(单个Sheet)
        /// </summary>
        /// <typeparam name="T">要导入的数据类型</typeparam>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>数据集合</returns>
        public static Task<List<T>> ImportAsync<T>(Stream fileStream, string sheetName = "Sheet1") where T : class, new()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage(fileStream);
            return ImportFromSheetAsync<T>(package, sheetName);
        }

        #endregion 单Sheet导入导出

        #region 多Sheet导入导出

        /// <summary>
        /// 导出Excel(多个Sheet)
        /// </summary>
        /// <param name="sheets">Sheet数据字典，key为sheet名称，value为数据集合</param>
        /// <returns>Excel文件字节数组</returns>
        public static Task<byte[]> ExportMultiSheetAsync<T>(Dictionary<string, IEnumerable<T>> sheets) where T : class
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            SetWorkbookProperties(package.Workbook);

            foreach (var sheet in sheets)
            {
                ExportToSheetAsync(package, sheet.Value, sheet.Key);
            }

            return package.GetAsByteArrayAsync();
        }

        /// <summary>
        /// 导入Excel(多个Sheet)
        /// </summary>
        /// <typeparam name="T">要导入的数据类型</typeparam>
        /// <param name="fileStream">Excel文件流</param>
        /// <returns>数据字典，key为sheet名称，value为数据集合</returns>
        public static Task<Dictionary<string, List<T>>> ImportMultiSheetAsync<T>(Stream fileStream) where T : class, new()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage(fileStream);
            var result = new Dictionary<string, List<T>>();

            foreach (var worksheet in package.Workbook.Worksheets)
            {
                result[worksheet.Name] = ImportFromSheetAsync<T>(package, worksheet.Name).Result;
            }

            return Task.FromResult(result);
        }

        #endregion 多Sheet导入导出

        #region 模板导入导出

        /// <summary>
        /// 生成Excel导入模板
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        public static Task<byte[]> GenerateTemplateAsync<T>(string sheetName = "Sheet1") where T : class, new()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            SetWorkbookProperties(package.Workbook);
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // 获取属性信息
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false)
                .ToList();

            // 写入表头
            for (int i = 0; i < properties.Count; i++)
            {
                var displayName = properties[i].GetCustomAttribute<DisplayNameAttribute>()?.DisplayName
                    ?? properties[i].Name;
                var description = properties[i].GetCustomAttribute<DescriptionAttribute>()?.Description;

                // 设置表头
                var cell = worksheet.Cells[1, i + 1];
                cell.Value = displayName;

                // 添加注释说明
                if (!string.IsNullOrEmpty(description))
                {
                    var comment = cell.AddComment(description, _options.Author);
                    comment.AutoFit = true;
                }

                // 设置数据验证和格式
                var propertyType = properties[i].PropertyType;
                var column = $"{(char)('A' + i)}2:{(char)('A' + i)}1000";

                if (propertyType.IsEnum)
                {
                    // 枚举类型添加下拉列表
                    var enumValues = Enum.GetNames(propertyType);
                    var validation = worksheet.DataValidations.AddListValidation(column);
                    foreach (var value in enumValues)
                    {
                        validation.Formula.Values.Add(value);
                    }
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    // 布尔类型添加是/否下拉列表
                    var validation = worksheet.DataValidations.AddListValidation(column);
                    validation.Formula.Values.Add("是");
                    validation.Formula.Values.Add("否");
                }
                else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    // 日期类型添加日期格式验证
                    var validation = worksheet.DataValidations.AddDateTimeValidation(column);
                    validation.ShowErrorMessage = true;
                    validation.Error = "请输入有效的日期格式";
                    worksheet.Column(i + 1).Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                }
                else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?) ||
                         propertyType == typeof(double) || propertyType == typeof(double?) ||
                         propertyType == typeof(float) || propertyType == typeof(float?))
                {
                    // 数值类型添加数值验证
                    var validation = worksheet.DataValidations.AddDecimalValidation(column);
                    validation.ShowErrorMessage = true;
                    validation.Error = "请输入有效的数值";
                    worksheet.Column(i + 1).Style.Numberformat.Format = "#,##0.00";
                }
                else if (propertyType == typeof(int) || propertyType == typeof(int?) ||
                         propertyType == typeof(long) || propertyType == typeof(long?))
                {
                    // 整数类型添加整数验证
                    var validation = worksheet.DataValidations.AddIntegerValidation(column);
                    validation.ShowErrorMessage = true;
                    validation.Error = "请输入有效的整数";
                    worksheet.Column(i + 1).Style.Numberformat.Format = "#,##0";
                }
            }

            // 自动调整列宽
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArrayAsync();
        }

        #endregion 模板导入导出

        #region 私有辅助方法

        /// <summary>
        /// 导出数据到指定Sheet
        /// </summary>
        private static void ExportToSheetAsync<T>(ExcelPackage package, IEnumerable<T> data, string sheetName) where T : class
        {
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // 获取属性信息
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false)
                .ToList();

            // 写入表头
            for (int i = 0; i < properties.Count; i++)
            {
                var displayName = properties[i].GetCustomAttribute<DisplayNameAttribute>()?.DisplayName
                    ?? properties[i].Name;
                worksheet.Cells[1, i + 1].Value = displayName;
            }

            // 写入数据
            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < properties.Count; col++)
                {
                    var property = properties[col];
                    var value = property.GetValue(item);

                    // 特殊类型处理
                    if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        worksheet.Cells[row, col + 1].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                    }
                    else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                    {
                        worksheet.Cells[row, col + 1].Style.Numberformat.Format = "#,##0.00";
                    }

                    worksheet.Cells[row, col + 1].Value = value;
                }
                row++;
            }

            // 自动调整列宽
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // 冻结首行
            worksheet.View.FreezePanes(2, 1);
        }

        /// <summary>
        /// 从指定Sheet导入数据
        /// </summary>
        private static Task<List<T>> ImportFromSheetAsync<T>(ExcelPackage package, string sheetName) where T : class, new()
        {
            var worksheet = package.Workbook.Worksheets[sheetName];
            if (worksheet == null || worksheet.Dimension == null)
                throw new Exception($"找不到名为 {sheetName} 的工作表或工作表为空");

            var result = new List<T>();
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false)
                .ToList();

            // 获取表头与属性的映射关系
            var headerMap = new Dictionary<string, PropertyInfo>();
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                var headerValue = worksheet.Cells[1, col].Value?.ToString();
                if (string.IsNullOrEmpty(headerValue)) continue;

                var property = properties.FirstOrDefault(p =>
                    p.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName == headerValue
                    || p.Name == headerValue);

                if (property != null)
                {
                    headerMap[headerValue] = property;
                }
            }

            // 读取数据
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var item = new T();
                foreach (var header in headerMap)
                {
                    var property = header.Value;
                    var col = GetColumnByHeader(worksheet, header.Key);
                    if (col == -1) continue;

                    var cell = worksheet.Cells[row, col];
                    var cellValue = cell.Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue)) continue;

                    var value = ConvertValue(cellValue, property.PropertyType);
                    if (value != null)
                    {
                        property.SetValue(item, value);
                    }
                }
                result.Add(item);
            }

            return Task.FromResult(result);
        }

        private static int GetColumnByHeader(ExcelWorksheet worksheet, string headerText)
        {
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (worksheet.Cells[1, col].Value?.ToString() == headerText)
                    return col;
            }
            return -1;
        }

        private static object? ConvertValue(string value, Type targetType)
        {
            if (string.IsNullOrEmpty(value)) return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;

            try
            {
                if (targetType == typeof(string))
                    return value;

                if (targetType.IsEnum)
                    return Enum.Parse(targetType, value);

                if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                {
                    return DateTime.Parse(value);
                }

                if (targetType == typeof(bool) || targetType == typeof(bool?))
                {
                    var strValue = value.ToLower();
                    return strValue == "是" || strValue == "1" || strValue == "true";
                }

                return Convert.ChangeType(value, targetType);
            }
            catch
            {
                return null;
            }
        }

        #endregion 私有辅助方法
    }
}