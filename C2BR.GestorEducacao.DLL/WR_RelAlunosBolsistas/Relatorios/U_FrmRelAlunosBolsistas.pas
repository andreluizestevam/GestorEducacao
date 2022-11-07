unit U_FrmRelAlunosBolsistas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelAlunosBolsistas = class(TFrmRelTemplate)
    QRLabel14: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioCO_ALU: TAutoIncField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioFLA_BOLSISTA: TStringField;
    QryRelatorioDT_VENC_BOLSA: TDateTimeField;
    QryRelatorioDT_VENC_BOLSAF: TDateTimeField;
    QryRelatorioNU_PEC_DESBOL: TBCDField;
    QryRelatorioDE_TIPO_BOLSA: TStringField;
    QryRelatorioNO_TUR: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioVL_TOT_MODU_MAT: TBCDField;
    QryRelatorioVL_DES_MOD_MAT: TBCDField;
    QryRelatorioVL_PAR_MOD_MAT: TBCDField;
    QryRelatorioDE_MODU_CUR: TStringField;
    QryRelatorioNU_CPF_RESP: TStringField;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioCO_TUR: TAutoIncField;
    QryRelatorioCO_CUR: TAutoIncField;
    QryRelatorioCO_ANO_MES_MAT: TStringField;
    QRLabel4: TQRLabel;
    QrlCurso: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel2: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel5: TQRLabel;
    QRDBText3: TQRDBText;
    QRShape6: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel11: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel16: TQRLabel;
    QRLTotTurCont: TQRLabel;
    QRLTotTurBolsa: TQRLabel;
    QrlTotalValorTurma: TQRLabel;
    DetailBand1: TQRBand;
    QRLMatricula: TQRLabel;
    QRDBText8: TQRDBText;
    QRLBolsa: TQRLabel;
    QRLContrato: TQRLabel;
    QrlValorBolsa: TQRLabel;
    QRLMensal: TQRLabel;
    QRLVlLiquido: TQRLabel;
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    totTurma,totCurso,totContTur,totBolTur,totContCur,totBolCur : Double;
  end;

var
  FrmRelAlunosBolsistas: TFrmRelAlunosBolsistas;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelAlunosBolsistas.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QrlTotalValorTurma.Caption := '0';
  QRLTotTurCont.Caption := '0';
  QRLTotTurBolsa.Caption := '0';
end;

procedure TFrmRelAlunosBolsistas.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotalValorTurma.Caption := '0';
  totTurma := 0;
  totContTur := 0;
  totBolTur := 0;
end;

procedure TFrmRelAlunosBolsistas.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  //Valor do Contrato
  if not QryRelatorioVL_TOT_MODU_MAT.IsNull then
  begin
    QRLContrato.Caption := FloatToStrF(QryRelatorioVL_TOT_MODU_MAT.AsFloat,ffNumber,14,2);
    totContTur := totContTur + QryRelatorioVL_TOT_MODU_MAT.AsFloat;
  end
  else
    QRLContrato.Caption := '-';

  //Valor da BOLSA
  if not QryRelatorioVL_DES_MOD_MAT.IsNull then
  begin
    QrlValorBolsa.Caption := FloatToStrF(QryRelatorioVL_DES_MOD_MAT.AsFloat,ffNumber,14,2);
    totBolTur := totBolTur + QryRelatorioVL_DES_MOD_MAT.AsFloat;
  end
  else
    QrlValorBolsa.Caption := '-';

  //Valor Mensalidade
  if not QryRelatorioVL_PAR_MOD_MAT.IsNull then
    QRLMensal.Caption := FloatToStrF(QryRelatorioVL_PAR_MOD_MAT.AsFloat,ffNumber,14,2)
  else
    QRLMensal.Caption := '-';

  //Valor Líquido
  if (not QryRelatorioVL_TOT_MODU_MAT.IsNull) and (not QryRelatorioVL_DES_MOD_MAT.IsNull) then
    QRLVlLiquido.Caption := FloatToStrF(QryRelatorioVL_TOT_MODU_MAT.AsFloat - QryRelatorioVL_DES_MOD_MAT.AsFloat,ffNumber,15,2)
  else
    QRLVlLiquido.Caption := '-';

  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;

  //QrlTotalValorCurso.Caption := FormatFloat('#0.00',StrToFloat(QRLVlLiquido.Caption)+StrToFloat(QrlTotalValorCurso.Caption));
  if (not QryRelatorioVL_TOT_MODU_MAT.IsNull) and (not QryRelatorioVL_DES_MOD_MAT.IsNull) then
    totTurma := totTurma + (QryRelatorioVL_TOT_MODU_MAT.AsFloat - QryRelatorioVL_DES_MOD_MAT.AsFloat);

  QRLMatricula.Caption := '';
  if not QryRelatorioco_alu_cad.IsNull then
    QRLMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorioco_alu_cad.AsString) + ' - ' + UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLMatricula.Caption := '-';

  QRLBolsa.Caption := '';
  if not qryRelatorioDE_TIPO_BOLSA.isnull then
  begin
    QRLBolsa.Caption := qryRelatorioDE_TIPO_BOLSA.asString;
    if not qryRelatorioDT_VENC_BOLSA.isnull then
      QRLBolsa.Caption := '( ' + qryRelatorioNU_PEC_DESBOL.asString + '% - ' + qryRelatorioDT_VENC_BOLSA.asString + ' à ' +
      qryRelatorioDT_VENC_BOLSAF.asString + ' )'
    else
      QRLBolsa.Caption := '-';
  end
  else
  begin
    if not qryRelatorioDT_VENC_BOLSA.isnull then
      QRLBolsa.Caption := '( ' + qryRelatorioNU_PEC_DESBOL.asString + ' - ' + qryRelatorioDT_VENC_BOLSA.asString + ' à ' +
      qryRelatorioDT_VENC_BOLSAF.asString + ' )'
    else
      QRLBolsa.Caption := '-';
  end;
end;

procedure TFrmRelAlunosBolsistas.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  totCurso := totCurso + totTurma;
  QrlTotalValorTurma.Caption := FloatToStrf(totTurma,ffNumber,15,2);
  totContCur := totContCur + totContTur;
  QRLTotTurCont.Caption := FloatToStrf(totContTur,ffNumber,15,2);
  totBolCur := totBolCur + totBolTur;
  QRLTotTurBolsa.Caption := FloatToStrf(totBolTur,ffNumber,15,2);
end;

procedure TFrmRelAlunosBolsistas.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  totCurso := 0;
  totContCur := 0;
  totBolCur := 0;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelAlunosBolsistas]);

end.
