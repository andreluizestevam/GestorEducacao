library WR_RelCurvaABCSolicitacoes;

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
  U_FrmRelCurvaABCSolicitacoes in 'Relatorios\u_FrmRelCurvaABCSolicitacoes.pas' {FrmRelCurvaABCSolicitacoes};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelCurvaABCSolicitacoes(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP,
 strP_TP_SOLIC, strP_DT_INI, strP_DT_FIM, strP_ISENT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelCurvaABCSolicitacoes;
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

      SqlString := 'SET LANGUAGE PORTUGUESE ' +
               'Select H.CO_TIPO_SOLI, TP.NO_TIPO_SOLI, ' +
               ' Count(H.CO_TIPO_SOLI) NUTOTSOL, SUM(VA_SOLI_ATEN) VL_TOT_SOL ' +
               'From TB65_HIST_SOLICIT H, TB64_SOLIC_ATEND S, TB66_TIPO_SOLIC TP ' +
               'Where H.CO_EMP = S.CO_EMP ' +
               '  AND H.CO_SOLI_ATEN = S.CO_SOLI_ATEN ' +
               '  AND H.CO_TIPO_SOLI = TP.CO_TIPO_SOLI ' +
               ' AND S.CO_SIT not in (' + QuotedStr('C') + ')' +
               ' and H.CO_EMP = ' + strP_CO_EMP;

      if strP_TP_SOLIC <> 'S' then
        SqlString := SqlString + ' AND H.CO_SITU_SOLI = ' + QuotedStr(strP_TP_SOLIC);

      if (strP_DT_INI <> nil) and (strP_DT_FIM <> nil)  then
        SqlString := SqlString + ' AND (S.DT_SOLI_ATEN BETWEEN ' + QuotedStr(strP_DT_INI) +
                                 ' AND ' + QuotedStr(strP_DT_FIM) + ')';

      if strP_ISENT <> nil then
        SqlString := SqlString + ' AND S.CO_ISEN_TAXA = ' + QuotedStr(strP_ISENT);

      SqlString := SqlString + ' Group By H.CO_TIPO_SOLI, TP.NO_TIPO_SOLI ' +
                               ' Order by NUTOTSOL desc';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelCurvaABCSolicitacoes.Create(Nil);

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
        Relatorio.QrlSituacao.Caption := strParamRel;

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
  DLLRelCurvaABCSolicitacoes;

end.
