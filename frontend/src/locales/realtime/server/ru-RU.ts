export default {
  realtime: {
    server: {
      title: 'Мониторинг Сервера',
      refresh: 'Обновить',
      refreshResult: {
        success: 'Данные успешно обновлены',
        failed: 'Ошибка обновления данных'
      },
      resource: {
        title: 'Использование Ресурсов',
        cpu: 'Использование ЦП',
        memory: 'Использование Памяти',
        disk: 'Использование Диска'
      },
      system: {
        title: 'Информация о Системе',
        os: 'Операционная Система',
        architecture: 'Архитектура',
        version: 'Версия',
        processor: {
          name: 'Процессор',
          count: 'Количество ядер',
          unit: 'ядер'
        },
        startup: {
          time: 'Время запуска',
          uptime: 'Время работы',
          day: 'дней',
          hour: 'часов'
        }
      },
      dotnet: {
        title: 'Информация о .NET Runtime',
        runtime: {
          version: 'Версия .NET Runtime',
          directory: 'Директория Runtime'
        },
        clr: {
          version: 'Версия CLR'
        }
      },
      network: {
        title: 'Информация о Сети',
        adapter: 'Адаптер',
        mac: 'MAC Адрес',
        ip: {
          address: 'IP Адрес',
          location: 'Местоположение',
          unknown: 'Неизвестное местоположение'
        },
        rate: {
          send: 'Скорость отправки',
          receive: 'Скорость получения'
        }
      }
    }
  }
}