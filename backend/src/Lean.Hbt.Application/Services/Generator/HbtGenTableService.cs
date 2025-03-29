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
using System.Linq.Expressions;

namespace Lean.Hbt.Application.Services.Generator;

/// <summary>
/// 代码生成表服务实现
/// </summary>
public class HbtGenTableService : IHbtGenTableService
{
    private readonly IHbtRepository<HbtGenTable> _tableRepository;
    private readonly IHbtRepository<HbtGenColumn> _columnRepository;
    private readonly IHbtCodeGeneratorService _codeGeneratorService;
    private readonly ISqlSugarClient _db;
    private readonly ILogger<HbtGenTableService> _logger;
    private readonly IHbtCurrentUser _currentUser;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tableRepository">表仓储</param>
    /// <param name="columnRepository">字段仓储</param>
    /// <param name="codeGeneratorService">代码生成服务</param>
    /// <param name="db">数据库客户端</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    public HbtGenTableService(
        IHbtRepository<HbtGenTable> tableRepository,
        IHbtRepository<HbtGenColumn> columnRepository,
        IHbtCodeGeneratorService codeGeneratorService,
        ISqlSugarClient db,
        ILogger<HbtGenTableService> logger,
        IHbtCurrentUser currentUser)
    {
        _tableRepository = tableRepository;
        _columnRepository = columnRepository;
        _codeGeneratorService = codeGeneratorService;
        _db = db;
        _logger = logger;
        _currentUser = currentUser;
    }

    #region 基础操作

    /// <summary>
    /// 根据ID获取表信息
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>表信息</returns>
    public async Task<HbtGenTableDto> GetByIdAsync(long id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null)
            throw new HbtException($"表[{id}]不存在");

        return table.Adapt<HbtGenTableDto>();
    }

    /// <summary>
    /// 获取分页表列表
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <returns>分页结果</returns>
    public async Task<HbtPagedResult<HbtGenTableDto>> GetListAsync(HbtGenTableQueryDto query)
    {
        var exp = Expressionable.Create<HbtGenTable>();

        if (!string.IsNullOrEmpty(query.TableName))
            exp = exp.And(x => x.TableName.Contains(query.TableName));

        if (!string.IsNullOrEmpty(query.TableComment))
            exp = exp.And(x => x.TableComment.Contains(query.TableComment));

        var result = await _tableRepository.GetPagedListAsync(
            exp.ToExpression(),
            query.PageIndex,
            query.PageSize,
            x => x.CreateTime,
            OrderByType.Desc);

        return new HbtPagedResult<HbtGenTableDto>
        {
            Rows = result.Rows.Adapt<List<HbtGenTableDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>字段列表</returns>
    public async Task<List<HbtGenColumnDto>> GetColumnListAsync(long tableId)
    {
        var columns = await _columnRepository.GetListAsync(x => x.TableId == tableId);
        return columns.Adapt<List<HbtGenColumnDto>>();
    }

    /// <summary>
    /// 创建表信息
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建结果</returns>
    public async Task<HbtGenTableDto> CreateAsync(HbtGenTableDto input)
    {
        var entity = input.Adapt<HbtGenTable>();
        entity.CreateTime = DateTime.Now;
        entity.UpdateTime = DateTime.Now;
        entity.CreateBy = _currentUser.UserName ?? "Hbt365";
        entity.UpdateBy = _currentUser.UserName ?? "Hbt365";
        
        await _tableRepository.CreateAsync(entity);
        return entity.Adapt<HbtGenTableDto>();
    }

    /// <summary>
    /// 更新表信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表信息</returns>
    public async Task<HbtGenTableDto> UpdateAsync(HbtGenTableUpdateDto input)
    {
        var table = await _tableRepository.GetByIdAsync(input.Id);
        if (table == null)
        {
            throw new Exception($"未找到ID为{input.Id}的表");
        }

        // 更新表信息
        input.Adapt(table);
        table.UpdateTime = DateTime.Now;
        table.UpdateBy = _currentUser.UserName ?? "Hbt365";

        await _tableRepository.UpdateAsync(table);
        return table.Adapt<HbtGenTableDto>();
    }

    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        // 删除表信息
        await _tableRepository.DeleteAsync(id);

        // 删除关联的字段信息
        var columns = await _columnRepository.GetListAsync(x => x.TableId == id);
        foreach (var column in columns)
        {
            await _columnRepository.DeleteAsync(column.Id);
        }

        return true;
    }

    #endregion 基础操作

    #region 表操作

    /// <summary>
    /// 导入表
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入的表列表</returns>
    public async Task<List<HbtGenTableDto>> ImportTablesAsync(HbtGenTableImportDto input)
    {
        var result = new List<HbtGenTableDto>();

        // 创建表信息
        var table = new HbtGenTable
        {
            DatabaseName = input.DatabaseName,
            TableName = input.TableName,
            TableComment = input.TableComment,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            CreateBy = _currentUser.UserName ?? "Hbt365",
            UpdateBy = _currentUser.UserName ?? "Hbt365"
        };

        await _tableRepository.CreateAsync(table);
        result.Add(table.Adapt<HbtGenTableDto>());

        // 创建字段信息
        if (input.Columns != null)
        {
            foreach (var column in input.Columns)
            {
                var genColumn = new HbtGenColumn
                {
                    TableId = table.Id,
                    ColumnName = column.ColumnName,
                    ColumnComment = column.ColumnComment,
                    DbColumnType = column.DbColumnType,
                    CsharpType = column.CsharpType,
                    CsharpColumn = column.CsharpColumn,
                    CsharpLength = int.Parse(column.CsharpLength ?? "0"),
                    CsharpDecimalDigits = int.Parse(column.CsharpDecimalDigits ?? "0"),
                    IsIncrement = int.Parse(column.IsIncrement ?? "0"),
                    IsPrimaryKey = int.Parse(column.IsPrimaryKey ?? "0"),
                    IsRequired = int.Parse(column.IsRequired ?? "0"),
                    IsInsert = int.Parse(column.IsInsert ?? "0"),
                    IsEdit = int.Parse(column.IsEdit ?? "0"),
                    IsList = int.Parse(column.IsList ?? "0"),
                    IsQuery = int.Parse(column.IsQuery ?? "0"),
                    QueryType = column.QueryType,
                    IsSort = int.Parse(column.IsSort ?? "0"),
                    IsExport = int.Parse(column.IsExport ?? "0"),
                    DisplayType = column.DisplayType,
                    DictType = column.DictType,
                    OrderNum = int.Parse(column.OrderNum ?? "0"),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateBy = _currentUser.UserName ?? "Hbt365",
                    UpdateBy = _currentUser.UserName ?? "Hbt365"
                };

                await _columnRepository.CreateAsync(genColumn);
            }
        }

        return result;
    }

    /// <summary>
    /// 导出表
    /// </summary>
    /// <returns>导出的表列表</returns>
    public async Task<List<HbtGenTableExportDto>> ExportTablesAsync()
    {
        var tables = await _tableRepository.GetListAsync();
        var result = new List<HbtGenTableExportDto>();

        foreach (var table in tables)
        {
            var columns = await _columnRepository.GetListAsync(x => x.TableId == table.Id);
            result.Add(new HbtGenTableExportDto
            {
                Table = table.Adapt<HbtGenTableDto>(),
                Columns = columns.Adapt<List<HbtGenColumnDto>>()
            });
        }

        return result;
    }

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
            IsPk = x.IsPrimarykey ? 1 : 0,
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
        table.UpdateBy = _currentUser.UserName ?? "Hbt365";
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
                    CsharpColumn = ToPascalCase(column.DbColumnName),
                    CsharpLength = column.Length,
                    CsharpDecimalDigits = column.DecimalDigits,
                    IsIncrement = column.IsIdentity ? 1 : 0,
                    IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                    IsRequired = column.IsNullable ? 0 : 1,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateBy = _currentUser.UserName ?? "Hbt365",
                    UpdateBy = _currentUser.UserName ?? "Hbt365"
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
                existingColumn.IsIncrement = column.IsIdentity ? 1 : 0;
                existingColumn.IsPrimaryKey = column.IsPrimarykey ? 1 : 0;
                existingColumn.IsRequired = column.IsNullable ? 0 : 1;
                existingColumn.UpdateTime = DateTime.Now;
                existingColumn.UpdateBy = _currentUser.UserName ?? "Hbt365";
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

        return await _codeGeneratorService.GenerateCodeAsync(table);
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
                _logger.LogError(ex, "生成代码失败，表ID：{Id}", id);
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