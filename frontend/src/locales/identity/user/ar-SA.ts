export default {
  identity: {
    user: {
      title: 'إدارة المستخدمين',
      table: {
        columns: {
          userId: 'معرف المستخدم',
          tenantId: 'المستأجر',
          userName: 'اسم المستخدم',
          nickName: 'الاسم المستعار',
          englishName: 'الاسم بالإنجليزية',
          userType: 'النوع',
          email: 'البريد الإلكتروني',
          phoneNumber: 'رقم الهاتف',
          gender: 'الجنس',
          avatar: 'الصورة الرمزية',
          status: 'الحالة',
          lastPasswordChangeTime: 'آخر تعديل كلمة المرور',
          lockEndTime: 'انتهاء القفل',
          lockReason: 'سبب القفل',
          isLock: 'هل هو مقفل',
          errorLimit: 'حد الأخطاء',
          loginCount: 'عدد مرات الدخول',
          deptName: 'القسم',
          role: 'الدور',
          createBy: 'أنشأ بواسطة',
          createTime: 'تاريخ الإنشاء',
          updateBy: 'تم التحديث بواسطة',
          updateTime: 'تاريخ التحديث',
          deleteBy: 'تم الحذف بواسطة',
          deleteTime: 'تاريخ الحذف',
          isDeleted: 'تم الحذف',
          remark: 'ملاحظة',
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
      fields: {
        userId: 'معرف المستخدم',
        tenantId: {
          label: 'المستأجر',
          placeholder: 'يرجى اختيار المستأجر',
          validation: {
            required: 'المستأجر مطلوب'
          }
        },
        userName: {
          label: 'اسم المستخدم',
          placeholder: 'يرجى إدخال اسم المستخدم',
          validation: {
            required: 'اسم المستخدم مطلوب',
            format: 'يجب أن يبدأ اسم المستخدم بحرف صغير، وأن يكون طوله 6-20 حرفًا، وأن يحتوي فقط على أحرف صغيرة وأرقام وشرطات سفلية'
          }
        },
        nickName: {
          label: 'الاسم المستعار',
          placeholder: 'يرجى إدخال الاسم المستعار',
          validation: {
            required: 'الاسم المستعار مطلوب',
            format: 'يجب أن يكون طول الاسم المستعار 2-20 حرفًا وأن يحتوي فقط على أحرف صينية وأحرف إنجليزية وأرقام وشرطات سفلية'
          }
        },
        englishName: {
          label: 'الاسم بالإنجليزية',
          placeholder: 'يرجى إدخال الاسم بالإنجليزية',
          validation: {
            format: 'يجب أن يكون طول الاسم بالإنجليزية 2-50 حرفًا وأن يحتوي فقط على أحرف إنجليزية ومسافات وشرطات'
          }
        },
        userType: {
          label: 'النوع',
          placeholder: 'يرجى اختيار نوع المستخدم',
          options: {
            admin: 'مدير',
            user: 'مستخدم عادي'
          }
        },
        email: {
          label: 'البريد الإلكتروني',
          placeholder: 'يرجى إدخال البريد الإلكتروني',
          validation: {
            required: 'البريد الإلكتروني مطلوب',
            invalid: 'يجب أن يكون طول البريد الإلكتروني 6-100 حرفًا وأن يكون بتنسيق صالح'
          }
        },
        phoneNumber: {
          label: 'رقم الهاتف',
          placeholder: 'يرجى إدخال رقم الهاتف',
          validation: {
            required: 'رقم الهاتف مطلوب',
            invalid: 'أدخل تنسيق رقم هاتف محمول أو ثابت صالح'
          }
        },
        gender: {
          label: 'الجنس',
          placeholder: 'يرجى اختيار الجنس',
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
        status: {
          label: 'الحالة',
          placeholder: 'يرجى اختيار الحالة',
          options: {
            enabled: 'مفعل',
            disabled: 'معطل'
          }
        },
        deptName: {
          label: 'القسم',
          placeholder: 'يرجى اختيار القسم',
          validation: {
            required: 'القسم مطلوب'
          }
        },
        role: {
          label: 'الدور',
          placeholder: 'يرجى اختيار الدور',
          validation: {
            required: 'الدور مطلوب'
          }
        },
        post: {
          label: 'المنصب',
          placeholder: 'يرجى اختيار المنصب',
          validation: {
            required: 'المنصب مطلوب'
          }
        },
        remark: {
          label: 'ملاحظة',
          placeholder: 'يرجى إدخال ملاحظة'
        }
      },
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
      resetPwd: 'إعادة تعيين كلمة المرور'
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
