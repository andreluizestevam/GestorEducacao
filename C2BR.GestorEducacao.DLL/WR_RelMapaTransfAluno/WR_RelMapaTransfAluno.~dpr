library WR_RelMapaTransfAluno;

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
  U_FrmRelMapaTransfAluno in 'Relatorios\U_FrmRelMapaTransfAluno.pas' {FrmRelMapaTransfAluno};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelMapaTransfAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaTransfAluno;
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
      SqlString := 'set language portuguese Select distinct e.sigla, e.NO_FANTAS_EMP, e.co_emp, ' +
                   '(select count(NU_REF_TRANSF) from TB_TRANSF_INTERNA ti '+
                   ' where ti.CO_EMP_ALU = e.co_emp and TI.CO_UNIDA_ATUAL = TI.CO_UNIDA_DESTI '+
                   ' and TI.DT_EFETI_TRANSF between ' + quotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM + ' 23:59:59') + ' ) as transEnTur,' +
                   '(select count(NU_REF_TRANSF) from TB_TRANSF_INTERNA ti '+
                   ' where ti.CO_EMP_ALU = e.co_emp and TI.CO_UNIDA_ATUAL <> TI.CO_UNIDA_DESTI '+
                   ' and TI.DT_EFETI_TRANSF between ' + quotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM + ' 23:59:59') + ' ) as transEnUni,' +
                   '(select count(CO_NIS_ALUNO) from TB_TRANSF_EXTERNA ti '+
                   ' where ti.CO_UNIDA_ATUAL = e.co_emp '+
                   ' and TI.DT_EFETI_TRANSF between ' + quotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM + ' 23:59:59') + ' ) as transExt ' +
                   ' from tb25_empresa e';

      SQLString := SqlString + ' order by e.NO_FANTAS_EMP';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelMapaTransfAluno.Create(Nil);

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

        Relatorio.QRLParam.Caption := 'Período: ' + strP_DT_INI + ' até ' + strP_DT_FIM;

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
  DLLRelMapaTransfAluno;

end.
