library WR_RelContasPagar;

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
  U_FrmRelContasPagar in 'Relatorios\U_FrmRelContasPagar.pas' {FrmRelContasPagar};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelContasPagar(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_IC_SIT_DOC, strP_CO_FORN, strP_NU_DOC, strP_DT_INI, strP_DT_FIM, strP_DT_VEN_DOC: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelContasPagar;
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

      // Monta a Consulta do Relatório.
      SqlString := 'SET LANGUAGE PORTUGUESE ' +
               ' Select C.*, H.DE_HISTORICO ' +
               ' From TB38_CTA_PAGAR C ' +
               ' LEFT JOIN TB39_HISTORICO H on C.CO_HISTORICO = H.CO_HISTORICO' +
               ' Where C.CO_EMP = ' + strP_CO_EMP;

      if strP_IC_SIT_DOC <> 'T' then
        SqlString := SqlString + ' and C.IC_SIT_DOC = ' + QuotedStr(strP_IC_SIT_DOC);

      if strP_CO_FORN <> 'T' then
        SqlString := SqlString + ' and C.CO_FORN = ' + strP_CO_FORN;

      if strP_NU_DOC <> 'T' then
        SqlString := SqlString + ' and C.NU_DOC = ' + QuotedStr(strP_NU_DOC);

      if (strP_DT_INI <> nil) and (strP_DT_FIM <> nil) then
        SqlString := SqlString + ' and C.DT_CAD_DOC BETWEEN ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM);

      if strP_DT_VEN_DOC <> 'T' then
        SqlString := SqlString + ' and C.DT_VEN_DOC = ' + QuotedStr(strP_DT_VEN_DOC);

      SqlString := SqlString + ' Order By C.DT_CAD_DOC ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelContasPagar.Create(Nil);

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

        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
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
  DLLRelContasPagar;

end.
