export default {
  realtime: {
    server: {
      title: 'Server Monitor',
      refresh: 'Refresh',
      refreshResult: {
        success: 'Data refreshed successfully',
        failed: 'Failed to refresh data'
      },
      resource: {
        title: 'Resource Usage',
        cpu: 'CPU Usage',
        memory: 'Memory Usage',
        disk: 'Disk Usage'
      },
      system: {
        title: 'System Information',
        os: 'Operating System',
        architecture: 'Architecture',
        version: 'Version',
        processor: {
          name: 'Processor',
          count: 'Processor Cores',
          unit: 'Cores'
        },
        startup: {
          time: 'System Start Time',
          uptime: 'System Uptime',
          day: 'Days',
          hour: 'Hours'
        }
      },
      dotnet: {
        title: '.NET Runtime Information',
        runtime: {
          version: '.NET Runtime Version',
          directory: 'Runtime Directory'
        },
        clr: {
          version: 'CLR Version'
        }
      },
      network: {
        title: 'Network Information',
        adapter: 'Adapter Name',
        mac: 'MAC Address',
        ip: {
          address: 'IP Address',
          location: 'IP Location',
          unknown: 'Unknown Location'
        },
        rate: {
          send: 'Send Rate',
          receive: 'Receive Rate'
        }
      }
    }
  }
} 