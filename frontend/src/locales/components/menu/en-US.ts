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
    admin: {
      _self: 'System Administration',
      config: 'Config Management',
      language: 'Language Management',
      dict: 'Dictionary Management',

    },
    identity: {
      _self: 'Identity Management',
      user: 'User Management',
      role: 'Role Management',
      dept: 'Department Management',
      post: 'Post Management',
      menu: 'Menu Management',
      tenant: 'Tenant Management',
      oauth: 'OAuth Management'
    },
    audit: {
      _self: 'Audit Logs',
      operlog: 'Operation Logs',
      loginlog: 'Login Logs',
      dbdifflog: 'Database Diff Logs',
      exceptionlog: 'Exception Logs',
      auditlog: 'Audit Logs',
      quartzlog: 'Quartz Logs'
    },
    workflow: {
      _self: 'Workflow',
      definition: 'Workflow Definition',
      instance: 'Workflow Instance',
      task: 'Workflow Tasks',
      node: 'Workflow Nodes',
      variable: 'Workflow Variables',
      history: 'Workflow History'
    },
    signalr: {
      _self: 'Real-Time Monitoring',
      server: 'Server Monitoring',
      online: 'Online Users',
      message: 'Online Messages'
    },
    generator: {
      _self: 'Code Generator',
      table: 'Database Tables',
      tableDefine: 'Custom Tables',
      template: 'Code Templates',
      config: 'Generation Configuration',
      api: 'API Documentation'
    },
    routine: {
      _self: 'Routine Office',
      file: 'File Management',
      mail: 'Mail Management',
      mailTmpl: 'Mail Templates',
      notice: 'Notices',
      task: 'Tasks',
      schedule: 'Schedule Management'
    },
    finance: {
      _self: 'Finance',
      management: {
        _self: 'Management Accounting',
        cost: {
          _self: 'Cost Management',
          costFactors: 'Cost Factors',
          costCenter: 'Cost Center',
          profitCenter: 'Profit Center',
          productCost: 'Product Cost',
          activityType: 'Activity Type',
          internalOrder: 'Internal Orders'
        },
        planning: {
          _self: 'Planning Management',
          costPlanning: 'Cost Planning',
          profitPlanning: 'Profit Planning',
          budgetControl: 'Budget Control'
        },
        reporting: {
          _self: 'Reports & Analysis',
          costReports: 'Cost Reports',
          profitReports: 'Profit Reports',
          varianceAnalysis: 'Variance Analysis'
        }
      },
      financial: {
        _self: 'Financial Accounting',
        generalLedger: {
          _self: 'General Ledger',
          account: 'Accounts',
          accountType: 'Account Types',
          journalEntry: 'Journal Entries',
          reconciliation: 'Reconciliation',
          closing: 'Period Closing'
        },
        accountsReceivable: {
          _self: 'Accounts Receivable',
          customer: 'Customer Management',
          invoice: 'Customer Invoices',
          payment: 'Customer Payments',
          creditControl: 'Credit Control'
        },
        accountsPayable: {
          _self: 'Accounts Payable',
          supplier: 'Supplier Management',
          invoice: 'Supplier Invoices',
          payment: 'Supplier Payments',
          agingReport: 'Aging Reports'
        },
        assetAccounting: {
          _self: 'Asset Accounting',
          assets: 'Fixed Assets',
          depreciation: 'Depreciation Management',
          assetTransfer: 'Asset Transfers',
          assetRetirement: 'Asset Retirement'
        },
        tax: {
          _self: 'Tax Management',
          taxCodes: 'Tax Codes',
          taxReporting: 'Tax Reports',
          taxPayments: 'Tax Payments'
        },
        financialReporting: {
          _self: 'Financial Reports',
          balanceSheet: 'Balance Sheet',
          profitAndLoss: 'Profit & Loss Statement',
          cashFlow: 'Cash Flow Statement'
        }
      }
    },
    logistics: {
      _self: 'Logistics',
      sales: {
        _self: 'Sales Management',
        customer: {
          _self: 'Customer Management',
          client: 'Clients',
          customers: 'Customer List',
          creditControl: 'Credit Control'
        },
        order: {
          _self: 'Order Management',
          order: 'Sales Orders',
          orderDetail: 'Order Details',
          orderTracking: 'Order Tracking'
        },
        delivery: {
          _self: 'Delivery Management',
          delivery: 'Delivery Notes',
          deliveryDetail: 'Delivery Details',
          shipping: 'Shipping Management'
        },
        billing: {
          _self: 'Billing Management',
          invoice: 'Invoice Management',
          invoiceDetail: 'Invoice Details',
          payment: 'Payment Management'
        },
        reporting: {
          _self: 'Reports & Analysis',
          salesReports: 'Sales Reports',
          performanceAnalysis: 'Performance Analysis'
        }
      },
      production: {
        _self: 'Production Management',
        bom: 'Bill of Materials (BOM)',
        routing: 'Routing',
        workOrder: {
          _self: 'Production Orders',
          create: 'Create Production Order',
          manage: 'Manage Production Orders',
          release: 'Release Production Orders',
          complete: 'Complete Production Orders'
        },
        capacityPlanning: {
          _self: 'Capacity Planning',
          workCenter: 'Work Centers',
          capacityEvaluation: 'Capacity Evaluation',
          capacityLeveling: 'Capacity Leveling'
        },
        productionScheduling: {
          _self: 'Production Scheduling',
          schedule: 'Scheduling',
          reschedule: 'Rescheduling'
        },
        productionExecution: {
          _self: 'Production Execution',
          confirm: 'Production Confirmation',
          goodsIssue: 'Goods Issue',
          goodsReceipt: 'Goods Receipt'
        },
        productionReporting: {
          _self: 'Production Reports',
          orderReports: 'Order Reports',
          capacityReports: 'Capacity Reports',
          efficiencyReports: 'Efficiency Reports'
        },
        qualityManagement: {
          _self: 'Quality Management',
          inspectionLot: 'Inspection Lots',
          resultsRecording: 'Results Recording',
          defectRecording: 'Defect Recording'
        }
      },
      material: {
        _self: 'Material Management',
        materialMaster: 'Material Master Data',
        materialCategory: 'Material Categories',
        materialUnit: 'Material Units',
        materialStock: {
          _self: 'Material Stock',
          stockOverview: 'Stock Overview',
          stockIn: 'Stock In',
          stockOut: 'Stock Out',
          stockTransfer: 'Stock Transfer',
          stockAdjustment: 'Stock Adjustment',
          stockCheck: 'Stock Check'
        },
        purchase: {
          _self: 'Purchase Management',
          purchaseRequisition: 'Purchase Requisitions',
          purchaseOrder: 'Purchase Orders',
          purchaseOrderDetail: 'Purchase Order Details',
          supplier: 'Supplier Management'
        },
        inventoryManagement: {
          _self: 'Inventory Management',
          goodsReceipt: 'Goods Receipt',
          goodsIssue: 'Goods Issue',
          transferPosting: 'Transfer Posting',
          stockOverview: 'Stock Overview'
        },
        valuation: {
          _self: 'Material Valuation',
          priceControl: 'Price Control',
          standardPrice: 'Standard Price',
          movingAveragePrice: 'Moving Average Price'
        },
        reporting: {
          _self: 'Reports & Analysis',
          stockReports: 'Stock Reports',
          purchaseReports: 'Purchase Reports',
          inventoryReports: 'Inventory Analysis Reports'
        }
      }
    },
    quality: {
      _self: 'Quality Management',
      inspection: {
        _self: 'Inspection Management',
        inspectionLot: 'Inspection Lots',
        resultsRecording: 'Results Recording',
        defectRecording: 'Defect Recording',
        usageDecision: 'Usage Decision'
      },
      qualityPlanning: {
        _self: 'Quality Planning',
        inspectionPlan: 'Inspection Plans',
        qualityInfoRecord: 'Quality Info Records',
        samplingProcedure: 'Sampling Procedures'
      },
      qualityControl: {
        _self: 'Quality Control',
        controlChart: 'Control Charts',
        qualityNotifications: 'Quality Notifications',
        correctiveActions: 'Corrective Actions'
      },
      qualityReporting: {
        _self: 'Quality Reports',
        inspectionReports: 'Inspection Reports',
        defectReports: 'Defect Reports',
        qualityAnalysis: 'Quality Analysis'
      }
    },
    service: {
      _self: 'Customer Service',
      serviceOrder: {
        _self: 'Service Orders',
        create: 'Create Service Order',
        manage: 'Manage Service Orders',
        complete: 'Complete Service Orders',
        cancel: 'Cancel Service Orders'
      },
      serviceContract: {
        _self: 'Service Contracts',
        create: 'Create Service Contract',
        manage: 'Manage Service Contracts',
        renew: 'Renew Service Contracts',
        terminate: 'Terminate Service Contracts'
      },
      customerInteraction: {
        _self: 'Customer Interaction',
        inquiries: 'Customer Inquiries',
        complaints: 'Customer Complaints',
        feedback: 'Customer Feedback'
      },
      serviceExecution: {
        _self: 'Service Execution',
        schedule: 'Service Scheduling',
        dispatch: 'Service Dispatch',
        execution: 'Service Execution',
        confirmation: 'Service Confirmation'
      },
      serviceReporting: {
        _self: 'Service Reports',
        orderReports: 'Service Order Reports',
        contractReports: 'Service Contract Reports',
        performanceReports: 'Service Performance Reports'
      }
    },
    equipment: {
      _self: 'Equipment Management',
      equipmentMaster: 'Equipment Master Data',
      maintenancePlanning: {
        _self: 'Maintenance Planning',
        preventiveMaintenance: 'Preventive Maintenance',
        maintenanceTaskList: 'Maintenance Task List',
        scheduling: 'Maintenance Scheduling'
      },
      maintenanceExecution: {
        _self: 'Maintenance Execution',
        workOrder: 'Maintenance Work Orders',
        confirmation: 'Maintenance Confirmation',
        breakdownMaintenance: 'Breakdown Maintenance'
      },
      maintenanceReporting: {
        _self: 'Maintenance Reports',
        equipmentReports: 'Equipment Reports',
        maintenanceHistory: 'Maintenance History',
        performanceAnalysis: 'Performance Analysis'
      },
      sparePartsManagement: {
        _self: 'Spare Parts Management',
        sparePartsInventory: 'Spare Parts Inventory',
        sparePartsProcurement: 'Spare Parts Procurement',
        sparePartsUsage: 'Spare Parts Usage'
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
        trainingPlan: 'Training Plans',
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
