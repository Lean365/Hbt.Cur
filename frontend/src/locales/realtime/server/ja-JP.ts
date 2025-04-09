export default {
  realtime: {
    server: {
      title: 'サーバーモニター',
      refresh: '更新',
      refreshResult: {
        success: 'データの更新に成功しました',
        failed: 'データの更新に失敗しました'
      },
      resource: {
        title: 'リソース使用状況',
        cpu: 'CPU使用率',
        memory: 'メモリ使用率',
        disk: 'ディスク使用率'
      },
      system: {
        title: 'システム情報',
        os: 'オペレーティングシステム',
        architecture: 'アーキテクチャ',
        version: 'バージョン',
        processor: {
          name: 'プロセッサ',
          count: 'コア数',
          unit: 'コア'
        },
        startup: {
          time: '起動時刻',
          uptime: '稼働時間',
          day: '日',
          hour: '時間'
        }
      },
      dotnet: {
        title: '.NET Runtime情報',
        runtime: {
          version: '.NET Runtimeバージョン',
          directory: 'ランタイムディレクトリ'
        },
        clr: {
          version: 'CLRバージョン'
        }
      },
      network: {
        title: 'ネットワーク情報',
        adapter: 'アダプター',
        mac: 'MACアドレス',
        ip: {
          address: 'IPアドレス',
          location: '場所',
          unknown: '不明な場所'
        },
        rate: {
          send: '送信速度',
          receive: '受信速度'
        }
      }
    }
  }
}