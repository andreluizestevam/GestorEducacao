unit U_FrmRelAtivExtraAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelAtivExtraAluno = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRShape1: TQRShape;
    QRLabel15: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel13: TQRLabel;
    QRDBText7: TQRDBText;
    QRLIdade: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel4: TQRLabel;
    QRLMasFem: TQRLabel;
    QRLTotMas: TQRLabel;
    QRLTotFem: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLabel2: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QryRelatoriono_alu: TStringField;
    QryRelatorionu_nire: TIntegerField;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatoriodt_nasc_alu: TDateTimeField;
    QryRelatoriode_modu_cur: TStringField;
    QryRelatoriono_cur: TStringField;
    QryRelatoriono_turma: TStringField;
    QryRelatorioDT_CAD_ATIV: TDateTimeField;
    QryRelatorioQT_MES_ATIV: TIntegerField;
    QryRelatorioDES_ATIV_EXTRA: TStringField;
    QRDBText1: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText8: TQRDBText;
    QRLAtividade: TQRLabel;
    QRLParametros: TQRLabel;
    QRLNIRE: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelAtivExtraAluno: TFrmRelAtivExtraAluno;

implementation

uses U_DataModuleSGE,MaskUtils,DateUtils;

{$R *.dfm}

procedure TFrmRelAtivExtraAluno.SummaryBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
end;

procedure TFrmRelAtivExtraAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
end;

procedure TFrmRelAtivExtraAluno.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  QRLNIRE.Caption := FormatMaskText('00.000.000-0;0',FormatFloat('000000000;1',QryRelatorionu_nire.AsFloat));

  if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';

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

procedure TFrmRelAtivExtraAluno.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLAtividade.Caption := UpperCase(QryRelatorioDES_ATIV_EXTRA.AsString);
end;

procedure TFrmRelAtivExtraAluno.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLMasFem.Caption := '( Masculino: ' + QRLTotMas.Caption + ' - Feminino: ' + QRLTotFem.Caption + ' )';
end;

end.
