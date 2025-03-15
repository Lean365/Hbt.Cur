export default {
  identity: {
    role: {
      title: 'Role Management',
      fields: {
        roleId: {
          label: 'Role ID'
        },
        roleName: {
          label: 'Role Name',
          placeholder: 'Please enter role name',
          validation: {
            required: 'Role name cannot be empty',
            length: 'Role name length must be between 2-20 characters'
          }
        },
        roleKey: {
          label: 'Role Key',
          placeholder: 'Please enter role key',
          validation: {
            required: 'Role key cannot be empty',
            length: 'Role key length must be between 2-100 characters'
          }
        },
        roleSort: {
          label: 'Display Order',
          placeholder: 'Please enter display order'
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          options: {
            enabled: 'Enabled',
            disabled: 'Disabled'
          }
        },
        description: {
          label: 'Description',
          placeholder: 'Please enter description'
        },
        createTime: 'Create Time',
        menuPermission: {
          label: 'Menu Permissions',
          selectAll: 'Select All/None'
        },
        dataScope: {
          label: 'Data Scope',
          options: {
            all: 'All Data',
            custom: 'Custom Data',
            dept: 'Department Data',
            deptAndChild: 'Department and Child Data',
            self: 'Personal Data'
          }
        }
      },
      actions: {
        add: 'Add Role',
        edit: 'Edit Role',
        delete: 'Delete Role',
        export: 'Export Roles'
      },
      messages: {
        confirmDelete: 'Are you sure you want to delete the selected role(s)?',
        deleteSuccess: 'Role deleted successfully',
        deleteFailed: 'Failed to delete role',
        saveSuccess: 'Role information saved successfully',
        saveFailed: 'Failed to save role information'
      }
    }
  }
} 