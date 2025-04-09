export default {
  realtime: {
    server: {
      title: '伺服器監控',
      refresh: '重新整理',
      refreshResult: {
        success: '資料重新整理成功',
        failed: '資料重新整理失敗'
      },
      resource: {
        title: '資源使用',
        cpu: 'CPU使用率',
        memory: '記憶體使用率',
        disk: '磁碟使用率'
      },
      system: {
        title: '系統資訊',
        os: '作業系統',
        architecture: '系統架構',
        version: '系統版本',
        processor: {
          name: '處理器',
          count: '處理器核心數',
          unit: '核心'
        },
        startup: {
          time: '系統啟動時間',
          uptime: '系統運行時間',
          day: '天',
          hour: '小時'
        }
      },
      dotnet: {
        title: '.NET 執行環境資訊',
        runtime: {
          version: '.NET 執行環境版本',
          directory: '執行環境目錄'
        },
        clr: {
          version: 'CLR 版本'
        }
      },
      network: {
        title: '網路資訊',
        adapter: '網路卡名稱',
        mac: 'MAC位址',
        ip: {
          address: 'IP位址',
          location: 'IP位置',
          unknown: '未知位置'
        },
        rate: {
          send: '傳送速率',
          receive: '接收速率'
        }
      }
    }
  }
}