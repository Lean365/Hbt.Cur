export default {
  identity: {
    user: {
      title: '使用者管理',
      toolbar: {
        add: '新增使用者',
        edit: '編輯使用者',
        delete: '刪除使用者',
        import: '匯入使用者',
        export: '匯出使用者',
        resetPassword: '重設密碼',
        downloadTemplate: '下載範本'
      },
      table: {
        columns: {
          userName: '使用者名稱',
          nickName: '暱稱',
          deptName: '部門',
          role: '角色',
          email: '電子郵件',
          phoneNumber: '電話號碼',
          gender: '性別',
          status: '狀態',
          createTime: '建立時間',
          lastLoginTime: '最後登入時間',
          operation: '操作'
        },
        operation: {
          edit: '編輯',
          delete: '刪除',
          resetPassword: '重設密碼'
        },
        status: {
          enabled: '啟用',
          disabled: '停用',
          toggle: {
            enable: '啟用',
            disable: '停用'
          }
        }
      },
      userId: '使用者ID',
      userName: {
        label: '使用者名稱',
        placeholder: '請輸入使用者名稱',
        validation: {
          required: '使用者名稱為必填項',
          format: '使用者名稱必須以小寫字母開頭，長度為6-20個字元，且只能包含小寫字母、數字和底線'
        }
      },
      nickName: {
        label: '暱稱',
        placeholder: '請輸入暱稱',
        validation: {
          required: '暱稱為必填項',
          format: '暱稱長度必須為2-20個字元，且只能包含中文字元、英文字母、數字和底線'
        }
      },
      englishName: {
        label: '英文名稱',
        placeholder: '請輸入英文名稱',
        validation: {
          format: '英文名稱長度必須為2-50個字元，且只能包含英文字母、空格和連字符'
        }
      },
      password: {
        label: '密碼',
        placeholder: '請輸入密碼',
        validation: {
          required: '密碼為必填項',
          length: '密碼長度必須為6-20個字元'
        }
      },
      confirmPassword: {
        label: '確認密碼',
        placeholder: '請再次輸入密碼',
        validation: {
          required: '確認密碼為必填項',
          notMatch: '密碼不一致'
        }
      },
      email: {
        label: '電子郵件',
        placeholder: '請輸入電子郵件',
        validation: {
          required: '電子郵件為必填項',
          invalid: '電子郵件長度必須為6-100個字元，且必須為有效的格式'
        }
      },
      phoneNumber: {
        label: '電話號碼',
        placeholder: '請輸入電話號碼',
        validation: {
          required: '電話號碼為必填項',
          invalid: '請輸入有效的手機或固定電話號碼格式'
        }
      },
      gender: {
        label: '性別',
        placeholder: '請選擇性別',
        options: {
          male: '男',
          female: '女',
          unknown: '未知'
        }
      },
      avatar: {
        label: '頭像',
        upload: '上傳頭像',
        uploadSuccess: '頭像上傳成功',
        uploadError: '頭像上傳失敗'
      },
      deptName: {
        label: '部門',
        placeholder: '請選擇部門'
      },
      role: {
        label: '角色',
        placeholder: '請選擇角色'
      },
      post: {
        label: '職位',
        placeholder: '請選擇職位'
      },
      status: {
        label: '狀態',
        placeholder: '請選擇狀態',
        options: {
          enabled: '啟用',
          disabled: '停用'
        }
      },
      resetPwd: '重設密碼',
      import: {
        title: '匯入使用者',
        template: '下載範本',
        success: '匯入成功',
        failed: '匯入失敗'
      },
      export: {
        title: '匯出使用者',
        success: '匯出成功',
        failed: '匯出失敗'
      },
      userType: {
        label: '使用者類型',
        placeholder: '請選擇使用者類型',
        options: {
          admin: '管理員',
          user: '一般使用者'
        }
      },
      createTime: '建立時間',
      lastLoginTime: '最後登入時間',
      messages: {
        confirmDelete: '確定要刪除選中的使用者嗎？',
        confirmResetPassword: '確定要重設選中使用者的密碼嗎？',
        confirmToggleStatus: '確定要{action}此使用者嗎？',
        deleteSuccess: '使用者刪除成功',
        deleteFailed: '使用者刪除失敗',
        saveSuccess: '使用者資訊儲存成功',
        saveFailed: '使用者資訊儲存失敗',
        resetPasswordSuccess: '密碼重設成功',
        resetPasswordFailed: '密碼重設失敗',
        importSuccess: '使用者匯入成功',
        importFailed: '使用者匯入失敗',
        exportSuccess: '使用者匯出成功',
        exportFailed: '使用者匯出失敗',
        toggleStatusSuccess: '狀態修改成功',
        toggleStatusFailed: '狀態修改失敗'
      },
      tab: {
        basic: '基本資訊',
        profile: '個人資料',
        role: '角色權限',
        dept: '部門職位',
        other: '其他資訊',
        avatar: '頭像設定',
        loginLog: '登入記錄',
        operateLog: '操作記錄',
        errorLog: '錯誤記錄',
        taskLog: '任務記錄'
      },
      update: {
        validation: {
          required: '使用者ID和租戶ID為必填項'
        }
      },
      tenantId: {
        label: '租戶',
        placeholder: '請選擇租戶',
        validation: {
          required: '租戶為必填項'
        }
      },
      roles: {
        label: '角色',
        placeholder: '請選擇角色',
        validation: {
          required: '請至少選擇一個角色'
        }
      },
      posts: {
        label: '職位',
        placeholder: '請選擇職位',
        validation: {
          required: '請至少選擇一個職位'
        }
      },
      depts: {
        label: '部門',
        placeholder: '請選擇部門',
        validation: {
          required: '請至少選擇一個部門'
        }
      },
      remark: {
        label: '備註',
        placeholder: '請輸入備註'
      }
    }
  },
  actions: {
    add: '新增使用者',
    edit: '編輯使用者',
    delete: '刪除使用者',
    resetPassword: '重設密碼',
    export: '匯出使用者'
  },
  rules: {
    userName: '使用者名稱為必填項',
    nickName: '暱稱為必填項',
    phoneNumber: '請輸入有效的電話號碼',
    email: '請輸入有效的電子郵件地址'
  }
}
