export default {
  menu: {
    home: 'Inicio',
    dashboard: {
      title: 'Panel de Control',
      workplace: 'Espacio de Trabajo',
      analysis: 'Análisis',
      monitor: 'Monitor'
    },
    components: {
      title: 'Componentes',
      icons: 'Iconos'
    },
    about: {
      title: 'Acerca de Nosotros',
      privacy: 'Política de Privacidad',
      terms: 'Términos de Servicio',
      index: 'Acerca de Hbt'
    },
    core: {
      _self: 'Gestión Central',
      config: 'Configuración del Sistema',
      language: 'Gestión de Idioma',
      dict: 'Gestión de Diccionario',
    },
    identity: {
      _self: 'Autenticación de Identidad',
      user: 'Gestión de Usuarios',
      role: 'Gestión de Roles',
      dept: 'Gestión de Departamentos',
      post: 'Gestión de Puestos',
      menu: 'Gestión de Menús',
      tenant: 'Gestión de Inquilinos',
      oauth: 'Gestión OAuth',
      profile: 'Información Personal',
      changePassword: 'Cambiar Contraseña'
    },
    audit: {
      _self: 'Registros de Auditoría',
      operlog: 'Registro de Operaciones',
      loginlog: 'Registro de Inicio de Sesión',
      sqldifflog: 'Registro de Diferencias',
      exceptionlog: 'Registro de Excepciones',
      auditlog: 'Registro de Auditoría',
      quartzlog: 'Registro de Tareas',
      server: 'Monitor del Servidor'
    },
    workflow: {
      _self: 'Flujo de Trabajo',
      overview: 'Vista General del Proceso',
      my: 'Mis Procesos',
      form: 'Gestión de Formularios',
      definition: 'Definición del Proceso',
      instance: 'Instancia del Proceso',
      task: 'Tareas de Trabajo',
      node: 'Nodo del Proceso',
      variable: 'Variables del Proceso',
      history: 'Historial del Proceso'
    },
    signalr: {
      _self: 'Comunicación en Tiempo Real',
      online: 'Usuarios en Línea',
      message: 'Mensajes en Línea'
    },
    generator: {
      _self: 'Generador de Código',
      table: 'Tablas de Base de Datos',
      tableDefine: 'Definición de Columnas de Tabla',
      template: 'Plantillas de Código',
      config: 'Configuración de Generación',
      api: 'Documentación de API'
    },
    routine: {
      _self: 'Oficina Diaria',
      schedule: {
        _self: 'Gestión de Horarios',
        myschedule: 'Mi Horario',
        dashboard: 'Panel de Horarios',
      },
      car: {
        _self: 'Gestión de Vehículos',
        info: 'Información del Vehículo',
        application: 'Solicitud de Vehículo',
        dashboard: 'Panel de Vehículos',
        maintenance: 'Mantenimiento del Vehículo',
      },
      email: {
        _self: 'Gestión de Correo Electrónico',
        inbox: 'Bandeja de Entrada',
        drafts: 'Borradores',
        sent: 'Enviados',
        trash: 'Papelera',
        template: 'Plantillas de Correo',        
      },
      meeting: {
        _self: 'Gestión de Reuniones',
        room: 'Salas de Reuniones',
        mymeeting: 'Mis Reuniones',
        booking: 'Reserva de Reuniones',
        dashboard: 'Panel de Reuniones',
      },
      notice: { 
        _self: 'Notificaciones y Anuncios',
        message: {
          _self: 'Gestión de Mensajes',
          mymessages: 'Mis Mensajes',
          list: 'Panel de Mensajes',
        },
        announcement: {
          _self: 'Gestión de Anuncios',
          signoff: 'Firmar Anuncios',
          list: 'Lista de Anuncios',
        },
        notification: {
          _self: 'Gestión de Notificaciones',
          ack: 'Notificaciones Leídas',
          list: 'Lista de Notificaciones',
        },
      },
      hr: {
        _self: 'RRHH y Asistencia',
        recruitment: {
          _self: 'Gestión de Reclutamiento',
          apply: 'Solicitud de Reclutamiento',
          approval: 'Aprobación de Reclutamiento',
          list: 'Lista de Reclutamiento',

        },
        transfer: {
          _self: 'Gestión de Transferencias',
          apply: 'Solicitud de Transferencia',
          approval: 'Aprobación de Transferencia',
          list: 'Lista de Transferencias',
        },
        leave: {
          _self: 'Gestión de Permisos',
          apply: 'Solicitud de Permiso',
          approval: 'Aprobación de Permiso',
          list: 'Lista de Permisos',
        },
        trip: {
          _self: 'Gestión de Viajes de Negocios',
          apply: 'Solicitud de Viaje',
          approval: 'Aprobación de Viaje',
          list: 'Lista de Viajes',
        },
        overtime: {
          _self: 'Gestión de Horas Extras',
          apply: 'Solicitud de Horas Extras',
          approval: 'Aprobación de Horas Extras',
          list: 'Lista de Horas Extras',
      },
    },
    expense:{
      _self: 'Gestión de Gastos',
      daily: {
        _self: 'Gastos Diarios',
        apply: 'Solicitud de Gasto',
        approve: 'Aprobación de Gasto',
        list: 'Lista de Gastos',
      },
      travel: {
        _self: 'Gastos de Viaje',
        apply: 'Solicitud de Gasto de Viaje',
        approve: 'Aprobación de Gasto de Viaje',
        list: 'Lista de Gastos de Viaje',
      },
    },
    file:{
      _self: 'Gestión de Archivos',
      daily: {
        _self: 'Archivos Diarios',
        list: 'Lista de Archivos',
      },
      iso: {
        _self: 'Archivos ISO',
        version: 'Versión',
        signoff: 'Firma',
        list: 'Archivos ISO',
      },
      document: { 
        _self: 'Gestión de Documentos',
        version: 'Versión',
        signoff: 'Firma',
        list: 'Lista de Documentos',
      },
    },
    officesupplies:{
      _self: 'Suministros de Oficina',
      inventory:{
        _self: 'Gestión de Inventario',
        requisition: 'Gestión de Requisiciones',
        inbound: 'Gestión de Entrada',
        stocktaking: 'Gestión de Inventario',
      },
      usage:{
        _self: 'Gestión de Uso',
        apply: 'Solicitud de Uso',
        approve: 'Aprobación de Uso',
        receive: 'Registro de Uso',
      }
    },
    book:{
      _self: 'Gestión de Libros',
      inventory:{
        _self: 'Gestión de Inventario',
        requisition: 'Gestión de Requisiciones',
        inbound: 'Gestión de Entrada',
        list: 'Lista de Libros',
        stocktaking: 'Gestión de Inventario',
      },
      usage:{
        _self: 'Gestión de Uso',
        card: 'Tarjeta de Biblioteca',
        borrow: 'Prestar',
        return: 'Devolver',
      }

    },
    medical:{
      _self: 'Gestión Médica',
      medicine:{
        _self: 'Gestión de Inventario',
        requisition: 'Gestión de Requisiciones',
        inbound: 'Gestión de Entrada',
        list: 'Lista de Medicamentos',
        stocktaking: 'Gestión de Inventario',
      },
      usage:{
        _self: 'Gestión de Uso',
        archive: 'Archivo',
        receive: 'Recibo de Medicamentos',
        cost: 'Costo',
      }

    },
  },
  accounting: {
      _self: 'Contabilidad',
      financial: {
        _self: 'Contabilidad de Gestión',
        company: "Información de la Empresa",
        account: 'Cuentas Contables',
        companyaccount: 'Cuentas de la Empresa',
        ledger: 'Libro Mayor',
        payable: 'Cuentas por Pagar',
        receivable: 'Cuentas por Cobrar',
        fixedasset: 'Activos Fijos',
        bank: 'Información Bancaria',

      },
      controlling: {
        _self: 'Contabilidad de Control',
        costelement: 'Elementos de Costo',
        costcenter: 'Centros de Costo',
        profitcenter: 'Centros de Beneficio',
        accountsReceivable: 'Cuentas por Cobrar',
        accountsPayable: 'Cuentas por Pagar',
        assetAccounting: 'Contabilidad de Activos',
        tax: 'Gestión de Impuestos',
        financialReporting: 'Informes Financieros'      
    },
    budget:{
      _self: 'Presupuesto Integral',
        formulation: {
          _self: 'Formulación del Presupuesto',
          sales: {
            _self: 'Presupuesto de Ventas',
            cost: 'Costo de Ventas',
            rolling: 'Ventas Rodantes',
          },
          production: {
            _self: 'Presupuesto de Producción',
            auxiliary: 'Materiales Auxiliares de Producción',
            labor: 'Mano de Obra de Producción',
            manufacturing: 'Manufactura',
          },
          cost: {
            _self: 'Presupuesto de Costos',
            directmaterial: 'Materiales Directos',
            directlabor: 'Mano de Obra Directa',
            indirectlabor: 'Mano de Obra Indirecta',
            manufacturing: 'Gastos de Manufactura',
          },
          expense: {
            _self: 'Presupuesto de Gastos',
            sales: 'Gastos de Ventas',
            management: 'Gastos de Administración',
            financial: 'Gastos Financieros',
          },
          financial: {
            _self: 'Presupuesto Financiero',
            cashflow: 'Flujo de Caja',
            balancesheet: 'Balance General',
            income: 'Estado de Resultados',
          },
        },
        control: {
          _self: 'Control de Presupuesto',
          dashboard: 'Panel de Presupuesto',
          approval: 'Aprobación de Presupuesto',
        },   
  },
},
    logistics: {
      _self: 'Gestión Logística',
      equipment: {
        _self: 'Gestión de Equipos',
        data: 'Datos Maestros de Equipos',
        location: 'Ubicación de Equipos',
        material: 'Asociación de Materiales',
        workorder: 'Orden de Trabajo'

      },
      material: {
        _self: 'Gestión de Materiales',
        material:{
          _self: 'Gestión de Materiales',
          material: 'Datos Maestros de Materiales',
          plant: 'Información de Planta',
          master: 'Datos de Materiales',
          plantmaster: 'Materiales de Planta',
          vendor: 'Información del Vendedor',
          supplier: 'Información del Proveedor',
        },
        purchase:{
          _self: 'Gestión de Compras',
          vendor: 'Información del Vendedor',
          supplier: 'Información del Proveedor',
          price: 'Precio de Compra',
          requisition: 'Requisición de Compra',
          order: 'Orden de Compra',

        },



      },
      production: {
        _self: 'Gestión de Producción',
        bom: 'Lista de Materiales',
        change: {
          _self: 'Cambio de Diseño',
          implementation: 'Implementación del Cambio',
          techcontact: 'Contacto Técnico',
          material: 'Confirmación de Materiales',
          query: 'Consulta de Cambio',
          oldproduct: 'Control de Productos Antiguos',
          sop: 'Confirmación SOP',
          batch: 'Lote de Entrada',
          input: {
            _self: 'Entrada de Cambio',
            gijutsu: 'Departamento Técnico',
            seikan: 'Departamento de Gestión de Producción',
            koubai: 'Departamento de Compras',
            uketsuke: 'Departamento de Recepción',
            bukan: 'Departamento de Gestión',
            seizou2: 'Departamento de Producción 2',
            seizou1: 'Departamento de Producción 1',
            hinkan: 'Departamento de Control de Calidad',
            seizougijutsu: 'Departamento de Tecnología de Producción',
  
          }
        },
        workcenter: 'Centro de Trabajo',
        order: 'Orden de Producción',
        kanban: 'Kanban',
        oph:{
          _self: 'Gestión OPH',
          workshop1: {
            _self: 'Departamento de Producción 1',
            output: 'Reporte Diario de Producción',
            defect: 'Defectos de Producción',
            worktime: 'Tiempo de Trabajo de Producción',
            productionReport: 'Reporte de Producción',
            defectSummary: 'Resumen de Defectos',
            worktimeReport: 'Reporte de Tiempo de Trabajo'
          },
          workshop2: {
            _self: 'Departamento de Producción 2',
            output: 'Reporte Diario de Producción',
            inspection: 'Registros de Inspección',
            repair: 'Registros de Reparación',
            worktime: 'Tiempo de Trabajo de Producción',
            productionReport: 'Reporte de Producción',
            inspectionReport: 'Reporte de Inspección',
            repairReport: 'Reporte de Reparación',
            worktimeReport: 'Reporte de Tiempo de Trabajo'
          }
        }

      },
      project: {
        _self: 'Gestión de Proyectos',
        define: 'Definición del Proyecto',
        cost: 'Planificación de Costos',
        resource: 'Planificación de Recursos',
        schedule: 'Planificación de Cronograma',

      },
      quality: {
        _self: 'Gestión de Calidad',
        item: 'Elementos de Inspección',
        receiving: 'Inspección de Entrada',
        process: 'Inspección de Proceso',
        storage: 'Inspección de Almacenamiento',
        return: 'Inspección de Devolución',
  
      },
      sales: {
        _self: 'Gestión de Ventas',
        customer: 'Información del Cliente',
        client: 'Información del Cliente',
        price: 'Precio de Venta',
        order: 'Orden de Venta',
      },
      service: {
        _self: 'Servicio al Cliente',
        item: 'Elementos de Servicio',
        contract: 'Contrato de Servicio',
        request: 'Solicitud de Servicio',
        workorder: 'Orden de Trabajo de Servicio',
        timesheet: 'Registros de Tiempo',
        consumption: 'Consumo de Materiales',
        outsourcing: 'Servicios de Subcontratación'

      },
      complaint: {
        _self: 'Gestión de Quejas de Clientes',
        notice: 'Aviso de Calidad',
        mark: 'Detalles de Queja',
        analysis: 'Análisis de Causa',
        corrective: 'Acciones Correctivas',
        return: 'Ejecución de Devolución',
        followUp: 'Seguimiento'
      }
    },
    humanResources: {
      _self: 'Gestión de Recursos Humanos',
      employeeManagement: {
        _self: 'Gestión de Empleados',
        employeeMaster: 'Datos Maestros de Empleados',
        attendance: 'Gestión de Asistencia',
        leave: 'Gestión de Permisos',
        payroll: 'Gestión de Nómina',
        contractManagement: 'Gestión de Contratos'
      },
      recruitment: {
        _self: 'Gestión de Reclutamiento',
        jobPosting: 'Publicación de Empleos',
        candidateManagement: 'Gestión de Candidatos',
        interviewScheduling: 'Programación de Entrevistas',
        offerManagement: 'Gestión de Ofertas'
      },
      training: {
        _self: 'Gestión de Capacitación',
        trainingPlan: 'Plan de Capacitación',
        trainingExecution: 'Ejecución de Capacitación',
        trainingEvaluation: 'Evaluación de Capacitación'
      },
      performance: {
        _self: 'Gestión de Rendimiento',
        goalSetting: 'Establecimiento de Objetivos',
        performanceReview: 'Revisión de Rendimiento',
        feedback: 'Gestión de Retroalimentación'
      },
      reporting: {
        _self: 'Informes de RRHH',
        employeeReports: 'Informes de Empleados',
        attendanceReports: 'Informes de Asistencia',
        payrollReports: 'Informes de Nómina',
        performanceReports: 'Informes de Rendimiento'
      }
    }
  }
}
