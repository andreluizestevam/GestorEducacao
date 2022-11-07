unit U_RelInadimplenciaSerTur;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, DateUtils;

type
  TFrmRelInadimplenciaSerTur = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLParametros: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRLLegenda: TQRLabel;
    QRLTotPar: TQRLabel;
    QrlTotVlTit: TQRLabel;
    QrlTotMul: TQRLabel;
    QrlTotVlJur: TQRLabel;
    QrlTotVlLiq: TQRLabel;
    QRLabel11: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel19: TQRLabel;
    QRShape1: TQRShape;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QRLVlLiquido: TQRLabel;
    QRLVlJuros: TQRLabel;
    QRLVlMulta: TQRLabel;
    QRLVlTitulo: TQRLabel;
    QRDBText3: TQRDBText;
    QRLDias: TQRLabel;
    QRLDtVencimento: TQRLabel;
    QRLSerTur: TQRLabel;
    QRLNuNis: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel1: TQRLabel;
    QRLVlDesc: TQRLabel;
    QrlTotVlDes: TQRLabel;
    QRLCPFResp: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    totLiq,totJur,totMul,totTit,totDesc: Double;
  public
    { Public declarations }
    strDataIni, strDataFim : String;
  end;

var
  FrmRelInadimplenciaSerTur: TFrmRelInadimplenciaSerTur;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelInadimplenciaSerTur.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  vlLiq : Double;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';

  if not QryRelatorio.FieldByName('NU_CPF_RESP').IsNull then
    QRLCPFResp.Caption := FormatMaskText('000\.000\.000\-00;0',QryRelatorio.FieldByName('NU_CPF_RESP').AsString)
  else
    QRLCPFResp.Caption := '-';

  QRLNuNis.Caption := QryRelatorio.FieldByName('nu_nis').AsString;

  //Data Vencimento
  if not QryRelatorio.FieldByName('DT_VEN_DOC').IsNull then
    QRLDtVencimento.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime)
  else
    QRLDtVencimento.Caption := '-';

  //Dias de atraso
  QrlDias.Caption := IntToStr(DaysBetween(QryRelatorio.FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())));

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

  //R$DESCTO
  if not QryRelatorio.FieldByName('VR_DES_DOC').IsNull then
  begin
    if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_DES').AsString = 'V' then
    begin
      QRLVlDesc.Caption := FloatToStrF(QryRelatorio.FieldByName('VR_DES_DOC').AsFloat,ffNumber,15,2);
      totDesc := totDesc + QryRelatorio.FieldByName('VR_DES_DOC').AsFloat;
    end
    else
    begin
      QRLVlDesc.Caption := FloatToStrF((QryRelatorio.FieldByName('VR_DES_DOC').AsFloat * QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat)/100,ffNumber,15,2);
      totDesc := totDesc + ((QryRelatorio.FieldByName('VR_DES_DOC').AsFloat * QryRelatorio.FieldByName('VR_PAR_DOC').AsFloat)/100);
    end;
  end
  else
    QRLVlDesc.Caption := '0.00';

  vlLiq := 0;
  //R$LIQUIDO
  if not QryRelatorio.FieldByName('VR_PAR_DOC').isnull then
  begin
    QRLVlLiquido.caption := FloatToStrF(QryRelatorio.FieldByName('VR_PAR_DOC').asFloat,ffNumber,15,2);

    if not QryRelatorio.FieldByName('VR_MUL_DOC').isnull then
    begin
      if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_MUL').AsString = 'V' then
      begin
        vlLiq := vlLiq + QryRelatorio.FieldByName('VR_MUL_DOC').asFloat;
      end
      else
      begin
        vlLiq := vlLiq + ((QryRelatorio.FieldByName('VR_PAR_DOC').asFloat * QryRelatorio.FieldByName('VR_MUL_DOC').asFloat)/100);
      end;
    end;
    if not QryRelatorio.FieldByName('VR_JUR_DOC').isnull then
    begin
      if QryRelatorio.FieldByName('CO_FLAG_TP_VALOR_JUR').AsString = 'V' then
      begin
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

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  //Numero de parcelas
  QRLTotPar.Caption := IntToStr(StrToInt(QRLTotPar.Caption) + 1);

end;

procedure TFrmRelInadimplenciaSerTur.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  qtdeAlunos : String;
  qtdTotCAR : Double;
begin
  inherited;
  QrlTotVlTit.Caption := FloatToStrF(totTit,ffnumber,15,2);
  QrlTotVlLiq.Caption := FloatToStrF(totLiq,ffnumber,15,2);
  QrlTotMul.Caption := FloatToStrF(totMul,ffnumber,15,2);
  QrlTotVlJur.Caption := FloatToStrF(totJur,ffnumber,15,2);
  QrlTotVlDes.Caption := FloatToStrF(totDesc,ffnumber,15,2);

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select count(distinct co_alu) as totalAlunos from tb47_cta_receb '+
                'where co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                '  AND  DT_VEN_DOC <= ' + quotedStr(strDataFim) +
                '  AND  DT_VEN_DOC >= ' + quotedStr(strDataIni) +
                '  AND IC_SIT_DOC = ' + quotedStr('A');
    Open;

    if not IsEmpty then
    begin
      qtdeAlunos := FieldByName('totalAlunos').AsString;
    end
    else
      qtdeAlunos := '0';
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select SUM(VR_PAR_DOC) as totalCAR from tb47_cta_receb '+
                'where co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                //'  AND  DT_VEN_DOC <= ' + quotedStr(strDataFim) +
                //'  AND  DT_VEN_DOC >= ' + quotedStr(strDataIni) +
                '  AND IC_SIT_DOC in (' + quotedStr('A') + ',' + QuotedStr('Q') + ')';
    Open;

    if not IsEmpty then
    begin
      qtdTotCAR := FieldByName('totalCAR').AsFloat;
    end
    else
      qtdTotCAR := 0;
  end;

  QRLLegenda.Caption := '(Tot de Alunos: ' + qtdeAlunos + ' - Inadimplência Média: R$';

  if qtdeAlunos = '0' then
    QRLLegenda.Caption := QRLLegenda.Caption + '0,00'
  else
    QRLLegenda.Caption := QRLLegenda.Caption + FloatToStrF((totTit)/StrToInt(qtdeAlunos),ffNumber,15,2) + ' )';

  QRLLegenda.Caption := QRLLegenda.Caption + ' (Tot CAR Período: R$ ' + FloatToStrF(qtdTotCAR,ffNumber,15,2);

  if qtdTotCAR > 0 then
    QRLLegenda.Caption := QRLLegenda.Caption + ' - Inadimplência: ' + FloatToStrF(totTit*100/qtdTotCAR,ffNumber,10,2) + '%)'
  else
    QRLLegenda.Caption := QRLLegenda.Caption + ' - Inadimplência: 0,0%)'

end;

procedure TFrmRelInadimplenciaSerTur.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotPar.Caption := '0';
  QrlTotVlTit.Caption := '0';
  QrlTotVlLiq.Caption := '0';
  QrlTotMul.Caption := '0';
  QrlTotVlJur.Caption := '0';
  QrlTotVlDes.Caption := '0';
  totLiq := 0;
  totJur := 0;
  totTit := 0;
  totMul := 0;
  totDesc := 0;
end;

end.
