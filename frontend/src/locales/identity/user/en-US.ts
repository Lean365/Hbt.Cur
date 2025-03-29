export default {
  identity: {
    user: {
      title: 'User Management',
      toolbar: {
        add: 'Add User',
        edit: 'Edit User',
        delete: 'Delete User',
        import: 'Import Users',
        export: 'Export Users',
        resetPassword: 'Reset Password',
        downloadTemplate: 'Download Template'
      },
      table: {
        columns: {
          userName: 'Username',
          nickName: 'Nickname',
          deptName: 'Department',
          role: 'Role',
          email: 'Email',
          phoneNumber: 'Phone Number',
          gender: 'Gender',
          status: 'Status',
          createTime: 'Create Time',
          lastLoginTime: 'Last Login Time',
          operation: 'Operation'
        },
        operation: {
          edit: 'Edit',
          delete: 'Delete',
          resetPassword: 'Reset Password'
        },
        status: {
          enabled: 'Enabled',
          disabled: 'Disabled',
          toggle: {
            enable: 'Enable',
            disable: 'Disable'
          }
        }
      },
      userId: 'User ID',
      userName: {
        label: 'Username',
        placeholder: 'Please enter username',
        validation: {
          required: 'Username cannot be empty',
          format: 'Username must start with a lowercase letter, be 6-20 characters long, and can only contain lowercase letters, numbers, and underscores'
        }
      },
      nickName: {
        label: 'Nickname',
        placeholder: 'Please enter nickname',
        validation: {
          required: 'Nickname cannot be empty',
          format: 'Nickname must be 2-20 characters long and can only contain Chinese characters, English letters, numbers, and underscores'
        }
      },
      englishName: {
        label: 'English Name',
        placeholder: 'Please enter English name',
        validation: {
          format: 'English name must be 2-50 characters long and can only contain English letters, spaces, and hyphens'
        }
      },
      password: {
        label: 'Password',
        placeholder: 'Please enter password',
        validation: {
          required: 'Password cannot be empty',
          length: 'Password length must be between 6-20 characters'
        }
      },
      confirmPassword: {
        label: 'Confirm Password',
        placeholder: 'Please enter password again',
        validation: {
          required: 'Confirm password cannot be empty',
          notMatch: 'The two passwords do not match'
        }
      },
      email: {
        label: 'Email',
        placeholder: 'Please enter email',
        validation: {
          required: 'Email cannot be empty',
          invalid: 'Email must be 6-100 characters long and in valid format'
        }
      },
      phoneNumber: {
        label: 'Phone Number',
        placeholder: 'Please enter phone number',
        validation: {
          required: 'Phone number cannot be empty',
          invalid: 'Please enter a valid phone number or landline number format'
        }
      },
      gender: {
        label: 'Gender',
        placeholder: 'Please select gender',
        unknown: 'Unknown',
        male: 'Male',
        female: 'Female'
      },
      avatar: {
        label: 'Avatar',
        upload: 'Upload Avatar'
      },
      deptName: {
        label: 'Department',
        placeholder: 'Please select department'
      },
      role: {
        label: 'Role',
        placeholder: 'Please select role'
      },
      post: {
        label: 'Post',
        placeholder: 'Please select post'
      },
      status: {
        label: 'Status',
        normal: 'Normal',
        disabled: 'Disabled'
      },
      resetPwd: 'Reset Password',
      import: {
        title: 'Import Users',
        template: 'Download Template',
        success: 'Import successful',
        failed: 'Import failed'
      },
      export: {
        title: 'Export Users',
        success: 'Export successful',
        failed: 'Export failed'
      },
      messages: {
        confirmDelete: 'Are you sure you want to delete the selected user(s)?',
        confirmResetPassword: 'Are you sure you want to reset the password for selected user(s)?',
        confirmToggleStatus: 'Are you sure you want to {action} this user?',
        deleteSuccess: 'User deleted successfully',
        deleteFailed: 'Failed to delete user',
        saveSuccess: 'User information saved successfully',
        saveFailed: 'Failed to save user information',
        resetPasswordSuccess: 'Password reset successfully',
        resetPasswordFailed: 'Failed to reset password',
        importSuccess: 'Users imported successfully',
        importFailed: 'Failed to import users',
        exportSuccess: 'Users exported successfully',
        exportFailed: 'Failed to export users',
        toggleStatusSuccess: 'Status updated successfully',
        toggleStatusFailed: 'Failed to update status'
      },
      // Tabs
      tab: {
        basic: 'Basic Info',
        profile: 'Profile',
        role: 'Roles & Permissions',
        dept: 'Department & Post',
        other: 'Other Info'
      },
      tenantId: {
        label: 'Tenant',
        placeholder: 'Please select tenant',
        validation: {
          required: 'Tenant cannot be empty'
        }
      },
      roles: {
        label: 'Roles',
        placeholder: 'Please select roles',
        validation: {
          required: 'Please select at least one role'
        }
      },
      posts: {
        label: 'Posts',
        placeholder: 'Please select posts',
        validation: {
          required: 'Please select at least one post'
        }
      },
      depts: {
        label: 'Departments',
        placeholder: 'Please select departments',
        validation: {
          required: 'Please select at least one department'
        }
      },
      remark: {
        label: 'Remark',
        placeholder: 'Please enter remark information'
      }
    }
  },
  title: 'User Management',
  fields: {
    userName: {
      label: 'Username',
      placeholder: 'Please enter username'
    },
    nickName: {
      label: 'Nickname',
      placeholder: 'Please enter nickname'
    },
    phoneNumber: {
      label: 'Phone Number',
      placeholder: 'Please enter phone number'
    },
    email: {
      label: 'Email',
      placeholder: 'Please enter email'
    },
    gender: {
      label: 'Gender',
      placeholder: 'Please select gender',
      options: {
        male: 'Male',
        female: 'Female',
        unknown: 'Unknown'
      }
    },
    userType: {
      label: 'User Type',
      placeholder: 'Please select user type',
      options: {
        admin: 'Administrator',
        user: 'Regular User'
      }
    },
    status: {
      label: 'Status',
      placeholder: 'Please select status',
      options: {
        enabled: 'Enabled',
        disabled: 'Disabled'
      }
    },
    createTime: 'Create Time',
    lastLoginTime: 'Last Login Time'
  },
  actions: {
    add: 'Add User',
    edit: 'Edit User',
    delete: 'Delete User',
    resetPassword: 'Reset Password',
    export: 'Export Users'
  },
  rules: {
    userName: 'Username is required',
    nickName: 'Nickname is required',
    phoneNumber: 'Please enter a valid phone number',
    email: 'Please enter a valid email address'
  }
} 