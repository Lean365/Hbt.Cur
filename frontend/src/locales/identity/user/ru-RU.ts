export default {
  identity: {
    user: {
      title: 'Управление пользователями',
      toolbar: {
        add: 'Добавить пользователя',
        edit: 'Редактировать пользователя',
        delete: 'Удалить пользователя',
        import: 'Импортировать пользователей',
        export: 'Экспортировать пользователей',
        resetPassword: 'Сбросить пароль',
        downloadTemplate: 'Скачать шаблон'
      },
      table: {
        columns: {
          userName: 'Имя пользователя',
          nickName: 'Псевдоним',
          deptName: 'Отдел',
          role: 'Роль',
          email: 'Электронная почта',
          phoneNumber: 'Номер телефона',
          gender: 'Пол',
      status: 'Статус',
          createTime: 'Дата создания',
          lastLoginTime: 'Последний вход',
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
      userId: 'ID пользователя',
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
        label: 'Имя на английском',
        placeholder: 'Введите имя на английском',
        validation: {
          format: 'Имя на английском должно иметь длину 2-50 символов и содержать только английские буквы, пробелы и дефисы'
        }
      },
      password: {
        label: 'Пароль',
        placeholder: 'Введите пароль',
        validation: {
          required: 'Пароль обязателен',
          length: 'Пароль должен иметь длину 6-20 символов'
        }
      },
      confirmPassword: {
        label: 'Подтвердите пароль',
        placeholder: 'Введите пароль еще раз',
        validation: {
          required: 'Подтверждение пароля обязательно',
          notMatch: 'Пароли не совпадают'
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
      deptName: {
        label: 'Отдел',
        placeholder: 'Выберите отдел'
      },
      role: {
        label: 'Роль',
        placeholder: 'Выберите роль'
      },
      post: {
        label: 'Должность',
        placeholder: 'Выберите должность'
      },
      status: {
        label: 'Статус',
        placeholder: 'Выберите статус',
        options: {
          enabled: 'Включен',
          disabled: 'Отключен'
        }
      },
      resetPwd: 'Сбросить пароль',
      import: {
        title: 'Импортировать пользователей',
        template: 'Скачать шаблон',
        success: 'Импорт успешен',
        failed: 'Ошибка импорта'
      },
      export: {
        title: 'Экспортировать пользователей',
        success: 'Экспорт успешен',
        failed: 'Ошибка экспорта'
      },
      userType: {
        label: 'Тип пользователя',
        placeholder: 'Выберите тип пользователя',
        options: {
          admin: 'Администратор',
          user: 'Обычный пользователь'
        }
      },
      createTime: 'Дата создания',
      lastLoginTime: 'Последний вход',
      messages: {
        confirmDelete: 'Вы уверены, что хотите удалить выбранных пользователей?',
        confirmResetPassword: 'Вы уверены, что хотите сбросить пароль выбранных пользователей?',
        confirmToggleStatus: 'Вы уверены, что хотите {action} этого пользователя?',
        deleteSuccess: 'Пользователь успешно удален',
        deleteFailed: 'Ошибка удаления пользователя',
        saveSuccess: 'Информация о пользователе успешно сохранена',
        saveFailed: 'Ошибка сохранения информации о пользователе',
        resetPasswordSuccess: 'Пароль успешно сброшен',
        resetPasswordFailed: 'Ошибка сброса пароля',
        importSuccess: 'Пользователи успешно импортированы',
        importFailed: 'Ошибка импорта пользователей',
        exportSuccess: 'Пользователи успешно экспортированы',
        exportFailed: 'Ошибка экспорта пользователей',
        toggleStatusSuccess: 'Статус успешно изменен',
        toggleStatusFailed: 'Ошибка изменения статуса'
      },
      tab: {
        basic: 'Основная информация',
        profile: 'Профиль',
        role: 'Роли и разрешения',
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
      tenantId: {
        label: 'Арендатор',
        placeholder: 'Выберите арендатора',
        validation: {
          required: 'Арендатор обязателен'
        }
      },
      roles: {
        label: 'Роли',
        placeholder: 'Выберите роли',
        validation: {
          required: 'Выберите хотя бы одну роль'
        }
      },
      posts: {
        label: 'Должности',
        placeholder: 'Выберите должности',
        validation: {
          required: 'Выберите хотя бы одну должность'
        }
      },
      depts: {
        label: 'Отделы',
        placeholder: 'Выберите отделы',
        validation: {
          required: 'Выберите хотя бы один отдел'
        }
      },
      remark: {
        label: 'Примечание',
        placeholder: 'Введите примечание'
      }
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
