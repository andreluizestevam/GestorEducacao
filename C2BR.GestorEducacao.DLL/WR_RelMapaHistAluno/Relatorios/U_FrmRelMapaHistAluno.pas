unit U_FrmRelMapaHistAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  StdCtrls;

type
  TFrmRelMapaHistAluno = class(TFrmRelTemplate)
    QRGroup2: TQRGroup;
    QRBand1: TQRBand;
    QRDBText2: TQRDBText;
    QRBand2: TQRBand;
    QRLabel3: TQRLabel;
    QrlTotalDisc: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLDtProva: TQRLabel;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QrlTituloMatricula: TQRLabel;
    QrlTitSerie: TQRLabel;
    QrlNota: TQRLabel;
    QRLMatrAluno: TQRLabel;
    QRLValorNota: TQRLabel;
    QRShape5: TQRShape;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel15: TQRLabel;
    QRDBText4: TQRDBText;
    QrlMatricula: TQRLabel;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioCO_ANO_MES_MAT: TStringField;
    QryRelatorioNU_SEM_LET: TStringField;
    QryRelatorioCO_MODU_CUR: TIntegerField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioDT_PROV: TDateTimeField;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioVL_NOTA: TBCDField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioSIGLA: TWideStringField;
    QRDBText6: TQRDBText;
    QRDBText5: TQRDBText;
    QryRelatorioTP_AVAL: TStringField;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatorioNO_COL: TStringField;
    QRLabel14: TQRLabel;
    QrlProfessor: TQRLabel;
    QrlParam: TQRLabel;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorionu_nis: TBCDField;
    QryRelatoriono_tur: TStringField;
    QRLSerTur: TQRLabel;
    QryRelatorioNO_RED_MATERIA: TStringField;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;var PrintReport: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
  private
    FTotalDisc : Integer;
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMapaHistAluno: TFrmRelMapaHistAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaHistAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotalDisc.Caption := '';
  FTotalDisc := 0;
end;

procedure TFrmRelMapaHistAluno.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;

  QRLSerTur.Caption := QryRelatorioCO_SIGL_CUR.AsString + ' / ' + QryRelatoriono_tur.AsString;

  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;

  if not QryRelatorionu_nis.IsNull then
    QRLMatrAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString) + ' (Nº NIS ' + QryRelatorionu_nis.AsString + ')'
  else
    QRLMatrAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString);

  QrlProfessor.Caption := FormatMaskText('00.000-00;0', QryRelatorioCO_MAT_COL.AsString) + '- ' + UpperCase(QryRelatorioNO_COL.AsString);

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioCO_ALU_CAD.AsString);
//  QrlSerieTurma.Caption := QryRelatorioNO_CUR.AsString + ' / ' + QryRelatorioNO_TUR.AsString;

//  QrlTipoAval.Caption := QryRelatorioTP_AVAL.AsString;

{  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;
}
  FTotalDisc := FTotalDisc + 1 ;

  QRLDtProva.Caption := FormatDateTime('dd/mm/yy',qryRelatorio.FieldByname('dt_prov').AsDateTime);

  QRLValorNota.Caption := FormatFloat('0.00',QryRelatorio.FieldByName('VL_NOTA').AsFloat);

  if StrToFloat(QRLValorNota.Caption) >= 5 then
  begin
    QRLValorNota.Font.Color := clBlack;
  end
  else
  begin
    QrlValorNota.Font.Color := clRed;
  end;

end;

procedure TFrmRelMapaHistAluno.QRBand2BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalDisc.Caption := IntToStr(FTotalDisc);
  FTotalDisc := 0;
  
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaHistAluno]);

end.
