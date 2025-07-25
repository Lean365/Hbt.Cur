export default {
  menu: {
    home: 'Inicio',
    dashboard: {
      title: 'Panel de control',
      workplace: 'Espacio de trabajo',
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
      terms: 'Términos de servicio',
      index: 'Acerca de Hbt'
    },

    identity: {
      _self: 'Autenticación de identidad',
      user: 'Gestión de usuarios',
      role: 'Gestión de roles',
      dept: 'Gestión de departamentos',
      post: 'Gestión de puestos',
      menu: 'Gestión de menús',
      tenant: 'Gestión de inquilinos',
      oauth: 'Gestión OAuth',
      profile: 'Información personal',
      changePassword: 'Cambiar contraseña'
    },
    audit: {
      _self: 'Registros de auditoría',
      operlog: 'Registro de operaciones',
      loginlog: 'Registro de inicio de sesión',
      sqldifflog: 'Registro de diferencias SQL',
      exceptionlog: 'Registro de excepciones',
      auditlog: 'Registro de auditoría',
      quartzlog: 'Registro de tareas',
      server: 'Monitor del servidor'
    },
    workflow: {
      _self: 'Flujo de trabajo',
      engine:{
        _self: 'Motor de procesos',
        monitor: 'Monitor de procesos',
        todo: 'Tareas pendientes',
        done: 'Tareas completadas',
        signoff: 'Aprobación de procesos',
        execution: 'Ejecución de procesos',
        designer: 'Diseñador de procesos'
      },
      manage:{
        _self: 'Gestión de procesos',
        form: 'Gestión de formularios',
        scheme: 'Esquema de procesos',
        instance: 'Instancia de proceso',
        oper: 'Operaciones de instancia',
        trans: 'Flujo de instancia'
      }
    },
    signalr: {
      _self: 'Comunicación en tiempo real',
      online: 'Usuarios en línea',
      message: 'Mensajes en línea'
    },
    generator: {
      _self: 'Generador de código',
      table: 'Tablas de base de datos',
      tableDefine: 'Definición de columnas de tabla',
      template: 'Plantillas de código',
      config: 'Configuración de generación',
      api: 'Documentación API'
    },
    routine: {
      _self: 'Oficina diaria',
      core: {
        _self: 'Servicios básicos',
        numberrule: 'Reglas de numeración',
        config: 'Configuración del sistema',
        language: 'Gestión de idiomas',
        dict: 'Gestión de diccionarios'
      },
      contract: {
        _self: 'Gestión de contratos',
        template: {
          _self: 'Plantillas de contratos',
          manage: 'Gestión de plantillas',
          category: 'Categorías de plantillas'
        },
        draft: {
          _self: 'Redacción de contratos',
          apply: 'Solicitud de redacción',
          my: 'Mis redacciones'
        },
        approval: {
          _self: 'Aprobación de contratos',
          pending: 'Aprobación de contratos',
          approved: 'Aprobado',
          record: 'Registros de aprobación'
        },
        execution: {
          _self: 'Ejecución de contratos',
          track: 'Seguimiento de ejecución',
          change: 'Gestión de cambios',
          payment: 'Gestión de pagos'
        },
        archive: {
          _self: 'Archivo de contratos',
          manage: 'Gestión de archivos',
          query: 'Estadísticas de consulta'
        }
      },
      project: {
        _self: 'Gestión de proyectos',
        info: {
          _self: 'Información del proyecto',
          list: 'Lista de proyectos'
        },
        plan: {
          _self: 'Planificación de proyectos',
          request: 'Solicitud de plan',
          gantt: 'Diagrama de Gantt del proyecto'
        },
        task: {
          _self: 'Tareas del proyecto',
          assign: 'Asignación de tareas',
          track: 'Seguimiento de tareas',
          board: 'Tablero de tareas'
        },
        resource: {
          _self: 'Recursos del proyecto',
          personnel: 'Gestión de personal',
          equipment: 'Gestión de equipos',
          budget: 'Gestión de presupuesto'
        },
        monitor: {
          _self: 'Monitoreo de proyectos',
          progress: 'Monitoreo de progreso',
          quality: 'Monitoreo de calidad',
          risk: 'Monitoreo de riesgos'
        }
      },
      quartz: {
        _self: 'Programación de tareas',
        job: {
          _self: 'Gestión de tareas',
          config: 'Configuración de tareas',
          list: 'Lista de tareas',
          status: 'Estado de tareas'
        },
        schedule: {
          _self: 'Programación de tareas',
          config: 'Configuración de programación',
          monitor: 'Monitor de programación',
          stats: 'Estadísticas de programación'
        }
      },
      schedule: {
        _self: 'Gestión de horarios',
        myschedule: 'Mi horario',
        dashboard: 'Panel de control de horarios'
      },
      vehicle: {
        _self: 'Gestión de vehículos',
        my: 'Mis vehículos',
        application: 'Solicitud de vehículo',
        dashboard: 'Panel de control de vehículos',
        manage: {
          _self: 'Gestión de vehículos',
          info: 'Información de vehículos',
          maintenance: 'Mantenimiento de vehículos'
        }
      },
      email: {
        _self: 'Gestión de correo electrónico',
        inbox: 'Bandeja de entrada',
        drafts: 'Borradores',
        sent: 'Enviados',
        trash: 'Papelera',
        template: 'Plantillas de correo'
      },
      meeting: {
        _self: 'Gestión de reuniones',
        room: 'Salas de reuniones',
        mymeeting: 'Mis reuniones',
        booking: 'Reserva de reuniones',
        dashboard: 'Panel de control de reuniones'
      },
      notice: {
        _self: 'Notificaciones y anuncios',
        message: {
          _self: 'Gestión de mensajes',
          mymessages: 'Mis mensajes',
          list: 'Tablero de mensajes'
        },
        announcement: {
          _self: 'Gestión de anuncios',
          signoff: 'Firmar anuncios',
          list: 'Lista de anuncios'
        },
        notification: {
          _self: 'Gestión de notificaciones',
          ack: 'Notificaciones leídas',
          list: 'Lista de notificaciones'
        }
      },
      hr: {
        _self: 'RRHH y asistencia',
        recruitment: {
          _self: 'Gestión de reclutamiento',
          apply: 'Solicitud de reclutamiento',
          approval: 'Aprobación de reclutamiento',
          list: 'Lista de reclutamiento'
        },
        transfer: {
          _self: 'Gestión de transferencias',
          apply: 'Solicitud de transferencia',
          approval: 'Aprobación de transferencia',
          list: 'Lista de transferencias'
        },
        leave: {
          _self: 'Gestión de permisos',
          apply: 'Solicitud de permiso',
          approval: 'Aprobación de permisos',
          list: 'Lista de permisos'
        },
        trip: {
          _self: 'Gestión de viajes de negocios',
          apply: 'Solicitud de viaje',
          approval: 'Aprobación de viaje',
          list: 'Lista de viajes'
        },
        overtime: {
          _self: 'Gestión de horas extra',
          apply: 'Solicitud de horas extra',
          approval: 'Aprobación de horas extra',
          list: 'Lista de horas extra'
        }
      },
      expense: {
        _self: 'Gestión de gastos',
        daily: {
          _self: 'Gastos diarios',
          apply: 'Solicitud de gastos',
          approve: 'Aprobación de gastos',
          list: 'Lista de gastos'
        },
        travel: {
          _self: 'Gastos de viaje',
          apply: 'Solicitud de gastos de viaje',
          approve: 'Aprobación de gastos de viaje',
          list: 'Lista de gastos de viaje'
        }
      },
      document: {
        _self: 'Gestión de documentos',
        news: {
          _self: 'Gestión de noticias',
        },
        regulation: {
          _self: 'Regulaciones y reglas',
          manage: 'Gestión de regulaciones',
          control: 'Control de regulaciones',
        },
        file: {
          _self: 'Archivos diarios',
        },
        iso: {
          _self: 'Archivos ISO',
          manage: 'Gestión de archivos',
          control: 'Control de archivos',
        },
        official: {
          _self: 'Gestión de documentos oficiales',
          manage: 'Gestión de documentos',
          issuance: 'Control de documentos',
        },
        law: {
          _self: 'Leyes y regulaciones',
        }
      },
      officesupplies: {
        _self: 'Suministros de oficina',
        inventory: {
          _self: 'Gestión de inventario',
          requisition: 'Gestión de compras',
          inbound: 'Gestión de entrada',
          stocktaking: 'Gestión de inventario'
        },
        usage: {
          _self: 'Gestión de uso',
          apply: 'Solicitud de uso',
          approve: 'Aprobación de uso',
          list: 'Registros de uso'
        }
      },
      book: {
        _self: 'Gestión de libros',
        inventory: {
          _self: 'Gestión de inventario',
          requisition: 'Gestión de compras',
          inbound: 'Gestión de entrada',
          list: 'Lista de libros',
          stocktaking: 'Gestión de inventario'
        },
        usage: {
          _self: 'Gestión de uso',
          card: 'Tarjeta de biblioteca',
          borrow: 'Prestar',
          return: 'Devolver'
        }
      },
      medical: {
        _self: 'Gestión médica',
        medicine: {
          _self: 'Gestión de inventario',
          requisition: 'Gestión de compras',
          inbound: 'Gestión de entrada',
          list: 'Lista de medicamentos',
          stocktaking: 'Gestión de inventario'
        },
        usage: {
          _self: 'Gestión de uso',
          archive: 'Archivo',
          receive: 'Recibir medicamentos',
          cost: 'Costo'
        }
      }
    },
    accounting: {
      _self: 'Contabilidad',
      financial: {
        _self: 'Contabilidad de gestión',
        company: 'Información de la empresa',
        account: 'Plan de cuentas',
        companyaccount: 'Cuentas de la empresa',
        ledger: 'Libro mayor',
        payable: 'Cuentas por pagar',
        receivable: 'Cuentas por cobrar',
        fixedasset: 'Activos fijos',
        bank: 'Información bancaria'
      },
      controlling: {
        _self: 'Control de gestión',
        costelement: 'Elementos de costo',
        costcenter: 'Centros de costo',
        profitcenter: 'Centros de beneficio',
        accountsReceivable: 'Cuentas por cobrar',
        accountsPayable: 'Cuentas por pagar',
        assetAccounting: 'Contabilidad de activos',
        tax: 'Gestión fiscal',
        financialReporting: 'Informes financieros'
      },
      budget: {
        _self: 'Presupuesto integral',
        formulation: {
          _self: 'Formulación del presupuesto',
          sales: {
            _self: 'Presupuesto de ventas',
            cost: 'Costo de ventas',
            rolling: 'Ventas rodantes'
          },
          production: {
            _self: 'Presupuesto de producción',
            auxiliary: 'Auxiliares de producción',
            labor: 'Mano de obra de producción',
            manufacturing: 'Fabricación de producción'
          },
          cost: {
            _self: 'Presupuesto de costos',
            directmaterial: 'Materiales directos',
            directlabor: 'Mano de obra directa',
            indirectlabor: 'Mano de obra indirecta',
            manufacturing: 'Gastos generales de fabricación'
          },
          expense: {
            _self: 'Presupuesto de gastos',
            sales: 'Gastos de ventas',
            manage: 'Gastos de gestión',
            financial: 'Gastos financieros'
          },
          financial: {
            _self: 'Presupuesto financiero',
            cashflow: 'Flujo de caja',
            balancesheet: 'Balance general',
            income: 'Estado de resultados'
          }
        },
        control: {
          _self: 'Control presupuestario',
          dashboard: 'Panel de control presupuestario',
          approval: 'Aprobación presupuestaria'
        }
      }
    },
    logistics: {
      _self: 'Gestión logística',
      equipment: {
        _self: 'Gestión de equipos',
        master: {
          _self: 'Datos de equipos',
          list: 'Información de equipos',
          location: 'Ubicación funcional',
          material: 'Asociación de materiales'
        },
        maintenance: {
          _self: 'Mantenimiento de equipos',
          workorder: 'Planes de mantenimiento',
          assign: 'Asignación de mantenimiento',
          execute: 'Ejecución de mantenimiento'
        }
      },
      material: {
        _self: 'Gestión de materiales',
        manage: {
          _self: 'Información de materiales',
          master: 'Materiales de grupo',
          plant: {
            _self: 'Información de planta',
            master: 'Materiales de planta'
          }
        },
        purchase: {
          _self: 'Gestión de compras',
          vendor: 'Información de vendedores',
          supplier: 'Información de proveedores',
          price: 'Precios de compra',
          requisition: 'Solicitud de compra',
          order: 'Órdenes de compra'
        },
        sample:{
          _self: 'Gestión de muestras',
          component: 'Muestras de componentes',
          product: 'Muestras de productos'
        },
        drawing: {
          _self: 'Gestión de dibujos',
          design: 'Gestión de dibujos',
          engineering: 'Control de dibujos',
          gerber: 'Archivos Gerber',
          coordinate: 'Archivos de coordenadas',
          assembly: 'Dibujos de ensamblaje',
          structure: 'Archivos de estructura',
          impedance: 'Archivos de impedancia',
          process: 'Flujo de procesos'
        },
        csm: {  
          _self: 'Gestión de artículos suministrados por el cliente',
          raw: 'Materiales suministrados por el cliente',
          good: 'Productos suministrados por el cliente'
        }
      },
      production: {
        _self: 'Gestión de producción',
        basic: {
          _self: 'Datos básicos',
          bom: 'Lista de materiales',
          workcenter: 'Centros de trabajo',   
          routing: 'Rutas de procesos',
          order: 'Órdenes de producción',
          worktime: 'Horas de producción',
          kanban: 'Kanban'
        },
        change: {
          _self: 'Cambios de diseño',
          implementation: 'Implementación de cambios',
          techcontact: 'Contacto técnico',
          material: 'Confirmación de materiales',
          query: 'Consulta de cambios',
          oldproduct: 'Control de productos antiguos',
          sop: 'Confirmación SOP',
          batch: 'Lote de entrada',
          input: {
            _self: 'Entrada de cambios',
            gijutsu: 'Departamento técnico',
            seikan: 'Departamento de control de producción',
            koubai: 'Departamento de compras',
            uketsuke: 'Departamento de inspección',
            bukan: 'Gestión de departamento',
            seizou2: 'Departamento de producción 2',
            seizou1: 'Departamento de producción 1',
            hinkan: 'Departamento de control de calidad',
            seizougijutsu: 'Departamento de tecnología de producción'
          }
        },       
        output: {
          _self: 'Gestión de fabricación',
          workshop1:{
            _self: 'Departamento de producción 1',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: 'Producción',
              modify: 'Modificación',
              rework: 'Retrabajo'
            },
            defect:{
              _self: 'Defectos',
              epp: 'EPP',
              production: 'Producción',
              modify: 'Modificación',
              rework: 'Retrabajo'
            },
            worktime: {
              _self: 'Horas de trabajo',
              epp: 'EPP',
              production: 'Producción',
              modify: 'Modificación',
              rework: 'Retrabajo'
            }
          },
          workshop2:{
            _self: 'Departamento de producción 2',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: 'Producción',
              modify: 'Modificación',
              rework: 'Retrabajo'
            },
            defect:{
              _self: 'Defectos',
              eppInspection: 'Inspección EPP',
              eppRepair: 'Reparación EPP',
              productionInspection: 'Inspección de producción',
              productionRepair: 'Reparación de producción',
              modifyInspection: 'Inspección de modificación',
              modifyRepair: 'Reparación de modificación',
              reworkInspection: 'Inspección de retrabajo',
              reworkRepair: 'Reparación de retrabajo'
            },
            worktime: {
              _self: 'Horas de trabajo',
              epp: 'EPP',
              production: 'Producción',
              modify: 'Modificación',
              rework: 'Retrabajo'
            }
          }
        },
        sop: {
          _self: 'Gestión SOP',
          workshop1: 'Departamento de producción 1',
          workshop2: 'Departamento de producción 2'
        },
        techcontact: {
          _self: 'Contacto técnico',
          epp: 'Contacto EPP',
          engineering: 'Contacto de ingeniería',
          external: 'Contacto externo'
        }
      },
      project: {
        _self: 'Gestión de proyectos',
        define: 'Definición de proyecto',
        cost: 'Planificación de costos',
        resource: 'Planificación de recursos',
        schedule: 'Planificación de horarios'
      },
      quality: {
        _self: 'Gestión de calidad',
        basic: {
          _self: 'Datos básicos',
          item: 'Elementos de inspección',
          method: 'Métodos de inspección',
          sampling: 'Planes de muestreo',
          defect: 'Categorías de defectos',
          rule: 'Reglas de juicio',
          category: 'Categorías de calidad'
        },
        inspection:{
          _self: 'Gestión de inspección',
          receiving: 'Inspección de recepción',
          process: 'Inspección de procesos',
          storage: 'Inspección de almacenamiento',
          return: 'Inspección de devolución'
        },
        trace:{
          _self: 'Gestión de trazabilidad',
          batch: 'Trazabilidad de lotes',
          corrective: 'Acciones correctivas',
          notification: 'Notificaciones',
        },
        cost:{
          _self: 'Costos de calidad',
          business:'Actividades de calidad',
          rework:'Actividades de retrabajo',
          scrap:'Actividades de desperdicio',
        },
        plan: {
          _self: 'Planificación de calidad',
          supplier: 'Evaluación de proveedores',
          customer: 'Encuesta de clientes'
        },
        item: 'Elementos de inspección',
        receiving: 'Inspección de recepción',
        process: 'Inspección de procesos',
        storage: 'Inspección de almacenamiento',
        return: 'Inspección de devolución'
      },
      sales: {
        _self: 'Gestión de ventas',
        customer: 'Información del cliente',
        client: 'Información del cliente',
        price: 'Precios de venta',
        order: 'Órdenes de venta'
      },
      service: {
        _self: 'Servicio al cliente',
        cs: {
          _self: 'Servicio al cliente',
          item: 'Elementos de servicio',
          contract: 'Contratos de servicio',
          request: 'Solicitudes de servicio',
          workorder: 'Órdenes de trabajo de servicio',
          timesheet: 'Horas de servicio',
          consumption: 'Consumo de materiales',
          outsourcing: 'Servicios externalizados'
        },
        cc: {
          _self: 'Gestión de quejas de clientes',
          notice: 'Notificaciones de calidad',
          mark: 'Detalles de quejas',
          analysis: 'Análisis de causas',
          corrective: 'Acciones correctivas',
          return: 'Ejecución de devoluciones/intercambios',
          followUp: 'Procesamiento de seguimiento'
        }
      }
    },
    hrm: {
      _self: 'Recursos humanos',
      attendance: {
        _self: 'Gestión de asistencia',
        record: 'Registros de asistencia',
        holiday: 'Gestión de vacaciones',
        overtime: 'Gestión de horas extra',
        compensatory: 'Gestión de tiempo compensatorio'
      },
      benefit: {
        _self: 'Gestión de beneficios',
        project: 'Proyectos de beneficios',
        employee: 'Beneficios de empleados'
      },
      employee: {
        _self: 'Gestión de personal',
        info: 'Información de personal',
        contracttype: 'Tipos de contrato',
        contract: 'Gestión de contratos',
        promotion: 'Gestión de promociones',
        promotionhistory: 'Historial de promociones',
        resignation: 'Gestión de renuncias',
        transfer: 'Lista de personal',
        transferhistory: 'Historial de transferencias'
      },
      leave: {
        _self: 'Gestión de permisos',
        type: 'Tipos de permiso',
        employee: 'Permisos de empleados'
      },
      organization: {
        _self: 'Gestión organizacional',
        positioncategory: 'Categorías de puestos',
        company: 'Información de la empresa',
        department: 'Información de departamentos',
        position: 'Información de puestos'
      },
      performance: {
        _self: 'Gestión de rendimiento',
        assessmentitem: 'Elementos de evaluación',
        assessment: 'Evaluación de rendimiento'
      },
      recruitment: {
        _self: 'Gestión de reclutamiento',
        application: 'Solicitudes de empleo',
        posting: 'Publicaciones de empleo',
        candidate: 'Gestión de candidatos',
        interview: 'Gestión de entrevistas'
      },
      salary: {
        _self: 'Gestión de salarios',
        employee: 'Salarios de empleados',
        housing: 'Fondo de vivienda',
        housinglevel: 'Seguridad social',
        tax: 'Gestión fiscal',
        taxlevel: 'Niveles de impuestos',
        structure: 'Estructura de salarios',
        social: 'Seguridad social',
        socialbase: 'Base de seguridad social'
      },
      training: {
        _self: 'Gestión de capacitación',
        category: 'Categorías de capacitación',
        course: 'Cursos de capacitación',
        record: 'Capacitación de empleados'
      },
      report: {
        _self: 'Gestión de informes',
        employeeinfo: 'Información de personal',
        resignation: 'Informes de renuncia',
        transfer: 'Informes de transferencia',
        promotion: 'Informes de promoción',
        training: 'Informes de capacitación',
        salary: 'Informes de salarios',
        performance: 'Informes de rendimiento',
        attendance: 'Informes de asistencia',
        benefit: 'Informes de beneficios',
        recruitment: 'Informes de reclutamiento'
      }
    }
  }
}
