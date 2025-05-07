export default {
  workflow: {
    variable: {
      title: 'Variable de Workflow',
      list: {
        title: 'Liste des Variables de Workflow',
        search: {
          name: 'Nom de la Variable',
          type: 'Type de Variable',
          scope: 'Portée',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin'
        },
        table: {
          name: 'Nom de la Variable',
          type: 'Type de Variable',
          scope: 'Portée',
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
          create: 'Créer une Variable de Workflow',
          edit: 'Modifier une Variable de Workflow'
        },
        fields: {
          name: 'Nom de la Variable',
          type: 'Type de Variable',
          scope: 'Portée',
          description: 'Description',
          input: 'Entrée',
          output: 'Sortie',
          error: 'Erreur'
        },
        rules: {
          name: {
            required: 'Veuillez saisir le nom de la variable'
          },
          type: {
            required: 'Veuillez sélectionner le type de variable'
          },
          scope: {
            required: 'Veuillez sélectionner la portée de la variable'
          }
        },
        buttons: {
          submit: 'Envoyer',
          cancel: 'Annuler'
        }
      },
      detail: {
        title: 'Détail de la Variable de Workflow',
        basic: {
          title: 'Informations de Base',
          name: 'Nom de la Variable',
          type: 'Type de Variable',
          scope: 'Portée',
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