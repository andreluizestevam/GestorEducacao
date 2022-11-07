unit U_FrmRelExtSolicitAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelExtSolicitAluno = class(TFrmRelTemplate)
    QRLPeriodo: TQRLabel;
    QryRelatorioCO_SOLI_ATEN: TIntegerField;
    QryRelatorioDT_SOLI_ATEN: TDateTimeField;
    QryRelatorioDT_PREV_ENTR: TDateTimeField;
    QryRelatorioDT_FIM_SOLI: TDateTimeField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioNO_TIPO_SOLI: TStringField;
    QryRelatorioCO_SIT_SOLI: TStringField;
    QryRelatorioVA_SOLI_ATEN: TBCDField;
    QRLabel1: TQRLabel;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_TIPO_SOLI: TIntegerField;
    QRBand1: TQRBand;
    QRLabel13: TQRLabel;
    QRBand2: TQRBand;
    QRDBText3: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText8: TQRDBText;
    QRShape1: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLTotalAluno: TQRLabel;
    QRLabel2: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel12: TQRLabel;
    QRDBText9: TQRDBText;
    QrlTitSerieTurma: TQRLabel;
    QRLabelDataAbreviada: TQRLabel;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioMES_SOLI_ATEN: TIntegerField;
    QryRelatorioANO_SOLI_ATEN: TIntegerField;
    qrlSolic: TQRLabel;
    QrlMatriNis: TQRLabel;
    QrlSerieTurma: TQRLabel;
    QryRelatorioDE_OBS_SOLI: TStringField;
    QRLabel7: TQRLabel;
    QRDBText2: TQRDBText;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorioNU_NIS: TBCDField;
    QryRelatorioNO_TUR: TStringField;
    QryRelatorioNU_DCTO_SOLIC: TStringField;
    QRLNoAlu: TQRLabel;
    procedure QRDBText8Print(sender: TObject; var Value: String);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    Cont, ContAlunos : Integer;
    TotalAluno : Integer;
  public
    { Public declarations }
  end;

var
  FrmRelExtSolicitAluno: TFrmRelExtSolicitAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelExtSolicitAluno.QRDBText8Print(sender: TObject;
  var Value: String);
begin
  inherited;
  with QryRelatorio do
  begin
    if FieldByName('CO_SIT_SOLI').AsString = 'A' then
      Value := 'Em Aberto';

    if FieldByName('CO_SIT_SOLI').AsString = 'T' then
      Value := 'Em Trâmite';

    if FieldByName('CO_SIT_SOLI').AsString = 'F' then
      Value := 'Finalizada';

    if FieldByName('CO_SIT_SOLI').AsString = 'E' then
      Value := 'Entregue';

    if FieldByName('CO_SIT_SOLI').AsString = 'C' then
      Value := 'Cancelada';
  end;
end;

procedure TFrmRelExtSolicitAluno.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  Cont := 0;
end;

procedure TFrmRelExtSolicitAluno.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  ContAlunos := 0;
  TotalAluno := 0;
end;

procedure TFrmRelExtSolicitAluno.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

//  QRLNuNis.Caption := FormatMaskText('00.000-0;0',nu_nis);

  QrlMatriNis.Caption := 'Matrícula: ' + FormatMaskText('00.000.000000;0', QryRelatorioCO_ALU_CAD.AsString) + ' - Nº NIS: ' + QryRelatorioNU_NIS.AsString;

  QrlSerieTurma.Caption := QryRelatorioCO_SIGL_CUR.AsString + ' / ' + QryRelatorioNO_TUR.AsString;

  qrlSolic.Caption := QryRelatorioNU_DCTO_SOLIC.AsString;

  Cont := Cont + 1;
  TotalAluno := TotalAluno + 1;

  if QRBand2.Color = clWhite then
     QRBand2.Color := $00D8D8D8
  else
     QRBand2.Color := clWhite;
end;

procedure TFrmRelExtSolicitAluno.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotalAluno.Caption := IntToStr(TotalAluno);

end;

procedure TFrmRelExtSolicitAluno.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  ano,mes,dia: word;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
//  if Sys_DescricaoTipoCurso = 'EP' then

  QrlTitSerieTurma.Caption := 'SÉRIE/TURMA';

  //adicionado em: sex-06.set.08
  //adicionado por: JJJr

  //código novo
  DecodeDate(Now, ano, mes, dia);
  ano := StrToInt(Copy(IntToStr(ano),2,4));
  QRLabelDataAbreviada.Caption := FormatFloat('00',dia) + '/' + FormatFloat('00',mes) + '/' + FormatFloat('00',ano);
  //fim código novo
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelExtSolicitAluno]);

end.
