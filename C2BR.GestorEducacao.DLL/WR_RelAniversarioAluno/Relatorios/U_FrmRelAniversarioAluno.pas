unit U_FrmRelAniversarioAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelAniversarioAluno = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRShape1: TQRShape;
    QRDBText3: TQRDBText;
    QRBand2: TQRBand;
    QRLabel2: TQRLabel;
    QRExpr1: TQRExpr;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioNU_TELE_RESI_ALU: TStringField;
    QryRelatorioDT_NASC_ALU: TDateTimeField;
    QryRelatorioNO_CUR: TStringField;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLIdade: TQRLabel;
    QRDBNoResp: TQRDBText;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatoriono_resp: TStringField;
    QryRelatorionu_tele_resi_resp: TStringField;
    QRLMatricula: TQRLabel;
    QRLSerTur: TQRLabel;
    QRGroup1: TQRGroup;
    QryRelatoriomes: TIntegerField;
    QRLMes: TQRLabel;
    QRLabel6: TQRLabel;
    QRShape2: TQRShape;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel4: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel1: TQRLabel;
    QRLDiaMes: TQRLabel;
    QRLabel7: TQRLabel;
    QRLNuNis: TQRLabel;
    QRBand3: TQRBand;
    QRLabel8: TQRLabel;
    QRLTotMes: TQRLabel;
    QrlTelAluno: TQRLabel;
    QrlTelResp: TQRLabel;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorionu_nis: TBCDField;
    QryRelatoriono_tur: TStringField;
    QRLNoAlu: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand3AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelAniversarioAluno: TFrmRelAniversarioAluno;

implementation

uses U_DataModuleSGE, U_Funcoes, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelAniversarioAluno.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  if (not QryRelatorioNU_TELE_RESI_ALU.IsNull) and (QryRelatorioNU_TELE_RESI_ALU.AsString <> '') then
  begin
    QrlTelAluno.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_RESI_ALU').AsString);
  end
  else
  begin
    QrlTelAluno.Caption := '-';
  end;

  if (not QryRelatorionu_tele_resi_resp.IsNull) and (QryRelatorionu_tele_resi_resp.AsString <> '') then
  begin
    QrlTelResp.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('nu_tele_resi_resp').AsString);
  end
  else
  begin
    QrlTelResp.Caption := '-';
  end;

  QRLNuNis.Caption := QryRelatorionu_nis.AsString;

  diasnoano := 365.6;
  QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_ALU').AsDateTime) / diasnoano));
  if QRBand1.Color = clWhite then
    QRBand1.Color := $00EBEBEB
  else
    QRBand1.Color := clWhite;

  if not QryRelatorioco_alu_cad.IsNull then
    QRLMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorioco_alu_cad.AsString);

//  if not(QryRelatoriono_cur.IsNull) and not(QryRelatoriono_tur.IsNull) then
    QRLSerTur.Caption :=  QryRelatorioCO_SIGL_CUR.AsString + ' / ' + QryRelatoriono_tur.AsString;

  if not QryRelatoriodt_nasc_alu.IsNull then
  QRLDiaMes.Caption := FormatDateTime('dd',QryRelatoriodt_nasc_alu.AsDateTime) + '/' + FormatDateTime('MM',QryRelatoriodt_nasc_alu.AsDateTime);

  QRLTotMes.Caption := IntToStr(StrToInt(QRLTotMes.Caption) + 1); 
end;

procedure TFrmRelAniversarioAluno.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  if QryRelatoriomes.AsInteger = 1 then
    QRLMes.Caption := 'Janeiro';
  if QryRelatoriomes.AsInteger = 2 then
    QRLMes.Caption := 'Fevereiro';
  if QryRelatoriomes.AsInteger = 3 then
    QRLMes.Caption := 'Mar�o';
  if QryRelatoriomes.AsInteger = 4 then
    QRLMes.Caption := 'Abril';
  if QryRelatoriomes.AsInteger = 5 then
    QRLMes.Caption := 'Maio';
  if QryRelatoriomes.AsInteger = 6 then
    QRLMes.Caption := 'Junho';
  if QryRelatoriomes.AsInteger = 7 then
    QRLMes.Caption := 'Julho';
  if QryRelatoriomes.AsInteger = 8 then
    QRLMes.Caption := 'Agosto';
  if QryRelatoriomes.AsInteger = 9 then
    QRLMes.Caption := 'Setembro';
  if QryRelatoriomes.AsInteger = 10 then
    QRLMes.Caption := 'Outubro';
  if QryRelatoriomes.AsInteger = 11 then
    QRLMes.Caption := 'Novembro';
  if QryRelatoriomes.AsInteger = 12 then
    QRLMes.Caption := 'Dezembro';
end;

procedure TFrmRelAniversarioAluno.QRBand3AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotMes.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelAniversarioAluno]);

end.
