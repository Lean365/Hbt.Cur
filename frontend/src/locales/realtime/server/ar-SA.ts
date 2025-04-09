export default {
  realtime: {
    server: {
      title: 'مراقبة الخادم',
      refresh: 'تحديث',
      refreshResult: {
        success: 'تم تحديث البيانات بنجاح',
        failed: 'فشل تحديث البيانات'
      },
      resource: {
        title: 'استخدام الموارد',
        cpu: 'استخدام المعالج',
        memory: 'استخدام الذاكرة',
        disk: 'استخدام القرص'
      },
      system: {
        title: 'معلومات النظام',
        os: 'نظام التشغيل',
        architecture: 'البنية',
        version: 'الإصدار',
        processor: {
          name: 'المعالج',
          count: 'عدد النواة',
          unit: 'نواة'
        },
        startup: {
          time: 'وقت بدء النظام',
          uptime: 'مدة التشغيل',
          day: 'يوم',
          hour: 'ساعة'
        }
      },
      dotnet: {
        title: 'معلومات .NET Runtime',
        runtime: {
          version: 'إصدار .NET Runtime',
          directory: 'مسار التشغيل'
        },
        clr: {
          version: 'إصدار CLR'
        }
      },
      network: {
        title: 'معلومات الشبكة',
        adapter: 'اسم المحول',
        mac: 'عنوان MAC',
        ip: {
          address: 'عنوان IP',
          location: 'الموقع',
          unknown: 'موقع غير معروف'
        },
        rate: {
          send: 'معدل الإرسال',
          receive: 'معدل الاستقبال'
        }
      }
    }
  }
}