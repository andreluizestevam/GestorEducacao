unit U_FrmRelRelacaoInativo;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacaoInativo = class(TFrmRelTemplate)
    QRGroup3: TQRGroup;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioDE_ENDE_ALU: TStringField;
    QryRelatorioNU_ENDE_ALU: TIntegerField;
    QryRelatorioDE_COMP_ALU: TStringField;
    QryRelatorioCO_ESTA_ALU: TStringField;
    QryRelatorioCO_CEP_ALU: TStringField;
    QryRelatorioNO_TUR: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioNU_SEM_LET: TStringField;
    QryRelatorioCO_ANO_MES_MAT: TStringField;
    QryRelatorioDE_MODU_CUR: TStringField;
    QryRelatorioSITUACAO: TStringField;
    QryRelatorioCO_BAIRRO: TIntegerField;
    QryRelatorioCO_CIDADE: TIntegerField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioNO_BAIRRO: TStringField;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QRDBText2: TQRDBText;
    QryRelatorioCO_SEXO_ALU: TStringField;
    QRDBText4: TQRDBText;
    QryRelatorioNO_RESP: TStringField;
    QryRelatorioNU_TELE_RESP: TStringField;
    QRExpr1: TQRExpr;
    QRLData: TQRLabel;
    QRLMatricula: TQRLabel;
    SummaryBand1: TQRBand;
    QrlTotal: TQRLabel;
    QrlSerieTurma: TQRLabel;
    QryRelatorioDT_NASC_ALU: TDateTimeField;
    QryRelatorioTP_DEF: TStringField;
    QryRelatorioDEFICIENCIA: TStringField;
    QRDBText3: TQRDBText;
    QrlIdade: TQRLabel;
    QryRelatorioSIGLA: TWideStringField;
    QRDBText5: TQRDBText;
    QryRelatorioDE_GRAU_PAREN: TStringField;
    QryRelatorioPARENTESCO: TStringField;
    QRDBText6: TQRDBText;
    QryRelatorioDT_SIT_MAT: TDateTimeField;
    QRLNuNis: TQRLabel;
    QRLParametros: TQRLabel;
    QRLabel9: TQRLabel;
    QRShape6: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QryRelatorioMATRICULAS: TIntegerField;
    QrlTotalMatriculas: TQRLabel;
    QrlTotalCompleto: TQRLabel;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorioNU_NIS: TBCDField;
    QRLNoAlu: TQRLabel;
    procedure QRGroup3BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacaoInativo: TFrmRelRelacaoInativo;

implementation

uses U_DataModuleSGE,MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelRelacaoInativo.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

procedure TFrmRelRelacaoInativo.QRGroup3BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';

  QRLNuNis.Caption := QryRelatorioNU_NIS.AsString;

  diasnoano := 365.6;
  QrlIdade.Caption := FormatMaskText('00;1', IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano)));

  QRLData.Caption := QryRelatorioSITUACAO.AsString + ' / ' + FormatDateTime('dd/mm/yy',QryRelatorio.FieldByName('DT_SIT_MAT').AsDateTime);

  if not(QryRelatorioCO_SIGL_CUR.IsNull) and not(QryRelatorioNO_TUR.IsNull) then
    QrlSerieTurma.Caption := QryRelatorioCO_SIGL_CUR.AsString + ' / ' + QryRelatorioNO_TUR.AsString;

  if QRGroup3.Color = clWhite then
     QRGroup3.Color := $00D8D8D8
  else
     QRGroup3.Color := clWhite;

  if not QryRelatorio.fieldByName('co_alu_cad').IsNull then
    QRLMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorio.fieldByName('co_alu_cad').AsString);

  QrlTotalMatriculas.Caption := QryRelatorioMATRICULAS.AsString;
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) + 1);

end;

procedure TFrmRelRelacaoInativo.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalCompleto.Caption := 'Total de Matriculas: ' + QrlTotalMatriculas.Caption + ' - Total de Alunos: ' + QrlTotal.Caption;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacaoInativo]);

end.
