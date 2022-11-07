unit U_FrmRelDemonstrativoReserva;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelDemonstrativoReserva = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRLStatusRes: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRShape8: TQRShape;
    QRLabel12: TQRLabel;
    QRLabel17: TQRLabel;
    QRLPage: TQRLabel;
    QRSysData4: TQRSysData;
    QrlTel: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel19: TQRLabel;
    QRLTotal: TQRLabel;
    qrlParametros: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel25: TQRLabel;
    QRLNumero: TQRLabel;
    QRLabel8: TQRLabel;
    QRLIdade: TQRLabel;
    QRLDtNasc: TQRLabel;
    QRLCPFResp: TQRLabel;
    QRLNoResp: TQRLabel;
    QRLSxResp: TQRLabel;
    QRLSxAlu: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelDemonstrativoReserva: TFrmRelDemonstrativoReserva;

implementation

uses U_DataModuleSGE,MaskUtils,DateUtils;

{$R *.dfm}

procedure TFrmRelDemonstrativoReserva.DetailBand1BeforePrint(
  Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  {muda o nome do Caption "Situação" de acordo com o que vem do banco}
  with QryRelatorio do
  begin
    if FieldByName('CO_STATUS').AsString = 'A' then
      QRLStatusRes.Caption := 'Em aberto';
    if FieldByName('CO_STATUS').AsString = 'H' then
      QRLStatusRes.Caption := 'Pré-Matrícula';
    if FieldByName('CO_STATUS').AsString = 'M' then
      QRLStatusRes.Caption := 'Matriculada';
    if FieldByName('CO_STATUS').AsString = 'C' then
      QRLStatusRes.Caption := 'Cancelada';
    if FieldByName('CO_STATUS').AsString = 'E' then
      QRLStatusRes.Caption := 'Efetivada';

    QRLnumero.Caption := FieldByName('NU_RESERVA').AsString;
  end;

  if not QryRelatorio.FieldByName('NU_TELE_RESI_RESP').IsNull then
    QrlTel.Caption := FormatMaskText('(00)0000-0000;0',QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString)
  else
  begin
    if not QryRelatorio.FieldByName('NU_TEL_RESP').IsNull then
      QrlTel.Caption := FormatMaskText('(00)0000-0000;0',QryRelatorio.FieldByName('NU_TEL_RESP').AsString)
    else
      QrlTel.Caption := '-';
  end;

  //IDADE
  if not QryRelatorio.FieldByName('dt_nascimento').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := FormatFloat('00;0',Trunc(DaysBetween(now,QryRelatorio.FieldByName('dt_nascimento').AsDateTime) / diasnoano));
    QRLDtNasc.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('dt_nascimento').AsDateTime);
  end
  else
  begin
    if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
    begin
      diasnoano := 365.6;
      QRLIdade.Caption := FormatFloat('00;0',Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
      QRLDtNasc.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime);
    end
    else
      QRLIdade.Caption := '-';
  end;

  if not QryRelatorio.FieldByName('sexo_alu').IsNull then
    QRLSxAlu.Caption := QryRelatorio.FieldByName('sexo_alu').AsString
  else
    QRLSxAlu.Caption := QryRelatorio.FieldByName('CO_SEXO_ALU').AsString;

  if not QryRelatorio.FieldByName('CO_RESP').IsNull then
  begin
    QRLNoResp.Caption := QryRelatorio.FieldByName('no_responsavel').AsString;
    QRLSxResp.Caption := QryRelatorio.FieldByName('CO_SEXO_RESP').AsString;
    QRLCPFResp.Caption := FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('nu_cpf_responsavel').AsString);
  end
  else
  begin
    QRLNoResp.Caption := QryRelatorio.FieldByName('NO_RESP').AsString;
    QRLSxResp.Caption := '-';
    QRLCPFResp.Caption := FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('NU_CPF_RESP').AsString);
  end;

  if not QryRelatorio.FieldByName('no_aluno').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('no_aluno').AsString)
  else
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('no_alu').AsString);

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;
end;

procedure TFrmRelDemonstrativoReserva.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
