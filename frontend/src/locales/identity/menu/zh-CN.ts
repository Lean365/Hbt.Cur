export default {
  identity: {
    menu: {
      title: '菜单管理',
      columns: {
        menuName: '菜单名称',
        transKey: '国际化标识',
        parentId: '上级菜单',
        orderNum: '排序',
        path: '路由地址',
        component: '组件路径',
        queryParams: '路由参数',
        isExternal: '是否外链',
        isCache: '是否缓存',
        menuType: '菜单类型',
        visible: '显示状态',
        status: '状态',
        perms: '权限标识',
        icon: '图标',
        tenantId: '租户ID',
        id: '主键',
        createBy: '创建者',
        createTime: '创建时间',
        updateBy: '更新者',
        updateTime: '更新时间',
        deleteBy: '删除者',
        deleteTime: '删除时间',
        isDeleted: '是否删除',
        remark: '备注',
        action: '操作'
      },
      fields: {
        menuName: {
          label: '菜单名称',
          placeholder: '请输入菜单名称',
          validation: {
            required: '请输入菜单名称',
            length: '菜单名称长度必须在2-50个字符之间',
            format: '菜单名称格式错误，必须为英文（不允许重音符号）、法语/西语、中文、日文、韩文、阿拉伯文、俄文'
          }
        },
        menuDirectoryName: {
          label: '目录名称',
          placeholder: '请输入目录名称',
          validation: {
            required: '请输入目录名称',
            length: '目录名称长度必须在2-50个字符之间',
            format: '目录名称格式错误，必须为英文（不允许重音符号）、法语/西语、中文、日文、韩文、阿拉伯文、俄文'
          }
        },
        menuButtonName: {
          label: '按钮名称',
          placeholder: '请输入按钮名称',
          validation: {
            required: '请输入按钮名称',
            length: '按钮名称长度必须在2-50个字符之间',
            format: '按钮名称格式错误，必须为英文（不允许重音符号）、法语/西语、中文、日文、韩文、阿拉伯文、俄文',
            menuNameExists: '菜单名称已存在'
          }
        },
        transKey: {
          label: '国际化标识',
          placeholder: '请输入国际化标识',
          validation: {
            required: '请输入国际化标识',
            format: '国际化标识格式错误，必须为 menu.xxx._self 或 menu.xxx.xxx._self，且中间为字母'
          }
        },
        parentId: {
          label: '上级菜单',
          placeholder: '请选择上级菜单',
          root: '根菜单',
          validation: {
            required: '请选择上级菜单'
          }
        },
        orderNum: {
          label: '显示排序',
          placeholder: '请输入显示排序',
          validation: {
            required: '请输入显示排序'
          }
        },
        path: {
          label: '路由地址',
          placeholder: '请输入路由地址',
          validation: {
            required: '请输入路由地址',
            length: '路由地址长度必须在2-50个字符之间',
            format: '路由地址格式错误，且只能包含小写字母'
          }
        },
        component: {
          label: '组件路径',
          placeholder: '请输入组件路径',
          validation: {
            required: '请输入组件路径'
          }
        },
        queryParams: {
          label: '路由参数',
          placeholder: '请输入路由参数',
          validation: {
            required: '请输入路由参数'
          }
        },
        isExternal: {
          label: '是否外链',
          placeholder: '请选择是否外链',
          options: {
            yes: '是',
            no: '否'
          }
        },
        isCache: {
          label: '是否缓存',
          placeholder: '请选择是否缓存',
          options: {
            yes: '是',
            no: '否'
          }
        },
        menuType: {
          label: '菜单类型',
          options: {
            directory: '目录',
            menu: '菜单',
            button: '按钮'
          },
          validation: {
            required: '请选择菜单类型'
          }
        },
        visible: {
          label: '显示状态',
          placeholder: '请选择显示状态',
          options: {
            show: '显示',
            hide: '隐藏'
          }
        },
        status: {
          label: '状态',
          placeholder: '请选择菜单状态',
          options: {
            normal: '正常',
            disabled: '停用'
          }
        },
        perms: {
          label: '权限标识',
          placeholder: '请输入权限标识',
          validation: {
            required: '请输入权限标识'
          }
        },
        icon: {
          label: '菜单图标',
          placeholder: '请输入菜单图标',
          select: '选择图标',
          validation: {
            required: '请选择菜单图标'
          }
        },
        tenantId: {
          label: '租户ID',
          placeholder: '请输入租户ID'
        }
      },
      dialog: {
        create: '添加菜单',
        update: '编辑菜单',
        delete: '删除菜单'
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