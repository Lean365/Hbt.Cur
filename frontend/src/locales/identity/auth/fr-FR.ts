export default {

  identity: {
    auth: {
      login: {
        title: 'Connexion',
        tenantId: 'Configuration du locataire',
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
          passwordRequired: 'Veuillez entrer votre mot de passe',
          tenantIdRequired: 'Veuillez sélectionner la configuration du locataire'
        }
      },
      register: {
        title: 'Inscription Utilisateur',
        subtitle: 'Veuillez compléter l\'inscription utilisateur étape par étape',
        step1: 'Vérification Captcha',
        step2: 'Informations de Base',
        step3: 'Autres Informations',
        step4: 'Configuration des Permissions',
        username: 'Nom d\'utilisateur',
        usernamePlaceholder: 'Entrez votre nom d\'utilisateur',
        password: 'Mot de passe',
        passwordPlaceholder: 'Entrez votre mot de passe',
        confirmPassword: 'Confirmer le mot de passe',
        confirmPasswordPlaceholder: 'Confirmez votre mot de passe',
        email: 'E-mail',
        emailPlaceholder: 'Entrez votre e-mail',
        nickName: 'Pseudo',
        nickNamePlaceholder: 'Entrez votre pseudo',
        realName: 'Nom réel',
        realNamePlaceholder: 'Entrez votre nom réel',
        fullName: 'Nom complet',
        fullNamePlaceholder: 'Entrez votre nom complet',
        englishName: 'Nom en anglais',
        englishNamePlaceholder: 'Entrez votre nom en anglais',
        phoneNumber: 'Numéro de téléphone',
        phoneNumberPlaceholder: 'Entrez votre numéro de téléphone',
        gender: 'Genre',
        genderUnknown: 'Inconnu',
        genderMale: 'Masculin',
        genderFemale: 'Féminin',
        userType: 'Type d\'utilisateur',
        userTypePlaceholder: 'Sélectionnez le type d\'utilisateur',
        userTypeNormal: 'Utilisateur normal',
        userTypeAdmin: 'Administrateur',
        status: 'Statut',
        statusPlaceholder: 'Sélectionnez le statut',
        statusNormal: 'Normal',
        statusDisabled: 'Désactivé',
        deptId: 'ID du département',
        deptIdPlaceholder: 'Entrez l\'ID du département',
        roleIds: 'Rôles',
        roleIdsPlaceholder: 'Sélectionnez les rôles',
        roleUser: 'Utilisateur',
        roleAdmin: 'Administrateur',
        postIds: 'Postes',
        postIdsPlaceholder: 'Sélectionnez les postes',
        postEmployee: 'Employé',
        postManager: 'Manager',
        deptIds: 'Départements',
        deptIdsPlaceholder: 'Sélectionnez les départements',
        deptIT: 'Département IT',
        deptHR: 'Département RH',
        remark: 'Remarque',
        remarkPlaceholder: 'Entrez une remarque',
        agreement: 'J\'ai lu et j\'accepte',
        agreementPrefix: 'J\'ai lu et j\'accepte',
        agreementLink: 'Accord Utilisateur',
        agreementSuffix: '',
        agreementTitle: 'Accord d\'Inscription Utilisateur',
        agreementContent: 'Veuillez lire et accepter cet accord avant de vous inscrire.',
        captcha: 'Captcha',
        submit: 'Terminer l\'inscription',
        nextStep: 'Suivant',
        back: 'Précédent',
        backToLogin: 'Retour à la connexion',
        login: 'Se connecter avec un compte existant',
        success: 'Inscription réussie',
        successTitle: 'Inscription réussie',
        successSubtitle: 'Votre compte a été créé avec succès, connectez-vous avec votre nouveau compte',
        successMessage: 'L\'utilisateur {userName} a été inscrit avec succès',
        step1Success: 'Vérification captcha terminée',
        step2Success: 'Informations de base terminées',
        step3Success: 'Autres informations terminées',
        step4Success: 'Configuration des permissions terminée',
        error: {
          step1Failed: 'Échec de la vérification captcha',
          step2Failed: 'Échec de la validation des informations de base',
          step3Failed: 'Échec de la validation des autres informations',
          step4Failed: 'Échec de la configuration des permissions',
          unknown: 'Échec de l\'inscription, veuillez réessayer plus tard'
        },
        form: {
          usernameRequired: 'Entrez le nom d\'utilisateur',
          usernameLength: 'Le nom d\'utilisateur doit contenir entre 3-20 caractères',
          usernameFormat: 'Le nom d\'utilisateur ne peut contenir que des lettres, des chiffres et des underscores',
          emailRequired: 'Entrez l\'e-mail',
          emailFormat: 'Entrez un format d\'e-mail valide',
          captchaRequired: 'Entrez le captcha',
          nickNameRequired: 'Entrez le pseudo',
          nickNameLength: 'Le pseudo doit contenir entre 2-20 caractères',
          realNameRequired: 'Entrez le nom réel',
          realNameLength: 'Le nom réel doit contenir entre 2-20 caractères',
          fullNameRequired: 'Entrez le nom complet',
          fullNameLength: 'Le nom complet doit contenir entre 2-50 caractères',
          englishNameLength: 'Le nom en anglais doit contenir entre 2-50 caractères',
          englishNameFormat: 'Le nom en anglais ne peut contenir que des lettres et des espaces',
          phoneNumberFormat: 'Entrez un format de numéro de téléphone valide',
          passwordRequired: 'Entrez le mot de passe',
          passwordLength: 'Le mot de passe doit contenir entre 6-20 caractères',
          passwordFormat: 'Le mot de passe doit contenir des majuscules, des minuscules et des chiffres',
          confirmPasswordRequired: 'Confirmez le mot de passe',
          passwordMismatch: 'Les mots de passe ne correspondent pas',
          userTypeRequired: 'Sélectionnez le type d\'utilisateur',
          statusRequired: 'Sélectionnez le statut',
          deptIdRequired: 'Entrez l\'ID du département',
          roleIdsRequired: 'Sélectionnez les rôles',
          postIdsRequired: 'Sélectionnez les postes',
          deptIdsRequired: 'Sélectionnez les départements',
          agreementRequired: 'Lisez et acceptez l\'accord utilisateur'
        }
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
      captcha: {
        // Captcha à glissière
        slider: {
          title: 'Captcha à glissière',
          default: 'Veuillez faire glisser le curseur pour aligner avec l\'espace',
          moving: 'Vérification...',
          success: 'Vérification réussie !',
          failed: 'Échec de la vérification, veuillez réessayer',
          expired: 'Captcha expiré, actualisation automatique',
          countdown: '{seconds}s',
          clickToRefresh: 'Cliquez sur l\'image pour actualiser le captcha',
          verifyError: 'Échec de la demande de vérification',
          maxRetryReached: 'Trop d\'échecs de vérification, veuillez actualiser et réessayer',
          hint: {
            slide: 'Veuillez glisser vers la droite',
            dragToAlign: 'Veuillez faire glisser le curseur pour aligner avec l\'espace',
            align: 'Aligner avec l\'espace'
          },
          error: {
            invalidResponse: 'Données de captcha invalides',
            loadFailed: 'Échec du chargement du captcha'
          },
          bgImage: 'Image d\'arrière-plan',
          sliderImage: 'Image du curseur'
        },
        // Captcha comportemental
        behavior: {
          title: 'Captcha comportemental',
          default: 'Veuillez maintenir et faire glisser le curseur vers la droite',
          success: 'Vérification réussie !',
          failed: 'Échec de la vérification, veuillez réessayer',
          verifyError: 'Échec de la demande de vérification',
          hint: {
            slide: 'Glisser vers la droite',
            dragToEnd: 'Faire glisser jusqu\'à la fin'
          },
          error: {
            loadFailed: 'Échec du chargement du captcha'
          }
        }
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