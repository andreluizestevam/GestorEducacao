library WR_RelMapaEstaticEmpr;

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
  U_FrmRelMapaEstaticEmpr in 'Relatorios\U_FrmRelMapaEstaticEmpr.pas' {FrmRelMapaEstaticEmpr};

// Controle Administrativo > Controle Frequencia Funcion�rio
// Relat�rio: EMISS�O DA CURVA ABC DE FREQ��NCIA DE FUNCION�RIOS ***Fun��o*** (Ponto Padr�o/Livre)
// STATUS: OK
function DLLRelMapaEstaticEmpr(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ANO_REFER, strP_CNPJ_INSTI: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaEstaticEmpr;
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
      SqlString := ' SET LANGUAGE PORTUGUESE SELECT DISTINCT A.CO_ISBN_ACER, eb.CO_EMP, A.NO_ACERVO FROM tb123_Empr_Bib_Itens ebi, TB35_ACERVO A, TB36_EMPR_BIBLIOT EB ' +
                 ' WHERE eb.co_emp = ' + strP_CO_EMP +
                 ' AND ebi.CO_NUM_EMP = EB.CO_NUM_EMP ' +
                 ' AND A.CO_ISBN_ACER = ebi.CO_ISBN_ACER ' +
                 ' and EB.DT_EMPR_BIBLIOT > ' + QuotedStr('1/1/' + strP_CO_ANO_REFER) +
                 ' and EB.DT_EMPR_BIBLIOT < ' + QuotedStr('31/12/' + strP_CO_ANO_REFER) +
                 ' order by a.no_acervo';

      // Cria uma inst�ncia do Relat�rio.
      Relatorio := TFrmRelMapaEstaticEmpr.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
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
        // Atualiza Campos do Relat�rio Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;
        
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.qrlAno.Caption := strP_CO_ANO_REFER;
        Relatorio.Ano := StrToInt(strP_CO_ANO_REFER);

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
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
  DLLRelMapaEstaticEmpr;

end.
