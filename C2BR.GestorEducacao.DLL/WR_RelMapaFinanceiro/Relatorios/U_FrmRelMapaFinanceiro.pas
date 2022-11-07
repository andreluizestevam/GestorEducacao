unit U_FrmRelMapaFinanceiro;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, DateUtils;

type
  TFrmRelMapaFinanceiro = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRShape1: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRGroup2: TQRGroup;
    QRLabel13: TQRLabel;
    QRBand1: TQRBand;
    QRDBText2: TQRDBText;
    QRBand2: TQRBand;
    QRShape18: TQRShape;
    QrlTotalValor: TQRLabel;
    QrlTotalValorPago: TQRLabel;
    QrlTotalMulta: TQRLabel;
    QrlTotalValorCorrigido: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel10: TQRLabel;
    QRDBText4: TQRDBText;
    QRDBValor: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBValorPago: TQRDBText;
    QrlValor: TQRLabel;
    QrlMulta: TQRLabel;
    QrlDia: TQRLabel;
    QRLabel11: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel12: TQRLabel;
    QRLJuros: TQRLabel;
    QRLabel14: TQRLabel;
    QRLParametros: TQRLabel;
    QRLDesconto: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel1: TQRLabel;
    QrlTotalValorSerTur: TQRLabel;
    QrlTotalValorPagoSerTur: TQRLabel;
    QrlTotalMultaSerTur: TQRLabel;
    QrlTotalValorCorrigidoSerTur: TQRLabel;
    QrlTotalJuros: TQRLabel;
    QrlTotalDesconto: TQRLabel;
    QrlTotalJurosSerTur: TQRLabel;
    QrlTotalDescontoSerTur: TQRLabel;
    QRLDocStatus: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure QRDBValorPrint(sender: TObject; var Value: String);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    VlTotDoc, VlTotJur, VlTotMul, VlTotDesc, VlTotPag, VlTotCorr: Extended;
    VlTotDocG, VlTotJurG, VlTotMulG, VlTotDescG, VlTotPagG, VlTotCorrG: Extended;
  public
    { Public declarations }
  end;

var
  FrmRelMapaFinanceiro: TFrmRelMapaFinanceiro;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMapaFinanceiro.QRDBValorPrint(sender: TObject;var Value: String);
begin
  inherited;
  if Value <> '' then
    Value := FormatFloat('#0.00',StrToFloat(Value));
end;

procedure TFrmRelMapaFinanceiro.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'Q' then
    QRLDocStatus.Caption := QryRelatorio.FieldByName('NU_DOC').AsString + ' / Pago';
  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'A' then
    QRLDocStatus.Caption := QryRelatorio.FieldByName('NU_DOC').AsString + ' / Em aberto';
  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'C' then
    QRLDocStatus.Caption := QryRelatorio.FieldByName('NU_DOC').AsString + ' / Cancelado';

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'A' then
  begin
    if QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime < now then
      QrlDia.Caption := '+' + intToStr((DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime,trunc(now))))
    else
      QrlDia.Caption := '-' + intToStr((DaysBetween(trunc(now), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime)));
  end;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'Q' then
  begin
    if QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime < QryRelatorio.FieldByName('DT_REC_DOC').AsDateTime then
      QrlDia.Caption := '+' + intToStr((DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime,trunc(QryRelatorio.FieldByName('DT_REC_DOC').AsDateTime))))
    else if QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime > QryRelatorio.FieldByName('DT_REC_DOC').AsDateTime then
      QrlDia.Caption := '-' + intToStr((DaysBetween(trunc(QryRelatorio.FieldByName('DT_REC_DOC').AsDateTime), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime)))
    else
      QrlDia.Caption := '0';
  end;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'A' then
  begin
    if not QryRelatorio.FieldByName('VR_JUR_DOC').IsNull then
    begin
      if (DaysBetween(trunc(now), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime) > 0) and (QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime < trunc(now)) then
      begin
        if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_JUR').AsString = 'V' then
          QRLJuros.Caption := FloatToStrF((DaysBetween(trunc(now), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime)) * QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat,ffNumber,10,2)
        else
          QRLJuros.Caption := FloatToStrF((DaysBetween(trunc(now), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime)) * ((QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat * QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat)/100),ffNumber,10,2)
      end
      else
        QRLJuros.Caption := '0.00';
    end
    else
      QRLJuros.Caption := '0.00';

    if not QryRelatorio.FieldByName('VR_MUL_DOC').IsNull then
    begin
      if QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime > now then
      begin
        if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_MUL').AsString = 'V' then
          QRLMulta.Caption := FloatToStrf(QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat,ffNumber,10,2)
        else
          QRLMulta.Caption := FloatToStrf(((QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat * QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat)/100),ffNumber,10,2)
      end
      else
        QRLMulta.Caption := '0.00';
    end
    else
      QRLMulta.Caption := '0.00';

    if not QryRelatorio.FieldByName('VR_DES_DOC').IsNull then
    begin
      if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_MUL').AsString = 'V' then
        QRLDesconto.Caption := FloatToStrf(QryRelatorio.FieldByName('VR_DES_DOC').AsFloat,ffNumber,10,2)
      else
        QRLDesconto.Caption := FloatToStrf(((QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat * QryRelatorio.FieldByName('VR_DES_DOC').AsFloat)/100),ffNumber,10,2)
    end
    else
      QRLDesconto.Caption := '0.00';
  end;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'Q' then
  begin
    if not QryRelatorio.FieldByName('VR_JUR_PAG').IsNull then
    begin
      QRLJuros.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_JUR_PAG').AsFloat,ffNumber,10,2);
    end
    else
      QRLJuros.Caption := '0.00';

    if not QryRelatorio.FieldByName('VR_MUL_PAG').IsNull then
    begin
      QRLMulta.Caption := FloatToStrf(QryRelatorio.FieldByName('VR_MUL_PAG').AsFloat,ffNumber,10,2)
    end
    else
      QRLMulta.Caption := '0.00';

    if not QryRelatorio.FieldByName('VR_DES_PAG').IsNull then
    begin
      QRLDesconto.Caption := FloatToStrf(QryRelatorio.FieldByName('VR_DES_PAG').AsFloat,ffNumber,10,2)
    end
    else
      QRLDesconto.Caption := '0.00';
  end;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'A' then
  begin
    if QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime > now then
    begin
      QRLValor.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat + StrToFloat(StringReplace(QRLJuros.Caption,'.','', [rfReplaceAll])) + StrToFloat(StringReplace(QRLMulta.Caption,'.','', [rfReplaceAll])) - StrToFloat(StringReplace(QRLDesconto.Caption,'.','', [rfReplaceAll])),ffNumber,10,2)
      //QRLValor.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat + (DaysBetween(trunc(now), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime)) * QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat + QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat - QryRelatorio.FieldByName('VR_DES_DOC').AsFloat,ffNumber,10,2)
    end
    else
      QRLValor.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat - QryRelatorio.FieldByName('VR_DES_DOC').AsFloat,ffNumber,10,2);
  end;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'Q' then
  begin
      QRLValor.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAG').AsFloat,ffNumber,10,2);
  end;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'C' then
  begin
      QRLValor.Caption := '0.00';
      QrlDia.Caption := '0.00';
      QrlMulta.Caption := '0.00';
      QRLJuros.Caption := '0.00';
      QRLDesconto.Caption := '0.00';
      QrlValor.Caption := '0.00';
  end;

  VlTotDoc := VlTotDoc + QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat;
  if (QRLJuros.Caption <> '0,00') and (QRLJuros.Caption <> '0.00')  then
    VlTotJur := VlTotJur + StrToFloat(StringReplace(QRLJuros.Caption,'.','', [rfReplaceAll]));
  if (QRLMulta.Caption <> '0,00') and (QRLMulta.Caption <> '0.00') then
    VlTotMul := VlTotMul + StrToFloat(StringReplace(QRLMulta.Caption,'.','', [rfReplaceAll]));
  if (QRLDesconto.Caption <> '0,00') and (QRLDesconto.Caption <> '0.00') then
    VlTotDesc := VlTotDesc + StrToFloat(StringReplace(QRLDesconto.Caption,'.','', [rfReplaceAll]));
  VlTotPag := VlTotPag + QryRelatorio.FieldByName('VR_PAG').AsFloat;
  if (QRLValor.Caption <> '0,00') and (QRLValor.Caption <> '0.00') then
    VlTotCorr := VlTotCorr + StrToFloat(StringReplace(QRLValor.Caption,'.','', [rfReplaceAll]));

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;
end;

procedure TFrmRelMapaFinanceiro.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QrlTotalValor.Caption := FormatFloat('###,##0.00', VlTotDoc);
  QrlTotalValorPago.Caption := FormatFloat('###,##0.00', VlTotPag);
  QrlTotalMulta.Caption := FormatFloat('###,##0.00', VlTotMul);
  QrlTotalJuros.Caption := FormatFloat('###,##0.00', VlTotJur);
  QrlTotalDesconto.Caption := FormatFloat('###,##0.00', VlTotDesc);
  QrlTotalValorCorrigido.Caption := FormatFloat('###,##0.00', VlTotCorr);

  VlTotDocG      := VlTotDocG + VlTotDoc;
  VlTotCorrG     := VlTotCorrG + VlTotCorr;
  VlTotJurG      := VlTotJurG + VlTotJur;
  VlTotMulG      := VlTotMulG + VlTotMul;
  VlTotDescG     := VlTotDescG + VlTotDesc;
  VlTotPagG      := VlTotPagG + VlTotPag;

  VlTotDoc       := 0;
  VlTotCorr      := 0;
  VlTotJur       := 0;
  VlTotMul       := 0;
  VlTotDesc      := 0;
  VlTotPag       := 0;


end;

procedure TFrmRelMapaFinanceiro.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;

  VlTotDocG      := 0;
  VlTotCorrG     := 0;
  VlTotJurG      := 0;
  VlTotMulG      := 0;
  VlTotDescG     := 0;
  VlTotPagG      := 0;

  VlTotDoc       := 0;
  VlTotCorr      := 0;
  VlTotJur       := 0;
  VlTotMul       := 0;
  VlTotDesc      := 0;
  VlTotPag       := 0;
end;

procedure TFrmRelMapaFinanceiro.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  QrlTotalValorSerTur.Caption := FormatFloat('###,##0.00', VlTotDoc);
  QrlTotalValorPagoSerTur.Caption := FormatFloat('###,##0.00', VlTotPag);
  QrlTotalMultaSerTur.Caption := FormatFloat('###,##0.00', VlTotMul);
  QrlTotalJurosSerTur.Caption := FormatFloat('###,##0.00', VlTotJur);
  QrlTotalDescontoSerTur.Caption := FormatFloat('###,##0.00', VlTotDesc);
  QrlTotalValorCorrigidoSerTur.Caption := FormatFloat('###,##0.00', VlTotCorr);

  VlTotDocG      := 0;
  VlTotCorrG     := 0;
  VlTotJurG      := 0;
  VlTotMulG      := 0;
  VlTotDescG     := 0;
  VlTotPagG      := 0;
end;

procedure TFrmRelMapaFinanceiro.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLJuros.Caption := '0.00';
  QRLMulta.Caption := '0.00';
  QRLDesconto.Caption := '0.00';
  QRLValor.Caption := '0.00';
end;

procedure TFrmRelMapaFinanceiro.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaFinanceiro]);

end.
