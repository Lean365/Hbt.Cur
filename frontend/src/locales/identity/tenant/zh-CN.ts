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
          licenseType: '许可证类型',
          licenseKey: '许可注册码',
          expireTime: '过期时间',
          status: '状态',
          domain: '域名',
          logoUrl: 'Logo URL',
          theme: '主题',
          licenseStartTime: '许可证开始时间',
          licenseEndTime: '许可证结束时间',
          maxUserCount: '最大用户数'
        }
      },
      form: {
        tenantName: '租户名称',
        tenantCode: '租户标识',
        contactUser: '联系人',
        contactPhone: '联系电话',
        contactEmail: '邮箱',
        address: '地址',
        licenseType: '许可证类型',
        licenseKey: '许可注册码',
        expireTime: '过期时间',
        domain: '域名',
        logoUrl: 'Logo URL',
        theme: '主题',
        licenseStartTime: '许可证开始时间',
        licenseEndTime: '许可证结束时间',
        maxUserCount: '最大用户数',
        status: '状态'
      },
      placeholder: {
        tenantName: '请输入租户名称',
        tenantCode: '请输入租户标识',
        contactUser: '请输入联系人',
        contactPhone: '请输入联系电话',
        contactEmail: '请输入邮箱',
        address: '请输入地址',
        licenseType: '请选择许可证类型',
        licenseKey: '请输入许可注册码',
        expireTime: '请选择过期时间',
        domain: '请输入域名',
        logoUrl: '请输入Logo URL',
        theme: '请输入主题',
        licenseStartTime: '请选择许可证开始时间',
        licenseEndTime: '请选择许可证结束时间',
        maxUserCount: '请输入最大用户数'
      },
      rules: {
        tenantName: '租户名称不能为空',
        tenantCode: '租户标识不能为空',
        contactUser: '联系人不能为空',
        contactPhone: '联系电话不能为空',
        contactEmail: '邮箱不能为空',
        domain: '域名不能为空',
        maxUserCount: '最大用户数不能为空'
      },
      status: {
        0: '正常',
        1: '停用'
      },
      licenseType: {
        Enterprise: '企业版',
        Professional: '专业版',
        Standard: '标准版'
      },
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