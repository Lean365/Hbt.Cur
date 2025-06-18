using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Scriban;

#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableDefineService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表定义服务实现
//===================================================================

namespace Lean.Hbt.Application.Services.Generator;

/// <summary>
/// 代码生成表定义服务实现
/// </summary>
public class HbtGenTableDefineService : HbtBaseService, IHbtGenTableDefineService
{
    private readonly IHbtRepository<HbtGenTableDefine> _tableDefineRepository;
    private readonly IHbtRepository<HbtGenColumnDefine> _columnDefineRepository;
    private readonly ISqlSugarClient _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tableDefineRepository">表定义仓储</param>
    /// <param name="columnDefineRepository">列定义仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="currentTenant">当前租户服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="db">数据库客户端</param>
    /// <param name="webHostEnvironment">Web主机环境</param>
    public HbtGenTableDefineService(
        IHbtRepository<HbtGenTableDefine> tableDefineRepository,
        IHbtRepository<HbtGenColumnDefine> columnDefineRepository,
        IHbtLogger logger,
        IHttpContextAccessor httpContextAccessor,
        IHbtCurrentUser currentUser,
        IHbtCurrentTenant currentTenant,
        IHbtLocalizationService localization,
        ISqlSugarClient db,
        IWebHostEnvironment webHostEnvironment) : base(logger, httpContextAccessor, currentUser, currentTenant, localization)
    {
        _tableDefineRepository = tableDefineRepository ?? throw new ArgumentNullException(nameof(tableDefineRepository));
        _columnDefineRepository = columnDefineRepository ?? throw new ArgumentNullException(nameof(columnDefineRepository));
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
    }

    #region 表定义基础操作

    /// <summary>
    /// 根据ID获取表定义信息
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>表定义信息</returns>
    public async Task<HbtGenTableDefineDto?> GetTableByIdAsync(long id)
    {
        var table = await _tableDefineRepository.GetByIdAsync(id);
        if (table == null)
            return null;

        var dto = table.Adapt<HbtGenTableDefineDto>();

        // 获取列定义
        var columns = await _columnDefineRepository.GetListAsync(x => x.TableId == id);
        dto.Columns = columns.Adapt<List<HbtGenColumnDefineDto>>();

        return dto;
    }

    /// <summary>
    /// 获取分页表定义列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    public async Task<HbtPagedResult<HbtGenTableDefineDto>> GetTableListAsync(HbtGenTableDefineQueryDto input)
    {
        var exp = Expressionable.Create<HbtGenTableDefine>();

        if (!string.IsNullOrEmpty(input.TableName))
            exp = exp.And(x => x.TableName.Contains(input.TableName));

        if (!string.IsNullOrEmpty(input.TableComment))
            exp = exp.And(x => x.TableComment.Contains(input.TableComment));

        var result = await _tableDefineRepository.GetPagedListAsync(
            exp.ToExpression(),
            input.PageIndex,
            input.PageSize,
            x => x.CreateTime,
            OrderByType.Desc);

        var dtos = result.Rows.Adapt<List<HbtGenTableDefineDto>>();

        // 获取列定义
        foreach (var dto in dtos)
        {
            var columns = await _columnDefineRepository.GetListAsync(x => x.TableId == dto.GenTableDefineId);
            dto.Columns = columns.Adapt<List<HbtGenColumnDefineDto>>();
        }

        return new HbtPagedResult<HbtGenTableDefineDto>
        {
            Rows = dtos,
            TotalNum = result.TotalNum,
            PageIndex = input.PageIndex,
            PageSize = input.PageSize
        };
    }

    /// <summary>
    /// 创建代码生成表定义
    /// </summary>
    /// <param name="input">代码生成表定义信息</param>
    /// <returns>创建结果</returns>
    public async Task<HbtGenTableDefineDto> CreateTableAsync(HbtGenTableDefineCreateDto input)
    {
        // 检查表名是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_tableDefineRepository, nameof(HbtGenTableDefine.TableName), input.TableName);

        var entity = input.Adapt<HbtGenTableDefine>();

        // 创建表定义
        await _tableDefineRepository.CreateAsync(entity);

        // 根据表名查询获取新建表的ID
        var createdTable = await _tableDefineRepository.GetFirstAsync(x => x.TableName == input.TableName);
        if (createdTable == null || createdTable.Id <= 0)
        {
            throw new HbtException(L("Generator.TableDefine.CreateFailed"));
        }

        // 创建列定义
        if (input.Columns != null && input.Columns.Any())
        {
            var columns = input.Columns.Adapt<List<HbtGenColumnDefine>>();
            foreach (var column in columns)
            {
                column.TableId = createdTable.Id;
                await _columnDefineRepository.CreateAsync(column);
            }
        }

        // 获取完整的表定义信息
        var result = await GetTableByIdAsync(createdTable.Id);
        if (result == null)
        {
            throw new HbtException(L("Generator.TableDefine.CreateFailed"));
        }

        return result;
    }

    /// <summary>
    /// 更新表定义信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表定义信息</returns>
    public async Task<HbtGenTableDefineDto> UpdateTableAsync(HbtGenTableDefineUpdateDto input)
    {
        var table = await _tableDefineRepository.GetByIdAsync(input.GenTableDefineId);
        if (table == null)
            throw new HbtException(L("Generator.TableDefine.NotFound", input.GenTableDefineId));

        // 检查表名是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_tableDefineRepository, nameof(HbtGenTableDefine.TableName), input.TableName, input.GenTableDefineId);

        // 更新表定义
        input.Adapt(table);
        await _tableDefineRepository.UpdateAsync(table);

        // 更新列定义
        if (input.Columns != null && input.Columns.Any())
        {
            // 删除旧的列定义
            var oldColumns = await _columnDefineRepository.GetListAsync(x => x.TableId == input.GenTableDefineId);
            foreach (var column in oldColumns)
            {
                await _columnDefineRepository.DeleteAsync(column.Id);
            }

            // 创建新的列定义
            var columns = input.Columns.Adapt<List<HbtGenColumnDefine>>();
            foreach (var column in columns)
            {
                column.TableId = input.GenTableDefineId;
                await _columnDefineRepository.CreateAsync(column);
            }
        }

        return await GetTableByIdAsync(input.GenTableDefineId) ?? throw new HbtException(L("Generator.TableDefine.UpdateFailed"));
    }

    /// <summary>
    /// 删除表定义
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteTableAsync(long id)
    {
        // 删除表定义
        await _tableDefineRepository.DeleteAsync(id);

        // 删除关联的列定义
        var columns = await _columnDefineRepository.GetListAsync(x => x.TableId == id);
        foreach (var column in columns)
        {
            await _columnDefineRepository.DeleteAsync(column.Id);
        }

        return true;
    }

    /// <summary>
    /// 批量删除表定义
    /// </summary>
    /// <param name="ids">表定义ID数组</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> BatchDeleteTableAsync(long[] ids)
    {
        foreach (var id in ids)
        {
            await DeleteTableAsync(id);
        }
        return true;
    }

    #endregion

    #region 表定义导入导出操作

    /// <summary>
    /// 导入表定义
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量，失败数量）</returns>
    public async Task<(int success, int fail)> ImportTableAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var tables = await HbtExcelHelper.ImportAsync<HbtGenTableDefineImportDto>(fileStream, sheetName);
            if (!tables.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;
            foreach (var table in tables)
            {
                try
                {
                    if (string.IsNullOrEmpty(table.TableName) || string.IsNullOrEmpty(table.TableComment))
                    {
                        _logger.Warn(L("Generator.TableDefine.Log.ImportEmptyFields"));
                        fail++;
                        continue;
                    }

                    // 检查表名是否已存在
                    await HbtValidateUtils.ValidateFieldExistsAsync(_tableDefineRepository, nameof(HbtGenTableDefine.TableName), table.TableName);

                    var entity = table.Adapt<HbtGenTableDefine>();
                    entity.CreateTime = DateTime.Now;
                    entity.CreateBy = _currentUser.UserName;

                    await _tableDefineRepository.CreateAsync(entity);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Warn(L("Generator.TableDefine.Log.ImportFailed", ex.Message));
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error(L("Generator.TableDefine.Log.ImportDataFailed"), ex);
            throw new HbtException(L("Generator.TableDefine.ImportFailed"));
        }
    }

    /// <summary>
    /// 获取表定义模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    public async Task<(string fileName, byte[] content)> GetTableTemplateAsync(string sheetName = "Sheet1")
    {
        var (fileName, content) = await HbtExcelHelper.GenerateTemplateAsync<HbtGenTableDefineTemplateDto>(sheetName);
        return (fileName, content);
    }

    /// <summary>
    /// 导出表定义
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    public async Task<(string fileName, byte[] content)> ExportTableAsync(HbtGenTableDefineQueryDto query, string sheetName = "Sheet1")
    {
        try
        {
            var list = await _tableDefineRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<HbtGenTableDefineExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "表定义数据");
        }
        catch (Exception ex)
        {
            _logger.Error(L("Generator.TableDefine.Log.ExportDataFailed"), ex);
            throw new HbtException(L("Generator.TableDefine.ExportFailed"));
        }
    }

    #endregion

    #region 表定义特殊操作

    /// <summary>
    /// 初始化表结构
    /// </summary>
    /// <param name="input">初始化参数</param>
    /// <returns>初始化结果</returns>
    public async Task<List<HbtGenTableDefineDto>> InitializeTableListAsync(HbtGenTableDefineInitializeDto input)
    {
        var result = new List<HbtGenTableDefineDto>();

        try
        {
            // 1. 验证数据库连接信息
            if (string.IsNullOrEmpty(input.ConnectionString))
            {
                throw new HbtException(L("Generator.Database.ConnectionStringRequired"));
            }

            if (string.IsNullOrEmpty(input.DatabaseName))
            {
                throw new HbtException(L("Generator.Database.NameRequired"));
            }

            // 2. 检查表定义是否存在
            var existingTable = await _tableDefineRepository.GetByIdAsync(input.GenTableDefineId);
            if (existingTable == null)
            {
                throw new HbtException(L("Generator.TableDefine.NotFound", input.GenTableDefineId));
            }

            // 3. 获取完整的表定义信息
            var dto = await GetTableByIdAsync(existingTable.Id);
            if (dto == null)
            {
                throw new HbtException(L("Generator.TableDefine.GetFailed"));
            }

            result.Add(dto);

            // 4. 根据生成模式和模板类型选择模板文件
            var templateFile = "EntityDefine.scriban"; // 使用实体定义模板

            // 5. 读取模板文件
            var templatePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Generator", "backend", "Csharp", "Entities", templateFile);
            if (!File.Exists(templatePath))
            {
                _logger.Error(L("Generator.Template.NotFound"), new { Path = templatePath });
                throw new HbtException(L("Generator.Template.NotFound", templatePath));
            }

            var templateContent = await File.ReadAllTextAsync(templatePath);

            // 6. 准备模板数据
            var templateData = new
            {
                table = new
                {
                    name = dto.TableName,
                    comment = dto.TableComment,
                    table_name = dto.TableName,
                    author = dto.Author,
                    columns = dto.Columns.Select(c => new
                    {
                        column_name = c.ColumnName,
                        column_comment = c.ColumnComment,
                        data_type = c.DbColumnType,
                        is_pk = c.IsPrimaryKey,
                        is_increment = c.IsIncrement,
                        is_required = c.IsRequired,
                        column_length = c.ColumnLength,
                        decimal_digits = c.DecimalDigits,
                    })
                },
                date = DateTime.Now.ToString("yyyy-MM-dd")
            };

            // 7. 渲染模板
            var template = Template.Parse(templateContent);
            var resultContent = await template.RenderAsync(templateData);

            // 8. 保存文件
            var modulePath = Path.Combine(_webHostEnvironment.ContentRootPath, "..", "src", "Lean.Hbt.Domain", "Entities");
            if (!Directory.Exists(modulePath))
            {
                Directory.CreateDirectory(modulePath);
            }

            var filePath = Path.Combine(modulePath, $"{dto.TableName}.cs");
            await File.WriteAllTextAsync(filePath, resultContent);

            // 9. 记录操作日志（简化日志内容以避免截断）
            _logger.Info(L("Generator.TableDefine.InitializeSuccess"), new
            {
                TableId = dto.GenTableDefineId,
                TableName = dto.TableName,
                FilePath = filePath
            });

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Generator.TableDefine.InitializeFailed"), ex);
            throw;
        }
    }

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>是否同步成功</returns>
    public async Task<bool> SyncTableAsync(long id)
    {
        var table = await _tableDefineRepository.GetByIdAsync(id);
        if (table == null)
            return false;

        try
        {
            // 获取数据库表结构
            var dbColumns = _db.DbMaintenance.GetColumnInfosByTableName(table.TableName, false);
            if (dbColumns == null || !dbColumns.Any())
                return false;

            // 删除旧的列定义
            var oldColumns = await _columnDefineRepository.GetListAsync(x => x.TableId == id);
            foreach (var column in oldColumns)
            {
                await _columnDefineRepository.DeleteAsync(column.Id);
            }

            // 创建新的列定义
            foreach (var dbColumn in dbColumns)
            {
                var column = new HbtGenColumnDefine
                {
                    TableId = id,
                    ColumnName = dbColumn.DbColumnName,
                    ColumnComment = dbColumn.ColumnDescription ?? string.Empty,
                    DbColumnType = dbColumn.DataType
                };

                await _columnDefineRepository.CreateAsync(column);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error("同步表结构失败", ex);
            return false;
        }
    }

    #endregion

    #region 列定义操作

    /// <summary>
    /// 获取列定义列表
    /// </summary>
    /// <param name="query">查询参数</param>
    /// <returns>列定义列表</returns>
    public async Task<HbtPagedResult<HbtGenColumnDefineDto>> GetColumnListAsync(HbtGenColumnDefineQueryDto query)
    {
        var exp = Expressionable.Create<HbtGenColumnDefine>();

        if (query.TableId.HasValue)
            exp.And(x => x.TableId == query.TableId.Value);

        if (!string.IsNullOrEmpty(query.ColumnName))
            exp.And(x => x.ColumnName.Contains(query.ColumnName));

        if (!string.IsNullOrEmpty(query.ColumnComment))
            exp.And(x => x.ColumnComment.Contains(query.ColumnComment));

        var result = await _columnDefineRepository.GetPagedListAsync(
            exp.ToExpression(),
            query.PageIndex,
            query.PageSize,
            x => x.OrderNum,
            OrderByType.Asc);

        return new HbtPagedResult<HbtGenColumnDefineDto>
        {
            Rows = result.Rows.Adapt<List<HbtGenColumnDefineDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 创建列定义
    /// </summary>
    /// <param name="input">列定义信息</param>
    /// <returns>创建结果</returns>
    public async Task<HbtGenColumnDefineDto> CreateColumnAsync(HbtGenColumnDefineCreateDto input)
    {
        // 检查表是否存在
        var table = await _tableDefineRepository.GetByIdAsync(input.TableId);
        if (table == null)
            throw new HbtException(L("Generator.TableDefine.NotFound", input.TableId));

        // 检查列名是否已存在
        var fieldValues = new Dictionary<string, string>
        {
            { "TableId", input.TableId.ToString() },
            { "ColumnName", input.ColumnName }
        };
        await HbtValidateUtils.ValidateFieldsExistsAsync(_columnDefineRepository, fieldValues);

        var entity = input.Adapt<HbtGenColumnDefine>();
        entity.CreateTime = DateTime.Now;
        entity.CreateBy = _currentUser.UserName;

        await _columnDefineRepository.CreateAsync(entity);
        return entity.Adapt<HbtGenColumnDefineDto>();
    }

    /// <summary>
    /// 更新列定义
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的列定义信息</returns>
    public async Task<HbtGenColumnDefineDto> UpdateColumnAsync(HbtGenColumnDefineUpdateDto input)
    {
        var column = await _columnDefineRepository.GetByIdAsync(input.GenColumnDefineId);
        if (column == null)
            throw new HbtException(L("Generator.ColumnDefine.NotFound", input.GenColumnDefineId));

        // 检查列名是否已存在
        var fieldValues = new Dictionary<string, string>
        {
            { "TableId", column.TableId.ToString() },
            { "ColumnName", input.ColumnName }
        };
        await HbtValidateUtils.ValidateFieldsExistsAsync(_columnDefineRepository, fieldValues, input.GenColumnDefineId);

        input.Adapt(column);
        column.UpdateTime = DateTime.Now;
        column.UpdateBy = _currentUser.UserName;

        await _columnDefineRepository.UpdateAsync(column);
        return column.Adapt<HbtGenColumnDefineDto>();
    }

    /// <summary>
    /// 删除列定义
    /// </summary>
    /// <param name="id">列定义ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteColumnAsync(long id)
    {
        var column = await _columnDefineRepository.GetByIdAsync(id);
        if (column == null)
            return false;

        await _columnDefineRepository.DeleteAsync(id);
        return true;
    }

    /// <summary>
    /// 批量删除列定义
    /// </summary>
    /// <param name="ids">列定义ID数组</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> BatchDeleteColumnsAsync(long[] ids)
    {
        foreach (var id in ids)
        {
            await DeleteColumnAsync(id);
        }
        return true;
    }

    /// <summary>
    /// 导入列定义
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量，失败数量）</returns>
    public async Task<(int success, int fail)> ImportColumnAsync(Stream fileStream, string sheetName = "Sheet1")
    {
        try
        {
            var columns = await HbtExcelHelper.ImportAsync<HbtGenColumnDefineImportDto>(fileStream, sheetName);
            if (!columns.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;
            foreach (var column in columns)
            {
                try
                {
                    if (string.IsNullOrEmpty(column.ColumnName) || string.IsNullOrEmpty(column.ColumnComment))
                    {
                        _logger.Warn(L("Generator.ColumnDefine.Log.ImportEmptyFields"));
                        fail++;
                        continue;
                    }

                    var entity = column.Adapt<HbtGenColumnDefine>();
                    entity.CreateTime = DateTime.Now;
                    entity.CreateBy = _currentUser.UserName;

                    await _columnDefineRepository.CreateAsync(entity);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Warn(L("Generator.ColumnDefine.Log.ImportFailed", ex.Message));
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error(L("Generator.ColumnDefine.Log.ImportDataFailed"), ex);
            throw new HbtException(L("Generator.ColumnDefine.ImportFailed"));
        }
    }

    /// <summary>
    /// 获取列定义模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    public async Task<(string fileName, byte[] content)> GetColumnTemplateAsync(string sheetName = "Sheet1")
    {
        var (fileName, content) = await HbtExcelHelper.GenerateTemplateAsync<HbtGenColumnDefineTemplateDto>(sheetName);
        return (fileName, content);
    }

    /// <summary>
    /// 导出列定义
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的文件名和内容</returns>
    public async Task<(string fileName, byte[] content)> ExportColumnAsync(long tableId, string sheetName = "Sheet1")
    {
        try
        {
            var list = await _columnDefineRepository.GetListAsync(x => x.TableId == tableId);
            var exportList = list.Adapt<List<HbtGenColumnDefineExportDto>>();
            return await HbtExcelHelper.ExportAsync(exportList, sheetName, "列定义数据");
        }
        catch (Exception ex)
        {
            _logger.Error(L("Generator.ColumnDefine.Log.ExportDataFailed"), ex);
            throw new HbtException(L("Generator.ColumnDefine.ExportFailed"));
        }
    }

    #endregion

    /// <summary>
    /// 构建表定义查询条件
    /// </summary>
    private Expression<Func<HbtGenTableDefine, bool>> QueryExpression(HbtGenTableDefineQueryDto query)
    {
        var exp = Expressionable.Create<HbtGenTableDefine>();

        if (!string.IsNullOrEmpty(query.TableName))
            exp.And(x => x.TableName.Contains(query.TableName));

        if (!string.IsNullOrEmpty(query.TableComment))
            exp.And(x => x.TableComment.Contains(query.TableComment));

        return exp.ToExpression();
    }
}
