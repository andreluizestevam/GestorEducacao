library WR_RelDespesasFixas;

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
  U_FrmRelDespesasFixas in 'Relatorios\U_FrmRelDespesasFixas.pas' {FrmRelDespesasFixas};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelDespesasFixas(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_IC_SIT_RECDES, strP_CO_CON_RECDES, strP_DT_INI, strP_DT_FIM, strP_CO_CPF_CNPJ_RECDES,
                             strP_NO_CLI_RECDES: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDespesasFixas;
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
               ' Select C.*,forn.DE_RAZSOC_FORN,forn.CO_CPFCGC_FORN, H.DE_HISTORICO, P.CO_GRUP_CTA, P.CO_SGRUP_CTA, P.CO_CONTA_PC, P.DE_CONTA_PC, ' +
               {'   TIPOCLIENTE = (CASE cli.TP_CLIENTE '+
               '		WHEN ' + QuotedStr('J') + ' THEN ' + QuotedStr('Pessoa Jurídica') +
               '		WHEN ' + QuotedStr('F') + ' THEN ' + QuotedStr('Pessoa Física') +
               '				 END) ' +}
               '   SITUACAO_DES_FIX = (CASE C.IC_SIT_RECDES '+
               '		WHEN ' + QuotedStr('A') + ' THEN ' + QuotedStr('Em Aberto') +
               '		WHEN ' + QuotedStr('C') + ' THEN ' + QuotedStr('Cancelado') +
               '		WHEN ' + QuotedStr('Q') + ' THEN ' + QuotedStr('Quitado') +
               '				 END) '+
               ' From TB37_RECDES_FIXA C '+
               ' join tb41_fornec forn on forn.co_forn = c.co_forn ' +
               ' JOIN TB39_HISTORICO H ON H.CO_HISTORICO = C.CO_HISTORICO '+
               ' JOIN TB56_PLANOCTA P ON P.CO_SEQU_PC = C.CO_SEQU_PC '+
               ' JOIN TB086_TIPO_DOC TP ON TP.CO_TIPO_DOC = C.CO_TIPO_DOC '+
               ' Where C.TP_CON_RECDES = ' + '''' + 'D' + '''' +
               '   AND C.CO_EMP = ' + strP_CO_EMP;

      if strP_IC_SIT_RECDES <> 'T' then
        SqlString := SqlString + ' and C.IC_SIT_RECDES = '+ QuotedStr(strP_IC_SIT_RECDES);

      if strP_CO_CON_RECDES <> 'T' then
        SqlString := SqlString + ' and C.CO_CON_RECDES = ' + QuotedStr(strP_CO_CON_RECDES);

      if (strP_DT_INI <> 'T') and (strP_DT_FIM <> 'T') then
        SqlString := SqlString + ' and C.DT_CAD_RECDES BETWEEN ' + '''' + strP_DT_INI + ' 00:00:00' + '''' + ' and ' + '''' + strP_DT_FIM + ' 23:59:59' + '''';

      if strP_CO_CPF_CNPJ_RECDES <> 'T' then
        SqlString := SqlString + ' and forn.CO_CPFCGC_FORN = ' + '''' + strP_CO_CPF_CNPJ_RECDES + '''';

      if strP_NO_CLI_RECDES <> 'T' then
        SqlString := SqlString + ' and forn.DE_RAZSOC_FORN Like ' + '''' + '%' + strP_NO_CLI_RECDES + '%' + '''';

      SqlString := SqlString + ' order by forn.DE_RAZSOC_FORN';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelDespesasFixas.Create(Nil);

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
        //'Documentos: ' + RGTipoDoc.Items.Strings[RGTipoDoc.ItemIndex];

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
  DLLRelDespesasFixas;

end.
