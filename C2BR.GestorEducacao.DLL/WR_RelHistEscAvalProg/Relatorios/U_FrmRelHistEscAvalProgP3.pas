unit U_FrmRelHistEscAvalProgP3;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, DB, ADODB, ExtCtrls, QuickRpt, QRCtrls;

type
  TFrmRelHistEscAvalProgP3 = class(TForm)
    QuickRep3: TQuickRep;
    QryRelatorio3: TADOQuery;
    QryRelatorio3NO_ALU: TStringField;
    PageHeaderBand1: TQRBand;
    PageFooterBand1: TQRBand;
    DetailBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel4: TQRLabel;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRLabel5: TQRLabel;
    QRShape7: TQRShape;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRLabel10: TQRLabel;
    QRShape11: TQRShape;
    QRLabel11: TQRLabel;
    QRShape12: TQRShape;
    QRLabel12: TQRLabel;
    QRShape13: TQRShape;
    QRLabel13: TQRLabel;
    QRShape14: TQRShape;
    QRLabel14: TQRLabel;
    QrSh1CINI: TQRShape;
    QRLabel15: TQRLabel;
    QrSh2CINI: TQRShape;
    QrSh1CINT: TQRShape;
    QRLabel16: TQRLabel;
    QrSh1CFIN: TQRShape;
    QRLabel17: TQRLabel;
    QrSh2CFIN: TQRShape;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRShape27: TQRShape;
    QRShape35: TQRShape;
    QRShape34: TQRShape;
    QRLabel40: TQRLabel;
    QRLabel39: TQRLabel;
    QRLabel36: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel38: TQRLabel;
    QRShape33: TQRShape;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape36: TQRShape;
    QrlEmpresaP3: TQRLabel;
    QrlNoAlunoP3: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelHistEscAvalProgP3: TFrmRelHistEscAvalProgP3;

implementation

{$R *.dfm}
uses U_DataModuleSGE, DateUtils, U_FrmRelHistEscAvalProg, U_FrmRelHistEscAvalProgP2, U_FrmRelHistEscAvalProgP4;

procedure TFrmRelHistEscAvalProgP3.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  QrlEmpresaP3.Caption := FrmRelHistEscAvalProg.QrlNoEmpresa.Caption;
  QrlNoAlunoP3.Caption := FrmRelHistEscAvalProg.QrlNoAlu.Caption;

end;

end.
