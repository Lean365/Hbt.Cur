export default {
  identity: {
    post: {
      title: '岗位管理',
      fields: {
        postId: {
          label: '岗位ID'
        },
        postCode: {
          label: '岗位编码',
          placeholder: '请输入岗位编码',
          validation: {
            required: '岗位编码不能为空',
            length: '岗位编码长度必须在2-20个字符之间'
          }
        },
        postName: {
          label: '岗位名称',
          placeholder: '请输入岗位名称',
          validation: {
            required: '岗位名称不能为空',
            length: '岗位名称长度必须在2-20个字符之间'
          }
        },
        postSort: {
          label: '显示顺序',
          placeholder: '请输入显示顺序'
        },
        status: {
          label: '状态',
          placeholder: '请选择状态',
          options: {
            enabled: '启用',
            disabled: '禁用'
          }
        },
        description: {
          label: '备注',
          placeholder: '请输入备注'
        },
        createTime: '创建时间'
      },
      actions: {
        add: '新增岗位',
        edit: '编辑岗位',
        delete: '删除岗位',
        export: '导出岗位'
      },
      messages: {
        confirmDelete: '是否确认删除岗位编码为"{code}"的岗位？',
        deleteSuccess: '岗位删除成功',
        deleteFailed: '岗位删除失败',
        saveSuccess: '岗位信息保存成功',
        saveFailed: '岗位信息保存失败'
      }
    }
  }
} 