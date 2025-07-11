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
      _self: 'Core Management',
      config: 'System Configuration',
      language: 'Language Management',
      dict: 'Dictionary Management',
    },
    identity: {
      _self: 'Identity Authentication',
      user: 'User Management',
      role: 'Role Management',
      dept: 'Department Management',
      post: 'Position Management',
      menu: 'Menu Management',
      tenant: 'Tenant Management',
      oauth: 'OAuth Management',
      profile: 'Personal Information',
      changePassword: 'Change Password'
    },
    audit: {
      _self: 'Audit Logs',
      operlog: 'Operation Log',
      loginlog: 'Login Log',
      sqldifflog: 'Difference Log',
      exceptionlog: 'Exception Log',
      auditlog: 'Audit Log',
      quartzlog: 'Task Log',
      server: 'Server Monitor'
    },
    workflow: {
      _self: 'Workflow',
      overview: 'Process Overview',
      my: 'My Processes',
      form: 'Form Management',
      definition: 'Process Definition',
      instance: 'Process Instance',
      task: 'Work Tasks',
      node: 'Process Node',
      variable: 'Process Variables',
      history: 'Process History'
    },
    signalr: {
      _self: 'Real-time Communication',
      online: 'Online Users',
      message: 'Online Messages'
    },
    generator: {
      _self: 'Code Generator',
      table: 'Database Tables',
      tableDefine: 'Table Column Definition',
      template: 'Code Templates',
      config: 'Generation Configuration',
      api: 'API Documentation'
    },
    routine: {
      _self: 'Daily Office',
      schedule: {
        _self: 'Schedule Management',
        myschedule: 'My Schedule',
        dashboard: 'Schedule Dashboard',
      },
      car: {
        _self: 'Vehicle Management',
        info: 'Vehicle Information',
        application: 'Vehicle Application',
        dashboard: 'Vehicle Dashboard',
        maintenance: 'Vehicle Maintenance',
      },
      email: {
        _self: 'Email Management',
        inbox: 'Inbox',
        drafts: 'Drafts',
        sent: 'Sent',
        trash: 'Trash',
        template: 'Email Templates',        
      },
      meeting: {
        _self: 'Meeting Management',
        room: 'Meeting Rooms',
        mymeeting: 'My Meetings',
        booking: 'Meeting Booking',
        dashboard: 'Meeting Dashboard',
      },
      notice: { 
        _self: 'Notifications & Announcements',
        message: {
          _self: 'Message Management',
          mymessages: 'My Messages',
          list: 'Message Dashboard',
        },
        announcement: {
          _self: 'Announcement Management',
          signoff: 'Sign Announcements',
          list: 'Announcement List',
        },
        notification: {
          _self: 'Notification Management',
          ack: 'Read Notifications',
          list: 'Notification List',
        },
      },
      hr: {
        _self: 'HR & Attendance',
        recruitment: {
          _self: 'Recruitment Management',
          apply: 'Recruitment Application',
          approval: 'Recruitment Approval',
          list: 'Recruitment List',

        },
        transfer: {
          _self: 'Transfer Management',
          apply: 'Transfer Application',
          approval: 'Transfer Approval',
          list: 'Transfer List',
        },
        leave: {
          _self: 'Leave Management',
          apply: 'Leave Application',
          approval: 'Leave Approval',
          list: 'Leave List',
        },
        trip: {
          _self: 'Business Trip Management',
          apply: 'Trip Application',
          approval: 'Trip Approval',
          list: 'Trip List',
        },
        overtime: {
          _self: 'Overtime Management',
          apply: 'Overtime Application',
          approval: 'Overtime Approval',
          list: 'Overtime List',
      },
    },
    expense:{
      _self: 'Expense Management',
      daily: {
        _self: 'Daily Expenses',
        apply: 'Expense Application',
        approve: 'Expense Approval',
        list: 'Expense List',
      },
      travel: {
        _self: 'Travel Expenses',
        apply: 'Travel Expense Application',
        approve: 'Travel Expense Approval',
        list: 'Travel Expense List',
      },
    },
    file:{
      _self: 'File Management',
      daily: {
        _self: 'Daily Files',
        list: 'File List',
      },
      iso: {
        _self: 'ISO Files',
        version: 'Version',
        signoff: 'Sign-off',
        list: 'ISO Files',
      },
      document: { 
        _self: 'Document Management',
        version: 'Version',
        signoff: 'Sign-off',
        list: 'Document List',
      },
    },
    officesupplies:{
      _self: 'Office Supplies',
      inventory:{
        _self: 'Inventory Management',
        requisition: 'Requisition Management',
        inbound: 'Inbound Management',
        stocktaking: 'Stocktaking Management',
      },
      usage:{
        _self: 'Usage Management',
        apply: 'Usage Application',
        approve: 'Usage Approval',
        receive: 'Usage Record',
      }
    },
    book:{
      _self: 'Book Management',
      inventory:{
        _self: 'Inventory Management',
        requisition: 'Requisition Management',
        inbound: 'Inbound Management',
        list: 'Book List',
        stocktaking: 'Stocktaking Management',
      },
      usage:{
        _self: 'Usage Management',
        card: 'Library Card',
        borrow: 'Borrow',
        return: 'Return',
      }

    },
    medical:{
      _self: 'Medical Management',
      medicine:{
        _self: 'Inventory Management',
        requisition: 'Requisition Management',
        inbound: 'Inbound Management',
        list: 'Medicine List',
        stocktaking: 'Stocktaking Management',
      },
      usage:{
        _self: 'Usage Management',
        archive: 'Archive',
        receive: 'Medicine Receipt',
        cost: 'Cost',
      }

    },
  },
  accounting: {
      _self: 'Accounting',
      financial: {
        _self: 'Management Accounting',
        company: "Company Information",
        account: 'Accounting Accounts',
        companyaccount: 'Company Accounts',
        ledger: 'General Ledger',
        payable: 'Accounts Payable',
        receivable: 'Accounts Receivable',
        fixedasset: 'Fixed Assets',
        bank: 'Bank Information',

      },
      controlling: {
        _self: 'Controlling Accounting',
        costelement: 'Cost Elements',
        costcenter: 'Cost Centers',
        profitcenter: 'Profit Centers',
        accountsReceivable: 'Accounts Receivable',
        accountsPayable: 'Accounts Payable',
        assetAccounting: 'Asset Accounting',
        tax: 'Tax Management',
        financialReporting: 'Financial Reports'      
    },
    budget:{
      _self: 'Comprehensive Budget',
        formulation: {
          _self: 'Budget Formulation',
          sales: {
            _self: 'Sales Budget',
            cost: 'Sales Cost',
            rolling: 'Sales Rolling',
          },
          production: {
            _self: 'Production Budget',
            auxiliary: 'Production Auxiliary Materials',
            labor: 'Production Labor',
            manufacturing: 'Manufacturing',
          },
          cost: {
            _self: 'Cost Budget',
            directmaterial: 'Direct Materials',
            directlabor: 'Direct Labor',
            indirectlabor: 'Indirect Labor',
            manufacturing: 'Manufacturing Expenses',
          },
          expense: {
            _self: 'Expense Budget',
            sales: 'Sales Expenses',
            management: 'Management Expenses',
            financial: 'Financial Expenses',
          },
          financial: {
            _self: 'Financial Budget',
            cashflow: 'Cash Flow',
            balancesheet: 'Balance Sheet',
            income: 'Income Statement',
          },
        },
        control: {
          _self: 'Budget Control',
          dashboard: 'Budget Dashboard',
          approval: 'Budget Approval',
        },   
  },
},
    logistics: {
      _self: 'Logistics Management',
      equipment: {
        _self: 'Equipment Management',
        data: 'Equipment Master Data',
        location: 'Equipment Location',
        material: 'Material Association',
        workorder: 'Work Order'

      },
      material: {
        _self: 'Material Management',
        material:{
          _self: 'Material Management',
          material: 'Material Master Data',
          plant: 'Plant Information',
          master: 'Material Data',
          plantmaster: 'Plant Materials',
          vendor: 'Vendor Information',
          supplier: 'Supplier Information',
        },
        purchase:{
          _self: 'Purchase Management',
          vendor: 'Vendor Information',
          supplier: 'Supplier Information',
          price: 'Purchase Price',
          requisition: 'Purchase Requisition',
          order: 'Purchase Order',

        },



      },
      production: {
        _self: 'Production Management',
        bom: 'Bill of Materials',
        change: {
          _self: 'Design Change',
          implementation: 'Change Implementation',
          techcontact: 'Technical Contact',
          material: 'Material Confirmation',
          query: 'Change Query',
          oldproduct: 'Old Product Control',
          sop: 'SOP Confirmation',
          batch: 'Input Batch',
          input: {
            _self: 'Change Input',
            gijutsu: 'Technical Department',
            seikan: 'Production Management Department',
            koubai: 'Procurement Department',
            uketsuke: 'Reception Department',
            bukan: 'Management Department',
            seizou2: 'Production Department 2',
            seizou1: 'Production Department 1',
            hinkan: 'Quality Control Department',
            seizougijutsu: 'Production Technology Department',
  
          }
        },
        workcenter: 'Work Center',
        order: 'Production Order',
        kanban: 'Kanban',
        oph:{
          _self: 'OPH Management',
          workshop1: {
            _self: 'Production Department 1',
            output: 'Daily Production Report',
            defect: 'Production Defects',
            worktime: 'Production Work Time',
            productionReport: 'Production Report',
            defectSummary: 'Defect Summary',
            worktimeReport: 'Work Time Report'
          },
          workshop2: {
            _self: 'Production Department 2',
            output: 'Daily Production Report',
            inspection: 'Inspection Records',
            repair: 'Repair Records',
            worktime: 'Production Work Time',
            productionReport: 'Production Report',
            inspectionReport: 'Inspection Report',
            repairReport: 'Repair Report',
            worktimeReport: 'Work Time Report'
          }
        }

      },
      project: {
        _self: 'Project Management',
        define: 'Project Definition',
        cost: 'Cost Planning',
        resource: 'Resource Planning',
        schedule: 'Schedule Planning',

      },
      quality: {
        _self: 'Quality Management',
        item: 'Inspection Items',
        receiving: 'Incoming Inspection',
        process: 'Process Inspection',
        storage: 'Storage Inspection',
        return: 'Return Inspection',
  
      },
      sales: {
        _self: 'Sales Management',
        customer: 'Customer Information',
        client: 'Client Information',
        price: 'Sales Price',
        order: 'Sales Order',
      },
      service: {
        _self: 'Customer Service',
        item: 'Service Items',
        contract: 'Service Contract',
        request: 'Service Request',
        workorder: 'Service Work Order',
        timesheet: 'Time Records',
        consumption: 'Material Consumption',
        outsourcing: 'Outsourcing Services'

      },
      complaint: {
        _self: 'Customer Complaint Management',
        notice: 'Quality Notice',
        mark: 'Complaint Details',
        analysis: 'Cause Analysis',
        corrective: 'Corrective Actions',
        return: 'Return Execution',
        followUp: 'Follow-up'
      }
    },
    humanResources: {
      _self: 'Human Resources Management',
      employeeManagement: {
        _self: 'Employee Management',
        employeeMaster: 'Employee Master Data',
        attendance: 'Attendance Management',
        leave: 'Leave Management',
        payroll: 'Payroll Management',
        contractManagement: 'Contract Management'
      },
      recruitment: {
        _self: 'Recruitment Management',
        jobPosting: 'Job Posting',
        candidateManagement: 'Candidate Management',
        interviewScheduling: 'Interview Scheduling',
        offerManagement: 'Offer Management'
      },
      training: {
        _self: 'Training Management',
        trainingPlan: 'Training Plan',
        trainingExecution: 'Training Execution',
        trainingEvaluation: 'Training Evaluation'
      },
      performance: {
        _self: 'Performance Management',
        goalSetting: 'Goal Setting',
        performanceReview: 'Performance Review',
        feedback: 'Feedback Management'
      },
      reporting: {
        _self: 'HR Reports',
        employeeReports: 'Employee Reports',
        attendanceReports: 'Attendance Reports',
        payrollReports: 'Payroll Reports',
        performanceReports: 'Performance Reports'
      }
    }
  }
}
