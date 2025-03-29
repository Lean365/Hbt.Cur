export default {
  menu: {
    home: 'الصفحة الرئيسية',
    dashboard: {
      title: 'لوحة القيادة',
      workplace: 'مكان العمل',
      analysis: 'تحليل',
      monitor: 'مراقبة'
    },
    components: {
      title: 'المكونات',
      icons: 'الأيقونات'
    },
    about: {
      title: 'معلومات عنا',
      privacy: 'سياسة الخصوصية',
      terms: 'شروط الخدمة',
      index: 'حول Hbt'
    },
    admin: {
      _self: 'إدارة النظام',
      config: 'إعدادات النظام',
      language: 'إدارة اللغة',
      dicttype: 'أنواع القاموس',
      dictdata: 'بيانات القاموس',
      translation: 'إدارة الترجمة'
    },
    identity: {
      _self: 'إدارة الهوية',
      user: 'إدارة المستخدمين',
      role: 'إدارة الأدوار',
      dept: 'إدارة الأقسام',
      post: 'إدارة الوظائف',
      menu: 'إدارة القوائم',
      tenant: 'إدارة المستأجرين',
      oauth: 'إدارة OAuth'
    },
    audit: {
      _self: 'سجلات التدقيق',
      operlog: 'سجلات العمليات',
      loginlog: 'سجلات تسجيل الدخول',
      dbdifflog: 'سجلات اختلاف قاعدة البيانات',
      exceptionlog: 'سجلات الاستثناءات'
    },
    workflow: {
      _self: 'سير العمل',
      definition: 'تعريف سير العمل',
      instance: 'مثيل سير العمل',
      task: 'المهام',
      node: 'العقد',
      variable: 'المتغيرات',
      history: 'السجل'
    },
    realtime: {
      _self: 'المراقبة في الوقت الحقيقي',
      server: 'مراقبة الخادم',
      online: 'المستخدمون المتصلون',
      message: 'الرسائل المتصلة'
    },
    generator: {
      _self: 'توليد الكود',
      table: 'جداول قاعدة البيانات',
      tableDefine: 'الجداول المخصصة',
      template: 'قوالب الكود',
      config: 'إعدادات التوليد',
      api: 'وثائق API'
    },
    routine: {
      _self: 'الأعمال اليومية',
      file: 'إدارة الملفات',
      mail: 'إدارة البريد',
      mailTmpl: 'قوالب البريد',
      notice: 'الإشعارات',
      task: 'المهام',
      schedule: 'إدارة الجداول'
    },
    finance: {
      _self: 'المالية',
      management: {
        _self: 'المحاسبة الإدارية',
        cost: {
          _self: 'إدارة التكاليف',
          costFactors: 'عوامل التكلفة',
          costCenter: 'مركز التكلفة',
          profitCenter: 'مركز الربح',
          productCost: 'تكلفة المنتج',
          activityType: 'نوع النشاط',
          internalOrder: 'الأوامر الداخلية'
        },
        planning: {
          _self: 'إدارة التخطيط',
          costPlanning: 'تخطيط التكاليف',
          profitPlanning: 'تخطيط الأرباح',
          budgetControl: 'إدارة الميزانية'
        },
        reporting: {
          _self: 'التقارير والتحليل',
          costReports: 'تقارير التكاليف',
          profitReports: 'تقارير الأرباح',
          varianceAnalysis: 'تحليل الفروقات'
        }
      },
      financial: {
        _self: 'المحاسبة المالية',
        generalLedger: {
          _self: 'دفتر الأستاذ العام',
          account: 'الحسابات',
          accountType: 'أنواع الحسابات',
          journalEntry: 'إدخالات اليومية',
          reconciliation: 'التسوية',
          closing: 'الإغلاق الدوري'
        },
        accountsReceivable: {
          _self: 'الحسابات المدينة',
          customer: 'إدارة العملاء',
          invoice: 'فواتير العملاء',
          payment: 'مدفوعات العملاء',
          creditControl: 'إدارة الائتمان'
        },
        accountsPayable: {
          _self: 'الحسابات الدائنة',
          supplier: 'إدارة الموردين',
          invoice: 'فواتير الموردين',
          payment: 'مدفوعات الموردين',
          agingReport: 'تقارير الأعمار'
        },
        assetAccounting: {
          _self: 'محاسبة الأصول',
          assets: 'الأصول الثابتة',
          depreciation: 'إدارة الإهلاك',
          assetTransfer: 'نقل الأصول',
          assetRetirement: 'إخراج الأصول'
        },
        tax: {
          _self: 'إدارة الضرائب',
          taxCodes: 'رموز الضرائب',
          taxReporting: 'تقارير الضرائب',
          taxPayments: 'مدفوعات الضرائب'
        },
        financialReporting: {
          _self: 'التقارير المالية',
          balanceSheet: 'الميزانية العمومية',
          profitAndLoss: 'بيان الأرباح والخسائر',
          cashFlow: 'بيان التدفقات النقدية'
        }
      }
    },
    logistics: {
      _self: 'اللوجستيات',
      sales: {
        _self: 'إدارة المبيعات',
        customer: {
          _self: 'إدارة العملاء',
          client: 'العملاء',
          customers: 'قائمة العملاء',
          creditControl: 'إدارة الائتمان'
        },
        order: {
          _self: 'إدارة الطلبات',
          order: 'طلبات المبيعات',
          orderDetail: 'تفاصيل الطلب',
          orderTracking: 'تتبع الطلبات'
        },
        delivery: {
          _self: 'إدارة التسليم',
          delivery: 'مستندات التسليم',
          deliveryDetail: 'تفاصيل التسليم',
          shipping: 'إدارة الشحن'
        },
        billing: {
          _self: 'إدارة الفواتير',
          invoice: 'إدارة الفواتير',
          invoiceDetail: 'تفاصيل الفواتير',
          payment: 'إدارة المدفوعات'
        },
        reporting: {
          _self: 'التقارير والتحليل',
          salesReports: 'تقارير المبيعات',
          performanceAnalysis: 'تحليل الأداء'
        }
      },
      production: {
        _self: 'إدارة الإنتاج',
        bom: 'قائمة المواد (BOM)',
        routing: 'التوجيه',
        workOrder: {
          _self: 'أوامر العمل',
          create: 'إنشاء أمر عمل',
          manage: 'إدارة أوامر العمل',
          release: 'إصدار أوامر العمل',
          complete: 'إكمال أوامر العمل'
        },
        capacityPlanning: {
          _self: 'تخطيط السعة',
          workCenter: 'مراكز العمل',
          capacityEvaluation: 'تقييم السعة',
          capacityLeveling: 'تسوية السعة'
        },
        productionScheduling: {
          _self: 'جدولة الإنتاج',
          schedule: 'الجدولة',
          reschedule: 'إعادة الجدولة'
        },
        productionExecution: {
          _self: 'تنفيذ الإنتاج',
          confirm: 'تأكيد الإنتاج',
          goodsIssue: 'إصدار البضائع',
          goodsReceipt: 'استلام البضائع'
        },
        productionReporting: {
          _self: 'تقارير الإنتاج',
          orderReports: 'تقارير الأوامر',
          capacityReports: 'تقارير السعة',
          efficiencyReports: 'تقارير الكفاءة'
        },
        qualityManagement: {
          _self: 'إدارة الجودة',
          inspectionLot: 'دفعات التفتيش',
          resultsRecording: 'تسجيل النتائج',
          defectRecording: 'تسجيل العيوب'
        }
      },
      material: {
        _self: 'إدارة المواد',
        materialMaster: 'بيانات المواد الرئيسية',
        materialCategory: 'فئات المواد',
        materialUnit: 'وحدات المواد',
        materialStock: {
          _self: 'مخزون المواد',
          stockOverview: 'نظرة عامة على المخزون',
          stockIn: 'إدخال المخزون',
          stockOut: 'إخراج المخزون',
          stockTransfer: 'تحويل المخزون',
          stockAdjustment: 'تعديل المخزون',
          stockCheck: 'فحص المخزون'
        },
        purchase: {
          _self: 'إدارة المشتريات',
          purchaseRequisition: 'طلبات الشراء',
          purchaseOrder: 'أوامر الشراء',
          purchaseOrderDetail: 'تفاصيل أوامر الشراء',
          supplier: 'إدارة الموردين'
        },
        inventoryManagement: {
          _self: 'إدارة المخزون',
          goodsReceipt: 'استلام البضائع',
          goodsIssue: 'إصدار البضائع',
          transferPosting: 'تحويل القيود',
          stockOverview: 'نظرة عامة على المخزون'
        },
        valuation: {
          _self: 'تقييم المواد',
          priceControl: 'التحكم في الأسعار',
          standardPrice: 'السعر القياسي',
          movingAveragePrice: 'متوسط السعر المتحرك'
        },
        reporting: {
          _self: 'التقارير والتحليل',
          stockReports: 'تقارير المخزون',
          purchaseReports: 'تقارير المشتريات',
          inventoryReports: 'تقارير تحليل المخزون'
        }
      }
    },
    quality: {
      _self: 'إدارة الجودة',
      inspection: {
        _self: 'إدارة التفتيش',
        inspectionLot: 'دفعات التفتيش',
        resultsRecording: 'تسجيل النتائج',
        defectRecording: 'تسجيل العيوب',
        usageDecision: 'قرار الاستخدام'
      },
      qualityPlanning: {
        _self: 'تخطيط الجودة',
        inspectionPlan: 'خطط التفتيش',
        qualityInfoRecord: 'سجلات معلومات الجودة',
        samplingProcedure: 'إجراءات أخذ العينات'
      },
      qualityControl: {
        _self: 'مراقبة الجودة',
        controlChart: 'مخططات التحكم',
        qualityNotifications: 'إشعارات الجودة',
        correctiveActions: 'الإجراءات التصحيحية'
      },
      qualityReporting: {
        _self: 'تقارير الجودة',
        inspectionReports: 'تقارير التفتيش',
        defectReports: 'تقارير العيوب',
        qualityAnalysis: 'تحليل الجودة'
      }
    },
    service: {
      _self: 'خدمة العملاء',
      serviceOrder: {
        _self: 'أوامر الخدمة',
        create: 'إنشاء أمر خدمة',
        manage: 'إدارة أوامر الخدمة',
        complete: 'إكمال أوامر الخدمة',
        cancel: 'إلغاء أوامر الخدمة'
      },
      serviceContract: {
        _self: 'عقود الخدمة',
        create: 'إنشاء عقد خدمة',
        manage: 'إدارة عقود الخدمة',
        renew: 'تجديد عقود الخدمة',
        terminate: 'إنهاء عقود الخدمة'
      },
      customerInteraction: {
        _self: 'تفاعل العملاء',
        inquiries: 'استفسارات العملاء',
        complaints: 'شكاوى العملاء',
        feedback: 'ملاحظات العملاء'
      },
      serviceExecution: {
        _self: 'تنفيذ الخدمة',
        schedule: 'جدولة الخدمة',
        dispatch: 'إرسال الخدمة',
        execution: 'تنفيذ الخدمة',
        confirmation: 'تأكيد الخدمة'
      },
      serviceReporting: {
        _self: 'تقارير الخدمة',
        orderReports: 'تقارير أوامر الخدمة',
        contractReports: 'تقارير عقود الخدمة',
        performanceReports: 'تقارير الأداء'
      }
    },
    equipment: {
      _self: 'إدارة المعدات',
      equipmentMaster: 'بيانات المعدات الرئيسية',
      maintenancePlanning: {
        _self: 'تخطيط الصيانة',
        preventiveMaintenance: 'الصيانة الوقائية',
        maintenanceTaskList: 'قائمة مهام الصيانة',
        scheduling: 'جدولة الصيانة'
      },
      maintenanceExecution: {
        _self: 'تنفيذ الصيانة',
        workOrder: 'أوامر العمل للصيانة',
        confirmation: 'تأكيد الصيانة',
        breakdownMaintenance: 'صيانة الأعطال'
      },
      maintenanceReporting: {
        _self: 'تقارير الصيانة',
        equipmentReports: 'تقارير المعدات',
        maintenanceHistory: 'سجل الصيانة',
        performanceAnalysis: 'تحليل الأداء'
      },
      sparePartsManagement: {
        _self: 'إدارة قطع الغيار',
        sparePartsInventory: 'مخزون قطع الغيار',
        sparePartsProcurement: 'شراء قطع الغيار',
        sparePartsUsage: 'استخدام قطع الغيار'
      }
    },
    humanResources: {
      _self: 'إدارة الموارد البشرية',
      employeeManagement: {
        _self: 'إدارة الموظفين',
        employeeMaster: 'بيانات الموظفين الرئيسية',
        attendance: 'إدارة الحضور',
        leave: 'إدارة الإجازات',
        payroll: 'إدارة الرواتب',
        contractManagement: 'إدارة العقود'
      },
      recruitment: {
        _self: 'إدارة التوظيف',
        jobPosting: 'إعلانات الوظائف',
        candidateManagement: 'إدارة المرشحين',
        interviewScheduling: 'جدولة المقابلات',
        offerManagement: 'إدارة العروض'
      },
      training: {
        _self: 'إدارة التدريب',
        trainingPlan: 'خطط التدريب',
        trainingExecution: 'تنفيذ التدريب',
        trainingEvaluation: 'تقييم التدريب'
      },
      performance: {
        _self: 'إدارة الأداء',
        goalSetting: 'تحديد الأهداف',
        performanceReview: 'مراجعة الأداء',
        feedback: 'إدارة الملاحظات'
      },
      reporting: {
        _self: 'تقارير الموارد البشرية',
        employeeReports: 'تقارير الموظفين',
        attendanceReports: 'تقارير الحضور',
        payrollReports: 'تقارير الرواتب',
        performanceReports: 'تقارير الأداء'
      }
    }
  }
}
