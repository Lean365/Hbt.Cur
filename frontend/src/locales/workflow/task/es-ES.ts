export default {
  workflow: {
    task: {
      title: 'Tarea de Flujo de Trabajo',
      list: {
        title: 'Lista de Tareas de Flujo de Trabajo',
        search: {
          name: 'Nombre de la Tarea',
          type: 'Tipo de Tarea',
          status: 'Estado',
          startTime: 'Hora de Inicio',
          endTime: 'Hora de Finalización'
        },
        table: {
          name: 'Nombre de la Tarea',
          type: 'Tipo de Tarea',
          status: 'Estado',
          startTime: 'Hora de Inicio',
          endTime: 'Hora de Finalización',
          duration: 'Duración',
          actions: 'Acciones'
        },
        actions: {
          view: 'Ver',
          edit: 'Editar',
          delete: 'Eliminar',
          refresh: 'Actualizar'
        },
        status: {
          running: 'En Ejecución',
          completed: 'Completado',
          terminated: 'Terminado',
          failed: 'Fallido'
        }
      },
      form: {
        title: {
          create: 'Crear Tarea de Flujo de Trabajo',
          edit: 'Editar Tarea de Flujo de Trabajo'
        },
        fields: {
          name: 'Nombre de la Tarea',
          type: 'Tipo de Tarea',
          description: 'Descripción',
          input: 'Entrada',
          output: 'Salida',
          error: 'Error'
        },
        rules: {
          name: {
            required: 'Por favor ingrese el nombre de la tarea'
          },
          type: {
            required: 'Por favor seleccione el tipo de tarea'
          }
        },
        buttons: {
          submit: 'Enviar',
          cancel: 'Cancelar'
        }
      },
      detail: {
        title: 'Detalle de Tarea de Flujo de Trabajo',
        basic: {
          title: 'Información Básica',
          name: 'Nombre de la Tarea',
          type: 'Tipo de Tarea',
          description: 'Descripción',
          status: 'Estado',
          startTime: 'Hora de Inicio',
          endTime: 'Hora de Finalización',
          duration: 'Duración'
        },
        input: {
          title: 'Información de Entrada',
          value: 'Valor de Entrada'
        },
        output: {
          title: 'Información de Salida',
          value: 'Valor de Salida'
        },
        error: {
          title: 'Información de Error',
          message: 'Mensaje de Error',
          stackTrace: 'Seguimiento de Pila'
        },
        actions: {
          back: 'Volver'
        }
      }
    }
  }
} 