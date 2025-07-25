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
      engine: {
        _self: '流程引擎',
        monitor: '流程監控',
        todo: '待辦任務',
        done: '已辦任務',
        signoff: '流程簽核',
        execution: '流程執行',
        designer: '流程設計器'
      },
      manage: {
        _self: '流程管理',
        form: '表單管理',
        scheme: '流程方案',
        instance: '流程實例',
        oper: '實例操作',
        trans: '實例流轉'
      }
    },
    signalr: {
      _self: '即時通訊',
      online: '線上用戶',
      message: '線上消息'
    },
    generator: {
      _self: '代碼生成',
      table: '資料庫表',
      tableDefine: '表欄定義',
      template: '代碼模板',
      config: '生成配置',
      api: '接口文件'
    },
    routine: {
      _self: '日常辦公',
      core: {
        _self: '基礎服務',
        numberrule: '編碼規則',
        config: '系統配置',
        language: '語言管理',
        dict: '字典管理'
      },
      contract: {
        _self: '合同管理',
        template: {
          _self: '合同模板',
          manage: '模板管理',
          category: '模板分類'
        },
        draft: {
          _self: '合同起草',
          apply: '起草申請',
          my: '我的起草'
        },
        approval: {
          _self: '合同審批',
          pending: '合同審批',
          approved: '已審批',
          record: '審批記錄'
        },
        execution: {
          _self: '合同執行',
          track: '執行跟蹤',
          change: '變更管理',
          payment: '付款管理'
        },
        archive: {
          _self: '合同歸檔',
          manage: '歸檔管理',
          query: '查詢統計'
        }
      },
      project: {
        _self: '項目管理',
        info: {
          _self: '項目信息',
          list: '項目列表'
        },
        plan: {
          _self: '項目計劃',
          request: '計劃請求',
          gantt: '項目甘特圖'
        },
        task: {
          _self: '項目任務',
          assign: '任務分配',
          track: '任務跟蹤',
          board: '任務看板'
        },
        resource: {
          _self: '項目資源',
          personnel: '人員管理',
          equipment: '設備管理',
          budget: '預算管理'
        },
        monitor: {
          _self: '項目監控',
          progress: '進度監控',
          quality: '質量監控',
          risk: '風險監控'
        }
      },
      quartz: {
        _self: '任務調度',
        job: {
          _self: '任務管理',
          config: '任務配置',
          list: '任務列表',
          status: '任務狀態'
        },
        schedule: {
          _self: '任務調度',
          config: '調度配置',
          monitor: '調度監控',
          stats: '調度統計'
        }
      },
      schedule: {
        _self: '日程管理',
        myschedule: '我的日程',
        dashboard: '日程看板'
      },
      vehicle: {
        _self: '用車管理',
        my: '我的用車',
        application: '用車申請',
        dashboard: '用車看板',
        manage: {
          _self: '車管管理',
          info: '車輛信息',
          maintenance: '車輛保養'
        }
      },
      email: {
        _self: '郵件管理',
        inbox: '收件箱',
        drafts: '草稿箱',
        sent: '已發送',
        trash: '垃圾箱',
        template: '郵件模板'
      },
      meeting: {
        _self: '會議管理',
        room: '會議室',
        mymeeting: '我的會議',
        booking: '會議預約',
        dashboard: '會議看板'
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
          signoff: '簽收公告',
          list: '公告列表'
        },
        notification: {
          _self: '通知管理',
          ack: '已讀通知',
          list: '通知列表'
        }
      },
      hr: {
        _self: '人事考勤',
        recruitment: {
          _self: '招聘管理',
          apply: '招聘申請',
          approval: '招聘審批',
          list: '招聘列表'
        },
        transfer: {
          _self: '轉崗管理',
          apply: '轉崗申請',
          approval: '轉崗審批',
          list: '轉崗列表'
        },
        leave: {
          _self: '請假管理',
          apply: '請假申請',
          approval: '請假審批',
          list: '請假列表'
        },
        trip: {
          _self: '出差管理',
          apply: '出差申請',
          approval: '出差審批',
          list: '出差列表'
        },
        overtime: {
          _self: '加班管理',
          apply: '加班申請',
          approval: '加班審批',
          list: '加班列表'
        }
      },
      expense: {
        _self: '費用管理',
        daily: {
          _self: '日常費用',
          apply: '費用申請',
          approve: '費用審批',
          list: '費用列表'
        },
        travel: {
          _self: '差旅費用',
          apply: '差旅費申請',
          approve: '差旅費審批',
          list: '差旅費列表'
        }
      },
      document: {
        _self: '文件管理',
        news: {
          _self: '新聞管理'
        },
        regulation: {
          _self: '規章制度',
          manage: '制度管理',
          control: '制度控制'
        },
        file: {
          _self: '日常文件'
        },
        iso: {
          _self: 'ISO文件',
          manage: '文件管理',
          control: '文件控制'
        },
        official: {
          _self: '公文管理',
          manage: '公文管理',
          issuance: '公文控制'
        },
        law: {
          _self: '法律法規'
        }
      },
      officesupplies: {
        _self: '辦公用品',
        inventory: {
          _self: '庫存管理',
          requisition: '請購管理',
          inbound: '入庫管理',
          stocktaking: '盤點管理'
        },
        usage: {
          _self: '領用管理',
          apply: '領用申請',
          approve: '領用審批',
          list: '領用記錄'
        }
      },
      book: {
        _self: '圖書管理',
        inventory: {
          _self: '庫存管理',
          requisition: '請購管理',
          inbound: '入庫管理',
          list: '圖書清單',
          stocktaking: '盤點管理'
        },
        usage: {
          _self: '領用管理',
          card: '借閱證',
          borrow: '借出',
          return: '歸還'
        }
      },
      medical: {
        _self: '醫務管理',
        medicine: {
          _self: '庫存管理',
          requisition: '請購管理',
          inbound: '入庫管理',
          list: '藥品清單',
          stocktaking: '盤點管理'
        },
        usage: {
          _self: '領用管理',
          archive: '檔案',
          receive: '領藥',
          cost: '費用'
        }
      }
    },
    accounting: {
      _self: '會計核算',
      financial: {
        _self: '管理會計',
        company: '公司信息',
        account: '會計科目',
        companyaccount: '公司科目',
        ledger: '總賬',
        payable: '應付',
        receivable: '應收',
        fixedasset: '固定資產',
        bank: '銀行信息'
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
      budget: {
        _self: '全面預算',
        formulation: {
          _self: '預算編制',
          sales: {
            _self: '銷售預算',
            cost: '銷售成本',
            rolling: '銷售滾動'
          },
          production: {
            _self: '生產預算',
            auxiliary: '生產輔料',
            labor: '生產人工',
            manufacturing: '生產製造'
          },
          cost: {
            _self: '成本預算',
            directmaterial: '直接材料',
            directlabor: '直接人工',
            indirectlabor: '間接人工',
            manufacturing: '製造費用'
          },
          expense: {
            _self: '費用預算',
            sales: '銷售費用',
            manage: '管理費用',
            financial: '財務費用'
          },
          financial: {
            _self: '財務預算',
            cashflow: '現金流量',
            balancesheet: '資產負債表',
            income: '利潤表'
          }
        },
        control: {
          _self: '預算控制',
          dashboard: '預算看板',
          approval: '預算審批'
        }
      }
    },
    logistics: {
      _self: '後勤管理',
      equipment: {
        _self: '設備管理',
        master: {
          _self: '設備數據',
          list: '設備信息',
          location: '功能位置',
          material: '物料關聯'
        },
        maintenance: {
          _self: '設備維護',
          workorder: '維護計劃',
          assign: '維護分配',
          execute: '維護執行'
        }
      },
      material: {
        _self: '物料管理',
        manage: {
          _self: '物料信息',
          master: '集團物料',
          plant: {
            _self: '工廠信息',
            master: '工廠物料'
          }
        },
        purchase: {
          _self: '採購管理',
          vendor: '賣方信息',
          supplier: '供應商信息',
          price: '採購價格',
          requisition: '採購請購',
          order: '採購訂單'
        },
        sample: {
          _self: '樣品管理',
          component: '料件樣品',
          product: '產品樣品'
        },
        drawing: {
          _self: '圖紙管理',
          design: '圖紙管理',
          engineering: '圖紙控制',
          gerber: 'Gerber文件',
          coordinate: '座標文件',
          assembly: '裝配圖紙',
          structure: '結構文件',
          impedance: '阻抗文件',
          process: '工藝流程'
        },
        csm: {
          _self: '客供品管理',
          raw: '客供材料',
          good: '客供產品'
        }
      },
      production: {
        _self: '生產管理',
        basic: {
          _self: '基礎數據',
          bom: '物料清單',
          workcenter: '工作中心',
          routing: '工藝路線',
          order: '生產訂單',
          worktime: '生產工時',
          kanban: '看板'
        },
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
            seizougijutsu: '制技課'
          }
        },
        output: {
          _self: '製造管理',
          workshop1: {
            _self: '制一課',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: '生產',
              defect: '不良',
              worktime: '工時',
              productionReport: '生產報表',
              defectSummary: '不良彙總',
              worktimeReport: '工時報表'
            }
          },
          workshop2: {
            _self: '制二課',
            output: '生產日報',
            inspection: '檢查記錄',
            repair: '修理記錄',
            worktime: '生產工時',
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
        schedule: '進度計劃'
      },
      quality: {
        _self: '質量管理',
        item: '檢驗項目',
        receiving: '來料檢驗',
        process: '過程檢驗',
        storage: '入庫檢驗',
        return: '退貨檢驗'
      },
      sales: {
        _self: '銷售管理',
        customer: '客戶信息',
        client: '客戶信息',
        price: '銷售價格',
        order: '銷售訂單'
      },
      service: {
        _self: '客戶服務',
        item: '服務項目',
        contract: '服務合同',
        request: '服務請求',
        workorder: '服務工單',
        timesheet: '工時記錄',
        consumption: '物料消耗',
        outsourcing: '外包服務'
      },
      cc: {
        _self: '客訴管理',
        notice: '質量通知單',
        mark: '客訴明細',
        analysis: '原因分析',
        corrective: '糾正措施',
        return: '退換貨執行',
        followUp: '跟進處理'
      }
    },
    hrm: {
      _self: '人力資源',
      attendance: {
        _self: '考勤管理',
        record: '考勤記錄',
        holiday: '假期管理',
        overtime: '加班管理',
        compensatory: '調休管理'
      },
      benefit: {
        _self: '福利管理',
        project: '福利項目',
        employee: '員工福利'
      },
      employee: {
        _self: '人員管理',
        info: '人員信息',
        contracttype: '合同類型',
        contract: '合同管理',
        promotion: '晉升管理',
        promotionhistory: '晉升歷史',
        resignation: '離職管理',
        transfer: '人員列表',
        transferhistory: '調崗歷史'
      },
      leave: {
        _self: '請假管理',
        type: '請假類型',
        employee: '員工請假'
      },
      organization: {
        _self: '組織管理',
        positioncategory: '職位類別',
        company: '公司信息',
        department: '部門信息',
        position: '崗位信息'
      },
      performance: {
        _self: '績效管理',
        assessmentitem: '考核項目',
        assessment: '績效考核'
      },
      recruitment: {
        _self: '招聘管理',
        application: '職位申請',
        posting: '職位發布',
        candidate: '候選人管理',
        interview: '面試管理'
      },
      salary: {
        _self: '薪資管理',
        employee: '員工薪資',
        housing: '公積金',
        housinglevel: '社保',
        tax: '稅務管理',
        taxlevel: '個稅等級',
        structure: '薪資結構',
        social: '社保',
        socialbase: '社保基數'
      },
      training: {
        _self: '培訓管理',
        category: '培訓類別',
        course: '培訓課程',
        record: '員工培訓'
      },
      report: {
        _self: '報表管理',
        employeeinfo: '人員信息',
        resignation: '離職報表',
        transfer: '調崗報表',
        promotion: '晉升報表',
        training: '培訓報表',
        salary: '薪資報表',
        performance: '績效報表',
        attendance: '考勤報表',
        benefit: '福利報表',
        recruitment: '招聘報表'
      }
    }
  }
}
