export default {
  workflow: {
    task: {
      title: 'مهمة سير العمل',
      list: {
        title: 'قائمة مهام سير العمل',
        search: {
          name: 'اسم المهمة',
          type: 'نوع المهمة',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء'
        },
        table: {
          name: 'اسم المهمة',
          type: 'نوع المهمة',
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
          create: 'إنشاء مهمة سير عمل',
          edit: 'تعديل مهمة سير عمل'
        },
        fields: {
          name: 'اسم المهمة',
          type: 'نوع المهمة',
          description: 'الوصف',
          input: 'الإدخال',
          output: 'الإخراج',
          error: 'الخطأ'
        },
        rules: {
          name: {
            required: 'الرجاء إدخال اسم المهمة'
          },
          type: {
            required: 'الرجاء اختيار نوع المهمة'
          }
        },
        buttons: {
          submit: 'إرسال',
          cancel: 'إلغاء'
        }
      },
      detail: {
        title: 'تفاصيل مهمة سير العمل',
        basic: {
          title: 'المعلومات الأساسية',
          name: 'اسم المهمة',
          type: 'نوع المهمة',
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