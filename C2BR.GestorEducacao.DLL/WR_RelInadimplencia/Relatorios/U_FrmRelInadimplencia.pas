unit U_FrmRelInadimplencia;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, DateUtils;

type
  TFrmRelInadimplencia = class(TFrmRelTemplate)
    QrlTitCurso: TQRLabel;
    QRGroup1: TQRGroup;
    QRBand3: TQRBand;
    QRShape20: TQRShape;
    QrlTotVlLiq: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel11: TQRLabel;
    QRLPage: TQRLabel;
    QRShape5: TQRShape;
    QRLAluno: TQRLabel;
    DetailBand1: TQRBand;
    QRLDtVencimento: TQRLabel;
    QRLDias: TQRLabel;
    QRDBText1: TQRDBText;
    QRLDtEmissao: TQRLabel;
    QRLVlTitulo: TQRLabel;
    QRLVlMulta: TQRLabel;
    QRLVlJuros: TQRLabel;
    QRLVlLiquido: TQRLabel;
    QRDBText3: TQRDBText;
    QRLResp: TQRLabel;
    QRLTelefones: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel1: TQRLabel;
    QrlTotVlJur: TQRLabel;
    QrlTotMul: TQRLabel;
    QrlTotVlTit: TQRLabel;
    QrlTotGerVlTit: TQRLabel;
    QrlTotGerMul: TQRLabel;
    QrlTotGerVlJur: TQRLabel;
    QrlTotGerVlLiq: TQRLabel;
    QRLTotPar: TQRLabel;
    QRLTotGerPar: TQRLabel;
    QRLabel4: TQRLabel;
    QRShape3: TQRShape;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRLDesconto: TQRLabel;
    QRLabel3: TQRLabel;
    QrlTotVlDes: TQRLabel;
    QrlTotGerVlDes: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand3AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    totLiq,totJur,totMul,totTit,totDes,totGerLiq,totGerJur,totGerMul,totGerTit,totGerDes : Double;
  public
    { Public declarations }
  end;

var
  FrmRelInadimplencia: TFrmRelInadimplencia;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelInadimplencia.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  vlLiq : Double;
begin
  inherited;
  //Data Vencimento
  if not QryRelatorio.FieldByName('DT_VEN_DOC').IsNull then
    QRLDtVencimento.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime)
  else
    QRLDtVencimento.Caption := '-';

  //Dias de atraso
  QrlDias.Caption := IntToStr(DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())));

  //Data Emissão
  if not QryRelatorio.FieldByName('DT_CAD_DOC').IsNull then
    QRLDtEmissao.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_CAD_DOC').AsDateTime)
  else
    QRLDtEmissao.Caption := '-';

  //R$TITULO
  if not QryRelatorio.FieldByName('VR_PAR_DOC').IsNull then
  begin
    QRLVlTitulo.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat,ffNumber,15,2);
    totTit := totTit + QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat;
  end
  else
    QRLVlTitulo.Caption := '-';


  //R$MULTA
  if not QryRelatorio.FieldByName('VR_MUL_DOC').IsNull then
  begin
    if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_MUL').AsString = 'V' then
    begin
      QRLVlMulta.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat,ffNumber,15,2);
      totMul := totMul + QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat;
    end
    else
    begin
      QRLVlMulta.Caption := FloatToStrF((QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat * QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat)/100,ffNumber,15,2);
      totMul := totMul + ((QryRelatorio.FieldByName('VR_MUL_DOC').AsFloat * QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat)/100);
    end;
  end
  else
    QRLVlMulta.Caption := '-';

  //R$JUROS
  if not QryRelatorio.FieldByName('VR_JUR_DOC').IsNull then
  begin
    if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_JUR').AsString = 'V' then
    begin
      QRLVlJuros.Caption := FloatToStrF(DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())) * QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat,ffNumber,15,2);
      totJur := totJur + (DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now()))*QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat);
    end
    else
    begin
      QRLVlJuros.Caption := FloatToStrF((DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*((QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat * QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat)/100),ffNumber,15,2);
      totJur := totJur + ((DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*((QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat * QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat)/100));
    end;
  end
  else
    QRLVlJuros.Caption := '-';

  //R$DESCONTO
  if not QryRelatorio.FieldByName('VR_DES_DOC').IsNull then
  begin
    if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_DES').AsString = 'V' then
    begin
      QRLDesconto.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_DES_DOC').AsFloat,ffNumber,15,2);
      totDes := totDes + QryRelatorio.FieldByName('VR_DES_DOC').AsFloat;
    end
    else
    begin
      QRLDesconto.Caption := FloatToStrF((QryRelatorio.FieldByName('VR_DES_DOC').AsFloat * QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat)/100,ffNumber,15,2);
      totDes := totDes + ((QryRelatorio.FieldByName('VR_DES_DOC').AsFloat * QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat)/100);
    end;
  end
  else
  begin
    QRLDesconto.Caption := '-';
  end;

  //R$LIQUIDO
  vlLiq := 0;
  if not QryRelatorio.FieldByName('VR_PAR_DOC').isnull then
  begin
    QRLVlLiquido.caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat, ffNumber, 10,2);
    if not QryRelatorio.FieldByName('VR_MUL_DOC').isnull then
    begin
      if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_MUL').AsString = 'V' then
      begin
        //QRLVlLiquido.caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').asFloat + QryRelatorio.FieldByName('VR_MUL_DOC').asFloat,ffNumber,15,2);
        vlLiq := vlLiq + QryRelatorio.FieldByName('VR_MUL_DOC').asFloat;
      end
      else
      begin
        //QRLVlLiquido.caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').asFloat + ((QryRelatorio.FieldByName('VR_PAR_DOC').asFloat * QryRelatorio.FieldByName('VR_MUL_DOC').asFloat)/100,ffNumber,15,2);
        vlLiq := vlLiq + ((QryRelatorio.FieldByName('VR_PAR_DOC').asFloat * QryRelatorio.FieldByName('VR_MUL_DOC').asFloat)/100);
      end;
    end;
    if not QryRelatorio.FieldByName('VR_JUR_DOC').isnull then
    begin
      if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_JUR').AsString = 'V' then
      begin
        //QRLVlLiquido.caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').asFloat + QryRelatorio.FieldByName('VR_MUL_DOC').asFloat+((DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*(QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat)),ffNumber,15,2);
        vlLiq := vlLiq + ((DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*(QryRelatorio.FieldByName('VR_JUR_DOC').AsFloat));
      end
      else
      begin
        vlLiq := vlLiq + ((DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*((QryRelatorio.FieldByName('VR_PAR_DOC').asFloat * QryRelatorio.FieldByName('VR_JUR_DOC').asFloat)/100));
      end;
    end;
    if not QryRelatorio.FieldByName('VR_DES_DOC').isnull then
    begin
      if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_DES').AsString = 'P' then
      begin
        vlLiq := vlLiq - ((QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat * QryRelatorio.FieldByName('VR_DES_DOC').AsFloat)/100);
      end
      else
      begin
        vlLiq := vlLiq - QryRelatorio.FieldByName('VR_DES_DOC').AsFloat;
      end;
    end;
    vlLiq := vlLiq + QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat;
    QRLVlLiquido.caption := FloatToStrF(vlLiq,ffNumber,15,2);
    totLiq := totLiq + vlLiq;
  end
  else
  begin
    QRLVlLiquido.caption := '-';
  end;

  //DetailBand1.Color := $00D8D8D8
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00F2F2F2
  else
    DetailBand1.Color := clWhite;

  //Numero de parcelas
  QRLTotPar.Caption := IntToStr(StrToInt(QRLTotPar.Caption) + 1);
end;

procedure TFrmRelInadimplencia.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_RESP').IsNull then
  begin
    QRLResp.Caption := QryRelatorio.FieldByName('NO_RESP').AsString;

    if not QryRelatorio.FieldByName('NU_CPF_RESP').IsNull then
      QRLResp.Caption := QryRelatorio.FieldByName('NO_RESP').AsString + ' ( CPF: ' + FormatMaskText('000.000.000-00;0;_',QryRelatorio.FieldByName('NU_CPF_RESP').AsString) + ' )';
  end
  else
  begin
    QRLResp.Caption := '-';
  end;

  if not QryRelatorio.FieldByName('NU_TELE_RESI_RESP').IsNull then
  begin
    QRLTelefones.Caption := '(Telefones: ' + FormatMaskText('(99) 9999-9999;0;_',QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString) + ' R )';
    if not QryRelatorio.FieldByName('NU_TELE_CELU_RESP').IsNull then
      QRLTelefones.Caption := '(Telefones: ' + FormatMaskText('(99) 9999-9999;0;_',QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString) + ' R / ' +
      FormatMaskText('(99) 9999-9999;0;_',QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString)  +' C )';
    if not QryRelatorio.FieldByName('NU_TELE_COME_RESP').IsNull then
      QRLTelefones.Caption := '(Telefones: ' + FormatMaskText('(99) 9999-9999;0;_',QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString) + ' R / ' +
      FormatMaskText('(99) 9999-9999;0;_',QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString)  +' C / '+ FormatMaskText('(99) 9999-9999;0;_',QryRelatorio.FieldByName('NU_TELE_COME_RESP').AsString) +' T )';
  end
  else
  begin
    QRLTelefones.Caption := '(Telefones:-/-/-)';
  end;
end;

procedure TFrmRelInadimplencia.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
  begin
    QRLAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString);
    if not QryRelatorio.FieldByName('NU_NIS').IsNull then
      QRLAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString) + ' - Nº NIS: ' + QryRelatorio.FieldByName('NU_NIS').AsString;
  end
  else
    QRLAluno.Caption := '-';
end;

procedure TFrmRelInadimplencia.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotGerPar.Caption := '0';
  totGerLiq := 0;
  totGerJur := 0;
  totGerTit := 0;
  totGerMul := 0;
  totGerDes := 0;
  QrlTotVlTit.Caption := '0';
  QrlTotVlLiq.Caption := '0';
  QrlTotMul.Caption := '0';
  QrlTotVlJur.Caption := '0';
  QRLTotPar.Caption := '0';
  QrlTotVlDes.Caption := '0';
  totLiq := 0;
  totJur := 0;
  totTit := 0;
  totMul := 0;
  totDes := 0;
end;

procedure TFrmRelInadimplencia.QRBand3BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  totGerLiq := totGerLiq + totLiq;
  totGerJur := totGerJur + totJur;
  totGerTit := totGerTit + totTit;
  totGerMul := totGerMul + totMul;
  totGerDes := totGerDes + totDes;
  QrlTotVlTit.Caption := FloatToStrF(totTit,ffnumber,15,2);
  QrlTotVlLiq.Caption := FloatToStrF(totLiq,ffnumber,15,2);
  QrlTotMul.Caption := FloatToStrF(totMul,ffnumber,15,2);
  QrlTotVlJur.Caption := FloatToStrF(totJur,ffnumber,15,2);
  QrlTotVlDes.Caption := FloatToStrF(totDes,ffnumber,15,2);
  QRLTotGerPar.Caption := IntToStr(StrToInt(QRLTotGerPar.Caption) + StrToInt(QRLTotPar.Caption));
end;

procedure TFrmRelInadimplencia.QRBand3AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QrlTotVlTit.Caption := '0';
  QrlTotVlLiq.Caption := '0';
  QrlTotMul.Caption := '0';
  QrlTotVlJur.Caption := '0';
  QRLTotPar.Caption := '0';
  QrlTotVlDes.Caption := '0';
  totLiq := 0;
  totJur := 0;
  totTit := 0;
  totMul := 0;
  totDes := 0;
end;

procedure TFrmRelInadimplencia.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotGerVlTit.Caption := FloatToStrF(totGerTit,ffnumber,15,2);
  QrlTotGerVlLiq.Caption := FloatToStrF(totGerLiq,ffnumber,15,2);
  QrlTotGerMul.Caption := FloatToStrF(totGerMul,ffnumber,15,2);
  QrlTotGerVlJur.Caption := FloatToStrF(totGerJur,ffnumber,15,2);
  QrlTotGerVlDes.Caption := FloatToStrF(totGerDes,ffnumber,15,2);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelInadimplencia]);

end.
