unit U_FrmRelMapaCursoTurmaProf;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaCursoTurmaProf = class(TFrmRelTemplate)
    QRLParametros: TQRLabel;
    QRBand1: TQRBand;
    QRDBText3: TQRDBText;
    QRDBText1: TQRDBText;
    QRLabel6: TQRLabel;
    QRLPage: TQRLabel;
    QRLCurFor: TQRLabel;
    QRLEspec: TQRLabel;
    QRDBText4: TQRDBText;
    QRGroup1: TQRGroup;
    QRLabel4: TQRLabel;
    QRShape3: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel5: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLColCurso: TQRLabel;
    QRLabel9: TQRLabel;
    QRBand2: TQRBand;
    QRLabel11: TQRLabel;
    QrlSerieTurma: TQRLabel;
    qryTotalProf: TADOQuery;
    qryTotalProfTotal: TIntegerField;
    QrlTotalProf: TQRLabel;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorioCO_ESPEC: TIntegerField;
    QryRelatorioCO_CURFORM: TIntegerField;
    QryRelatorioNO_TPCON: TStringField;
    QryRelatorioNU_TELE_RESI_COL: TStringField;
    QryRelatorioNU_TELE_CELU_COL: TStringField;
    QryRelatorioCO_SIGLA_TURMA: TStringField;
    QryRelatorioNO_RED_MATERIA: TStringField;
    QRDBText5: TQRDBText;
    QRLabel1: TQRLabel;
    QryRelatorioDE_MODU_CUR: TStringField;
    QRDBText6: TQRDBText;
    QryRelatorioCO_MODU_CUR: TIntegerField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioSITUACAO: TStringField;
    QRLNoCol: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa: String;
  end;

var
  FrmRelMapaCursoTurmaProf: TFrmRelMapaCursoTurmaProf;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaCursoTurmaProf.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString)
  else
    QRLNoCol.Caption := '-';

  QrlSerieTurma.Caption := QryRelatorioCO_SIGL_CUR.AsString + ' / ' + QryRelatorioCO_SIGLA_TURMA.AsString;

  QRLCurFor.Caption := '-';
  QRLEspec.Caption := '-';

  if not QryRelatorioCO_ESPEC.IsNull then
  begin
    with DM.QrySql do
    begin
      CLose;
      SQL.Clear;
      SQL.Text := 'select de_espec from tb100_especializacao '+
                  'where co_espec = ' + QryRelatorioCO_ESPEC.AsString;
      Open;

      if not IsEmpty then
        QRLEspec.Caption := FieldByName('de_espec').AsString;
    end;
  end;

  if not QryRelatorioCO_CURFORM.IsNull then
  begin
    with DM.QrySql do
    begin
      CLose;
      SQL.Clear;
      SQL.Text := 'select de_espec from tb100_especializacao '+
                  'where co_espec = ' + QryRelatorioCO_CURFORM.AsString;
      Open;

      if not IsEmpty then
        QRLCurFor.Caption := FieldByName('de_espec').AsString;
    end;
  end;

  If QRBand1.Color = ClWhite then
    QRBand1.Color := $00D8D8D8
  Else
    QRBand1.Color := ClWhite;

//  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelMapaCursoTurmaProf.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
//  QRLTotal.Caption := '0';
end;

procedure TFrmRelMapaCursoTurmaProf.QRBand2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  with qryTotalProf do
  begin
  Close;
  Sql.Clear;
  Sql.Text := ' SELECT COUNT(DISTINCT(P.CO_COL_RESP)) as Total '+
              '	FROM TB_RESPON_MATERIA P '+
              ' WHERE P.CO_EMP = ' + codigoEmpresa +
              ' AND P.CO_MODU_CUR = ' + QryRelatorioCO_MODU_CUR.AsString +
              ' AND P.CO_TUR = ' + QryRelatorioCO_TUR.AsString +
              ' AND P.CO_CUR = ' + QryRelatorioCO_CUR.AsString;
  Open;

  if not IsEmpty then
    QrlTotalProf.Caption := FormatMaskText('00;0', qryTotalProfTotal.AsString)
  else
    QrlTotalProf.Caption := '-';
  end;

end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaCursoTurmaProf]);

end.
