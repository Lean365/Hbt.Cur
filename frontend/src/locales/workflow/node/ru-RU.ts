export default {
  workflow: {
    node: {
      title: 'Узел Рабочего Процесса',
      list: {
        title: 'Список Узлов Рабочего Процесса',
        search: {
          name: 'Название Узла',
          type: 'Тип Узла',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания'
        },
        table: {
          name: 'Название Узла',
          type: 'Тип Узла',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания',
          duration: 'Длительность',
          actions: 'Действия'
        },
        actions: {
          view: 'Просмотр',
          edit: 'Редактировать',
          delete: 'Удалить',
          refresh: 'Обновить'
        },
        status: {
          running: 'Выполняется',
          completed: 'Завершено',
          terminated: 'Прервано',
          failed: 'Ошибка'
        }
      },
      form: {
        title: {
          create: 'Создание Узла Рабочего Процесса',
          edit: 'Редактирование Узла Рабочего Процесса'
        },
        fields: {
          name: 'Название Узла',
          type: 'Тип Узла',
          description: 'Описание',
          input: 'Вход',
          output: 'Выход',
          error: 'Ошибка'
        },
        rules: {
          name: {
            required: 'Пожалуйста, введите название узла'
          },
          type: {
            required: 'Пожалуйста, выберите тип узла'
          }
        },
        buttons: {
          submit: 'Отправить',
          cancel: 'Отмена'
        }
      },
      detail: {
        title: 'Детали Узла Рабочего Процесса',
        basic: {
          title: 'Основная Информация',
          name: 'Название Узла',
          type: 'Тип Узла',
          description: 'Описание',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания',
          duration: 'Длительность'
        },
        input: {
          title: 'Информация о Входе',
          value: 'Значение Входа'
        },
        output: {
          title: 'Информация о Выходе',
          value: 'Значение Выхода'
        },
        error: {
          title: 'Информация об Ошибке',
          message: 'Сообщение об Ошибке',
          stackTrace: 'Трассировка Стека'
        },
        actions: {
          back: 'Назад'
        }
      }
    }
  }
} 