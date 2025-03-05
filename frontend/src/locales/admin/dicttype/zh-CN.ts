export default {
  admin: {
    dicttype: {
      title: '字典类型',
      form: {
        name: '字典名称',
        namePlaceholder: '请输入字典名称',
        type: '字典类型',
        typePlaceholder: '请输入字典类型',
        category: '字典类别',
        categoryPlaceholder: '请选择字典类别',
        status: '状态',
        statusPlaceholder: '请选择状态',
        tenantId: '租户ID',
        orderNum: '排序号',
        sqlScript: 'SQL脚本',
        sqlScriptPlaceholder: '请输入SQL脚本',
        remark: '备注',
        remarkPlaceholder: '请输入备注'
      },
      category: {
        system: '系统',
        sql: 'SQL'
      },
      rules: {
        tenantIdRequired: '租户ID不能为空',
        tenantIdRange: '租户ID必须在0-9999之间',
        nameRequired: '字典名称不能为空',
        namePattern: '字典名称只能包含字母、数字、中文、下划线和连字符，长度在2-100之间',
        typeRequired: '字典类型不能为空',
        typePattern: '字典类型必须以字母开头，只能包含字母、数字和下划线，长度在2-100之间',
        categoryRequired: '字典类别不能为空',
        orderNumRequired: '排序号不能为空',
        orderNumRange: '排序号必须在0-9999之间',
        statusRequired: '状态不能为空',
        sqlPattern: 'SQL脚本必须包含SELECT和FROM语句'
      },
      sqlHelp: {
        title: '标准SQL示例：',
        description: '字段说明：',
        example: `SELECT
  name as label,    -- 显示文本
  code as value,    -- 键值
  css_class,        -- CSS类名
  list_class,       -- 列表类名
  status,          -- 状态(0正常 1停用)
  ext_label,       -- 扩展标签
  ext_value,       -- 扩展值
  trans_key,       -- 翻译键
  order_num,       -- 排序号
  remark           -- 备注
FROM your_table 
WHERE status = 0 
ORDER BY order_num`,
        fields: {
          label: 'label/name/text: 显示文本',
          value: 'value/key/code: 键值',
          cssClass: 'css_class: CSS类名',
          listClass: 'list_class: 列表类名',
          status: 'status: 状态(0正常 1停用)',
          extLabel: 'ext_label: 扩展标签',
          extValue: 'ext_value: 扩展值',
          transKey: 'trans_key: 翻译键',
          orderNum: 'order_num: 排序号',
          remark: 'remark: 备注'
        }
      }
    }
  }
}