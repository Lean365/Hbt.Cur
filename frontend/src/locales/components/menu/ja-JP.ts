export default {
  menu: {
    home: 'ホーム',
    dashboard: {
      title: 'ダッシュボード',
      workplace: 'ワークスペース',
      analysis: '分析',
      monitor: 'モニター'
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
    admin: {
      _self: 'システム管理',
      config: 'システム設定',
      language: '言語管理',
      dicttype: '辞書タイプ',
      dictdata: '辞書データ',
      translation: '翻訳管理'
    },
    identity: {
      _self: '認証管理',
      user: 'ユーザー管理',
      role: 'ロール管理',
      dept: '部門管理',
      post: 'ポスト管理',
      menu: 'メニュー管理',
      tenant: 'テナント管理',
      oauth: 'OAuth管理'
    },
    audit: {
      _self: '監査ログ',
      operlog: '操作ログ',
      loginlog: 'ログインログ',
      dbdifflog: 'データベース差分ログ',
      exceptionlog: '例外ログ'
    },
    workflow: {
      _self: 'ワークフロー',
      definition: 'ワークフロー定義',
      instance: 'ワークフローインスタンス',
      task: 'タスク',
      node: 'ノード',
      variable: '変数',
      history: '履歴'
    },
    realtime: {
      _self: 'リアルタイム監視',
      server: 'サーバー監視',
      online: 'オンラインユーザー',
      message: 'オンラインメッセージ'
    },
    generator: {
      _self: 'コード生成',
      table: 'データベーステーブル',
      tableDefine: 'カスタムテーブル',
      template: 'コードテンプレート',
      config: '生成設定',
      api: 'APIドキュメント'
    },
    routine: {
      _self: '日常業務',
      file: 'ファイル管理',
      mail: 'メール管理',
      mailTmpl: 'メールテンプレート',
      notice: '通知',
      task: 'タスク',
      schedule: 'スケジュール管理'
    },
    finance: {
      _self: '財務',
      management: {
        _self: '管理会計',
        cost: {
          _self: 'コスト管理',
          costFactors: 'コスト要素',
          costCenter: 'コストセンター',
          profitCenter: '利益センター',
          productCost: '製品コスト',
          activityType: '活動タイプ',
          internalOrder: '内部注文'
        },
        planning: {
          _self: '計画管理',
          costPlanning: 'コスト計画',
          profitPlanning: '利益計画',
          budgetControl: '予算管理'
        },
        reporting: {
          _self: 'レポートと分析',
          costReports: 'コストレポート',
          profitReports: '利益レポート',
          varianceAnalysis: '差異分析'
        }
      },
      financial: {
        _self: '財務会計',
        generalLedger: {
          _self: '総勘定元帳',
          account: '勘定科目',
          accountType: '勘定タイプ',
          journalEntry: '仕訳入力',
          reconciliation: '調整',
          closing: '期末決算'
        },
        accountsReceivable: {
          _self: '売掛金管理',
          customer: '顧客管理',
          invoice: '顧客請求書',
          payment: '顧客支払い',
          creditControl: '信用管理'
        },
        accountsPayable: {
          _self: '買掛金管理',
          supplier: 'サプライヤー管理',
          invoice: 'サプライヤー請求書',
          payment: 'サプライヤー支払い',
          agingReport: '年齢分析レポート'
        },
        assetAccounting: {
          _self: '資産会計',
          assets: '固定資産',
          depreciation: '減価償却管理',
          assetTransfer: '資産移転',
          assetRetirement: '資産廃棄'
        },
        tax: {
          _self: '税務管理',
          taxCodes: '税コード',
          taxReporting: '税務レポート',
          taxPayments: '税金支払い'
        },
        financialReporting: {
          _self: '財務レポート',
          balanceSheet: '貸借対照表',
          profitAndLoss: '損益計算書',
          cashFlow: 'キャッシュフロー計算書'
        }
      }
    },
    logistics: {
      _self: '物流',
      sales: {
        _self: '販売管理',
        customer: {
          _self: '顧客管理',
          client: '顧客',
          customers: '顧客リスト',
          creditControl: '信用管理'
        },
        order: {
          _self: '注文管理',
          order: '販売注文',
          orderDetail: '注文詳細',
          orderTracking: '注文追跡'
        },
        delivery: {
          _self: '配送管理',
          delivery: '配送伝票',
          deliveryDetail: '配送詳細',
          shipping: '配送管理'
        },
        billing: {
          _self: '請求管理',
          invoice: '請求書管理',
          invoiceDetail: '請求書詳細',
          payment: '支払い管理'
        },
        reporting: {
          _self: 'レポートと分析',
          salesReports: '販売レポート',
          performanceAnalysis: 'パフォーマンス分析'
        }
      },
      production: {
        _self: '生産管理',
        bom: '部品表 (BOM)',
        routing: 'ルーティング',
        workOrder: {
          _self: '生産指図',
          create: '生産指図の作成',
          manage: '生産指図の管理',
          release: '生産指図のリリース',
          complete: '生産指図の完了'
        },
        capacityPlanning: {
          _self: '能力計画',
          workCenter: '作業センター',
          capacityEvaluation: '能力評価',
          capacityLeveling: '能力平準化'
        },
        productionScheduling: {
          _self: '生産スケジューリング',
          schedule: 'スケジュール',
          reschedule: '再スケジュール'
        },
        productionExecution: {
          _self: '生産実行',
          confirm: '生産確認',
          goodsIssue: '出庫',
          goodsReceipt: '入庫'
        },
        productionReporting: {
          _self: '生産レポート',
          orderReports: '指図レポート',
          capacityReports: '能力レポート',
          efficiencyReports: '効率レポート'
        },
        qualityManagement: {
          _self: '品質管理',
          inspectionLot: '検査ロット',
          resultsRecording: '結果記録',
          defectRecording: '欠陥記録'
        }
      },
      material: {
        _self: '資材管理',
        materialMaster: '資材マスターデータ',
        materialCategory: '資材カテゴリ',
        materialUnit: '資材単位',
        materialStock: {
          _self: '資材在庫',
          stockOverview: '在庫概要',
          stockIn: '入庫',
          stockOut: '出庫',
          stockTransfer: '在庫移動',
          stockAdjustment: '在庫調整',
          stockCheck: '在庫確認'
        },
        purchase: {
          _self: '購買管理',
          purchaseRequisition: '購買依頼',
          purchaseOrder: '購買注文',
          purchaseOrderDetail: '購買注文詳細',
          supplier: 'サプライヤー管理'
        },
        inventoryManagement: {
          _self: '在庫管理',
          goodsReceipt: '入庫',
          goodsIssue: '出庫',
          transferPosting: '転記',
          stockOverview: '在庫概要'
        },
        valuation: {
          _self: '資材評価',
          priceControl: '価格管理',
          standardPrice: '標準価格',
          movingAveragePrice: '移動平均価格'
        },
        reporting: {
          _self: 'レポートと分析',
          stockReports: '在庫レポート',
          purchaseReports: '購買レポート',
          inventoryReports: '在庫分析レポート'
        }
      }
    },
    quality: {
      _self: '品質管理',
      inspection: {
        _self: '検査管理',
        inspectionLot: '検査ロット',
        resultsRecording: '結果記録',
        defectRecording: '欠陥記録',
        usageDecision: '使用決定'
      },
      qualityPlanning: {
        _self: '品質計画',
        inspectionPlan: '検査計画',
        qualityInfoRecord: '品質情報記録',
        samplingProcedure: 'サンプリング手順'
      },
      qualityControl: {
        _self: '品質管理',
        controlChart: '管理図',
        qualityNotifications: '品質通知',
        correctiveActions: '是正措置'
      },
      qualityReporting: {
        _self: '品質レポート',
        inspectionReports: '検査レポート',
        defectReports: '欠陥レポート',
        qualityAnalysis: '品質分析'
      }
    },
    service: {
      _self: '顧客サービス',
      serviceOrder: {
        _self: 'サービス指図',
        create: 'サービス指図の作成',
        manage: 'サービス指図の管理',
        complete: 'サービス指図の完了',
        cancel: 'サービス指図のキャンセル'
      },
      serviceContract: {
        _self: 'サービス契約',
        create: 'サービス契約の作成',
        manage: 'サービス契約の管理',
        renew: 'サービス契約の更新',
        terminate: 'サービス契約の終了'
      },
      customerInteraction: {
        _self: '顧客対応',
        inquiries: '顧客問い合わせ',
        complaints: '顧客苦情',
        feedback: '顧客フィードバック'
      },
      serviceExecution: {
        _self: 'サービス実行',
        schedule: 'サービススケジュール',
        dispatch: 'サービス派遣',
        execution: 'サービス実行',
        confirmation: 'サービス確認'
      },
      serviceReporting: {
        _self: 'サービスレポート',
        orderReports: 'サービス指図レポート',
        contractReports: 'サービス契約レポート',
        performanceReports: 'サービスパフォーマンスレポート'
      }
    },
    equipment: {
      _self: '設備管理',
      equipmentMaster: '設備マスターデータ',
      maintenancePlanning: {
        _self: '保守計画',
        preventiveMaintenance: '予防保守',
        maintenanceTaskList: '保守タスクリスト',
        scheduling: '保守スケジュール'
      },
      maintenanceExecution: {
        _self: '保守実行',
        workOrder: '保守作業指図',
        confirmation: '保守確認',
        breakdownMaintenance: '故障保守'
      },
      maintenanceReporting: {
        _self: '保守レポート',
        equipmentReports: '設備レポート',
        maintenanceHistory: '保守履歴',
        performanceAnalysis: '性能分析'
      },
      sparePartsManagement: {
        _self: '予備部品管理',
        sparePartsInventory: '予備部品在庫',
        sparePartsProcurement: '予備部品調達',
        sparePartsUsage: '予備部品使用'
      }
    },
    humanResources: {
      _self: '人事管理',
      employeeManagement: {
        _self: '従業員管理',
        employeeMaster: '従業員マスターデータ',
        attendance: '勤怠管理',
        leave: '休暇管理',
        payroll: '給与管理',
        contractManagement: '契約管理'
      },
      recruitment: {
        _self: '採用管理',
        jobPosting: '求人情報',
        candidateManagement: '候補者管理',
        interviewScheduling: '面接スケジュール',
        offerManagement: '採用オファー管理'
      },
      training: {
        _self: '研修管理',
        trainingPlan: '研修計画',
        trainingExecution: '研修実行',
        trainingEvaluation: '研修評価'
      },
      performance: {
        _self: 'パフォーマンス管理',
        goalSetting: '目標設定',
        performanceReview: 'パフォーマンスレビュー',
        feedback: 'フィードバック管理'
      },
      reporting: {
        _self: '人事レポート',
        employeeReports: '従業員レポート',
        attendanceReports: '勤怠レポート',
        payrollReports: '給与レポート',
        performanceReports: 'パフォーマンスレポート'
      }
    }
  }
}
