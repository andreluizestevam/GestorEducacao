library WR_RelExtIndEstagio;

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
  U_FrmRelExtIndEstagio in 'Relatorios\U_FrmRelExtIndEstagio.pas' {FrmRelExtIndEstagio};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelExtIndEstagio(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_ID_EFETI_ESTAGIO: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelExtIndEstagio;
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

      SqlString := ' select ee.*,OE.*,ce.TP_CANDID_ESTAG,ce.NO_CANDID_ESTAG,ce.CO_EMP_ALU,ce.CO_ALU, STATUS = (CASE OE.CO_STA_OFERT_ESTAG ' +
                    			'WHEN ' + quotedStr('A') + ' THEN ' + QuotedStr('Ativo') +
                    			'WHEN ' + quotedStr('I') + ' THEN ' + QuotedStr('Inativo') +
                    			'END), CI.NO_CIDADE, BA.NO_BAIRRO, ee.VL_REMUN as remunEnt,ce.CO_EMP_COL,ce.CO_COL ' +
                  'from TB221_EFETI_ESTAGIO ee ' +
                  'join TB219_CANDID_OFERTAS co on co.ID_CANDID_OFERTAS = ee.ID_CANDID_OFERTAS ' +
                  'join TB188_OFERT_ESTAG OE on OE.CO_OFERT_ESTAG = co.CO_OFERT_ESTAG ' +
                  ' JOIN TB904_CIDADE CI on CI.CO_CIDADE = OE.CO_CIDADE_OFERT_ESTAG' +
                  ' JOIN TB905_BAIRRO BA on BA.CO_BAIRRO = OE.CO_BAIRRO_EMP_OFERT_ESTAG' +
                  ' join TB218_CANDID_ESTAGIO ce on ce.ID_CANDID_ESTAG = co.ID_CANDID_ESTAG ' +
                  ' where ee.ID_EFETI_ESTAGIO = ' + strP_ID_EFETI_ESTAGIO;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelExtIndEstagio.Create(Nil);

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
  DLLRelExtIndEstagio;

end.
