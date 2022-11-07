library WR_RelRelacSolicEstagio;

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
  U_FrmRelRelacSolicEstagio in 'Relatorios\U_FrmRelRelacSolicEstagio.pas' {FrmRelRelacSolicEstagio};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelRelacSolicEstagio(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_OFERT_ESTAG, strP_ID_CANDID_ESTAG:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacSolicEstagio;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString: string;
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

      SqlString := ' SELECT oe.NO_OFERT_ESTAG, co.DT_CADASTRO, ce.TP_CANDID_ESTAG, ce.NO_CANDID_ESTAG,co.CO_STATUS,oe.CO_EMP,' +
                 'ce.co_alu, ce.co_emp_alu, ce.co_col, ce.co_emp_col ' +
                 ' FROM TB219_CANDID_OFERTAS co '+
                 ' JOIN TB188_OFERT_ESTAG oe ON oe.CO_OFERT_ESTAG = co.CO_OFERT_ESTAG '+
                 ' JOIN TB218_CANDID_ESTAGIO ce ON ce.ID_CANDID_ESTAG = co.ID_CANDID_ESTAG '+
                 ' WHERE oe.CO_EMP = ' + strP_CO_EMP;

        if strP_CO_OFERT_ESTAG <> 'T' then
          SQLString := SQLString + ' AND co.CO_OFERT_ESTAG = ' + strP_CO_OFERT_ESTAG;

        if strP_ID_CANDID_ESTAG <> 'T' then
          SQLString := SQLString + ' AND co.ID_CANDID_ESTAG = ' + strP_ID_CANDID_ESTAG;

        SQLString := SQLString + ' ORDER BY oe.NO_OFERT_ESTAG, co.DT_CADASTRO ';
        //oe.CO_OFERT_ESTAG,

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacSolicEstagio.Create(Nil);

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
  DLLRelRelacSolicEstagio;

end.
