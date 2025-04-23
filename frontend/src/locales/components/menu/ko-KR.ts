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
      title: '구성 요소',
      icons: '아이콘'
    },
    about: {
      title: '회사 소개',
      privacy: '개인정보 보호정책',
      terms: '서비스 약관',
      index: 'Hbt 소개'
    },
    admin: {
      _self: '시스템 관리',
      config: '시스템 설정',
      language: '언어 관리',
      dict: '사전 유형',

    },
    identity: {
      _self: '인증 관리',
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
      dbdifflog: '데이터베이스 차이 로그',
      exceptionlog: '예외 로그'
    },
    workflow: {
      _self: '워크플로우',
      definition: '워크플로우 정의',
      instance: '워크플로우 인스턴스',
      task: '작업',
      node: '노드',
      variable: '변수',
      history: '히스토리'
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
      file: '파일 관리',
      mail: '메일 관리',
      mailTmpl: '메일 템플릿',
      notice: '공지사항',
      task: '작업',
      schedule: '일정 관리'
    },
    finance: {
      _self: '재무',
      management: {
        _self: '관리 회계',
        cost: {
          _self: '비용 관리',
          costFactors: '비용 요소',
          costCenter: '비용 센터',
          profitCenter: '이익 센터',
          productCost: '제품 비용',
          activityType: '활동 유형',
          internalOrder: '내부 주문'
        },
        planning: {
          _self: '계획 관리',
          costPlanning: '비용 계획',
          profitPlanning: '이익 계획',
          budgetControl: '예산 관리'
        },
        reporting: {
          _self: '보고 및 분석',
          costReports: '비용 보고서',
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
          journalEntry: '분개 입력',
          reconciliation: '조정',
          closing: '기말 결산'
        },
        accountsReceivable: {
          _self: '매출 채권',
          customer: '고객 관리',
          invoice: '고객 송장',
          payment: '고객 결제',
          creditControl: '신용 관리'
        },
        accountsPayable: {
          _self: '매입 채무',
          supplier: '공급업체 관리',
          invoice: '공급업체 송장',
          payment: '공급업체 결제',
          agingReport: '연령 분석 보고서'
        },
        assetAccounting: {
          _self: '자산 회계',
          assets: '고정 자산',
          depreciation: '감가상각 관리',
          assetTransfer: '자산 이전',
          assetRetirement: '자산 폐기'
        },
        tax: {
          _self: '세무 관리',
          taxCodes: '세금 코드',
          taxReporting: '세금 보고서',
          taxPayments: '세금 납부'
        },
        financialReporting: {
          _self: '재무 보고서',
          balanceSheet: '대차대조표',
          profitAndLoss: '손익 계산서',
          cashFlow: '현금 흐름표'
        }
      }
    },
    logistics: {
      _self: '물류',
      sales: {
        _self: '판매 관리',
        customer: {
          _self: '고객 관리',
          client: '고객',
          customers: '고객 목록',
          creditControl: '신용 관리'
        },
        order: {
          _self: '주문 관리',
          order: '판매 주문',
          orderDetail: '주문 세부사항',
          orderTracking: '주문 추적'
        },
        delivery: {
          _self: '배송 관리',
          delivery: '배송 문서',
          deliveryDetail: '배송 세부사항',
          shipping: '운송 관리'
        },
        billing: {
          _self: '청구 관리',
          invoice: '송장 관리',
          invoiceDetail: '송장 세부사항',
          payment: '결제 관리'
        },
        reporting: {
          _self: '보고 및 분석',
          salesReports: '판매 보고서',
          performanceAnalysis: '성과 분석'
        }
      },
      production: {
        _self: '생산 관리',
        bom: '자재 명세서 (BOM)',
        routing: '라우팅',
        workOrder: {
          _self: '생산 지시서',
          create: '생산 지시서 생성',
          manage: '생산 지시서 관리',
          release: '생산 지시서 릴리스',
          complete: '생산 지시서 완료'
        },
        capacityPlanning: {
          _self: '용량 계획',
          workCenter: '작업 센터',
          capacityEvaluation: '용량 평가',
          capacityLeveling: '용량 평준화'
        },
        productionScheduling: {
          _self: '생산 일정 계획',
          schedule: '일정 계획',
          reschedule: '재일정 계획'
        },
        productionExecution: {
          _self: '생산 실행',
          confirm: '생산 확인',
          goodsIssue: '출고',
          goodsReceipt: '입고'
        },
        productionReporting: {
          _self: '생산 보고서',
          orderReports: '지시서 보고서',
          capacityReports: '용량 보고서',
          efficiencyReports: '효율 보고서'
        },
        qualityManagement: {
          _self: '품질 관리',
          inspectionLot: '검사 로트',
          resultsRecording: '결과 기록',
          defectRecording: '결함 기록'
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
          stockTransfer: '재고 이동',
          stockAdjustment: '재고 조정',
          stockCheck: '재고 확인'
        },
        purchase: {
          _self: '구매 관리',
          purchaseRequisition: '구매 요청',
          purchaseOrder: '구매 주문',
          purchaseOrderDetail: '구매 주문 세부사항',
          supplier: '공급업체 관리'
        },
        inventoryManagement: {
          _self: '재고 관리',
          goodsReceipt: '입고',
          goodsIssue: '출고',
          transferPosting: '이동 전표',
          stockOverview: '재고 개요'
        },
        valuation: {
          _self: '자재 평가',
          priceControl: '가격 관리',
          standardPrice: '표준 가격',
          movingAveragePrice: '이동 평균 가격'
        },
        reporting: {
          _self: '보고 및 분석',
          stockReports: '재고 보고서',
          purchaseReports: '구매 보고서',
          inventoryReports: '재고 분석 보고서'
        }
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
        qualityNotifications: '품질 알림',
        correctiveActions: '시정 조치'
      },
      qualityReporting: {
        _self: '품질 보고서',
        inspectionReports: '검사 보고서',
        defectReports: '결함 보고서',
        qualityAnalysis: '품질 분석'
      }
    },
    service: {
      _self: '고객 서비스',
      serviceOrder: {
        _self: '서비스 지시서',
        create: '서비스 지시서 생성',
        manage: '서비스 지시서 관리',
        complete: '서비스 지시서 완료',
        cancel: '서비스 지시서 취소'
      },
      serviceContract: {
        _self: '서비스 계약',
        create: '서비스 계약 생성',
        manage: '서비스 계약 관리',
        renew: '서비스 계약 갱신',
        terminate: '서비스 계약 종료'
      },
      customerInteraction: {
        _self: '고객 상호작용',
        inquiries: '고객 문의',
        complaints: '고객 불만',
        feedback: '고객 피드백'
      },
      serviceExecution: {
        _self: '서비스 실행',
        schedule: '서비스 일정',
        dispatch: '서비스 파견',
        execution: '서비스 실행',
        confirmation: '서비스 확인'
      },
      serviceReporting: {
        _self: '서비스 보고서',
        orderReports: '서비스 지시서 보고서',
        contractReports: '서비스 계약 보고서',
        performanceReports: '서비스 성과 보고서'
      }
    },
    equipment: {
      _self: '설비 관리',
      equipmentMaster: '설비 마스터 데이터',
      maintenancePlanning: {
        _self: '유지보수 계획',
        preventiveMaintenance: '예방 유지보수',
        maintenanceTaskList: '유지보수 작업 목록',
        scheduling: '유지보수 일정'
      },
      maintenanceExecution: {
        _self: '유지보수 실행',
        workOrder: '유지보수 작업 지시서',
        confirmation: '유지보수 확인',
        breakdownMaintenance: '고장 유지보수'
      },
      maintenanceReporting: {
        _self: '유지보수 보고서',
        equipmentReports: '설비 보고서',
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
    humanResources: {
      _self: '인적 자원 관리',
      employeeManagement: {
        _self: '직원 관리',
        employeeMaster: '직원 마스터 데이터',
        attendance: '출근 관리',
        leave: '휴가 관리',
        payroll: '급여 관리',
        contractManagement: '계약 관리'
      },
      recruitment: {
        _self: '채용 관리',
        jobPosting: '채용 공고',
        candidateManagement: '후보자 관리',
        interviewScheduling: '면접 일정',
        offerManagement: '채용 제안 관리'
      },
      training: {
        _self: '교육 관리',
        trainingPlan: '교육 계획',
        trainingExecution: '교육 실행',
        trainingEvaluation: '교육 평가'
      },
      performance: {
        _self: '성과 관리',
        goalSetting: '목표 설정',
        performanceReview: '성과 검토',
        feedback: '피드백 관리'
      },
      reporting: {
        _self: '인사 보고서',
        employeeReports: '직원 보고서',
        attendanceReports: '출근 보고서',
        payrollReports: '급여 보고서',
        performanceReports: '성과 보고서'
      }
    }
  }
}
