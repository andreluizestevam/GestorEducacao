unit U_FrmRelLinhaTranspPorAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelLinhaTranspPorAluno = class(TFrmRelTemplate)
    SummaryBand1: TQRBand;
    QRLParametros: TQRLabel;
    QrlTotOco: TQRLabel;
    QRLabel50: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel48: TQRLabel;
    DetailBand1: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRShape1: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QryRelatorioORG_CODIGO_ORGAO: TIntegerField;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioIDE_ALUNO_PASSE_ESCOLAR: TAutoIncField;
    QryRelatorioANO_REF: TIntegerField;
    QryRelatorioCO_LINHA_ONIBUS: TIntegerField;
    QryRelatorioMES_REF: TIntegerField;
    QryRelatorioQT_DIA_ALUNO_PASSE_ESCOLAR: TIntegerField;
    QryRelatorioFLA_STATUS_ALUNO_PASSE_ESCOLAR: TStringField;
    QryRelatorioDT_STATUS_ALUNO_PASSE_ESCOLAR: TDateTimeField;
    QryRelatorioDT_CADASTRO_ALUNO_PASSE_ESCOLAR: TDateTimeField;
    QryRelatorioCO_COL_RESP: TIntegerField;
    QryRelatorioCO_EMP_RESP: TIntegerField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioNO_LINHA_ONIBUS: TStringField;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QryRelatorioVL_TARIF_LINHA_ONIBUS: TBCDField;
    QRDBText6: TQRDBText;
    QRLStatus: TQRLabel;
    QRLTarifa: TQRLabel;
    QRLabel5: TQRLabel;
    QRDBText4: TQRDBText;
    QryRelatorioDE_LINHA_ONIBUS: TStringField;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelLinhaTranspPorAluno: TFrmRelLinhaTranspPorAluno;

implementation

{$R *.dfm}
uses U_DataModuleSGE, DateUtils, MaskUtils;

procedure TFrmRelLinhaTranspPorAluno.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if QryRelatorioFLA_STATUS_ALUNO_PASSE_ESCOLAR.AsString = 'A' then
    QRLStatus.Caption := 'Ativo'
  else
    QRLStatus.Caption := 'Inativo';

  if not QryRelatorioVL_TARIF_LINHA_ONIBUS.IsNull then
    QRLTarifa.Caption := FloatToStrF(QryRelatorioVL_TARIF_LINHA_ONIBUS.AsFloat,ffNumber,10,2)
  else
    QRLTarifa.Caption := '-';

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  QrlTotOco.Caption := IntToStr(StrToInt(QrlTotOco.Caption) + 1);
end;

procedure TFrmRelLinhaTranspPorAluno.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotOco.Caption := '0';
end;

procedure TFrmRelLinhaTranspPorAluno.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
end;

end.
