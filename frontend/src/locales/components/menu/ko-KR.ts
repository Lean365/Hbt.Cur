import { countReset } from "node:console";

export default {
  menu: {
    home: '홈',
    dashboard: {
      title: '대시보드',
      workplace: '작업 공간',
      analysis: '분석',
      monitor: '모니터링'
    },
    components: {
      title: '컴포넌트',
      icons: '아이콘'
    },
    about: {
      title: '회사 소개',
      privacy: '개인정보 보호정책',
      terms: '이용약관',
      index: 'Hbt 소개'
    },
    core: {
      _self: '시스템 관리',
      config: '시스템 설정',
      language: '언어 관리',
      dict: '사전 관리'
    },
    identity: {
      _self: '인증',
      user: '사용자 관리',
      role: '역할 관리',
      dept: '부서 관리',
      post: '직위 관리',
      menu: '메뉴 관리',
      tenant: '테넌트 관리',
      oauth: 'OAuth 관리'
    },
    audit: {
      _self: '감사 로그',
      operlog: '작업 로그',
      loginlog: '로그인 로그',
      sqldifflog: '데이터베이스 차이 로그',
      exceptionlog: '예외 로그',
      auditlog: '감사 로그',
      quartzlog: '작업 로그'
    },
    workflow: {
      _self: '워크플로우',
      definition: '프로세스 정의',
      instance: '프로세스 인스턴스',
      task: '작업',
      node: '프로세스 노드',
      variable: '프로세스 변수',
      history: '프로세스 이력'
    },
    signalr: {
      _self: '실시간 모니터링',
      server: '서버 모니터링',
      online: '온라인 사용자',
      message: '온라인 메시지'
    },
    generator: {
      _self: '코드 생성',
      table: '데이터베이스 테이블',
      tableDefine: '사용자 정의 테이블',
      template: '코드 템플릿',
      config: '생성 설정',
      api: 'API 문서'
    },
    routine: {
      _self: '일상 업무',
      vehicle: {
        _self: '차량 관리',
        vehicleMaster: {
          _self: '차량 마스터 데이터',
          vehicleInfo: '차량 정보',
          driverInfo: '운전자 정보',
          maintenance: '차량 유지보수'
        },
        vehicleBooking: {
          _self: '차량 예약',
          newBooking: '새 예약',
          bookingList: '예약 목록',
          bookingApproval: '예약 승인'
        },
        vehicleDispatch: {
          _self: '차량 배차',
          dispatchPlan: '배차 계획',
          realTimeTracking: '실시간 추적',
          dispatchHistory: '배차 이력'
        },
        vehicleReporting: {
          _self: '차량 보고서',
          usageReport: '사용 보고서',
          costReport: '비용 보고서',
          maintenanceReport: '유지보수 보고서'
        }
      },
      file: '파일 관리',
      mail: '메일 관리',
      mailTmpl: '메일 템플릿',
      meeting: {
        _self: '회의 관리',
        meetingRoom: {
          _self: '회의실 관리',
          roomInfo: '회의실 정보',
          roomBooking: '회의실 예약',
          roomSchedule: '회의실 일정'
        },
        meetingPlan: {
          _self: '회의 계획',
          newMeeting: '새 회의',
          meetingList: '회의 목록',
          meetingApproval: '회의 승인'
        },
        meetingExecution: {
          _self: '회의 실행',
          attendance: '출석',
          minutes: '회의록',
          followUp: '후속 조치'
        },
        meetingReporting: {
          _self: '회의 보고서',
          meetingReport: '회의 보고서',
          attendanceReport: '출석 보고서',
          costReport: '비용 보고서'
        }
      },
      notice: '공지사항',
      schedule: '일정 관리',
      quartz: '작업'
    },
    finance: {
      _self: '회계',
      management: {
        _self: '관리 회계',
        cost: {
          _self: '원가 관리',
          costFactors: '원가 요소',
          costCenter: '원가 센터',
          profitCenter: '이익 센터',
          productCost: '제품 원가',
          activityType: '활동 유형',
          internalOrder: '내부 주문'
        },
        planning: {
          _self: '계획 관리',
          costPlanning: '원가 계획',
          profitPlanning: '이익 계획',
          budgetControl: '예산 관리'
        },
        reporting: {
          _self: '보고서 및 분석',
          costReports: '원가 보고서',
          profitReports: '이익 보고서',
          varianceAnalysis: '차이 분석'
        }
      },
      financial: {
        _self: '재무 회계',
        generalLedger: {
          _self: '총계정원장',
          account: '계정',
          accountType: '계정 유형',
          journalEntry: '분개',
          reconciliation: '대조',
          closing: '기말 마감'
        },
        accountsReceivable: {
          _self: '매출채권',
          customer: '고객 관리',
          invoice: '고객 송장',
          payment: '고객 지불',
          creditControl: '신용 관리'
        },
        accountsPayable: {
          _self: '매입채무',
          supplier: '공급업체 관리',
          invoice: '공급업체 송장',
          payment: '공급업체 지불',
          agingReport: '연령 분석'
        },
        assetAccounting: {
          _self: '자산 회계',
          assets: '고정 자산',
          depreciation: '감가상각 관리',
          assetTransfer: '자산 이전',
          assetRetirement: '자산 폐기'
        },
        tax: {
          _self: '세금 관리',
          taxCodes: '세금 코드 관리',
          taxReporting: '세금 보고서',
          taxPayments: '세금 지불'
        },
        financialReporting: {
          _self: '재무 보고서',
          balanceSheet: '대차대조표',
          profitAndLoss: '손익계산서',
          cashFlow: '현금흐름표'
        }
      }
    },
    logistics: {
      _self: '물류',
      equipment: {
        _self: '장비 관리',
        equipmentMaster: '장비 마스터 데이터',
        maintenancePlanning: {
          _self: '유지보수 계획',
          preventiveMaintenance: '예방 유지보수',
          maintenanceTaskList: '유지보수 작업 목록',
          scheduling: '유지보수 일정'
        },
        maintenanceExecution: {
          _self: '유지보수 실행',
          workOrder: '작업 지시서',
          confirmation: '유지보수 확인',
          breakdownMaintenance: '고장 유지보수'
        },
        maintenanceReporting: {
          _self: '유지보수 보고서',
          equipmentReports: '장비 보고서',
          maintenanceHistory: '유지보수 이력',
          performanceAnalysis: '성능 분석'
        },
        sparePartsManagement: {
          _self: '예비 부품 관리',
          sparePartsInventory: '예비 부품 재고',
          sparePartsProcurement: '예비 부품 조달',
          sparePartsUsage: '예비 부품 사용'
        }
      },
      material: {
        _self: '자재 관리',
        materialMaster: '자재 마스터 데이터',
        materialCategory: '자재 카테고리',
        materialUnit: '자재 단위',
        materialStock: {
          _self: '자재 재고',
          stockOverview: '재고 개요',
          stockIn: '입고',
          stockOut: '출고',
          stockTransfer: '재고 이전',
          stockAdjustment: '재고 조정',
          stockCheck: '재고 확인'
        },
        purchase: {
          _self: '구매 관리',
          purchaseRequisition: '구매 요청',
          purchaseOrder: '구매 주문',
          purchaseOrderDetail: '구매 주문 상세',
          supplier: '공급업체 관리'
        },
        inventoryManagement: {
          _self: '재고 관리',
          goodsReceipt: '입고',
          goodsIssue: '출고',
          transferPosting: '이전 전기',
          stockOverview: '재고 개요'
        },
        valuation: {
          _self: '평가',
          priceControl: '가격 관리',
          standardPrice: '표준 가격',
          movingAveragePrice: '이동 평균 가격'
        },
        reporting: {
          _self: '보고서 및 분석',
          stockReports: '재고 보고서',
          purchaseReports: '구매 보고서',
          inventoryReports: '재고 보고서'
        }
      },
      production: {
        _self: '생산 관리',
        bom: '자재 명세서',
        routing: '공정 경로',
        workOrder: {
          _self: '작업 지시서',
          create: '작업 지시서 생성',
          manage: '작업 지시서 관리',
          release: '작업 지시서 발행',
          complete: '작업 지시서 완료'
        },
        capacityPlanning: {
          _self: '생산 능력 계획',
          workCenter: '작업 센터',
          capacityEvaluation: '생산 능력 평가',
          capacityLeveling: '생산 능력 균형'
        },
        productionScheduling: {
          _self: '생산 일정',
          schedule: '일정',
          reschedule: '일정 재조정'
        },
        productionExecution: {
          _self: '생산 실행',
          confirm: '생산 확인',
          goodsIssue: '자재 발행',
          goodsReceipt: '제품 입고'
        },
        productionReporting: {
          _self: '생산 보고서',
          orderReports: '주문 보고서',
          capacityReports: '생산 능력 보고서',
          efficiencyReports: '효율성 보고서'
        },
        qualityManagement: {
          _self: '품질 관리',
          inspectionLot: '검사 로트',
          resultsRecording: '결과 기록',
          defectRecording: '결함 기록'
        }
      },
      project: {
        _self: '프로젝트 관리',
        projectMaster: {
          _self: '프로젝트 마스터 데이터',
          projectDefinition: '프로젝트 정의',
          projectStructure: '프로젝트 구조',
          projectTeam: '프로젝트 팀',
          projectCalendar: '프로젝트 캘린더'
        },
        projectPlanning: {
          _self: '프로젝트 계획',
          workBreakdown: '작업 분해',
          scheduling: '일정 계획',
          resourcePlanning: '자원 계획',
          costPlanning: '원가 계획'
        },
        projectExecution: {
          _self: '프로젝트 실행',
          taskManagement: '작업 관리',
          progressTracking: '진도 추적',
          resourceManagement: '자원 관리',
          costControl: '원가 관리'
        },
        projectMonitoring: {
          _self: '프로젝트 모니터링',
          progressReports: '진도 보고서',
          resourceReports: '자원 보고서',
          costReports: '원가 보고서',
          riskManagement: '리스크 관리'
        },
        projectClosure: {
          _self: '프로젝트 종료',
          finalReport: '최종 보고서',
          lessonsLearned: '학습 내용',
          projectArchive: '프로젝트 보관'
        }
      },
      quality: {
        _self: '품질 관리',
        inspection: {
          _self: '검사 관리',
          inspectionLot: '검사 로트',
          resultsRecording: '결과 기록',
          defectRecording: '결함 기록',
          usageDecision: '사용 결정'
        },
        qualityPlanning: {
          _self: '품질 계획',
          inspectionPlan: '검사 계획',
          qualityInfoRecord: '품질 정보 기록',
          samplingProcedure: '샘플링 절차'
        },
        qualityControl: {
          _self: '품질 관리',
          controlChart: '관리도',
          qualityNotifications: '품질 통지',
          correctiveActions: '시정 조치'
        },
        qualityReporting: {
          _self: '품질 보고서',
          inspectionReports: '검사 보고서',
          defectReports: '결함 보고서',
          qualityAnalysis: '품질 분석'
        }
      },
      sales: {
        _self: '판매',
        customer: {
          _self: '고객 관리',
          client: '고객',
          customers: '고객 목록',
          creditControl: '신용 관리'
        },
        order: {
          _self: '주문 관리',
          order: '판매 주문',
          orderDetail: '주문 상세',
          orderTracking: '주문 추적'
        },
        delivery: {
          _self: '배송 관리',
          delivery: '배송',
          deliveryDetail: '배송 상세',
          shipping: '운송 관리'
        },
        billing: {
          _self: '청구',
          invoice: '송장 관리',
          invoiceDetail: '송장 상세',
          payment: '지불 관리'
        },
        reporting: {
          _self: '보고서 및 분석',
          salesReports: '판매 보고서',
          performanceAnalysis: '성과 분석'
        }
      },
      service: {
        _self: '서비스',
        serviceOrder: {
          _self: '서비스 주문',
          create: '주문 생성',
          manage: '주문 관리',
          complete: '주문 완료',
          cancel: '주문 취소'
        },
        serviceContract: {
          _self: '서비스 계약',
          create: '계약 생성',
          manage: '계약 관리',
          renew: '계약 갱신',
          terminate: '계약 종료'
        },
        customerInteraction: {
          _self: '고객 상호작용',
          inquiries: '고객 문의',
          complaints: '고객 불만',
          feedback: '고객 피드백'
        },
        serviceExecution: {
          _self: '서비스 실행',
          schedule: '일정',
          dispatch: '배치',
          execution: '실행',
          confirmation: '확인'
        },
        serviceReporting: {
          _self: '서비스 보고서',
          orderReports: '주문 보고서',
          contractReports: '계약 보고서',
          performanceReports: '성과 보고서'
        }
      }
    },
    humanResources: {
      _self: '인사 관리',
      employee: {
        _self: '직원 관리',
        employeeInfo: '직원 정보',
        employeeProfile: '직원 프로필',
        employeeContract: '직원 계약',
        employeeAttendance: '직원 출근',
        employeeLeave: '직원 휴가',
        employeePerformance: '직원 성과'
      },
      recruitment: {
        _self: '채용 관리',
        jobPosting: '채용 공고',
        candidate: '후보자 관리',
        interview: '면접 관리',
        offer: '채용 관리',
        onboarding: '입사 관리'
      },
      training: {
        _self: '교육 관리',
        trainingPlan: '교육 계획',
        trainingCourse: '교육 과정',
        trainingRecord: '교육 기록',
        trainingEvaluation: '교육 평가'
      },
      performance: {
        _self: '성과 관리',
        performancePlan: '성과 계획',
        performanceAppraisal: '성과 평가',
        performanceReview: '성과 검토',
        performanceImprovement: '성과 개선'
      },
      compensation: {
        _self: '보상 관리',
        salary: '급여 관리',
        bonus: '보너스 관리',
        benefits: '복리후생 관리',
        payroll: '급여 명세서'
      },
      reporting: {
        _self: '보고서 및 분석',
        employeeReports: '직원 보고서',
        recruitmentReports: '채용 보고서',
        trainingReports: '교육 보고서',
        performanceReports: '성과 보고서',
        compensationReports: '보상 보고서'
      }
    }
  }
}
