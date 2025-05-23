//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtDbSeedGenTemplate.cs
// 创建者 : Lean365
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 代码生成模板种子数据
//===================================================================

using Lean.Hbt.Domain.Entities.Generator;
using Lean.Hbt.Domain.IServices.Extensions;

namespace Lean.Hbt.Infrastructure.Data.Seeds;

/// <summary>
/// 代码生成模板种子数据
/// </summary>
public class HbtDbSeedGenTemplate
{
    private readonly IHbtRepository<HbtGenTemplate> _genTemplateRepository;
    private readonly IHbtLogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public HbtDbSeedGenTemplate(IHbtRepository<HbtGenTemplate> genTemplateRepository, IHbtLogger logger)
    {
        _genTemplateRepository = genTemplateRepository;
        _logger = logger;
    }

    /// <summary>
    /// 初始化代码生成模板数据
    /// </summary>
    public async Task<(int insertCount, int updateCount)> InitializeGenTemplateAsync(long tenantId)
    {
        var seedData = GetSeedData();
        var insertCount = 0;
        var updateCount = 0;

        foreach (var template in seedData)
        {
            var existingTemplate = await _genTemplateRepository.GetFirstAsync(x => x.TemplateName == template.TemplateName);

            if (existingTemplate == null)
            {
                template.TenantId = tenantId;
                template.CreateBy = "Hbt365";
                template.CreateTime = DateTime.Now;
                template.UpdateBy = "Hbt365";
                template.UpdateTime = DateTime.Now;
                
                await _genTemplateRepository.CreateAsync(template);
                insertCount++;
                _logger.Info($"[创建] 代码生成模板 '{template.TemplateName}' 创建成功");
            }
            else
            {
                existingTemplate.TemplateName = template.TemplateName;
                existingTemplate.TemplateType = template.TemplateType;
                existingTemplate.TemplateCategory = template.TemplateCategory;
                existingTemplate.TemplateLanguage = template.TemplateLanguage;
                existingTemplate.TemplateVersion = template.TemplateVersion;
                existingTemplate.FileName = template.FileName;
                existingTemplate.TemplateContent = template.TemplateContent;
                existingTemplate.TenantId = tenantId;
                existingTemplate.UpdateBy = "Hbt365";
                existingTemplate.UpdateTime = DateTime.Now;

                await _genTemplateRepository.UpdateAsync(existingTemplate);
                updateCount++;
                _logger.Info($"[更新] 代码生成模板 '{existingTemplate.TemplateName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取种子数据
    /// </summary>
    private static IEnumerable<HbtGenTemplate> GetSeedData()
    {
        return new List<HbtGenTemplate>
        {
            new HbtGenTemplate
            {
                TemplateName = "Entity",
                TemplateType = 0,
                TemplateCategory = 1,
                TemplateLanguage = 1,
                TemplateVersion = 1,
                FileName = "{{className}}.cs",
                TemplateContent = @"using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace {{base_namespace}}.Domain.Entities.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}
    /// </summary>
    [SugarTable(""{{table_name}}"", ""{{table_comment}}"")]
    public class {{className}} : HbtBaseEntity
    {
        {{for column in columns}}
        /// <summary>
        /// {{column.column_comment}}
        /// </summary>
        [SugarColumn(ColumnName = ""{{column.column_name}}"", ColumnDescription = ""{{column.column_comment}}"", {{if column.is_primary_key}}IsPrimaryKey = true, {{end}}{{if column.is_identity}}IsIdentity = true, {{end}}{{if column.length > 0}}Length = {{column.length}}, {{end}}ColumnDataType = ""{{column.data_type}}"", IsNullable = {{column.is_nullable}})]
        public {{column.csharp_type}} {{column.property_name}} { get; set; }
        {{end}}
    }
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "Dto",
                TemplateType = 1,
                TemplateCategory = 1,
                TemplateLanguage = 1,
                TemplateVersion = 1,
                FileName = "{{className}}Dto.cs",
                TemplateContent = @"using System.ComponentModel.DataAnnotations;

namespace {{base_namespace}}.Domain.Dtos.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}数据传输对象
    /// </summary>
    public class {{className}}Dto
    {
        {{for column in columns}}
        /// <summary>
        /// {{column.column_comment}}
        /// </summary>
        [Display(Name = ""{{column.column_comment}}"")]
        public {{column.csharp_type}} {{column.property_name}} { get; set; }
        {{end}}
    }
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "IService",
                TemplateType = 2,
                TemplateCategory = 1,
                TemplateLanguage = 1,
                TemplateVersion = 1,
                FileName = "I{{className}}Service.cs",
                TemplateContent = @"using {{base_namespace}}.Domain.Dtos.{{module_name}};
using {{base_namespace}}.Domain.Entities.{{module_name}};

namespace {{base_namespace}}.Domain.IServices.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}服务接口
    /// </summary>
    public interface I{{className}}Service
    {
        /// <summary>
        /// 获取{{table_comment}}列表
        /// </summary>
        Task<List<{{className}}Dto>> GetListAsync();

        /// <summary>
        /// 获取{{table_comment}}详情
        /// </summary>
        Task<{{className}}Dto> GetInfoAsync(long id);

        /// <summary>
        /// 创建{{table_comment}}
        /// </summary>
        Task<{{className}}Dto> CreateAsync({{className}}Dto dto);

        /// <summary>
        /// 更新{{table_comment}}
        /// </summary>
        Task<{{className}}Dto> UpdateAsync({{className}}Dto dto);

        /// <summary>
        /// 删除{{table_comment}}
        /// </summary>
        Task DeleteAsync(long id);
    }
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "Service",
                TemplateType = 3,
                TemplateCategory = 1,
                TemplateLanguage = 1,
                TemplateVersion = 1,
                FileName = "{{className}}Service.cs",
                TemplateContent = @"using {{base_namespace}}.Domain.Dtos.{{module_name}};
using {{base_namespace}}.Domain.Entities.{{module_name}};
using {{base_namespace}}.Domain.IServices.{{module_name}};
using {{base_namespace}}.Domain.IServices.Extensions;

namespace {{base_namespace}}.Infrastructure.Services.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}服务实现
    /// </summary>
    public class {{className}}Service : I{{className}}Service
    {
        private readonly IHbtRepository<{{className}}> _repository;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public {{className}}Service(IHbtRepository<{{className}}> repository, IHbtLogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// 获取{{table_comment}}列表
        /// </summary>
        public async Task<List<{{className}}Dto>> GetListAsync()
        {
            var list = await _repository.GetListAsync();
            return list.Select(x => new {{className}}Dto
            {
                {{for column in columns}}
                {{column.property_name}} = x.{{column.property_name}},
                {{end}}
            }).ToList();
        }

        /// <summary>
        /// 获取{{table_comment}}详情
        /// </summary>
        public async Task<{{className}}Dto> GetInfoAsync(long id)
        {
            var entity = await _repository.GetFirstAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception($""未找到ID为{id}的{{table_comment}}"");
            }

            return new {{className}}Dto
            {
                {{for column in columns}}
                {{column.property_name}} = entity.{{column.property_name}},
                {{end}}
            };
        }

        /// <summary>
        /// 创建{{table_comment}}
        /// </summary>
        public async Task<{{className}}Dto> CreateAsync({{className}}Dto dto)
        {
            var entity = new {{className}}
            {
                {{for column in columns}}
                {{column.property_name}} = dto.{{column.property_name}},
                {{end}}
            };

            await _repository.CreateAsync(entity);
            return dto;
        }

        /// <summary>
        /// 更新{{table_comment}}
        /// </summary>
        public async Task<{{className}}Dto> UpdateAsync({{className}}Dto dto)
        {
            var entity = await _repository.GetFirstAsync(x => x.Id == dto.Id);
            if (entity == null)
            {
                throw new Exception($""未找到ID为{dto.Id}的{{table_comment}}"");
            }

            {{for column in columns}}
            entity.{{column.property_name}} = dto.{{column.property_name}};
            {{end}}

            await _repository.UpdateAsync(entity);
            return dto;
        }

        /// <summary>
        /// 删除{{table_comment}}
        /// </summary>
        public async Task DeleteAsync(long id)
        {
            var entity = await _repository.GetFirstAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception($""未找到ID为{id}的{{table_comment}}"");
            }

            await _repository.DeleteAsync(entity);
        }
    }
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "IRepository",
                TemplateType = 5,
                TemplateCategory = 1,
                TemplateLanguage = 1,
                TemplateVersion = 1,
                FileName = "I{{className}}Repository.cs",
                TemplateContent = @"using {{base_namespace}}.Domain.Entities.{{module_name}};

namespace {{base_namespace}}.Domain.IRepositories.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}仓储接口
    /// </summary>
    public interface I{{className}}Repository : IHbtRepository<{{className}}>
    {
    }
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "Repository",
                TemplateType = 6,
                TemplateCategory = 1,
                TemplateLanguage = 1,
                TemplateVersion = 1,
                FileName = "{{className}}Repository.cs",
                TemplateContent = @"using {{base_namespace}}.Domain.Entities.{{module_name}};
using {{base_namespace}}.Domain.IRepositories.{{module_name}};

namespace {{base_namespace}}.Infrastructure.Repositories.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}仓储实现
    /// </summary>
    public class {{className}}Repository : HbtRepository<{{className}}>, I{{className}}Repository
    {
        public {{className}}Repository(IHbtDbContext dbContext) : base(dbContext)
        {
        }
    }
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "api.ts",
                TemplateType = 7,
                TemplateCategory = 2,
                TemplateLanguage = 2,
                TemplateVersion = 1,
                FileName = "api.ts",
                TemplateContent = @"import request from '@/utils/request';
import type { {{className}}Dto } from './type';

export function getList() {
  return request.get<{{className}}Dto[]>('/api/{{table_name}}');
}

export function getInfo(id: number) {
  return request.get<{{className}}Dto>(`/api/{{table_name}}/${id}`);
}

export function create(data: {{className}}Dto) {
  return request.post('/api/{{table_name}}', data);
}

export function update(data: {{className}}Dto) {
  return request.put('/api/{{table_name}}', data);
}

export function remove(id: number) {
  return request.delete(`/api/{{table_name}}/${id}`);
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "type.d.ts",
                TemplateType = 8,
                TemplateCategory = 2,
                TemplateLanguage = 2,
                TemplateVersion = 1,
                FileName = "type.d.ts",
                TemplateContent = @"export interface {{className}}Dto {
  {{for column in columns}}
  /** {{column.column_comment}} */
  {{column.property_name}}: {{column.ts_type}};
  {{end}}
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "index.vue",
                TemplateType = 9,
                TemplateCategory = 2,
                TemplateLanguage = 5,
                TemplateVersion = 1,
                FileName = "index.vue",
                TemplateContent = @"<template>
  <div class='{{table_name}}-manage'>
    <ManageForm />
  </div>
</template>
<script setup lang='ts'>
import ManageForm from './components/ManageForm.vue';
</script>
<style scoped>
.{{table_name}}-manage {
  padding: 16px;
}
</style>",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "ManageForm.vue",
                TemplateType = 10,
                TemplateCategory = 2,
                TemplateLanguage = 5,
                TemplateVersion = 1,
                FileName = "components/ManageForm.vue",
                TemplateContent = @"<template>
  <el-form :model='form'>
    <!-- 动态生成表单项 -->
    {{for column in columns}}
    <el-form-item label='{{column.column_comment}}'>
      <el-input v-model='form.{{column.property_name}}' />
    </el-form-item>
    {{end}}
  </el-form>
</template>
<script setup lang='ts'>
import { ref } from 'vue';
import type { {{className}}Dto } from '../type';

const form = ref<{{className}}Dto>({});
</script>
<style scoped>
</style>",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "zh-CN.ts",
                TemplateType = 11,
                TemplateCategory = 3,
                TemplateLanguage = 2,
                TemplateVersion = 1,
                FileName = "locales/zh-CN.ts",
                TemplateContent = @"export default {
  title: '{{table_comment}}管理',
  fields: {
    {{for column in columns}}
    {{column.property_name}}: '{{column.column_comment}}',
    {{end}}
  }
};",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "en-US.ts",
                TemplateType = 12,
                TemplateCategory = 3,
                TemplateLanguage = 2,
                TemplateVersion = 1,
                FileName = "locales/en-US.ts",
                TemplateContent = @"export default {
  title: '{{table_comment}} Management',
  fields: {
    {{for column in columns}}
    {{column.property_name}}: '{{column.column_comment}}',
    {{end}}
  }
};",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            },
            new HbtGenTemplate
            {
                TemplateName = "Controller",
                TemplateType = 4,
                TemplateCategory = 1,
                TemplateLanguage = 1,
                TemplateVersion = 1,
                FileName = "{{className}}Controller.cs",
                TemplateContent = @"using {{base_namespace}}.Domain.Dtos.{{module_name}};
using {{base_namespace}}.Domain.IServices.{{module_name}};
using Microsoft.AspNetCore.Mvc;

namespace {{base_namespace}}.Web.Controllers.{{module_name}}
{
    /// <summary>
    /// {{table_comment}}控制器
    /// </summary>
    [Route(""api/[controller]"")]
    [ApiController]
    public class {{className}}Controller : ControllerBase
    {
        private readonly I{{className}}Service _service;
        private readonly IHbtLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public {{className}}Controller(I{{className}}Service service, IHbtLogger logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// 获取{{table_comment}}列表
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<{{className}}Dto>>> GetList()
        {
            try
            {
                var list = await _service.GetListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.Error(""获取{{table_comment}}列表失败"", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 获取{{table_comment}}详情
        /// </summary>
        [HttpGet(""{id}"")]
        public async Task<ActionResult<{{className}}Dto>> GetInfo(long id)
        {
            try
            {
                var info = await _service.GetInfoAsync(id);
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.Error($""获取{{table_comment}}详情失败，ID：{id}"", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 创建{{table_comment}}
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<{{className}}Dto>> Create([FromBody] {{className}}Dto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(""创建{{table_comment}}失败"", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 更新{{table_comment}}
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<{{className}}Dto>> Update([FromBody] {{className}}Dto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($""更新{{table_comment}}失败，ID：{dto.Id}"", ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除{{table_comment}}
        /// </summary>
        [HttpDelete(""{id}"")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($""删除{{table_comment}}失败，ID：{id}"", ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}",
                CreateBy = "Hbt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Hbt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}
