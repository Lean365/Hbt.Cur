export default {
  identity: {
    user: {
      title: 'Управление пользователями',
      table: {
        columns: {
          userId: 'ID пользователя',
          tenantId: 'Арендатор',
          userName: 'Имя пользователя',
          nickName: 'Псевдоним',
          englishName: 'Английское имя',
          userType: 'Тип',
          email: 'Электронная почта',
          phoneNumber: 'Номер телефона',
          gender: 'Пол',
          avatar: 'Аватар',
          status: 'Статус',
          lastPasswordChangeTime: 'Последнее изменение пароля',
          lockEndTime: 'Время окончания блокировки',
          lockReason: 'Причина блокировки',
          isLock: 'Заблокирован',
          errorLimit: 'Лимит ошибок',
          loginCount: 'Количество входов',
          deptName: 'Отдел',
          role: 'Роль',
          createBy: 'Создатель',
          createTime: 'Время создания',
          updateBy: 'Обновил',
          updateTime: 'Время обновления',
          deleteBy: 'Удалил',
          deleteTime: 'Время удаления',
          isDeleted: 'Удалено',
          remark: 'Примечание',
          operation: 'Операция'
        },
        operation: {
          edit: 'Редактировать',
          delete: 'Удалить',
          resetPassword: 'Сбросить пароль'
        },
        status: {
          enabled: 'Включен',
          disabled: 'Отключен',
          toggle: {
            enable: 'Включить',
            disable: 'Отключить'
          }
        }
      },
      fields: {
        userId: 'ID пользователя',
        tenantId: {
          label: 'Арендатор',
          placeholder: 'Выберите арендатора',
          validation: {
            required: 'Арендатор обязателен'
          }
        },
        userName: {
          label: 'Имя пользователя',
          placeholder: 'Введите имя пользователя',
          validation: {
            required: 'Имя пользователя обязательно',
            format: 'Имя пользователя должно начинаться с маленькой буквы, иметь длину 6-20 символов и содержать только маленькие буквы, цифры и подчеркивания'
          }
        },
        nickName: {
          label: 'Псевдоним',
          placeholder: 'Введите псевдоним',
          validation: {
            required: 'Псевдоним обязателен',
            format: 'Псевдоним должен иметь длину 2-20 символов и содержать только китайские символы, английские буквы, цифры и подчеркивания'
          }
        },
        englishName: {
          label: 'Английское имя',
          placeholder: 'Введите английское имя',
          validation: {
            format: 'Английское имя должно иметь длину 2-50 символов и содержать только английские буквы, пробелы и дефисы'
          }
        },
        userType: {
          label: 'Тип',
          placeholder: 'Выберите тип пользователя',
          options: {
            admin: 'Администратор',
            user: 'Обычный пользователь'
          }
        },
        email: {
          label: 'Электронная почта',
          placeholder: 'Введите электронную почту',
          validation: {
            required: 'Электронная почта обязательна',
            invalid: 'Электронная почта должна иметь длину 6-100 символов и быть в правильном формате'
          }
        },
        phoneNumber: {
          label: 'Номер телефона',
          placeholder: 'Введите номер телефона',
          validation: {
            required: 'Номер телефона обязателен',
            invalid: 'Введите правильный формат мобильного или стационарного телефона'
          }
        },
        gender: {
          label: 'Пол',
          placeholder: 'Выберите пол',
          options: {
            male: 'Мужской',
            female: 'Женский',
            unknown: 'Неизвестно'
          }
        },
        avatar: {
          label: 'Аватар',
          upload: 'Загрузить аватар',
          uploadSuccess: 'Аватар успешно загружен',
          uploadError: 'Ошибка загрузки аватара'
        },
        status: {
          label: 'Статус',
          placeholder: 'Выберите статус',
          options: {
            enabled: 'Включен',
            disabled: 'Отключен'
          }
        },
        deptName: {
          label: 'Отдел',
          placeholder: 'Выберите отдел',
          validation: {
            required: 'Отдел обязателен'
          }
        },
        role: {
          label: 'Роль',
          placeholder: 'Выберите роль',
          validation: {
            required: 'Роль обязательна'
          }
        },
        post: {
          label: 'Должность',
          placeholder: 'Выберите должность',
          validation: {
            required: 'Должность обязательна'
          }
        },
        remark: {
          label: 'Примечание',
          placeholder: 'Введите примечание'
        }
      },
      messages: {
        confirmDelete: 'Вы уверены, что хотите удалить выбранного пользователя?',
        confirmResetPassword: 'Вы уверены, что хотите сбросить пароль выбранного пользователя?',
        confirmToggleStatus: 'Вы уверены, что хотите {action} этого пользователя?',
        deleteSuccess: 'Пользователь успешно удален',
        deleteFailed: 'Не удалось удалить пользователя',
        saveSuccess: 'Информация о пользователе успешно сохранена',
        saveFailed: 'Не удалось сохранить информацию о пользователе',
        resetPasswordSuccess: 'Пароль успешно сброшен',
        resetPasswordFailed: 'Не удалось сбросить пароль',
        importSuccess: 'Пользователь успешно импортирован',
        importFailed: 'Не удалось импортировать пользователя',
        exportSuccess: 'Пользователь успешно экспортирован',
        exportFailed: 'Не удалось экспортировать пользователя',
        toggleStatusSuccess: 'Статус успешно изменен',
        toggleStatusFailed: 'Не удалось изменить статус'
      },
      tab: {
        basic: 'Основная информация',
        profile: 'Профиль',
        role: 'Роли и права',
        dept: 'Отдел и должность',
        other: 'Другая информация',
        avatar: 'Настройки аватара',
        loginLog: 'Журнал входа',
        operateLog: 'Журнал операций',
        errorLog: 'Журнал ошибок',
        taskLog: 'Журнал задач'
      },
      update: {
        validation: {
          required: 'ID пользователя и ID арендатора обязательны'
        }
      },
      import: {
        title: 'Импорт пользователей',
        template: 'Скачать шаблон',
        success: 'Импорт успешно выполнен',
        failed: 'Ошибка импорта'
      },
      export: {
        title: 'Экспорт пользователей',
        success: 'Экспорт успешно выполнен',
        failed: 'Ошибка экспорта'
      },
      resetPwd: 'Сбросить пароль'
    }
  },
  actions: {
    add: 'Добавить пользователя',
    edit: 'Редактировать пользователя',
    delete: 'Удалить пользователя',
    resetPassword: 'Сбросить пароль',
    export: 'Экспортировать пользователей'
  },
  rules: {
    userName: 'Имя пользователя обязательно',
    nickName: 'Псевдоним обязателен',
    phoneNumber: 'Введите правильный номер телефона',
    email: 'Введите правильный адрес электронной почты'
  }
}
