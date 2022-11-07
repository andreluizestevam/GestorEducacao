library WR_RelCurvaABCFreqProfInst;

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
  U_FrmRelCurvaABCFreqProfInst in 'Relatorios\U_FrmRelCurvaABCFreqProfInst.pas' {FrmRelCurvaABCFreqProfInst};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE PROFESSORES ***Instituição*** (Ponto Padrão)
// STATUS: OK
function DLLRelCurvaABCFreqProfInst(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_DT_INI, strP_DT_FIM, strP_TP_PONTO: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelCurvaABCFreqProfInst;
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
      
      SqlString := ' SET LANGUAGE PORTUGUESE ' +
                     ' SELECT DISTINCT C.NO_COL,C.CO_COL,E.NO_RAZSOC_EMP, E.CO_EMP '+
                     ' FROM TB03_COLABOR C ' +
                     ' JOIN TB25_EMPRESA E ON C.CO_EMP = E.CO_EMP ' +
                     ' WHERE C.FLA_PROFESSOR = ' + QuotedStr('S');

      if strP_CO_EMP <> nil then
        SqlString := SqlString + ' AND C.CO_EMP = ' + strP_CO_EMP;

      SqlString := SqlString +
                   ' GROUP BY E.CO_EMP,E.NO_RAZSOC_EMP, C.NO_COL,C.CO_COL  order by E.CO_EMP, E.NO_RAZSOC_EMP, C.NO_COL';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelCurvaABCFreqProfInst.Create(Nil);

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
        
        Relatorio.QrlPeriodo.Caption := 'Período de: ' + strP_DT_INI + ' à ' + strP_DT_FIM;
        Relatorio.QRLTipo.Caption := 'Presença';
        Relatorio.dtInicial := strP_DT_INI;
        Relatorio.dtFinal := strP_DT_FIM;
        Relatorio.tipoPonto := strP_TP_PONTO;
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
  DLLRelCurvaABCFreqProfInst;

end.
