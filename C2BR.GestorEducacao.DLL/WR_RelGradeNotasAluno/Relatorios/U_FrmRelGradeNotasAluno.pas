unit U_FrmRelGradeNotasAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelGradeNotasAluno = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRShape7: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape27: TQRShape;
    QRShape29: TQRShape;
    QRShape59: TQRShape;
    QRShape60: TQRShape;
    QRShape61: TQRShape;
    QRShape62: TQRShape;
    QRShape63: TQRShape;
    QRShape64: TQRShape;
    QRShape65: TQRShape;
    QRShape66: TQRShape;
    QRShape67: TQRShape;
    QRShape68: TQRShape;
    QRShape69: TQRShape;
    QRShape70: TQRShape;
    QRShape71: TQRShape;
    QRShape72: TQRShape;
    QRShape73: TQRShape;
    N1B1M1: TQRLabel;
    N2B1M1: TQRLabel;
    M1B1: TQRLabel;
    M2B1: TQRLabel;
    N2B1M2: TQRLabel;
    N1B1M2: TQRLabel;
    M3B1: TQRLabel;
    N2B1M3: TQRLabel;
    N1B1M3: TQRLabel;
    N1B1M4: TQRLabel;
    N2B1M4: TQRLabel;
    M4B1: TQRLabel;
    N1B1M5: TQRLabel;
    N2B1M5: TQRLabel;
    M5B1: TQRLabel;
    N1B1M6: TQRLabel;
    N2B1M6: TQRLabel;
    M6B1: TQRLabel;
    M10B1: TQRLabel;
    N2B1M10: TQRLabel;
    N1B1M10: TQRLabel;
    M9B1: TQRLabel;
    N2B1M9: TQRLabel;
    N1B1M9: TQRLabel;
    N2B1M8: TQRLabel;
    M8B1: TQRLabel;
    N1B1M8: TQRLabel;
    M7B1: TQRLabel;
    N2B1M7: TQRLabel;
    N1B1M7: TQRLabel;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioCO_ALU_CAD: TStringField;
    QRShape86: TQRShape;
    QRShape87: TQRShape;
    QRShape88: TQRShape;
    M12B1: TQRLabel;
    N2B1M12: TQRLabel;
    N1B1M12: TQRLabel;
    M11B1: TQRLabel;
    N2B1M11: TQRLabel;
    N1B1M11: TQRLabel;
    QRShape89: TQRShape;
    QRShape90: TQRShape;
    QRShape91: TQRShape;
    QRShape74: TQRShape;
    QRBand3: TQRBand;
    QRMLegenda1: TQRMemo;
    QRLabel31: TQRLabel;
    QRLabel38: TQRLabel;
    QRLPage: TQRLabel;
    QRGroup1: TQRGroup;
    QRLParametros: TQRLabel;
    QRLabel1: TQRLabel;
    M1: TQRLabel;
    M2: TQRLabel;
    M3: TQRLabel;
    M4: TQRLabel;
    M5: TQRLabel;
    M6: TQRLabel;
    M7: TQRLabel;
    M8: TQRLabel;
    M9: TQRLabel;
    M10: TQRLabel;
    M11: TQRLabel;
    M12: TQRLabel;
    QRShape76: TQRShape;
    QRShape75: TQRShape;
    QRShape28: TQRShape;
    QRShape26: TQRShape;
    QRShape23: TQRShape;
    QRShape20: TQRShape;
    QRShape17: TQRShape;
    QRShape14: TQRShape;
    QRShape11: TQRShape;
    QRShape8: TQRShape;
    QRShape5: TQRShape;
    QRShape2: TQRShape;
    QRLabel39: TQRLabel;
    QRShape82: TQRShape;
    QRShape81: TQRShape;
    QRLabel40: TQRLabel;
    QRShape80: TQRShape;
    QRLabel41: TQRLabel;
    QRLabel42: TQRLabel;
    QRShape79: TQRShape;
    QRShape78: TQRShape;
    QRLabel43: TQRLabel;
    QRShape77: TQRShape;
    QRLabel44: TQRLabel;
    QRShape3: TQRShape;
    QRLabel26: TQRLabel;
    QRShape4: TQRShape;
    QRLabel27: TQRLabel;
    QRShape58: TQRShape;
    QRLabel28: TQRLabel;
    QRShape57: TQRShape;
    QRLabel29: TQRLabel;
    QRShape56: TQRShape;
    QRLabel30: TQRLabel;
    QRShape55: TQRShape;
    QRLabel36: TQRLabel;
    QRShape54: TQRShape;
    QRLabel14: TQRLabel;
    QRShape53: TQRShape;
    QRLabel15: TQRLabel;
    QRShape52: TQRShape;
    QRLabel25: TQRLabel;
    QRShape51: TQRShape;
    QRLabel16: TQRLabel;
    QRShape50: TQRShape;
    QRLabel17: TQRLabel;
    QRShape49: TQRShape;
    QRLabel18: TQRLabel;
    QRShape48: TQRShape;
    QRLabel19: TQRLabel;
    QRShape47: TQRShape;
    QRLabel20: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel21: TQRLabel;
    QRShape46: TQRShape;
    QRShape45: TQRShape;
    QRShape44: TQRShape;
    QRLabel24: TQRLabel;
    QRShape43: TQRShape;
    QRLabel23: TQRLabel;
    QRShape42: TQRShape;
    QRLabel8: TQRLabel;
    QRShape41: TQRShape;
    QRLabel9: TQRLabel;
    QRShape40: TQRShape;
    QRLabel10: TQRLabel;
    QRShape39: TQRShape;
    QRLabel11: TQRLabel;
    QRShape37: TQRShape;
    QRLabel12: TQRLabel;
    QRShape36: TQRShape;
    QRLabel13: TQRLabel;
    QRShape35: TQRShape;
    QRLabel5: TQRLabel;
    QRShape34: TQRShape;
    QRLabel6: TQRLabel;
    QRShape33: TQRShape;
    QRLabel7: TQRLabel;
    QRShape32: TQRShape;
    QRLabel4: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape31: TQRShape;
    QRLabel2: TQRLabel;
    QRShape30: TQRShape;
    QRShape6: TQRShape;
    QRShape1: TQRShape;
    QRLabel45: TQRLabel;
    QRLabel33: TQRLabel;
    N1B2M1: TQRLabel;
    QRShape126: TQRShape;
    N2B2M1: TQRLabel;
    QRShape127: TQRShape;
    M1B2: TQRLabel;
    QRShape128: TQRShape;
    N1B2M2: TQRLabel;
    QRShape129: TQRShape;
    N2B2M2: TQRLabel;
    QRShape130: TQRShape;
    M2B2: TQRLabel;
    QRShape131: TQRShape;
    N1B2M3: TQRLabel;
    QRShape132: TQRShape;
    N2B2M3: TQRLabel;
    QRShape133: TQRShape;
    M3B2: TQRLabel;
    QRShape134: TQRShape;
    N1B2M4: TQRLabel;
    QRShape135: TQRShape;
    N2B2M4: TQRLabel;
    QRShape136: TQRShape;
    M4B2: TQRLabel;
    QRShape137: TQRShape;
    N1B2M5: TQRLabel;
    QRShape138: TQRShape;
    N2B2M5: TQRLabel;
    QRShape139: TQRShape;
    M5B2: TQRLabel;
    QRShape140: TQRShape;
    N1B2M6: TQRLabel;
    QRShape141: TQRShape;
    N2B2M6: TQRLabel;
    QRShape142: TQRShape;
    M6B2: TQRLabel;
    QRShape143: TQRShape;
    N1B2M7: TQRLabel;
    QRShape144: TQRShape;
    N2B2M7: TQRLabel;
    QRShape145: TQRShape;
    M7B2: TQRLabel;
    QRShape146: TQRShape;
    N1B2M8: TQRLabel;
    QRShape147: TQRShape;
    N2B2M8: TQRLabel;
    QRShape148: TQRShape;
    M8B2: TQRLabel;
    QRShape149: TQRShape;
    N1B2M9: TQRLabel;
    QRShape150: TQRShape;
    N2B2M9: TQRLabel;
    QRShape151: TQRShape;
    M9B2: TQRLabel;
    QRShape152: TQRShape;
    N1B2M10: TQRLabel;
    QRShape153: TQRShape;
    N2B2M10: TQRLabel;
    QRShape154: TQRShape;
    M10B2: TQRLabel;
    QRShape155: TQRShape;
    N1B2M11: TQRLabel;
    QRShape156: TQRShape;
    N2B2M11: TQRLabel;
    QRShape157: TQRShape;
    M11B2: TQRLabel;
    QRShape158: TQRShape;
    N1B2M12: TQRLabel;
    QRShape159: TQRShape;
    N2B2M12: TQRLabel;
    QRShape160: TQRShape;
    M12B2: TQRLabel;
    QRShape161: TQRShape;
    N1B3M1: TQRLabel;
    QRShape162: TQRShape;
    N2B3M1: TQRLabel;
    QRShape163: TQRShape;
    M1B3: TQRLabel;
    QRShape164: TQRShape;
    N1B3M2: TQRLabel;
    QRShape165: TQRShape;
    N2B3M2: TQRLabel;
    QRShape166: TQRShape;
    M2B3: TQRLabel;
    QRShape167: TQRShape;
    N1B3M3: TQRLabel;
    QRShape168: TQRShape;
    N2B3M3: TQRLabel;
    QRShape169: TQRShape;
    M3B3: TQRLabel;
    QRShape170: TQRShape;
    N1B3M4: TQRLabel;
    QRShape171: TQRShape;
    N2B3M4: TQRLabel;
    QRShape172: TQRShape;
    M4B3: TQRLabel;
    QRShape173: TQRShape;
    N1B3M5: TQRLabel;
    QRShape174: TQRShape;
    N2B3M5: TQRLabel;
    QRShape175: TQRShape;
    M5B3: TQRLabel;
    QRShape176: TQRShape;
    N1B3M6: TQRLabel;
    QRShape177: TQRShape;
    N2B3M6: TQRLabel;
    QRShape178: TQRShape;
    M6B3: TQRLabel;
    QRShape179: TQRShape;
    N1B3M7: TQRLabel;
    QRShape180: TQRShape;
    N2B3M7: TQRLabel;
    QRShape181: TQRShape;
    M7B3: TQRLabel;
    QRShape182: TQRShape;
    N1B3M8: TQRLabel;
    QRShape183: TQRShape;
    N2B3M8: TQRLabel;
    QRShape184: TQRShape;
    M8B3: TQRLabel;
    QRShape185: TQRShape;
    N1B3M9: TQRLabel;
    QRShape186: TQRShape;
    N2B3M9: TQRLabel;
    QRShape187: TQRShape;
    M9B3: TQRLabel;
    QRShape188: TQRShape;
    N1B3M10: TQRLabel;
    QRShape189: TQRShape;
    N2B3M10: TQRLabel;
    QRShape190: TQRShape;
    M10B3: TQRLabel;
    QRShape191: TQRShape;
    N1B3M11: TQRLabel;
    QRShape192: TQRShape;
    N2B3M11: TQRLabel;
    QRShape193: TQRShape;
    M11B3: TQRLabel;
    QRShape194: TQRShape;
    N1B3M12: TQRLabel;
    QRShape195: TQRShape;
    N2B3M12: TQRLabel;
    QRShape196: TQRShape;
    M12B3: TQRLabel;
    QRShape197: TQRShape;
    N1B4M1: TQRLabel;
    QRShape198: TQRShape;
    N2B4M1: TQRLabel;
    QRShape199: TQRShape;
    M1B4: TQRLabel;
    QRShape200: TQRShape;
    N1B4M2: TQRLabel;
    QRShape201: TQRShape;
    N2B4M2: TQRLabel;
    QRShape202: TQRShape;
    M2B4: TQRLabel;
    QRShape203: TQRShape;
    N1B4M3: TQRLabel;
    QRShape204: TQRShape;
    N2B4M3: TQRLabel;
    QRShape205: TQRShape;
    M3B4: TQRLabel;
    QRShape206: TQRShape;
    N1B4M4: TQRLabel;
    QRShape207: TQRShape;
    N2B4M4: TQRLabel;
    QRShape208: TQRShape;
    M4B4: TQRLabel;
    QRShape209: TQRShape;
    N1B4M5: TQRLabel;
    QRShape210: TQRShape;
    N2B4M5: TQRLabel;
    QRShape211: TQRShape;
    M5B4: TQRLabel;
    QRShape212: TQRShape;
    N1B4M6: TQRLabel;
    QRShape213: TQRShape;
    N2B4M6: TQRLabel;
    QRShape214: TQRShape;
    M6B4: TQRLabel;
    QRShape215: TQRShape;
    N1B4M7: TQRLabel;
    QRShape216: TQRShape;
    N2B4M7: TQRLabel;
    QRShape217: TQRShape;
    M7B4: TQRLabel;
    QRShape218: TQRShape;
    N1B4M8: TQRLabel;
    QRShape219: TQRShape;
    N2B4M8: TQRLabel;
    QRShape220: TQRShape;
    M8B4: TQRLabel;
    QRShape221: TQRShape;
    N1B4M9: TQRLabel;
    QRShape222: TQRShape;
    N2B4M9: TQRLabel;
    QRShape223: TQRShape;
    M9B4: TQRLabel;
    QRShape224: TQRShape;
    N1B4M10: TQRLabel;
    QRShape225: TQRShape;
    N2B4M10: TQRLabel;
    QRShape226: TQRShape;
    M10B4: TQRLabel;
    QRShape227: TQRShape;
    N1B4M11: TQRLabel;
    QRShape228: TQRShape;
    N2B4M11: TQRLabel;
    QRShape229: TQRShape;
    M11B4: TQRLabel;
    QRShape230: TQRShape;
    N1B4M12: TQRLabel;
    QRShape231: TQRShape;
    N2B4M12: TQRLabel;
    QRShape232: TQRShape;
    M12B4: TQRLabel;
    QRShape233: TQRShape;
    M1N1M1: TQRLabel;
    QRShape234: TQRShape;
    M2N2M1: TQRLabel;
    QRShape235: TQRShape;
    MDM1: TQRLabel;
    QRShape236: TQRShape;
    M1N1M2: TQRLabel;
    QRShape237: TQRShape;
    M2N2M2: TQRLabel;
    QRShape238: TQRShape;
    MDM2: TQRLabel;
    QRShape239: TQRShape;
    M1N1M3: TQRLabel;
    QRShape240: TQRShape;
    M2N2M3: TQRLabel;
    QRShape241: TQRShape;
    MDM3: TQRLabel;
    QRShape242: TQRShape;
    M1N1M4: TQRLabel;
    QRShape243: TQRShape;
    M2N2M4: TQRLabel;
    QRShape244: TQRShape;
    MDM4: TQRLabel;
    QRShape245: TQRShape;
    M1N1M5: TQRLabel;
    QRShape246: TQRShape;
    M2N2M5: TQRLabel;
    QRShape247: TQRShape;
    MDM5: TQRLabel;
    QRShape248: TQRShape;
    M1N1M6: TQRLabel;
    QRShape249: TQRShape;
    M2N2M6: TQRLabel;
    QRShape250: TQRShape;
    MDM6: TQRLabel;
    QRShape251: TQRShape;
    M1N1M7: TQRLabel;
    QRShape252: TQRShape;
    M2N2M7: TQRLabel;
    QRShape253: TQRShape;
    MDM7: TQRLabel;
    QRShape254: TQRShape;
    M1N1M8: TQRLabel;
    QRShape255: TQRShape;
    M2N2M8: TQRLabel;
    QRShape256: TQRShape;
    MDM8: TQRLabel;
    QRShape257: TQRShape;
    M1N1M9: TQRLabel;
    QRShape258: TQRShape;
    M2N2M9: TQRLabel;
    QRShape259: TQRShape;
    MDM9: TQRLabel;
    QRShape260: TQRShape;
    M1N1M10: TQRLabel;
    QRShape261: TQRShape;
    M2N2M10: TQRLabel;
    QRShape262: TQRShape;
    MDM10: TQRLabel;
    QRShape263: TQRShape;
    M1N1M11: TQRLabel;
    QRShape264: TQRShape;
    M2N2M11: TQRLabel;
    QRShape265: TQRShape;
    MDM11: TQRLabel;
    QRShape266: TQRShape;
    M1N1M12: TQRLabel;
    QRShape267: TQRShape;
    M2N2M12: TQRLabel;
    QRShape268: TQRShape;
    MDM12: TQRLabel;
    QRShape269: TQRShape;
    QRLabel35: TQRLabel;
    QRShape38: TQRShape;
    QRLabel37: TQRLabel;
    QRShape83: TQRShape;
    QRLabel190: TQRLabel;
    QRShape84: TQRShape;
    QRShape85: TQRShape;
    QRLabel191: TQRLabel;
    QRMLegenda2: TQRMemo;
    QRLAluno: TQRLabel;
    QryRelatorioNU_NIS: TBCDField;
    QryRelatorioCO_SIT_MAT: TStringField;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;var PrintReport: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QrlMedia11Print(sender: TObject; var Value: String);
    procedure QRDBText1Print(sender: TObject; var Value: String);
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    //vDescricaoMaterias: String;
    CodMateria : Array[0..12] of integer;
    qtdMat : Integer;
  public
    { Public declarations }
    CodigoCurso, CodigoTurma, AnoCurso, CodigoModulo,DescCurso,DescTurma, codigoEmpresa: String;
    NumSemestre: Integer;
  end;

var
  FrmRelGradeNotasAluno: TFrmRelGradeNotasAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelGradeNotasAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep; var PrintReport: Boolean);
var
  SqlString: String;
  //ct: Integer;
begin
  inherited;
  QRMLegenda1.Lines.Clear;
  QRMLegenda2.Lines.Clear;
  M1.Caption :=  '';
  M2.Caption :=  '';
  M3.Caption :=  '';
  M4.Caption :=  '';
  M5.Caption :=  '';
  M6.Caption :=  '';
  M7.Caption :=  '';
  M8.Caption :=  '';
  M9.Caption :=  '';
  M10.Caption := '';
  M11.Caption := '';
  M12.Caption := '';
  //ct := 0;
  qtdMat := 0;

  with DM.QrySql do
  begin
    SqlString := ' Select DISTINCT CM.NO_MATERIA, CM.NO_SIGLA_MATERIA,MA.CO_MAT ' +
                 ' from TB43_GRD_CURSO H, TB02_MATERIA MA, TB107_CADMATERIAS CM ' +
                 ' where H.CO_EMP = MA.CO_EMP                                                ' +
                 '   AND H.CO_MAT = MA.CO_MAT                                                ' +
                 '   AND MA.ID_MATERIA = CM.ID_MATERIA ' +
                 '   AND H.CO_CUR = ' + CodigoCurso +
                 '   and h.co_modu_cur = ' + CodigoModulo +
                 '   and h.co_ano_grade = ' +     AnoCurso +
                 '   and h.co_emp = ' + codigoEmpresa +
                 ' ORDER BY CM.NO_SIGLA_MATERIA ';
    SQL.Text := SqlString;
    Open;
    if not IsEmpty then
    begin
      first;

      while not Eof do
      begin
        Case DM.QrySql.RecNo of
          1 :
          begin
            M1.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          2 :
          begin
            M2.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          3 :
          begin
            M3.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          4 : 
          begin
            M4.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          5 :
          begin
            M5.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          6 :
          begin
            M6.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          7 :
          begin
            M7.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          8 :
          begin
            M8.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          9 :
          begin
            M9.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          10 :
          begin
            M10.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          11 :
          begin
            M11.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
          12 :
          begin
            M12.Caption := Fields.FieldByName('NO_SIGLA_MATERIA').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
          end;
        end;
        { Recupera a legada das disciplinas }
        //vDescricaoMaterias := vDescricaoMaterias + FieldByName('NO_SIGLA_MATERIA').AsString + ' - ' + FieldByName('NO_MATERIA').AsString + #13;
        //QRMLegenda1.Lines.Text := vDescricaoMaterias;
        qtdMat := qtdMat + 1;
        CodMateria[qtdMat] := FieldByName('CO_MAT').AsInteger;
        Next;
      end;
    end;
  end;
end;

procedure TFrmRelGradeNotasAluno.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
var
  i,qtdMediasM1,qtdMediasM2,qtdMediasM3,qtdMediasM4,qtdMediasM5,qtdMediasM6,qtdMediasM7,qtdMediasM8,
  qtdMediasM9,qtdMediasM10,qtdMediasM11,qtdMediasM12: integer;
  ocoM1,ocoM2,ocoM3,ocoM4,ocoM5,ocoM6,ocoM7,ocoM8,
  ocoM9,ocoM10,ocoM11,ocoM12 : Boolean;
begin
  inherited;
  MDM1.Caption := '0';
  MDM2.Caption := '0';
  MDM3.Caption := '0';
  MDM4.Caption := '0';
  MDM5.Caption := '0';
  MDM6.Caption := '0';
  MDM7.Caption := '0';
  MDM8.Caption := '0';
  MDM9.Caption := '0';
  MDM10.Caption := '0';
  MDM11.Caption := '0';
  MDM12.Caption := '0';
  
  qtdMediasM1 := 0;
  qtdMediasM2 := 0;
  qtdMediasM3 := 0;
  qtdMediasM4 := 0;
  qtdMediasM5 := 0;
  qtdMediasM6 := 0;
  qtdMediasM7 := 0;
  qtdMediasM8 := 0;
  qtdMediasM9 := 0;
  qtdMediasM10 := 0;
  qtdMediasM11 := 0;
  qtdMediasM12 := 0;
  
  ocoM1 := false;
  ocoM2 := false;
  ocoM3 := false;
  ocoM4 := false;
  ocoM5 := false;
  ocoM6 := false;
  ocoM7 := false;
  ocoM8 := false;
  ocoM9 := false;
  ocoM10 := false;
  ocoM11 := false;
  ocoM12 := false;
  
  if not (QryRelatorioCO_ALU_CAD.IsNull) and not (QryRelatorioNO_ALU.IsNull) then
    if not QryRelatorioNU_NIS.IsNull then
      QRLAluno.Caption := FormatMaskText('00.000.000000;0',QryRelatorioCO_ALU_CAD.AsString) + ' - ' + UpperCase(QryRelatorioNO_ALU.AsString) +
      '  ( Nº NIS: ' + QryRelatorioNU_NIS.AsString + ' )'
    else
      QRLAluno.Caption := FormatMaskText('00.000.000000;0',QryRelatorioCO_ALU_CAD.AsString) + ' - ' + QryRelatorioNO_ALU.AsString + ' )';

    if QryRelatorioCO_SIT_MAT.AsString = 'A' then
      QRLAluno.Caption := QRLAluno.Caption + ' *** Em Aberto ***';
    if QryRelatorioCO_SIT_MAT.AsString = 'T' then
      QRLAluno.Caption := QRLAluno.Caption + ' *** Trancada ***';
    if QryRelatorioCO_SIT_MAT.AsString = 'F' then
      QRLAluno.Caption := QRLAluno.Caption + ' *** Finalizada ***';
    if QryRelatorioCO_SIT_MAT.AsString = 'P' then
      QRLAluno.Caption := QRLAluno.Caption + ' *** Pendente ***';
    if QryRelatorioCO_SIT_MAT.AsString = 'E' then
      QRLAluno.Caption := QRLAluno.Caption + ' *** Evadido ***';
    if QryRelatorioCO_SIT_MAT.AsString = 'X' then
      QRLAluno.Caption := QRLAluno.Caption + ' *** Transferido ***';

    for i:= 1 to qtdMat do
    begin

      with DM.QrySql do
      begin
        Close;

        {SQL.Text := ' select DISTINCT MM.CO_SIT_MAT, H.*,                                      '+
							    'N1B1 = (SELECT SUM(VL_NOTA)/COUNT(CO_LANC_NOTA) FROM TB49_NOTA_ALUNO        '+
									'WHERE CO_EMP = H.CO_EMP                                                     '+
									'AND CO_ALU = H.CO_ALU                                                       '+
									'AND CO_CUR = H.CO_CUR                                                       '+
                  'AND CO_MAT = ' + IntToStr(codMateria[i]) +
                  ' AND CO_BIMESTRE = 1 '+
									'AND CO_ANO_MES_MAT = H.CO_ANO_REF                                           '+
									'AND TP_AVAL = ' + QuotedStr('PM') +
									'AND (MONTH(DT_PROV) = 1 OR MONTH(DT_PROV) = 2)),                        '+
                  'N1B2 = (SELECT SUM(VL_NOTA)/COUNT(CO_LANC_NOTA) FROM TB49_NOTA_ALUNO        '+
									'WHERE CO_EMP = H.CO_EMP                                                     '+
									'AND CO_ALU = H.CO_ALU                                                       '+
								  'AND CO_CUR = H.CO_CUR                                                       '+
                  'AND CO_MAT = ' + IntToStr(codMateria[i]) +
                  ' AND CO_BIMESTRE = 2 '+
									'AND CO_ANO_MES_MAT = H.CO_ANO_REF                                           '+
									'AND TP_AVAL =' + QuotedStr('PM') +
									'AND (MONTH(DT_PROV) = 5 OR MONTH(DT_PROV) = 6)),                        '+
                  'N1B3 = (SELECT SUM(VL_NOTA)/COUNT(CO_LANC_NOTA) FROM TB49_NOTA_ALUNO        '+
									'WHERE CO_EMP = H.CO_EMP                                                     '+
									'AND CO_ALU = H.CO_ALU                                                       '+
									'AND CO_CUR = H.CO_CUR                                                       '+
                  'AND CO_MAT = ' + IntToStr(codMateria[i]) +
                  ' AND CO_BIMESTRE = 3 '+
									'AND CO_ANO_MES_MAT = H.CO_ANO_REF                                           '+
									'AND TP_AVAL ='+ QuotedStr('PM') +
									'AND MONTH(DT_PROV) = 8),                                                 '+
                  'N1B4 = (SELECT SUM(VL_NOTA)/COUNT(CO_LANC_NOTA) FROM TB49_NOTA_ALUNO        '+
									'WHERE CO_EMP = H.CO_EMP                                                     '+
									'AND CO_ALU = H.CO_ALU                                                       '+
									'AND CO_CUR = H.CO_CUR                                                       '+
                  'AND CO_MAT = ' + IntToStr(codMateria[i]) +
                  ' AND CO_BIMESTRE = 4 '+
									'AND CO_ANO_MES_MAT = H.CO_ANO_REF                                           '+
									'AND TP_AVAL ='+ QuotedStr('PM')+
									'AND (MONTH(DT_PROV) = 10 OR MONTH(DT_PROV) = 11)),                      '+
                  'N2B1 = (SELECT SUM(VL_NOTA)/COUNT(CO_LANC_NOTA) FROM TB49_NOTA_ALUNO        '+
									'WHERE CO_EMP = H.CO_EMP                                                     '+
									'AND CO_ALU = H.CO_ALU                                                       '+
									'AND CO_CUR = H.CO_CUR                                                       '+
                  'AND CO_MAT = ' + IntToStr(codMateria[i]) +
                  ' AND CO_BIMESTRE = 1 '+
									'AND CO_ANO_MES_MAT = H.CO_ANO_REF                                           '+
									'AND TP_AVAL =' + QuotedStr('PM') +
								  'AND (MONTH(DT_PROV) = 3 OR MONTH(DT_PROV) = 4)),                        '+
							    'N2B2 = (SELECT SUM(VL_NOTA)/COUNT(CO_LANC_NOTA) FROM TB49_NOTA_ALUNO        '+
									'WHERE CO_EMP = H.CO_EMP                                                     '+
									'AND CO_ALU = H.CO_ALU                                                       '+
									'AND CO_CUR = H.CO_CUR                                                       '+
                  'AND CO_MAT = ' + IntToStr(codMateria[i]) +
                  ' AND CO_BIMESTRE = 2 '+
									'AND CO_ANO_MES_MAT = H.CO_ANO_REF                                           '+
									'AND TP_AVAL = ' + QuotedStr('PM')+
								  'AND MONTH(DT_PROV) = 7),                                                 '+
							    'N2B3 = (SELECT SUM(VL_NOTA)/COUNT(CO_LANC_NOTA) FROM TB49_NOTA_ALUNO        '+
									'WHERE CO_EMP = H.CO_EMP                                                     '+
									'AND CO_ALU = H.CO_ALU                                                       '+
								  'AND CO_CUR = H.CO_CUR                                                       '+
                  'AND CO_MAT = ' + IntToStr(codMateria[i]) +
                  ' AND CO_BIMESTRE = 3 '+
									'AND CO_ANO_MES_MAT = H.CO_ANO_REF                                          '+
									'AND TP_AVAL = ' + quotedStr('PM')+
									'AND MONTH(DT_PROV) = 9),                                                 '+
							    'N2B4 = (SELECT SUM(VL_NOTA)/COUNT(CO_LANC_NOTA) FROM TB49_NOTA_ALUNO        '+
									'WHERE CO_EMP = H.CO_EMP                                                     '+
									'AND CO_ALU = H.CO_ALU                                                        '+
									'AND CO_CUR = H.CO_CUR                                                      '+
                  'AND CO_MAT = ' + IntToStr(codMateria[i]) +
                  ' AND CO_BIMESTRE = 4 '+
									'AND CO_ANO_MES_MAT = H.CO_ANO_REF                                          '+
									'AND TP_AVAL = ' + quotedStr('PM')+
									'AND MONTH(DT_PROV) = 12)                                               '+
                  'from TB079_HIST_ALUNO H ' +
                  ' JOIN TB07_ALUNO A on H.CO_EMP = A.CO_EMP AND H.CO_ALU = A.CO_ALU ' +
                  ' JOIN TB08_MATRCUR MM on H.CO_EMP = MM.CO_EMP AND H.CO_CUR = MM.CO_CUR AND H.CO_ALU = MM.CO_ALU ' +
                  ' JOIN TB02_MATERIA MA on H.CO_EMP = MA.CO_EMP AND H.CO_MAT = MA.CO_MAT '+
                  ' LEFT JOIN TB49_NOTA_ALUNO NA on H.CO_EMP = NA.CO_EMP AND H.CO_ALU = NA.CO_ALU AND H.CO_CUR = NA.CO_CUR AND H.CO_ANO_REF = NA.CO_ANO_MES_MAT AND H.CO_MAT = NA.CO_MAT '+
                  'where H.CO_EMP = ' + codigoEmpresa  +
                    '   AND H.CO_CUR = ' + CodigoCurso +
                    '   AND H.CO_MODU_CUR = ' + CodigoModulo +
                    '   AND H.CO_TUR = ' + CodigoTurma +
                    '   AND H.CO_ANO_REF = ' + QuotedStr(AnoCurso) +
                    //'   AND MM.CO_SIT_MAT = ' + QuotedStr('A')+
                    '   AND H.CO_MAT = ' + IntToStr(codMateria[i]) +
                    '   AND H.CO_ALU = ' + QryRelatorioCO_ALU.AsString; }
        SQL.Text := 'select H.VL_NOTA_BIM1,H.VL_NOTA_BIM2,H.VL_NOTA_BIM3,H.VL_NOTA_BIM4 ' +
                    ' from tb079_hist_aluno H ' +
                    'where H.CO_EMP = ' + codigoEmpresa  +
                    '   AND H.CO_CUR = ' + CodigoCurso +
                    '   AND H.CO_MODU_CUR = ' + CodigoModulo +
                    '   AND H.CO_TUR = ' + CodigoTurma +
                    '   AND H.CO_ANO_REF = ' + QuotedStr(AnoCurso) +
                    //'   AND MM.CO_SIT_MAT = ' + QuotedStr('A')+
                    '   AND H.CO_MAT = ' + IntToStr(codMateria[i]) +
                    '   AND H.CO_ALU = ' + QryRelatorioCO_ALU.AsString;
        Open;

        while not Eof do
        begin
          Case i of
            1 : begin
                 { //Notas 1, Matéria 1
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M1.Caption := ''
                  else
                  begin
                    N1B1M1.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M1.Caption := ''
                  else
                  begin
                    N1B2M1.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M1.Caption := ''
                  else
                  begin
                    N1B3M1.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M1.Caption := ''
                  else
                  begin
                    N1B4M1.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 1
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M1.Caption := ''
                  else
                  begin
                    N2B1M1.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M1.Caption := ''
                  else
                  begin
                    N2B2M1.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M1.Caption := ''
                  else
                  begin
                    N2B3M1.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M1.Caption := ''
                  else
                  begin
                    N2B4M1.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //    }

                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M1B1.Caption := ''
                  else
                  begin
                    M1B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM1.Caption := FormatFloat('#0.00',StrToFloat(MDM1.Caption) + StrToFloat(M1B1.Caption));
                    qtdMediasM1 := qtdMediasM1 + 1;
                    ocoM1 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M1B2.Caption := ''
                  else
                  begin
                    M1B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM1.Caption := FormatFloat('#0.00',StrToFloat(MDM1.Caption) + StrToFloat(M1B2.Caption));
                    qtdMediasM1 := qtdMediasM1 + 1;
                    ocoM1 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M1B3.Caption := ''
                  else
                  begin
                    M1B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM1.Caption := FormatFloat('#0.00',StrToFloat(MDM1.Caption) + StrToFloat(M1B3.Caption));
                    qtdMediasM1 := qtdMediasM1 + 1;
                    ocoM1 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M1B4.Caption := ''
                  else
                  begin
                    M1B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM1.Caption := FormatFloat('#0.00',StrToFloat(MDM1.Caption) + StrToFloat(M1B4.Caption));
                    qtdMediasM1 := qtdMediasM1 + 1;
                    ocoM1 := true;
                  end;

              {   if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM1.Caption := '';
                 end
                 else
                 begin
                  MDM1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;            }

                end;
            2 : begin
                  {
                  //Notas 1, Matéria 2
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M2.Caption := ''
                  else
                  begin
                    N1B1M2.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M2.Caption := ''
                  else
                  begin
                    N1B2M2.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M2.Caption := ''
                  else
                  begin
                    N1B3M2.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M2.Caption := ''
                  else
                  begin
                    N1B4M2.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 2
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M2.Caption := ''
                  else
                  begin
                    N2B1M2.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M2.Caption := ''
                  else
                  begin
                    N2B2M2.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M2.Caption := ''
                  else
                  begin
                    N2B3M2.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M2.Caption := ''
                  else
                  begin
                    N2B4M2.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }

                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M2B1.Caption := ''
                  else
                  begin
                    M2B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM2.Caption := FormatFloat('#0.00',StrToFloat(MDM2.Caption) + StrToFloat(M2B1.Caption));
                    qtdMediasM2 := qtdMediasM2 + 1;
                    ocoM2 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M2B2.Caption := ''
                  else
                  begin
                    M2B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM2.Caption := FormatFloat('#0.00',StrToFloat(MDM2.Caption) + StrToFloat(M2B2.Caption));
                    qtdMediasM2 := qtdMediasM2 + 1;
                    ocoM2 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M2B3.Caption := ''
                  else
                  begin
                    M2B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM2.Caption := FormatFloat('#0.00',StrToFloat(MDM2.Caption) + StrToFloat(M2B3.Caption));
                    qtdMediasM2 := qtdMediasM2 + 1;
                    ocoM2 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M2B4.Caption := ''
                  else
                  begin
                    M2B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM2.Caption := FormatFloat('#0.00',StrToFloat(MDM2.Caption) + StrToFloat(M2B4.Caption));
                    qtdMediasM2 := qtdMediasM2 + 1;
                    ocoM2 := true;
                  end;

               {  if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM2.Caption := '';
                 end
                 else
                 begin
                  MDM2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;    }

                end;
            3 : begin
                  {
                  //Notas 1, Matéria 3
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M3.Caption := ''
                  else
                  begin
                    N1B1M3.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M3.Caption := ''
                  else
                  begin
                    N1B2M3.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M3.Caption := ''
                  else
                  begin
                    N1B3M3.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M3.Caption := ''
                  else
                  begin
                    N1B4M3.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 3
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M3.Caption := ''
                  else
                  begin
                    N2B1M3.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M3.Caption := ''
                  else
                  begin
                    N2B2M3.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M3.Caption := ''
                  else
                  begin
                    N2B3M3.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M3.Caption := ''
                  else
                  begin
                    N2B4M3.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M3B1.Caption := ''
                  else
                  begin
                    M3B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM3.Caption := FormatFloat('#0.00',StrToFloat(MDM3.Caption) + StrToFloat(M3B1.Caption));
                    qtdMediasM3 := qtdMediasM3 + 1;
                    ocoM3 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M3B2.Caption := ''
                  else
                  begin
                    M3B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM3.Caption := FormatFloat('#0.00',StrToFloat(MDM3.Caption) + StrToFloat(M3B2.Caption));
                    qtdMediasM3 := qtdMediasM3 + 1;
                    ocoM3 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M3B3.Caption := ''
                  else
                  begin
                    M3B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM3.Caption := FormatFloat('#0.00',StrToFloat(MDM3.Caption) + StrToFloat(M3B3.Caption));
                    qtdMediasM3 := qtdMediasM3 + 1;
                    ocoM3 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M3B4.Caption := ''
                  else
                  begin
                    M3B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM3.Caption := FormatFloat('#0.00',StrToFloat(MDM3.Caption) + StrToFloat(M3B4.Caption));
                    qtdMediasM3 := qtdMediasM3 + 1;
                    ocoM3 := true;
                  end;

                 {if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM3.Caption := '';
                 end
                 else
                 begin
                  MDM3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;}

                end;
            4 : begin
                 {
                 //Notas 1, Matéria 4
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M4.Caption := ''
                  else
                  begin
                    N1B1M4.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M4.Caption := ''
                  else
                  begin
                    N1B2M4.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M4.Caption := ''
                  else
                  begin
                    N1B3M4.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M4.Caption := ''
                  else
                  begin
                    N1B4M4.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 4
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M4.Caption := ''
                  else
                  begin
                    N2B1M4.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M4.Caption := ''
                  else
                  begin
                    N2B2M4.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M4.Caption := ''
                  else
                  begin
                    N2B3M4.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M4.Caption := ''
                  else
                  begin
                    N2B4M4.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M4B1.Caption := ''
                  else
                  begin
                    M4B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM4.Caption := FormatFloat('#0.00',StrToFloat(MDM4.Caption) + StrToFloat(M4B1.Caption));
                    qtdMediasM4 := qtdMediasM4 + 1;
                    ocoM4 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M4B2.Caption := ''
                  else
                  begin
                    M4B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM4.Caption := FormatFloat('#0.00',StrToFloat(MDM4.Caption) + StrToFloat(M4B2.Caption));
                    qtdMediasM4 := qtdMediasM4 + 1;
                    ocoM4 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M4B3.Caption := ''
                  else
                  begin
                    M4B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM4.Caption := FormatFloat('#0.00',StrToFloat(MDM4.Caption) + StrToFloat(M4B3.Caption));
                    qtdMediasM4 := qtdMediasM4 + 1;
                    ocoM4 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M4B4.Caption := ''
                  else
                  begin
                    M4B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM4.Caption := FormatFloat('#0.00',StrToFloat(MDM4.Caption) + StrToFloat(M4B4.Caption));
                    qtdMediasM4 := qtdMediasM4 + 1;
                    ocoM4 := true;
                  end;

                { if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM4.Caption := '';
                 end
                 else
                 begin
                  MDM4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end; }

                end;
            5 : begin
                  {
                  //Notas 1, Matéria 5
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M5.Caption := ''
                  else
                  begin
                    N1B1M5.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M5.Caption := ''
                  else
                  begin
                    N1B2M5.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M5.Caption := ''
                  else
                  begin
                    N1B3M5.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M5.Caption := ''
                  else
                  begin
                    N1B4M5.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 5
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M5.Caption := ''
                  else
                  begin
                    N2B1M5.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M5.Caption := ''
                  else
                  begin
                    N2B2M5.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M5.Caption := ''
                  else
                  begin
                    N2B3M5.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M5.Caption := ''
                  else
                  begin
                    N2B4M5.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M5B1.Caption := ''
                  else
                  begin
                    M5B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM5.Caption := FormatFloat('#0.00',StrToFloat(MDM5.Caption) + StrToFloat(M5B1.Caption));
                    qtdMediasM5 := qtdMediasM5 + 1;
                    ocoM5 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M5B2.Caption := ''
                  else
                  begin
                    M5B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM5.Caption := FormatFloat('#0.00',StrToFloat(MDM5.Caption) + StrToFloat(M5B2.Caption));
                    qtdMediasM5 := qtdMediasM5 + 1;
                    ocoM5 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M5B3.Caption := ''
                  else
                  begin
                    M5B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM5.Caption := FormatFloat('#0.00',StrToFloat(MDM5.Caption) + StrToFloat(M5B3.Caption));
                    qtdMediasM5 := qtdMediasM5 + 1;
                    ocoM5 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M5B4.Caption := ''
                  else
                  begin
                    M5B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM5.Caption := FormatFloat('#0.00',StrToFloat(MDM5.Caption) + StrToFloat(M5B4.Caption));
                    qtdMediasM5 := qtdMediasM5 + 1;
                    ocoM5 := true;
                  end;

                 {if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM5.Caption := '';
                 end
                 else
                 begin
                  MDM5.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;            }

                end;
            6 : begin
                  {
                  //Notas 1, Matéria 6
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M6.Caption := ''
                  else
                  begin
                    N1B1M6.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M6.Caption := ''
                  else
                  begin
                    N1B2M6.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M6.Caption := ''
                  else
                  begin
                    N1B3M6.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M6.Caption := ''
                  else
                  begin
                    N1B4M6.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 6
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M6.Caption := ''
                  else
                  begin
                    N2B1M6.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M6.Caption := ''
                  else
                  begin
                    N2B2M6.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M6.Caption := ''
                  else
                  begin
                    N2B3M6.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M6.Caption := ''
                  else
                  begin
                    N2B4M6.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M6B1.Caption := ''
                  else
                  begin
                    M6B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM6.Caption := FormatFloat('#0.00',StrToFloat(MDM6.Caption) + StrToFloat(M6B1.Caption));
                    qtdMediasM6 := qtdMediasM6 + 1;
                    ocoM6 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M6B2.Caption := ''
                  else
                  begin
                    M6B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM6.Caption := FormatFloat('#0.00',StrToFloat(MDM6.Caption) + StrToFloat(M6B2.Caption));
                    qtdMediasM6 := qtdMediasM6 + 1;
                    ocoM6 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M6B3.Caption := ''
                  else
                  begin
                    M6B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM6.Caption := FormatFloat('#0.00',StrToFloat(MDM6.Caption) + StrToFloat(M6B3.Caption));
                    qtdMediasM6 := qtdMediasM6 + 1;
                    ocoM6 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M6B4.Caption := ''
                  else
                  begin
                    M6B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM6.Caption := FormatFloat('#0.00',StrToFloat(MDM6.Caption) + StrToFloat(M6B4.Caption));
                    qtdMediasM6 := qtdMediasM6 + 1;
                    ocoM6 := true;
                  end;

                { if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM6.Caption := '';
                 end
                 else
                 begin
                  MDM6.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;    }

                end;
            7 : begin
                  {
                  //Notas 1, Matéria 7
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M7.Caption := ''
                  else
                  begin
                    N1B1M7.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M7.Caption := ''
                  else
                  begin
                    N1B2M7.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M7.Caption := ''
                  else
                  begin
                    N1B3M7.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M7.Caption := ''
                  else
                  begin
                    N1B4M7.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 7
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M7.Caption := ''
                  else
                  begin
                    N2B1M7.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M7.Caption := ''
                  else
                  begin
                    N2B2M7.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M7.Caption := ''
                  else
                  begin
                    N2B3M7.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M7.Caption := ''
                  else
                  begin
                    N2B4M7.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M7B1.Caption := ''
                  else
                  begin
                    M7B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM7.Caption := FormatFloat('#0.00',StrToFloat(MDM7.Caption) + StrToFloat(M7B1.Caption));
                    qtdMediasM7 := qtdMediasM7 + 1;
                    ocoM7 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M7B2.Caption := ''
                  else
                  begin
                    M7B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM7.Caption := FormatFloat('#0.00',StrToFloat(MDM7.Caption) + StrToFloat(M7B2.Caption));
                    qtdMediasM7 := qtdMediasM7 + 1;
                    ocoM7 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M7B3.Caption := ''
                  else
                  begin
                    M7B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM7.Caption := FormatFloat('#0.00',StrToFloat(MDM7.Caption) + StrToFloat(M7B3.Caption));
                    qtdMediasM7 := qtdMediasM7 + 1;
                    ocoM7 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M7B4.Caption := ''
                  else
                  begin
                    M7B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM7.Caption := FormatFloat('#0.00',StrToFloat(MDM7.Caption) + StrToFloat(M7B4.Caption));
                    qtdMediasM7 := qtdMediasM7 + 1;
                    ocoM7 := true;
                  end;

                 {if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM7.Caption := '';
                 end
                 else
                 begin
                  MDM7.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;}

                end;
            8 : begin
                  {
                  //Notas 1, Matéria 8
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M8.Caption := ''
                  else
                  begin
                    N1B1M8.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M8.Caption := ''
                  else
                  begin
                    N1B2M8.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M8.Caption := ''
                  else
                  begin
                    N1B3M8.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M8.Caption := ''
                  else
                  begin
                    N1B4M8.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 8
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M8.Caption := ''
                  else
                  begin
                    N2B1M8.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M8.Caption := ''
                  else
                  begin
                    N2B2M8.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M8.Caption := ''
                  else
                  begin
                    N2B3M8.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M8.Caption := ''
                  else
                  begin
                    N2B4M8.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M8B1.Caption := ''
                  else
                  begin
                    M8B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM8.Caption := FormatFloat('#0.00',StrToFloat(MDM8.Caption) + StrToFloat(M8B1.Caption));
                    qtdMediasM8 := qtdMediasM8 + 1;
                    ocoM8 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M8B2.Caption := ''
                  else
                  begin
                    M8B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM8.Caption := FormatFloat('#0.00',StrToFloat(MDM8.Caption) + StrToFloat(M8B2.Caption));
                    qtdMediasM8 := qtdMediasM8 + 1;
                    ocoM8 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M8B3.Caption := ''
                  else
                  begin
                    M8B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM8.Caption := FormatFloat('#0.00',StrToFloat(MDM8.Caption) + StrToFloat(M8B3.Caption));
                    qtdMediasM8 := qtdMediasM8 + 1;
                    ocoM8 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M8B4.Caption := ''
                  else
                  begin
                    M8B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM8.Caption := FormatFloat('#0.00',StrToFloat(MDM8.Caption) + StrToFloat(M8B4.Caption));
                    qtdMediasM8 := qtdMediasM8 + 1;
                    ocoM8 := true;
                  end;

                 {if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM8.Caption := '';
                 end
                 else
                 begin
                  MDM8.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end; }

                end;
            9 : begin
                  {
                  //Notas 1, Matéria 9
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M9.Caption := ''
                  else
                  begin
                    N1B1M9.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M9.Caption := ''
                  else
                  begin
                    N1B2M9.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M9.Caption := ''
                  else
                  begin
                    N1B3M9.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M9.Caption := ''
                  else
                  begin
                    N1B4M9.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 9
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M9.Caption := ''
                  else
                  begin
                    N2B1M9.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M9.Caption := ''
                  else
                  begin
                    N2B2M9.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M9.Caption := ''
                  else
                  begin
                    N2B3M9.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M9.Caption := ''
                  else
                  begin
                    N2B4M9.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M9B1.Caption := ''
                  else
                  begin
                    M9B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM9.Caption := FormatFloat('#0.00',StrToFloat(MDM9.Caption) + StrToFloat(M9B1.Caption));
                    qtdMediasM9 := qtdMediasM9 + 1;
                    ocoM9 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M9B2.Caption := ''
                  else
                  begin
                    M9B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM9.Caption := FormatFloat('#0.00',StrToFloat(MDM9.Caption) + StrToFloat(M9B2.Caption));
                    qtdMediasM9 := qtdMediasM9 + 1;
                    ocoM9 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M9B3.Caption := ''
                  else
                  begin
                    M9B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM9.Caption := FormatFloat('#0.00',StrToFloat(MDM9.Caption) + StrToFloat(M9B3.Caption));
                    qtdMediasM9 := qtdMediasM9 + 1;
                    ocoM9 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M9B4.Caption := ''
                  else
                  begin
                    M9B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM9.Caption := FormatFloat('#0.00',StrToFloat(MDM9.Caption) + StrToFloat(M9B4.Caption));
                    qtdMediasM9 := qtdMediasM9 + 1;
                    ocoM9 := true;
                  end;

                 {if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM9.Caption := '';
                 end
                 else
                 begin
                  MDM9.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;  }

                end;
            10 :begin
                  {
                  //Notas 1, Matéria 10
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M10.Caption := ''
                  else
                  begin
                    N1B1M10.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M10.Caption := ''
                  else
                  begin
                    N1B2M10.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M10.Caption := ''
                  else
                  begin
                    N1B3M10.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M10.Caption := ''
                  else
                  begin
                    N1B4M10.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 10
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M10.Caption := ''
                  else
                  begin
                    N2B1M10.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M10.Caption := ''
                  else
                  begin
                    N2B2M10.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M10.Caption := ''
                  else
                  begin
                    N2B3M10.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M10.Caption := ''
                  else
                  begin
                    N2B4M10.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M10B1.Caption := ''
                  else
                  begin
                    M10B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM10.Caption := FormatFloat('#0.00',StrToFloat(MDM10.Caption) + StrToFloat(M10B1.Caption));
                    qtdMediasM10 := qtdMediasM10 + 1;
                    ocoM10 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M10B2.Caption := ''
                  else
                  begin
                    M10B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM10.Caption := FormatFloat('#0.00',StrToFloat(MDM10.Caption) + StrToFloat(M10B2.Caption));
                    qtdMediasM10 := qtdMediasM10 + 1;
                    ocoM10 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M10B3.Caption := ''
                  else
                  begin
                    M10B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM10.Caption := FormatFloat('#0.00',StrToFloat(MDM10.Caption) + StrToFloat(M10B3.Caption));
                    qtdMediasM10 := qtdMediasM10 + 1;
                    ocoM10 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M10B4.Caption := ''
                  else
                  begin
                    M10B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM10.Caption := FormatFloat('#0.00',StrToFloat(MDM10.Caption) + StrToFloat(M10B4.Caption));
                    qtdMediasM10 := qtdMediasM10 + 1;
                    ocoM10 := true;
                  end;

                { if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM10.Caption := '';
                 end
                 else
                 begin
                  MDM10.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end; }

                end;

            11 :begin
                  {
                  //Notas 1, Matéria 11
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M11.Caption := ''
                  else
                  begin
                    N1B1M11.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M11.Caption := ''
                  else
                  begin
                    N1B2M11.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M11.Caption := ''
                  else
                  begin
                    N1B3M11.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M11.Caption := ''
                  else
                  begin
                    N1B4M11.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 11
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M11.Caption := ''
                  else
                  begin
                    N2B1M11.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M11.Caption := ''
                  else
                  begin
                    N2B2M11.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M11.Caption := ''
                  else
                  begin
                    N2B3M11.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M11.Caption := ''
                  else
                  begin
                    N2B4M11.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M11B1.Caption := ''
                  else
                  begin
                    M11B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM11.Caption := FormatFloat('#0.00',StrToFloat(MDM11.Caption) + StrToFloat(M11B1.Caption));
                    qtdMediasM11 := qtdMediasM11 + 1;
                    ocoM11 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M11B2.Caption := ''
                  else
                  begin
                    M11B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM11.Caption := FormatFloat('#0.00',StrToFloat(MDM11.Caption) + StrToFloat(M11B2.Caption));
                    qtdMediasM11 := qtdMediasM11 + 1;
                    ocoM11 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M11B3.Caption := ''
                  else
                  begin
                    M11B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM11.Caption := FormatFloat('#0.00',StrToFloat(MDM11.Caption) + StrToFloat(M11B3.Caption));
                    qtdMediasM11 := qtdMediasM11 + 1;
                    ocoM11 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M11B4.Caption := ''
                  else
                  begin
                    M11B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM11.Caption := FormatFloat('#0.00',StrToFloat(MDM11.Caption) + StrToFloat(M11B4.Caption));
                    qtdMediasM11 := qtdMediasM11 + 1;
                    ocoM11 := true;
                  end;

                 {if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM11.Caption := '';
                 end
                 else
                 begin
                  MDM11.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;}

                end;
            12 :begin
                  {
                  //Notas 1, Matéria 12
                  if (Fields.FieldByName('N1B1').IsNull) then
                    N1B1M12.Caption := ''
                  else
                  begin
                    N1B1M12.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B2').IsNull) then
                    N1B2M12.Caption := ''
                  else
                  begin
                    N1B2M12.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B3').IsNull) then
                    N1B3M12.Caption := ''
                  else
                  begin
                    N1B3M12.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N1B4').IsNull) then
                    N1B4M12.Caption := ''
                  else
                  begin
                    N1B4M12.Caption := FormatFloat('#0.00', Fields.FieldByName('N1B4').AsFloat);
                  end;
                  //
                  //Notas 2, Matéria 12
                  if (Fields.FieldByName('N2B1').IsNull) then
                    N2B1M12.Caption := ''
                  else
                  begin
                    N2B1M12.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B1').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B2').IsNull) then
                    N2B2M12.Caption := ''
                  else
                  begin
                    N2B2M12.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B2').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B3').IsNull) then
                    N2B3M12.Caption := ''
                  else
                  begin
                    N2B3M12.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B3').AsFloat);
                  end;
                  if (Fields.FieldByName('N2B4').IsNull) then
                    N2B4M12.Caption := ''
                  else
                  begin
                    N2B4M12.Caption := FormatFloat('#0.00', Fields.FieldByName('N2B4').AsFloat);
                  end;
                  //
                  }
                  if (Fields.FieldByName('VL_NOTA_BIM1').IsNull) then
                    M12B1.Caption := ''
                  else
                  begin
                    M12B1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM1').AsFloat);
                    MDM12.Caption := FormatFloat('#0.00',StrToFloat(MDM12.Caption) + StrToFloat(M12B1.Caption));
                    qtdMediasM12 := qtdMediasM12 + 1;
                    ocoM12 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM2').IsNull) then
                    M12B2.Caption := ''
                  else
                  begin
                    M12B2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM2').AsFloat);
                    MDM12.Caption := FormatFloat('#0.00',StrToFloat(MDM12.Caption) + StrToFloat(M12B2.Caption));
                    qtdMediasM12 := qtdMediasM12 + 1;
                    ocoM12 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM3').IsNull) then
                    M12B3.Caption := ''
                  else
                  begin
                    M12B3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM3').AsFloat);
                    MDM12.Caption := FormatFloat('#0.00',StrToFloat(MDM12.Caption) + StrToFloat(M12B3.Caption));
                    qtdMediasM12 := qtdMediasM12 + 1;
                    ocoM12 := true;
                  end;
                  if (Fields.FieldByName('VL_NOTA_BIM4').IsNull) then
                    M12B4.Caption := ''
                  else
                  begin
                    M12B4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_NOTA_BIM4').AsFloat);
                    MDM12.Caption := FormatFloat('#0.00',StrToFloat(MDM12.Caption) + StrToFloat(M12B4.Caption));
                    qtdMediasM12 := qtdMediasM12 + 1;
                    ocoM12 := true;
                  end;

                 {if Fields.FieldByName('VL_MEDIA_FINAL').IsNull then
                 begin
                  MDM12.Caption := '';
                 end
                 else
                 begin
                  MDM12.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                 end;  }

                end;
          end;
          Next;
        end;
      end;
      Next;
    end;

    if ocoM1 then
    begin
      if qtdMediasM1 > 0 then
        MDM1.Caption := FormatFloat('#0.00',StrToFloat(MDM1.Caption)/qtdMediasM1);
      //MDM1.Visible := true;
    end
    else
    begin
      MDM1.Caption := '';
    end;

    if ocoM2 then
    begin
      if qtdMediasM2 > 0 then
        MDM2.Caption := FormatFloat('#0.00',StrToFloat(MDM2.Caption)/qtdMediasM2);
      //MDM2.Visible := true;
    end
    else
    begin
      MDM2.Caption := '';
    end;

    if ocoM3 then
    begin
      if qtdMediasM3 > 0 then
        MDM3.Caption := FormatFloat('#0.00',StrToFloat(MDM3.Caption)/qtdMediasM3);
      //MDM3.Visible := true;
    end
    else
    begin
      MDM3.Caption := '';
    end;

    if ocoM4 then
    begin
      if qtdMediasM4 > 0 then
        MDM4.Caption := FormatFloat('#0.00',StrToFloat(MDM4.Caption)/qtdMediasM4);
      //MDM4.Visible := true;
    end
    else
    begin
      MDM4.Caption := '';
    end;

    if ocoM5 then
    begin
      if qtdMediasM5 > 0 then
        MDM5.Caption := FormatFloat('#0.00',StrToFloat(MDM5.Caption)/qtdMediasM5);
      //MDM5.Visible := true;
    end
    else
    begin
      MDM5.Caption := '';
    end;

    if ocoM6 then
    begin
      if qtdMediasM6 > 0 then
        MDM6.Caption := FormatFloat('#0.00',StrToFloat(MDM6.Caption)/qtdMediasM6);
      //MDM6.Visible := true;
    end
    else
    begin
      MDM6.Caption := '';
    end;

    if ocoM7 then
    begin
      if qtdMediasM7 > 0 then
        MDM7.Caption := FormatFloat('#0.00',StrToFloat(MDM7.Caption)/qtdMediasM7);
      //MDM7.Visible := true;
    end
    else
    begin
      MDM7.Caption := '';
    end;

    if ocoM8 then
    begin
      if qtdMediasM8 > 0 then
        MDM8.Caption := FormatFloat('#0.00',StrToFloat(MDM8.Caption)/qtdMediasM8);
      //MDM8.Visible := true;
    end
    else
    begin
      MDM8.Caption := '';
    end;

    if ocoM9 then
    begin
      if qtdMediasM9 > 0 then
        MDM9.Caption := FormatFloat('#0.00',StrToFloat(MDM9.Caption)/qtdMediasM9);
      //MDM9.Visible := true;
    end
    else
    begin
      MDM9.Caption := '';
    end;

    if ocoM10 then
    begin
      if qtdMediasM10 > 0 then
        MDM10.Caption := FormatFloat('#0.00',StrToFloat(MDM10.Caption)/qtdMediasM10);
      //MDM10.Visible := true;
    end
    else
    begin
      MDM10.Caption := '';
    end;

    if ocoM11 then
    begin
      if qtdMediasM11 > 0 then
        MDM11.Caption := FormatFloat('#0.00',StrToFloat(MDM11.Caption)/qtdMediasM11);
      //MDM11.Visible := true;
    end
    else
    begin
      MDM11.Caption := '';
    end;

    if ocoM12 then
    begin
      if qtdMediasM12 > 0 then
        MDM12.Caption := FormatFloat('#0.00',StrToFloat(MDM12.Caption)/qtdMediasM12);
    //  MDM12.Visible := true;
    end
    else
    begin
      MDM12.Caption := '';
    end;
  //end;
end;

procedure TFrmRelGradeNotasAluno.QrlMedia11Print(sender: TObject;var Value: String);
begin
  inherited;
  if not(Sender is TQRLabel) then
    Exit;

  if Value = '0' then
  begin
    Value := '';
    Exit;
  end;

  if (Sender as TQRLabel).Tag = 0 then
    Exit;

  if Value <> '' then
    Value := FormatFloat('#0.00',(StrToFloat(Value)/(Sender as TQRLabel).Tag));
end;

procedure TFrmRelGradeNotasAluno.QRDBText1Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := Value + ' - ' + QryRelatorioNO_ALU.AsString;
end;

procedure TFrmRelGradeNotasAluno.QRBand3BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  //QRMLegenda.Lines.Text := vDescricaoMaterias;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelGradeNotasAluno]);

end.

