import { countReset } from "node:console";

export default {
  menu: {
    home: '首頁',
    dashboard: {
      title: '儀表板',
      workplace: '工作台',
      analysis: '分析台',
      monitor: '監控台'
    },
    components: {
      title: '組件',
      icons: '圖標'
    },
    about: {
      title: '關於我們',
      privacy: '隱私政策',
      terms: '服務條款',
      index: '關於Hbt'
    },
    core: {
      _self: '系統管理',
      config: '系統配置',
      language: '語言管理',
      dict: '字典管理'
    },
    identity: {
      _self: '身份認證',
      user: '用戶管理',
      role: '角色管理',
      dept: '部門管理',
      post: '崗位管理',
      menu: '菜單管理',
      tenant: '租戶管理',
      oauth: 'OAuth管理'
    },
    audit: {
      _self: '審計日誌',
      operlog: '操作日誌',
      loginlog: '登錄日誌',
      dbdifflog: '差異日誌',
      exceptionlog: '異常日誌',
      auditlog: '審計日誌',
      quartzlog: '任務日誌'
    },
    workflow: {
      _self: '工作流程',
      definition: '流程定義',
      instance: '流程實例',
      task: '工作任務',
      node: '流程節點',
      variable: '流程變量',
      history: '流程歷史'
    },
    signalr: {
      _self: '實時監控',
      server: '服務監控',
      online: '在線用戶',
      message: '在線消息'
    },
    generator: {
      _self: '代碼生成',
      table: '數據庫表',
      tableDefine: '自定義表',
      template: '代碼模板',
      config: '生成配置',
      api: '接口文檔'
    },
    routine: {
      _self: '日常辦公',
      vehicle: {
        _self: '用車管理',
        vehicleMaster: {
          _self: '車輛主數據',
          vehicleInfo: '車輛信息',
          driverInfo: '駕駛員信息',
          maintenance: '車輛維護'
        },
        vehicleBooking: {
          _self: '用車申請',
          newBooking: '新建申請',
          bookingList: '申請列表',
          bookingApproval: '申請審批'
        },
        vehicleDispatch: {
          _self: '車輛調度',
          dispatchPlan: '調度計劃',
          realTimeTracking: '實時跟蹤',
          dispatchHistory: '調度歷史'
        },
        vehicleReporting: {
          _self: '用車報表',
          usageReport: '使用報表',
          costReport: '費用報表',
          maintenanceReport: '維護報表'
        }
      },
      file: '文件管理',
      mail: '郵件管理',
      mailTmpl: '郵件模板',
      meeting: {
        _self: '會議管理',
        meetingRoom: {
          _self: '會議室管理',
          roomInfo: '會議室信息',
          roomBooking: '會議室預訂',
          roomSchedule: '會議室日程'
        },
        meetingPlan: {
          _self: '會議計劃',
          newMeeting: '新建會議',
          meetingList: '會議列表',
          meetingApproval: '會議審批'
        },
        meetingExecution: {
          _self: '會議執行',
          attendance: '會議簽到',
          minutes: '會議紀要',
          followUp: '會議跟進'
        },
        meetingReporting: {
          _self: '會議報表',
          meetingReport: '會議報表',
          attendanceReport: '出席報表',
          costReport: '費用報表'
        }
      },
      notice: '通知公告',
      schedule: '日程管理',
      quartz: '工作任務'
    },
    finance: {
      _self: '核算',
      management: {
        _self: '管理會計',
        cost: {
          _self: '成本管理',
          costFactors: '成本要素',
          costCenter: '成本中心',
          profitCenter: '利潤中心',
          productCost: '產品成本',
          activityType: '活動類型',
          internalOrder: '內部訂單'
        },
        planning: {
          _self: '計劃管理',
          costPlanning: '成本計劃',
          profitPlanning: '利潤計劃',
          budgetControl: '預算控制'
        },
        reporting: {
          _self: '報表與分析',
          costReports: '成本報表',
          profitReports: '利潤報表',
          varianceAnalysis: '差異分析'
        }
      },
      financial: {
        _self: '財務會計',
        generalLedger: {
          _self: '總賬',
          account: '科目',
          accountType: '科目類型',
          journalEntry: '憑證錄入',
          reconciliation: '對賬',
          closing: '期末結賬'
        },
        accountsReceivable: {
          _self: '應收賬款',
          customer: '客戶管理',
          invoice: '客戶發票',
          payment: '客戶收款',
          creditControl: '信用控制'
        },
        accountsPayable: {
          _self: '應付賬款',
          supplier: '供應商管理',
          invoice: '供應商發票',
          payment: '供應商付款',
          agingReport: '賬齡分析'
        },
        assetAccounting: {
          _self: '資產會計',
          assets: '固定資產',
          depreciation: '折舊管理',
          assetTransfer: '資產轉移',
          assetRetirement: '資產報廢'
        },
        tax: {
          _self: '稅務管理',
          taxCodes: '稅碼管理',
          taxReporting: '稅務報表',
          taxPayments: '稅款支付'
        },
        financialReporting: {
          _self: '財務報表',
          balanceSheet: '資產負債表',
          profitAndLoss: '利潤表',
          cashFlow: '現金流量表'
        }
      }
    },
    logistics: {
      _self: '後勤',
      equipment: {
        _self: '設備管理',
        equipmentMaster: '設備主數據',
        maintenancePlanning: {
          _self: '維護計劃',
          preventiveMaintenance: '預防性維護',
          maintenanceTaskList: '維護任務清單',
          scheduling: '維護排程'
        },
        maintenanceExecution: {
          _self: '維護執行',
          workOrder: '維護工單',
          confirmation: '維護確認',
          breakdownMaintenance: '故障維護'
        },
        maintenanceReporting: {
          _self: '維護報表',
          equipmentReports: '設備報表',
          maintenanceHistory: '維護歷史',
          performanceAnalysis: '性能分析'
        },
        sparePartsManagement: {
          _self: '備件管理',
          sparePartsInventory: '備件庫存',
          sparePartsProcurement: '備件採購',
          sparePartsUsage: '備件使用'
        }
      },
      material: {
        _self: '物料管理',
        materialMaster: '物料主數據',
        materialCategory: '物料類別',
        materialUnit: '物料單位',
        materialStock: {
          _self: '物料庫存',
          stockOverview: '庫存概覽',
          stockIn: '物料入庫',
          stockOut: '物料出庫',
          stockTransfer: '庫存轉移',
          stockAdjustment: '庫存調整',
          stockCheck: '庫存盤點'
        },
        purchase: {
          _self: '採購管理',
          purchaseRequisition: '採購申請',
          purchaseOrder: '採購訂單',
          purchaseOrderDetail: '採購訂單明細',
          supplier: '供應商管理'
        },
        inventoryManagement: {
          _self: '庫存管理',
          goodsReceipt: '收貨',
          goodsIssue: '發貨',
          transferPosting: '轉移過賬',
          stockOverview: '庫存概覽'
        },
        valuation: {
          _self: '評估',
          priceControl: '價格控制',
          standardPrice: '標準價格',
          movingAveragePrice: '移動平均價格'
        },
        reporting: {
          _self: '報表分析',
          stockReports: '庫存報表',
          purchaseReports: '採購報表',
          inventoryReports: '庫存報表'
        }
      },
      production: {
        _self: '生產管理',
        bom: '物料清單',
        routing: '工藝路線',
        workOrder: {
          _self: '生產訂單',
          create: '創建訂單',
          manage: '管理訂單',
          release: '發放訂單',
          complete: '完成訂單'
        },
        capacityPlanning: {
          _self: '產能計劃',
          workCenter: '工作中心',
          capacityEvaluation: '產能評估',
          capacityLeveling: '產能平衡'
        },
        productionScheduling: {
          _self: '生產排程',
          schedule: '排程',
          reschedule: '重新排程'
        },
        productionExecution: {
          _self: '生產執行',
          confirm: '生產確認',
          goodsIssue: '物料發放',
          goodsReceipt: '產品入庫'
        },
        productionReporting: {
          _self: '生產報表',
          orderReports: '訂單報表',
          capacityReports: '產能報表',
          efficiencyReports: '效率報表'
        },
        qualityManagement: {
          _self: '質量管理',
          inspectionLot: '檢驗批次',
          resultsRecording: '結果記錄',
          defectRecording: '缺陷記錄'
        }
      },
      project: {
        _self: '項目管理',
        projectMaster: {
          _self: '項目主數據',
          projectDefinition: '項目定義',
          projectStructure: '項目結構',
          projectTeam: '項目團隊',
          projectCalendar: '項目日曆'
        },
        projectPlanning: {
          _self: '項目計劃',
          workBreakdown: '工作分解',
          scheduling: '進度計劃',
          resourcePlanning: '資源計劃',
          costPlanning: '成本計劃'
        },
        projectExecution: {
          _self: '項目執行',
          taskManagement: '任務管理',
          progressTracking: '進度跟踪',
          resourceManagement: '資源管理',
          costControl: '成本控制'
        },
        projectMonitoring: {
          _self: '項目監控',
          progressReports: '進度報表',
          resourceReports: '資源報表',
          costReports: '成本報表',
          riskManagement: '風險管理'
        },
        projectClosure: {
          _self: '項目收尾',
          finalReport: '最終報表',
          lessonsLearned: '經驗教訓',
          projectArchive: '項目歸檔'
        }
      },
      quality: {
        _self: '質量管理',
        inspection: {
          _self: '檢驗管理',
          inspectionLot: '檢驗批次',
          resultsRecording: '結果記錄',
          defectRecording: '缺陷記錄',
          usageDecision: '使用決策'
        },
        qualityPlanning: {
          _self: '質量計劃',
          inspectionPlan: '檢驗計劃',
          qualityInfoRecord: '質量信息記錄',
          samplingProcedure: '抽樣程序'
        },
        qualityControl: {
          _self: '質量控制',
          controlChart: '控制圖',
          qualityNotifications: '質量通知',
          correctiveActions: '糾正措施'
        },
        qualityReporting: {
          _self: '質量報表',
          inspectionReports: '檢驗報表',
          defectReports: '缺陷報表',
          qualityAnalysis: '質量分析'
        }
      },
      sales: {
        _self: '銷售',
        customer: {
          _self: '客戶管理',
          client: '客戶',
          customers: '客戶列表',
          creditControl: '信用控制'
        },
        order: {
          _self: '訂單管理',
          order: '銷售訂單',
          orderDetail: '訂單明細',
          orderTracking: '訂單跟踪'
        },
        delivery: {
          _self: '發貨管理',
          delivery: '發貨',
          deliveryDetail: '發貨明細',
          shipping: '運輸管理'
        },
        billing: {
          _self: '開票',
          invoice: '發票管理',
          invoiceDetail: '發票明細',
          payment: '付款管理'
        },
        reporting: {
          _self: '報表分析',
          salesReports: '銷售報表',
          performanceAnalysis: '績效分析'
        }
      },
      service: {
        _self: '服務',
        serviceOrder: {
          _self: '服務訂單',
          create: '創建訂單',
          manage: '管理訂單',
          complete: '完成訂單',
          cancel: '取消訂單'
        },
        serviceContract: {
          _self: '服務合同',
          create: '創建合同',
          manage: '管理合同',
          renew: '續簽合同',
          terminate: '終止合同'
        },
        customerInteraction: {
          _self: '客戶互動',
          inquiries: '客戶諮詢',
          complaints: '客戶投訴',
          feedback: '客戶反饋'
        },
        serviceExecution: {
          _self: '服務執行',
          schedule: '排程',
          dispatch: '派工',
          execution: '執行',
          confirmation: '確認'
        },
        serviceReporting: {
          _self: '服務報表',
          orderReports: '訂單報表',
          contractReports: '合同報表',
          performanceReports: '績效報表'
        }
      }
    },
    humanResources: {
      _self: '人力資源',
      employee: {
        _self: '員工管理',
        employeeInfo: '員工信息',
        employeeProfile: '員工檔案',
        employeeContract: '員工合同',
        employeeAttendance: '員工考勤',
        employeeLeave: '員工休假',
        employeePerformance: '員工績效'
      },
      recruitment: {
        _self: '招聘管理',
        jobPosting: '職位發布',
        candidate: '候選人管理',
        interview: '面試管理',
        offer: '錄用管理',
        onboarding: '入職管理'
      },
      training: {
        _self: '培訓管理',
        trainingPlan: '培訓計劃',
        trainingCourse: '培訓課程',
        trainingRecord: '培訓記錄',
        trainingEvaluation: '培訓評估'
      },
      performance: {
        _self: '績效管理',
        performancePlan: '績效計劃',
        performanceAppraisal: '績效考核',
        performanceReview: '績效評審',
        performanceImprovement: '績效改進'
      },
      compensation: {
        _self: '薪酬管理',
        salary: '薪資管理',
        bonus: '獎金管理',
        benefits: '福利管理',
        payroll: '工資單'
      },
      reporting: {
        _self: '報表分析',
        employeeReports: '員工報表',
        recruitmentReports: '招聘報表',
        trainingReports: '培訓報表',
        performanceReports: '績效報表',
        compensationReports: '薪酬報表'
      }
    }
  }
}
