export default {
  core: {
    dict: {
      dictTypes: {
        title: '字典类型',
        table: {
          columns: {
            dictTypeId: '字典类型ID',
            dictName: '字典名称',
            dictType: '字典类型',
            dictCategory: '字典类别',          
            isBuiltin: '是否内置',
            sqlScript: 'SQL脚本',
            orderNum: '排序号',          
            status: '状态',
          }
        },
        fields: {
          dictName: {
            label: '字典名称',
            placeholder: '请输入字典名称',
            validate: {
              required: '字典名称不能为空',
              pattern: '字典名称只能包含字母、数字、中文、下划线和连字符，长度在2-100之间',
              length: '字典名称长度不能超过100个字符'
            }
          },
          dictType: {
            label: '字典类型',
            placeholder: '请输入字典类型',
            validate: {
              required: '字典类型不能为空',
              pattern: '字典类型必须以字母开头，只能包含字母、数字和下划线，长度在2-100之间',
              length: '字典类型长度不能超过100个字符',
              format: '字典类型必须由三部分组成，用下划线分隔',
              first: '第一部分必须为3位小写字母，不允许重叠字符（如aa、aaa等），不允许特殊字符，且不允许与其他部分有三个或以上重复字符',
              second: '第二部分必须为3-10位小写字母，不允许重叠字符（如aa、aaa等），不允许特殊字符，且不允许与其他部分有三个或以上重复字符',
              third: '第三部分必须以小写字母开头，可包含数字，不允许重叠字符（如aa、aaa等），不允许特殊字符，总长度3-10位，且不允许与其他部分有三个或以上重复字符'
            }
          },
          dictCategory: {
            label: '字典类别',
            placeholder: '请选择字典类别',
            validate: {
              required: '字典类别不能为空'
            }
          },
          isBuiltin: {
            label: '是否内置',
            placeholder: '请选择是否内置',
            validate: {
              required: '是否内置不能为空'
            }
          },
          sqlScript: {
            label: 'SQL脚本',
            placeholder: '请输入SQL脚本',
            validate: {
              required: 'SQL脚本不能为空'
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
            }
          },
          orderNum: {
            label: '排序号',
            placeholder: '请输入排序号',
            validate: {
              required: '排序号不能为空'
            }
          },
          status: {
            
            label: '状态',
            placeholder: '请选择状态',
            validate: {
              required: '状态不能为空'
            }
          },
          tenantId: {
            label: '租户ID',
            placeholder: '请输入租户ID',
            validate: {
              required: '租户ID不能为空'
            }
          },
          remark: {
            label: '备注',
            placeholder: '请输入备注',
            validate: {
              required: '备注不能为空'
            }
          }
        }
      },
      dictDatas: {
        title: '字典数据',
        table: {
          columns: {
            dictDataId: '字典数据ID',
            dictType: '字典类型',
            dictLabel: '字典标签',
            dictValue: '字典值',
            extLabel: '扩展标签',
            extValue: '扩展值',
            transKey: '翻译键',
            orderNum: '排序号',
            cssClass: 'CSS类名',
            listClass: '列表类名',
            status: '状态',
          }
        },
        fields: {
          dictType: {
            label: '字典类型',
            placeholder: '请选择字典类型',
            validate: {
              required: '字典类型不能为空'
            }
          },
          dictLabel: {
            label: '字典标签',
            placeholder: '请输入字典标签',
            validate: {
              required: '字典标签不能为空'
            }
          },
          dictValue: {
            label: '字典值',
            placeholder: '请输入字典值',
            validate: {
              required: '字典值不能为空'
            }
          },
          extLabel: {
            label: '扩展标签',
            placeholder: '请输入扩展标签',
            validate: {
              required: '扩展标签不能为空'
            }
          },
          extValue: {
            label: '扩展值',
            placeholder: '请输入扩展值',
            validate: {
              required: '扩展值不能为空'
            }
          },
          transKey: {
            label: '翻译键',
            placeholder: '请输入翻译键',
            validate: {
              required: '翻译键不能为空'
            }
          },
          orderNum: {
            label: '排序号',
            placeholder: '请输入排序号',
            validate: {
              required: '排序号不能为空'
            }
          },
          cssClass: {
            label: 'CSS类名',
            placeholder: '请输入CSS类名',
            validate: {
              required: 'CSS类名不能为空'
            }
          },
          listClass: {
            label: '列表类名',
            placeholder: '请输入列表类名',
            validate: {
              required: '列表类名不能为空'
            }
          },
          status: {
            label: '状态',
            placeholder: '请选择状态',
            validate: {
              required: '状态不能为空'
            }
          },
          tenantId: {
            label: '租户ID',
            placeholder: '请输入租户ID',
            validate: {
              required: '租户ID不能为空'
            }
          },
          remark: {
            label: '备注',
            placeholder: '请输入备注',
            validate: {
              required: '备注不能为空'
            }
          }
        }
      }
    }
  }
}