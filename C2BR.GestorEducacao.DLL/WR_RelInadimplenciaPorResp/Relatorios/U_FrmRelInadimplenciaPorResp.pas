unit U_FrmRelInadimplenciaPorResp;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelInadimplenciaPorResp = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    SummaryBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRLLegenda: TQRLabel;
    QrlTotVlTit: TQRLabel;
    QrlTotMul: TQRLabel;
    QrlTotVlJur: TQRLabel;
    QrlTotVlLiq: TQRLabel;
    QRLParametros: TQRLabel;
    QRLabel11: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel8: TQRLabel;
    QRShape1: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRLTelRes: TQRLabel;
    QRLQTA: TQRLabel;
    QRLQTT: TQRLabel;
    QRLDtVencimento: TQRLabel;
    QRLDias: TQRLabel;
    QRLVlTitulo: TQRLabel;
    QRLVlMulta: TQRLabel;
    QRLVlJuros: TQRLabel;
    QRLVlLiquido: TQRLabel;
    QRLTelCel: TQRLabel;
    QryRelatorioNO_RESP: TStringField;
    QryRelatorioCO_RESP: TAutoIncField;
    QryRelatorioNU_CPF_RESP: TStringField;
    QryRelatorioNU_TELE_RESI_RESP: TStringField;
    QryRelatorioNU_TELE_CELU_RESP: TStringField;
    QRLVlDes: TQRLabel;
    QrlTotVlDes: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    totLiq,totJur,totMul,totTit,totDes: Double;
    qtdAlunos,qtdTitulos : Integer;
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelInadimplenciaPorResp: TFrmRelInadimplenciaPorResp;

implementation

uses U_DataModuleSGE,MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelInadimplenciaPorResp.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  vlLiq : Double;
  i : Integer;
  totTitPar,totMulPar,totJurPar,totLiqPar,totDesPar : Double;
begin
  inherited;
  totTitPar := 0;
  totMulPar := 0;
  totJurPar := 0;
  totLiqPar := 0;
  totDesPar := 0;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=  'select Count (distinct c.CO_ALU) as total' +
                 ' FROM TB47_CTA_RECEB C'+
                 ' join tb07_aluno a on c.co_alu = a.co_alu and c.co_emp = a.co_emp'+
                 ' join tb08_matrcur mm on c.co_alu = mm.co_alu and c.co_emp = mm.co_emp'+
                 ' join tb108_responsavel r on a.co_resp = r.co_resp ' +
                 ' WHERE C.IC_SIT_DOC = ' + '''' + 'A' + '''' +
                 '  AND  DT_VEN_DOC < GETDATE() ' +
                 '  AND  A.CO_RESP = ' + QryRelatorioCO_RESP.AsString +
                 '  AND  C.CO_EMP = ' + codigoEmpresa +
                 '  AND MM.CO_SIT_MAT = ' + quotedStr('A');
    Open;

    if not IsEmpty then
    begin
      QRLQTA.Caption := FieldByName('total').AsString;
      qtdAlunos := qtdAlunos + FieldByName('total').AsInteger;
    end
    else
      QRLQTA.Caption := '-';
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=  'select Count (distinct C.NU_DOC) as total' +
                 ' FROM TB47_CTA_RECEB C'+
                 ' join tb07_aluno a on c.co_alu = a.co_alu and c.co_emp = a.co_emp'+
                 ' join tb108_responsavel r on a.co_resp = r.co_resp' +
                 ' WHERE C.IC_SIT_DOC = ' + '''' + 'A' + '''' +
                 '  AND  DT_VEN_DOC < GETDATE() ' +
                 '  AND  A.CO_RESP = ' + QryRelatorioCO_RESP.AsString +
                 '  AND  C.CO_EMP = ' + codigoEmpresa;
    Open;

    if not IsEmpty then
    begin
      QRLQTT.Caption := FieldByName('total').AsString;
      qtdTitulos := qtdTitulos + FieldByName('total').AsInteger;
    end
    else
      QRLQTT.Caption := '-';
  end;

  //Celular
  if not QryRelatorioNU_TELE_CELU_RESP.IsNull then
    QRLTelCel.Caption := FormatMaskText('(99) 9999-9999;0;_',QryRelatorioNU_TELE_CELU_RESP.AsString)
  else
    QRLTelCel.Caption := '-';

  //Residencial
  if not QryRelatorioNU_TELE_RESI_RESP.IsNull then
    QRLTelRes.Caption := FormatMaskText('(99) 9999-9999;0;_',QryRelatorioNU_TELE_RESI_RESP.AsString)
  else
    QRLTelRes.Caption := '-';

  //Retornar os somat�rios
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=  'select C.* ' +
                 ' FROM TB47_CTA_RECEB C'+
                 ' join tb07_aluno a on c.co_alu = a.co_alu and c.co_emp = a.co_emp'+
                 ' join tb108_responsavel r on a.co_resp = r.co_resp' +
                 ' WHERE C.IC_SIT_DOC = ' + '''' + 'A' + '''' +
                 '  AND  DT_VEN_DOC < GETDATE() ' +
                 '  AND  A.CO_RESP = ' + QryRelatorioCO_RESP.AsString +
                 '  AND  C.CO_EMP = ' + codigoEmpresa +
                 ' ORDER BY C.DT_VEN_DOC ';
    Open;

    i := 0;
    while not Eof do
    begin
      if i = 0 then
      begin
        //Data Vencimento
        if not FieldByName('DT_VEN_DOC').IsNull then
        begin
          QRLDtVencimento.Caption := FormatDateTime('dd/MM/yy',FieldByName('DT_VEN_DOC').AsDateTime);
          //Dias de atraso
          QrlDias.Caption := IntToStr(DaysBetween(FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())));
        end
        else
          QRLDtVencimento.Caption := '-';
        i := 1;
      end;

      //Somat�rio R$TITULO
      if not FieldByName('VR_PAR_DOC').IsNull then
      begin
        totTitPar := totTitPar + FieldByName('VR_PAR_DOC').AsFloat;
        totTit := totTit + FieldByName('VR_PAR_DOC').AsFloat;
      end;

      //Somat�rio R$MULTA
      if not FieldByName('VR_MUL_DOC').IsNull then
      begin
        if FieldByName('CO_FLAG_TP_VALOR_MUL').AsString = 'V' then
        begin
          totMulPar := totMulPar + FieldByName('VR_MUL_DOC').AsFloat;
          totMul := totMul + FieldByName('VR_MUL_DOC').AsFloat;
        end
        else
        begin
          totMulPar := totMulPar + ((FieldByName('VR_PAR_DOC').AsFloat * FieldByName('VR_MUL_DOC').AsFloat)/100);
          totMul := totMul + ((FieldByName('VR_PAR_DOC').AsFloat * FieldByName('VR_MUL_DOC').AsFloat)/100);
        end;
      end;

      if not FieldByName('VR_DES_DOC').IsNull then
      begin
        if FieldByName('CO_FLAG_TP_VALOR_DES').AsString = 'V' then
        begin
          totDesPar := totDesPar + FieldByName('VR_DES_DOC').AsFloat;
          totDes := totDes + FieldByName('VR_DES_DOC').AsFloat;
        end
        else
        begin
          totDesPar := totDesPar + ((FieldByName('VR_PAR_DOC').AsFloat * FieldByName('VR_DES_DOC').AsFloat)/100);
          totDes := totDes + ((FieldByName('VR_PAR_DOC').AsFloat * FieldByName('VR_DES_DOC').AsFloat)/100);
        end;
      end;

      //Somat�rio R$JUROS
      if not FieldByName('VR_JUR_DOC').IsNull then
      begin
        if FieldByName('CO_FLAG_TP_VALOR_JUR').AsString = 'V' then
        begin
          totJurPar := totJurPar + (DaysBetween(FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*(FieldByName('VR_JUR_DOC').AsFloat);
        end
        else
        begin
          totJurPar := totJurPar + (DaysBetween(FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*((FieldByName('VR_PAR_DOC').AsFloat * FieldByName('VR_JUR_DOC').AsFloat)/100);
        end;
      end;

      vlLiq := 0;
      //Somat�rio R$LIQUIDO
      if not FieldByName('VR_PAR_DOC').isnull then
      begin
        if not FieldByName('VR_MUL_DOC').isnull then
        begin
          if FieldByName('CO_FLAG_TP_VALOR_MUL').AsString = 'V' then
          begin
            vlLiq := vlLiq + FieldByName('VR_MUL_DOC').asFloat;
          end
          else
          begin
            vlLiq := vlLiq + ((FieldByName('VR_PAR_DOC').asFloat * FieldByName('VR_MUL_DOC').asFloat)/100);
          end;
        end;
        if not FieldByName('VR_JUR_DOC').isnull then
        begin
          if FieldByName('CO_FLAG_TP_VALOR_JUR').AsString = 'V' then
          begin
            vlLiq := vlLiq + ((DaysBetween(FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*(FieldByName('VR_JUR_DOC').AsFloat));
          end
          else
          begin
            vlLiq := vlLiq + ((DaysBetween(FieldByName('DT_VEN_DOC').AsDateTime, Trunc(Now())))*((FieldByName('VR_PAR_DOC').asFloat * FieldByName('VR_JUR_DOC').asFloat)/100));
          end;
        end;
        if not FieldByName('VR_DES_DOC').isnull then
        begin
          if FieldByName('CO_FLAG_TP_VALOR_DES').AsString = 'P' then
          begin
            vlLiq := vlLiq - ((FieldByName('VR_PAR_DOC').AsFloat * FieldByName('VR_DES_DOC').AsFloat)/100);
          end
          else
          begin
            vlLiq := vlLiq - FieldByName('VR_DES_DOC').AsFloat;
          end;
        end;
        
        vlLiq := vlLiq + FieldByName('VR_PAR_DOC').AsFloat;
        totLiqPar := totLiqPar + vlLiq;
      end;

      Next;
    end;

  end;

  //Escrever R$TITULO
  if totTitPar > 0 then
  begin
    QRLVlTitulo.Caption := FloatToStrF(totTitPar,ffNumber,15,2);
  end
  else
    QRLVlTitulo.Caption := '-';

  //Escrever R$MULTA
  if totMulPar > 0 then
  begin
    QRLVlMulta.Caption := FloatToStrF(totMulPar,ffNumber,15,2);
  end
  else
    QRLVlMulta.Caption := '-';

  //Escrever R$JUROS
  if totJurPar > 0 then
  begin
    QRLVlJuros.Caption := FloatToStrF(totJurPar,ffNumber,15,2);
    totJur := totJur + totJurPar;
  end
  else
    QRLVlJuros.Caption := '-';

  //Escrever R$DESCTO
  if totDesPar > 0 then
  begin
    QRLVlDes.Caption := FloatToStrF(totDesPar,ffNumber,15,2);
  end
  else
    QRLVlDes.Caption := '-';

  //Escrever R$LIQUIDO
  if totLiqPar > 0 then
  begin
    QRLVlLiquido.caption := FloatToStrF(totLiqPar,ffNumber,15,2);
    totLiq := totLiq + totLiqPar;
  end
  else
  begin
    QRLVlLiquido.caption := '-';
  end;

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

end;

procedure TFrmRelInadimplenciaPorResp.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  vlTotCAR : Double;
begin
  inherited;
  QrlTotVlTit.Caption := FloatToStrF(totTit,ffnumber,15,2);
  QrlTotVlLiq.Caption := FloatToStrF(totLiq,ffnumber,15,2);
  QrlTotMul.Caption := FloatToStrF(totMul,ffnumber,15,2);
  QrlTotVlJur.Caption := FloatToStrF(totJur,ffnumber,15,2);
  QrlTotVlDes.Caption := FloatToStrF(totDes,ffnumber,15,2);

  QRLLegenda.Caption := '(Quantidade = Resp: ' + IntToStr(QryRelatorio.RecordCount) + ' - Alunos: ' +
  IntToStr(qtdAlunos) +' - T�tulos: '+ IntToStr(qtdTitulos) + ' )  (T�tulos = Valor M�dio: R$';

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select SUM(VR_PAR_DOC) as totalCAR from tb47_cta_receb '+
                'where co_emp = ' + codigoEmpresa +
                '  AND IC_SIT_DOC in (' + quotedStr('A') + ',' + QuotedStr('Q') + ')';
    Open;

    if not IsEmpty then
    begin
      vlTotCAR := FieldByName('totalCAR').AsFloat;
    end
    else
      vlTotCAR := 0;
  end;

  if qtdTitulos > 0 then
    QRLLegenda.Caption := QRLLegenda.Caption + FloatToStrF(totTit/qtdTitulos,ffNumber,15,2) + ')  (% Inadimp: '
  else
    QRLLegenda.Caption := QRLLegenda.Caption + '0,00)  (% Inadimp: ';

  if vlTotCAR > 0 then
    QRLLegenda.Caption := QRLLegenda.Caption + FloatToStrF((totTit*100)/vlTotCAR,ffNumber,15,2) + ')'
  else
    QRLLegenda.Caption := QRLLegenda.Caption + '0,0)';

end;

procedure TFrmRelInadimplenciaPorResp.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotVlTit.Caption := '0';
  QrlTotVlLiq.Caption := '0';
  QrlTotMul.Caption := '0';
  QrlTotVlJur.Caption := '0';
  QrlTotVlDes.Caption := '0';
  totLiq := 0;
  totJur := 0;
  totTit := 0;
  totMul := 0;
  totDes := 0;
  qtdAlunos := 0;
  qtdTitulos := 0;
end;

end.