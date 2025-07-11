export default {
  workflow: {
    node: {
      fields: {
        instanceId: 'Instance ID',
        nodeId: 'Node ID',
        nodeName: 'Node Name',
        nodeType: 'Node Type',
        definitionId: 'Definition ID',
        definitionName: 'Definition Name',
        parentNodeId: 'Parent Node ID',
        parentNodeName: 'Parent Node Name',
        nodeConfig: 'Node Config',
        status: 'Status',
        startTime: 'Start Time',
        endTime: 'End Time',
        orderNum: 'Order',
      },
      title: 'Workflow Node',
      list: {
        title: 'Workflow Node List',
        search: {
          name: 'Node Name',
          type: 'Node Type',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time'
        },
        table: {
          name: 'Node Name',
          type: 'Node Type',
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
          create: 'Create Workflow Node',
          edit: 'Edit Workflow Node'
        },
        fields: {
          name: 'Node Name',
          type: 'Node Type',
          description: 'Description',
          input: 'Input',
          output: 'Output',
          error: 'Error'
        },
        rules: {
          name: {
            required: 'Please enter node name'
          },
          type: {
            required: 'Please select node type'
          }
        },
        buttons: {
          submit: 'Submit',
          cancel: 'Cancel'
        }
      },
      detail: {
        title: 'Workflow Node Detail',
        basic: {
          title: 'Basic Information',
          name: 'Node Name',
          type: 'Node Type',
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