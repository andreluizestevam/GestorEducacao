library WR_RelRelacaoInativo;

uses
  Windows,
  Forms,
  ComServ,
  Controls,
  Classes,
  SysUtils,
  Dialogs,
  QuickRpt,
  QRPDFFilt,
  bsutils in '..\General\bsutils.pas',
  U_DataModuleSGE in '..\General\U_DataModuleSGE.pas' {DataModuleSGE: TDataModule},
  UTPersistente in '..\General\UTPersistente.pas',
  U_FrmRelTemplate in '..\General\U_FrmRelTemplate.pas' {FrmRelTemplate},
  U_Funcoes in '..\General\U_Funcoes.pas',
  U_FrmRelRelacaoInativo in 'Relatorios\U_FrmRelRelacaoInativo.pas' {FrmRelRelacaoInativo};

// Gestão de Atividades Acadêmico/Pedagógica > Informações Operacionais de Alunos
// Relatório: EMISSÃO DA RELAÇÃO DE ALUNOS - SITUAÇÃO DE MATRÍCULA
// STATUS: OK
function DLLRelRelacaoInativo(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ANO_MES_MAT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_SIT_MAT : PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacaoInativo;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath : string;
  SqlString : string;
begin

  intReturn := 0;
  PDFFilt := Nil;
  Relatorio := Nil;

  Try
    Try

      FilePath := '' + strPathReportGenerate + strReportName;
      PDFFilt := TQRPDFDocumentFilter.Create(FilePath);
      PDFFilt.AddFontMap( 'WebDings:ZapfDingBats' );
      PDFFilt.TextOnTop := true;
      PDFFilt.LeftMargin := 0;
      PDFFilt.topMargin := 0;
      PDFFilt.CompressionOn := true;
      PDFFilt.Concatenating := true;

      // Monta a Consulta do Relatório.
      SqlString := ' SET LANGUAGE PORTUGUESE ' +
          ' SELECT Distinct A.CO_GRAU_PAREN_RESP as DE_GRAU_PAREN, E.SIGLA, A.CO_ALU, A.NU_NIS, A.DT_NASC_ALU, A.TP_DEF, ' +
          '   A.NO_ALU, ' +
          '   MM.CO_ALU_CAD, ' +
          '   A.DE_ENDE_ALU, ' +
          '   A.NU_ENDE_ALU, ' +
          '   A.DE_COMP_ALU, ' +
          '   A.CO_CIDADE, A.CO_BAIRRO, ' +
          '   A.CO_ESTA_ALU, ' +
          '   A.CO_CEP_ALU, ' +
          '   A.CO_SEXO_ALU, ' +
          '   ( ' +
          '     SELECT COUNT(CO_ALU) FROM TB08_MATRCUR ' +
          '     WHERE CO_ANO_MES_MAT = ' + strP_CO_ANO_MES_MAT;

      if strP_CO_SIT_MAT <> 'U' then
        SqlString := SqlString + ' AND CO_SIT_MAT = ' + QuotedStr(strP_CO_SIT_MAT);

      SqlString := SqlString +
          '     AND CO_EMP = ' + strP_CO_EMP +
          '   ) MATRICULAS, ' +
          '   ( ' +
          '     Select RESP.NO_RESP ' +
          '     From TB108_RESPONSAVEL RESP ' +
          '     Where RESP.CO_RESP = A.CO_RESP ' +
          '   ) NO_RESP, ' +
          '   ( ' +
          '     Select Coalesce(RESP.NU_TELE_RESI_RESP, Coalesce(RESP.NU_TELE_CELU_RESP, ' + QuotedStr('') +  ')) '+
          '     From TB108_RESPONSAVEL RESP '+
          '     Where RESP.CO_RESP = A.CO_RESP ' +
          '   ) NU_TELE_RESP, ' +
          '   TU.CO_SIGLA_TURMA [NO_TUR], ' +
          '   C.NO_CUR, C.CO_SIGL_CUR, ' +
          '   G.CO_CUR, ' +
          '   G.CO_TUR, ' +
          '   G.NU_SEM_LET, ' +
          '   G.CO_ANO_MES_MAT, ' +
          '   MM.DT_SIT_MAT, ' +
          '   MO.DE_MODU_CUR, CD.NO_CIDADE, BB.NO_BAIRRO, ' +
          '   SITUACAO = (CASE MM.CO_SIT_MAT ' +
          '     WHEN ' + QuotedStr('C') + ' THEN ' + QuotedStr('Cancelada') +
          '     WHEN ' + QuotedStr('T') + ' THEN ' + QuotedStr('Trancada') +
          '     WHEN ' + QuotedStr('F') + ' THEN ' + QuotedStr('Finalizada') +
          '     WHEN ' + QuotedStr('X') + ' THEN ' + QuotedStr('Transferido') +
          '     WHEN ' + QuotedStr('D') + ' THEN ' + QuotedStr('Desistente') +
          '     WHEN ' + QuotedStr('P') + ' THEN ' + QuotedStr('Pendente') +
          '     WHEN ' + QuotedStr('B') + ' THEN ' + QuotedStr('Abandono') +
          '     WHEN ' + QuotedStr('A') + ' THEN ' + QuotedStr('Matriculado') +
          '   END), ' +
          '   DEFICIENCIA = (CASE A.TP_DEF ' +
          '     WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Nenhuma') +
          '     WHEN ' + QuotedStr('A') + ' THEN ' + QuotedStr('Auditiva') +
          '     WHEN ' + QuotedStr('V') + ' THEN ' + QuotedStr('Visual') +
          '     WHEN ' + QuotedStr('F') + ' THEN ' + QuotedStr('Física') +
          '     WHEN ' + QuotedStr('M') + ' THEN ' + QuotedStr('Mental') +
          '     WHEN ' + QuotedStr('I') + ' THEN ' + QuotedStr('Múltiplas') +
          '     WHEN ' + QuotedStr('O') + ' THEN ' + QuotedStr('Outra') +
          '   END), ' +
          '   PARENTESCO = (CASE RESP.DE_GRAU_PAREN ' +
          '     WHEN ' + QuotedStr('PM') + ' THEN ' + QuotedStr('Pai/Mãe') +
          '     WHEN ' + QuotedStr('AV') + ' THEN ' + QuotedStr('Avô/Avó') +
          '     WHEN ' + QuotedStr('IR') + ' THEN ' + QuotedStr('Irmão(ã)') +
          '     WHEN ' + QuotedStr('TI') + ' THEN ' + QuotedStr('Tio(a)') +
          '     WHEN ' + QuotedStr('PR') + ' THEN ' + QuotedStr('Primo(a)') +
          '     WHEN ' + QuotedStr('CN') + ' THEN ' + QuotedStr('Cunhado(a)') +
          '     WHEN ' + QuotedStr('TU') + ' THEN ' + QuotedStr('Tutor(a)') +
          '     WHEN ' + QuotedStr('OU') + ' THEN ' + QuotedStr('Outros') +
          '   END) ' +
          ' FROM TB08_MATRCUR MM ' +
          '   JOIN TB48_GRADE_ALUNO G on G.CO_EMP = MM.CO_EMP AND G.CO_CUR = MM.CO_CUR AND G.CO_ALU = MM.CO_ALU AND  G.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT ' +
          '   JOIN TB25_EMPRESA E on E.CO_EMP = MM.CO_EMP ' +
          '   JOIN TB07_ALUNO A on MM.CO_EMP = A.CO_EMP AND  MM.CO_ALU = A.CO_ALU ' +
          '   JOIN TB108_RESPONSAVEL RESP on RESP.CO_RESP = A.CO_RESP ' +
          '   JOIN TB06_TURMAS T on G.CO_EMP = T.CO_EMP AND  G.CO_TUR = T.CO_TUR ' +
          '   JOIN TB129_CADTURMAS TU on G.CO_TUR = TU.CO_TUR AND  G.CO_MODU_CUR = TU.CO_MODU_CUR ' +
          '   JOIN TB01_CURSO C on G.CO_EMP = C.CO_EMP AND  G.CO_CUR = C.CO_CUR AND G.CO_MODU_CUR = C.CO_MODU_CUR ' +
          '   JOIN TB44_MODULO MO on G.CO_MODU_CUR = MO.CO_MODU_CUR ' +
          '   LEFT JOIN TB904_CIDADE CD on A.CO_CIDADE = CD.CO_CIDADE ' +
          '   LEFT JOIN TB905_BAIRRO BB on A.CO_CIDADE = BB.CO_CIDADE and A.CO_BAIRRO = BB.CO_BAIRRO ' +
          ' WHERE G.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_MES_MAT) +
          //'   AND G.NU_SEM_LET =  ' + IntToStr(1) +
          '   AND  G.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_MODU_CUR <> Nil then
        SqlString := SqlString + ' and mm.co_modu_cur = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' and mm.co_cur = ' + strP_CO_CUR;

      if strP_CO_TUR <> 'T' then
        SqlString := SqlString + ' and mm.co_tur = ' + strP_CO_TUR;

      if strP_CO_SIT_MAT <> 'U' then
        SqlString := SqlString + ' AND MM.CO_SIT_MAT = ' + QuotedStr(strP_CO_SIT_MAT);

      SqlString := SqlString +
                   '  ORDER BY A.NO_ALU ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelRelacaoInativo.Create(Nil);

      //Preenche alguns campos do relatorio

      { Carrega as globais do relatório }

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SqlString;
      Relatorio.QryRelatorio.Open;

      if Relatorio.QryRelatorio.IsEmpty then
      begin
        // Retorna -1 para Sem Registros na Consulta.
        intReturn := -1;
      end
      else
      begin
        // Atualiza Campos do Relatório Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;
        
        Relatorio.QRLParametros.Caption := strParamRel;
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relatório Gerado com Sucesso.
        intReturn := 1;
      end;

    Except
      on E : Exception do
        intReturn := 0;
        //ShowMessage(E.ClassName + ' error raised, with message : ' + E.Message);
    end;

  Finally
    Relatorio.QuickRep1.QRPrinter.Free;
    if intReturn = 1 then
      PDFFilt.EndConcat;
    PDFFilt.Free;
    Relatorio.Free;
    Result := intReturn;
  end;

end;

exports
  DllGetClassObject,
  DllCanUnloadNow,
  DllRegisterServer,
  DllUnregisterServer,
  
  //Relatórios
  DLLRelRelacaoInativo;

end.
