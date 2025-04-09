export default {
  realtime: {
    server: {
      title: 'Monitor del Servidor',
      refresh: 'Actualizar',
      refreshResult: {
        success: 'Datos actualizados con éxito',
        failed: 'Error al actualizar datos'
      },
      resource: {
        title: 'Uso de Recursos',
        cpu: 'Uso de CPU',
        memory: 'Uso de Memoria',
        disk: 'Uso de Disco'
      },
      system: {
        title: 'Información del Sistema',
        os: 'Sistema Operativo',
        architecture: 'Arquitectura',
        version: 'Versión',
        processor: {
          name: 'Procesador',
          count: 'Núcleos',
          unit: 'núcleos'
        },
        startup: {
          time: 'Hora de Inicio',
          uptime: 'Tiempo de Actividad',
          day: 'días',
          hour: 'horas'
        }
      },
      dotnet: {
        title: 'Información de .NET Runtime',
        runtime: {
          version: 'Versión de .NET Runtime',
          directory: 'Directorio de Runtime'
        },
        clr: {
          version: 'Versión de CLR'
        }
      },
      network: {
        title: 'Información de Red',
        adapter: 'Adaptador',
        mac: 'Dirección MAC',
        ip: {
          address: 'Dirección IP',
          location: 'Ubicación',
          unknown: 'Ubicación desconocida'
        },
        rate: {
          send: 'Velocidad de envío',
          receive: 'Velocidad de recepción'
        }
      }
    }
  }
}