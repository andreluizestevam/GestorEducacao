library WR_RelAlunosBolsistas;

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
  U_FrmRelAlunosBolsistas in 'Relatorios\U_FrmRelAlunosBolsistas.pas' {FrmRelAlunosBolsistas};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelAlunosBolsistas(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_TIPO_BOLSA:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAlunosBolsistas;
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
               'SELECT Distinct  A.CO_ALU,MM.CO_ALU_CAD,A.NO_ALU,A.FLA_BOLSISTA,A.DT_VENC_BOLSA,A.DT_VENC_BOLSAF,'+
               ' A.NU_PEC_DESBOL,A.DE_TIPO_BOLSA,TU.NO_TURMA [NO_TUR],T.CO_TUR,C.CO_CUR,'+
               ' C.NO_CUR,MM.VL_TOT_MODU_MAT,MM.VL_DES_MOD_MAT,MM.VL_PAR_MOD_MAT,'+
               ' M.DE_MODU_CUR,R.NU_CPF_RESP,MM.CO_ANO_MES_MAT'+
               ' FROM TB08_MATRCUR MM'+
               ' JOIN TB07_ALUNO A ON MM.CO_ALU = A.CO_ALU AND MM.CO_EMP = A.CO_EMP'+
               ' JOIN TB06_TURMAS T ON MM.CO_TUR = T.CO_TUR AND MM.CO_EMP = T.CO_EMP and T.CO_CUR = MM.CO_CUR '+
               ' JOIN TB129_CADTURMAS TU ON MM.CO_TUR = TU.CO_TUR AND MM.CO_EMP = T.CO_EMP'+
               ' JOIN TB01_CURSO C ON MM.CO_CUR = C.CO_CUR AND MM.CO_EMP = C.CO_EMP'+
               //' JOIN TB108_RESPONSAVEL R ON A.CO_RESP = R.CO_RESP AND A.CO_EMP = R.CO_EMP'+
               ' JOIN TB108_RESPONSAVEL R ON A.CO_RESP = R.CO_RESP '+
               ' JOIN TB44_MODULO M ON MM.CO_MODU_CUR = M.CO_MODU_CUR  ' +
               ' WHERE A.FLA_BOLSISTA = '+ QuotedStr('S') +
               '  AND  A.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_MODU_CUR <> 'T' then
        SqlString := SqlString + ' AND MM.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' AND MM.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_ANO_MES_MAT <> 'T' then
        SqlString := SqlString + ' AND MM.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_MES_MAT);

      if strP_CO_TUR <> 'T' then
        SqlString := SqlString + ' AND MM.CO_TUR = ' + strP_CO_TUR;

      if strP_CO_TIPO_BOLSA <> 'T' then
        SqlString := SqlString + ' AND A.CO_TIPO_BOLSA = ' + strP_CO_TIPO_BOLSA;


      SqlString := SqlString +
                   ' ORDER BY C.CO_CUR, T.CO_TUR, A.NO_ALU ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelAlunosBolsistas.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SQLString;
      //ShowMessage(SQLString);
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
  DLLRelAlunosBolsistas;

end.
