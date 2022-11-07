unit U_FrmRelFornecedores;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelFornecedores = class(TFrmRelTemplate)
    QRLabel1: TQRLabel;
    QRShape1: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    Detail: TQRBand;
    QRDBText2: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QryRelatorioCO_FORN: TAutoIncField;
    QryRelatorioCO_TIPOEMP: TIntegerField;
    QryRelatorioCO_CPFCGC_FORN: TStringField;
    QryRelatorioDE_RAZSOC_FORN: TStringField;
    QryRelatorioNO_FAN_FOR: TStringField;
    QryRelatorioCO_INS_EST_FORN: TStringField;
    QryRelatorioCO_INS_MUN_FORN: TStringField;
    QryRelatorioDE_END_FORN: TStringField;
    QryRelatorioCO_BAIRRO: TIntegerField;
    QryRelatorioCO_CIDADE: TIntegerField;
    QryRelatorioDE_COM_FORN: TStringField;
    QryRelatorioCO_UF_FORN: TStringField;
    QryRelatorioCO_CEP_FORN: TStringField;
    QryRelatorioCO_TEL1_FORN: TStringField;
    QryRelatorioCO_TEL2_FORN: TStringField;
    QryRelatorioCO_FAX_FORN: TStringField;
    QryRelatorioDE_WEB_FORN: TStringField;
    QryRelatorioDE_OBS_FORN: TStringField;
    QryRelatorioDT_CAD_FORN: TDateTimeField;
    QryRelatorioCO_SIT_FORN: TStringField;
    QryRelatorioDT_SIT_FORN: TDateTimeField;
    QryRelatorioDE_EMAIL_CLI: TStringField;
    QryRelatorioNO_CIDADE: TStringField;
    QrlTel: TQRLabel;
    QrlFax: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel13: TQRLabel;
    QRLTotal: TQRLabel;
    QRLNoCli: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelFornecedores: TFrmRelFornecedores;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelFornecedores.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_FAN_FOR.IsNull then
    QRLNoCli.Caption := UpperCase(QryRelatorioNO_FAN_FOR.AsString)
  else
    QRLNoCli.Caption := '-';

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  // TELEFONE
  if not QryRelatorioCO_TEL1_FORN.IsNull then
  begin
    QrlTel.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorioCO_TEL1_FORN.AsString);
  end
  else
  begin
    QrlTel.Caption := ' - ';
  end;

  // FAX
  if not QryRelatorioCO_FAX_FORN.IsNull then
  begin
    QrlFax.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorioCO_FAX_FORN.AsString);
  end
  else
  begin
    QrlFax.Caption := ' - ';
  end;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);

end;

procedure TFrmRelFornecedores.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
