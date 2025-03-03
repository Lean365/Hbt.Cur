export default {
  common: {
    // 基础标题
    title: {
      list: '列表',
      detail: '详情',
      create: '新增',
      edit: '编辑',
      preview: '预览'
    },

    // 系统信息
    system: {
      title: '黑冰台',
      slogan: '专业、高效、安全的企业级管理系统',
      description: '基于.NET 8和Vue 3的现代化企业管理系统',
      copyright: '© 2024 Lean365. 保留所有权利。'
    },

    // 操作按钮
    actions: {
      // === 基础操作按钮 ===
      add: '新增',           // @btn-add-color
      edit: '编辑',          // @btn-edit-color
      delete: '删除',        // @btn-delete-color
      batchDelete: '批量删除', // @btn-batch-delete-color
      view: '查看',          // @btn-view-color
      clear: '清空',         // @btn-clear-color

      // === 数据操作按钮 ===
      import: '导入',        // @btn-import-color
      export: '导出',        // @btn-export-color
      template: '模板',      // @btn-template-color
      preview: '预览',       // @btn-preview-color
      download: '下载',      // @btn-download-color
      batchImport: '批量导入', // @btn-batch-import-color
      batchExport: '批量导出', // @btn-batch-export-color
      batchPrint: '批量打印', // @btn-batch-print-color
      batchEdit: '批量编辑',  // @btn-batch-edit-color
      batchUpdate: '批量更新', // @btn-batch-update-color

      // === 状态操作按钮 ===
      audit: '审核',         // @btn-audit-color
      revoke: '撤销',        // @btn-revoke-color
      stop: '停止',          // @btn-stop-color
      run: '运行',           // @btn-run-color
      force: '强制',         // @btn-forced-color

      // === 系统功能按钮 ===
      generate: '生成',      // @btn-generate-color
      refresh: '刷新',       // @btn-refresh-color
      info: '信息',          // @btn-info-color
      log: '日志',           // @btn-log-color
      chat: '消息',          // @btn-chat-color
      copy: '复制',          // @btn-copy-color
      execute: '执行',       // @btn-execute-color
      resetPwd: '重置密码',   // @btn-reset-pwd-color
      open: '打开',          // @btn-open-color
      close: '关闭',         // @btn-close-color
      more: '更多',          // @btn-more-color
      density: '密度',       // @btn-density-color
      columnSetting: '列设置', // @btn-column-setting-color

      // === 扩展功能按钮 ===
      search: '搜索',        // @btn-search-color
      filter: '筛选',        // @btn-filter-color
      sort: '排序',          // @btn-sort-color
      config: '配置',        // @btn-config-color
      save: '保存',          // @btn-save-color
      cancel: '取消',        // @btn-cancel-color
      upload: '上传',        // @btn-upload-color
      print: '打印',         // @btn-print-color
      help: '帮助',          // @btn-help-color
      share: '分享',         // @btn-share-color
      lock: '锁定',          // @btn-lock-color
      sync: '同步',          // @btn-sync-color
      expand: '展开',        // @btn-expand-color
      collapse: '收起',      // @btn-collapse-color
      approve: '同意',       // @btn-approve-color
      reject: '拒绝',        // @btn-reject-color
      comment: '评论',       // @btn-comment-color
      attach: '附件',        // @btn-attach-color

      // === 语言支持按钮 ===
      translate: '翻译',     // @btn-translate-color
      langSwitch: '切换语言', // @btn-lang-switch-color
      dict: '字典',          // @btn-dict-color

      // === 数据分析按钮 ===
      analyze: '分析',       // @btn-analyze-color
      chart: '图表',         // @btn-chart-color
      report: '报表',        // @btn-report-color
      dashboard: '仪表盘',    // @btn-dashboard-color
      statistics: '统计',    // @btn-statistics-color
      forecast: '预测',      // @btn-forecast-color
      compare: '对比',       // @btn-compare-color

      // === 工作流按钮 ===
      startFlow: '启动流程',  // @btn-start-flow-color
      endFlow: '结束流程',    // @btn-end-flow-color
      suspendFlow: '暂停流程', // @btn-suspend-flow-color
      resumeFlow: '恢复流程',  // @btn-resume-flow-color
      transfer: '转办',       // @btn-transfer-color
      delegate: '委托',       // @btn-delegate-color
      notify: '通知',        // @btn-notify-color
      urge: '催办',          // @btn-urge-color
      sign: '签名',          // @btn-sign-color
      countersign: '会签',    // @btn-countersign-color

      // === 移动端专用按钮 ===
      scan: '扫描',          // @btn-scan-color
      location: '定位',      // @btn-location-color
      call: '呼叫',          // @btn-call-color
      photo: '拍照',         // @btn-photo-color
      voice: '语音',         // @btn-voice-color
      faceId: '人脸识别',     // @btn-face-id-color
      fingerPrint: '指纹',    // @btn-finger-print-color

      // === 社交协作按钮 ===
      follow: '关注',        // @btn-follow-color
      collect: '收藏',       // @btn-collect-color
      like: '点赞',          // @btn-like-color
      forward: '转发',       // @btn-forward-color
      at: '@',              // @btn-at-color
      group: '群组',         // @btn-group-color
      team: '团队',          // @btn-team-color

      // === 安全认证按钮 ===
      verifyCode: '验证码',   // @btn-verify-code-color
      bind: '绑定',          // @btn-bind-color
      unbind: '解绑',        // @btn-unbind-color
      authorize: '授权',      // @btn-authorize-color
      deauthorize: '取消授权', // @btn-deauthorize-color

      // === 高级功能按钮 ===
      version: '版本',       // @btn-version-color
      history: '历史',       // @btn-history-color
      restore: '还原',       // @btn-restore-color
      archive: '归档',       // @btn-archive-color
      unarchive: '取消归档',  // @btn-unarchive-color
      merge: '合并',         // @btn-merge-color
      split: '拆分',         // @btn-split-color

      // === 系统管理按钮 ===
      backup: '备份',        // @btn-backup-color
      restoreSys: '系统还原', // @btn-restore-sys-color
      clean: '清理',         // @btn-clean-color
      optimize: '优化',      // @btn-optimize-color
      monitor: '监控',       // @btn-monitor-color
      diagnose: '诊断',      // @btn-diagnose-color
      maintain: '维护'       // @btn-maintain-color
    },

    // 状态
    status: {
      label: '状态',
      normal: '正常',
      disabled: '停用',
      placeholder: '请选择状态'
    },

    // 是否
    yesNo: {
      yes: '是',
      no: '否'
    },

    // 显示
    visible: {
      show: '显示',
      hide: '隐藏'
    },

    // 时间
    datetime: {
      date: '日期',
      time: '时间',
      year: '年',
      month: '月',
      day: '日',
      hour: '时',
      minute: '分',
      second: '秒',
      startDate: '开始日期',
      endDate: '结束日期',
      startTime: '开始时间',
      endTime: '结束时间',
      createTime: '创建时间',
      updateTime: '更新时间',
      formatError: '格式化时间失败',
      relativeTimeFormatError: '格式化相对时间失败',
      parseError: '解析日期失败',
      rangeSeparator: ' 至 '
    },

    // 表单
    form: {
      required: '必填',
      optional: '选填',
      invalid: '无效',
      placeholder: {
        select: '请选择',
        input: '请输入',
        date: '请选择日期',
        time: '请选择时间'
      }
    },

    // 表格
    table: {
      header: {
        operation: '操作'
      },
      config: {
        density: {
          default: '默认',
          middle: '中等',
          small: '紧凑'
        },
        columnSetting: '列设置'
      },
      pagination: {
        total: '共 {total} 条',
        current: '第 {current} 页',
        pageSize: '每页 {pageSize} 条',
        jump: '跳至'
      },
      empty: '暂无数据',
      loading: '加载中...',
      selectAll: '全选',
      selected: '已选择 {total} 项'
    },

    // 导入导出
    import: {
      title: '导入数据',
      file: '选择文件',
      select: '选择文件',
      template: '下载模板',
      download: '下载模板',
      note: '导入说明',
      tips: '请严格按照导入模板的格式填写数据，否则可能导致导入失败',
      format: '仅支持导入Excel文件！',
      size: '文件大小不能超过2MB！',
      total: '总记录数',
      success: '成功数',
      failed: '失败数',
      message: '失败原因'
    },

    // 上传
    upload: {
      text: '将文件拖到此处，或点击上传',
      picture: '点击上传图片',
      file: '点击上传文件',
      icon: '点击上传图标',
      limit: {
        size: '文件大小不能超过 {size}',
        type: '仅支持 {type} 格式'
      }
    },

    // 结果
    result: {
      success: '操作成功',
      failed: '操作失败',
      warning: '警告',
      info: '提示',
      error: '错误'
    },

    // 消息
    message: {
      loading: '处理中...',
      saving: '保存中...',
      submitting: '提交中...',
      deleting: '删除中...',
      operationSuccess: '操作成功',
      operationFailed: '操作失败',
      deleteConfirm: '确定要删除吗？',
      deleteSuccess: '删除成功',
      deleteFailed: '删除失败',
      createSuccess: '新增成功',
      createFailed: '新增失败',
      updateSuccess: '更新成功',
      updateFailed: '更新失败',
      networkError: '网络连接失败，请检查网络',
      systemError: '系统错误',
      timeout: '请求超时',
      invalidResponse: '响应数据格式错误',
      backendNotStarted: '后端服务未启动，请先启动后端服务',
      invalidRequest: '请求参数错误',
      unauthorized: '未授权，请重新登录',
      forbidden: '拒绝访问',
      notFound: '请求的资源不存在',
      serverError: '服务器内部错误',
      httpError: {
        400: '请求参数错误',
        401: '未授权，请重新登录',
        403: '拒绝访问',
        404: '请求的资源不存在',
        500: '服务器内部错误',
        502: '网关错误',
        503: '服务不可用',
        504: '网关超时'
      }
    },

    add: '新增',
    edit: '编辑',
    delete: '删除',
    save: '保存',
    cancel: '取消',
    search: '搜索',
    reset: '重置',
    confirm: '确认',
    back: '返回'
  }
} 