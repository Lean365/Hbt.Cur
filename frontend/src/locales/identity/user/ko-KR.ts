export default {
  identity: {
    user: {
      title: '사용자 관리',
      table: {
        columns: {
          userId: '사용자 ID',
          tenantId: '테넌트',
          userName: '사용자명',
          nickName: '닉네임',
          englishName: '영문명',
          userType: '유형',
          email: '이메일',
          phoneNumber: '전화번호',
          gender: '성별',
          avatar: '아바타',
          status: '상태',
          lastPasswordChangeTime: '마지막 비밀번호 변경 시간',
          lockEndTime: '잠금 종료 시간',
          lockReason: '잠금 사유',
          isLock: '잠금 여부',
          errorLimit: '오류 횟수 제한',
          loginCount: '로그인 횟수',
          deptName: '소속 부서',
          role: '소속 역할',
          createBy: '생성자',
          createTime: '생성 시간',
          updateBy: '수정자',
          updateTime: '수정 시간',
          deleteBy: '삭제자',
          deleteTime: '삭제 시간',
          isDeleted: '삭제 여부',
          remark: '비고',
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
      fields: {
        userId: '사용자 ID',
        tenantId: {
          label: '테넌트',
          placeholder: '테넌트를 선택하세요',
          validation: {
            required: '테넌트는 필수입니다'
          }
        },
        userName: {
          label: '사용자명',
          placeholder: '사용자명을 입력하세요',
          validation: {
            required: '사용자명은 필수입니다',
            format: '사용자명은 소문자로 시작하고, 6~20자, 소문자, 숫자, 밑줄만 포함할 수 있습니다'
          }
        },
        nickName: {
          label: '닉네임',
          placeholder: '닉네임을 입력하세요',
          validation: {
            required: '닉네임은 필수입니다',
            format: '닉네임은 2~20자, 한자, 영문, 숫자, 밑줄만 포함할 수 있습니다'
          }
        },
        englishName: {
          label: '영문명',
          placeholder: '영문명을 입력하세요',
          validation: {
            format: '영문명은 2~50자, 영문, 공백, 하이픈만 포함할 수 있습니다'
          }
        },
        userType: {
          label: '유형',
          placeholder: '사용자 유형을 선택하세요',
          options: {
            admin: '관리자',
            user: '일반 사용자'
          }
        },
        email: {
          label: '이메일',
          placeholder: '이메일을 입력하세요',
          validation: {
            required: '이메일은 필수입니다',
            invalid: '이메일은 6~100자, 올바른 형식이어야 합니다'
          }
        },
        phoneNumber: {
          label: '전화번호',
          placeholder: '전화번호를 입력하세요',
          validation: {
            required: '전화번호는 필수입니다',
            invalid: '올바른 휴대폰 또는 유선 전화번호 형식을 입력하세요'
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
        status: {
          label: '상태',
          placeholder: '상태를 선택하세요',
          options: {
            enabled: '활성화',
            disabled: '비활성화'
          }
        },
        deptName: {
          label: '소속 부서',
          placeholder: '소속 부서를 선택하세요',
          validation: {
            required: '소속 부서는 필수입니다'
          }
        },
        role: {
          label: '소속 역할',
          placeholder: '소속 역할을 선택하세요',
          validation: {
            required: '소속 역할은 필수입니다'
          }
        },
        post: {
          label: '소속 직위',
          placeholder: '소속 직위를 선택하세요',
          validation: {
            required: '소속 직위는 필수입니다'
          }
        },
        remark: {
          label: '비고',
          placeholder: '비고를 입력하세요'
        }
      },
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
        role: '역할 권한',
        dept: '부서/직위',
        other: '기타 정보',
        avatar: '아바타 설정',
        loginLog: '로그인 로그',
        operateLog: '작업 로그',
        errorLog: '오류 로그',
        taskLog: '작업 로그'
      },
      update: {
        validation: {
          required: '사용자 ID와 테넌트 ID는 필수입니다'
        }
      },
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
      resetPwd: '비밀번호 재설정'
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
    userName: '사용자명은 필수입니다',
    nickName: '닉네임은 필수입니다',
    phoneNumber: '올바른 전화번호를 입력하세요',
    email: '올바른 이메일 주소를 입력하세요'
  }
}
