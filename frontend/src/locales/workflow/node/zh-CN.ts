export default {
  workflow: {
    node: {
      title: '工作流节点',
      list: {
        title: '工作流节点列表',
        search: {
          name: '节点名称',
          nodeType: '节点类型',
          workflowDefinitionId: '工作流定义'
        },
        table: {
          name: '节点名称',
          nodeType: '节点类型',
          description: '描述',
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
        nodeType: {
          start: '开始节点',
          end: '结束节点',
          task: '任务节点',
          condition: '条件节点',
          parallel: '并行节点',
          join: '合并节点'
        }
      },
      form: {
        title: {
          create: '新建工作流节点',
          edit: '编辑工作流节点'
        },
        fields: {
          name: '节点名称',
          nodeType: '节点类型',
          description: '描述',
          workflowDefinitionId: '工作流定义',
          config: '节点配置'
        },
        rules: {
          name: {
            required: '请输入节点名称',
            max: '节点名称不能超过50个字符'
          },
          nodeType: {
            required: '请选择节点类型'
          },
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
        title: '工作流节点详情',
        basic: {
          title: '基本信息',
          name: '节点名称',
          nodeType: '节点类型',
          description: '描述',
          workflowDefinitionId: '工作流定义',
          createTime: '创建时间',
          updateTime: '更新时间'
        },
        config: {
          title: '节点配置',
          taskId: '任务ID',
          condition: '条件表达式',
          parallelNodes: '并行节点'
        },
        actions: {
          edit: '编辑',
          back: '返回'
        }
      }
    }
  }
} 