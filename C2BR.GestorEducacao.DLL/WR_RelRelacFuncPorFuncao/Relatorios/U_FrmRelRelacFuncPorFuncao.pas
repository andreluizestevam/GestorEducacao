unit U_FrmRelRelacFuncPorFuncao;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacFuncPorFuncao = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRDBText7: TQRDBText;
    QRGroup1: TQRGroup;
    QRLabel22: TQRLabel;
    QRShape1: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel3: TQRLabel;
    QRDBBairro: TQRDBText;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QRLIdade: TQRLabel;
    QRLabel1: TQRLabel;
    QRLDeficiencia: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel9: TQRLabel;
    QRLEndereco: TQRLabel;
    QRLabel10: TQRLabel;
    QRLMatricula: TQRLabel;
    QRLSituacao: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    SummaryBand1: TQRBand;
    QRLTotMas: TQRLabel;
    QRLTotFem: TQRLabel;
    QRLTotGeral: TQRLabel;
    QrlSiglaUnid: TQRLabel;
    QrlTelMovel: TQRLabel;
    QRLTotDef: TQRLabel;
    QRLTotalCompleto: TQRLabel;
    QRLNoCol: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacFuncPorFuncao: TFrmRelRelacFuncPorFuncao;

implementation

Uses U_DataModuleSGE,U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacFuncPorFuncao.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  if not QryRelatorio.FieldByName('CO_MAT_COL').IsNull then
    QRLMatricula.Caption := FormatMaskText('00.000-0;0',QryRelatorio.FieldByName('CO_MAT_COL').AsString)
  else
    QRLMatricula.Caption := '-';

  if not QryRelatorio.FieldByName('NO_COL').IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorio.FieldByName('NO_COL').AsString)
  else
    QRLNoCol.Caption := '-';

  if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'ATI' then
    QRLSituacao.Caption := 'Atividade Interna'
  else if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'ATE' then
    QRLSituacao.Caption := 'Atividade Externa'
  else if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'FCE' then
    QRLSituacao.Caption := 'Cedido'
  else if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'FES' then
    QRLSituacao.Caption := 'Estagiário'
  else if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'LFR' then
    QRLSituacao.Caption := 'Licença Funcional'
  else if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'LME' then
    QRLSituacao.Caption := 'Licença Médica'
  else if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'SUS' then
    QRLSituacao.Caption := 'Suspenso'
  else if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'TRE' then
    QRLSituacao.Caption := 'Treinamento'
  else if QryRelatorio.FieldByName('CO_SITU_COL').AsString = 'FER' then
    QRLSituacao.Caption := 'Férias'
  else
    QRLSituacao.Caption := 'Licença Maternidade';

  if (not QryRelatorio.FieldByName('sigla').IsNull) then
    QrlSiglaUnid.Caption := QryRelatorio.FieldByName('sigla').AsString
  else
    QrlSiglaUnid.Caption := '-';

  if (not QryRelatorio.FieldByName('NU_TELE_CELU_COL').IsNull) and (QryRelatorio.FieldByName('NU_TELE_CELU_COL').AsString <> '') then
  begin
    QrlTelMovel.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_CELU_COL').AsString);
  end
  else
  begin
    QrlTelMovel.Caption := '-';
  end;

  QRLEndereco.Caption := '';
  QRLDeficiencia.Caption := '-';

  if QryRelatorio.FieldByName('TP_DEF').AsString = 'N' then
    QRLDeficiencia.Caption := 'Nenhuma';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'A' then
    QRLDeficiencia.Caption := 'Auditiva';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'V' then
    QRLDeficiencia.Caption := 'Visual';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'F' then
    QRLDeficiencia.Caption := 'Física';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'M' then
    QRLDeficiencia.Caption := 'Mental';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'I' then
    QRLDeficiencia.Caption := 'Múltiplas';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'O' then
    QRLDeficiencia.Caption := 'Outras';

  if not QryRelatorio.FieldByName('DT_NASC_COL').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_COL').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';

  if not QryRelatorio.FieldByName('DE_ENDE_COL').IsNull and (QryRelatorio.FieldByName('DE_ENDE_COL').AsString <> '')  then
    QRLEndereco.Caption := QryRelatorio.FieldByName('DE_ENDE_COL').AsString;

  if not QryRelatorio.FieldByName('NU_ENDE_COL').IsNull and (QryRelatorio.FieldByName('NU_ENDE_COL').AsString <> '') then
    QRLEndereco.Caption := QRLEndereco.Caption + ', Nº ' + QryRelatorio.FieldByName('NU_ENDE_COL').AsString;

  if not QryRelatorio.FieldByName('DE_COMP_ENDE_COL').IsNull and (QryRelatorio.FieldByName('DE_COMP_ENDE_COL').AsString <> '') then
    QRLEndereco.Caption := QRLEndereco.Caption + ',' + QryRelatorio.FieldByName('DE_COMP_ENDE_COL').AsString;

  if QryRelatorio.FieldByName('co_sexo_col').AsString = 'M' then
    QRLTotMas.Caption := IntToStr(StrToInt(QRLTotMas.Caption) + 1)
  else
  if QryRelatorio.FieldByName('co_sexo_col').AsString = 'F' then
    QRLTotFem.Caption := IntToStr(StrToInt(QRLTotFem.Caption) + 1);

  if QryRelatorio.FieldByName('TP_DEF').AsString <> 'N' then
    QRLTotDef.Caption := IntToStr(StrToInt(QRLTotDef.Caption) +1);

  QRLTotGeral.Caption := IntToStr(StrToInt(QRLTotFem.Caption) + StrToInt(QRLTotMas.Caption));

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelRelacFuncPorFuncao.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
  QRLTotDef.Caption := '0';
end;

procedure TFrmRelRelacFuncPorFuncao.SummaryBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
  QRLTotDef.Caption := '0';
end;

procedure TFrmRelRelacFuncPorFuncao.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotalCompleto.Caption := 'Total de Funcionários: ' + FormatFloat('0000', StrToFloat(QRLTotGeral.Caption)) + ' (Masculino: ' + FormatFloat('0000', StrToFloat(QRLTotMas.Caption)) + ' - Feminino: ' + FormatFloat('0000', StrToFloat(QRLTotFem.Caption)) + ' - Deficientes: ' + FormatFloat('0000', StrToFloat(QRLTotDef.Caption)) + ')';
end;

end.
