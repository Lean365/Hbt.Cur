export default {
  identity: {
    auth: {
      login: {
        title: 'Iniciar sesión',
        tenantId: 'Configuración del inquilino',
        username: 'Nombre de usuario',
        password: 'Contraseña',
        rememberMe: 'Recordarme',
        forgotPassword: 'Olvidé mi contraseña',
        submit: 'Iniciar sesión',
        register: 'Registrarse',
        success: 'Inicio de sesión exitoso',
        error: {
          invalidCredentials: 'Nombre de usuario o contraseña incorrectos',
          accountLocked: 'Cuenta bloqueada',
          accountDisabled: 'Cuenta deshabilitada',
          accountExpired: 'Cuenta expirada',
          credentialsExpired: 'Contraseña expirada',
          invalidCaptcha: 'Captcha inválido',
          invalidTenant: 'Inquilino inválido',
          invalidDevice: 'Información del dispositivo inválida',
          invalidGrant: 'Concesión inválida',
          tooManyAttempts: 'Demasiados intentos de inicio de sesión, por favor intente más tarde'
        },
        noToken: 'No hay token de acceso en la respuesta de inicio de sesión',
        otherLogin: 'Otros métodos de inicio de sesión',
        form: {
          usernameRequired: 'Por favor ingrese su nombre de usuario',
          passwordRequired: 'Por favor ingrese su contraseña',
          tenantIdRequired: 'Por favor seleccione la configuración del inquilino'
        }
      },
      register: {
        title: 'Registrarse',
        username: 'Nombre de usuario',
        password: 'Contraseña',
        confirm: 'Confirmar contraseña',
        email: 'Correo electrónico',
        phone: 'Teléfono',
        submit: 'Registrarse',
        login: 'Iniciar sesión con cuenta existente',
        success: 'Registro exitoso',
        error: 'Error en el registro'
      },
      forgot: {
        title: 'Olvidé mi contraseña',
        email: 'Correo electrónico',
        submit: 'Enviar',
        back: 'Volver al inicio de sesión',
        success: 'Correo de restablecimiento de contraseña enviado',
        error: 'Error al restablecer la contraseña'
      },
      info: {
        loading: 'Cargando información del usuario',
        success: 'Información del usuario cargada exitosamente'
      },
      autoLogout: 'Ha sido desconectado automáticamente por inactividad',
      error: {
        noResponse: 'El servidor no responde',
        noSaltData: 'Error al obtener parámetros de encriptación',
        invalidSalt: 'Formato de parámetro de encriptación inválido',
        invalidIterations: 'Número de iteraciones de encriptación inválido',
        permanentlyLocked: 'Cuenta bloqueada permanentemente, por favor contacte al administrador',
        temporarilyLocked: 'Cuenta bloqueada temporalmente, por favor intente en {minutes} minutos',
        tooManyAttempts: 'Demasiados intentos fallidos de inicio de sesión, cuenta bloqueada',
        invalidCredentials: 'Nombre de usuario o contraseña incorrectos'
      }
    }
  }
} 