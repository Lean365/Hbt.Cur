export default {
  workflow: {
    variable: {
      title: 'متغيرات سير العمل',
      list: {
        title: 'قائمة متغيرات سير العمل',
        search: {
          name: 'اسم المتغير',
          type: 'نوع المتغير',
          scope: 'النطاق',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء'
        },
        table: {
          name: 'اسم المتغير',
          type: 'نوع المتغير',
          scope: 'النطاق',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء',
          duration: 'المدة',
          actions: 'الإجراءات'
        },
        actions: {
          view: 'عرض',
          edit: 'تعديل',
          delete: 'حذف',
          refresh: 'تحديث'
        },
        status: {
          running: 'قيد التشغيل',
          completed: 'مكتمل',
          terminated: 'مُنهي',
          failed: 'فشل'
        }
      },
      form: {
        title: {
          create: 'إنشاء متغير سير عمل',
          edit: 'تعديل متغير سير عمل'
        },
        fields: {
          name: 'اسم المتغير',
          type: 'نوع المتغير',
          scope: 'النطاق',
          description: 'الوصف',
          input: 'الإدخال',
          output: 'الإخراج',
          error: 'الخطأ'
        },
        rules: {
          name: {
            required: 'الرجاء إدخال اسم المتغير'
          },
          type: {
            required: 'الرجاء اختيار نوع المتغير'
          },
          scope: {
            required: 'الرجاء اختيار نطاق المتغير'
          }
        },
        buttons: {
          submit: 'إرسال',
          cancel: 'إلغاء'
        }
      },
      detail: {
        title: 'تفاصيل متغير سير العمل',
        basic: {
          title: 'المعلومات الأساسية',
          name: 'اسم المتغير',
          type: 'نوع المتغير',
          scope: 'النطاق',
          description: 'الوصف',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء',
          duration: 'المدة'
        },
        input: {
          title: 'معلومات الإدخال',
          value: 'قيمة الإدخال'
        },
        output: {
          title: 'معلومات الإخراج',
          value: 'قيمة الإخراج'
        },
        error: {
          title: 'معلومات الخطأ',
          message: 'رسالة الخطأ',
          stackTrace: 'تتبع المكدس'
        },
        actions: {
          back: 'رجوع'
        }
      }
    }
  }
} 