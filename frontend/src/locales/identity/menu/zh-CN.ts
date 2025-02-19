export default {
  identity: {
    menu: {
      title: '菜单管理',
      columns: {
        menuName: '菜单名称',
        icon: '图标',
        orderNum: '排序',
        perms: '权限标识',
        component: '组件路径',
        status: '状态',
        createTime: '创建时间',
        action: '操作'
      },
      form: {
        base: {
          parentMenu: {
            label: '上级菜单',
            placeholder: '请选择上级菜单',
            root: '根菜单'
          },
          name: {
            label: '菜单名称',
            placeholder: '请输入菜单名称'
          },
          transKey: {
            label: '翻译键',
            placeholder: '请输入翻译键',
            preview: '预览',
            notFound: '未找到翻译'
          },
          orderNum: {
            label: '显示排序'
          }
        },
        display: {
          type: {
            label: '菜单类型',
            directory: '目录',
            menu: '菜单',
            button: '按钮'
          },
          icon: {
            label: '菜单图标',
            placeholder: '请输入菜单图标'
          },
          isFrame: {
            label: '是否外链',
            yes: '是',
            no: '否'
          },
          isCache: {
            label: '是否缓存',
            yes: '是',
            no: '否'
          },
          visible: {
            label: '显示状态',
            show: '显示',
            hide: '隐藏'
          },
          status: {
            label: '菜单状态',
            normal: '正常',
            disabled: '停用'
          }
        },
        route: {
          path: {
            label: '路由地址',
            placeholder: '请输入路由地址'
          },
          component: {
            label: '组件路径',
            placeholder: '请输入组件路径'
          },
          query: {
            label: '路由参数',
            placeholder: '请输入路由参数'
          }
        },
        permission: {
          perms: {
            label: '权限标识',
            placeholder: '请输入权限标识'
          }
        }
      },
      operation: {
        add: {
          title: '添加菜单',
          success: '添加成功',
          failed: '添加失败'
        },
        edit: {
          title: '编辑菜单',
          success: '修改成功',
          failed: '修改失败'
        },
        delete: {
          title: '删除菜单',
          confirm: '是否确认删除该菜单?',
          success: '删除成功',
          failed: '删除失败'
        }
      }
    }
  }
} 