export default {
  workflow: {
    node: {
      title: 'Nœud de Workflow',
      list: {
        title: 'Liste des Nœuds de Workflow',
        search: {
          name: 'Nom du Nœud',
          type: 'Type de Nœud',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin'
        },
        table: {
          name: 'Nom du Nœud',
          type: 'Type de Nœud',
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
          create: 'Créer un Nœud de Workflow',
          edit: 'Modifier un Nœud de Workflow'
        },
        fields: {
          name: 'Nom du Nœud',
          type: 'Type de Nœud',
          description: 'Description',
          input: 'Entrée',
          output: 'Sortie',
          error: 'Erreur'
        },
        rules: {
          name: {
            required: 'Veuillez saisir le nom du nœud'
          },
          type: {
            required: 'Veuillez sélectionner le type de nœud'
          }
        },
        buttons: {
          submit: 'Envoyer',
          cancel: 'Annuler'
        }
      },
      detail: {
        title: 'Détail du Nœud de Workflow',
        basic: {
          title: 'Informations de Base',
          name: 'Nom du Nœud',
          type: 'Type de Nœud',
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