export default {
  workflow: {
    instance: {
      title: '工作流实例',
      fields: {
        instanceId: '实例ID',
        instanceName: '实例名称',
        businessKey: '业务主键',
        definitionId: '流程ID',
        definitionName: '流程名称',
        currentNodeId: '当前节点ID',
        currentNodeName: '当前节点名称',
        initiatorId: '发起人ID',
        initiatorName: '发起人',
        formData: '表单数据',
        status: '状态',
        startTime: '开始时间',
        endTime: '结束时间',
        remark: '备注',        
      },
      placeholder: {
        instanceName: '实例名称',
        businessKey: '业务主键',
        definitionId: '流程ID',
        currentNodeId: '当前节点ID',
        initiatorId: '发起人',
        formData: '表单数据',
        status: '状态',
        startTime: '开始时间',
        endTime: '结束时间',
        remark: '备注',    
        validation: {
          instanceName: {
            required: '请输入实例名称',
            length: '实例名称长度必须在2-50个字符之间',
            pattern: '实例名称只能包含中文、英文、数字、下划线和横线'
          },
          businessKey: {
            required: '请输入业务主键',
            length: '业务主键长度必须在2-50个字符之间',
            pattern: '业务主键只能包含中文、英文、数字、下划线和横线'
          },
          definitionId: {
            required: '请输入流程ID',
            length: '流程ID长度必须在2-50个字符之间',
            pattern: '流程ID只能包含中文、英文、数字、下划线和横线'
          },
          currentNodeId: {
            required: '请输入当前节点ID',
            length: '当前节点ID长度必须在2-50个字符之间',
            pattern: '当前节点ID只能包含中文、英文、数字、下划线和横线'
          },
          initiatorId: {
            required: '请选择发起人',
          },
          formData: {
            required: '请输入表单数据',
            length: '表单数据长度必须在2-20000个字符之间',
            pattern: '表单数据只能包含中文、英文、数字、下划线和横线'
          },
          status: {
            required: '请选择状态',
          },
          startTime: {
            required: '请选择开始时间',
          },
          endTime: {
            required: '请选择结束时间',
          },
          remark: {
            required: '请输入备注',
            length: '备注长度必须在2-200个字符之间',
          }
        }
      }
    }
  }
}
 