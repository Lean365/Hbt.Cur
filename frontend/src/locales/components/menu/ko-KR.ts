import { countReset } from "node:console";

export default {
  menu: {
    home: '홈',
    dashboard: {
      title: '대시보드',
      workplace: '작업공간',
      analysis: '분석대',
      monitor: '모니터링대'
    },
    components: {
      title: '컴포넌트',
      icons: '아이콘'
    },
    about: {
      title: '회사소개',
      privacy: '개인정보처리방침',
      terms: '이용약관',
      index: 'Hbt 소개'
    },
    core: {
      _self: '핵심관리',
      config: '시스템설정',
      language: '언어관리',
      dict: '사전관리',
    },
    identity: {
      _self: '신원인증',
      user: '사용자관리',
      role: '역할관리',
      dept: '부서관리',
      post: '직위관리',
      menu: '메뉴관리',
      tenant: '테넌트관리',
      oauth: 'OAuth관리',
      profile: '개인정보',
      changePassword: '비밀번호변경'
    },
    audit: {
      _self: '감사로그',
      operlog: '작업로그',
      loginlog: '로그인로그',
      sqldifflog: '차이로그',
      exceptionlog: '예외로그',
      auditlog: '감사로그',
      quartzlog: '작업로그',
      server: '서비스모니터링'
    },
    workflow: {
      _self: '워크플로우',
      overview: '프로세스개요',
      my: '내프로세스',
      form: '폼관리',
      definition: '프로세스정의',
      instance: '프로세스인스턴스',
      task: '작업태스크',
      node: '프로세스노드',
      variable: '프로세스변수',
      history: '프로세스이력'
    },
    signalr: {
      _self: '실시간통신',
      online: '온라인사용자',
      message: '온라인메시지'
    },
    generator: {
      _self: '코드생성',
      table: '데이터베이스테이블',
      tableDefine: '테이블열정의',
      template: '코드템플릿',
      config: '생성설정',
      api: 'API문서'
    },
    routine: {
      _self: '일상업무',
      schedule: {
        _self: '일정관리',
        myschedule: '내일정',
        dashboard: '일정대시보드',
      },
      car: {
        _self: '차량관리',
        info: '차량정보',
        application: '차량신청',
        dashboard: '차량대시보드',
        maintenance: '차량정비',
      },
      email: {
        _self: '이메일관리',
        inbox: '받은편지함',
        drafts: '임시저장',
        sent: '보낸편지함',
        trash: '휴지통',
        template: '이메일템플릿',        
      },
      meeting: {
        _self: '회의관리',
        room: '회의실',
        mymeeting: '내회의',
        booking: '회의예약',
        dashboard: '회의대시보드',
      },
      notice: { 
        _self: '공지사항',
        message: {
          _self: '메시지관리',
          mymessages: '내메시지',
          list: '메시지대시보드',
        },
        announcement: {
          _self: '공지관리',
          signoff: '공지수신',
          list: '공지목록',
        },
        notification: {
          _self: '알림관리',
          ack: '읽음알림',
          list: '알림목록',
        },
      },
      hr: {
        _self: '인사근태',
        recruitment: {
          _self: '채용관리',
          apply: '채용신청',
          approval: '채용승인',
          list: '채용목록',

        },
        transfer: {
          _self: '전보관리',
          apply: '전보신청',
          approval: '전보승인',
          list: '전보목록',
        },
        leave: {
          _self: '휴가관리',
          apply: '휴가신청',
          approval: '휴가승인',
          list: '휴가목록',
        },
        trip: {
          _self: '출장관리',
          apply: '출장신청',
          approval: '출장승인',
          list: '출장목록',
        },
        overtime: {
          _self: '초과근무관리',
          apply: '초과근무신청',
          approval: '초과근무승인',
          list: '초과근무목록',
      },
    },
    expense:{
      _self: '비용관리',
      daily: {
        _self: '일상비용',
        apply: '비용신청',
        approve: '비용승인',
        list: '비용목록',
      },
      travel: {
        _self: '출장비용',
        apply: '출장비신청',
        approve: '출장비승인',
        list: '출장비목록',
      },
    },
    file:{
      _self: '파일관리',
      daily: {
        _self: '일상파일',
        list: '파일목록',
      },
      iso: {
        _self: 'ISO파일',
        version: '버전',
        signoff: '수신',
        list: 'ISO파일',
      },
      document: { 
        _self: '공문관리',
        version: '버전',
        signoff: '수신',
        list: '공문목록',
      },
    },
    officesupplies:{
      _self: '사무용품',
      inventory:{
        _self: '재고관리',
        requisition: '구매관리',
        inbound: '입고관리',
        stocktaking: '재고실사',
      },
      usage:{
        _self: '사용관리',
        apply: '사용신청',
        approve: '사용승인',
        receive: '사용기록',
      }
    },
    book:{
      _self: '도서관리',
      inventory:{
        _self: '재고관리',
        requisition: '구매관리',
        inbound: '입고관리',
        list: '도서목록',
        stocktaking: '재고실사',
      },
      usage:{
        _self: '사용관리',
        card: '대출증',
        borrow: '대출',
        return: '반납',
      }

    },
    medical:{
      _self: '의무관리',
      medicine:{
        _self: '재고관리',
        requisition: '구매관리',
        inbound: '입고관리',
        list: '약품목록',
        stocktaking: '재고실사',
      },
      usage:{
        _self: '사용관리',
        archive: '아카이브',
        receive: '약품수령',
        cost: '비용',
      }

    },
  },
  accounting: {
      _self: '회계계산',
      financial: {
        _self: '관리회계',
        company: "회사정보",
        account: '회계과목',
        companyaccount: '회사과목',
        ledger: '총계정원장',
        payable: '미지급금',
        receivable: '미수금',
        fixedasset: '고정자산',
        bank: '은행정보',

      },
      controlling: {
        _self: '관리회계',
        costelement: '원가요소',
        costcenter: '원가센터',
        profitcenter: '이익센터',
        accountsReceivable: '미수금',
        accountsPayable: '미지급금',
        assetAccounting: '자산회계',
        tax: '세무관리',
        financialReporting: '재무보고'      
    },
    budget:{
      _self: '종합예산',
        formulation: {
          _self: '예산편성',
          sales: {
            _self: '매출예산',
            cost: '매출원가',
            rolling: '매출롤링',
          },
          production: {
            _self: '생산예산',
            auxiliary: '생산부자재',
            labor: '생산인건비',
            manufacturing: '생산제조',
          },
          cost: {
            _self: '원가예산',
            directmaterial: '직접재료',
            directlabor: '직접노무비',
            indirectlabor: '간접노무비',
            manufacturing: '제조간접비',
          },
          expense: {
            _self: '비용예산',
            sales: '판매비',
            management: '관리비',
            financial: '재무비용',
          },
          financial: {
            _self: '재무예산',
            cashflow: '현금흐름',
            balancesheet: '대차대조표',
            income: '손익계산서',
          },
        },
        control: {
          _self: '예산관리',
          dashboard: '예산대시보드',
          approval: '예산승인',
        },   
  },
},
    logistics: {
      _self: '후방관리',
      equipment: {
        _self: '설비관리',
        data: '설비마스터데이터',
        location: '설비위치',
        material: '자재연관',
        workorder: '작업지시서'

      },
      material: {
        _self: '자재관리',
        material:{
          _self: '자재관리',
          material: '자재마스터데이터',
          plant: '공장정보',
          master: '자재데이터',
          plantmaster: '공장자재',
          vendor: '매도인정보',
          supplier: '공급업체정보',
        },
        purchase:{
          _self: '구매관리',
          vendor: '매도인정보',
          supplier: '공급업체정보',
          price: '구매가격',
          requisition: '구매요청',
          order: '구매발주',

        },



      },
      production: {
        _self: '생산관리',
        bom: '부품표',
        change: {
          _self: '설계변경',
          implementation: '설변실시',
          techcontact: '기술연락',
          material: '자재확인',
          query: '설변조회',
          oldproduct: '구품관리',
          sop: 'SOP확인',
          batch: '투입로트',
          input: {
            _self: '설변입력',
            gijutsu: '기술과',
            seikan: '생관과',
            koubai: '구매과',
            uketsuke: '수검과',
            bukan: '부관과',
            seizou2: '제2과',
            seizou1: '제1과',
            hinkan: '품관과',
            seizougijutsu: '제기과',
  
          }
        },
        workcenter: '작업센터',
        order: '생산지시',
        kanban: '간반',
        oph:{
          _self: 'OPH관리',
          workshop1: {
            _self: '제1과',
            output: '생산일보',
            defect: '생산불량',
            worktime: '생산공수',
            productionReport: '생산보고서',
            defectSummary: '불량집계',
            worktimeReport: '공수보고서'
          },
          workshop2: {
            _self: '제2과',
            output: '생산일보',
            inspection: '검사기록',
            repair: '수리기록',
            worktime: '생산공수',
            productionReport: '생산보고서',
            inspectionReport: '검사보고서',
            repairReport: '수리보고서',
            worktimeReport: '공수보고서'
          }
        }

      },
      project: {
        _self: '프로젝트관리',
        define: '프로젝트정의',
        cost: '원가계획',
        resource: '자원계획',
        schedule: '일정계획',

      },
      quality: {
        _self: '품질관리',
        item: '검사항목',
        receiving: '입하검사',
        process: '공정검사',
        storage: '입고검사',
        return: '반품검사',
  
      },
      sales: {
        _self: '판매관리',
        customer: '고객정보',
        client: '클라이언트정보',
        price: '판매가격',
        order: '판매주문',
      },
      service: {
        _self: '고객서비스',
        item: '서비스항목',
        contract: '서비스계약',
        request: '서비스요청',
        workorder: '서비스작업지시서',
        timesheet: '공수기록',
        consumption: '자재소비',
        outsourcing: '외주서비스'

      },
      complaint: {
        _self: '클레임관리',
        notice: '품질통지서',
        mark: '클레임명세',
        analysis: '원인분석',
        corrective: '시정조치',
        return: '반품교환실행',
        followUp: '후속처리'
      }
    },
    humanResources: {
      _self: '인적자원관리',
      employeeManagement: {
        _self: '직원관리',
        employeeMaster: '직원마스터데이터',
        attendance: '근태관리',
        leave: '휴가관리',
        payroll: '급여관리',
        contractManagement: '계약관리' // 신규계약관리
      },
      recruitment: {
        _self: '채용관리',
        jobPosting: '직위공모',
        candidateManagement: '후보자관리',
        interviewScheduling: '면접스케줄',
        offerManagement: '채용관리'
      },
      training: {
        _self: '교육관리',
        trainingPlan: '교육계획',
        trainingExecution: '교육실행',
        trainingEvaluation: '교육평가'
      },
      performance: {
        _self: '성과관리',
        goalSetting: '목표설정',
        performanceReview: '성과평가',
        feedback: '피드백관리'
      },
      reporting: {
        _self: '인적자원보고서',
        employeeReports: '직원보고서',
        attendanceReports: '근태보고서',
        payrollReports: '급여보고서',
        performanceReports: '성과보고서'
      }
    }
  }
}
