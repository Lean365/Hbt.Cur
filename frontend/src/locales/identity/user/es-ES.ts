export default {
  identity: {
    user: {
      title: 'Gestión de usuarios',
      table: {
        columns: {
          userId: 'ID de usuario',
          tenantId: 'Arrendatario',
          userName: 'Nombre de usuario',
          nickName: 'Apodo',
          englishName: 'Nombre en inglés',
          userType: 'Tipo',
          email: 'Correo electrónico',
          phoneNumber: 'Número de teléfono',
          gender: 'Género',
          avatar: 'Avatar',
          status: 'Estado',
          lastPasswordChangeTime: 'Última modificación de contraseña',
          lockEndTime: 'Fin del bloqueo',
          lockReason: 'Razón del bloqueo',
          isLock: '¿Bloqueado?',
          errorLimit: 'Límite de errores',
          loginCount: 'Número de inicios de sesión',
          deptName: 'Departamento',
          role: 'Rol',
          createBy: 'Creado por',
          createTime: 'Fecha de creación',
          updateBy: 'Actualizado por',
          updateTime: 'Fecha de actualización',
          deleteBy: 'Eliminado por',
          deleteTime: 'Fecha de eliminación',
          isDeleted: 'Eliminado',
          remark: 'Observación',
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
      fields: {
        userId: 'ID de usuario',
        tenantId: {
          label: 'Arrendatario',
          placeholder: 'Seleccione arrendatario',
          validation: {
            required: 'El arrendatario es obligatorio'
          }
        },
        userName: {
          label: 'Nombre de usuario',
          placeholder: 'Ingrese el nombre de usuario',
          validation: {
            required: 'El nombre de usuario es obligatorio',
            format: 'El nombre de usuario debe comenzar con una letra minúscula, tener una longitud de 6-20 caracteres y contener solo letras minúsculas, números y guiones bajos'
          }
        },
        nickName: {
          label: 'Apodo',
          placeholder: 'Ingrese el apodo',
          validation: {
            required: 'El apodo es obligatorio',
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
        userType: {
          label: 'Tipo',
          placeholder: 'Seleccione el tipo de usuario',
          options: {
            admin: 'Administrador',
            user: 'Usuario normal'
          }
        },
        email: {
          label: 'Correo electrónico',
          placeholder: 'Ingrese el correo electrónico',
          validation: {
            required: 'El correo electrónico es obligatorio',
            invalid: 'El correo electrónico debe tener una longitud de 6-100 caracteres y estar en un formato válido'
          }
        },
        phoneNumber: {
          label: 'Número de teléfono',
          placeholder: 'Ingrese el número de teléfono',
          validation: {
            required: 'El número de teléfono es obligatorio',
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
        status: {
          label: 'Estado',
          placeholder: 'Seleccione el estado',
          options: {
            enabled: 'Habilitado',
            disabled: 'Deshabilitado'
          }
        },
        deptName: {
          label: 'Departamento',
          placeholder: 'Seleccione el departamento',
          validation: {
            required: 'El departamento es obligatorio'
          }
        },
        role: {
          label: 'Rol',
          placeholder: 'Seleccione el rol',
          validation: {
            required: 'El rol es obligatorio'
          }
        },
        post: {
          label: 'Cargo',
          placeholder: 'Seleccione el cargo',
          validation: {
            required: 'El cargo es obligatorio'
          }
        },
        remark: {
          label: 'Observación',
          placeholder: 'Ingrese una observación'
        }
      },
      messages: {
        confirmDelete: '¿Está seguro de que desea eliminar el/los usuario(s) seleccionado(s)?',
        confirmResetPassword: '¿Está seguro de que desea restablecer la contraseña del/los usuario(s) seleccionado(s)?',
        confirmToggleStatus: '¿Está seguro de que desea {action} este usuario?',
        deleteSuccess: 'Usuario eliminado correctamente',
        deleteFailed: 'Error al eliminar el usuario',
        saveSuccess: 'Información del usuario guardada correctamente',
        saveFailed: 'Error al guardar la información del usuario',
        resetPasswordSuccess: 'Contraseña restablecida correctamente',
        resetPasswordFailed: 'Error al restablecer la contraseña',
        importSuccess: 'Usuario(s) importado(s) correctamente',
        importFailed: 'Error al importar usuario(s)',
        exportSuccess: 'Usuario(s) exportado(s) correctamente',
        exportFailed: 'Error al exportar usuario(s)',
        toggleStatusSuccess: 'Estado modificado correctamente',
        toggleStatusFailed: 'Error al modificar el estado'
      },
      tab: {
        basic: 'Información básica',
        profile: 'Perfil',
        role: 'Permisos de rol',
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
          required: 'El ID de usuario y el ID de arrendatario son obligatorios'
        }
      },
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
      resetPwd: 'Restablecer contraseña'
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
    userName: 'El nombre de usuario es obligatorio',
    nickName: 'El apodo es obligatorio',
    phoneNumber: 'Ingrese un número de teléfono válido',
    email: 'Ingrese una dirección de correo electrónico válida'
  }
}
