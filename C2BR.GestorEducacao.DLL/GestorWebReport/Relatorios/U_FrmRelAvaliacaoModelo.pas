unit U_FrmRelAvaliacaoModelo;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls;

type
  TFrmRelAvaliacaoModelo = class(TFrmRelTemplate)
    QRDBText1: TQRDBText;
    QRLSerie: TQRLabel;
    QRLDisciplina: TQRLabel;
    QRLNome: TQRLabel;
    QRShapeSerie: TQRShape;
    QRShapeDisciplina: TQRShape;
    QRShapeNome: TQRShape;
    QRDBTDescAval: TQRDBText;
    QRGroup2: TQRGroup;
    QRShape5: TQRShape;
    QRDBText2: TQRDBText;
    QRLabel3: TQRLabel;
    QRShape6: TQRShape;
    QRSubDetail2: TQRSubDetail;
    QRDBText4: TQRDBText;
    QRShape8: TQRShape;
    QRBand2: TQRBand;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRDBText10: TQRDBText;
    QryQuestaoTit: TADOQuery;
    QryQuestaoTitCO_TIPO_AVAL: TIntegerField;
    QryQuestaoTitCO_TITU_AVAL: TIntegerField;
    QryQuestaoTitNU_QUES_AVAL: TIntegerField;
    QryQuestaoTitDE_QUES_AVAL: TStringField;
    QRLabel1: TQRLabel;
    QRLPage: TQRLabel;
    QRShapeTurma: TQRShape;
    QRLTurma: TQRLabel;
    QRLModulo: TQRLabel;
    QRShapeModulo: TQRShape;
    QRLPublicoAlvo: TQRLabel;
    QRLNumPesq: TQRLabel;
    procedure QRGroup2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRSubDetail2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    strModulo, strSerie, strTurma, strDisciplina, strNome, strNumPesq : String;
  end;

var
  FrmRelAvaliacaoModelo: TFrmRelAvaliacaoModelo;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelAvaliacaoModelo.QRGroup2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  with QryQuestaoTit do
  begin
    Close;
    Parameters.ParamByName('P_CO_TIPO_AVAL').Value := QryRelatorio.FieldByName('CO_TIPO_AVAL').Value;
    Parameters.ParamByName('P_CO_TITU_AVAL').Value := QryRelatorio.FieldByName('CO_TITU_AVAL').Value;
    Open;
  end;
end;

procedure TFrmRelAvaliacaoModelo.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  if QryRelatorio.FieldByName('FLA_PUBLICO_ALVO').AsString = 'A' then
    QRLPublicoAlvo.Caption := 'Público Alvo: Aluno'
  else if QryRelatorio.FieldByName('FLA_PUBLICO_ALVO').AsString = 'F' then
    QRLPublicoAlvo.Caption := 'Público Alvo: Funcionário'
  else if QryRelatorio.FieldByName('FLA_PUBLICO_ALVO').AsString = 'P' then
    QRLPublicoAlvo.Caption := 'Público Alvo: Professor'
  else
    QRLPublicoAlvo.Caption := 'Público Alvo: Outros';

  QRLNumPesq.Caption := 'Nº ' + QryRelatorio.FieldByName('ANO_REFER').AsString + '.' + FormatFloat('00',QryRelatorio.FieldByName('MES_REFER').AsFloat) + '.' +
  FormatFloat('000',QryRelatorio.FieldByName('CO_TIPO_AVAL').AsFloat) + '.' + FormatFloat('0000',QryRelatorio.FieldByName('NU_AVAL_MASTER').AsFloat);

  if not QryRelatorio.FieldByName('de_modu_cur').IsNull then
  begin
    QRLModulo.Enabled := true;
    QRLModulo.Caption := 'Módulo: ' + QryRelatorio.FieldByName('DE_MODU_CUR').AsString;
  end
  else
  begin
    QRLModulo.Enabled := false;
  end;

  if not QryRelatorio.FieldByName('no_cur').IsNull then
  begin
    QRLSerie.Enabled := true;
    QRLSerie.Caption := 'Série: ' + QryRelatorio.FieldByName('NO_CUR').AsString;
  end
  else
  begin
    QRLSerie.Enabled := false;
  end;

  if not QryRelatorio.FieldByName('no_turma').IsNull then
  begin
    QRLTurma.Enabled := true;
    QRLTurma.Caption := 'Turma: ' + QryRelatorio.FieldByName('NO_TURMA').AsString;
  end
  else
  begin
    QRLTurma.Enabled := false;
  end;

  if not QryRelatorio.FieldByName('no_materia').IsNull then
  begin
    QRLDisciplina.Enabled := true;
    QRLDisciplina.Caption := 'Disciplina: ' + QryRelatorio.FieldByName('NO_MATERIA').AsString;
  end
  else
  begin
    QRLDisciplina.Enabled := false;
  end;
end;

procedure TFrmRelAvaliacaoModelo.QRSubDetail2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if QRSubDetail2.Color = clWhite then
    QRSubDetail2.Color := $00D8D8D8
  else
    QRSubDetail2.Color := clWhite;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelAvaliacaoModelo]);

end.
