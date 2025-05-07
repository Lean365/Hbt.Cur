export default {
  workflow: {
    variable: {
      title: 'ワークフロー変数',
      list: {
        title: 'ワークフロー変数一覧',
        search: {
          name: '変数名',
          type: '変数タイプ',
          scope: 'スコープ',
          status: 'ステータス',
          startTime: '開始時間',
          endTime: '終了時間'
        },
        table: {
          name: '変数名',
          type: '変数タイプ',
          scope: 'スコープ',
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
          create: 'ワークフロー変数作成',
          edit: 'ワークフロー変数編集'
        },
        fields: {
          name: '変数名',
          type: '変数タイプ',
          scope: 'スコープ',
          description: '説明',
          input: '入力',
          output: '出力',
          error: 'エラー'
        },
        rules: {
          name: {
            required: '変数名を入力してください'
          },
          type: {
            required: '変数タイプを選択してください'
          },
          scope: {
            required: 'スコープを選択してください'
          }
        },
        buttons: {
          submit: '送信',
          cancel: 'キャンセル'
        }
      },
      detail: {
        title: 'ワークフロー変数詳細',
        basic: {
          title: '基本情報',
          name: '変数名',
          type: '変数タイプ',
          scope: 'スコープ',
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