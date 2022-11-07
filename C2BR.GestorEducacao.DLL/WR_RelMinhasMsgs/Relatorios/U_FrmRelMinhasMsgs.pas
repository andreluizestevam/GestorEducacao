unit U_FrmRelMinhasMsgs;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMinhasMsgs = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel5: TQRLabel;
    QRLHrSMS: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel9: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLabel7: TQRLabel;
    QRLReceptor: TQRLabel;
    QRLMsg: TQRLabel;
    QRLDtSMS: TQRLabel;
    QRLabel14: TQRLabel;
    QRLStatus: TQRLabel;
    QRDBText4: TQRDBText;
    QRLTelReceptor: TQRLabel;
    QRLEmissor: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    totalEnv, totalNEnv : integer;
  public
    { Public declarations }
  end;

var
  FrmRelMinhasMsgs: TFrmRelMinhasMsgs;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelMinhasMsgs.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('DT_ENVIO_MENSAG_SMS').IsNull then
    QRLDtSMS.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_ENVIO_MENSAG_SMS').AsDateTime)
  else
    QRLDtSMS.Caption := '-';

  if not QryRelatorio.FieldByName('DT_ENVIO_MENSAG_SMS').IsNull then
    QRLHrSMS.Caption := FormatDateTime('hh:mm:ss',QryRelatorio.FieldByName('DT_ENVIO_MENSAG_SMS').AsDateTime)
  else
    QRLHrSMS.Caption := '-';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  if QryRelatorio.FieldByName('FLA_SMS_SUCESS').AsString = 'S' then
  begin
    QRLStatus.Caption := 'Enviada';
    totalEnv := totalEnv + 1;
  end
  else
  begin
    QRLStatus.Caption := 'Não Enviada';
    totalNEnv := totalNEnv + 1;
  end;

  if not QryRelatorio.FieldByName('DES_MENSAG_SMS').IsNull then
    QRLMsg.Caption := QryRelatorio.FieldByName('DES_MENSAG_SMS').AsString
  else
    QRLMsg.Caption := '-';

  if QryRelatorio.FieldByName('CO_TP_CONTAT_SMS').AsString = 'R' then
  begin
    With DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select no_resp, NU_TELE_CELU_RESP from tb108_responsavel '+
                  'where co_resp = ' + QryRelatorio.FieldByName('CO_RECEPT').AsString;
      Open;

      if not IsEmpty then
      begin
        QRLReceptor.Caption := FieldByName('NO_RESP').AsString + ' - ***';
        QRLTelReceptor.Caption := FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_CELU_RESP').AsString);
      end
      else
      begin
        QRLReceptor.Caption := '*** - ***';
        QRLTelReceptor.Caption := '***';
      end;
    end;
  end
  else if QryRelatorio.FieldByName('CO_TP_CONTAT_SMS').AsString = 'A' then
  begin
    With DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select a.no_alu, a.NU_TELE_CELU_ALU, e.no_fantas_emp from tb07_aluno '+
                  ' join tb25_empresa e on e.co_emp = a.co_emp ' +
                  'where a.co_alu = ' + QryRelatorio.FieldByName('CO_RECEPT').AsString +
                  ' and a.co_emp = ' + QryRelatorio.FieldByName('CO_EMP_RECEPT').AsString;
      Open;

      if not IsEmpty then
      begin
        QRLReceptor.Caption := FieldByName('no_alu').AsString + ' - ' + FieldByName('no_fantas_emp').AsString;
        QRLTelReceptor.Caption := FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_CELU_ALU').AsString);
      end
      else
      begin
        QRLReceptor.Caption := '*** - ***';
        QRLTelReceptor.Caption := '***';
      end;
    end;
  end
  else
  begin
    With DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select c.no_col, c.NU_TELE_CELU_COL, e.no_fantas_emp from tb03_colabor c '+
                  ' join tb25_empresa e on e.co_emp = c.co_emp ' +
                  'where c.co_col = ' + QryRelatorio.FieldByName('CO_RECEPT').AsString +
                  ' and c.co_emp = ' + QryRelatorio.FieldByName('CO_EMP_RECEPT').AsString;
      Open;

      if not IsEmpty then
      begin
        QRLReceptor.Caption := FieldByName('no_col').AsString + ' - ' + FieldByName('no_fantas_emp').AsString;
        QRLTelReceptor.Caption := FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_CELU_COL').AsString);
      end
      else
      begin
        QRLReceptor.Caption := '*** - ***';
        QRLTelReceptor.Caption := '***';
      end;
    end;
  end;
end;

procedure TFrmRelMinhasMsgs.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  totalEnv := 0;
  totalNEnv := 0;
end;

procedure TFrmRelMinhasMsgs.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(totalEnv) + ' (Enviadas) - ' + IntToStr(totalNEnv) + ' (Não Enviadas)';
end;

procedure TFrmRelMinhasMsgs.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_COL').IsNull then
  begin
    QRLEmissor.Caption := QryRelatorio.FieldByName('NO_COL').AsString +
    ' - ' + QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString;
  end
  else
    QRLEmissor.Caption := '-';

  if not QryRelatorio.FieldByName('NU_TELE_CELU_COL').IsNull then
    QRLEmissor.Caption := QRLEmissor.Caption + ' - Celular: ' + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_CELU_COL').AsString);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMinhasMsgs]);

end.
