unit U_FrmRelMapaHistAlunoSerTur;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  StdCtrls;

type
  TFrmRelMapaHistAlunoSerTur = class(TFrmRelTemplate)
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel3: TQRLabel;
    QrlTotal: TQRLabel;
    QRGroup1: TQRGroup;
    QRLMatrAluno: TQRLabel;
    QRShape5: TQRShape;
    DetailBand2: TQRBand;
    QRDBText4: TQRDBText;
    QrlMatricula: TQRLabel;
    QRDBText2: TQRDBText;
    QRLDtProva: TQRLabel;
    QRDBText5: TQRDBText;
    QRLValorNota: TQRLabel;
    QRDBText6: TQRDBText;
    QRLabel2: TQRLabel;
    QrlTituloMatricula: TQRLabel;
    QrlTitSerie: TQRLabel;
    QrlNota: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel15: TQRLabel;
    QRShape1: TQRShape;
    QrlProfessor: TQRLabel;
    QRLabel16: TQRLabel;
    QrlParam: TQRLabel;
    QRLSerTur: TQRLabel;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure DetailBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMapaHistAlunoSerTur: TFrmRelMapaHistAlunoSerTur;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaHistAlunoSerTur.QuickRep1BeforePrint(Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

procedure TFrmRelMapaHistAlunoSerTur.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorio.FieldByName('nu_nis').IsNull then
    QRLMatrAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString) + ' (Nº NIS ' + QryRelatorio.FieldByName('nu_nis').AsString + ')'
  else
    QRLMatrAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString);

  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);
end;

procedure TFrmRelMapaHistAlunoSerTur.DetailBand2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  if DetailBand2.Color = clWhite then
     DetailBand2.Color := $00D8D8D8
  else
     DetailBand2.Color := clWhite;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := ' SELECT C.NO_COL,C.CO_MAT_COL    ' +
                   ' FROM TB_RESPON_MATERIA P ' +
                   'JOIN TB03_COLABOR C on P.CO_COL_RESP = C.CO_COL AND P.CO_EMP = C.CO_EMP ' +
                   'JOIN TB02_MATERIA M on M.CO_MODU_CUR = P.CO_MODU_CUR AND M.CO_CUR = P.CO_MODU_CUR ' +
                   'AND P.CO_MAT = M.CO_MAT AND P.CO_EMP = M.CO_EMP ' +
                   ' WHERE P.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP_ALU').AsString +
                   ' AND P.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                   ' AND P.CO_TUR = ' + QryRelatorio.FieldByName('CO_TUR').AsString +
                   ' AND M.ID_MATERIA = ' + QryRelatorio.FieldByName('ID_MATERIA').AsString +
                   ' AND P.CO_MODU_CUR = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                   ' AND P.CO_ANO_REF = ' + QryRelatorio.FieldByName('CO_ANO').AsString;
    Open;

    if not IsEmpty then
    begin
      QrlProfessor.Caption := FormatMaskText('00.000-00;0', FieldByName('CO_MAT_COL').AsString) + '- ' + UpperCase(FieldByName('NO_COL').AsString);
    end
    else
      QrlProfessor.Caption := '**********';
  end;

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorio.FieldByName('CO_ALU_CAD').AsString);
  QRLSerTur.Caption := QryRelatorio.FieldByName('NO_CUR').AsString + ' / ' + QryRelatorio.FieldByName('NO_TUR').AsString;

  QRLDtProva.Caption := FormatDateTime('dd/mm/yy',qryRelatorio.FieldByname('DT_NOTA_ATIV').AsDateTime);

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

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaHistAlunoSerTur]);

end.
