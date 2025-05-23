export default {
  core: {
    language: {
      title: '语言管理',
      table: {
        columns: {
          languageId: 'ID',
          langCode: '代码',
          langName: '名称',
          langIcon: '图标',
          orderNum: '排序',
          isDefault: '默认',
          isBuiltin: '内置',
          status: '状态',
        }
      },
      fields: {
        languageId: {
          label: 'ID',    
          placeholder: '请输入ID'
        },
        langCode: {   
          label: '代码',
          placeholder: '请输入代码',
          validation: {
            required: '代码不能为空',
            maxLength: '代码长度不能超过50个字符'
          }

        },
        langName: {
          label: '名称',
          placeholder: '请输入名称',
          validation: {
            required: '名称不能为空',
            maxLength: '名称长度不能超过50个字符'
          }
        },
        langIcon: { 
          label: '图标',
          placeholder: '请输入图标',
          validation: {
            required: '图标不能为空',
            maxLength: '图标长度不能超过50个字符'
          }
        },
        orderNum: {
          label: '排序',
          placeholder: '请输入排序'
        },
        isDefault: {
          label: '默认',
          placeholder: '请输入默认'
        },
        isBuiltin: {
          label: '内置',
          placeholder: '请输入内置'
        },
        status: {
          label: '状态',
          placeholder: '请输入状态'
        },
        remark: {
          label: '备注',
          placeholder: '请输入备注'
        }
      },
      actions: {
        add: '新增',
        edit: '编辑',
        delete: '删除',
        view: '查看',
        translate: '翻译'
      },
      dialog: {
        title: {
          add: '新增语言',
          edit: '编辑语言',
          view: '查看语言',
          translate: '翻译语言'
      }
    },
    message: {
      success: '操作成功',
      error: '操作失败',
      warning: '警告',
      info: '信息',
      confirm: '确认',
    }
  }
  }
} 
