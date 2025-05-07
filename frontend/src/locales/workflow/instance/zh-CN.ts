export default {
  workflow: {
    instance: {
      title: '工作流实例',
      list: {
        title: '工作流实例列表',
        search: {
          name: '工作流名称',
          key: '工作流标识',
          version: '版本号',
          status: '状态',
          startTime: '开始时间',
          endTime: '结束时间'
        },
        table: {
          name: '工作流名称',
          key: '工作流标识',
          version: '版本号',
          status: '状态',
          startTime: '开始时间',
          endTime: '结束时间',
          duration: '持续时间',
          actions: '操作'
        },
        actions: {
          view: '查看',
          delete: '删除',
          terminate: '终止',
          export: '导出',
          import: '导入',
          refresh: '刷新'
        },
        status: {
          running: '运行中',
          completed: '已完成',
          terminated: '已终止',
          failed: '已失败'
        }
      },
      form: {
        title: {
          create: '新建工作流实例',
          import: '导入工作流实例'
        },
        fields: {
          workflowDefinitionId: '工作流定义',
          variables: '变量配置'
        },
        rules: {
          workflowDefinitionId: {
            required: '请选择工作流定义'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流实例详情',
        basic: {
          title: '基本信息',
          name: '工作流名称',
          key: '工作流标识',
          version: '版本号',
          status: '状态',
          startTime: '开始时间',
          endTime: '结束时间',
          duration: '持续时间'
        },
        nodes: {
          title: '节点信息',
          name: '节点名称',
          type: '节点类型',
          status: '状态',
          startTime: '开始时间',
          endTime: '结束时间',
          duration: '持续时间',
          input: '输入',
          output: '输出',
          error: '错误'
        },
        actions: {
          back: '返回'
        }
      },
      terminate: {
        title: '终止工作流实例',
        confirm: '确定要终止此工作流实例吗？',
        reason: '终止原因',
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      }
    }
  }
} 