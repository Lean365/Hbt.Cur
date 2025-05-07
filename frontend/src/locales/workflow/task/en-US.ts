export default {
  workflow: {
    task: {
      title: 'Workflow Task',
      list: {
        title: 'Workflow Task List',
        search: {
          name: 'Task Name',
          type: 'Task Type',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time'
        },
        table: {
          name: 'Task Name',
          type: 'Task Type',
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
          create: 'Create Workflow Task',
          edit: 'Edit Workflow Task'
        },
        fields: {
          name: 'Task Name',
          type: 'Task Type',
          description: 'Description',
          input: 'Input',
          output: 'Output',
          error: 'Error'
        },
        rules: {
          name: {
            required: 'Please enter task name'
          },
          type: {
            required: 'Please select task type'
          }
        },
        buttons: {
          submit: 'Submit',
          cancel: 'Cancel'
        }
      },
      detail: {
        title: 'Workflow Task Detail',
        basic: {
          title: 'Basic Information',
          name: 'Task Name',
          type: 'Task Type',
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