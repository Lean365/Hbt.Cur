import { countReset } from "node:console";

export default {
  menu: {
    home: 'Accueil',
    dashboard: {
      title: 'Tableau de bord',
      workplace: 'Espace de travail',
      analysis: 'Analyse',
      monitor: 'Moniteur'
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

    identity: {
      _self: 'Authentification d\'identité',
      user: 'Gestion des utilisateurs',
      role: 'Gestion des rôles',
      dept: 'Gestion des départements',
      post: 'Gestion des postes',
      menu: 'Gestion des menus',
      tenant: 'Gestion des locataires',
      oauth: 'Gestion OAuth',
      profile: 'Informations personnelles',
      changePassword: 'Changer le mot de passe'
    },
    audit: {
      _self: 'Journaux d\'audit',
      operlog: 'Journal des opérations',
      loginlog: 'Journal de connexion',
      sqldifflog: 'Journal des différences SQL',
      exceptionlog: 'Journal des exceptions',
      auditlog: 'Journal d\'audit',
      quartzlog: 'Journal des tâches',
      server: 'Moniteur de serveur'
    },
    workflow: {
      _self: 'Workflow',
      engine:{
        _self: 'Moteur de processus',
        monitor: 'Moniteur de processus',
        todo: 'Tâches à faire',
        done: 'Tâches terminées',
        signoff: 'Validation de processus',
        execution: 'Exécution de processus',
        designer: 'Concepteur de processus'
      },
      manage:{
        _self: 'Gestion des processus',
        form: 'Gestion des formulaires',
        scheme: 'Schéma de processus',
        instance: 'Instance de processus',
        oper: 'Opérations d\'instance',
        trans: 'Flux d\'instance'
      }
    },
    signalr: {
      _self: 'Communication en temps réel',
      online: 'Utilisateurs en ligne',
      message: 'Messages en ligne'
    },
    generator: {
      _self: 'Générateur de code',
      table: 'Tables de base de données',
      tableDefine: 'Définition des colonnes de table',
      template: 'Modèles de code',
      config: 'Configuration de génération',
      api: 'Documentation API'
    },
    routine: {
      _self: 'Bureau quotidien',
      core: {
        _self: 'Services de base',
        numberrule: 'Règles de numérotation',
        config: 'Configuration système',
        language: 'Gestion des langues',
        dict: 'Gestion des dictionnaires'
      },
      contract: {
        _self: 'Gestion des contrats',
        template: {
          _self: 'Modèles de contrats',
          manage: 'Gestion des modèles',
          category: 'Catégories de modèles'
        },
        draft: {
          _self: 'Rédaction de contrats',
          apply: 'Demande de rédaction',
          my: 'Mes rédactions'
        },
        approval: {
          _self: 'Approbation de contrats',
          pending: 'Approbation de contrats',
          approved: 'Approuvé',
          record: 'Enregistrements d\'approbation'
        },
        execution: {
          _self: 'Exécution de contrats',
          track: 'Suivi d\'exécution',
          change: 'Gestion des changements',
          payment: 'Gestion des paiements'
        },
        archive: {
          _self: 'Archivage de contrats',
          manage: 'Gestion des archives',
          query: 'Statistiques de requête'
        }
      },
      project: {
        _self: 'Gestion de projets',
        info: {
          _self: 'Informations sur le projet',
          list: 'Liste des projets'
        },
        plan: {
          _self: 'Planification de projet',
          request: 'Demande de plan',
          gantt: 'Diagramme de Gantt du projet'
        },
        task: {
          _self: 'Tâches de projet',
          assign: 'Attribution de tâches',
          track: 'Suivi des tâches',
          board: 'Tableau de tâches'
        },
        resource: {
          _self: 'Ressources de projet',
          personnel: 'Gestion du personnel',
          equipment: 'Gestion des équipements',
          budget: 'Gestion du budget'
        },
        monitor: {
          _self: 'Surveillance de projet',
          progress: 'Surveillance des progrès',
          quality: 'Surveillance de la qualité',
          risk: 'Surveillance des risques'
        }
      },
      quartz: {
        _self: 'Planification des tâches',
        job: {
          _self: 'Gestion des tâches',
          config: 'Configuration des tâches',
          list: 'Liste des tâches',
          status: 'Statut des tâches'
        },
        schedule: {
          _self: 'Planification des tâches',
          config: 'Configuration de planification',
          monitor: 'Moniteur de planification',
          stats: 'Statistiques de planification'
        }
      },
      schedule: {
        _self: 'Gestion des horaires',
        myschedule: 'Mon horaire',
        dashboard: 'Tableau de bord des horaires'
      },
      vehicle: {
        _self: 'Gestion des véhicules',
        my: 'Mes véhicules',
        application: 'Demande de véhicule',
        dashboard: 'Tableau de bord des véhicules',
        manage: {
          _self: 'Gestion des véhicules',
          info: 'Informations sur les véhicules',
          maintenance: 'Maintenance des véhicules'
        }
      },
      email: {
        _self: 'Gestion des e-mails',
        inbox: 'Boîte de réception',
        drafts: 'Brouillons',
        sent: 'Envoyés',
        trash: 'Corbeille',
        template: 'Modèles d\'e-mail'
      },
      meeting: {
        _self: 'Gestion des réunions',
        room: 'Salles de réunion',
        mymeeting: 'Mes réunions',
        booking: 'Réservation de réunions',
        dashboard: 'Tableau de bord des réunions'
      },
      notice: {
        _self: 'Notifications et annonces',
        message: {
          _self: 'Gestion des messages',
          mymessages: 'Mes messages',
          list: 'Tableau de messages'
        },
        announcement: {
          _self: 'Gestion des annonces',
          signoff: 'Signer les annonces',
          list: 'Liste des annonces'
        },
        notification: {
          _self: 'Gestion des notifications',
          ack: 'Notifications lues',
          list: 'Liste des notifications'
        }
      },
      hr: {
        _self: 'RH et présence',
        recruitment: {
          _self: 'Gestion du recrutement',
          apply: 'Demande de recrutement',
          approval: 'Approbation du recrutement',
          list: 'Liste de recrutement'
        },
        transfer: {
          _self: 'Gestion des transferts',
          apply: 'Demande de transfert',
          approval: 'Approbation du transfert',
          list: 'Liste des transferts'
        },
        leave: {
          _self: 'Gestion des congés',
          apply: 'Demande de congé',
          approval: 'Approbation des congés',
          list: 'Liste des congés'
        },
        trip: {
          _self: 'Gestion des voyages d\'affaires',
          apply: 'Demande de voyage',
          approval: 'Approbation du voyage',
          list: 'Liste des voyages'
        },
        overtime: {
          _self: 'Gestion des heures supplémentaires',
          apply: 'Demande d\'heures supplémentaires',
          approval: 'Approbation des heures supplémentaires',
          list: 'Liste des heures supplémentaires'
        }
      },
      expense: {
        _self: 'Gestion des dépenses',
        daily: {
          _self: 'Dépenses quotidiennes',
          apply: 'Demande de dépenses',
          approve: 'Approbation des dépenses',
          list: 'Liste des dépenses'
        },
        travel: {
          _self: 'Dépenses de voyage',
          apply: 'Demande de dépenses de voyage',
          approve: 'Approbation des dépenses de voyage',
          list: 'Liste des dépenses de voyage'
        }
      },
      document: {
        _self: 'Gestion des documents',
        news: {
          _self: 'Gestion des actualités',
        },
        regulation: {
          _self: 'Règlements et règles',
          manage: 'Gestion des règlements',
          control: 'Contrôle des règlements',
        },
        file: {
          _self: 'Fichiers quotidiens',
        },
        iso: {
          _self: 'Fichiers ISO',
          manage: 'Gestion des fichiers',
          control: 'Contrôle des fichiers',
        },
        official: {
          _self: 'Gestion des documents officiels',
          manage: 'Gestion des documents',
          issuance: 'Contrôle des documents',
        },
        law: {
          _self: 'Lois et règlements',
        }
      },
      officesupplies: {
        _self: 'Fournitures de bureau',
        inventory: {
          _self: 'Gestion des stocks',
          requisition: 'Gestion des achats',
          inbound: 'Gestion des entrées',
          stocktaking: 'Gestion de l\'inventaire'
        },
        usage: {
          _self: 'Gestion de l\'utilisation',
          apply: 'Demande d\'utilisation',
          approve: 'Approbation de l\'utilisation',
          list: 'Enregistrements d\'utilisation'
        }
      },
      book: {
        _self: 'Gestion des livres',
        inventory: {
          _self: 'Gestion des stocks',
          requisition: 'Gestion des achats',
          inbound: 'Gestion des entrées',
          list: 'Liste des livres',
          stocktaking: 'Gestion de l\'inventaire'
        },
        usage: {
          _self: 'Gestion de l\'utilisation',
          card: 'Carte de bibliothèque',
          borrow: 'Emprunter',
          return: 'Retourner'
        }
      },
      medical: {
        _self: 'Gestion médicale',
        medicine: {
          _self: 'Gestion des stocks',
          requisition: 'Gestion des achats',
          inbound: 'Gestion des entrées',
          list: 'Liste des médicaments',
          stocktaking: 'Gestion de l\'inventaire'
        },
        usage: {
          _self: 'Gestion de l\'utilisation',
          archive: 'Archive',
          receive: 'Recevoir des médicaments',
          cost: 'Coût'
        }
      }
    },
    accounting: {
      _self: 'Comptabilité',
      financial: {
        _self: 'Comptabilité de gestion',
        company: 'Informations sur l\'entreprise',
        account: 'Plan comptable',
        companyaccount: 'Comptes de l\'entreprise',
        ledger: 'Grand livre',
        payable: 'Comptes fournisseurs',
        receivable: 'Comptes clients',
        fixedasset: 'Immobilisations',
        bank: 'Informations bancaires'
      },
      controlling: {
        _self: 'Contrôle de gestion',
        costelement: 'Éléments de coût',
        costcenter: 'Centres de coût',
        profitcenter: 'Centres de profit',
        accountsReceivable: 'Comptes clients',
        accountsPayable: 'Comptes fournisseurs',
        assetAccounting: 'Comptabilité des actifs',
        tax: 'Gestion fiscale',
        financialReporting: 'Rapports financiers'
      },
      budget: {
        _self: 'Budget global',
        formulation: {
          _self: 'Formulation du budget',
          sales: {
            _self: 'Budget des ventes',
            cost: 'Coût des ventes',
            rolling: 'Ventes glissantes'
          },
          production: {
            _self: 'Budget de production',
            auxiliary: 'Auxiliaires de production',
            labor: 'Main-d\'œuvre de production',
            manufacturing: 'Fabrication de production'
          },
          cost: {
            _self: 'Budget des coûts',
            directmaterial: 'Matériaux directs',
            directlabor: 'Main-d\'œuvre directe',
            indirectlabor: 'Main-d\'œuvre indirecte',
            manufacturing: 'Frais généraux de fabrication'
          },
          expense: {
            _self: 'Budget des dépenses',
            sales: 'Dépenses de vente',
            manage: 'Dépenses de gestion',
            financial: 'Dépenses financières'
          },
          financial: {
            _self: 'Budget financier',
            cashflow: 'Flux de trésorerie',
            balancesheet: 'Bilan',
            income: 'Compte de résultat'
          }
        },
        control: {
          _self: 'Contrôle budgétaire',
          dashboard: 'Tableau de bord budgétaire',
          approval: 'Approbation budgétaire'
        }
      }
    },
    logistics: {
      _self: 'Gestion logistique',
      equipment: {
        _self: 'Gestion des équipements',
        master: {
          _self: 'Données d\'équipement',
          list: 'Informations sur les équipements',
          location: 'Emplacement fonctionnel',
          material: 'Association de matériaux'
        },
        maintenance: {
          _self: 'Maintenance des équipements',
          workorder: 'Plans de maintenance',
          assign: 'Attribution de maintenance',
          execute: 'Exécution de maintenance'
        }
      },
      material: {
        _self: 'Gestion des matériaux',
        manage: {
          _self: 'Informations sur les matériaux',
          master: 'Matériaux de groupe',
          plant: {
            _self: 'Informations sur l\'usine',
            master: 'Matériaux d\'usine'
          }
        },
        purchase: {
          _self: 'Gestion des achats',
          vendor: 'Informations sur les vendeurs',
          supplier: 'Informations sur les fournisseurs',
          price: 'Prix d\'achat',
          requisition: 'Demande d\'achat',
          order: 'Commandes d\'achat'
        },
        sample:{
          _self: 'Gestion des échantillons',
          component: 'Échantillons de composants',
          product: 'Échantillons de produits'
        },
        drawing: {
          _self: 'Gestion des dessins',
          design: 'Gestion des dessins',
          engineering: 'Contrôle des dessins',
          gerber: 'Fichiers Gerber',
          coordinate: 'Fichiers de coordonnées',
          assembly: 'Dessins d\'assemblage',
          structure: 'Fichiers de structure',
          impedance: 'Fichiers d\'impédance',
          process: 'Flux de processus'
        },
        csm: {  
          _self: 'Gestion des articles fournis par le client',
          raw: 'Matériaux fournis par le client',
          good: 'Produits fournis par le client'
        }
      },
      production: {
        _self: 'Gestion de la production',
        basic: {
          _self: 'Données de base',
          bom: 'Nomenclature',
          workcenter: 'Centres de travail',   
          routing: 'Itinéraires de processus',
          order: 'Ordres de production',
          worktime: 'Heures de production',
          kanban: 'Kanban'
        },
        change: {
          _self: 'Changements de conception',
          implementation: 'Mise en œuvre des changements',
          techcontact: 'Contact technique',
          material: 'Confirmation des matériaux',
          query: 'Requête de changement',
          oldproduct: 'Contrôle des anciens produits',
          sop: 'Confirmation SOP',
          batch: 'Lot d\'entrée',
          input: {
            _self: 'Saisie de changement',
            gijutsu: 'Département technique',
            seikan: 'Département de contrôle de production',
            koubai: 'Département des achats',
            uketsuke: 'Département d\'inspection',
            bukan: 'Gestion de département',
            seizou2: 'Département de production 2',
            seizou1: 'Département de production 1',
            hinkan: 'Département de contrôle qualité',
            seizougijutsu: 'Département de technologie de production'
          }
        },       
        output: {
          _self: 'Gestion de la fabrication',
          workshop1:{
            _self: 'Département de production 1',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: 'Production',
              modify: 'Modification',
              rework: 'Retouche'
            },
            defect:{
              _self: 'Défauts',
              epp: 'EPP',
              production: 'Production',
              modify: 'Modification',
              rework: 'Retouche'
            },
            worktime: {
              _self: 'Heures de travail',
              epp: 'EPP',
              production: 'Production',
              modify: 'Modification',
              rework: 'Retouche'
            }
          },
          workshop2:{
            _self: 'Département de production 2',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: 'Production',
              modify: 'Modification',
              rework: 'Retouche'
            },
            defect:{
              _self: 'Défauts',
              eppInspection: 'Inspection EPP',
              eppRepair: 'Réparation EPP',
              productionInspection: 'Inspection de production',
              productionRepair: 'Réparation de production',
              modifyInspection: 'Inspection de modification',
              modifyRepair: 'Réparation de modification',
              reworkInspection: 'Inspection de retouche',
              reworkRepair: 'Réparation de retouche'
            },
            worktime: {
              _self: 'Heures de travail',
              epp: 'EPP',
              production: 'Production',
              modify: 'Modification',
              rework: 'Retouche'
            }
          }
        },
        sop: {
          _self: 'Gestion SOP',
          workshop1: 'Département de production 1',
          workshop2: 'Département de production 2'
        },
        techcontact: {
          _self: 'Contact technique',
          epp: 'Contact EPP',
          engineering: 'Contact d\'ingénierie',
          external: 'Contact externe'
        }
      },
      project: {
        _self: 'Gestion de projets',
        define: 'Définition de projet',
        cost: 'Planification des coûts',
        resource: 'Planification des ressources',
        schedule: 'Planification des horaires'
      },
      quality: {
        _self: 'Gestion de la qualité',
        basic: {
          _self: 'Données de base',
          item: 'Éléments d\'inspection',
          method: 'Méthodes d\'inspection',
          sampling: 'Plans d\'échantillonnage',
          defect: 'Catégories de défauts',
          rule: 'Règles de jugement',
          category: 'Catégories de qualité'
        },
        inspection:{
          _self: 'Gestion de l\'inspection',
          receiving: 'Inspection de réception',
          process: 'Inspection de processus',
          storage: 'Inspection de stockage',
          return: 'Inspection de retour'
        },
        trace:{
          _self: 'Gestion de la traçabilité',
          batch: 'Traçabilité des lots',
          corrective: 'Actions correctives',
          notification: 'Notifications',
        },
        cost:{
          _self: 'Coûts de qualité',
          business:'Activités de qualité',
          rework:'Activités de retouche',
          scrap:'Activités de rebut',
        },
        plan: {
          _self: 'Planification de la qualité',
          supplier: 'Évaluation des fournisseurs',
          customer: 'Enquête client'
        },
        item: 'Éléments d\'inspection',
        receiving: 'Inspection de réception',
        process: 'Inspection de processus',
        storage: 'Inspection de stockage',
        return: 'Inspection de retour'
      },
      sales: {
        _self: 'Gestion des ventes',
        customer: 'Informations client',
        client: 'Informations client',
        price: 'Prix de vente',
        order: 'Commandes de vente'
      },
      service: {
        _self: 'Service client',
        cs: {
          _self: 'Service client',
          item: 'Éléments de service',
          contract: 'Contrats de service',
          request: 'Demandes de service',
          workorder: 'Ordres de travail de service',
          timesheet: 'Heures de service',
          consumption: 'Consommation de matériaux',
          outsourcing: 'Services externalisés'
        },
        cc: {
          _self: 'Gestion des réclamations clients',
          notice: 'Notifications de qualité',
          mark: 'Détails des réclamations',
          analysis: 'Analyse des causes',
          corrective: 'Actions correctives',
          return: 'Exécution des retours/échanges',
          followUp: 'Traitement de suivi'
        }
      }
    },
    hrm: {
      _self: 'Ressources humaines',
      attendance: {
        _self: 'Gestion de la présence',
        record: 'Enregistrements de présence',
        holiday: 'Gestion des congés',
        overtime: 'Gestion des heures supplémentaires',
        compensatory: 'Gestion du temps compensatoire'
      },
      benefit: {
        _self: 'Gestion des avantages',
        project: 'Projets d\'avantages',
        employee: 'Avantages des employés'
      },
      employee: {
        _self: 'Gestion du personnel',
        info: 'Informations sur le personnel',
        contracttype: 'Types de contrats',
        contract: 'Gestion des contrats',
        promotion: 'Gestion des promotions',
        promotionhistory: 'Historique des promotions',
        resignation: 'Gestion des démissions',
        transfer: 'Liste du personnel',
        transferhistory: 'Historique des transferts'
      },
      leave: {
        _self: 'Gestion des congés',
        type: 'Types de congés',
        employee: 'Congés des employés'
      },
      organization: {
        _self: 'Gestion organisationnelle',
        positioncategory: 'Catégories de postes',
        company: 'Informations sur l\'entreprise',
        department: 'Informations sur les départements',
        position: 'Informations sur les postes'
      },
      performance: {
        _self: 'Gestion des performances',
        assessmentitem: 'Éléments d\'évaluation',
        assessment: 'Évaluation des performances'
      },
      recruitment: {
        _self: 'Gestion du recrutement',
        application: 'Candidatures',
        posting: 'Offres d\'emploi',
        candidate: 'Gestion des candidats',
        interview: 'Gestion des entretiens'
      },
      salary: {
        _self: 'Gestion des salaires',
        employee: 'Salaires des employés',
        housing: 'Fonds de logement',
        housinglevel: 'Sécurité sociale',
        tax: 'Gestion fiscale',
        taxlevel: 'Niveaux d\'imposition',
        structure: 'Structure des salaires',
        social: 'Sécurité sociale',
        socialbase: 'Base de sécurité sociale'
      },
      training: {
        _self: 'Gestion de la formation',
        category: 'Catégories de formation',
        course: 'Cours de formation',
        record: 'Formation des employés'
      },
      report: {
        _self: 'Gestion des rapports',
        employeeinfo: 'Informations sur le personnel',
        resignation: 'Rapports de démission',
        transfer: 'Rapports de transfert',
        promotion: 'Rapports de promotion',
        training: 'Rapports de formation',
        salary: 'Rapports de salaire',
        performance: 'Rapports de performance',
        attendance: 'Rapports de présence',
        benefit: 'Rapports d\'avantages',
        recruitment: 'Rapports de recrutement'
      }
    }
  }
}
