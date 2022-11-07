library WR_RelMapaCursoTurmaProf;

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
  U_FrmRelMapaCursoTurmaProf in 'Relatorios\U_FrmRelMapaCursoTurmaProf.pas' {FrmRelMapaCursoTurmaProf};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaCursoTurmaProf(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP,strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaCursoTurmaProf;
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
               ' SELECT DISTINCT P.CO_MAT_COL, P.NO_COL, CM.NO_RED_MATERIA, C.NO_CUR, C.CO_SIGL_CUR, CT.CO_SIGLA_TURMA, P.CO_ESPEC, P.CO_CURFORM, ' +
               ' TC.NO_TPCON,P.NU_TELE_RESI_COL,P.NU_TELE_CELU_COL, ' +
               ' SITUACAO = (CASE P.CO_SITU_COL ' +
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
               '          	END),MO.DE_MODU_CUR, PA.CO_MODU_CUR, PA.CO_CUR, PA.CO_TUR ' +
               ' FROM TB03_COLABOR P ' +
               ' LEFT JOIN TB20_TIPOCON TC ON TC.CO_TPCON = P.CO_TPCON ' +
               ' JOIN TB_RESPON_MATERIA PA ON PA.CO_COL_RESP = P.CO_COL ' +
               ' JOIN TB44_MODULO MO ON MO.CO_MODU_CUR = PA.CO_MODU_CUR ' +
               ' JOIN TB01_CURSO C ON C.CO_CUR = PA.CO_CUR ' +
               ' JOIN TB06_TURMAS T ON T.CO_TUR = PA.CO_TUR ' +
               ' JOIN TB129_CADTURMAS CT ON T.CO_TUR = CT.CO_TUR AND T.CO_MODU_CUR = CT.CO_MODU_CUR ' +
               ' LEFT JOIN TB02_MATERIA M ON M.CO_MAT = PA.CO_MAT ' +
               ' LEFT JOIN TB107_CADMATERIAS CM ON CM.ID_MATERIA = M.ID_MATERIA ' +
               ' WHERE P.FLA_PROFESSOR = ' + QuotedStr('S') +
               ' AND P.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_MODU_CUR <> nil then
      begin
        SQLString := SQLString + ' and PA.co_modu_cur = ' + strP_CO_MODU_CUR;
      end;

      if strP_CO_CUR <> nil then
      begin
        SQLString := SQLString + ' and PA.co_cur = ' + strP_CO_CUR;
      end;

      if strP_CO_TUR <> nil then
      begin
        SQLString := SQLString + ' and PA.co_tur = ' + strP_CO_TUR;
      end;

      SqlString := SqlString +
                   ' ORDER BY P.NO_COL, CM.NO_RED_MATERIA ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaCursoTurmaProf.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.codigoEmpresa := strP_CO_EMP;

        Relatorio.QRLParametros.Caption := strParamRel;

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
  DLLRelMapaCursoTurmaProf;

end.
