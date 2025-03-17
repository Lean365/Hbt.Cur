export default {
  identity: {
    auth: {
      login: {
        title: '登录',
        username: '用户名',
        password: '密码',
        rememberMe: '记住密码',
        forgotPassword: '忘记密码',
        submit: '登录',
        register: '注册账号',
        success: '登录成功',
        error: {
          invalidCredentials: '用户名或密码错误',
          accountLocked: '账号已被锁定',
          accountDisabled: '账号已被禁用',
          accountExpired: '账号已过期',
          credentialsExpired: '密码已过期',
          invalidCaptcha: '验证码错误',
          invalidTenant: '无效的租户',
          invalidDevice: '设备信息无效',
          invalidGrant: '授权信息无效',
          tooManyAttempts: '登录尝试次数过多，请稍后再试'
        },
        noToken: '登录响应中没有访问令牌',
        otherLogin: '其他登录方式',
        form: {
          usernameRequired: '请输入用户名',
          passwordRequired: '请输入密码'
        }
      },
      register: {
        title: '注册',
        username: '用户名',
        password: '密码',
        confirm: '确认密码',
        email: '邮箱',
        phone: '手机号',
        submit: '注册',
        login: '使用已有账号登录',
        success: '注册成功',
        error: '注册失败'
      },
      forgot: {
        title: '忘记密码',
        email: '邮箱',
        submit: '提交',
        back: '返回登录',
        success: '重置密码邮件已发送',
        error: '重置密码失败'
      },
      info: {
        loading: '正在加载用户信息',
        success: '获取用户信息成功'
      },
      autoLogout: '由于长时间未操作，您已被自动登出',
      error: {
        noResponse: '服务器无响应',
        noSaltData: '获取加密参数失败',
        invalidSalt: '加密参数格式无效',
        invalidIterations: '加密迭代次数无效',
        permanentlyLocked: '账号已被永久锁定，请联系管理员解锁',
        temporarilyLocked: '账号已被临时锁定，请等待{minutes}分钟后再试',
        tooManyAttempts: '登录失败次数过多，账号已被锁定',
        invalidCredentials: '用户名或密码错误'
      }
    }
  }
} 