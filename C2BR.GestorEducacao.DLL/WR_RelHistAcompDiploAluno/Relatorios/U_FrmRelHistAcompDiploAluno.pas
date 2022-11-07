unit U_FrmRelHistAcompDiploAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelHistAcompDiploAluno = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    SummaryBand1: TQRBand;
    QRGroup1: TQRGroup;
    QRLAluno: TQRLabel;
    QRLParam: TQRLabel;
    QRLabel22: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QRLabel8: TQRLabel;
    QRLabel10: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelHistAcompDiploAluno: TFrmRelHistAcompDiploAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelHistAcompDiploAluno.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotal.Caption := IntToStr(StrToInT(QRLTotal.Caption) + 1);

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelHistAcompDiploAluno.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLAluno.Caption := 'Aluno: ' + UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString) + ' - Diploma: ' + QryRelatorio.FieldByName('CO_EMP').AsString +
  '.' + FormatDateTime('yyyy',QryRelatorio.FieldByName('DT_INIC_CURS_DIP').AsDateTime) + FormatDateTime('MM',QryRelatorio.FieldByName('DT_INIC_CURS_DIP').AsDateTime) +
  '.' + FormatFloat('000000;0',QryRelatorio.FieldByName('CO_DIPLOMA').AsFloat);
end;

procedure TFrmRelHistAcompDiploAluno.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
