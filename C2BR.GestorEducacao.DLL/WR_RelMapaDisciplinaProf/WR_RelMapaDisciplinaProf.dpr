library WR_RelMapaDisciplinaProf;

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
  U_FrmRelMapaDisciplinaProf in 'Relatorios\U_FrmRelMapaDisciplinaProf.pas' {FrmRelMapaDisciplinaProf};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaDisciplinaProf(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_ID_MATERIA, strP_CLASSI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaDisciplinaProf;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString : string;
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

      SqlString := ' SET LANGUAGE PORTUGUESE ' +
               ' SELECT DISTINCT P.CO_MAT_COL, P.NO_COL, CM.NO_RED_MATERIA as NO_MATERIA, C.NO_CUR, CT.CO_SIGLA_TURMA as NO_TURMA, P.CO_ESPEC, P.CO_CURFORM, ' +
               ' TC.NO_TPCON,P.NU_TELE_RESI_COL,P.NU_TELE_CELU_COL,P.CO_COL,'+
               'SITUACAO = (CASE P.CO_SITU_COL ' +
               '      WHEN ' + QuotedStr('ATI') + ' THEN ' + QuotedStr('Atividade Interna') +
               '      WHEN ' + QuotedStr('ATE') + ' THEN ' + QuotedStr('Atividade Externa') +
               '      WHEN ' + QuotedStr('FCE') + ' THEN ' + QuotedStr('Cedido') +
               '      WHEN ' + QuotedStr('FES') + ' THEN ' + QuotedStr('Estagiário') +
               '      WHEN ' + QuotedStr('LFR') + ' THEN ' + QuotedStr('Licença Funcional') +
               '      WHEN ' + QuotedStr('LME') + ' THEN ' + QuotedStr('Licença Médica') +
               '      WHEN ' + QuotedStr('LMA') + ' THEN ' + QuotedStr('Licença Maternidade') +
               '      WHEN ' + QuotedStr('SUS') + ' THEN ' + QuotedStr('Suspenso') +
               '      WHEN ' + QuotedStr('TRE') + ' THEN ' + QuotedStr('Treinamento') +
               '      WHEN ' + QuotedStr('FER') + ' THEN ' + QuotedStr('Férias') +
               '          	END),MO.DE_MODU_CUR' +
               ' FROM TB03_COLABOR P ' +
               ' JOIN TB20_TIPOCON TC ON TC.CO_TPCON = P.CO_TPCON ' +
               ' JOIN TB17_PLANO_AULA PA ON PA.CO_COL = P.CO_COL ' +
               ' JOIN TB01_CURSO C ON C.CO_CUR = PA.CO_CUR ' +
               ' JOIN TB06_TURMAS T ON T.CO_TUR = PA.CO_TUR ' +
               ' JOIN TB129_CADTURMAS CT ON T.CO_TUR = CT.CO_TUR AND T.CO_MODU_CUR = CT.CO_MODU_CUR ' +
               ' JOIN TB44_MODULO MO on MO.CO_MODU_CUR = PA.CO_MODU_CUR ' +
               ' LEFT JOIN TB02_MATERIA M ON M.CO_MAT = PA.CO_MAT ' +
               ' LEFT JOIN TB107_CADMATERIAS CM ON CM.ID_MATERIA = M.ID_MATERIA ' +
               ' WHERE P.FLA_PROFESSOR = ' + QuotedStr('S') +
               ' AND P.CO_EMP = ' + strP_CO_EMP;

      if strP_ID_MATERIA <> 'T' then
      begin
        SQLString := SQLString + ' and cm.id_materia = ' + strP_ID_MATERIA;
      end;

      if strP_CLASSI = 'P' then
        SqlString := SqlString + ' order by p.no_col, cm.no_red_materia,mo.de_modu_cur,c.no_cur,ct.co_sigla_turma'
      else
        SqlString := SqlString + ' order by cm.no_red_materia,p.no_col,mo.de_modu_cur,c.no_cur,ct.co_sigla_turma';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaDisciplinaProf.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SQLString;
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

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.codigoEmpresa := strP_CO_EMP;

        if strP_ID_MATERIA = 'M' then
        begin
          Relatorio.QRDBTMateria.Left := 3;
          Relatorio.QRLTitMateria.left := 3;
          Relatorio.QRDBTMatricula.left := Relatorio.QRDBTMatricula.left + 118;
          Relatorio.QRLNoCol.left := Relatorio.QRLNoCol.left + 118;
          Relatorio.QRLTitMatricula.left := Relatorio.QRLTitMatricula.left + 118;
          Relatorio.QRLTitProfessor.left := Relatorio.QRLTitProfessor.left + 118;
          Relatorio.LblTituloRel.Caption := 'RELAÇÃO DE PROFESSORES EM ATIVIDADE - POR MATÉRIA';
        end
        else
        begin
          //QRDBTMateria.Left := 3;
          //QRLTitMateria.left := 3;
          //QRDBTMatricula.left := QRDBTMatricula.left + 118;
          //QRDBTProfessor.left := QRDBTProfessor.left + 118;
          //QRLTitMatricula.left := QRLTitMatricula.left + 118;
          //QRLTitProfessor.left := QRLTitProfessor.left + 118;
          Relatorio.LblTituloRel.Caption := 'RELAÇÃO DE PROFESSORES EM ATIVIDADE - POR PROFESSOR';
        end;

        Relatorio.QRLParametros.Caption := strParamRel;
        //'Unidade: ' + EdtPesqEmpresa.Text + ' - Matéria: ' + cbMateria.Text;

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
  DLLRelMapaDisciplinaProf;

end.
