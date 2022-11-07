library WR_RelEspenhoDiscCursadas;

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
  U_FrmRelEspenhoDiscCursadas in 'Relatorios\U_FrmRelEspenhoDiscCursadas.pas' {FrmRelEspenhoDiscCursadas};

// Gestão de Atividades Acadêmico/Pedagógica > Informações Operacionais de Alunos
// Relatório: EMISSÃO DO ESPELHO DICIPLINAS CURSADAS
// STATUS: OK
function DLLRelEspenhoDiscCursadas(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR : PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelEspenhoDiscCursadas;
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
          ' Select Distinct H.*,CM.NO_RED_MATERIA as NO_MATERIA,CM.NO_SIGLA_MATERIA,M.QT_CARG_HORA_MAT,mm.co_sit_mat,' +
          '   M.QT_CRED_MAT,MD.DE_MODU_CUR,ct.co_sigla_turma as no_tur, H.VL_MEDIA_FINAL,a.no_alu,a.nu_nis,mm.co_alu_cad,c.no_cur,C.CO_SIGL_CUR ' +
          ' From TB079_HIST_ALUNO H,TB02_MATERIA M,TB107_CADMATERIAS CM,TB44_MODULO MD,' +
          '   TB06_TURMAS T,tb129_cadturmas ct,tb07_aluno a, tb08_matrcur mm, tb01_curso c ' +
          ' Where h.co_modu_cur = md.co_modu_cur and H.CO_EMP = M.CO_EMP   And H.CO_MAT = M.CO_MAT   AND M.ID_MATERIA = CM.ID_MATERIA ' +
          '   and h.co_alu = a.co_alu and h.co_emp = a.co_emp ' +
          '   and h.co_cur = c.co_cur and h.co_emp = c.co_emp ' +
          '   and h.co_alu = mm.co_alu and h.co_emp = mm.co_emp ' +
          '   And T.CO_EMP = H.CO_EMP   And T.CO_CUR = H.CO_CUR   And T.CO_TUR = H.CO_TUR ' +
          '   And T.CO_TUR = CT.CO_TUR and h.co_modu_cur = t.co_modu_cur and c.co_modu_cur = h.co_modu_cur and h.co_ano_ref = mm.co_ano_mes_mat' +
          '   And H.CO_EMP = ' + strP_CO_EMP ;

      if strP_CO_ALU <> 'T' then
        SqlString := SqlString + ' and h.co_alu = ' + strP_CO_ALU;

      if strP_CO_ANO_REF <> Nil then
        SqlString := SqlString + ' and h.co_ano_ref = ' + QuotedStr(strP_CO_ANO_REF);

      if strP_CO_MODU_CUR <> Nil then
        SqlString := SqlString + ' and h.co_modu_cur = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SQLString := SQLString + ' and h.co_cur = ' + strP_CO_CUR;

      if strP_CO_TUR <> 'T' then
        SQLString := SQLString + ' and h.co_tur = ' + strP_CO_TUR;

      SqlString := SqlString + ' ORDER BY A.NO_ALU ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelEspenhoDiscCursadas.Create(Nil);

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
  DLLRelEspenhoDiscCursadas;

end.
