library WR_RelResumoContasReceber;

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
  U_FrmRelResumoContasReceber in 'Relatorios\U_FrmRelResumoContasReceber.pas' {FrmRelResumoContasReceber};

// Controle Administrativo > Controle Frequencia Funcion�rio
// Relat�rio: EMISS�O DA CURVA ABC DE FREQ��NCIA DE FUNCION�RIOS ***Fun��o*** (Ponto Padr�o/Livre)
// STATUS: OK
function DLLRelResumoContasReceber(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_IC_SIT_DOC, strP_DT_INI, strP_DT_FIM, strP_NU_DOC, strP_CO_ALU,
                                   strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_MES_MAT, strP_CO_TUR: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelResumoContasReceber;
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

      // Monta a Consulta do Relat�rio.
      SqlString := 'SET LANGUAGE PORTUGUESE                                                 '+
               'Select S.NO_CUR, C.*, CT.CO_SIGLA_TURMA, M.DE_MODU_CUR, A.NO_ALU,A.NU_NIS, H.DE_HISTORICO '+
               'From TB47_CTA_RECEB C ' +
               'LEFT JOIN TB01_CURSO S ON C.CO_CUR = S.CO_CUR AND C.CO_EMP = S.CO_EMP ' +
					     'JOIN TB07_ALUNO A ON C.CO_ALU = A.CO_ALU AND C.CO_EMP = A.CO_EMP ' +
				       'LEFT JOIN TB06_TURMAS T ON C.CO_TUR = T.CO_TUR AND C.CO_EMP = T.CO_EMP AND T.CO_CUR = C.CO_CUR ' +
               'LEFT JOIN TB129_CADTURMAS CT ON CT.CO_TUR = T.CO_TUR AND CT.CO_MODU_CUR = T.CO_MODU_CUR ' +
               'LEFT JOIN TB44_MODULO M ON C.CO_MODU_CUR = M.CO_MODU_CUR ' +
               'LEFT JOIN TB39_HISTORICO H ON C.CO_HISTORICO = H.CO_HISTORICO  ' +
               'Where C.CO_EMP = ' + strP_CO_EMP;

      if strP_IC_SIT_DOC <> 'T' then
        SqlString := SqlString + ' and C.IC_SIT_DOC = ' + QuotedStr(strP_IC_SIT_DOC);

      if (strP_DT_INI <> nil) and (strP_DT_FIM <> nil) then
        SqlString := SqlString + ' and C.DT_VEN_DOC BETWEEN ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM);


      if strP_NU_DOC <> 'T' then
        SqlString := SqlString + ' and C.NU_DOC = ' + QuotedStr(strP_NU_DOC);

      if strP_CO_ALU <> 'T' then
        SqlString := SqlString + ' and C.CO_ALU = ' + strP_CO_ALU;

      if strP_CO_MODU_CUR <> 'T' then
          SqlString := SqlString + ' AND C.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' and C.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_ANO_MES_MAT <> 'T' then
        SqlString := SqlString + ' AND C.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_MES_MAT);

      if strP_CO_TUR <> 'T' then
        SqlString := SqlString + ' AND C.CO_TUR = ' + strP_CO_TUR;

      SqlString := SqlString + ' Order by C.DT_CAD_DOC,S.CO_CUR, C.CO_ANO_MES_MAT, C.NU_SEM_LET, M.CO_MODU_CUR, T.CO_TUR, '+
                               'C.CO_ALU, C.NU_DOC, C.NU_PAR, C.DT_VEN_DOC, C.VR_PAR_DOC ' +
                               ' for browse ';

      // Cria uma inst�ncia do Relat�rio.
      Relatorio := TFrmRelResumoContasReceber.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
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
        // Atualiza Campos do Relat�rio Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        paramRel := 'Unidade: ' + Relatorio.QryCabecalhoRel.FieldByName('NO_FANTAS_EMP').AsString;

        if strP_CO_ANO_MES_MAT <> 'T' then
          paramRel := paramRel + ' - Ano Refer�ncia: ' + Trim(strP_CO_ANO_MES_MAT)
        else
          paramRel := paramRel + ' - Ano Refer�ncia: Todos';

        if strP_CO_MODU_CUR <> 'T' then
          paramRel := paramRel + ' - Modalidade: ' + Relatorio.QryRelatorio.FieldByName('DE_MODU_CUR').AsString
        else
          paramRel := paramRel + ' - Modalidade: Todas';

        if strP_CO_CUR <> 'T' then
          paramRel := paramRel + ' - S�rie: ' + Relatorio.QryRelatorio.FieldByName('NO_CUR').AsString
        else
          paramRel := paramRel + ' - S�rie: Todas';

        if strP_CO_CUR <> 'T' then
          paramRel := paramRel + ' - Turma: ' + Relatorio.QryRelatorio.FieldByName('CO_SIGLA_TURMA').AsString
        else
          paramRel := paramRel + ' - Turma: Todas';

        Relatorio.QRLParametros.Caption := paramRel; 

        Relatorio.LblTituloRel.Caption := strParamRel;

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
  DLLRelResumoContasReceber;

end.
