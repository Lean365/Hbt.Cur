export default {
  identity: {
    user: {
      title: '用户管理',
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
        label: '手机号',
        placeholder: '请输入手机号',
        validation: {
          required: '手机号不能为空',
          invalid: '请输入正确的手机号格式'
        }
      },
      gender: {
        label: '性别',
        placeholder: '请选择性别',
        unknown: '未知',
        male: '男',
        female: '女'
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
        normal: '正常',
        disabled: '停用'
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
      }
    }
  }
} 