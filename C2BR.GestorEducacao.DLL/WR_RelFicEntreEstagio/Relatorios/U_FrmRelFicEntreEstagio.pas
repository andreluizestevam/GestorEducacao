unit U_FrmRelFicEntreEstagio;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelFicEntreEstagio = class(TFrmRelTemplate)
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRShape1: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRShape2: TQRShape;
    QRLabel5: TQRLabel;
    QRDBText2: TQRDBText;
    QRShape3: TQRShape;
    QRLabel9: TQRLabel;
    QRShape5: TQRShape;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRDBText9: TQRDBText;
    QRShape6: TQRShape;
    QRLabel20: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel26: TQRLabel;
    QRLEndereco: TQRLabel;
    QRLCidUF: TQRLabel;
    QRLabel11: TQRLabel;
    QRDBText21: TQRDBText;
    QRLHrIni: TQRLabel;
    QRLHrFinal: TQRLabel;
    QRLRemun: TQRLabel;
    QRLabel2: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel3: TQRLabel;
    QRLEfet: TQRLabel;
    QRLabel6: TQRLabel;
    QRDBText4: TQRDBText;
    QRDBText6: TQRDBText;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLDiasSemana: TQRLabel;
    QRShape4: TQRShape;
    QRLabel13: TQRLabel;
    QRDBText7: TQRDBText;
    QRLabel14: TQRLabel;
    QRDBText8: TQRDBText;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLTelContato: TQRLabel;
    DetailBand1: TQRBand;
    QRLabel12: TQRLabel;
    QRDBText10: TQRDBText;
    QRLabel17: TQRLabel;
    QRDBText11: TQRDBText;
    QRLabel21: TQRLabel;
    QRLPubAlvo: TQRLabel;
    QRLabel27: TQRLabel;
    QRLIdades: TQRLabel;
    QRLabel29: TQRLabel;
    QRDBText12: TQRDBText;
    QRLabel30: TQRLabel;
    QRDBText13: TQRDBText;
    QRLabel31: TQRLabel;
    QRDBText16: TQRDBText;
    QRLabel32: TQRLabel;
    QRLDtIni: TQRLabel;
    QRLabel34: TQRLabel;
    QRLDtFim: TQRLabel;
    QRLCEPEmp: TQRLabel;
    QRShape7: TQRShape;
    QRLabel25: TQRLabel;
    QRLabel28: TQRLabel;
    QRShape8: TQRShape;
    QRLabel33: TQRLabel;
    QRDBText17: TQRDBText;
    QRLabel35: TQRLabel;
    QRLFormu: TQRLabel;
    QRLabel37: TQRLabel;
    QRLAvalEnt: TQRLabel;
    QRLNumEntrevista: TQRLabel;
    QRLabel42: TQRLabel;
    QRLResulEnt: TQRLabel;
    QRLabel44: TQRLabel;
    QRDBText19: TQRDBText;
    QRLHrEntre: TQRLabel;
    QRLabel49: TQRLabel;
    QRLCandidato: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelFicEntreEstagio: TFrmRelFicEntreEstagio;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelFicEntreEstagio.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLNumEntrevista.Caption := 'N� ' + FormatDateTime('yyyy',QryRelatorio.FieldByName('DT_ENTRE_ESTAGIO').AsDateTime) + '.' +
  FormatFloat('0000;0',QryRelatorio.FieldByName('CO_EMP').AsFloat) + '.' + FormatFloat('00000;0',QryRelatorio.FieldByName('ID_ENTRE_ESTAGIO').AsFloat);
  QRLCEPEmp.Caption := FormatMaskText('00000-000;0',QryRelatorio.FieldByName('CO_CEP_EMP_OFERT_ESTAG').AsString);
  if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'O' then
  begin
    QRLCandidato.Caption := UpperCase(QryRelatorio.FieldByName('NO_CANDID_ESTAG').AsString);
  end
  else
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'A' then
      begin
        SQL.Text := 'select top 1 a.no_alu as nome, a.dt_nasc_alu as dtNasc, a.co_sexo_alu as sexo, a.nu_tele_celu_alu as tel, m.co_alu_cad as matric from tb07_aluno a '+
                  'left join tb08_matrcur m on m.co_emp = a.co_emp and m.co_alu = a.co_alu ' +
                  'where a.co_emp = ' + QryRelatorio.FieldByName('CO_EMP_ALU').AsString +
                  ' and a.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' order by m.co_ano_mes_mat desc'
      end
      else
      begin
        SQL.Text := 'select no_col as nome, co_mat_col as matric, dt_nasc_col as dtNasc, co_sexo_col as sexo, nu_tele_celu_col as tel from tb03_colabor '+
                  'where co_emp = ' + QryRelatorio.FieldByName('CO_EMP_COL').AsString +
                  ' and co_col = ' +  QryRelatorio.FieldByName('CO_COL').AsString;
      end;
      Open;

      if not IsEmpty then
      begin
        QRLCandidato.Caption := UpperCase(FieldByName('nome').AsString);
      end
    end;
  end;

  if not QryRelatorio.FieldByName('HR_ENTRE_ESTAGIO').IsNull then
    QRLHrEntre.Caption := QryRelatorio.FieldByName('HR_ENTRE_ESTAGIO').AsString
  else
    QRLHrEntre.Caption := '-';

  if QryRelatorio.FieldByName('FLA_FORMU_AVAL').AsString = 'S' then
    QRLFormu.Caption := 'Sim'
  else
    QRLFormu.Caption := 'N�o';

  if not QryRelatorio.FieldByName('VL_NOTA_RESUL').IsNull then
    QRLAvalEnt.Caption := QryRelatorio.FieldByName('VL_NOTA_RESUL').AsString
  else
    QRLAvalEnt.Caption := '-';

  if QryRelatorio.FieldByName('FLA_RESUL').AsString = 'N' then
    QRLResulEnt.Caption := 'N�o Selecionado'
  else if QryRelatorio.FieldByName('FLA_RESUL').AsString = 'S' then
    QRLResulEnt.Caption := 'Selecionado'
  else if QryRelatorio.FieldByName('FLA_RESUL').AsString = 'E' then
    QRLResulEnt.Caption := 'Entrevista'
  else if QryRelatorio.FieldByName('FLA_RESUL').AsString = 'P' then
    QRLResulEnt.Caption := 'Processo de Sele��o'
  else if QryRelatorio.FieldByName('FLA_RESUL').AsString = 'F' then
    QRLResulEnt.Caption := 'Faltou'
  else
    QRLResulEnt.Caption := 'Cancelada';

  if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'A' then
    QRLPubAlvo.Caption := 'Alunos'
  else if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'F' then
    QRLPubAlvo.Caption := 'Funcion�rio'
  else if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'P' then
    QRLPubAlvo.Caption := 'Professor'
  else
    QRLPubAlvo.Caption := 'Outros';

  if not QryRelatorio.FieldByName('VL_IDADE_MIN').IsNull then
  begin
    QRLIdades.Caption := QryRelatorio.FieldByName('VL_IDADE_MIN').AsString;
    if not QryRelatorio.FieldByName('VL_IDADE_MAX').IsNull then
      QRLIdades.Caption := QRLIdades.Caption + ' � ' + QryRelatorio.FieldByName('VL_IDADE_MAX').AsString;
  end
  else
    QRLIdades.Caption := '-';

  if not QryRelatorio.FieldByName('DT_INI_OFERT_ESTAG').IsNull then
    QRLDtIni.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('DT_INI_OFERT_ESTAG').AsDateTime)
  else
    QRLDtIni.Caption := '-';

  if not QryRelatorio.FieldByName('DT_FIM_OFERT_ESTAG').IsNull then
    QRLDtFim.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('DT_FIM_OFERT_ESTAG').AsDateTime)
  else
    QRLDtFim.Caption := '-';

  QRLEndereco.Caption := QryRelatorio.FieldByName('DE_END_EMP_OFERT_ESTAG').AsString + ', N� ' + QryRelatorio.FieldByName('NU_END_EMP_OFERT_ESTAG').AsString;

  if QryRelatorio.FieldByName('FLA_DIA_SEMANA').AsString = 'SSE' then
    QRLDiasSemana.Caption := 'Segunda �Sexta'
  else if QryRelatorio.FieldByName('FLA_DIA_SEMANA').AsString = 'SSA' then
    QRLDiasSemana.Caption := 'Segunda �S�bado'
  else if QryRelatorio.FieldByName('FLA_DIA_SEMANA').AsString = 'OUT' then
    QRLDiasSemana.Caption := 'Outros'
  else QRLDiasSemana.Caption := '-';

  if not QryRelatorio.FieldByName('DE_COMP_EMP_OFERT_ESTAG').IsNull then
    QRLEndereco.Caption := QRLEndereco.Caption + ', ' + QryRelatorio.FieldByName('DE_COMP_EMP_OFERT_ESTAG').AsString;

  QRLCidUF.Caption := QryRelatorio.FieldByName('CO_UF_EMP_OFERT_ESTAG').AsString + ' / ' + QryRelatorio.FieldByName('NO_CIDADE').AsString;

  if not QryRelatorio.FieldByName('HR_INI_OFERT_ESTAG').IsNull then
  begin
    QRLHrIni.Caption := FormatFloat('00:00',QryRelatorio.FieldByName('HR_INI_OFERT_ESTAG').AsFloat);
  end
  else
    QRLHrIni.Caption := '-';

  if not QryRelatorio.FieldByName('HR_FIM_OFERT_ESTAG').IsNull then
  begin
    QRLHrFinal.Caption := FormatFloat('00:00',QryRelatorio.FieldByName('HR_FIM_OFERT_ESTAG').AsFloat);
  end
  else
    QRLHrFinal.Caption := '-';

  if not QryRelatorio.FieldByName('remunEnt').IsNull then
    QRLRemun.Caption := 'R$ ' + FloatToStrF(QryRelatorio.FieldByName('remunEnt').AsFloat,ffNumber,15,2)
  else
  begin
    if not QryRelatorio.FieldByName('VL_REMUN').IsNull then
      QRLRemun.Caption := 'R$ ' + FloatToStrF(QryRelatorio.FieldByName('VL_REMUN').AsFloat,ffNumber,15,2)
    else
      QRLRemun.Caption := 'N�o';
  end;

  if QryRelatorio.FieldByName('FLA_POSS_EFETI_OFERT_ESTAG').AsString = 'S' then
    QRLEfet.Caption := 'Sim'
  else
    QRLEfet.Caption := 'N�o';

  if not QryRelatorio.FieldByName('NU_TELE_CONTA_OFERT_ESTAG').IsNull then
    QRLTelContato.Caption := FormatFloat('(##) ####-####',QryRelatorio.FieldByName('NU_TELE_CONTA_OFERT_ESTAG').AsFloat)
  else
    QRLTelContato.Caption := '-';
end;

end.
