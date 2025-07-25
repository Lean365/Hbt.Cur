export default {

  identity: {
    auth: {
      login: {
        title: '로그인',
        tenantId: '테넌트 설정',
        username: '사용자 이름',
        password: '비밀번호',
        rememberMe: '로그인 정보 저장',
        forgotPassword: '비밀번호 찾기',
        submit: '로그인',
        register: '회원가입',
        success: '로그인 성공',
        error: {
          invalidCredentials: '사용자 이름 또는 비밀번호가 올바르지 않습니다',
          accountLocked: '계정이 잠겨 있습니다',
          accountDisabled: '계정이 비활성화되었습니다',
          accountExpired: '계정이 만료되었습니다',
          credentialsExpired: '비밀번호가 만료되었습니다',
          invalidCaptcha: '캡차가 유효하지 않습니다',
          invalidTenant: '유효하지 않은 테넌트입니다',
          invalidDevice: '장치 정보가 유효하지 않습니다',
          invalidGrant: '권한이 유효하지 않습니다',
          tooManyAttempts: '로그인 시도가 너무 많습니다. 나중에 다시 시도하세요'
        },
        noToken: '로그인 응답에 액세스 토큰이 없습니다',
        otherLogin: '다른 로그인 방법',
        form: {
          usernameRequired: '사용자 이름을 입력하세요',
          passwordRequired: '비밀번호를 입력하세요',
          tenantIdRequired: '테넌트 설정을 선택하세요'
        }
      },
      register: {
        title: '사용자 등록',
        subtitle: '사용자 등록을 단계별로 완료해 주세요',
        step1: '캡차 인증',
        step2: '기본 정보',
        step3: '기타 정보',
        step4: '권한 설정',
        username: '사용자명',
        usernamePlaceholder: '사용자명을 입력해 주세요',
        password: '비밀번호',
        passwordPlaceholder: '비밀번호를 입력해 주세요',
        confirmPassword: '비밀번호 확인',
        confirmPasswordPlaceholder: '비밀번호를 다시 입력해 주세요',
        email: '이메일',
        emailPlaceholder: '이메일을 입력해 주세요',
        nickName: '닉네임',
        nickNamePlaceholder: '닉네임을 입력해 주세요',
        realName: '실명',
        realNamePlaceholder: '실명을 입력해 주세요',
        fullName: '전체 이름',
        fullNamePlaceholder: '전체 이름을 입력해 주세요',
        englishName: '영문 이름',
        englishNamePlaceholder: '영문 이름을 입력해 주세요',
        phoneNumber: '전화번호',
        phoneNumberPlaceholder: '전화번호를 입력해 주세요',
        gender: '성별',
        genderUnknown: '알 수 없음',
        genderMale: '남성',
        genderFemale: '여성',
        userType: '사용자 유형',
        userTypePlaceholder: '사용자 유형을 선택해 주세요',
        userTypeNormal: '일반 사용자',
        userTypeAdmin: '관리자',
        status: '상태',
        statusPlaceholder: '상태를 선택해 주세요',
        statusNormal: '정상',
        statusDisabled: '비활성화',
        deptId: '부서 ID',
        deptIdPlaceholder: '부서 ID를 입력해 주세요',
        roleIds: '역할',
        roleIdsPlaceholder: '역할을 선택해 주세요',
        roleUser: '사용자',
        roleAdmin: '관리자',
        postIds: '직책',
        postIdsPlaceholder: '직책을 선택해 주세요',
        postEmployee: '직원',
        postManager: '매니저',
        deptIds: '소속 부서',
        deptIdsPlaceholder: '소속 부서를 선택해 주세요',
        deptIT: 'IT 부서',
        deptHR: '인사 부서',
        remark: '비고',
        remarkPlaceholder: '비고를 입력해 주세요',
        agreement: '이용약관에 동의합니다',
        agreementPrefix: '이용약관에 동의합니다',
        agreementLink: '《이용약관》',
        agreementSuffix: '',
        agreementTitle: '사용자 등록 약관',
        agreementContent: '등록하기 전에 본 약관을 잘 읽고 동의해 주세요.',
        captcha: '캡차',
        submit: '등록 완료',
        nextStep: '다음',
        back: '이전',
        backToLogin: '로그인으로 돌아가기',
        login: '기존 계정으로 로그인',
        success: '등록 성공',
        successTitle: '등록 성공',
        successSubtitle: '계정이 성공적으로 생성되었습니다. 새 계정으로 로그인해 주세요',
        successMessage: '사용자 {userName}이(가) 성공적으로 등록되었습니다',
        step1Success: '캡차 인증 완료',
        step2Success: '기본 정보 완료',
        step3Success: '기타 정보 완료',
        step4Success: '권한 설정 완료',
        error: {
          step1Failed: '캡차 인증 실패',
          step2Failed: '기본 정보 검증 실패',
          step3Failed: '기타 정보 검증 실패',
          step4Failed: '권한 설정 실패',
          unknown: '등록 실패, 나중에 다시 시도해 주세요'
        },
        form: {
          usernameRequired: '사용자명을 입력해 주세요',
          usernameLength: '사용자명은 3-20자로 입력해 주세요',
          usernameFormat: '사용자명은 영문, 숫자, 언더스코어만 사용 가능합니다',
          emailRequired: '이메일을 입력해 주세요',
          emailFormat: '올바른 이메일 형식을 입력해 주세요',
          captchaRequired: '캡차를 입력해 주세요',
          nickNameRequired: '닉네임을 입력해 주세요',
          nickNameLength: '닉네임은 2-20자로 입력해 주세요',
          realNameRequired: '실명을 입력해 주세요',
          realNameLength: '실명은 2-20자로 입력해 주세요',
          fullNameRequired: '전체 이름을 입력해 주세요',
          fullNameLength: '전체 이름은 2-50자로 입력해 주세요',
          englishNameLength: '영문 이름은 2-50자로 입력해 주세요',
          englishNameFormat: '영문 이름은 영문과 공백만 사용 가능합니다',
          phoneNumberFormat: '올바른 전화번호 형식을 입력해 주세요',
          passwordRequired: '비밀번호를 입력해 주세요',
          passwordLength: '비밀번호는 6-20자로 입력해 주세요',
          passwordFormat: '비밀번호는 대문자, 소문자, 숫자를 포함해야 합니다',
          confirmPasswordRequired: '비밀번호를 확인해 주세요',
          passwordMismatch: '비밀번호가 일치하지 않습니다',
          userTypeRequired: '사용자 유형을 선택해 주세요',
          statusRequired: '상태를 선택해 주세요',
          deptIdRequired: '부서 ID를 입력해 주세요',
          roleIdsRequired: '역할을 선택해 주세요',
          postIdsRequired: '직책을 선택해 주세요',
          deptIdsRequired: '소속 부서를 선택해 주세요',
          agreementRequired: '이용약관을 읽고 동의해 주세요'
        }
      },
      forgot: {
        title: '비밀번호 찾기',
        email: '이메일',
        submit: '제출',
        back: '로그인으로 돌아가기',
        success: '비밀번호 재설정 이메일이 전송되었습니다',
        error: '비밀번호 재설정 실패'
      },
      info: {
        loading: '사용자 정보 로딩 중',
        success: '사용자 정보 로딩 성공'
      },
      captcha: {
        // 슬라이더 캡차
        slider: {
          title: '슬라이더 캡차',
          default: '슬라이더를 드래그하여 간격에 맞춰주세요',
          moving: '확인 중...',
          success: '확인 성공!',
          failed: '확인 실패, 다시 시도하세요',
          expired: '캡차가 만료되었습니다. 자동 새로고침 중',
          countdown: '{seconds}초',
          clickToRefresh: '이미지를 클릭하여 캡차 새로고침',
          verifyError: '확인 요청 실패',
          maxRetryReached: '확인 실패가 너무 많습니다. 새로고침하고 다시 시도하세요',
          hint: {
            slide: '오른쪽으로 슬라이드하세요',
            dragToAlign: '슬라이더를 드래그하여 간격에 맞춰주세요',
            align: '간격에 맞춰주세요'
          },
          error: {
            invalidResponse: '캡차 데이터가 유효하지 않습니다',
            loadFailed: '캡차 로드 실패'
          },
          bgImage: '배경 이미지',
          sliderImage: '슬라이더 이미지'
        },
        // 행동 캡차
        behavior: {
          title: '행동 캡차',
          default: '슬라이더를 누르고 오른쪽으로 드래그하세요',
          success: '확인 성공!',
          failed: '확인 실패, 다시 시도하세요',
          verifyError: '확인 요청 실패',
          hint: {
            slide: '오른쪽으로 슬라이드',
            dragToEnd: '끝까지 드래그'
          },
          error: {
            loadFailed: '캡차 로드 실패'
          }
        }
      },
      autoLogout: '장시간 미사용으로 인해 자동 로그아웃되었습니다',
      error: {
        noResponse: '서버가 응답하지 않습니다',
        noSaltData: '암호화 매개변수를 가져오지 못했습니다',
        invalidSalt: '암호화 매개변수 형식이 유효하지 않습니다',
        invalidIterations: '암호화 반복 횟수가 유효하지 않습니다',
        permanentlyLocked: '계정이 영구적으로 잠겨 있습니다. 관리자에게 문의하세요',
        temporarilyLocked: '계정이 임시로 잠겨 있습니다. {minutes}분 후에 다시 시도하세요',
        tooManyAttempts: '로그인 실패 횟수가 너무 많아 계정이 잠겼습니다',
        invalidCredentials: '사용자 이름 또는 비밀번호가 올바르지 않습니다'
      }
    }
  }
} 