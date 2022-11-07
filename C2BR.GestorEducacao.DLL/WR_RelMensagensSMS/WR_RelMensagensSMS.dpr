library WR_RelMensagensSMS;

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
  U_FrmRelMensagensSMS in 'Relatorios\U_FrmRelMensagensSMS.pas' {FrmRelMensagensSMS};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelMensagensSMS(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_TP_CONTAT_SMS, strP_CO_EMP_COL, strP_CO_COL, strP_DT_INI, strP_DT_FIM, strP_STATUS:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMensagensSMS;
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

      SqlString := 'set language portuguese select LA.*, CO.NO_COL, EM.NO_FANTAS_EMP, CO.NU_TELE_CELU_COL '+
                   'from TB249_MENSAG_SMS LA ' +
                   'join TB03_COLABOR CO on CO.CO_COL = LA.CO_COL_EMISSOR and CO.CO_EMP = LA.CO_EMP_EMISSOR ' +
                   'join TB25_EMPRESA EM on EM.CO_EMP = CO.CO_EMP ' +
                   ' where LA.DT_ENVIO_MENSAG_SMS between ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM);

      if strP_CO_TP_CONTAT_SMS <> 'T' then
        SqlString := SqlString + ' and LA.CO_TP_CONTAT_SMS = ' + QuotedStr(strP_CO_TP_CONTAT_SMS);

      if strP_CO_EMP_COL <> 'T' then
        SqlString := SqlString + ' and LA.CO_EMP_EMISSOR = ' + strP_CO_EMP_COL;

      if strP_CO_COL <> 'T' then
        SqlString := SqlString + ' and LA.CO_COL_EMISSOR = ' + strP_CO_COL;

      if strP_STATUS <> 'T' then
        SqlString := SqlString + ' and LA.FLA_SMS_SUCESS = ' + QuotedStr(strP_STATUS);

      SqlString := SqlString +
                   '  order by LA.DT_ENVIO_MENSAG_SMS, CO.NO_COL';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelMensagensSMS.Create(Nil);

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
        //Relatorio.QRLParametros.Caption := strParamRel;

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
  DLLRelMensagensSMS;

end.