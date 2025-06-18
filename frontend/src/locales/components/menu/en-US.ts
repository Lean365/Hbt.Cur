export default {
  menu: {
    home: 'Home',
    dashboard: {
      title: 'Dashboard',
      workplace: 'Workplace',
      analysis: 'Analysis',
      monitor: 'Monitor'
    },
    components: {
      title: 'Components',
      icons: 'Icons'
    },
    about: {
      title: 'About Us',
      privacy: 'Privacy Policy',
      terms: 'Terms of Service',
      index: 'About Hbt'
    },
    core: {
      _self: 'Core',
      config: 'Config',
      language: 'Language',
      dict: 'Dictionary',
    },
    identity: {
      _self: 'Identity',
      user: 'User',
      role: 'Role',
      dept: 'Dept',
      post: 'Post',
      menu: 'Menu',
      tenant: 'Tenant',
      oauth: 'OAuth',
      profile: 'Profile',
      changePassword: 'Change Password'
    },
    audit: {
      _self: 'Logging',
      operlog: 'Operation',
      loginlog: 'Login',
      sqldifflog: 'SQL Diff',
      exceptionlog: 'Exception',
      auditlog: 'Audit',
      quartzlog: 'Task',
      server: 'Monitor'
    },
    workflow: {
      _self: 'Workflow',
      form: 'Form',
      definition: 'Definition',
      instance: 'Instance',
      task: 'Task',
      node: 'Node',
      variable: 'Variable',
      history: 'History'
    },
    signalr: {
      _self: 'SignalR',
      online: 'Users',
      message: 'Message'
    },
    generator: {
      _self: 'Generator',
      table: 'DB Table',
      tableDefine: 'Table Column',
      template: 'Code Template',
      config: 'Generator Config',
      api: 'API Doc'
    },
    routine: {
      _self: 'Routine',
      vehicle: {
        _self: 'Vehicle Management',
        vehicleMaster: {
          _self: 'Vehicle Master',
          vehicleInfo: 'Vehicle Info',
          driverInfo: 'Driver Info',
          maintenance: 'Maintenance'
        },
        vehicleBooking: {
          _self: 'Vehicle Booking',
          newBooking: 'New Booking',
          bookingList: 'Booking List',
          bookingApproval: 'Booking Approval'
        },
        vehicleDispatch: {
          _self: 'Vehicle Dispatch',
          dispatchPlan: 'Dispatch Plan',
          realTimeTracking: 'Real-time Tracking',
          dispatchHistory: 'Dispatch History'
        },
        vehicleReporting: {
          _self: 'Vehicle Report',
          usageReport: 'Usage Report',
          costReport: 'Cost Report',
          maintenanceReport: 'Maintenance Report'
        }
      },
      file: 'File Management',
      mail: 'Mail Management',
      mailTmpl: 'Mail Template',
      meeting: {
        _self: 'Meeting Management',
        meetingRoom: {
          _self: 'Meeting Room',
          roomInfo: 'Room Info',
          roomBooking: 'Room Booking',
          roomSchedule: 'Room Schedule'
        },
        meetingPlan: {
          _self: 'Meeting Plan',
          newMeeting: 'New Meeting',
          meetingList: 'Meeting List',
          meetingApproval: 'Meeting Approval'
        },
        meetingExecution: {
          _self: 'Meeting Execution',
          attendance: 'Attendance',
          minutes: 'Minutes',
          followUp: 'Follow Up'
        },
        meetingReporting: {
          _self: 'Meeting Report',
          meetingReport: 'Meeting Report',
          attendanceReport: 'Attendance Report',
          costReport: 'Cost Report'
        }
      },
      notice: 'Notice',
      schedule: 'Schedule',
      quartz: 'Task'
    },
    finance: {
      _self: 'Finance',
      accounting: {
        _self: 'Management Accounting',
        companyaccounts: 'Company Accounts',
        glaccount: 'GL Account',
        generalledger: 'General Ledger',
        payable: 'Payable',
        receivable: 'Receivable',
        asset: 'Asset',
        bank: 'Bank',
        tax: 'Tax',
        planning: 'Planning',
        reporting: 'Reporting'
      },
      controlling: {
        _self: 'Controlling',
        costelement: 'Cost Element',
        costcenter: 'Cost Center',
        profitcenter: 'Profit Center',
        accountsReceivable: 'Accounts Receivable',
        accountsPayable: 'Accounts Payable',
        assetAccounting: 'Asset Accounting',
        tax: 'Tax Management',
        financialReporting: 'Financial Reporting'
      }
    },
    logistics: {
      _self: 'Logistics',
      equipment: {
        _self: 'Equipment Management',
        data: 'Equipment Data',
        location: 'Equipment Location',
        material: 'Material Link',
        workorder: 'Work Order'
      },
      material: {
        _self: 'Material Management',
        info: 'Material Info',
        factory: 'Factory Material',
        vendor: 'Vendor',
        supplier: 'Supplier',
        price: 'Material Price',
        requisition: 'Purchase Requisition',
        order: 'Purchase Order'
      },
      production: {
        _self: 'Production Management',
        bom: 'BOM',
        routing: 'Routing',
        change: 'Engineering Change',
        workcenter: 'Work Center',
        order: 'Production Order',
        kanban: 'Kanban'
      },
      project: {
        _self: 'Project Management',
        define: 'Project Define',
        cost: 'Cost Plan',
        resource: 'Resource Plan',
        schedule: 'Schedule'
      },
      quality: {
        _self: 'Quality Management',
        item: 'Inspection Item',
        receiving: 'Receiving Inspection',
        process: 'Process Inspection',
        storage: 'Storage Inspection',
        return: 'Return Inspection'
      },
      sales: {
        _self: 'Sales Management',
        customer: 'Customer',
        client: 'Client',
        price: 'Sales Price',
        order: 'Sales Order'
      },
      service: {
        _self: 'Customer Service',
        item: 'Service Item',
        contract: 'Service Contract',
        request: 'Service Request',
        workorder: 'Service Work Order',
        timesheet: 'Timesheet',
        consumption: 'Material Consumption',
        outsourcing: 'Outsourcing Service'
      },
      complaint: {
        _self: 'Complaint Management',
        notice: 'Quality Notice',
        mark: 'Complaint Detail',
        analysis: 'Cause Analysis',
        corrective: 'Corrective Action',
        return: 'Return Execution',
        followUp: 'Follow Up'
      }
    },
    humanResources: {
      _self: 'HR Management',
      employeeManagement: {
        _self: 'Employee Management',
        employeeMaster: 'Employee Master',
        attendance: 'Attendance',
        leave: 'Leave',
        payroll: 'Payroll',
        contractManagement: 'Contract Management'
      },
      recruitment: {
        _self: 'Recruitment',
        jobPosting: 'Job Posting',
        candidateManagement: 'Candidate Management',
        interviewScheduling: 'Interview Scheduling',
        offerManagement: 'Offer Management'
      },
      training: {
        _self: 'Training',
        trainingPlan: 'Training Plan',
        trainingExecution: 'Training Execution',
        trainingEvaluation: 'Training Evaluation'
      },
      performance: {
        _self: 'Performance',
        goalSetting: 'Goal Setting',
        performanceReview: 'Performance Review',
        feedback: 'Feedback'
      },
      reporting: {
        _self: 'HR Reporting',
        employeeReports: 'Employee Reports',
        attendanceReports: 'Attendance Reports',
        payrollReports: 'Payroll Reports',
        performanceReports: 'Performance Reports'
      }
    }
  }
}
