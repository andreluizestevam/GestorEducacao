library WR_RelMapaHistAlunoSerTur;

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
  U_FrmRelMapaHistAlunoSerTur in 'Relatorios\U_FrmRelMapaHistAlunoSerTur.pas' {FrmRelMapaHistAlunoSerTur};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaHistAlunoSerTur(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_MAT, strP_CO_ALU, strP_TP_AVAL:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaHistAlunoSerTur;
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
                     ' SELECT DISTINCT EM.SIGLA,CUR.NO_CUR,CUR.CO_SIGL_CUR,d.nu_nis,a.CO_EMP_ALU, '+
                     '        a.CO_TUR, a.ID_MATERIA, '+
                     '        a.CO_ANO, '+
                     '        a.CO_MODU_CUR, '+
                     '        a.CO_CUR, '+
                     '        ct.CO_SIGLA_TURMA as no_tur, '+
                     '        a.CO_ALU, '+
                     '        d.NO_ALU, '+
                     '        CM.NO_RED_MATERIA, '+
                     '        a.DT_NOTA_ATIV, '+
                     '        m.CO_ALU_CAD, '+
                     ' CO_TIPO_ATIV = (CASE a.CO_TIPO_ATIV '+
                     '             WHEN ' + '''' + 'ANO' + '''' + ' THEN ' + '''' + 'Aula Normal' + '''' +
                     '             WHEN ' + '''' + 'AEX' + '''' + ' THEN ' + '''' + 'Aula Extra' + '''' +
                     '             WHEN ' + '''' + 'ARE' + '''' + ' THEN ' + '''' + 'Aula Reforço' + '''' +
                     '             WHEN ' + '''' + 'ARC' + '''' + ' THEN ' + '''' + 'Aula de Recuperação' + '''' +
                     '             WHEN ' + '''' + 'TES' + '''' + ' THEN ' + '''' + 'Teste' + '''' +
                     '             WHEN ' + '''' + 'PRO' + '''' + ' THEN ' + '''' + 'Prova' + '''' +
                     '             WHEN ' + '''' + 'TRA' + '''' + ' THEN ' + '''' + 'Trabalho' + '''' +
                     '             WHEN ' + '''' + 'AGR' + '''' + ' THEN ' + '''' + 'Atividade em Grupo' + '''' +
                     '             WHEN ' + '''' + 'ATE' + '''' + ' THEN ' + '''' + 'Atividade Externa' + '''' +
                     '             WHEN ' + '''' + 'ATI' + '''' + ' THEN ' + '''' + 'Atividade Interna' + '''' +
                     '             WHEN ' + '''' + 'OUT' + '''' + ' THEN ' + '''' + 'Outros' + '''' +
                     '             ELSE ' + '''' + '' + '''' +
                     '           END), '+
                     '        a.VL_NOTA '+
                     ' FROM TB49_NOTA_ATIV_ALUNO a'+
                     ' join TB107_CADMATERIAS CM on cm.id_materia = a.id_materia and cm.CO_EMP = a.CO_EMP_MAT'+
                     ' join TB06_TURMAS c on c.co_tur = a.co_tur' +
                     ' join tb129_cadturmas ct on ct.co_tur = c.co_tur' +
                     ' join TB01_CURSO CUR on CUR.co_cur = a.co_cur'+
                     ' join TB07_ALUNO d on d.co_alu = a.co_alu and d.co_emp = a.co_emp_alu'+
                     ' join TB08_MATRCUR m on m.co_alu = a.co_alu and a.CO_EMP_ALU = m.CO_EMP and m.co_ano_mes_mat = a.co_ano'+
                     ' and a.co_cur = m.co_cur'+
                     ' join TB25_EMPRESA EM on a.co_emp_alu = em.co_emp'+
                     ' WHERE a.CO_EMP_ALU = ' + strP_CO_EMP +
                     ' AND   a.CO_ANO = ' + strP_CO_ANO_MES_MAT;

        if strP_CO_MODU_CUR <> nil then
        begin
          SQLString := SQLString + ' and a.co_modu_cur = ' + strP_CO_MODU_CUR;
        end;

        if strP_CO_CUR <> 'T' then
        begin
          SQLString := SQLString + ' and a.co_cur = ' + strP_CO_CUR;
        end;

        if strP_CO_TUR <> 'T' then
        begin
          SQLString := SQLString + ' and a.co_tur = ' + strP_CO_TUR;
        end;

        if strP_CO_MAT <> 'T' then
        begin
          SQLString := SQLString + ' and a.co_mat = ' + strP_CO_MAT;
        end;

        if strP_TP_AVAL <> 'T' then
          SQLString := SQLString + ' and a.TP_AVAL = ' + QuotedStr(strP_TP_AVAL);

        SqlString := SqlString + ' Order by CUR.NO_CUR, '+
                                 '          ct.CO_SIGLA_TURMA, '+
                                 '          d.NO_ALU, '+
                                 '          a.DT_NOTA_ATIV, '+
                                 '          a.CO_TIPO_ATIV, '+
                                 '          a.CO_ANO, '+
                                 '          a.CO_MODU_CUR, '+
                                 '          CM.NO_RED_MATERIA ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaHistAlunoSerTur.Create(Nil);

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
        //'Ano: ' + edtAno.Text + ' - Módulo: ' + cbModulo.Text + ' -  ' + Sys_DescricaoTipoCurso + ': ' + cbSerie.Text +
        //' - Turma: ' + cbTurma.Text + ' - Tipo de Avaliação: ' + cbTpAvaPorSerTur.Text + ' - Matéria: ' + cbMatPorAlu.Text +
        //' - UE: ' + QryRelatorioSIGLA.AsString;

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
  DLLRelMapaHistAlunoSerTur;

end.
