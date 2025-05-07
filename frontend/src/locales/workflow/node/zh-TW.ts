export default {
  workflow: {
    node: {
      title: '工作流程節點',
      list: {
        title: '工作流程節點列表',
        search: {
          name: '節點名稱',
          type: '節點類型',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間'
        },
        table: {
          name: '節點名稱',
          type: '節點類型',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間',
          duration: '持續時間',
          actions: '操作'
        },
        actions: {
          view: '查看',
          edit: '編輯',
          delete: '刪除',
          refresh: '重新整理'
        },
        status: {
          running: '執行中',
          completed: '已完成',
          terminated: '已終止',
          failed: '已失敗'
        }
      },
      form: {
        title: {
          create: '新建工作流程節點',
          edit: '編輯工作流程節點'
        },
        fields: {
          name: '節點名稱',
          type: '節點類型',
          description: '描述',
          input: '輸入',
          output: '輸出',
          error: '錯誤'
        },
        rules: {
          name: {
            required: '請輸入節點名稱'
          },
          type: {
            required: '請選擇節點類型'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流程節點詳情',
        basic: {
          title: '基本資訊',
          name: '節點名稱',
          type: '節點類型',
          description: '描述',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間',
          duration: '持續時間'
        },
        input: {
          title: '輸入資訊',
          value: '輸入值'
        },
        output: {
          title: '輸出資訊',
          value: '輸出值'
        },
        error: {
          title: '錯誤資訊',
          message: '錯誤訊息',
          stackTrace: '堆疊追蹤'
        },
        actions: {
          back: '返回'
        }
      }
    }
  }
} 