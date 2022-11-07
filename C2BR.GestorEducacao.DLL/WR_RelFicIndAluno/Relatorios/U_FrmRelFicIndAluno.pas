unit U_FrmRelFicIndAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelFicIndAluno = class(TFrmRelTemplate)
    QRBDetail: TQRBand;
    QRBand2: TQRBand;
    QRLTotFaltas: TQRLabel;
    QRLDescGUIA: TQRLabel;
    QRLObs: TQRLabel;
    QRLLeg: TQRLabel;
    QRLResult: TQRLabel;
    QRDBTeste: TQRDBText;
    QRLMedia: TQRLabel;
    QRGroup1: TQRGroup;
    QRLAluno: TQRLabel;
    QRLDadosAlu: TQRLabel;
    QRLFiliacao: TQRLabel;
    QRLDescSerie: TQRLabel;
    QRShape10: TQRShape;
    QRLArCon: TQRLabel;
    QRLAv1: TQRLabel;
    QRLN1: TQRLabel;
    QRLF1: TQRLabel;
    QRLAv2: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLAv3: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLAv4: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel8: TQRLabel;
    QRLMF: TQRLabel;
    QRLRes: TQRLabel;
    QRShape14: TQRShape;
    QRShape13: TQRShape;
    QRShape12: TQRShape;
    QRShape11: TQRShape;
    QRShape9: TQRShape;
    QRShape5: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRShape27: TQRShape;
    QRShape28: TQRShape;
    QRShape29: TQRShape;
    QRLabel14: TQRLabel;
    QRLPage: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRShape30: TQRShape;
    QRLabel2: TQRLabel;
    QRLResultFinal: TQRLabel;
    QRLFaltas: TQRLabel;
    QRShape31: TQRShape;
    QRLDescUnidDestino: TQRLabel;
    QRLCdUFDia: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRLabel20: TQRLabel;
    QRLNoSecretario: TQRLabel;
    QRLabel22: TQRLabel;
    QRLMatSecretario: TQRLabel;
    QRLabel23: TQRLabel;
    QRLMatDiretor: TQRLabel;
    QRLNoDiretor: TQRLabel;
    QRLabel21: TQRLabel;
    QRLObservacao: TQRLabel;
    QRLabel1: TQRLabel;
    procedure QRBDetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    totalGeralFaltas : Integer;
    ocoTotGerFaltas : Boolean;
  public
    { Public declarations }
    AnoCurso, NomeModulo, NomeCurso, codigoEmpresa: String;
    NumSemestre,numAluno: Integer;
  end;

var
  FrmRelFicIndAluno: TFrmRelFicIndAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelFicIndAluno.QRBDetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  mediaAprovacao, mediaBimestre : Double;
  qtdeFaltas : Integer;
  ocoFaltas,ocoMedias : Boolean;
begin
  inherited;
  mediaAprovacao := QryRelatorio.FieldByName('MED_FINAL_CUR').AsFloat;
  mediaBimestre := 0;
  qtdeFaltas := 0;
  ocoFaltas := false;
  ocoMedias := false;

  if not(QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull) and not(QryRelatorio.FieldByName('VL_NOTA_BIM2').IsNull)
  and not(QryRelatorio.FieldByName('VL_NOTA_BIM3').IsNull) and not(QryRelatorio.FieldByName('VL_NOTA_BIM4').IsNull) then
  begin
    mediaBimestre := (QryRelatorio.FieldByName('VL_NOTA_BIM1').AsFloat + QryRelatorio.FieldByName('VL_NOTA_BIM2').AsFloat +
    QryRelatorio.FieldByName('VL_NOTA_BIM3').AsFloat +QryRelatorio.FieldByName('VL_NOTA_BIM4').AsFloat)/4;
    ocoMedias := true;
  end
  else if not(QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull) and not(QryRelatorio.FieldByName('VL_NOTA_BIM2').IsNull)
          and not(QryRelatorio.FieldByName('VL_NOTA_BIM3').IsNull) then
  begin
    mediaBimestre := (QryRelatorio.FieldByName('VL_NOTA_BIM1').AsFloat + QryRelatorio.FieldByName('VL_NOTA_BIM2').AsFloat +
    QryRelatorio.FieldByName('VL_NOTA_BIM3').AsFloat)/3;
    ocoMedias := true;
  end
  else if not(QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull) and not(QryRelatorio.FieldByName('VL_NOTA_BIM2').IsNull) then
  begin
    mediaBimestre := (QryRelatorio.FieldByName('VL_NOTA_BIM1').AsFloat + QryRelatorio.FieldByName('VL_NOTA_BIM2').AsFloat)/2;
    ocoMedias := true;
  end
  else if not(QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull) then
  begin
    mediaBimestre := QryRelatorio.FieldByName('VL_NOTA_BIM1').AsFloat;
    ocoMedias := true;
  end;

  if not(QryRelatorio.FieldByName('QT_FALTA_BIM1').IsNull) and not(QryRelatorio.FieldByName('QT_FALTA_BIM2').IsNull)
  and not(QryRelatorio.FieldByName('QT_FALTA_BIM3').IsNull) and not(QryRelatorio.FieldByName('QT_FALTA_BIM4').IsNull) then
  begin
    ocoFaltas := true;
    ocoTotGerFaltas := true;
    qtdeFaltas := (QryRelatorio.FieldByName('QT_FALTA_BIM1').AsInteger + QryRelatorio.FieldByName('QT_FALTA_BIM2').AsInteger +
    QryRelatorio.FieldByName('QT_FALTA_BIM3').AsInteger +QryRelatorio.FieldByName('QT_FALTA_BIM4').AsInteger);
  end
  else if not(QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull) and not(QryRelatorio.FieldByName('VL_NOTA_BIM2').IsNull)
          and not(QryRelatorio.FieldByName('VL_NOTA_BIM3').IsNull) then
  begin
    ocoFaltas := true;
    ocoTotGerFaltas := true;
    qtdeFaltas := QryRelatorio.FieldByName('QT_FALTA_BIM1').AsInteger + QryRelatorio.FieldByName('QT_FALTA_BIM2').AsInteger +
    QryRelatorio.FieldByName('QT_FALTA_BIM3').AsInteger;
  end
  else if not(QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull) and not(QryRelatorio.FieldByName('VL_NOTA_BIM2').IsNull) then
  begin
    ocoFaltas := true;
    ocoTotGerFaltas := true;
    qtdeFaltas := QryRelatorio.FieldByName('QT_FALTA_BIM1').AsInteger + QryRelatorio.FieldByName('QT_FALTA_BIM2').AsInteger;
  end
  else if not(QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull) then
  begin
    ocoFaltas := true;
    ocoTotGerFaltas := true;
    qtdeFaltas := QryRelatorio.FieldByName('QT_FALTA_BIM1').AsInteger;
  end;

  if ocoMedias then
  begin
    QRLMedia.Caption := FloatToStrF(mediaBimestre,ffNumber,10,2);
  end
  else
    QRLMedia.Caption := '-';

  if not(QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull) and not(QryRelatorio.FieldByName('VL_NOTA_BIM2').IsNull)
  and not(QryRelatorio.FieldByName('VL_NOTA_BIM3').IsNull) and not(QryRelatorio.FieldByName('VL_NOTA_BIM4').IsNull) then
  begin
     if (( mediaBimestre < mediaAprovacao)) then
     begin
        QRLResult.Caption := 'REPROVADO'
     end
     else
       QRLResult.Caption := 'APROVADO';
  end
  else
  begin
    QRLResult.Caption := '-';
  end;
  
  if ocoFaltas then
  begin
    QRLFaltas.Caption := IntToStr(qtdeFaltas);
    totalGeralFaltas := totalGeralFaltas + qtdeFaltas;
  end
  else
    QRLFaltas.Caption := '-';

  //Deixar o relatório zebrado
  if QRBDetail.Color = clWhite then
    QRBDetail.Color := $00D8D8D8
  else
    QRBDetail.Color := clWhite;
end;

procedure TFrmRelFicIndAluno.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLAluno.Caption := UpperCase(QryRelatorio.FieldByName('no_alu').AsString) + ' (Matrícula: ' + FormatMaskText('00.000.0000.###;0', QryRelatorio.FieldByName('co_alu_cad').AsString) +
  ' - Nº NIRE: ' + FormatFloat('00000000000;0',QryRelatorio.FieldByName('nu_nire').AsFloat) + ' )';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'I' then
    QRLDescSerie.Caption := '( Ensino Infantil - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'F' then
    QRLDescSerie.Caption := '( Ensino Fundamental - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'M' then
    QRLDescSerie.Caption := '( Ensino Médio - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'G' then
    QRLDescSerie.Caption := '( Graduação - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'P' then
    QRLDescSerie.Caption := '( Pós-Graduação - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'E' then
    QRLDescSerie.Caption := '( Mestrado - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'D' then
    QRLDescSerie.Caption := '( Doutorado - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'U' then
    QRLDescSerie.Caption := '( Pós-Doutorado - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'S' then
    QRLDescSerie.Caption := '( Especialização - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'T' then
    QRLDescSerie.Caption := '( Técnico - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'R' then
    QRLDescSerie.Caption := '( Preparatório - Série: ';
  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'O' then
    QRLDescSerie.Caption := '( Outros - Série: ';

  QRLDescSerie.Caption := QRLDescSerie.Caption + NomeCurso + ' - Turma: ' + QryRelatorio.FieldByName('no_tur').AsString +
  ' - Ano Letivo: ' + Trim(AnoCurso) + ' )'; 

  QRLDadosAlu.Caption := 'Data Nascimento: ' + QryRelatorio.FieldByName('dt_nasc_alu').AsString + ' - Nacionalidade: ';

  if QryRelatorio.FieldByName('CO_NACI_ALU').AsString = 'B' then
    QRLDadosAlu.Caption := QRLDadosAlu.Caption + 'Brasileiro'
  else
    QRLDadosAlu.Caption := QRLDadosAlu.Caption + QryRelatorio.FIeldByName('DE_NACI_ALU').AsString;

  QRLDadosAlu.Caption := QRLDadosAlu.Caption + ' - Naturalidade: ' + QryRelatorio.FieldByName('DE_NATU_ALU').AsString +
  ' - ' + QryRelatorio.FieldByName('CO_UF_NATU_ALU').AsString;

  QRLFiliacao.Caption := 'Filiação: (Pai) ' + QryRelatorio.FieldByName('no_pai_alu').AsString + ' - (Mãe) ' + QryRelatorio.FieldByName('no_mae_alu').AsString;

  totalGeralFaltas := 0;
  ocoTotGerFaltas := false;

end;

procedure TFrmRelFicIndAluno.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('CO_STA_APROV').IsNull then
  begin
    if QryRelatorio.FieldByName('CO_STA_APROV').AsString = 'A' then
      QRLResultFinal.Caption := 'Resultado Final: APROVADO'
    else
      QRLResultFinal.Caption := 'Resultado Final: REPROVADO';
  end
  else
    QRLResultFinal.Caption := 'Resultado Final: -';

  if ocoTotGerFaltas then
    QRLTotFaltas.Caption := 'Total Geral de Faltas: ' + IntToStr(totalGeralFaltas)
  else
    QRLTotFaltas.Caption := 'Total Geral de Faltas: ' + ' -';

  QRLDescGUIA.Caption := '               A presente transferência é expedida ao Aluno ' + QryRelatorio.FieldByName('no_alu').AsString +
  ' matriculado nesta Unidade Educacional, na Série ' + NomeCurso + ', neste Ano Letivo de ' + Trim(AnoCurso) +
  ', nos termos da Lei Vigente e demais normas baixadas pelo Sistema de Ensino, tem direito a prosseguir os Estudos ' +
  'em Estabelecimento Fundamental ou correspondente indicado abaixo.';

  if not QryRelatorio.FieldByName('NU_NIS').IsNull then
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select * from tb_transf_externa ' +
                  'where CO_NIS_ALUNO = ' + QryRelatorio.FieldByName('NU_NIS').AsString +
                  ' and CO_UNIDA_ATUAL = ' + codigoEmpresa;
      Open;
      if not IsEmpty then
      begin
        QRLDescUnidDestino.Caption := 'Unidade Destino: ' + FieldByName('NM_UNIDA_DESTI').AsString +
        ' (Código INEP: ' + FieldByName('NU_INEP_DESTI').AsString + ')' + #13 +
        'Endereço: ' + FieldByName('DE_ENDER_DESTI').AsString +', ' + FieldByName('DE_COMPL_DESTI').AsString  +
        ' - Bairro ' + FieldByName('NO_BAIRR_DESTI').AsString + #13 +
        'CEP: ' + FormatMaskText('99999-999;0; ',FieldByName('CO_CEP_DESTI').AsString) + ' - ' +
        FieldByName('NO_CIDAD_DESTI').AsString+ ' - ' + FieldByName('CO_UF_DESTI').AsString;
        QRLObservacao.Caption := FieldByName('DE_MOTIVO_TRANSF').AsString;
      end
      else
      begin
        QRLDescUnidDestino.Caption := '';
        QRLObservacao.Caption := '';
      end;
    end;
  end
  else
  begin
    QRLDescUnidDestino.Caption := '';
    QRLObservacao.Caption := '';
  end;

  with DM.QrySql do
  begin
    Close;
    SQl.Clear;
    SQL.Text := 'select c.no_col,c.co_mat_col,ci.NO_CIDADE,e.CO_UF_EMP from tb25_empresa e ' +
                ' join TB83_PARAMETRO p on p.co_emp = e.co_emp ' +
                ' left join tb03_colabor c on c.co_col = p.CO_DIR1 ' +
                ' left join TB904_CIDADE CI on E.CO_CIDADE = CI.CO_CIDADE ' +
                ' where e.co_emp = ' + codigoEmpresa;
    Open;

    if not IsEmpty then
    begin
      if not FieldByName('no_cidade').IsNull then
        QRLCdUFDia.Caption := FieldByName('no_cidade').AsString + ' - ' + FieldByName('CO_UF_EMP').AsString +
        ', ' + FormatDateTime('dd/MM/yyyy',now);
      if not FieldByName('no_col').IsNull then
      begin
        QRLNoDiretor.Caption := FieldByName('no_col').AsString;
        QRLMatDiretor.Caption := FormatMaskText('00.000-0;0',FieldByName('co_mat_col').AsString)
      end
      else
      begin
        QRLNoDiretor.Caption := '';
        QRLMatDiretor.Caption := '';
      end;
    end
    else
    begin
      QRLNoDiretor.Caption := '';
      QRLMatDiretor.Caption := '';
    end;
  end;
  //Escrever Coordenador
  with DM.QrySql do
  begin
    Close;
    SQl.Clear;
    SQL.Text := 'select c.no_col,c.co_mat_col from tb25_empresa e ' +
                ' join TB83_PARAMETRO p on p.co_emp = e.co_emp ' +
                ' left join tb03_colabor c on c.co_col = p.CO_COOR1 ' +
                ' where e.co_emp = ' + codigoEmpresa;
    Open;

    if not IsEmpty then
    begin
      QRLNoSecretario.Caption := FieldByName('no_col').AsString;
      QRLMatSecretario.Caption := FormatMaskText('00.000-0;0',FieldByName('co_mat_col').AsString)
    end
    else
    begin
      QRLNoSecretario.Caption := '';
      QRLMatSecretario.Caption := '';
    end;
  end;


end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelFicIndAluno]);

end.
