library WR_RelRelacFuncPorFuncao;

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
  U_FrmRelRelacFuncPorFuncao in 'Relatorios\U_FrmRelRelacFuncPorFuncao.pas' {FrmRelRelacFuncPorFuncao};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelRelacFuncPorFuncao(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_SITU_COL, strP_CO_FUN:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacFuncPorFuncao;
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

      SQLString := ' select distinct a.co_col,a.no_col,a.co_mat_col,a.CO_SEXO_COL,a.NU_TELE_RESI_COL,a.TP_DEF,a.NU_TELE_CELU_COL,'+
                   'a.DT_NASC_COL,a.DE_ENDE_COL,a.NU_ENDE_COL,a.DE_COMP_ENDE_COL, a.co_emp, a.CO_SITU_COL, e.sigla, fun.no_fun ' +
                   '  from tb03_colabor a ' +
                   '  join TB15_FUNCAO fun on fun.co_fun = a.co_fun ' +
                   '  join tb25_empresa e on e.co_emp = a.co_emp' +
                   ' where 1 = 1 ';


      if strP_CO_EMP_COL <> 'T' then
      begin
        SQLString := SQLString + ' and a.CO_EMP = '+ strP_CO_EMP_COL;
      end;

      if strP_CO_SITU_COL <> 'T' then
      begin
        SQLString := SQLString + ' and a.CO_SITU_COL = '+ QuotedStr(strP_CO_SITU_COL);
      end;

      if strP_CO_FUN <> 'T' then
      begin
        SQLString := SQLString + ' and a.co_fun = ' + strP_CO_FUN;
      end;    

      SQLString := SQLString + ' order by fun.no_fun,a.no_col';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelRelacFuncPorFuncao.Create(Nil);

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
  DLLRelRelacFuncPorFuncao;

end.
