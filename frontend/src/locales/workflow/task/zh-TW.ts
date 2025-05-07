export default {
  workflow: {
    task: {
      title: '工作流程任務',
      list: {
        title: '工作流程任務列表',
        search: {
          name: '任務名稱',
          type: '任務類型',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間'
        },
        table: {
          name: '任務名稱',
          type: '任務類型',
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
          refresh: '刷新'
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
          create: '創建工作流程任務',
          edit: '編輯工作流程任務'
        },
        fields: {
          name: '任務名稱',
          type: '任務類型',
          description: '描述',
          input: '輸入',
          output: '輸出',
          error: '錯誤'
        },
        rules: {
          name: {
            required: '請輸入任務名稱'
          },
          type: {
            required: '請選擇任務類型'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流程任務詳情',
        basic: {
          title: '基本信息',
          name: '任務名稱',
          type: '任務類型',
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