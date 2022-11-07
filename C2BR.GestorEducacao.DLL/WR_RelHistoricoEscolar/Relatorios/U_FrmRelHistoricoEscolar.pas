unit U_FrmRelHistoricoEscolar;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  QrAngLbl;

type
  TFrmRelHistoricoEscolar = class(TFrmRelTemplate)
    QRLTitCertificado: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRLMPD2: TQRLabel;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape33: TQRShape;
    QRShape34: TQRShape;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QRShape37: TQRShape;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape40: TQRShape;
    QRShape41: TQRShape;
    QRShape42: TQRShape;
    QRShape43: TQRShape;
    QRShape44: TQRShape;
    QRShape45: TQRShape;
    QRShape46: TQRShape;
    QRShape47: TQRShape;
    QRShape48: TQRShape;
    QRShape49: TQRShape;
    QRShape50: TQRShape;
    QRShape51: TQRShape;
    QRShape52: TQRShape;
    QRShape53: TQRShape;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    QRLMEX: TQRLabel;
    QRLMBNC7: TQRLabel;
    QRLMBNC6: TQRLabel;
    QRLMBNC5: TQRLabel;
    QRLMBNC4: TQRLabel;
    QRLMBNC3: TQRLabel;
    QRLMBNC2: TQRLabel;
    QRLMBNC1: TQRLabel;
    QRLabel15: TQRLabel;
    DetailBand1: TQRBand;
    QRShape54: TQRShape;
    QRShape55: TQRShape;
    QRShape56: TQRShape;
    QRShape57: TQRShape;
    QRShape58: TQRShape;
    QRShape59: TQRShape;
    QRShape60: TQRShape;
    QRShape61: TQRShape;
    QRShape62: TQRShape;
    QRShape63: TQRShape;
    QRShape64: TQRShape;
    QRShape65: TQRShape;
    QRShape66: TQRShape;
    QRShape67: TQRShape;
    QRShape68: TQRShape;
    QRShape69: TQRShape;
    QRShape70: TQRShape;
    QRShape71: TQRShape;
    QRShape72: TQRShape;
    QRShape73: TQRShape;
    QRShape74: TQRShape;
    QRShape75: TQRShape;
    QRShape76: TQRShape;
    QRShape77: TQRShape;
    QRShape78: TQRShape;
    QRShape79: TQRShape;
    QRShape80: TQRShape;
    QRShape81: TQRShape;
    QRShape82: TQRShape;
    QRShape83: TQRShape;
    QRShape84: TQRShape;
    QRShape85: TQRShape;
    QRShape86: TQRShape;
    QRShape87: TQRShape;
    SummaryBand1: TQRBand;
    QRShape88: TQRShape;
    QRLabel25: TQRLabel;
    QRLabel28: TQRLabel;
    QRShape90: TQRShape;
    QRLabel31: TQRLabel;
    QRLNoInst: TQRLabel;
    QRLUFCidadeInst: TQRLabel;
    QRLAnoMatricula: TQRLabel;
    QRLResultadoFinal: TQRLabel;
    QRLBNCN1: TQRLabel;
    QRDBNoCur: TQRDBText;
    QRLNoAlu: TQRLabel;
    qryAluno: TADOQuery;
    qryAlunono_alu: TStringField;
    qryAlunoco_sexo_alu: TStringField;
    qryAlunodt_nasc_alu: TDateTimeField;
    qryAlunoCO_NACI_ALU: TStringField;
    qryAlunoDE_NATU_ALU: TStringField;
    qryAlunoDE_NACI_ALU: TStringField;
    qryAlunoCO_ORG_RG_ALU: TStringField;
    qryAlunoCO_ESTA_RG_ALU: TStringField;
    qryAlunoDT_EMIS_RG_ALU: TDateTimeField;
    qryAlunoNO_PAI_ALU: TStringField;
    qryAlunoNO_MAE_ALU: TStringField;
    QRLDtNasc: TQRLabel;
    QRLSxAlu: TQRLabel;
    QRLNacAlu: TQRLabel;
    QRLNatuAlu: TQRLabel;
    QRLRGAlu: TQRLabel;
    QRLOERGAlu: TQRLabel;
    QRLUFRGAlu: TQRLabel;
    QRLDtRGAlu: TQRLabel;
    QRLNoPaiMaeAlu: TQRLabel;
    qryAlunoCO_UF_NATU_ALU: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioCO_CUR: TAutoIncField;
    QryRelatorioSEQ_IMPRESSAO: TIntegerField;
    QRLMPD1: TQRLabel;
    qryAlunoco_alu: TAutoIncField;
    QryRelatorioCO_MODU_CUR: TIntegerField;
    QRLBNCCH1: TQRLabel;
    QRLBNCF1: TQRLabel;
    QRLBNCN2: TQRLabel;
    QRLBNCCH2: TQRLabel;
    QRLBNCF2: TQRLabel;
    QRLBNCN3: TQRLabel;
    QRLBNCCH3: TQRLabel;
    QRLBNCF3: TQRLabel;
    QRLBNCN4: TQRLabel;
    QRLBNCCH4: TQRLabel;
    QRLBNCF4: TQRLabel;
    QRLBNCN5: TQRLabel;
    QRLBNCCH5: TQRLabel;
    QRLBNCF5: TQRLabel;
    QRLBNCN6: TQRLabel;
    QRLBNCCH6: TQRLabel;
    QRLBNCF6: TQRLabel;
    QRLBNCN7: TQRLabel;
    QRLBNCCH7: TQRLabel;
    QRLBNCF7: TQRLabel;
    QRLPDN1: TQRLabel;
    QRLPDCH1: TQRLabel;
    QRLPDF1: TQRLabel;
    QRLPDN2: TQRLabel;
    QRLPDCH2: TQRLabel;
    QRLPDF2: TQRLabel;
    QRLECNota: TQRLabel;
    QRLECCH: TQRLabel;
    QRLECFaltas: TQRLabel;
    QRLTotDiasLetivos: TQRLabel;
    QRLTotCH: TQRLabel;
    QRLTotFalHoras: TQRLabel;
    qryAlunoCO_RG_ALU: TStringField;
    QryRelatorioCO_ANO_REF: TStringField;
    QryRelatorioQT_AULA_CUR: TIntegerField;
    QRLNuNIS: TQRLabel;
    qryAlunonu_nis: TBCDField;
    QRLabel1: TQRLabel;
    QRLabel32: TQRLabel;
    QRLNoSecretario: TQRLabel;
    QRLNoDiretor: TQRLabel;
    QRLabel8: TQRLabel;
    QRLMatSecretario: TQRLabel;
    QRLabel9: TQRLabel;
    QRLMatDiretor: TQRLabel;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel35: TQRLabel;
    QRLabel36: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel38: TQRLabel;
    QRLabel39: TQRLabel;
    QRLabel40: TQRLabel;
    QRLabel41: TQRLabel;
    QRLabel42: TQRLabel;
    QRLabel43: TQRLabel;
    QRLabel44: TQRLabel;
    QRLabel45: TQRLabel;
    QRLabel46: TQRLabel;
    QRLabel47: TQRLabel;
    QRLabel48: TQRLabel;
    QRLabel49: TQRLabel;
    QRLabel50: TQRLabel;
    QRLabel51: TQRLabel;
    QRLabel52: TQRLabel;
    QRLabel53: TQRLabel;
    QRLabel54: TQRLabel;
    QRLabel55: TQRLabel;
    QRLabel56: TQRLabel;
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    qtdMatBNC,qtdPD : Integer;
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelHistoricoEscolar: TFrmRelHistoricoEscolar;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelHistoricoEscolar.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLMBNC1.Caption := '';
  QRLMBNC2.Caption := '';
  QRLMBNC3.Caption := '';
  QRLMBNC4.Caption := '';
  QRLMBNC5.Caption := '';
  QRLMBNC6.Caption := '';
  QRLMBNC7.Caption := '';
  QRLMPD1.Caption := '';
  QRLMPD2.Caption := '';
  QRLMEX.Caption := '';
  qtdMatBNC := 0;
  qtdPD := 0;
  if not qryAlunono_alu.IsNull then
    QRLNoAlu.Caption := UpperCase(qryAlunono_alu.AsString)
  else
    QRLNoAlu.Caption := '-';

  if not qryAlunodt_nasc_alu.IsNull then
    QRLDtNasc.Caption := qryAlunodt_nasc_alu.AsString
  else
    QRLDtNasc.Caption := '-';

  if not qryAlunonu_nis.IsNull then
    QRLNuNIS.Caption := FormatFloat('000000000000;1',qryAlunonu_nis.AsFloat)
  else
    QRLNuNIS.Caption := '-';

  if not qryAlunoco_sexo_alu.IsNull then
  begin
    if qryAlunoco_sexo_alu.AsString = 'M' then
      QRLSxAlu.Caption := 'Masculino'
    else
      QRLSxAlu.Caption := 'Feminino';
  end
  else
    QRLSxAlu.Caption := '-';

  if not qryAlunoCO_NACI_ALU.IsNull then
  begin
    if qryAlunoCO_NACI_ALU.AsString = 'B' then
      QRLNacAlu.Caption := 'Brasileiro'
    else
      if not qryAlunoDE_NACI_ALU.IsNull then
        QRLNacAlu.Caption := qryAlunoDE_NACI_ALU.AsString
      else
        QRLNacAlu.Caption := '-';
  end
  else
    QRLNacAlu.Caption := '-';

  if not (qryAlunoDE_NATU_ALU.IsNull) and not (qryAlunoCO_UF_NATU_ALU.IsNull) then
    QRLNatuAlu.Caption := qryAlunoDE_NATU_ALU.AsString +'/'+ qryAlunoCO_UF_NATU_ALU.AsString
  else
    QRLNatuAlu.Caption := '-';

  if not (qryAlunoCO_RG_ALU.IsNull) then
    QRLRGAlu.Caption := 'Nº RG: ' + qryAlunoCO_RG_ALU.AsString
  else
    QRLRGAlu.Caption := 'Nº RG: -';

  if not (qryAlunoCO_ORG_RG_ALU.IsNull) then
    QRLOERGAlu.Caption := 'Orgão Emissor: ' + qryAlunoCO_ORG_RG_ALU.AsString
  else
    QRLOERGAlu.Caption := 'Orgão Emissor: -';

  if not (qryAlunoCO_ESTA_RG_ALU.IsNull) then
    QRLUFRGAlu.Caption := 'UF: ' + qryAlunoCO_ESTA_RG_ALU.AsString
  else
    QRLUFRGAlu.Caption := 'UF: -';

  if not (qryAlunoDT_EMIS_RG_ALU.IsNull) then
    QRLDtRGAlu.Caption := 'Data Expedição: ' + qryAlunoDT_EMIS_RG_ALU.AsString
  else
    QRLDtRGAlu.Caption := 'Data Expedição: -';

  if not (qryAlunoNO_PAI_ALU.IsNull) and not (qryAlunoNO_MAE_ALU.IsNull) then
    QRLNoPaiMaeAlu.Caption := 'Filiação (Pai/Mãe): ' + qryAlunoNO_PAI_ALU.AsString + ' / ' + qryAlunoNO_MAE_ALU.AsString
  else
    QRLNoPaiMaeAlu.Caption := 'Filiação (Pai/Mãe): -';

  {******************
    Escrever Disciplinas de acordo com seu tipo.
  *******************}
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct cm.no_red_materia from tb107_cadmaterias cm ' +
                ' join tb02_materia m on m.id_materia = cm.id_materia and m.co_emp = cm.co_emp ' +
                'where cm.co_emp = ' + codigoEmpresa +
                ' and cm.co_class_boletim = 1 ' +
                ' and m.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                //' and m.co_cur = ' + QryRelatorioCO_CUR.AsString +
                ' order by cm.no_red_materia ';
    Open;

    First;
    qtdMatBNC := 0;

    while not DM.QrySql.Eof do
    begin
      if qtdMatBNC = 0 then
      begin
        QRLMBNC1.Caption := FieldByName('no_red_materia').AsString;
      end;
      if qtdMatBNC = 1 then
      begin
        QRLMBNC2.Caption := FieldByName('no_red_materia').AsString;
      end;
      if qtdMatBNC = 2 then
      begin
        QRLMBNC3.Caption := FieldByName('no_red_materia').AsString;
      end;
      if qtdMatBNC = 3 then
      begin
        QRLMBNC4.Caption := FieldByName('no_red_materia').AsString;
      end;
      if qtdMatBNC = 4 then
      begin
        QRLMBNC5.Caption := FieldByName('no_red_materia').AsString;
      end;
      if qtdMatBNC = 5 then
      begin
        QRLMBNC6.Caption := FieldByName('no_red_materia').AsString;
      end;
      if qtdMatBNC = 6 then
      begin
        QRLMBNC7.Caption := FieldByName('no_red_materia').AsString;
      end;

      qtdMatBNC := qtdMatBNC + 1;
      DM.QrySql.Next;
    end;
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct cm.no_red_materia from tb107_cadmaterias cm ' +
                ' join tb02_materia m on m.id_materia = cm.id_materia and m.co_emp = cm.co_emp ' +
                'where cm.co_emp = ' + codigoEmpresa +
                ' and cm.co_class_boletim = 2 ' +
                ' and m.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                //' and m.co_cur = ' + QryRelatorioCO_CUR.AsString +
                ' order by cm.no_red_materia ';
    Open;

    First;
    qtdPD := 0;

    while not DM.QrySql.Eof do
    begin
      if qtdPD = 0 then
      begin
        QRLMPD1.Caption := FieldByName('no_red_materia').AsString;
      end;
      if qtdPD = 1 then
      begin
        QRLMPD2.Caption := FieldByName('no_red_materia').AsString;
      end;

      qtdPD := qtdPD + 1;
      DM.QrySql.Next;
    end;
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct cm.no_red_materia from tb107_cadmaterias cm ' +
                ' join tb02_materia m on m.id_materia = cm.id_materia and m.co_emp = cm.co_emp ' +
                'where cm.co_emp = ' + codigoEmpresa +
                ' and cm.co_class_boletim = 3 ' +
                ' and m.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                //' and m.co_cur = ' + QryRelatorioCO_CUR.AsString +
                ' order by cm.no_red_materia ';
    Open;

    if not IsEmpty then
    begin
      QRLMEX.Caption := FieldByName('no_red_materia').AsString;
    end;
  end;

end;

procedure TFrmRelHistoricoEscolar.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  totCH,totFaltas : Integer;
begin
  inherited;
  totFaltas := 0;
  totCH := 0;
  QRLBNCN1.Caption := '';
  QRLBNCCH1.Caption := '';
  QRLBNCF1.Caption := '';
  QRLBNCN2.Caption := '';
  QRLBNCCH2.Caption := '';
  QRLBNCF2.Caption := '';
  QRLBNCN3.Caption := '';
  QRLBNCCH3.Caption := '';
  QRLBNCF3.Caption := '';
  QRLBNCN4.Caption := '';
  QRLBNCCH4.Caption := '';
  QRLBNCF4.Caption := '';
  QRLBNCN5.Caption := '';
  QRLBNCCH5.Caption := '';
  QRLBNCF5.Caption := '';
  QRLBNCN6.Caption := '';
  QRLBNCCH6.Caption := '';
  QRLBNCF6.Caption := '';
  QRLBNCN7.Caption := '';
  QRLBNCCH7.Caption := '';
  QRLBNCF7.Caption := '';
  QRLPDN1.Caption := '';
  QRLPDCH1.Caption := '';
  QRLPDF1.Caption := '';
  QRLPDN2.Caption := '';
  QRLPDCH2.Caption := '';
  QRLPDF2.Caption := '';
  QRLECNota.Caption := '';
  QRLECCH.Caption := '';
  QRLECFaltas.Caption := '';
  QRLTotDiasLetivos.Caption := '';
  QRLTotCH.Caption := '';
  QRLTotFalHoras.Caption := '';

  with DM.QrySql2 do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select e.no_fantas_emp,CID.NO_CIDADE, E.CO_UF_EMP,mm.co_sta_aprov,mm.CO_ANO_MES_MAT from tb08_matrcur mm ' +
                ' join tb25_empresa e on e.co_emp = mm.co_emp ' +
                ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = E.CO_CIDADE '+
                ' where mm.co_alu = ' + qryAlunoco_alu.AsString +
                ' and mm.co_cur = ' + QryRelatorioCO_CUR.AsString +
                ' and mm.co_emp = ' + codigoEmpresa +
                ' and mm.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                ' and mm.CO_ANO_MES_MAT = ' + QryRelatorioCO_ANO_REF.AsString;
    Open;
    Last;

    if not IsEmpty then
    begin

      QRLNoInst.Caption := 'Instituição Educacional: ' + FieldByName('no_fantas_emp').AsString;

      if not (FieldByName('NO_CIDADE').IsNull) and not (FieldByName('CO_UF_EMP').IsNull) then
      begin
        QRLUFCidadeInst.Caption := 'Cidade / UF: ' + FieldByName('NO_CIDADE').AsString + ' / ' + FieldByName('CO_UF_EMP').AsString;
      end
      else
        QRLUFCidadeInst.Caption := 'Cidade / UF: -';

      if not (FieldByName('CO_ANO_MES_MAT').IsNull) then
      begin
        QRLAnoMatricula.Caption := 'Ano: ' + Trim(FieldByName('CO_ANO_MES_MAT').AsString);
      end
      else
        QRLAnoMatricula.Caption := 'Ano: -';

      if not FieldByName('co_sta_aprov').IsNull then
      begin
        if FieldByName('co_sta_aprov').AsString = 'A' then
          QRLResultadoFinal.Caption := 'Resultado Final: Aprovado'
        else
          QRLResultadoFinal.Caption := 'Resultado Final : Reprovado'
      end
      else
        QRLResultadoFinal.Caption := 'Resultado Final : -';
    end
    else
    begin
      with DM.QrySql do
      begin
        Close;
        SQL.Clear;
        SQL.Text := 'select NO_INST,NO_CIDADE_INST,CO_UF_INST,CO_STA_APROV,CO_ANO_REF from tb130_hist_ext_aluno he ' +
                    ' where he.co_alu = ' + qryAlunoco_alu.AsString +
                    ' and he.co_cur = ' + QryRelatorioCO_CUR.AsString +
                    ' and he.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                    ' and he.co_ano_ref = ' + QryRelatorioCO_ANO_REF.AsString;
        Open;

        if not IsEmpty then
        begin
          QRLNoInst.Caption := 'Instituição Educacional: ' + FieldByName('NO_INST').AsString;
          QRLUFCidadeInst.Caption := 'Cidade / UF: ' + FieldByName('NO_CIDADE_INST').AsString + ' / ' + FieldByName('CO_UF_INST').AsString;
          if  FieldByName('CO_STA_APROV').AsString = 'A' then
            QRLResultadoFinal.Caption := 'Resultado Final: Aprovado'
          else
            QRLResultadoFinal.Caption := 'Resultado Final : Reprovado';

          QRLAnoMatricula.Caption := 'Ano: ' + Trim(FieldByName('CO_ANO_REF').AsString);
        end
        else
        begin
          QRLNoInst.Caption := 'Instituição Educacional: -';
          QRLUFCidadeInst.Caption := 'Cidade / UF: -';
          QRLResultadoFinal.Caption := 'Resultado Final: -';
          QRLAnoMatricula.Caption := 'Ano: -';
        end;
      end;
    end;
  end;

  {*****************
  Gerar as médias das disciplinas de tipo Base Nacional Comum
  ******************}

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct m.ID_MATERIA,cm.no_red_materia from tb02_materia m ' +
                ' join tb107_cadmaterias cm on m.id_materia = cm.id_materia and m.co_emp = cm.co_emp ' +
                ' join tb43_grd_curso gc on gc.co_mat = m.co_mat and gc.co_cur = m.co_cur and gc.co_emp = m.co_emp ' +
                'where m.co_emp = ' + codigoEmpresa +
                //' and m.co_cur = ' + QryRelatorioCO_CUR.AsString +
                ' and gc.co_ano_grade = ' + QryRelatorioCO_ANO_REF.AsString + 
                ' and cm.co_class_boletim = 1 ' +
                ' order by cm.no_red_materia ';
    Open;

    First;
    qtdMatBNC := 0;

    while not DM.QrySql.Eof do
    begin
      with DM.QrySql2 do
      begin
        Close;
        SQL.Clear;
        //tb43_grd_curso
        SQL.Text := 'select ha.*,gc.QTDE_CH_SEM from tb079_hist_aluno ha ' +
                    'join tb02_materia m on ha.co_mat = m.co_mat and m.co_cur = ha.co_cur and m.co_emp = ha.co_emp and m.co_modu_cur = ha.co_modu_cur ' +
                    ' join tb43_grd_curso gc on gc.co_cur = ha.co_cur and gc.co_modu_cur = ha.co_modu_cur ' +
                    ' and gc.co_emp = ha.co_emp and ha.co_mat = gc.co_mat and ha.co_ano_ref = gc.co_ano_grade' +
                    ' where ha.co_alu = ' + qryAlunoco_alu.AsString +
                    ' and ha.co_cur = ' + QryRelatorioCO_CUR.AsString +
                    ' and ha.co_emp = ' + codigoEmpresa +
                    ' and ha.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                    ' and m.id_materia = ' + DM.QrySql.FieldByName('ID_MATERIA').AsString +
                    ' and ha.co_ano_ref = ' + QryRelatorioCO_ANO_REF.AsString;
        Open;
        Last;

        if not IsEmpty then
        begin
          if qtdMatBNC = 0 then
          begin
            QRLBNCN1.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLBNCF1.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLBNCF1.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLBNCCH1.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

          if qtdMatBNC = 1 then
          begin
            QRLBNCN2.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLBNCF2.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLBNCF2.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLBNCCH2.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

          if qtdMatBNC = 2 then
          begin
            QRLBNCN3.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLBNCF3.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLBNCF3.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLBNCCH3.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

          if qtdMatBNC = 3 then
          begin
            QRLBNCN4.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLBNCF4.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLBNCF4.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLBNCCH4.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

          if qtdMatBNC = 4 then
          begin
            QRLBNCN5.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLBNCF5.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLBNCF5.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLBNCCH5.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

          if qtdMatBNC = 5 then
          begin
            QRLBNCN6.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLBNCF6.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLBNCF6.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLBNCCH6.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

          if qtdMatBNC = 6 then
          begin
            QRLBNCN7.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLBNCF7.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLBNCF7.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLBNCCH7.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

          QRLTotDiasLetivos.Caption := QryRelatorioQT_AULA_CUR.AsString;
        end
        else
        begin
          with DM.QrySql2 do
          begin
            Close;
            SQL.Clear;
            SQL.Text := 'select ha.* from tb130_hist_ext_aluno ha ' +
                        'join tb02_materia m on ha.co_mat = m.co_mat and m.co_cur = ha.co_cur and m.co_emp = ha.co_emp and m.co_modu_cur = ha.co_modu_cur ' +
                        ' where ha.co_alu = ' + qryAlunoco_alu.AsString +
                        ' and ha.co_cur = ' + QryRelatorioCO_CUR.AsString +
                        ' and ha.co_emp = ' + codigoEmpresa +
                        ' and ha.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                        ' and m.id_materia = ' + DM.QrySql.FieldByName('ID_MATERIA').AsString +
                        ' and ha.co_ano_ref = ' + QryRelatorioCO_ANO_REF.AsString;
            Open;
            Last;

            if not IsEmpty then
            begin
              if qtdMatBNC = 0 then
              begin
                QRLBNCN1.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLBNCF1.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLBNCCH1.Caption := FieldByName('QT_CH_MAT').AsString;
              end;

              if qtdMatBNC = 1 then
              begin
                QRLBNCN2.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLBNCF2.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLBNCCH2.Caption := FieldByName('QT_CH_MAT').AsString;
              end;

              if qtdMatBNC = 2 then
              begin
                QRLBNCN3.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLBNCF3.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLBNCCH3.Caption := FieldByName('QT_CH_MAT').AsString;
              end;

              if qtdMatBNC = 3 then
              begin
                QRLBNCN4.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLBNCF4.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLBNCCH4.Caption := FieldByName('QT_CH_MAT').AsString;
              end;

              if qtdMatBNC = 4 then
              begin
                QRLBNCN5.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLBNCF5.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLBNCCH5.Caption := FieldByName('QT_CH_MAT').AsString;
              end;

              if qtdMatBNC = 5 then
              begin
                QRLBNCN6.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLBNCF6.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLBNCCH6.Caption := FieldByName('QT_CH_MAT').AsString;
              end;

              if qtdMatBNC = 6 then
              begin
                QRLBNCN7.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLBNCF7.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLBNCCH7.Caption := FieldByName('QT_CH_MAT').AsString;
              end;
              QRLTotDiasLetivos.Caption := FieldByName('QT_TOTAL_DIAS_ANO').AsString;
              QRLTotFalHoras.Caption := FieldByName('QT_TOTAL_FALTAS_HORA').AsString;
              QRLTotCH.Caption := FieldByName('QT_CH_FINAL').AsString;
            end
            else
            begin
            end;
          end;
        end;

      end;

      qtdMatBNC := qtdMatBNC + 1;
      Next;
    end;
  end;

  {*****************
  Gerar as médias das disciplinas de tipo Parte Diversificada
  ******************}

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct m.id_materia,cm.no_red_materia from tb02_materia m ' +
                ' join tb107_cadmaterias cm on m.id_materia = cm.id_materia and m.co_emp = cm.co_emp ' +
                'where m.co_emp = ' + codigoEmpresa +
                //' and m.co_cur = ' + QryRelatorioCO_CUR.AsString +
                ' and cm.co_class_boletim = 2 ' +
                ' order by cm.no_red_materia ';
    Open;

    First;
    qtdPD := 0;

    while not DM.QrySql.Eof do
    begin
      with DM.QrySql2 do
      begin
        Close;
        SQL.Clear;
        //tb43_grd_curso
        SQL.Text := 'select ha.*,gc.QTDE_CH_SEM from tb079_hist_aluno ha ' +
                    'join tb02_materia m on ha.co_mat = m.co_mat and m.co_cur = ha.co_cur and m.co_emp = ha.co_emp and m.co_modu_cur = ha.co_modu_cur ' +
                    ' join tb43_grd_curso gc on gc.co_cur = ha.co_cur and gc.co_modu_cur = ha.co_modu_cur ' +
                    ' and gc.co_emp = ha.co_emp and ha.co_mat = gc.co_mat and ha.co_ano_ref = gc.co_ano_grade' +
                    ' where ha.co_alu = ' + qryAlunoco_alu.AsString +
                    ' and ha.co_cur = ' + QryRelatorioCO_CUR.AsString +
                    ' and ha.co_emp = ' + codigoEmpresa +
                    ' and ha.co_ano_ref = ' + QryRelatorioCO_ANO_REF.AsString +
                    ' and ha.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                    ' and m.id_materia = ' + DM.QrySql.FieldByName('ID_MATERIA').AsString;
        Open;

        Last;

        if not IsEmpty then
        begin
          if qtdPD = 0 then
          begin
            QRLPDN1.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLPDF1.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLPDF1.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLPDCH1.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

          if qtdPD = 1 then
          begin
            QRLPDN2.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLPDF2.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLPDF2.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLPDCH2.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
          end;

        end
        else
        begin
          with DM.QrySql2 do
          begin
            Close;
            SQL.Clear;
            SQL.Text := 'select ha.* from tb130_hist_ext_aluno ha ' +
                        'join tb02_materia m on ha.co_mat = m.co_mat and m.co_cur = ha.co_cur and m.co_emp = ha.co_emp and m.co_modu_cur = ha.co_modu_cur ' +
                        ' where ha.co_alu = ' + qryAlunoco_alu.AsString +
                        ' and ha.co_cur = ' + QryRelatorioCO_CUR.AsString +
                        ' and ha.co_emp = ' + codigoEmpresa +
                        ' and ha.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                        ' and m.id_materia = ' + DM.QrySql.FieldByName('ID_MATERIA').AsString +
                        ' and ha.co_ano_ref = ' + QryRelatorioCO_ANO_REF.AsString;
            Open;
            
            Last;

            if not IsEmpty then
            begin
              if qtdPD = 0 then
              begin
                QRLPDN1.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLPDF1.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLPDCH1.Caption := FieldByName('QT_CH_MAT').AsString;
              end;

              if qtdPD = 1 then
              begin
                QRLPDN2.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLPDF2.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLPDCH2.Caption := FieldByName('QT_CH_MAT').AsString;
              end;

            end
            else
            begin
            end;
          end;
        end;

      end;

      qtdPD := qtdPD + 1;
      Next;
    end;
  end;


  {*****************
  Gerar as médias das disciplinas de tipo Extra Curricular
  ******************}

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct m.id_materia,cm.no_red_materia from tb02_materia m ' +
                ' join tb107_cadmaterias cm on m.id_materia = cm.id_materia and m.co_emp = cm.co_emp ' +
                'where m.co_emp = ' + codigoEmpresa +
                //' and m.co_cur = ' + QryRelatorioCO_CUR.AsString +
                ' and cm.co_class_boletim = 3 ' +
                ' order by cm.no_red_materia ';
    Open;

    First;

    if not DM.QrySql.IsEmpty then
    begin
      with DM.QrySql2 do
      begin
        Close;
        SQL.Clear;
        //tb43
        SQL.Text := 'select ha.*,gc.QTDE_CH_SEM from tb079_hist_aluno ha ' +
                    'join tb02_materia m on ha.co_mat = m.co_mat and m.co_cur = ha.co_cur and m.co_emp = ha.co_emp and m.co_modu_cur = ha.co_modu_cur ' +
                    ' join tb43_grd_curso gc on gc.co_cur = ha.co_cur and gc.co_modu_cur = ha.co_modu_cur ' +
                    ' and gc.co_emp = ha.co_emp and ha.co_mat = gc.co_mat and ha.co_ano_ref = gc.co_ano_grade' +
                    ' where ha.co_alu = ' + qryAlunoco_alu.AsString +
                    ' and ha.co_cur = ' + QryRelatorioCO_CUR.AsString +
                    ' and ha.co_emp = ' + codigoEmpresa +
                    ' and ha.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                    ' and ha.co_ano_ref = ' + QryRelatorioCO_ANO_REF.AsString +
                    ' and m.id_materia = ' + DM.QrySql.FieldByName('ID_MATERIA').AsString;
        Open;
        Last;

        if not IsEmpty then
        begin
            QRLECNota.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
            QRLECFaltas.Caption := IntToStr(FieldByName('QT_FALTA_BIM1').AsInteger + FieldByName('QT_FALTA_BIM2').AsInteger +
            FieldByName('QT_FALTA_BIM3').AsInteger + FieldByName('QT_FALTA_BIM4').AsInteger);
            totFaltas := totFaltas + StrToInt(QRLPDF2.Caption);
            if not FieldByName('QTDE_CH_SEM').IsNull then
            begin
              QRLECCH.Caption := FieldByName('QTDE_CH_SEM').AsString;
              totCH := totCH + FieldByName('QTDE_CH_SEM').AsInteger;
            end;
        end
        else
        begin
          with DM.QrySql2 do
          begin
            Close;
            SQL.Clear;
            SQL.Text := 'select ha.* from tb130_hist_ext_aluno ha ' +
                        'join tb02_materia m on ha.co_mat = m.co_mat and m.co_cur = ha.co_cur and m.co_emp = ha.co_emp and m.co_modu_cur = ha.co_modu_cur ' +
                        ' where ha.co_alu = ' + qryAlunoco_alu.AsString +
                        ' and ha.co_cur = ' + QryRelatorioCO_CUR.AsString +
                        ' and ha.co_emp = ' + codigoEmpresa +
                        ' and ha.co_modu_cur = ' + QryRelatorioCO_MODU_CUR.AsString +
                        ' and m.id_materia = ' + DM.QrySql.FieldByName('ID_MATERIA').AsString +
                        ' and ha.co_ano_ref = ' + QryRelatorioCO_ANO_REF.AsString;
            Open;
            Last;

            if not IsEmpty then
            begin
                QRLECNota.Caption := FloatToStrF(FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,1);
                QRLECFaltas.Caption := FieldByName('QT_FALTA_FINAL').AsString;
                QRLECCH.Caption := FieldByName('QT_CH_MAT').AsString;
            end
            else
            begin
            end;
          end;
        end;

      end;
    end;
  end;

  if totCH > 0 then
  begin
    QRLTotCH.Caption := IntToStr(totCH);
  end;

  if totFaltas > 0 then
  begin
    QRLTotFalHoras.Caption := IntToStr(Round((totFaltas * 45)/60));
  end;
end;

procedure TFrmRelHistoricoEscolar.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  with DM.QrySql do
  begin
    Close;
    SQl.Clear;
    SQL.Text := 'select c.no_col as no_diretor,c.co_mat_col as mat_diretor,co.no_col as no_secretario,co.co_mat_col as mat_secretario from tb25_empresa e ' +
                ' left join tb03_colabor c on c.co_col = e.co_dir and c.co_emp = e.co_emp ' +
                ' left join tb03_colabor co on co.co_col = e.co_sec and co.co_emp = e.co_emp ' +
                ' where e.co_emp = ' + codigoEmpresa;
    Open;

    if not IsEmpty then
    begin
      QRLNoDiretor.Caption := FieldByName('no_diretor').AsString;
      QRLMatDiretor.Caption := FormatMaskText('00.000-0;0',FieldByName('mat_diretor').AsString);
      QRLNoSecretario.Caption := FieldByName('no_secretario').AsString;
      QRLMatSecretario.Caption := FormatMaskText('00.000-0;0',FieldByName('mat_secretario').AsString);
    end
    else
    begin
      QRLNoDiretor.Caption := '';
      QRLMatDiretor.Caption := '';
      QRLNoSecretario.Caption := '';
      QRLMatSecretario.Caption := '';
    end;
  end;
end;

end.
