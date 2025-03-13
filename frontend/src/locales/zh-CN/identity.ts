export default {
  user: {
    profile: {
      basic: '基本信息',
      avatar: '头像',
      upload: '上传',
      avatarValidation: {
        type: '只能上传JPG/PNG格式的图片',
        size: '图片大小不能超过2MB'
      }
    },
    nickName: {
      label: '昵称',
      placeholder: '请输入昵称',
      validation: {
        required: '请输入昵称',
        length: '昵称长度必须在2-30个字符之间'
      }
    },
    phoneNumber: {
      label: '手机号',
      placeholder: '请输入手机号',
      validation: {
        required: '请输入手机号',
        invalid: '请输入正确的手机号'
      }
    },
    email: {
      label: '邮箱',
      placeholder: '请输入邮箱',
      validation: {
        required: '请输入邮箱',
        invalid: '请输入正确的邮箱'
      }
    },
    password: {
      oldPassword: '原密码',
      newPassword: '新密码',
      confirmPassword: '确认密码',
      oldValidation: {
        placeholder: '请输入原密码',
        validation: {
          required: '请输入原密码'
        }
      },
      newValidation: {
        placeholder: '请输入新密码',
        validation: {
          required: '请输入新密码',
          length: '密码长度必须在6-20个字符之间'
        }
      },
      confirmValidation: {
        placeholder: '请再次输入新密码',
        validation: {
          required: '请再次输入新密码',
          notMatch: '两次输入的密码不一致'
        }
      },
      success: '密码修改成功',
      failed: '密码修改失败'
    }
  }
} 