export default {
  workflow: {
    history: {
      title: '工作流历史',
      list: {
        title: '工作流历史列表',
        search: {
          workflowName: '工作流名称',
          workflowKey: '工作流标识',
          version: '版本号',
          status: '状态',
          startTime: '开始时间',
          endTime: '结束时间'
        },
        table: {
          workflowName: '工作流名称',
          workflowKey: '工作流标识',
          version: '版本号',
          status: '状态',
          startTime: '开始时间',
          endTime: '结束时间',
          duration: '执行时长',
          actions: '操作'
        },
        actions: {
          view: '查看',
          delete: '删除',
          export: '导出',
          refresh: '刷新'
        },
        status: {
          running: '运行中',
          completed: '已完成',
          failed: '失败',
          terminated: '已终止'
        }
      },
      detail: {
        title: '工作流历史详情',
        basic: {
          title: '基本信息',
          workflowName: '工作流名称',
          workflowKey: '工作流标识',
          version: '版本号',
          status: '状态',
          startTime: '开始时间',
          endTime: '结束时间',
          duration: '执行时长',
          createTime: '创建时间',
          updateTime: '更新时间'
        },
        execution: {
          title: '执行记录',
          nodeName: '节点名称',
          nodeType: '节点类型',
          status: '状态',
          startTime: '开始时间',
          endTime: '结束时间',
          duration: '执行时长',
          message: '执行消息'
        },
        variables: {
          title: '变量记录',
          variableName: '变量名称',
          variableType: '变量类型',
          variableValue: '变量值',
          scope: '作用域',
          createTime: '创建时间',
          updateTime: '更新时间'
        },
        actions: {
          back: '返回'
        }
      }
    }
  }
} 