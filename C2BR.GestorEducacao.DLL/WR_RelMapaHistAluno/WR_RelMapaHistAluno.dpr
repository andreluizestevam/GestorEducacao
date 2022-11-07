library WR_RelMapaHistAluno;

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
  U_FrmRelMapaHistAluno in 'Relatorios\U_FrmRelMapaHistAluno.pas' {FrmRelMapaHistAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaHistAluno(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_MAT, strP_CO_ALU, strP_TP_AVAL:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaHistAluno;
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
                     ' SELECT DISTINCT CO.CO_MAT_COL, CO.NO_COL, EM.SIGLA,CUR.NO_CUR,CUR.CO_SIGL_CUR,d.nu_nis,a.CO_EMP, '+
                     '        a.CO_TUR, '+
                     '        a.CO_ANO_MES_MAT, '+ 
                     '        a.NU_SEM_LET, '+
                     '        a.CO_MODU_CUR, '+
                     '        a.CO_CUR, '+
                     '        ct.CO_SIGLA_TURMA as no_tur, '+
                     '        a.CO_ALU, '+
                     '        d.NO_ALU, '+
                     '        CM.NO_RED_MATERIA, '+
                     '        a.DT_PROV, '+
                     '        m.CO_ALU_CAD, '+
                     ' TP_AVAL = (CASE a.tp_aval '+
                     '             WHEN ' + '''' + 'TA' + '''' + ' THEN ' + '''' + 'TA - Teste/Avaliação' + '''' +
                     '             WHEN ' + '''' + 'TR' + '''' + ' THEN ' + '''' + 'TR - Trabalho/Atividades' + '''' +
                     '             WHEN ' + '''' + 'PM' + '''' + ' THEN ' + '''' + 'PM - Prova Mensal' + '''' +
                     '             WHEN ' + '''' + 'PB' + '''' + ' THEN ' + '''' + 'PB - Prova Bimestral' + '''' +
                     '             WHEN ' + '''' + 'PS' + '''' + ' THEN ' + '''' + 'PS - Prova Semestral' + '''' +
                     '             WHEN ' + '''' + 'PR' + '''' + ' THEN ' + '''' + 'PR - Prova de Recuperação' + '''' +
                     '             WHEN ' + '''' + 'PF' + '''' + ' THEN ' + '''' + 'PF - Prova Final' + '''' +
                     '             WHEN ' + '''' + 'RF' + '''' + ' THEN ' + '''' + 'RF - Recuperação Final' + '''' +
                     '             ELSE ' + '''' + '' + '''' +
                     '           END), '+
                     '        a.VL_NOTA '+
                     ' FROM TB49_NOTA_ALUNO a'+
                     ' JOIN TB02_MATERIA b on a.co_mat = b.co_mat'+
                     ' join TB107_CADMATERIAS CM on cm.id_materia = b.id_materia'+
                     ' join TB06_TURMAS c on c.co_tur = a.co_tur' +
                     ' join tb129_cadturmas ct on ct.co_tur = c.co_tur' +
                     ' join TB01_CURSO CUR on CUR.co_cur = a.co_cur'+
                     ' join TB07_ALUNO d on d.co_alu = a.co_alu and d.co_emp = a.co_emp'+
                     ' join TB08_MATRCUR m on m.co_alu = a.co_alu and a.CO_EMP = m.CO_EMP and m.co_ano_mes_mat = a.co_ano_mes_mat'+
                     ' and a.co_cur = m.co_cur'+
                     ' join TB25_EMPRESA EM on a.co_emp = em.co_emp'+
                     ' left join TB_RESPON_MATERIA PA on A.CO_EMP = PA.CO_EMP  AND'+
                     ' PA.CO_EMP = A.CO_EMP   AND PA.CO_CUR = A.CO_CUR   AND PA.CO_TUR = A.CO_TUR   AND'+
                     ' PA.CO_MAT = A.CO_MAT'+
                     ' left join TB03_COLABOR CO on CO.CO_COL = PA.CO_PROFES_RESP and CO.CO_EMP = PA.CO_EMP '+
                     ' WHERE a.CO_EMP = ' + strP_CO_EMP +
                     ' AND   a.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_MES_MAT);

        if strP_CO_MAT <> 'T' then
        begin
          SQLString := SQLString + ' and a.co_mat = ' + strP_CO_MAT;
        end;

        if strP_TP_AVAL <> 'T' then
          SQLString := SQLString + ' and a.TP_AVAL = ' + QuotedStr(strP_TP_AVAL);

        if strP_CO_ALU <> nil then
          SqlString := SqlString + ' and d.co_alu = ' + strP_CO_ALU;

        SqlString := SqlString + ' Order by a.CO_CUR, '+
                                 '          a.CO_ANO_MES_MAT, '+
                                 '          a.NU_SEM_LET, '+
                                 '          ct.CO_SIGLA_TURMA, '+
                                 '          a.CO_MODU_CUR, '+
                                 '          a.DT_PROV, '+
                                 '          a.TP_AVAL, '+
                                 '          d.NO_ALU, '+
                                 '          CM.NO_RED_MATERIA ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaHistAluno.Create(Nil);

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
        Relatorio.QrlParam.Caption := strParamRel;

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
  DLLRelMapaHistAluno;

end.
