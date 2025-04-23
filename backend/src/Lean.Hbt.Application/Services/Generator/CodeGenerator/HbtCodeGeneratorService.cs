using System.IO.Compression;
using System.Linq.Expressions;
using Lean.Hbt.Application.Services.Generator.CodeGenerator.Models;
using Lean.Hbt.Application.Services.Generator.CodeGenerator.Templates;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Lean.Hbt.Application.Services.Generator.CodeGenerator;

/// <summary>
/// 代码生成服务实现
/// </summary>
public class HbtCodeGeneratorService : IHbtCodeGeneratorService
{
    private readonly IHbtTemplateEngine _templateEngine;
    private readonly IHbtLogger _logger;
    private readonly string _outputPath;
    private readonly ISqlSugarClient _db;
    private readonly HbtCodeGenerationConfig _config;
    private readonly IHbtRepository<HbtGenTable> _tableRepository;
    private readonly IHbtRepository<HbtGenColumn> _columnRepository;
    private readonly IHbtCurrentUser _currentUser;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="templateEngine"> 模板引擎</param>
    /// <param name="logger"> 日志</param>
    /// <param name="db"> 数据库连接</param>
    /// <param name="configuration"> 配置</param>
    /// <param name="webHostEnvironment"> WebHost环境</param>
    /// <param name="tableRepository"> 表信息仓储</param>
    /// <param name="columnRepository">  字段信息仓储</param>
    /// <param name="currentUser"> 当前用户</param>
    public HbtCodeGeneratorService(
        IHbtTemplateEngine templateEngine,
        IHbtLogger logger,
        ISqlSugarClient db,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment,
        IHbtRepository<HbtGenTable> tableRepository,
        IHbtRepository<HbtGenColumn> columnRepository,
        IHbtCurrentUser currentUser)
    {
        _templateEngine = templateEngine;
        _logger = logger;
        _db = db;
        _config = HbtCodeGenerationConfig.FromConfiguration(configuration, webHostEnvironment);
        _tableRepository = tableRepository;
        _columnRepository = columnRepository;
        _currentUser = currentUser;

        // 从配置中获取输出路径，如果没有则使用默认值
        _outputPath = configuration.GetValue<string>("CodeGenerator:OutputPath") ?? "wwwroot/generator/output";

        // 确保输出路径是相对于应用程序根目录的
        _outputPath = Path.Combine(webHostEnvironment.ContentRootPath, _outputPath);

        if (!Directory.Exists(_outputPath))
        {
            Directory.CreateDirectory(_outputPath);
        }
    }

    /// <summary>
    /// 获取当前数据库中的所有表
    /// </summary>
    /// <returns>表信息列表</returns>
    public async Task<List<HbtGenTable>> GetAllTablesFromDatabaseAsync()
    {
        try
        {
            _logger.Info("开始获取数据库中的所有表");

            // 获取数据库表信息
            var tableInfos = _db.DbMaintenance.GetTableInfoList();
            if (!tableInfos.Any())
            {
                _logger.Warn("未找到任何表");
                return new List<HbtGenTable>();
            }

            var tables = new List<HbtGenTable>();
            foreach (var tableInfo in tableInfos)
            {
                // 获取表字段信息
                var columns = _db.DbMaintenance.GetColumnInfosByTableName(tableInfo.Name);

                // 创建表信息
                var table = new HbtGenTable
                {
                    DatabaseName = _db.CurrentConnectionConfig.ConnectionString.Split(';').FirstOrDefault(x => x.StartsWith("Database=", StringComparison.OrdinalIgnoreCase))?.Split('=')[1] ?? "Unknown",
                    TableName = tableInfo.Name,
                    TableComment = tableInfo.Description,
                    ClassName = ToPascalCase(tableInfo.Name),
                    Namespace = "Lean.Hbt.Domain.Entities",
                    BaseNamespace = "Lean.Hbt",
                    CsharpTypeName = ToPascalCase(tableInfo.Name),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateBy = _currentUser.UserName ?? "Lean365",
                    UpdateBy = _currentUser.UserName ?? "Lean365"
                };

                // 保存表信息
                await _tableRepository.CreateAsync(table);

                // 创建字段信息
                foreach (var column in columns)
                {
                    var genColumn = new HbtGenColumn
                    {
                        TableId = table.Id,
                        ColumnName = column.DbColumnName,
                        ColumnComment = column.ColumnDescription,
                        DbColumnType = column.DataType,
                        CsharpType = GetCsharpType(column.DataType),
                        CsharpColumn = ToPascalCase(column.DbColumnName),
                        CsharpLength = column.Length,
                        CsharpDecimalDigits = column.DecimalDigits,
                        IsIncrement = column.IsIdentity ? 1 : 0,
                        IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                        IsRequired = column.IsNullable ? 0 : 1,
                        IsInsert = 1,
                        IsEdit = 1,
                        IsList = 1,
                        IsQuery = 0,
                        QueryType = "EQ",
                        IsSort = 0,
                        IsExport = 1,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        CreateBy = _currentUser.UserName ?? "Lean365",
                        UpdateBy = _currentUser.UserName ?? "Lean365"
                    };

                    await _columnRepository.CreateAsync(genColumn);
                }

                tables.Add(table);
            }

            _logger.Info($"成功获取到 {tables.Count} 个表");
            return tables;
        }
        catch (Exception ex)
        {
            _logger.Error($"获取数据库表失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 从数据库同步到页面
    /// </summary>
    public async Task<bool> SyncFromDatabaseAsync(HbtGenTable table)
    {
        try
        {
            _logger.Info($"开始从数据库同步到页面,表名:{table.TableName}");

            // 获取数据库表信息
            var tableInfo = _db.DbMaintenance.GetTableInfoList();
            var currentTable = tableInfo.FirstOrDefault(x => x.Name == table.TableName);
            if (currentTable == null)
            {
                _logger.Warn($"未找到表:{table.TableName}");
                return false;
            }

            // 获取表字段信息
            var columns = _db.DbMaintenance.GetColumnInfosByTableName(table.TableName);
            if (!columns.Any())
            {
                _logger.Warn($"表 {table.TableName} 没有字段信息");
                return false;
            }

            // 更新表信息
            table.TableComment = currentTable.Description;
            table.UpdateTime = DateTime.Now;

            // 更新字段信息
            var columnList = columns.Select(col => new HbtGenColumn
            {
                TableId = table.Id,
                ColumnName = col.DbColumnName,
                ColumnComment = col.ColumnDescription,
                DbColumnType = col.DataType,
                CsharpType = GetCsharpType(col.DataType),
                CsharpColumn = ToPascalCase(col.DbColumnName),
                CsharpLength = col.Length,
                CsharpDecimalDigits = col.DecimalDigits,
                IsIncrement = col.IsIdentity ? 1 : 0,
                IsPrimaryKey = col.IsPrimarykey ? 1 : 0,
                IsRequired = col.IsNullable ? 0 : 1,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            }).ToList();

            // 保存表信息
            await _tableRepository.UpdateAsync(table);

            // 删除原有字段
            Expression<Func<HbtGenColumn, bool>> deleteExpression = x => x.TableId == table.Id;
            await _columnRepository.DeleteAsync(deleteExpression);

            // 保存新字段
            foreach (var column in columnList)
            {
                await _columnRepository.CreateAsync(column);
            }

            _logger.Info($"从数据库同步到页面成功,表名:{table.TableName},字段数:{columnList.Count}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"从数据库同步到页面失败,表名:{table.TableName}", ex);
            return false;
        }
    }

    /// <summary>
    /// 从页面同步到数据库
    /// </summary>
    public async Task<bool> SyncToDatabaseAsync(HbtGenTable table)
    {
        try
        {
            _logger.Info($"开始从页面同步到数据库,表名:{table.TableName}");

            // 更新表注释
            var updateTableSql = GenerateUpdateTableSql(table);
            await _db.Ado.ExecuteCommandAsync(updateTableSql);
            _logger.Info($"更新表成功,表名:{table.TableName}");

            // 同步字段
            var columns = _db.DbMaintenance.GetColumnInfosByTableName(table.TableName);
            var existingColumns = columns.Select(x => x.DbColumnName).ToList();
            var newColumns = table.Columns?.Where(x => !existingColumns.Contains(x.ColumnName)).ToList() ?? new List<HbtGenColumn>();
            var updateColumns = table.Columns?.Where(x => existingColumns.Contains(x.ColumnName)).ToList() ?? new List<HbtGenColumn>();

            // 添加新字段
            foreach (var column in newColumns)
            {
                var addColumnSql = GenerateAddColumnSql(table.TableName, column);
                await _db.Ado.ExecuteCommandAsync(addColumnSql);
                _logger.Info($"添加字段成功,表名:{table.TableName},字段名:{column.ColumnName}");
            }

            // 更新现有字段
            foreach (var column in updateColumns)
            {
                var updateColumnSql = GenerateUpdateColumnSql(table.TableName, column);
                await _db.Ado.ExecuteCommandAsync(updateColumnSql);
                _logger.Info($"更新字段成功,表名:{table.TableName},字段名:{column.ColumnName}");
            }

            _logger.Info($"从页面同步到数据库成功,表名:{table.TableName}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"从页面同步到数据库失败,表名:{table.TableName}", ex);
            return false;
        }
    }

    /// <summary>
    /// 生成代码
    /// </summary>
    public async Task<bool> GenerateCodeAsync(HbtGenTable table)
    {
        try
        {
            _logger.Info($"开始生成代码,表名:{table.TableName}");

            // 准备生成模型
            var model = new HbtGeneratorModel
            {
                Table = table,
                Config = _config,
                Options = new Models.HbtGeneratorOptions
                {
                    Author = _config.Author,
                    ModuleName = _config.ModuleName,
                    PackageName = _config.PackageName,
                    BaseNamespace = _config.BaseNamespace,
                    GenPath = _config.GenPath,
                    GenerateController = true,
                    GenerateService = true,
                    GenerateRepository = true,
                    GenerateEntity = true,
                    GenerateDto = true,
                    GenerateFrontend = true,
                    GenerateApiDoc = true,
                    IsTree = false,
                    IsMasterDetail = false
                }
            };

            // 生成代码
            foreach (var template in _config.Templates)
            {
                _logger.Debug($"开始生成模板:{template.TemplateName}");

                // 渲染模板
                var content = await _templateEngine.RenderAsync(template.TemplateContent, model);

                // 生成文件
                var filePath = Path.Combine(_outputPath, _config.GenPath, template.FileName);
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                await File.WriteAllTextAsync(filePath, content);
                _logger.Info($"模板生成成功:{template.TemplateName},文件路径:{filePath}");
            }

            _logger.Info($"代码生成成功,表名:{table.TableName}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"代码生成失败,表名:{table.TableName}", ex);
            return false;
        }
    }

    /// <summary>
    /// 预览代码
    /// </summary>
    public async Task<Dictionary<string, string>> PreviewCodeAsync(HbtGenTable table)
    {
        try
        {
            _logger.Info($"开始预览代码,表名:{table.TableName}");

            // 准备生成模型
            var model = new HbtGeneratorModel
            {
                Table = table,
                Config = _config,
                Options = new Models.HbtGeneratorOptions
                {
                    Author = _config.Author,
                    ModuleName = _config.ModuleName,
                    PackageName = _config.PackageName,
                    BaseNamespace = _config.BaseNamespace,
                    GenPath = _config.GenPath,
                    GenerateController = true,
                    GenerateService = true,
                    GenerateRepository = true,
                    GenerateEntity = true,
                    GenerateDto = true,
                    GenerateFrontend = true,
                    GenerateApiDoc = true,
                    IsTree = false,
                    IsMasterDetail = false
                }
            };

            var result = new Dictionary<string, string>();

            // 预览代码
            foreach (var template in _config.Templates)
            {
                _logger.Debug($"开始预览模板:{template.TemplateName}");

                // 渲染模板
                var content = await _templateEngine.RenderAsync(template.TemplateContent, model);
                result[template.FileName] = content;

                _logger.Info($"模板预览成功:{template.TemplateName}");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"代码预览失败,表名:{table.TableName}", ex);
            return new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// 下载代码
    /// </summary>
    public async Task<byte[]> DownloadCodeAsync(HbtGenTable table)
    {
        try
        {
            _logger.Info($"开始下载代码,表名:{table.TableName}");

            // 准备生成模型
            var model = new HbtGeneratorModel
            {
                Table = table,
                Config = _config,
                Options = new Models.HbtGeneratorOptions
                {
                    Author = _config.Author,
                    ModuleName = _config.ModuleName,
                    PackageName = _config.PackageName,
                    BaseNamespace = _config.BaseNamespace,
                    GenPath = _config.GenPath
                }
            };

            // 创建ZIP文件
            using var ms = new MemoryStream();
            using (var archive = new ZipArchive(ms, ZipArchiveMode.Create))
            {
                foreach (var template in _config.Templates)
                {
                    // 渲染模板
                    var content = await _templateEngine.RenderAsync(template.TemplateContent, model);

                    // 添加到ZIP
                    var filePath = Path.Combine(_config.GenPath, template.FileName);
                    var entry = archive.CreateEntry(filePath);
                    using var entryStream = entry.Open();
                    using var writer = new StreamWriter(entryStream);
                    await writer.WriteAsync(content);
                }
            }

            var zipBytes = ms.ToArray();
            _logger.Info($"代码下载成功,ZIP文件大小:{zipBytes.Length}字节");
            return zipBytes;
        }
        catch (Exception ex)
        {
            _logger.Error($"代码下载失败,表名:{table.TableName}", ex);
            return Array.Empty<byte>();
        }
    }

    #region 私有方法

    /// <summary>
    /// 生成更新表SQL
    /// </summary>
    private string GenerateUpdateTableSql(HbtGenTable table)
    {
        return $"ALTER TABLE [{table.TableName}] MODIFY COMMENT '{table.TableComment}'";
    }

    /// <summary>
    /// 生成添加字段SQL
    /// </summary>
    private string GenerateAddColumnSql(string tableName, HbtGenColumn column)
    {
        return $"ALTER TABLE [{tableName}] ADD [{column.ColumnName}] {column.DbColumnType}" +
               (column.CsharpLength > 0 ? $"({column.CsharpLength})" : "") +
               (column.IsRequired == 1 ? " NOT NULL" : " NULL") +
               (column.IsPrimaryKey == 1 ? " PRIMARY KEY" : "") +
               (column.IsIncrement == 1 ? " IDENTITY(1,1)" : "") +
               (string.IsNullOrEmpty(column.ColumnComment) ? "" : $" COMMENT '{column.ColumnComment}'");
    }

    /// <summary>
    /// 生成更新字段SQL
    /// </summary>
    private string GenerateUpdateColumnSql(string tableName, HbtGenColumn column)
    {
        return $"ALTER TABLE [{tableName}] ALTER COLUMN [{column.ColumnName}] {column.DbColumnType}" +
               (column.CsharpLength > 0 ? $"({column.CsharpLength})" : "") +
               (column.IsRequired == 1 ? " NOT NULL" : " NULL") +
               (string.IsNullOrEmpty(column.ColumnComment) ? "" : $" COMMENT '{column.ColumnComment}'");
    }

    /// <summary>
    /// 获取C#类型
    /// </summary>
    private string GetCsharpType(string dbType)
    {
        return dbType.ToLower() switch
        {
            "int" => "int",
            "bigint" => "long",
            "smallint" => "short",
            "tinyint" => "byte",
            "decimal" => "decimal",
            "numeric" => "decimal",
            "float" => "float",
            "real" => "double",
            "datetime" => "DateTime",
            "date" => "DateTime",
            "time" => "TimeSpan",
            "char" => "string",
            "varchar" => "string",
            "nvarchar" => "string",
            "text" => "string",
            "ntext" => "string",
            "bit" => "bool",
            "uniqueidentifier" => "Guid",
            _ => "string"
        };
    }

    /// <summary>
    /// 转换为Pascal命名
    /// </summary>
    private string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var words = input.Split(new[] { '_', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(words.Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
    }

    #endregion 私有方法
}