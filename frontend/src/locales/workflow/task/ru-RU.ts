export default {
  workflow: {
    task: {
      title: 'Задача Рабочего Процесса',
      list: {
        title: 'Список Задач Рабочего Процесса',
        search: {
          name: 'Название Задачи',
          type: 'Тип Задачи',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания'
        },
        table: {
          name: 'Название Задачи',
          type: 'Тип Задачи',
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
          create: 'Создание Задачи Рабочего Процесса',
          edit: 'Редактирование Задачи Рабочего Процесса'
        },
        fields: {
          name: 'Название Задачи',
          type: 'Тип Задачи',
          description: 'Описание',
          input: 'Входные Данные',
          output: 'Выходные Данные',
          error: 'Ошибка'
        },
        rules: {
          name: {
            required: 'Введите название задачи'
          },
          type: {
            required: 'Выберите тип задачи'
          }
        },
        buttons: {
          submit: 'Отправить',
          cancel: 'Отмена'
        }
      },
      detail: {
        title: 'Детали Задачи Рабочего Процесса',
        basic: {
          title: 'Основная Информация',
          name: 'Название Задачи',
          type: 'Тип Задачи',
          description: 'Описание',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания',
          duration: 'Длительность'
        },
        input: {
          title: 'Входные Данные',
          value: 'Значение'
        },
        output: {
          title: 'Выходные Данные',
          value: 'Значение'
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