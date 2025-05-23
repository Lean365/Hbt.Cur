import { countReset } from "node:console";

export default {
  menu: {
    home: 'Inicio',
    dashboard: {
      title: 'Panel de control',
      workplace: 'Espacio de trabajo',
      analysis: 'Análisis',
      monitor: 'Monitorización'
    },
    components: {
      title: 'Componentes',
      icons: 'Iconos'
    },
    about: {
      title: 'Acerca de',
      privacy: 'Política de privacidad',
      terms: 'Términos de uso',
      index: 'Acerca de Hbt'
    },
    core: {
      _self: 'Gestión del sistema',
      config: 'Configuración del sistema',
      language: 'Gestión de idiomas',
      dict: 'Gestión de diccionarios'
    },
    identity: {
      _self: 'Autenticación',
      user: 'Gestión de usuarios',
      role: 'Gestión de roles',
      dept: 'Gestión de departamentos',
      post: 'Gestión de puestos',
      menu: 'Gestión de menús',
      tenant: 'Gestión de inquilinos',
      oauth: 'Gestión OAuth'
    },
    audit: {
      _self: 'Auditoría',
      operlog: 'Registro de operaciones',
      loginlog: 'Registro de inicio de sesión',
      sqldifflog: 'Registro de diferencias de base de datos',
      exceptionlog: 'Registro de excepciones',
      auditlog: 'Registro de auditoría',
      quartzlog: 'Registro de tareas'
    },
    workflow: {
      _self: 'Flujo de trabajo',
      definition: 'Definición del proceso',
      instance: 'Instancia del proceso',
      task: 'Tarea',
      node: 'Nodo del proceso',
      variable: 'Variable del proceso',
      history: 'Historial del proceso'
    },
    signalr: {
      _self: 'Monitorización en tiempo real',
      server: 'Monitorización del servidor',
      online: 'Usuarios en línea',
      message: 'Mensajes en línea'
    },
    generator: {
      _self: 'Generación de código',
      table: 'Tabla de base de datos',
      tableDefine: 'Tabla personalizada',
      template: 'Plantilla de código',
      config: 'Configuración de generación',
      api: 'Documentación API'
    },
    routine: {
      _self: 'Tareas diarias',
      vehicle: {
        _self: 'Gestión de vehículos',
        vehicleMaster: {
          _self: 'Datos principales de vehículos',
          vehicleInfo: 'Información de vehículos',
          driverInfo: 'Información de conductores',
          maintenance: 'Mantenimiento de vehículos'
        },
        vehicleBooking: {
          _self: 'Reserva de vehículos',
          newBooking: 'Nueva reserva',
          bookingList: 'Lista de reservas',
          bookingApproval: 'Aprobación de reservas'
        },
        vehicleDispatch: {
          _self: 'Despacho de vehículos',
          dispatchPlan: 'Plan de despacho',
          realTimeTracking: 'Seguimiento en tiempo real',
          dispatchHistory: 'Historial de despachos'
        },
        vehicleReporting: {
          _self: 'Informes de vehículos',
          usageReport: 'Informe de uso',
          costReport: 'Informe de costos',
          maintenanceReport: 'Informe de mantenimiento'
        }
      },
      file: 'Gestión de archivos',
      mail: 'Gestión de correos',
      mailTmpl: 'Plantilla de correo',
      meeting: {
        _self: 'Gestión de reuniones',
        meetingRoom: {
          _self: 'Gestión de salas de reuniones',
          roomInfo: 'Información de sala',
          roomBooking: 'Reserva de sala',
          roomSchedule: 'Programación de sala'
        },
        meetingPlan: {
          _self: 'Planificación de reuniones',
          newMeeting: 'Nueva reunión',
          meetingList: 'Lista de reuniones',
          meetingApproval: 'Aprobación de reuniones'
        },
        meetingExecution: {
          _self: 'Ejecución de reuniones',
          attendance: 'Asistencia',
          minutes: 'Acta',
          followUp: 'Seguimiento'
        },
        meetingReporting: {
          _self: 'Informes de reuniones',
          meetingReport: 'Informe de reunión',
          attendanceReport: 'Informe de asistencia',
          costReport: 'Informe de costos'
        }
      },
      notice: 'Avisos',
      schedule: 'Gestión de horarios',
      quartz: 'Tarea'
    },
    finance: {
      _self: 'Finanzas',
      management: {
        _self: 'Contabilidad de gestión',
        cost: {
          _self: 'Gestión de costos',
          costFactors: 'Tipos de costos',
          costCenter: 'Centro de costos',
          profitCenter: 'Centro de beneficios',
          productCost: 'Costo de productos',
          activityType: 'Tipo de actividad',
          internalOrder: 'Orden interna'
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
          account: 'Cuenta',
          accountType: 'Tipo de cuenta',
          journalEntry: 'Asiento contable',
          reconciliation: 'Conciliación',
          closing: 'Cierre'
        },
        accountsReceivable: {
          _self: 'Cuentas por cobrar',
          customer: 'Gestión de clientes',
          invoice: 'Factura de cliente',
          payment: 'Pago de cliente',
          creditControl: 'Control de crédito'
        },
        accountsPayable: {
          _self: 'Cuentas por pagar',
          supplier: 'Gestión de proveedores',
          invoice: 'Factura de proveedor',
          payment: 'Pago a proveedor',
          agingReport: 'Análisis de vencimientos'
        },
        assetAccounting: {
          _self: 'Contabilidad de activos',
          assets: 'Activos fijos',
          depreciation: 'Gestión de depreciación',
          assetTransfer: 'Transferencia de activos',
          assetRetirement: 'Retiro de activos'
        },
        tax: {
          _self: 'Gestión de impuestos',
          taxCodes: 'Gestión de códigos fiscales',
          taxReporting: 'Declaración de impuestos',
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
      equipment: {
        _self: 'Gestión de equipos',
        equipmentMaster: 'Datos principales de equipos',
        maintenancePlanning: {
          _self: 'Planificación de mantenimiento',
          preventiveMaintenance: 'Mantenimiento preventivo',
          maintenanceTaskList: 'Lista de tareas de mantenimiento',
          scheduling: 'Programación de mantenimiento'
        },
        maintenanceExecution: {
          _self: 'Ejecución de mantenimiento',
          workOrder: 'Orden de trabajo',
          confirmation: 'Confirmación de mantenimiento',
          breakdownMaintenance: 'Mantenimiento por avería'
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
      material: {
        _self: 'Gestión de materiales',
        materialMaster: 'Datos principales de materiales',
        materialCategory: 'Categoría de material',
        materialUnit: 'Unidad de material',
        materialStock: {
          _self: 'Stock de materiales',
          stockOverview: 'Vista general de stock',
          stockIn: 'Entrada de stock',
          stockOut: 'Salida de stock',
          stockTransfer: 'Transferencia de stock',
          stockAdjustment: 'Ajuste de stock',
          stockCheck: 'Verificación de stock'
        },
        purchase: {
          _self: 'Compras',
          purchaseRequisition: 'Solicitud de compra',
          purchaseOrder: 'Orden de compra',
          purchaseOrderDetail: 'Detalle de orden de compra',
          supplier: 'Gestión de proveedores'
        },
        inventoryManagement: {
          _self: 'Gestión de inventario',
          goodsReceipt: 'Recepción de mercancías',
          goodsIssue: 'Emisión de mercancías',
          transferPosting: 'Transferencia',
          stockOverview: 'Vista general de stock'
        },
        valuation: {
          _self: 'Valuación',
          priceControl: 'Control de precios',
          standardPrice: 'Precio estándar',
          movingAveragePrice: 'Precio promedio móvil'
        },
        reporting: {
          _self: 'Informes y análisis',
          stockReports: 'Informes de stock',
          purchaseReports: 'Informes de compras',
          inventoryReports: 'Informes de inventario'
        }
      },
      production: {
        _self: 'Gestión de producción',
        bom: 'Lista de materiales',
        routing: 'Ruta de fabricación',
        workOrder: {
          _self: 'Orden de fabricación',
          create: 'Creación de orden',
          manage: 'Gestión de orden',
          release: 'Liberación de orden',
          complete: 'Cierre de orden'
        },
        capacityPlanning: {
          _self: 'Planificación de capacidad',
          workCenter: 'Centro de trabajo',
          capacityEvaluation: 'Evaluación de capacidad',
          capacityLeveling: 'Nivelación de capacidad'
        },
        productionScheduling: {
          _self: 'Programación de producción',
          schedule: 'Programación',
          reschedule: 'Reprogramación'
        },
        productionExecution: {
          _self: 'Ejecución de producción',
          confirm: 'Confirmación de producción',
          goodsIssue: 'Emisión de materiales',
          goodsReceipt: 'Recepción de productos'
        },
        productionReporting: {
          _self: 'Informes de producción',
          orderReports: 'Informes de órdenes',
          capacityReports: 'Informes de capacidad',
          efficiencyReports: 'Informes de eficiencia'
        },
        qualityManagement: {
          _self: 'Gestión de calidad',
          inspectionLot: 'Lote de inspección',
          resultsRecording: 'Registro de resultados',
          defectRecording: 'Registro de defectos'
        }
      },
      project: {
        _self: 'Gestión de proyectos',
        projectMaster: {
          _self: 'Datos principales del proyecto',
          projectDefinition: 'Definición del proyecto',
          projectStructure: 'Estructura del proyecto',
          projectTeam: 'Equipo del proyecto',
          projectCalendar: 'Calendario del proyecto'
        },
        projectPlanning: {
          _self: 'Planificación del proyecto',
          workBreakdown: 'Estructura de desglose del trabajo',
          scheduling: 'Programación',
          resourcePlanning: 'Planificación de recursos',
          costPlanning: 'Planificación de costos'
        },
        projectExecution: {
          _self: 'Ejecución del proyecto',
          taskManagement: 'Gestión de tareas',
          progressTracking: 'Seguimiento de progreso',
          resourceManagement: 'Gestión de recursos',
          costControl: 'Control de costos'
        },
        projectMonitoring: {
          _self: 'Monitoreo del proyecto',
          progressReports: 'Informes de progreso',
          resourceReports: 'Informes de recursos',
          costReports: 'Informes de costos',
          riskManagement: 'Gestión de riesgos'
        },
        projectClosure: {
          _self: 'Cierre del proyecto',
          finalReport: 'Informe final',
          lessonsLearned: 'Lecciones aprendidas',
          projectArchive: 'Archivo del proyecto'
        }
      },
      quality: {
        _self: 'Gestión de calidad',
        inspection: {
          _self: 'Gestión de inspecciones',
          inspectionLot: 'Lote de inspección',
          resultsRecording: 'Registro de resultados',
          defectRecording: 'Registro de defectos',
          usageDecision: 'Decisión de uso'
        },
        qualityPlanning: {
          _self: 'Planificación de calidad',
          inspectionPlan: 'Plan de inspección',
          qualityInfoRecord: 'Registro de información de calidad',
          samplingProcedure: 'Procedimiento de muestreo'
        },
        qualityControl: {
          _self: 'Control de calidad',
          controlChart: 'Gráfico de control',
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
      sales: {
        _self: 'Ventas',
        customer: {
          _self: 'Gestión de clientes',
          client: 'Cliente',
          customers: 'Lista de clientes',
          creditControl: 'Control de crédito'
        },
        order: {
          _self: 'Gestión de pedidos',
          order: 'Pedido de venta',
          orderDetail: 'Detalle de pedido',
          orderTracking: 'Seguimiento de pedido'
        },
        delivery: {
          _self: 'Gestión de entregas',
          delivery: 'Albarán',
          deliveryDetail: 'Detalle de entrega',
          shipping: 'Gestión de envíos'
        },
        billing: {
          _self: 'Facturación',
          invoice: 'Gestión de facturas',
          invoiceDetail: 'Detalle de factura',
          payment: 'Gestión de pagos'
        },
        reporting: {
          _self: 'Informes y análisis',
          salesReports: 'Informes de ventas',
          performanceAnalysis: 'Análisis de rendimiento'
        }
      },
      service: {
        _self: 'Servicio',
        serviceOrder: {
          _self: 'Orden de servicio',
          create: 'Creación de orden',
          manage: 'Gestión de orden',
          complete: 'Cierre de orden',
          cancel: 'Cancelación de orden'
        },
        serviceContract: {
          _self: 'Contrato de servicio',
          create: 'Creación de contrato',
          manage: 'Gestión de contrato',
          renew: 'Renovación de contrato',
          terminate: 'Terminación de contrato'
        },
        customerInteraction: {
          _self: 'Interacción con clientes',
          inquiries: 'Consultas de clientes',
          complaints: 'Reclamaciones',
          feedback: 'Retroalimentación'
        },
        serviceExecution: {
          _self: 'Ejecución del servicio',
          schedule: 'Programación',
          dispatch: 'Despacho',
          execution: 'Ejecución',
          confirmation: 'Confirmación'
        },
        serviceReporting: {
          _self: 'Informes de servicio',
          orderReports: 'Informes de órdenes',
          contractReports: 'Informes de contratos',
          performanceReports: 'Informes de rendimiento'
        }
      }
    },
    humanResources: {
      _self: 'Recursos humanos',
      employee: {
        _self: 'Gestión de empleados',
        employeeInfo: 'Información de empleado',
        employeeProfile: 'Perfil de empleado',
        employeeContract: 'Contrato de empleado',
        employeeAttendance: 'Asistencia de empleado',
        employeeLeave: 'Permisos de empleado',
        employeePerformance: 'Rendimiento de empleado'
      },
      recruitment: {
        _self: 'Gestión de reclutamiento',
        jobPosting: 'Publicación de empleo',
        candidate: 'Gestión de candidatos',
        interview: 'Gestión de entrevistas',
        offer: 'Gestión de ofertas',
        onboarding: 'Incorporación'
      },
      training: {
        _self: 'Gestión de capacitación',
        trainingPlan: 'Plan de capacitación',
        trainingCourse: 'Curso de capacitación',
        trainingRecord: 'Registro de capacitación',
        trainingEvaluation: 'Evaluación de capacitación'
      },
      performance: {
        _self: 'Gestión de rendimiento',
        performancePlan: 'Plan de rendimiento',
        performanceAppraisal: 'Evaluación de rendimiento',
        performanceReview: 'Revisión de rendimiento',
        performanceImprovement: 'Mejora de rendimiento'
      },
      compensation: {
        _self: 'Gestión de compensación',
        salary: 'Gestión de salarios',
        bonus: 'Gestión de bonos',
        benefits: 'Gestión de beneficios',
        payroll: 'Nómina'
      },
      reporting: {
        _self: 'Informes y análisis',
        employeeReports: 'Informes de empleados',
        recruitmentReports: 'Informes de reclutamiento',
        trainingReports: 'Informes de capacitación',
        performanceReports: 'Informes de rendimiento',
        compensationReports: 'Informes de compensación'
      }
    }
  }
}
