
export default {
  core: {
    config: {
      title: '系统配置',
      table: {
        columns: {
          configId: '配置ID',          
          configName: '配置名称',
          configKey: '配置键名',
          configValue: '配置键值',
          isBuiltin: '系统内置',
          orderNum: '排序号',
          status: '状态',
          isEncrypted: '是否加密'
        }
      },
      fields: {
        configId: {
          label: '配置ID'
        },
        configName: {
          label: '配置名称',
          placeholder: '请输入配置名称',
          validation: {
            required: '配置名称不能为空',
            length: '配置名称长度必须在2-50个字符之间',
            maxLength: '配置名称长度不能超过50个字符'
          }
        },
        configKey: {
          label: '配置键名',
          placeholder: '请输入配置键名',
          validation: {
            required: '配置键名不能为空',
            length: '配置键名长度必须在2-50个字符之间',
            maxLength: '配置键名长度不能超过50个字符',
            pattern: '配置键名必须以字母或下划线开头，只能包含字母、数字、下划线和冒号'
          }
        },
        configValue: {
          label: '配置键值',
          placeholder: '请输入配置键值',
          validation: {
            required: '配置键值不能为空',
            length: '配置键值长度必须在2-500个字符之间',
            maxLength: '配置键值长度不能超过500个字符'
          }
        },
        isBuiltin: {
          label: '系统内置',
          placeholder: '请选择是否系统内置',
          options: {
            yes: '是',
            no: '否'
          }
        },
        orderNum: {
          label: '排序号',
          placeholder: '请输入排序号',
          validation: {
            required: '排序号不能为空',
            type: 'number'
          }
        },
        remark: {
          label: '备注',
          placeholder: '请输入备注',
          validation: {
            length: '备注长度必须在2-200个字符之间'
          }
        },
        status: {
          label: '状态',
          placeholder: '请选择状态',
          options: {
            enabled: '启用',
            disabled: '禁用'
          }
        },
        isEncrypted: {
          label: '是否加密',
          placeholder: '请选择是否加密',
          options: {
            yes: '是',
            no: '否'
          }
        }
      },
      validation: {
        required: '不能为空',
        length: '长度必须在{min}-{max}个字符之间',
        type: {
          number: '请输入数字'
        }
      },
      options: {
        status: {
          enabled: '启用',
          disabled: '禁用'
        },
        isBuiltin: {
          yes: '是',
          no: '否'
        }
      },
      actions: {
        create: '新增配置',
        update: '编辑配置',
        delete: '删除配置',
        export: '导出配置',
        import: '导入配置',
        refresh: '刷新缓存'
      },
      messages: {
        confirmDelete: '是否确认删除名称为"{name}"的配置？',
        deleteSuccess: '配置删除成功',
        deleteFailed: '配置删除失败',
        saveSuccess: '配置信息保存成功',
        saveFailed: '配置信息保存失败',
        importSuccess: '配置导入成功',
        importFailed: '配置导入失败',
        refreshSuccess: '配置缓存刷新成功',
        refreshFailed: '配置缓存刷新失败'
      }
    }
  }
}
