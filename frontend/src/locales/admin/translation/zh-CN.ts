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
      key: '翻译键',
      value: '翻译值',
      remark: {
        label: '备注',
        placeholder: '请输入备注'
      },
      language: '语言',
      actions: {
        import: '导入',
        export: '导出',
        template: '下载模板',
        refresh: '刷新缓存'
      }
    }
  }
} 