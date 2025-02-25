export default {
  identity: {
    auth: {
      // 登录相关
      login: {
        title: '用户登录',
        username: '用户名',
        password: '密码',
        tenantId: '租户ID',
        rememberMe: '记住我',
        forgotPassword: '忘记密码？',
        submit: '登录',
        otherLogin: '其他登录方式',
        start: '开始登录',
        success: '登录成功',
        failed: '登录失败',
        noToken: '未收到访问令牌',
        
        form: {
          tenantIdRequired: '请输入租户ID',
          usernameRequired: '请输入用户名',
          usernameLength: '用户名长度必须在3-50个字符之间',
          passwordRequired: '请输入密码',
          passwordLength: '密码长度必须在6-100个字符之间',
          forgot: '忘记密码',
          submit: '登录'
        },

        error: {
          waitingRetry: '请等待 {seconds} 秒后再试',
          saltError: '获取加密参数失败，请重试',
          accountLocked: '账号已被锁定 {minutes} 分钟',
          remainingAttempts: '登录失败，剩余尝试次数：{count} 次',
          serverError: '服务器内部错误',
          invalidCredentials: '用户名或密码错误',
          accountDisabled: '账号已被禁用',
          tenantDisabled: '租户已被禁用',
          tenantNotFound: '租户不存在'
        },

        notAvailable: '{feature}功能暂未开放'
      },

      // 验证码相关
      captcha: {
        title: '安全验证',
        required: '请完成安全验证',
        waitingRetry: '请等待 {seconds} 秒后重试',
        verifyFailed: '验证失败，请重试',
        success: '验证成功',
        moving: '请向右滑动滑块',
        default: '请向右滑动完成验证',
        bgImage: '背景图片',
        sliderImage: '滑块图片',
        failed: '验证失败',
        maxRetryReached: '验证失败次数过多，请稍后重试',
        verifyError: '验证出错，请重试',
        error: {
          getFailed: '获取验证码失败',
          dataEmpty: '验证码数据为空',
          dataIncomplete: '验证码数据不完整',
          tooManyRequests: '请求过于频繁，请等待 {seconds} 秒后重试'
        }
      },

      // 用户信息
      info: {
        loading: '正在加载用户信息',
        invalidResponse: '无效的响应数据',
        success: '用户信息加载成功',
        error: {
          invalidResponse: '获取用户信息失败：无效的响应',
          failed: '获取用户信息失败'
        }
      },

      // 登出相关
      logout: {
        title: '退出登录',
        confirm: '确定要退出登录吗？',
        start: '开始退出登录',
        success: '退出登录成功',
        error: '退出登录失败'
      },

      // 用户状态
      status: {
        online: '在线',
        offline: '离线',
        busy: '忙碌',
        away: '离开'
      },

      // 用户角色
      role: {
        admin: '管理员',
        user: '普通用户',
        guest: '访客'
      },

      // 个人信息
      profile: {
        title: '个人信息',
        basic: {
          username: '用户名',
          nickname: '昵称',
          email: '邮箱',
          phone: '手机号',
          avatar: '头像'
        },
        security: {
          password: '密码',
          oldPassword: '原密码',
          newPassword: '新密码',
          confirmPassword: '确认密码'
        },
        preferences: {
          language: '语言',
          theme: '主题',
          notification: '通知设置'
        }
      },

      // 错误消息
      error: {
        accountNotExist: '账号不存在',
        accountDisabled: '账号已被禁用',
        accountLocked: '账号已被锁定',
        passwordError: '密码错误',
        captchaError: '验证码错误',
        tokenExpired: '登录已过期，请重新登录',
        unauthorized: '未经授权的访问',
        forbidden: '禁止访问该资源'
      }
    }
  }
} 