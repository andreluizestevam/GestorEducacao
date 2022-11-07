unit U_FrmRelContasReceber;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, jpeg;

type
  TFrmRelContasReceber = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    DetailBand1: TQRBand;
    QRDBText2: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText13: TQRDBText;
    QRBand1: TQRBand;
    QRLabel10: TQRLabel;
    QRL_VlDoc: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel12: TQRLabel;
    QRDBText1: TQRDBText;
    QRL_Juros: TQRLabel;
    QRL_Multa: TQRLabel;
    QRL_Desc: TQRLabel;
    QRL_VlPago: TQRLabel;
    QRL_VlDocG: TQRLabel;
    QRL_JurosG: TQRLabel;
    QRL_MultaG: TQRLabel;
    QRL_DescG: TQRLabel;
    QRL_VlPagoG: TQRLabel;
    QRLPeriodo: TQRLabel;
    QrlCancel: TQRLabel;
    QRLTipoDoc: TQRLabel;
    QRShape1: TQRShape;
    QRLabel16: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRExpr1: TQRExpr;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    VlTotDoc, VlTotDocTot, VlTotJur, VlTotMul, VlTotDesc, VlTotPag: Extended;
    VlTotDocG, VlTotDocTotG, VlTotJurG, VlTotMulG, VlTotDescG, VlTotPagG: Extended;
  public
    { Public declarations }
  end;

var
  FrmRelContasReceber: TFrmRelContasReceber;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelContasReceber.DetailBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  // Somar valores
  VlTotDoc := VlTotDoc + QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat;
  VlTotDocTot := VlTotDocTot + QryRelatorio.FieldByName('VR_TOT_DOC').AsFloat;
  VlTotJur := VlTotJur + QryRelatorio.FieldByName('VR_JUR_PAG').AsFloat;
  VlTotMul := VlTotMul + QryRelatorio.FieldByName('VR_MUL_PAG').AsFloat;
  VlTotDesc := VlTotDesc + QryRelatorio.FieldByName('VR_DES_PAG').AsFloat;
  VlTotPag := VlTotPag + QryRelatorio.FieldByName('VR_PAG').AsFloat;

  VlTotDocG := VlTotDocG + QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat;
  VlTotDocTotG := VlTotDocTotG + QryRelatorio.FieldByName('VR_TOT_DOC').AsFloat;
  VlTotJurG := VlTotJurG + QryRelatorio.FieldByName('VR_JUR_PAG').AsFloat;
  VlTotMulG := VlTotMulG + QryRelatorio.FieldByName('VR_MUL_PAG').AsFloat;
  VlTotDescG := VlTotDescG + QryRelatorio.FieldByName('VR_DES_PAG').AsFloat;
  VlTotPagG := VlTotPagG + QryRelatorio.FieldByName('VR_PAG').AsFloat;
end;

procedure TFrmRelContasReceber.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';

  //  Zebrar o relatório
  if DetailBand1.Color = $00F2F2F2 then
  begin
     DetailBand1.Color := clWhite;
     QRDBText2.Color := clWhite;
     QRDBText4.Color := clWhite;
     QRDBText5.Color := clWhite;
     QRDBText7.Color := clWhite;
     QRDBText10.Color := clWhite;
     QRDBText11.Color := clWhite;
     QRDBText12.Color := clWhite;
     QRDBText13.Color := clWhite;
     QRDBText1.Color := clWhite;
  end
  else
  begin
     DetailBand1.Color := $00F2F2F2;
     QRDBText2.Color := $00F2F2F2;
     QRDBText4.Color := $00F2F2F2;
     QRDBText5.Color := $00F2F2F2;
     QRDBText7.Color := $00F2F2F2;
     QRDBText11.Color := $00F2F2F2;
     QRDBText10.Color := $00F2F2F2;
     QRDBText12.Color := $00F2F2F2;
     QRDBText13.Color := $00F2F2F2;
     QRDBText1.Color := $00F2F2F2;
  end;

end;

procedure TFrmRelContasReceber.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRL_VlDoc.Caption := FormatFloat('###,##0.00', VlTotDoc);
  QRL_Juros.Caption := FormatFloat('###,##0.00', VlTotJur);
  QRL_Multa.Caption := FormatFloat('###,##0.00', VlTotMul);
  QRL_Desc.Caption := FormatFloat('###,##0.00', VlTotDesc);
  QRL_VlPago.Caption := FormatFloat('###,##0.00', VlTotPag);

  VlTotDoc := 0;
  VlTotDocTot := 0;
  VlTotJur := 0;
  VlTotMul := 0;
  VlTotDesc := 0;
  VlTotPag := 0;

end;

procedure TFrmRelContasReceber.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRL_VlDocG.Caption := FormatFloat('###,##0.00', VlTotDocG);
  QRL_JurosG.Caption := FormatFloat('###,##0.00', VlTotJurG);
  QRL_MultaG.Caption := FormatFloat('###,##0.00', VlTotMulG);
  QRL_DescG.Caption := FormatFloat('###,##0.00', VlTotDescG);
  QRL_VlPagoG.Caption := FormatFloat('###,##0.00', VlTotPagG);

  VlTotDocG := 0;
  VlTotDocTotG := 0;
  VlTotJurG := 0;
  VlTotMulG := 0;
  VlTotDescG := 0;
  VlTotPagG := 0;

end;

procedure TFrmRelContasReceber.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  VlTotDoc := 0;
  VlTotDocTot := 0;
  VlTotJur := 0;
  VlTotMul := 0;
  VlTotDesc := 0;
  VlTotPag := 0;

  VlTotDocG := 0;
  VlTotDocTotG := 0;
  VlTotJurG := 0;
  VlTotMulG := 0;
  VlTotDescG := 0;
  VlTotPagG := 0;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelContasReceber]);

end.
