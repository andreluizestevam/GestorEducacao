unit U_FrmRelFicEstagio;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelFicEstagio = class(TFrmRelTemplate)
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRShape1: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRShape2: TQRShape;
    QRLabel5: TQRLabel;
    QRDBText2: TQRDBText;
    QRShape3: TQRShape;
    QRLabel9: TQRLabel;
    QRShape5: TQRShape;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRDBText9: TQRDBText;
    QRShape6: TQRShape;
    QRLabel20: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel26: TQRLabel;
    QRLEndereco: TQRLabel;
    QRLCidUF: TQRLabel;
    QRLabel11: TQRLabel;
    QRDBText21: TQRDBText;
    QRLHrIni: TQRLabel;
    QRLHrFinal: TQRLabel;
    QRLRemun: TQRLabel;
    QRLabel2: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel3: TQRLabel;
    QRLEfet: TQRLabel;
    QRLabel6: TQRLabel;
    QRDBText4: TQRDBText;
    QRDBText6: TQRDBText;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLDiasSemana: TQRLabel;
    QRShape4: TQRShape;
    QRLabel13: TQRLabel;
    QRDBText7: TQRDBText;
    QRLabel14: TQRLabel;
    QRDBText8: TQRDBText;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLTelContato: TQRLabel;
    DetailBand1: TQRBand;
    QRLabel12: TQRLabel;
    QRDBText10: TQRDBText;
    QRLabel17: TQRLabel;
    QRDBText11: TQRDBText;
    QRLabel21: TQRLabel;
    QRLPubAlvo: TQRLabel;
    QRLabel27: TQRLabel;
    QRLIdades: TQRLabel;
    QRLabel29: TQRLabel;
    QRDBText12: TQRDBText;
    QRLabel30: TQRLabel;
    QRDBText13: TQRDBText;
    QRLabel31: TQRLabel;
    QRDBText16: TQRDBText;
    QRLabel32: TQRLabel;
    QRLDtIni: TQRLabel;
    QRLabel34: TQRLabel;
    QRLDtFim: TQRLabel;
    QRLCEPEmp: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelFicEstagio: TFrmRelFicEstagio;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelFicEstagio.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLCEPEmp.Caption := FormatMaskText('00000-000;0',QryRelatorio.FieldByName('CO_CEP_EMP_OFERT_ESTAG').AsString);

  if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'A' then
    QRLPubAlvo.Caption := 'Alunos'
  else if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'F' then
    QRLPubAlvo.Caption := 'Funcionário'
  else if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'P' then
    QRLPubAlvo.Caption := 'Professor'
  else
    QRLPubAlvo.Caption := 'Outros';

  if not QryRelatorio.FieldByName('VL_IDADE_MIN').IsNull then
  begin
    QRLIdades.Caption := QryRelatorio.FieldByName('VL_IDADE_MIN').AsString;
    if not QryRelatorio.FieldByName('VL_IDADE_MAX').IsNull then
      QRLIdades.Caption := QRLIdades.Caption + ' à ' + QryRelatorio.FieldByName('VL_IDADE_MAX').AsString;
  end
  else
    QRLIdades.Caption := '-';

  if not QryRelatorio.FieldByName('DT_INI_OFERT_ESTAG').IsNull then
    QRLDtIni.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('DT_INI_OFERT_ESTAG').AsDateTime)
  else
    QRLDtIni.Caption := '-';

  if not QryRelatorio.FieldByName('DT_FIM_OFERT_ESTAG').IsNull then
    QRLDtFim.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('DT_FIM_OFERT_ESTAG').AsDateTime)
  else
    QRLDtFim.Caption := '-';

  QRLEndereco.Caption := QryRelatorio.FieldByName('DE_END_EMP_OFERT_ESTAG').AsString + ', Nº ' + QryRelatorio.FieldByName('NU_END_EMP_OFERT_ESTAG').AsString;

  if QryRelatorio.FieldByName('FLA_DIA_SEMANA').AsString = 'SSE' then
    QRLDiasSemana.Caption := 'Segunda à Sexta'
  else if QryRelatorio.FieldByName('FLA_DIA_SEMANA').AsString = 'SSA' then
    QRLDiasSemana.Caption := 'Segunda à Sábado'
  else if QryRelatorio.FieldByName('FLA_DIA_SEMANA').AsString = 'OUT' then
    QRLDiasSemana.Caption := 'Outros'
  else QRLDiasSemana.Caption := '-';

  if not QryRelatorio.FieldByName('DE_COMP_EMP_OFERT_ESTAG').IsNull then
    QRLEndereco.Caption := QRLEndereco.Caption + ', ' + QryRelatorio.FieldByName('DE_COMP_EMP_OFERT_ESTAG').AsString;

  QRLCidUF.Caption := QryRelatorio.FieldByName('CO_UF_EMP_OFERT_ESTAG').AsString + ' / ' + QryRelatorio.FieldByName('NO_CIDADE').AsString;

  if not QryRelatorio.FieldByName('HR_INI_OFERT_ESTAG').IsNull then
  begin
    QRLHrIni.Caption := FormatFloat('00:00',QryRelatorio.FieldByName('HR_INI_OFERT_ESTAG').AsFloat);
  end
  else
    QRLHrIni.Caption := '-';

  if not QryRelatorio.FieldByName('HR_FIM_OFERT_ESTAG').IsNull then
  begin
    QRLHrFinal.Caption := FormatFloat('00:00',QryRelatorio.FieldByName('HR_FIM_OFERT_ESTAG').AsFloat);
  end
  else
    QRLHrFinal.Caption := '-';

  if not QryRelatorio.FieldByName('VL_REMUN').IsNull then
    QRLRemun.Caption := 'R$ ' + FloatToStrF(QryRelatorio.FieldByName('VL_REMUN').AsFloat,ffNumber,15,2)
  else
    QRLRemun.Caption := 'Não';

  if QryRelatorio.FieldByName('FLA_POSS_EFETI_OFERT_ESTAG').AsString = 'S' then
    QRLEfet.Caption := 'Sim'
  else
    QRLEfet.Caption := 'Não';

  if not QryRelatorio.FieldByName('NU_TELE_CONTA_OFERT_ESTAG').IsNull then
    QRLTelContato.Caption := FormatFloat('(##) ####-####',QryRelatorio.FieldByName('NU_TELE_CONTA_OFERT_ESTAG').AsFloat)
  else
    QRLTelContato.Caption := '-';
end;

end.
