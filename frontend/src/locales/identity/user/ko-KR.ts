export default {
  identity: {
    user: {
      title: '사용자 관리',
      toolbar: {
        add: '사용자 추가',
        edit: '사용자 수정',
        delete: '사용자 삭제',
        import: '사용자 가져오기',
        export: '사용자 내보내기',
        resetPassword: '비밀번호 재설정',
        downloadTemplate: '템플릿 다운로드'
      },
      table: {
        columns: {
          userName: '사용자 이름',
          nickName: '닉네임',
          deptName: '부서',
          role: '역할',
          email: '이메일',
          phoneNumber: '전화번호',
          gender: '성별',
          status: '상태',
          createTime: '생성 시간',
          lastLoginTime: '마지막 로그인',
          operation: '작업'
        },
        operation: {
          edit: '수정',
          delete: '삭제',
          resetPassword: '비밀번호 재설정'
        },
        status: {
          enabled: '활성화',
          disabled: '비활성화',
          toggle: {
            enable: '활성화',
            disable: '비활성화'
          }
        }
      },
      userId: '사용자 ID',
      userName: {
        label: '사용자 이름',
        placeholder: '사용자 이름을 입력하세요',
        validation: {
          required: '사용자 이름은 필수입니다',
          format: '사용자 이름은 소문자로 시작하고, 길이가 6-20자이며, 소문자, 숫자, 밑줄만 포함할 수 있습니다'
        }
      },
      nickName: {
        label: '닉네임',
        placeholder: '닉네임을 입력하세요',
        validation: {
          required: '닉네임은 필수입니다',
          format: '닉네임은 2-20자 길이여야 하며, 한자, 영문자, 숫자, 밑줄만 포함할 수 있습니다'
        }
      },
      englishName: {
        label: '영문 이름',
        placeholder: '영문 이름을 입력하세요',
        validation: {
          format: '영문 이름은 2-50자 길이여야 하며, 영문자, 공백, 하이픈만 포함할 수 있습니다'
        }
      },
      password: {
        label: '비밀번호',
        placeholder: '비밀번호를 입력하세요',
        validation: {
          required: '비밀번호는 필수입니다',
          length: '비밀번호는 6-20자 길이여야 합니다'
        }
      },
      confirmPassword: {
        label: '비밀번호 확인',
        placeholder: '비밀번호를 다시 입력하세요',
        validation: {
          required: '비밀번호 확인은 필수입니다',
          notMatch: '비밀번호가 일치하지 않습니다'
        }
      },
      email: {
        label: '이메일',
        placeholder: '이메일을 입력하세요',
        validation: {
          required: '이메일은 필수입니다',
          invalid: '이메일은 6-100자 길이여야 하며, 올바른 형식이어야 합니다'
        }
      },
      phoneNumber: {
        label: '전화번호',
        placeholder: '전화번호를 입력하세요',
        validation: {
          required: '전화번호는 필수입니다',
          invalid: '올바른 휴대폰 또는 유선 전화 번호 형식을 입력하세요'
        }
      },
      gender: {
        label: '성별',
        placeholder: '성별을 선택하세요',
        options: {
          male: '남성',
          female: '여성',
          unknown: '알 수 없음'
        }
      },
      avatar: {
        label: '아바타',
        upload: '아바타 업로드',
        uploadSuccess: '아바타 업로드 성공',
        uploadError: '아바타 업로드 실패'
      },
      deptName: {
        label: '부서',
        placeholder: '부서를 선택하세요'
      },
      role: {
        label: '역할',
        placeholder: '역할을 선택하세요'
      },
      post: {
        label: '직위',
        placeholder: '직위를 선택하세요'
      },
      status: {
        label: '상태',
        placeholder: '상태를 선택하세요',
        options: {
          enabled: '활성화',
          disabled: '비활성화'
        }
      },
      resetPwd: '비밀번호 재설정',
      import: {
        title: '사용자 가져오기',
        template: '템플릿 다운로드',
        success: '가져오기 성공',
        failed: '가져오기 실패'
      },
      export: {
        title: '사용자 내보내기',
        success: '내보내기 성공',
        failed: '내보내기 실패'
      },
      userType: {
        label: '사용자 유형',
        placeholder: '사용자 유형을 선택하세요',
        options: {
          admin: '관리자',
          user: '일반 사용자'
        }
      },
      createTime: '생성 시간',
      lastLoginTime: '마지막 로그인',
      messages: {
        confirmDelete: '선택한 사용자를 삭제하시겠습니까?',
        confirmResetPassword: '선택한 사용자의 비밀번호를 재설정하시겠습니까?',
        confirmToggleStatus: '이 사용자를 {action}하시겠습니까?',
        deleteSuccess: '사용자 삭제 성공',
        deleteFailed: '사용자 삭제 실패',
        saveSuccess: '사용자 정보 저장 성공',
        saveFailed: '사용자 정보 저장 실패',
        resetPasswordSuccess: '비밀번호 재설정 성공',
        resetPasswordFailed: '비밀번호 재설정 실패',
        importSuccess: '사용자 가져오기 성공',
        importFailed: '사용자 가져오기 실패',
        exportSuccess: '사용자 내보내기 성공',
        exportFailed: '사용자 내보내기 실패',
        toggleStatusSuccess: '상태 변경 성공',
        toggleStatusFailed: '상태 변경 실패'
      },
      tab: {
        basic: '기본 정보',
        profile: '프로필',
        role: '역할 및 권한',
        dept: '부서 및 직위',
        other: '기타 정보',
        avatar: '아바타 설정',
        loginLog: '로그인 기록',
        operateLog: '작업 기록',
        errorLog: '오류 기록',
        taskLog: '작업 기록'
      },
      update: {
        validation: {
          required: '사용자 ID와 테넌트 ID는 필수입니다'
        }
      },
      tenantId: {
        label: '테넌트',
        placeholder: '테넌트를 선택하세요',
        validation: {
          required: '테넌트는 필수입니다'
        }
      },
      roles: {
        label: '역할',
        placeholder: '역할을 선택하세요',
        validation: {
          required: '최소 하나의 역할을 선택하세요'
        }
      },
      posts: {
        label: '직위',
        placeholder: '직위를 선택하세요',
        validation: {
          required: '최소 하나의 직위를 선택하세요'
        }
      },
      depts: {
        label: '부서',
        placeholder: '부서를 선택하세요',
        validation: {
          required: '최소 하나의 부서를 선택하세요'
        }
      },
      remark: {
        label: '비고',
        placeholder: '비고를 입력하세요'
      }
    }
  },
  actions: {
    add: '사용자 추가',
    edit: '사용자 수정',
    delete: '사용자 삭제',
    resetPassword: '비밀번호 재설정',
    export: '사용자 내보내기'
  },
  rules: {
    userName: '사용자 이름은 필수입니다',
    nickName: '닉네임은 필수입니다',
    phoneNumber: '올바른 전화번호를 입력하세요',
    email: '올바른 이메일 주소를 입력하세요'
  }
}
