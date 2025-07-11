export default {
  menu: {
    home: 'الرئيسية',
    dashboard: {
      title: 'لوحة التحكم',
      workplace: 'مساحة العمل',
      analysis: 'منصة التحليل',
      monitor: 'منصة المراقبة'
    },
    components: {
      title: 'المكونات',
      icons: 'الأيقونات'
    },
    about: {
      title: 'معلومات عنا',
      privacy: 'سياسة الخصوصية',
      terms: 'شروط الخدمة',
      index: 'عن Hbt'
    },
    core: {
      _self: 'الإدارة الأساسية',
      config: 'إعدادات النظام',
      language: 'إدارة اللغة',
      dict: 'إدارة القاموس',
    },
    identity: {
      _self: 'المصادقة',
      user: 'إدارة المستخدمين',
      role: 'إدارة الأدوار',
      dept: 'إدارة الأقسام',
      post: 'إدارة المناصب',
      menu: 'إدارة القوائم',
      tenant: 'إدارة المستأجرين',
      oauth: 'إدارة OAuth',
      profile: 'المعلومات الشخصية',
      changePassword: 'تغيير كلمة المرور'
    },
    audit: {
      _self: 'سجلات التدقيق',
      operlog: 'سجل العمليات',
      loginlog: 'سجل تسجيل الدخول',
      sqldifflog: 'سجل الاختلافات',
      exceptionlog: 'سجل الاستثناءات',
      auditlog: 'سجل التدقيق',
      quartzlog: 'سجل المهام',
      server: 'مراقبة الخادم'
    },
    workflow: {
      _self: 'سير العمل',
      overview: 'نظرة عامة على العملية',
      my: 'عملياتي',
      form: 'إدارة النماذج',
      definition: 'تعريف العملية',
      instance: 'مثيل العملية',
      task: 'مهام العمل',
      node: 'عقدة العملية',
      variable: 'متغيرات العملية',
      history: 'تاريخ العملية'
    },
    signalr: {
      _self: 'الاتصال المباشر',
      online: 'المستخدمون المتصلون',
      message: 'الرسائل المباشرة'
    },
    generator: {
      _self: 'مولد الكود',
      table: 'جداول قاعدة البيانات',
      tableDefine: 'تعريف أعمدة الجدول',
      template: 'قوالب الكود',
      config: 'إعدادات التوليد',
      api: 'وثائق API'
    },
    routine: {
      _self: 'المكتب اليومي',
      schedule: {
        _self: 'إدارة الجدول',
        myschedule: 'جدولي',
        dashboard: 'لوحة الجدول',
      },
      car: {
        _self: 'إدارة السيارات',
        info: 'معلومات السيارة',
        application: 'طلب السيارة',
        dashboard: 'لوحة السيارات',
        maintenance: 'صيانة السيارة',
      },
      email: {
        _self: 'إدارة البريد الإلكتروني',
        inbox: 'صندوق الوارد',
        drafts: 'المسودات',
        sent: 'المرسل',
        trash: 'سلة المحذوفات',
        template: 'قوالب البريد الإلكتروني',        
      },
      meeting: {
        _self: 'إدارة الاجتماعات',
        room: 'غرف الاجتماعات',
        mymeeting: 'اجتماعاتي',
        booking: 'حجز الاجتماعات',
        dashboard: 'لوحة الاجتماعات',
      },
      notice: { 
        _self: 'الإشعارات والإعلانات',
        message: {
          _self: 'إدارة الرسائل',
          mymessages: 'رسائلي',
          list: 'لوحة الرسائل',
        },
        announcement: {
          _self: 'إدارة الإعلانات',
          signoff: 'توقيع الإعلانات',
          list: 'قائمة الإعلانات',
        },
        notification: {
          _self: 'إدارة الإشعارات',
          ack: 'الإشعارات المقروءة',
          list: 'قائمة الإشعارات',
        },
      },
      hr: {
        _self: 'الموارد البشرية والحضور',
        recruitment: {
          _self: 'إدارة التوظيف',
          apply: 'طلب التوظيف',
          approval: 'موافقة التوظيف',
          list: 'قائمة التوظيف',

        },
        transfer: {
          _self: 'إدارة النقل',
          apply: 'طلب النقل',
          approval: 'موافقة النقل',
          list: 'قائمة النقل',
        },
        leave: {
          _self: 'إدارة الإجازات',
          apply: 'طلب الإجازة',
          approval: 'موافقة الإجازة',
          list: 'قائمة الإجازات',
        },
        trip: {
          _self: 'إدارة السفر',
          apply: 'طلب السفر',
          approval: 'موافقة السفر',
          list: 'قائمة السفر',
        },
        overtime: {
          _self: 'إدارة العمل الإضافي',
          apply: 'طلب العمل الإضافي',
          approval: 'موافقة العمل الإضافي',
          list: 'قائمة العمل الإضافي',
      },
    },
    expense:{
      _self: 'إدارة المصاريف',
      daily: {
        _self: 'المصاريف اليومية',
        apply: 'طلب المصاريف',
        approve: 'موافقة المصاريف',
        list: 'قائمة المصاريف',
      },
      travel: {
        _self: 'مصاريف السفر',
        apply: 'طلب مصاريف السفر',
        approve: 'موافقة مصاريف السفر',
        list: 'قائمة مصاريف السفر',
      },
    },
    file:{
      _self: 'إدارة الملفات',
      daily: {
        _self: 'الملفات اليومية',
        list: 'قائمة الملفات',
      },
      iso: {
        _self: 'ملفات ISO',
        version: 'الإصدار',
        signoff: 'التوقيع',
        list: 'ملفات ISO',
      },
      document: { 
        _self: 'إدارة الوثائق',
        version: 'الإصدار',
        signoff: 'التوقيع',
        list: 'قائمة الوثائق',
      },
    },
    officesupplies:{
      _self: 'المستلزمات المكتبية',
      inventory:{
        _self: 'إدارة المخزون',
        requisition: 'إدارة الطلبات',
        inbound: 'إدارة الوارد',
        stocktaking: 'إدارة الجرد',
      },
      usage:{
        _self: 'إدارة الاستخدام',
        apply: 'طلب الاستخدام',
        approve: 'موافقة الاستخدام',
        receive: 'سجل الاستخدام',
      }
    },
    book:{
      _self: 'إدارة الكتب',
      inventory:{
        _self: 'إدارة المخزون',
        requisition: 'إدارة الطلبات',
        inbound: 'إدارة الوارد',
        list: 'قائمة الكتب',
        stocktaking: 'إدارة الجرد',
      },
      usage:{
        _self: 'إدارة الاستخدام',
        card: 'بطاقة الإعارة',
        borrow: 'الإعارة',
        return: 'الإرجاع',
      }

    },
    medical:{
      _self: 'الإدارة الطبية',
      medicine:{
        _self: 'إدارة المخزون',
        requisition: 'إدارة الطلبات',
        inbound: 'إدارة الوارد',
        list: 'قائمة الأدوية',
        stocktaking: 'إدارة الجرد',
      },
      usage:{
        _self: 'إدارة الاستخدام',
        archive: 'الأرشيف',
        receive: 'استلام الدواء',
        cost: 'التكلفة',
      }

    },
  },
  accounting: {
      _self: 'المحاسبة',
      financial: {
        _self: 'المحاسبة الإدارية',
        company: "معلومات الشركة",
        account: 'الحسابات المحاسبية',
        companyaccount: 'حسابات الشركة',
        ledger: 'الدفتر العام',
        payable: 'المدينون',
        receivable: 'الذمم المدينة',
        fixedasset: 'الأصول الثابتة',
        bank: 'معلومات البنك',

      },
      controlling: {
        _self: 'المحاسبة الرقابية',
        costelement: 'عناصر التكلفة',
        costcenter: 'مراكز التكلفة',
        profitcenter: 'مراكز الربح',
        accountsReceivable: 'الذمم المدينة',
        accountsPayable: 'المدينون',
        assetAccounting: 'محاسبة الأصول',
        tax: 'إدارة الضرائب',
        financialReporting: 'التقارير المالية'      
    },
    budget:{
      _self: 'الميزانية الشاملة',
        formulation: {
          _self: 'إعداد الميزانية',
          sales: {
            _self: 'ميزانية المبيعات',
            cost: 'تكلفة المبيعات',
            rolling: 'المبيعات المتداولة',
          },
          production: {
            _self: 'ميزانية الإنتاج',
            auxiliary: 'المواد المساعدة للإنتاج',
            labor: 'العمالة الإنتاجية',
            manufacturing: 'التصنيع',
          },
          cost: {
            _self: 'ميزانية التكلفة',
            directmaterial: 'المواد المباشرة',
            directlabor: 'العمالة المباشرة',
            indirectlabor: 'العمالة غير المباشرة',
            manufacturing: 'مصاريف التصنيع',
          },
          expense: {
            _self: 'ميزانية المصاريف',
            sales: 'مصاريف المبيعات',
            management: 'مصاريف الإدارة',
            financial: 'المصاريف المالية',
          },
          financial: {
            _self: 'الميزانية المالية',
            cashflow: 'التدفق النقدي',
            balancesheet: 'الميزانية العمومية',
            income: 'قائمة الدخل',
          },
        },
        control: {
          _self: 'التحكم في الميزانية',
          dashboard: 'لوحة الميزانية',
          approval: 'موافقة الميزانية',
        },   
  },
},
    logistics: {
      _self: 'إدارة اللوجستيات',
      equipment: {
        _self: 'إدارة المعدات',
        data: 'البيانات الرئيسية للمعدات',
        location: 'موقع المعدات',
        material: 'ارتباط المواد',
        workorder: 'أمر العمل'

      },
      material: {
        _self: 'إدارة المواد',
        material:{
          _self: 'إدارة المواد',
          material: 'البيانات الرئيسية للمواد',
          plant: 'معلومات المصنع',
          master: 'بيانات المواد',
          plantmaster: 'مواد المصنع',
          vendor: 'معلومات البائع',
          supplier: 'معلومات المورد',
        },
        purchase:{
          _self: 'إدارة المشتريات',
          vendor: 'معلومات البائع',
          supplier: 'معلومات المورد',
          price: 'سعر الشراء',
          requisition: 'طلب الشراء',
          order: 'أمر الشراء',

        },



      },
      production: {
        _self: 'إدارة الإنتاج',
        bom: 'قائمة المواد',
        change: {
          _self: 'تغيير التصميم',
          implementation: 'تنفيذ التغيير',
          techcontact: 'الاتصال التقني',
          material: 'تأكيد المواد',
          query: 'استعلام التغيير',
          oldproduct: 'التحكم في المنتجات القديمة',
          sop: 'تأكيد SOP',
          batch: 'دفعة الإدخال',
          input: {
            _self: 'إدخال التغيير',
            gijutsu: 'قسم التقنية',
            seikan: 'قسم إدارة الإنتاج',
            koubai: 'قسم المشتريات',
            uketsuke: 'قسم الاستقبال',
            bukan: 'قسم الإدارة',
            seizou2: 'قسم الإنتاج الثاني',
            seizou1: 'قسم الإنتاج الأول',
            hinkan: 'قسم مراقبة الجودة',
            seizougijutsu: 'قسم تقنية الإنتاج',
  
          }
        },
        workcenter: 'مركز العمل',
        order: 'أمر الإنتاج',
        kanban: 'الكانبان',
        oph:{
          _self: 'إدارة OPH',
          workshop1: {
            _self: 'قسم الإنتاج الأول',
            output: 'التقرير اليومي للإنتاج',
            defect: 'عيوب الإنتاج',
            worktime: 'وقت العمل الإنتاجي',
            productionReport: 'تقرير الإنتاج',
            defectSummary: 'ملخص العيوب',
            worktimeReport: 'تقرير وقت العمل'
          },
          workshop2: {
            _self: 'قسم الإنتاج الثاني',
            output: 'التقرير اليومي للإنتاج',
            inspection: 'سجل الفحص',
            repair: 'سجل الإصلاح',
            worktime: 'وقت العمل الإنتاجي',
            productionReport: 'تقرير الإنتاج',
            inspectionReport: 'تقرير الفحص',
            repairReport: 'تقرير الإصلاح',
            worktimeReport: 'تقرير وقت العمل'
          }
        }

      },
      project: {
        _self: 'إدارة المشاريع',
        define: 'تعريف المشروع',
        cost: 'خطة التكلفة',
        resource: 'خطة الموارد',
        schedule: 'خطة الجدول',

      },
      quality: {
        _self: 'إدارة الجودة',
        item: 'عناصر الفحص',
        receiving: 'فحص المواد الواردة',
        process: 'فحص العملية',
        storage: 'فحص التخزين',
        return: 'فحص الإرجاع',
  
      },
      sales: {
        _self: 'إدارة المبيعات',
        customer: 'معلومات العميل',
        client: 'معلومات العميل',
        price: 'سعر المبيعات',
        order: 'أمر المبيعات',
      },
      service: {
        _self: 'خدمة العملاء',
        item: 'عناصر الخدمة',
        contract: 'عقد الخدمة',
        request: 'طلب الخدمة',
        workorder: 'أمر عمل الخدمة',
        timesheet: 'سجل الوقت',
        consumption: 'استهلاك المواد',
        outsourcing: 'الخدمات الخارجية'

      },
      complaint: {
        _self: 'إدارة شكاوى العملاء',
        notice: 'إشعار الجودة',
        mark: 'تفاصيل شكوى العميل',
        analysis: 'تحليل السبب',
        corrective: 'الإجراءات التصحيحية',
        return: 'تنفيذ الإرجاع',
        followUp: 'المتابعة'
      }
    },
    humanResources: {
      _self: 'إدارة الموارد البشرية',
      employeeManagement: {
        _self: 'إدارة الموظفين',
        employeeMaster: 'البيانات الرئيسية للموظفين',
        attendance: 'إدارة الحضور',
        leave: 'إدارة الإجازات',
        payroll: 'إدارة الرواتب',
        contractManagement: 'إدارة العقود'
      },
      recruitment: {
        _self: 'إدارة التوظيف',
        jobPosting: 'نشر الوظائف',
        candidateManagement: 'إدارة المرشحين',
        interviewScheduling: 'جدولة المقابلات',
        offerManagement: 'إدارة العروض'
      },
      training: {
        _self: 'إدارة التدريب',
        trainingPlan: 'خطة التدريب',
        trainingExecution: 'تنفيذ التدريب',
        trainingEvaluation: 'تقييم التدريب'
      },
      performance: {
        _self: 'إدارة الأداء',
        goalSetting: 'تحديد الأهداف',
        performanceReview: 'تقييم الأداء',
        feedback: 'إدارة التغذية الراجعة'
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
