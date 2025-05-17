export default {
  identity: {
    user: {
      title: 'Gestion des utilisateurs',
      table: {
        columns: {
          userId: 'ID utilisateur',
          tenantId: 'Locataire',
          userName: 'Nom d\'utilisateur',
          nickName: 'Pseudo',
          englishName: 'Nom anglais',
          userType: 'Type',
          email: 'E-mail',
          phoneNumber: 'Numéro de téléphone',
          gender: 'Genre',
          avatar: 'Avatar',
          status: 'Statut',
          lastPasswordChangeTime: 'Dernière modification du mot de passe',
          lockEndTime: 'Fin du verrouillage',
          lockReason: 'Raison du verrouillage',
          isLock: 'Verrouillé',
          errorLimit: 'Limite d\'erreurs',
          loginCount: 'Nombre de connexions',
          deptName: 'Département',
          role: 'Rôle',
          createBy: 'Créé par',
          createTime: 'Date de création',
          updateBy: 'Mis à jour par',
          updateTime: 'Date de mise à jour',
          deleteBy: 'Supprimé par',
          deleteTime: 'Date de suppression',
          isDeleted: 'Supprimé',
          remark: 'Remarque',
          operation: 'Opération'
        },
        operation: {
          edit: 'Éditer',
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
      fields: {
        userId: 'ID utilisateur',
        tenantId: {
          label: 'Locataire',
          placeholder: 'Veuillez sélectionner un locataire',
          validation: {
            required: 'Le locataire est requis'
          }
        },
        userName: {
          label: 'Nom d\'utilisateur',
          placeholder: 'Veuillez saisir le nom d\'utilisateur',
          validation: {
            required: 'Le nom d\'utilisateur est requis',
            format: 'Le nom d\'utilisateur doit commencer par une lettre minuscule, comporter entre 6 et 20 caractères et ne contenir que des lettres minuscules, des chiffres et des underscores'
          }
        },
        nickName: {
          label: 'Pseudo',
          placeholder: 'Veuillez saisir le pseudo',
          validation: {
            required: 'Le pseudo est requis',
            format: 'Le pseudo doit avoir une longueur de 2-20 caractères et ne contenir que des caractères chinois, des lettres anglaises, des chiffres et des underscores'
          }
        },
        englishName: {
          label: 'Nom anglais',
          placeholder: 'Veuillez saisir le nom anglais',
          validation: {
            format: 'Le nom anglais doit avoir une longueur de 2-50 caractères et ne contenir que des lettres anglaises, des espaces et des tirets'
          }
        },
        userType: {
          label: 'Type',
          placeholder: 'Veuillez sélectionner le type d\'utilisateur',
          options: {
            admin: 'Administrateur',
            user: 'Utilisateur normal'
          }
        },
        email: {
          label: 'E-mail',
          placeholder: 'Veuillez saisir l\'e-mail',
          validation: {
            required: 'L\'e-mail est requis',
            invalid: 'L\'e-mail doit comporter entre 6 et 100 caractères et être au bon format'
          }
        },
        phoneNumber: {
          label: 'Numéro de téléphone',
          placeholder: 'Veuillez saisir le numéro de téléphone',
          validation: {
            required: 'Le numéro de téléphone est requis',
            invalid: 'Veuillez saisir un numéro de téléphone mobile ou fixe valide'
          }
        },
        gender: {
          label: 'Genre',
          placeholder: 'Veuillez sélectionner le genre',
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
        status: {
          label: 'Statut',
          placeholder: 'Veuillez sélectionner le statut',
          options: {
            enabled: 'Activé',
            disabled: 'Désactivé'
          }
        },
        deptName: {
          label: 'Département',
          placeholder: 'Veuillez sélectionner le département',
          validation: {
            required: 'Le département est requis'
          }
        },
        role: {
          label: 'Rôle',
          placeholder: 'Veuillez sélectionner le rôle',
          validation: {
            required: 'Le rôle est requis'
          }
        },
        post: {
          label: 'Poste',
          placeholder: 'Veuillez sélectionner le poste',
          validation: {
            required: 'Le poste est requis'
          }
        },
        remark: {
          label: 'Remarque',
          placeholder: 'Veuillez saisir une remarque'
        }
      },
      messages: {
        confirmDelete: 'Êtes-vous sûr de vouloir supprimer l\'utilisateur sélectionné ?',
        confirmResetPassword: 'Êtes-vous sûr de vouloir réinitialiser le mot de passe de l\'utilisateur sélectionné ?',
        confirmToggleStatus: 'Êtes-vous sûr de vouloir {action} cet utilisateur ?',
        deleteSuccess: 'Utilisateur supprimé avec succès',
        deleteFailed: 'Échec de la suppression de l\'utilisateur',
        saveSuccess: 'Informations utilisateur enregistrées avec succès',
        saveFailed: 'Échec de l\'enregistrement des informations utilisateur',
        resetPasswordSuccess: 'Mot de passe réinitialisé avec succès',
        resetPasswordFailed: 'Échec de la réinitialisation du mot de passe',
        importSuccess: 'Utilisateur(s) importé(s) avec succès',
        importFailed: 'Échec de l\'importation de l\'utilisateur',
        exportSuccess: 'Utilisateur(s) exporté(s) avec succès',
        exportFailed: 'Échec de l\'exportation de l\'utilisateur',
        toggleStatusSuccess: 'Statut modifié avec succès',
        toggleStatusFailed: 'Échec de la modification du statut'
      },
      tab: {
        basic: 'Informations de base',
        profile: 'Profil',
        role: 'Permissions de rôle',
        dept: 'Département et poste',
        other: 'Autres informations',
        avatar: 'Paramètres de l\'avatar',
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
      import: {
        title: 'Importer des utilisateurs',
        template: 'Télécharger le modèle',
        success: 'Importation réussie',
        failed: 'Échec de l\'importation'
      },
      export: {
        title: 'Exporter des utilisateurs',
        success: 'Exportation réussie',
        failed: 'Échec de l\'exportation'
      },
      resetPwd: 'Réinitialiser le mot de passe'
    }
  },
  actions: {
    add: 'Ajouter un utilisateur',
    edit: 'Éditer l\'utilisateur',
    delete: 'Supprimer l\'utilisateur',
    resetPassword: 'Réinitialiser le mot de passe',
    export: 'Exporter des utilisateurs'
  },
  rules: {
    userName: 'Le nom d\'utilisateur est requis',
    nickName: 'Le pseudo est requis',
    phoneNumber: 'Veuillez saisir un numéro de téléphone valide',
    email: 'Veuillez saisir une adresse e-mail valide'
  }
}
