import { countReset } from "node:console";

export default {
  menu: {
    home: '홈',
    dashboard: {
      title: '대시보드',
      workplace: '작업공간',
      analysis: '분석',
      monitor: '모니터'
    },
    components: {
      title: '컴포넌트',
      icons: '아이콘'
    },
    about: {
      title: '우리에 대해',
      privacy: '개인정보 보호정책',
      terms: '서비스 약관',
      index: 'Hbt에 대해'
    },

    identity: {
      _self: '신원 인증',
      user: '사용자 관리',
      role: '역할 관리',
      dept: '부서 관리',
      post: '직책 관리',
      menu: '메뉴 관리',
      tenant: '테넌트 관리',
      oauth: 'OAuth 관리',
      profile: '개인 정보',
      changePassword: '비밀번호 변경'
    },
    audit: {
      _self: '감사 로그',
      operlog: '작업 로그',
      loginlog: '로그인 로그',
      sqldifflog: 'SQL 차이 로그',
      exceptionlog: '예외 로그',
      auditlog: '감사 로그',
      quartzlog: '작업 로그',
      server: '서버 모니터'
    },
    workflow: {
      _self: '워크플로우',
      engine:{
        _self: '프로세스 엔진',
        monitor: '프로세스 모니터',
        todo: '할 일',
        done: '완료된 작업',
        signoff: '프로세스 승인',
        execution: '프로세스 실행',
        designer: '프로세스 디자이너'
      },
      manage:{
        _self: '프로세스 관리',
        form: '폼 관리',
        scheme: '프로세스 스키마',
        instance: '프로세스 인스턴스',
        oper: '인스턴스 작업',
        trans: '인스턴스 흐름'
      }
    },
    signalr: {
      _self: '실시간 통신',
      online: '온라인 사용자',
      message: '온라인 메시지'
    },
    generator: {
      _self: '코드 생성기',
      table: '데이터베이스 테이블',
      tableDefine: '테이블 열 정의',
      template: '코드 템플릿',
      config: '생성 구성',
      api: 'API 문서'
    },
    routine: {
      _self: '일상 업무',
      core: {
        _self: '핵심 서비스',
        numberrule: '번호 규칙',
        config: '시스템 구성',
        language: '언어 관리',
        dict: '사전 관리'
      },
      contract: {
        _self: '계약 관리',
        template: {
          _self: '계약 템플릿',
          manage: '템플릿 관리',
          category: '템플릿 카테고리'
        },
        draft: {
          _self: '계약 작성',
          apply: '작성 신청',
          my: '내 작성'
        },
        approval: {
          _self: '계약 승인',
          pending: '계약 승인',
          approved: '승인됨',
          record: '승인 기록'
        },
        execution: {
          _self: '계약 실행',
          track: '실행 추적',
          change: '변경 관리',
          payment: '결제 관리'
        },
        archive: {
          _self: '계약 보관',
          manage: '보관 관리',
          query: '조회 통계'
        }
      },
      project: {
        _self: '프로젝트 관리',
        info: {
          _self: '프로젝트 정보',
          list: '프로젝트 목록'
        },
        plan: {
          _self: '프로젝트 계획',
          request: '계획 요청',
          gantt: '프로젝트 간트 차트'
        },
        task: {
          _self: '프로젝트 작업',
          assign: '작업 할당',
          track: '작업 추적',
          board: '작업 보드'
        },
        resource: {
          _self: '프로젝트 리소스',
          personnel: '인력 관리',
          equipment: '장비 관리',
          budget: '예산 관리'
        },
        monitor: {
          _self: '프로젝트 모니터링',
          progress: '진행 모니터링',
          quality: '품질 모니터링',
          risk: '위험 모니터링'
        }
      },
      quartz: {
        _self: '작업 스케줄링',
        job: {
          _self: '작업 관리',
          config: '작업 구성',
          list: '작업 목록',
          status: '작업 상태'
        },
        schedule: {
          _self: '작업 스케줄링',
          config: '스케줄 구성',
          monitor: '스케줄 모니터',
          stats: '스케줄 통계'
        }
      },
      schedule: {
        _self: '일정 관리',
        myschedule: '내 일정',
        dashboard: '일정 대시보드'
      },
      vehicle: {
        _self: '차량 관리',
        my: '내 차량',
        application: '차량 신청',
        dashboard: '차량 대시보드',
        manage: {
          _self: '차량 관리',
          info: '차량 정보',
          maintenance: '차량 유지보수'
        }
      },
      email: {
        _self: '이메일 관리',
        inbox: '받은 편지함',
        drafts: '임시보관함',
        sent: '보낸 편지함',
        trash: '휴지통',
        template: '이메일 템플릿'
      },
      meeting: {
        _self: '회의 관리',
        room: '회의실',
        mymeeting: '내 회의',
        booking: '회의 예약',
        dashboard: '회의 대시보드'
      },
      notice: {
        _self: '알림 및 공지',
        message: {
          _self: '메시지 관리',
          mymessages: '내 메시지',
          list: '메시지 보드'
        },
        announcement: {
          _self: '공지 관리',
          signoff: '공지 확인',
          list: '공지 목록'
        },
        notification: {
          _self: '알림 관리',
          ack: '읽은 알림',
          list: '알림 목록'
        }
      },
      hr: {
        _self: '인사 및 출근',
        recruitment: {
          _self: '채용 관리',
          apply: '채용 신청',
          approval: '채용 승인',
          list: '채용 목록'
        },
        transfer: {
          _self: '이동 관리',
          apply: '이동 신청',
          approval: '이동 승인',
          list: '이동 목록'
        },
        leave: {
          _self: '휴가 관리',
          apply: '휴가 신청',
          approval: '휴가 승인',
          list: '휴가 목록'
        },
        trip: {
          _self: '출장 관리',
          apply: '출장 신청',
          approval: '출장 승인',
          list: '출장 목록'
        },
        overtime: {
          _self: '초과근무 관리',
          apply: '초과근무 신청',
          approval: '초과근무 승인',
          list: '초과근무 목록'
        }
      },
      expense: {
        _self: '경비 관리',
        daily: {
          _self: '일상 경비',
          apply: '경비 신청',
          approve: '경비 승인',
          list: '경비 목록'
        },
        travel: {
          _self: '출장 경비',
          apply: '출장 경비 신청',
          approve: '출장 경비 승인',
          list: '출장 경비 목록'
        }
      },
      document: {
        _self: '문서 관리',
        news: {
          _self: '뉴스 관리',
        },
        regulation: {
          _self: '규정 및 규칙',
          manage: '규정 관리',
          control: '규정 제어',
        },
        file: {
          _self: '일상 파일',
        },
        iso: {
          _self: 'ISO 파일',
          manage: '파일 관리',
          control: '파일 제어',
        },
        official: {
          _self: '공식 문서 관리',
          manage: '문서 관리',
          issuance: '문서 제어',
        },
        law: {
          _self: '법률 및 규정',
        }
      },
      officesupplies: {
        _self: '사무용품',
        inventory: {
          _self: '재고 관리',
          requisition: '구매 관리',
          inbound: '입고 관리',
          stocktaking: '재고 실사 관리'
        },
        usage: {
          _self: '사용 관리',
          apply: '사용 신청',
          approve: '사용 승인',
          list: '사용 기록'
        }
      },
      book: {
        _self: '도서 관리',
        inventory: {
          _self: '재고 관리',
          requisition: '구매 관리',
          inbound: '입고 관리',
          list: '도서 목록',
          stocktaking: '재고 실사 관리'
        },
        usage: {
          _self: '사용 관리',
          card: '도서 카드',
          borrow: '대출',
          return: '반납'
        }
      },
      medical: {
        _self: '의료 관리',
        medicine: {
          _self: '재고 관리',
          requisition: '구매 관리',
          inbound: '입고 관리',
          list: '의약품 목록',
          stocktaking: '재고 실사 관리'
        },
        usage: {
          _self: '사용 관리',
          archive: '보관',
          receive: '의약품 수령',
          cost: '비용'
        }
      }
    },
    accounting: {
      _self: '회계',
      financial: {
        _self: '관리 회계',
        company: '회사 정보',
        account: '계정과목',
        companyaccount: '회사 계정',
        ledger: '총계정원장',
        payable: '미지급금',
        receivable: '미수금',
        fixedasset: '고정자산',
        bank: '은행 정보'
      },
      controlling: {
        _self: '관리 회계',
        costelement: '원가 요소',
        costcenter: '원가 센터',
        profitcenter: '이익 센터',
        accountsReceivable: '미수금',
        accountsPayable: '미지급금',
        assetAccounting: '자산 회계',
        tax: '세무 관리',
        financialReporting: '재무 보고서'
      },
      budget: {
        _self: '종합 예산',
        formulation: {
          _self: '예산 수립',
          sales: {
            _self: '매출 예산',
            cost: '매출 원가',
            rolling: '롤링 매출'
          },
          production: {
            _self: '생산 예산',
            auxiliary: '생산 보조',
            labor: '생산 노무',
            manufacturing: '생산 제조'
          },
          cost: {
            _self: '원가 예산',
            directmaterial: '직접 재료',
            directlabor: '직접 노무',
            indirectlabor: '간접 노무',
            manufacturing: '제조 간접비'
          },
          expense: {
            _self: '경비 예산',
            sales: '판매비',
            manage: '관리비',
            financial: '재무비'
          },
          financial: {
            _self: '재무 예산',
            cashflow: '현금 흐름',
            balancesheet: '대차대조표',
            income: '손익계산서'
          }
        },
        control: {
          _self: '예산 관리',
          dashboard: '예산 대시보드',
          approval: '예산 승인'
        }
      }
    },
    logistics: {
      _self: '물류 관리',
      equipment: {
        _self: '장비 관리',
        master: {
          _self: '장비 데이터',
          list: '장비 정보',
          location: '기능 위치',
          material: '자재 연관'
        },
        maintenance: {
          _self: '장비 유지보수',
          workorder: '유지보수 계획',
          assign: '유지보수 할당',
          execute: '유지보수 실행'
        }
      },
      material: {
        _self: '자재 관리',
        manage: {
          _self: '자재 정보',
          master: '그룹 자재',
          plant: {
            _self: '공장 정보',
            master: '공장 자재'
          }
        },
        purchase: {
          _self: '구매 관리',
          vendor: '벤더 정보',
          supplier: '공급업체 정보',
          price: '구매 가격',
          requisition: '구매 요청',
          order: '구매 주문'
        },
        sample:{
          _self: '샘플 관리',
          component: '부품 샘플',
          product: '제품 샘플'
        },
        drawing: {
          _self: '도면 관리',
          design: '도면 관리',
          engineering: '도면 제어',
          gerber: 'Gerber 파일',
          coordinate: '좌표 파일',
          assembly: '조립 도면',
          structure: '구조 파일',
          impedance: '임피던스 파일',
          process: '프로세스 플로우'
        },
        csm: {  
          _self: '고객 제공 품목 관리',
          raw: '고객 제공 자재',
          good: '고객 제공 제품'
        }
      },
      production: {
        _self: '생산 관리',
        basic: {
          _self: '기본 데이터',
          bom: '자재 명세서',
          workcenter: '작업 센터',   
          routing: '공정 경로',
          order: '생산 주문',
          worktime: '생산 시간',
          kanban: '칸반'
        },
        change: {
          _self: '설계 변경',
          implementation: '변경 구현',
          techcontact: '기술 연락',
          material: '자재 확인',
          query: '변경 조회',
          oldproduct: '구 제품 관리',
          sop: 'SOP 확인',
          batch: '투입 배치',
          input: {
            _self: '변경 입력',
            gijutsu: '기술과',
            seikan: '생관과',
            koubai: '구매과',
            uketsuke: '검수과',
            bukan: '부관과',
            seizou2: '제2과',
            seizou1: '제1과',
            hinkan: '품관과',
            seizougijutsu: '제기과'
          }
        },       
        output: {
          _self: '제조 관리',
          workshop1:{
            _self: '제1과',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: '생산',
              modify: '수정',
              rework: '재작업'
            },
            defect:{
              _self: '불량',
              epp: 'EPP',
              production: '생산',
              modify: '수정',
              rework: '재작업'
            },
            worktime: {
              _self: '작업 시간',
              epp: 'EPP',
              production: '생산',
              modify: '수정',
              rework: '재작업'
            }
          },
          workshop2:{
            _self: '제2과',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: '생산',
              modify: '수정',
              rework: '재작업'
            },
            defect:{
              _self: '불량',
              eppInspection: 'EPP 검사',
              eppRepair: 'EPP 수리',
              productionInspection: '생산 검사',
              productionRepair: '생산 수리',
              modifyInspection: '수정 검사',
              modifyRepair: '수정 수리',
              reworkInspection: '재작업 검사',
              reworkRepair: '재작업 수리'
            },
            worktime: {
              _self: '작업 시간',
              epp: 'EPP',
              production: '생산',
              modify: '수정',
              rework: '재작업'
            }
          }
        },
        sop: {
          _self: 'SOP 관리',
          workshop1: '제1과',
          workshop2: '제2과'
        },
        techcontact: {
          _self: '기술 연락',
          epp: 'EPP 연락',
          engineering: '공정 연락',
          external: '외부 연락'
        }
      },
      project: {
        _self: '프로젝트 관리',
        define: '프로젝트 정의',
        cost: '원가 계획',
        resource: '리소스 계획',
        schedule: '일정 계획'
      },
      quality: {
        _self: '품질 관리',
        basic: {
          _self: '기본 데이터',
          item: '검사 항목',
          method: '검사 방법',
          sampling: '샘플링 계획',
          defect: '불량 분류',
          rule: '판정 규칙',
          category: '품질 카테고리'
        },
        inspection:{
          _self: '검사 관리',
          receiving: '입고 검사',
          process: '공정 검사',
          storage: '입고 검사',
          return: '반품 검사'
        },
        trace:{
          _self: '추적 관리',
          batch: '배치 추적',
          corrective: '시정 조치',
          notification: '통지서',
        },
        cost:{
          _self: '품질 원가',
          business:'품질 업무',
          rework:'재작업 업무',
          scrap:'폐기 업무',
        },
        plan: {
          _self: '품질 계획',
          supplier: '공급업체 평가',
          customer: '고객 조사'
        },
        item: '검사 항목',
        receiving: '입고 검사',
        process: '공정 검사',
        storage: '입고 검사',
        return: '반품 검사'
      },
      sales: {
        _self: '판매 관리',
        customer: '고객 정보',
        client: '클라이언트 정보',
        price: '판매 가격',
        order: '판매 주문'
      },
      service: {
        _self: '고객 서비스',
        cs: {
          _self: '고객 서비스',
          item: '서비스 항목',
          contract: '서비스 계약',
          request: '서비스 요청',
          workorder: '서비스 작업 주문',
          timesheet: '서비스 시간',
          consumption: '자재 소비',
          outsourcing: '외주 서비스'
        },
        cc: {
          _self: '고객 불만 관리',
          notice: '품질 통지서',
          mark: '불만 세부사항',
          analysis: '원인 분석',
          corrective: '시정 조치',
          return: '반품/교환 실행',
          followUp: '후속 처리'
        }
      }
    },
    hrm: {
      _self: '인적 자원',
      attendance: {
        _self: '출근 관리',
        record: '출근 기록',
        holiday: '휴가 관리',
        overtime: '초과근무 관리',
        compensatory: '대체 휴가 관리'
      },
      benefit: {
        _self: '복리후생 관리',
        project: '복리후생 프로젝트',
        employee: '직원 복리후생'
      },
      employee: {
        _self: '인력 관리',
        info: '인력 정보',
        contracttype: '계약 유형',
        contract: '계약 관리',
        promotion: '승진 관리',
        promotionhistory: '승진 이력',
        resignation: '퇴직 관리',
        transfer: '인력 목록',
        transferhistory: '이동 이력'
      },
      leave: {
        _self: '휴가 관리',
        type: '휴가 유형',
        employee: '직원 휴가'
      },
      organization: {
        _self: '조직 관리',
        positioncategory: '직책 카테고리',
        company: '회사 정보',
        department: '부서 정보',
        position: '직책 정보'
      },
      performance: {
        _self: '성과 관리',
        assessmentitem: '평가 항목',
        assessment: '성과 평가'
      },
      recruitment: {
        _self: '채용 관리',
        application: '직무 지원',
        posting: '채용 공고',
        candidate: '후보자 관리',
        interview: '면접 관리'
      },
      salary: {
        _self: '급여 관리',
        employee: '직원 급여',
        housing: '주택 적립금',
        housinglevel: '사회보험',
        tax: '세무 관리',
        taxlevel: '세율',
        structure: '급여 구조',
        social: '사회보험',
        socialbase: '사회보험 기준'
      },
      training: {
        _self: '교육 관리',
        category: '교육 카테고리',
        course: '교육 과정',
        record: '직원 교육'
      },
      report: {
        _self: '보고서 관리',
        employeeinfo: '인력 정보',
        resignation: '퇴직 보고서',
        transfer: '이동 보고서',
        promotion: '승진 보고서',
        training: '교육 보고서',
        salary: '급여 보고서',
        performance: '성과 보고서',
        attendance: '출근 보고서',
        benefit: '복리후생 보고서',
        recruitment: '채용 보고서'
      }
    }
  }
}
