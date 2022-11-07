unit U_FrmRelHistEscAvalProgP4;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, DB, ADODB, QuickRpt, ExtCtrls, QRCtrls;

type
  TFrmRelHistEscAvalProgP4 = class(TForm)
    QuickRep4: TQuickRep;
    PageFooterBand1: TQRBand;
    PageHeaderBand1: TQRBand;
    DetailBand1: TQRBand;
    QryRelatorio4: TADOQuery;
    QryRelatorio4NO_ALU: TStringField;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRShape7: TQRShape;
    QRLabel6: TQRLabel;
    QRShape3: TQRShape;
    QRShape5: TQRShape;
    QRShape4: TQRShape;
    QRShape6: TQRShape;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRShape19: TQRShape;
    QRShape16: TQRShape;
    QRLabel15: TQRLabel;
    QRLabel18: TQRLabel;
    QRShape17: TQRShape;
    QRLabel16: TQRLabel;
    QRLabel14: TQRLabel;
    QRShape15: TQRShape;
    QRLabel5: TQRLabel;
    QRLabel4: TQRLabel;
    QRShape11: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRShape12: TQRShape;
    QRLabel12: TQRLabel;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRLabel13: TQRLabel;
    QRShape2: TQRShape;
    QRShape10: TQRShape;
    QRShape9: TQRShape;
    QRShape8: TQRShape;
    QRLabel40: TQRLabel;
    QRLabel39: TQRLabel;
    QRLabel36: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel38: TQRLabel;
    QRShape33: TQRShape;
    QRShape34: TQRShape;
    QRShape35: TQRShape;
    QRLabel17: TQRLabel;
    QRShape18: TQRShape;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRShape27: TQRShape;
    QRShape28: TQRShape;
    QRLabel19: TQRLabel;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape36: TQRShape;
    QRShape37: TQRShape;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape40: TQRShape;
    QRShape41: TQRShape;
    QrlEmpresaP4: TQRLabel;
    QrlNoAlunoP4: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelHistEscAvalProgP4: TFrmRelHistEscAvalProgP4;

implementation

{$R *.dfm}
uses U_DataModuleSGE, U_FrmRelHistEscAvalProg, U_FrmRelHistEscAvalProgP2, U_FrmRelHistEscAvalProgP3;

procedure TFrmRelHistEscAvalProgP4.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  QrlEmpresaP4.Caption := FrmRelHistEscAvalProg.QrlNoEmpresa.Caption;
  QrlNoAlunoP4.Caption := FrmRelHistEscAvalProg.QrlNoAlu.Caption;
end;

end.
