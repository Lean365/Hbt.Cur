export default {
  identity: {
    user: {
      title: 'Gestión de usuarios',
      toolbar: {
        add: 'Agregar usuario',
        edit: 'Editar usuario',
        delete: 'Eliminar usuario',
        import: 'Importar usuarios',
        export: 'Exportar usuarios',
        resetPassword: 'Restablecer contraseña',
        downloadTemplate: 'Descargar plantilla'
      },
      table: {
        columns: {
          userName: 'Nombre de usuario',
          nickName: 'Apodo',
          deptName: 'Departamento',
          role: 'Rol',
          email: 'Correo electrónico',
          phoneNumber: 'Número de teléfono',
          gender: 'Género',
          status: 'Estado',
          createTime: 'Fecha de creación',
          lastLoginTime: 'Último inicio de sesión',
          operation: 'Operación'
        },
        operation: {
          edit: 'Editar',
          delete: 'Eliminar',
          resetPassword: 'Restablecer contraseña'
        },
        status: {
          enabled: 'Habilitado',
          disabled: 'Deshabilitado',
          toggle: {
            enable: 'Habilitar',
            disable: 'Deshabilitar'
          }
        }
      },
      userId: 'ID de usuario',
      userName: {
        label: 'Nombre de usuario',
        placeholder: 'Ingrese el nombre de usuario',
        validation: {
          required: 'El nombre de usuario es requerido',
          format: 'El nombre de usuario debe comenzar con una letra minúscula, tener una longitud de 6-20 caracteres y contener solo letras minúsculas, números y guiones bajos'
        }
      },
      nickName: {
        label: 'Apodo',
        placeholder: 'Ingrese el apodo',
        validation: {
          required: 'El apodo es requerido',
          format: 'El apodo debe tener una longitud de 2-20 caracteres y contener solo caracteres chinos, letras inglesas, números y guiones bajos'
        }
      },
      englishName: {
        label: 'Nombre en inglés',
        placeholder: 'Ingrese el nombre en inglés',
        validation: {
          format: 'El nombre en inglés debe tener una longitud de 2-50 caracteres y contener solo letras inglesas, espacios y guiones'
        }
      },
      password: {
        label: 'Contraseña',
        placeholder: 'Ingrese la contraseña',
        validation: {
          required: 'La contraseña es requerida',
          length: 'La contraseña debe tener una longitud de 6-20 caracteres'
        }
      },
      confirmPassword: {
        label: 'Confirmar contraseña',
        placeholder: 'Ingrese la contraseña nuevamente',
        validation: {
          required: 'La confirmación de contraseña es requerida',
          notMatch: 'Las contraseñas no coinciden'
        }
      },
      email: {
        label: 'Correo electrónico',
        placeholder: 'Ingrese el correo electrónico',
        validation: {
          required: 'El correo electrónico es requerido',
          invalid: 'El correo electrónico debe tener una longitud de 6-100 caracteres y estar en un formato válido'
        }
      },
      phoneNumber: {
        label: 'Número de teléfono',
        placeholder: 'Ingrese el número de teléfono',
        validation: {
          required: 'El número de teléfono es requerido',
          invalid: 'Ingrese un formato de número de teléfono móvil o fijo válido'
        }
      },
      gender: {
        label: 'Género',
        placeholder: 'Seleccione el género',
        options: {
          male: 'Masculino',
          female: 'Femenino',
          unknown: 'Desconocido'
        }
      },
      avatar: {
        label: 'Avatar',
        upload: 'Subir avatar',
        uploadSuccess: 'Avatar subido exitosamente',
        uploadError: 'Error al subir el avatar'
      },
      deptName: {
        label: 'Departamento',
        placeholder: 'Seleccione el departamento'
      },
      role: {
        label: 'Rol',
        placeholder: 'Seleccione el rol'
      },
      post: {
        label: 'Cargo',
        placeholder: 'Seleccione el cargo'
      },
      status: {
        label: 'Estado',
        placeholder: 'Seleccione el estado',
        options: {
          enabled: 'Habilitado',
          disabled: 'Deshabilitado'
        }
      },
      resetPwd: 'Restablecer contraseña',
      import: {
        title: 'Importar usuarios',
        template: 'Descargar plantilla',
        success: 'Importación exitosa',
        failed: 'Error en la importación'
      },
      export: {
        title: 'Exportar usuarios',
        success: 'Exportación exitosa',
        failed: 'Error en la exportación'
      },
      userType: {
        label: 'Tipo de usuario',
        placeholder: 'Seleccione el tipo de usuario',
        options: {
          admin: 'Administrador',
          user: 'Usuario normal'
        }
      },
      createTime: 'Fecha de creación',
      lastLoginTime: 'Último inicio de sesión',
      messages: {
        confirmDelete: '¿Está seguro de que desea eliminar los usuarios seleccionados?',
        confirmResetPassword: '¿Está seguro de que desea restablecer la contraseña de los usuarios seleccionados?',
        confirmToggleStatus: '¿Está seguro de que desea {action} este usuario?',
        deleteSuccess: 'Usuario eliminado exitosamente',
        deleteFailed: 'Error al eliminar el usuario',
        saveSuccess: 'Información de usuario guardada exitosamente',
        saveFailed: 'Error al guardar la información del usuario',
        resetPasswordSuccess: 'Contraseña restablecida exitosamente',
        resetPasswordFailed: 'Error al restablecer la contraseña',
        importSuccess: 'Usuarios importados exitosamente',
        importFailed: 'Error al importar usuarios',
        exportSuccess: 'Usuarios exportados exitosamente',
        exportFailed: 'Error al exportar usuarios',
        toggleStatusSuccess: 'Estado modificado exitosamente',
        toggleStatusFailed: 'Error al modificar el estado'
      },
      tab: {
        basic: 'Información básica',
        profile: 'Perfil',
        role: 'Roles y permisos',
        dept: 'Departamento y cargo',
        other: 'Otra información',
        avatar: 'Configuración de avatar',
        loginLog: 'Registro de inicio de sesión',
        operateLog: 'Registro de operaciones',
        errorLog: 'Registro de errores',
        taskLog: 'Registro de tareas'
      },
      update: {
        validation: {
          required: 'El ID de usuario y el ID del inquilino son requeridos'
        }
      },
      tenantId: {
        label: 'Inquilino',
        placeholder: 'Seleccione el inquilino',
        validation: {
          required: 'El inquilino es requerido'
        }
      },
      roles: {
        label: 'Roles',
        placeholder: 'Seleccione los roles',
        validation: {
          required: 'Seleccione al menos un rol'
        }
      },
      posts: {
        label: 'Cargos',
        placeholder: 'Seleccione los cargos',
        validation: {
          required: 'Seleccione al menos un cargo'
        }
      },
      depts: {
        label: 'Departamentos',
        placeholder: 'Seleccione los departamentos',
        validation: {
          required: 'Seleccione al menos un departamento'
        }
      },
      remark: {
        label: 'Observación',
        placeholder: 'Ingrese una observación'
      }
    }
  },
  actions: {
    add: 'Agregar usuario',
    edit: 'Editar usuario',
    delete: 'Eliminar usuario',
    resetPassword: 'Restablecer contraseña',
    export: 'Exportar usuarios'
  },
  rules: {
    userName: 'El nombre de usuario es requerido',
    nickName: 'El apodo es requerido',
    phoneNumber: 'Ingrese un número de teléfono válido',
    email: 'Ingrese una dirección de correo electrónico válida'
  }
}
