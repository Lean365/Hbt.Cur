export default {
  admin: {
    translation: {
      title: '翻译管理',
      id: '翻译ID',
      langCode: {
        label: '语言编码',
        placeholder: '请选择语言编码',
        validation: {
          required: '语言编码不能为空'
        }
      },
      module: {
        label: '模块名称',
        placeholder: '请输入模块名称',
        validation: {
          required: '模块名称不能为空'
        }
      },
      key: {
        label: '翻译键名',
        placeholder: '请输入翻译键名',
        validation: {
          required: '翻译键名不能为空'
        }
      },
      value: {
        label: '翻译内容',
        placeholder: '请输入翻译内容',
        validation: {
          required: '翻译内容不能为空'
        }
      },
      remark: {
        label: '备注',
        placeholder: '请输入备注'
      },
      actions: {
        import: '导入',
        export: '导出',
        template: '下载模板',
        refresh: '刷新缓存'
      }
    }
  }
} 