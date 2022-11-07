library WR_RelAtivRealizProfessor;

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
  U_FrmRelAtivRealizProfessor in 'Relatorios\U_FrmRelAtivRealizProfessor.pas' {FrmRelAtivRealizProfessor},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelAtivRealizProfessor(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_COL, strP_CO_ANO_MES_MAT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAtivRealizProfessor;
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

      SqlString := 'Select Distinct G.*, A.NO_COL, A.CO_MAT_COL, CT.CO_SIGLA_TURMA as NO_TUR, A.CO_MAT_COL, CM.NO_MATERIA, P.DT_PREV_PLA, P.QT_CARG_HORA_PLA, P.DE_TEMA_AULA   ' +
               'From TB119_ATIV_PROF_TURMA G ' +
               ' left join TB17_PLANO_AULA P ON G.CO_EMP = P.CO_EMP AND P.CO_PLA_AULA = G.CO_PLA_AULA ' +
               ' join TB06_TURMAS T ON T.CO_TUR = G.CO_TUR AND G.CO_CUR = T.CO_CUR ' +
               ' join TB129_CADTURMAS CT ON T.CO_TUR = CT.CO_TUR ' +
               ' join TB03_COLABOR A ON G.CO_EMP = A.CO_EMP AND G.CO_COL_ATIV = A.CO_COL ' +
               ' JOIN TB02_MATERIA M ON M.CO_MAT = G.CO_MAT ' +
               ' join TB107_CADMATERIAS CM ON M.ID_MATERIA = CM.ID_MATERIA ' +
               'WHERE G.CO_CUR = ' + strP_CO_CUR +
               '   AND G.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
               ' AND G.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_MES_MAT) +
               //' AND G.NU_SEM_LET =  1 '  +
               ' AND G.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_COL <> nil then
        SqlString := SqlString + ' and G.CO_COL_ATIV = ' + strP_CO_COL;

      if strP_CO_TUR <> nil then
        SqlString := SqlString + ' and G.CO_TUR = ' + strP_CO_TUR;
      //fim da alteração

      SqlString := SqlString + ' order by G.CO_COL_ATIV, G.CO_CUR, G.CO_TUR, G.DT_ATIV_REAL ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelAtivRealizProfessor.Create(Nil);

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

        Relatorio.QRLParametros.Caption := strParamRel;
        //Módulo: - Série: - Turma: - Ano Referência:

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
  DLLRelAtivRealizProfessor;

end.
