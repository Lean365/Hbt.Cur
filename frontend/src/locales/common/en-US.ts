export default {
  common: {
    // Basic Titles
    title: {
      list: 'List',
      detail: 'Detail',
      create: 'Create',
      edit: 'Edit',
      preview: 'Preview'
    },

    // System Information
    system: {
      title: 'Black Ice Platform',
      slogan: 'Professional, Efficient, and Secure Enterprise Management System',
      description: 'Modern Enterprise Management System Based on .NET 8 and Vue 3',
      copyright: '© 2024 Lean365. All rights reserved.'
    },

    // Action Buttons
    actions: {
      add: 'Add',
      edit: 'Edit',
      delete: 'Delete',
      view: 'View',
      search: 'Search',
      reset: 'Reset',
      refresh: 'Refresh',
      confirm: 'Confirm',
      cancel: 'Cancel',
      save: 'Save',
      back: 'Back',
      export: 'Export',
      import: 'Import',
      download: 'Download',
      upload: 'Upload',
      preview: 'Preview',
      print: 'Print'
    },

    // Status
    status: {
      label: 'Status',
      normal: 'Normal',
      disabled: 'Disabled',
      placeholder: 'Please select status'
    },

    // Yes/No
    yesNo: {
      yes: 'Yes',
      no: 'No'
    },

    // Visibility
    visible: {
      show: 'Show',
      hide: 'Hide'
    },

    // Date & Time
    datetime: {
      date: 'Date',
      time: 'Time',
      year: 'Year',
      month: 'Month',
      day: 'Day',
      hour: 'Hour',
      minute: 'Minute',
      second: 'Second',
      startDate: 'Start Date',
      endDate: 'End Date',
      startTime: 'Start Time',
      endTime: 'End Time',
      createTime: 'Create Time',
      updateTime: 'Update Time'
    },

    // Form
    form: {
      required: 'Required',
      optional: 'Optional',
      invalid: 'Invalid',
      placeholder: {
        select: 'Please select',
        input: 'Please enter',
        date: 'Please select date',
        time: 'Please select time'
      }
    },

    // Table
    table: {
      header: {
        operation: 'Operation'
      },
      config: {
        density: {
          default: 'Default',
          middle: 'Middle',
          small: 'Compact'
        },
        columnSetting: 'Column Settings'
      },
      pagination: {
        total: 'Total {total} items',
        current: 'Page {current}',
        pageSize: '{pageSize} items/page',
        jump: 'Go to'
      },
      empty: 'No Data',
      loading: 'Loading...',
      selectAll: 'Select All',
      selected: '{total} items selected'
    },

    // Import & Export
    import: {
      title: 'Import Data',
      file: 'Select File',
      select: 'Select File',
      template: 'Download Template',
      download: 'Download Template',
      note: 'Import Notes',
      tips: 'Please strictly follow the format of the import template, otherwise the import may fail',
      format: 'Only Excel files are supported!',
      size: 'File size cannot exceed 2MB!',
      total: 'Total Records',
      success: 'Success Count',
      failed: 'Failed Count',
      message: 'Failure Reason'
    },

    // Upload
    upload: {
      text: 'Drag file here or click to upload',
      picture: 'Click to upload picture',
      file: 'Click to upload file',
      icon: 'Click to upload icon',
      limit: {
        size: 'File size cannot exceed {size}',
        type: 'Only {type} format is supported'
      }
    },

    // Result
    result: {
      success: 'Success',
      failed: 'Failed',
      warning: 'Warning',
      info: 'Information',
      error: 'Error'
    },

    // Messages
    message: {
      loading: 'Processing...',
      saving: 'Saving...',
      submitting: 'Submitting...',
      deleting: 'Deleting...',
      operationSuccess: 'Operation successful',
      operationFailed: 'Operation failed',
      deleteConfirm: 'Are you sure you want to delete?',
      deleteSuccess: 'Delete successful',
      deleteFailed: 'Delete failed',
      createSuccess: 'Create successful',
      createFailed: 'Create failed',
      updateSuccess: 'Update successful',
      updateFailed: 'Update failed',
      networkError: 'Network connection failed, please check your network',
      systemError: 'System error',
      timeout: 'Request timeout',
      // New error messages
      invalidResponse: 'Invalid response data format',
      backendNotStarted: 'Backend service not started, please start the backend service first',
      invalidRequest: 'Invalid request parameters',
      unauthorized: 'Unauthorized, please login again',
      forbidden: 'Access denied',
      notFound: 'Requested resource not found',
      serverError: 'Internal server error',
      httpError: {
        400: 'Invalid request parameters',
        401: 'Unauthorized, please login again',
        403: 'Access denied',
        404: 'Requested resource not found',
        500: 'Internal server error',
        502: 'Gateway error',
        503: 'Service unavailable',
        504: 'Gateway timeout'
      }
    }
  }
} 