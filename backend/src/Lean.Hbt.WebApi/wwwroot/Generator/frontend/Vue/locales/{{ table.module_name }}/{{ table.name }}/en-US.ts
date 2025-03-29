//===================================================================
// Project : Lean.Hbt
// File    : en-US.ts
// Creator : CodeGenerator
// Time    : {{ datetime }}
// Version : v1.0.0
// Desc    : {{ table.comment }} Internationalization
//===================================================================

export default {
  {{ table.module_name }}: {
    {{ table.name }}: {
      title: '{{ table.comment }} Management',
      toolbar: {
        add: 'Add {{ table.comment }}',
        edit: 'Edit {{ table.comment }}',
        delete: 'Delete {{ table.comment }}',
        import: 'Import {{ table.comment }}',
        export: 'Export {{ table.comment }}',
        downloadTemplate: 'Download Template'
      },
      table: {
        columns: {
          {{~ for column in table.columns ~}}
          {{ column.column_name }}: '{{ column.comment_en }}'{{~ if !for.last ~}},{{~ end ~}}
          {{~ end ~}}
        },
        operation: {
          edit: 'Edit',
          delete: 'Delete'
        },
        status: {
          enabled: 'Enabled',
          disabled: 'Disabled',
          toggle: {
            enable: 'Enable',
            disable: 'Disable'
          }
        }
      },
      {{~ for column in table.columns ~}}
      {{ column.column_name }}: {
        label: '{{ column.comment_en }}'
        {{~ if column.column_name != "id" and column.column_name != "create_time" and column.column_name != "update_time" ~}}
        ,placeholder: 'Please input {{ column.comment_en }}'
        {{~ end ~}}
        {{~ if not column.is_nullable and column.column_name != "id" and column.column_name != "create_time" and column.column_name != "update_time" ~}}
        ,validation: {
          required: '{{ column.comment_en }} is required'
          {{~ if column.data_type == "string" ~}}
          ,length: '{{ column.comment_en }} cannot exceed {{ column.length }} characters'
          {{~ end ~}}
        }
        {{~ end ~}}
        {{~ if column.column_name == "status" ~}}
        ,options: {
          enabled: 'Enabled',
          disabled: 'Disabled'
        }
        {{~ end ~}}
      }{{~ if !for.last ~}},{{~ end ~}}
      {{~ end ~}},
      messages: {
        confirmDelete: 'Are you sure to delete the selected {{ table.comment }}?',
        deleteSuccess: '{{ table.comment }} deleted successfully',
        deleteFailed: '{{ table.comment }} deletion failed',
        saveSuccess: '{{ table.comment }} saved successfully',
        saveFailed: '{{ table.comment }} save failed',
        importSuccess: '{{ table.comment }} imported successfully',
        importFailed: '{{ table.comment }} import failed',
        exportSuccess: '{{ table.comment }} exported successfully',
        exportFailed: '{{ table.comment }} export failed'
      }
    }
  }
} 