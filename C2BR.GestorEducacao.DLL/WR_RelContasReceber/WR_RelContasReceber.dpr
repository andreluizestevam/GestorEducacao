library WR_RelContasReceber;

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
  U_FrmRelContasReceber in 'Relatorios\U_FrmRelContasReceber.pas' {FrmRelContasReceber};

// Controle Administrativo > Controle Frequencia Funcion�rio
// Relat�rio: EMISS�O DA CURVA ABC DE FREQ��NCIA DE FUNCION�RIOS ***Fun��o*** (Ponto Padr�o/Livre)
// STATUS: OK
function DLLRelContasReceber(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_IC_SIT_DOC, strP_NU_DOC, strP_CO_ALU, strP_DT_INI, strP_DT_FIM,
                             strP_DT_VEN_INI, strP_DT_VEN_FIM, strP_CO_MODU_CUR, strP_DE_MODU_CUR, strP_CO_CUR, strP_DE_CUR, strP_CO_TUR, strP_DE_TUR: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelContasReceber;
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

      // Monta a Consulta do Relat�rio.
      SqlString := 'SET LANGUAGE PORTUGUESE ' +
               ' Select C.*, H.DE_HISTORICO, A.NO_ALU ' +
               ' From TB47_CTA_RECEB C ' +
               ' LEFT JOIN TB39_HISTORICO H on C.CO_HISTORICO = H.CO_HISTORICO ' +
               ' JOIN TB07_ALUNO A on C.CO_ALU = A.CO_ALU and C.CO_EMP = A.CO_EMP ' +
               ' Where C.CO_EMP = '+ strP_CO_EMP;

      if strP_IC_SIT_DOC <> 'T' then
        SqlString := SqlString + ' and C.IC_SIT_DOC = ' + quotedStr(strP_IC_SIT_DOC);

      if strP_NU_DOC <> 'T' then
        SqlString := SqlString + ' and C.NU_DOC = ' + QuotedStr(strP_NU_DOC);

      if strP_CO_ALU <> 'T' then
        SqlString := SqlString + ' and C.CO_ALU = ' + strP_CO_ALU;

      if (strP_DT_INI <> 'T') and (strP_DT_FIM <> 'T') then
        SqlString := SqlString + ' and C.DT_CAD_DOC BETWEEN ' + '''' + strP_DT_INI + '''' + ' and ' + '''' + strP_DT_FIM + '''';

      if (strP_DT_VEN_INI <> 'T') and (strP_DT_VEN_FIM <> 'T') then
        SqlString := SqlString + ' and C.DT_VEN_DOC BETWEEN ' + '''' + strP_DT_VEN_INI + '''' + ' and ' + '''' + strP_DT_VEN_FIM + '''';

      if strP_CO_MODU_CUR <> 'T' then
        SqlString := SqlString + ' and C.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' and C.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_TUR <> 'T' then
        SqlString := SqlString + ' and C.CO_TUR = ' + strP_CO_TUR;

      SqlString := SqlString + ' Order By A.NO_ALU, A.CO_ALU, C.DT_CAD_DOC ';

      // Cria uma inst�ncia do Relat�rio.
      Relatorio := TFrmRelContasReceber.Create(Nil);

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
        
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        if (strP_DT_INI <> 'T') and (strP_DT_FIM <> 'T') then
          Relatorio.QRLPeriodo.Caption := 'Per�odo de: ' + strP_DT_INI + ' � ' + strP_DT_FIM;

        Relatorio.QRLTipoDoc.Caption := '';
        Relatorio.QRLCancel.Caption := '';

        Relatorio.QRLCancel.Caption := 'Modalidade: ' + strP_DE_MODU_CUR + ' - S�rie: ' + strP_DE_CUR + ' - Turma: ' + strP_DE_TUR;

        Relatorio.QRLTipoDoc.Caption := strParamRel;
        //'Documento(s) em Abertos';
        //'Documento(s) Cancelados';
        //'Documento(s) Quitados';
        //'Todos os Documentos';

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
  DLLRelContasReceber;

end.