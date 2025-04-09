export default {
  identity: {
    user: {
      title: 'إدارة المستخدمين',
      toolbar: {
        add: 'إضافة مستخدم',
        edit: 'تعديل المستخدم',
        delete: 'حذف المستخدم',
        import: 'استيراد المستخدمين',
        export: 'تصدير المستخدمين',
        resetPassword: 'إعادة تعيين كلمة المرور',
        downloadTemplate: 'تحميل القالب'
      },
      table: {
        columns: {
          userName: 'اسم المستخدم',
          nickName: 'الاسم المستعار',
          deptName: 'القسم',
          role: 'الدور',
          email: 'البريد الإلكتروني',
          phoneNumber: 'رقم الهاتف',
          gender: 'الجنس',
      status: 'الحالة',
          createTime: 'تاريخ الإنشاء',
          lastLoginTime: 'آخر تسجيل دخول',
          operation: 'العملية'
        },
        operation: {
          edit: 'تعديل',
          delete: 'حذف',
          resetPassword: 'إعادة تعيين كلمة المرور'
        },
        status: {
          enabled: 'مفعل',
          disabled: 'معطل',
          toggle: {
            enable: 'تفعيل',
            disable: 'تعطيل'
          }
        }
      },
      userId: 'معرف المستخدم',
      userName: {
        label: 'اسم المستخدم',
        placeholder: 'أدخل اسم المستخدم',
        validation: {
          required: 'اسم المستخدم مطلوب',
          format: 'يجب أن يبدأ اسم المستخدم بحرف صغير، وأن يكون طوله 6-20 حرفًا، وأن يحتوي فقط على أحرف صغيرة وأرقام وشرطات سفلية'
        }
      },
      nickName: {
        label: 'الاسم المستعار',
        placeholder: 'أدخل الاسم المستعار',
        validation: {
          required: 'الاسم المستعار مطلوب',
          format: 'يجب أن يكون طول الاسم المستعار 2-20 حرفًا وأن يحتوي فقط على أحرف صينية وأحرف إنجليزية وأرقام وشرطات سفلية'
        }
      },
      englishName: {
        label: 'الاسم بالإنجليزية',
        placeholder: 'أدخل الاسم بالإنجليزية',
        validation: {
          format: 'يجب أن يكون طول الاسم بالإنجليزية 2-50 حرفًا وأن يحتوي فقط على أحرف إنجليزية ومسافات وشرطات'
        }
      },
      password: {
        label: 'كلمة المرور',
        placeholder: 'أدخل كلمة المرور',
        validation: {
          required: 'كلمة المرور مطلوبة',
          length: 'يجب أن يكون طول كلمة المرور 6-20 حرفًا'
        }
      },
      confirmPassword: {
        label: 'تأكيد كلمة المرور',
        placeholder: 'أدخل كلمة المرور مرة أخرى',
        validation: {
          required: 'تأكيد كلمة المرور مطلوب',
          notMatch: 'كلمات المرور غير متطابقة'
        }
      },
      email: {
        label: 'البريد الإلكتروني',
        placeholder: 'أدخل البريد الإلكتروني',
        validation: {
          required: 'البريد الإلكتروني مطلوب',
          invalid: 'يجب أن يكون طول البريد الإلكتروني 6-100 حرفًا وأن يكون بتنسيق صالح'
        }
      },
      phoneNumber: {
        label: 'رقم الهاتف',
        placeholder: 'أدخل رقم الهاتف',
        validation: {
          required: 'رقم الهاتف مطلوب',
          invalid: 'أدخل تنسيق رقم هاتف محمول أو ثابت صالح'
        }
      },
      gender: {
        label: 'الجنس',
        placeholder: 'اختر الجنس',
        options: {
          male: 'ذكر',
          female: 'أنثى',
          unknown: 'غير معروف'
        }
      },
      avatar: {
        label: 'الصورة الرمزية',
        upload: 'تحميل الصورة الرمزية',
        uploadSuccess: 'تم تحميل الصورة الرمزية بنجاح',
        uploadError: 'فشل في تحميل الصورة الرمزية'
      },
      deptName: {
        label: 'القسم',
        placeholder: 'اختر القسم'
      },
      role: {
        label: 'الدور',
        placeholder: 'اختر الدور'
      },
      post: {
        label: 'المنصب',
        placeholder: 'اختر المنصب'
      },
      status: {
        label: 'الحالة',
        placeholder: 'اختر الحالة',
        options: {
          enabled: 'مفعل',
          disabled: 'معطل'
        }
      },
      resetPwd: 'إعادة تعيين كلمة المرور',
      import: {
        title: 'استيراد المستخدمين',
        template: 'تحميل القالب',
        success: 'تم الاستيراد بنجاح',
        failed: 'فشل في الاستيراد'
      },
      export: {
        title: 'تصدير المستخدمين',
        success: 'تم التصدير بنجاح',
        failed: 'فشل في التصدير'
      },
      userType: {
        label: 'نوع المستخدم',
        placeholder: 'اختر نوع المستخدم',
        options: {
          admin: 'مدير',
          user: 'مستخدم عادي'
        }
      },
      createTime: 'تاريخ الإنشاء',
      lastLoginTime: 'آخر تسجيل دخول',
      messages: {
        confirmDelete: 'هل أنت متأكد من حذف المستخدمين المحددين؟',
        confirmResetPassword: 'هل أنت متأكد من إعادة تعيين كلمة المرور للمستخدمين المحددين؟',
        confirmToggleStatus: 'هل أنت متأكد من {action} هذا المستخدم؟',
        deleteSuccess: 'تم حذف المستخدم بنجاح',
        deleteFailed: 'فشل في حذف المستخدم',
        saveSuccess: 'تم حفظ معلومات المستخدم بنجاح',
        saveFailed: 'فشل في حفظ معلومات المستخدم',
        resetPasswordSuccess: 'تم إعادة تعيين كلمة المرور بنجاح',
        resetPasswordFailed: 'فشل في إعادة تعيين كلمة المرور',
        importSuccess: 'تم استيراد المستخدمين بنجاح',
        importFailed: 'فشل في استيراد المستخدمين',
        exportSuccess: 'تم تصدير المستخدمين بنجاح',
        exportFailed: 'فشل في تصدير المستخدمين',
        toggleStatusSuccess: 'تم تعديل الحالة بنجاح',
        toggleStatusFailed: 'فشل في تعديل الحالة'
      },
      tab: {
        basic: 'المعلومات الأساسية',
        profile: 'الملف الشخصي',
        role: 'الأدوار والصلاحيات',
        dept: 'القسم والمنصب',
        other: 'معلومات أخرى',
        avatar: 'إعدادات الصورة الرمزية',
        loginLog: 'سجل تسجيل الدخول',
        operateLog: 'سجل العمليات',
        errorLog: 'سجل الأخطاء',
        taskLog: 'سجل المهام'
      },
      update: {
        validation: {
          required: 'معرف المستخدم ومعرف المستأجر مطلوبان'
        }
      },
      tenantId: {
        label: 'المستأجر',
        placeholder: 'اختر المستأجر',
        validation: {
          required: 'المستأجر مطلوب'
        }
      },
      roles: {
        label: 'الأدوار',
        placeholder: 'اختر الأدوار',
        validation: {
          required: 'اختر دورًا واحدًا على الأقل'
        }
      },
      posts: {
        label: 'المناصب',
        placeholder: 'اختر المناصب',
        validation: {
          required: 'اختر منصبًا واحدًا على الأقل'
        }
      },
      depts: {
        label: 'الأقسام',
        placeholder: 'اختر الأقسام',
        validation: {
          required: 'اختر قسمًا واحدًا على الأقل'
        }
      },
      remark: {
        label: 'ملاحظة',
        placeholder: 'أدخل ملاحظة'
      }
    }
  },
  actions: {
    add: 'إضافة مستخدم',
    edit: 'تعديل المستخدم',
    delete: 'حذف المستخدم',
    resetPassword: 'إعادة تعيين كلمة المرور',
    export: 'تصدير المستخدمين'
  },
  rules: {
    userName: 'اسم المستخدم مطلوب',
    nickName: 'الاسم المستعار مطلوب',
    phoneNumber: 'أدخل رقم هاتف صالح',
    email: 'أدخل عنوان بريد إلكتروني صالح'
  }
}
