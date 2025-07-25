#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表服务实现
//===================================================================

using Lean.Hbt.Common.Utils;
using Microsoft.AspNetCore.Http;

namespace Lean.Hbt.Application.Services.Generator;

/// <summary>
/// 代码生成表服务实现
/// </summary>
public class HbtGenTableService : HbtBaseService, IHbtGenTableService
{
    protected readonly IHbtRepositoryFactory _repositoryFactory;
    private readonly ISqlSugarClient _db;

    private IHbtRepository<HbtGenTable> TableRepository => _repositoryFactory.GetGeneratorRepository<HbtGenTable>();
    private IHbtRepository<HbtGenColumn> ColumnRepository => _repositoryFactory.GetGeneratorRepository<HbtGenColumn>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">表仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="db">数据库连接</param>
    public HbtGenTableService(
        IHbtRepositoryFactory repositoryFactory,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtLocalizationService localization,
        ISqlSugarClient db) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    #region 基础操作

    /// <summary>
    /// 获取分页表列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    public async Task<HbtPagedResult<HbtGenTableDto>> GetListAsync(HbtGenTableQueryDto input)
    {
        var result = await TableRepository.GetPagedListAsync(
            QueryExpression(input),
            input.PageIndex,
            input.PageSize,
            x => x.CreateTime,
            OrderByType.Desc);

        return result.Adapt<HbtPagedResult<HbtGenTableDto>>();
    }

    /// <summary>
    /// 根据ID获取表信息
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>表信息</returns>
    public async Task<HbtGenTableDto?> GetByIdAsync(long id)
    {
        var table = await TableRepository.GetByIdAsync(id);
        if (table == null)
            return null;

        var dto = table.Adapt<HbtGenTableDto>();

        // 获取列信息
        var columns = await ColumnRepository.GetListAsync(x => x.TableId == id);
        dto.Columns = columns.Adapt<List<HbtGenColumnDto>>();

        return dto;
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>字段列表</returns>
    public async Task<List<HbtGenColumnDto>> GetColumnListAsync(long tableId)
    {
        var columns = await ColumnRepository.GetListAsync(x => x.TableId == tableId);
        return columns.Adapt<List<HbtGenColumnDto>>();
    }

    /// <summary>
    /// 创建表信息
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建结果</returns>
    public async Task<HbtGenTableDto> CreateAsync(HbtGenTableCreateDto input)
    {
        // 检查表名是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(TableRepository, nameof(HbtGenTable.TableName), input.TableName);

        // 创建表信息
        var table = input.Adapt<HbtGenTable>();
        await TableRepository.CreateAsync(table);

        // 创建列信息
        if (input.Columns != null && input.Columns.Any())
        {
            var columns = input.Columns.Adapt<List<HbtGenColumn>>();
            foreach (var column in columns)
            {
                column.TableId = table.Id;
                await ColumnRepository.CreateAsync(column);
            }
        }

        return await GetByIdAsync(table.Id) ?? throw new HbtException(L("Generator.Table.CreateFailed"));
    }

    /// <summary>
    /// 更新表信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表信息</returns>
    public async Task<HbtGenTableDto> UpdateAsync(HbtGenTableUpdateDto input)
    {
        var table = await TableRepository.GetByIdAsync(input.GenTableId);
        if (table == null)
            throw new HbtException(L("Generator.Table.NotFound", input.GenTableId));

        // 检查表名是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(TableRepository, nameof(HbtGenTable.TableName), input.TableName, input.GenTableId);

        // 更新表信息
        input.Adapt(table);
        await TableRepository.UpdateAsync(table);

        // 更新列信息
        if (input.Columns != null && input.Columns.Any())
        {
            // 删除旧的列信息
            var oldColumns = await ColumnRepository.GetListAsync(x => x.TableId == input.GenTableId);
            foreach (var column in oldColumns)
            {
                await ColumnRepository.DeleteAsync(column.Id);
            }

            // 创建新的列信息
            var columns = input.Columns.Adapt<List<HbtGenColumn>>();
            foreach (var column in columns)
            {
                column.TableId = input.GenTableId;
                await ColumnRepository.CreateAsync(column);
            }
        }

        return await GetByIdAsync(input.GenTableId) ?? throw new HbtException(L("Generator.Table.UpdateFailed"));
    }

    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        // 删除表信息
        await TableRepository.DeleteAsync(id);

        // 删除关联的列信息
        var columns = await ColumnRepository.GetListAsync(x => x.TableId == id);
        foreach (var column in columns)
        {
            await ColumnRepository.DeleteAsync(column.Id);
        }

        return true;
    }

    #endregion

    #region 表操作

    /// <summary>
    /// 导入表
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入的表列表</returns>
    public async Task<List<HbtGenTableDto>> ImportTablesAsync(HbtGenTableImportDto input)
    {
        var result = new List<HbtGenTableDto>();
        var failTables = new List<string>();

        try
        {
            _logger.Info($"开始导入表：{input.TableName}");

            // 获取表信息
            var tableInfo = await Task.Run(() => _db.DbMaintenance.GetTableInfoList(false).FirstOrDefault(x => x.Name == input.TableName));
            if (tableInfo == null)
            {
                _logger.Error($"表不存在：{input.TableName}");
                throw new HbtException(L("Generator.Table.NotFound", input.TableName));
            }

            _logger.Info($"获取到表信息：{input.TableName}");

            // 检查表是否已存在
            var existingTable = await TableRepository.GetFirstAsync(x => x.TableName == input.TableName);
            if (existingTable != null)
            {
                _logger.Info($"表已存在，准备更新：{input.TableName}");

                // 更新表信息
                existingTable.DatabaseName = input.DatabaseName;
                existingTable.TableName = input.TableName;
                existingTable.TableComment = input.TableComment;
                existingTable.EntityClassName = input.EntityClassName;
                existingTable.EntityNamespace = input.EntityNamespace;
                existingTable.BaseNamespace = "Lean.Hbt";
                existingTable.DtoType = input.DtoType;
                existingTable.DtoClassName = HbtNamingHelper.GetDtoClassName(input.TableName);
                existingTable.TplType = input.TplType;
                existingTable.TplCategory = input.TplCategory;
                existingTable.SubTableName = input.SubTableName;
                existingTable.SubTableFkName = input.SubTableFkName;
                existingTable.TreeCode = input.TreeCode;
                tableInfo.Description = input.TableComment;
                existingTable.TreeName = input.TreeName;
                existingTable.TreeParentCode = input.TreeParentCode;
                existingTable.ModuleName = input.ModuleName;
                existingTable.BusinessName = input.BusinessName;
                existingTable.FunctionName = input.FunctionName;
                existingTable.Author = input.Author;
                existingTable.GenMethod = input.GenMethod;
                existingTable.GenPath = input.GenPath;
                existingTable.ParentMenuId = input.ParentMenuId;
                existingTable.SortType = input.SortType;
                existingTable.SortField = input.SortField;
                existingTable.PermsPrefix = input.PermsPrefix;
                existingTable.GenerateMenu = input.GenerateMenu;
                existingTable.FrontTpl = input.FrontTpl;
                existingTable.BtnStyle = input.BtnStyle;
                existingTable.FrontStyle = input.FrontStyle;
                existingTable.IsGenCode = input.IsGenCode;


                // 获取并创建新的列信息
                var columns = await Task.Run(() => _db.DbMaintenance.GetColumnInfosByTableName(input.TableName, false));
                if (columns == null || !columns.Any())
                {
                    _logger.Error($"表没有列信息：{input.TableName}");
                    throw new HbtException(L("Generator.Table.NoColumns", input.TableName));
                }

                _logger.Info($"获取到列信息，数量：{columns.Count}");

                // 处理列信息
                var existingColumns = await ColumnRepository.GetListAsync(x => x.TableId == existingTable.Id);
                var currentColumnNames = columns.Select(x => x.DbColumnName).ToList();

                // 更新或创建列
                foreach (var column in columns)
                {
                    var existingColumn = existingColumns.FirstOrDefault(x => x.ColumnName == column.DbColumnName);

                    if (existingColumn != null)
                    {
                        _logger.Info($"列已存在，准备更新：{column.DbColumnName}");
                        // 更新列信息
                        existingColumn.ColumnName = column.DbColumnName;
                        existingColumn.ColumnComment = column.ColumnDescription;
                        existingColumn.DbColumnType = column.DataType;
                        existingColumn.CsharpType = HbtStringUtils.GetCsharpType(column.DataType);
                        existingColumn.CsharpField = HbtStringUtils.ToCamelCase(column.DbColumnName);
                        existingColumn.CsharpLength = column.Length;
                        existingColumn.CsharpDecimalDigits = column.DecimalDigits;
                        existingColumn.IsIncrement = column.IsIdentity ? 1 : 0;
                        existingColumn.IsPrimaryKey = column.IsPrimarykey ? 1 : 0;
                        existingColumn.IsRequired = !column.IsNullable ? 1 : 0;
                        existingColumn.IsInsert = 1;
                        existingColumn.IsEdit = 1;
                        existingColumn.IsList = 1;
                        existingColumn.IsQuery = 0;
                        existingColumn.QueryType = "EQ";
                        existingColumn.IsSort = 0;
                        existingColumn.IsExport = 1;
                        existingColumn.DisplayType = "input";
                        existingColumn.DictType = "";
                        existingColumn.OrderNum = column.CreateTableFieldSort;

                        await ColumnRepository.UpdateAsync(existingColumn);
                    }
                    else
                    {
                        _logger.Info($"列不存在，准备创建：{column.DbColumnName}");
                        // 创建新列
                        var newColumn = new HbtGenColumn
                        {
                            TableId = existingTable.Id,
                            ColumnName = column.DbColumnName,
                            ColumnComment = column.ColumnDescription,
                            DbColumnType = column.DataType,
                            CsharpType = HbtStringUtils.GetCsharpType(column.DataType),
                            CsharpField = HbtStringUtils.ToCamelCase(column.DbColumnName),
                            CsharpLength = column.Length,
                            CsharpDecimalDigits = column.DecimalDigits,
                            IsIncrement = column.IsIdentity ? 1 : 0,
                            IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                            IsRequired = !column.IsNullable ? 1 : 0,
                            IsInsert = 1,
                            IsEdit = 1,
                            IsList = 1,
                            IsQuery = 0,
                            QueryType = "EQ",
                            IsSort = 0,
                            IsExport = 1,
                            DisplayType = "input",
                            DictType = "",
                            OrderNum = column.CreateTableFieldSort
                        };

                        await ColumnRepository.CreateAsync(newColumn);
                    }
                }

                // 删除不再存在的列
                var columnsToDelete = existingColumns.Where(x => !currentColumnNames.Contains(x.ColumnName)).ToList();
                foreach (var column in columnsToDelete)
                {
                    _logger.Info($"删除不再存在的列：{column.ColumnName}");
                    await ColumnRepository.DeleteAsync(column.Id);
                }

                await TableRepository.UpdateAsync(existingTable);
                _logger.Info($"表信息更新成功，ID：{existingTable.Id}");

                var updatedTable = await GetByIdAsync(existingTable.Id);
                if (updatedTable == null)
                {
                    _logger.Error($"更新后无法获取表信息：{input.TableName}");
                    throw new HbtException(L("Generator.Table.ImportFailed"));
                }

                result.Add(updatedTable);
                _logger.Info($"表更新成功：{input.TableName}");
            }
            else
            {
                // 创建新表
                var table = new HbtGenTable
                {
                    DatabaseName = input.DatabaseName,
                    TableName = input.TableName,
                    TableComment = input.TableComment,
                    Remark = input.TableComment,
                    EntityClassName = input.EntityClassName,
                    BaseNamespace = "Lean.Hbt"
                };

                // 设置实体相关命名空间
                table.EntityNamespace = HbtNamingHelper.GetEntityNamespace(table.BaseNamespace, input.TableName);
                table.DtoType = input.DtoType;
                table.DtoNamespace = HbtNamingHelper.GetDtoNamespace(table.BaseNamespace, input.TableName);
                table.DtoClassName = HbtNamingHelper.GetDtoClassName(input.TableName);

                // 设置服务相关命名空间
                table.ServiceNamespace = HbtNamingHelper.GetServiceNamespace(table.BaseNamespace, input.TableName);
                table.IServiceClassName = HbtNamingHelper.GetIServiceClassName(input.TableName);
                table.ServiceClassName = HbtNamingHelper.GetServiceClassName(input.TableName);

                // 设置仓储相关命名空间
                table.IRepositoryNamespace = HbtNamingHelper.GetRepositoryNamespace(table.BaseNamespace, input.TableName);
                table.IRepositoryClassName = HbtNamingHelper.GetIRepositoryClassName(input.TableName);
                table.RepositoryNamespace = HbtNamingHelper.GetRepositoryNamespace(table.BaseNamespace, input.TableName);
                table.RepositoryClassName = HbtNamingHelper.GetRepositoryClassName(input.TableName);

                // 设置控制器相关命名空间
                table.ControllerNamespace = HbtNamingHelper.GetControllerNamespace(table.BaseNamespace, input.TableName);
                table.ControllerClassName = HbtNamingHelper.GetControllerClassName(input.TableName);

                // 设置其他属性
                table.TplCategory = input.TplCategory;
                table.TplType = input.TplType;
                table.SubTableName = input.SubTableName;
                table.SubTableFkName = input.SubTableFkName;
                table.TreeCode = input.TreeCode;
                table.TreeName = input.TreeName;
                table.TreeParentCode = input.TreeParentCode;
                table.ModuleName = input.ModuleName;
                table.BusinessName = input.BusinessName;
                table.FunctionName = input.FunctionName;
                table.Author = input.Author;
                table.GenMethod = input.GenMethod;
                table.GenPath = input.GenPath;
                table.ParentMenuId = input.ParentMenuId;
                table.SortType = input.SortType;
                table.SortField = input.SortField;
                table.PermsPrefix = input.PermsPrefix;
                table.GenerateMenu = input.GenerateMenu;
                table.FrontTpl = input.FrontTpl;
                table.BtnStyle = input.BtnStyle;
                table.FrontStyle = input.FrontStyle;
                table.IsGenCode = input.IsGenCode;


                _logger.Info($"开始获取列信息：{input.TableName}");

                // 获取并创建列信息
                var columns = await Task.Run(() => _db.DbMaintenance.GetColumnInfosByTableName(input.TableName, false));
                if (columns == null || !columns.Any())
                {
                    _logger.Error($"表没有列信息：{input.TableName}");
                    throw new HbtException(L("Generator.Table.NoColumns", input.TableName));
                }

                _logger.Info($"获取到列信息，数量：{columns.Count}");

                // 处理列信息
                var existingColumns = await ColumnRepository.GetListAsync(x => x.TableId == table.Id);
                var currentColumnNames = columns.Select(x => x.DbColumnName).ToList();

                // 更新或创建列
                foreach (var column in columns)
                {
                    var existingColumn = existingColumns.FirstOrDefault(x => x.ColumnName == column.DbColumnName);

                    if (existingColumn != null)
                    {
                        _logger.Info($"列已存在，准备更新：{column.DbColumnName}");
                        // 更新列信息
                        existingColumn.ColumnName = column.DbColumnName;
                        existingColumn.ColumnComment = column.ColumnDescription;
                        existingColumn.DbColumnType = column.DataType;
                        existingColumn.CsharpType = HbtStringUtils.GetCsharpType(column.DataType);
                        existingColumn.CsharpField = HbtStringUtils.ToCamelCase(column.DbColumnName);
                        existingColumn.CsharpLength = column.Length;
                        existingColumn.CsharpDecimalDigits = column.DecimalDigits;
                        existingColumn.IsIncrement = column.IsIdentity ? 1 : 0;
                        existingColumn.IsPrimaryKey = column.IsPrimarykey ? 1 : 0;
                        existingColumn.IsRequired = !column.IsNullable ? 1 : 0;
                        existingColumn.IsInsert = 1;
                        existingColumn.IsEdit = 1;
                        existingColumn.IsList = 1;
                        existingColumn.IsQuery = 0;
                        existingColumn.QueryType = "EQ";
                        existingColumn.IsSort = 0;
                        existingColumn.IsExport = 1;
                        existingColumn.DisplayType = "input";
                        existingColumn.DictType = "";
                        existingColumn.OrderNum = column.CreateTableFieldSort;

                        await ColumnRepository.UpdateAsync(existingColumn);
                    }
                    else
                    {
                        _logger.Info($"列不存在，准备创建：{column.DbColumnName}");
                        // 创建新列
                        var newColumn = new HbtGenColumn
                        {
                            TableId = table.Id,
                            ColumnName = column.DbColumnName,
                            ColumnComment = column.ColumnDescription,
                            DbColumnType = column.DataType,
                            CsharpType = HbtStringUtils.GetCsharpType(column.DataType),
                            CsharpField = HbtStringUtils.ToCamelCase(column.DbColumnName),
                            CsharpLength = column.Length,
                            CsharpDecimalDigits = column.DecimalDigits,
                            IsIncrement = column.IsIdentity ? 1 : 0,
                            IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                            IsRequired = !column.IsNullable ? 1 : 0,
                            IsInsert = 1,
                            IsEdit = 1,
                            IsList = 1,
                            IsQuery = 0,
                            QueryType = "EQ",
                            IsSort = 0,
                            IsExport = 1,
                            DisplayType = "input",
                            DictType = "",
                            OrderNum = column.CreateTableFieldSort
                        };

                        await ColumnRepository.CreateAsync(newColumn);
                    }
                }

                // 删除不再存在的列
                var columnsToDelete = existingColumns.Where(x => !currentColumnNames.Contains(x.ColumnName)).ToList();
                foreach (var column in columnsToDelete)
                {
                    _logger.Info($"删除不再存在的列：{column.ColumnName}");
                    await ColumnRepository.DeleteAsync(column.Id);
                }

                _logger.Info($"开始保存表信息：{input.TableName}");

                // 保存表信息
                await TableRepository.CreateAsync(table);

                // 获取新创建表的ID
                var savedTable = await TableRepository.GetFirstAsync(x => x.TableName == input.TableName);
                if (savedTable == null)
                {
                    _logger.Error($"保存后无法获取表信息：{input.TableName}");
                    throw new HbtException(L("Generator.Table.ImportFailed"));
                }
                table = savedTable;

                result.Add(await GetByIdAsync(table.Id) ?? throw new HbtException(L("Generator.Table.ImportFailed")));
                _logger.Info($"表导入成功：{input.TableName}");
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"导入表失败，表名：{input.TableName}", ex);
            throw;
        }

        return result;
    }

    /// <summary>
    /// 导入表及其所有字段信息
    /// </summary>
    /// <param name="databaseName">数据库名</param>
    /// <param name="tableName">表名</param>
    /// <returns>是否成功</returns>
    public async Task<bool> ImportTableAndColumnsAsync(string databaseName, string tableName)
    {
        try
        {
            _logger.Info($"开始导入表和列信息：{tableName}");

            // 检查表是否存在
            var tableExists = await Task.Run(() => _db.DbMaintenance.IsAnyTable(tableName, false));
            if (!tableExists)
            {
                _logger.Error($"表不存在：{tableName}");
                throw new HbtException(L("Generator.Table.NotFound", tableName));
            }

            // 获取表信息
            var tableInfo = await Task.Run(() => _db.DbMaintenance.GetTableInfoList(false).FirstOrDefault(x => x.Name == tableName));
            if (tableInfo == null)
            {
                _logger.Error($"无法获取表信息：{tableName}");
                throw new HbtException(L("Generator.Table.InfoNotFound", tableName));
            }

            _logger.Info($"获取到表信息：{tableName}");

            // 检查表是否已存在
            var existingTable = await TableRepository.GetFirstAsync(x => x.TableName == tableName);
            HbtGenTable table;

            if (existingTable != null)
            {
                _logger.Info($"表已存在，准备更新：{tableName}");
                table = existingTable;

                // 更新表信息
                table.DatabaseName = databaseName;
                table.TableName = tableName;
                table.TableComment = tableInfo.Description;
                table.Remark = tableName + "(" + tableInfo.Description + ")";
                table.EntityClassName = HbtNamingHelper.GetEntityClassName(tableName);
                table.BaseNamespace = "Lean.Hbt";
                table.EntityNamespace = HbtNamingHelper.GetEntityNamespace(table.BaseNamespace, tableName);
                table.DtoType = "Dto,QueryDto,CreateDto,UpdateDto,DeleteDto,TplDto,ImportDto,ExportDto";
                table.DtoNamespace = HbtNamingHelper.GetDtoNamespace(table.BaseNamespace, tableName);
                table.DtoClassName = HbtNamingHelper.GetDtoClassName(tableName);
                table.ServiceNamespace = HbtNamingHelper.GetServiceNamespace(table.BaseNamespace, tableName);
                table.IServiceClassName = HbtNamingHelper.GetIServiceClassName(tableName);
                table.ServiceClassName = HbtNamingHelper.GetServiceClassName(tableName);
                table.IRepositoryNamespace = HbtNamingHelper.GetRepositoryNamespace(table.BaseNamespace, tableName);
                table.IRepositoryClassName = HbtNamingHelper.GetIRepositoryClassName(tableName);
                table.RepositoryNamespace = HbtNamingHelper.GetRepositoryNamespace(table.BaseNamespace, tableName);
                table.RepositoryClassName = HbtNamingHelper.GetRepositoryClassName(tableName);
                table.ControllerNamespace = HbtNamingHelper.GetControllerNamespace(table.BaseNamespace, tableName);
                table.ControllerClassName = HbtNamingHelper.GetControllerClassName(tableName);
                table.TplType = "0";
                table.TplCategory = "crud";
                table.ModuleName = HbtNamingHelper.ExtractModuleName(tableName);
                table.BusinessName = HbtNamingHelper.GetBusinessName(tableName);
                table.FunctionName = tableInfo.Description;
                table.Author = "Lean365";
                table.GenMethod = "0";
                table.GenPath = "/";
                table.ParentMenuId = 0;
                table.SortType = "asc";
                table.SortField = "CreateTime";
                table.PermsPrefix = HbtNamingHelper.GetPermsPrefix(tableName);
                table.GenerateMenu = 1;
                table.FrontTpl = 2;
                table.BtnStyle = 1;
                table.FrontStyle = 24;
                table.IsGenCode = 0;


                await TableRepository.UpdateAsync(table);

                // 获取更新后的表信息
                var updatedTable = await TableRepository.GetFirstAsync(x => x.TableName == tableName);
                if (updatedTable == null)
                {
                    _logger.Error($"更新后无法获取表信息：{tableName}");
                    throw new HbtException(L("Generator.Table.ImportFailed"));
                }
                table = updatedTable;
            }
            else
            {
                _logger.Info($"表不存在，准备创建：{tableName}");

                // 创建新表
                table = new HbtGenTable
                {
                    DatabaseName = databaseName,
                    TableName = tableName,
                    TableComment = tableInfo.Description,
                    Remark = tableName + "(" + tableInfo.Description + ")",
                    EntityClassName = HbtNamingHelper.GetEntityClassName(tableName),
                    BaseNamespace = "Lean.Hbt"
                };

                // 设置实体相关命名空间
                table.EntityNamespace = HbtNamingHelper.GetEntityNamespace(table.BaseNamespace, tableName);
                table.DtoNamespace = HbtNamingHelper.GetDtoNamespace(table.BaseNamespace, tableName);
                table.DtoClassName = HbtNamingHelper.GetDtoClassName(tableName);
                table.DtoType = "Dto,QueryDto,CreateDto,UpdateDto,DeleteDto,TplDto,ImportDto,ExportDto";

                // 设置服务相关命名空间
                table.ServiceNamespace = HbtNamingHelper.GetServiceNamespace(table.BaseNamespace, tableName);
                table.IServiceClassName = HbtNamingHelper.GetIServiceClassName(tableName);
                table.ServiceClassName = HbtNamingHelper.GetServiceClassName(tableName);

                // 设置仓储相关命名空间
                table.IRepositoryNamespace = HbtNamingHelper.GetRepositoryNamespace(table.BaseNamespace, tableName);
                table.IRepositoryClassName = HbtNamingHelper.GetIRepositoryClassName(tableName);
                table.RepositoryNamespace = HbtNamingHelper.GetRepositoryNamespace(table.BaseNamespace, tableName);
                table.RepositoryClassName = HbtNamingHelper.GetRepositoryClassName(tableName);

                // 设置控制器相关命名空间
                table.ControllerNamespace = HbtNamingHelper.GetControllerNamespace(table.BaseNamespace, tableName);
                table.ControllerClassName = HbtNamingHelper.GetControllerClassName(tableName);

                // 设置其他属性
                table.TplType = "0";
                table.TplCategory = "crud";
                table.ModuleName = HbtNamingHelper.ExtractModuleName(tableName);
                table.BusinessName = HbtNamingHelper.GetBusinessName(tableName);
                table.FunctionName = tableInfo.Description;
                table.Author = "Lean365";
                table.GenMethod = "0";
                table.GenPath = "/";
                table.ParentMenuId = 0;
                table.SortType = "asc";
                table.SortField = "CreateTime";
                table.PermsPrefix = HbtNamingHelper.GetPermsPrefix(tableName);
                table.GenerateMenu = 1;
                table.FrontTpl = 2;
                table.BtnStyle = 1;
                table.FrontStyle = 24;
                table.IsGenCode = 0;


                await TableRepository.CreateAsync(table);

                // 获取新创建表的ID
                var savedTable = await TableRepository.GetFirstAsync(x => x.TableName == tableName);
                if (savedTable == null)
                {
                    _logger.Error($"保存后无法获取表信息：{tableName}");
                    throw new HbtException(L("Generator.Table.ImportFailed"));
                }
                table = savedTable;
            }

            // 获取列信息
            var columns = await Task.Run(() => _db.DbMaintenance.GetColumnInfosByTableName(tableName, false));
            if (columns == null || !columns.Any())
            {
                _logger.Error($"表没有列信息：{tableName}");
                throw new HbtException(L("Generator.Table.NoColumns", tableName));
            }

            _logger.Info($"获取到列信息，数量：{columns.Count}");

            // 处理列信息
            var existingColumns = await ColumnRepository.GetListAsync(x => x.TableId == table.Id);
            var currentColumnNames = columns.Select(x => x.DbColumnName).ToList();

            // 更新或创建列
            foreach (var column in columns)
            {
                var existingColumn = existingColumns.FirstOrDefault(x => x.ColumnName == column.DbColumnName);

                if (existingColumn != null)
                {
                    _logger.Info($"列已存在，准备更新：{column.DbColumnName}");
                    // 更新列信息
                    existingColumn.ColumnName = column.DbColumnName;
                    existingColumn.ColumnComment = column.ColumnDescription;
                    existingColumn.DbColumnType = column.DataType;
                    existingColumn.CsharpType = HbtStringUtils.GetCsharpType(column.DataType);
                    existingColumn.CsharpField = HbtStringUtils.ToCamelCase(column.DbColumnName);
                    existingColumn.CsharpLength = column.Length;
                    existingColumn.CsharpDecimalDigits = column.DecimalDigits;
                    existingColumn.IsIncrement = column.IsIdentity ? 1 : 0;
                    existingColumn.IsPrimaryKey = column.IsPrimarykey ? 1 : 0;
                    existingColumn.IsRequired = !column.IsNullable ? 1 : 0;
                    existingColumn.IsInsert = 1;
                    existingColumn.IsEdit = 1;
                    existingColumn.IsList = 1;
                    existingColumn.IsQuery = 0;
                    existingColumn.QueryType = "EQ";
                    existingColumn.IsSort = 0;
                    existingColumn.IsExport = 1;
                    existingColumn.DisplayType = "input";
                    existingColumn.DictType = "";
                    existingColumn.OrderNum = column.CreateTableFieldSort;

                    await ColumnRepository.UpdateAsync(existingColumn);
                }
                else
                {
                    _logger.Info($"列不存在，准备创建：{column.DbColumnName}");
                    // 创建新列
                    var newColumn = new HbtGenColumn
                    {
                        TableId = table.Id,
                        ColumnName = column.DbColumnName,
                        ColumnComment = column.ColumnDescription,
                        DbColumnType = column.DataType,
                        CsharpType = HbtStringUtils.GetCsharpType(column.DataType),
                        CsharpField = HbtStringUtils.ToCamelCase(column.DbColumnName),
                        CsharpLength = column.Length,
                        CsharpDecimalDigits = column.DecimalDigits,
                        IsIncrement = column.IsIdentity ? 1 : 0,
                        IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                        IsRequired = !column.IsNullable ? 1 : 0,
                        IsInsert = 1,
                        IsEdit = 1,
                        IsList = 1,
                        IsQuery = 0,
                        QueryType = "EQ",
                        IsSort = 0,
                        IsExport = 1,
                        DisplayType = "input",
                        DictType = "",
                        OrderNum = column.CreateTableFieldSort
                    };

                    await ColumnRepository.CreateAsync(newColumn);
                }
            }

            // 删除不再存在的列
            var columnsToDelete = existingColumns.Where(x => !currentColumnNames.Contains(x.ColumnName)).ToList();
            foreach (var column in columnsToDelete)
            {
                _logger.Info($"删除不再存在的列：{column.ColumnName}");
                await ColumnRepository.DeleteAsync(column.Id);
            }

            _logger.Info($"表和列信息导入完成：{tableName}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"导入表和列信息失败：{tableName}", ex);
            throw;
        }
    }

    /// <summary>
    /// 导出表
    /// </summary>
    /// <returns>导出的表列表</returns>
    public async Task<List<HbtGenTableExportDto>> ExportTablesAsync()
    {
        var tables = await TableRepository.GetListAsync();
        var result = new List<HbtGenTableExportDto>();

        foreach (var table in tables)
        {
            var dto = table.Adapt<HbtGenTableExportDto>();
            var columns = await ColumnRepository.GetListAsync(x => x.TableId == table.Id);
            dto.Columns = columns.Adapt<List<HbtGenColumnDto>>();
            result.Add(dto);
        }

        return result;
    }

    /// <summary>
    /// 获取数据库列表
    /// </summary>
    /// <returns>数据库列表</returns>
    public Task<List<string>> GetDatabaseListByDbAsync()
    {
        return Task.FromResult(_db.DbMaintenance.GetDataBaseList());
    }

    /// <summary>
    /// 获取表列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <returns>表列表</returns>
    public async Task<List<HbtGenTableInfoDto>> GetTableListByDbAsync(string databaseName)
    {
        var tables = await Task.Run(() => _db.DbMaintenance.GetTableInfoList(false));
        return tables.Select(x => new HbtGenTableInfoDto
        {
            TableName = x.Name,
            TableComment = x.Description
        }).ToList();
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <param name="tableName">表名</param>
    /// <returns>字段列表</returns>
    public async Task<List<HbtGenTableColumnInfoDto>> GetTableColumnListByDbAsync(string databaseName, string tableName)
    {
        var columns = await Task.Run(() => _db.DbMaintenance.GetColumnInfosByTableName(tableName, false));
        return columns.Select(x => new HbtGenTableColumnInfoDto
        {
            DbColumnName = x.DbColumnName,
            ColumnDescription = x.ColumnDescription,
            DataType = x.DataType,
            IsPrimarykey = x.IsPrimarykey,
            IsIdentity = x.IsIdentity,
            IsNullable = x.IsNullable
        }).ToList();
    }

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否同步成功</returns>
    public async Task<bool> SyncTableAsync(long id)
    {
        var table = await TableRepository.GetByIdAsync(id);
        if (table == null)
            throw new HbtException(L("Generator.Table.NotFound", id));

        return await ImportTableAndColumnsAsync(table.DatabaseName, table.TableName);
    }

    #endregion

    /// <summary>
    /// 构建查询条件
    /// </summary>
    private Expression<Func<HbtGenTable, bool>> QueryExpression(HbtGenTableQueryDto input)
    {
        return Expressionable.Create<HbtGenTable>()
            .AndIF(!string.IsNullOrEmpty(input.TableName), x => x.TableName.Contains(input.TableName))
            .AndIF(!string.IsNullOrEmpty(input.TableComment), x => x.TableComment.Contains(input.TableComment))
            .ToExpression();
    }
}
