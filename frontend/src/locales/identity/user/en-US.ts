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
          lastLoginTime: 'Last Login',
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
          required: 'Username is required',
          format: 'Username must start with a lowercase letter, be 6-20 characters long, and contain only lowercase letters, numbers, and underscores'
        }
      },
      nickName: {
        label: 'Nickname',
        placeholder: 'Please enter nickname',
        validation: {
          required: 'Nickname is required',
          format: 'Nickname must be 2-20 characters long and contain only Chinese characters, English letters, numbers, and underscores'
        }
      },
      englishName: {
        label: 'English Name',
        placeholder: 'Please enter English name',
        validation: {
          format: 'English name must be 2-50 characters long and contain only English letters, spaces, and hyphens'
        }
      },
      password: {
        label: 'Password',
        placeholder: 'Please enter password',
        validation: {
          required: 'Password is required',
          length: 'Password must be 6-20 characters long'
        }
      },
      confirmPassword: {
        label: 'Confirm Password',
        placeholder: 'Please enter password again',
        validation: {
          required: 'Password confirmation is required',
          notMatch: 'Passwords do not match'
        }
      },
      email: {
        label: 'Email',
        placeholder: 'Please enter email',
        validation: {
          required: 'Email is required',
          invalid: 'Email must be 6-100 characters long and be in a valid format'
        }
      },
      phoneNumber: {
        label: 'Phone Number',
        placeholder: 'Please enter phone number',
        validation: {
          required: 'Phone number is required',
          invalid: 'Please enter a valid mobile or landline phone number format'
        }
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
      avatar: {
        label: 'Avatar',
        upload: 'Upload Avatar',
        uploadSuccess: 'Avatar uploaded successfully',
        uploadError: 'Avatar upload failed'
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
        label: 'Position',
        placeholder: 'Please select position'
      },
      status: {
        label: 'Status',
        placeholder: 'Please select status',
        options: {
          enabled: 'Enabled',
          disabled: 'Disabled'
        }
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
      userType: {
        label: 'User Type',
        placeholder: 'Please select user type',
        options: {
          admin: 'Administrator',
          user: 'Normal User'
        }
      },
      createTime: 'Create Time',
      lastLoginTime: 'Last Login',
      messages: {
        confirmDelete: 'Are you sure you want to delete the selected users?',
        confirmResetPassword: 'Are you sure you want to reset the password for the selected users?',
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
        toggleStatusSuccess: 'Status changed successfully',
        toggleStatusFailed: 'Failed to change status'
      },
      tab: {
        basic: 'Basic Information',
        profile: 'Profile',
        role: 'Roles and Permissions',
        dept: 'Department and Position',
        other: 'Other Information',
        avatar: 'Avatar Settings',
        loginLog: 'Login History',
        operateLog: 'Operation History',
        errorLog: 'Error Log',
        taskLog: 'Task Log'
      },
      update: {
        validation: {
          required: 'User ID and Tenant ID are required'
        }
      },
      tenantId: {
        label: 'Tenant',
        placeholder: 'Please select tenant',
        validation: {
          required: 'Tenant is required'
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
        label: 'Positions',
        placeholder: 'Please select positions',
        validation: {
          required: 'Please select at least one position'
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
        placeholder: 'Please enter remark'
      }
    }
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
