library WR_RelMeusAcessos;

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
  U_FrmRelMeusAcessos in 'Relatorios\U_FrmRelMeusAcessos.pas' {FrmRelMeusAcessos};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMeusAcessos(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_COL, strP_DT_INI, strP_DT_FIM:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMeusAcessos;
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

      SqlString := 'set language portuguese select LA.*, CO.NO_COL, CO.CO_MAT_COL, AM.nomModulo, EMC.sigla, EM.NO_FANTAS_EMP '+
                   'from TB236_LOG_ATIVIDADES LA ' +
                   'join TB25_EMPRESA EM on EM.CO_EMP = LA.CO_EMP_ATIVI_LOG ' +
                   'join TB03_COLABOR CO on CO.CO_COL = LA.CO_COL and CO.CO_EMP = LA.CO_EMP ' +
                   'join TB25_EMPRESA EMC on EMC.CO_EMP = LA.CO_EMP ' +
                   'join ADMMODULO AM on AM.ideAdmModulo = LA.ideAdmModulo ' +
                   ' where LA.DT_ATIVI_LOG between ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM);

      if strP_CO_EMP_COL <> 'T' then
        SqlString := SqlString + ' and LA.CO_EMP = ' + strP_CO_EMP_COL;

      if strP_CO_COL <> 'T' then
        SqlString := SqlString + ' and LA.CO_COL = ' + strP_CO_COL;

      SqlString := SqlString +
                   '  order by LA.CO_EMP_ATIVI_LOG, LA.DT_ATIVI_LOG';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMeusAcessos.Create(Nil);

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
  DLLRelMeusAcessos;

end.
