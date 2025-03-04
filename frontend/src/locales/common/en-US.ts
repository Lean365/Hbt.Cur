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
      // === Basic Operation Buttons ===
      add: 'Add',           // @btn-add-color
      edit: 'Edit',         // @btn-edit-color
      delete: 'Delete',     // @btn-delete-color
      batchDelete: 'Batch Delete', // @btn-batch-delete-color
      view: 'View',         // @btn-view-color
      clear: 'Clear',       // @btn-clear-color

      // === Data Operation Buttons ===
      import: 'Import',     // @btn-import-color
      export: 'Export',     // @btn-export-color
      template: 'Template', // @btn-template-color
      preview: 'Preview',   // @btn-preview-color
      download: 'Download', // @btn-download-color
      batchImport: 'Batch Import', // @btn-batch-import-color
      batchExport: 'Batch Export', // @btn-batch-export-color
      batchPrint: 'Batch Print', // @btn-batch-print-color
      batchEdit: 'Batch Edit',  // @btn-batch-edit-color
      batchUpdate: 'Batch Update', // @btn-batch-update-color

      // === Status Operation Buttons ===
      audit: 'Audit',       // @btn-audit-color
      revoke: 'Revoke',     // @btn-revoke-color
      stop: 'Stop',         // @btn-stop-color
      run: 'Run',          // @btn-run-color
      force: 'Force',      // @btn-forced-color

      // === System Function Buttons ===
      generate: 'Generate', // @btn-generate-color
      refresh: 'Refresh',   // @btn-refresh-color
      info: 'Info',        // @btn-info-color
      log: 'Log',          // @btn-log-color
      chat: 'Message',     // @btn-chat-color
      copy: 'Copy',        // @btn-copy-color
      execute: 'Execute',   // @btn-execute-color
      resetPwd: 'Reset Password', // @btn-reset-pwd-color
      open: 'Open',        // @btn-open-color
      close: 'Close',      // @btn-close-color
      more: 'More',        // @btn-more-color
      density: 'Density',  // @btn-density-color
      columnSetting: 'Column Settings', // @btn-column-setting-color

      // === Extended Function Buttons ===
      search: 'Search',     // @btn-search-color
      filter: 'Filter',     // @btn-filter-color
      sort: 'Sort',        // @btn-sort-color
      config: 'Config',     // @btn-config-color
      save: 'Save',        // @btn-save-color
      cancel: 'Cancel',     // @btn-cancel-color
      upload: 'Upload',     // @btn-upload-color
      print: 'Print',      // @btn-print-color
      help: 'Help',        // @btn-help-color
      share: 'Share',      // @btn-share-color
      lock: 'Lock',        // @btn-lock-color
      sync: 'Sync',        // @btn-sync-color
      expand: 'Expand',     // @btn-expand-color
      collapse: 'Collapse', // @btn-collapse-color
      approve: 'Approve',   // @btn-approve-color
      reject: 'Reject',     // @btn-reject-color
      comment: 'Comment',   // @btn-comment-color
      attach: 'Attachment', // @btn-attach-color

      // === Language Support Buttons ===
      translate: 'Translate', // @btn-translate-color
      langSwitch: 'Switch Language', // @btn-lang-switch-color
      dict: 'Dictionary',   // @btn-dict-color

      // === Data Analysis Buttons ===
      analyze: 'Analyze',   // @btn-analyze-color
      chart: 'Chart',      // @btn-chart-color
      report: 'Report',    // @btn-report-color
      dashboard: 'Dashboard', // @btn-dashboard-color
      statistics: 'Statistics', // @btn-statistics-color
      forecast: 'Forecast', // @btn-forecast-color
      compare: 'Compare',  // @btn-compare-color

      // === Workflow Buttons ===
      startFlow: 'Start Flow',   // @btn-start-flow-color
      endFlow: 'End Flow',      // @btn-end-flow-color
      suspendFlow: 'Suspend Flow', // @btn-suspend-flow-color
      resumeFlow: 'Resume Flow',  // @btn-resume-flow-color
      transfer: 'Transfer',      // @btn-transfer-color
      delegate: 'Delegate',      // @btn-delegate-color
      notify: 'Notify',         // @btn-notify-color
      urge: 'Urge',            // @btn-urge-color
      sign: 'Sign',            // @btn-sign-color
      countersign: 'Countersign', // @btn-countersign-color

      // === Mobile Specific Buttons ===
      scan: 'Scan',         // @btn-scan-color
      location: 'Location', // @btn-location-color
      call: 'Call',        // @btn-call-color
      photo: 'Photo',      // @btn-photo-color
      voice: 'Voice',      // @btn-voice-color
      faceId: 'Face ID',   // @btn-face-id-color
      fingerPrint: 'Fingerprint', // @btn-finger-print-color

      // === Social Collaboration Buttons ===
      follow: 'Follow',     // @btn-follow-color
      collect: 'Collect',   // @btn-collect-color
      like: 'Like',        // @btn-like-color
      forward: 'Forward',   // @btn-forward-color
      at: '@',             // @btn-at-color
      group: 'Group',      // @btn-group-color
      team: 'Team',        // @btn-team-color

      // === Security Authentication Buttons ===
      verifyCode: 'Verification Code', // @btn-verify-code-color
      bind: 'Bind',        // @btn-bind-color
      unbind: 'Unbind',    // @btn-unbind-color
      authorize: 'Authorize', // @btn-authorize-color
      deauthorize: 'Deauthorize', // @btn-deauthorize-color

      // === Advanced Function Buttons ===
      version: 'Version',   // @btn-version-color
      history: 'History',   // @btn-history-color
      restore: 'Restore',   // @btn-restore-color
      archive: 'Archive',   // @btn-archive-color
      unarchive: 'Unarchive', // @btn-unarchive-color
      merge: 'Merge',      // @btn-merge-color
      split: 'Split',      // @btn-split-color

      // === System Management Buttons ===
      backup: 'Backup',     // @btn-backup-color
      restoreSys: 'System Restore', // @btn-restore-sys-color
      clean: 'Clean',      // @btn-clean-color
      optimize: 'Optimize', // @btn-optimize-color
      monitor: 'Monitor',   // @btn-monitor-color
      diagnose: 'Diagnose', // @btn-diagnose-color
      maintain: 'Maintain'  // @btn-maintain-color
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
      updateTime: 'Update Time',
      formatError: 'Failed to format time',
      relativeTimeFormatError: 'Failed to format relative time',
      parseError: 'Failed to parse date',
      rangeSeparator: ' to '
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
      invalidResponse: 'Invalid response format',
      backendNotStarted: 'Backend service not started, please start the backend service first',
      invalidRequest: 'Invalid request parameters',
      unauthorized: 'Unauthorized, please login again',
      forbidden: 'Access denied',
      notFound: 'Resource not found',
      serverError: 'Internal server error',
      httpError: {
        400: 'Invalid request parameters',
        401: 'Unauthorized, please login again',
        403: 'Access denied',
        404: 'Resource not found',
        500: 'Internal server error',
        502: 'Gateway error',
        503: 'Service unavailable',
        504: 'Gateway timeout'
      },
      loadFailed: 'Load failed'
    },

    select: {
      placeholder: 'Please select',
      loadMore: 'Load more',
      loading: 'Loading...',
      notFound: 'No data found',
      selected: '{count} items selected',
      selectedTotal: 'Total {total} items',
      clear: 'Clear',
      search: 'Search',
      all: 'All',
      // Error messages
      loadError: 'Failed to load data',
      searchError: 'Search failed',
      networkError: 'Network connection failed',
      serverError: 'Server error',
      unknownError: 'Unknown error'
    }
  }
} 