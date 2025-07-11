import { CiCircleFilled } from "@ant-design/icons-vue";
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
      overview: '流程总览',
      my: '我的流程',
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
      core: {
        _self: '基础服务',
        numberrule: '编码规则',
        config: '系统配置',
        language: '语言管理',
        dict: '字典管理',
      },
      contract: {
        _self: '合同管理',
        template:{
          _self: '合同模板',
          manage: '模板管理',
          category: '模板分类',
        },
        draft: {
          _self: '合同起草',
          apply: '起草申请',
          my: '我的起草',
        },
        approval: {
          _self: '合同审批',
          pending: '合同审批',
          approved: '已审批',
          record: '审批记录',
        },
        execution: {
          _self: '合同执行',
          track: '执行跟踪',
          change: '变更管理',
          payment: '付款管理',
        },
        archive: {
          _self: '合同归档',
          manage: '归档管理',
          query: '查询统计',
        },
      },
      project: {
        _self: '项目管理',
        info:{
          _self: '项目信息',
          list: '项目列表',
        },
        plan:{
          _self: '项目计划',
          request: '计划请求',
          gantt: '项目甘特图',
        },
        task:{
          _self: '项目任务',
          assign: '任务分配',
          track: '任务跟踪',
          board: '任务看板',
        },
        resource:{
          _self: '项目资源',
          personnel: '人员管理',
          equipment: '设备管理',
          budget: '预算管理',
        },
        monitor:{
          _self: '项目监控',
          progress: '进度监控',
          quality: '质量监控',
          risk: '风险监控',
        },
      },
      quartz: {
        _self: '任务调度',
        job: {
          _self: '任务管理',
          config: '任务配置',
          list: '任务列表',
          status: '任务状态',
        },
        schedule: {
          _self: '任务调度',
          config: '调度配置',
          monitor: '调度监控',
          stats: '调度统计',
        },
      },
      schedule: {
        _self: '日程管理',
        myschedule: '我的日程',
        dashboard: '日程看板',
      },
      car: {
        _self: '用车管理',
        my: '我的用车',
        
        application: '用车申请',
        dashboard: '用车看板',
        management:{
          _self: '车管管理',
          info: '车辆信息',
          maintenance: '车辆保养',
        }

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
      news:{
        _self: '新闻管理',
        list: '新闻列表',
      },
      regulation:{
        _self: '规章制度',
        publish: '发布',
        signoff: '签收',
        list: '规章制度',
      },
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
  accounting: {
      _self: '会计核算',
      financial: {
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
            epp: 'EPP生产',
            oph: '生产OPH',
            modify: '生产改修',
            rework: '生产返工',
            eppDefect: 'EPP不良',
            productionDefect: '生产不良',
            modifyDefect: '改修不良',
            reworkDefect: '返工不良',
            eppWorktime: 'EPP工数',
            productionWorktime: '生产工数',
            modifyWorktime: '修改工数',
            reworkWorktime: '返工工数',
            productionReport: '生产报表',
            defectSummary: '不良集计',
            worktimeReport: '工时报表'
          },
          workshop2: {
            _self: '制二课',
            epp: 'EPP生产',
            oph: '生产OPH',
            modify: '生产改修',
            rework: '生产返工',
            eppInspection: 'EPP检查',
            productionInspection: '生产检查',
            modifyInspection: '改修检查',
            reworkInspection: '返工检查',
            eppRepair: 'EPP修理',
            productionRepair: '生产修理',
            modifyRepair: '改修修理',
            reworkRepair: '返工修理',
            eppWorktime: 'EPP工数',
            productionWorktime: '生产工数',
            modifyWorktime: '修改工数',
            reworkWorktime: '返工工数',
            productionReport: '生产报表',
            defectSummary: '不良集计',
            worktimeReport: '工时报表'
          }
        },
        sop: {
          _self: 'SOP管理',
          workshop1: '制一课',
          workshop2: '制二课',
        },
        techcontact: {
          _self: '技联管理',
          epp: 'EPP联络',
          engineering: '工程设变',
          external: '外部联络',
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
        cs: {
          _self: '客户服务',
          item: '服务项目',
          contract: '服务合同',
          request: '服务请求',
          workorder: '服务工单',
        },
        cc: {
          _self: '客诉管理',
          notice: '质量通知单',
          mark: '客诉明细',
          analysis: '原因分析',
          corrective: '纠正措施',
          return: '退换货执行',
          followUp: '跟进处理'
        }
    },
  },
    hrm: {
      _self: '人力资源',
      attendance: {
        _self: '考勤管理',
        record: '考勤记录',
        holiday: '假期管理',
        overtime: '加班管理',
        compensatory: '调休管理',
      },
      benefit: {
        _self: '福利管理',
        project: '福利项目',
        employee: '员工福利',
      },
      employee: {
        _self: '人员管理',
        info: '人员信息',
        contracttype: '合同类型',
        contract: '合同管理',
        promotion: '晋升管理',
        promotionhistory: '晋升历史',
        resignation: '离职管理',
        transfer: '人员列表',
        transferhistory: '调岗历史',
      },
      leave: {
        _self: '请假管理',
        type: '请假类型',
        employee: '员工请假',
      },
      organization: {
        _self: '组织管理',
        positioncategory: '职位类别',
        company: '公司信息',
        department: '部门信息',
        position: '岗位信息',
      },
      performance: {
        _self: '绩效管理',
        assessmentitem: '考核项目',
        assessment: '绩效考核',
      },
      recruitment: {
        _self: '招聘管理',
        application: '职位申请',
        posting: '职位发布',
        candidate: '候选人管理',
        interview: '面试管理',
      },
      salary: {
        _self: '薪资管理',
        employee: '员工薪资',
        housing: '公积金',
        housinglevel: '社保',
        tax: '税务管理',
        taxlevel: '个税等级',
        structure: '薪资结构',
        social: '社保',
        socialbase: '社保基数',
      },
      training: {
        _self: '培训管理',
        category: '培训类别',
        course: '培训课程',
        record: '员工培训',
      },
      report: {
        _self: '报表管理',
        employeeinfo: '人员信息',
        resignation: '离职报表',
        transfer: '调岗报表',
        promotion: '晋升报表',
        training: '培训报表',
        salary: '薪资报表',
        performance: '绩效报表',
        attendance: '考勤报表',
        benefit: '福利报表',
        recruitment: '招聘报表',
      }
    }

}
}

