export default {
  workflow: {
    variable: {
      title: '工作流变量',
      list: {
        title: '工作流变量列表',
        search: {
          variableName: '变量名称',
          variableType: '变量类型',
          scope: '作用域',
          workflowInstanceId: '工作流实例'
        },
        table: {
          variableName: '变量名称',
          variableType: '变量类型',
          variableValue: '变量值',
          scope: '作用域',
          workflowInstanceId: '工作流实例',
          nodeId: '节点ID',
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
        scope: {
          global: '全局',
          node: '节点'
        }
      },
      form: {
        title: {
          create: '新建工作流变量',
          edit: '编辑工作流变量'
        },
        fields: {
          variableName: '变量名称',
          variableType: '变量类型',
          variableValue: '变量值',
          scope: '作用域',
          workflowInstanceId: '工作流实例',
          nodeId: '节点ID'
        },
        rules: {
          variableName: {
            required: '请输入变量名称',
            max: '变量名称不能超过50个字符'
          },
          variableType: {
            required: '请选择变量类型'
          },
          variableValue: {
            required: '请输入变量值'
          },
          scope: {
            required: '请选择作用域'
          },
          workflowInstanceId: {
            required: '请选择工作流实例'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流变量详情',
        basic: {
          title: '基本信息',
          variableName: '变量名称',
          variableType: '变量类型',
          variableValue: '变量值',
          scope: '作用域',
          workflowInstanceId: '工作流实例',
          nodeId: '节点ID',
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