#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtGenTableDefineService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表定义服务实现
//===================================================================

using Lean.Hbt.Application.Dtos.Generator;
using Lean.Hbt.Common.Models;
using Lean.Hbt.Domain.Entities.Generator;
using Lean.Hbt.Domain.Repositories;
using Lean.Hbt.Domain.Utils;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Linq.Expressions;
using System.Text;
using Scriban;

namespace Lean.Hbt.Application.Services.Generator;

/// <summary>
/// 代码生成表定义服务实现
/// </summary>
public class HbtGenTableDefineService : IHbtGenTableDefineService
{
    private readonly IHbtRepository<HbtGenTableDefine> _tableDefineRepository;
    private readonly IHbtRepository<HbtGenColumnDefine> _columnDefineRepository;
    private readonly ILogger<HbtGenTableDefineService> _logger;
    private readonly IHbtCurrentUser _currentUser;
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtGenTableDefineService(
        IHbtRepository<HbtGenTableDefine> tableDefineRepository,
        IHbtRepository<HbtGenColumnDefine> columnDefineRepository,
        ILogger<HbtGenTableDefineService> logger,
        IHbtCurrentUser currentUser,
        ISqlSugarClient db)
    {
        _tableDefineRepository = tableDefineRepository;
        _columnDefineRepository = columnDefineRepository;
        _logger = logger;
        _currentUser = currentUser;
        _db = db;
    }

    #region 基础操作

    /// <summary>
    /// 根据ID获取表定义信息
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>表定义信息</returns>
    public async Task<HbtGenTableDefineDto?> GetByIdAsync(long id)
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
    public async Task<HbtPagedResult<HbtGenTableDefineDto>> GetListAsync(HbtGenTableDefineQueryDto input)
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
            var columns = await _columnDefineRepository.GetListAsync(x => x.TableId == dto.Id);
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
    public async Task<HbtGenTableDefineDto> CreateAsync(HbtGenTableDefineCreateDto input)
    {
        // 检查表名是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_tableDefineRepository, nameof(HbtGenTableDefine.TableName), input.TableName);

        var entity = input.Adapt<HbtGenTableDefine>();

        // 创建表定义
        await _tableDefineRepository.CreateAsync(entity);

        // 创建列定义
        if (input.Columns != null && input.Columns.Any())
        {
            var columns = input.Columns.Adapt<List<HbtGenColumnDefine>>();
            foreach (var column in columns)
            {
                column.TableId = entity.Id;
                await _columnDefineRepository.CreateAsync(column);
            }
        }

        return await GetByIdAsync(entity.Id) ?? throw new HbtException("创建表定义失败");
    }

    /// <summary>
    /// 更新表定义信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表定义信息</returns>
    public async Task<HbtGenTableDefineDto> UpdateAsync(HbtGenTableDefineUpdateDto input)
    {
        var table = await _tableDefineRepository.GetByIdAsync(input.Id);
        if (table == null)
            throw new HbtException($"表定义[{input.Id}]不存在");

        // 检查表名是否已存在
        await HbtValidateUtils.ValidateFieldExistsAsync(_tableDefineRepository, nameof(HbtGenTableDefine.TableName), input.TableName, input.Id);

        // 更新表定义
        input.Adapt(table);
        await _tableDefineRepository.UpdateAsync(table);

        // 更新列定义
        if (input.Columns != null && input.Columns.Any())
        {
            // 删除旧的列定义
            var oldColumns = await _columnDefineRepository.GetListAsync(x => x.TableId == input.Id);
            foreach (var column in oldColumns)
            {
                await _columnDefineRepository.DeleteAsync(column.Id);
            }

            // 创建新的列定义
            var columns = input.Columns.Adapt<List<HbtGenColumnDefine>>();
            foreach (var column in columns)
            {
                column.TableId = input.Id;
                await _columnDefineRepository.CreateAsync(column);
            }
        }

        return await GetByIdAsync(input.Id) ?? throw new HbtException("更新表定义失败");
    }

    /// <summary>
    /// 删除表定义
    /// </summary>
    /// <param name="id">表定义ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
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
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        foreach (var id in ids)
        {
            await DeleteAsync(id);
        }
        return true;
    }

    #endregion

    #region 导入导出操作

    /// <summary>
    /// 导入表定义
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入的表定义列表</returns>
    public async Task<List<HbtGenTableDefineDto>> ImportTablesAsync(HbtGenTableDefineImportDto input)
    {
        var result = new List<HbtGenTableDefineDto>();

        // 创建表信息
        var table = new HbtGenTableDefine
        {
            DatabaseName = input.DatabaseName,
            TableName = input.TableName,
            TableComment = input.TableComment,
            ClassName = input.ClassName,
            Namespace = input.Namespace,
            BaseNamespace = input.BaseNamespace,
            CsharpTypeName = input.CsharpTypeName,
            ParentTableName = input.ParentTableName,
            ParentTableFkName = input.ParentTableFkName,
            TemplateType = input.TemplateType,
            ModuleName = input.ModuleName,
            BusinessName = input.BusinessName,
            FunctionName = input.FunctionName,
            Author = input.Author,
            GenMode = input.GenMode,
            GenPath = input.GenPath,
            Options = input.Options,
            Fields = input.Columns.Select(c => new HbtGenColumnDefine
            {
                ColumnName = c.ColumnName,
                ColumnComment = c.ColumnComment,
                DbColumnType = c.DbColumnType,
                CsharpType = c.CsharpType,
                CsharpColumn = c.CsharpColumn,
                CsharpLength = Convert.ToInt32(c.CsharpLength),
                CsharpDecimalDigits = Convert.ToInt32(c.CsharpDecimalDigits),
                IsIncrement = Convert.ToInt32(c.IsIncrement),
                IsPrimaryKey = Convert.ToInt32(c.IsPrimaryKey),
                IsRequired = Convert.ToInt32(c.IsRequired),
                IsInsert = Convert.ToInt32(c.IsInsert),
                IsEdit = Convert.ToInt32(c.IsEdit),
                IsList = Convert.ToInt32(c.IsList),
                IsQuery = Convert.ToInt32(c.IsQuery),
                QueryType = c.QueryType,
                IsSort = Convert.ToInt32(c.IsSort),
                IsExport = Convert.ToInt32(c.IsExport),
                DisplayType = c.DisplayType,
                DictType = c.DictType,
                OrderNum = Convert.ToInt32(c.OrderNum)
            }).ToList()
        };

        await _tableDefineRepository.CreateAsync(table);
        result.Add(table.Adapt<HbtGenTableDefineDto>());

        return result;
    }

    /// <summary>
    /// 导出表定义
    /// </summary>
    /// <returns>导出的表定义列表</returns>
    public async Task<List<HbtGenTableDefineExportDto>> ExportTablesAsync()
    {
        var tables = await _tableDefineRepository.GetListAsync();
        var result = new List<HbtGenTableDefineExportDto>();

        foreach (var table in tables)
        {
            var dto = table.Adapt<HbtGenTableDefineExportDto>();
            var columns = await _columnDefineRepository.GetListAsync(x => x.TableId == table.Id);
            dto.Columns = columns.Adapt<List<HbtGenColumnDefineDto>>();
            result.Add(dto);
        }

        return result;
    }

    /// <summary>
    /// 获取表定义模板
    /// </summary>
    /// <returns>模板结果</returns>
    public async Task<HbtGenTableDefineTemplateDto> GetTemplateAsync()
    {
        return new HbtGenTableDefineTemplateDto();
    }

    #endregion

    #region 特殊操作

    /// <summary>
    /// 初始化表结构
    /// </summary>
    /// <param name="input">初始化参数</param>
    /// <returns>初始化结果</returns>
    public async Task<List<HbtGenTableDefineDto>> InitializeTablesAsync(HbtGenTableDefineCreateDto input)
    {
        var result = new List<HbtGenTableDefineDto>();

        try
        {
            // 1. 创建表定义
            var dto = await CreateAsync(input);
            if (dto == null)
                throw new HbtException("创建表定义失败");

            result.Add(dto);

            // 2. 根据生成模式和模板类型选择模板文件
            var templateFile = (dto.GenMode, dto.TemplateType) switch
            {
                (1, _) => "Entity.scriban", // 基本实体
                (2, _) => "MasterDetailEntity.scriban", // 主从表实体
                (3, _) => "TreeEntity.scriban", // 树形结构实体
                _ => throw new HbtException($"不支持的生成模式: GenMode={dto.GenMode}, TemplateType={dto.TemplateType}")
            };

            // 3. 读取模板文件
            var templatePath = Path.Combine("backend", "src", "Lean.Hbt.WebApi", "wwwroot", "Generator", "backend", "Csharp", "Entities", templateFile);
            if (!File.Exists(templatePath))
                throw new HbtException($"模板文件不存在: {templatePath}");

            var templateContent = await File.ReadAllTextAsync(templatePath);

            // 4. 准备模板数据
            var templateData = new
            {
                table = new
                {
                    name = dto.TableName,
                    comment = dto.TableComment,
                    module_name = dto.ModuleName,
                    table_name = dto.TableName,
                    columns = dto.Columns.Select(c => new
                    {
                        column_name = c.ColumnName,
                        column_comment = c.ColumnComment,
                        data_type = c.DbColumnType,
                        is_pk = c.IsPrimaryKey == 1,
                        is_increment = c.IsIncrement == 1,
                        is_nullable = c.IsRequired == 0,
                        length = c.CsharpLength,
                        precision = c.CsharpLength,
                        scale = c.CsharpDecimalDigits,
                        default_value = GetDefaultValue(c.CsharpType)
                    })
                },
                date = DateTime.Now.ToString("yyyy-MM-dd")
            };

            // 5. 渲染模板
            var template = Template.Parse(templateContent);
            var resultContent = await template.RenderAsync(templateData);

            // 6. 保存文件
            var modulePath = Path.Combine("backend", "src", "Lean.Hbt.Domain", "Entities", dto.ModuleName);
            if (!Directory.Exists(modulePath))
                Directory.CreateDirectory(modulePath);

            var filePath = Path.Combine(modulePath, $"{dto.ClassName}.cs");
            await File.WriteAllTextAsync(filePath, resultContent);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "初始化表结构失败");
            throw;
        }
    }

    /// <summary>
    /// 获取默认值
    /// </summary>
    private string GetDefaultValue(string csharpType)
    {
        return csharpType switch
        {
            "string" => "",
            "int" => "0",
            "long" => "0",
            "decimal" => "0",
            "double" => "0",
            "float" => "0",
            "DateTime" => "",
            "bool" => "false",
            _ => ""
        };
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
            _logger.LogError(ex, "同步表结构失败");
            return false;
        }
    }

    #endregion
}
