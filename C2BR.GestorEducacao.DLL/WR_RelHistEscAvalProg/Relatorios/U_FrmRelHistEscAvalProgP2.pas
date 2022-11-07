unit U_FrmRelHistEscAvalProgP2;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, DB, ADODB, QuickRpt, QRCtrls, ExtCtrls;

type
  TFrmRelHistEscAvalProgP2 = class(TForm)
    QuickRep2: TQuickRep;
    PageHeaderBand1: TQRBand;
    PageFooterBand1: TQRBand;
    DetailBand1: TQRBand;
    QryRelatorio2: TADOQuery;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel10: TQRLabel;
    QRShape13: TQRShape;
    QRShape12: TQRShape;
    QRShape6: TQRShape;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape7: TQRShape;
    QRShape5: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape14: TQRShape;
    QryRelatorio2NO_ALU: TStringField;
    QryRelatorio2DT_NASC_ALU: TDateTimeField;
    QryRelatorio2NO_PAI_ALU: TStringField;
    QryRelatorio2NO_MAE_ALU: TStringField;
    QrlNoAluP2: TQRLabel;
    QrlDTnascP2: TQRLabel;
    QrlObsP2: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QrlNoPaiP2: TQRLabel;
    QrlNoMaeP2: TQRLabel;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelHistEscAvalProgP2: TFrmRelHistEscAvalProgP2;

implementation

{$R *.dfm}
uses U_DataModuleSGE, U_FrmRelHistEscAvalProg, U_FrmRelHistEscAvalProgP3,
  U_FrmRelHistEscAvalProgP4;

end.
