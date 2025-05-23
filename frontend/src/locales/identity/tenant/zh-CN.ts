import { theme } from "ant-design-vue";

export default {
  identity: {
    tenant: {
      title: '租户管理',
      table: {
        columns: {
          tenantId: '租户ID',
          tenantNo: '租户编号',
          tenantName: '租户名称',
          tenantCode: '租户标识',
          contactUser: '联系人',
          contactPhone: '联系电话',
          contactEmail: '邮箱',
          address: '地址',
          license: '许可证',
          expireTime: '过期时间',
          status: '状态',
          isDefault: '是否默认',
          dbConnection: '数据库连接',
          domain: '域名',
          logoUrl: 'Logo URL',
          theme: '主题',
          licenseStartTime: '许可证开始时间',
          licenseEndTime: '许可证结束时间',
          maxUserCount: '最大用户数'
        }
      },
      fields: {
        tenantId: {
          label: '租户ID'
        },
        tenantNo: {
          label: '租户编号',
          placeholder: '请输入租户编号',
          validation: {
            required: '租户编号不能为空',
            length: '租户编号长度必须在2-20个字符之间'  
          }
        },
        tenantName: {
          label: '租户名称',
          placeholder: '请输入租户名称',
          validation: {
            required: '租户名称不能为空',
            length: '租户名称长度必须在2-30个字符之间'
          }
        },
        tenantCode: {
          label: '租户标识',
          placeholder: '请输入租户标识',
          validation: {
            required: '租户标识不能为空',
            length: '租户标识长度必须在2-20个字符之间'
          }
        },
        contactUser: {
          label: '联系人',
          placeholder: '请输入联系人姓名',
          validation: {
            required: '联系人不能为空',
            length: '联系人长度必须在2-20个字符之间'
          }
        },
        contactPhone: {
          label: '联系电话',
          placeholder: '请输入联系电话',
          validation: {
            required: '联系电话不能为空',
            length: '联系电话长度必须在2-20个字符之间'
          }
        },
        contactEmail: {
          label: '邮箱',
          placeholder: '请输入邮箱',
          validation: {
            required: '邮箱不能为空',
            length: '邮箱长度必须在2-50个字符之间'
          }
        },
        address: {
          label: '地址',
          placeholder: '请输入地址',
          validation: {
            required: '地址不能为空',
            length: '地址长度必须在2-100个字符之间'
          }
        },
        license: {
          label: '许可证',
          placeholder: '请选择许可证',
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
        isDefault: {
          label: '是否默认',
          placeholder: '请选择是否默认'
        },
        dbConnection: { 
          label: '数据库连接',
          placeholder: '请输入数据库连接',
          validation: {
            required: '数据库连接不能为空',
            length: '数据库连接长度必须在2-100个字符之间'
          }
        },
        domain: {
          label: '域名',
          placeholder: '请输入域名',
          validation: {
            required: '域名不能为空',
            length: '域名长度必须在2-100个字符之间'
          }
        },
        logoUrl: {
          label: 'Logo URL',
          placeholder: '请输入Logo URL',
          validation: {
            required: 'Logo URL不能为空',
            length: 'Logo URL长度必须在2-100个字符之间'
          }
        },
        theme: {
          label: '主题',
          placeholder: '请选择主题',
          validation: {
            required: '主题不能为空',
            length: '主题长度必须在2-100个字符之间'
          }
        },
        licenseStartTime: {
          label: '起始时间',
          placeholder: '请选择许可起始时间'
        },
        licenseEndTime: {
          label: '结束时间',
          placeholder: '请选择许可证结束时间'
        },
        maxUserCount: {
          label: '最大用户数',
          placeholder: '请输入最大用户数'
        }

      },
      actions: {
        create: '新增租户',
        update: '编辑租户',
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