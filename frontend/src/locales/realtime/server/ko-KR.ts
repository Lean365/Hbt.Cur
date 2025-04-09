export default {
  realtime: {
    server: {
      title: '서버 모니터',
      refresh: '새로고침',
      refreshResult: {
        success: '데이터 새로고침 성공',
        failed: '데이터 새로고침 실패'
      },
      resource: {
        title: '리소스 사용량',
        cpu: 'CPU 사용률',
        memory: '메모리 사용률',
        disk: '디스크 사용률'
      },
      system: {
        title: '시스템 정보',
        os: '운영체제',
        architecture: '아키텍처',
        version: '버전',
        processor: {
          name: '프로세서',
          count: '코어 수',
          unit: '코어'
        },
        startup: {
          time: '시스템 시작 시간',
          uptime: '가동 시간',
          day: '일',
          hour: '시간'
        }
      },
      dotnet: {
        title: '.NET Runtime 정보',
        runtime: {
          version: '.NET Runtime 버전',
          directory: '런타임 디렉토리'
        },
        clr: {
          version: 'CLR 버전'
        }
      },
      network: {
        title: '네트워크 정보',
        adapter: '어댑터',
        mac: 'MAC 주소',
        ip: {
          address: 'IP 주소',
          location: '위치',
          unknown: '알 수 없는 위치'
        },
        rate: {
          send: '전송 속도',
          receive: '수신 속도'
        }
      }
    }
  }
}