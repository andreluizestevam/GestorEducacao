library WR_RelInformacaoCurso;

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
  U_FrmRelInformacaoCurso in 'Relatorios\U_FrmRelInformacaoCurso.pas' {FrmRelInformacaoCurso};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelInformacaoCurso(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_MODU_CUR,
 strP_CO_CUR, strP_CO_NIVEL_CUR, strP_CO_DPTO_CUR, strP_CO_SUB_DPTO_CUR, strP_CO_SITU: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelInformacaoCurso;
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

      SqlString := 'Select C.*, D.NO_DPTO_CUR, CC.NO_COOR_CUR as NO_SUBDPTO_CUR, I.*,MO.DE_MODU_CUR ' +
               'From TB01_CURSO C ' +
               ' LEFT JOIN TB77_DPTO_CURSO D on C.CO_EMP = D.CO_EMP AND C.CO_DPTO_CUR = D.CO_DPTO_CUR ' +
               ' LEFT JOIN TB68_COORD_CURSO CC on C.CO_EMP = CC.CO_EMP AND C.CO_DPTO_CUR = CC.CO_DPTO_CUR AND C.CO_SUB_DPTO_CUR = CC.CO_COOR_CUR ' +
               ' LEFT JOIN TB19_INFOR_CURSO I on C.CO_CUR = I.CO_CUR AND C.CO_EMP = I.CO_EMP ' +
               ' JOIN TB44_MODULO MO on C.CO_MODU_CUR = MO.CO_MODU_CUR ' +
               'Where C.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_CUR <> nil then
        SqlString := SqlString + ' AND C.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_NIVEL_CUR <> nil then
        SqlString := SqlString + ' AND C.CO_NIVEL_CUR = ' + quotedStr(strP_CO_NIVEL_CUR);

      if strP_CO_DPTO_CUR <> nil then
        SqlString := SqlString + ' AND C.CO_DPTO_CUR = ' + strP_CO_DPTO_CUR;

      if strP_CO_MODU_CUR <> nil then
        SqlString := SqlString + ' AND C.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_SUB_DPTO_CUR <> nil then
        SqlString := SqlString + ' AND C.CO_SUB_DPTO_CUR = ' + strP_CO_SUB_DPTO_CUR;

      if strP_CO_SITU <> nil then
        SqlString := SqlString + ' AND C.CO_SITU = ' + quotedStr(strP_CO_SITU);

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelInformacaoCurso.Create(Nil);

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
  DLLRelInformacaoCurso;

end.
