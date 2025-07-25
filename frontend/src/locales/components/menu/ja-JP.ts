import { countReset } from "node:console";

export default {
  menu: {
    home: 'ホーム',
    dashboard: {
      title: 'ダッシュボード',
      workplace: 'ワークプレイス',
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

    identity: {
      _self: '認証',
      user: 'ユーザー',
      role: 'ロール',
      dept: '部門',
      post: '職位',
      menu: 'メニュー',
      tenant: 'テナント',
      oauth: 'OAuth',
      profile: '個人情報',
      changePassword: 'パスワード変更'
    },
    audit: {
      _self: '監査ログ',
      operlog: '操作',
      loginlog: 'イン',
      sqldifflog: '差分',
      exceptionlog: '例外',
      auditlog: '監査',
      quartzlog: 'タスク',
      server: 'サーバーモニター'
    },
    workflow: {
      _self: 'ワークフロー',
      engine:{
        _self: 'プロセスエンジン',
        monitor: 'プロセス監視',
        todo: '未完了タスク',
        done: '完了タスク',
        signoff: 'プロセス承認',
        execution: 'プロセス実行',
        designer: 'プロセスデザイナー'
      },
      manage:{
        _self: 'プロセス',
        form: 'フォーム',
        scheme: 'プロセススキーム',
        instance: 'プロセスインスタンス',
        oper: 'インスタンス操作',
        trans: 'インスタンスフロー'
      }
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
      core: {
        _self: '基本サービス',
        numberrule: '番号規則',
        config: 'システム設定',
        language: '言語',
        dict: '辞書'
      },
      contract: {
        _self: '契約',
        template: {
          _self: '契約テンプレート',
          manage: 'テンプレート',
          category: 'テンプレートカテゴリ'
        },
        draft: {
          _self: '契約起草',
          apply: '起草申請',
          my: '私の起草'
        },
        approval: {
          _self: '契約承認',
          pending: '契約承認',
          approved: '承認済み',
          record: '承認記録'
        },
        execution: {
          _self: '契約実行',
          track: '実行追跡',
          change: '変更',
          payment: '支払い'
        },
        archive: {
          _self: '契約アーカイブ',
          manage: 'アーカイブ',
          query: '照会統計'
        }
      },
      project: {
        _self: 'プロジェクト',
        info: {
          _self: 'プロジェクト情報',
          list: 'プロジェクトリスト'
        },
        plan: {
          _self: 'プロジェクト計画',
          request: '計画要求',
          gantt: 'プロジェクトガントチャート'
        },
        task: {
          _self: 'プロジェクトタスク',
          assign: 'タスク割り当て',
          track: 'タスク追跡',
          board: 'タスクボード'
        },
        resource: {
          _self: 'プロジェクトリソース',
          personnel: '人員',
          equipment: '設備',
          budget: '予算'
        },
        monitor: {
          _self: 'プロジェクト監視',
          progress: '進捗監視',
          quality: '品質監視',
          risk: 'リスク監視'
        }
      },
      quartz: {
        _self: 'タスクスケジューリング',
        job: {
          _self: 'タスク',
          config: 'タスク設定',
          list: 'タスクリスト',
          status: 'タスクステータス'
        },
        schedule: {
          _self: 'タスクスケジューリング',
          config: 'スケジュール設定',
          monitor: 'スケジュール監視',
          stats: 'スケジュール統計'
        }
      },
      schedule: {
        _self: 'スケジュール',
        myschedule: '私のスケジュール',
        dashboard: 'スケジュールダッシュボード'
      },
      vehicle: {
        _self: '車両',
        my: '私の車両',
        application: '車両申請',
        dashboard: '車両ダッシュボード',
        manage: {
          _self: '車両',
          info: '車両情報',
          maintenance: '車両メンテナンス'
        }
      },
      email: {
        _self: 'メール',
        inbox: '受信トレイ',
        drafts: '下書き',
        sent: '送信済み',
        trash: 'ゴミ箱',
        template: 'メールテンプレート'
      },
      meeting: {
        _self: '会議',
        room: '会議室',
        mymeeting: '私の会議',
        booking: '会議予約',
        dashboard: '会議ダッシュボード'
      },
      notice: {
        _self: '通知・お知らせ',
        message: {
          _self: 'メッセージ',
          mymessages: '私のメッセージ',
          list: 'メッセージボード'
        },
        announcement: {
          _self: 'お知らせ',
          signoff: 'お知らせ確認',
          list: 'お知らせリスト'
        },
        notification: {
          _self: '通知',
          ack: '既読通知',
          list: '通知リスト'
        }
      },
      hr: {
        _self: '人事勤怠',
        recruitment: {
          _self: '採用',
          apply: '採用申請',
          approval: '採用承認',
          list: '採用リスト'
        },
        transfer: {
          _self: '異動',
          apply: '異動申請',
          approval: '異動承認',
          list: '異動リスト'
        },
        leave: {
          _self: '休暇',
          apply: '休暇申請',
          approval: '休暇承認',
          list: '休暇リスト'
        },
        trip: {
          _self: '出張',
          apply: '出張申請',
          approval: '出張承認',
          list: '出張リスト'
        },
        overtime: {
          _self: '残業',
          apply: '残業申請',
          approval: '残業承認',
          list: '残業リスト'
        }
      },
      expense: {
        _self: '経費',
        daily: {
          _self: '日常経費',
          apply: '経費申請',
          approve: '経費承認',
          list: '経費リスト'
        },
        travel: {
          _self: '出張経費',
          apply: '出張経費申請',
          approve: '出張経費承認',
          list: '出張経費リスト'
        }
      },
      document: {
        _self: '文書',
        news: {
          _self: 'ニュース',
        },
        regulation: {
          _self: '規制・規則',
          manage: '規制',
          control: '規制制御',
        },
        file: {
          _self: '日常ファイル',
        },
        iso: {
          _self: 'ISOファイル',
          manage: 'ファイル',
          control: 'ファイル制御',
        },
        official: {
          _self: '公文書',
          manage: '文書',
          issuance: '文書制御',
        },
        law: {
          _self: '法律・規制',
        }
      },
      officesupplies: {
        _self: '事務用品',
        inventory: {
          _self: '在庫',
          requisition: '購入',
          inbound: '入庫',
          stocktaking: '棚卸'
        },
        usage: {
          _self: '使用',
          apply: '使用申請',
          approve: '使用承認',
          list: '使用記録'
        }
      },
      book: {
        _self: '図書',
        inventory: {
          _self: '在庫',
          requisition: '購入',
          inbound: '入庫',
          list: '図書リスト',
          stocktaking: '棚卸'
        },
        usage: {
          _self: '使用',
          card: '図書カード',
          borrow: '貸出',
          return: '返却'
        }
      },
      medical: {
        _self: '医療',
        medicine: {
          _self: '在庫',
          requisition: '購入',
          inbound: '入庫',
          list: '医薬品リスト',
          stocktaking: '棚卸'
        },
        usage: {
          _self: '使用',
          archive: 'アーカイブ',
          receive: '薬剤受取',
          cost: '費用'
        }
      }
    },
    accounting: {
      _self: '会計',
      financial: {
        _self: '会計',
        company: '会社情報',
        account: '勘定科目',
        companyaccount: '会社勘定',
        ledger: '総勘定元帳',
        payable: '買掛金',
        receivable: '売掛金',
        fixedasset: '固定資産',
        bank: '銀行情報'
      },
      controlling: {
        _self: '会計',
        costelement: '費用要素',
        costcenter: 'コストセンター',
        profitcenter: '利益センター',
        accountsReceivable: '売掛金',
        accountsPayable: '買掛金',
        assetAccounting: '資産会計',
        tax: '税務',
        financialReporting: '財務報告'
      },
      budget: {
        _self: '総合予算',
        formulation: {
          _self: '予算策定',
          sales: {
            _self: '売上予算',
            cost: '売上原価',
            rolling: '売上ローリング'
          },
          production: {
            _self: '生産予算',
            auxiliary: '生産補助',
            labor: '生産労務',
            manufacturing: '生産製造'
          },
          cost: {
            _self: '原価予算',
            directmaterial: '直接材料',
            directlabor: '直接労務',
            indirectlabor: '間接労務',
            manufacturing: '製造間接費'
          },
          expense: {
            _self: '経費予算',
            sales: '販売費',
            manage: '費',
            financial: '財務費'
          },
          financial: {
            _self: '財務予算',
            cashflow: 'キャッシュフロー',
            balancesheet: '貸借対照表',
            income: '損益計算書'
          }
        },
        control: {
          _self: '予算',
          dashboard: '予算ダッシュボード',
          approval: '予算承認'
        }
      }
    },
    logistics: {
      _self: 'ロジスティクス',
      equipment: {
        _self: '設備',
        master: {
          _self: '設備データ',
          list: '設備情報',
          location: '機能位置',
          material: '材料関連'
        },
        maintenance: {
          _self: '設備保守',
          workorder: '保守計画',
          assign: '保守割り当て',
          execute: '保守実行'
        }
      },
      material: {
        _self: '材料',
        manage: {
          _self: '材料情報',
          master: 'グループ材料',
          plant: {
            _self: '工場情報',
            master: '工場材料'
          }
        },
        purchase: {
          _self: '購買',
          vendor: 'ベンダー情報',
          supplier: 'サプライヤー情報',
          price: '購買価格',
          requisition: '購買要求',
          order: '購買発注'
        },
        sample:{
          _self: 'サンプル',
          component: '部品サンプル',
          product: '製品サンプル'
        },
        drawing: {
          _self: '図面',
          design: '図面',
          engineering: '図面制御',
          gerber: 'ガーバーファイル',
          coordinate: '座標ファイル',
          assembly: '組立図面',
          structure: '構造ファイル',
          impedance: 'インピーダンスファイル',
          process: 'プロセスフロー'
        },
        csm: {  
          _self: '客供品',
          raw: '客供材料',
          good: '客供製品'
        }
      },
      production: {
        _self: '生産',
        basic: {
          _self: '基本データ',
          bom: '部品表',
          workcenter: '作業センター',   
          routing: '工程ルート',
          order: '生産指示',
          worktime: '生産工数',
          kanban: 'かんばん'
        },
        change: {
          _self: '設計変更',
          implementation: '変更実施',
          techcontact: '技術連絡',
          material: '材料確認',
          query: '変更照会',
          oldproduct: '旧品',
          sop: 'SOP確認',
          batch: '投入ロット',
          input: {
            _self: '変更入力',
            gijutsu: '技術課',
            seikan: '生管課',
            koubai: '購買課',
            uketsuke: '受検課',
            bukan: '部管課',
            seizou2: '製二課',
            seizou1: '製一课',
            hinkan: '品管課',
            seizougijutsu: '製技課'
          }
        },       
        output: {
          _self: '製造',
          workshop1:{
            _self: '製一课',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: '生産',
              modify: '改修',
              rework: '手直し'
            },
            defect:{
              _self: '不良',
              epp: 'EPP',
              production: '生産',
              modify: '改修',
              rework: '手直し'
            },
            worktime: {
              _self: '工数',
              epp: 'EPP',
              production: '生産',
              modify: '改修',
              rework: '手直し'
            }
          },
          workshop2:{
            _self: '製二課',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: '生産',
              modify: '改修',
              rework: '手直し'
            },
            defect:{
              _self: '不良',
              eppInspection: 'EPP検査',
              eppRepair: 'EPP修理',
              productionInspection: '生産検査',
              productionRepair: '生産修理',
              modifyInspection: '改修検査',
              modifyRepair: '改修修理',
              reworkInspection: '手直し検査',
              reworkRepair: '手直し修理'
            },
            worktime: {
              _self: '工数',
              epp: 'EPP',
              production: '生産',
              modify: '改修',
              rework: '手直し'
            }
          }
        },
        sop: {
          _self: 'SOP',
          workshop1: '製一课',
          workshop2: '製二課'
        },
        techcontact: {
          _self: '技術連絡',
          epp: 'EPP連絡',
          engineering: '工程連絡',
          external: '外部連絡'
        }
      },
      project: {
        _self: 'プロジェクト',
        define: 'プロジェクト定義',
        cost: 'コスト計画',
        resource: 'リソース計画',
        schedule: 'スケジュール計画'
      },
      quality: {
        _self: '品質',
        basic: {
          _self: '基本データ',
          item: '検査項目',
          method: '検査方法',
          sampling: 'サンプリング計画',
          defect: '不良分類',
          rule: '判定規則',
          category: '品質カテゴリ'
        },
        inspection:{
          _self: '検査',
          receiving: '入荷検査',
          process: '工程検査',
          storage: '入庫検査',
          return: '返品検査'
        },
        trace:{
          _self: 'トレーサビリティ',
          batch: 'ロットトレース',
          corrective: '是正措置',
          notification: '通知書',
        },
        cost:{
          _self: '品質コスト',
          business:'品質業務',
          rework:'手直し業務',
          scrap:'廃棄業務',
        },
        plan: {
          _self: '品質計画',
          supplier: 'サプライヤー評価',
          customer: '顧客調査'
        },
        item: '検査項目',
        receiving: '入荷検査',
        process: '工程検査',
        storage: '入庫検査',
        return: '返品検査'
      },
      sales: {
        _self: '販売',
        customer: '顧客情報',
        client: 'クライアント情報',
        price: '販売価格',
        order: '販売発注'
      },
      service: {
        _self: '顧客サービス',
        cs: {
          _self: '顧客サービス',
          item: 'サービス項目',
          contract: 'サービス契約',
          request: 'サービス要求',
          workorder: 'サービス作業指示',
          timesheet: 'サービス工数',
          consumption: '材料消費',
          outsourcing: '外注サービス'
        },
        cc: {
          _self: '顧客クレーム',
          notice: '品質通知書',
          mark: 'クレーム詳細',
          analysis: '原因分析',
          corrective: '是正措置',
          return: '返品・交換実行',
          followUp: 'フォローアップ処理'
        }
      }
    },
    hrm: {
      _self: '人事',
      attendance: {
        _self: '勤怠',
        record: '勤怠記録',
        holiday: '休暇',
        overtime: '残業',
        compensatory: '振替休暇'
      },
      benefit: {
        _self: '福利厚生',
        project: '福利厚生プロジェクト',
        employee: '従業員福利厚生'
      },
      employee: {
        _self: '人員',
        info: '人員情報',
        contracttype: '契約タイプ',
        contract: '契約',
        promotion: '昇進',
        promotionhistory: '昇進履歴',
        resignation: '退職',
        transfer: '人員リスト',
        transferhistory: '異動履歴'
      },
      leave: {
        _self: '休暇',
        type: '休暇タイプ',
        employee: '従業員休暇'
      },
      organization: {
        _self: '組織',
        positioncategory: '職位カテゴリ',
        company: '会社情報',
        department: '部門情報',
        position: '職位情報'
      },
      performance: {
        _self: '業績',
        assessmentitem: '評価項目',
        assessment: '業績評価'
      },
      recruitment: {
        _self: '採用',
        application: '職位応募',
        posting: '職位募集',
        candidate: '候補者',
        interview: '面接'
      },
      salary: {
        _self: '給与',
        employee: '従業員給与',
        housing: '住宅積立金',
        housinglevel: '社会保険',
        tax: '税務',
        taxlevel: '税率',
        structure: '給与構造',
        social: '社会保険',
        socialbase: '社会保険基数'
      },
      training: {
        _self: '研修',
        category: '研修カテゴリ',
        course: '研修コース',
        record: '従業員研修'
      },
      report: {
        _self: 'レポート',
        employeeinfo: '人員情報',
        resignation: '退職レポート',
        transfer: '異動レポート',
        promotion: '昇進レポート',
        training: '研修レポート',
        salary: '給与レポート',
        performance: '業績レポート',
        attendance: '勤怠レポート',
        benefit: '福利厚生レポート',
        recruitment: '採用レポート'
      }
    }
  }
}
