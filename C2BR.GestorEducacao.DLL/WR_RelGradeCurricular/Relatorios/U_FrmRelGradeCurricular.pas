unit U_FrmRelGradeCurricular;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelGradeCurricular = class(TFrmRelTemplate)
    QRLabel2: TQRLabel;
    QRDBText11: TQRDBText;
    QRDBText1: TQRDBText;
    QRLabel10: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRShape3: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    DetailBand1: TQRBand;
    QRDBText4: TQRDBText;
    QRDBText6: TQRDBText;
    QRShape8: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRDBText2: TQRDBText;
    QRDBText7: TQRDBText;
    QRBand2: TQRBand;
    QRLabel9: TQRLabel;
    QRExpr1: TQRExpr;
    QRLabel1: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_ANO_GRADE: TStringField;
    QryRelatorioNU_SEM_GRADE: TIntegerField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_MAT: TIntegerField;
    QryRelatorioCO_MODU_CUR: TIntegerField;
    QryRelatorioCO_SITU_MATE_GRC: TStringField;
    QryRelatorioDT_SITU_MATE_GRC: TDateTimeField;
    QryRelatorioQTDE_AULA_SEM: TIntegerField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioNO_SIGLA_MATERIA: TStringField;
    QryRelatorioQT_CARG_HORA_MAT: TIntegerField;
    QryRelatorioQT_CRED_MAT: TIntegerField;
    QryRelatorioDE_MODU_CUR: TStringField;
    QrlSerie: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText3: TQRDBText;
    QryRelatorioQTDE_CH_SEM: TIntegerField;
    QryRelatorioNO_MATERIA: TStringField;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelGradeCurricular: TFrmRelGradeCurricular;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelGradeCurricular.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelGradeCurricular]);

end.