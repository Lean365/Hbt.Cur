export default {
  menu: {
    home: 'Главная',
    dashboard: {
      title: 'Панель управления',
      workplace: 'Рабочее место',
      analysis: 'Анализ',
      monitor: 'Мониторинг'
    },
    components: {
      title: 'Компоненты',
      icons: 'Иконки'
    },
    about: {
      title: 'О нас',
      privacy: 'Политика конфиденциальности',
      terms: 'Условия использования',
      index: 'О Hbt'
    },
    admin: {
      _self: 'Системное администрирование',
      config: 'Настройки системы',
      language: 'Управление языками',
      dict: 'Типы словарей',

    },
    identity: {
      _self: 'Управление идентификацией',
      user: 'Управление пользователями',
      role: 'Управление ролями',
      dept: 'Управление отделами',
      post: 'Управление должностями',
      menu: 'Управление меню',
      tenant: 'Управление арендаторами',
      oauth: 'Управление OAuth'
    },
    audit: {
      _self: 'Журналы аудита',
      operlog: 'Журналы операций',
      loginlog: 'Журналы входов',
      dbdifflog: 'Журналы различий базы данных',
      exceptionlog: 'Журналы исключений'
    },
    workflow: {
      _self: 'Рабочий процесс',
      definition: 'Определение процесса',
      instance: 'Экземпляр процесса',
      task: 'Задачи',
      node: 'Узлы',
      variable: 'Переменные',
      history: 'История'
    },
    signalr: {
      _self: 'Мониторинг в реальном времени',
      server: 'Мониторинг сервера',
      online: 'Онлайн пользователи',
      message: 'Онлайн сообщения'
    },
    generator: {
      _self: 'Генератор кода',
      table: 'Таблицы базы данных',
      tableDefine: 'Пользовательские таблицы',
      template: 'Шаблоны кода',
      config: 'Настройки генерации',
      api: 'Документация API'
    },
    routine: {
      _self: 'Ежедневная работа',
      file: 'Управление файлами',
      mail: 'Управление почтой',
      mailTmpl: 'Шаблоны писем',
      notice: 'Уведомления',
      task: 'Задачи',
      schedule: 'Управление расписанием'
    },
    finance: {
      _self: 'Финансы',
      management: {
        _self: 'Управленческий учет',
        cost: {
          _self: 'Управление затратами',
          costFactors: 'Факторы затрат',
          costCenter: 'Центр затрат',
          profitCenter: 'Центр прибыли',
          productCost: 'Стоимость продукта',
          activityType: 'Тип деятельности',
          internalOrder: 'Внутренние заказы'
        },
        planning: {
          _self: 'Управление планированием',
          costPlanning: 'Планирование затрат',
          profitPlanning: 'Планирование прибыли',
          budgetControl: 'Контроль бюджета'
        },
        reporting: {
          _self: 'Отчеты и анализ',
          costReports: 'Отчеты по затратам',
          profitReports: 'Отчеты по прибыли',
          varianceAnalysis: 'Анализ отклонений'
        }
      },
      financial: {
        _self: 'Финансовый учет',
        generalLedger: {
          _self: 'Главная книга',
          account: 'Счета',
          accountType: 'Типы счетов',
          journalEntry: 'Бухгалтерские проводки',
          reconciliation: 'Сверка',
          closing: 'Периодическое закрытие'
        },
        accountsReceivable: {
          _self: 'Дебиторская задолженность',
          customer: 'Управление клиентами',
          invoice: 'Счета клиентов',
          payment: 'Платежи клиентов',
          creditControl: 'Контроль кредита'
        },
        accountsPayable: {
          _self: 'Кредиторская задолженность',
          supplier: 'Управление поставщиками',
          invoice: 'Счета поставщиков',
          payment: 'Платежи поставщикам',
          agingReport: 'Отчеты по срокам задолженности'
        },
        assetAccounting: {
          _self: 'Учет активов',
          assets: 'Основные средства',
          depreciation: 'Управление амортизацией',
          assetTransfer: 'Передача активов',
          assetRetirement: 'Списание активов'
        },
        tax: {
          _self: 'Налоговое управление',
          taxCodes: 'Налоговые коды',
          taxReporting: 'Налоговые отчеты',
          taxPayments: 'Налоговые платежи'
        },
        financialReporting: {
          _self: 'Финансовая отчетность',
          balanceSheet: 'Баланс',
          profitAndLoss: 'Отчет о прибылях и убытках',
          cashFlow: 'Отчет о движении денежных средств'
        }
      }
    },
    logistics: {
      _self: 'Логистика',
      sales: {
        _self: 'Управление продажами',
        customer: {
          _self: 'Управление клиентами',
          client: 'Клиенты',
          customers: 'Список клиентов',
          creditControl: 'Контроль кредита'
        },
        order: {
          _self: 'Управление заказами',
          order: 'Заказы на продажу',
          orderDetail: 'Детали заказа',
          orderTracking: 'Отслеживание заказов'
        },
        delivery: {
          _self: 'Управление доставкой',
          delivery: 'Документы доставки',
          deliveryDetail: 'Детали доставки',
          shipping: 'Управление перевозками'
        },
        billing: {
          _self: 'Управление выставлением счетов',
          invoice: 'Управление счетами',
          invoiceDetail: 'Детали счетов',
          payment: 'Управление платежами'
        },
        reporting: {
          _self: 'Отчеты и анализ',
          salesReports: 'Отчеты по продажам',
          performanceAnalysis: 'Анализ производительности'
        }
      },
      production: {
        _self: 'Управление производством',
        bom: 'Спецификация материалов (BOM)',
        routing: 'Маршрутизация',
        workOrder: {
          _self: 'Производственные заказы',
          create: 'Создать производственный заказ',
          manage: 'Управление производственными заказами',
          release: 'Выпуск производственных заказов',
          complete: 'Завершение производственных заказов'
        },
        capacityPlanning: {
          _self: 'Планирование мощностей',
          workCenter: 'Рабочие центры',
          capacityEvaluation: 'Оценка мощностей',
          capacityLeveling: 'Выравнивание мощностей'
        },
        productionScheduling: {
          _self: 'Планирование производства',
          schedule: 'Планировать',
          reschedule: 'Перепланировать'
        },
        productionExecution: {
          _self: 'Исполнение производства',
          confirm: 'Подтвердить производство',
          goodsIssue: 'Выдача материалов',
          goodsReceipt: 'Приемка материалов'
        },
        productionReporting: {
          _self: 'Отчеты по производству',
          orderReports: 'Отчеты по заказам',
          capacityReports: 'Отчеты по мощностям',
          efficiencyReports: 'Отчеты по эффективности'
        },
        qualityManagement: {
          _self: 'Управление качеством',
          inspectionLot: 'Партии инспекций',
          resultsRecording: 'Запись результатов',
          defectRecording: 'Запись дефектов'
        }
      },
      material: {
        _self: 'Управление материалами',
        materialMaster: 'Основные данные материалов',
        materialCategory: 'Категории материалов',
        materialUnit: 'Единицы материалов',
        materialStock: {
          _self: 'Запасы материалов',
          stockOverview: 'Обзор запасов',
          stockIn: 'Поступление материалов',
          stockOut: 'Выдача материалов',
          stockTransfer: 'Перемещение запасов',
          stockAdjustment: 'Корректировка запасов',
          stockCheck: 'Проверка запасов'
        },
        purchase: {
          _self: 'Управление закупками',
          purchaseRequisition: 'Заявки на закупку',
          purchaseOrder: 'Заказы на закупку',
          purchaseOrderDetail: 'Детали заказов на закупку',
          supplier: 'Управление поставщиками'
        },
        inventoryManagement: {
          _self: 'Управление запасами',
          goodsReceipt: 'Приемка товаров',
          goodsIssue: 'Выдача товаров',
          transferPosting: 'Перемещение запасов',
          stockOverview: 'Обзор запасов'
        },
        valuation: {
          _self: 'Оценка материалов',
          priceControl: 'Контроль цен',
          standardPrice: 'Стандартная цена',
          movingAveragePrice: 'Средневзвешенная цена'
        },
        reporting: {
          _self: 'Отчеты и анализ',
          stockReports: 'Отчеты по запасам',
          purchaseReports: 'Отчеты по закупкам',
          inventoryReports: 'Отчеты по анализу запасов'
        }
      }
    },
    quality: {
      _self: 'Управление качеством',
      inspection: {
        _self: 'Управление инспекциями',
        inspectionLot: 'Партии инспекций',
        resultsRecording: 'Запись результатов',
        defectRecording: 'Запись дефектов',
        usageDecision: 'Решение о применении'
      },
      qualityPlanning: {
        _self: 'Планирование качества',
        inspectionPlan: 'Планы инспекций',
        qualityInfoRecord: 'Записи информации о качестве',
        samplingProcedure: 'Процедуры выборки'
      },
      qualityControl: {
        _self: 'Контроль качества',
        controlChart: 'Контрольные диаграммы',
        qualityNotifications: 'Уведомления о качестве',
        correctiveActions: 'Корректирующие действия'
      },
      qualityReporting: {
        _self: 'Отчеты по качеству',
        inspectionReports: 'Отчеты по инспекциям',
        defectReports: 'Отчеты по дефектам',
        qualityAnalysis: 'Анализ качества'
      }
    },
    service: {
      _self: 'Обслуживание клиентов',
      serviceOrder: {
        _self: 'Заказы на обслуживание',
        create: 'Создать заказ на обслуживание',
        manage: 'Управление заказами на обслуживание',
        complete: 'Завершить заказы на обслуживание',
        cancel: 'Отменить заказы на обслуживание'
      },
      serviceContract: {
        _self: 'Контракты на обслуживание',
        create: 'Создать контракт на обслуживание',
        manage: 'Управление контрактами на обслуживание',
        renew: 'Продлить контракты на обслуживание',
        terminate: 'Расторгнуть контракты на обслуживание'
      },
      customerInteraction: {
        _self: 'Взаимодействие с клиентами',
        inquiries: 'Запросы клиентов',
        complaints: 'Жалобы клиентов',
        feedback: 'Отзывы клиентов'
      },
      serviceExecution: {
        _self: 'Исполнение обслуживания',
        schedule: 'Планирование обслуживания',
        dispatch: 'Отправка обслуживания',
        execution: 'Исполнение обслуживания',
        confirmation: 'Подтверждение обслуживания'
      },
      serviceReporting: {
        _self: 'Отчеты по обслуживанию',
        orderReports: 'Отчеты по заказам на обслуживание',
        contractReports: 'Отчеты по контрактам на обслуживание',
        performanceReports: 'Отчеты по производительности'
      }
    },
    equipment: {
      _self: 'Управление оборудованием',
      equipmentMaster: 'Основные данные оборудования',
      maintenancePlanning: {
        _self: 'Планирование обслуживания',
        preventiveMaintenance: 'Профилактическое обслуживание',
        maintenanceTaskList: 'Список задач обслуживания',
        scheduling: 'Планирование обслуживания'
      },
      maintenanceExecution: {
        _self: 'Исполнение обслуживания',
        workOrder: 'Заказы на обслуживание',
        confirmation: 'Подтверждение обслуживания',
        breakdownMaintenance: 'Ремонт при поломке'
      },
      maintenanceReporting: {
        _self: 'Отчеты по обслуживанию',
        equipmentReports: 'Отчеты по оборудованию',
        maintenanceHistory: 'История обслуживания',
        performanceAnalysis: 'Анализ производительности'
      },
      sparePartsManagement: {
        _self: 'Управление запасными частями',
        sparePartsInventory: 'Запасы запасных частей',
        sparePartsProcurement: 'Закупка запасных частей',
        sparePartsUsage: 'Использование запасных частей'
      }
    },
    humanResources: {
      _self: 'Управление персоналом',
      employeeManagement: {
        _self: 'Управление сотрудниками',
        employeeMaster: 'Основные данные сотрудников',
        attendance: 'Управление посещаемостью',
        leave: 'Управление отпусками',
        payroll: 'Управление заработной платой',
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
        trainingPlan: 'Планы обучения',
        trainingExecution: 'Исполнение обучения',
        trainingEvaluation: 'Оценка обучения'
      },
      performance: {
        _self: 'Управление производительностью',
        goalSetting: 'Постановка целей',
        performanceReview: 'Оценка производительности',
        feedback: 'Управление обратной связью'
      },
      reporting: {
        _self: 'Отчеты по персоналу',
        employeeReports: 'Отчеты по сотрудникам',
        attendanceReports: 'Отчеты по посещаемости',
        payrollReports: 'Отчеты по заработной плате',
        performanceReports: 'Отчеты по производительности'
      }
    }
  }
}
