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
        error: 'Login failed',
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
        invalidIterations: 'Invalid encryption iteration count'
      }
    }
  }
} 