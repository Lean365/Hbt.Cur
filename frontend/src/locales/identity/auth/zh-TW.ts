export default {
  identity: {
    auth: {
      login: {
        title: '登入',
        username: '使用者名稱',
        password: '密碼',
        rememberMe: '記住密碼',
        forgotPassword: '忘記密碼',
        submit: '登入',
        register: '註冊帳號',
        success: '登入成功',
        error: {
          invalidCredentials: '使用者名稱或密碼錯誤',
          accountLocked: '帳號已被鎖定',
          accountDisabled: '帳號已被停用',
          accountExpired: '帳號已過期',
          credentialsExpired: '密碼已過期',
          invalidCaptcha: '驗證碼錯誤',
          invalidTenant: '無效的租戶',
          invalidDevice: '裝置資訊無效',
          invalidGrant: '授權資訊無效',
          tooManyAttempts: '登入嘗試次數過多，請稍後再試'
        },
        noToken: '登入回應中沒有存取權杖',
        otherLogin: '其他登入方式',
        form: {
          usernameRequired: '請輸入使用者名稱',
          passwordRequired: '請輸入密碼'
        }
      },
      register: {
        title: '註冊',
        username: '使用者名稱',
        password: '密碼',
        confirm: '確認密碼',
        email: '電子郵件',
        phone: '電話號碼',
        submit: '註冊',
        login: '使用現有帳號登入',
        success: '註冊成功',
        error: '註冊失敗'
      },
      forgot: {
        title: '忘記密碼',
        email: '電子郵件',
        submit: '提交',
        back: '返回登入',
        success: '重設密碼郵件已發送',
        error: '重設密碼失敗'
      },
      info: {
        loading: '正在載入使用者資訊',
        success: '取得使用者資訊成功'
      },
      autoLogout: '由於長時間未操作，您已被自動登出',
      error: {
        noResponse: '伺服器無回應',
        noSaltData: '取得加密參數失敗',
        invalidSalt: '加密參數格式無效',
        invalidIterations: '加密迭代次數無效',
        permanentlyLocked: '帳號已被永久鎖定，請聯絡管理員解鎖',
        temporarilyLocked: '帳號已被暫時鎖定，請等待{minutes}分鐘後再試',
        tooManyAttempts: '登入失敗次數過多，帳號已被鎖定',
        invalidCredentials: '使用者名稱或密碼錯誤'
      }
    }
  }
}