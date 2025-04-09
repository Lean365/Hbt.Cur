export default {

  identity: {
    auth: {
      login: {
        title: 'ログイン',
        username: 'ユーザー名',
        password: 'パスワード',
        rememberMe: 'ログイン情報を保存',
        forgotPassword: 'パスワードを忘れた',
        submit: 'ログイン',
        register: '新規登録',
        success: 'ログイン成功',
        error: {
          invalidCredentials: 'ユーザー名またはパスワードが正しくありません',
          accountLocked: 'アカウントがロックされています',
          accountDisabled: 'アカウントが無効になっています',
          accountExpired: 'アカウントの有効期限が切れています',
          credentialsExpired: 'パスワードの有効期限が切れています',
          invalidCaptcha: 'キャプチャが無効です',
          invalidTenant: '無効なテナントです',
          invalidDevice: 'デバイス情報が無効です',
          invalidGrant: '認可が無効です',
          tooManyAttempts: 'ログイン試行回数が多すぎます。後で再試行してください'
        },
        noToken: 'ログインレスポンスにアクセストークンがありません',
        otherLogin: 'その他のログイン方法',
        form: {
          usernameRequired: 'ユーザー名を入力してください',
          passwordRequired: 'パスワードを入力してください'
        }
      },
      register: {
        title: '新規登録',
        username: 'ユーザー名',
        password: 'パスワード',
        confirm: 'パスワード確認',
        email: 'メールアドレス',
        phone: '電話番号',
        submit: '登録',
        login: '既存のアカウントでログイン',
        success: '登録成功',
        error: '登録失敗'
      },
      forgot: {
        title: 'パスワードを忘れた',
        email: 'メールアドレス',
        submit: '送信',
        back: 'ログインに戻る',
        success: 'パスワードリセットメールを送信しました',
        error: 'パスワードリセットに失敗しました'
      },
      info: {
        loading: 'ユーザー情報を読み込み中',
        success: 'ユーザー情報の読み込みに成功しました'
      },
      autoLogout: '長時間の操作がないため、自動的にログアウトされました',
      error: {
        noResponse: 'サーバーが応答しません',
        noSaltData: '暗号化パラメータの取得に失敗しました',
        invalidSalt: '暗号化パラメータの形式が無効です',
        invalidIterations: '暗号化の反復回数が無効です',
        permanentlyLocked: 'アカウントが永久にロックされています。管理者に連絡してください',
        temporarilyLocked: 'アカウントが一時的にロックされています。{minutes}分後に再試行してください',
        tooManyAttempts: 'ログイン失敗回数が多すぎるため、アカウントがロックされました',
        invalidCredentials: 'ユーザー名またはパスワードが正しくありません'
      }
    }
  }
} 