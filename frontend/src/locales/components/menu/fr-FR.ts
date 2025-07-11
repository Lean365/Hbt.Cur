import { countReset } from "node:console";

export default {
  menu: {
    home: 'Accueil',
    dashboard: {
      title: 'Tableau de bord',
      workplace: 'Espace de travail',
      analysis: 'Tableau d\'analyse',
      monitor: 'Tableau de surveillance'
    },
    components: {
      title: 'Composants',
      icons: 'Icônes'
    },
    about: {
      title: 'À propos de nous',
      privacy: 'Politique de confidentialité',
      terms: 'Conditions de service',
      index: 'À propos de Hbt'
    },
    core: {
      _self: 'Gestion centrale',
      config: 'Configuration système',
      language: 'Gestion des langues',
      dict: 'Gestion des dictionnaires',
    },
    identity: {
      _self: 'Authentification',
      user: 'Gestion des utilisateurs',
      role: 'Gestion des rôles',
      dept: 'Gestion des départements',
      post: 'Gestion des postes',
      menu: 'Gestion des menus',
      tenant: 'Gestion des locataires',
      oauth: 'Gestion OAuth',
      profile: 'Informations personnelles',
      changePassword: 'Modifier le mot de passe'
    },
    audit: {
      _self: 'Journaux d\'audit',
      operlog: 'Journaux d\'opération',
      loginlog: 'Journaux de connexion',
      sqldifflog: 'Journaux de différence SQL',
      exceptionlog: 'Journaux d\'exception',
      auditlog: 'Journaux d\'audit',
      quartzlog: 'Journaux de tâches',
      server: 'Surveillance des services'
    },
    workflow: {
      _self: 'Workflow',
      overview: 'Vue d\'ensemble des processus',
      my: 'Mes processus',
      form: 'Gestion des formulaires',
      definition: 'Définition des processus',
      instance: 'Instances de processus',
      task: 'Tâches de travail',
      node: 'Nœuds de processus',
      variable: 'Variables de processus',
      history: 'Historique des processus'
    },
    signalr: {
      _self: 'Communication en temps réel',
      online: 'Utilisateurs en ligne',
      message: 'Messages en ligne'
    },
    generator: {
      _self: 'Génération de code',
      table: 'Tables de base de données',
      tableDefine: 'Définition des colonnes de table',
      template: 'Modèles de code',
      config: 'Configuration de génération',
      api: 'Documentation API'
    },
    routine: {
      _self: 'Bureau quotidien',
      schedule: {
        _self: 'Gestion des horaires',
        myschedule: 'Mon horaire',
        dashboard: 'Tableau de bord des horaires',
      },
      car: {
        _self: 'Gestion des véhicules',
        info: 'Informations sur les véhicules',
        application: 'Demande de véhicule',
        dashboard: 'Tableau de bord des véhicules',
        maintenance: 'Entretien des véhicules',
      },
      email: {
        _self: 'Gestion des e-mails',
        inbox: 'Boîte de réception',
        drafts: 'Brouillons',
        sent: 'Envoyés',
        trash: 'Corbeille',
        template: 'Modèles d\'e-mail',        
      },
      meeting: {
        _self: 'Gestion des réunions',
        room: 'Salles de réunion',
        mymeeting: 'Mes réunions',
        booking: 'Réservation de réunion',
        dashboard: 'Tableau de bord des réunions',
      },
      notice: { 
        _self: 'Notifications et annonces',
        message: {
          _self: 'Gestion des messages',
          mymessages: 'Mes messages',
          list: 'Tableau de bord des messages',
        },
        announcement: {
          _self: 'Gestion des annonces',
          signoff: 'Réception des annonces',
          list: 'Liste des annonces',
        },
        notification: {
          _self: 'Gestion des notifications',
          ack: 'Notifications lues',
          list: 'Liste des notifications',
        },
      },
      hr: {
        _self: 'Ressources humaines et présence',
        recruitment: {
          _self: 'Gestion du recrutement',
          apply: 'Demande de recrutement',
          approval: 'Approbation du recrutement',
          list: 'Liste de recrutement',

        },
        transfer: {
          _self: 'Gestion des transferts',
          apply: 'Demande de transfert',
          approval: 'Approbation du transfert',
          list: 'Liste des transferts',
        },
        leave: {
          _self: 'Gestion des congés',
          apply: 'Demande de congé',
          approval: 'Approbation des congés',
          list: 'Liste des congés',
        },
        trip: {
          _self: 'Gestion des voyages d\'affaires',
          apply: 'Demande de voyage d\'affaires',
          approval: 'Approbation des voyages d\'affaires',
          list: 'Liste des voyages d\'affaires',
        },
        overtime: {
          _self: 'Gestion des heures supplémentaires',
          apply: 'Demande d\'heures supplémentaires',
          approval: 'Approbation des heures supplémentaires',
          list: 'Liste des heures supplémentaires',
      },
    },
    expense:{
      _self: 'Gestion des dépenses',
      daily: {
        _self: 'Dépenses quotidiennes',
        apply: 'Demande de dépenses',
        approve: 'Approbation des dépenses',
        list: 'Liste des dépenses',
      },
      travel: {
        _self: 'Dépenses de voyage',
        apply: 'Demande de frais de voyage',
        approve: 'Approbation des frais de voyage',
        list: 'Liste des frais de voyage',
      },
    },
    file:{
      _self: 'Gestion des fichiers',
      daily: {
        _self: 'Fichiers quotidiens',
        list: 'Liste des fichiers',
      },
      iso: {
        _self: 'Fichiers ISO',
        version: 'Version',
        signoff: 'Réception',
        list: 'Fichiers ISO',
      },
      document: { 
        _self: 'Gestion des documents officiels',
        version: 'Version',
        signoff: 'Réception',
        list: 'Liste des documents officiels',
      },
    },
    officesupplies:{
      _self: 'Fournitures de bureau',
      inventory:{
        _self: 'Gestion des stocks',
        requisition: 'Gestion des achats',
        inbound: 'Gestion des entrées',
        stocktaking: 'Inventaire',
      },
      usage:{
        _self: 'Gestion de l\'utilisation',
        apply: 'Demande d\'utilisation',
        approve: 'Approbation de l\'utilisation',
        receive: 'Enregistrement de l\'utilisation',
      }
    },
    book:{
      _self: 'Gestion des livres',
      inventory:{
        _self: 'Gestion des stocks',
        requisition: 'Gestion des achats',
        inbound: 'Gestion des entrées',
        list: 'Liste des livres',
        stocktaking: 'Inventaire',
      },
      usage:{
        _self: 'Gestion de l\'utilisation',
        card: 'Carte d\'emprunt',
        borrow: 'Emprunt',
        return: 'Retour',
      }

    },
    medical:{
      _self: 'Gestion médicale',
      medicine:{
        _self: 'Gestion des stocks',
        requisition: 'Gestion des achats',
        inbound: 'Gestion des entrées',
        list: 'Liste des médicaments',
        stocktaking: 'Inventaire',
      },
      usage:{
        _self: 'Gestion de l\'utilisation',
        archive: 'Archives',
        receive: 'Réception de médicaments',
        cost: 'Coûts',
      }

    },
  },
  accounting: {
      _self: 'Comptabilité',
      financial: {
        _self: 'Comptabilité de gestion',
        company: "Informations de l'entreprise",
        account: 'Comptes comptables',
        companyaccount: 'Comptes de l\'entreprise',
        ledger: 'Grand livre',
        payable: 'Comptes fournisseurs',
        receivable: 'Comptes clients',
        fixedasset: 'Immobilisations',
        bank: 'Informations bancaires',

      },
      controlling: {
        _self: 'Comptabilité de contrôle',
        costelement: 'Éléments de coût',
        costcenter: 'Centres de coût',
        profitcenter: 'Centres de profit',
        accountsReceivable: 'Comptes clients',
        accountsPayable: 'Comptes fournisseurs',
        assetAccounting: 'Comptabilité des actifs',
        tax: 'Gestion fiscale',
        financialReporting: 'Rapports financiers'      
    },
    budget:{
      _self: 'Budget global',
        formulation: {
          _self: 'Élaboration du budget',
          sales: {
            _self: 'Budget des ventes',
            cost: 'Coût des ventes',
            rolling: 'Ventes glissantes',
          },
          production: {
            _self: 'Budget de production',
            auxiliary: 'Matériaux auxiliaires de production',
            labor: 'Main-d\'œuvre de production',
            manufacturing: 'Fabrication de production',
          },
          cost: {
            _self: 'Budget des coûts',
            directmaterial: 'Matériaux directs',
            directlabor: 'Main-d\'œuvre directe',
            indirectlabor: 'Main-d\'œuvre indirecte',
            manufacturing: 'Frais de fabrication',
          },
          expense: {
            _self: 'Budget des dépenses',
            sales: 'Frais de vente',
            management: 'Frais de gestion',
            financial: 'Frais financiers',
          },
          financial: {
            _self: 'Budget financier',
            cashflow: 'Flux de trésorerie',
            balancesheet: 'Bilan',
            income: 'Compte de résultat',
          },
        },
        control: {
          _self: 'Contrôle budgétaire',
          dashboard: 'Tableau de bord budgétaire',
          approval: 'Approbation budgétaire',
        },   
  },
},
    logistics: {
      _self: 'Gestion logistique',
      equipment: {
        _self: 'Gestion des équipements',
        data: 'Données principales des équipements',
        location: 'Emplacement des équipements',
        material: 'Association des matériaux',
        workorder: 'Ordres de travail'

      },
      material: {
        _self: 'Gestion des matériaux',
        material:{
          _self: 'Gestion des matériaux',
          material: 'Données principales des matériaux',
          plant: 'Informations de l\'usine',
          master: 'Données des matériaux',
          plantmaster: 'Matériaux de l\'usine',
          vendor: 'Informations du vendeur',
          supplier: 'Informations du fournisseur',
        },
        purchase:{
          _self: 'Gestion des achats',
          vendor: 'Informations du vendeur',
          supplier: 'Informations du fournisseur',
          price: 'Prix d\'achat',
          requisition: 'Demande d\'achat',
          order: 'Commandes d\'achat',

        },



      },
      production: {
        _self: 'Gestion de la production',
        bom: 'Liste des matériaux',
        change: {
          _self: 'Changements de conception',
          implementation: 'Mise en œuvre des changements',
          techcontact: 'Contact technique',
          material: 'Confirmation des matériaux',
          query: 'Requête de changement',
          oldproduct: 'Contrôle des anciens produits',
          sop: 'Confirmation SOP',
          batch: 'Lots d\'introduction',
          input: {
            _self: 'Saisie des changements',
            gijutsu: 'Département technique',
            seikan: 'Département de gestion de production',
            koubai: 'Département des achats',
            uketsuke: 'Département de réception',
            bukan: 'Département de gestion',
            seizou2: 'Département de fabrication 2',
            seizou1: 'Département de fabrication 1',
            hinkan: 'Département de contrôle qualité',
            seizougijutsu: 'Département technique de fabrication',
  
          }
        },
        workcenter: 'Centres de travail',
        order: 'Ordres de production',
        kanban: 'Kanban',
        oph:{
          _self: 'Gestion OPH',
          workshop1: {
            _self: 'Atelier 1',
            output: 'Rapport de production quotidien',
            defect: 'Défauts de production',
            worktime: 'Temps de travail de production',
            productionReport: 'Rapport de production',
            defectSummary: 'Résumé des défauts',
            worktimeReport: 'Rapport de temps de travail'
          },
          workshop2: {
            _self: 'Atelier 2',
            output: 'Rapport de production quotidien',
            inspection: 'Enregistrements d\'inspection',
            repair: 'Enregistrements de réparation',
            worktime: 'Temps de travail de production',
            productionReport: 'Rapport de production',
            inspectionReport: 'Rapport d\'inspection',
            repairReport: 'Rapport de réparation',
            worktimeReport: 'Rapport de temps de travail'
          }
        }

      },
      project: {
        _self: 'Gestion de projet',
        define: 'Définition de projet',
        cost: 'Planification des coûts',
        resource: 'Planification des ressources',
        schedule: 'Planification des délais',

      },
      quality: {
        _self: 'Gestion de la qualité',
        item: 'Éléments d\'inspection',
        receiving: 'Inspection des matières premières',
        process: 'Inspection du processus',
        storage: 'Inspection d\'entrée',
        return: 'Inspection de retour',
  
      },
      sales: {
        _self: 'Gestion des ventes',
        customer: 'Informations clients',
        client: 'Informations clients',
        price: 'Prix de vente',
        order: 'Commandes de vente',
      },
      service: {
        _self: 'Service client',
        item: 'Éléments de service',
        contract: 'Contrats de service',
        request: 'Demandes de service',
        workorder: 'Ordres de travail de service',
        timesheet: 'Enregistrements de temps',
        consumption: 'Consommation de matériaux',
        outsourcing: 'Services externalisés'

      },
      complaint: {
        _self: 'Gestion des réclamations',
        notice: 'Avis de qualité',
        mark: 'Détails des réclamations',
        analysis: 'Analyse des causes',
        corrective: 'Mesures correctives',
        return: 'Exécution des retours',
        followUp: 'Suivi du traitement'
      }
    },
    humanResources: {
      _self: 'Gestion des ressources humaines',
      employeeManagement: {
        _self: 'Gestion des employés',
        employeeMaster: 'Données principales des employés',
        attendance: 'Gestion de la présence',
        leave: 'Gestion des congés',
        payroll: 'Gestion de la paie',
        contractManagement: 'Gestion des contrats' // Nouvelle gestion des contrats
      },
      recruitment: {
        _self: 'Gestion du recrutement',
        jobPosting: 'Publication d\'emploi',
        candidateManagement: 'Gestion des candidats',
        interviewScheduling: 'Planification des entretiens',
        offerManagement: 'Gestion des offres'
      },
      training: {
        _self: 'Gestion de la formation',
        trainingPlan: 'Plan de formation',
        trainingExecution: 'Exécution de la formation',
        trainingEvaluation: 'Évaluation de la formation'
      },
      performance: {
        _self: 'Gestion de la performance',
        goalSetting: 'Définition des objectifs',
        performanceReview: 'Évaluation de la performance',
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
