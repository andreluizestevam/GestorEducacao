library WR_RelRelacOfertEstagio;

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
  U_FrmRelRelacOfertEstagio in 'Relatorios\U_FrmRelRelacOfertEstagio.pas' {FrmRelRelacOfertEstagio};

function DLLRelRelacOfertEstagio(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_ESTAG:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacOfertEstagio;
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

      SqlString := 'select OE.*,IE.NO_FAN_INST_ESTAG, ' +
                   'EFET = (CASE OE.FLA_POSS_EFETI_OFERT_ESTAG ' +
                      'WHEN ' + quotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                      'WHEN ' + quotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   'END) '+
      {'STATUS = (CASE OE.CO_STA_OFERT_ESTAG ' +
                    			'WHEN ' + quotedStr('A') + ' THEN ' + QuotedStr('Ativo') +
                    			'WHEN ' + quotedStr('I') + ' THEN ' + QuotedStr('Inativo') +
                    			'END)' +}
                  'from TB188_OFERT_ESTAG OE'+
                  ' JOIN TB195_INST_ESTAGIO IE on IE.CO_INST_ESTAG = OE.CO_INST_ESTAG ' +
                  ' where OE.CO_EMP = ' + strP_CO_EMP +
                  ' and OE.CO_STA_OFERT_ESTAG = ' + QuotedStr('A');

      if strP_CO_EMP_ESTAG <> 'T' then
      begin
        SQLString := SQLString + ' and OE.CO_INST_ESTAG = ' + strP_CO_EMP_ESTAG;
      end;

      SQLString := SQLString + ' order by OE.DE_VAGA_OFERT_ESTAG';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacOfertEstagio.Create(Nil);

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
  DLLRelRelacOfertEstagio;

end.
