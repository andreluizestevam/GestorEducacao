library WR_RelCurvaABCFreqFunc;

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
  U_FrmRelCurvaABCFreqFunc in 'Relatorios\U_FrmRelCurvaABCFreqFunc.pas' {FrmRelCurvaABCFreqFunc};

// Controle Administrativo > Controle Frequencia Funcion�rio
// Relat�rio: EMISS�O DA CURVA ABC DE FREQ��NCIA DE FUNCION�RIOS ***Fun��o*** (Ponto Padr�o/Livre)
// STATUS: OK
function DLLRelCurvaABCFreqFunc(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_FUN, strP_DT_INI, strP_DT_FIM, strP_TP_PONTO: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelCurvaABCFreqFunc;
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
      SqlString := ' SET LANGUAGE PORTUGUESE ' +
                     ' SELECT DISTINCT F.CO_FUN,F.NO_FUN '+
                     ' FROM TB15_FUNCAO F ';

      if strP_CO_FUN <> 'T' then
        SQLString := SQLString + ' WHERE F.CO_FUN = ' + strP_CO_FUN;

      SqlString := SqlString + ' GROUP BY F.CO_FUN,F.NO_FUN ORDER BY F.NO_FUN';


      // Cria uma inst�ncia do Relat�rio.
      Relatorio := TFrmRelCurvaABCFreqFunc.Create(Nil);

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
        
        Relatorio.QrlPeriodo.Caption := 'Per�odo de: ' + strP_DT_INI + ' � ' + strP_DT_FIM;
        Relatorio.QRLTipo.Caption := 'Presen�a';
        Relatorio.dtInicial := strP_DT_INI;
        Relatorio.dtFinal := strP_DT_FIM;
        Relatorio.codigoEmpresa := strP_CO_EMP;
        Relatorio.tipoPonto := strP_TP_PONTO;
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
  DLLRelCurvaABCFreqFunc;

end.
