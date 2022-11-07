unit U_FrmRelMapaPerfilSalaAulaAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaPerfilSalaAulaAluno = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel22: TQRLabel;
    QRShape1: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel10: TQRLabel;
    SummaryBand1: TQRBand;
    QRLParam: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRDBText11: TQRDBText;
    QRLUnidade: TQRLabel;
    QRLTpSala: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLVentil: TQRLabel;
    QRLArCond: TQRLabel;
    QRLArea: TQRLabel;
    QRLAltura: TQRLabel;
    QRLCompri: TQRLabel;
    QRLLargur: TQRLabel;
    QRLabel15: TQRLabel;
    QRLTotal: TQRLabel;
    SummaryBand2: TQRBand;
    QRLabel16: TQRLabel;
    QRLTotGeral: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
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
  FrmRelMapaPerfilSalaAulaAluno: TFrmRelMapaPerfilSalaAulaAluno;

implementation

Uses U_DataModuleSGE,U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaPerfilSalaAulaAluno.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  
  if QryRelatorio.FieldByName('CO_TIPO_SALA_AULA').AsString = 'A' then
    QRLTpSala.Caption := 'Aula'
  else if QryRelatorio.FieldByName('CO_TIPO_SALA_AULA').AsString = 'L' then
    QRLTpSala.Caption := 'Laboratório'
  else if QryRelatorio.FieldByName('CO_TIPO_SALA_AULA').AsString = 'E' then
    QRLTpSala.Caption := 'Estudo'
  else if QryRelatorio.FieldByName('CO_TIPO_SALA_AULA').AsString = 'M' then
    QRLTpSala.Caption := 'Monitoria'
  else
    QRLTpSala.Caption := 'Outros';

  if not QryRelatorio.FieldByName('VL_LARGUR_SALA_AULA').IsNull then
    QRLLargur.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_LARGUR_SALA_AULA').AsFloat, ffNumber, 15,2)
  else
    QRLLargur.Caption := '-';
    
  if not QryRelatorio.FieldByName('VL_ALTURA_SALA_AULA').IsNull then
    QRLAltura.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_ALTURA_SALA_AULA').AsFloat, ffNumber, 15,2)
  else
    QRLAltura.Caption := '-';

  if not QryRelatorio.FieldByName('VL_COMPRI_SALA_AULA').IsNull then
    QRLCompri.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_COMPRI_SALA_AULA').AsFloat, ffNumber, 15,2)
  else
    QRLCompri.Caption := '-';

  if (not QryRelatorio.FieldByName('VL_LARGUR_SALA_AULA').IsNull) and (not QryRelatorio.FieldByName('VL_ALTURA_SALA_AULA').IsNull) then
    QRLArea.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_LARGUR_SALA_AULA').AsFloat * QryRelatorio.FieldByName('VL_ALTURA_SALA_AULA').AsFloat, ffNumber, 15,2)
  else
    QRLArea.Caption := '-';

  if (not QryRelatorio.FieldByName('QT_VENTIL_SALA_AULA').IsNull) and (QryRelatorio.FieldByName('QT_VENTIL_SALA_AULA').AsString <> '') then
    QRLVentil.Caption := 'Sim'
  else
    QRLVentil.Caption := 'Não';

  if (not QryRelatorio.FieldByName('QT_ARCOND_SALA_AULA').IsNull) and (QryRelatorio.FieldByName('QT_VENTIL_SALA_AULA').AsString <> '') then
    QRLArCond.Caption := 'Sim'
  else
    QRLArCond.Caption := 'Não';

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelMapaPerfilSalaAulaAluno.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLUnidade.Caption := UpperCase(QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString);
end;

procedure TFrmRelMapaPerfilSalaAulaAluno.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
  QRLTotGeral.Caption := '0';
end;

procedure TFrmRelMapaPerfilSalaAulaAluno.SummaryBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelMapaPerfilSalaAulaAluno.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(StrToInt(QRLTotGeral.Caption) + StrToInt(QRLTotal.Caption)); 
end;

end.
