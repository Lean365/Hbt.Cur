#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表服务实现
//===================================================================

using Lean.Hbt.Application.Services.Generator.CodeGenerator;
using Lean.Hbt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;
using Lean.Hbt.Domain.Entities.Generator;
using Lean.Hbt.Application.Dtos.Generator;
using System.Linq.Expressions;

namespace Lean.Hbt.Application.Services.Generator;

/// <summary>
/// 代码生成表服务实现
/// </summary>
public class HbtGenTableService : HbtBaseService, IHbtGenTableService
{
    private readonly IHbtRepository<HbtGenTable> _tableRepository;
    private readonly IHbtRepository<HbtGenColumn> _columnRepository;
    private readonly IHbtCodeGeneratorService _codeGeneratorService;
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tableRepository">表仓储</param>
    /// <param name="columnRepository">列仓储</param>
    /// <param name="codeGeneratorService">代码生成服务</param>
    /// <param name="db">数据库客户端</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    public HbtGenTableService(
        IHbtRepository<HbtGenTable> tableRepository,
        IHbtRepository<HbtGenColumn> columnRepository,
        IHbtCodeGeneratorService codeGeneratorService,
        ISqlSugarClient db,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
    {
        _tableRepository = tableRepository;
        _columnRepository = columnRepository;
        _codeGeneratorService = codeGeneratorService;
        _db = db;
    }

    #region 基础操作

    /// <summary>
    /// 根据ID获取表信息
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>表信息</returns>
    public async Task<HbtGenTableDto?> GetByIdAsync(long id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null)
        {
            return null;
        }

        return table.Adapt<HbtGenTableDto>();
    }

    /// <summary>
    /// 构建查询条件
    /// </summary>
    private Expression<Func<HbtGenTable, bool>> HbtGenTableQueryExpression(HbtGenTableQueryDto query)
    {
        var exp = Expressionable.Create<HbtGenTable>();

        if (!string.IsNullOrEmpty(query.TableName))
            exp.And(x => x.TableName.Contains(query.TableName));

        if (!string.IsNullOrEmpty(query.TableComment))
            exp.And(x => x.TableComment.Contains(query.TableComment));

        if (query.Status.HasValue)
            exp.And(x => x.Status == query.Status.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 获取分页表列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    public async Task<HbtPagedResult<HbtGenTableDto>> GetListAsync(HbtGenTableQueryDto input)
    {
        var exp = HbtGenTableQueryExpression(input);

        var result = await _tableRepository.GetPagedListAsync(
            exp,
            input.PageIndex,
            input.PageSize,
            x => x.Id,
            OrderByType.Asc);

        return new HbtPagedResult<HbtGenTableDto>
        {
            Rows = result.Rows.Adapt<List<HbtGenTableDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = input.PageIndex,
            PageSize = input.PageSize
        };
    }

    /// <summary>
    /// 创建表信息
    /// </summary>
    /// <param name="input">表信息</param>
    /// <returns>创建结果</returns>
    public async Task<HbtGenTableDto> CreateAsync(HbtGenTableDto input)
    {
        // 验证表名是否已存在
        var existTable = await _tableRepository.GetFirstAsync(x => x.TableName == input.TableName);
        if (existTable != null)
        {
            throw new HbtException($"表名[{input.TableName}]已存在");
        }

        var table = input.Adapt<HbtGenTable>();
        table.CreateBy = UserName;
        table.CreateTime = DateTime.Now;
        table.UpdateBy = UserName;
        table.UpdateTime = DateTime.Now;

        var result = await _tableRepository.CreateAsync(table);
        if (result <= 0)
        {
            throw new HbtException("创建表失败");
        }

        return table.Adapt<HbtGenTableDto>();
    }

    /// <summary>
    /// 更新表信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表信息</returns>
    public async Task<HbtGenTableDto> UpdateAsync(HbtGenTableUpdateDto input)
    {
        var table = await _tableRepository.GetByIdAsync(input.TableId);
        if (table == null)
        {
            throw new HbtException($"表[{input.TableId}]不存在");
        }

        // 验证表名是否已存在
        if (table.TableName != input.TableName)
        {
            var existTable = await _tableRepository.GetFirstAsync(x => x.TableName == input.TableName);
            if (existTable != null)
            {
                throw new HbtException($"表名[{input.TableName}]已存在");
            }
        }

        table.TableName = input.TableName;
        table.TableComment = input.TableComment;
        table.ClassName = input.ClassName;
        table.BaseNamespace = input.BaseNamespace;
        table.ModuleName = input.ModuleName;
        table.BusinessName = input.BusinessName;
        table.FunctionName = input.FunctionName;
        table.Author = input.Author;
        table.GenMode = input.GenMode;
        table.GenPath = input.GenPath;
        table.Options = input.Options;
        table.Status = input.Status;
        table.Remark = input.Remark;
        table.UpdateBy = UserName;
        table.UpdateTime = DateTime.Now;

        var result = await _tableRepository.UpdateAsync(table);
        if (result <= 0)
        {
            throw new HbtException("更新表失败");
        }

        return table.Adapt<HbtGenTableDto>();
    }

    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null)
        {
            throw new HbtException($"表[{id}]不存在");
        }

        return await _tableRepository.DeleteAsync(table) > 0;
    }

    /// <summary>
    /// 批量删除表
    /// </summary>
    /// <param name="ids">表ID集合</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            throw new HbtException("请选择要删除的表");
        }

        var tables = await _tableRepository.GetListAsync(x => ids.Contains(x.Id));
        if (tables?.Count > 0)
        {
            return await _tableRepository.DeleteRangeAsync(tables) > 0;
        }

        return false;
    }

    #endregion 基础操作

    #region 列操作

    /// <summary>
    /// 获取表列列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>列列表</returns>
    public async Task<List<HbtGenColumnDto>> GetColumnListAsync(long tableId)
    {
        var columns = await _columnRepository.GetListAsync(x => x.TableId == tableId);
        return columns.Adapt<List<HbtGenColumnDto>>();
    }

    /// <summary>
    /// 更新表列信息
    /// </summary>
    /// <param name="input">列信息</param>
    /// <returns>是否更新成功</returns>
    public async Task<bool> UpdateColumnAsync(HbtGenColumnUpdateDto input)
    {
        var column = await _columnRepository.GetByIdAsync(input.GenColumnId);
        if (column == null)
        {
            throw new HbtException($"列[{input.GenColumnId}]不存在");
        }

        column.ColumnComment = input.ColumnComment;
        column.DbColumnType = input.DbColumnType;
        column.CsharpType = input.CsharpType;
        column.CsharpField = input.CsharpField;
        column.IsPrimaryKey = input.IsPrimaryKey;
        column.IsIncrement = input.IsIncrement;
        column.IsRequired = input.IsRequired;
        column.IsInsert = input.IsInsert;
        column.IsEdit = input.IsEdit;
        column.IsList = input.IsList;
        column.IsQuery = input.IsQuery;
        column.QueryType = input.QueryType;
        column.DisplayType = input.DisplayType;
        column.DictType = input.DictType;
        column.OrderNum = input.OrderNum;
        column.UpdateBy = UserName;
        column.UpdateTime = DateTime.Now;

        return await _columnRepository.UpdateAsync(column) > 0;
    }

    #endregion 列操作

    #region 导入导出

    /// <summary>
    /// 导入表
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入的表列表</returns>
    public async Task<List<HbtGenTableDto>> ImportTablesAsync(HbtGenTableImportDto input)
    {
        try
        {
            var table = new HbtGenTable
            {
                DatabaseName = input.DatabaseName,
                TableName = input.TableName,
                TableComment = input.TableComment,
                ClassName = input.ClassName,
                Namespace = input.Namespace,
                BaseNamespace = input.BaseNamespace,
                CsharpTypeName = input.CsharpTypeName,
                ModuleName = input.ModuleName,
                BusinessName = input.BusinessName,
                FunctionName = input.FunctionName,
                Author = input.Author,
                GenMode = input.GenMode,
                GenPath = input.GenPath,
                Options = input.Options,
                CreateBy = UserName,
                CreateTime = DateTime.Now,
                UpdateBy = UserName,
                UpdateTime = DateTime.Now
            };

            var result = await _tableRepository.CreateAsync(table);
            if (result <= 0)
            {
                throw new HbtException("导入表失败");
            }

            return new List<HbtGenTableDto> { table.Adapt<HbtGenTableDto>() };
        }
        catch (Exception ex)
        {
            _logger.Error(L("GenTable.Import.Failed"), ex);
            throw new HbtException(L("GenTable.Import.Failed"), ex);
        }
    }

    /// <summary>
    /// 导出表
    /// </summary>
    /// <returns>导出的表列表</returns>
    public async Task<List<HbtGenTableExportDto>> ExportTablesAsync()
    {
        try
        {
            var tables = await _tableRepository.GetListAsync();
            var result = new List<HbtGenTableExportDto>();

            foreach (var table in tables)
            {
                var dto = table.Adapt<HbtGenTableExportDto>();
                var columns = await _columnRepository.GetListAsync(x => x.TableId == table.Id);
                dto.Columns = columns.Adapt<List<HbtGenColumnDto>>();
                result.Add(dto);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(L("GenTable.Export.Failed"), ex);
            throw new HbtException(L("GenTable.Export.Failed"), ex);
        }
    }

    /// <summary>
    /// 获取导入表模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "Sheet1")
    {
        return await HbtExcelHelper.GenerateTemplateAsync<HbtGenTable>(sheetName);
    }

    #endregion 导入导出

    #region 表操作

    /// <summary>
    /// 获取数据库列表
    /// </summary>
    /// <returns>数据库列表</returns>
    public Task<List<string>> GetDatabaseListAsync()
    {
        var databases = _db.DbMaintenance.GetDataBaseList();
        return Task.FromResult(databases);
    }

    /// <summary>
    /// 获取表列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <returns>表列表</returns>
    public Task<List<string>> GetTableListAsync(string databaseName)
    {
        var tables = _db.DbMaintenance.GetTableInfoList();
        return Task.FromResult(tables.Select(x => x.Name).ToList());
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <param name="tableName">表名</param>
    /// <returns>字段列表</returns>
    public Task<List<HbtGenColumnDto>> GetTableColumnListAsync(string databaseName, string tableName)
    {
        var columns = _db.DbMaintenance.GetColumnInfosByTableName(tableName);
        return Task.FromResult(columns.Select(x => new HbtGenColumnDto
        {
            ColumnName = x.DbColumnName,
            ColumnComment = x.ColumnDescription,
            DbColumnType = x.DataType,
            CsharpType = GetCsharpType(x.DataType),
            CsharpColumn = ToPascalCase(x.DbColumnName),
            CsharpLength = x.Length,
            CsharpDecimalDigits = x.DecimalDigits,
            IsIncrement = x.IsIdentity ? 1 : 0,
            IsPrimaryKey = x.IsPrimarykey ? 1 : 0,
            IsRequired = x.IsNullable ? 0 : 1,
            IsInsert = 1,
            IsEdit = 1,
            IsList = 1,
            IsQuery = 0,
            QueryType = "EQ",
            IsSort = 0,
            IsExport = 1,
            DisplayType = "input",
            DictType = string.Empty,
            OrderNum = 0
        }).ToList());
    }

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否同步成功</returns>
    public async Task<bool> SyncTableAsync(long id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null)
        {
            throw new Exception($"未找到ID为{id}的表");
        }

        // 同步表信息
        var tableInfo = _db.DbMaintenance.GetTableInfoList();
        var currentTable = tableInfo.FirstOrDefault(x => x.Name == table.TableName);
        if (currentTable == null)
        {
            throw new Exception($"未找到表:{table.TableName}");
        }

        // 更新表信息
        table.TableComment = currentTable.Description;
        table.UpdateTime = DateTime.Now;
        table.UpdateBy = UserName;
        await _tableRepository.UpdateAsync(table);

        // 同步字段信息
        var columns = _db.DbMaintenance.GetColumnInfosByTableName(table.TableName);
        var existingColumns = await _columnRepository.GetListAsync(x => x.TableId == table.Id);

        // 删除不存在的字段
        var columnNames = columns.Select(x => x.DbColumnName).ToList();
        foreach (var column in existingColumns)
        {
            if (!columnNames.Contains(column.ColumnName))
            {
                await _columnRepository.DeleteAsync(column.Id);
            }
        }

        // 更新或添加字段
        foreach (var column in columns)
        {
            var existingColumn = existingColumns.FirstOrDefault(x => x.ColumnName == column.DbColumnName);
            if (existingColumn == null)
            {
                // 添加新字段
                var newColumn = new HbtGenColumn
                {
                    TableId = table.Id,
                    ColumnName = column.DbColumnName,
                    ColumnComment = column.ColumnDescription,
                    DbColumnType = column.DataType,
                    CsharpType = GetCsharpType(column.DataType),
                    CsharpField = ToPascalCase(column.DbColumnName),
                    CsharpLength = column.Length,
                    CsharpDecimalDigits = column.DecimalDigits,
                    IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                    IsIncrement = column.IsIdentity ? 1 : 0,
                    IsRequired = column.IsNullable ? 0 : 1,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateBy = UserName,
                    UpdateBy = UserName
                };
                await _columnRepository.CreateAsync(newColumn);
            }
            else
            {
                // 更新现有字段
                existingColumn.ColumnComment = column.ColumnDescription;
                existingColumn.DbColumnType = column.DataType;
                existingColumn.CsharpType = GetCsharpType(column.DataType);
                existingColumn.CsharpLength = column.Length;
                existingColumn.CsharpDecimalDigits = column.DecimalDigits;
                existingColumn.IsPrimaryKey = column.IsPrimarykey ? 1 : 0;
                existingColumn.IsIncrement = column.IsIdentity ? 1 : 0;
                existingColumn.IsRequired = column.IsNullable ? 0 : 1;
                existingColumn.UpdateTime = DateTime.Now;
                existingColumn.UpdateBy = UserName;
                await _columnRepository.UpdateAsync(existingColumn);
            }
        }

        return true;
    }

    #endregion 表操作

    #region 代码生成

    /// <summary>
    /// 预览代码
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>代码预览结果</returns>
    public async Task<Dictionary<string, string>> PreviewCodeAsync(long id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null)
            throw new HbtException($"表[{id}]不存在");

        return await _codeGeneratorService.PreviewCodeAsync(table);
    }

    /// <summary>
    /// 生成代码
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否生成成功</returns>
    public async Task<bool> GenerateCodeAsync(long id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null)
            throw new HbtException($"表[{id}]不存在");

        try
        {
            return await _codeGeneratorService.GenerateCodeAsync(table);
        }
        catch (Exception ex)
        {
            _logger.Error("生成代码失败，表ID：{0}", id, ex);
            return false;
        }
    }

    /// <summary>
    /// 批量生成代码
    /// </summary>
    /// <param name="ids">表ID集合</param>
    /// <returns>生成结果</returns>
    public async Task<string> BatchGenerateCodeAsync(long[] ids)
    {
        var successCount = 0;
        var failCount = 0;
        foreach (var id in ids)
        {
            try
            {
                if (await GenerateCodeAsync(id))
                    successCount++;
                else
                    failCount++;
            }
            catch (Exception ex)
            {
                _logger.Error("生成代码失败，表ID：{0}", id, ex);
                failCount++;
            }
        }
        return $"生成完成，成功：{successCount}，失败：{failCount}";
    }

    /// <summary>
    /// 下载代码
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>代码文件字节数组</returns>
    public async Task<byte[]> DownloadCodeAsync(long id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null)
            throw new HbtException($"表[{id}]不存在");

        return await _codeGeneratorService.DownloadCodeAsync(table);
    }

    #endregion 代码生成

    #region 私有方法

    /// <summary>
    /// 获取C#类型
    /// </summary>
    /// <param name="dbType">数据库类型</param>
    /// <returns>C#类型</returns>
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
    /// <param name="input">输入字符串</param>
    /// <returns>Pascal命名字符串</returns>
    private string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var words = input.Split(new[] { '_', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(words.Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
    }

    #endregion 私有方法
}