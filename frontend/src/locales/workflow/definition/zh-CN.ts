export default {
  workflow: {
    definition: {
      title: '工作流定义',
      list: {
        title: '工作流定义列表',
        search: {
          name: '工作流名称',
          key: '工作流标识',
          version: '版本号',
          status: '状态'
        },
        table: {
          name: '工作流名称',
          key: '工作流标识',
          version: '版本号',
          status: '状态',
          createTime: '创建时间',
          updateTime: '更新时间',
          actions: '操作'
        },
        actions: {
          create: '新建',
          edit: '编辑',
          delete: '删除',
          view: '查看',
          deploy: '部署',
          export: '导出',
          import: '导入',
          refresh: '刷新'
        },
        status: {
          draft: '草稿',
          deployed: '已部署',
          disabled: '已禁用'
        }
      },
      form: {
        title: {
          create: '新建工作流定义',
          edit: '编辑工作流定义'
        },
        fields: {
          name: '工作流名称',
          key: '工作流标识',
          version: '版本号',
          description: '描述',
          status: '状态'
        },
        rules: {
          name: {
            required: '请输入工作流名称',
            max: '工作流名称不能超过50个字符'
          },
          key: {
            required: '请输入工作流标识',
            pattern: '工作流标识只能包含字母、数字和下划线',
            max: '工作流标识不能超过50个字符'
          },
          version: {
            required: '请输入版本号',
            pattern: '版本号格式不正确，应为x.y.z格式'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流定义详情',
        basic: {
          title: '基本信息',
          name: '工作流名称',
          key: '工作流标识',
          version: '版本号',
          description: '描述',
          status: '状态',
          createTime: '创建时间',
          updateTime: '更新时间'
        },
        actions: {
          edit: '编辑',
          back: '返回'
        }
      }
    }
  }
} 