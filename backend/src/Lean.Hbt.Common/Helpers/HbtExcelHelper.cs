//===================================================================
// 项目名 : Lean.Hbt 
// 文件名 : HbtExcelHelper.cs 
// 创建者 : Lean365
// 创建时间: 2024-01-16 21:50
// 版本号 : V1.0.0
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
using OfficeOpenXml;

namespace Lean.Hbt.Common.Helpers
{
    /// <summary>
    /// Excel导入导出帮助类
    /// </summary>
    public class HbtExcelHelper
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <typeparam name="T">要导出的数据类型</typeparam>
        /// <param name="data">数据集合</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public static async Task<byte[]> ExportAsync<T>(IEnumerable<T> data, string sheetName = "Sheet1") where T : class
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            
            using var package = new ExcelPackage();
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
                    var value = properties[col].GetValue(item);
                    worksheet.Cells[row, col + 1].Value = value;
                }
                row++;
            }

            // 自动调整列宽
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return await package.GetAsByteArrayAsync();
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <typeparam name="T">要导入的数据类型</typeparam>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>数据集合</returns>
        public static async Task<List<T>> ImportAsync<T>(Stream fileStream, string sheetName = "Sheet1") where T : class, new()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            
            using var package = new ExcelPackage(fileStream);
            var worksheet = package.Workbook.Worksheets[sheetName];
            if (worksheet == null)
                throw new Exception($"找不到名为 {sheetName} 的工作表");

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
                    var col = GetColumnByHeader(worksheet, header.Key);
                    if (col == -1) continue;

                    var cell = worksheet.Cells[row, col];
                    if (cell.Value == null) continue;

                    var property = header.Value;
                    var value = ConvertValue(cell.Value, property.PropertyType);
                    property.SetValue(item, value);
                }
                result.Add(item);
            }

            return result;
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

        private static object ConvertValue(object value, Type targetType)
        {
            if (value == null) return null;

            try
            {
                if (targetType == typeof(string))
                    return value.ToString();

                if (targetType.IsEnum)
                    return Enum.Parse(targetType, value.ToString());

                if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                {
                    if (value is DateTime dateValue)
                        return dateValue;
                    return DateTime.Parse(value.ToString());
                }

                return Convert.ChangeType(value, targetType);
            }
            catch
            {
                return null;
            }
        }
    }
} 