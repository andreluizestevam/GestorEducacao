unit U_FrmRelExtIndEstagio;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelExtIndEstagio = class(TFrmRelTemplate)
    QRLabel2: TQRLabel;
    QRLPage: TQRLabel;
    QRLabelDataAbreviada: TQRLabel;
    QRLabel20: TQRLabel;
    QRLHrIni: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel18: TQRLabel;
    QRShape5: TQRShape;
    QRLabel30: TQRLabel;
    QRDBText13: TQRDBText;
    QRDBText9: TQRDBText;
    QRLabel19: TQRLabel;
    QRLabel29: TQRLabel;
    QRDBText12: TQRDBText;
    QRLabel21: TQRLabel;
    QRLPubAlvo: TQRLabel;
    QRLabel27: TQRLabel;
    QRLIdades: TQRLabel;
    QRLabel3: TQRLabel;
    QRLEfet: TQRLabel;
    QRLabel8: TQRLabel;
    QRLDiasSemana: TQRLabel;
    QRLabel1: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel10: TQRLabel;
    QRShape1: TQRShape;
    QRLabel4: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel5: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel9: TQRLabel;
    QRLEndereco: TQRLabel;
    QRLabel11: TQRLabel;
    QRLCEPEmp: TQRLabel;
    QRDBText10: TQRDBText;
    QRLabel12: TQRLabel;
    QRLabel17: TQRLabel;
    QRLCidUF: TQRLabel;
    QRLabel26: TQRLabel;
    QRShape3: TQRShape;
    QRLabel14: TQRLabel;
    QRDBText8: TQRDBText;
    QRLabel15: TQRLabel;
    QRDBText11: TQRDBText;
    QRLabel16: TQRLabel;
    QRLTelContato: TQRLabel;
    QRLRemun: TQRLabel;
    QRLabel42: TQRLabel;
    QRLTpEstag: TQRLabel;
    QRLTitIdent: TQRLabel;
    QRLIdent: TQRLabel;
    QRLNumContEstag: TQRLabel;
    QRLabel49: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel28: TQRLabel;
    QRLCandidato: TQRLabel;
    QRShape8: TQRShape;
    QRShape7: TQRShape;
    QRLabel25: TQRLabel;
    QRLHrFinal: TQRLabel;
    QRLabel6: TQRLabel;
    QRLDtIniEstag: TQRLabel;
    QRLabel13: TQRLabel;
    QRLDtFimEstag: TQRLabel;
    QRLabel24: TQRLabel;
    QRLDtContEstag: TQRLabel;
    QRBand1: TQRBand;
    QRBand2: TQRBand;
    qryAvaliacoes: TADOQuery;
    qryOcorrencias: TADOQuery;
    QRLabel32: TQRLabel;
    QRShape2: TQRShape;
    QRShape4: TQRShape;
    qrlTpAval: TQRLabel;
    QRDBText5: TQRDBText;
    QrlOrigem: TQRLabel;
    QRLabel34: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel31: TQRLabel;
    QRLabel35: TQRLabel;
    QRSubDetail2: TQRSubDetail;
    QRLabel36: TQRLabel;
    QRLDtCadasOcorr: TQRLabel;
    QRLDtOcorr: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetail2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelExtIndEstagio: TFrmRelExtIndEstagio;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelExtIndEstagio.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  ano,mes,dia: word;
begin
  inherited;
  QRLCEPEmp.Caption := FormatMaskText('00000-000;0',QryRelatorio.FieldByName('CO_CEP_EMP_OFERT_ESTAG').AsString);

  if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'O' then
  begin
    QRLCandidato.Caption := UpperCase(QryRelatorio.FieldByName('NO_CANDID_ESTAG').AsString);
    QRLIdent.Caption := FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('NU_CPF_CANDID_ESTAG').AsString);
    QRLIdent.Left := 65;
    QRLTpEstag.Caption := 'Outros';
  end
  else
  begin
    QRLIdent.Left := 55;
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
        QRLTitIdent.Caption := 'Matrícula:';
        if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'A' then
        begin
          QRLTpEstag.Caption := 'Aluno';
          if not FieldByName('matric').IsNull then
            QRLIdent.Caption := FormatMaskText('00.000.000000;0',FieldByName('matric').AsString)
          else
            QRLIdent.Caption := '-';
        end
        else
        begin
          QRLTpEstag.Caption := 'Funcionário/Professor';
          if not FieldByName('matric').IsNull then
            QRLIdent.Caption := FormatMaskText('00.000-0;0',FieldByName('matric').AsString)
          else
            QRLIdent.Caption := '-';
        end;
      end
    end;
  end;

  if not QryRelatorio.FieldByName('NR_CONTRATO').IsNull then
    QRLNumContEstag.Caption := QryRelatorio.FieldByName('NR_CONTRATO').AsString
  else
    QRLNumContEstag.Caption := '-';

  if not QryRelatorio.FieldByName('DT_ASSIN_CONTRATO').IsNull then
    QRLDtContEstag.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('DT_ASSIN_CONTRATO').AsDateTime)
  else
    QRLDtContEstag.Caption := '-';

  if not QryRelatorio.FieldByName('VL_REMUN').IsNull then
    QRLRemun.Caption := 'R$ ' + FloatToStrF(QryRelatorio.FieldByName('VL_REMUN').AsFloat,ffNumber,15,2)
  else
  begin
    QRLRemun.Caption := '-';
    {if not QryRelatorio.FieldByName('VL_REMUN').IsNull then
      QRLRemun.Caption := 'R$ ' + FloatToStrF(QryRelatorio.FieldByName('VL_REMUN').AsFloat,ffNumber,15,2)
    else
      QRLRemun.Caption := 'Não';}
  end;

  if not QryRelatorio.FieldByName('DT_INI_EFETI_ESTAGIO').IsNull then
    QRLDtIniEstag.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('DT_INI_EFETI_ESTAGIO').AsDateTime)
  else
    QRLDtIniEstag.Caption := '-';

  if not QryRelatorio.FieldByName('DT_FIM_EFETI_ESTAGIO').IsNull then
    QRLDtFimEstag.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('DT_FIM_EFETI_ESTAGIO').AsDateTime)
  else
    QRLDtFimEstag.Caption := '-';

  if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'A' then
    QRLPubAlvo.Caption := 'Alunos'
  else if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'F' then
    QRLPubAlvo.Caption := 'Funcionário'
  else if QryRelatorio.FieldByName('TP_PUBLI_ALVO').AsString = 'P' then
    QRLPubAlvo.Caption := 'Professor'
  else
    QRLPubAlvo.Caption := 'Outros';

  if not QryRelatorio.FieldByName('VL_IDADE_MIN').IsNull then
  begin
    QRLIdades.Caption := QryRelatorio.FieldByName('VL_IDADE_MIN').AsString;
    if not QryRelatorio.FieldByName('VL_IDADE_MAX').IsNull then
      QRLIdades.Caption := QRLIdades.Caption + ' à ' + QryRelatorio.FieldByName('VL_IDADE_MAX').AsString;
  end
  else
    QRLIdades.Caption := '-';

  QRLEndereco.Caption := QryRelatorio.FieldByName('DE_END_EMP_OFERT_ESTAG').AsString + ', Nº ' + QryRelatorio.FieldByName('NU_END_EMP_OFERT_ESTAG').AsString;

  if QryRelatorio.FieldByName('FLA_DIA_SEMANA').AsString = 'SSE' then
    QRLDiasSemana.Caption := 'Segunda à Sexta'
  else if QryRelatorio.FieldByName('FLA_DIA_SEMANA').AsString = 'SSA' then
    QRLDiasSemana.Caption := 'Segunda à Sábado'
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
      QRLRemun.Caption := 'Não';
  end;

  if QryRelatorio.FieldByName('FLA_POSS_EFETI_OFERT_ESTAG').AsString = 'S' then
    QRLEfet.Caption := 'Sim'
  else
    QRLEfet.Caption := 'Não';

  if not QryRelatorio.FieldByName('NU_TELE_CONTA_OFERT_ESTAG').IsNull then
    QRLTelContato.Caption := FormatFloat('(##) ####-####',QryRelatorio.FieldByName('NU_TELE_CONTA_OFERT_ESTAG').AsFloat)
  else
    QRLTelContato.Caption := '-';

  //código novo
  DecodeDate(Now, ano, mes, dia);
  ano := StrToInt(Copy(IntToStr(ano),2,4));
  QRLabelDataAbreviada.Caption := FormatFloat('00',dia) + '/' + FormatFloat('00',mes) + '/' + FormatFloat('00',ano);
  //fim código novo
end;

procedure TFrmRelExtIndEstagio.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
 { with qryAvaliacoes do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select oe.* from TB222_OCORR_ESTAGIO oe ' +
                'where oe.ID_EFETI_ESTAGIO = ' + QryRelatorio.FieldByName('ID_EFETI_ESTAGIO').AsString +
                ' order by oe.DT_OCORR';
    Open;
  end;  }
end;

procedure TFrmRelExtIndEstagio.QRBand2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  with qryOcorrencias do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select oe.* from TB222_OCORR_ESTAGIO oe ' +
                'where oe.ID_EFETI_ESTAGIO = ' + QryRelatorio.FieldByName('ID_EFETI_ESTAGIO').AsString +
                ' order by oe.DT_OCORR';
    Open;
  end;
end;

procedure TFrmRelExtIndEstagio.QRSubDetail2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not qryOcorrencias.IsEmpty then
  begin
    if qryOcorrencias.FieldByName('TP_AVAL').AsString = 'P' then
      qrlTpAval.Caption := 'Positiva'
    else
      qrlTpAval.Caption := 'Negativa';

    if qryOcorrencias.FieldByName('ORIGEM_OCORR').AsString = 'E' then
      QrlOrigem.Caption := 'Empresa'
    else
      QrlOrigem.Caption := 'Instituição';

    if not qryOcorrencias.FieldByName('DT_CADASTRO').IsNull then
      QRLDtCadasOcorr.Caption := FormatDateTime('dd/MM/yyyy',qryOcorrencias.FieldByName('DT_CADASTRO').AsDateTime)
    else
      QRLDtCadasOcorr.Caption := '-';

    if not qryOcorrencias.FieldByName('DT_OCORR').IsNull then
      QRLDtOcorr.Caption := FormatDateTime('dd/MM/yyyy',qryOcorrencias.FieldByName('DT_OCORR').AsDateTime)
    else
      QRLDtOcorr.Caption := '-';

    if QRSubDetail2.Color = clWhite then
       QRSubDetail2.Color := $00D8D8D8
    else
       QRSubDetail2.Color := clWhite;
  end
  else
  begin
    qrlTpAval.Caption := '-';
    QrlOrigem.Caption := '-';
  end;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelExtIndEstagio]);

end.
