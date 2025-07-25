export default {
  identity: {
    user: {
      title: '用户管理',
      profile: '个人信息',
      changePassword: '修改密码',
      
      // 密码相关
      password: {
        old: {
          label: '旧密码',
          placeholder: '请输入旧密码',
          validation: {
            required: '旧密码不能为空',
          }
        },
        new: {
          label: '新密码',
          placeholder: '请输入新密码',
          validation: {
            required: '新密码不能为空',
          }
        },
        confirm: {
          label: '确认密码',
          placeholder: '请输入确认密码',
          validation: {
            required: '确认密码不能为空',
          }
        }
      },

      // 表单字段定义
      fields: {
        userId: {
          label: '用户ID'
        },
        tenantId: {
          label: '租户',
          placeholder: '请选择租户',
          validation: {
            required: '租户不能为空'
          }
        },
        userName: {
          label: '用户名',
          placeholder: '请输入用户名',
          validation: {
            required: '用户名不能为空',
            format: '必须以小写字母开头，只能包含小写字母、数字和连字符，不允许点和下划线，长度在4-50位之间'
          }
        },
        nickName: {
          label: '昵称',
          placeholder: '请输入昵称',
          validation: {
            required: '昵称不能为空',
            format: '2-50位字符，允许中文、日语、韩语、英文、数字、英文句点和连字符，不允许下划线和空格'
          }
        },
        realName: {
          label: '真实姓名',
          placeholder: '请输入真实姓名',
          validation: {
            required: '真实姓名不能为空',
            format: '真实姓名长度必须在2-20位之间，只能包含中文、英文、数字和下划线'
          }
        },
        fullName: {
          label: '全名',
          placeholder: '请输入全名',
          validation: {
            required: '全名不能为空',
            format: '全名长度必须在2-20位之间，只能包含中文、英文、数字和下划线'
          }
        },
        englishName: {
          label: '英文名',
          placeholder: '请输入英文名',
          validation: {
            required: '英文名不能为空',
            format: '英文名长度必须在2-50位之间，只能包含英文字母、空格和连字符'
          }
        },
        userType: {
          label: '用户类型',
          placeholder: '请选择用户类型',
          options: {
            admin: '管理员',
            user: '普通用户'
          }
        },
        email: {
          label: '邮箱',
          placeholder: '请输入邮箱',
          validation: {
            required: '邮箱不能为空',
            invalid: '邮箱长度必须在6-100位之间，且格式正确'
          }
        },
        phoneNumber: {
          label: '手机号码',
          placeholder: '请输入手机号码',
          validation: {
            required: '手机号码不能为空',
            invalid: '请输入正确的手机号码或座机号码格式'
          }
        },
        gender: {
          label: '性别',
          placeholder: '请选择性别',
          options: {
            male: '男',
            female: '女',
            unknown: '未知'
          }
        },
        avatar: {
          label: '头像',
          upload: '上传头像',
          uploadSuccess: '头像上传成功',
          uploadError: '头像上传失败'
        },
        status: {
          label: '状态',
          placeholder: '请选择状态',
          options: {
            enabled: '启用',
            disabled: '禁用'
          }
        },
        lastPasswordChangeTime: {
          label: '最后密码修改时间'
        },
        lockEndTime: {
          label: '锁定结束时间'
        },
        lockReason: {
          label: '锁定原因'
        },
        isLock: {
          label: '是否锁定'
        },
        errorLimit: {
          label: '错误次数上限'
        },
        loginCount: {
          label: '登录次数'
        },
        deptName: {
          label: '所属部门',
          placeholder: '请选择所属部门',
          validation: {
            required: '所属部门不能为空'
          }
        },
        role: {
          label: '所属角色',
          placeholder: '请选择所属角色',
          validation: {
            required: '所属角色不能为空'
          }
        },
        post: {
          label: '所属岗位',
          placeholder: '请选择所属岗位',
          validation: {
            required: '所属岗位不能为空'
          }
        },
        createBy: {
          label: '创建者'
        },
        createTime: {
          label: '创建时间'
        },
        updateBy: {
          label: '更新者'
        },
        updateTime: {
          label: '更新时间'
        },
        deleteBy: {
          label: '删除者'
        },
        deleteTime: {
          label: '删除时间'
        },
        isDeleted: {
          label: '是否删除'
        },
        remark: {
          label: '备注',
          placeholder: '请输入备注信息'
        }
      },

      // 操作按钮
      actions: {
        add: '新增用户',
        edit: '编辑用户',
        delete: '删除用户',
        resetPassword: '重置密码',
        export: '导出用户'
      },

      // 消息提示
      messages: {
        confirmDelete: '是否确认删除选中的用户？',
        confirmResetPassword: '是否确认重置所选用户的密码？',
        confirmToggleStatus: '是否确认{action}该用户？',
        deleteSuccess: '用户删除成功',
        deleteFailed: '用户删除失败',
        saveSuccess: '用户信息保存成功',
        saveFailed: '用户信息保存失败',
        resetPasswordSuccess: '密码重置成功',
        resetPasswordFailed: '密码重置失败',
        importSuccess: '用户导入成功',
        importFailed: '用户导入失败',
        exportSuccess: '用户导出成功',
        exportFailed: '用户导出失败',
        toggleStatusSuccess: '状态修改成功',
        toggleStatusFailed: '状态修改失败'
      },

      // 标签页
      tab: {
        basic: '基本信息',
        profile: '个人资料',
        role: '角色权限',
        dept: '部门岗位',
        other: '其他信息',
        avatar: '头像设置',
        loginLog: '登录日志',
        operateLog: '操作日志',
        errorLog: '异常日志',
        taskLog: '任务日志'
      },

      // 导入导出
      import: {
        title: '导入用户',
        template: '下载模板',
        success: '导入成功',
        failed: '导入失败'
      },
      export: {
        title: '导出用户',
        success: '导出成功',
        failed: '导出失败'
      },

      // 重置密码
      resetPwd: '重置密码'
    }
  }
}