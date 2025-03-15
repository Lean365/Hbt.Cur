export default {
  identity: {
    role: {
      title: '角色管理',
      fields: {
        roleId: {
          label: '角色ID'
        },
        roleName: {
          label: '角色名称',
          placeholder: '请输入角色名称',
          validation: {
            required: '角色名称不能为空',
            length: '角色名称长度必须在2-20个字符之间'
          }
        },
        roleKey: {
          label: '角色标识',
          placeholder: '请输入角色标识',
          validation: {
            required: '角色标识不能为空',
            length: '角色标识长度必须在2-100个字符之间'
          }
        },
        roleSort: {
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
        createTime: '创建时间',
        menuPermission: {
          label: '菜单权限',
          selectAll: '全选/全不选'
        },
        dataScope: {
          label: '数据权限',
          options: {
            all: '全部数据权限',
            custom: '自定义数据权限',
            dept: '本部门数据权限',
            deptAndChild: '本部门及以下数据权限',
            self: '仅本人数据权限'
          }
        }
      },
      actions: {
        add: '新增角色',
        edit: '编辑角色',
        delete: '删除角色',
        export: '导出角色'
      },
      messages: {
        confirmDelete: '确认删除选中的角色吗？',
        deleteSuccess: '角色删除成功',
        deleteFailed: '角色删除失败',
        saveSuccess: '角色信息保存成功',
        saveFailed: '角色信息保存失败'
      }
    }
  }
} 