export default {
  menu: {
    home: 'Startseite',
    dashboard: {
      title: 'Dashboard',
      workplace: 'Arbeitsplatz',
      analysis: 'Analyse',
      monitor: 'Monitor'
    },
    components: {
      title: 'Komponenten',
      icons: 'Symbole'
    },
    about: {
      title: 'Über uns',
      privacy: 'Datenschutzrichtlinie',
      terms: 'Nutzungsbedingungen',
      index: 'Über Hbt'
    },

    identity: {
      _self: 'Identitätsauthentifizierung',
      user: 'Benutzerverwaltung',
      role: 'Rollenverwaltung',
      dept: 'Abteilungsverwaltung',
      post: 'Positionsverwaltung',
      menu: 'Menüverwaltung',
      tenant: 'Mandantenverwaltung',
      oauth: 'OAuth-Verwaltung',
      profile: 'Persönliche Informationen',
      changePassword: 'Passwort ändern'
    },
    audit: {
      _self: 'Audit-Protokolle',
      operlog: 'Betriebsprotokoll',
      loginlog: 'Anmeldeprotokoll',
      sqldifflog: 'SQL-Differenzprotokoll',
      exceptionlog: 'Ausnahmeprotokoll',
      auditlog: 'Audit-Protokoll',
      quartzlog: 'Aufgabenprotokoll',
      server: 'Server-Monitor'
    },
    workflow: {
      _self: 'Workflow',
      engine:{
        _self: 'Prozess-Engine',
        monitor: 'Prozess-Monitor',
        todo: 'Zu erledigen',
        done: 'Erledigte Aufgaben',
        signoff: 'Prozess-Genehmigung',
        execution: 'Prozess-Ausführung',
        designer: 'Prozess-Designer'
      },
      manage:{
        _self: 'Prozessverwaltung',
        form: 'Formularverwaltung',
        scheme: 'Prozess-Schema',
        instance: 'Prozess-Instanz',
        oper: 'Instanz-Operationen',
        trans: 'Instanz-Fluss'
      }
    },
    signalr: {
      _self: 'Echtzeit-Kommunikation',
      online: 'Online-Benutzer',
      message: 'Online-Nachrichten'
    },
    generator: {
      _self: 'Code-Generator',
      table: 'Datenbanktabellen',
      tableDefine: 'Tabellenspalten-Definition',
      template: 'Code-Vorlagen',
      config: 'Generierungskonfiguration',
      api: 'API-Dokumentation'
    },
    routine: {
      _self: 'Tägliches Büro',
      core: {
        _self: 'Kernservices',
        numberrule: 'Nummerierungsregeln',
        config: 'Systemkonfiguration',
        language: 'Sprachverwaltung',
        dict: 'Wörterbuchverwaltung'
      },
      contract: {
        _self: 'Vertragsverwaltung',
        template: {
          _self: 'Vertragsvorlagen',
          manage: 'Vorlagenverwaltung',
          category: 'Vorlagenkategorien'
        },
        draft: {
          _self: 'Vertragserstellung',
          apply: 'Erstellungsantrag',
          my: 'Meine Entwürfe'
        },
        approval: {
          _self: 'Vertragsgenehmigung',
          pending: 'Vertragsgenehmigung',
          approved: 'Genehmigt',
          record: 'Genehmigungsaufzeichnungen'
        },
        execution: {
          _self: 'Vertragsausführung',
          track: 'Ausführungsverfolgung',
          change: 'Änderungsverwaltung',
          payment: 'Zahlungsverwaltung'
        },
        archive: {
          _self: 'Vertragsarchivierung',
          manage: 'Archivverwaltung',
          query: 'Abfrage-Statistiken'
        }
      },
      project: {
        _self: 'Projektverwaltung',
        info: {
          _self: 'Projektinformationen',
          list: 'Projektliste'
        },
        plan: {
          _self: 'Projektplanung',
          request: 'Planungsantrag',
          gantt: 'Projekt-Gantt-Diagramm'
        },
        task: {
          _self: 'Projektaufgaben',
          assign: 'Aufgabenzuweisung',
          track: 'Aufgabenverfolgung',
          board: 'Aufgabenboard'
        },
        resource: {
          _self: 'Projektressourcen',
          personnel: 'Personalverwaltung',
          equipment: 'Ausrüstungsverwaltung',
          budget: 'Budgetverwaltung'
        },
        monitor: {
          _self: 'Projektüberwachung',
          progress: 'Fortschrittsüberwachung',
          quality: 'Qualitätsüberwachung',
          risk: 'Risikoüberwachung'
        }
      },
      quartz: {
        _self: 'Aufgabenplanung',
        job: {
          _self: 'Aufgabenverwaltung',
          config: 'Aufgabenkonfiguration',
          list: 'Aufgabenliste',
          status: 'Aufgabenstatus'
        },
        schedule: {
          _self: 'Aufgabenplanung',
          config: 'Planungskonfiguration',
          monitor: 'Planungsmonitor',
          stats: 'Planungsstatistiken'
        }
      },
      schedule: {
        _self: 'Terminverwaltung',
        myschedule: 'Mein Terminplan',
        dashboard: 'Terminplan-Dashboard'
      },
      vehicle: {
        _self: 'Fahrzeugverwaltung',
        my: 'Meine Fahrzeuge',
        application: 'Fahrzeugantrag',
        dashboard: 'Fahrzeug-Dashboard',
        manage: {
          _self: 'Fahrzeugverwaltung',
          info: 'Fahrzeuginformationen',
          maintenance: 'Fahrzeugwartung'
        }
      },
      email: {
        _self: 'E-Mail-Verwaltung',
        inbox: 'Posteingang',
        drafts: 'Entwürfe',
        sent: 'Gesendet',
        trash: 'Papierkorb',
        template: 'E-Mail-Vorlagen'
      },
      meeting: {
        _self: 'Besprechungsverwaltung',
        room: 'Besprechungsräume',
        mymeeting: 'Meine Besprechungen',
        booking: 'Besprechungsbuchung',
        dashboard: 'Besprechungs-Dashboard'
      },
      notice: {
        _self: 'Benachrichtigungen und Ankündigungen',
        message: {
          _self: 'Nachrichtenverwaltung',
          mymessages: 'Meine Nachrichten',
          list: 'Nachrichtenboard'
        },
        announcement: {
          _self: 'Ankündigungsverwaltung',
          signoff: 'Ankündigungen bestätigen',
          list: 'Ankündigungsliste'
        },
        notification: {
          _self: 'Benachrichtigungsverwaltung',
          ack: 'Gelesene Benachrichtigungen',
          list: 'Benachrichtigungsliste'
        }
      },
      hr: {
        _self: 'Personal und Anwesenheit',
        recruitment: {
          _self: 'Rekrutierungsverwaltung',
          apply: 'Rekrutierungsantrag',
          approval: 'Rekrutierungsgenehmigung',
          list: 'Rekrutierungsliste'
        },
        transfer: {
          _self: 'Versetzungsverwaltung',
          apply: 'Versetzungsantrag',
          approval: 'Versetzungsgenehmigung',
          list: 'Versetzungsliste'
        },
        leave: {
          _self: 'Urlaubsverwaltung',
          apply: 'Urlaubsantrag',
          approval: 'Urlaubsgenehmigung',
          list: 'Urlaubsliste'
        },
        trip: {
          _self: 'Dienstreiseverwaltung',
          apply: 'Dienstreiseantrag',
          approval: 'Dienstreisegenehmigung',
          list: 'Dienstreiseliste'
        },
        overtime: {
          _self: 'Überstundenverwaltung',
          apply: 'Überstundenantrag',
          approval: 'Überstundengenehmigung',
          list: 'Überstundenliste'
        }
      },
      expense: {
        _self: 'Ausgabenverwaltung',
        daily: {
          _self: 'Tägliche Ausgaben',
          apply: 'Ausgabenantrag',
          approve: 'Ausgabengenehmigung',
          list: 'Ausgabenliste'
        },
        travel: {
          _self: 'Reiseausgaben',
          apply: 'Reiseausgabenantrag',
          approve: 'Reiseausgabengenehmigung',
          list: 'Reiseausgabenliste'
        }
      },
      document: {
        _self: 'Dokumentenverwaltung',
        news: {
          _self: 'Nachrichtenverwaltung',
        },
        regulation: {
          _self: 'Vorschriften und Regeln',
          manage: 'Vorschriftenverwaltung',
          control: 'Vorschriftenkontrolle',
        },
        file: {
          _self: 'Tägliche Dateien',
        },
        iso: {
          _self: 'ISO-Dateien',
          manage: 'Dateiverwaltung',
          control: 'Dateikontrolle',
        },
        official: {
          _self: 'Offizielle Dokumentenverwaltung',
          manage: 'Dokumentenverwaltung',
          issuance: 'Dokumentenkontrolle',
        },
        law: {
          _self: 'Gesetze und Vorschriften',
        }
      },
      officesupplies: {
        _self: 'Büromaterial',
        inventory: {
          _self: 'Bestandsverwaltung',
          requisition: 'Beschaffungsverwaltung',
          inbound: 'Eingangsverwaltung',
          stocktaking: 'Inventurverwaltung'
        },
        usage: {
          _self: 'Nutzungsverwaltung',
          apply: 'Nutzungsantrag',
          approve: 'Nutzungsgenehmigung',
          list: 'Nutzungsaufzeichnungen'
        }
      },
      book: {
        _self: 'Buchverwaltung',
        inventory: {
          _self: 'Bestandsverwaltung',
          requisition: 'Beschaffungsverwaltung',
          inbound: 'Eingangsverwaltung',
          list: 'Buchliste',
          stocktaking: 'Inventurverwaltung'
        },
        usage: {
          _self: 'Nutzungsverwaltung',
          card: 'Bibliothekskarte',
          borrow: 'Ausleihen',
          return: 'Zurückgeben'
        }
      },
      medical: {
        _self: 'Medizinische Verwaltung',
        medicine: {
          _self: 'Bestandsverwaltung',
          requisition: 'Beschaffungsverwaltung',
          inbound: 'Eingangsverwaltung',
          list: 'Arzneimittelliste',
          stocktaking: 'Inventurverwaltung'
        },
        usage: {
          _self: 'Nutzungsverwaltung',
          archive: 'Archiv',
          receive: 'Arzneimittel erhalten',
          cost: 'Kosten'
        }
      }
    },
    accounting: {
      _self: 'Buchhaltung',
      financial: {
        _self: 'Management-Buchhaltung',
        company: 'Unternehmensinformationen',
        account: 'Kontenplan',
        companyaccount: 'Unternehmenskonten',
        ledger: 'Hauptbuch',
        payable: 'Verbindlichkeiten',
        receivable: 'Forderungen',
        fixedasset: 'Anlagevermögen',
        bank: 'Bankinformationen'
      },
      controlling: {
        _self: 'Controlling',
        costelement: 'Kostenelemente',
        costcenter: 'Kostenstellen',
        profitcenter: 'Erfolgsstellen',
        accountsReceivable: 'Forderungen',
        accountsPayable: 'Verbindlichkeiten',
        assetAccounting: 'Anlagenbuchhaltung',
        tax: 'Steuerverwaltung',
        financialReporting: 'Finanzberichte'
      },
      budget: {
        _self: 'Integriertes Budget',
        formulation: {
          _self: 'Budgetformulierung',
          sales: {
            _self: 'Umsatzbudget',
            cost: 'Umsatzkosten',
            rolling: 'Rollierender Umsatz'
          },
          production: {
            _self: 'Produktionsbudget',
            auxiliary: 'Produktionshilfsstoffe',
            labor: 'Produktionsarbeit',
            manufacturing: 'Produktionsfertigung'
          },
          cost: {
            _self: 'Kostenbudget',
            directmaterial: 'Direkte Materialien',
            directlabor: 'Direkte Arbeit',
            indirectlabor: 'Indirekte Arbeit',
            manufacturing: 'Fertigungsgemeinkosten'
          },
          expense: {
            _self: 'Ausgabenbudget',
            sales: 'Vertriebsausgaben',
            manage: 'Verwaltungsausgaben',
            financial: 'Finanzausgaben'
          },
          financial: {
            _self: 'Finanzbudget',
            cashflow: 'Cashflow',
            balancesheet: 'Bilanz',
            income: 'Gewinn- und Verlustrechnung'
          }
        },
        control: {
          _self: 'Budgetkontrolle',
          dashboard: 'Budget-Dashboard',
          approval: 'Budgetgenehmigung'
        }
      }
    },
    logistics: {
      _self: 'Logistikverwaltung',
      equipment: {
        _self: 'Ausrüstungsverwaltung',
        master: {
          _self: 'Ausrüstungsdaten',
          list: 'Ausrüstungsinformationen',
          location: 'Funktionsstandort',
          material: 'Materialverknüpfung'
        },
        maintenance: {
          _self: 'Ausrüstungswartung',
          workorder: 'Wartungspläne',
          assign: 'Wartungszuweisung',
          execute: 'Wartungsausführung'
        }
      },
      material: {
        _self: 'Materialverwaltung',
        manage: {
          _self: 'Materialinformationen',
          master: 'Gruppenmaterialien',
          plant: {
            _self: 'Werkinformationen',
            master: 'Werkmaterialien'
          }
        },
        purchase: {
          _self: 'Beschaffungsverwaltung',
          vendor: 'Lieferanteninformationen',
          supplier: 'Zuliefererinformationen',
          price: 'Beschaffungspreise',
          requisition: 'Beschaffungsanfrage',
          order: 'Beschaffungsaufträge'
        },
        sample:{
          _self: 'Probenverwaltung',
          component: 'Komponentenproben',
          product: 'Produktproben'
        },
        drawing: {
          _self: 'Zeichnungsverwaltung',
          design: 'Zeichnungsverwaltung',
          engineering: 'Zeichnungskontrolle',
          gerber: 'Gerber-Dateien',
          coordinate: 'Koordinatendateien',
          assembly: 'Montagezeichnungen',
          structure: 'Strukturdateien',
          impedance: 'Impedanzdateien',
          process: 'Prozessfluss'
        },
        csm: {  
          _self: 'Kunden bereitgestellte Artikel-Verwaltung',
          raw: 'Kunden bereitgestellte Materialien',
          good: 'Kunden bereitgestellte Produkte'
        }
      },
      production: {
        _self: 'Produktionsverwaltung',
        basic: {
          _self: 'Grunddaten',
          bom: 'Stückliste',
          workcenter: 'Arbeitsplätze',   
          routing: 'Arbeitsplan',
          order: 'Produktionsaufträge',
          worktime: 'Produktionszeiten',
          kanban: 'Kanban'
        },
        change: {
          _self: 'Konstruktionsänderungen',
          implementation: 'Änderungsimplementierung',
          techcontact: 'Technischer Kontakt',
          material: 'Materialbestätigung',
          query: 'Änderungsabfrage',
          oldproduct: 'Altproduktkontrolle',
          sop: 'SOP-Bestätigung',
          batch: 'Eingabecharge',
          input: {
            _self: 'Änderungseingabe',
            gijutsu: 'Technische Abteilung',
            seikan: 'Produktionskontrollabteilung',
            koubai: 'Beschaffungsabteilung',
            uketsuke: 'Inspektionsabteilung',
            bukan: 'Abteilungsverwaltung',
            seizou2: 'Produktionsabteilung 2',
            seizou1: 'Produktionsabteilung 1',
            hinkan: 'Qualitätskontrollabteilung',
            seizougijutsu: 'Produktionstechnische Abteilung'
          }
        },       
        output: {
          _self: 'Fertigungsverwaltung',
          workshop1:{
            _self: 'Produktionsabteilung 1',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: 'Produktion',
              modify: 'Änderung',
              rework: 'Nacharbeit'
            },
            defect:{
              _self: 'Fehler',
              epp: 'EPP',
              production: 'Produktion',
              modify: 'Änderung',
              rework: 'Nacharbeit'
            },
            worktime: {
              _self: 'Arbeitszeiten',
              epp: 'EPP',
              production: 'Produktion',
              modify: 'Änderung',
              rework: 'Nacharbeit'
            }
          },
          workshop2:{
            _self: 'Produktionsabteilung 2',
            oph: {
              _self: 'OPH',
              epp: 'EPP',
              production: 'Produktion',
              modify: 'Änderung',
              rework: 'Nacharbeit'
            },
            defect:{
              _self: 'Fehler',
              eppInspection: 'EPP-Inspektion',
              eppRepair: 'EPP-Reparatur',
              productionInspection: 'Produktionsinspektion',
              productionRepair: 'Produktionsreparatur',
              modifyInspection: 'Änderungsinspektion',
              modifyRepair: 'Änderungsreparatur',
              reworkInspection: 'Nacharbeitsinspektion',
              reworkRepair: 'Nacharbeitsreparatur'
            },
            worktime: {
              _self: 'Arbeitszeiten',
              epp: 'EPP',
              production: 'Produktion',
              modify: 'Änderung',
              rework: 'Nacharbeit'
            }
          }
        },
        sop: {
          _self: 'SOP-Verwaltung',
          workshop1: 'Produktionsabteilung 1',
          workshop2: 'Produktionsabteilung 2'
        },
        techcontact: {
          _self: 'Technischer Kontakt',
          epp: 'EPP-Kontakt',
          engineering: 'Ingenieurkontakt',
          external: 'Externer Kontakt'
        }
      },
      project: {
        _self: 'Projektverwaltung',
        define: 'Projektdefinition',
        cost: 'Kostenplanung',
        resource: 'Ressourcenplanung',
        schedule: 'Terminplanung'
      },
      quality: {
        _self: 'Qualitätsverwaltung',
        basic: {
          _self: 'Grunddaten',
          item: 'Inspektionspunkte',
          method: 'Inspektionsmethoden',
          sampling: 'Stichprobenpläne',
          defect: 'Fehlerkategorien',
          rule: 'Beurteilungsregeln',
          category: 'Qualitätskategorien'
        },
        inspection:{
          _self: 'Inspektionsverwaltung',
          receiving: 'Eingangsinspektion',
          process: 'Prozessinspektion',
          storage: 'Lagerinspektion',
          return: 'Rückgabeinspektion'
        },
        trace:{
          _self: 'Rückverfolgbarkeitsverwaltung',
          batch: 'Chargenrückverfolgbarkeit',
          corrective: 'Korrekturmaßnahmen',
          notification: 'Benachrichtigungen',
        },
        cost:{
          _self: 'Qualitätskosten',
          business:'Qualitätsaktivitäten',
          rework:'Nacharbeitsaktivitäten',
          scrap:'Ausschussaktivitäten',
        },
        plan: {
          _self: 'Qualitätsplanung',
          supplier: 'Lieferantenbewertung',
          customer: 'Kundenumfrage'
        },
        item: 'Inspektionspunkte',
        receiving: 'Eingangsinspektion',
        process: 'Prozessinspektion',
        storage: 'Lagerinspektion',
        return: 'Rückgabeinspektion'
      },
      sales: {
        _self: 'Vertriebsverwaltung',
        customer: 'Kundeninformationen',
        client: 'Kundeninformationen',
        price: 'Verkaufspreise',
        order: 'Verkaufsaufträge'
      },
      service: {
        _self: 'Kundenservice',
        cs: {
          _self: 'Kundenservice',
          item: 'Servicepunkte',
          contract: 'Serviceverträge',
          request: 'Serviceanfragen',
          workorder: 'Service-Arbeitsaufträge',
          timesheet: 'Servicezeiten',
          consumption: 'Materialverbrauch',
          outsourcing: 'Outsourcing-Services'
        },
        cc: {
          _self: 'Kundenbeschwerdeverwaltung',
          notice: 'Qualitätsbenachrichtigungen',
          mark: 'Beschwerdedetails',
          analysis: 'Ursachenanalyse',
          corrective: 'Korrekturmaßnahmen',
          return: 'Rückgabe/Austausch-Ausführung',
          followUp: 'Nachbearbeitung'
        }
      }
    },
    hrm: {
      _self: 'Personalwesen',
      attendance: {
        _self: 'Anwesenheitsverwaltung',
        record: 'Anwesenheitsaufzeichnungen',
        holiday: 'Urlaubsverwaltung',
        overtime: 'Überstundenverwaltung',
        compensatory: 'Ausgleichszeitverwaltung'
      },
      benefit: {
        _self: 'Sozialleistungsverwaltung',
        project: 'Sozialleistungsprojekte',
        employee: 'Mitarbeiter-Sozialleistungen'
      },
      employee: {
        _self: 'Personalverwaltung',
        info: 'Personalinformationen',
        contracttype: 'Vertragstypen',
        contract: 'Vertragsverwaltung',
        promotion: 'Beförderungsverwaltung',
        promotionhistory: 'Beförderungshistorie',
        resignation: 'Kündigungsverwaltung',
        transfer: 'Personalliste',
        transferhistory: 'Versetzungshistorie'
      },
      leave: {
        _self: 'Urlaubsverwaltung',
        type: 'Urlaubstypen',
        employee: 'Mitarbeiterurlaub'
      },
      organization: {
        _self: 'Organisationsverwaltung',
        positioncategory: 'Positionenkategorien',
        company: 'Unternehmensinformationen',
        department: 'Abteilungsinformationen',
        position: 'Positionsinformationen'
      },
      performance: {
        _self: 'Leistungsverwaltung',
        assessmentitem: 'Bewertungspunkte',
        assessment: 'Leistungsbewertung'
      },
      recruitment: {
        _self: 'Rekrutierungsverwaltung',
        application: 'Stellenbewerbungen',
        posting: 'Stellenausschreibungen',
        candidate: 'Kandidatenverwaltung',
        interview: 'Bewerbungsgesprächsverwaltung'
      },
      salary: {
        _self: 'Gehaltsverwaltung',
        employee: 'Mitarbeitergehälter',
        housing: 'Wohnungsfonds',
        housinglevel: 'Sozialversicherung',
        tax: 'Steuerverwaltung',
        taxlevel: 'Steuersätze',
        structure: 'Gehaltsstruktur',
        social: 'Sozialversicherung',
        socialbase: 'Sozialversicherungsbasis'
      },
      training: {
        _self: 'Schulungsverwaltung',
        category: 'Schulungskategorien',
        course: 'Schulungskurse',
        record: 'Mitarbeiterschulungen'
      },
      report: {
        _self: 'Berichtsverwaltung',
        employeeinfo: 'Personalinformationen',
        resignation: 'Kündigungsberichte',
        transfer: 'Versetzungsberichte',
        promotion: 'Beförderungsberichte',
        training: 'Schulungsberichte',
        salary: 'Gehaltsberichte',
        performance: 'Leistungsberichte',
        attendance: 'Anwesenheitsberichte',
        benefit: 'Sozialleistungsberichte',
        recruitment: 'Rekrutierungsberichte'
      }
    }
  }
} 