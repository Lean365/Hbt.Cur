export default {
  identity: {
    user: {
      title: 'Gestion des utilisateurs',
      toolbar: {
        add: 'Ajouter un utilisateur',
        edit: 'Modifier l\'utilisateur',
        delete: 'Supprimer l\'utilisateur',
        import: 'Importer des utilisateurs',
        export: 'Exporter des utilisateurs',
        resetPassword: 'Réinitialiser le mot de passe',
        downloadTemplate: 'Télécharger le modèle'
      },
      table: {
        columns: {
          userName: 'Nom d\'utilisateur',
          nickName: 'Pseudo',
          deptName: 'Département',
          role: 'Rôle',
          email: 'E-mail',
          phoneNumber: 'Numéro de téléphone',
          gender: 'Genre',
      status: 'Statut',
          createTime: 'Date de création',
          lastLoginTime: 'Dernière connexion',
          operation: 'Opération'
        },
        operation: {
          edit: 'Modifier',
          delete: 'Supprimer',
          resetPassword: 'Réinitialiser le mot de passe'
        },
        status: {
          enabled: 'Activé',
          disabled: 'Désactivé',
          toggle: {
            enable: 'Activer',
            disable: 'Désactiver'
          }
        }
      },
      userId: 'ID utilisateur',
      userName: {
        label: 'Nom d\'utilisateur',
        placeholder: 'Entrez le nom d\'utilisateur',
        validation: {
          required: 'Le nom d\'utilisateur est requis',
          format: 'Le nom d\'utilisateur doit commencer par une lettre minuscule, avoir une longueur de 6-20 caractères et ne contenir que des lettres minuscules, des chiffres et des underscores'
        }
      },
      nickName: {
        label: 'Pseudo',
        placeholder: 'Entrez le pseudo',
        validation: {
          required: 'Le pseudo est requis',
          format: 'Le pseudo doit avoir une longueur de 2-20 caractères et ne contenir que des caractères chinois, des lettres anglaises, des chiffres et des underscores'
        }
      },
      englishName: {
        label: 'Nom anglais',
        placeholder: 'Entrez le nom anglais',
        validation: {
          format: 'Le nom anglais doit avoir une longueur de 2-50 caractères et ne contenir que des lettres anglaises, des espaces et des tirets'
        }
      },
      password: {
        label: 'Mot de passe',
        placeholder: 'Entrez le mot de passe',
        validation: {
          required: 'Le mot de passe est requis',
          length: 'Le mot de passe doit avoir une longueur de 6-20 caractères'
        }
      },
      confirmPassword: {
        label: 'Confirmer le mot de passe',
        placeholder: 'Entrez à nouveau le mot de passe',
        validation: {
          required: 'La confirmation du mot de passe est requise',
          notMatch: 'Les mots de passe ne correspondent pas'
        }
      },
      email: {
        label: 'E-mail',
        placeholder: 'Entrez l\'e-mail',
        validation: {
          required: 'L\'e-mail est requis',
          invalid: 'L\'e-mail doit avoir une longueur de 6-100 caractères et être dans un format valide'
        }
      },
      phoneNumber: {
        label: 'Numéro de téléphone',
        placeholder: 'Entrez le numéro de téléphone',
        validation: {
          required: 'Le numéro de téléphone est requis',
          invalid: 'Entrez un format de numéro de téléphone mobile ou fixe valide'
        }
      },
      gender: {
        label: 'Genre',
        placeholder: 'Sélectionnez le genre',
        options: {
          male: 'Masculin',
          female: 'Féminin',
          unknown: 'Inconnu'
        }
      },
      avatar: {
        label: 'Avatar',
        upload: 'Télécharger l\'avatar',
        uploadSuccess: 'Avatar téléchargé avec succès',
        uploadError: 'Échec du téléchargement de l\'avatar'
      },
      deptName: {
        label: 'Département',
        placeholder: 'Sélectionnez le département'
      },
      role: {
        label: 'Rôle',
        placeholder: 'Sélectionnez le rôle'
      },
      post: {
        label: 'Poste',
        placeholder: 'Sélectionnez le poste'
      },
      status: {
        label: 'Statut',
        placeholder: 'Sélectionnez le statut',
        options: {
          enabled: 'Activé',
          disabled: 'Désactivé'
        }
      },
      resetPwd: 'Réinitialiser le mot de passe',
      import: {
        title: 'Importer des utilisateurs',
        template: 'Télécharger le modèle',
        success: 'Import réussi',
        failed: 'Échec de l\'import'
      },
      export: {
        title: 'Exporter des utilisateurs',
        success: 'Export réussi',
        failed: 'Échec de l\'export'
      },
      userType: {
        label: 'Type d\'utilisateur',
        placeholder: 'Sélectionnez le type d\'utilisateur',
        options: {
          admin: 'Administrateur',
          user: 'Utilisateur normal'
        }
      },
      createTime: 'Date de création',
      lastLoginTime: 'Dernière connexion',
      messages: {
        confirmDelete: 'Êtes-vous sûr de vouloir supprimer les utilisateurs sélectionnés ?',
        confirmResetPassword: 'Êtes-vous sûr de vouloir réinitialiser le mot de passe des utilisateurs sélectionnés ?',
        confirmToggleStatus: 'Êtes-vous sûr de vouloir {action} cet utilisateur ?',
        deleteSuccess: 'Utilisateur supprimé avec succès',
        deleteFailed: 'Échec de la suppression de l\'utilisateur',
        saveSuccess: 'Informations utilisateur enregistrées avec succès',
        saveFailed: 'Échec de l\'enregistrement des informations utilisateur',
        resetPasswordSuccess: 'Mot de passe réinitialisé avec succès',
        resetPasswordFailed: 'Échec de la réinitialisation du mot de passe',
        importSuccess: 'Utilisateurs importés avec succès',
        importFailed: 'Échec de l\'import des utilisateurs',
        exportSuccess: 'Utilisateurs exportés avec succès',
        exportFailed: 'Échec de l\'export des utilisateurs',
        toggleStatusSuccess: 'Statut modifié avec succès',
        toggleStatusFailed: 'Échec de la modification du statut'
      },
      tab: {
        basic: 'Informations de base',
        profile: 'Profil',
        role: 'Rôles et permissions',
        dept: 'Département et poste',
        other: 'Autres informations',
        avatar: 'Paramètres d\'avatar',
        loginLog: 'Journal de connexion',
        operateLog: 'Journal des opérations',
        errorLog: 'Journal des erreurs',
        taskLog: 'Journal des tâches'
      },
      update: {
        validation: {
          required: 'L\'ID utilisateur et l\'ID locataire sont requis'
        }
      },
      tenantId: {
        label: 'Locataire',
        placeholder: 'Sélectionnez le locataire',
        validation: {
          required: 'Le locataire est requis'
        }
      },
      roles: {
        label: 'Rôles',
        placeholder: 'Sélectionnez les rôles',
        validation: {
          required: 'Sélectionnez au moins un rôle'
        }
      },
      posts: {
        label: 'Postes',
        placeholder: 'Sélectionnez les postes',
        validation: {
          required: 'Sélectionnez au moins un poste'
        }
      },
      depts: {
        label: 'Départements',
        placeholder: 'Sélectionnez les départements',
        validation: {
          required: 'Sélectionnez au moins un département'
        }
      },
      remark: {
        label: 'Remarque',
        placeholder: 'Entrez une remarque'
      }
    }
  },
  actions: {
    add: 'Ajouter un utilisateur',
    edit: 'Modifier l\'utilisateur',
    delete: 'Supprimer l\'utilisateur',
    resetPassword: 'Réinitialiser le mot de passe',
    export: 'Exporter des utilisateurs'
  },
  rules: {
    userName: 'Le nom d\'utilisateur est requis',
    nickName: 'Le pseudo est requis',
    phoneNumber: 'Entrez un numéro de téléphone valide',
    email: 'Entrez une adresse e-mail valide'
  }
}
