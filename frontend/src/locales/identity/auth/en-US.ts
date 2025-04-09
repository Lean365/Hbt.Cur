export default {
  identity: {
    auth: {
      login: {
        title: 'Login',
        username: 'Username',
        password: 'Password',
        rememberMe: 'Remember me',
        forgotPassword: 'Forgot password',
        submit: 'Login',
        register: 'Register',
        success: 'Login successful',
        error: {
          invalidCredentials: 'Invalid username or password',
          accountLocked: 'Account is locked',
          accountDisabled: 'Account is disabled',
          accountExpired: 'Account has expired',
          credentialsExpired: 'Password has expired',
          invalidCaptcha: 'Invalid captcha',
          invalidTenant: 'Invalid tenant',
          invalidDevice: 'Invalid device info',
          invalidGrant: 'Invalid grant',
          tooManyAttempts: 'Too many login attempts, please try again later',
          concurrentLogin: 'This account is already logged in on another device, please log out first',
          existingSession: 'This account is already logged in on {deviceInfo}, please log out first'
        },
        noToken: 'No access token in login response',
        otherLogin: 'Other login methods',
        form: {
          usernameRequired: 'Please enter your username',
          passwordRequired: 'Please enter your password'
        }
      },
      register: {
        title: 'Register',
        username: 'Username',
        password: 'Password',
        confirm: 'Confirm password',
        email: 'Email',
        phone: 'Phone',
        submit: 'Register',
        login: 'Login with existing account',
        success: 'Registration successful',
        error: 'Registration failed'
      },
      forgot: {
        title: 'Forgot Password',
        email: 'Email',
        submit: 'Submit',
        back: 'Back to login',
        success: 'Password reset email sent',
        error: 'Password reset failed'
      },
      info: {
        loading: 'Loading user information',
        success: 'User information loaded successfully'
      },
      autoLogout: 'You have been automatically logged out due to inactivity',
      error: {
        noResponse: 'Server not responding',
        noSaltData: 'Failed to get encryption parameters',
        invalidSalt: 'Invalid encryption parameter format',
        invalidIterations: 'Invalid encryption iteration count',
        permanentlyLocked: 'Account is permanently locked, please contact administrator',
        temporarilyLocked: 'Account is temporarily locked, please try again in {minutes} minutes',
        tooManyAttempts: 'Too many failed login attempts, account is locked',
        invalidCredentials: 'Invalid username or password',
        concurrentLogin: 'This account is already logged in on another device, please log out first'
      }
    }
  }
} 