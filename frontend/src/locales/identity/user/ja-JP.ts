export default {
  identity: {
    user: {
      title: 'ユーザー管理',
      toolbar: {
        add: 'ユーザー追加',
        edit: 'ユーザー編集',
        delete: 'ユーザー削除',
        import: 'ユーザーインポート',
        export: 'ユーザーエクスポート',
        resetPassword: 'パスワードリセット',
        downloadTemplate: 'テンプレートダウンロード'
      },
      table: {
        columns: {
          userName: 'ユーザー名',
          nickName: 'ニックネーム',
          deptName: '部署',
          role: '役割',
          email: 'メールアドレス',
          phoneNumber: '電話番号',
          gender: '性別',
          status: 'ステータス',
          createTime: '作成日時',
          lastLoginTime: '最終ログイン',
          operation: '操作'
        },
        operation: {
          edit: '編集',
          delete: '削除',
          resetPassword: 'パスワードリセット'
        },
        status: {
          enabled: '有効',
          disabled: '無効',
          toggle: {
            enable: '有効化',
            disable: '無効化'
          }
        }
      },
      userId: 'ユーザーID',
      userName: {
        label: 'ユーザー名',
        placeholder: 'ユーザー名を入力してください',
        validation: {
          required: 'ユーザー名は必須です',
          format: 'ユーザー名は小文字で始まり、6-20文字の長さで、小文字、数字、アンダースコアのみを含むことができます'
        }
      },
      nickName: {
        label: 'ニックネーム',
        placeholder: 'ニックネームを入力してください',
        validation: {
          required: 'ニックネームは必須です',
          format: 'ニックネームは2-20文字の長さで、漢字、英字、数字、アンダースコアのみを含むことができます'
        }
      },
      englishName: {
        label: '英語名',
        placeholder: '英語名を入力してください',
        validation: {
          format: '英語名は2-50文字の長さで、英字、スペース、ハイフンのみを含むことができます'
        }
      },
      password: {
        label: 'パスワード',
        placeholder: 'パスワードを入力してください',
        validation: {
          required: 'パスワードは必須です',
          length: 'パスワードは6-20文字の長さである必要があります'
        }
      },
      confirmPassword: {
        label: 'パスワード確認',
        placeholder: 'パスワードを再入力してください',
        validation: {
          required: 'パスワード確認は必須です',
          notMatch: 'パスワードが一致しません'
        }
      },
      email: {
        label: 'メールアドレス',
        placeholder: 'メールアドレスを入力してください',
        validation: {
          required: 'メールアドレスは必須です',
          invalid: 'メールアドレスは6-100文字の長さで、有効な形式である必要があります'
        }
      },
      phoneNumber: {
        label: '電話番号',
        placeholder: '電話番号を入力してください',
        validation: {
          required: '電話番号は必須です',
          invalid: '有効な携帯電話または固定電話番号の形式を入力してください'
        }
      },
      gender: {
        label: '性別',
        placeholder: '性別を選択してください',
        options: {
          male: '男性',
          female: '女性',
          unknown: '不明'
        }
      },
      avatar: {
        label: 'アバター',
        upload: 'アバターアップロード',
        uploadSuccess: 'アバターのアップロードに成功しました',
        uploadError: 'アバターのアップロードに失敗しました'
      },
      deptName: {
        label: '部署',
        placeholder: '部署を選択してください'
      },
      role: {
        label: '役割',
        placeholder: '役割を選択してください'
      },
      post: {
        label: '役職',
        placeholder: '役職を選択してください'
      },
      status: {
        label: 'ステータス',
        placeholder: 'ステータスを選択してください',
        options: {
          enabled: '有効',
          disabled: '無効'
        }
      },
      resetPwd: 'パスワードリセット',
      import: {
        title: 'ユーザーインポート',
        template: 'テンプレートダウンロード',
        success: 'インポート成功',
        failed: 'インポート失敗'
      },
      export: {
        title: 'ユーザーエクスポート',
        success: 'エクスポート成功',
        failed: 'エクスポート失敗'
      },
      userType: {
        label: 'ユーザータイプ',
        placeholder: 'ユーザータイプを選択してください',
        options: {
          admin: '管理者',
          user: '一般ユーザー'
        }
      },
      createTime: '作成日時',
      lastLoginTime: '最終ログイン',
      messages: {
        confirmDelete: '選択したユーザーを削除してもよろしいですか？',
        confirmResetPassword: '選択したユーザーのパスワードをリセットしてもよろしいですか？',
        confirmToggleStatus: 'このユーザーを{action}してもよろしいですか？',
        deleteSuccess: 'ユーザーの削除に成功しました',
        deleteFailed: 'ユーザーの削除に失敗しました',
        saveSuccess: 'ユーザー情報の保存に成功しました',
        saveFailed: 'ユーザー情報の保存に失敗しました',
        resetPasswordSuccess: 'パスワードのリセットに成功しました',
        resetPasswordFailed: 'パスワードのリセットに失敗しました',
        importSuccess: 'ユーザーのインポートに成功しました',
        importFailed: 'ユーザーのインポートに失敗しました',
        exportSuccess: 'ユーザーのエクスポートに成功しました',
        exportFailed: 'ユーザーのエクスポートに失敗しました',
        toggleStatusSuccess: 'ステータスの変更に成功しました',
        toggleStatusFailed: 'ステータスの変更に失敗しました'
      },
      tab: {
        basic: '基本情報',
        profile: 'プロフィール',
        role: '役割と権限',
        dept: '部署と役職',
        other: 'その他の情報',
        avatar: 'アバター設定',
        loginLog: 'ログイン履歴',
        operateLog: '操作履歴',
        errorLog: 'エラーログ',
        taskLog: 'タスクログ'
      },
      update: {
        validation: {
          required: 'ユーザーIDとテナントIDは必須です'
        }
      },
      tenantId: {
        label: 'テナント',
        placeholder: 'テナントを選択してください',
        validation: {
          required: 'テナントは必須です'
        }
      },
      roles: {
        label: '役割',
        placeholder: '役割を選択してください',
        validation: {
          required: '少なくとも1つの役割を選択してください'
        }
      },
      posts: {
        label: '役職',
        placeholder: '役職を選択してください',
        validation: {
          required: '少なくとも1つの役職を選択してください'
        }
      },
      depts: {
        label: '部署',
        placeholder: '部署を選択してください',
        validation: {
          required: '少なくとも1つの部署を選択してください'
        }
      },
      remark: {
        label: '備考',
        placeholder: '備考を入力してください'
      }
    }
  },
  actions: {
    add: 'ユーザー追加',
    edit: 'ユーザー編集',
    delete: 'ユーザー削除',
    resetPassword: 'パスワードリセット',
    export: 'ユーザーエクスポート'
  },
  rules: {
    userName: 'ユーザー名は必須です',
    nickName: 'ニックネームは必須です',
    phoneNumber: '有効な電話番号を入力してください',
    email: '有効なメールアドレスを入力してください'
  }
}
