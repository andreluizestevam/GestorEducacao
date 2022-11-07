unit U_FrmRelObrasEmAtraso;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, DateUtil;

type
  TFrmRelObrasEmAtraso = class(TFrmRelTemplate)
    QRLabel5: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QrlParametros: TQRLabel;
    QRBand1: TQRBand;
    QRDBText2: TQRDBText;
    QRDBText1: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText7: TQRDBText;
    QRLabel4: TQRLabel;
    QrlDiasAtraso: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel6: TQRLabel;
    qrlTelefoneResi: TQRLabel;
    QRShape12: TQRShape;
    SummaryBand1: TQRBand;
    QRDBText4: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText9: TQRDBText;
    qrlTelefoneCelu: TQRLabel;
    QRLMulta: TQRLabel;
    QRLNumEmp: TQRLabel;
    QRLDescTot: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    totMulta : Double;
    totDias : integer;
  public
    { Public declarations }
  end;

var
  FrmRelObrasEmAtraso: TFrmRelObrasEmAtraso;

implementation

uses U_DataModuleSGE, U_Funcoes, MaskUtils;

{$R *.dfm}

procedure TFrmRelObrasEmAtraso.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;

  QRLNumEmp.Caption := FormatFloat('0000',QryRelatorio.FieldByName('CO_EMP').AsFloat) + '-' +
  FormatDateTime('yy', QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').asDateTime) + '/' + FormatDateTime('MM', QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').asDateTime) +
  FormatFloat('000000',QryRelatorio.FieldByName('CO_NUM_EMP').AsFloat);
  
  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;
    
  { Recupera a descrição do calendário }
  with DM.QrySql do
  begin
    { Grava a cabeça do calendário }
    Close;
    Sql.Clear;
    if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'A' then
    begin
      Sql.text := 'Select A.NU_TELE_RESI_ALU as teleResi, A.NU_TELE_CELU_ALU as teleCelu ' +
      ' From TB07_ALUNO A ' +
      ' JOIN TB205_USUARIO_BIBLIOT UB on UB.CO_ALU = A.CO_ALU and UB.CO_EMP_ALU = A.CO_EMP ' +
      ' Where UB.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and UB.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString;
    end
    else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'O' then
    begin
      Sql.text := 'Select A.NU_TELE_RESI_USU_BIB as teleResi, A.NU_TELE_CELU_USU_BIB as teleCelu ' +
      ' From TB205_USUARIO_BIBLIOT A ' +
      ' Where A.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and A.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString;
    end
    else
    begin
      Sql.text := 'Select A.NU_TELE_RESI_COL as teleResi, A.NU_TELE_CELU_COL as teleCelu ' +
      ' From TB03_COLABOR A ' +
      ' JOIN TB205_USUARIO_BIBLIOT UB on UB.CO_COL = A.CO_COL and UB.CO_EMP_COL = A.CO_EMP ' +
      ' Where UB.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and UB.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString;
    end;
    Open;

    if not IsEmpty then
    begin
      if (not FieldByName('teleResi').IsNull) and (FieldByName('teleResi').AsString <> '') then
        qrlTelefoneResi.Caption := FormatMaskText('(00) 0000-0000;0',FieldByName('teleResi').AsString)
      else
        qrlTelefoneResi.Caption := '-';

      if (not FieldByName('teleCelu').IsNull) and (FieldByName('teleCelu').AsString <> '')  then
        qrlTelefoneCelu.Caption := FormatMaskText('(00) 0000-0000;0',FieldByName('teleCelu').AsString)
      else
        qrlTelefoneCelu.Caption := '-';
    end;
  end;

  QrlDiasAtraso.Caption := '';
  if (not QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').IsNull) and (QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsString <> '') then
  begin
    QrlDiasAtraso.Caption := '+ '+ IntToStr(DaysInPeriod(QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsDateTime,now) - 1);
    totDias := totDias + (DaysInPeriod(QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsDateTime,now) - 1);

    if not QryRelatorio.FieldByName('VL_MULT_DIA_ATRASO').IsNull then
    begin
        QRLMulta.Caption := FormatFloat('###,##0.00',QryRelatorio.FieldByName('VL_MULT_DIA_ATRASO').AsFloat *
                (DaysInPeriod(QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsDateTime,now) - 1));
        totMulta := totMulta + (QryRelatorio.FieldByName('VL_MULT_DIA_ATRASO').AsFloat *
                (DaysInPeriod(QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsDateTime,now) - 1));
    end
    else
      QRLMulta.Caption := '-';
  end
  else
  begin
    QrlDiasAtraso.Caption := '-';
    QRLMulta.Caption := '-';
  end;
end;

procedure TFrmRelObrasEmAtraso.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLDescTot.Caption := 'Total itens: ' + IntToStr(QryRelatorio.RecordCount) + ' - Atraso médio (em dias): +' + FloatToStrF(totDias/QryRelatorio.RecordCount,ffNumber,10,2) +
  ' - Valor Total de Multa: R$ ' + FloatToStrF(totMulta,ffNumber,15,2);
end;

procedure TFrmRelObrasEmAtraso.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  totMulta := 0;
  totDias := 0;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelObrasEmAtraso]);

end.
