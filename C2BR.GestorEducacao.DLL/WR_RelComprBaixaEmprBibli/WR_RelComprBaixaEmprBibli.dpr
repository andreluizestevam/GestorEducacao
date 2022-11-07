library WR_RelComprBaixaEmprBibli;

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
  U_FrmRelComprBaixaEmprBibli in 'Relatorios\U_FrmRelComprBaixaEmprBibli.pas' {FrmRelComprBaixaEmprBibli};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelComprBaixaEmprBibli(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_NUM_EMP, strP_CNPJ_INSTI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelComprBaixaEmprBibli;
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

      SqlString := 'select E.*,AI.CO_CTRL_INTERNO,EI.DT_PREV_DEVO_ACER,EI.DE_OBS_EMP,A.NO_ACERVO,EI.CO_ISBN_ACER,UB.TP_USU_BIB, co.no_col,'+
                   'co.co_mat_col, EI.DT_REAL_DEVO_ACER, EI.DE_OBS_EMP_BAIXA '+
                   'from TB36_EMPR_BIBLIOT E ' +
                   'join TB123_EMPR_BIB_ITENS EI on EI.CO_NUM_EMP = E.CO_NUM_EMP ' +
                   'join TB205_USUARIO_BIBLIOT UB on UB.ORG_CODIGO_ORGAO = E.ORG_CODIGO_ORGAO_USU and UB.CO_USUARIO_BIBLIOT = E.CO_USUARIO_BIBLIOT ' +
                   'join TB204_ACERVO_ITENS AI on AI.ORG_CODIGO_ORGAO = EI.ORG_CODIGO_ORGAO and AI.CO_ISBN_ACER = EI.CO_ISBN_ACER ' +
                   ' AND AI.CO_ACERVO_AQUISI = EI.CO_ACERVO_AQUISI and AI.CO_ACERVO_ITENS = EI.CO_ACERVO_ITENS ' +
                   'join TB35_ACERVO A on A.CO_ISBN_ACER = AI.CO_ISBN_ACER ' +
                   'join tb03_colabor co on co.co_col = ei.co_col_baixa and co.co_emp = ei.co_emp_col_baixa ' +
                   ' where E.CO_EMP = ' + strP_CO_EMP +
                   ' and EI.DT_REAL_DEVO_ACER is not null ' +
                   ' and E.CO_NUM_EMP = ' + strP_CO_NUM_EMP;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelComprBaixaEmprBibli.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
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
        //Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
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
  DLLRelComprBaixaEmprBibli;

end.
