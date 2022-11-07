unit U_FrmRelHistAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  QrAngLbl;

type
  TFrmRelHistAluno = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRBand1: TQRBand;
    QRLabel6: TQRLabel;
    QRShape1: TQRShape;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRAngledLabel1: TQRAngledLabel;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText7: TQRDBText;
    QRShape2: TQRShape;
    QRLabel15: TQRLabel;
    QrlAvalAnoSerie: TQRLabel;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRLabel17: TQRLabel;
    QRLMat1: TQRLabel;
    QRLMat2: TQRLabel;
    QRLMat3: TQRLabel;
    QRLMat4: TQRLabel;
    QRLMat5: TQRLabel;
    QRLMat6: TQRLabel;
    QRLMat7: TQRLabel;
    QRLMat8: TQRLabel;
    QRShape15: TQRShape;
    QRLMat9: TQRLabel;
    QRShape16: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRShape20: TQRShape;
    QRLabel32: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel34: TQRLabel;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRShape27: TQRShape;
    QRLSer1: TQRLabel;
    QRLSer2: TQRLabel;
    QRLSer3: TQRLabel;
    QRLSer4: TQRLabel;
    QRLSer5: TQRLabel;
    QRLSer6: TQRLabel;
    QRLSer7: TQRLabel;
    QRLSer8: TQRLabel;
    QRLSer9: TQRLabel;
    QRShape28: TQRShape;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape33: TQRShape;
    QRShape34: TQRShape;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QRShape37: TQRShape;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape40: TQRShape;
    QRShape41: TQRShape;
    QRShape42: TQRShape;
    QRShape43: TQRShape;
    QRShape44: TQRShape;
    QRShape45: TQRShape;
    QRShape46: TQRShape;
    QRShape47: TQRShape;
    QRShape48: TQRShape;
    QRShape49: TQRShape;
    QRShape50: TQRShape;
    QRShape51: TQRShape;
    QRShape52: TQRShape;
    QRShape53: TQRShape;
    QRShape54: TQRShape;
    QRShape55: TQRShape;
    QRShape56: TQRShape;
    QRShape57: TQRShape;
    QRShape58: TQRShape;
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
    QRShape74: TQRShape;
    QRShape75: TQRShape;
    QRShape76: TQRShape;
    QRShape77: TQRShape;
    QRShape78: TQRShape;
    QRShape79: TQRShape;
    QRShape80: TQRShape;
    QRShape81: TQRShape;
    QRShape82: TQRShape;
    QRShape83: TQRShape;
    QRShape84: TQRShape;
    QRShape85: TQRShape;
    QRShape86: TQRShape;
    QRShape87: TQRShape;
    QRShape88: TQRShape;
    QRShape89: TQRShape;
    QRShape90: TQRShape;
    QRShape91: TQRShape;
    QRShape92: TQRShape;
    QRShape93: TQRShape;
    QRShape94: TQRShape;
    QRShape95: TQRShape;
    QRShape96: TQRShape;
    QRShape97: TQRShape;
    QRShape98: TQRShape;
    QRShape99: TQRShape;
    QRShape100: TQRShape;
    QRShape101: TQRShape;
    QRShape102: TQRShape;
    QRShape103: TQRShape;
    QRShape104: TQRShape;
    QRShape105: TQRShape;
    QRShape106: TQRShape;
    QRShape107: TQRShape;
    QRShape108: TQRShape;
    QRShape109: TQRShape;
    QRShape110: TQRShape;
    QRShape111: TQRShape;
    QRShape112: TQRShape;
    QRShape113: TQRShape;
    QRShape114: TQRShape;
    QRShape115: TQRShape;
    QRShape116: TQRShape;
    QRShape117: TQRShape;
    QRShape118: TQRShape;
    QRShape119: TQRShape;
    QRShape120: TQRShape;
    QRShape121: TQRShape;
    QRShape122: TQRShape;
    QRShape123: TQRShape;
    QRShape124: TQRShape;
    QRShape125: TQRShape;
    QRShape126: TQRShape;
    QRShape127: TQRShape;
    QRShape128: TQRShape;
    QRShape129: TQRShape;
    QRShape130: TQRShape;
    QRShape131: TQRShape;
    QRShape132: TQRShape;
    QRShape133: TQRShape;
    QRShape134: TQRShape;
    QRShape135: TQRShape;
    QRShape136: TQRShape;
    QRShape137: TQRShape;
    QRShape138: TQRShape;
    QRShape139: TQRShape;
    QRShape140: TQRShape;
    QRShape141: TQRShape;
    QRShape142: TQRShape;
    QRShape143: TQRShape;
    QRShape144: TQRShape;
    QRShape145: TQRShape;
    QRShape146: TQRShape;
    QRShape147: TQRShape;
    QRShape148: TQRShape;
    L11: TQRLabel;
    L21: TQRLabel;
    L31: TQRLabel;
    L41: TQRLabel;
    L51: TQRLabel;
    L61: TQRLabel;
    L71: TQRLabel;
    L81: TQRLabel;
    L91: TQRLabel;
    L12: TQRLabel;
    L22: TQRLabel;
    L32: TQRLabel;
    L42: TQRLabel;
    L52: TQRLabel;
    L62: TQRLabel;
    L72: TQRLabel;
    L82: TQRLabel;
    L92: TQRLabel;
    L13: TQRLabel;
    L23: TQRLabel;
    L33: TQRLabel;
    L43: TQRLabel;
    L53: TQRLabel;
    L63: TQRLabel;
    L73: TQRLabel;
    L83: TQRLabel;
    L93: TQRLabel;
    L14: TQRLabel;
    L24: TQRLabel;
    L34: TQRLabel;
    L44: TQRLabel;
    L54: TQRLabel;
    L64: TQRLabel;
    L74: TQRLabel;
    L84: TQRLabel;
    L94: TQRLabel;
    L15: TQRLabel;
    L25: TQRLabel;
    L35: TQRLabel;
    L45: TQRLabel;
    L55: TQRLabel;
    L65: TQRLabel;
    L75: TQRLabel;
    L85: TQRLabel;
    L95: TQRLabel;
    L16: TQRLabel;
    L26: TQRLabel;
    L36: TQRLabel;
    L46: TQRLabel;
    L56: TQRLabel;
    L66: TQRLabel;
    L76: TQRLabel;
    L86: TQRLabel;
    L96: TQRLabel;
    L17: TQRLabel;
    L27: TQRLabel;
    L37: TQRLabel;
    L47: TQRLabel;
    L57: TQRLabel;
    L67: TQRLabel;
    L77: TQRLabel;
    L87: TQRLabel;
    L97: TQRLabel;
    L18: TQRLabel;
    L28: TQRLabel;
    L38: TQRLabel;
    L48: TQRLabel;
    L58: TQRLabel;
    L68: TQRLabel;
    L78: TQRLabel;
    L88: TQRLabel;
    L98: TQRLabel;
    L19: TQRLabel;
    L29: TQRLabel;
    L39: TQRLabel;
    L49: TQRLabel;
    L59: TQRLabel;
    L69: TQRLabel;
    L79: TQRLabel;
    L89: TQRLabel;
    L99: TQRLabel;
    QRLM1ResFin: TQRLabel;
    QRLM2ResFin: TQRLabel;
    QRLM3ResFin: TQRLabel;
    QRLM4ResFin: TQRLabel;
    QRLM5ResFin: TQRLabel;
    QRLM6ResFin: TQRLabel;
    QRLM7ResFin: TQRLabel;
    QRLM8ResFin: TQRLabel;
    QRLM9ResFin: TQRLabel;
    QryAux: TADOQuery;
    QrySQL: TADOQuery;
    qryCurso: TADOQuery;
    QRLabel14: TQRLabel;
    QRLPage: TQRLabel;
    CH1: TQRLabel;
    CH2: TQRLabel;
    CH3: TQRLabel;
    CH4: TQRLabel;
    CH5: TQRLabel;
    CH6: TQRLabel;
    CH7: TQRLabel;
    CH8: TQRLabel;
    CH9: TQRLabel;
    FR1: TQRLabel;
    FR2: TQRLabel;
    FR3: TQRLabel;
    FR5: TQRLabel;
    FR6: TQRLabel;
    FR7: TQRLabel;
    FR8: TQRLabel;
    FR9: TQRLabel;
    FR4: TQRLabel;
    QRLAta: TQRLabel;
    QRLEndEmp: TQRLabel;
    QRLMat10: TQRLabel;
    L110: TQRLabel;
    L210: TQRLabel;
    L310: TQRLabel;
    L410: TQRLabel;
    L510: TQRLabel;
    L610: TQRLabel;
    L710: TQRLabel;
    L810: TQRLabel;
    L910: TQRLabel;
    QRLInstNuCNPJ: TQRLabel;
    QRLCEPCidUF: TQRLabel;
    QRLabel18: TQRLabel;
    QRLSexo: TQRLabel;
    QRLabel19: TQRLabel;
    QRDBText6: TQRDBText;
    QRLNacionalidade: TQRLabel;
    QRShape5: TQRShape;
    QRShape17: TQRShape;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLNoSecretario: TQRLabel;
    QRLNoDiretor: TQRLabel;
    QRLabel22: TQRLabel;
    QRLMatSecretario: TQRLabel;
    QRLabel23: TQRLabel;
    QRLMatDiretor: TQRLabel;
    QRLCdUFDia: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codmaterias : array[1..10] of integer;
	  contmateria: integer;
	  contcurso: integer;
    codigoEmpresa : String;
  end;

var
  FrmRelHistAluno: TFrmRelHistAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelHistAluno.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
  var
  mf1,mf2,mf3,mf4,mf5,mf6,mf7,mf8,mf9 : Double;
  cf1,cf2,cf3,cf4,cf5,cf6,cf7,cf8,cf9 : Integer;
    i: integer;
begin
  inherited;
    mf1 := 0;
    mf2 := 0;
    mf3 := 0;
    mf4 := 0;
    mf5 := 0;
    mf6 := 0;
    mf7 := 0;
    mf8 := 0;
    mf9 := 0;
    cf1 := 1;
    cf2 := 1;
    cf3 := 1;
    cf4 := 1;
    cf5 := 1;
    cf6 := 1;
    cf7 := 1;
    cf8 := 1;
    cf9 := 1;

  qryCurso.Close;
  qryCurso.SQL.Clear;
  qryCurso.SQL.Text := 'SELECT * FROM tb01_curso c ' +
    'JOIN tb08_matrcur m ON m.co_cur = c.co_cur ' +
    'WHERE m.co_alu = ' + qryRelatorio.FieldByName('co_alu').AsString +
    ' and m.co_emp = ' + codigoEmpresa;
  qryCurso.Open;

  while not qryCurso.Eof do
  begin
    contcurso := contcurso + 1;

    case contcurso of
      1:
      begin
        QrlSer1.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH1.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR1.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
             //   if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR1.Caption := '-'
                else
                  FR1.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                  //FR1.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
           // end;
         // end;
        end;
        //
      end;
      2:
      begin
        QrlSer2.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH2.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR2.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
             //   if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR2.Caption := '-'
                else
                begin
                  FR2.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                end;
                  //FR2.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
          //  end;
         // end;
        end;
        //
      end;
      3:
      begin
        QrlSer3.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH3.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR3.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
                //if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR3.Caption := '-'
                else
                  FR3.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                  //FR3.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
          //  end;
         // end;
        end;
        //
      end;
      4:
      begin
        QrlSer4.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH4.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR4.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
                //if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR4.Caption := '-'
                else
                  FR4.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                  //FR4.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
           // end;
         // end;
        end;
        //
      end;
      5:
      begin
        QrlSer5.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH5.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR5.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
                //if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR5.Caption := '-'
                else
                  FR5.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                  //FR5.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
           // end;
         // end;
        end;
        //
      end;
      6:
      begin
        QrlSer6.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH6.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR6.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
                //if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR6.Caption := '-'
                else
                  FR6.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                  //FR6.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
         //   end;
        //  end;
        end;
        //
      end;
      7:
      begin
        QrlSer7.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH7.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR7.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
                //if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR7.Caption := '-'
                else
                  FR7.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                  //FR7.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
          //  end;
        //  end;
        end;
        //
      end;
      8:
      begin
        QrlSer8.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH8.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR8.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
                //if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR8.Caption := '-'
                else
                  FR8.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                  //FR8.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
           // end;
        //  end;
        end;
        //
      end;
      9:
      begin
        QrlSer9.Caption := qryCurso.FieldByName('CO_SIGL_CUR').AsString;
        CH9.Caption := qryCurso.FieldByName('QT_CARG_HORA_CUR').AsString;
        //FR9.Caption := qryCurso.fieldByName('PE_FALT_CUR').AsString + '%';
        //Percentual de Faltas
            with DM.QrySql2 do
            begin
              Close;
              SQL.Clear;
              SQL.Text := 'select count(co_alu) as totalPre FROM TB132_FREQ_ALU' +
                          ' where co_cur = ' + qryCurso.FieldByName('co_cur').AsString +
                          ' and co_emp_alu = ' + codigoEmpresa +
                          ' and co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                          ' and year(DT_FRE) = ' + QuotedStr(qryCurso.FieldByName('CO_ANO_MES_MAT').AsString) +
                          ' and CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S');
              Open;

              if not IsEmpty then
              begin
                //if (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').IsNull) or (DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat = 0) then
                if qryCurso.FieldByName('QT_CARG_HORA_CUR').IsNull then
                  FR9.Caption := '-'
                else
                  FR9.Caption := FloatToStrF(((DM.QrySql2.FieldByName('totalPre').AsInteger * 100)/qryCurso.FieldByName('QT_CARG_HORA_CUR').AsFloat),ffNumber,10,2) + '%';
                  //FR9.Caption := FloatToStrF(((DataModuleSge.QrySql2.FieldByName('totalPre').AsInteger * 100)/DataModuleSGE.QrySql.FieldByName('QTDE_AULAS_REAL').AsFloat),ffNumber,10,2) + '%';
              end;
          //  end;
         // end;
        end;
        //
      end;

    end;

    qrySQL.Close;
    qrySQL.SQL.Clear;
    qrySQL.SQL.Text := 'SELECT * FROM tb079_hist_aluno ' +
      'WHERE co_alu = ' + qryRelatorio.FieldByName('co_alu').AsString +
      ' and co_cur = ' + qryCurso.FieldByName('co_cur').AsString;
    qrySQL.Open;

    while not qrySQL.Eof do
    begin
      for i:=1 to 10 do
      begin
        if (qrySQL.FieldByName('co_mat').AsInteger = codmaterias[i]) then
          break;
      end;

      if i>9 then
      begin
        contmateria := contmateria + 1;

        qryAux.Close;
        qryAux.SQL.Clear;
        qryAux.SQL.Text := ' SELECT m.co_mat, cm.no_materia FROM tb02_materia m, tb107_cadmaterias cm ' +
                           ' WHERE m.co_mat = ' + qrySQL.FieldByName('co_mat').AsString +
                           ' and m.id_materia = cm.id_materia';
        qryAux.Open;

        case contmateria of
          1: QRLMat1.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          2: QRLMat2.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          3: QRLMat3.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          4: QRLMat4.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          5: QRLMat5.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          6: QRLMat6.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          7: QRLMat7.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          8: QRLMat8.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          9: QRLMat9.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
          10: QRLMat10.Caption := qryAux.FieldByName('NO_MATERIA').AsString;
        end;

        codmaterias[contmateria] := qrySQL.FieldByName('co_mat').AsInteger;

        if (contcurso = 1) and (contmateria = 1) then
        begin
          L11.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 2) then
        begin
          L12.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 3) then
        begin
          L13.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 4) then
        begin
          L14.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 5) then
        begin
          L15.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 6) then
        begin
          L16.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 7) then
        begin
          L17.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 8) then
        begin
          L18.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 9) then
        begin
          L19.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (contmateria = 10) then
        begin
          L110.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 2) and (contmateria = 1) then
        begin
          L21.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 2) then
        begin
          L22.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 3) then
        begin
          L23.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 4) then
        begin
          L24.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 5) then
        begin
          L25.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 6) then
        begin
          L26.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 7) then
        begin
          L27.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 8) then
        begin
          L28.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 9) then
        begin
          L29.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (contmateria = 10) then
        begin
          L210.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 3) and (contmateria = 1) then
        begin
          L31.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (contmateria = 2) then
        begin
          L32.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (contmateria = 3) then
        begin
          L33.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (contmateria = 4) then
        begin
          L34.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (contmateria = 5) then
        begin
          L35.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (contmateria = 6) then
        begin
          L36.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (contmateria = 7) then
        begin
          L37.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (contmateria = 8) then
        begin
          L38.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (contmateria = 9) then
        begin
          L39.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 4) and (contmateria = 1) then
        begin
          L41.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 2) then
        begin
          L42.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 3) then
        begin
          L43.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 4) then
        begin
          L44.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 5) then
        begin
          L45.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 6) then
        begin
          L46.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 7) then
        begin
          L47.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 8) then
        begin
          L48.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 9) then
        begin
          L49.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (contmateria = 10) then
        begin
          L410.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 5) and (contmateria = 1) then
        begin
          L51.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 2) then
        begin
          L52.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 3) then
        begin
          L53.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 4) then
        begin
          L54.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 5) then
        begin
          L55.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 6) then
        begin
          L56.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 7) then
        begin
          L57.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 8) then
        begin
          L58.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 9) then
        begin
          L59.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (contmateria = 10) then
        begin
          L510.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 6) and (contmateria = 1) then
        begin
          L61.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 2) then
        begin
          L62.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 3) then
        begin
          L63.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 4) then
        begin
          L64.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 5) then
        begin
          L65.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 6) then
        begin
          L66.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 7) then
        begin
          L67.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 8) then
        begin
          L68.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 9) then
        begin
          L69.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (contmateria = 10) then
        begin
          L610.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 7) and (contmateria = 1) then
        begin
          L71.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 2) then
        begin
          L72.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 3) then
        begin
          L73.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 4) then
        begin
          L74.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 5) then
        begin
          L75.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 6) then
        begin
          L76.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 7) then
        begin
          L77.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 8) then
        begin
          L78.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 9) then
        begin
          L79.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (contmateria = 10) then
        begin
          L710.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 8) and (contmateria = 1) then
        begin
          L81.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 2) then
        begin
          L82.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 3) then
        begin
          L83.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 4) then
        begin
          L84.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 5) then
        begin
          L85.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 6) then
        begin
          L86.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 7) then
        begin
          L87.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 8) then
        begin
          L88.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 9) then
        begin
          L89.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (contmateria = 10) then
        begin
          L810.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 9) and (contmateria = 1) then
        begin
          L91.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 2) then
        begin
          L92.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 3) then
        begin
          L93.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 4) then
        begin
          L94.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 5) then
        begin
          L95.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 6) then
        begin
          L96.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 7) then
        begin
          L97.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 8) then
        begin
          L98.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 9) then
        begin
          L99.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (contmateria = 10) then
        begin
          L910.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

      end
      else
      begin
        if (contcurso = 1) and (i = 1) then
        begin
          L11.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 2) then
        begin
          L12.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 3) then
        begin
          L13.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 4) then
        begin
          L14.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 5) then
        begin
          L15.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 6) then
        begin
          L16.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 7) then
        begin
          L17.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 8) then
        begin
          L18.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 9) then
        begin
          L19.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 1) and (i = 10) then
        begin
          L110.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf1 := cf1 + 1;
          mf1 := mf1 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 2) and (i = 1) then
        begin
          L21.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 2) then
        begin
          L22.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 3) then
        begin
          L23.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 4) then
        begin
          L24.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 5) then
        begin
          L25.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 6) then
        begin
          L26.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 7) then
        begin
          L27.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 8) then
        begin
          L28.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 9) then
        begin
          L29.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 2) and (i = 10) then
        begin
          L210.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf2 := cf2 + 1;
          mf2 := mf2 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 3) and (i = 1) then
        begin
          L31.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 2) then
        begin
          L32.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 3) then
        begin
          L33.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 4) then
        begin
          L34.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 5) then
        begin
          L35.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 6) then
        begin
          L36.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 7) then
        begin
          L37.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 8) then
        begin
          L38.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 9) then
        begin
          L39.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 3) and (i = 10) then
        begin
          L310.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf3 := cf3 + 1;
          mf3 := mf3 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 4) and (i = 1) then
        begin
          L41.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 2) then
        begin
          L42.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 3) then
        begin
          L43.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 4) then
        begin
          L44.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 5) then
        begin
          L45.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 6) then
        begin
          L46.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 7) then
        begin
          L47.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 8) then
        begin
          L48.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 9) then
        begin
          L49.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 4) and (i = 10) then
        begin
          L410.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf4 := cf4 + 1;
          mf4 := mf4 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 5) and (i = 1) then
        begin
          L51.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 2) then
        begin
          L52.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 3) then
        begin
          L53.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 4) then
        begin
          L54.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 5) then
        begin
          L55.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 6) then
        begin
          L56.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 7) then
        begin
          L57.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 8) then
        begin
          L58.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 9) then
        begin
          L59.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 5) and (i = 10) then
        begin
          L510.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf5 := cf5 + 1;
          mf5 := mf5 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 6) and (i = 1) then
        begin
          L61.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 2) then
        begin
          L62.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 3) then
        begin
          L63.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 4) then
        begin
          L64.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 5) then
        begin
          L65.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 6) then
        begin
          L66.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 7) then
        begin
          L67.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 8) then
        begin
          L68.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 9) then
        begin
          L69.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 6) and (i = 10) then
        begin
          L610.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf6 := cf6 + 1;
          mf6 := mf6 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 7) and (i = 1) then
        begin
          L71.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 2) then
        begin
          L72.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 3) then
        begin
          L73.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 4) then
        begin
          L74.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 5) then
        begin
          L75.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 6) then
        begin
          L76.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 7) then
        begin
          L77.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 8) then
        begin
          L78.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 9) then
        begin
          L79.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 7) and (i = 10) then
        begin
          L710.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf7 := cf7 + 1;
          mf7 := mf7 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 8) and (i = 1) then
        begin
          L81.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 2) then
        begin
          L82.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 3) then
        begin
          L83.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 4) then
        begin
          L84.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 5) then
        begin
          L85.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 6) then
        begin
          L86.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 7) then
        begin
          L87.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 8) then
        begin
          L88.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 9) then
        begin
          L89.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 8) and (i = 10) then
        begin
          L810.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf8 := cf8 + 1;
          mf8 := mf8 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;

        if (contcurso = 9) and (i = 1) then
        begin
          L91.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 2) then
        begin
          L92.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 3) then
        begin
          L93.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 4) then
        begin
          L94.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 5) then
        begin
          L95.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 6) then
        begin
          L96.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 7) then
        begin
          L97.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 8) then
        begin
          L98.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 9) then
        begin
          L99.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
        if (contcurso = 9) and (i = 10) then
        begin
          L910.Caption := FloatToStrF(qrySQL.FieldByName('vl_media_final').AsFloat,ffNumber,10,2);
          cf9 := cf9 + 1;
          mf9 := mf9 + qrySQL.fieldByName('vl_media_final').AsFloat;
        end;
      end;

      qrySQL.Next;
    end;
    qryCurso.Next;
  end;
  QRLM1ResFin.Caption := FormatFloat('##.##',mf1 / cf1);
  QRLM2ResFin.Caption := FormatFloat('##.##',mf2 / cf2);
  QRLM3ResFin.Caption := FormatFloat('##.##',mf3 / cf3);
  QRLM4ResFin.Caption := FormatFloat('##.##',mf4 / cf4);
  QRLM5ResFin.Caption := FormatFloat('##.##',mf5 / cf5);
  QRLM6ResFin.Caption := FormatFloat('##.##',mf6 / cf6);
  QRLM7ResFin.Caption := FormatFloat('##.##',mf7 / cf7);
  QRLM8ResFin.Caption := FormatFloat('##.##',mf8 / cf8);
  QRLM9ResFin.Caption := FormatFloat('##.##',mf9 / cf9);

  with DM.QrySql do
  begin
    Close;
    SQl.Clear;
    SQL.Text := 'select c.no_col,c.co_mat_col from tb25_empresa e ' +
                ' join tb03_colabor c on c.co_col = e.co_dir and c.co_emp = e.co_emp ' +
                ' where e.co_emp = ' + codigoEmpresa;
    Open;

    if not IsEmpty then
    begin
      QRLNoDiretor.Caption := FieldByName('no_col').AsString;
      QRLMatDiretor.Caption := FormatMaskText('00.000-0;0',FieldByName('co_mat_col').AsString)
    end
    else
    begin
      QRLNoDiretor.Caption := '';
      QRLMatDiretor.Caption := '';
    end;
  end;
  //Escrever Secretário
  with DM.QrySql do
  begin
    Close;
    SQl.Clear;
    SQL.Text := 'select c.no_col,c.co_mat_col from tb25_empresa e ' +
                ' join tb03_colabor c on c.co_col = e.co_sec and c.co_emp = e.co_emp ' +
                ' where e.co_emp = ' + codigoEmpresa;
    Open;

    if not IsEmpty then
    begin
      QRLNoSecretario.Caption := FieldByName('no_col').AsString;
      QRLMatSecretario.Caption := FormatMaskText('00.000-0;0',FieldByName('co_mat_col').AsString)
    end
    else
    begin
      QRLNoSecretario.Caption := '';
      QRLMatSecretario.Caption := '';
    end;
  end;
  if not qryRelatorio.FieldByName('no_cidade').IsNull then
    QRLCdUFDia.Caption := qryRelatorio.FieldByName('no_cidade').AsString + ' - ' + qryRelatorio.FieldByName('CO_UF_EMP').AsString +
  ', ' + FormatDateTime('dd/MM/yyyy',now);

end;

procedure TFrmRelHistAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
var
    i: integer;
begin
  inherited;
  for i:=1 to 10 do
	begin
		codmaterias[i] := 0;
	end;

	contmateria := 0;
	contcurso := 0;
end;

procedure TFrmRelHistAluno.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';
  
  QRLAta.Caption := QryRelatorio.FieldByName('NU_ATA').AsString + '-' +
                    QryRelatorio.FieldByName('DT_ATA').AsString;

  QRLEndEmp.Caption := 'Endereço: ' + QryRelatorio.FieldByName('DE_END_EMP').AsString + ', ' +
                       QryRelatorio.FieldByName('DE_COM_ENDE_EMP').AsString + ' - Bairro ' +
                       QryRelatorio.FieldByName('NO_BAIRRO').AsString;
  QRLInstNuCNPJ.Caption := 'Instituição: ' + QryRelatorio.FieldByName('NO_RAZSOC_EMP').AsString +
  ' - CNPJ nº ' + FormatMaskText('99.999.999/9999-99;0;_',QryRelatorio.FieldByName('CO_CPFCGC_EMP').AsString);
  QRLCEPCidUF.Caption := 'CEP: ' + FormatMaskText('99999-999;0; ',QryRelatorio.FieldByName('CO_CEP_EMP').AsString) +
  ' - ' + QryRelatorio.FieldByName('NO_CIDADE').AsString + ' - ' + QryRelatorio.FieldByName('CO_UF_EMP').AsString;

  if QryRelatorio.FieldByName('CO_SEXO_ALU').AsString = 'M' then
    QRLSexo.Caption := 'Masculino'
  else
    QRLSexo.Caption := 'Feminino';

  if QryRelatorio.FieldByName('CO_NACI_ALU').AsString = 'B' then
    QRLNacionalidade.Caption := 'Brasileiro'
  else
    QRLNacionalidade.Caption := QryRelatorio.FIeldByName('DE_NACI_ALU').AsString;

end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelHistAluno]);

end.
