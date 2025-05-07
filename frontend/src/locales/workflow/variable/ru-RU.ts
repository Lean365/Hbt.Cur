export default {
  workflow: {
    variable: {
      title: 'Переменная Рабочего Процесса',
      list: {
        title: 'Список Переменных Рабочего Процесса',
        search: {
          name: 'Название Переменной',
          type: 'Тип Переменной',
          scope: 'Область',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания'
        },
        table: {
          name: 'Название Переменной',
          type: 'Тип Переменной',
          scope: 'Область',
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
          create: 'Создание Переменной Рабочего Процесса',
          edit: 'Редактирование Переменной Рабочего Процесса'
        },
        fields: {
          name: 'Название Переменной',
          type: 'Тип Переменной',
          scope: 'Область',
          description: 'Описание',
          input: 'Входные Данные',
          output: 'Выходные Данные',
          error: 'Ошибка'
        },
        rules: {
          name: {
            required: 'Введите название переменной'
          },
          type: {
            required: 'Выберите тип переменной'
          },
          scope: {
            required: 'Выберите область переменной'
          }
        },
        buttons: {
          submit: 'Отправить',
          cancel: 'Отмена'
        }
      },
      detail: {
        title: 'Детали Переменной Рабочего Процесса',
        basic: {
          title: 'Основная Информация',
          name: 'Название Переменной',
          type: 'Тип Переменной',
          scope: 'Область',
          description: 'Описание',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания',
          duration: 'Длительность'
        },
        input: {
          title: 'Входные Данные',
          value: 'Значение Входа'
        },
        output: {
          title: 'Выходные Данные',
          value: 'Значение Выхода'
        },
        error: {
          title: 'Информация об Ошибке',
          message: 'Сообщение об Ошибке',
          stackTrace: 'Стек Вызовов'
        },
        actions: {
          back: 'Назад'
        }
      }
    }
  }
} 