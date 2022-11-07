library WR_RelMapaEstatDistrDiplo;

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
  U_FrmRelMapaEstatDistrDiplo in 'Relatorios\U_FrmRelMapaEstatDistrDiplo.pas' {FrmRelMapaEstatDistrDiplo};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelMapaEstatDistrDiplo(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ANO: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaEstatDistrDiplo;
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

      // Monta a Consulta do Relatório.
      SqlString := 'Select distinct Year(U.DT_SOLI_DIP) as ano,' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 1 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as jan, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 2 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as fev, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 3 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as mar, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 4 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as abr, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 5 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as mai, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 6 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as jun,' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 7 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as jul, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 8 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as ago, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 9 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as sete, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 10 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as out, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 11 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as nov, ' +
                  '(select COUNT(d.CO_DIPLOMA) from tb16_diploma d ' +
                  'where year(d.DT_SOLI_DIP) = YEAR(U.DT_SOLI_DIP) ' +
                  'and MONTH(d.DT_SOLI_DIP) = 12 ' +
                  'and d.CO_EMP = U.CO_EMP ' +
                  ') as dez ' +
                  'from TB16_DIPLOMA U ' +
                  'where U.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_ANO <> 'T' then
        SQLString := SQLString + ' and Year(U.DT_SOLI_DIP) = ' + strP_CO_ANO;

      SQLString := SqlString + ' order by Year(U.DT_SOLI_DIP)';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelMapaEstatDistrDiplo.Create(Nil);

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
  DLLRelMapaEstatDistrDiplo;

end.
