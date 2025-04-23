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
    admin: {
      _self: '系统管理',
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
      oauth: 'OAuth管理'
    },
    audit: {
      _self: '审计日志',
      operlog: '操作日志',
      loginlog: '登录日志',
      dbdifflog: '差异日志',
      exceptionlog: '异常日志',
      auditlog: '审计日志',
      quartzlog: '任务日志'
    },
    workflow: {
      _self: '工作流程',
      definition: '流程定义',
      instance: '流程实例',
      task: '工作任务',
      node: '流程节点',
      variable: '流程变量',
      history: '流程历史'
    },
    signalr: {
      _self: '实时监控',
      server: '服务监控',
      online: '在线用户',
      message: '在线消息'
    },
    generator: {
      _self: '代码生成',
      table: '数据库表',
      tableDefine: '自定义表',
      template: '代码模板',
      config: '生成配置',
      api: '接口文档'
    },
    routine: {
      _self: '日常办公',
      file: '文件管理',
      mail: '邮件管理',
      mailTmpl: '邮件模板',
      notice: '通知公告',
      task: '工作任务',
      schedule: '日程管理'
    },
    finance: {
      _self: '核算',
      management: {
        _self: '管理会计',
        cost: {
          _self: '成本管理',
          costFactors: '成本要素',
          costCenter: '成本中心',
          profitCenter: '利润中心',
          productCost: '产品成本',
          activityType: '活动类型',
          internalOrder: '内部订单'
        },
        planning: {
          _self: '计划管理',
          costPlanning: '成本计划',
          profitPlanning: '利润计划',
          budgetControl: '预算控制'
        },
        reporting: {
          _self: '报表与分析',
          costReports: '成本报表',
          profitReports: '利润报表',
          varianceAnalysis: '差异分析'
        }
      },
      financial: {
        _self: '财务会计',
        generalLedger: {
          _self: '总账',
          account: '科目',
          accountType: '科目类型',
          journalEntry: '凭证录入',
          reconciliation: '对账',
          closing: '期末结账'
        },
        accountsReceivable: {
          _self: '应收账款',
          customer: '客户管理',
          invoice: '客户发票',
          payment: '客户收款',
          creditControl: '信用控制'
        },
        accountsPayable: {
          _self: '应付账款',
          supplier: '供应商管理',
          invoice: '供应商发票',
          payment: '供应商付款',
          agingReport: '账龄分析'
        },
        assetAccounting: {
          _self: '资产会计',
          assets: '固定资产',
          depreciation: '折旧管理',
          assetTransfer: '资产转移',
          assetRetirement: '资产报废'
        },
        tax: {
          _self: '税务管理',
          taxCodes: '税码管理',
          taxReporting: '税务报表',
          taxPayments: '税款支付'
        },
        financialReporting: {
          _self: '财务报表',
          balanceSheet: '资产负债表',
          profitAndLoss: '利润表',
          cashFlow: '现金流量表'
        }
      }
    },
    logistics: {
      _self: '后勤',
      sales: {
        _self: '销售管理',
        customer: {
          _self: '客户管理',
          client: '顾客',
          customers: '客户列表',
          creditControl: '信用管理'
        },
        order: {
          _self: '订单管理',
          order: '销售订单',
          orderDetail: '订单明细',
          orderTracking: '订单跟踪'
        },
        delivery: {
          _self: '交货管理',
          delivery: '交货单',
          deliveryDetail: '交货明细',
          shipping: '运输管理'
        },
        billing: {
          _self: '开票管理',
          invoice: '发票管理',
          invoiceDetail: '发票明细',
          payment: '收款管理'
        },
        reporting: {
          _self: '报表与分析',
          salesReports: '销售报表',
          performanceAnalysis: '绩效分析'
        }
      },
      production: {
        _self: '生产管理',
        bom: '物料清单 (BOM)',
        routing: '工艺路线',
        workOrder: {
          _self: '生产订单',
          create: '创建生产订单',
          manage: '管理生产订单',
          release: '下达生产订单',
          complete: '完成生产订单'
        },
        capacityPlanning: {
          _self: '产能计划',
          workCenter: '工作中心',
          capacityEvaluation: '产能评估',
          capacityLeveling: '产能平衡'
        },
        productionScheduling: {
          _self: '生产排程',
          schedule: '排程计划',
          reschedule: '重新排程'
        },
        productionExecution: {
          _self: '生产执行',
          confirm: '生产确认',
          goodsIssue: '生产领料',
          goodsReceipt: '生产入库'
        },
        productionReporting: {
          _self: '生产报表',
          orderReports: '订单报表',
          capacityReports: '产能报表',
          efficiencyReports: '效率报表'
        },
        qualityManagement: {
          _self: '质量管理',
          inspectionLot: '检验批',
          resultsRecording: '结果记录',
          defectRecording: '缺陷记录'
        }
      },
      material: {
        _self: '物料管理',
        materialMaster: '物料主数据',
        materialCategory: '物料类别',
        materialUnit: '物料单位',
        materialStock: {
          _self: '物料库存',
          stockOverview: '库存概览',
          stockIn: '物料入库',
          stockOut: '物料出库',
          stockTransfer: '库存转移',
          stockAdjustment: '库存调整',
          stockCheck: '库存盘点'
        },
        purchase: {
          _self: '采购管理',
          purchaseRequisition: '采购申请',
          purchaseOrder: '采购订单',
          purchaseOrderDetail: '采购订单明细',
          supplier: '供应商管理'
        },
        inventoryManagement: {
          _self: '库存管理',
          goodsReceipt: '收货',
          goodsIssue: '发货',
          transferPosting: '转储过账',
          stockOverview: '库存概览'
        },
        valuation: {
          _self: '物料估价',
          priceControl: '价格控制',
          standardPrice: '标准价格',
          movingAveragePrice: '移动平均价格'
        },
        reporting: {
          _self: '报表与分析',
          stockReports: '库存报表',
          purchaseReports: '采购报表',
          inventoryReports: '库存分析报表'
        }
      }
    },
    quality: {
      _self: '质量管理',
      inspection: {
        _self: '检验管理',
        inspectionLot: '检验批',
        resultsRecording: '结果记录',
        defectRecording: '缺陷记录',
        usageDecision: '使用决策'
      },
      qualityPlanning: {
        _self: '质量计划',
        inspectionPlan: '检验计划',
        qualityInfoRecord: '质量信息记录',
        samplingProcedure: '抽样程序'
      },
      qualityControl: {
        _self: '质量控制',
        controlChart: '控制图',
        qualityNotifications: '质量通知',
        correctiveActions: '纠正措施'
      },
      qualityReporting: {
        _self: '质量报表',
        inspectionReports: '检验报表',
        defectReports: '缺陷报表',
        qualityAnalysis: '质量分析'
      }
    },
    service: {
      _self: '客户服务',
      serviceOrder: {
        _self: '服务订单',
        create: '创建服务订单',
        manage: '管理服务订单',
        complete: '完成服务订单',
        cancel: '取消服务订单'
      },
      serviceContract: {
        _self: '服务合同',
        create: '创建服务合同',
        manage: '管理服务合同',
        renew: '续签服务合同',
        terminate: '终止服务合同'
      },
      customerInteraction: {
        _self: '客户互动',
        inquiries: '客户咨询',
        complaints: '客户投诉',
        feedback: '客户反馈'
      },
      serviceExecution: {
        _self: '服务执行',
        schedule: '服务计划',
        dispatch: '服务派工',
        execution: '服务执行',
        confirmation: '服务确认'
      },
      serviceReporting: {
        _self: '服务报表',
        orderReports: '服务订单报表',
        contractReports: '服务合同报表',
        performanceReports: '服务绩效报表'
      }
    },
    equipment: {
      _self: '设备管理',
      equipmentMaster: '设备主数据',
      maintenancePlanning: {
        _self: '维护计划',
        preventiveMaintenance: '预防性维护',
        maintenanceTaskList: '维护任务清单',
        scheduling: '维护排程'
      },
      maintenanceExecution: {
        _self: '维护执行',
        workOrder: '维护工单',
        confirmation: '维护确认',
        breakdownMaintenance: '故障维护'
      },
      maintenanceReporting: {
        _self: '维护报表',
        equipmentReports: '设备报表',
        maintenanceHistory: '维护历史',
        performanceAnalysis: '性能分析'
      },
      sparePartsManagement: {
        _self: '备件管理',
        sparePartsInventory: '备件库存',
        sparePartsProcurement: '备件采购',
        sparePartsUsage: '备件使用'
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
