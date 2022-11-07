unit U_FrmRelRelacCtasUnidade;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacCtasUnidade = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLabel2: TQRLabel;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRDBText4: TQRDBText;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLNoEmp: TQRLabel;
    QRBand2: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLAgencia: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLTotal: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel9: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLabel7: TQRLabel;
    QRLDtAbertura: TQRLabel;
    QRLConta: TQRLabel;
    QRLNoGerente: TQRLabel;
    QRLabel10: TQRLabel;
    QRLTelGeren: TQRLabel;
    QRLabel12: TQRLabel;
    QRLStatus: TQRLabel;
    QRLabel11: TQRLabel;
    QRLBoleto: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
    total : integer;
  public
    { Public declarations }
  end;

var
  FrmRelRelacCtasUnidade: TFrmRelRelacCtasUnidade;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacCtasUnidade.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('DI_AGENCIA').IsNull then
    QRLAgencia.Caption := QryRelatorio.FieldByName('CO_AGENCIA').AsString + '-' + QryRelatorio.FieldByName('DI_AGENCIA').AsString
  else
    QRLAgencia.Caption := QryRelatorio.FieldByName('CO_AGENCIA').AsString;

  if not QryRelatorio.FieldByName('DI_AGENCIA').IsNull then
    QRLConta.Caption := Trim(QryRelatorio.FieldByName('CO_CONTA').AsString) + '-' + QryRelatorio.FieldByName('CO_DIG_CONTA').AsString
  else
    QRLConta.Caption := QryRelatorio.FieldByName('CO_CONTA').AsString;

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  if not QryRelatorio.FieldByName('NO_GER_CTA').IsNull then
    QRLNoGerente.Caption := QryRelatorio.FieldByName('NO_GER_CTA').AsString
  else
    QRLNoGerente.Caption := '-';

  if not QryRelatorio.FieldByName('NU_TEL_GER_CTA').IsNull then
    QRLTelGeren.Caption := FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TEL_GER_CTA').AsString)
  else
    QRLTelGeren.Caption := '-';

  if not QryRelatorio.FieldByName('DT_ABERT_CTA').IsNull then
    QRLDtAbertura.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_ABERT_CTA').AsDateTime)
  else
    QRLDtAbertura.Caption := '-';

  if QryRelatorio.FieldByName('CO_STATUS').AsString = 'A' then
    QRLStatus.Caption := 'Ativo'
  else
    QRLStatus.Caption := 'Inativo';

  if QryRelatorio.FieldByName('FLAG_EMITE_BOLETO_BANC').AsString = 'S' then
    QRLBoleto.Caption := 'Sim'
  else
    QRLBoleto.Caption := 'Não';

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);

  total := total + 1;
end;

procedure TFrmRelRelacCtasUnidade.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorio.FieldByName('NO_FANTAS_EMP').IsNull then
    QRLNoEmp.Caption := QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString
  else
    QRLNoEmp.Caption := '-';

end;

procedure TFrmRelRelacCtasUnidade.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
  total := 0;
end;

procedure TFrmRelRelacCtasUnidade.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(total);
end;

procedure TFrmRelRelacCtasUnidade.QRBand2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacCtasUnidade]);

end.
