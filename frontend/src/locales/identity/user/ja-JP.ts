export default {
  identity: {
    user: {
      title: 'ユーザー管理',
      table: {
        columns: {
          userId: 'ユーザーID',
          tenantId: 'テナント',
          userName: 'ユーザー名',
          nickName: 'ニックネーム',
          englishName: '英語名',
          userType: 'タイプ',
          email: 'メールアドレス',
          phoneNumber: '電話番号',
          gender: '性別',
          avatar: 'アバター',
          status: '状態',
          lastPasswordChangeTime: '最終パスワード変更時間',
          lockEndTime: 'ロック終了時間',
          lockReason: 'ロック理由',
          isLock: 'ロック状態',
          errorLimit: 'エラー回数上限',
          loginCount: 'ログイン回数',
          deptName: '所属部門',
          role: '所属ロール',
          createBy: '作成者',
          createTime: '作成日時',
          updateBy: '更新者',
          updateTime: '更新日時',
          deleteBy: '削除者',
          deleteTime: '削除日時',
          isDeleted: '削除済み',
          remark: '備考',
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
            enable: '有効',
            disable: '無効'
          }
        }
      },
      fields: {
        userId: 'ユーザーID',
        tenantId: {
          label: 'テナント',
          placeholder: 'テナントを選択してください',
          validation: {
            required: 'テナントは必須です'
          }
        },
        userName: {
          label: 'ユーザー名',
          placeholder: 'ユーザー名を入力してください',
          validation: {
            required: 'ユーザー名は必須です',
            format: 'ユーザー名は小文字で始まり、6～20文字で、小文字、数字、アンダースコアのみ使用できます'
          }
        },
        nickName: {
          label: 'ニックネーム',
          placeholder: 'ニックネームを入力してください',
          validation: {
            required: 'ニックネームは必須です',
            format: 'ニックネームは2～20文字で、漢字、英字、数字、アンダースコアのみ使用できます'
          }
        },
        englishName: {
          label: '英語名',
          placeholder: '英語名を入力してください',
          validation: {
            format: '英語名は2～50文字で、英字、スペース、ハイフンのみ使用できます'
          }
        },
        userType: {
          label: 'タイプ',
          placeholder: 'ユーザータイプを選択してください',
          options: {
            admin: '管理者',
            user: '一般ユーザー'
          }
        },
        email: {
          label: 'メールアドレス',
          placeholder: 'メールアドレスを入力してください',
          validation: {
            required: 'メールアドレスは必須です',
            invalid: 'メールアドレスは6～100文字で、正しい形式で入力してください'
          }
        },
        phoneNumber: {
          label: '電話番号',
          placeholder: '電話番号を入力してください',
          validation: {
            required: '電話番号は必須です',
            invalid: '正しい携帯番号または固定電話番号を入力してください'
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
          upload: 'アバターをアップロード',
          uploadSuccess: 'アバターのアップロードに成功しました',
          uploadError: 'アバターのアップロードに失敗しました'
        },
        status: {
          label: '状態',
          placeholder: '状態を選択してください',
          options: {
            enabled: '有効',
            disabled: '無効'
          }
        },
        deptName: {
          label: '所属部門',
          placeholder: '所属部門を選択してください',
          validation: {
            required: '所属部門は必須です'
          }
        },
        role: {
          label: '所属ロール',
          placeholder: '所属ロールを選択してください',
          validation: {
            required: '所属ロールは必須です'
          }
        },
        post: {
          label: '所属ポスト',
          placeholder: '所属ポストを選択してください',
          validation: {
            required: '所属ポストは必須です'
          }
        },
        remark: {
          label: '備考',
          placeholder: '備考を入力してください'
        }
      },
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
        toggleStatusSuccess: '状態の変更に成功しました',
        toggleStatusFailed: '状態の変更に失敗しました'
      },
      tab: {
        basic: '基本情報',
        profile: 'プロフィール',
        role: 'ロール権限',
        dept: '部門・ポスト',
        other: 'その他情報',
        avatar: 'アバター設定',
        loginLog: 'ログイン履歴',
        operateLog: '操作履歴',
        errorLog: 'エラー履歴',
        taskLog: 'タスク履歴'
      },
      update: {
        validation: {
          required: 'ユーザーIDとテナントIDは必須です'
        }
      },
      import: {
        title: 'ユーザーのインポート',
        template: 'テンプレートをダウンロード',
        success: 'インポートに成功しました',
        failed: 'インポートに失敗しました'
      },
      export: {
        title: 'ユーザーのエクスポート',
        success: 'エクスポートに成功しました',
        failed: 'エクスポートに失敗しました'
      },
      resetPwd: 'パスワードリセット'
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
    phoneNumber: '正しい電話番号を入力してください',
    email: '正しいメールアドレスを入力してください'
  }
}
