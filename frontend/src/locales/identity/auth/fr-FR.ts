export default {

  identity: {
    auth: {
      login: {
        title: 'Connexion',
        username: 'Nom d\'utilisateur',
        password: 'Mot de passe',
        rememberMe: 'Se souvenir de moi',
        forgotPassword: 'Mot de passe oublié',
        submit: 'Se connecter',
        register: 'S\'inscrire',
        success: 'Connexion réussie',
        error: {
          invalidCredentials: 'Nom d\'utilisateur ou mot de passe incorrect',
          accountLocked: 'Compte verrouillé',
          accountDisabled: 'Compte désactivé',
          accountExpired: 'Compte expiré',
          credentialsExpired: 'Mot de passe expiré',
          invalidCaptcha: 'Captcha invalide',
          invalidTenant: 'Locataire invalide',
          invalidDevice: 'Informations de l\'appareil invalides',
          invalidGrant: 'Autorisation invalide',
          tooManyAttempts: 'Trop de tentatives de connexion, veuillez réessayer plus tard'
        },
        noToken: 'Pas de jeton d\'accès dans la réponse de connexion',
        otherLogin: 'Autres méthodes de connexion',
        form: {
          usernameRequired: 'Veuillez entrer votre nom d\'utilisateur',
          passwordRequired: 'Veuillez entrer votre mot de passe'
        }
      },
      register: {
        title: 'Inscription',
        username: 'Nom d\'utilisateur',
        password: 'Mot de passe',
        confirm: 'Confirmer le mot de passe',
        email: 'E-mail',
        phone: 'Téléphone',
        submit: 'S\'inscrire',
        login: 'Se connecter avec un compte existant',
        success: 'Inscription réussie',
        error: 'Échec de l\'inscription'
      },
      forgot: {
        title: 'Mot de passe oublié',
        email: 'E-mail',
        submit: 'Envoyer',
        back: 'Retour à la connexion',
        success: 'E-mail de réinitialisation du mot de passe envoyé',
        error: 'Échec de la réinitialisation du mot de passe'
      },
      info: {
        loading: 'Chargement des informations utilisateur',
        success: 'Informations utilisateur chargées avec succès'
      },
      autoLogout: 'Vous avez été déconnecté automatiquement en raison de l\'inactivité',
      error: {
        noResponse: 'Le serveur ne répond pas',
        noSaltData: 'Échec de l\'obtention des paramètres de chiffrement',
        invalidSalt: 'Format de paramètre de chiffrement invalide',
        invalidIterations: 'Nombre d\'itérations de chiffrement invalide',
        permanentlyLocked: 'Compte verrouillé définitivement, veuillez contacter l\'administrateur',
        temporarilyLocked: 'Compte temporairement verrouillé, veuillez réessayer dans {minutes} minutes',
        tooManyAttempts: 'Trop de tentatives de connexion échouées, compte verrouillé',
        invalidCredentials: 'Nom d\'utilisateur ou mot de passe incorrect'
      }
    }
  }
} 