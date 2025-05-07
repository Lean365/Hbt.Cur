export default {
  workflow: {
    variable: {
      title: '워크플로우 변수',
      list: {
        title: '워크플로우 변수 목록',
        search: {
          name: '변수명',
          type: '변수 유형',
          scope: '범위',
          status: '상태',
          startTime: '시작 시간',
          endTime: '종료 시간'
        },
        table: {
          name: '변수명',
          type: '변수 유형',
          scope: '범위',
          status: '상태',
          startTime: '시작 시간',
          endTime: '종료 시간',
          duration: '실행 시간',
          actions: '작업'
        },
        actions: {
          view: '보기',
          edit: '수정',
          delete: '삭제',
          refresh: '새로고침'
        },
        status: {
          running: '실행 중',
          completed: '완료',
          terminated: '종료',
          failed: '실패'
        }
      },
      form: {
        title: {
          create: '워크플로우 변수 생성',
          edit: '워크플로우 변수 수정'
        },
        fields: {
          name: '변수명',
          type: '변수 유형',
          scope: '범위',
          description: '설명',
          input: '입력',
          output: '출력',
          error: '오류'
        },
        rules: {
          name: {
            required: '변수명을 입력하세요'
          },
          type: {
            required: '변수 유형을 선택하세요'
          },
          scope: {
            required: '범위를 선택하세요'
          }
        },
        buttons: {
          submit: '제출',
          cancel: '취소'
        }
      },
      detail: {
        title: '워크플로우 변수 상세',
        basic: {
          title: '기본 정보',
          name: '변수명',
          type: '변수 유형',
          scope: '범위',
          description: '설명',
          status: '상태',
          startTime: '시작 시간',
          endTime: '종료 시간',
          duration: '실행 시간'
        },
        input: {
          title: '입력 정보',
          value: '입력값'
        },
        output: {
          title: '출력 정보',
          value: '출력값'
        },
        error: {
          title: '오류 정보',
          message: '오류 메시지',
          stackTrace: '스택 트레이스'
        },
        actions: {
          back: '돌아가기'
        }
      }
    }
  }
} 