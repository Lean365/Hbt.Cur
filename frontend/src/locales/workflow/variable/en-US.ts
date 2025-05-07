export default {
  workflow: {
    variable: {
      title: 'Workflow Variable',
      list: {
        title: 'Workflow Variable List',
        search: {
          name: 'Variable Name',
          type: 'Variable Type',
          scope: 'Scope',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time'
        },
        table: {
          name: 'Variable Name',
          type: 'Variable Type',
          scope: 'Scope',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time',
          duration: 'Duration',
          actions: 'Actions'
        },
        actions: {
          view: 'View',
          edit: 'Edit',
          delete: 'Delete',
          refresh: 'Refresh'
        },
        status: {
          running: 'Running',
          completed: 'Completed',
          terminated: 'Terminated',
          failed: 'Failed'
        }
      },
      form: {
        title: {
          create: 'Create Workflow Variable',
          edit: 'Edit Workflow Variable'
        },
        fields: {
          name: 'Variable Name',
          type: 'Variable Type',
          scope: 'Scope',
          description: 'Description',
          input: 'Input',
          output: 'Output',
          error: 'Error'
        },
        rules: {
          name: {
            required: 'Please enter variable name'
          },
          type: {
            required: 'Please select variable type'
          },
          scope: {
            required: 'Please select variable scope'
          }
        },
        buttons: {
          submit: 'Submit',
          cancel: 'Cancel'
        }
      },
      detail: {
        title: 'Workflow Variable Detail',
        basic: {
          title: 'Basic Information',
          name: 'Variable Name',
          type: 'Variable Type',
          scope: 'Scope',
          description: 'Description',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time',
          duration: 'Duration'
        },
        input: {
          title: 'Input Information',
          value: 'Input Value'
        },
        output: {
          title: 'Output Information',
          value: 'Output Value'
        },
        error: {
          title: 'Error Information',
          message: 'Error Message',
          stackTrace: 'Stack Trace'
        },
        actions: {
          back: 'Back'
        }
      }
    }
  }
} 