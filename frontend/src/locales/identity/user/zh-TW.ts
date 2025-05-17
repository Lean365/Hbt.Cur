export default {
  identity: {
    user: {
      title: '用戶管理',
      table: {
        columns: {
          userId: '用戶ID',
          tenantId: '租戶',
          userName: '用戶名',
          nickName: '暱稱',
          englishName: '英文名',
          userType: '類型',
          email: '郵箱',
          phoneNumber: '手機號碼',
          gender: '性別',
          avatar: '頭像',
          status: '狀態',
          lastPasswordChangeTime: '最後密碼修改時間',
          lockEndTime: '鎖定結束時間',
          lockReason: '鎖定原因',
          isLock: '是否鎖定',
          errorLimit: '錯誤次數上限',
          loginCount: '登錄次數',
          deptName: '所屬部門',
          role: '所屬角色',
          createBy: '創建者',
          createTime: '創建時間',
          updateBy: '更新者',
          updateTime: '更新時間',
          deleteBy: '刪除者',
          deleteTime: '刪除時間',
          isDeleted: '是否刪除',
          remark: '備註',
          operation: '操作'
        },
        operation: {
          edit: '編輯',
          delete: '刪除',
          resetPassword: '重置密碼'
        },
        status: {
          enabled: '啟用',
          disabled: '禁用',
          toggle: {
            enable: '啟用',
            disable: '禁用'
          }
        }
      },
      fields: {
        userId: '用戶ID',
        tenantId: {
          label: '租戶',
          placeholder: '請選擇租戶',
          validation: {
            required: '租戶不能為空'
          }
        },
        userName: {
          label: '用戶名',
          placeholder: '請輸入用戶名',
          validation: {
            required: '用戶名不能為空',
            format: '用戶名必須以小寫字母開頭，長度在6-20位之間，只能包含小寫字母、數字和下劃線'
          }
        },
        nickName: {
          label: '暱稱',
          placeholder: '請輸入暱稱',
          validation: {
            required: '暱稱不能為空',
            format: '暱稱長度必須在2-20位之間，只能包含中文、英文、數字和下劃線'
          }
        },
        englishName: {
          label: '英文名',
          placeholder: '請輸入英文名',
          validation: {
            format: '英文名長度必須在2-50位之間，只能包含英文字母、空格和連字號'
          }
        },
        userType: {
          label: '類型',
          placeholder: '請選擇用戶類型',
          options: {
            admin: '管理員',
            user: '普通用戶'
          }
        },
        email: {
          label: '郵箱',
          placeholder: '請輸入郵箱',
          validation: {
            required: '郵箱不能為空',
            invalid: '郵箱長度必須在6-100位之間，且格式正確'
          }
        },
        phoneNumber: {
          label: '手機號碼',
          placeholder: '請輸入手機號碼',
          validation: {
            required: '手機號碼不能為空',
            invalid: '請輸入正確的手機號碼或座機號碼格式'
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
        status: {
          label: '狀態',
          placeholder: '請選擇狀態',
          options: {
            enabled: '啟用',
            disabled: '禁用'
          }
        },
        deptName: {
          label: '所屬部門',
          placeholder: '請選擇所屬部門',
          validation: {
            required: '所屬部門不能為空'
          }
        },
        role: {
          label: '所屬角色',
          placeholder: '請選擇所屬角色',
          validation: {
            required: '所屬角色不能為空'
          }
        },
        post: {
          label: '所屬崗位',
          placeholder: '請選擇所屬崗位',
          validation: {
            required: '所屬崗位不能為空'
          }
        },
        remark: {
          label: '備註',
          placeholder: '請輸入備註信息'
        }
      },
      messages: {
        confirmDelete: '是否確認刪除選中的用戶？',
        confirmResetPassword: '是否確認重置所選用戶的密碼？',
        confirmToggleStatus: '是否確認{action}該用戶？',
        deleteSuccess: '用戶刪除成功',
        deleteFailed: '用戶刪除失敗',
        saveSuccess: '用戶信息保存成功',
        saveFailed: '用戶信息保存失敗',
        resetPasswordSuccess: '密碼重置成功',
        resetPasswordFailed: '密碼重置失敗',
        importSuccess: '用戶導入成功',
        importFailed: '用戶導入失敗',
        exportSuccess: '用戶導出成功',
        exportFailed: '用戶導出失敗',
        toggleStatusSuccess: '狀態修改成功',
        toggleStatusFailed: '狀態修改失敗'
      },
      tab: {
        basic: '基本信息',
        profile: '個人資料',
        role: '角色權限',
        dept: '部門崗位',
        other: '其他信息',
        avatar: '頭像設置',
        loginLog: '登錄日誌',
        operateLog: '操作日誌',
        errorLog: '異常日誌',
        taskLog: '任務日誌'
      },
      update: {
        validation: {
          required: '用戶ID和租戶ID不能為空'
        }
      },
      import: {
        title: '導入用戶',
        template: '下載模板',
        success: '導入成功',
        failed: '導入失敗'
      },
      export: {
        title: '導出用戶',
        success: '導出成功',
        failed: '導出失敗'
      },
      resetPwd: '重置密碼'
    }
  },
  actions: {
    add: '新增用戶',
    edit: '編輯用戶',
    delete: '刪除用戶',
    resetPassword: '重置密碼',
    export: '導出用戶'
  },
  rules: {
    userName: '用戶名不能為空',
    nickName: '暱稱不能為空',
    phoneNumber: '請輸入正確的手機號碼',
    email: '請輸入正確的郵箱地址'
  }
}
