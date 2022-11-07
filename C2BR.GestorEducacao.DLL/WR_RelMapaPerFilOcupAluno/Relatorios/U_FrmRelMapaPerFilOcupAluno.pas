unit U_FrmRelMapaPerFilOcupAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaPerFilOcupAluno = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel22: TQRLabel;
    QRShape1: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel10: TQRLabel;
    SummaryBand1: TQRBand;
    QRLParam: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLLegenda: TQRLabel;
    QRDBText4: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText11: TQRDBText;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMapaPerFilOcupAluno: TFrmRelMapaPerFilOcupAluno;

implementation

Uses U_DataModuleSGE,U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaPerFilOcupAluno.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

end.