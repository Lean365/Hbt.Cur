export default {
  workflow: {
    node: {
      title: 'ワークフローノード',
      list: {
        title: 'ワークフローノード一覧',
        search: {
          name: 'ノード名',
          type: 'ノードタイプ',
          status: 'ステータス',
          startTime: '開始時間',
          endTime: '終了時間'
        },
        table: {
          name: 'ノード名',
          type: 'ノードタイプ',
          status: 'ステータス',
          startTime: '開始時間',
          endTime: '終了時間',
          duration: '実行時間',
          actions: '操作'
        },
        actions: {
          view: '表示',
          edit: '編集',
          delete: '削除',
          refresh: '更新'
        },
        status: {
          running: '実行中',
          completed: '完了',
          terminated: '終了',
          failed: '失敗'
        }
      },
      form: {
        title: {
          create: 'ワークフローノードの作成',
          edit: 'ワークフローノードの編集'
        },
        fields: {
          name: 'ノード名',
          type: 'ノードタイプ',
          description: '説明',
          input: '入力',
          output: '出力',
          error: 'エラー'
        },
        rules: {
          name: {
            required: 'ノード名を入力してください'
          },
          type: {
            required: 'ノードタイプを選択してください'
          }
        },
        buttons: {
          submit: '送信',
          cancel: 'キャンセル'
        }
      },
      detail: {
        title: 'ワークフローノード詳細',
        basic: {
          title: '基本情報',
          name: 'ノード名',
          type: 'ノードタイプ',
          description: '説明',
          status: 'ステータス',
          startTime: '開始時間',
          endTime: '終了時間',
          duration: '実行時間'
        },
        input: {
          title: '入力情報',
          value: '入力値'
        },
        output: {
          title: '出力情報',
          value: '出力値'
        },
        error: {
          title: 'エラー情報',
          message: 'エラーメッセージ',
          stackTrace: 'スタックトレース'
        },
        actions: {
          back: '戻る'
        }
      }
    }
  }
} 