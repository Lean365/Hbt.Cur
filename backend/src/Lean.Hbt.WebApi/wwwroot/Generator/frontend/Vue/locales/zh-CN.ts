export default {
  {{ table.module_name }}: {
    {{ table.name }}: {
      title: '{{ table.comment }}管理',
      fields: {
        id: {
          label: 'ID'
        },
        name: {
          label: '名称',
          placeholder: '请输入名称'
        },
        code: {
          label: '编码',
          placeholder: '请输入编码'
        },
        sort: {
          label: '排序',
          placeholder: '请输入排序'
        },
        status: {
          label: '状态',
          placeholder: '请选择状态'
        },
        remark: {
          label: '备注',
          placeholder: '请输入备注'
        },
        createTime: {
          label: '创建时间'
        },
        updateTime: {
          label: '更新时间'
        }
      }
    }
  }
} 