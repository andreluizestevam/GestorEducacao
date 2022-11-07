unit U_FrmRelRelacAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacAluno = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioNU_CPF_ALU: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioCO_ALU_CAD: TStringField;
    QRLabel5: TQRLabel;
    QRShape2: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRBand2: TQRBand;
    QRExpr1: TQRExpr;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QryRelatorioNU_TELE_RESI_ALU: TStringField;
    QRLabel10: TQRLabel;
    lblTurma: TQRLabel;
    lblSemestre: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRDBText3: TQRDBText;
    QryRelatorioNO_RESP: TStringField;
    QRDBText4: TQRDBText;
    QryRelatorioCO_SEXO_ALU: TStringField;
    QRDBText5: TQRDBText;
    QryRelatorioDT_NASC_ALU: TDateTimeField;
    QRDBText6: TQRDBText;
    QRLIdade: TQRLabel;
    QrlMatricula: TQRLabel;
    QrlTelefone: TQRLabel;
    QRLParametros: TQRLabel;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioNO_TURMA: TStringField;
    QRLabel16: TQRLabel;
    QRLSituacao: TQRLabel;
    QryRelatorioCO_SIT_MAT: TStringField;
    QRLNoAlu: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacAluno: TFrmRelRelacAluno;

implementation

uses U_DataModuleSGE, U_Funcoes, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelRelacAluno.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
  var diasnoano : real;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';

  diasnoano := 365.6;

  QrlMatricula.Caption := FormatMaskText('00.000.0000.###;0', QryRelatorioCO_ALU_CAD.AsString);

  if (not QryRelatorioNU_TELE_RESI_ALU.IsNull) and (QryRelatorioNU_TELE_RESI_ALU.AsString <> '') then
  begin
    QrlTelefone.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorioNU_TELE_RESI_ALU.AsString);
  end
  else
  begin
    QrlTelefone.Caption := ' - ';
  end;

  If QryRelatorioCO_SIT_MAT.AsString = 'C' then
  begin
    QRLSituacao.Caption := 'CANCELADO';
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'T' then
  begin
    QRLSituacao.Caption := 'TRANCADO';
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'X' then
  begin
    QRLSituacao.Caption := 'TRANSFERIDO';
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'F' then
  begin
    QRLSituacao.Caption := 'FINALIZADO';
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'E' then
  begin
    QRLSituacao.Caption := 'EVADIDO';
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'P' then
  begin
    QRLSituacao.Caption := 'PENDENTE';
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'A' then
  begin
   QRLSituacao.Caption := 'EM ABERTO';
  end;

  with QryRelatorio do
  begin
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,fieldbyname('DT_NASC_ALU').AsDateTime) / diasnoano));
  end;

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacAluno]);

end.
