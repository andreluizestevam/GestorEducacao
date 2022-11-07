unit U_FrmRelDemUnidEscBai;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelDemUnidEscBai = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    SummaryBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRShape2: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRDBText1: TQRDBText;
    QRShape17: TQRShape;
    QRLabel13: TQRLabel;
    QRLPage: TQRLabel;
    QrlQTUnEn: TQRLabel;
    QrlQTFUN: TQRLabel;
    QrlQTPROF: TQRLabel;
    QrlQTALU: TQRLabel;
    QrlTotQTUnEn: TQRLabel;
    QrlTotQTFUN: TQRLabel;
    QrlTotQTPROF: TQRLabel;
    QrlTotQTALU: TQRLabel;
    QRShape22: TQRShape;
    QRShape25: TQRShape;
    QRShape27: TQRShape;
    QrlMDFUN: TQRLabel;
    QrlMDPROF: TQRLabel;
    QrlMDALU: TQRLabel;
    QrlTotMDFUN: TQRLabel;
    QrlTotMDPROF: TQRLabel;
    QrlTotMDALU: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRShape20: TQRShape;
    QrlPCalu: TQRLabel;
    QRShape24: TQRShape;
    QRLabel19: TQRLabel;
    QrlPCprof: TQRLabel;
    QrlPCfun: TQRLabel;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape18: TQRShape;
    QRShape21: TQRShape;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorioQTUnEn: TIntegerField;
    QryRelatorioQTFUN: TIntegerField;
    QryRelatorioQTOTALFUN: TIntegerField;
    QryRelatorioQTPROF: TIntegerField;
    QryRelatorioQTOTALPROF: TIntegerField;
    QryRelatorioQTALU: TIntegerField;
    QryRelatorioQTOTALALU: TIntegerField;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelDemUnidEscBai: TFrmRelDemUnidEscBai;

implementation

{$R *.dfm}
uses U_DataModuleSGE;

procedure TFrmRelDemUnidEscBai.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
// ZEBRADO
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

// QUANTIDADE DE UNIDADES DE ENSINO
  if not QryRelatorioQTUnEn.IsNull then
  begin
    QrlQTUnEn.Caption := QryRelatorioQTUnEn.AsString;
  end
  else
  begin
    QrlQTUnEn.Caption := ' 0 ';
  end;
  QrlTotQTUnEn.Caption := IntToStr(StrToInt(QrlTotQTUnEn.Caption) + QryRelatorioQTUnEn.AsInteger);

// QUANTIDADE DE FUNCIONÁRIOS
  if not QryRelatorioQTFUN.IsNull then
  begin
    QrlQTFUN.Caption := QryRelatorioQTFUN.AsString;
  end
  else
  begin
    QrlQTFUN.Caption := ' 0 ';
  end;
  QrlTotQTFUN.Caption := IntToStr(StrToInt(QrlTotQTFUN.Caption) + QryRelatorioQTFUN.AsInteger);

  // MÉDIA DE FUNCIONÁRIOS POR UNIDADE
  if StrToInt(QrlQTUnEn.Caption) > 0 then
    QrlMDFUN.Caption := FloatToStrF(QryRelatorioQTFUN.AsFloat/QryRelatorioQTUnEn.AsFloat,ffNumber,10,1)
  else
    QrlMDFUN.Caption := ' 0 ';

  // TOTAL DA MÉDIA DE FUNCIONÁRIOS
  QrlTotMDFUN.Caption := FloatToStrF(StrToFloat(QrlTotMDFUN.Caption) + StrToFloat(QrlMDFUN.Caption),ffNumber,10,1);

    // porcentagem de funcionários
    if QryRelatorioQTFUN.AsInteger > 0 then
    begin
      QrlPCfun.Caption := FloatToStrF((QryRelatorioQTFUN.AsInteger * 100) / (StrToInt(QryRelatorioQTOTALFUN.AsString)),ffNumber,10,1);
    end
    else
    begin
      QrlPCfun.Caption := ' 0 ';
    end;

// QUANTIDADE DE PROFESSORES
  if not QryRelatorioQTPROF.IsNull then
  begin
    QrlQTPROF.Caption := QryRelatorioQTPROF.AsString;
  end
  else
  begin
    QrlQTPROF.Caption := ' 0 ';
  end;
  QrlTotQTPROF.Caption := IntToStr(StrToInt(QrlTotQTPROF.Caption) + QryRelatorioQTPROF.AsInteger);

  // MÉDIA DE PROFESSORES POR UNIDADE
  if StrToInt(QrlQTUnEn.Caption) > 0 then
    QrlMDPROF.Caption := FloatToStrF(QryRelatorioQTPROF.AsFloat/QryRelatorioQTUnEn.AsFloat,ffNumber,10,1)
  else
    QrlMDPROF.Caption := ' 0 ';

  // TOTAL DA MÉDIA DE PROFESSORES
  QrlTotMDPROF.Caption := FloatToStrF(StrToFloat(QrlTotMDPROF.Caption) + StrToFloat(QrlMDPROF.Caption),ffNumber,10,1);

    // porcentagem de professor
    if QryRelatorioQTPROF.AsInteger > 0 then
    begin
      QrlPCprof.Caption := FloatToStrF((QryRelatorioQTPROF.AsInteger * 100) / (StrToInt(QryRelatorioQTOTALPROF.AsString)),ffNumber,10,1);
    end
    else
    begin
      QrlPCprof.Caption := ' 0 ';
    end;

// QUANTIDADE DE ALUNOS
  if not QryRelatorioQTALU.IsNull then
  begin
    QrlQTALU.Caption := QryRelatorioQTALU.AsString;
  end
  else
  begin
    QrlQTALU.Caption := ' 0 ';
  end;
  QrlTotQTALU.Caption := IntToStr(StrToInt(QrlTotQTALU.Caption) + QryRelatorioQTALU.AsInteger);

  // MÉDIA DE ALUNOS POR UNIDADE
  if StrToInt(QrlQTUnEn.Caption) > 0 then
    QrlMDALU.Caption := FloatToStrF(QryRelatorioQTALU.AsFloat/QryRelatorioQTUnEn.AsFloat,ffNumber,10,1)
  else
    QrlMDALU.Caption := ' 0 ';

  // TOTAL DA MÉDIA DE Alunos
  //QrlTotMDALU.Caption := FloatToStrF(StrToFloat(QrlTotMDALU.Caption) + StrToFloat(QrlMDALU.Caption),ffNumber,10,1);

     // porcentagem de alunos
    if QryRelatorioQTALU.AsInteger > 0 then
    begin
      QrlPCalu.Caption := FloatToStrF((QryRelatorioQTALU.AsInteger * 100) / (StrToInt(QryRelatorioQTOTALALU.AsString)),ffNumber,10,1);
    end
    else
    begin
      QrlPCalu.Caption := ' 0 ';
    end;

end;

procedure TFrmRelDemUnidEscBai.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotQTUnEn.Caption := '0';
  QrlTotQTFUN.Caption := '0';
  QrlTotQTPROF.Caption := '0';
  QrlTotQTALU.Caption := '0';

  QrlTotMDFUN.Caption := '0';
  QrlTotMDPROF.Caption := '0';
  QrlTotMDALU.Caption := '0';
end;

procedure TFrmRelDemUnidEscBai.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  //'#00000.00',
  if StrToFloat(QrlTotQTUnEn.Caption) > 0 then
    QrlTotMDALU.Caption := FloatToStrF(StrToFloat(QrlTotQTALU.Caption) / StrToFloat(QrlTotQTUnEn.Caption),ffNumber,20,2);

  if StrToFloat(QrlTotQTUnEn.Caption) > 0 then
    QrlTotMDFUN.Caption := FloatToStrF(StrToFloat(QrlTotQTFUN.Caption) / StrToFloat(QrlTotQTUnEn.Caption),ffNumber,20,2);

  if StrToFloat(QrlTotQTUnEn.Caption) > 0 then
    QrlTotMDPROF.Caption := FloatToStrF(StrToFloat(QrlTotQTPROF.Caption) / StrToFloat(QrlTotQTUnEn.Caption),ffNumber,20,2);
end;

end.
