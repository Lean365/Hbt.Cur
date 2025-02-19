export default {
  identity: {
    user: {
      title: 'User Management',
      userId: 'User ID',
      userName: {
        label: 'Username',
        placeholder: 'Please enter username',
        validation: {
          required: 'Username cannot be empty',
          length: 'Username length must be between 2-20 characters'
        }
      },
      nickName: {
        label: 'Nickname',
        placeholder: 'Please enter nickname',
        validation: {
          required: 'Nickname cannot be empty',
          length: 'Nickname length must be between 2-30 characters'
        }
      },
      englishName: {
        label: 'English Name',
        placeholder: 'Please enter English name',
        validation: {
          length: 'English name length cannot exceed 50 characters'
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
          invalid: 'Please enter a valid email format'
        }
      },
      phoneNumber: {
        label: 'Phone Number',
        placeholder: 'Please enter phone number',
        validation: {
          required: 'Phone number cannot be empty',
          invalid: 'Please enter a valid phone number format'
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
      }
    }
  }
} 