export default {
  admin: {
    translation: {
      title: 'Translation Management',
      id: 'Translation ID',
      langCode: {
        label: 'Language Code',
        placeholder: 'Please select language code',
        validation: {
          required: 'Language code cannot be empty'
        }
      },
      module: {
        label: 'Module Name',
        placeholder: 'Please enter module name',
        validation: {
          required: 'Module name cannot be empty'
        }
      },
      key: {
        label: 'Translation Key',
        placeholder: 'Please enter translation key',
        validation: {
          required: 'Translation key cannot be empty'
        }
      },
      value: {
        label: 'Translation Value',
        placeholder: 'Please enter translation value',
        validation: {
          required: 'Translation value cannot be empty'
        }
      },
      remark: {
        label: 'Remark',
        placeholder: 'Please enter remark'
      },
      actions: {
        import: 'Import',
        export: 'Export',
        template: 'Download Template',
        refresh: 'Refresh Cache'
      }
    }
  }
} 