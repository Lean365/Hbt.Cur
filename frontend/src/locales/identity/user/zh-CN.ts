export default {
  identity: {
    user: {
      title: '用户管理',
      toolbar: {
        add: '新增用户',
        edit: '编辑用户',
        delete: '删除用户',
        import: '导入用户',
        export: '导出用户',
        resetPassword: '重置密码',
        downloadTemplate: '下载模板'
      },
      table: {
        columns: {
          userName: '用户名',
          nickName: '昵称',
          deptName: '所属部门',
          role: '所属角色',
          email: '邮箱',
          phoneNumber: '手机号码',
          gender: '性别',
          status: '状态',
          createTime: '创建时间',
          lastLoginTime: '最后登录时间',
          operation: '操作'
        },
        operation: {
          edit: '编辑',
          delete: '删除',
          resetPassword: '重置密码'
        },
        status: {
          enabled: '启用',
          disabled: '禁用',
          toggle: {
            enable: '启用',
            disable: '禁用'
          }
        }
      },
      userId: '用户ID',
      userName: {
        label: '用户名',
        placeholder: '请输入用户名',
        validation: {
          required: '用户名不能为空',
          length: '用户名长度必须在2-20个字符之间'
        }
      },
      nickName: {
        label: '昵称',
        placeholder: '请输入昵称',
        validation: {
          required: '昵称不能为空',
          length: '昵称长度必须在2-30个字符之间'
        }
      },
      englishName: {
        label: '英文名',
        placeholder: '请输入英文名',
        validation: {
          length: '英文名长度不能超过50个字符'
        }
      },
      password: {
        label: '密码',
        placeholder: '请输入密码',
        validation: {
          required: '密码不能为空',
          length: '密码长度必须在6-20个字符之间'
        }
      },
      confirmPassword: {
        label: '确认密码',
        placeholder: '请再次输入密码',
        validation: {
          required: '确认密码不能为空',
          notMatch: '两次输入的密码不一致'
        }
      },
      email: {
        label: '邮箱',
        placeholder: '请输入邮箱',
        validation: {
          required: '邮箱不能为空',
          invalid: '请输入正确的邮箱格式'
        }
      },
      phoneNumber: {
        label: '手机号码',
        placeholder: '请输入手机号码',
        validation: {
          required: '手机号码不能为空',
          invalid: '请输入正确的手机号码格式'
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
        upload: '上传头像'
      },
      deptName: {
        label: '所属部门',
        placeholder: '请选择所属部门'
      },
      role: {
        label: '所属角色',
        placeholder: '请选择所属角色'
      },
      post: {
        label: '所属岗位',
        placeholder: '请选择所属岗位'
      },
      status: {
        label: '状态',
        placeholder: '请选择状态',
        options: {
          enabled: '启用',
          disabled: '禁用'
        }
      },
      resetPwd: '重置密码',
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
      userType: {
        label: '用户类型',
        placeholder: '请选择用户类型',
        options: {
          admin: '管理员',
          user: '普通用户'
        }
      },
      createTime: '创建时间',
      lastLoginTime: '最后登录时间',
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
      tab: {
        basic: '基本信息',
        profile: '个人资料',
        role: '角色权限',
        dept: '部门岗位',
        other: '其他信息'
      }
    }
  },
  actions: {
    add: '新增用户',
    edit: '编辑用户',
    delete: '删除用户',
    resetPassword: '重置密码',
    export: '导出用户'
  },
  rules: {
    userName: '用户名不能为空',
    nickName: '昵称不能为空',
    phoneNumber: '请输入正确的手机号码',
    email: '请输入正确的邮箱地址'
  }
} 