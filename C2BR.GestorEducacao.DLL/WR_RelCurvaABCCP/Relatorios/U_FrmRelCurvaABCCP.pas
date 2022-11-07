unit U_FrmRelCurvaABCCP;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls;

type
  TFrmRelCurvaABCCP = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRShape1: TQRShape;
    QRBand2: TQRBand;
    qrdbQtdeDoc: TQRDBText;
    qrdbValorTot: TQRDBText;
    QRBand3: TQRBand;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRExpr1: TQRExpr;
    QRExpr2: TQRExpr;
    QRShape2: TQRShape;
    QrlSituacao: TQRLabel;
    QRShape3: TQRShape;
    QRLNumSeq: TQRLabel;
    QryRelatorioCO_FORN: TIntegerField;
    QryRelatorioNO_FAN_FOR: TStringField;
    QryRelatorioTOT_DOC: TIntegerField;
    QryRelatorioVL_TOTAL: TBCDField;
    QRLNoForn: TQRLabel;
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    numSeq : Double;
  public
    { Public declarations }
  end;

var
  FrmRelCurvaABCCP: TFrmRelCurvaABCCP;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelCurvaABCCP.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_FAN_FOR.IsNull then
    QRLNoForn.Caption := UpperCase(QryRelatorioNO_FAN_FOR.AsString)
  else
    QRLNoForn.Caption := '-';

  QRLNumSeq.Caption := FormatFloat('##00', numSeq);

  if QRBand2.Color = $00F0F0F0 then
  begin
     QRBand2.Color := clWhite;
  end
  else
  begin
     QRBand2.Color := $00F0F0F0;
  end;

  numSeq := numSeq + 1;
end;

procedure TFrmRelCurvaABCCP.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  numSeq := 1;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelCurvaABCCP]);

end.
