export default {
  workflow: {
    node: {
      title: 'Nodo de Flujo de Trabajo',
      list: {
        title: 'Lista de Nodos de Flujo de Trabajo',
        search: {
          name: 'Nombre del Nodo',
          type: 'Tipo de Nodo',
          status: 'Estado',
          startTime: 'Hora de Inicio',
          endTime: 'Hora de Finalización'
        },
        table: {
          name: 'Nombre del Nodo',
          type: 'Tipo de Nodo',
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
          create: 'Crear Nodo de Flujo de Trabajo',
          edit: 'Editar Nodo de Flujo de Trabajo'
        },
        fields: {
          name: 'Nombre del Nodo',
          type: 'Tipo de Nodo',
          description: 'Descripción',
          input: 'Entrada',
          output: 'Salida',
          error: 'Error'
        },
        rules: {
          name: {
            required: 'Por favor ingrese el nombre del nodo'
          },
          type: {
            required: 'Por favor seleccione el tipo de nodo'
          }
        },
        buttons: {
          submit: 'Enviar',
          cancel: 'Cancelar'
        }
      },
      detail: {
        title: 'Detalle del Nodo de Flujo de Trabajo',
        basic: {
          title: 'Información Básica',
          name: 'Nombre del Nodo',
          type: 'Tipo de Nodo',
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