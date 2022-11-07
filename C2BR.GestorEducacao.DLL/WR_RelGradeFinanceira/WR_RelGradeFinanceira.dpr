library WR_RelGradeFinanceira;

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
  U_FrmRelGradeFinanceira in 'Relatorios\U_FrmRelGradeFinanceira.pas' {FrmRelGradeFinanceira};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelGradeFinanceira(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ALU:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelGradeFinanceira;
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

      SqlString := ' SET LANGUAGE PORTUGUESE                                                  ' +
               '  SELECT C.CO_ALU, C.CO_EMP, A.NO_ALU,C.CO_ANO_MES_MAT,  '+
               '         VR_MES_PAR_01 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 1 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_02 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 2 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_03 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 3 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_04 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 4 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_05 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 5 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_06 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 6 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_07 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 7 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_08 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 8 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_09 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 9 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END), '+
               '         VR_MES_PAR_10 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 10 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END),'+
               '         VR_MES_PAR_11 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 11 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END),'+
               '         VR_MES_PAR_12 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 12 THEN C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO END),'+
               '         IC_SIT_DOC_01 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 1 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_02 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 2 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_03 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 3 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_04 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 4 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_05 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 5 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_06 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 6 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_07 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 7 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_08 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 8 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_09 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 9 THEN C.IC_SIT_DOC END), '+
               '         IC_SIT_DOC_10 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 10 THEN C.IC_SIT_DOC END),'+
               '         IC_SIT_DOC_11 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 11 THEN C.IC_SIT_DOC END),'+
               '         IC_SIT_DOC_12 = MAX(CASE MONTH(C.DT_VEN_DOC) WHEN 12 THEN C.IC_SIT_DOC END),'+
               '         SUM(C.VR_PAG) VR_TOT_PAG,                                        '+
               '         SUM(C.VR_PAR_DOC - C.VL_DES_BOLSA_ALUNO) VR_TOT_PAG_DOC                                 '+
               ' FROM TB47_CTA_RECEB C ' +
               'join TB07_ALUNO A on C.CO_EMP = A.CO_EMP and C.co_alu = a.co_alu ' +
               '  WHERE C.IC_SIT_DOC <> '+ '''' +'C'+ ''''+
               '   AND  C.CO_EMP = ' + strP_CO_EMP;

  if strP_CO_ANO_REF <> 'T' then
    SqlString := SqlString + ' AND C.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF);

  if strP_CO_MODU_CUR <> 'T' then
    SqlString := SqlString + ' AND C.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

  if strP_CO_CUR <> 'T' then
    SqlString := SqlString + ' AND C.CO_CUR = ' + strP_CO_CUR;

  if strP_CO_TUR <> 'T' then
    SqlString := SqlString + ' AND C.CO_TUR = ' + strP_CO_TUR;

  if strP_CO_ALU <> 'T' then
    SqlString := SqlString + ' AND C.CO_ALU = ' + strP_CO_ALU;

  SqlString := SqlString +
               ' GROUP BY C.CO_ALU, C.CO_EMP, A.NO_ALU, C.CO_ANO_MES_MAT ORDER BY A.NO_ALU';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelGradeFinanceira.Create(Nil);

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

        Relatorio.QRLParametros.Caption := strParamRel;

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
  DLLRelGradeFinanceira;

end.
