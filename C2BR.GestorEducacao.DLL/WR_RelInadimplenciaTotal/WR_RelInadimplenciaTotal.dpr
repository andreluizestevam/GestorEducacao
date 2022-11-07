library WR_RelInadimplenciaTotal;

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
  U_FrmRelInadimplenciaTotal in 'Relatorios\U_FrmRelInadimplenciaTotal.pas' {FrmRelInadimplenciaTotal};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelInadimplenciaTotal(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelInadimplenciaTotal;
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

      SqlString := ' SET LANGUAGE PORTUGUESE '+
               '  SELECT A.CO_ALU, A.NO_ALU, '+
               '         A.NU_CPF_ALU, A.CO_RG_ALU, A.NU_TELE_RESI_ALU, '+
               '         C.CO_CUR, T.NO_CUR, '+
               '        VR_DEBITO = SUM(CASE C.IC_SIT_DOC WHEN '+ '''' +'A'+ '''' +' THEN C.VR_PAR_DOC END),' +
               '        SUM(C.VR_PAG) VR_PAG, '+
               ' SUM(CASE C.CO_FLAG_TP_VALOR_MUL when ' + quotedStr('V') + ' then C.VR_MUL_DOC when ' + quotedStr('P') + ' then (C.VR_PAR_DOC * C.VR_MUL_DOC)/100 END) VR_MUL_DOC, '+
               ' SUM(CASE C.CO_FLAG_TP_VALOR_JUR when ' + quotedStr('V') + ' then C.VR_JUR_DOC when ' + quotedStr('P') + ' then (C.VR_PAR_DOC * C.VR_JUR_DOC)/100 END) VR_JUR_DOC, '+
               '        SUM(C.VR_PAR_DOC) VR_PAR_DOC  '+
               '  FROM TB47_CTA_RECEB C ' +
               ' join TB07_ALUNO A on a.co_alu = c.co_alu and a.co_emp = c.co_emp ' +
               ' left join TB01_CURSO T on t.co_cur = c.co_cur and c.co_emp = t.co_emp and c.co_modu_cur = t.co_modu_cur  '+
               '  WHERE C.IC_SIT_DOC = '+ '''' + 'A' + '''' +
               '   AND  DT_VEN_DOC < GETDATE() ' +
               '   AND  C.CO_EMP = ' + strP_CO_EMP;

    if strP_CO_ANO_REF <> 'T' then
      SQLString := SQLString + ' AND  C.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF);

    if strP_CO_ALU <> 'T' then
      SqlString := SqlString + ' AND C.CO_ALU = ' + strP_CO_ALU;

    SqlString := SqlString +
                 '  GROUP BY A.CO_ALU, A.NO_ALU, A.NU_CPF_ALU, '+
                 '           A.CO_RG_ALU, A.NU_TELE_RESI_ALU, '+
                 '           C.CO_CUR, T.NO_CUR'+
                 '  ORDER BY A.NO_ALU, C.CO_CUR ';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelInadimplenciaTotal.Create(Nil);

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
        
        if strP_CO_ANO_REF <> 'T' then
          Relatorio.QrlAno.Caption := strP_CO_ANO_REF
        else
          Relatorio.QrlAno.Caption := 'Todos';

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
  DLLRelInadimplenciaTotal;

end.