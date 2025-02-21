export default {
  menu: {
    // Basic Routes
    login: 'Login',
    home: 'Home',
    dashboard: {
      title: 'Dashboard',
      workplace: 'Workplace',
      analysis: 'Analysis',
      monitor: 'Monitor'
    },
    about: {
      title: 'About',
      index: 'About System',
      terms: 'Terms of Service',
      privacy: 'Privacy Policy'
    },

    /**
     * System Management
     */
    admin: {
      _self: 'System',
      config: 'Configuration',
      language: 'Language',
      dicttype: 'Dict Type',
      dictdata: 'Dict Data',
      translation: 'Translation'
    },

    /**
     * Identity Management
     */
    identity: {
      _self: 'Identity',
      user: 'Users',
      role: 'Roles',
      dept: 'Departments',
      post: 'Posts',
      menu: 'Menus',
      tenant: 'Tenants',
      auth: 'Authentication',
      oauth: 'OAuth',
      loginpolicy: 'Login Policy',
      loginextend: 'Login Extension',
      deviceextend: 'Device Extension'
    },

    /**
     * Audit Logs
     */
    audit: {
      _self: 'Audit',
      operlog: 'Operation Log',
      loginlog: 'Login Log',
      dbdifflog: 'Data Changes',
      exceptionlog: 'Exception Log'
    },

    /**
     * Workflow Management
     */
    workflow: {
      _self: 'Workflow',
      definition: 'Definitions',
      instance: 'Instances',
      task: 'Tasks',
      node: 'Nodes',
      variable: 'Variables',
      history: 'History'
    },

    /**
     * Real-time Monitoring
     */
    realtime: {
      _self: 'Real-time',
      'onlineuser': 'Online Users',
      'onlinemessage': 'Online Messages'
    },

    /**
     * Security Management
     */
    security: {
      _self: 'Security',
      captcha: 'CAPTCHA'
    }
  }
} 