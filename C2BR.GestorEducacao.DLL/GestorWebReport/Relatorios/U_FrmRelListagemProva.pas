unit U_FrmRelListagemProva;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelListagemProva = class(TFrmRelTemplate)
    QRLParametros: TQRLabel;
    QRBand2: TQRBand;
    QRShape4: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel8: TQRLabel;
    QRBand1: TQRBand;
    QRBand3: TQRBand;
    QRShape13: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel9: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRLabel6: TQRLabel;
    QRLabel11: TQRLabel;
    QrlPercFaltas: TQRLabel;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioVL_NOTA: TBCDField;
    QryRelatorioPE_CERT_CUR: TBCDField;
    QryRelatorioQT_AULA_CUR: TIntegerField;
    QryRelatorioFALTA: TIntegerField;
    QryRelatorioPE_FALT_CUR: TBCDField;
    QRLabel2: TQRLabel;
    QRLPage: TQRLabel;
    QRShape1: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape2: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QrlMatNome: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QRDBText2Print(sender: TObject; var Value: String);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelListagemProva: TFrmRelListagemProva;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelListagemProva.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;

  QrlMatNome.Caption := FormatMaskText('00.000.0000.###;0', QryRelatorioCO_ALU_CAD.AsString) + ' - ' + UpperCase(QryRelatorioNO_ALU.AsString);

  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;

  QrlPercFaltas.Caption := '';

  if (QryRelatorioQT_AULA_CUR.AsString <> '0')
  and (QryRelatorioQT_AULA_CUR.AsString <> '') then
    QrlPercFaltas.Caption := FloatToStrF(((QryRelatorioFALTA.Value/QryRelatorioQT_AULA_CUR.Value)* 100),ffNumber,15,2);
end;

procedure TFrmRelListagemProva.QRDBText2Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := QryRelatorioCO_ALU_CAD.AsString + ' - ' + Value; 
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelListagemProva]);

end.

