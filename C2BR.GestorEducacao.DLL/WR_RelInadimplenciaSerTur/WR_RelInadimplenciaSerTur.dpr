library WR_RelInadimplenciaSerTur;

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
  U_RelInadimplenciaSerTur in 'Relatorios\U_RelInadimplenciaSerTur.pas' {FrmRelInadimplenciaSerTur};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelInadimplenciaSerTur(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_DT_INI, strP_DT_FIM:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelInadimplenciaSerTur;
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
                 'SELECT Distinct C.*,A.NO_ALU,A.NU_NIS,R.NU_CPF_RESP,C.CO_ALU ' +
                 'FROM TB47_CTA_RECEB C'+
                 ' join tb07_aluno a on c.co_alu = a.co_alu and c.co_emp = a.co_emp'+
                 ' join tb108_responsavel r on a.co_resp = r.co_resp ' +
                 ' WHERE C.IC_SIT_DOC = ' + '''' + 'A' + '''' +
                 '  AND  DT_VEN_DOC <= ' + quotedStr(strP_DT_FIM) +
                 '  AND  DT_VEN_DOC >= ' + quotedStr(strP_DT_INI);

      if strP_CO_ANO_REF <> 'T' then
        SQLString := SQLString + '  AND C.CO_ANO_MES_MAT = ' + strP_CO_ANO_REF;

      if strP_CO_MODU_CUR <> 'T' then
        SQLString := SQLString + '  AND C.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SQLString := SQLString + '  AND C.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_TUR <> 'T' then
        SQLString := SQLString + '  AND C.CO_TUR = ' + strP_CO_TUR;

      SQLString := SQLString + ' ORDER BY A.NO_ALU,C.DT_VEN_DOC,C.NU_PAR';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelInadimplenciaSerTur.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
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
        // Atualiza Campos do Relat�rio Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        Relatorio.QRLParametros.Caption := strParamRel;
        Relatorio.strDataIni := strP_DT_INI;
        Relatorio.strDataFim := strP_DT_FIM;

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relat�rio Gerado com Sucesso.
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
  
  //Relat�rios
  DLLRelInadimplenciaSerTur;

end.
