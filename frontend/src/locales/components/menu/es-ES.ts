export default {
  menu: {
    home: 'Inicio',
    dashboard: {
      title: 'Tablero',
      workplace: 'Lugar de trabajo',
      analysis: 'Análisis',
      monitor: 'Monitor'
    },
    components: {
      title: 'Componentes',
      icons: 'Iconos'
    },
    about: {
      title: 'Acerca de nosotros',
      privacy: 'Política de privacidad',
      terms: 'Términos del servicio',
      index: 'Acerca de Hbt'
    },
    admin: {
      _self: 'Administración del sistema',
      config: 'Configuración del sistema',
      language: 'Gestión de idiomas',
      dicttype: 'Tipos de diccionario',
      dictdata: 'Datos del diccionario',
      translation: 'Gestión de traducciones'
    },
    identity: {
      _self: 'Gestión de identidad',
      user: 'Gestión de usuarios',
      role: 'Gestión de roles',
      dept: 'Gestión de departamentos',
      post: 'Gestión de puestos',
      menu: 'Gestión de menús',
      tenant: 'Gestión de inquilinos',
      oauth: 'Gestión de OAuth'
    },
    audit: {
      _self: 'Registros de auditoría',
      operlog: 'Registros de operaciones',
      loginlog: 'Registros de inicio de sesión',
      dbdifflog: 'Registros de diferencias de base de datos',
      exceptionlog: 'Registros de excepciones'
    },
    workflow: {
      _self: 'Flujo de trabajo',
      definition: 'Definición del flujo de trabajo',
      instance: 'Instancia del flujo de trabajo',
      task: 'Tareas',
      node: 'Nodos',
      variable: 'Variables',
      history: 'Historial'
    },
    signalr: {
      _self: 'Supervisión en tiempo real',
      server: 'Supervisión del servidor',
      online: 'Usuarios en línea',
      message: 'Mensajes en línea'
    },
    generator: {
      _self: 'Generador de código',
      table: 'Tablas de base de datos',
      tableDefine: 'Tablas personalizadas',
      template: 'Plantillas de código',
      config: 'Configuración de generación',
      api: 'Documentación de API'
    },
    routine: {
      _self: 'Trabajo diario',
      file: 'Gestión de archivos',
      mail: 'Gestión de correos',
      mailTmpl: 'Plantillas de correo',
      notice: 'Avisos',
      task: 'Tareas',
      schedule: 'Gestión de horarios'
    },
    finance: {
      _self: 'Finanzas',
      management: {
        _self: 'Contabilidad de gestión',
        cost: {
          _self: 'Gestión de costos',
          costFactors: 'Factores de costo',
          costCenter: 'Centro de costos',
          profitCenter: 'Centro de beneficios',
          productCost: 'Costo del producto',
          activityType: 'Tipo de actividad',
          internalOrder: 'Órdenes internas'
        },
        planning: {
          _self: 'Gestión de planificación',
          costPlanning: 'Planificación de costos',
          profitPlanning: 'Planificación de beneficios',
          budgetControl: 'Control presupuestario'
        },
        reporting: {
          _self: 'Informes y análisis',
          costReports: 'Informes de costos',
          profitReports: 'Informes de beneficios',
          varianceAnalysis: 'Análisis de variaciones'
        }
      },
      financial: {
        _self: 'Contabilidad financiera',
        generalLedger: {
          _self: 'Libro mayor',
          account: 'Cuentas',
          accountType: 'Tipos de cuentas',
          journalEntry: 'Asientos contables',
          reconciliation: 'Conciliación',
          closing: 'Cierre periódico'
        },
        accountsReceivable: {
          _self: 'Cuentas por cobrar',
          customer: 'Gestión de clientes',
          invoice: 'Facturas de clientes',
          payment: 'Pagos de clientes',
          creditControl: 'Control de crédito'
        },
        accountsPayable: {
          _self: 'Cuentas por pagar',
          supplier: 'Gestión de proveedores',
          invoice: 'Facturas de proveedores',
          payment: 'Pagos a proveedores',
          agingReport: 'Informes de antigüedad'
        },
        assetAccounting: {
          _self: 'Contabilidad de activos',
          assets: 'Activos fijos',
          depreciation: 'Gestión de depreciación',
          assetTransfer: 'Transferencia de activos',
          assetRetirement: 'Baja de activos'
        },
        tax: {
          _self: 'Gestión fiscal',
          taxCodes: 'Códigos fiscales',
          taxReporting: 'Informes fiscales',
          taxPayments: 'Pagos de impuestos'
        },
        financialReporting: {
          _self: 'Informes financieros',
          balanceSheet: 'Balance general',
          profitAndLoss: 'Estado de resultados',
          cashFlow: 'Estado de flujo de efectivo'
        }
      }
    },
    logistics: {
      _self: 'Logística',
      sales: {
        _self: 'Gestión de ventas',
        customer: {
          _self: 'Gestión de clientes',
          client: 'Clientes',
          customers: 'Lista de clientes',
          creditControl: 'Control de crédito'
        },
        order: {
          _self: 'Gestión de pedidos',
          order: 'Pedidos de ventas',
          orderDetail: 'Detalles del pedido',
          orderTracking: 'Seguimiento de pedidos'
        },
        delivery: {
          _self: 'Gestión de entregas',
          delivery: 'Documentos de entrega',
          deliveryDetail: 'Detalles de la entrega',
          shipping: 'Gestión de envíos'
        },
        billing: {
          _self: 'Gestión de facturación',
          invoice: 'Gestión de facturas',
          invoiceDetail: 'Detalles de facturas',
          payment: 'Gestión de pagos'
        },
        reporting: {
          _self: 'Informes y análisis',
          salesReports: 'Informes de ventas',
          performanceAnalysis: 'Análisis de rendimiento'
        }
      },
      production: {
        _self: 'Gestión de producción',
        bom: 'Lista de materiales (BOM)',
        routing: 'Enrutamiento',
        workOrder: {
          _self: 'Órdenes de trabajo',
          create: 'Crear orden de trabajo',
          manage: 'Gestionar órdenes de trabajo',
          release: 'Liberar órdenes de trabajo',
          complete: 'Completar órdenes de trabajo'
        },
        capacityPlanning: {
          _self: 'Planificación de capacidad',
          workCenter: 'Centros de trabajo',
          capacityEvaluation: 'Evaluación de capacidad',
          capacityLeveling: 'Nivelación de capacidad'
        },
        productionScheduling: {
          _self: 'Programación de producción',
          schedule: 'Programar',
          reschedule: 'Reprogramar'
        },
        productionExecution: {
          _self: 'Ejecución de producción',
          confirm: 'Confirmar producción',
          goodsIssue: 'Salida de mercancías',
          goodsReceipt: 'Recepción de mercancías'
        },
        productionReporting: {
          _self: 'Informes de producción',
          orderReports: 'Informes de órdenes',
          capacityReports: 'Informes de capacidad',
          efficiencyReports: 'Informes de eficiencia'
        },
        qualityManagement: {
          _self: 'Gestión de calidad',
          inspectionLot: 'Lotes de inspección',
          resultsRecording: 'Registro de resultados',
          defectRecording: 'Registro de defectos'
        }
      },
      material: {
        _self: 'Gestión de materiales',
        materialMaster: 'Datos maestros de materiales',
        materialCategory: 'Categorías de materiales',
        materialUnit: 'Unidades de materiales',
        materialStock: {
          _self: 'Inventario de materiales',
          stockOverview: 'Resumen de inventario',
          stockIn: 'Entrada de inventario',
          stockOut: 'Salida de inventario',
          stockTransfer: 'Transferencia de inventario',
          stockAdjustment: 'Ajuste de inventario',
          stockCheck: 'Verificación de inventario'
        },
        purchase: {
          _self: 'Gestión de compras',
          purchaseRequisition: 'Requisiciones de compra',
          purchaseOrder: 'Órdenes de compra',
          purchaseOrderDetail: 'Detalles de órdenes de compra',
          supplier: 'Gestión de proveedores'
        },
        inventoryManagement: {
          _self: 'Gestión de inventarios',
          goodsReceipt: 'Recepción de mercancías',
          goodsIssue: 'Salida de mercancías',
          transferPosting: 'Transferencia de inventario',
          stockOverview: 'Resumen de inventario'
        },
        valuation: {
          _self: 'Valoración de materiales',
          priceControl: 'Control de precios',
          standardPrice: 'Precio estándar',
          movingAveragePrice: 'Precio promedio móvil'
        },
        reporting: {
          _self: 'Informes y análisis',
          stockReports: 'Informes de inventario',
          purchaseReports: 'Informes de compras',
          inventoryReports: 'Informes de análisis de inventario'
        }
      }
    },
    quality: {
      _self: 'Gestión de calidad',
      inspection: {
        _self: 'Gestión de inspecciones',
        inspectionLot: 'Lotes de inspección',
        resultsRecording: 'Registro de resultados',
        defectRecording: 'Registro de defectos',
        usageDecision: 'Decisión de uso'
      },
      qualityPlanning: {
        _self: 'Planificación de calidad',
        inspectionPlan: 'Planes de inspección',
        qualityInfoRecord: 'Registros de información de calidad',
        samplingProcedure: 'Procedimientos de muestreo'
      },
      qualityControl: {
        _self: 'Control de calidad',
        controlChart: 'Gráficos de control',
        qualityNotifications: 'Notificaciones de calidad',
        correctiveActions: 'Acciones correctivas'
      },
      qualityReporting: {
        _self: 'Informes de calidad',
        inspectionReports: 'Informes de inspección',
        defectReports: 'Informes de defectos',
        qualityAnalysis: 'Análisis de calidad'
      }
    },
    service: {
      _self: 'Servicio al cliente',
      serviceOrder: {
        _self: 'Órdenes de servicio',
        create: 'Crear orden de servicio',
        manage: 'Gestionar órdenes de servicio',
        complete: 'Completar órdenes de servicio',
        cancel: 'Cancelar órdenes de servicio'
      },
      serviceContract: {
        _self: 'Contratos de servicio',
        create: 'Crear contrato de servicio',
        manage: 'Gestionar contratos de servicio',
        renew: 'Renovar contratos de servicio',
        terminate: 'Terminar contratos de servicio'
      },
      customerInteraction: {
        _self: 'Interacción con clientes',
        inquiries: 'Consultas de clientes',
        complaints: 'Quejas de clientes',
        feedback: 'Comentarios de clientes'
      },
      serviceExecution: {
        _self: 'Ejecución del servicio',
        schedule: 'Programar servicio',
        dispatch: 'Despacho de servicio',
        execution: 'Ejecución del servicio',
        confirmation: 'Confirmación del servicio'
      },
      serviceReporting: {
        _self: 'Informes de servicio',
        orderReports: 'Informes de órdenes de servicio',
        contractReports: 'Informes de contratos de servicio',
        performanceReports: 'Informes de rendimiento'
      }
    },
    equipment: {
      _self: 'Gestión de equipos',
      equipmentMaster: 'Datos maestros de equipos',
      maintenancePlanning: {
        _self: 'Planificación de mantenimiento',
        preventiveMaintenance: 'Mantenimiento preventivo',
        maintenanceTaskList: 'Lista de tareas de mantenimiento',
        scheduling: 'Programación de mantenimiento'
      },
      maintenanceExecution: {
        _self: 'Ejecución de mantenimiento',
        workOrder: 'Órdenes de trabajo de mantenimiento',
        confirmation: 'Confirmación de mantenimiento',
        breakdownMaintenance: 'Mantenimiento por averías'
      },
      maintenanceReporting: {
        _self: 'Informes de mantenimiento',
        equipmentReports: 'Informes de equipos',
        maintenanceHistory: 'Historial de mantenimiento',
        performanceAnalysis: 'Análisis de rendimiento'
      },
      sparePartsManagement: {
        _self: 'Gestión de repuestos',
        sparePartsInventory: 'Inventario de repuestos',
        sparePartsProcurement: 'Adquisición de repuestos',
        sparePartsUsage: 'Uso de repuestos'
      }
    },
    humanResources: {
      _self: 'Gestión de recursos humanos',
      employeeManagement: {
        _self: 'Gestión de empleados',
        employeeMaster: 'Datos maestros de empleados',
        attendance: 'Gestión de asistencia',
        leave: 'Gestión de permisos',
        payroll: 'Gestión de nóminas',
        contractManagement: 'Gestión de contratos'
      },
      recruitment: {
        _self: 'Gestión de reclutamiento',
        jobPosting: 'Publicación de empleos',
        candidateManagement: 'Gestión de candidatos',
        interviewScheduling: 'Programación de entrevistas',
        offerManagement: 'Gestión de ofertas'
      },
      training: {
        _self: 'Gestión de formación',
        trainingPlan: 'Planes de formación',
        trainingExecution: 'Ejecución de formación',
        trainingEvaluation: 'Evaluación de formación'
      },
      performance: {
        _self: 'Gestión del rendimiento',
        goalSetting: 'Establecimiento de objetivos',
        performanceReview: 'Revisión del rendimiento',
        feedback: 'Gestión de comentarios'
      },
      reporting: {
        _self: 'Informes de recursos humanos',
        employeeReports: 'Informes de empleados',
        attendanceReports: 'Informes de asistencia',
        payrollReports: 'Informes de nóminas',
        performanceReports: 'Informes de rendimiento'
      }
    }
  }
}
