library WR_RelMapaEstaticSolic;

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
  U_FrmRelMapaEstaticSolic in 'Relatorios\U_FrmRelMapaEstaticSolic.pas' {FrmRelMapaEstaticSolic};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaEstaticSolic(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_DE_EMP,
 strP_TP_SIT, strP_DE_SIT, strP_CO_ANO_REF, strP_TP_VISU: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaEstaticSolic;
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

      SqlString := ' SELECT TS.CO_TIPO_SOLI, SA.ANO_SOLI_ATEN,  TS.NO_TIPO_SOLI           '+
               '  FROM TB64_SOLIC_ATEND SA, TB65_HIST_SOLICIT HS, TB66_TIPO_SOLIC TS  '+
               ' WHERE SA.CO_ALU = HS.CO_ALU                                          '+
               '    AND SA.CO_CUR = HS.CO_CUR                                         '+
               ' AND SA.CO_SIT = ' + QuotedStr('A') +
               '    AND SA.CO_EMP = HS.CO_EMP                                         '+
               '    AND SA.CO_SOLI_ATEN = HS.CO_SOLI_ATEN                             '+
               '    AND HS.CO_TIPO_SOLI = TS.CO_TIPO_SOLI                             ';

      if strP_CO_EMP <> 'T' then
        SqlString := SqlString + ' AND SA.CO_EMP = ' + strP_CO_EMP;

      if strP_TP_SIT <> 'S' then
        SqlString := SqlString + ' AND HS.CO_SITU_SOLI = ' + QuotedStr(strP_TP_SIT);

      if strP_CO_ANO_REF <> 'T' then
      begin
        SqlString := SqlString + ' AND SA.ANO_SOLI_ATEN = '+ quotedStr(strP_CO_ANO_REF);
      end;

      SqlString := SqlString + ' GROUP BY TS.CO_TIPO_SOLI, SA.ANO_SOLI_ATEN, TS.NO_TIPO_SOLI';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaEstaticSolic.Create(Nil);

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

        if strP_CO_EMP <> nil then
          Relatorio.codEmpresa := strToInt(strP_CO_EMP)
        else
          Relatorio.codEmpresa := 0;

        Relatorio.qrlSituacao.Caption := strP_DE_SIT;
        Relatorio.staSolicitacao := strP_TP_SIT;

        if strP_CO_ANO_REF <> 'T' then
          Relatorio.anoSelecionado := strP_CO_ANO_REF
        else
          Relatorio.anoSelecionado := '';

        if strP_CO_ANO_REF <> 'T' then
          Relatorio.qrlUnidade.Caption := strP_DE_EMP + '  -  Ano: ' + strP_CO_ANO_REF
        else
          Relatorio.qrlUnidade.Caption := strP_DE_EMP + '  -  Ano: Todos';

        Relatorio.itemvisualiza := 0;
        
        if strP_TP_VISU = 'A' then
          Relatorio.itemtipo := 0
        else
          Relatorio.itemtipo := 1;

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
  DLLRelMapaEstaticSolic;

end.
