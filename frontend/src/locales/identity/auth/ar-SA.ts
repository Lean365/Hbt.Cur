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
        title: 'تسجيل',
        username: 'اسم المستخدم',
        password: 'كلمة المرور',
        confirm: 'تأكيد كلمة المرور',
        email: 'البريد الإلكتروني',
        phone: 'رقم الهاتف',
        submit: 'تسجيل',
        login: 'تسجيل الدخول بحساب موجود',
        success: 'تم التسجيل بنجاح',
        error: 'فشل التسجيل'
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