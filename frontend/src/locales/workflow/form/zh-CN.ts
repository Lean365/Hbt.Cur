export default {
  workflow: {
    form: {
      title: '表单管理',
      detail: '表单详情',
      tabs: {
        basic: '基础字段',
        designer: '表单设计器',
      },
      fields: {
        formName: '表单名称',
        formDesc: '表单描述',
        formCategory: '表单分类',
        formVersion: '表单版本',
        formConfig: '表单配置',
        definitionId: '流程ID',
        status: '状态',
        remark: '备注',
      },
      placeholder: {
        formName: '请输入表单名称',
        formDesc: '请输入表单描述',
        formCategory: '请选择表单分类',
        formVersion: '请选择表单版本',
        formConfig: '请输入表单配置',
        status: '请选择状态',
        remark: '请输入备注',
        validation: {
          formName: {
            required: '请输入表单名称',
            length: '表单名称长度必须在2-50个字符之间',
            pattern: '表单名称只能包含中文、英文、数字、下划线和横线'
          },
          formDesc: {
            required: '请输入表单描述',
            length: '表单描述长度必须在2-200个字符之间'
          },
          formConfig: {
            required: '请通过表单设计器配置表单',
            length: '表单配置长度必须在2-20000个字符之间'
          },
          status: {
            required: '请选择状态'
          },
          remark: {
            required: '请输入备注',
            length: '备注长度必须在2-200个字符之间'
          }
        }
      }
    }
  }
};
