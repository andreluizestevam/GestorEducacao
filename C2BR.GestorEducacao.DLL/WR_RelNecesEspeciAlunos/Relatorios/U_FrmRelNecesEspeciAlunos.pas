unit U_FrmRelNecesEspeciAlunos;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelNecesEspeciAlunos = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLabel2: TQRLabel;
    QRBand1: TQRBand;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLNoAlu: TQRLabel;
    SummaryBand1: TQRBand;
    QRLSitu: TQRLabel;
    QRLParam: TQRLabel;
    QRBand2: TQRBand;
    QRLabel5: TQRLabel;
    QRShape6: TQRShape;
    QRLMatricula: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel1: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLDtCadastro: TQRLabel;
    QRLAcao: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelNecesEspeciAlunos: TFrmRelNecesEspeciAlunos;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelNecesEspeciAlunos.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('DT_CADAS_NECES_ESPEC').IsNull then
    QRLDtCadastro.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_CADAS_NECES_ESPEC').AsDateTime)
  else
    QRLDtCadastro.Caption := '-';

  if QryRelatorio.FieldByName('TP_ACAO_NECES_ESPEC_ALUNO').AsString = 'AP' then
    QRLAcao.Caption := 'Aplicar'
  else if QryRelatorio.FieldByName('TP_ACAO_NECES_ESPEC_ALUNO').AsString = 'OB' then
    QRLAcao.Caption := 'Observar'
  else if QryRelatorio.FieldByName('TP_ACAO_NECES_ESPEC_ALUNO').AsString = 'AC' then
    QRLAcao.Caption := 'Acompanhar'
  else if QryRelatorio.FieldByName('TP_ACAO_NECES_ESPEC_ALUNO').AsString = 'LR' then
    QRLAcao.Caption := 'Ligar para o Responsável'
  else if QryRelatorio.FieldByName('TP_ACAO_NECES_ESPEC_ALUNO').AsString = 'EM' then
    QRLAcao.Caption := 'Encaminhar ao médico'
  else if QryRelatorio.FieldByName('TP_ACAO_NECES_ESPEC_ALUNO').AsString = 'EH' then
    QRLAcao.Caption := 'Encaminhar ao Hospital'
  else
    QRLAcao.Caption := 'Nenhuma';

  if QryRelatorio.FieldByName('CO_SITUA_NECES_ESPEC').AsString = 'A' then
    QRLSitu.Caption := 'Ativo - ' + FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_SITUA_NECES_ESPEC').AsDateTime)
  else
    QRLSitu.Caption := 'Inativo - ' + FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_SITUA_NECES_ESPEC').AsDateTime);

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;
end;

procedure TFrmRelNecesEspeciAlunos.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';
  {
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select top 1 mm.co_alu_cad, c.no_cur, mm.CO_TURN_MAT, m.de_modu_cur, ct.CO_SIGLA_TURMA ' +
                'from tb08_matrcur mm ' +
                ' join tb44_modulo m on m.co_modu_cur = mm.co_modu_cur ' +
                ' join tb01_curso c on c.co_modu_cur = mm.co_modu_cur and c.co_cur = mm.co_cur and c.co_emp = mm.co_emp ' +
                ' join tb06_turmas t on t.co_modu_cur = mm.co_modu_cur and t.co_cur = mm.co_cur and t.co_emp = mm.co_emp and t.co_tur = mm.co_tur ' +
                ' join tb129_cadturmas ct on ct.co_tur = t.co_tur ' +
                ' where mm.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' and mm.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' order by mm.co_ano_mes_mat desc';
    Open;

    if not IsEmpty then
    begin
      if FieldByName('CO_TURN_MAT').AsString = 'M' then
        turno := 'Matutino'
      else if FieldByName('CO_TURN_MAT').AsString = 'N' then
        turno := 'Noturno'
      else
        turno := 'Vespertino';

      QRLMatricula.Caption := '(Matr.: ' +  FormatMaskText('00.000.000000;0',FieldByName('CO_ALU_CAD').AsString) + ' - Modalidade: ' + FieldByName('DE_MODU_CUR').AsString  + ' - Série: ' + FieldByName('NO_CUR').AsString + ' - Turma: ' + FieldByName('CO_SIGLA_TURMA').AsString + ' - Turno: ' + turno + ')';
    end
    else
      QRLMatricula.Caption := '(Sem registro de Matrícula)';
  end;  }

end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelNecesEspeciAlunos]);

end.
