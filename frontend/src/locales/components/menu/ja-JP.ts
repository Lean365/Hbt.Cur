import { countReset } from "node:console";

export default {
  menu: {
    home: 'ホーム',
    dashboard: {
      title: 'ダッシュボード',
      workplace: 'ワークプレース',
      analysis: '分析台',
      monitor: '監視台'
    },
    components: {
      title: 'コンポーネント',
      icons: 'アイコン'
    },
    about: {
      title: '私たちについて',
      privacy: 'プライバシーポリシー',
      terms: '利用規約',
      index: 'Hbtについて'
    },
    core: {
      _self: 'コア管理',
      config: 'システム設定',
      language: '言語管理',
      dict: '辞書管理',
    },
    identity: {
      _self: '認証',
      user: 'ユーザー管理',
      role: 'ロール管理',
      dept: '部門管理',
      post: '職位管理',
      menu: 'メニュー管理',
      tenant: 'テナント管理',
      oauth: 'OAuth管理',
      profile: '個人情報',
      changePassword: 'パスワード変更'
    },
    audit: {
      _self: '監査ログ',
      operlog: '操作ログ',
      loginlog: 'ログインログ',
      sqldifflog: '差分ログ',
      exceptionlog: '例外ログ',
      auditlog: '監査ログ',
      quartzlog: 'タスクログ',
      server: 'サービス監視'
    },
    workflow: {
      _self: 'ワークフロー',
      overview: 'プロセス概要',
      my: 'マイプロセス',
      form: 'フォーム管理',
      definition: 'プロセス定義',
      instance: 'プロセスインスタンス',
      task: 'ワークタスク',
      node: 'プロセスノード',
      variable: 'プロセス変数',
      history: 'プロセス履歴'
    },
    signalr: {
      _self: 'リアルタイム通信',
      online: 'オンラインユーザー',
      message: 'オンラインメッセージ'
    },
    generator: {
      _self: 'コード生成',
      table: 'データベーステーブル',
      tableDefine: 'テーブル列定義',
      template: 'コードテンプレート',
      config: '生成設定',
      api: 'APIドキュメント'
    },
    routine: {
      _self: '日常業務',
      schedule: {
        _self: 'スケジュール管理',
        myschedule: 'マイスケジュール',
        dashboard: 'スケジュールダッシュボード',
      },
      car: {
        _self: '車両管理',
        info: '車両情報',
        application: '車両申請',
        dashboard: '車両ダッシュボード',
        maintenance: '車両メンテナンス',
      },
      email: {
        _self: 'メール管理',
        inbox: '受信トレイ',
        drafts: '下書き',
        sent: '送信済み',
        trash: 'ゴミ箱',
        template: 'メールテンプレート',        
      },
      meeting: {
        _self: '会議管理',
        room: '会議室',
        mymeeting: 'マイ会議',
        booking: '会議予約',
        dashboard: '会議ダッシュボード',
      },
      notice: { 
        _self: '通知公告',
        message: {
          _self: 'メッセージ管理',
          mymessages: 'マイメッセージ',
          list: 'メッセージダッシュボード',
        },
        announcement: {
          _self: '公告管理',
          signoff: '公告受領',
          list: '公告リスト',
        },
        notification: {
          _self: '通知管理',
          ack: '既読通知',
          list: '通知リスト',
        },
      },
      hr: {
        _self: '人事勤怠',
        recruitment: {
          _self: '採用管理',
          apply: '採用申請',
          approval: '採用承認',
          list: '採用リスト',

        },
        transfer: {
          _self: '異動管理',
          apply: '異動申請',
          approval: '異動承認',
          list: '異動リスト',
        },
        leave: {
          _self: '休暇管理',
          apply: '休暇申請',
          approval: '休暇承認',
          list: '休暇リスト',
        },
        trip: {
          _self: '出張管理',
          apply: '出張申請',
          approval: '出張承認',
          list: '出張リスト',
        },
        overtime: {
          _self: '残業管理',
          apply: '残業申請',
          approval: '残業承認',
          list: '残業リスト',
      },
    },
    expense:{
      _self: '費用管理',
      daily: {
        _self: '日常費用',
        apply: '費用申請',
        approve: '費用承認',
        list: '費用リスト',
      },
      travel: {
        _self: '出張費用',
        apply: '出張費申請',
        approve: '出張費承認',
        list: '出張費リスト',
      },
    },
    file:{
      _self: 'ファイル管理',
      daily: {
        _self: '日常ファイル',
        list: 'ファイルリスト',
      },
      iso: {
        _self: 'ISOファイル',
        version: 'バージョン',
        signoff: '受領',
        list: 'ISOファイル',
      },
      document: { 
        _self: '公文管理',
        version: 'バージョン',
        signoff: '受領',
        list: '公文リスト',
      },
    },
    officesupplies:{
      _self: '事務用品',
      inventory:{
        _self: '在庫管理',
        requisition: '購入管理',
        inbound: '入庫管理',
        stocktaking: '棚卸管理',
      },
      usage:{
        _self: '使用管理',
        apply: '使用申請',
        approve: '使用承認',
        receive: '使用記録',
      }
    },
    book:{
      _self: '図書管理',
      inventory:{
        _self: '在庫管理',
        requisition: '購入管理',
        inbound: '入庫管理',
        list: '図書リスト',
        stocktaking: '棚卸管理',
      },
      usage:{
        _self: '使用管理',
        card: '貸出証',
        borrow: '貸出',
        return: '返却',
      }

    },
    medical:{
      _self: '医療管理',
      medicine:{
        _self: '在庫管理',
        requisition: '購入管理',
        inbound: '入庫管理',
        list: '薬品リスト',
        stocktaking: '棚卸管理',
      },
      usage:{
        _self: '使用管理',
        archive: 'アーカイブ',
        receive: '薬品受領',
        cost: '費用',
      }

    },
  },
  accounting: {
      _self: '会計計算',
      financial: {
        _self: '管理会計',
        company: "会社情報",
        account: '会計科目',
        companyaccount: '会社科目',
        ledger: '総勘定元帳',
        payable: '買掛金',
        receivable: '売掛金',
        fixedasset: '固定資産',
        bank: '銀行情報',

      },
      controlling: {
        _self: '管理会計',
        costelement: '原価要素',
        costcenter: '原価センター',
        profitcenter: '利益センター',
        accountsReceivable: '売掛金',
        accountsPayable: '買掛金',
        assetAccounting: '資産会計',
        tax: '税務管理',
        financialReporting: '財務報告'      
    },
    budget:{
      _self: '総合予算',
        formulation: {
          _self: '予算編成',
          sales: {
            _self: '売上予算',
            cost: '売上原価',
            rolling: '売上ロール',
          },
          production: {
            _self: '生産予算',
            auxiliary: '生産副資材',
            labor: '生産人件費',
            manufacturing: '生産製造',
          },
          cost: {
            _self: '原価予算',
            directmaterial: '直接材料',
            directlabor: '直接労務費',
            indirectlabor: '間接労務費',
            manufacturing: '製造間接費',
          },
          expense: {
            _self: '費用予算',
            sales: '販売費',
            management: '管理費',
            financial: '財務費用',
          },
          financial: {
            _self: '財務予算',
            cashflow: 'キャッシュフロー',
            balancesheet: '貸借対照表',
            income: '損益計算書',
          },
        },
        control: {
          _self: '予算管理',
          dashboard: '予算ダッシュボード',
          approval: '予算承認',
        },   
  },
},
    logistics: {
      _self: '後方管理',
      equipment: {
        _self: '設備管理',
        data: '設備マスタデータ',
        location: '設備位置',
        material: '資材関連',
        workorder: '作業指示書'

      },
      material: {
        _self: '資材管理',
        material:{
          _self: '資材管理',
          material: '資材マスタデータ',
          plant: '工場情報',
          master: '資材データ',
          plantmaster: '工場資材',
          vendor: '売主情報',
          supplier: 'サプライヤー情報',
        },
        purchase:{
          _self: '購買管理',
          vendor: '売主情報',
          supplier: 'サプライヤー情報',
          price: '購買価格',
          requisition: '購買要求',
          order: '購買発注',

        },



      },
      production: {
        _self: '生産管理',
        bom: '部品表',
        change: {
          _self: '設計変更',
          implementation: '設変実施',
          techcontact: '技術連絡',
          material: '資材確認',
          query: '設変照会',
          oldproduct: '旧品管理',
          sop: 'SOP確認',
          batch: '投入ロット',
          input: {
            _self: '設変入力',
            gijutsu: '技術課',
            seikan: '生管課',
            koubai: '購買課',
            uketsuke: '受検課',
            bukan: '部管課',
            seizou2: '制二課',
            seizou1: '制一課',
            hinkan: '品管課',
            seizougijutsu: '制技課',
  
          }
        },
        workcenter: '作業センター',
        order: '生産指示',
        kanban: 'かんばん',
        oph:{
          _self: 'OPH管理',
          workshop1: {
            _self: '制一課',
            output: '生産日報',
            defect: '生産不良',
            worktime: '生産工数',
            productionReport: '生産レポート',
            defectSummary: '不良集計',
            worktimeReport: '工数レポート'
          },
          workshop2: {
            _self: '制二課',
            output: '生産日報',
            inspection: '検査記録',
            repair: '修理記録',
            worktime: '生産工数',
            productionReport: '生産レポート',
            inspectionReport: '検査レポート',
            repairReport: '修理レポート',
            worktimeReport: '工数レポート'
          }
        }

      },
      project: {
        _self: 'プロジェクト管理',
        define: 'プロジェクト定義',
        cost: 'コスト計画',
        resource: 'リソース計画',
        schedule: 'スケジュール計画',

      },
      quality: {
        _self: '品質管理',
        item: '検査項目',
        receiving: '入荷検査',
        process: '工程検査',
        storage: '入庫検査',
        return: '返品検査',
  
      },
      sales: {
        _self: '販売管理',
        customer: '顧客情報',
        client: 'クライアント情報',
        price: '販売価格',
        order: '販売注文',
      },
      service: {
        _self: '顧客サービス',
        item: 'サービス項目',
        contract: 'サービス契約',
        request: 'サービス要求',
        workorder: 'サービス作業指示書',
        timesheet: '工数記録',
        consumption: '資材消費',
        outsourcing: '外注サービス'

      },
      complaint: {
        _self: 'クレーム管理',
        notice: '品質通知書',
        mark: 'クレーム明細',
        analysis: '原因分析',
        corrective: '是正措置',
        return: '返品交換実行',
        followUp: 'フォローアップ処理'
      }
    },
    humanResources: {
      _self: '人材管理',
      employeeManagement: {
        _self: '従業員管理',
        employeeMaster: '従業員マスタデータ',
        attendance: '勤怠管理',
        leave: '休暇管理',
        payroll: '給与管理',
        contractManagement: '契約管理' // 新規契約管理
      },
      recruitment: {
        _self: '採用管理',
        jobPosting: '職位公募',
        candidateManagement: '候補者管理',
        interviewScheduling: '面接スケジュール',
        offerManagement: '採用管理'
      },
      training: {
        _self: '研修管理',
        trainingPlan: '研修計画',
        trainingExecution: '研修実行',
        trainingEvaluation: '研修評価'
      },
      performance: {
        _self: '業績管理',
        goalSetting: '目標設定',
        performanceReview: '業績評価',
        feedback: 'フィードバック管理'
      },
      reporting: {
        _self: '人材レポート',
        employeeReports: '従業員レポート',
        attendanceReports: '勤怠レポート',
        payrollReports: '給与レポート',
        performanceReports: '業績レポート'
      }
    }
  }
}
