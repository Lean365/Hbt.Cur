export default {
  core: {
    translation: {
      title: '翻译管理',
      table: {
        columns: {
          translationId: 'ID',
          langCode: '语言编码',         
          transKey: '翻译键',
          transValue: '翻译值',
          moduleName: '模块名称',
          orderNum: '排序',
          status: '状态',
          tenantId: '租户ID',
        }
      },
      fields: {
        translationId: {
          label: 'ID',  
          placeholder: '请输入ID'
        },
        langCode: {
          label: '语言编码',
          placeholder: '请输入语言编码',
          validation: {
            required: '语言编码不能为空'
          }
        },
        transKey: {
          label: '翻译键',
          placeholder: '请输入翻译键',
          validation: {
            required: '翻译键不能为空',
            length: '翻译键长度必须在2-100个字符之间'
          }
        },
        transValue: {
          label: '翻译值',
          placeholder: '请输入翻译值',
          validation: {
            required: '翻译值不能为空',
            length: '翻译值长度必须在2-100个字符之间'
          }
        },
        moduleName: {
          label: '模块名称',
          placeholder: '请输入模块名称',
          validation: {
            required: '模块名称不能为空',
            length: '模块名称长度必须在2-100个字符之间'
          }
        },
        remark: {
          label: '备注',
          placeholder: '请输入备注',
          validation: {
            required: '备注不能为空'
          }
        }
      },
      actions: {
        create: '新增翻译',
        edit: '编辑翻译',
        delete: '删除翻译',
        view: '查看翻译',
        import: '导入翻译',
        export: '导出翻译',
        detail: '翻译详情'
        
      },
      message: {
        success: '操作成功',
        error: '操作失败',
        warning: '警告',
        info: '信息',
        confirm: '确认',
        deleteSuccess: '删除成功',
        deleteFailed: '删除失败',
        saveSuccess: '保存成功',
        saveFailed: '保存失败',
        importSuccess: '导入成功',
        importFailed: '导入失败'
      }
    }
  }
}
