unit U_FrmRelFreqAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelFreqAluno = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    SummaryBand1: TQRBand;
    QrlTPunidade: TQRLabel;
    QrlCurso: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRShape7: TQRShape;
    QRLabel23: TQRLabel;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRLabel24: TQRLabel;
    QRShape10: TQRShape;
    QRLabel25: TQRLabel;
    QRShape11: TQRShape;
    QRLabel26: TQRLabel;
    QRShape12: TQRShape;
    QRLabel27: TQRLabel;
    QRShape13: TQRShape;
    QRLabel28: TQRLabel;
    QRShape14: TQRShape;
    QRLabel29: TQRLabel;
    QRShape15: TQRShape;
    QRLabel30: TQRLabel;
    QRShape16: TQRShape;
    QRLabel31: TQRLabel;
    QRShape17: TQRShape;
    QRLabel32: TQRLabel;
    QRShape18: TQRShape;
    QRLabel33: TQRLabel;
    QRShape19: TQRShape;
    QRLabel34: TQRLabel;
    QRShape20: TQRShape;
    QRLabel35: TQRLabel;
    QRShape21: TQRShape;
    QRLabel36: TQRLabel;
    QRShape22: TQRShape;
    QRLabel37: TQRLabel;
    QRShape23: TQRShape;
    QRLabel38: TQRLabel;
    QRShape24: TQRShape;
    QRLabel39: TQRLabel;
    QRShape25: TQRShape;
    QRLabel40: TQRLabel;
    QRShape26: TQRShape;
    QRLabel41: TQRLabel;
    QRShape27: TQRShape;
    QRLabel42: TQRLabel;
    QRShape28: TQRShape;
    QRLabel43: TQRLabel;
    QRLabel15: TQRLabel;
    QRShape29: TQRShape;
    QRLabel16: TQRLabel;
    QRShape30: TQRShape;
    QRLabel17: TQRLabel;
    QRShape31: TQRShape;
    QRLabel44: TQRLabel;
    QRShape32: TQRShape;
    QRLabel45: TQRLabel;
    QRShape33: TQRShape;
    QRLabel46: TQRLabel;
    QRShape34: TQRShape;
    QRLabel47: TQRLabel;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QrlMatricula: TQRLabel;
    QRShape37: TQRShape;
    QRShape39: TQRShape;
    QRShape40: TQRShape;
    QrlTotAluno: TQRLabel;
    QrlTotFaltTurmaPorcent: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText5: TQRDBText;
    QrlTurno: TQRLabel;
    QRLabel50: TQRLabel;
    QRLPage: TQRLabel;
    QrlQTsexo: TQRLabel;
    QRLabel48: TQRLabel;
    QryFreqAluno: TADOQuery;
    QrlP01: TQRLabel;
    QRShape41: TQRShape;
    QRShape42: TQRShape;
    QrlP02: TQRLabel;
    QRShape43: TQRShape;
    QRShape44: TQRShape;
    QRShape45: TQRShape;
    QRShape46: TQRShape;
    QrlP03: TQRLabel;
    QRShape47: TQRShape;
    QrlP06: TQRLabel;
    QrlP05: TQRLabel;
    QrlP04: TQRLabel;
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
    QrlP07: TQRLabel;
    QrlP08: TQRLabel;
    QrlP09: TQRLabel;
    QrlP10: TQRLabel;
    QrlP11: TQRLabel;
    QrlP12: TQRLabel;
    QrlP24: TQRLabel;
    QrlP23: TQRLabel;
    QrlP22: TQRLabel;
    QrlP21: TQRLabel;
    QrlP20: TQRLabel;
    QrlP19: TQRLabel;
    QrlP18: TQRLabel;
    QrlP17: TQRLabel;
    QrlP16: TQRLabel;
    QrlP15: TQRLabel;
    QrlP14: TQRLabel;
    QrlP13: TQRLabel;
    QrlP31: TQRLabel;
    QrlP30: TQRLabel;
    QrlP29: TQRLabel;
    QrlP28: TQRLabel;
    QrlP27: TQRLabel;
    QrlP26: TQRLabel;
    QrlP25: TQRLabel;
    QRShape74: TQRShape;
    QRShape76: TQRShape;
    QRShape77: TQRShape;
    QrlAnoBase: TQRLabel;
    QrlMesRef: TQRLabel;
    QrlDiasUteis: TQRLabel;
    QrySexo: TADOQuery;
    QrySexoMASC: TIntegerField;
    QrySexoFEM: TIntegerField;
    QrlEduc01: TQRLabel;
    QrlEduc02: TQRLabel;
    QryTF: TADOQuery;
    QrlTotalFaltas: TQRLabel;
    QrlMes: TQRLabel;
    QrlTotFaltTurma: TQRLabel;
    QrlTFPorcent: TQRLabel;
    QryFreqAlunoP01: TStringField;
    QryFreqAlunoP02: TStringField;
    QryFreqAlunoP03: TStringField;
    QryFreqAlunoP04: TStringField;
    QryFreqAlunoP05: TStringField;
    QryFreqAlunoP06: TStringField;
    QryFreqAlunoP07: TStringField;
    QryFreqAlunoP08: TStringField;
    QryFreqAlunoP09: TStringField;
    QryFreqAlunoP10: TStringField;
    QryFreqAlunoP11: TStringField;
    QryFreqAlunoP12: TStringField;
    QryFreqAlunoP13: TStringField;
    QryFreqAlunoP14: TStringField;
    QryFreqAlunoP15: TStringField;
    QryFreqAlunoP16: TStringField;
    QryFreqAlunoP17: TStringField;
    QryFreqAlunoP18: TStringField;
    QryFreqAlunoP19: TStringField;
    QryFreqAlunoP20: TStringField;
    QryFreqAlunoP21: TStringField;
    QryFreqAlunoP22: TStringField;
    QryFreqAlunoP23: TStringField;
    QryFreqAlunoP24: TStringField;
    QryFreqAlunoP25: TStringField;
    QryFreqAlunoP26: TStringField;
    QryFreqAlunoP27: TStringField;
    QryFreqAlunoP28: TStringField;
    QryFreqAlunoP29: TStringField;
    QryFreqAlunoP30: TStringField;
    QryFreqAlunoP31: TStringField;
    QrlNumAluno: TQRLabel;
    QrySexoNO_TURMA: TStringField;
    QryTFCO_EMP: TIntegerField;
    QryTFCO_CUR: TIntegerField;
    QryTFCO_MAT: TIntegerField;
    QryTFCO_ANO_REF: TIntegerField;
    QryTFQT_DIAS_LETIVO_JAN: TIntegerField;
    QryTFQT_AULAS_PROG_JAN: TIntegerField;
    QryTFQT_AULAS_REAL_JAN: TIntegerField;
    QryTFQT_DIAS_LETIVO_FEV: TIntegerField;
    QryTFQT_AULAS_PROG_FEV: TIntegerField;
    QryTFQT_AULAS_REAL_FEV: TIntegerField;
    QryTFQT_DIAS_LETIVO_MAR: TIntegerField;
    QryTFQT_AULAS_PROG_MAR: TIntegerField;
    QryTFQT_AULAS_REAL_MAR: TIntegerField;
    QryTFQT_DIAS_LETIVO_ABR: TIntegerField;
    QryTFQT_AULAS_PROG_ABR: TIntegerField;
    QryTFQT_AULAS_REAL_ABR: TIntegerField;
    QryTFQT_DIAS_LETIVO_MAI: TIntegerField;
    QryTFQT_AULAS_PROG_MAI: TIntegerField;
    QryTFQT_AULAS_REAL_MAI: TIntegerField;
    QryTFQT_DIAS_LETIVO_JUN: TIntegerField;
    QryTFQT_AULAS_PROG_JUN: TIntegerField;
    QryTFQT_AULAS_REAL_JUN: TIntegerField;
    QryTFQT_DIAS_LETIVO_JUL: TIntegerField;
    QryTFQT_AULAS_PROG_JUL: TIntegerField;
    QryTFQT_AULAS_REAL_JUL: TIntegerField;
    QryTFQT_DIAS_LETIVO_AGO: TIntegerField;
    QryTFQT_AULAS_PROG_AGO: TIntegerField;
    QryTFQT_AULAS_REAL_AGO: TIntegerField;
    QryTFQT_DIAS_LETIVO_SET: TIntegerField;
    QryTFQT_AULAS_PROG_SET: TIntegerField;
    QryTFQT_AULAS_REAL_SET: TIntegerField;
    QryTFQT_DIAS_LETIVO_OUT: TIntegerField;
    QryTFQT_AULAS_PROG_OUT: TIntegerField;
    QryTFQT_AULAS_REAL_OUT: TIntegerField;
    QryTFQT_DIAS_LETIVO_NOV: TIntegerField;
    QryTFQT_AULAS_PROG_NOV: TIntegerField;
    QryTFQT_AULAS_REAL_NOV: TIntegerField;
    QryTFQT_DIAS_LETIVO_DEZ: TIntegerField;
    QryTFQT_AULAS_PROG_DEZ: TIntegerField;
    QryTFQT_AULAS_REAL_DEZ: TIntegerField;
    QryTFQT_AULA_CUR: TIntegerField;
    QryTFTFMES: TIntegerField;
    QryTFTFTURMA: TIntegerField;
    QryTFCO_MODU_CUR: TIntegerField;
    QryTFCO_ALU: TIntegerField;
    QryFreqAlunoCO_ALU: TIntegerField;
    QRLNoAlu: TQRLabel;
    QRDBText3: TQRDBText;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    totFeminino, totMasculino : Integer;
  public
    { Public declarations }
    co_disciplina : Integer;
    codigoEmpresa : String;
  end;

var
  FrmRelFreqAluno: TFrmRelFreqAluno;

implementation

{$R *.dfm}
uses U_DataModuleSGE, DateUtils, MaskUtils;

procedure TFrmRelFreqAluno.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
  var
    SqlString : String;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';
    
  QrlTFPorcent.Caption := '-';

  if QryRelatorio.FieldByName('CO_SEXO_ALU').AsString = 'M' then
    totMasculino := totMasculino + 1
  else
    totFeminino := totFeminino + 1;

  // ZEBRADO
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00EBEBEB
  else
    DetailBand1.Color := clWhite;

  // MATR�CULA
  if not QryRelatorio.FieldByName('CO_ALU_CAD').IsNull then
    QrlMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorio.FieldByName('CO_ALU_CAD').AsString);

  // TOTAL DE ALUNOS
  QrlTotAluno.Caption := IntToStr(StrToInt(QrlTotAluno.Caption) + 1);
  QrlNumAluno.Caption := IntToStr(StrToInt(QrlNumAluno.Caption) + 1);

  with DM.QrySql do
  begin
    CLose;
    SQL.Clear;
    if QryRelatorio.FieldByName('CO_PARAM_FREQ_TIPO').asString = 'M' then
      SQL.Text := 'SELECT COUNT(*) as TFMES, ' +
                '  (SELECT COUNT(*) FROM TB132_FREQ_ALU F '+
                '   WHERE YEAR(F.DT_FRE) = ' + QrlAnoBase.Caption +
                '   and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
                '   and F.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                '   and F.CO_TUR = ' + QryRelatorio.FieldByName('CO_TUR').AsString +
                '    AND F.CO_MODU_CUR = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                '  	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                ' and F.CO_MAT = ' + IntToStr(co_disciplina) +
                '  ) TFTURMA '+
                ' FROM TB132_FREQ_ALU F '+
                '   WHERE YEAR(F.DT_FRE) = ' + QrlAnoBase.Caption +
                '  	  and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
                '	  and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                '  	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                ' and F.CO_MAT = ' + IntToStr(co_disciplina)
    else
      SQL.Text := 'SELECT COUNT(*) as TFMES, ' +
                '  (SELECT COUNT(*) FROM TB132_FREQ_ALU F '+
                '   WHERE YEAR(F.DT_FRE) = ' + QrlAnoBase.Caption +
                '   and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
                '   and F.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                '   and F.CO_TUR = ' + QryRelatorio.FieldByName('CO_TUR').AsString +
                '    AND F.CO_MODU_CUR = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                '  	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                ' and F.CO_MAT is null '+
                '  ) TFTURMA '+
                ' FROM TB132_FREQ_ALU F '+
                '   WHERE YEAR(F.DT_FRE) = ' + QrlAnoBase.Caption +
                '  	  and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
                '	  and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                '  	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                ' and F.CO_MAT is null';
    Open;

    if not IsEmpty then
    begin
      QrlTotalFaltas.Caption := FieldByName('TFMES').AsString;
      QrlTotFaltTurma.Caption := 'FALTAS TURMA: ' + FieldByName('TFTURMA').AsString + '   -';
    end;
  end;

  // TOTAL DE FALTAS
  with QryTF do
  begin
    Close;
    Sql.Clear;
    if QryRelatorio.FieldByName('CO_PARAM_FREQ_TIPO').asString = 'M' then
      Sql.Text := '  SELECT DISTINCT QTA.*, F.CO_ALU, C.QT_AULA_CUR, '+
                '  (SELECT COUNT(*) FROM TB132_FREQ_ALU F '+
                '   WHERE YEAR(F.DT_FRE) = ' + QrlAnoBase.Caption +
                '  	  and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
                '	  and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                '  	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                ' and F.CO_MAT = ' + IntToStr(co_disciplina) +
                ' ) TFMES, '+
                '  (SELECT COUNT(*) FROM TB132_FREQ_ALU F '+
                '   WHERE YEAR(F.DT_FRE) = ' + QrlAnoBase.Caption +
                '   and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
                '   and F.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                '   and F.CO_TUR = ' + QryRelatorio.FieldByName('CO_TUR').AsString +
                '    AND F.CO_MODU_CUR = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                '  	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                ' and F.CO_MAT = ' + IntToStr(co_disciplina) +
                '  ) TFTURMA '+
                '   FROM TB132_FREQ_ALU F '+
                '  JOIN TB01_CURSO C ON C.CO_CUR = F.CO_CUR '+
                '  JOIN TB_QTDE_AULAS QTA ON QTA.CO_CUR = C.CO_CUR AND QTA.CO_MAT = F.CO_MAT '+
                '  WHERE F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' AND QTA.CO_ANO_REF = ' + QrlAnoBase.Caption +
                ' AND F.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                ' AND F.CO_MAT = ' + IntToStr(co_disciplina)
      else
        Sql.Text := '  SELECT DISTINCT QTA.*, F.CO_ALU, C.QT_AULA_CUR, '+
                '  (SELECT COUNT(*) FROM TB132_FREQ_ALU F '+
                '   WHERE YEAR(F.DT_FRE) = ' + QrlAnoBase.Caption +
                '  	  and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
                '	  and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                '  	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                ' and F.CO_MAT is null ' +
                ' ) TFMES, '+
                '  (SELECT COUNT(*) FROM TB132_FREQ_ALU F '+
                '   WHERE YEAR(F.DT_FRE) = ' + QrlAnoBase.Caption +
                '   and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
                '   and F.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                '   and F.CO_TUR = ' + QryRelatorio.FieldByName('CO_TUR').AsString +
                '    AND F.CO_MODU_CUR = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                '  	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                ' and F.CO_MAT is null ' + IntToStr(co_disciplina) +
                '  ) TFTURMA '+
                '   FROM TB132_FREQ_ALU F '+
                '  JOIN TB01_CURSO C ON C.CO_CUR = F.CO_CUR '+
                '  JOIN TB_QTDE_AULAS QTA ON QTA.CO_CUR = C.CO_CUR AND QTA.CO_MAT = F.CO_MAT '+
                '  WHERE F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' AND QTA.CO_ANO_REF = ' + QrlAnoBase.Caption +
                ' AND F.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                ' AND F.CO_MAT is null ';
    Open;
  end;

  if not qryTF.IsEmpty then
  begin
    // TOTAL DE FALTAS POR M�S
    if QrlMes.Caption = '1' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_JAN.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_JAN.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_JAN.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '2' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_FEV.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_FEV.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_FEV.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '3' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_MAR.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_MAR.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_MAR.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '4' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_ABR.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_ABR.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_ABR.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '5' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_MAI.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_MAI.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_MAI.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '6' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_JUN.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_JUN.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_JUN.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '7' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_JUL.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_JUL.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_JUL.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '8' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_AGO.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_AGO.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_AGO.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '9' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_SET.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_SET.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_SET.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '10' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_OUT.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_OUT.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_OUT.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '11' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_NOV.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_NOV.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_NOV.AsString)),ffNumber,10,2);
    end;
    if QrlMes.Caption = '12' then
    begin
      QrlDiasUteis.Caption := QryTFQT_DIAS_LETIVO_DEZ.AsString;
      if QryTFTFMES.AsInteger > 0 then // TOTAL DE FALTAS ALUNO - PORCENTAGEM
        QrlTFPorcent.Caption := FloatToStrF((QryTFTFMES.AsInteger * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_DEZ.AsString)),ffNumber,10,1);
      if QryTFTFTURMA.AsInteger > 0 then // TOTAL DE FALTAS TURMA - PORCENTAGEM
        QrlTotFaltTurmaPorcent.Caption := FloatToStrF(((QryTFTFTURMA.AsInteger / StrToInt(QrlTotAluno.Caption)) * 100) / (StrToInt(QryTFQT_DIAS_LETIVO_DEZ.AsString)),ffNumber,10,2);
    end;
  end
  else
  begin
    QrlTotFaltTurmaPorcent.Caption := '-';
  end;

  if QryRelatorio.FieldByName('CO_PARAM_FREQ_TIPO').AsString = 'M' then
  // FREQU�NCIA DO ALUNO
    SqlString := ' SELECT DISTINCT F.CO_ALU, '+
               // CONSULTA PRESEN�A EM JANEIRO;
               ' ( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('01') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ' ) P01, ' +
               ' ( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('02') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P02, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('03') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P03, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('04') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P04, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('05') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P05, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('06') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P06, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('07') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P07, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('08') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P08, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('09') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P09, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('10') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P10, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('11') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P11, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('12') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P12, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('13') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P13, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('14') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P14, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('15') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P15, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('16') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P16, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('17') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P17, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('18') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P18, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('19') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P19, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('20') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P20, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('21') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P21, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('22') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P22, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('23') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
            //   '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P23, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('24') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P24, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('25') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P25, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('26') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
            //   '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P26, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('27') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P27, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('28') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P28, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('29') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P29, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('30') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
            //   '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P30, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('31') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina) +
               /////
           //    '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P31 '+
               'FROM TB132_FREQ_ALU F '+
               'WHERE F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT = ' + IntToStr(co_disciplina)
               /////
  else
    SqlString := ' SELECT DISTINCT F.CO_ALU, '+
               // CONSULTA PRESEN�A EM JANEIRO;
               ' ( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('01') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ' ) P01, ' +
               ' ( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('02') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P02, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('03') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P03, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('04') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P04, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('05') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P05, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('06') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P06, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('07') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P07, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('08') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P08, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('09') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P09, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('10') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P10, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('11') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P11, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('12') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P12, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('13') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P13, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('14') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P14, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('15') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P15, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('16') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P16, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('17') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P17, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('18') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P18, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('19') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P19, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('20') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P20, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('21') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P21, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('22') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P22, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('23') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
            //   '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P23, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('24') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P24, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('25') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P25, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('26') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
            //   '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P26, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('27') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P27, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('28') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P28, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('29') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
             //  '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P29, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('30') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
            //   '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P30, '+
               '( SELECT F.CO_FLAG_FREQ_ALUNO FROM TB132_FREQ_ALU F '+
               ' 	WHERE YEAR(F.DT_FRE) = ' + QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString +
               '	and MONTH(F.DT_FRE) = ' + QrlMes.Caption +
               '	and DAY(F.DT_FRE) = ' + QuotedStr('31') +
               '	and F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ' +
               /////
           //    '	and F.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('S') +
               ') P31 '+
               'FROM TB132_FREQ_ALU F '+
               'WHERE F.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' AND F.CO_MAT is null ';
               /////


  with QryFreqAluno do
  begin
    Close;
    Sql.Clear;
    Sql.Text := SqlString;
    Open;
  end;

  // LABELS DE JANEIRO
  if QryFreqAlunoP01.IsNull then QrlP01.Caption := '-';
  if QryFreqAlunoP01.AsString = 'S' then QrlP01.Caption := 'P';
  if QryFreqAlunoP01.AsString = 'N' then QrlP01.Caption := 'F';

  if QryFreqAlunoP02.IsNull then QrlP02.Caption := '-';
  if QryFreqAlunoP02.AsString = 'S' then QrlP02.Caption := 'P';
  if QryFreqAlunoP02.AsString = 'N' then QrlP02.Caption := 'F';

  if QryFreqAlunoP03.IsNull then QrlP03.Caption := '-';
  if QryFreqAlunoP03.AsString = 'S' then QrlP03.Caption := 'P';
  if QryFreqAlunoP03.AsString = 'N' then QrlP03.Caption := 'F';

  if QryFreqAlunoP04.IsNull then QrlP04.Caption := '-';
  if QryFreqAlunoP04.AsString = 'S' then QrlP04.Caption := 'P';
  if QryFreqAlunoP04.AsString = 'N' then QrlP04.Caption := 'F';

  if QryFreqAlunoP05.IsNull then QrlP05.Caption := '-';
  if QryFreqAlunoP05.AsString = 'S' then QrlP05.Caption := 'P';
  if QryFreqAlunoP05.AsString = 'N' then QrlP05.Caption := 'F';

  if QryFreqAlunoP06.IsNull then QrlP06.Caption := '-';
  if QryFreqAlunoP06.AsString = 'S' then QrlP06.Caption := 'P';
  if QryFreqAlunoP06.AsString = 'N' then QrlP06.Caption := 'F';

  if QryFreqAlunoP07.IsNull then QrlP07.Caption := '-';
  if QryFreqAlunoP07.AsString = 'S' then QrlP07.Caption := 'P';
  if QryFreqAlunoP07.AsString = 'N' then QrlP07.Caption := 'F';

  if QryFreqAlunoP08.IsNull then QrlP08.Caption := '-';
  if QryFreqAlunoP08.AsString = 'S' then QrlP08.Caption := 'P';
  if QryFreqAlunoP08.AsString = 'N' then QrlP08.Caption := 'F';

  if QryFreqAlunoP09.IsNull then QrlP09.Caption := '-';
  if QryFreqAlunoP09.AsString = 'S' then QrlP09.Caption := 'P';
  if QryFreqAlunoP09.AsString = 'N' then QrlP09.Caption := 'F';

  if QryFreqAlunoP10.IsNull then QrlP10.Caption := '-';
  if QryFreqAlunoP10.AsString = 'S' then QrlP10.Caption := 'P';
  if QryFreqAlunoP10.AsString = 'N' then QrlP10.Caption := 'F';

  if QryFreqAlunoP11.IsNull then QrlP11.Caption := '-';
  if QryFreqAlunoP11.AsString = 'S' then QrlP11.Caption := 'P';
  if QryFreqAlunoP11.AsString = 'N' then QrlP11.Caption := 'F';

  if QryFreqAlunoP12.IsNull then QrlP12.Caption := '-';
  if QryFreqAlunoP12.AsString = 'S' then QrlP12.Caption := 'P';
  if QryFreqAlunoP12.AsString = 'N' then QrlP12.Caption := 'F';

  if QryFreqAlunoP13.IsNull then QrlP13.Caption := '-';
  if QryFreqAlunoP13.AsString = 'S' then QrlP13.Caption := 'P';
  if QryFreqAlunoP13.AsString = 'N' then QrlP13.Caption := 'F';

  if QryFreqAlunoP14.IsNull then QrlP14.Caption := '-';
  if QryFreqAlunoP14.AsString = 'S' then QrlP14.Caption := 'P';
  if QryFreqAlunoP15.AsString = 'N' then QrlP14.Caption := 'F';

  if QryFreqAlunoP15.IsNull then QrlP15.Caption := '-';
  if QryFreqAlunoP15.AsString = 'S' then QrlP15.Caption := 'P';
  if QryFreqAlunoP15.AsString = 'N' then QrlP15.Caption := 'F';

  if QryFreqAlunoP16.IsNull then QrlP16.Caption := '-';
  if QryFreqAlunoP16.AsString = 'S' then QrlP16.Caption := 'P';
  if QryFreqAlunoP16.AsString = 'N' then QrlP16.Caption := 'F';

  if QryFreqAlunoP17.IsNull then QrlP17.Caption := '-';
  if QryFreqAlunoP17.AsString = 'S' then QrlP17.Caption := 'P';
  if QryFreqAlunoP17.AsString = 'N' then QrlP17.Caption := 'F';

  if QryFreqAlunoP18.IsNull then QrlP18.Caption := '-';
  if QryFreqAlunoP18.AsString = 'S' then QrlP18.Caption := 'P';
  if QryFreqAlunoP18.AsString = 'N' then QrlP18.Caption := 'F';

  if QryFreqAlunoP19.IsNull then QrlP19.Caption := '-';
  if QryFreqAlunoP19.AsString = 'S' then QrlP19.Caption := 'P';
  if QryFreqAlunoP19.AsString = 'N' then QrlP19.Caption := 'F';

  if QryFreqAlunoP20.IsNull then QrlP20.Caption := '-';
  if QryFreqAlunoP20.AsString = 'S' then QrlP20.Caption := 'P';
  if QryFreqAlunoP20.AsString = 'N' then QrlP20.Caption := 'F';

  if QryFreqAlunoP21.IsNull then QrlP21.Caption := '-';
  if QryFreqAlunoP21.AsString = 'S' then QrlP21.Caption := 'P';
  if QryFreqAlunoP21.AsString = 'N' then QrlP21.Caption := 'F';

  if QryFreqAlunoP22.IsNull then QrlP22.Caption := '-';
  if QryFreqAlunoP22.AsString = 'S' then QrlP22.Caption := 'P';
  if QryFreqAlunoP22.AsString = 'N' then QrlP22.Caption := 'F';

  if QryFreqAlunoP23.IsNull then QrlP23.Caption := '-';
  if QryFreqAlunoP23.AsString = 'S' then QrlP23.Caption := 'P';
  if QryFreqAlunoP23.AsString = 'N' then QrlP23.Caption := 'F';

  if QryFreqAlunoP24.IsNull then QrlP24.Caption := '-';
  if QryFreqAlunoP24.AsString = 'S' then QrlP24.Caption := 'P';
  if QryFreqAlunoP24.AsString = 'N' then QrlP24.Caption := 'F';

  if QryFreqAlunoP25.IsNull then QrlP25.Caption := '-';
  if QryFreqAlunoP25.AsString = 'S' then QrlP25.Caption := 'P';
  if QryFreqAlunoP25.AsString = 'N' then QrlP25.Caption := 'F';

  if QryFreqAlunoP26.IsNull then QrlP26.Caption := '-';
  if QryFreqAlunoP26.AsString = 'S' then QrlP26.Caption := 'P';
  if QryFreqAlunoP26.AsString = 'N' then QrlP26.Caption := 'F';

  if QryFreqAlunoP27.IsNull then QrlP27.Caption := '-';
  if QryFreqAlunoP27.AsString = 'S' then QrlP27.Caption := 'P';
  if QryFreqAlunoP27.AsString = 'N' then QrlP27.Caption := 'F';

  if QryFreqAlunoP28.IsNull then QrlP28.Caption := '-';
  if QryFreqAlunoP28.AsString = 'S' then QrlP28.Caption := 'P';
  if QryFreqAlunoP28.AsString = 'N' then QrlP28.Caption := 'F';

  if QryFreqAlunoP29.IsNull then QrlP29.Caption := '-';
  if QryFreqAlunoP29.AsString = 'S' then QrlP29.Caption := 'P';
  if QryFreqAlunoP29.AsString = 'N' then QrlP29.Caption := 'F';

  if QryFreqAlunoP30.IsNull then QrlP30.Caption := '-';
  if QryFreqAlunoP30.AsString = 'S' then QrlP30.Caption := 'P';
  if QryFreqAlunoP30.AsString = 'N' then QrlP30.Caption := 'F';

  if QryFreqAlunoP31.IsNull then QrlP31.Caption := '-';
  if QryFreqAlunoP31.AsString = 'S' then QrlP31.Caption := 'P';
  if QryFreqAlunoP31.AsString = 'N' then QrlP31.Caption := 'F';

//  if QryFreqAlunoP31.AsString <> '0' then QrlP31.Caption := 'P' else QrlP31.Caption := '-';

end;

procedure TFrmRelFreqAluno.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select c.no_col as professor_resp  from tb_respon_materia rm ' +
                ' left join tb03_colabor c on rm.CO_COL_RESP = c.co_col and c.co_emp = rm.co_emp ' +
                'where rm.co_emp = ' + codigoEmpresa +
                ' and rm.co_cur = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                ' and rm.co_modu_cur = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                ' and rm.co_tur = ' + QryRelatorio.FieldByName('CO_TUR').AsString +
                ' and rm.CO_CLASS_RESP = ' + QuotedStr('P');
    Open;
    if not IsEmpty then
    begin
      if not FieldByName('professor_resp').IsNull then
        QrlEduc01.Caption := FieldByName('professor_resp').AsString;
    end;

    Close;
    SQL.Clear;
    SQL.Text := 'select c.no_col as professor_resp  from tb_respon_materia rm ' +
                ' left join tb03_colabor c on rm.CO_COL_RESP = c.co_col and c.co_emp = rm.co_emp ' +
                'where rm.co_emp = ' + codigoEmpresa +
                ' and rm.co_cur = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                ' and rm.co_modu_cur = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                ' and rm.co_tur = ' + QryRelatorio.FieldByName('CO_TUR').AsString +
                ' and rm.CO_CLASS_RESP = ' + QuotedStr('A');
    Open;
    if not IsEmpty then
    begin
      if not FieldByName('professor_resp').IsNull then
        QrlEduc01.Caption := FieldByName('professor_resp').AsString;
    end;
  end;

  if QryRelatorio.FieldByName('CO_PERI_TUR').AsString = 'M' then
    QrlTurno.Caption := 'Manh�';
  if QryRelatorio.FieldByName('CO_PERI_TUR').AsString = 'V' then
    QrlTurno.Caption := 'Tarde';
  if QryRelatorio.FieldByName('CO_PERI_TUR').AsString = 'N' then
    QrlTurno.Caption := 'Noite';

  if QryRelatorio.FieldByName('CO_PARAM_FREQ_TIPO').AsString = 'D' then
    QrlTPunidade.Caption := '(Tipo de Unidade: ' + QryRelatorio.FieldByName('NO_TIPOEMP').AsString + ')'
  else
  begin
    QrlTPunidade.Caption := '(Modalidade: ' + QryRelatorio.FieldByName('DE_MODU_CUR').AsString + ' - Disciplina: ' + QryRelatorio.FieldByName('NO_RED_MATERIA').AsString + ')';
  end;

end;

procedure TFrmRelFreqAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QrlTotAluno.Caption := '0';
  QrlNumAluno.Caption := '0';
  totFeminino := 0;
  totMasculino := 0;
end;

procedure TFrmRelFreqAluno.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QrlQTsexo.Caption := '(Masculino: ' + IntToStr(totMasculino) + '   -   Feminino: ' + IntToStr(totFeminino) + ')';
end;

end.
