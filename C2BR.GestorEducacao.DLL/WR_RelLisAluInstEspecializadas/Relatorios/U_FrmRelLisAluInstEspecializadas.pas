unit U_FrmRelLisAluInstEspecializadas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,U_DataModuleSGE,U_Funcoes, DateUtils;

type
  TFrmRelLisAluInstEspecializadas = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRGroup1: TQRGroup;
    QrlCurso: TQRLabel;
    QRDBNoCur: TQRDBText;
    QRLabel2: TQRLabel;
    QRLPage: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText5: TQRDBText;
    QRLabel22: TQRLabel;
    QRShape1: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel3: TQRLabel;
    QRDBText7: TQRDBText;
    QRLIdade: TQRLabel;
    QryRelatorioco_cur: TAutoIncField;
    QryRelatoriono_cur: TStringField;
    QryRelatoriono_alu: TStringField;
    QryRelatorionu_tele_resi_alu: TStringField;
    QryRelatoriode_ende_alu: TStringField;
    QryRelatorionu_ende_alu: TIntegerField;
    QryRelatoriode_comp_alu: TStringField;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatorioco_alu_cad: TStringField;
    QryRelatoriodt_nasc_alu: TDateTimeField;
    QryRelatoriono_inst_esp: TStringField;
    QryRelatoriono_resp: TStringField;
    QryRelatorionu_tele_celu_resp: TStringField;
    SummaryBand1: TQRBand;
    QRShape2: TQRShape;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLTotMas: TQRLabel;
    QRLTotFem: TQRLabel;
    QRLabel11: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRDBText8: TQRDBText;
    QRLDeficiencia: TQRLabel;
    QryRelatoriono_bairro: TStringField;
    QryRelatoriotp_def: TStringField;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QrlMatricula: TQRLabel;
    QryRelatorioNO_CIDADE: TStringField;
    QrlCidadeBairro: TQRLabel;
    QRLNuNis: TQRLabel;
    QryRelatorionu_nis: TBCDField;
    QryRelatoriono_tur: TStringField;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QryRelatoriode_modu_cur: TStringField;
    QRLTelefone: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelLisAluInstEspecializadas: TFrmRelLisAluInstEspecializadas;

implementation

uses MaskUtils;

{$R *.dfm}

procedure TFrmRelLisAluInstEspecializadas.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  if (not QryRelatorionu_tele_celu_resp.IsNull) and (QryRelatorionu_tele_celu_resp.AsString <> '') then
    QRLTelefone.Caption := FormatMaskText('(99) 9999-9999;0', QryRelatorionu_tele_celu_resp.AsString)
  else
    QRLTelefone.Caption := '-';

  QRLNuNis.Caption := QryRelatorionu_nis.AsString;

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioco_alu_cad.AsString);

  QrlCidadeBairro.Caption := QryRelatorioNO_CIDADE.AsString + '/' + QryRelatoriono_bairro.AsString;

  if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';

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

  if QryRelatorio.FieldByName('co_sexo_alu').AsString = 'M' then
    QRLTotMas.Caption := IntToStr(StrToInt(QRLTotMas.Caption) + 1)
  else
  if QryRelatorio.FieldByName('co_sexo_alu').AsString = 'F' then
    QRLTotFem.Caption := IntToStr(StrToInt(QRLTotFem.Caption) + 1);

  QRLTotGeral.Caption := IntToStr(StrToInt(QRLTotFem.Caption) + StrToInt(QRLTotMas.Caption));
    
  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;
end;

procedure TFrmRelLisAluInstEspecializadas.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
end;

procedure TFrmRelLisAluInstEspecializadas.SummaryBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
end;

end.
