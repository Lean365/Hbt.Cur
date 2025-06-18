import { countReset } from "node:console";

export default {
  menu: {
    home: 'الرئيسية',
    dashboard: {
      title: 'لوحة التحكم',
      workplace: 'مساحة العمل',
      analysis: 'التحليل',
      monitor: 'المراقبة'
    },
    components: {
      title: 'المكونات',
      icons: 'الأيقونات'
    },
    about: {
      title: 'حول',
      privacy: 'سياسة الخصوصية',
      terms: 'شروط الاستخدام',
      index: 'حول Hbt'
    },
    core: {
      _self: 'إدارة النظام',
      config: 'إعدادات النظام',
      language: 'إدارة اللغات',
      dict: 'إدارة القواميس'
    },
    identity: {
      _self: 'المصادقة',
      user: 'إدارة المستخدمين',
      role: 'إدارة الأدوار',
      dept: 'إدارة الأقسام',
      post: 'إدارة المناصب',
      menu: 'إدارة القوائم',
      tenant: 'إدارة المستأجرين',
      oauth: 'إدارة OAuth'
    },
    audit: {
      _self: 'التدقيق',
      operlog: 'سجل العمليات',
      loginlog: 'سجل تسجيل الدخول',
      sqldifflog: 'سجل اختلافات قاعدة البيانات',
      exceptionlog: 'سجل الاستثناءات',
      auditlog: 'سجل التدقيق',
      quartzlog: 'سجل المهام'
    },
    workflow: {
      _self: 'سير العمل',
      form: 'إدارة النماذج',
      definition: 'تعريف العملية',
      instance: 'مثيل العملية',
      task: 'المهمة',
      node: 'عقدة العملية',
      variable: 'متغير العملية',
      history: 'تاريخ العملية'
    },
    signalr: {
      _self: 'الوقت الفعلي',
      online: 'المستخدمون',
      message: 'الرسائل'
    },
    generator: {
      _self: 'إنشاء الكود',
      table: 'جدول قاعدة البيانات',
      tableDefine: 'جدول مخصص',
      template: 'قالب الكود',
      config: 'إعدادات الإنشاء',
      api: 'توثيق API'
    },
    routine: {
      _self: 'المهام اليومية',
      vehicle: {
        _self: 'إدارة المركبات',
        vehicleMaster: {
          _self: 'البيانات الرئيسية للمركبات',
          vehicleInfo: 'معلومات المركبة',
          driverInfo: 'معلومات السائق',
          maintenance: 'صيانة المركبة'
        },
        vehicleBooking: {
          _self: 'حجز المركبات',
          newBooking: 'حجز جديد',
          bookingList: 'قائمة الحجوزات',
          bookingApproval: 'موافقة الحجز'
        },
        vehicleDispatch: {
          _self: 'إرسال المركبات',
          dispatchPlan: 'خطة الإرسال',
          realTimeTracking: 'التتبع المباشر',
          dispatchHistory: 'سجل الإرسال'
        },
        vehicleReporting: {
          _self: 'تقارير المركبات',
          usageReport: 'تقرير الاستخدام',
          costReport: 'تقرير التكاليف',
          maintenanceReport: 'تقرير الصيانة'
        }
      },
      file: 'إدارة الملفات',
      mail: 'إدارة البريد',
      mailTmpl: 'قالب البريد',
      meeting: {
        _self: 'إدارة الاجتماعات',
        meetingRoom: {
          _self: 'إدارة قاعات الاجتماعات',
          roomInfo: 'معلومات القاعة',
          roomBooking: 'حجز القاعة',
          roomSchedule: 'جدولة القاعة'
        },
        meetingPlan: {
          _self: 'تخطيط الاجتماعات',
          newMeeting: 'اجتماع جديد',
          meetingList: 'قائمة الاجتماعات',
          meetingApproval: 'موافقة الاجتماع'
        },
        meetingExecution: {
          _self: 'تنفيذ الاجتماعات',
          attendance: 'الحضور',
          minutes: 'محضر الاجتماع',
          followUp: 'المتابعة'
        },
        meetingReporting: {
          _self: 'تقارير الاجتماعات',
          meetingReport: 'تقرير الاجتماع',
          attendanceReport: 'تقرير الحضور',
          costReport: 'تقرير التكاليف'
        }
      },
      notice: 'الإشعارات',
      schedule: 'إدارة الجداول',
      quartz: 'المهمة'
    },
    finance: {
      _self: 'المالية',
      management: {
        _self: 'المحاسبة الإدارية',
        cost: {
          _self: 'إدارة التكاليف',
          costFactors: 'أنواع التكاليف',
          costCenter: 'مركز التكلفة',
          profitCenter: 'مركز الربح',
          productCost: 'تكلفة المنتج',
          activityType: 'نوع النشاط',
          internalOrder: 'الطلب الداخلي'
        },
        planning: {
          _self: 'إدارة التخطيط',
          costPlanning: 'تخطيط التكاليف',
          profitPlanning: 'تخطيط الأرباح',
          budgetControl: 'التحكم في الميزانية'
        },
        reporting: {
          _self: 'التقارير والتحليل',
          costReports: 'تقارير التكاليف',
          profitReports: 'تقارير الأرباح',
          varianceAnalysis: 'تحليل الانحرافات'
        }
      },
      financial: {
        _self: 'المحاسبة المالية',
        generalLedger: {
          _self: 'دفتر الأستاذ العام',
          account: 'الحساب',
          accountType: 'نوع الحساب',
          journalEntry: 'قيد اليومية',
          reconciliation: 'التسوية',
          closing: 'الإقفال'
        },
        accountsReceivable: {
          _self: 'الحسابات المدينة',
          customer: 'إدارة العملاء',
          invoice: 'فاتورة العميل',
          payment: 'دفعة العميل',
          creditControl: 'التحكم في الائتمان'
        },
        accountsPayable: {
          _self: 'الحسابات الدائنة',
          supplier: 'إدارة الموردين',
          invoice: 'فاتورة المورد',
          payment: 'دفعة المورد',
          agingReport: 'تحليل المبالغ المستحقة'
        },
        assetAccounting: {
          _self: 'محاسبة الأصول',
          assets: 'الأصول الثابتة',
          depreciation: 'إدارة الإهلاك',
          assetTransfer: 'نقل الأصول',
          assetRetirement: 'تصفية الأصول'
        },
        tax: {
          _self: 'إدارة الضرائب',
          taxCodes: 'إدارة الرموز الضريبية',
          taxReporting: 'الإقرار الضريبي',
          taxPayments: 'مدفوعات الضرائب'
        },
        financialReporting: {
          _self: 'التقارير المالية',
          balanceSheet: 'الميزانية العمومية',
          profitAndLoss: 'قائمة الدخل',
          cashFlow: 'قائمة التدفق النقدي'
        }
      }
    },
    logistics: {
      _self: 'اللوجستيات',
      equipment: {
        _self: 'إدارة المعدات',
        equipmentMaster: 'البيانات الرئيسية للمعدات',
        maintenancePlanning: {
          _self: 'تخطيط الصيانة',
          preventiveMaintenance: 'الصيانة الوقائية',
          maintenanceTaskList: 'قائمة مهام الصيانة',
          scheduling: 'جدولة الصيانة'
        },
        maintenanceExecution: {
          _self: 'تنفيذ الصيانة',
          workOrder: 'أمر العمل',
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
      material: {
        _self: 'إدارة المواد',
        materialMaster: 'البيانات الرئيسية للمواد',
        materialCategory: 'فئة المادة',
        materialUnit: 'وحدة المادة',
        materialStock: {
          _self: 'مخزون المواد',
          stockOverview: 'نظرة عامة على المخزون',
          stockIn: 'دخول المخزون',
          stockOut: 'خروج المخزون',
          stockTransfer: 'نقل المخزون',
          stockAdjustment: 'تعديل المخزون',
          stockCheck: 'فحص المخزون'
        },
        purchase: {
          _self: 'المشتريات',
          purchaseRequisition: 'طلب الشراء',
          purchaseOrder: 'أمر الشراء',
          purchaseOrderDetail: 'تفاصيل أمر الشراء',
          supplier: 'إدارة الموردين'
        },
        inventoryManagement: {
          _self: 'إدارة المخزون',
          goodsReceipt: 'استلام البضائع',
          goodsIssue: 'إصدار البضائع',
          transferPosting: 'النقل',
          stockOverview: 'نظرة عامة على المخزون'
        },
        valuation: {
          _self: 'التقييم',
          priceControl: 'التحكم في الأسعار',
          standardPrice: 'السعر القياسي',
          movingAveragePrice: 'متوسط السعر المتحرك'
        },
        reporting: {
          _self: 'التقارير والتحليل',
          stockReports: 'تقارير المخزون',
          purchaseReports: 'تقارير المشتريات',
          inventoryReports: 'تقارير المخزون'
        }
      },
      production: {
        _self: 'إدارة الإنتاج',
        bom: 'قائمة المواد',
        routing: 'مسار التصنيع',
        workOrder: {
          _self: 'أمر التصنيع',
          create: 'إنشاء الأمر',
          manage: 'إدارة الأمر',
          release: 'إصدار الأمر',
          complete: 'إكمال الأمر'
        },
        capacityPlanning: {
          _self: 'تخطيط الطاقة الإنتاجية',
          workCenter: 'مركز العمل',
          capacityEvaluation: 'تقييم الطاقة الإنتاجية',
          capacityLeveling: 'تسوية الطاقة الإنتاجية'
        },
        productionScheduling: {
          _self: 'جدولة الإنتاج',
          schedule: 'الجدولة',
          reschedule: 'إعادة الجدولة'
        },
        productionExecution: {
          _self: 'تنفيذ الإنتاج',
          confirm: 'تأكيد الإنتاج',
          goodsIssue: 'إصدار المواد',
          goodsReceipt: 'استلام المنتجات'
        },
        productionReporting: {
          _self: 'تقارير الإنتاج',
          orderReports: 'تقارير الأوامر',
          capacityReports: 'تقارير الطاقة الإنتاجية',
          efficiencyReports: 'تقارير الكفاءة'
        },
        qualityManagement: {
          _self: 'إدارة الجودة',
          inspectionLot: 'دفعة الفحص',
          resultsRecording: 'تسجيل النتائج',
          defectRecording: 'تسجيل العيوب'
        }
      },
      project: {
        _self: 'إدارة المشاريع',
        projectMaster: {
          _self: 'البيانات الرئيسية للمشروع',
          projectDefinition: 'تعريف المشروع',
          projectStructure: 'هيكل المشروع',
          projectTeam: 'فريق المشروع',
          projectCalendar: 'تقويم المشروع'
        },
        projectPlanning: {
          _self: 'تخطيط المشروع',
          workBreakdown: 'هيكل تقسيم العمل',
          scheduling: 'الجدولة',
          resourcePlanning: 'تخطيط الموارد',
          costPlanning: 'تخطيط التكاليف'
        },
        projectExecution: {
          _self: 'تنفيذ المشروع',
          taskManagement: 'إدارة المهام',
          progressTracking: 'تتبع التقدم',
          resourceManagement: 'إدارة الموارد',
          costControl: 'التحكم في التكاليف'
        },
        projectMonitoring: {
          _self: 'مراقبة المشروع',
          progressReports: 'تقارير التقدم',
          resourceReports: 'تقارير الموارد',
          costReports: 'تقارير التكاليف',
          riskManagement: 'إدارة المخاطر'
        },
        projectClosure: {
          _self: 'إغلاق المشروع',
          finalReport: 'التقرير النهائي',
          lessonsLearned: 'الدروس المستفادة',
          projectArchive: 'أرشيف المشروع'
        }
      },
      quality: {
        _self: 'إدارة الجودة',
        inspection: {
          _self: 'إدارة الفحص',
          inspectionLot: 'دفعة الفحص',
          resultsRecording: 'تسجيل النتائج',
          defectRecording: 'تسجيل العيوب',
          usageDecision: 'قرار الاستخدام'
        },
        qualityPlanning: {
          _self: 'تخطيط الجودة',
          inspectionPlan: 'خطة الفحص',
          qualityInfoRecord: 'سجل معلومات الجودة',
          samplingProcedure: 'إجراء أخذ العينات'
        },
        qualityControl: {
          _self: 'مراقبة الجودة',
          controlChart: 'مخطط المراقبة',
          qualityNotifications: 'إشعارات الجودة',
          correctiveActions: 'الإجراءات التصحيحية'
        },
        qualityReporting: {
          _self: 'تقارير الجودة',
          inspectionReports: 'تقارير الفحص',
          defectReports: 'تقارير العيوب',
          qualityAnalysis: 'تحليل الجودة'
        }
      },
      sales: {
        _self: 'المبيعات',
        customer: {
          _self: 'إدارة العملاء',
          client: 'العميل',
          customers: 'قائمة العملاء',
          creditControl: 'التحكم في الائتمان'
        },
        order: {
          _self: 'إدارة الطلبات',
          order: 'طلب المبيعات',
          orderDetail: 'تفاصيل الطلب',
          orderTracking: 'تتبع الطلب'
        },
        delivery: {
          _self: 'إدارة التسليم',
          delivery: 'إذن التسليم',
          deliveryDetail: 'تفاصيل التسليم',
          shipping: 'إدارة الشحن'
        },
        billing: {
          _self: 'الفواتير',
          invoice: 'إدارة الفواتير',
          invoiceDetail: 'تفاصيل الفاتورة',
          payment: 'إدارة المدفوعات'
        },
        reporting: {
          _self: 'التقارير والتحليل',
          salesReports: 'تقارير المبيعات',
          performanceAnalysis: 'تحليل الأداء'
        }
      },
      service: {
        _self: 'الخدمة',
        serviceOrder: {
          _self: 'أمر الخدمة',
          create: 'إنشاء الأمر',
          manage: 'إدارة الأمر',
          complete: 'إكمال الأمر',
          cancel: 'إلغاء الأمر'
        },
        serviceContract: {
          _self: 'عقد الخدمة',
          create: 'إنشاء العقد',
          manage: 'إدارة العقد',
          renew: 'تجديد العقد',
          terminate: 'إنهاء العقد'
        },
        customerInteraction: {
          _self: 'تفاعل العملاء',
          inquiries: 'استفسارات العملاء',
          complaints: 'الشكاوى',
          feedback: 'التغذية الراجعة'
        },
        serviceExecution: {
          _self: 'تنفيذ الخدمة',
          schedule: 'الجدولة',
          dispatch: 'الإرسال',
          execution: 'التنفيذ',
          confirmation: 'التأكيد'
        },
        serviceReporting: {
          _self: 'تقارير الخدمة',
          orderReports: 'تقارير الأوامر',
          contractReports: 'تقارير العقود',
          performanceReports: 'تقارير الأداء'
        }
      }
    },
    humanResources: {
      _self: 'الموارد البشرية',
      employee: {
        _self: 'إدارة الموظفين',
        employeeInfo: 'معلومات الموظف',
        employeeProfile: 'ملف الموظف',
        employeeContract: 'عقد الموظف',
        employeeAttendance: 'حضور الموظف',
        employeeLeave: 'إجازات الموظف',
        employeePerformance: 'أداء الموظف'
      },
      recruitment: {
        _self: 'إدارة التوظيف',
        jobPosting: 'نشر الوظيفة',
        candidate: 'إدارة المرشحين',
        interview: 'إدارة المقابلات',
        offer: 'إدارة العروض',
        onboarding: 'الانضمام'
      },
      training: {
        _self: 'إدارة التدريب',
        trainingPlan: 'خطة التدريب',
        trainingCourse: 'دورة التدريب',
        trainingRecord: 'سجل التدريب',
        trainingEvaluation: 'تقييم التدريب'
      },
      performance: {
        _self: 'إدارة الأداء',
        performancePlan: 'خطة الأداء',
        performanceAppraisal: 'تقييم الأداء',
        performanceReview: 'مراجعة الأداء',
        performanceImprovement: 'تحسين الأداء'
      },
      compensation: {
        _self: 'إدارة التعويضات',
        salary: 'إدارة الرواتب',
        bonus: 'إدارة المكافآت',
        benefits: 'إدارة المزايا',
        payroll: 'كشف الرواتب'
      },
      reporting: {
        _self: 'التقارير والتحليل',
        employeeReports: 'تقارير الموظفين',
        recruitmentReports: 'تقارير التوظيف',
        trainingReports: 'تقارير التدريب',
        performanceReports: 'تقارير الأداء',
        compensationReports: 'تقارير التعويضات'
      }
    }
  }
}
