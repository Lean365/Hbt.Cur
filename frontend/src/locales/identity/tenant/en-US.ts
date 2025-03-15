export default {
  identity: {
    tenant: {
      title: 'Tenant Management',
      fields: {
        tenantId: {
          label: 'Tenant ID'
        },
        tenantName: {
          label: 'Tenant Name',
          placeholder: 'Please enter tenant name',
          validation: {
            required: 'Tenant name cannot be empty',
            length: 'Tenant name length must be between 2-30 characters'
          }
        },
        tenantKey: {
          label: 'Tenant Key',
          placeholder: 'Please enter tenant key',
          validation: {
            required: 'Tenant key cannot be empty',
            length: 'Tenant key length must be between 2-20 characters'
          }
        },
        contactUser: {
          label: 'Contact Person',
          placeholder: 'Please enter contact person name'
        },
        contactPhone: {
          label: 'Contact Phone',
          placeholder: 'Please enter contact phone'
        },
        email: {
          label: 'Email',
          placeholder: 'Please enter email'
        },
        address: {
          label: 'Address',
          placeholder: 'Please enter address'
        },
        domain: {
          label: 'Domain',
          placeholder: 'Please enter domain'
        },
        licenseType: {
          label: 'License Type',
          placeholder: 'Please select license type',
          options: {
            trial: 'Trial Version',
            standard: 'Standard Version',
            professional: 'Professional Version',
            enterprise: 'Enterprise Version'
          }
        },
        expireTime: {
          label: 'Expiration Time',
          placeholder: 'Please select expiration time'
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          options: {
            enabled: 'Enabled',
            disabled: 'Disabled',
            expired: 'Expired'
          }
        },
        description: {
          label: 'Description',
          placeholder: 'Please enter description'
        },
        createTime: 'Create Time'
      },
      actions: {
        add: 'Add Tenant',
        edit: 'Edit Tenant',
        delete: 'Delete Tenant',
        export: 'Export Tenants',
        renew: 'Renew',
        config: 'Parameter Configuration'
      },
      messages: {
        confirmDelete: 'Are you sure you want to delete the tenant "{name}"?',
        deleteSuccess: 'Tenant deleted successfully',
        deleteFailed: 'Failed to delete tenant',
        saveSuccess: 'Tenant information saved successfully',
        saveFailed: 'Failed to save tenant information',
        renewSuccess: 'Tenant renewed successfully',
        renewFailed: 'Failed to renew tenant',
        configSuccess: 'Parameter configuration saved successfully',
        configFailed: 'Failed to save parameter configuration'
      }
    }
  }
} 