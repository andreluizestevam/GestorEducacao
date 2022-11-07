library WR_RelDemUnidEscBai;

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
  U_FrmRelDemUnidEscBai in 'Relatorios\U_FrmRelDemUnidEscBai.pas' {FrmRelDemUnidEscBai};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelDemUnidEscBai(strIdentFunc, strPathReportGenerate, strReportName , strP_CO_EMP, strP_CO_UF, strP_CO_CIDADE, strP_CO_BAIRRO:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDemUnidEscBai;
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

      SqlString := ' SELECT DISTINCT BAI.NO_BAIRRO, '+
                    ' ( SELECT COUNT(E.CO_EMP) FROM TB25_EMPRESA E '+
                    ' 	WHERE BAI.CO_BAIRRO = E.CO_BAIRRO '+
                    '   AND E.CO_EMP <> E.CO_EMP_PAI '+
                    ' ) QTUnEn, '+
                    ' ( SELECT COUNT(COL.CO_COL) FROM TB03_COLABOR COL '+
                    ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP '+
                    ' 	WHERE BAI.CO_BAIRRO = E.CO_BAIRRO '+
                    ' 	AND COL.FLA_PROFESSOR = ' + QuotedStr('N') +
                    ' ) QTFUN, '+
                    ' ( SELECT COUNT(COL.CO_COL) FROM TB03_COLABOR COL '+
                    ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP '+
                    ' 	WHERE COL.FLA_PROFESSOR = ' + QuotedStr('N') +
                    ' ) QTOTALFUN, '+
                    ' ( SELECT COUNT(COL.CO_COL) FROM TB03_COLABOR COL '+
                    ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP '+
                    ' 	WHERE BAI.CO_BAIRRO = E.CO_BAIRRO '+
                    ' 	AND COL.FLA_PROFESSOR = ' + QuotedStr('S') +
                    ' ) QTPROF, '+
                    ' ( SELECT COUNT(COL.CO_COL) FROM TB03_COLABOR COL '+
                    ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP '+
                    ' 	WHERE COL.FLA_PROFESSOR = ' + QuotedStr('S') +
                    ' ) QTOTALPROF, '+
                    ' ( SELECT COUNT(ALU.CO_ALU) FROM TB07_ALUNO ALU '+
                    ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = ALU.CO_EMP '+
                    ' 	WHERE BAI.CO_BAIRRO = E.CO_BAIRRO '+
                    ' ) QTALU, '+
                    ' ( SELECT COUNT(ALU.CO_ALU) FROM TB07_ALUNO ALU '+
                    ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = ALU.CO_EMP '+
                    ' ) QTOTALALU '+
                    ' FROM TB25_EMPRESA E '+
                    ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = E.CO_BAIRRO WHERE 1 = 1 ';

      if (strP_CO_UF <> nil) then
        SQLString := SQLString + ' AND E.CO_UF_EMP = ' + quotedStr(strP_CO_UF);

      if (strP_CO_CIDADE <> nil) then
        SQLString := SQLString + ' AND E.CO_CIDADE = ' + strP_CO_CIDADE;

      if (strP_CO_BAIRRO <> nil) and (strP_CO_BAIRRO <> 'T') then
        SQLString := SQLString + ' AND E.CO_BAIRRO = ' + strP_CO_BAIRRO;

      SQLString := SQLString + ' ORDER BY BAI.NO_BAIRRO ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDemUnidEscBai.Create(Nil);

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
  DLLRelDemUnidEscBai;

end.
