export default {
  menu: {
    home: 'Accueil',
    dashboard: {
      title: 'Tableau de bord',
      workplace: 'Espace de travail',
      analysis: 'Analyse',
      monitor: 'Surveillance'
    },
    components: {
      title: 'Composants',
      icons: 'Icônes'
    },
    about: {
      title: 'À propos de nous',
      privacy: 'Politique de confidentialité',
      terms: "Conditions d'utilisation",
      index: 'À propos de Hbt'
    },
    admin: {
      _self: 'Administration du système',
      config: 'Configuration du système',
      language: 'Gestion des langues',
      dicttype: 'Types de dictionnaire',
      dictdata: 'Données du dictionnaire',
      translation: 'Gestion des traductions'
    },
    identity: {
      _self: 'Gestion des identités',
      user: 'Gestion des utilisateurs',
      role: 'Gestion des rôles',
      dept: 'Gestion des départements',
      post: 'Gestion des postes',
      menu: 'Gestion des menus',
      tenant: 'Gestion des locataires',
      oauth: 'Gestion OAuth'
    },
    audit: {
      _self: "Journaux d'audit",
      operlog: 'Journaux des opérations',
      loginlog: 'Journaux de connexion',
      dbdifflog: 'Journaux des différences de base de données',
      exceptionlog: 'Journaux des exceptions'
    },
    workflow: {
      _self: 'Flux de travail',
      definition: 'Définition du flux de travail',
      instance: 'Instance du flux de travail',
      task: 'Tâches',
      node: 'Nœuds',
      variable: 'Variables',
      history: 'Historique'
    },
    realtime: {
      _self: 'Surveillance en temps réel',
      server: 'Surveillance du serveur',
      online: 'Utilisateurs en ligne',
      message: 'Messages en ligne'
    },
    generator: {
      _self: 'Générateur de code',
      table: 'Tables de base de données',
      tableDefine: 'Tables personnalisées',
      template: 'Modèles de code',
      config: 'Configuration de génération',
      api: 'Documentation API'
    },
    routine: {
      _self: 'Travail quotidien',
      file: 'Gestion des fichiers',
      mail: 'Gestion des courriels',
      mailTmpl: 'Modèles de courriels',
      notice: 'Avis',
      task: 'Tâches',
      schedule: 'Gestion des horaires'
    },
    finance: {
      _self: 'Finance',
      management: {
        _self: 'Comptabilité de gestion',
        cost: {
          _self: 'Gestion des coûts',
          costFactors: 'Facteurs de coût',
          costCenter: 'Centre de coûts',
          profitCenter: 'Centre de profit',
          productCost: 'Coût du produit',
          activityType: "Type d'activité",
          internalOrder: 'Ordres internes'
        },
        planning: {
          _self: 'Gestion de la planification',
          costPlanning: 'Planification des coûts',
          profitPlanning: 'Planification des profits',
          budgetControl: 'Contrôle budgétaire'
        },
        reporting: {
          _self: 'Rapports et analyses',
          costReports: 'Rapports de coûts',
          profitReports: 'Rapports de profits',
          varianceAnalysis: 'Analyse des écarts'
        }
      },
      financial: {
        _self: 'Comptabilité financière',
        generalLedger: {
          _self: 'Grand livre',
          account: 'Comptes',
          accountType: 'Types de comptes',
          journalEntry: 'Écritures comptables',
          reconciliation: 'Rapprochement',
          closing: 'Clôture périodique'
        },
        accountsReceivable: {
          _self: 'Comptes clients',
          customer: 'Gestion des clients',
          invoice: 'Factures clients',
          payment: 'Paiements clients',
          creditControl: 'Contrôle de crédit'
        },
        accountsPayable: {
          _self: 'Comptes fournisseurs',
          supplier: 'Gestion des fournisseurs',
          invoice: 'Factures fournisseurs',
          payment: 'Paiements fournisseurs',
          agingReport: "Rapports d'ancienneté"
        },
        assetAccounting: {
          _self: 'Comptabilité des actifs',
          assets: 'Actifs fixes',
          depreciation: 'Gestion des amortissements',
          assetTransfer: "Transfert d'actifs",
          assetRetirement: "Sortie d'actifs"
        },
        tax: {
          _self: 'Gestion fiscale',
          taxCodes: 'Codes fiscaux',
          taxReporting: 'Rapports fiscaux',
          taxPayments: 'Paiements fiscaux'
        },
        financialReporting: {
          _self: 'Rapports financiers',
          balanceSheet: 'Bilan',
          profitAndLoss: 'Compte de résultat',
          cashFlow: 'Tableau des flux de trésorerie'
        }
      }
    },
    logistics: {
      _self: 'Logistique',
      sales: {
        _self: 'Gestion des ventes',
        customer: {
          _self: 'Gestion des clients',
          client: 'Clients',
          customers: 'Liste des clients',
          creditControl: 'Contrôle de crédit'
        },
        order: {
          _self: 'Gestion des commandes',
          order: 'Commandes de vente',
          orderDetail: 'Détails des commandes',
          orderTracking: 'Suivi des commandes'
        },
        delivery: {
          _self: 'Gestion des livraisons',
          delivery: 'Documents de livraison',
          deliveryDetail: 'Détails des livraisons',
          shipping: 'Gestion des expéditions'
        },
        billing: {
          _self: 'Gestion de la facturation',
          invoice: 'Gestion des factures',
          invoiceDetail: 'Détails des factures',
          payment: 'Gestion des paiements'
        },
        reporting: {
          _self: 'Rapports et analyses',
          salesReports: 'Rapports de ventes',
          performanceAnalysis: 'Analyse des performances'
        }
      },
      production: {
        _self: 'Gestion de la production',
        bom: 'Liste des matériaux (BOM)',
        routing: 'Routage',
        workOrder: {
          _self: 'Ordres de travail',
          create: 'Créer un ordre de travail',
          manage: 'Gérer les ordres de travail',
          release: 'Libérer les ordres de travail',
          complete: 'Terminer les ordres de travail'
        },
        capacityPlanning: {
          _self: 'Planification de capacité',
          workCenter: 'Centres de travail',
          capacityEvaluation: 'Évaluation de capacité',
          capacityLeveling: 'Nivellement de capacité'
        },
        productionScheduling: {
          _self: 'Planification de la production',
          schedule: 'Planifier',
          reschedule: 'Replanifier'
        },
        productionExecution: {
          _self: 'Exécution de la production',
          confirm: 'Confirmer la production',
          goodsIssue: 'Sortie de marchandises',
          goodsReceipt: 'Réception de marchandises'
        },
        productionReporting: {
          _self: 'Rapports de production',
          orderReports: 'Rapports des ordres',
          capacityReports: 'Rapports de capacité',
          efficiencyReports: "Rapports d'efficacité"
        },
        qualityManagement: {
          _self: 'Gestion de la qualité',
          inspectionLot: "Lots d'inspection",
          resultsRecording: 'Enregistrement des résultats',
          defectRecording: 'Enregistrement des défauts'
        }
      },
      material: {
        _self: 'Gestion des matériaux',
        materialMaster: 'Données maîtres des matériaux',
        materialCategory: 'Catégories de matériaux',
        materialUnit: 'Unités de matériaux',
        materialStock: {
          _self: 'Inventaire des matériaux',
          stockOverview: 'Aperçu des stocks',
          stockIn: 'Entrée de stock',
          stockOut: 'Sortie de stock',
          stockTransfer: 'Transfert de stock',
          stockAdjustment: 'Ajustement de stock',
          stockCheck: 'Vérification de stock'
        },
        purchase: {
          _self: 'Gestion des achats',
          purchaseRequisition: "Demandes d'achat",
          purchaseOrder: "Ordres d'achat",
          purchaseOrderDetail: "Détails des ordres d'achat",
          supplier: 'Gestion des fournisseurs'
        },
        inventoryManagement: {
          _self: 'Gestion des stocks',
          goodsReceipt: 'Réception de marchandises',
          goodsIssue: 'Sortie de marchandises',
          transferPosting: 'Transfert de stock',
          stockOverview: 'Aperçu des stocks'
        },
        valuation: {
          _self: 'Évaluation des matériaux',
          priceControl: 'Contrôle des prix',
          standardPrice: 'Prix standard',
          movingAveragePrice: 'Prix moyen mobile'
        },
        reporting: {
          _self: 'Rapports et analyses',
          stockReports: 'Rapports de stocks',
          purchaseReports: "Rapports d'achats",
          inventoryReports: "Rapports d'analyse des stocks"
        }
      }
    },
    quality: {
      _self: 'Gestion de la qualité',
      inspection: {
        _self: 'Gestion des inspections',
        inspectionLot: "Lots d'inspection",
        resultsRecording: 'Enregistrement des résultats',
        defectRecording: 'Enregistrement des défauts',
        usageDecision: "Décision d'utilisation"
      },
      qualityPlanning: {
        _self: 'Planification de la qualité',
        inspectionPlan: "Plans d'inspection",
        qualityInfoRecord: "Enregistrements d'information sur la qualité",
        samplingProcedure: "Procédures d'échantillonnage"
      },
      qualityControl: {
        _self: 'Contrôle de la qualité',
        controlChart: 'Graphiques de contrôle',
        qualityNotifications: 'Notifications de qualité',
        correctiveActions: 'Actions correctives'
      },
      qualityReporting: {
        _self: 'Rapports de qualité',
        inspectionReports: "Rapports d'inspection",
        defectReports: 'Rapports de défauts',
        qualityAnalysis: 'Analyse de la qualité'
      }
    },
    service: {
      _self: 'Service client',
      serviceOrder: {
        _self: 'Ordres de service',
        create: 'Créer un ordre de service',
        manage: 'Gérer les ordres de service',
        complete: 'Terminer les ordres de service',
        cancel: 'Annuler les ordres de service'
      },
      serviceContract: {
        _self: 'Contrats de service',
        create: 'Créer un contrat de service',
        manage: 'Gérer les contrats de service',
        renew: 'Renouveler les contrats de service',
        terminate: 'Terminer les contrats de service'
      },
      customerInteraction: {
        _self: 'Interaction client',
        inquiries: 'Demandes des clients',
        complaints: 'Réclamations des clients',
        feedback: 'Commentaires des clients'
      },
      serviceExecution: {
        _self: 'Exécution du service',
        schedule: 'Planifier le service',
        dispatch: 'Envoyer le service',
        execution: 'Exécuter le service',
        confirmation: 'Confirmer le service'
      },
      serviceReporting: {
        _self: 'Rapports de service',
        orderReports: 'Rapports des ordres de service',
        contractReports: 'Rapports des contrats de service',
        performanceReports: 'Rapports de performance'
      }
    },
    equipment: {
      _self: 'Gestion des équipements',
      equipmentMaster: 'Données maîtres des équipements',
      maintenancePlanning: {
        _self: 'Planification de la maintenance',
        preventiveMaintenance: 'Maintenance préventive',
        maintenanceTaskList: 'Liste des tâches de maintenance',
        scheduling: 'Planification de la maintenance'
      },
      maintenanceExecution: {
        _self: 'Exécution de la maintenance',
        workOrder: 'Ordres de travail de maintenance',
        confirmation: 'Confirmation de maintenance',
        breakdownMaintenance: 'Maintenance corrective'
      },
      maintenanceReporting: {
        _self: 'Rapports de maintenance',
        equipmentReports: 'Rapports des équipements',
        maintenanceHistory: 'Historique de maintenance',
        performanceAnalysis: 'Analyse des performances'
      },
      sparePartsManagement: {
        _self: 'Gestion des pièces détachées',
        sparePartsInventory: 'Inventaire des pièces détachées',
        sparePartsProcurement: 'Approvisionnement des pièces détachées',
        sparePartsUsage: 'Utilisation des pièces détachées'
      }
    },
    humanResources: {
      _self: 'Gestion des ressources humaines',
      employeeManagement: {
        _self: 'Gestion des employés',
        employeeMaster: 'Données maîtres des employés',
        attendance: 'Gestion des présences',
        leave: 'Gestion des congés',
        payroll: 'Gestion des salaires',
        contractManagement: 'Gestion des contrats'
      },
      recruitment: {
        _self: 'Gestion du recrutement',
        jobPosting: "Publication des offres d'emploi",
        candidateManagement: 'Gestion des candidats',
        interviewScheduling: 'Planification des entretiens',
        offerManagement: 'Gestion des offres'
      },
      training: {
        _self: 'Gestion de la formation',
        trainingPlan: 'Plans de formation',
        trainingExecution: 'Exécution de la formation',
        trainingEvaluation: 'Évaluation de la formation'
      },
      performance: {
        _self: 'Gestion des performances',
        goalSetting: 'Définition des objectifs',
        performanceReview: 'Évaluation des performances',
        feedback: 'Gestion des retours'
      },
      reporting: {
        _self: 'Rapports RH',
        employeeReports: 'Rapports des employés',
        attendanceReports: 'Rapports de présence',
        payrollReports: 'Rapports de paie',
        performanceReports: 'Rapports de performance'
      }
    }
  }
}
