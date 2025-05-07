export default {
  workflow: {
    task: {
      title: '워크플로우 태스크',
      list: {
        title: '워크플로우 태스크 목록',
        search: {
          name: '태스크명',
          type: '태스크 유형',
          status: '상태',
          startTime: '시작 시간',
          endTime: '종료 시간'
        },
        table: {
          name: '태스크명',
          type: '태스크 유형',
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
          create: '워크플로우 태스크 생성',
          edit: '워크플로우 태스크 수정'
        },
        fields: {
          name: '태스크명',
          type: '태스크 유형',
          description: '설명',
          input: '입력',
          output: '출력',
          error: '오류'
        },
        rules: {
          name: {
            required: '태스크명을 입력하세요'
          },
          type: {
            required: '태스크 유형을 선택하세요'
          }
        },
        buttons: {
          submit: '제출',
          cancel: '취소'
        }
      },
      detail: {
        title: '워크플로우 태스크 상세',
        basic: {
          title: '기본 정보',
          name: '태스크명',
          type: '태스크 유형',
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