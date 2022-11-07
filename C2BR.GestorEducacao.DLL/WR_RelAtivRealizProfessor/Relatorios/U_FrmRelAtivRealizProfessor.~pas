unit U_FrmRelAtivRealizProfessor;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelAtivRealizProfessor = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    detail: TQRBand;
    QRShape1: TQRShape;
    QRLabel2: TQRLabel;
    QrlMes1: TQRLabel;
    QrlMes2: TQRLabel;
    QrlMes4: TQRLabel;
    QRDBText2: TQRDBText;
    QRLParametros: TQRLabel;
    QRBand2: TQRBand;
    QRLabel27: TQRLabel;
    QRLabel14: TQRLabel;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_ANO_MES_MAT: TStringField;
    QryRelatorioCO_MODU_CUR: TIntegerField;
    QryRelatorioNU_SEM_LET: TStringField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioDT_ATIV_REAL: TDateTimeField;
    QryRelatorioCO_COL: TIntegerField;
    QryRelatorioHR_INI_ATIV: TStringField;
    QryRelatorioHR_TER_ATIV: TStringField;
    QryRelatorioDE_RES_ATIV: TStringField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatorioNO_TUR: TStringField;
    QRDBText3: TQRDBText;
    QRExpr1: TQRExpr;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRExprMemo1: TQRExprMemo;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    qrlDataReal: TQRLabel;
    QRLabel5: TQRLabel;
    QryRelatorioDT_PREV_PLA: TDateTimeField;
    QryRelatorioQT_CARG_HORA_PLA: TBCDField;
    QRDBText4: TQRDBText;
    QryRelatorioNO_MATERIA: TStringField;
    qrlDataPlan: TQRLabel;
    QryRelatorioDE_TEMA_AULA: TStringField;
    QRLNoCol: TQRLabel;
    procedure detailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }

  public
    { Public declarations }
  end;

var
  FrmRelAtivRealizProfessor: TFrmRelAtivRealizProfessor;

implementation

uses U_DataModuleSGE, U_Funcoes, DateUtils;

{$R *.dfm}

procedure TFrmRelAtivRealizProfessor.detailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  qrlDataReal.Caption := FormatDateTime('dd/mm/yy', QryRelatorioDT_ATIV_REAL.AsFloat) + ' | ' + QryRelatorioHR_INI_ATIV.AsString + '/' + QryRelatorioHR_TER_ATIV.AsString;
  if not QryRelatorioQT_CARG_HORA_PLA.IsNull then
    qrlDataPlan.Caption := FormatDateTime('dd/mm/yy', QryRelatorioDT_PREV_PLA.AsFloat) + ' | ' + QryRelatorioQT_CARG_HORA_PLA.AsString + ' min'
  else
    qrlDataPlan.Caption := FormatDateTime('dd/mm/yy', QryRelatorioDT_PREV_PLA.AsFloat) + ' | Não Planejada';

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelAtivRealizProfessor.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString)
  else
    QRLNoCol.Caption := '-';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelAtivRealizProfessor]);

end.
