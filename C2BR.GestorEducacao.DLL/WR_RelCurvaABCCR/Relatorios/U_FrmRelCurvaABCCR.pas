unit U_FrmRelCurvaABCCR;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelCurvaABCCR = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRBand2: TQRBand;
    qrdbQtdeDoc: TQRDBText;
    qrdbValorTot: TQRDBText;
    QRShape1: TQRShape;
    QRBand3: TQRBand;
    QRLabel5: TQRLabel;
    QRExpr1: TQRExpr;
    QRExpr2: TQRExpr;
    QrlSituacao: TQRLabel;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel6: TQRLabel;
    QRLPercent: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLNumSeq: TQRLabel;
    QrlNome: TQRLabel;
    QRLObservacao: TQRLabel;
    QRLCodigo: TQRLabel;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioDE_RAZSOC_CLI: TStringField;
    QryRelatorioCO_CLIENTE: TIntegerField;
    QryRelatorioTOT_DOC: TIntegerField;
    QryRelatorioVL_TOTAL: TBCDField;
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    numSeq : Double;
  public
    valorTotalCR : Double;
  end;

var
  FrmRelCurvaABCCR: TFrmRelCurvaABCCR;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelCurvaABCCR.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  QRLNumSeq.Caption := FormatFloat('##00', numSeq);

  if not QryRelatorio.FieldByName('CO_ALU').IsNull then
    QRLCodigo.Caption := QryRelatorio.FieldByName('CO_ALU').AsString
  else
    QRLCodigo.Caption := QryRelatorio.FieldByName('CO_CLIENTE').AsString;

  if not QryRelatorio.FieldByName('CO_ALU').IsNull then
    QRLObservacao.Caption := 'Aluno'
  else
    QRLObservacao.Caption := 'Cliente';

  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNome.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNome.Caption := UpperCase(QryRelatorio.FieldByName('DE_RAZSOC_CLI').AsString);

  if QRBand2.Color = $00F0F0F0 then
  begin
     QRBand2.Color := clWhite;
  end
  else
  begin
     QRBand2.Color := $00F0F0F0;
  end;

  if valorTotalCR > 0 then
  begin
    QRLPercent.Caption := FloatToStrF((QryRelatorioVL_TOTAL.AsFloat*100)/valorTotalCR,ffNumber,10,2);
  end
  else
    QRLPercent.Caption := '-';

  numSeq := numSeq + 1;
end;

procedure TFrmRelCurvaABCCR.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  numSeq := 1;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelCurvaABCCR]);

end.
