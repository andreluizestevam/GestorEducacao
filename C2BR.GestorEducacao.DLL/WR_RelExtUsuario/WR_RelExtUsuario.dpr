library WR_RelExtUsuario;

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
  U_FrmRelExtUsuario in 'Relatorios\U_FrmRelExtUsuario.pas' {FrmRelExtUsuario};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelExtUsuario(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_TP_USUARIO, strP_CNPJ_INSTI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelExtUsuario;
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

      SqlString := 'select E.*,AI.CO_CTRL_INTERNO,A.NO_ACERVO,EI.CO_ISBN_ACER,UB.TP_USU_BIB,EI.DT_REAL_DEVO_ACER,UB.NO_USU_BIB,EI.DT_PREV_DEVO_ACER from TB36_EMPR_BIBLIOT E ' +
                   'join TB123_EMPR_BIB_ITENS EI on EI.CO_NUM_EMP = E.CO_NUM_EMP ' +
                   'join TB205_USUARIO_BIBLIOT UB on UB.ORG_CODIGO_ORGAO = E.ORG_CODIGO_ORGAO_USU and UB.CO_USUARIO_BIBLIOT = E.CO_USUARIO_BIBLIOT ' +
                   'join TB204_ACERVO_ITENS AI on AI.ORG_CODIGO_ORGAO = EI.ORG_CODIGO_ORGAO and AI.CO_ISBN_ACER = EI.CO_ISBN_ACER ' +
                   ' AND AI.CO_ACERVO_AQUISI = EI.CO_ACERVO_AQUISI and AI.CO_ACERVO_ITENS = EI.CO_ACERVO_ITENS ' +
                   'join TB35_ACERVO A on A.CO_ISBN_ACER = AI.CO_ISBN_ACER ' +
                   ' where E.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_TP_USUARIO = 'A' then
        SqlString := SqlString + ' and UB.co_alu is not null ';

      if strP_CO_TP_USUARIO = 'F' then
        SqlString := SqlString + ' and UB.co_col is not null ';

      if strP_CO_TP_USUARIO = 'O' then
        SqlString := SqlString + ' and UB.co_alu is null and UB.co_col is null ';

      SqlString := SqlString +
                   '  order by UB.NO_USU_BIB,A.NO_ACERVO ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelExtUsuario.Create(Nil);

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
        //Relatorio.QRLParametros.Caption := strParamRel;

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
  DLLRelExtUsuario;

end.
