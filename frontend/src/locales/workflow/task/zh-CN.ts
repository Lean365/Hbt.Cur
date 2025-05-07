export default {
  workflow: {
    task: {
      title: '工作流任务',
      list: {
        title: '工作流任务列表',
        search: {
          name: '任务名称',
          taskType: '任务类型',
          status: '状态'
        },
        table: {
          name: '任务名称',
          taskType: '任务类型',
          description: '描述',
          config: '任务配置',
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
          export: '导出',
          import: '导入',
          refresh: '刷新'
        },
        status: {
          enabled: '启用',
          disabled: '禁用'
        }
      },
      form: {
        title: {
          create: '新建工作流任务',
          edit: '编辑工作流任务'
        },
        fields: {
          name: '任务名称',
          taskType: '任务类型',
          description: '描述',
          config: '任务配置',
          status: '状态'
        },
        rules: {
          name: {
            required: '请输入任务名称',
            max: '任务名称不能超过50个字符'
          },
          taskType: {
            required: '请选择任务类型'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流任务详情',
        basic: {
          title: '基本信息',
          name: '任务名称',
          taskType: '任务类型',
          description: '描述',
          config: '任务配置',
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