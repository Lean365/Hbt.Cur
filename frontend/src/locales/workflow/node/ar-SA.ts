export default {
  workflow: {
    node: {
      title: 'عقدة سير العمل',
      list: {
        title: 'قائمة عقد سير العمل',
        search: {
          name: 'اسم العقدة',
          type: 'نوع العقدة',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء'
        },
        table: {
          name: 'اسم العقدة',
          type: 'نوع العقدة',
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
          create: 'إنشاء عقدة سير عمل',
          edit: 'تعديل عقدة سير عمل'
        },
        fields: {
          name: 'اسم العقدة',
          type: 'نوع العقدة',
          description: 'الوصف',
          input: 'الإدخال',
          output: 'الإخراج',
          error: 'الخطأ'
        },
        rules: {
          name: {
            required: 'الرجاء إدخال اسم العقدة'
          },
          type: {
            required: 'الرجاء اختيار نوع العقدة'
          }
        },
        buttons: {
          submit: 'إرسال',
          cancel: 'إلغاء'
        }
      },
      detail: {
        title: 'تفاصيل عقدة سير العمل',
        basic: {
          title: 'المعلومات الأساسية',
          name: 'اسم العقدة',
          type: 'نوع العقدة',
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