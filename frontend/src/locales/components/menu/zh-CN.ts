import { countReset } from "node:console";

export default {
  menu: {
    home: '首页',
    dashboard: {
      title: '仪表盘',
      workplace: '工作台',
      analysis: '分析台',
      monitor: '监控台'
    },
    components: {
      title: '组件',
      icons: '图标'
    },
    about: {
      title: '关于我们',
      privacy: '隐私政策',
      terms: '服务条款',
      index: '关于Hbt'
    },
    core: {
      _self: '核心管理',
      config: '系统配置',
      language: '语言管理',
      dict: '字典管理',
    },
    identity: {
      _self: '身份认证',
      user: '用户管理',
      role: '角色管理',
      dept: '部门管理',
      post: '岗位管理',
      menu: '菜单管理',
      tenant: '租户管理',
      oauth: 'OAuth管理',
      profile: '个人信息',
      changePassword: '修改密码'
    },
    audit: {
      _self: '审计日志',
      operlog: '操作日志',
      loginlog: '登录日志',
      sqldifflog: '差异日志',
      exceptionlog: '异常日志',
      auditlog: '审计日志',
      quartzlog: '任务日志',
      server: '服务监控'
    },
    workflow: {
      _self: '工作流程',
      form: '表单管理',
      definition: '流程定义',
      instance: '流程实例',
      task: '工作任务',
      node: '流程节点',
      variable: '流程变量',
      history: '流程历史'
    },
    signalr: {
      _self: '实时通信',
      online: '在线用户',
      message: '在线消息'
    },
    generator: {
      _self: '代码生成',
      table: '数据库表',
      tableDefine: '表列定义',
      template: '代码模板',
      config: '生成配置',
      api: '接口文档'
    },
    routine: {
      _self: '日常办公',
      vehicle: {
        _self: '用车管理',
        vehicleMaster: {
          _self: '车辆主数据',
          vehicleInfo: '车辆信息',
          driverInfo: '驾驶员信息',
          maintenance: '车辆维护'
        },
        vehicleBooking: {
          _self: '用车申请',
          newBooking: '新建申请',
          bookingList: '申请列表',
          bookingApproval: '申请审批'
        },
        vehicleDispatch: {
          _self: '车辆调度',
          dispatchPlan: '调度计划',
          realTimeTracking: '实时跟踪',
          dispatchHistory: '调度历史'
        },
        vehicleReporting: {
          _self: '用车报表',
          usageReport: '使用报表',
          costReport: '费用报表',
          maintenanceReport: '维护报表'
        }
      },
      file: '文件管理',
      mail: '邮件管理',
      mailTmpl: '邮件模板',
      meeting: {
        _self: '会议管理',
        meetingRoom: {
          _self: '会议室管理',
          roomInfo: '会议室信息',
          roomBooking: '会议室预订',
          roomSchedule: '会议室日程'
        },
        meetingPlan: {
          _self: '会议计划',
          newMeeting: '新建会议',
          meetingList: '会议列表',
          meetingApproval: '会议审批'
        },
        meetingExecution: {
          _self: '会议执行',
          attendance: '会议签到',
          minutes: '会议纪要',
          followUp: '会议跟进'
        },
        meetingReporting: {
          _self: '会议报表',
          meetingReport: '会议报表',
          attendanceReport: '出席报表',
          costReport: '费用报表'
        }
      },
      notice: '通知公告',
      schedule: '日程管理',
      quartz: '工作任务'
    },
    finance: {
      _self: '核算',
      accounting: {
        _self: '管理会计',
        companyaccounts: '公司科目',
        glaccount: '会计科目',
        generalledger: '总账',
        payable: '应付',
        receivable: '应收',
        asset: '固定资产',
        bank: '银行',
        tax: '税务',
        planning: '计划管理',
        reporting: '报表与分析'
      },
      controlling: {
        _self: '控制会计',
        costelement: '成本要素',
        costcenter: '成本中心',
        profitcenter: '利润中心',
        accountsReceivable: '应收账款',
        accountsPayable: '应付账款',
        assetAccounting: '资产会计',
        tax: '税务管理',
        financialReporting: '财务报表'
      
    },
  },
    logistics: {
      _self: '后勤',
      equipment: {
        _self: '设备管理',
        data: '设备主数据',
        location: '设备位置',
        material: '物料关联',
        workorder: '工单'

      },
      material: {
        _self: '物料管理',
        info: '物料数据',
        factory: '工厂物料',
        vendor: '卖方',
        supplier: '供应商',
        price: '物料价格',
        requisition: '采购申请',
        order: '采购订单'


      },
      production: {
        _self: '生产管理',
        bom: '物料清单 (BOM)',
        routing: '工艺路线',
        change: '工程变更',
        workcenter: '工作中心',
        order: '生产订单',
        kanban: '看板'


      },
      project: {
        _self: '项目管理',
        define: '项目定义',
        cost: '成本计划',
        resource: '资源计划',
        schedule: '进度计划',

      },
      quality: {
        _self: '质量管理',
        item: '检验项目',
        receiving: '收货检验',
        process: '过程检验',
        storage: '存储检验',
        return: '退货检验',
  
      },
      sales: {
        _self: '销售管理',
        customer: '顾客',
        client: '客户',
        price: '销售价格',
        order: '销售订单',
      },
      service: {
        _self: '客户服务',
        item: '服务项目',
        contract: '服务合同',
        request: '服务请求',
        workorder: '服务工单',
        timesheet: '工时记录',
        consumption: '物料消耗',
        outsourcing: '外协服务'

      },
      complaint: {
        _self: '客诉管理',
        notice: '质量通知单',
        mark: '客诉明细',
        analysis: '原因分析',
        corrective: '纠正措施',
        return: '退换货执行',
        followUp: '跟进处理'
      }
    },
    humanResources: {
      _self: '人力资源管理',
      employeeManagement: {
        _self: '员工管理',
        employeeMaster: '员工主数据',
        attendance: '考勤管理',
        leave: '请假管理',
        payroll: '薪资管理',
        contractManagement: '合同管理' // 新增合同管理
      },
      recruitment: {
        _self: '招聘管理',
        jobPosting: '职位发布',
        candidateManagement: '候选人管理',
        interviewScheduling: '面试安排',
        offerManagement: '录用管理'
      },
      training: {
        _self: '培训管理',
        trainingPlan: '培训计划',
        trainingExecution: '培训执行',
        trainingEvaluation: '培训评估'
      },
      performance: {
        _self: '绩效管理',
        goalSetting: '目标设定',
        performanceReview: '绩效评估',
        feedback: '反馈管理'
      },
      reporting: {
        _self: '人力资源报表',
        employeeReports: '员工报表',
        attendanceReports: '考勤报表',
        payrollReports: '薪资报表',
        performanceReports: '绩效报表'
      }
    }
  }
}
