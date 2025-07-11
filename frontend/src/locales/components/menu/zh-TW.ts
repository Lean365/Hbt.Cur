import { countReset } from "node:console";

export default {
  menu: {
    home: '首頁',
    dashboard: {
      title: '儀表盤',
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
      _self: '核心管理',
      config: '系統配置',
      language: '語言管理',
      dict: '字典管理',
    },
    identity: {
      _self: '身份認證',
      user: '用戶管理',
      role: '角色管理',
      dept: '部門管理',
      post: '崗位管理',
      menu: '菜單管理',
      tenant: '租戶管理',
      oauth: 'OAuth管理',
      profile: '個人信息',
      changePassword: '修改密碼'
    },
    audit: {
      _self: '審計日誌',
      operlog: '操作日誌',
      loginlog: '登錄日誌',
      sqldifflog: '差異日誌',
      exceptionlog: '異常日誌',
      auditlog: '審計日誌',
      quartzlog: '任務日誌',
      server: '服務監控'
    },
    workflow: {
      _self: '工作流程',
      overview: '流程總覽',
      my: '我的流程',
      form: '表單管理',
      definition: '流程定義',
      instance: '流程實例',
      task: '工作任務',
      node: '流程節點',
      variable: '流程變量',
      history: '流程歷史'
    },
    signalr: {
      _self: '實時通信',
      online: '在線用戶',
      message: '在線消息'
    },
    generator: {
      _self: '代碼生成',
      table: '數據庫表',
      tableDefine: '表列定義',
      template: '代碼模板',
      config: '生成配置',
      api: '接口文檔'
    },
    routine: {
      _self: '日常辦公',
      schedule: {
        _self: '日程管理',
        myschedule: '我的日程',
        dashboard: '日程看板',
      },
      car: {
        _self: '用車管理',
        info: '車輛信息',
        application: '用車申請',
        dashboard: '用車看板',
        maintenance: '車輛保養',
      },
      email: {
        _self: '郵件管理',
        inbox: '收件箱',
        drafts: '草稿箱',
        sent: '已發送',
        trash: '垃圾箱',
        template: '郵件模板',        
      },
      meeting: {
        _self: '會議管理',
        room: '會議室',
        mymeeting: '我的會議',
        booking: '會議預約',
        dashboard: '會議看板',
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
          signoff: '簽收公告',
          list: '公告列表',
        },
        notification: {
          _self: '通知管理',
          ack: '已讀通知',
          list: '通知列表',
        },
      },
      hr: {
        _self: '人事考勤',
        recruitment: {
          _self: '招聘管理',
          apply: '招聘申請',
          approval: '招聘審批',
          list: '招聘列表',

        },
        transfer: {
          _self: '轉崗管理',
          apply: '轉崗申請',
          approval: '轉崗審批',
          list: '轉崗列表',
        },
        leave: {
          _self: '請假管理',
          apply: '請假申請',
          approval: '請假審批',
          list: '請假列表',
        },
        trip: {
          _self: '出差管理',
          apply: '出差申請',
          approval: '出差審批',
          list: '出差列表',
        },
        overtime: {
          _self: '加班管理',
          apply: '加班申請',
          approval: '加班審批',
          list: '加班列表',
      },
    },
    expense:{
      _self: '費用管理',
      daily: {
        _self: '日常費用',
        apply: '費用申請',
        approve: '費用審批',
        list: '費用列表',
      },
      travel: {
        _self: '差旅費用',
        apply: '差旅費申請',
        approve: '差旅費審批',
        list: '差旅費列表',
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
        signoff: '簽收',
        list: 'ISO文件',
      },
      document: { 
        _self: '公文管理',
        version: '版本',
        signoff: '簽收',
        list: '公文列表',
      },
    },
    officesupplies:{
      _self: '辦公用品',
      inventory:{
        _self: '庫存管理',
        requisition: '請購管理',
        inbound: '入庫管理',
        stocktaking: '盤點管理',
      },
      usage:{
        _self: '領用管理',
        apply: '領用申請',
        approve: '領用審批',
        receive: '領用記錄',
      }
    },
    book:{
      _self: '圖書管理',
      inventory:{
        _self: '庫存管理',
        requisition: '請購管理',
        inbound: '入庫管理',
        list: '圖書清單',
        stocktaking: '盤點管理',
      },
      usage:{
        _self: '領用管理',
        card: '借閱證',
        borrow: '借出',
        return: '歸還',
      }

    },
    medical:{
      _self: '醫務管理',
      medicine:{
        _self: '庫存管理',
        requisition: '請購管理',
        inbound: '入庫管理',
        list: '藥品清單',
        stocktaking: '盤點管理',
      },
      usage:{
        _self: '領用管理',
        archive: '檔案',
        receive: '領藥',
        cost: '費用',
      }

    },
  },
  accounting: {
      _self: '會計核算',
      financial: {
        _self: '管理會計',
        company: "公司信息",
        account: '會計科目',
        companyaccount: '公司科目',
        ledger: '總賬',
        payable: '應付',
        receivable: '應收',
        fixedasset: '固定資產',
        bank: '銀行信息',

      },
      controlling: {
        _self: '控制會計',
        costelement: '成本要素',
        costcenter: '成本中心',
        profitcenter: '利潤中心',
        accountsReceivable: '應收賬款',
        accountsPayable: '應付賬款',
        assetAccounting: '資產會計',
        tax: '稅務管理',
        financialReporting: '財務報表'      
    },
    budget:{
      _self: '全面預算',
        formulation: {
          _self: '預算編制',
          sales: {
            _self: '銷售預算',
            cost: '銷售成本',
            rolling: '銷售滾動',
          },
          production: {
            _self: '生產預算',
            auxiliary: '生產輔料',
            labor: '生產人工',
            manufacturing: '生產製造',
          },
          cost: {
            _self: '成本預算',
            directmaterial: '直接材料',
            directlabor: '直接人工',
            indirectlabor: '間接人工',
            manufacturing: '製造費用',
          },
          expense: {
            _self: '費用預算',
            sales: '銷售費用',
            management: '管理費用',
            financial: '財務費用',
          },
          financial: {
            _self: '財務預算',
            cashflow: '現金流量',
            balancesheet: '資產負債表',
            income: '利潤表',
          },
        },
        control: {
          _self: '預算控制',
          dashboard: '預算看板',
          approval: '預算審批',
        },   
  },
},
    logistics: {
      _self: '後勤管理',
      equipment: {
        _self: '設備管理',
        data: '設備主數據',
        location: '設備位置',
        material: '物料關聯',
        workorder: '工單'

      },
      material: {
        _self: '物料管理',
        material:{
          _self: '物料管理',
          material: '物料主數據',
          plant: '工廠信息',
          master: '物料數據',
          plantmaster: '工廠物料',
          vendor: '賣方信息',
          supplier: '供應商信息',
        },
        purchase:{
          _self: '採購管理',
          vendor: '賣方信息',
          supplier: '供應商信息',
          price: '採購價格',
          requisition: '採購請購',
          order: '採購訂單',

        },



      },
      production: {
        _self: '生產管理',
        bom: '物料清單',
        change: {
          _self: '設計變更',
          implementation: '設變實施',
          techcontact: '技術聯絡',
          material: '物料確認',
          query: '設變查詢',
          oldproduct: '舊品管制',
          sop: 'SOP確認',
          batch: '投入批次',
          input: {
            _self: '設變錄入',
            gijutsu: '技術課',
            seikan: '生管課',
            koubai: '採購課',
            uketsuke: '受檢課',
            bukan: '部管課',
            seizou2: '制二課',
            seizou1: '制一課',
            hinkan: '品管課',
            seizougijutsu: '制技課',
  
          }
        },
        workcenter: '工作中心',
        order: '生產訂單',
        kanban: '看板',
        oph:{
          _self: 'OPH管理',
          workshop1: {
            _self: '制一課',
            output: '生產日報',
            defect: '生產不良',
            worktime: '生產工數',
            productionReport: '生產報表',
            defectSummary: '不良集計',
            worktimeReport: '工時報表'
          },
          workshop2: {
            _self: '制二課',
            output: '生產日報',
            inspection: '檢查記錄',
            repair: '修理記錄',
            worktime: '生產工數',
            productionReport: '生產報表',
            inspectionReport: '檢查報表',
            repairReport: '修理報表',
            worktimeReport: '工時報表'
          }
        }

      },
      project: {
        _self: '項目管理',
        define: '項目定義',
        cost: '成本計劃',
        resource: '資源計劃',
        schedule: '進度計劃',

      },
      quality: {
        _self: '質量管理',
        item: '檢驗項目',
        receiving: '來料檢驗',
        process: '過程檢驗',
        storage: '入庫檢驗',
        return: '退貨檢驗',
  
      },
      sales: {
        _self: '銷售管理',
        customer: '顧客信息',
        client: '客戶信息',
        price: '銷售價格',
        order: '銷售訂單',
      },
      service: {
        _self: '客戶服務',
        item: '服務項目',
        contract: '服務合同',
        request: '服務請求',
        workorder: '服務工單',
        timesheet: '工時記錄',
        consumption: '物料消耗',
        outsourcing: '外協服務'

      },
      complaint: {
        _self: '客訴管理',
        notice: '質量通知單',
        mark: '客訴明細',
        analysis: '原因分析',
        corrective: '糾正措施',
        return: '退換貨執行',
        followUp: '跟進處理'
      }
    },
    humanResources: {
      _self: '人力資源管理',
      employeeManagement: {
        _self: '員工管理',
        employeeMaster: '員工主數據',
        attendance: '考勤管理',
        leave: '請假管理',
        payroll: '薪資管理',
        contractManagement: '合同管理' // 新增合同管理
      },
      recruitment: {
        _self: '招聘管理',
        jobPosting: '職位發布',
        candidateManagement: '候選人管理',
        interviewScheduling: '面試安排',
        offerManagement: '錄用管理'
      },
      training: {
        _self: '培訓管理',
        trainingPlan: '培訓計劃',
        trainingExecution: '培訓執行',
        trainingEvaluation: '培訓評估'
      },
      performance: {
        _self: '績效管理',
        goalSetting: '目標設定',
        performanceReview: '績效評估',
        feedback: '反饋管理'
      },
      reporting: {
        _self: '人力資源報表',
        employeeReports: '員工報表',
        attendanceReports: '考勤報表',
        payrollReports: '薪資報表',
        performanceReports: '績效報表'
      }
    }
  }
}
