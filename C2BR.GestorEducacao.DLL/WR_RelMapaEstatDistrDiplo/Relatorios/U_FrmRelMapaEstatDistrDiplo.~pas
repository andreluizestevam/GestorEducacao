unit U_FrmRelMapaEstatDistrDiplo;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaEstatDistrDiplo = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    SummaryBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRShape2: TQRShape;
    QRDBText1: TQRDBText;
    QRLTot: TQRLabel;
    QRLTotJan: TQRLabel;
    QRLTotFev: TQRLabel;
    QRLTotMar: TQRLabel;
    QRLTotAbr: TQRLabel;
    QRLTotMai: TQRLabel;
    QRLTotJun: TQRLabel;
    QRLTotJul: TQRLabel;
    QRLTotAgo: TQRLabel;
    QRLTotSet: TQRLabel;
    QRLTotOut: TQRLabel;
    QRLTotNov: TQRLabel;
    QRLTotDez: TQRLabel;
    QRLTotal: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText13: TQRDBText;
    QRLabel16: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMapaEstatDistrDiplo: TFrmRelMapaEstatDistrDiplo;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaEstatDistrDiplo.DetailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLTot.Caption := IntToStr(QryRelatorio.FieldByName('jan').AsInteger + QryRelatorio.FieldByName('fev').AsInteger + QryRelatorio.FieldByName('mar').AsInteger +
  QryRelatorio.FieldByName('abr').AsInteger + QryRelatorio.FieldByName('mai').AsInteger + QryRelatorio.FieldByName('jun').AsInteger + QryRelatorio.FieldByName('jul').AsInteger +
  QryRelatorio.FieldByName('ago').AsInteger + QryRelatorio.FieldByName('sete').AsInteger + QryRelatorio.FieldByName('out').AsInteger + QryRelatorio.FieldByName('nov').AsInteger +
  QryRelatorio.FieldByName('dez').AsInteger);
  QRLTotJan.Caption := IntToStr(StrToInt(QRLTotJan.Caption) + QryRelatorio.FieldByName('jan').AsInteger);
  QRLTotFev.Caption := IntToStr(StrToInt(QRLTotFev.Caption) + QryRelatorio.FieldByName('fev').AsInteger);
  QRLTotMar.Caption := IntToStr(StrToInt(QRLTotMar.Caption) + QryRelatorio.FieldByName('mar').AsInteger);
  QRLTotAbr.Caption := IntToStr(StrToInt(QRLTotAbr.Caption) + QryRelatorio.FieldByName('abr').AsInteger);
  QRLTotMai.Caption := IntToStr(StrToInt(QRLTotMai.Caption) + QryRelatorio.FieldByName('mai').AsInteger);
  QRLTotJun.Caption := IntToStr(StrToInt(QRLTotJun.Caption) + QryRelatorio.FieldByName('jun').AsInteger);
  QRLTotJul.Caption := IntToStr(StrToInt(QRLTotJul.Caption) + QryRelatorio.FieldByName('jul').AsInteger);
  QRLTotAgo.Caption := IntToStr(StrToInt(QRLTotAgo.Caption) + QryRelatorio.FieldByName('ago').AsInteger);
  QRLTotSet.Caption := IntToStr(StrToInt(QRLTotSet.Caption) + QryRelatorio.FieldByName('sete').AsInteger);
  QRLTotOut.Caption := IntToStr(StrToInt(QRLTotOut.Caption) + QryRelatorio.FieldByName('out').AsInteger);
  QRLTotNov.Caption := IntToStr(StrToInt(QRLTotNov.Caption) + QryRelatorio.FieldByName('nov').AsInteger);
  QRLTotDez.Caption := IntToStr(StrToInt(QRLTotDez.Caption) + QryRelatorio.FieldByName('dez').AsInteger);
  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + StrToInt(QRLTot.Caption));
end;

procedure TFrmRelMapaEstatDistrDiplo.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotJan.Caption := '0';
  QRLTotFev.Caption := '0';
  QRLTotMar.Caption := '0';
  QRLTotAbr.Caption := '0';
  QRLTotMai.Caption := '0';
  QRLTotJun.Caption := '0';
  QRLTotJul.Caption := '0';
  QRLTotAgo.Caption := '0';
  QRLTotSet.Caption := '0';
  QRLTotOut.Caption := '0';
  QRLTotNov.Caption := '0';
  QRLTotDez.Caption := '0';
  QRLTotal.Caption := '0';
end;

end.
