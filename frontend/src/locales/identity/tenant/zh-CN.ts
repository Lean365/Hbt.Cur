import { theme } from "ant-design-vue";

export default {
  identity: {
    tenant: {
      title: '租户管理',
      
      // 表单字段定义
      fields: {
        tenantId: {
          label: '租户ID'
        },
        tenantNo: {
          label: '租户编号'
        },
        tenantName: {
          label: '租户名称',
          placeholder: '请输入租户名称',
          validation: {
            required: '租户名称不能为空'
          }
        },
        tenantCode: {
          label: '租户标识',
          placeholder: '请输入租户标识',
          validation: {
            required: '租户标识不能为空'
          }
        },
        contactUser: {
          label: '联系人',
          placeholder: '请输入联系人',
          validation: {
            required: '联系人不能为空'
          }
        },
        contactPhone: {
          label: '联系电话',
          placeholder: '请输入联系电话',
          validation: {
            required: '联系电话不能为空'
          }
        },
        contactEmail: {
          label: '邮箱',
          placeholder: '请输入邮箱',
          validation: {
            required: '邮箱不能为空'
          }
        },
        address: {
          label: '地址',
          placeholder: '请输入地址'
        },
        licenseType: {
          label: '许可证类型',
          placeholder: '请选择许可证类型',
          options: {
            Enterprise: '企业版',
            Professional: '专业版',
            Standard: '标准版'
          }
        },
        licenseKey: {
          label: '许可注册码',
          placeholder: '请输入许可注册码'
        },
        expireTime: {
          label: '过期时间',
          placeholder: '请选择过期时间'
        },
        status: {
          label: '状态',
          options: {
            0: '正常',
            1: '停用'
          }
        },
        domain: {
          label: '域名',
          placeholder: '请输入域名',
          validation: {
            required: '域名不能为空'
          }
        },
        logoUrl: {
          label: 'Logo URL',
          placeholder: '请输入Logo URL'
        },
        theme: {
          label: '主题',
          placeholder: '请输入主题'
        },
        licenseStartTime: {
          label: '许可证开始时间',
          placeholder: '请选择许可证开始时间'
        },
        licenseEndTime: {
          label: '许可证结束时间',
          placeholder: '请选择许可证结束时间'
        },
        maxUserCount: {
          label: '最大用户数',
          placeholder: '请输入最大用户数',
          validation: {
            required: '最大用户数不能为空'
          }
        }
      },

      // 操作按钮
      actions: {
        create: '创建租户',
        edit: '编辑租户',
        delete: '删除租户',
        batchDelete: '批量删除',
        import: '导入',
        export: '导出',
        template: '下载模板',
        status: '状态管理'
      },

      // 消息提示
      messages: {
        createSuccess: '租户创建成功',
        updateSuccess: '租户更新成功',
        deleteSuccess: '租户删除成功',
        batchDeleteSuccess: '批量删除成功',
        importSuccess: '导入成功',
        exportSuccess: '导出成功',
        confirmDelete: '确定要删除该租户吗？',
        confirmBatchDelete: '确定要删除选中的租户吗？',
        noData: '暂无数据',
        loading: '加载中...'
      }
    }
  }
} 