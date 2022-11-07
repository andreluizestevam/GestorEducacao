unit U_FrmRelPautaChamadaVerso;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelPautaChamadaVerso = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRShape2: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRBand2: TQRBand;
    QRShape13: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRLabel11: TQRLabel;
    qrlCont: TQRLabel;
    QRBand3: TQRBand;
    QRShape21: TQRShape;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel18: TQRLabel;
    QRShape17: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRShape20: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRShape26: TQRShape;
    QRShape12: TQRShape;
    QRShape27: TQRShape;
    QRShape1: TQRShape;
    QRShape3: TQRShape;
    procedure qrlContPrint(sender: TObject; var Value: String);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private

  public
    Cont, i : Integer;
  end;

var
  FrmRelPautaChamadaVerso: TFrmRelPautaChamadaVerso;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelPautaChamadaVerso.qrlContPrint(sender: TObject;
  var Value: String);
begin
  inherited;
    Value := IntToStr(i + 1);
    Inc(i);
end;

procedure TFrmRelPautaChamadaVerso.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  i := 0;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelPautaChamadaVerso]);

end.
