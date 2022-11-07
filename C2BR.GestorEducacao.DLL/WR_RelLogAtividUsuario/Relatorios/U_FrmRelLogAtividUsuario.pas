unit U_FrmRelLogAtividUsuario;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelLogAtividUsuario = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLabel2: TQRLabel;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLNoEmp: TQRLabel;
    QRBand2: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLHrLog: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLTotal: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel9: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLabel7: TQRLabel;
    QRLAcao: TQRLabel;
    QRLabel10: TQRLabel;
    QRLUsuario: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel11: TQRLabel;
    QRLDtLog: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRLabel14: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
    total : integer;
  public
    { Public declarations }
  end;

var
  FrmRelLogAtividUsuario: TFrmRelLogAtividUsuario;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelLogAtividUsuario.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('DT_ATIVI_LOG').IsNull then
    QRLDtLog.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_ATIVI_LOG').AsDateTime)
  else
    QRLDtLog.Caption := '-';

  if not QryRelatorio.FieldByName('DT_ATIVI_LOG').IsNull then
    QRLHrLog.Caption := FormatDateTime('hh:mm:ss',QryRelatorio.FieldByName('DT_ATIVI_LOG').AsDateTime)
  else
    QRLHrLog.Caption := '-';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  if not QryRelatorio.FieldByName('CO_MAT_COL').IsNull then
  begin
    QRLUsuario.Caption := FormatMaskText('9.999-99;0',QryRelatorio.FieldByName('CO_MAT_COL').AsString) +
    ' - ' + QryRelatorio.FieldByName('NO_COL').AsString;
  end
  else
    QRLUsuario.Caption := '-';

  if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'I' then
    QRLAcao.Caption := 'Inclus�o'
  else if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'A' then
    QRLAcao.Caption := 'Altera��o'
  else if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'E' then
    QRLAcao.Caption := 'Exclus�o'
  else if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'C' then
    QRLAcao.Caption := 'Consulta'
  else if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'R' then
    QRLAcao.Caption := 'Relat�rio'
  else
    QRLAcao.Caption := 'Nenhuma';

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);

  total := total + 1;
end;

procedure TFrmRelLogAtividUsuario.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorio.FieldByName('NO_FANTAS_EMP').IsNull then
    QRLNoEmp.Caption := UpperCase(QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString)
  else
    QRLNoEmp.Caption := '-';

end;

procedure TFrmRelLogAtividUsuario.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
  total := 0;
end;

procedure TFrmRelLogAtividUsuario.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(total);
end;

procedure TFrmRelLogAtividUsuario.QRBand2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelLogAtividUsuario]);

end.
