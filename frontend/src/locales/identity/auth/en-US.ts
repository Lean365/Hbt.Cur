export default {
  identity: {
    auth: {
      // Login related
      login: {
        title: 'User Login',
        username: 'Username',
        password: 'Password',
        tenantId: 'Tenant ID',
        rememberMe: 'Remember me',
        forgotPassword: 'Forgot password?',
        submit: 'Login',
        otherLogin: 'Other login methods',
        start: 'Starting login',
        success: 'Login successful',
        failed: 'Login failed',
        noToken: 'No access token received',
        
        form: {
          tenantIdRequired: 'Please enter tenant ID',
          usernameRequired: 'Please enter username',
          usernameLength: 'Username must be between 3-50 characters',
          passwordRequired: 'Please enter password',
          passwordLength: 'Password must be between 6-100 characters',
          forgot: 'Forgot password',
          submit: 'Login'
        },

        error: {
          waitingRetry: 'Please wait {seconds} seconds before retrying',
          saltError: 'Failed to get encryption parameters, please try again',
          accountLocked: 'Account has been locked for {minutes} minutes',
          remainingAttempts: 'Login failed, {count} attempts remaining',
          serverError: 'Internal server error',
          invalidCredentials: 'Invalid username or password',
          accountDisabled: 'Account is disabled',
          tenantDisabled: 'Tenant is disabled',
          tenantNotFound: 'Tenant not found'
        },

        notAvailable: '{feature} feature is not available yet'
      },

      // Captcha related
      captcha: {
        title: 'Security Verification',
        required: 'Please complete security verification',
        waitingRetry: 'Please wait {seconds} seconds before retrying',
        verifyFailed: 'Verification failed, please try again',
        success: 'Verification successful',
        moving: 'Please slide the slider to the right',
        default: 'Please slide to verify',
        bgImage: 'Background image',
        sliderImage: 'Slider image',
        failed: 'Verification failed',
        maxRetryReached: 'Too many failed attempts, please try again later',
        verifyError: 'Verification error, please try again',
        error: {
          getFailed: 'Failed to get verification code',
          dataEmpty: 'Verification code data is empty',
          dataIncomplete: 'Verification code data is incomplete',
          tooManyRequests: 'Too many requests, please wait {seconds} seconds before retrying'
        }
      },

      // User information
      info: {
        loading: 'Loading user information',
        invalidResponse: 'Invalid response data',
        success: 'User information loaded successfully',
        error: {
          invalidResponse: 'Failed to get user info: Invalid response',
          failed: 'Failed to get user information'
        }
      },

      // Logout related
      logout: {
        title: 'Logout',
        confirm: 'Are you sure you want to logout?',
        start: 'Starting logout',
        success: 'Logout successful',
        error: 'Logout failed'
      },

      // User status
      status: {
        online: 'Online',
        offline: 'Offline',
        busy: 'Busy',
        away: 'Away'
      },

      // User roles
      role: {
        admin: 'Administrator',
        user: 'User',
        guest: 'Guest'
      },

      // Profile information
      profile: {
        title: 'Profile',
        basic: {
          username: 'Username',
          nickname: 'Nickname',
          email: 'Email',
          phone: 'Phone',
          avatar: 'Avatar'
        },
        security: {
          password: 'Password',
          oldPassword: 'Current Password',
          newPassword: 'New Password',
          confirmPassword: 'Confirm Password'
        },
        preferences: {
          language: 'Language',
          theme: 'Theme',
          notification: 'Notifications'
        }
      },

      // Error messages
      error: {
        accountNotExist: 'Account does not exist',
        accountDisabled: 'Account is disabled',
        accountLocked: 'Account is locked',
        passwordError: 'Incorrect password',
        captchaError: 'Invalid captcha',
        tokenExpired: 'Login expired, please login again',
        unauthorized: 'Unauthorized access',
        forbidden: 'Access forbidden'
      }
    }
  }
} 