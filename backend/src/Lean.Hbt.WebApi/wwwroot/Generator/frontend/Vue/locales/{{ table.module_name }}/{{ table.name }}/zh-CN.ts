//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : zh-CN.ts
// 创建者 : CodeGenerator
// 创建时间: {{ datetime }}
// 版本号 : v1.0.0
// 描述    : {{ table.comment }}国际化
//===================================================================

export default {
  {{ table.module_name }}: {
    {{ table.name }}: {
      title: '{{ table.comment }}管理',
      toolbar: {
        add: '新增{{ table.comment }}',
        edit: '编辑{{ table.comment }}',
        delete: '删除{{ table.comment }}',
        import: '导入{{ table.comment }}',
        export: '导出{{ table.comment }}',
        downloadTemplate: '下载模板'
      },
      table: {
        columns: {
          {{~ for column in table.columns ~}}
          {{ column.column_name }}: '{{ column.comment }}'{{~ if !for.last ~}},{{~ end ~}}
          {{~ end ~}}
        },
        operation: {
          edit: '编辑',
          delete: '删除'
        },
        status: {
          enabled: '启用',
          disabled: '禁用',
          toggle: {
            enable: '启用',
            disable: '禁用'
          }
        }
      },
      {{~ for column in table.columns ~}}
      {{ column.column_name }}: {
        label: '{{ column.comment }}'
        {{~ if column.column_name != "id" and column.column_name != "create_time" and column.column_name != "update_time" ~}}
        ,placeholder: '请输入{{ column.comment }}'
        {{~ end ~}}
        {{~ if not column.is_nullable and column.column_name != "id" and column.column_name != "create_time" and column.column_name != "update_time" ~}}
        ,validation: {
          required: '{{ column.comment }}不能为空'
          {{~ if column.data_type == "string" ~}}
          ,length: '{{ column.comment }}长度不能超过{{ column.length }}个字符'
          {{~ end ~}}
        }
        {{~ end ~}}
        {{~ if column.column_name == "status" ~}}
        ,options: {
          enabled: '启用',
          disabled: '禁用'
        }
        {{~ end ~}}
      }{{~ if !for.last ~}},{{~ end ~}}
      {{~ end ~}},
      messages: {
        confirmDelete: '是否确认删除选中的{{ table.comment }}？',
        deleteSuccess: '{{ table.comment }}删除成功',
        deleteFailed: '{{ table.comment }}删除失败',
        saveSuccess: '{{ table.comment }}保存成功',
        saveFailed: '{{ table.comment }}保存失败',
        importSuccess: '{{ table.comment }}导入成功',
        importFailed: '{{ table.comment }}导入失败',
        exportSuccess: '{{ table.comment }}导出成功',
        exportFailed: '{{ table.comment }}导出失败'
      }
    }
  }
} 