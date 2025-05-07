import { countReset } from "node:console";

export default {
  menu: {
    home: 'ホーム',
    dashboard: {
      title: 'ダッシュボード',
      workplace: 'ワークスペース',
      analysis: '分析',
      monitor: 'モニタリング'
    },
    components: {
      title: 'コンポーネント',
      icons: 'アイコン'
    },
    about: {
      title: '会社概要',
      privacy: 'プライバシーポリシー',
      terms: '利用規約',
      index: 'Hbtについて'
    },
    core: {
      _self: 'システム管理',
      config: 'システム設定',
      language: '言語管理',
      dict: '辞書管理'
    },
    identity: {
      _self: '認証',
      user: 'ユーザー管理',
      role: 'ロール管理',
      dept: '部門管理',
      post: '職位管理',
      menu: 'メニュー管理',
      tenant: 'テナント管理',
      oauth: 'OAuth管理'
    },
    audit: {
      _self: '監査ログ',
      operlog: '操作ログ',
      loginlog: 'ログインログ',
      dbdifflog: 'データベース差分ログ',
      exceptionlog: '例外ログ',
      auditlog: '監査ログ',
      quartzlog: 'タスクログ'
    },
    workflow: {
      _self: 'ワークフロー',
      definition: 'プロセス定義',
      instance: 'プロセスインスタンス',
      task: 'タスク',
      node: 'プロセスのノード',
      variable: 'プロセス変数',
      history: 'プロセス履歴'
    },
    signalr: {
      _self: 'リアルタイムモニタリング',
      server: 'サーバーモニタリング',
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
      vehicle: {
        _self: '車両管理',
        vehicleMaster: {
          _self: '車両マスターデータ',
          vehicleInfo: '車両情報',
          driverInfo: '運転者情報',
          maintenance: '車両メンテナンス'
        },
        vehicleBooking: {
          _self: '車両予約',
          newBooking: '新規予約',
          bookingList: '予約一覧',
          bookingApproval: '予約承認'
        },
        vehicleDispatch: {
          _self: '車両配車',
          dispatchPlan: '配車計画',
          realTimeTracking: 'リアルタイム追跡',
          dispatchHistory: '配車履歴'
        },
        vehicleReporting: {
          _self: '車両レポート',
          usageReport: '使用レポート',
          costReport: 'コストレポート',
          maintenanceReport: 'メンテナンスレポート'
        }
      },
      file: 'ファイル管理',
      mail: 'メール管理',
      mailTmpl: 'メールテンプレート',
      meeting: {
        _self: '会議管理',
        meetingRoom: {
          _self: '会議室管理',
          roomInfo: '会議室情報',
          roomBooking: '会議室予約',
          roomSchedule: '会議室スケジュール'
        },
        meetingPlan: {
          _self: '会議計画',
          newMeeting: '新規会議',
          meetingList: '会議一覧',
          meetingApproval: '会議承認'
        },
        meetingExecution: {
          _self: '会議実行',
          attendance: '出席',
          minutes: '議事録',
          followUp: 'フォローアップ'
        },
        meetingReporting: {
          _self: '会議レポート',
          meetingReport: '会議レポート',
          attendanceReport: '出席レポート',
          costReport: 'コストレポート'
        }
      },
      notice: 'お知らせ',
      schedule: 'スケジュール管理',
      quartz: 'タスク'
    },
    finance: {
      _self: '会計',
      management: {
        _self: '管理会計',
        cost: {
          _self: '原価管理',
          costFactors: '原価要素',
          costCenter: '原価センター',
          profitCenter: '利益センター',
          productCost: '製品原価',
          activityType: '活動タイプ',
          internalOrder: '内部発注'
        },
        planning: {
          _self: '計画管理',
          costPlanning: '原価計画',
          profitPlanning: '利益計画',
          budgetControl: '予算管理'
        },
        reporting: {
          _self: 'レポートと分析',
          costReports: '原価レポート',
          profitReports: '利益レポート',
          varianceAnalysis: '差異分析'
        }
      },
      financial: {
        _self: '財務会計',
        generalLedger: {
          _self: '総勘定元帳',
          account: '勘定科目',
          accountType: '勘定科目タイプ',
          journalEntry: '仕訳',
          reconciliation: '照合',
          closing: '決算'
        },
        accountsReceivable: {
          _self: '売掛金',
          customer: '顧客管理',
          invoice: '顧客請求書',
          payment: '顧客支払い',
          creditControl: '与信管理'
        },
        accountsPayable: {
          _self: '買掛金',
          supplier: '仕入先管理',
          invoice: '仕入先請求書',
          payment: '仕入先支払い',
          agingReport: '債権債務分析'
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
          taxCodes: '税コード管理',
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
      _self: 'ロジスティクス',
      equipment: {
        _self: '設備管理',
        equipmentMaster: '設備マスターデータ',
        maintenancePlanning: {
          _self: 'メンテナンス計画',
          preventiveMaintenance: '予防メンテナンス',
          maintenanceTaskList: 'メンテナンスタスクリスト',
          scheduling: 'メンテナンススケジュール'
        },
        maintenanceExecution: {
          _self: 'メンテナンス実行',
          workOrder: '作業指示書',
          confirmation: 'メンテナンス確認',
          breakdownMaintenance: '故障メンテナンス'
        },
        maintenanceReporting: {
          _self: 'メンテナンスレポート',
          equipmentReports: '設備レポート',
          maintenanceHistory: 'メンテナンス履歴',
          performanceAnalysis: '性能分析'
        },
        sparePartsManagement: {
          _self: '予備部品管理',
          sparePartsInventory: '予備部品在庫',
          sparePartsProcurement: '予備部品調達',
          sparePartsUsage: '予備部品使用'
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
          stockTransfer: '在庫移転',
          stockAdjustment: '在庫調整',
          stockCheck: '在庫確認'
        },
        purchase: {
          _self: '購買管理',
          purchaseRequisition: '購買依頼',
          purchaseOrder: '購買発注',
          purchaseOrderDetail: '購買発注明細',
          supplier: '仕入先管理'
        },
        inventoryManagement: {
          _self: '在庫管理',
          goodsReceipt: '入荷',
          goodsIssue: '出荷',
          transferPosting: '転記',
          stockOverview: '在庫概要'
        },
        valuation: {
          _self: '評価',
          priceControl: '価格管理',
          standardPrice: '標準価格',
          movingAveragePrice: '移動平均価格'
        },
        reporting: {
          _self: 'レポートと分析',
          stockReports: '在庫レポート',
          purchaseReports: '購買レポート',
          inventoryReports: '在庫レポート'
        }
      },
      production: {
        _self: '生産管理',
        bom: '部品表',
        routing: '工程ルート',
        workOrder: {
          _self: '作業指示書',
          create: '作業指示書作成',
          manage: '作業指示書管理',
          release: '作業指示書発行',
          complete: '作業指示書完了'
        },
        capacityPlanning: {
          _self: '生産能力計画',
          workCenter: '作業センター',
          capacityEvaluation: '生産能力評価',
          capacityLeveling: '生産能力平準化'
        },
        productionScheduling: {
          _self: '生産スケジューリング',
          schedule: 'スケジュール',
          reschedule: 'スケジュール再調整'
        },
        productionExecution: {
          _self: '生産実行',
          confirm: '生産確認',
          goodsIssue: '資材払出',
          goodsReceipt: '製品入庫'
        },
        productionReporting: {
          _self: '生産レポート',
          orderReports: '注文レポート',
          capacityReports: '生産能力レポート',
          efficiencyReports: '効率レポート'
        },
        qualityManagement: {
          _self: '品質管理',
          inspectionLot: '検査ロット',
          resultsRecording: '結果記録',
          defectRecording: '欠陥記録'
        }
      },
      project: {
        _self: 'プロジェクト管理',
        projectMaster: {
          _self: 'プロジェクトマスターデータ',
          projectDefinition: 'プロジェクト定義',
          projectStructure: 'プロジェクト構造',
          projectTeam: 'プロジェクトチーム',
          projectCalendar: 'プロジェクトカレンダー'
        },
        projectPlanning: {
          _self: 'プロジェクト計画',
          workBreakdown: '作業分解',
          scheduling: 'スケジューリング',
          resourcePlanning: 'リソース計画',
          costPlanning: '原価計画'
        },
        projectExecution: {
          _self: 'プロジェクト実行',
          taskManagement: 'タスク管理',
          progressTracking: '進捗追跡',
          resourceManagement: 'リソース管理',
          costControl: '原価管理'
        },
        projectMonitoring: {
          _self: 'プロジェクトモニタリング',
          progressReports: '進捗レポート',
          resourceReports: 'リソースレポート',
          costReports: '原価レポート',
          riskManagement: 'リスク管理'
        },
        projectClosure: {
          _self: 'プロジェクト終了',
          finalReport: '最終レポート',
          lessonsLearned: '教訓',
          projectArchive: 'プロジェクトアーカイブ'
        }
      },
      quality: {
        _self: '品質管理',
        inspection: {
          _self: '検査管理',
          inspectionLot: '検査ロット',
          resultsRecording: '結果記録',
          defectRecording: '欠陥記録',
          usageDecision: '使用判断'
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
      sales: {
        _self: '販売',
        customer: {
          _self: '顧客管理',
          client: '顧客',
          customers: '顧客一覧',
          creditControl: '与信管理'
        },
        order: {
          _self: '注文管理',
          order: '販売注文',
          orderDetail: '注文明細',
          orderTracking: '注文追跡'
        },
        delivery: {
          _self: '出荷管理',
          delivery: '出荷',
          deliveryDetail: '出荷明細',
          shipping: '配送管理'
        },
        billing: {
          _self: '請求',
          invoice: '請求書管理',
          invoiceDetail: '請求書明細',
          payment: '支払い管理'
        },
        reporting: {
          _self: 'レポートと分析',
          salesReports: '販売レポート',
          performanceAnalysis: '業績分析'
        }
      },
      service: {
        _self: 'サービス',
        serviceOrder: {
          _self: 'サービス注文',
          create: '注文作成',
          manage: '注文管理',
          complete: '注文完了',
          cancel: '注文取消'
        },
        serviceContract: {
          _self: 'サービス契約',
          create: '契約作成',
          manage: '契約管理',
          renew: '契約更新',
          terminate: '契約終了'
        },
        customerInteraction: {
          _self: '顧客対応',
          inquiries: '顧客問い合わせ',
          complaints: '顧客苦情',
          feedback: '顧客フィードバック'
        },
        serviceExecution: {
          _self: 'サービス実行',
          schedule: 'スケジュール',
          dispatch: '派遣',
          execution: '実行',
          confirmation: '確認'
        },
        serviceReporting: {
          _self: 'サービスレポート',
          orderReports: '注文レポート',
          contractReports: '契約レポート',
          performanceReports: '業績レポート'
        }
      }
    },
    humanResources: {
      _self: '人事管理',
      employee: {
        _self: '従業員管理',
        employeeInfo: '従業員情報',
        employeeProfile: '従業員プロフィール',
        employeeContract: '従業員契約',
        employeeAttendance: '従業員出勤',
        employeeLeave: '従業員休暇',
        employeePerformance: '従業員業績'
      },
      recruitment: {
        _self: '採用管理',
        jobPosting: '求人掲載',
        candidate: '候補者管理',
        interview: '面接管理',
        offer: '採用管理',
        onboarding: '入社管理'
      },
      training: {
        _self: '教育管理',
        trainingPlan: '教育計画',
        trainingCourse: '教育コース',
        trainingRecord: '教育記録',
        trainingEvaluation: '教育評価'
      },
      performance: {
        _self: '業績管理',
        performancePlan: '業績計画',
        performanceAppraisal: '業績評価',
        performanceReview: '業績レビュー',
        performanceImprovement: '業績改善'
      },
      compensation: {
        _self: '報酬管理',
        salary: '給与管理',
        bonus: '賞与管理',
        benefits: '福利厚生管理',
        payroll: '給与明細'
      },
      reporting: {
        _self: 'レポートと分析',
        employeeReports: '従業員レポート',
        recruitmentReports: '採用レポート',
        trainingReports: '教育レポート',
        performanceReports: '業績レポート',
        compensationReports: '報酬レポート'
      }
    }
  }
}
