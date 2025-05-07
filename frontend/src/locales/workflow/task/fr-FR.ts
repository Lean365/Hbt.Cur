export default {
  workflow: {
    task: {
      title: 'Tâche de Workflow',
      list: {
        title: 'Liste des Tâches de Workflow',
        search: {
          name: 'Nom de la Tâche',
          type: 'Type de Tâche',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin'
        },
        table: {
          name: 'Nom de la Tâche',
          type: 'Type de Tâche',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin',
          duration: 'Durée',
          actions: 'Actions'
        },
        actions: {
          view: 'Voir',
          edit: 'Modifier',
          delete: 'Supprimer',
          refresh: 'Actualiser'
        },
        status: {
          running: 'En Cours',
          completed: 'Terminé',
          terminated: 'Interrompu',
          failed: 'Échoué'
        }
      },
      form: {
        title: {
          create: 'Créer une Tâche de Workflow',
          edit: 'Modifier une Tâche de Workflow'
        },
        fields: {
          name: 'Nom de la Tâche',
          type: 'Type de Tâche',
          description: 'Description',
          input: 'Entrée',
          output: 'Sortie',
          error: 'Erreur'
        },
        rules: {
          name: {
            required: 'Veuillez saisir le nom de la tâche'
          },
          type: {
            required: 'Veuillez sélectionner le type de tâche'
          }
        },
        buttons: {
          submit: 'Envoyer',
          cancel: 'Annuler'
        }
      },
      detail: {
        title: 'Détail de la Tâche de Workflow',
        basic: {
          title: 'Informations de Base',
          name: 'Nom de la Tâche',
          type: 'Type de Tâche',
          description: 'Description',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin',
          duration: 'Durée'
        },
        input: {
          title: 'Informations d\'Entrée',
          value: 'Valeur d\'Entrée'
        },
        output: {
          title: 'Informations de Sortie',
          value: 'Valeur de Sortie'
        },
        error: {
          title: 'Informations d\'Erreur',
          message: 'Message d\'Erreur',
          stackTrace: 'Trace de Pile'
        },
        actions: {
          back: 'Retour'
        }
      }
    }
  }
} 