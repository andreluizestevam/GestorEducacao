library WR_RelCurvaABCCP;

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
  U_FrmRelCurvaABCCP in 'Relatorios\U_FrmRelCurvaABCCP.pas' {FrmRelCurvaABCCP};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelCurvaABCCP(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_IC_SIT_DOC, strP_DT_INI, strP_DT_FIM:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelCurvaABCCP;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString, VarSqlSoma : string;
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

      if (strP_IC_SIT_DOC = 'A') or (strP_IC_SIT_DOC = 'C') then
        VarSqlSoma := 'VR_PAR_DOC'
      else
        VarSqlSoma := 'VR_PAG';

      SqlString := ' SET LANGUAGE PORTUGUESE ' +
                   ' Select A.CO_FORN, B.NO_FAN_FOR, ' +
                   ' COUNT(A.NU_DOC) TOT_DOC, SUM(A.' + VarSqlSoma + ') VL_TOTAL ' +
                   ' From TB38_CTA_PAGAR A, TB41_FORNEC B ' +
                   ' Where A.CO_FORN = B.CO_FORN ' +
                   ' AND A.CO_EMP = ' + strP_CO_EMP;

      if strP_IC_SIT_DOC <> 'T' then
        SqlString := SqlString + ' and A.IC_SIT_DOC = ' + quotedStr(strP_IC_SIT_DOC);

      if (strP_DT_INI <> 'T') and (strP_DT_FIM <> 'T') then
        SqlString := SqlString + ' AND A.DT_CAD_DOC BETWEEN ' + QuotedStr(strP_DT_INI) + ' And ' +  QuotedStr(strP_DT_FIM);

      SqlString := SqlString + ' GROUP BY A.CO_FORN, B.NO_FAN_FOR Order by VL_TOTAL desc';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelCurvaABCCP.Create(Nil);

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
        // Atualiza Campos do Relat�rio Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.QrlSituacao.Caption := strParamRel;
        //'( Per�odo de: ' + edtDataMovIni.Text + ' � ' + edtDataMovFim.Text + ' - Situa��o: ' + RGTipoPag.Items.Strings[RGTipoPag.itemindex] + ' )';

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
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
  DLLRelCurvaABCCP;

end.
