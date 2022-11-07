unit U_FrmRelTelefoAlunos;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelTelefoAlunos = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLabel2: TQRLabel;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLNoAlu: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel5: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel9: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLAluno: TQRLabel;
    QRLabel14: TQRLabel;
    QRDBText3: TQRDBText;
    QRLEndereco: TQRLabel;
    QRLParam: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    total : integer;
  public
    { Public declarations }
  end;

var
  FrmRelTelefoAlunos: TFrmRelTelefoAlunos;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelTelefoAlunos.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLAluno.Caption := '-';

  if not QryRelatorio.FieldByName('CO_TIPO_TELEFONE').IsNull then
  begin
    QRLEndereco.Caption := QryRelatorio.FieldByName('CO_TIPO_TELEFONE').AsString;
    QRLEndereco.Caption := QRLEndereco.Caption + ' / (' + FormatFloat('000;0',QryRelatorio.FieldByName('NR_DDD').AsFloat) + ')';
    QRLEndereco.Caption := QRLEndereco.Caption + ' ' + FormatMaskText('0000-0000;0',QryRelatorio.FieldByName('NR_TELEFONE').AsString);
  end
  else
    QRLEndereco.Caption := '-';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  total := total + 1;
end;

procedure TFrmRelTelefoAlunos.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  total := 0;
end;

procedure TFrmRelTelefoAlunos.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(total);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelTelefoAlunos]);

end.
