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
      schedule: {
        _self: '日程管理',
        myschedule: '我的日程',
        dashboard: '日程看板',
      },
      car: {
        _self: '用车管理',
        info: '车辆信息',
        application: '用车申请',
        dashboard: '用车看板',
        maintenance: '车辆保养',
      },
      email: {
        _self: '邮件管理',
        inbox: '收件箱',
        drafts: '草稿箱',
        sent: '已发送',
        trash: '垃圾箱',
        template: '邮件模板',        
      },
      meeting: {
        _self: '会议管理',
        room: '会议室',
        mymeeting: '我的会议',
        booking: '会议预约',
        dashboard: '会议看板',
      },
      notice: { 
        _self: '通知公告',
        message: {
          _self: '消息管理',
          mymessages: '我的消息',
          list: '消息看板',
        },
        announcement: {
          _self: '公告管理',
          signoff: '签收公告',
          list: '公告列表',
        },
        notification: {
          _self: '通知管理',
          ack: '已读通知',
          list: '通知列表',
        },
      },
      hr: {
        _self: '人事考勤',
        recruitment: {
          _self: '招聘管理',
          apply: '招聘申请',
          approval: '招聘审批',
          list: '招聘列表',

        },
        transfer: {
          _self: '转岗管理',
          apply: '转岗申请',
          approval: '转岗审批',
          list: '转岗列表',
        },
        leave: {
          _self: '请假管理',
          apply: '请假申请',
          approval: '请假审批',
          list: '请假列表',
        },
        trip: {
          _self: '出差管理',
          apply: '出差申请',
          approval: '出差审批',
          list: '出差列表',
        },
        overtime: {
          _self: '加班管理',
          apply: '加班申请',
          approval: '加班审批',
          list: '加班列表',
      },
    },
    expense:{
      _self: '费用管理',
      daily: {
        _self: '日常费用',
        apply: '费用申请',
        approve: '费用审批',
        list: '费用列表',
      },
      travel: {
        _self: '差旅费用',
        apply: '差旅费申请',
        approve: '差旅费审批',
        list: '差旅费列表',
      },
    },
    file:{
      _self: '文件管理',
      daily: {
        _self: '日常文件',
        list: '文件列表',
      },
      iso: {
        _self: 'ISO文件',
        version: '版本',
        signoff: '签收',
        list: 'ISO文件',
      },
      document: { 
        _self: '公文管理',
        version: '版本',
        signoff: '签收',
        list: '公文列表',
      },
    },
    officesupplies:{
      _self: '办公用品',
      inventory:{
        _self: '库存管理',
        requisition: '请购管理',
        inbound: '入库管理',
        stocktaking: '盘点管理',
      },
      usage:{
        _self: '领用管理',
        apply: '领用申请',
        approve: '领用审批',
        receive: '领用记录',
      }
    },
    book:{
      _self: '图书管理',
      inventory:{
        _self: '库存管理',
        requisition: '请购管理',
        inbound: '入库管理',
        list: '图书清单',
        stocktaking: '盘点管理',
      },
      usage:{
        _self: '领用管理',
        card: '借阅证',
        borrow: '借出',
        return: '归还',
      }

    },
    medical:{
      _self: '医务管理',
      medicine:{
        _self: '库存管理',
        requisition: '请购管理',
        inbound: '入库管理',
        list: '药品清单',
        stocktaking: '盘点管理',
      },
      usage:{
        _self: '领用管理',
        archive: '档案',
        receive: '领药',
        cost: '费用',
      }

    },
  },
    finance: {
      _self: '会计核算',
      accounting: {
        _self: '管理会计',
        company: "公司信息",
        account: '会计科目',
        companyaccount: '公司科目',
        ledger: '总账',
        payable: '应付',
        receivable: '应收',
        fixedasset: '固定资产',
        bank: '银行信息',

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
    budget:{
      _self: '全面预算',
        formulation: {
          _self: '预算编制',
          sales: {
            _self: '销售预算',
            cost: '销售成本',
            rolling: '销售滚动',
          },
          production: {
            _self: '生产预算',
            auxiliary: '生产辅料',
            labor: '生产人工',
            manufacturing: '生产制造',
          },
          cost: {
            _self: '成本预算',
            directmaterial: '直接材料',
            directlabor: '直接人工',
            indirectlabor: '间接人工',
            manufacturing: '制造费用',
          },
          expense: {
            _self: '费用预算',
            sales: '销售费用',
            management: '管理费用',
            financial: '财务费用',
          },
          financial: {
            _self: '财务预算',
            cashflow: '现金流量',
            balancesheet: '资产负债表',
            income: '利润表',
          },
        },
        control: {
          _self: '预算控制',
          dashboard: '预算看板',
          approval: '预算审批',
        },   
  },
},
    logistics: {
      _self: '后勤管理',
      equipment: {
        _self: '设备管理',
        data: '设备主数据',
        location: '设备位置',
        material: '物料关联',
        workorder: '工单'

      },
      material: {
        _self: '物料管理',
        material:{
          _self: '物料管理',
          material: '物料主数据',
          plant: '工厂信息',
          master: '物料数据',
          plantmaster: '工厂物料',
          vendor: '卖方信息',
          supplier: '供应商信息',
        },
        purchase:{
          _self: '采购管理',
          vendor: '卖方信息',
          supplier: '供应商信息',
          price: '采购价格',
          requisition: '采购请购',
          order: '采购订单',

        },



      },
      production: {
        _self: '生产管理',
        bom: '物料清单',
        change: {
          _self: '设计变更',
          implementation: '设变实施',
          techcontact: '技术联络',
          material: '物料确认',
          query: '设变查询',
          oldproduct: '旧品管制',
          sop: 'SOP确认',
          batch: '投入批次',
          input: {
            _self: '设变录入',
            gijutsu: '技术课',
            seikan: '生管课',
            koubai: '采购课',
            uketsuke: '受检课',
            bukan: '部管课',
            seizou2: '制二课',
            seizou1: '制一课',
            hinkan: '品管课',
            seizougijutsu: '制技课',
  
          }
        },
        workcenter: '工作中心',
        order: '生产订单',
        kanban: '看板',
        oph:{
          _self: 'OPH管理',
          workshop1: {
            _self: '制一课',
            output: '生产日报',
            defect: '生产不良',
            worktime: '生产工数',
            productionReport: '生产报表',
            defectSummary: '不良集计',
            worktimeReport: '工时报表'
          },
          workshop2: {
            _self: '制二课',
            output: '生产日报',
            inspection: '检查记录',
            repair: '修理记录',
            worktime: '生产工数',
            productionReport: '生产报表',
            inspectionReport: '检查报表',
            repairReport: '修理报表',
            worktimeReport: '工时报表'
          }
        }

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
        receiving: '来料检验',
        process: '过程检验',
        storage: '入库检验',
        return: '退货检验',
  
      },
      sales: {
        _self: '销售管理',
        customer: '顾客信息',
        client: '客户信息',
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
