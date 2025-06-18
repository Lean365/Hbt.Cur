export default {
  menu: {
    home: 'Главная',
    dashboard: {
      title: 'Панель',
      workplace: 'Рабочее место',
      analysis: 'Аналитика',
      monitor: 'Мониторинг'
    },
    components: {
      title: 'Компоненты',
      icons: 'Иконки'
    },
    about: {
      title: 'О нас',
      privacy: 'Политика конфиденциальности',
      terms: 'Условия обслуживания',
      index: 'О Hbt'
    },
    core: {
      _self: 'Ядро управление',
      config: 'Конфигурация системы',
      language: 'Управление языками',
      dict: 'Управление словарями',
    },
    identity: {
      _self: 'Идентификация',
      user: 'Управление пользователями',
      role: 'Управление ролями',
      dept: 'Управление отделами',
      post: 'Управление должностями',
      menu: 'Управление меню',
      tenant: 'Управление арендаторами',
      oauth: 'OAuth управление',
      profile: 'Профиль',
      changePassword: 'Сменить пароль'
    },
    audit: {
      _self: 'Аудит',
      operlog: 'Журнал операций',
      loginlog: 'Журнал входа',
      sqldifflog: 'Журнал различий SQL',
      exceptionlog: 'Журнал исключений',
      auditlog: 'Журнал аудита',
      quartzlog: 'Журнал задач',
      server: 'Мониторинг сервера'
    },
    workflow: {
      _self: 'Рабочий процесс',
      form: 'Форма',
      definition: 'Определение',
      instance: 'Экземпляр',
      task: 'Задача',
      node: 'Узел',
      variable: 'Переменная',
      history: 'История'
    },
    signalr: {
      _self: 'Реальное время',
      online: 'Пользователи',
      message: 'Сообщения'
    },
    generator: {
      _self: 'Генератор кода',
      table: 'Таблица БД',
      tableDefine: 'Столбец таблицы',
      template: 'Шаблон кода',
      config: 'Конфигурация генератора',
      api: 'API Документация'
    },
    routine: {
      _self: 'Рутина',
      vehicle: {
        _self: 'Управление транспортом',
        vehicleMaster: {
          _self: 'Основные данные транспорта',
          vehicleInfo: 'Информация о транспорте',
          driverInfo: 'Информация о водителе',
          maintenance: 'Обслуживание'
        },
        vehicleBooking: {
          _self: 'Бронирование транспорта',
          newBooking: 'Новое бронирование',
          bookingList: 'Список бронирований',
          bookingApproval: 'Утверждение бронирования'
        },
        vehicleDispatch: {
          _self: 'Диспетчеризация транспорта',
          dispatchPlan: 'План диспетчеризации',
          realTimeTracking: 'Отслеживание в реальном времени',
          dispatchHistory: 'История диспетчеризации'
        },
        vehicleReporting: {
          _self: 'Отчеты по транспорту',
          usageReport: 'Отчет об использовании',
          costReport: 'Отчет о расходах',
          maintenanceReport: 'Отчет об обслуживании'
        }
      },
      file: 'Управление файлами',
      mail: 'Управление почтой',
      mailTmpl: 'Шаблон письма',
      meeting: {
        _self: 'Управление встречами',
        meetingRoom: {
          _self: 'Управление комнатами',
          roomInfo: 'Информация о комнате',
          roomBooking: 'Бронирование комнаты',
          roomSchedule: 'Расписание комнаты'
        },
        meetingPlan: {
          _self: 'План встреч',
          newMeeting: 'Новая встреча',
          meetingList: 'Список встреч',
          meetingApproval: 'Утверждение встречи'
        },
        meetingExecution: {
          _self: 'Проведение встречи',
          attendance: 'Посещение',
          minutes: 'Протокол',
          followUp: 'Последующие действия'
        },
        meetingReporting: {
          _self: 'Отчеты по встречам',
          meetingReport: 'Отчет о встрече',
          attendanceReport: 'Отчет о посещении',
          costReport: 'Отчет о расходах'
        }
      },
      notice: 'Уведомления',
      schedule: 'Расписание',
      quartz: 'Задача'
    },
    finance: {
      _self: 'Финансы',
      accounting: {
        _self: 'Управленческий учет',
        companyaccounts: 'Счета компании',
        glaccount: 'Главная книга',
        generalledger: 'Главная книга',
        payable: 'К оплате',
        receivable: 'К получению',
        asset: 'Активы',
        bank: 'Банк',
        tax: 'Налоги',
        planning: 'Планирование',
        reporting: 'Отчетность'
      },
      controlling: {
        _self: 'Контроллинг',
        costelement: 'Элемент затрат',
        costcenter: 'Центр затрат',
        profitcenter: 'Центр прибыли',
        accountsReceivable: 'Дебиторская задолженность',
        accountsPayable: 'Кредиторская задолженность',
        assetAccounting: 'Учет активов',
        tax: 'Управление налогами',
        financialReporting: 'Финансовая отчетность'
      }
    },
    logistics: {
      _self: 'Логистика',
      equipment: {
        _self: 'Управление оборудованием',
        data: 'Данные оборудования',
        location: 'Местоположение оборудования',
        material: 'Связь с материалом',
        workorder: 'Рабочий заказ'
      },
      material: {
        _self: 'Управление материалами',
        info: 'Данные о материале',
        factory: 'Материал завода',
        vendor: 'Поставщик',
        supplier: 'Поставщик',
        price: 'Цена материала',
        requisition: 'Заявка на закупку',
        order: 'Заказ на закупку'
      },
      production: {
        _self: 'Управление производством',
        bom: 'BOM',
        routing: 'Маршрут',
        change: 'Инженерные изменения',
        workcenter: 'Рабочий центр',
        order: 'Производственный заказ',
        kanban: 'Канбан'
      },
      project: {
        _self: 'Управление проектами',
        define: 'Определение проекта',
        cost: 'План затрат',
        resource: 'План ресурсов',
        schedule: 'Планирование'
      },
      quality: {
        _self: 'Управление качеством',
        item: 'Пункт проверки',
        receiving: 'Входной контроль',
        process: 'Процессный контроль',
        storage: 'Складской контроль',
        return: 'Контроль возврата'
      },
      sales: {
        _self: 'Управление продажами',
        customer: 'Клиент',
        client: 'Клиент',
        price: 'Цена продажи',
        order: 'Заказ на продажу'
      },
      service: {
        _self: 'Сервисное обслуживание',
        item: 'Сервисный элемент',
        contract: 'Сервисный контракт',
        request: 'Сервисный запрос',
        workorder: 'Сервисный заказ',
        timesheet: 'Табель учета рабочего времени',
        consumption: 'Потребление материалов',
        outsourcing: 'Аутсорсинг'
      },
      complaint: {
        _self: 'Управление жалобами',
        notice: 'Уведомление о качестве',
        mark: 'Детали жалобы',
        analysis: 'Анализ причин',
        corrective: 'Корректирующее действие',
        return: 'Выполнение возврата',
        followUp: 'Последующие действия'
      }
    },
    humanResources: {
      _self: 'Управление персоналом',
      employeeManagement: {
        _self: 'Управление сотрудниками',
        employeeMaster: 'Основные данные сотрудника',
        attendance: 'Посещаемость',
        leave: 'Отпуск',
        payroll: 'Зарплата',
        contractManagement: 'Управление контрактами'
      },
      recruitment: {
        _self: 'Управление наймом',
        jobPosting: 'Публикация вакансий',
        candidateManagement: 'Управление кандидатами',
        interviewScheduling: 'Планирование собеседований',
        offerManagement: 'Управление предложениями'
      },
      training: {
        _self: 'Управление обучением',
        trainingPlan: 'План обучения',
        trainingExecution: 'Проведение обучения',
        trainingEvaluation: 'Оценка обучения'
      },
      performance: {
        _self: 'Управление эффективностью',
        goalSetting: 'Постановка целей',
        performanceReview: 'Оценка эффективности',
        feedback: 'Обратная связь'
      },
      reporting: {
        _self: 'Отчетность по персоналу',
        employeeReports: 'Отчеты по сотрудникам',
        attendanceReports: 'Отчеты по посещаемости',
        payrollReports: 'Отчеты по зарплате',
        performanceReports: 'Отчеты по эффективности'
      }
    }
  }
}
