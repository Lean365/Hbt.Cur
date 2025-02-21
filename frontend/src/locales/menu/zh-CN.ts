export default {
  menu: {
    // 基础路由
    login: '登录',
    home: '首页',
    dashboard: {
      title: '仪表盘',
      workplace: '工作台',
      analysis: '分析页',
      monitor: '监控页'
    },
    about: {
      title: '关于',
      index: '关于系统',
      terms: '使用条款',
      privacy: '隐私政策'
    },

    /**
     * 系统管理
     */
    admin: {
      _self: '系统管理',
      config: '系统配置',
      language: '语言管理',
      dicttype: '字典类型',
      dictdata: '字典数据',
      translation: '翻译管理'
    },

    /**
     * 身份认证
     */
    identity: {
      _self: '身份认证',
      user: '用户管理',
      role: '角色管理',
      dept: '部门管理',
      post: '岗位管理',
      menu: '菜单管理',
      tenant: '租户管理',
      auth: '认证管理',
      oauth: 'OAuth管理',
      loginpolicy: '登录策略',
      loginextend: '登录扩展',
      deviceextend: '设备扩展'
    },

    /**
     * 审计日志
     */
    audit: {
      _self: '审计日志',
      operlog: '操作日志',
      loginlog: '登录日志',
      dbdifflog: '数据变更',
      exceptionlog: '异常日志'
    },

    /**
     * 工作流程
     */
    workflow: {
      _self: '工作流程',
      definition: '流程定义',
      instance: '流程实例',
      task: '任务管理',
      node: '节点管理',
      variable: '变量管理',
      history: '历史记录'
    },

    /**
     * 实时监控
     */
    realtime: {
      _self: '实时监控',
      'onlineuser': '在线用户',
      'onlinemessage': '在线消息'
    },

    /**
     * 安全管理
     */
    security: {
      _self: '安全管理',
      captcha: '验证码管理'
    }
  }
} 