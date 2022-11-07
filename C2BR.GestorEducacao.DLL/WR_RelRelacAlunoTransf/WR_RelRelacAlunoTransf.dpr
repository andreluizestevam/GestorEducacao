library WR_RelRelacAlunoTransf;

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
  U_FrmRelRelacAlunoTransf in 'Relatorios\U_FrmRelRelacAlunoTransf.pas' {FrmRelRelacAlunoTransf};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
// STATUS: OK
function DLLRelRelacAlunoTransf(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_REF, strP_DE_EMP_REF, strP_TP_TRANSF, strP_DT_INI, strP_DT_FIM, strP_CNPJ_INSTI: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacAlunoTransf;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString, paramRel : string;
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

      if strP_TP_TRANSF = 'I' then
      begin
        SqlString := 'set language portuguese select distinct E.sigla as DESTINO, TI.DE_OBSER_TRANSF as MOTIVO, TI.DT_EFETI_TRANSF as DT_TRANSF,' +
                 '  A.NO_ALU, MM.CO_ALU_CAD, MO.CO_SIGLA_MODU_CUR,' +
                 '  CT.CO_SIGLA_TURMA, C.CO_SIGL_CUR ' +
                 ' from TB08_MATRCUR MM, TB07_ALUNO A, TB44_MODULO MO, ' +
                 '      TB06_TURMAS T, TB01_CURSO C, TB129_CADTURMAS CT, TB25_EMPRESA E, TB_TRANSF_INTERNA TI ' +
                 ' where MM.CO_EMP = A.CO_EMP ' +
                 ' and MM.CO_EMP = T.CO_EMP ' +
                 ' and MM.CO_EMP = C.CO_EMP ' +
                 ' and TI.CO_UNIDA_DESTI = E.CO_EMP ' +
                 ' and A.CO_ALU = MM.CO_ALU ' +
                 ' and MM.CO_TUR = T.CO_TUR ' +
                 ' and CT.CO_TUR = T.CO_TUR ' +
                 ' and MO.CO_MODU_CUR = MM.CO_MODU_CUR ' +
                 ' and MM.CO_MODU_CUR = T.CO_MODU_CUR ' +
                 ' and T.CO_MODU_CUR = CT.CO_MODU_CUR ' +
                 ' and MM.CO_CUR = C.CO_CUR ' +
                 ' and MM.CO_ALU_CAD = TI.CO_MATRI_ATUAL ' +
                 ' and MM.CO_EMP = TI.CO_EMP_ALU ' +
                 ' and TI.DT_EFETI_TRANSF between ' + quotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM + ' 23:59:59') +
                 ' AND TI.CO_UNIDA_ATUAL = TI.CO_UNIDA_DESTI' +
                 ' AND TI.CO_EMP_ALU = ' + strP_CO_EMP_REF +
                 ' ORDER BY A.NO_ALU';
      end
      else if strP_TP_TRANSF = 'E' then
      begin
        SqlString := 'set language portuguese select distinct TI.NM_UNIDA_DESTI as DESTINO, TI.DE_MOTIVO_TRANSF as MOTIVO, TI.DT_EFETI_TRANSF as DT_TRANSF,' +
                 '  A.NO_ALU, MM.CO_ALU_CAD, MO.CO_SIGLA_MODU_CUR,' +
                 '  CT.CO_SIGLA_TURMA, C.CO_SIGL_CUR ' +
                 ' from TB08_MATRCUR MM, TB07_ALUNO A, TB44_MODULO MO, ' +
                 '      TB06_TURMAS T, TB01_CURSO C, TB129_CADTURMAS CT, TB_TRANSF_EXTERNA TI ' +
                 ' where MM.CO_EMP = A.CO_EMP ' +
                 ' and MM.CO_EMP = T.CO_EMP ' +
                 ' and MM.CO_EMP = C.CO_EMP ' +
                 ' and A.CO_ALU = MM.CO_ALU ' +
                 ' and MM.CO_TUR = T.CO_TUR ' +
                 ' and CT.CO_TUR = T.CO_TUR ' +
                 ' and MO.CO_MODU_CUR = MM.CO_MODU_CUR ' +
                 ' and MM.CO_MODU_CUR = T.CO_MODU_CUR ' +
                 ' and T.CO_MODU_CUR = CT.CO_MODU_CUR ' +
                 ' and MM.CO_CUR = C.CO_CUR ' +
                 ' and MM.CO_ALU_CAD = TI.CO_MATRI_ATUAL ' +
                 ' and MM.CO_EMP = TI.CO_UNIDA_ATUAL ' +
                 ' and TI.DT_EFETI_TRANSF between ' + quotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM + ' 23:59:59') +
                 ' AND TI.CO_UNIDA_ATUAL = ' + strP_CO_EMP_REF +
                 ' ORDER BY A.NO_ALU';
      end
      else
      begin
        SqlString := 'set language portuguese select distinct E.sigla as DESTINO, TI.DE_OBSER_TRANSF as MOTIVO, TI.DT_EFETI_TRANSF as DT_TRANSF,' +
                 '  A.NO_ALU, MM.CO_ALU_CAD, MO.CO_SIGLA_MODU_CUR,' +
                 '  CT.CO_SIGLA_TURMA, C.CO_SIGL_CUR ' +
                 ' from TB08_MATRCUR MM, TB07_ALUNO A, TB44_MODULO MO, ' +
                 '      TB06_TURMAS T, TB01_CURSO C, TB129_CADTURMAS CT, TB25_EMPRESA E, TB_TRANSF_INTERNA TI ' +
                 ' where MM.CO_EMP = A.CO_EMP ' +
                 ' and MM.CO_EMP = T.CO_EMP ' +
                 ' and MM.CO_EMP = C.CO_EMP ' +
                 ' and TI.CO_UNIDA_DESTI = E.CO_EMP ' +
                 ' and A.CO_ALU = MM.CO_ALU ' +
                 ' and MM.CO_TUR = T.CO_TUR ' +
                 ' and CT.CO_TUR = T.CO_TUR ' +
                 ' and MO.CO_MODU_CUR = MM.CO_MODU_CUR ' +
                 ' and MM.CO_MODU_CUR = T.CO_MODU_CUR ' +
                 ' and T.CO_MODU_CUR = CT.CO_MODU_CUR ' +
                 ' and MM.CO_CUR = C.CO_CUR ' +
                 ' and MM.CO_ALU_CAD = TI.CO_MATRI_ATUAL ' +
                 ' and MM.CO_EMP = TI.CO_EMP_ALU ' +
                 ' and TI.DT_EFETI_TRANSF between ' + quotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM + ' 23:59:59') +
                 ' AND TI.CO_UNIDA_ATUAL <> TI.CO_UNIDA_DESTI'+
                 ' AND TI.CO_EMP_ALU = ' + strP_CO_EMP_REF +
                 ' ORDER BY A.NO_ALU';
      end;

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelRelacAlunoTransf.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
      Relatorio.QryRelatorio.Close;

      ShowMessage(strP_CNPJ_INSTI);
      ShowMessage(retornaConexao(strP_CNPJ_INSTI));
      DM.Conn.ConnectionString := retornaConexao(strP_CNPJ_INSTI);
      DM.Conn.Open;
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
        // Atualiza Campos do Relat�rio Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        paramRel :=  'Unidade: ' + strP_DE_EMP_REF;

        if strP_TP_TRANSF = 'I' then
          paramRel := paramRel + ' - Tipo de Transfer�ncia: Entre Turmas'
        else if strP_TP_TRANSF = 'E' then
          paramRel := paramRel + ' - Tipo de Transfer�ncia: Externa'
        else
          paramRel := paramRel + ' - Tipo de Transfer�ncia: Entre Unidades';

        paramRel := paramRel + ' - Per�odo: ' + strP_DT_INI + ' at� ' + strP_DT_FIM;

        Relatorio.QRLParametros.Caption := paramRel;

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relat�rio Gerado com Sucesso.
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
  
  //Relat�rios
  DLLRelRelacAlunoTransf;

end.