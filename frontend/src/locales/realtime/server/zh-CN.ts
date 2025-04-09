export default {
  title: '服务器监控',
  refresh: '刷新',
  refreshResult: {
    success: '数据刷新成功',
    failed: '数据刷新失败'
  },
  resource: {
    title: '资源使用',
    cpu: 'CPU使用率',
    memory: '内存使用率',
    disk: '磁盘使用情况'
  },
  system: {
    title: '系统信息',
    os: '操作系统',
    architecture: '系统架构',
    version: '系统版本',
    processor: {
      name: '处理器',
      count: '处理器核心数',
      unit: '核'
    },
    startup: {
      time: '系统启动时间',
      uptime: '系统运行时间',
      day: '天',
      hour: '小时'
    }
  },
  dotnet: {
    title: '.NET运行时信息',
    runtime: {
      version: '.NET运行时版本',
      directory: '运行时目录'
    },
    clr: {
      version: 'CLR版本'
    }
  },
  network: {
    title: '网络信息',
    adapter: '网卡名称',
    mac: 'MAC地址',
    ip: {
      address: 'IP地址',
      location: 'IP位置',
      unknown: '未知位置'
    }
  }
} 