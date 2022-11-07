unit U_FrmRelResumoContasReceber;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, jpeg;

type
  TFrmRelResumoContasReceber = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRDBText2: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText11: TQRDBText;
    SummaryBand1: TQRBand;
    QRLabel12: TQRLabel;
    QRL_VlDocG: TQRLabel;
    QRL_JurosG: TQRLabel;
    QRL_MultaG: TQRLabel;
    QRL_DescG: TQRLabel;
    QRL_VlPagoG: TQRLabel;
    QRLabel2: TQRLabel;
    QRExpr1: TQRExpr;
    QRGroup3: TQRGroup;
    QRBand3: TQRBand;
    QRLabel25: TQRLabel;
    QRL_VlDocTur: TQRLabel;
    QRL_JurosTur: TQRLabel;
    QRL_MultaTur: TQRLabel;
    QRL_DescTur: TQRLabel;
    QRL_VlPagoTur: TQRLabel;
    QRShape1: TQRShape;
    QRLabel16: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel5: TQRLabel;
    QRLPage: TQRLabel;
    QRLNISAluno: TQRLabel;
    QRLJuros: TQRLabel;
    QRLMulta: TQRLabel;
    QRLValor: TQRLabel;
    QRLParametros: TQRLabel;
    QRLNuDocParc: TQRLabel;
    QRLDesconto: TQRLabel;
    QRLabel7: TQRLabel;
    QRLStatus: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRDBText3Print(sender: TObject; var Value: String);
    procedure QRDBText8Print(sender: TObject; var Value: String);
    procedure QRDBText17Print(sender: TObject; var Value: String);
    procedure QRDBText4Print(sender: TObject; var Value: String);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    VlTotDocG, VlTotDocTotG, VlTotJurG, VlTotMulG, VlTotDescG, VlTotPagG: Extended;
    VlTotDocTur, VlTotDocTotTur, VlTotJurTur, VlTotMulTur, VlTotDescTur, VlTotPagTur : Extended;
    VlTotDocCur, VlTotDocTotCur, VlTotJurCur, VlTotMulCur, VlTotDescCur, VlTotPagCur : Extended;
    //FlaImpBand: Boolean;
  public
    { Public declarations }
  end;

var
  FrmRelResumoContasReceber: TFrmRelResumoContasReceber;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelResumoContasReceber.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasAtraso : Integer;
  valorDocto: Extended;
begin
  inherited;
  //Gerar NIS e nome do aluno
  QRLNISAluno.Caption := QryRelatorio.FieldByName('nu_nis').AsString  + ' - ' + UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString);
  QRLNuDocParc.Caption := QryRelatorio.FieldByName('NU_DOC').AsString + '/' + QryRelatorio.FieldByName('NU_PAR').AsString;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'A' then
    QRLStatus.Caption := 'Em aberto';
  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'Q' then
    QRLStatus.Caption := 'Pago';
  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'C' then
    QRLStatus.Caption := 'Cancelado';

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'A' then
  begin
    if (not QryRelatorio.FieldByName('VR_JUR_DOC').IsNull)  then
    begin
      if (DaysBetween(trunc(now), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime) > 0) and (QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime < trunc(now)) then
        QRLJuros.Caption := FloatToStrF((DaysBetween(trunc(now), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime)) * QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat,ffNumber,10,2)
      else
        QRLJuros.Caption := '0.00';
    end
    else
      QRLJuros.Caption := '0.00';

    if not QryRelatorio.FieldByName('VR_MUL_DOC').IsNull then
    begin
      if QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime < now then
        QRLMulta.Caption := FloatToStrf(QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat,ffNumber,10,2)
      else
        QRLMulta.Caption := '0.00';
    end
    else
      QRLMulta.Caption := '0.00';

    if not QryRelatorio.FieldByName('VR_DES_DOC').IsNull then
    begin
      QRLDesconto.Caption := FloatToStrf(QryRelatorio.FieldByName('VR_DES_DOC').AsFloat,ffNumber,10,2)
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
    if QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime < now then
    begin
      valorDocto := QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat;
      diasAtraso := DaysBetween(trunc(now), QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime);

      if not QryRelatorio.FieldByName('VR_DES_DOC').IsNull then
        valorDocto := valorDocto + (diasAtraso * QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat);

      if not QryRelatorio.FieldByName('VR_MUL_DOC').IsNull then
        valorDocto := valorDocto + QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat;

      if not QryRelatorio.FieldByName('VR_DES_DOC').IsNull then
        valorDocto := valorDocto - QryRelatorio.FieldByName('VR_DES_DOC').AsFloat;
      QRLValor.Caption := FloatToStrF(valorDocto,ffNumber,10,2);
    end
    else
    begin
      if not QryRelatorio.FieldByName('VR_DES_DOC').IsNull then
        QRLValor.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat - QryRelatorio.FieldByName('VR_DES_DOC').AsFloat,ffNumber,10,2);
    end;
  end;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'Q' then
  begin
      QRLValor.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAG').AsFloat,ffNumber,10,2);
  end;

  if QryRelatorio.FieldByName('IC_SIT_DOC').AsString = 'C' then
  begin
      QRLValor.Caption := '0.00';
  end;

  //  Zebrar o relatório
  if DetailBand1.Color = $00F2F2F2 then
  begin
     DetailBand1.Color := clWhite;
  end
  else
  begin
     DetailBand1.Color := $00F2F2F2;
  end;

  // Somar valores
  VlTotDocTur := VlTotDocTur + QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat;
  if (QRLJuros.Caption <> '0,00') and (QRLJuros.Caption <> '0.00') and (QRLJuros.Caption <> '') then
    VlTotJurTur := VlTotJurTur + StrToFloat(StringReplace(QRLJuros.Caption,'.','', [rfReplaceAll]));
  if (QRLMulta.Caption <> '0,00') and (QRLMulta.Caption <> '0.00') and (QRLMulta.Caption <> '') then
    VlTotMulTur := VlTotMulTur + StrToFloat(StringReplace(QRLMulta.Caption,'.','', [rfReplaceAll]));
  if (QRLDesconto.Caption <> '0,00') and (QRLDesconto.Caption <> '0.00') and (QRLDesconto.Caption <> '') then
    VlTotDescTur := VlTotDescTur + StrToFloat(StringReplace(QRLDesconto.Caption,'.','', [rfReplaceAll]));
  if (QRLValor.Caption <> '0,00') and (QRLValor.Caption <> '0.00') and (QRLValor.Caption <> '') then
    VlTotPagTur := VlTotPagTur + StrToFloat(StringReplace(QRLValor.Caption,'.','', [rfReplaceAll]));
end;

procedure TFrmRelResumoContasReceber.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRL_VlDocG.Caption := FormatFloat('###,##0.00', VlTotDocTur);
  QRL_JurosG.Caption := FormatFloat('###,##0.00', VlTotJurTur);
  QRL_MultaG.Caption := FormatFloat('###,##0.00', VlTotMulTur);
  QRL_DescG.Caption := FormatFloat('###,##0.00', VlTotDescTur);
  QRL_VlPagoG.Caption := FormatFloat('###,##0.00', VlTotPagTur);

  VlTotDocG := 0;
  VlTotDocTotG := 0;
  VlTotJurG := 0;
  VlTotMulG := 0;
  VlTotDescG := 0;
  VlTotPagG := 0;

end;

procedure TFrmRelResumoContasReceber.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  VlTotDocG      := 0;
  VlTotDocTotG   := 0;
  VlTotJurG      := 0;
  VlTotMulG      := 0;
  VlTotDescG     := 0;
  VlTotPagG      := 0;

  VlTotDocTur    := 0;
  VlTotDocTotTur := 0;
  VlTotJurTur    := 0;
  VlTotMulTur    := 0;
  VlTotDescTur   := 0;
  VlTotPagTur    := 0;

  VlTotDocCur    := 0;
  VlTotDocTotCur := 0;
  VlTotJurCur    := 0;
  VlTotMulCur    := 0;
  VlTotDescCur   := 0;
  VlTotPagCur    := 0;

  QRLJuros.Caption := '0.00';
  QRLMulta.Caption := '0.00';
  QRLDesconto.Caption := '0.00';
  QRLValor.Caption := '0.00';

end;

procedure TFrmRelResumoContasReceber.QRBand3BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRL_VlDocTur.Caption := FormatFloat('###,##0.00', VlTotDocTur);
  QRL_JurosTur.Caption := FormatFloat('###,##0.00', VlTotJurTur);
  QRL_MultaTur.Caption := FormatFloat('###,##0.00', VlTotMulTur);
  QRL_DescTur.Caption := FormatFloat('###,##0.00', VlTotDescTur);
  QRL_VlPagoTur.Caption := FormatFloat('###,##0.00', VlTotPagTur);

  VlTotDocG    := VlTotDocG    + VlTotDocTur;
  VlTotJurG    := VlTotJurG    + VlTotJurTur;
  VlTotMulG    := VlTotMulG    + VlTotMulTur;
  VlTotDescG   := VlTotDescG   + VlTotDescTur;
  VlTotPagG    := VlTotPagG    + VlTotPagTur;

  VlTotDocTur    := 0;
  VlTotDocTotTur := 0;
  VlTotJurTur    := 0;
  VlTotMulTur    := 0;
  VlTotDescTur   := 0;
  VlTotPagTur    := 0;
end;

procedure TFrmRelResumoContasReceber.QRDBText3Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := QryRelatorio.FieldByName('CO_ALU').AsString + ' - ' + Value;
end;

procedure TFrmRelResumoContasReceber.QRDBText8Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := Value + '/' + QryRelatorio.FieldByName('NU_SEM_LET').AsString;
end;

procedure TFrmRelResumoContasReceber.QRDBText17Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := Copy(Value,1,1) + 'º';
end;

procedure TFrmRelResumoContasReceber.QRDBText4Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := Value + '/' + QryRelatorio.FieldByName('NU_PAR').AsString;
end;

procedure TFrmRelResumoContasReceber.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLJuros.Caption := '0.00';
  QRLMulta.Caption := '0.00';
  QRLDesconto.Caption := '0.00';
  QRLValor.Caption := '0.00';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelResumoContasReceber]);

end.
