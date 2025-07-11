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
        title: '회원가입',
        username: '사용자 이름',
        password: '비밀번호',
        confirm: '비밀번호 확인',
        email: '이메일',
        phone: '전화번호',
        submit: '가입',
        login: '기존 계정으로 로그인',
        success: '가입 성공',
        error: '가입 실패'
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