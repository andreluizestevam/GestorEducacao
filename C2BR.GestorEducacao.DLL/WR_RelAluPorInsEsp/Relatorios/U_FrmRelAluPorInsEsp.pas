unit U_FrmRelAluPorInsEsp;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelAluPorInsEsp = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRDBNoCur: TQRDBText;
    QRLabel14: TQRLabel;
    QRShape1: TQRShape;
    QRLabel15: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel10: TQRLabel;
    QryRelatorioco_cur: TAutoIncField;
    QryRelatoriono_cur: TStringField;
    QryRelatoriono_alu: TStringField;
    QryRelatorionu_tele_resi_alu: TStringField;
    QryRelatoriode_ende_alu: TStringField;
    QryRelatoriotp_def: TStringField;
    QryRelatorionu_ende_alu: TIntegerField;
    QryRelatoriode_comp_alu: TStringField;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatorioco_alu_cad: TStringField;
    QryRelatoriodt_nasc_alu: TDateTimeField;
    QryRelatoriono_inst_esp: TStringField;
    QryRelatoriono_resp: TStringField;
    QryRelatorionu_tele_celu_resp: TStringField;
    QryRelatoriono_bairro: TStringField;
    QryRelatorioNO_CIDADE: TStringField;
    QrlMatricula: TQRLabel;
    QRDBText7: TQRDBText;
    QRLIdade: TQRLabel;
    QRLDef: TQRLabel;
    QrlCidadeBairro: TQRLabel;
    QRDBText5: TQRDBText;
    SummaryBand1: TQRBand;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLTotMas: TQRLabel;
    QRLabel7: TQRLabel;
    QRLTotFem: TQRLabel;
    QRLabel11: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLSerTur: TQRLabel;
    QryRelatorioco_inst_esp: TAutoIncField;
    QRLabel2: TQRLabel;
    QRLPage: TQRLabel;
    QRLNuNis: TQRLabel;
    QryRelatorionu_nis: TBCDField;
    QryRelatoriono_tur: TStringField;
    QRLTelefone: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelAluPorInsEsp: TFrmRelAluPorInsEsp;

implementation

uses U_DataModuleSGE,MaskUtils,DateUtils;

{$R *.dfm}

procedure TFrmRelAluPorInsEsp.SummaryBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
end;

procedure TFrmRelAluPorInsEsp.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
end;

procedure TFrmRelAluPorInsEsp.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';

  if not QryRelatorionu_tele_celu_resp.IsNull then
    QRLTelefone.Caption := FormatMaskText('(99) 9999-9999;0', QryRelatorionu_tele_celu_resp.AsString)
  else
    QRLTelefone.Caption := '-';

  QRLNuNis.Caption := QryRelatorionu_nis.AsString;

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioco_alu_cad.AsString);

  QrlCidadeBairro.Caption := QryRelatorioNO_CIDADE.AsString + '/' + QryRelatoriono_bairro.AsString;

  if not(QryRelatoriono_cur.IsNull) and not(QryRelatoriono_tur.IsNull) then
    QRLSerTur.Caption := QryRelatoriono_cur.AsString + '/' + QryRelatoriono_tur.AsString;

  if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';

  if QryRelatorio.FieldByName('TP_DEF').AsString = 'N' then
    QRLDef.Caption := 'Nenhuma';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'A' then
    QRLDef.Caption := 'Auditivo';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'V' then
    QRLDef.Caption := 'Visual';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'F' then
    QRLDef.Caption := 'Físico';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'M' then
    QRLDef.Caption := 'Mental';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'I' then
    QRLDef.Caption := 'Múltiplas';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'O' then
    QRLDef.Caption := 'Outros';

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

end.
