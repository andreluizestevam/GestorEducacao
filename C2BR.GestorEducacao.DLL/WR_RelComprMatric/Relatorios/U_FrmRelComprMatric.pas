unit U_FrmRelComprMatric;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, jpeg, QuickRpt, ExtCtrls, DateUtil;

type
  TFrmRelComprMatric = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRShape3: TQRShape;
    QRLCabAss: TQRLabel;
    QRLNumMatricula: TQRLabel;
    QRShape2: TQRShape;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLTurma: TQRLabel;
    lblCurso: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel11: TQRLabel;
    lblResponsavel: TQRLabel;
    lblDataInclusao: TQRLabel;
    QRLabel12: TQRLabel;
    QRShape1: TQRShape;
    QRLabel20: TQRLabel;
    QRShape5: TQRShape;
    QRLabel13: TQRLabel;
    QRShape4: TQRShape;
    lblAlunoNome: TQRLabel;
    QRLabel14: TQRLabel;
    lblDtNascimento: TQRLabel;
    QRLabel7: TQRLabel;
    QRLSxAluno: TQRLabel;
    QRLabel16: TQRLabel;
    lblAlunoEndereco: TQRLabel;
    QRLabel17: TQRLabel;
    QRLAluUFCidade: TQRLabel;
    QRLabel18: TQRLabel;
    lblAlunoTelefone: TQRLabel;
    QRLabel21: TQRLabel;
    lblRespNome: TQRLabel;
    QRLabel22: TQRLabel;
    lblRespCPF: TQRLabel;
    QRLabel23: TQRLabel;
    lblRespRG: TQRLabel;
    QRLabel24: TQRLabel;
    lblRespEndereco: TQRLabel;
    QRLabel25: TQRLabel;
    QRLUFCidadeResp: TQRLabel;
    QRLabel26: TQRLabel;
    lblRespTelefone: TQRLabel;
    QRLabel2: TQRLabel;
    QRLModulo: TQRLabel;
    QRLabel3: TQRLabel;
    QRLBairroResp: TQRLabel;
    QRLAluBairro: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel15: TQRLabel;
    QRLMatCol: TQRLabel;
    QRLabel1: TQRLabel;
    QRLAnoMat: TQRLabel;
    QRLabel28: TQRLabel;
    QRShape6: TQRShape;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBANDSGIEBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    descUsu : String;
  end;

var
  FrmRelComprMatric: TFrmRelComprMatric;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelComprMatric.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLNumMatricula.Caption := 'Matr�cula ' + FormatMaskText('00.000.000000;0',QryRelatorio.FieldByName('CO_ALU_CAD').AsString);

  if not QryRelatorio.FieldByName('DT_EFE_MAT').IsNull then
    lblDataInclusao.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_EFE_MAT').AsDateTime)
  else
    lblDataInclusao.Caption := '-';

  if not QryRelatorio.FieldByName('co_mat_col').IsNull then
    QRLMatCol.Caption := FormatMaskText('99.999-9;0',QryRelatorio.FieldByName('co_mat_col').AsString)
  else
    QRLMatCol.Caption := '-';

  if not QryRelatorio.FieldByName('no_col').IsNull then
    lblResponsavel.Caption := QryRelatorio.FieldByName('no_col').AsString
  else
    lblResponsavel.Caption := '-';

  if not QryRelatorio.FieldByName('CO_ANO_MES_MAT').IsNull then
    QRLAnoMat.Caption := QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString
  else
    QRLAnoMat.Caption := '-';

  if not QryRelatorio.FieldByName('de_modu_cur').IsNull then
    QRLModulo.Caption := QryRelatorio.FieldByName('de_modu_cur').AsString
  else
    QRLModulo.Caption := '-';

  if not QryRelatorio.FieldByName('no_cur').IsNull then
    lblCurso.Caption := QryRelatorio.FieldByName('no_cur').AsString
  else
    lblCurso.Caption := '-';

  if not QryRelatorio.FieldByName('no_turma').IsNull then
    QRLTurma.Caption := QryRelatorio.FieldByName('no_turma').AsString
  else
    QRLTurma.Caption := '-';

  //*************** Informa��es do aluno
  lblAlunoNome.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString);

  if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
    lblDtNascimento.Caption := QryRelatorio.FieldByName('DT_NASC_ALU').AsString
  else
    lblDtNascimento.Caption := '-';

  if QryRelatorio.FieldByName('CO_SEXO_ALU').AsString = 'M' then
    QRLSxAluno.Caption := 'Masculino'
  else
    QRLSxAluno.Caption := 'Feminino';

  if (not QryRelatorio.FieldByName('DE_ENDE_ALU').IsNull) and (QryRelatorio.FieldByName('DE_ENDE_ALU').AsString <> '') then
  begin
    lblAlunoEndereco.Caption := QryRelatorio.FieldByName('DE_ENDE_ALU').AsString;
    if (not QryRelatorio.FieldByName('NU_ENDE_ALU').IsNull) and (QryRelatorio.FieldByName('NU_ENDE_ALU').AsString <> '') then
      lblAlunoEndereco.Caption := lblAlunoEndereco.Caption + ', ' + QryRelatorio.FieldByName('NU_ENDE_ALU').AsString;

    if (not QryRelatorio.FieldByName('DE_COMP_ALU').IsNull) and (QryRelatorio.FieldByName('DE_COMP_ALU').AsString <> '') then
      lblAlunoEndereco.Caption := lblAlunoEndereco.Caption + ' - ' + QryRelatorio.FieldByName('DE_COMP_ALU').AsString;
  end
  else
    lblAlunoEndereco.Caption := '-';

  if not QryRelatorio.FieldByName('baiAluno').IsNull then
    QRLAluBairro.Caption := QryRelatorio.FieldByName('baiAluno').AsString
  else
    QRLAluBairro.Caption := '-';

  if not QryRelatorio.FieldByName('UFAlu').IsNull then
    QRLAluUFCidade.Caption := QryRelatorio.FieldByName('UFAlu').AsString
  else
    QRLAluUFCidade.Caption := '** / ';

  if not QryRelatorio.FieldByName('cidAluno').IsNull then
    QRLAluUFCidade.Caption := QRLAluUFCidade.Caption + ' / ' + QryRelatorio.FieldByName('cidAluno').AsString
  else
    QRLAluUFCidade.Caption := QRLAluUFCidade.Caption + '***';

  if (not QryRelatorio.FieldByName('NU_TELE_RESI_ALU').IsNull) and (QryRelatorio.FieldByName('NU_TELE_RESI_ALU').AsString <> '') then
  begin
    lblAlunoTelefone.Caption := FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_RESI_ALU').AsString);
    if (not QryRelatorio.FieldByName('NU_TELE_CELU_ALU').IsNull) and (QryRelatorio.FieldByName('NU_TELE_CELU_ALU').AsString <> '') then
      lblAlunoTelefone.Caption := lblAlunoTelefone.Caption + ' / ' + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_CELU_ALU').AsString);
  end
  else
  begin
    if (not QryRelatorio.FieldByName('NU_TELE_CELU_ALU').IsNull) and (QryRelatorio.FieldByName('NU_TELE_CELU_ALU').AsString <> '') then
      lblAlunoTelefone.Caption := FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_CELU_ALU').AsString)
    else
      lblAlunoTelefone.Caption := '-';
  end;
  //***************************************************

  //*************** Informa��es do respons�vel
  lblRespNome.Caption := QryRelatorio.FieldByName('NO_RESP').AsString;

  if not QryRelatorio.FieldByName('NU_CPF_RESP').IsNull then
    lblRespCPF.Caption := FormatMaskText('000.000.000-00;0', QryRelatorio.FieldByName('NU_CPF_RESP').AsString)
  else
    lblRespCPF.Caption := '-';

  lblRespRG.Caption := '-';

  if (not QryRelatorio.FieldByName('DE_ENDE_RESP').IsNull) and (QryRelatorio.FieldByName('DE_ENDE_RESP').AsString <> '') then
  begin
    lblRespEndereco.Caption := QryRelatorio.FieldByName('DE_ENDE_RESP').AsString;
    if (not QryRelatorio.FieldByName('NU_ENDE_RESP').IsNull) and (QryRelatorio.FieldByName('NU_ENDE_RESP').AsString <> '') then
      lblRespEndereco.Caption := lblRespEndereco.Caption + ', ' + QryRelatorio.FieldByName('NU_ENDE_RESP').AsString;

    if (not QryRelatorio.FieldByName('DE_COMP_RESP').IsNull) and (QryRelatorio.FieldByName('DE_COMP_RESP').AsString <> '') then
      lblRespEndereco.Caption := lblRespEndereco.Caption + ' - ' + QryRelatorio.FieldByName('DE_COMP_RESP').AsString;
  end
  else
    lblRespEndereco.Caption := '-';

  if not QryRelatorio.FieldByName('baiResp').IsNull then
    QRLBairroResp.Caption := QryRelatorio.FieldByName('baiResp').AsString
  else
    QRLBairroResp.Caption := '-';

  if not QryRelatorio.FieldByName('UFResp').IsNull then
    QRLUFCidadeResp.Caption := QryRelatorio.FieldByName('UFResp').AsString
  else
    QRLUFCidadeResp.Caption := '** / ';

  if not QryRelatorio.FieldByName('cidResp').IsNull then
    QRLUFCidadeResp.Caption := QRLUFCidadeResp.Caption + ' / ' + QryRelatorio.FieldByName('cidResp').AsString
  else
    QRLUFCidadeResp.Caption := QRLUFCidadeResp.Caption + '***';

  if (not QryRelatorio.FieldByName('NU_TELE_RESI_RESP').IsNull) and (QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString <> '') then
  begin
    lblRespTelefone.Caption := FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString);
    if (not QryRelatorio.FieldByName('NU_TELE_CELU_RESP').IsNull) and (QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString <> '') then
      lblRespTelefone.Caption := lblRespTelefone.Caption + ' / ' + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString);
  end
  else
  begin
    if (not QryRelatorio.FieldByName('NU_TELE_CELU_RESP').IsNull) and (QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString <> '') then
      lblRespTelefone.Caption := lblRespTelefone.Caption + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString)
    else
      lblRespTelefone.Caption := '-';
  end;
  //***************************************************
end;

procedure TFrmRelComprMatric.QRBANDSGIEBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select c.no_cidade from tb25_empresa e ' +
                'join tb904_cidade c on c.co_cidade = e.co_cidade ' +
                'where e.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString;
    Open;

    if not IsEmpty then
    begin
      QRLCabAss.Caption := FieldByName('no_cidade').AsString + ', ' + FormatDateTime('dd "de" mmmm "de" yyyy', Now);
    end
    else
      QRLCabAss.Caption := '';
  end;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelComprMatric]);

end.