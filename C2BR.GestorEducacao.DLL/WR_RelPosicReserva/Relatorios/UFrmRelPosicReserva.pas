unit UFrmRelPosicReserva;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelPosicReserva = class(TFrmRelTemplate)
    QrlPeriodo: TQRLabel;
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRShape3: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRBand2: TQRBand;
    QRDBText1: TQRDBText;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioAberto: TIntegerField;
    QryRelatorioHabili: TIntegerField;
    QryRelatorioMatric: TIntegerField;
    QryRelatorioCancel: TIntegerField;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QrlTotAberto: TQRLabel;
    QrlTotHabilit: TQRLabel;
    QrlTotMatric: TQRLabel;
    QrlTotCancel: TQRLabel;
    QRLabel6: TQRLabel;
    QRLPage: TQRLabel;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRLabel7: TQRLabel;
    qrlTotalGeral: TQRLabel;
    QrlSomTotGeral: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRDBText2Print(sender: TObject; var Value: String);
    procedure QRDBText3Print(sender: TObject; var Value: String);
    procedure QRDBText4Print(sender: TObject; var Value: String);
    procedure QRDBText5Print(sender: TObject; var Value: String);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure qrlTotalGeralPrint(sender: TObject; var Value: String);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelPosicReserva: TFrmRelPosicReserva;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelPosicReserva.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotAberto.Caption := '0';
  QrlTotHabilit.Caption := '0';
  QrlTotMatric.Caption := '0';
  QrlTotCancel.Caption := '0';
  QrlSomTotGeral.Caption := '0';
end;

procedure TFrmRelPosicReserva.QRDBText2Print(sender: TObject;var Value: String);
begin
  inherited;
  QrlTotAberto.Caption := IntToStr(StrToInt(QrlTotAberto.Caption)+StrToInt(Value));
end;

procedure TFrmRelPosicReserva.QRDBText3Print(sender: TObject;var Value: String);
begin
  inherited;
  QrlTotHabilit.Caption := IntToStr(StrToInt(QrlTotHabilit.Caption)+StrToInt(Value));
end;

procedure TFrmRelPosicReserva.QRDBText4Print(sender: TObject;var Value: String);
begin
  inherited;
  QrlTotMatric.Caption := IntToStr(StrToInt(QrlTotMatric.Caption)+StrToInt(Value));
end;

procedure TFrmRelPosicReserva.QRDBText5Print(sender: TObject;var Value: String);
begin
  inherited;
  QrlTotCancel.Caption := IntToStr(StrToInt(QrlTotCancel.Caption)+StrToInt(Value));
end;

procedure TFrmRelPosicReserva.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  qrlTotalGeral.Caption := IntToStr(QryRelatorioAberto.AsInteger + QryRelatorioHabili.AsInteger + QryRelatorioMatric.AsInteger + QryRelatorioCancel.AsInteger);

// deixa tudo zebrado;
  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;

end;

procedure TFrmRelPosicReserva.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  qrlTotalGeral.Caption := '0';
end;

procedure TFrmRelPosicReserva.qrlTotalGeralPrint(sender: TObject;
  var Value: String);
begin
  inherited;
  QrlSomTotGeral.Caption := IntToStr(StrToInt(QrlSomTotGeral.Caption)+StrToInt(Value));
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelPosicReserva]);

end.
