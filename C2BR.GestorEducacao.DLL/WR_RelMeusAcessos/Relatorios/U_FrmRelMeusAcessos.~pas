unit U_FrmRelMeusAcessos;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMeusAcessos = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRBand2: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLTotal: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel9: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLabel7: TQRLabel;
    QRLAcao: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel12: TQRLabel;
    QRLDtLog: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRLabel14: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRLUsuario: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    total : integer;
  public
    { Public declarations }
  end;

var
  FrmRelMeusAcessos: TFrmRelMeusAcessos;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelMeusAcessos.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('DT_ATIVI_LOG').IsNull then
    QRLDtLog.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_ATIVI_LOG').AsDateTime)
  else
    QRLDtLog.Caption := '-';

  if not QryRelatorio.FieldByName('DT_ATIVI_LOG').IsNull then
    QRLDtLog.Caption := QRLDtLog.Caption + ' ' + FormatDateTime('hh:mm:ss',QryRelatorio.FieldByName('DT_ATIVI_LOG').AsDateTime)
  else
    QRLDtLog.Caption := QRLDtLog.Caption + ' -';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'I' then
    QRLAcao.Caption := 'Inclusão'
  else if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'A' then
    QRLAcao.Caption := 'Alteração'
  else if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'E' then
    QRLAcao.Caption := 'Exclusão'
  else if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'C' then
    QRLAcao.Caption := 'Consulta'
  else if QryRelatorio.FieldByName('CO_ACAO_ATIVI_LOG').AsString = 'R' then
    QRLAcao.Caption := 'Relatório'
  else
    QRLAcao.Caption := 'Nenhuma';

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);

  total := total + 1;
end;

procedure TFrmRelMeusAcessos.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
  total := 0;
end;

procedure TFrmRelMeusAcessos.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(total);
end;

procedure TFrmRelMeusAcessos.QRBand2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelMeusAcessos.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('CO_MAT_COL').IsNull then
  begin
    QRLUsuario.Caption := FormatMaskText('9.999-99;0',QryRelatorio.FieldByName('CO_MAT_COL').AsString) +
    ' - ' + QryRelatorio.FieldByName('NO_COL').AsString;
  end
  else
    QRLUsuario.Caption := '-';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMeusAcessos]);

end.
