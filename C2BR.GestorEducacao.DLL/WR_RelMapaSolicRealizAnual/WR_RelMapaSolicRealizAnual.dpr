library WR_RelMapaSolicRealizAnual;

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
  U_FrmRelMapaSolicRealizAnual in 'Relatorios\U_FrmRelMapaSolicRealizAnual.pas' {FrmRelMapaSolicRealizAnual};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaSolicRealizAnual(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,
 strP_CO_ANO_INI, strP_CO_ANO_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaSolicRealizAnual;
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

      SQLString := ' SELECT DISTINCT TS.NO_TIPO_SOLI, S.CO_TIPO_SOLI'+
                      ' FROM TB65_HIST_SOLICIT S '+
                      '	JOIN TB66_TIPO_SOLIC TS ON TS.CO_TIPO_SOLI = S.CO_TIPO_SOLI '+
                      ' WHERE YEAR(S.DT_ENTR_SOLI) between ' + QuotedStr(strP_CO_ANO_INI) + ' and ' + QuotedStr(strP_CO_ANO_FIM) +
                      ' AND S.CO_EMP = ' + strP_CO_EMP +
                      ' ORDER BY TS.NO_TIPO_SOLI ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaSolicRealizAnual.Create(Nil);

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
        Relatorio.codigoEmpresa := strP_CO_EMP;
        Relatorio.QrlAnoRef.Caption := strP_CO_ANO_INI + ' à ' + strP_CO_ANO_FIM;

        Relatorio.anoIni := strP_CO_ANO_INI;
        Relatorio.anoFim := strP_CO_ANO_FIM;

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
  DLLRelMapaSolicRealizAnual;

end.
