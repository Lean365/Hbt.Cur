export default {
  identity: {
    auth: {
      login: {
        title: 'تسجيل الدخول',
        tenantId: 'إعدادات المستأجر',
        username: 'اسم المستخدم',
        password: 'كلمة المرور',
        rememberMe: 'تذكرني',
        forgotPassword: 'نسيت كلمة المرور',
        submit: 'تسجيل الدخول',
        register: 'تسجيل',
        success: 'تم تسجيل الدخول بنجاح',
        error: {
          invalidCredentials: 'اسم المستخدم أو كلمة المرور غير صحيحة',
          accountLocked: 'الحساب مقفل',
          accountDisabled: 'الحساب معطل',
          accountExpired: 'الحساب منتهي الصلاحية',
          credentialsExpired: 'كلمة المرور منتهية الصلاحية',
          invalidCaptcha: 'رمز التحقق غير صحيح',
          invalidTenant: 'المستأجر غير صالح',
          invalidDevice: 'معلومات الجهاز غير صالحة',
          invalidGrant: 'المنحة غير صالحة',
          tooManyAttempts: 'محاولات تسجيل الدخول كثيرة جداً، يرجى المحاولة لاحقاً'
        },
        noToken: 'لا يوجد رمز وصول في استجابة تسجيل الدخول',
        otherLogin: 'طرق تسجيل دخول أخرى',
        form: {
          usernameRequired: 'الرجاء إدخال اسم المستخدم',
          passwordRequired: 'الرجاء إدخال كلمة المرور',
          tenantIdRequired: 'الرجاء اختيار إعدادات المستأجر'
        }
      },
      register: {
        title: 'تسجيل المستخدم',
        subtitle: 'يرجى إكمال تسجيل المستخدم خطوة بخطوة',
        step1: 'التحقق من الكابتشا',
        step2: 'المعلومات الأساسية',
        step3: 'معلومات أخرى',
        step4: 'إعدادات الصلاحيات',
        username: 'اسم المستخدم',
        usernamePlaceholder: 'يرجى إدخال اسم المستخدم',
        password: 'كلمة المرور',
        passwordPlaceholder: 'يرجى إدخال كلمة المرور',
        confirmPassword: 'تأكيد كلمة المرور',
        confirmPasswordPlaceholder: 'يرجى تأكيد كلمة المرور',
        email: 'البريد الإلكتروني',
        emailPlaceholder: 'يرجى إدخال البريد الإلكتروني',
        nickName: 'الاسم المستعار',
        nickNamePlaceholder: 'يرجى إدخال الاسم المستعار',
        realName: 'الاسم الحقيقي',
        realNamePlaceholder: 'يرجى إدخال الاسم الحقيقي',
        fullName: 'الاسم الكامل',
        fullNamePlaceholder: 'يرجى إدخال الاسم الكامل',
        englishName: 'الاسم بالإنجليزية',
        englishNamePlaceholder: 'يرجى إدخال الاسم بالإنجليزية',
        phoneNumber: 'رقم الهاتف',
        phoneNumberPlaceholder: 'يرجى إدخال رقم الهاتف',
        gender: 'الجنس',
        genderUnknown: 'غير محدد',
        genderMale: 'ذكر',
        genderFemale: 'أنثى',
        userType: 'نوع المستخدم',
        userTypePlaceholder: 'يرجى اختيار نوع المستخدم',
        userTypeNormal: 'مستخدم عادي',
        userTypeAdmin: 'مدير',
        status: 'الحالة',
        statusPlaceholder: 'يرجى اختيار الحالة',
        statusNormal: 'عادي',
        statusDisabled: 'معطل',
        deptId: 'معرف القسم',
        deptIdPlaceholder: 'يرجى إدخال معرف القسم',
        roleIds: 'الأدوار',
        roleIdsPlaceholder: 'يرجى اختيار الأدوار',
        roleUser: 'مستخدم',
        roleAdmin: 'مدير',
        postIds: 'المناصب',
        postIdsPlaceholder: 'يرجى اختيار المناصب',
        postEmployee: 'موظف',
        postManager: 'مدير',
        deptIds: 'الأقسام',
        deptIdsPlaceholder: 'يرجى اختيار الأقسام',
        deptIT: 'قسم تقنية المعلومات',
        deptHR: 'قسم الموارد البشرية',
        remark: 'ملاحظة',
        remarkPlaceholder: 'يرجى إدخال ملاحظة',
        agreement: 'لقد قرأت ووافقت على',
        agreementPrefix: 'لقد قرأت ووافقت على',
        agreementLink: 'اتفاقية المستخدم',
        agreementSuffix: '',
        agreementTitle: 'اتفاقية تسجيل المستخدم',
        agreementContent: 'يرجى قراءة والموافقة على هذه الاتفاقية قبل التسجيل.',
        captcha: 'الكابتشا',
        submit: 'إكمال التسجيل',
        nextStep: 'الخطوة التالية',
        back: 'الخطوة السابقة',
        backToLogin: 'العودة لتسجيل الدخول',
        login: 'تسجيل الدخول بحساب موجود',
        success: 'تم التسجيل بنجاح',
        successTitle: 'تم التسجيل بنجاح',
        successSubtitle: 'تم إنشاء حسابك بنجاح، يرجى تسجيل الدخول بحسابك الجديد',
        successMessage: 'تم تسجيل المستخدم {userName} بنجاح',
        step1Success: 'تم التحقق من الكابتشا بنجاح',
        step2Success: 'تم إكمال المعلومات الأساسية',
        step3Success: 'تم إكمال المعلومات الأخرى',
        step4Success: 'تم إكمال إعدادات الصلاحيات',
        error: {
          step1Failed: 'فشل التحقق من الكابتشا',
          step2Failed: 'فشل التحقق من المعلومات الأساسية',
          step3Failed: 'فشل التحقق من المعلومات الأخرى',
          step4Failed: 'فشل إعداد الصلاحيات',
          unknown: 'فشل التسجيل، يرجى المحاولة لاحقاً'
        },
        form: {
          usernameRequired: 'يرجى إدخال اسم المستخدم',
          usernameLength: 'يجب أن يكون طول اسم المستخدم بين 3-20 حرفاً',
          usernameFormat: 'يمكن أن يحتوي اسم المستخدم على أحرف وأرقام وشرطة سفلية فقط',
          emailRequired: 'يرجى إدخال البريد الإلكتروني',
          emailFormat: 'يرجى إدخال تنسيق بريد إلكتروني صحيح',
          captchaRequired: 'يرجى إدخال الكابتشا',
          nickNameRequired: 'يرجى إدخال الاسم المستعار',
          nickNameLength: 'يجب أن يكون طول الاسم المستعار بين 2-20 حرفاً',
          realNameRequired: 'يرجى إدخال الاسم الحقيقي',
          realNameLength: 'يجب أن يكون طول الاسم الحقيقي بين 2-20 حرفاً',
          fullNameRequired: 'يرجى إدخال الاسم الكامل',
          fullNameLength: 'يجب أن يكون طول الاسم الكامل بين 2-50 حرفاً',
          englishNameLength: 'يجب أن يكون طول الاسم بالإنجليزية بين 2-50 حرفاً',
          englishNameFormat: 'يمكن أن يحتوي الاسم بالإنجليزية على أحرف ومسافات فقط',
          phoneNumberFormat: 'يرجى إدخال تنسيق رقم هاتف صحيح',
          passwordRequired: 'يرجى إدخال كلمة المرور',
          passwordLength: 'يجب أن يكون طول كلمة المرور بين 6-20 حرفاً',
          passwordFormat: 'يجب أن تحتوي كلمة المرور على أحرف كبيرة وصغيرة وأرقام',
          confirmPasswordRequired: 'يرجى تأكيد كلمة المرور',
          passwordMismatch: 'كلمتا المرور غير متطابقتين',
          userTypeRequired: 'يرجى اختيار نوع المستخدم',
          statusRequired: 'يرجى اختيار الحالة',
          deptIdRequired: 'يرجى إدخال معرف القسم',
          roleIdsRequired: 'يرجى اختيار الأدوار',
          postIdsRequired: 'يرجى اختيار المناصب',
          deptIdsRequired: 'يرجى اختيار الأقسام',
          agreementRequired: 'يرجى قراءة والموافقة على اتفاقية المستخدم'
        }
      },
      forgot: {
        title: 'نسيت كلمة المرور',
        email: 'البريد الإلكتروني',
        submit: 'إرسال',
        back: 'العودة لتسجيل الدخول',
        success: 'تم إرسال بريد إعادة تعيين كلمة المرور',
        error: 'فشل إعادة تعيين كلمة المرور'
      },
      info: {
        loading: 'جاري تحميل معلومات المستخدم',
        success: 'تم تحميل معلومات المستخدم بنجاح'
      },
      captcha: {
        // كابتشا المنزلق
        slider: {
          title: 'كابتشا المنزلق',
          default: 'يرجى سحب المنزلق لمحاذاة الفجوة',
          moving: 'جاري التحقق...',
          success: 'تم التحقق بنجاح!',
          failed: 'فشل التحقق، يرجى المحاولة مرة أخرى',
          expired: 'انتهت صلاحية الكابتشا، تحديث تلقائي',
          countdown: '{seconds}ث',
          verifyError: 'فشل طلب التحقق',
          maxRetryReached: 'فشل التحقق مرات كثيرة جداً، يرجى التحديث والمحاولة مرة أخرى',
          hint: {
            slide: 'يرجى الانزلاق إلى اليمين',
            dragToAlign: 'يرجى سحب المنزلق لمحاذاة الفجوة',
            align: 'محاذاة مع الفجوة'
          },
          error: {
            invalidResponse: 'بيانات الكابتشا غير صالحة',
            loadFailed: 'فشل تحميل الكابتشا'
          },
          bgImage: 'صورة الخلفية',
          sliderImage: 'صورة المنزلق'
        },
        // كابتشا السلوك
        behavior: {
          title: 'كابتشا السلوك',
          default: 'يرجى الضغط والسحب على المنزلق إلى اليمين',
          success: 'تم التحقق بنجاح!',
          failed: 'فشل التحقق، يرجى المحاولة مرة أخرى',
          verifyError: 'فشل طلب التحقق',
          hint: {
            slide: 'انزلاق إلى اليمين',
            dragToEnd: 'اسحب إلى النهاية'
          },
          error: {
            loadFailed: 'فشل تحميل الكابتشا'
          }
        }
      },
      autoLogout: 'تم تسجيل خروجك تلقائياً بسبب عدم النشاط',
      error: {
        noResponse: 'الخادم لا يستجيب',
        noSaltData: 'فشل في الحصول على معلمات التشفير',
        invalidSalt: 'تنسيق معلمة التشفير غير صالح',
        invalidIterations: 'عدد تكرارات التشفير غير صالح',
        permanentlyLocked: 'الحساب مقفل نهائياً، يرجى الاتصال بالمسؤول',
        temporarilyLocked: 'الحساب مقفل مؤقتاً، يرجى المحاولة بعد {minutes} دقيقة',
        tooManyAttempts: 'محاولات تسجيل الدخول الفاشلة كثيرة جداً، الحساب مقفل',
        invalidCredentials: 'اسم المستخدم أو كلمة المرور غير صحيحة'
      }
    }
  }
}