export default {
  identity: {
    user: {
      title: 'User Management',
      table: {
        columns: {
          userId: 'User ID',
          tenantId: 'Tenant',
          userName: 'Username',
          nickName: 'Nickname',
          englishName: 'English Name',
          userType: 'Type',
          email: 'Email',
          phoneNumber: 'Phone Number',
          gender: 'Gender',
          avatar: 'Avatar',
          status: 'Status',
          lastPasswordChangeTime: 'Last Password Change Time',
          lockEndTime: 'Lock End Time',
          lockReason: 'Lock Reason',
          isLock: 'Locked',
          errorLimit: 'Error Limit',
          loginCount: 'Login Count',
          deptName: 'Department',
          role: 'Role',
          createBy: 'Created By',
          createTime: 'Created Time',
          updateBy: 'Updated By',
          updateTime: 'Updated Time',
          deleteBy: 'Deleted By',
          deleteTime: 'Deleted Time',
          isDeleted: 'Deleted',
          remark: 'Remark',
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
      fields: {
        userId: 'User ID',
        tenantId: {
          label: 'Tenant',
          placeholder: 'Please select tenant',
          validation: {
            required: 'Tenant is required'
          }
        },
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
            format: 'Nickname must be 2-20 characters long and can only contain Chinese, English, numbers, and underscores'
          }
        },
        englishName: {
          label: 'English Name',
          placeholder: 'Please enter English name',
          validation: {
            format: 'English name must be 2-50 characters long and can only contain English letters, spaces, and hyphens'
          }
        },
        userType: {
          label: 'Type',
          placeholder: 'Please select user type',
          options: {
            admin: 'Administrator',
            user: 'Regular User'
          }
        },
        email: {
          label: 'Email',
          placeholder: 'Please enter email',
          validation: {
            required: 'Email is required',
            invalid: 'Email must be 6-100 characters long and in the correct format'
          }
        },
        phoneNumber: {
          label: 'Phone Number',
          placeholder: 'Please enter phone number',
          validation: {
            required: 'Phone number is required',
            invalid: 'Please enter a valid mobile or landline number'
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
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          options: {
            enabled: 'Enabled',
            disabled: 'Disabled'
          }
        },
        deptName: {
          label: 'Department',
          placeholder: 'Please select department',
          validation: {
            required: 'Department is required'
          }
        },
        role: {
          label: 'Role',
          placeholder: 'Please select role',
          validation: {
            required: 'Role is required'
          }
        },
        post: {
          label: 'Post',
          placeholder: 'Please select post',
          validation: {
            required: 'Post is required'
          }
        },
        remark: {
          label: 'Remark',
          placeholder: 'Please enter remark information'
        }
      },
      messages: {
        confirmDelete: 'Are you sure you want to delete the selected user(s)?',
        confirmResetPassword: 'Are you sure you want to reset the password for the selected user(s)?',
        confirmToggleStatus: 'Are you sure you want to {action} this user?',
        deleteSuccess: 'User deleted successfully',
        deleteFailed: 'Failed to delete user',
        saveSuccess: 'User information saved successfully',
        saveFailed: 'Failed to save user information',
        resetPasswordSuccess: 'Password reset successfully',
        resetPasswordFailed: 'Failed to reset password',
        importSuccess: 'User(s) imported successfully',
        importFailed: 'Failed to import user(s)',
        exportSuccess: 'User(s) exported successfully',
        exportFailed: 'Failed to export user(s)',
        toggleStatusSuccess: 'Status updated successfully',
        toggleStatusFailed: 'Failed to update status'
      },
      tab: {
        basic: 'Basic Information',
        profile: 'Profile',
        role: 'Role Permissions',
        dept: 'Department & Post',
        other: 'Other Information',
        avatar: 'Avatar Settings',
        loginLog: 'Login Log',
        operateLog: 'Operation Log',
        errorLog: 'Error Log',
        taskLog: 'Task Log'
      },
      update: {
        validation: {
          required: 'User ID and Tenant ID are required'
        }
      },
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
      resetPwd: 'Reset Password'
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