unit U_FrmRelCustoFinFuncTpcon;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelCustoFinFuncTpcon = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QrlParamRel: TQRLabel;
    QrlMatricula: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText9: TQRDBText;
    QrlPage: TQRLabel;
    QRLabel14: TQRLabel;
    QrlRSBase: TQRLabel;
    QRLIdade: TQRLabel;
    QRLSalRef: TQRLabel;
    QRLTVR: TQRLabel;
    QryRelatoriono_fun: TStringField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioNU_CPF_COL: TStringField;
    QryRelatorioDT_INIC_ATIV_COL: TDateTimeField;
    QryRelatorioCO_TPCAL: TIntegerField;
    QryRelatorioVL_SALAR_COL: TFloatField;
    QryRelatorioNU_CARGA_HORARIA: TIntegerField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorioDEFICIENCIA: TStringField;
    QryRelatorioCATEGORIA: TStringField;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_SIGLA_DEPTO: TStringField;
    QryRelatorioNO_INST: TStringField;
    QryRelatorioNO_TPCON: TStringField;
    QryRelatorioNO_FANTAS_EMP: TStringField;
    QryRelatorioSIGLA: TWideStringField;
    QryRelatorioCO_MAT_COL: TStringField;
    QRGroup1: TQRGroup;
    QRLabel13: TQRLabel;
    QRShape2: TQRShape;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel11: TQRLabel;
    QryRelatorioCO_TPCON: TIntegerField;
    QRBand1: TQRBand;
    QRLabel15: TQRLabel;
    QrlTotalFunc: TQRLabel;
    QRLabel17: TQRLabel;
    QrlMDRSBase: TQRLabel;
    QRLabel16: TQRLabel;
    QrlTotRSBase: TQRLabel;
    QRLNoTpCon: TQRLabel;
    QRLNoCol: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    salTotal : Double;
    salAlter : Boolean;
  public
    { Public declarations }
  end;

var
  FrmRelCustoFinFuncTpcon: TFrmRelCustoFinFuncTpcon;

implementation

{$R *.dfm}
uses U_DataModuleSGE, MaskUtils, DateUtils;

procedure TFrmRelCustoFinFuncTpcon.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  salTotal := 0;
  QrlTotalFunc.Caption := '0';
  QrlTotRSBase.Caption := '0';
  QrlRSBase.Caption := '0';
end;

procedure TFrmRelCustoFinFuncTpcon.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;
  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString);
  //Aparecer 'data de nascimento(dd/MM/yy) (idade)'
  diasnoano := 365.6;
  QRLIdade.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.fieldbyname('DT_INIC_ATIV_COL').AsDateTime) + ' ('+IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_INIC_ATIV_COL').AsDateTime) / diasnoano))+')';

  //Escrever o TVR
  if QryRelatorioCO_TPCAL.IsNull then
    QRLTVR.Caption := '-'
  else
  begin
    if QryRelatorioCO_TPCAL.AsInteger = 4 then
      QRLTVR.Caption := 'HOR';
    if QryRelatorioCO_TPCAL.AsInteger = 3 then
      QRLTVR.Caption := 'MES';
    if QryRelatorioCO_TPCAL.AsInteger = 2 then
      QRLTVR.Caption := 'SEM';
    if QryRelatorioCO_TPCAL.AsInteger = 1 then
      QRLTVR.Caption := 'DIA';
  end;

  //Escrever o Salário referência
  if QryRelatorioVL_SALAR_COL.IsNull then
    QRLSalRef.Caption := '-'
  else
    QRLSalRef.Caption := FloatToStrF(QryRelatorioVL_SALAR_COL.AsFloat,ffNumber,15,2);

  //Escrever o Salário base
  if QryRelatorioVL_SALAR_COL.IsNull then
    QrlRSBase.Caption := '-'
  else
  begin
    if QryRelatorioCO_TPCAL.IsNull then
      QrlRSBase.Caption := QRLSalRef.Caption
    else
    begin
      if QryRelatorioCO_TPCAL.AsInteger = 4 then
      begin
        if QryRelatorioNU_CARGA_HORARIA.IsNull then
          QrlRSBase.Caption := '-'
        else
          QrlRSBase.Caption := FloatToStr(QryRelatorioVL_SALAR_COL.AsFloat*QryRelatorioNU_CARGA_HORARIA.AsInteger);
      end;

      if QryRelatorioCO_TPCAL.AsInteger = 1 then
      begin
        QrlRSBase.Caption := FloatToStr(QryRelatorioVL_SALAR_COL.AsFloat*30);
      end;

      if QryRelatorioCO_TPCAL.AsInteger = 3 then
      begin
        QrlRSBase.Caption := FloatToStr(QryRelatorioVL_SALAR_COL.AsFloat);
      end;

      if QryRelatorioCO_TPCAL.AsInteger = 2 then
      begin
        QrlRSBase.Caption := FloatToStr(QryRelatorioVL_SALAR_COL.AsFloat*4);
      end;
    end;
  end;
  
  // zebrado
  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;

  // total funcionários
  QrlTotalFunc.Caption := IntToStr(StrToInt(QrlTotalFunc.Caption) + 1);

  // mascara matricula
  QrlMatricula.Caption := FormatMaskText('00.000-0;0', QryRelatorioCO_MAT_COL.AsString);

  // total de salários base
  if QrlRSBase.Caption <> '-' then
  begin
    salAlter := true;
    salTotal := salTotal + StrToFloat(QrlRSBase.Caption);
  end;
    
  if QrlRSBase.Caption <> '-' then
    QrlRSBase.Caption := FloatToStrF(StrToFloat(QrlRSBase.Caption),ffNumber,15,2);
end;

procedure TFrmRelCustoFinFuncTpcon.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  // Média de salário base dos funcionários
  if (salTotal > 0) and (salAlter) then
  begin
    QrlTotRSBase.Caption := FloatToStrF(salTotal,ffNumber,20,2);
    QrlMDRSBase.Caption := FloatToStrF( salTotal / StrToFloat(QrlTotalFunc.Caption),ffNumber,20,2);
  end
  else
  begin
    QrlTotRSBase.Caption := '-';
    QrlMDRSBase.Caption := '-';
  end;
end;

procedure TFrmRelCustoFinFuncTpcon.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  salTotal := 0;
  QrlTotalFunc.Caption := '0';
  QrlTotRSBase.Caption := '0';
  QrlRSBase.Caption := '0';
end;

procedure TFrmRelCustoFinFuncTpcon.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_TPCON.IsNull then
    QRLNoTpCon.Caption := UpperCase(QryRelatorioNO_TPCON.AsString);
end;

end.
