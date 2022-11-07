unit U_FrmRelContasPagar;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, jpeg;

type
  TFrmRelContasPagar = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRBand1: TQRBand;
    SummaryBand1: TQRBand;
    DetailBand1: TQRBand;
    QRDBText2: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText13: TQRDBText;
    QRDBText1: TQRDBText;
    QRDBText6: TQRDBText;
    QRLabel10: TQRLabel;
    QRL_VlDoc: TQRLabel;
    QRL_Juros: TQRLabel;
    QRL_Multa: TQRLabel;
    QRL_Desc: TQRLabel;
    QRL_VlPago: TQRLabel;
    QRLabel12: TQRLabel;
    QRL_VlDocG: TQRLabel;
    QRL_JurosG: TQRLabel;
    QRL_MultaG: TQRLabel;
    QRL_DescG: TQRLabel;
    QRL_VlPagoG: TQRLabel;
    QRLParametros: TQRLabel;
    QRShape1: TQRShape;
    QRLabel16: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel1: TQRLabel;
    procedure QRDBText1Print(sender: TObject; var Value: String);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure DetailBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
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
  FrmRelContasPagar: TFrmRelContasPagar;

implementation

uses Mask, U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelContasPagar.QRDBText1Print(sender: TObject;
  var Value: String);
begin
  inherited;
//  if Length(Value) = 11 then
//    Value := FormatMaskText('999\.999\.999\-99;0; ', Value);

//  if Length(Value) = 14 then
//  Value := FormatMaskText('99\.999\.999\/9999-99;0; ', Value);
end;

procedure TFrmRelContasPagar.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
   QRL_VlDocG.Caption := FormatFloat('###,##0.00', VlTotDocG);
//  QRL_TotDocG.Caption := FormatFloat('###,##0.00', VlTotDocTotG);
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

procedure TFrmRelContasPagar.DetailBand1AfterPrint(Sender: TQRCustomBand;
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

  //VlTotDocG := VlTotDocG + QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat;
  VlTotDocTotG := VlTotDocTotG + QryRelatorio.FieldByName('VR_TOT_DOC').AsFloat;
  VlTotJurG := VlTotJurG + QryRelatorio.FieldByName('VR_JUR_PAG').AsFloat;
  VlTotMulG := VlTotMulG + QryRelatorio.FieldByName('VR_MUL_PAG').AsFloat;
  VlTotDescG := VlTotDescG + QryRelatorio.FieldByName('VR_DES_PAG').AsFloat;
  VlTotPagG := VlTotPagG + QryRelatorio.FieldByName('VR_PAG').AsFloat;
end;

procedure TFrmRelContasPagar.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
{  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'C' then
    QRLDocCancel.Caption := '*'
  else
    QRLDocCancel.Caption := '';
}

  //  Zebrar o relatório
  if DetailBand1.Color = $00F2F2F2 then
  begin
     DetailBand1.Color := clWhite;
     QRDBText2.Color := clWhite;
//     QRDBText3.Color := clWhite;
     QRDBText4.Color := clWhite;
     QRDBText5.Color := clWhite;
     QRDBText6.Color := clWhite;
     QRDBText7.Color := clWhite;
     QRDBText8.Color := clWhite;
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
//     QRDBText3.Color := $00F2F2F2;
     QRDBText4.Color := $00F2F2F2;
     QRDBText5.Color := $00F2F2F2;
     QRDBText6.Color := $00F2F2F2;
     QRDBText7.Color := $00F2F2F2;
     QRDBText8.Color := $00F2F2F2;
     QRDBText11.Color := $00F2F2F2;
     QRDBText10.Color := $00F2F2F2;
     QRDBText12.Color := $00F2F2F2;
     QRDBText13.Color := $00F2F2F2;
     QRDBText1.Color := $00F2F2F2;
  end;
end;

procedure TFrmRelContasPagar.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  VlTotDocG := VlTotDocG + VlTotDoc;
  QRL_VlDoc.Caption := FormatFloat('###,##0.00', VlTotDoc);
//  QRL_TotDoc.Caption := FormatFloat('###,##0.00', VlTotDocTot);
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

procedure TFrmRelContasPagar.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
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
  RegisterClasses([TFrmRelContasPagar]);

end.
