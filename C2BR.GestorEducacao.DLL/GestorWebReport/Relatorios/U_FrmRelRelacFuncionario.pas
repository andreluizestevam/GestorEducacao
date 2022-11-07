unit U_FrmRelRelacFuncionario;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacFuncionario = class(TFrmRelTemplate)
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    DetailBand1: TQRBand;
    QRDBText4: TQRDBText;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel6: TQRLabel;
    QrlMatricula: TQRLabel;
    QrlTelRes: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel15: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText8: TQRDBText;
    QrlDtnasc: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel11: TQRLabel;
    QrlCidadeBairro: TQRLabel;
    QrlDTadmiss: TQRLabel;
    QRDBText1: TQRDBText;
    QrlParamRel: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel10: TQRLabel;
    QRLTotFunc: TQRLabel;
    QryRelatoriono_fun: TStringField;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_COL: TAutoIncField;
    QryRelatorioDT_NASC_COL: TDateTimeField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatorioDT_INIC_ATIV_COL: TDateTimeField;
    QryRelatorioNU_TELE_CELU_COL: TStringField;
    QryRelatorioCO_SEXO_COL: TStringField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorioDEFICIENCIA: TStringField;
    QryRelatorioCATEGORIA: TStringField;
    QryRelatorioNO_DEPTO: TStringField;
    QryRelatorioNO_INST: TStringField;
    QryRelatorioNO_TPCON: TStringField;
    QRLNoCol: TQRLabel;
    procedure QuickRep1StartPage(Sender: TCustomQuickRep);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacFuncionario: TFrmRelRelacFuncionario;

implementation

uses U_DataModuleSGE, U_Funcoes ,MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelRelacFuncionario.QuickRep1StartPage(
  Sender: TCustomQuickRep);
begin
  inherited;
  if QryRelatorio.Active = False then
    QryRelatorio.Active := True;

  if QryCabecalhoRel.Active = False then
    QryCabecalhoRel.Active := True;

end;

procedure TFrmRelRelacFuncionario.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
  var
    diasnoano : real;
begin
  inherited;
  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString);
    
  diasnoano := 365.6;
//  QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_ALU').AsDateTime) / diasnoano));
  QrlDtnasc.Caption := FormatDateTime('dd/mm/yy', QryRelatorioDT_NASC_COL.AsDateTime) + ' (' + IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_COL').AsDateTime) / diasnoano)) + ')';

  QrlDTadmiss.Caption := FormatDateTime('dd/mm/yy', QryRelatorioDT_INIC_ATIV_COL.AsDateTime);

  QrlCidadeBairro.Caption := QryRelatorioNO_CIDADE.AsString + '/' + QryRelatorioNO_BAIRRO.AsString;

  QrlMatricula.Caption := FormatMaskText('00.000-0;0', QryRelatorioCO_MAT_COL.AsString);

  if (not QryRelatorioNU_TELE_CELU_COL.IsNull) and (QryRelatorioNU_TELE_CELU_COL.AsString <> '') then
  begin
    QrlTelRes.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorioNU_TELE_CELU_COL.AsString);
  end
  else
  begin
    QrlTelRes.Caption := '-';
  end;

  //Total de Funcionários
  QrlTotFunc.caption := IntToStr(StrToInt(QrlTotFunc.caption) + 1);
  //

  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;
end;

procedure TFrmRelRelacFuncionario.SummaryBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  QrlTotFunc.caption := '0';
end;

procedure TFrmRelRelacFuncionario.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotFunc.caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacFuncionario]);

end.
