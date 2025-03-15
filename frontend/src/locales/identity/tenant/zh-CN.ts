export default {
  identity: {
    tenant: {
      title: '租户管理',
      fields: {
        tenantId: {
          label: '租户ID'
        },
        tenantName: {
          label: '租户名称',
          placeholder: '请输入租户名称',
          validation: {
            required: '租户名称不能为空',
            length: '租户名称长度必须在2-30个字符之间'
          }
        },
        tenantKey: {
          label: '租户标识',
          placeholder: '请输入租户标识',
          validation: {
            required: '租户标识不能为空',
            length: '租户标识长度必须在2-20个字符之间'
          }
        },
        contactUser: {
          label: '联系人',
          placeholder: '请输入联系人姓名'
        },
        contactPhone: {
          label: '联系电话',
          placeholder: '请输入联系电话'
        },
        email: {
          label: '邮箱',
          placeholder: '请输入邮箱'
        },
        address: {
          label: '地址',
          placeholder: '请输入地址'
        },
        domain: {
          label: '域名',
          placeholder: '请输入域名'
        },
        licenseType: {
          label: '授权类型',
          placeholder: '请选择授权类型',
          options: {
            trial: '试用版',
            standard: '标准版',
            professional: '专业版',
            enterprise: '企业版'
          }
        },
        expireTime: {
          label: '过期时间',
          placeholder: '请选择过期时间'
        },
        status: {
          label: '状态',
          placeholder: '请选择状态',
          options: {
            enabled: '启用',
            disabled: '禁用',
            expired: '已过期'
          }
        },
        description: {
          label: '备注',
          placeholder: '请输入备注'
        },
        createTime: '创建时间'
      },
      actions: {
        add: '新增租户',
        edit: '编辑租户',
        delete: '删除租户',
        export: '导出租户',
        renew: '续期',
        config: '参数配置'
      },
      messages: {
        confirmDelete: '是否确认删除名称为"{name}"的租户？',
        deleteSuccess: '租户删除成功',
        deleteFailed: '租户删除失败',
        saveSuccess: '租户信息保存成功',
        saveFailed: '租户信息保存失败',
        renewSuccess: '租户续期成功',
        renewFailed: '租户续期失败',
        configSuccess: '参数配置保存成功',
        configFailed: '参数配置保存失败'
      }
    }
  }
} 