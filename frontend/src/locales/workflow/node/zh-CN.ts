export default {
  workflow: {
    node: {
      title: '工作流节点',
      fields: {
        instanceId: '实例ID',
        nodeId: '节点ID',
        nodeName: '节点名称',
        nodeType: '节点类型',
        definitionId: '定义ID',
        definitionName: '定义名称',
        parentNodeId: '父节点ID',
        parentNodeName: '父节点名称',
        nodeConfig: '节点配置',
        status: '状态',
        startTime: '开始时间',
        endTime: '结束时间',
        orderNum: '排序',
      },
      placeholder: {
        instanceId: '请输入实例ID',
        nodeId: '请输入节点ID',
        nodeName: '请输入节点名称',
        nodeType: '请选择节点类型',
        definitionId: '请输入定义ID',
        parentNodeId: '请输入父节点ID',
        nodeConfig: '请输入节点配置',
        status: '请选择状态',
        startTime: '请输入开始时间',
        endTime: '请输入结束时间',
        orderNum: '请输入排序',
        remark: '请输入备注',
        validation: {
          instanceId: {
            required: '请输入实例ID',
          },
          nodeId: {
            required: '请输入节点ID',
          },
          nodeName: {
            required: '请输入节点名称',
            length: '节点名称长度必须在2-50个字符之间',
            pattern: '节点名称只能包含中文、英文、数字、下划线和横线',
          },
          nodeType: {
            required: '请选择节点类型',
          },
          definitionId: {
            required: '请输入定义ID',
          },
          parentNodeId: {
            required: '请输入父节点ID',
          },
          nodeConfig: {
            required: '请输入节点配置',
            length: '节点配置长度必须在2-20000个字符之间',
          },
          status: {
            required: '请选择状态',
          },
          startTime: {
            required: '请输入开始时间',
          },
          endTime: {
            required: '请输入结束时间',
          },
          orderNum: {
            required: '请输入排序',
          },
          remark: {
            required: '请输入备注',
            length: '备注长度必须在2-200个字符之间',
          },
        },
      },
    },
  },
}