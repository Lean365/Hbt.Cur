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
      engine:{
        _self: '流程引擎',
        monitor: '流程监控',
        todo: '待办任务',
        done: '已办任务',
        signoff: '流程签核',
        execution: '流程执行',
        designer: '流程设计器'
      },
      manage:{
        _self: '流程管理',
        form: '表单管理',
        scheme: '流程方案',
        instance: '流程实例',
        oper: '实例操作',
        trans: '实例流转'
      }
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
        dict: '字典管理'
      },
      contract: {
        _self: '合同管理',
        template: '合同模板', 
        draft: '合同起草',
        approval: '合同审批',
        execution: '合同执行',
        archive: '合同归档'
      },
      project: {
        _self: '项目管理',
        info: '项目信息',
        plan: '项目计划',
        task: '项目任务',
        resource: '项目资源',
        monitor: '项目监控'
      },
      quartz: {
        _self: '任务调度',
        job: '定时任务',
        schedule: '任务调度'
      },
      schedule: {
        _self: '日程管理',
        myschedule: '我的日程',
        dashboard: '日程看板'
      },
      vehicle: {
        _self: '用车管理',
        dispatch: '用车调度',
          info: '车辆信息',
          maintenance: '车辆保养'

      },
      email: {
        _self: '邮件管理',
        inbox: '收件箱',
        drafts: '草稿箱',
        sent: '已发送',
        trash: '垃圾箱',
        template: '邮件模板'
      },
      news:{
        _self: '新闻管理',
        hot: '热点新闻',
        comment: '评论管理',
        like: '点赞管理'
      },

      meeting: {
        _self: '会议管理',
        room: '会议室',
        mymeeting: '我的会议',
        booking: '会议预约',
        dashboard: '会议看板'
      },
      notice: {
        _self: '通知公告',
        message: {
          _self: '消息管理',
          mymessages: '我的消息',
          list: '消息看板'
        },
        announcement: {
          _self: '公告管理',
          manage: '公告管理',
          sendreceive: '公告收发'
        },
        notification: {
          _self: '通知管理',
          manage: '通知管理',
          sendreceive: '通知收发'
        }
      },
      document: {
        _self: '文件管理',
        news: {
          _self: '新闻管理',
        },
        regulation: {
          _self: '规章制度',
          manage: '制度管理',
          control: '制度控制',
        },
        file: {
          _self: '日常文件',
        },
        iso: {
          _self: 'ISO文件',
          manage: '文件管理',
          control: '文件控制',
 
        },
        official: {
          _self: '公文管理',
          manage: '公文管理',
          issuance: '公文控制',

        },
        law: {
          _self: '法律法规',
        }
      },
      officesupplies: {
        _self: '办公用品',
        inventory: {
          _self: '库存管理',
          requisition: '请购管理',
          inbound: '入库管理',
          stocktaking: '盘点管理'
        },
        usage: {
          _self: '领用管理',
          apply: '领用申请',
          approve: '领用审批',
          list: '领用记录'
        }
      },
      book: {
        _self: '图书管理',
        inventory: {
          _self: '库存管理',
          requisition: '请购管理',
          inbound: '入库管理',
          list: '图书清单',
          stocktaking: '盘点管理'
        },
        usage: {
          _self: '领用管理',
          card: '借阅证',
          borrow: '借出',
          return: '归还'
        }
      },
      medical: {
        _self: '医务管理',
        medicine: {
          _self: '库存管理',
          requisition: '请购管理',
          inbound: '入库管理',
          list: '药品清单',
          stocktaking: '盘点管理'
        },
        usage: {
          _self: '领用管理',
          archive: '档案',
          receive: '领药',
          cost: '费用'
        }
      }
    },
    accounting: {
      _self: '会计核算',
      financial: {
        _self: '管理会计',
        company: '公司信息',
        account: '会计科目',
        companyaccount: '公司科目',
        ledger: '总账',
        payable: '应付',
        receivable: '应收',
        fixedasset: '固定资产',
        bank: '银行信息'
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
      budget: {
        _self: '全面预算',
        formulation: {
          _self: '预算编制',
          sales: {
            _self: '销售预算',
            cost: '销售成本',
            rolling: '销售滚动'
          },
          production: {
            _self: '生产预算',
            auxiliary: '生产辅料',
            labor: '生产人工',
            manufacturing: '生产制造'
          },
          cost: {
            _self: '成本预算',
            directmaterial: '直接材料',
            directlabor: '直接人工',
            indirectlabor: '间接人工',
            manufacturing: '制造费用'
          },
          expense: {
            _self: '费用预算',
            sales: '销售费用',
            manage: '管理费用',
            financial: '财务费用'
          },
          financial: {
            _self: '财务预算',
            cashflow: '现金流量',
            balancesheet: '资产负债表',
            income: '利润表'
          }
        },
        control: {
          _self: '预算控制',
          dashboard: '预算看板',
          approval: '预算审批'
        }
      }
    },
    logistics: {
      _self: '后勤管理',
      equipment: {
        _self: '设备管理',
        master: {
          _self: '设备数据',
          list: '设备信息',
          location: '功能位置',
          material: '物料关联'
        },
        maintenance: {
          _self: '设备维护',
          workorder: '维护计划',
          assign: '维护分配',
          execute: '维护执行'
        }
      },

      material: {
        _self: '物料管理',
        manage: {
          _self: '物料信息',
          master: '集团物料',
          plant: {
            _self: '工厂信息',
            master: '工厂物料'
          }
        },
        purchase: {
          _self: '采购管理',
          vendor: '卖方信息',
          supplier: '供应商信息',
          price: '采购价格',
          requisition: '采购请购',
          order: '采购订单'
        },
        sample:{
          _self: '样品管理',
          component: '料件样品',
          product: '产品样品'
        },
        drawing: {
          _self: '图纸管理',
          design: '图纸管理',
          engineering: '图纸控制',
          gerber: 'Gerber文件',
          coordinate: '坐标文件',
          assembly: '装配图纸',
          structure: '结构文件',
          impedance: '阻抗文件',
          process: '工艺流程'
        },
        csm: {  
          _self: '客供品管理',
          raw: '客供材料',
          good: '客供产品'
        }
      },
      production: {
        _self: '生产管理',
        basic: {
          _self: '基础数据',
          bom: '物料清单',
          workcenter: '工作中心',   
          routing: '工艺路线',
          order: '生产订单',
          worktime: '生产工时',
          kanban: '看板'
        },
        
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
            seizougijutsu: '制技课'
          }
        },       

        output: {
          _self: '制造管理',
          workshop1:{
            _self: '制一课',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: '生产',
              modify: '改修',
              rework: '返工'
            },
            defect:{
              _self: '不良',
              epp: 'EPP',
              production: '生产',
              modify: '改修',
              rework: '返工'
            },
            worktime: {
              _self: '工数',
              epp: 'EPP',
              production: '生产',
              modify: '改修',
              rework: '返工'
            }
          },
          workshop2:{
            _self: '制二课',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: '生产',
              modify: '改修',
              rework: '返工'
            },
            defect:{
              _self: '不良',
              eppInspection: 'EPP检查',
              eppRepair: 'EPP修理',
              productionInspection: '生产检查',
              productionRepair: '生产修理',
              modifyInspection: '改修检查',
              modifyRepair: '改修修理',
              reworkInspection: '返工检查',
              reworkRepair: '返工修理'
            },
            worktime: {
              _self: '工数',
              epp: 'EPP',
              production: '生产',
              modify: '改修',
              rework: '返工'
            }
          }
        },
        sop: {
          _self: 'SOP管理',
          workshop1: '制一课',
          workshop2: '制二课'
        },
        techcontact: {
          _self: '技术联络',
          epp: 'EPP联络',
          engineering: '工程联络',
          external: '外部联络'
        }
      },
      project: {
        _self: '项目管理',
        define: '项目定义',
        cost: '成本计划',
        resource: '资源计划',
        schedule: '进度计划'
      },
      quality: {
        _self: '质量管理',
        basic: {
          _self: '基础数据',
          item: '检验项目',
          method: '检验方法',
          sampling: '抽样方案',
          defect: '缺陷分类',
          rule: '判定规则',
          category: '品管类别'
        },
        inspection:{
          _self: '检验管理',
          receiving: '来料检验',
          process: '过程检验',
          storage: '入库检验',
          return: '退货检验'
        },
        trace:{
          _self: '追溯管理',
          batch: '批次追溯',
          corrective: '纠正措施',
          notification: '通知单',

        },
        cost:{
          _self: '质量成本',
          business:'品质业务',
          rework:'返工业务',
          scrap:'报废业务',
        },
        plan: {
          _self: '质量计划',
          supplier: '供应商评估',
          customer: '客户调查'
        },
        item: '检验项目',
        receiving: '来料检验',
        process: '过程检验',
        storage: '入库检验',
        return: '退货检验'
      },
      sales: {
        _self: '销售管理',
        customer: '顾客信息',
        client: '客户信息',
        price: '销售价格',
        order: '销售订单'
      },
      service: {
        _self: '客户服务',
        cs: {
          _self: '客户服务',
          item: '服务项目',
          contract: '服务合同',
          request: '服务请求',
          workorder: '服务工单',
          timesheet: '服务工时',
          consumption: '物料消耗',
          outsourcing: '外包服务'
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
      }
    },
    human: {
      _self: '人力资源',
      attendance: {
        _self: '考勤管理',
        record: '考勤记录',
        holiday: '假期管理',
        overtime: '加班管理',
        compensatory: '调休管理'
      },
      benefit: {
        _self: '福利管理',
        project: '福利项目',
        employee: '员工福利'
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
        transferhistory: '调岗历史'
      },
      leave: {
        _self: '请假管理',
        type: '请假类型',
        employee: '员工请假'
      },
      organization: {
        _self: '组织管理',
        positioncategory: '职位类别',
        company: '公司信息',
        department: '部门信息',
        position: '岗位信息'
      },
      performance: {
        _self: '绩效管理',
        assessmentitem: '考核项目',
        assessment: '绩效考核'
      },
      recruitment: {
        _self: '招聘管理',
        application: '职位申请',
        posting: '职位发布',
        candidate: '候选人管理',
        interview: '面试管理'
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
        socialbase: '社保基数'
      },
      training: {
        _self: '培训管理',
        category: '培训类别',
        course: '培训课程',
        record: '员工培训'
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
        recruitment: '招聘报表'
      }
    },
    advertisement:{
      _self: '广告管理',
      banner: '横幅广告',
      corner: '角落广告',
      floating: '浮动广告',
      interstitial: '插屏广告',
      overlay: '覆盖广告',
      popup:'弹窗广告',
      splash:'开屏广告',
      billing:'计费管理'
    }
  }
}
