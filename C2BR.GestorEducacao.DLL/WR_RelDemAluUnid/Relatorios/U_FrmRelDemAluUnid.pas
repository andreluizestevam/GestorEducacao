unit U_FrmRelDemAluUnid;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelDemAluUnid = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    SummaryBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QrlTotalAluno: TQRLabel;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRLMat12: TQRLabel;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRLMat11: TQRLabel;
    QRLMat10: TQRLabel;
    QRLMat9: TQRLabel;
    QRLMat5: TQRLabel;
    QRLMat6: TQRLabel;
    QRLMat7: TQRLabel;
    QRLMat8: TQRLabel;
    QRLMat1: TQRLabel;
    QRLMat2: TQRLabel;
    QRLMat3: TQRLabel;
    QRLMat4: TQRLabel;
    QRLabel16: TQRLabel;
    QRShape16: TQRShape;
    QrlTPunidade: TQRLabel;
    QRLabel18: TQRLabel;
    QRShape17: TQRShape;
    QrlSigla: TQRLabel;
    QRLabel20: TQRLabel;
    QrlNuINEP: TQRLabel;
    QRShape18: TQRShape;
    QRLabel22: TQRLabel;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRShape27: TQRShape;
    QRShape28: TQRShape;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape33: TQRShape;
    QRShape34: TQRShape;
    QryRelatorioNO_FANTAS_EMP: TStringField;
    QryRelatorioNU_INEP: TIntegerField;
    QryRelatorioNO_TIPOEMP: TStringField;
    QRDBText1: TQRDBText;
    Qrl1CINI: TQRLabel;
    QryAlunoCurso: TADOQuery;
    QryRelatorioCO_EMP: TAutoIncField;
    QrlINFAN: TQRLabel;
    Qrl1CINT: TQRLabel;
    Qrl1CFIN: TQRLabel;
    Qrl2CFIN: TQRLabel;
    Qrl3CINI: TQRLabel;
    Qrl3CFIN: TQRLabel;
    Qrl2CINI: TQRLabel;
    QrlEJA: TQRLabel;
    QrlMULTI: TQRLabel;
    Qrl4CFIN: TQRLabel;
    Qrl4CINI: TQRLabel;
    QryAlunoCursoCO_EMP: TAutoIncField;
    QryAlunoCursoALUINFAN: TIntegerField;
    QryAlunoCursoALU1CINI: TIntegerField;
    QryAlunoCursoALU1CINT: TIntegerField;
    QryAlunoCursoALU1CFIN: TIntegerField;
    QryAlunoCursoALU2CINI: TIntegerField;
    QryAlunoCursoALU2CFIN: TIntegerField;
    QryAlunoCursoALU3CINI: TIntegerField;
    QryAlunoCursoALU3CFIN: TIntegerField;
    QryAlunoCursoALU4CINI: TIntegerField;
    QryAlunoCursoALU4CFIN: TIntegerField;
    QryAlunoCursoMULTI: TIntegerField;
    QryAlunoCursoALUEJA: TIntegerField;
    QryAlunoCursoALUTOTAL: TIntegerField;
    QrlTotInfan: TQRLabel;
    QrlTot1CINI: TQRLabel;
    QrlTot1CINT: TQRLabel;
    QrlTot1CFIN: TQRLabel;
    QrlTot2CINI: TQRLabel;
    QrlTot2CFIN: TQRLabel;
    QrlTot3CINI: TQRLabel;
    QrlTot3CFIN: TQRLabel;
    QrlTot4CINI: TQRLabel;
    QrlTot4CFIN: TQRLabel;
    QrlTotMULTI: TQRLabel;
    QrlTotEJA: TQRLabel;
    QrlTotTOTAL: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel3: TQRLabel;
    QryRelatorioSIGLA: TWideStringField;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    TotINFAN, Tot1CINI, Tot1CINT, Tot1CFIN, Tot2CINI, Tot2CFIN, Tot3CINI, Tot3CFIN, Tot4CINI, Tot4CFIN, TotMULTI, TotEJA, TotTOTAL : integer;
  public
    { Public declarations }
    anoReferencia, situMatricula : String;
  end;

var
  FrmRelDemAluUnid: TFrmRelDemAluUnid;

implementation

{$R *.dfm}
uses U_DataModuleSGE;

procedure TFrmRelDemAluUnid.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  TotINFAN := 0;
  Tot1CINI := 0;
  Tot1CINT := 0;
  Tot1CFIN := 0;
  Tot2CINI := 0;
  Tot2CFIN := 0;
  Tot3CINI := 0;
  Tot3CFIN := 0;
  Tot4CINI := 0;
  Tot4CFIN := 0;
  TotMULTI := 0;
  TotEJA := 0;
  TotTOTAL := 0;
end;

procedure TFrmRelDemAluUnid.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  SQLString : String;
begin
  inherited;
// ZEBRADO
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00EBEBEB
  else
    DetailBand1.Color := clWhite;

  // NÚMERO INEP
  QrlNuINEP.Caption := QryRelatorioNU_INEP.AsString;

  // TIPO UNIDADE
  QrlTPunidade.Caption := QryRelatorioNO_TIPOEMP.AsString;

  // SIGLA
  if not QryRelatorioSIGLA.IsNull then
    QrlSigla.Caption := QryRelatorioSIGLA.AsString
  else
    QrlSigla.Caption := ' - ';

  // CONSULTA DOS TOTAIS - EMPRESA
  with QryAlunoCurso do
  begin
    Close;
    Sql.Clear;
      //if Sys_TipoEnsino = 'ES' then
      //begin
        SQLString := ' SELECT DISTINCT E.CO_EMP, '+
                  ' (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '  JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '  JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	 JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP ' + 
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat1.Caption) +

                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALUINFAN, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  ' 	AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat2.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  ' 	AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU1CINI, '+
                  ' (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat3.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU1CINT, '+
                  ' (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat4.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU1CFIN, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat5.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU2CINI, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat6.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU2CFIN, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat7.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU3CINI, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat8.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU3CFIN, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat9.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU4CINI, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat10.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALU4CFIN, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat11.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') MULTI, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat12.Caption) +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALUEJA, '+
                  '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                  '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                  '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                  '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                  '	WHERE A.CO_EMP = MM.CO_EMP' +
                  ' AND M.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT' +
                  '		AND M.CO_CUR = C.CO_CUR ';

                  if situMatricula <> 'T' then
                    SQLString := SQLString + '   AND M.CO_SIT_MAT = MM.CO_SIT_MAT ';

                  SQLString := SQLString + ') ALUTOTAL, '+
                  ' (SELECT DISTINCT COUNT(COL.CO_COL) FROM TB03_COLABOR COL '+
                  ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP '+
                  ' 	WHERE COL.CO_EMP = MM.CO_EMP' +
                  '		AND COL.FLA_PROFESSOR = ' + QuotedStr('S') +
                  ' ) TOTALPROF, '+
                  ' (SELECT DISTINCT COUNT(COL.CO_COL) FROM TB03_COLABOR COL '+
                  ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP '+
                  ' 	WHERE COL.CO_EMP = MM.CO_EMP' +
                  '		AND COL.FLA_PROFESSOR = ' + QuotedStr('N') +
                  ' ) TOTALFUN '+
                  'FROM TB25_EMPRESA E '+
                  ' JOIN TB08_MATRCUR MM ON MM.CO_EMP = E.CO_EMP '+
                  'WHERE E.CO_EMP = ' + QryRelatorioCO_EMP.AsString +
                  ' AND MM.CO_ANO_MES_MAT = ' + anoReferencia;
      //end;
  //  Sql.SaveToFile('D:\FrmRelDemAluUnid.sql');

    if situMatricula <> 'T' then
      SQLString := SQLString + ' and MM.CO_SIT_MAT = ' + QuotedStr(situMatricula);

    SQL.Text := SQLString;
    Open;
  end;

  // LEGENDAS DOS TOTAIS
  if QryAlunoCursoALUINFAN.AsString <> '0' then
  begin
    QrlINFAN.Caption := QryAlunoCursoALUINFAN.AsString
  end
  else
  begin
    QrlINFAN.Alignment := taCenter;
    QrlINFAN.Caption := ' - ';
  end;

  if QryAlunoCursoALU1CINI.AsString <> '0' then
  begin
    Qrl1CINI.Caption := QryAlunoCursoALU1CINI.AsString
  end
  else
  begin
    Qrl1CINI.Alignment := taCenter;
    Qrl1CINI.Caption := ' - ';
  end;

  if QryAlunoCursoALU1CINT.AsString <> '0' then
  begin
    Qrl1CINT.Caption := QryAlunoCursoALU1CINT.AsString
  end
  else
  begin
    Qrl1CINT.Alignment := taCenter;
    Qrl1CINT.Caption := ' - ';
  end;

  if QryAlunoCursoALU1CFIN.AsString <> '0' then
  begin
    Qrl1CFIN.Caption := QryAlunoCursoALU1CFIN.AsString
  end
  else
  begin
    Qrl1CFIN.Alignment := taCenter;
    Qrl1CFIN.Caption := ' - ';
  end;

  if QryAlunoCursoALU2CINI.AsString <> '0' then
  begin
    Qrl2CINI.Caption := QryAlunoCursoALU2CINI.AsString
  end
  else
  begin
    Qrl2CINI.Alignment := taCenter;
    Qrl2CINI.Caption := ' - ';
  end;

  if QryAlunoCursoALU2CFIN.AsString <> '0' then
  begin
    Qrl2CFIN.Caption := QryAlunoCursoALU2CFIN.AsString
  end
  else
  begin
    Qrl2CFIN.Alignment := taCenter;
    Qrl2CFIN.Caption := ' - ';
  end;

  if QryAlunoCursoALU3CINI.AsString <> '0' then
  begin
    Qrl3CINI.Caption := QryAlunoCursoALU3CINI.AsString
  end
  else
  begin
    Qrl3CINI.Alignment := taCenter;
    Qrl3CINI.Caption := ' - ';
  end;

  if QryAlunoCursoALU3CFIN.AsString <> '0' then
  begin
    Qrl3CFIN.Caption := QryAlunoCursoALU3CFIN.AsString
  end
  else
  begin
    Qrl3CFIN.Alignment := taCenter;
    Qrl3CFIN.Caption := ' - ';
  end;

  if QryAlunoCursoALU4CINI.AsString <> '0' then
  begin
    Qrl4CINI.Caption := QryAlunoCursoALU4CINI.AsString
  end
  else
  begin
    Qrl4CINI.Alignment := taCenter;
    Qrl4CINI.Caption := ' - ';
  end;

  if QryAlunoCursoALU4CFIN.AsString <> '0' then
  begin
    Qrl4CFIN.Caption := QryAlunoCursoALU4CFIN.AsString
  end
  else
  begin
    Qrl4CFIN.Alignment := taCenter;
    Qrl4CFIN.Caption := ' - ';
  end;

  if QryAlunoCursoMULTI.AsString <> '0' then
  begin
    QrlMULTI.Caption := QryAlunoCursoMULTI.AsString
  end
  else
  begin
    QrlMULTI.Alignment := taCenter;
    QrlMULTI.Caption := ' - ';
  end;

  if QryAlunoCursoALUEJA.AsString <> '0' then
  begin
    QrlEJA.Caption := QryAlunoCursoALUEJA.AsString
  end
  else
  begin
    QrlEJA.Alignment := taCenter;
    QrlEJA.Caption := ' - ';
  end;

  if QryAlunoCursoALUTOTAL.AsString <> '0' then
  begin
    QrlTotalAluno.Caption := QryAlunoCursoALUTOTAL.AsString
  end
  else
  begin
    QrlTotalAluno.Alignment := taCenter;
    QrlTotalAluno.Caption := ' - ';
  end;

  // Variáveis de Totais soman valores para a linha de totais
  TotINFAN := TotINFAN + QryAlunoCursoALUINFAN.AsInteger;
  Tot1CINI := Tot1CINI + QryAlunoCursoALU1CINI.AsInteger;
  Tot1CINT := Tot1CINT + QryAlunoCursoALU1CINT.AsInteger;
  Tot1CFIN := Tot1CFIN + QryAlunoCursoALU1CFIN.AsInteger;
  Tot2CINI := Tot2CINI + QryAlunoCursoALU2CINI.AsInteger;
  Tot2CFIN := Tot2CFIN + QryAlunoCursoALU2CFIN.AsInteger;
  Tot3CINI := Tot3CINI + QryAlunoCursoALU3CINI.AsInteger;
  Tot3CFIN := Tot3CFIN + QryAlunoCursoALU3CFIN.AsInteger;
  Tot4CINI := Tot4CINI + QryAlunoCursoALU4CINI.AsInteger;
  Tot4CFIN := Tot4CFIN + QryAlunoCursoALU4CFIN.AsInteger;
  TotMULTI := TotMULTI + QryAlunoCursoMULTI.AsInteger;
  TotEJA := TotEJA + QryAlunoCursoALUEJA.AsInteger;
  TotTOTAL := TotTOTAL + QryAlunoCursoALUTOTAL.AsInteger;

end;

procedure TFrmRelDemAluUnid.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if IntToStr(TotINFAN) <> '0' then
  begin
    QrlTotInfan.Caption := IntToStr(TotINFAN);
  end
  else
  begin
    QrlTotInfan.Alignment := taCenter;
    QrlTotInfan.Caption := ' - ';
  end;

  if IntToStr(Tot1CINI) <> '0' then
  begin
    QrlTot1CINI.Caption := IntToStr(Tot1CINI);
  end
  else
  begin
    QrlTot1CINI.Alignment := taCenter;
    QrlTot1CINI.Caption := ' - ';
  end;

  if IntToStr(Tot1CINT) <> '0' then
  begin
    QrlTot1CINT.Caption := IntToStr(Tot1CINI);
  end
  else
  begin
    QrlTot1CINT.Alignment := taCenter;
    QrlTot1CINT.Caption := ' - ';
  end;

  if IntToStr(Tot1CFIN) <> '0' then
  begin
    QrlTot1CFIN.Caption := IntToStr(Tot1CFIN);
  end
  else
  begin
    QrlTot1CFIN.Alignment := taCenter;
    QrlTot1CFIN.Caption := ' - ';
  end;

  if IntToStr(Tot2CINI) <> '0' then
  begin
    QrlTot2CINI.Caption := IntToStr(Tot2CINI);
  end
  else
  begin
    QrlTot2CINI.Alignment := taCenter;
    QrlTot2CINI.Caption := ' - ';
  end;

  if IntToStr(Tot2CFIN) <> '0' then
  begin
    QrlTot2CFIN.Caption := IntToStr(Tot2CFIN);
  end
  else
  begin
    QrlTot2CFIN.Alignment := taCenter;
    QrlTot2CFIN.Caption := ' - ';
  end;

  if IntToStr(Tot3CINI) <> '0' then
  begin
    QrlTot3CINI.Caption := IntToStr(Tot3CINI);
  end
  else
  begin
    QrlTot3CINI.Alignment := taCenter;
    QrlTot3CINI.Caption := ' - ';
  end;

  if IntToStr(Tot3CFIN) <> '0' then
  begin
    QrlTot3CFIN.Caption := IntToStr(Tot3CFIN);
  end
  else
  begin
    QrlTot3CFIN.Alignment := taCenter;
    QrlTot3CFIN.Caption := ' - ';
  end;

  if IntToStr(Tot4CINI) <> '0' then
  begin
    QrlTot4CINI.Caption := IntToStr(Tot4CINI);
  end
  else
  begin
    QrlTot4CINI.Alignment := taCenter;
    QrlTot4CINI.Caption := ' - ';
  end;

  if IntToStr(Tot4CFIN) <> '0' then
  begin
    QrlTot4CFIN.Caption := IntToStr(Tot4CFIN);
  end
  else
  begin
    QrlTot4CFIN.Alignment := taCenter;
    QrlTot4CFIN.Caption := ' - ';
  end;

  if IntToStr(TotMULTI) <> '0' then
  begin
    QrlTotMULTI.Caption := IntToStr(TotMULTI);
  end
  else
  begin
    QrlTotMULTI.Alignment := taCenter;
    QrlTotMULTI.Caption := ' - ';
  end;

  if IntToStr(TotEJA) <> '0' then
  begin
    QrlTotEJA.Caption := IntToStr(TotEJA);
  end
  else
  begin
    QrlTotEJA.Alignment := taCenter;
    QrlTotEJA.Caption := ' - ';
  end;

  if IntToStr(TotTOTAL) <> '0' then
  begin
    QrlTotTOTAL.Caption := IntToStr(TotTOTAL);
  end
  else
  begin
    QrlTotTOTAL.Alignment := taCenter;
    QrlTotTOTAL.Caption := ' - ';
  end;

end;

procedure TFrmRelDemAluUnid.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  i : Integer;
begin
  inherited;
  QRLMat1.Caption := '';
  QRLMat2.Caption := '';
  QRLMat3.Caption := '';
  QRLMat4.Caption := '';
  QRLMat5.Caption := '';
  QRLMat6.Caption := '';
  QRLMat7.Caption := '';
  QRLMat8.Caption := '';
  QRLMat9.Caption := '';
  QRLMat10.Caption := '';
  QRLMat11.Caption := '';
  QRLMat12.Caption := '';
  i := 1;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select CO_SIGL_CUR,SEQ_IMPRESSAO from tb01_curso ' +
                'where co_emp = 187' + //QryRelatorioCO_EMP.AsString +
                ' order by SEQ_IMPRESSAO';
    Open;
    First;

    if not IsEmpty then
    begin
      while not Eof do
      begin

        if not FieldByName('SEQ_IMPRESSAO').IsNull then
        begin
          case i of
            1: QRLMat1.Caption := FieldByName('CO_SIGL_CUR').AsString;
            2: QRLMat2.Caption := FieldByName('CO_SIGL_CUR').AsString;
            3: QRLMat3.Caption := FieldByName('CO_SIGL_CUR').AsString;
            4: QRLMat4.Caption := FieldByName('CO_SIGL_CUR').AsString;
            5: QRLMat5.Caption := FieldByName('CO_SIGL_CUR').AsString;
            6: QRLMat6.Caption := FieldByName('CO_SIGL_CUR').AsString;
            7: QRLMat7.Caption := FieldByName('CO_SIGL_CUR').AsString;
            8: QRLMat8.Caption := FieldByName('CO_SIGL_CUR').AsString;
            9: QRLMat9.Caption := FieldByName('CO_SIGL_CUR').AsString;
            10: QRLMat10.Caption := FieldByName('CO_SIGL_CUR').AsString;
            11: QRLMat11.Caption := FieldByName('CO_SIGL_CUR').AsString;
            12: QRLMat12.Caption := FieldByName('CO_SIGL_CUR').AsString;
          end;
        end;
        i := i + 1;
        Next;
      end;
    end;
  end;
end;

end.
