library WR_RelGradeCurricular;

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
  U_FrmRelGradeCurricular in 'Relatorios\U_FrmRelGradeCurricular.pas' {FrmRelGradeCurricular};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelGradeCurricular(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_MODU_CUR,
 strP_CO_CUR, strP_CO_ANO_INI, strP_CO_ANO_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelGradeCurricular;
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

      SqlString := 'Select Distinct G.*, C.NO_CUR, MT.NO_MATERIA, MT.NO_SIGLA_MATERIA, M.QT_CARG_HORA_MAT, M.QT_CRED_MAT, D.DE_MODU_CUR ' +
               ' From TB43_GRD_CURSO G,  TB01_CURSO C, TB02_MATERIA M, TB44_MODULO D , TB107_CADMATERIAS MT ' +
               ' WHERE G.CO_EMP = C.CO_EMP AND G.CO_EMP = M.CO_EMP AND ' +
               '       G.CO_CUR = C.CO_CUR AND ' +
               '       G.CO_MAT = M.CO_MAT AND ' +
               '       G.CO_MODU_CUR = D.CO_MODU_CUR AND ' +
               '       G.CO_MODU_CUR = C.CO_MODU_CUR AND ' +
               '       M.ID_MATERIA = MT.ID_MATERIA ' +
               ' and   G.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_CUR <> nil then
        SqlString := SqlString + ' AND G.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_MODU_CUR <> nil then
        SqlString := SqlString + ' AND G.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_ANO_INI <> nil then
        SqlString := SqlString + ' AND G.CO_ANO_GRADE >= ' + QuotedStr(strP_CO_ANO_INI);

      if strP_CO_ANO_FIM <> nil then
        SqlString := SqlString + ' AND G.CO_ANO_GRADE <= ' + QuotedStr(strP_CO_ANO_FIM);

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelGradeCurricular.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.codigoEmpresa := strP_CO_EMP;

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
  DLLRelGradeCurricular;

end.
