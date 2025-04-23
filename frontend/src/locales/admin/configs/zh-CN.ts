export default {
  admin: {
    // 系统配置
    config: {
      // 基础信息
      name: '配置名称',
      key: '配置键',
      value: '配置值',
      builtin: '系统内置',
      order: '排序',
      remark: '备注',
      status: '状态',
      // 时间信息
      createTime: '创建时间',
      createBy: '创建人',
      updateTime: '更新时间',
      updateBy: '更新人',
      // 操作
      operation: '操作',
      // 提示信息
      placeholder: {
        name: '请输入配置名称',
        key: '请输入配置键',
        value: '请输入配置值',
        builtin: '请选择是否系统内置',
        order: '请输入排序号',
        remark: '请输入备注',
        status: '请选择状态'
      },
      // 验证信息
      validation: {
        name: {
          required: '请输入配置名称',
          maxLength: '配置名称不能超过100个字符'
        },
        key: {
          required: '请输入配置键',
          maxLength: '配置键不能超过100个字符',
          pattern: '配置键必须以字母开头，且只能包含字母、数字、下划线、点和冒号'
        },
        value: {
          required: '请输入配置值',
          maxLength: '配置值不能超过500个字符'
        },
        builtin: {
          required: '请选择是否系统内置'
        },
        order: {
          required: '请输入排序号',
          range: '排序号必须在0到9999之间'
        },
        status: {
          required: '请选择状态'
        }
      },
      // 操作结果
      message: {
        addSuccess: '配置添加成功',
        editSuccess: '配置更新成功',
        deleteSuccess: '配置删除成功',
        deleteConfirm: '确定要删除配置"{name}"吗？',
        exportSuccess: '配置导出成功',
        importSuccess: '配置导入成功',
        refreshSuccess: '缓存刷新成功'
      }
    }
  }
}