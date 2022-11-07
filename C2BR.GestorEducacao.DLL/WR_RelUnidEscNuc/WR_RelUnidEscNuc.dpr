library WR_RelUnidEscNuc;

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
  U_FrmRelUnidEscNuc in 'Relatorios\U_FrmRelUnidEscNuc.pas' {FrmRelUnidEscNuc};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelUnidEscNuc(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_NUCLEO:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelUnidEscNuc;
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

      SqlString := ' SELECT E.NU_INEP, E.CO_CPFCGC_EMP, E.SIGLA, E.NO_FANTAS_EMP, NUC.DE_NUCLEO, '+
                 ' CID.NO_CIDADE, BAI.NO_BAIRRO, E.CO_EMP,E.CO_DIR, E.CO_TEL1_EMP '+
                 ' FROM TB25_EMPRESA E '+
                 ' LEFT JOIN TB_NUCLEO_INST NUC ON NUC.CO_NUCLEO = E.CO_NUCLEO '+
                 ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = E.CO_CIDADE '+
                 ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = E.CO_BAIRRO '+
                 ' WHERE E.CO_EMP <> E.CO_EMP_PAI ';

        if strP_CO_NUCLEO <> nil then
        begin
          SqlString := SqlString + ' AND E.CO_NUCLEO = ' + strP_CO_NUCLEO;
        end;

        SQLString := SQLString + ' GROUP BY NUC.DE_NUCLEO, E.NU_INEP, E.CO_CPFCGC_EMP, E.SIGLA, E.NO_FANTAS_EMP, NUC.DE_NUCLEO, '+
                                 ' CID.NO_CIDADE, BAI.NO_BAIRRO,E.CO_EMP,E.CO_DIR, E.CO_TEL1_EMP ';
                                 //' ORDER BY E.NO_FANTAS_EMP ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelUnidEscNuc.Create(Nil);

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
  DLLRelUnidEscNuc;

end.
