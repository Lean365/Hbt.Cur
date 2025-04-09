export default {
  realtime: {
    server: {
      title: 'Surveillance du Serveur',
      refresh: 'Actualiser',
      refreshResult: {
        success: 'Données actualisées avec succès',
        failed: 'Échec de l\'actualisation des données'
      },
      resource: {
        title: 'Utilisation des Ressources',
        cpu: 'Utilisation CPU',
        memory: 'Utilisation Mémoire',
        disk: 'Utilisation Disque'
      },
      system: {
        title: 'Informations Système',
        os: 'Système d\'exploitation',
        architecture: 'Architecture',
        version: 'Version',
        processor: {
          name: 'Processeur',
          count: 'Cœurs',
          unit: 'cœurs'
        },
        startup: {
          time: 'Heure de démarrage',
          uptime: 'Temps de fonctionnement',
          day: 'jours',
          hour: 'heures'
        }
      },
      dotnet: {
        title: 'Informations .NET Runtime',
        runtime: {
          version: 'Version .NET Runtime',
          directory: 'Répertoire Runtime'
        },
        clr: {
          version: 'Version CLR'
        }
      },
      network: {
        title: 'Informations Réseau',
        adapter: 'Adaptateur',
        mac: 'Adresse MAC',
        ip: {
          address: 'Adresse IP',
          location: 'Localisation',
          unknown: 'Localisation inconnue'
        },
        rate: {
          send: 'Débit d\'envoi',
          receive: 'Débit de réception'
        }
      }
    }
  }
}