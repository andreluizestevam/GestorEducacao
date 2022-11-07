unit U_FrmRelEndereAlunos;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelEndereAlunos = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLabel2: TQRLabel;
    QRBand1: TQRBand;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLNoAlu: TQRLabel;
    SummaryBand1: TQRBand;
    QRLEndereco: TQRLabel;
    QRLParam: TQRLabel;
    QRBand2: TQRBand;
    QRLabel5: TQRLabel;
    QRShape6: TQRShape;
    QRLTpEndereco: TQRLabel;
    QRLMatricula: TQRLabel;
    QRShape2: TQRShape;
    QRLTelefones: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelEndereAlunos: TFrmRelEndereAlunos;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelEndereAlunos.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('CO_TIPO_ENDERECO').IsNull then
  begin
    QRLTpEndereco.Caption := QryRelatorio.FieldByName('CO_TIPO_ENDERECO').AsString + ':';
  end;

  QRLEndereco.Caption := '';

  if not QryRelatorio.FieldByName('no_bairro').IsNull then
  begin
    if not QryRelatorio.FieldByName('DS_ENDERECO').IsNull then
      QRLEndereco.Caption := QryRelatorio.FieldByName('DS_ENDERECO').AsString;
    if not QryRelatorio.FieldByName('NR_ENDERECO').IsNull then
      QRLEndereco.Caption := QRLEndereco.Caption + ' - ' + QryRelatorio.FieldByName('NR_ENDERECO').AsString;
    if not QryRelatorio.FieldByName('DS_COMPLEMENTO').IsNull then
      QRLEndereco.Caption := QRLEndereco.Caption + ' - ' + QryRelatorio.FieldByName('DS_COMPLEMENTO').AsString;

    QRLEndereco.Caption := QRLEndereco.Caption + ' - ' + QryRelatorio.FieldByName('no_bairro').AsString + ' - ' + QryRelatorio.FieldByName('no_cidade').AsString + ' - ' + QryRelatorio.FieldByName('CO_UF').AsString;
    QRLEndereco.Caption := QRLEndereco.Caption + ' - ' + FormatMaskText('00000-000;0',QryRelatorio.FieldByName('CO_CEP').AsString);
  end
  else
    QRLEndereco.Caption := '-';

  if QryRelatorio.FieldByName('CO_SITUACAO').AsString = 'A' then
    QRLEndereco.Caption := QRLEndereco.Caption + ' - (Ativo ' + FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_SITUACAO').AsDateTime) + ')'
  else
    QRLEndereco.Caption := QRLEndereco.Caption + ' - (Inativo ' + FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_SITUACAO').AsDateTime) + ')';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;
end;

procedure TFrmRelEndereAlunos.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  turno : String;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';

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
  end;

end;

procedure TFrmRelEndereAlunos.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  controle : integer;
begin
  inherited;
  controle := 0;
  
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select at.NR_DDD, at.NR_TELEFONE, tt.CO_TIPO_TELEFONE ' +
                'from TB242_ALUNO_TELEFONE at ' +
                'join TB239_TIPO_TELEFONE tt on tt.ID_TIPO_TELEFONE = at.ID_TIPO_TELEFONE ' +
                ' where at.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' and at.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' and at.CO_SITUACAO = ' + QuotedStr('A');
    Open;

    QRLTelefones.Caption := 'Telefone(s): ';

    if IsEmpty then
      QRLTelefones.Caption := QRLTelefones.Caption + '*****';

    while not Eof do
    begin
      if controle = 0 then
        QRLTelefones.Caption := QRLTelefones.Caption + '(' + FormatFloat('00',FieldByName('NR_DDD').AsFloat) + ') ' + FormatMaskText('0000-0000;0',FieldByName('NR_TELEFONE').AsString) + ' (' + FieldByName('CO_TIPO_TELEFONE').AsString + ')'
      else
        QRLTelefones.Caption := QRLTelefones.Caption + ' / (' + FormatFloat('00',FieldByName('NR_DDD').AsFloat) + ') ' + FormatMaskText('0000-0000;0',FieldByName('NR_TELEFONE').AsString) + ' (' + FieldByName('CO_TIPO_TELEFONE').AsString + ')';

      controle := 1;
      Next;
    end;
  end;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelEndereAlunos]);

end.
