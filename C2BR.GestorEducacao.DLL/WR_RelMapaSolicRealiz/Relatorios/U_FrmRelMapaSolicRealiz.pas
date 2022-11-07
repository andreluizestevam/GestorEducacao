unit U_FrmRelMapaSolicRealiz;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaSolicRealiz = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    SummaryBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QryRelatorioNO_TIPO_SOLI: TStringField;
    QryRelatorioCO_TIPO_SOLI: TIntegerField;
    QRDBText1: TQRDBText;
    QrySolic: TADOQuery;
    QrySolicCO_EMP: TIntegerField;
    QrySolicJAN: TIntegerField;
    QrySolicFEV: TIntegerField;
    QrySolicMAR: TIntegerField;
    QrySolicABR: TIntegerField;
    QrySolicMAI: TIntegerField;
    QrySolicJUN: TIntegerField;
    QrySolicJUL: TIntegerField;
    QrySolicAGO: TIntegerField;
    QrySolicSETE: TIntegerField;
    QrySolicOUT: TIntegerField;
    QrySolicNOV: TIntegerField;
    QrySolicDEZ: TIntegerField;
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
    QrlAnoRef: TQRLabel;
    QrlTotJul: TQRLabel;
    QRShape2: TQRShape;
    QRLabel16: TQRLabel;
    QrlTotDez: TQRLabel;
    QrlTotNov: TQRLabel;
    QrlTotOut: TQRLabel;
    QrlTotSet: TQRLabel;
    QrlTotAgo: TQRLabel;
    QrlTotMar: TQRLabel;
    QrlTotAbr: TQRLabel;
    QrlTotMai: TQRLabel;
    QrlTotJun: TQRLabel;
    QrlTotFev: TQRLabel;
    QrlTotJan: TQRLabel;
    QrySolicTOTAL: TIntegerField;
    QRDBText16: TQRDBText;
    QrlTOTAL: TQRLabel;
    QRLabel17: TQRLabel;
    QrlPage: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelMapaSolicRealiz: TFrmRelMapaSolicRealiz;

implementation

{$R *.dfm}
uses U_DataModuleSGE;

procedure TFrmRelMapaSolicRealiz.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotJan.Caption := '0';
  QrlTotFev.Caption := '0';
  QrlTotMar.Caption := '0';
  QrlTotAbr.Caption := '0';
  QrlTotMai.Caption := '0';
  QrlTotJun.Caption := '0';
  QrlTotJul.Caption := '0';
  QrlTotAgo.Caption := '0';
  QrlTotSet.Caption := '0';
  QrlTotOut.Caption := '0';
  QrlTotNov.Caption := '0';
  QrlTotDez.Caption := '0';
  QrlTOTAL.Caption := '0';
end;

procedure TFrmRelMapaSolicRealiz.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  // ZEBRADO
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00EBEBEB
  else
    DetailBand1.Color := clWhite;

  with QrySolic do
  begin
    Close;
    Sql.Clear;
    Sql.Text := ' SELECT DISTINCT HS.CO_EMP, '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 1                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) JAN,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 2                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) FEV,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 3                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) MAR,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 4                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) ABR,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 5                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) MAI,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 6                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) JUN,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 7                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) JUL,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 8                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) AGO,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 9                      '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) SETE,                                            '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 10                     '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) OUT,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 11                     '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) NOV,                                             '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND MONTH(DT_ENTR_SOLI) = 12                     '+
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) DEZ,                                              '+
                '  (SELECT COUNT(CO_TIPO_SOLI) FROM TB65_HIST_SOLICIT '+
                '    WHERE YEAR(DT_ENTR_SOLI) = ' + QrlAnoRef.Caption +
                '    AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                ' AND CO_EMP = HS.CO_EMP ' +
                '  ) TOTAL                                              '+
                ' FROM TB65_HIST_SOLICIT HS' +
                ' WHERE HS.CO_EMP = ' + codigoEmpresa;
    Open;
  end;

  QrlTotJan.Caption := IntToStr(StrToInt(QrlTotJan.Caption) + StrToInt(QrySolicJAN.AsString));
  QrlTotFev.Caption := IntToStr(StrToInt(QrlTotFev.Caption) + StrToInt(QrySolicFEV.AsString));
  QrlTotMar.Caption := IntToStr(StrToInt(QrlTotMar.Caption) + StrToInt(QrySolicMAR.AsString));
  QrlTotAbr.Caption := IntToStr(StrToInt(QrlTotAbr.Caption) + StrToInt(QrySolicABR.AsString));
  QrlTotMai.Caption := IntToStr(StrToInt(QrlTotMai.Caption) + StrToInt(QrySolicMAI.AsString));
  QrlTotJun.Caption := IntToStr(StrToInt(QrlTotJun.Caption) + StrToInt(QrySolicJUN.AsString));
  QrlTotJul.Caption := IntToStr(StrToInt(QrlTotJul.Caption) + StrToInt(QrySolicJUL.AsString));
  QrlTotAgo.Caption := IntToStr(StrToInt(QrlTotAgo.Caption) + StrToInt(QrySolicAGO.AsString));
  QrlTotSet.Caption := IntToStr(StrToInt(QrlTotSet.Caption) + StrToInt(QrySolicSETE.AsString));
  QrlTotOut.Caption := IntToStr(StrToInt(QrlTotOut.Caption) + StrToInt(QrySolicOUT.AsString));
  QrlTotNov.Caption := IntToStr(StrToInt(QrlTotNov.Caption) + StrToInt(QrySolicNOV.AsString));
  QrlTotDez.Caption := IntToStr(StrToInt(QrlTotDez.Caption) + StrToInt(QrySolicDEZ.AsString));
  QrlTOTAL.Caption := IntToStr(StrToInt(QrlTOTAL.Caption) + StrToInt(QrySolicTOTAL.AsString));

end;

end.
