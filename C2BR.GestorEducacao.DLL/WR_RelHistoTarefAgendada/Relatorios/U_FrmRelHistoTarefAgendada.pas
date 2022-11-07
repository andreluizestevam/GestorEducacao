unit U_FrmRelHistoTarefAgendada;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelHistoTarefAgendada = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRGroup1: TQRGroup;
    QRShape1: TQRShape;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRLTotTarefas: TQRLabel;
    QryRelatorioORG_CODIGO_ORGAO: TIntegerField;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_COL: TIntegerField;
    QryRelatorioCO_IDENT_TAREF: TAutoIncField;
    QryRelatorioCO_ORGAO_SOLIC_TAREF_AGEND: TIntegerField;
    QryRelatorioCO_EMP_SOLIC_TAREF_AGEND: TIntegerField;
    QryRelatorioCO_FUNCI_SOLIC_TAREF_AGEND: TIntegerField;
    QryRelatorioNM_RESUM_TAREF_AGEND: TStringField;
    QryRelatorioDE_DETAL_TAREF_AGEND: TStringField;
    QryRelatorioDT_CADAS_TAREF_AGEND: TDateTimeField;
    QryRelatorioDT_COMPR_TAREF_AGEND: TDateTimeField;
    QryRelatorioDT_LIMIT_TAREF_AGEND: TDateTimeField;
    QryRelatorioDT_REALIZ_TAREF_AGEND: TDateTimeField;
    QryRelatorioCO_PRIOR_TAREF_AGEND: TStringField;
    QryRelatorioDT_REPAS_TAREF_AGEND: TDateTimeField;
    QryRelatorioDE_MOTIV_REPAS_TAREF_AGEND: TStringField;
    QryRelatorioCO_ORGAO_REPAS_TAREF_AGEND: TIntegerField;
    QryRelatorioCO_EMP_REPAS_TAREF_AGEND: TIntegerField;
    QryRelatorioCO_FUNCI_REPAS_TAREF_AGEND: TIntegerField;
    QryRelatorioCO_SITU_TAREF_AGEND: TStringField;
    QryRelatorioDE_OBSERV_TAREF_AGEND: TStringField;
    QryRelatorioCO_FLA_SMS_TAREF_AGEND: TStringField;
    QryRelatorioCO_FLA_REABERTA: TStringField;
    QryRelatoriono_fantas_emp: TStringField;
    QRDBText9: TQRDBText;
    QryRelatorioDE_SITU_TAREF_AGEND: TStringField;
    QRDBText10: TQRDBText;
    QryRelatorioDE_PRIOR_TAREF_AGEND: TStringField;
    QrlParametros: TQRLabel;
    QRLabel1: TQRLabel;
    QRLTarefa: TQRLabel;
    QRLDtCompr: TQRLabel;
    QRLSMS: TQRLabel;
    QRLabel5: TQRLabel;
    QRShape2: TQRShape;
    QrlTitSerieTurma: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel3: TQRLabel;
    QRBand2: TQRBand;
    QRLabel6: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel11: TQRLabel;
    QRLDtPrazo: TQRLabel;
    QRLDtCadas: TQRLabel;
    QRLResponsavel: TQRLabel;
    QRLEmissor: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QryRelatorioresponsavel: TStringField;
    QryRelatoriomatres: TStringField;
    QryRelatoriosolicitante: TStringField;
    QryRelatoriomatsol: TStringField;
    QryRelatoriosiglaResp: TWideStringField;
    QryRelatoriosiglaSol: TWideStringField;
    QRLabel8: TQRLabel;
    QRDBText3: TQRDBText;
    QryRelatorioCO_CHAVE_UNICA_TAREF: TFloatField;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelHistoTarefAgendada: TFrmRelHistoTarefAgendada;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelHistoTarefAgendada.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioDT_COMPR_TAREF_AGEND.IsNull then
    QRLDtCompr.Caption := FormatDateTime('dd/MM/yy',QryRelatorioDT_COMPR_TAREF_AGEND.AsDateTime)
  else
    QRLDtCompr.Caption := '-';

  if not QryRelatorioDT_CADAS_TAREF_AGEND.IsNull then
    QRLDtCadas.Caption := FormatDateTime('dd/MM/yy',QryRelatorioDT_CADAS_TAREF_AGEND.AsDateTime)
  else
    QRLDtCadas.Caption := '-';

  if not QryRelatorioDT_LIMIT_TAREF_AGEND.IsNull then
    QRLDtPrazo.Caption := FormatDateTime('dd/MM/yy',QryRelatorioDT_LIMIT_TAREF_AGEND.AsDateTime)
  else
    QRLDtPrazo.Caption := '-';

  if not QryRelatorioresponsavel.IsNull then
    QRLResponsavel.Caption := FormatMaskText('99.999-9;0',QryRelatoriomatres.AsString) + ' ' + QryRelatorioresponsavel.AsString
  else
    QRLResponsavel.Caption := '-';

  if not QryRelatorioresponsavel.IsNull then
    QRLEmissor.Caption := FormatMaskText('99.999-9;0',QryRelatoriomatsol.AsString) + ' ' + QryRelatoriosolicitante.AsString
  else
    QRLEmissor.Caption := '-';

  if QryRelatorioCO_FLA_SMS_TAREF_AGEND.AsString = 'N' then
    QRLSMS.Caption := 'Não'
  else
    QRLSMS.Caption := 'Sim';

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  QRLTotTarefas.Caption := IntToStr(StrToInt(QRLTotTarefas.Caption) + 1);
end;

procedure TFrmRelHistoTarefAgendada.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotTarefas.Caption := '0';
end;

procedure TFrmRelHistoTarefAgendada.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLTarefa.Caption := FormatFloat('000000;0',QryRelatorioCO_CHAVE_UNICA_TAREF.AsFloat) + ' - ' + QryRelatorioNM_RESUM_TAREF_AGEND.AsString;
end;

end.
