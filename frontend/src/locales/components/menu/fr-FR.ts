import { countReset } from "node:console";

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
      title: 'À propos',
      privacy: 'Politique de confidentialité',
      terms: 'Conditions d\'utilisation',
      index: 'À propos de Hbt'
    },
    core: {
      _self: 'Gestion du système',
      config: 'Configuration du système',
      language: 'Gestion des langues',
      dict: 'Gestion des dictionnaires'
    },
    identity: {
      _self: 'Authentification',
      user: 'Gestion des utilisateurs',
      role: 'Gestion des rôles',
      dept: 'Gestion des départements',
      post: 'Gestion des postes',
      menu: 'Gestion des menus',
      tenant: 'Gestion des locataires',
      oauth: 'Gestion OAuth'
    },
    audit: {
      _self: 'Audit',
      operlog: 'Journal des opérations',
      loginlog: 'Journal de connexion',
      sqldifflog: 'Journal des différences de base de données',
      exceptionlog: 'Journal des exceptions',
      auditlog: 'Journal d\'audit',
      quartzlog: 'Journal des tâches'
    },
    workflow: {
      _self: 'Workflow',
      form: 'Formulaire',
      definition: 'Définition du processus',
      instance: 'Instance du processus',
      task: 'Tâche',
      node: 'Nœud du processus',
      variable: 'Variable du processus',
      history: 'Historique du processus'
    },
    signalr: {
      _self: 'Tiempo real',
      online: 'Utilisateurs',
      message: 'Messages'
    },
    generator: {
      _self: 'Génération de code',
      table: 'Table de base de données',
      tableDefine: 'Table personnalisée',
      template: 'Modèle de code',
      config: 'Configuration de génération',
      api: 'Documentation API'
    },
    routine: {
      _self: 'Tâches quotidiennes',
      vehicle: {
        _self: 'Gestion des véhicules',
        vehicleMaster: {
          _self: 'Données principales des véhicules',
          vehicleInfo: 'Informations sur les véhicules',
          driverInfo: 'Informations sur les conducteurs',
          maintenance: 'Maintenance des véhicules'
        },
        vehicleBooking: {
          _self: 'Réservation de véhicules',
          newBooking: 'Nouvelle réservation',
          bookingList: 'Liste des réservations',
          bookingApproval: 'Approbation des réservations'
        },
        vehicleDispatch: {
          _self: 'Expédition des véhicules',
          dispatchPlan: 'Plan d\'expédition',
          realTimeTracking: 'Suivi en temps réel',
          dispatchHistory: 'Historique des expéditions'
        },
        vehicleReporting: {
          _self: 'Rapports sur les véhicules',
          usageReport: 'Rapport d\'utilisation',
          costReport: 'Rapport des coûts',
          maintenanceReport: 'Rapport de maintenance'
        }
      },
      file: 'Gestion des fichiers',
      mail: 'Gestion des e-mails',
      mailTmpl: 'Modèle d\'e-mail',
      meeting: {
        _self: 'Gestion des réunions',
        meetingRoom: {
          _self: 'Gestion des salles de réunion',
          roomInfo: 'Informations sur la salle',
          roomBooking: 'Réservation de salle',
          roomSchedule: 'Planning de la salle'
        },
        meetingPlan: {
          _self: 'Planification des réunions',
          newMeeting: 'Nouvelle réunion',
          meetingList: 'Liste des réunions',
          meetingApproval: 'Approbation des réunions'
        },
        meetingExecution: {
          _self: 'Exécution des réunions',
          attendance: 'Présence',
          minutes: 'Procès-verbal',
          followUp: 'Suivi'
        },
        meetingReporting: {
          _self: 'Rapports sur les réunions',
          meetingReport: 'Rapport de réunion',
          attendanceReport: 'Rapport de présence',
          costReport: 'Rapport des coûts'
        }
      },
      notice: 'Avis',
      schedule: 'Gestion des horaires',
      quartz: 'Tâche'
    },
    finance: {
      _self: 'Finance',
      management: {
        _self: 'Comptabilité de gestion',
        cost: {
          _self: 'Gestion des coûts',
          costFactors: 'Types de coûts',
          costCenter: 'Centre de coûts',
          profitCenter: 'Centre de profit',
          productCost: 'Coût des produits',
          activityType: 'Type d\'activité',
          internalOrder: 'Commande interne'
        },
        planning: {
          _self: 'Gestion de la planification',
          costPlanning: 'Planification des coûts',
          profitPlanning: 'Planification des profits',
          budgetControl: 'Contrôle budgétaire'
        },
        reporting: {
          _self: 'Rapports et analyse',
          costReports: 'Rapports sur les coûts',
          profitReports: 'Rapports sur les profits',
          varianceAnalysis: 'Analyse des écarts'
        }
      },
      financial: {
        _self: 'Comptabilité financière',
        generalLedger: {
          _self: 'Grand livre',
          account: 'Compte',
          accountType: 'Type de compte',
          journalEntry: 'Écriture comptable',
          reconciliation: 'Réconciliation',
          closing: 'Clôture'
        },
        accountsReceivable: {
          _self: 'Comptes clients',
          customer: 'Gestion des clients',
          invoice: 'Facture client',
          payment: 'Paiement client',
          creditControl: 'Contrôle du crédit'
        },
        accountsPayable: {
          _self: 'Comptes fournisseurs',
          supplier: 'Gestion des fournisseurs',
          invoice: 'Facture fournisseur',
          payment: 'Paiement fournisseur',
          agingReport: 'Analyse des échéances'
        },
        assetAccounting: {
          _self: 'Comptabilité des actifs',
          assets: 'Immobilisations',
          depreciation: 'Gestion de l\'amortissement',
          assetTransfer: 'Transfert d\'actifs',
          assetRetirement: 'Retrait d\'actifs'
        },
        tax: {
          _self: 'Gestion des taxes',
          taxCodes: 'Gestion des codes fiscaux',
          taxReporting: 'Déclaration fiscale',
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
      equipment: {
        _self: 'Gestion des équipements',
        equipmentMaster: 'Données principales des équipements',
        maintenancePlanning: {
          _self: 'Planification de la maintenance',
          preventiveMaintenance: 'Maintenance préventive',
          maintenanceTaskList: 'Liste des tâches de maintenance',
          scheduling: 'Planification de la maintenance'
        },
        maintenanceExecution: {
          _self: 'Exécution de la maintenance',
          workOrder: 'Ordre de travail',
          confirmation: 'Confirmation de maintenance',
          breakdownMaintenance: 'Maintenance en cas de panne'
        },
        maintenanceReporting: {
          _self: 'Rapports de maintenance',
          equipmentReports: 'Rapports sur les équipements',
          maintenanceHistory: 'Historique de maintenance',
          performanceAnalysis: 'Analyse des performances'
        },
        sparePartsManagement: {
          _self: 'Gestion des pièces de rechange',
          sparePartsInventory: 'Inventaire des pièces de rechange',
          sparePartsProcurement: 'Approvisionnement en pièces de rechange',
          sparePartsUsage: 'Utilisation des pièces de rechange'
        }
      },
      material: {
        _self: 'Gestion des matériaux',
        materialMaster: 'Données principales des matériaux',
        materialCategory: 'Catégorie de matériau',
        materialUnit: 'Unité de matériau',
        materialStock: {
          _self: 'Stock de matériaux',
          stockOverview: 'Aperçu du stock',
          stockIn: 'Entrée en stock',
          stockOut: 'Sortie de stock',
          stockTransfer: 'Transfert de stock',
          stockAdjustment: 'Ajustement de stock',
          stockCheck: 'Vérification de stock'
        },
        purchase: {
          _self: 'Achats',
          purchaseRequisition: 'Demande d\'achat',
          purchaseOrder: 'Commande d\'achat',
          purchaseOrderDetail: 'Détail de la commande d\'achat',
          supplier: 'Gestion des fournisseurs'
        },
        inventoryManagement: {
          _self: 'Gestion des stocks',
          goodsReceipt: 'Réception de marchandises',
          goodsIssue: 'Émission de marchandises',
          transferPosting: 'Transfert',
          stockOverview: 'Aperçu du stock'
        },
        valuation: {
          _self: 'Évaluation',
          priceControl: 'Contrôle des prix',
          standardPrice: 'Prix standard',
          movingAveragePrice: 'Prix moyen mobile'
        },
        reporting: {
          _self: 'Rapports et analyse',
          stockReports: 'Rapports sur les stocks',
          purchaseReports: 'Rapports sur les achats',
          inventoryReports: 'Rapports sur les inventaires'
        }
      },
      production: {
        _self: 'Gestion de la production',
        bom: 'Nomenclature',
        routing: 'Gamme de fabrication',
        workOrder: {
          _self: 'Ordre de fabrication',
          create: 'Création d\'ordre',
          manage: 'Gestion d\'ordre',
          release: 'Lancement d\'ordre',
          complete: 'Clôture d\'ordre'
        },
        capacityPlanning: {
          _self: 'Planification des capacités',
          workCenter: 'Centre de travail',
          capacityEvaluation: 'Évaluation des capacités',
          capacityLeveling: 'Nivellement des capacités'
        },
        productionScheduling: {
          _self: 'Planification de la production',
          schedule: 'Planification',
          reschedule: 'Replanification'
        },
        productionExecution: {
          _self: 'Exécution de la production',
          confirm: 'Confirmation de production',
          goodsIssue: 'Émission de matières',
          goodsReceipt: 'Réception de produits'
        },
        productionReporting: {
          _self: 'Rapports de production',
          orderReports: 'Rapports sur les ordres',
          capacityReports: 'Rapports sur les capacités',
          efficiencyReports: 'Rapports sur l\'efficacité'
        },
        qualityManagement: {
          _self: 'Gestion de la qualité',
          inspectionLot: 'Lot d\'inspection',
          resultsRecording: 'Enregistrement des résultats',
          defectRecording: 'Enregistrement des défauts'
        }
      },
      project: {
        _self: 'Gestion de projet',
        projectMaster: {
          _self: 'Données principales du projet',
          projectDefinition: 'Définition du projet',
          projectStructure: 'Structure du projet',
          projectTeam: 'Équipe du projet',
          projectCalendar: 'Calendrier du projet'
        },
        projectPlanning: {
          _self: 'Planification du projet',
          workBreakdown: 'Structure de répartition du travail',
          scheduling: 'Planification',
          resourcePlanning: 'Planification des ressources',
          costPlanning: 'Planification des coûts'
        },
        projectExecution: {
          _self: 'Exécution du projet',
          taskManagement: 'Gestion des tâches',
          progressTracking: 'Suivi de l\'avancement',
          resourceManagement: 'Gestion des ressources',
          costControl: 'Contrôle des coûts'
        },
        projectMonitoring: {
          _self: 'Suivi du projet',
          progressReports: 'Rapports d\'avancement',
          resourceReports: 'Rapports sur les ressources',
          costReports: 'Rapports sur les coûts',
          riskManagement: 'Gestion des risques'
        },
        projectClosure: {
          _self: 'Clôture du projet',
          finalReport: 'Rapport final',
          lessonsLearned: 'Leçons apprises',
          projectArchive: 'Archivage du projet'
        }
      },
      quality: {
        _self: 'Gestion de la qualité',
        inspection: {
          _self: 'Gestion des inspections',
          inspectionLot: 'Lot d\'inspection',
          resultsRecording: 'Enregistrement des résultats',
          defectRecording: 'Enregistrement des défauts',
          usageDecision: 'Décision d\'utilisation'
        },
        qualityPlanning: {
          _self: 'Planification de la qualité',
          inspectionPlan: 'Plan d\'inspection',
          qualityInfoRecord: 'Enregistrement d\'informations qualité',
          samplingProcedure: 'Procédure d\'échantillonnage'
        },
        qualityControl: {
          _self: 'Contrôle qualité',
          controlChart: 'Carte de contrôle',
          qualityNotifications: 'Notifications qualité',
          correctiveActions: 'Actions correctives'
        },
        qualityReporting: {
          _self: 'Rapports qualité',
          inspectionReports: 'Rapports d\'inspection',
          defectReports: 'Rapports sur les défauts',
          qualityAnalysis: 'Analyse qualité'
        }
      },
      sales: {
        _self: 'Ventes',
        customer: {
          _self: 'Gestion des clients',
          client: 'Client',
          customers: 'Liste des clients',
          creditControl: 'Contrôle du crédit'
        },
        order: {
          _self: 'Gestion des commandes',
          order: 'Commande de vente',
          orderDetail: 'Détail de la commande',
          orderTracking: 'Suivi de commande'
        },
        delivery: {
          _self: 'Gestion des livraisons',
          delivery: 'Bon de livraison',
          deliveryDetail: 'Détail de la livraison',
          shipping: 'Gestion des expéditions'
        },
        billing: {
          _self: 'Facturation',
          invoice: 'Gestion des factures',
          invoiceDetail: 'Détail de la facture',
          payment: 'Gestion des paiements'
        },
        reporting: {
          _self: 'Rapports et analyse',
          salesReports: 'Rapports de ventes',
          performanceAnalysis: 'Analyse des performances'
        }
      },
      service: {
        _self: 'Service',
        serviceOrder: {
          _self: 'Ordre de service',
          create: 'Création d\'ordre',
          manage: 'Gestion d\'ordre',
          complete: 'Clôture d\'ordre',
          cancel: 'Annulation d\'ordre'
        },
        serviceContract: {
          _self: 'Contrat de service',
          create: 'Création de contrat',
          manage: 'Gestion de contrat',
          renew: 'Renouvellement de contrat',
          terminate: 'Résiliation de contrat'
        },
        customerInteraction: {
          _self: 'Interaction client',
          inquiries: 'Demandes clients',
          complaints: 'Réclamations',
          feedback: 'Retours clients'
        },
        serviceExecution: {
          _self: 'Exécution du service',
          schedule: 'Planification',
          dispatch: 'Dispatching',
          execution: 'Exécution',
          confirmation: 'Confirmation'
        },
        serviceReporting: {
          _self: 'Rapports de service',
          orderReports: 'Rapports sur les ordres',
          contractReports: 'Rapports sur les contrats',
          performanceReports: 'Rapports sur les performances'
        }
      }
    },
    humanResources: {
      _self: 'Ressources humaines',
      employee: {
        _self: 'Gestion des employés',
        employeeInfo: 'Informations employé',
        employeeProfile: 'Profil employé',
        employeeContract: 'Contrat employé',
        employeeAttendance: 'Présence employé',
        employeeLeave: 'Congés employé',
        employeePerformance: 'Performance employé'
      },
      recruitment: {
        _self: 'Gestion du recrutement',
        jobPosting: 'Offre d\'emploi',
        candidate: 'Gestion des candidats',
        interview: 'Gestion des entretiens',
        offer: 'Gestion des offres',
        onboarding: 'Intégration'
      },
      training: {
        _self: 'Gestion de la formation',
        trainingPlan: 'Plan de formation',
        trainingCourse: 'Cours de formation',
        trainingRecord: 'Enregistrement de formation',
        trainingEvaluation: 'Évaluation de formation'
      },
      performance: {
        _self: 'Gestion de la performance',
        performancePlan: 'Plan de performance',
        performanceAppraisal: 'Évaluation de performance',
        performanceReview: 'Revue de performance',
        performanceImprovement: 'Amélioration de performance'
      },
      compensation: {
        _self: 'Gestion de la rémunération',
        salary: 'Gestion des salaires',
        bonus: 'Gestion des primes',
        benefits: 'Gestion des avantages',
        payroll: 'Bulletin de paie'
      },
      reporting: {
        _self: 'Rapports et analyse',
        employeeReports: 'Rapports sur les employés',
        recruitmentReports: 'Rapports sur le recrutement',
        trainingReports: 'Rapports sur la formation',
        performanceReports: 'Rapports sur la performance',
        compensationReports: 'Rapports sur la rémunération'
      }
    }
  }
}
