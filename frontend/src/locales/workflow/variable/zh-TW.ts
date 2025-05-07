export default {
  workflow: {
    variable: {
      title: '工作流程變數',
      list: {
        title: '工作流程變數列表',
        search: {
          name: '變數名稱',
          type: '變數類型',
          scope: '作用域',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間'
        },
        table: {
          name: '變數名稱',
          type: '變數類型',
          scope: '作用域',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間',
          duration: '執行時間',
          actions: '操作'
        },
        actions: {
          view: '查看',
          edit: '編輯',
          delete: '刪除',
          refresh: '更新'
        },
        status: {
          running: '執行中',
          completed: '已完成',
          terminated: '已終止',
          failed: '失敗'
        }
      },
      form: {
        title: {
          create: '創建工作流程變數',
          edit: '編輯工作流程變數'
        },
        fields: {
          name: '變數名稱',
          type: '變數類型',
          scope: '作用域',
          description: '描述',
          input: '輸入',
          output: '輸出',
          error: '錯誤'
        },
        rules: {
          name: {
            required: '請輸入變數名稱'
          },
          type: {
            required: '請選擇變數類型'
          },
          scope: {
            required: '請選擇變數作用域'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流程變數詳情',
        basic: {
          title: '基本信息',
          name: '變數名稱',
          type: '變數類型',
          scope: '作用域',
          description: '描述',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間',
          duration: '執行時間'
        },
        input: {
          title: '輸入信息',
          value: '輸入值'
        },
        output: {
          title: '輸出信息',
          value: '輸出值'
        },
        error: {
          title: '錯誤信息',
          message: '錯誤消息',
          stackTrace: '堆棧跟踪'
        },
        actions: {
          back: '返回'
        }
      }
    }
  }
} 