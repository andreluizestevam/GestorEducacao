library WR_RelObrasReservadas;

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
  U_FrmRelObrasReservadas in 'Relatorios\U_FrmRelObrasReservadas.pas' {FrmRelObrasReservadas};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelObrasReservadas(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_AREACON, strP_CO_CLAS, strP_CO_ISBN_ACER, strP_CO_TP_USU, strP_CO_USU, strP_DT_INI, strP_DT_FIM, strP_CNPJ_INSTI: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelObrasReservadas;
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
      SqlString := ' SET LANGUAGE PORTUGUESE            '+
               '   Select A.CO_AREACON,                 '+
               '        B.NO_AREACON,                   '+
               '        CL.NO_CLAS_ACER,                '+
               '        A.NO_ACERVO,                    '+
               '        D.NO_AUTOR,                     '+
               '        C.NO_EDITORA,                   '+
               '        E.DT_RESERVA,              '+
               '        F.DT_ENTREGA_ITEM, E.DT_LIMITE_NECESSI_RESERVA,     '+
               '        G.NO_COL, UB.NO_USU_BIB, G.CO_MAT_COL,EM.NO_FANTAS_EMP,     '+
               'tpUsuario =(CASE UB.TP_USU_BIB ' +
               'WHEN ' + quotedStr('A') + ' THEN '+  quotedStr('Aluno') +
               ' WHEN ' +  quotedStr('P') + ' THEN' + quotedStr('Professor') +
               ' WHEN ' + quotedStr('F') + 'THEN ' + quotedStr('Funcionário') +
               ' WHEN ' + quotedStr('O') + 'THEN ' + quotedStr('Outros') +
		           ' END), ' +
               'SITU =(CASE F.CO_SITU_ITEM_RESERVA ' +
               'WHEN ' + quotedStr('A') + ' THEN '+  quotedStr('Ativo') +
               ' WHEN ' +  quotedStr('I') + ' THEN' + quotedStr('Inativo') +
		           ' END), AI.CO_CTRL_INTERNO ' +
               ' From TB35_ACERVO A,                    '+
               '      TB31_AREA_CONHEC B,               '+
               '      TB32_CLASSIF_ACER CL,             '+
               '      TB33_EDITORA C,                   '+
               '      TB34_AUTOR D,                     '+
               '      TB206_RESERVA_BIBLIOTECA  E,      '+
               '      TB207_RESERVA_ITENS_BIBLIOTECA F, '+
               '      TB03_COLABOR G,                   '+
               '      TB205_USUARIO_BIBLIOT UB,         '+
               '      TB204_ACERVO_ITENS AI, TB25_EMPRESA EM '+
               ' Where A.CO_AREACON = B.CO_AREACON      '+
               ' AND   A.CO_EDITORA = C.CO_EDITORA      '+
               ' AND   A.CO_AUTOR = D.CO_AUTOR          '+
               ' AND   A.CO_ISBN_ACER = F.CO_ISBN_ACER  '+
               ' AND   A.CO_CLAS_ACER = CL.CO_CLAS_ACER AND A.CO_AREACON = CL.CO_AREACON ' +
               ' AND   A.ORG_CODIGO_ORGAO = F.ORG_CODIGO_ORGAO ' +
               ' AND   F.CO_RESERVA_BIBLIOTECA = E.CO_RESERVA_BIBLIOTECA  '+
               ' AND   E.CO_COL_RESP = G.CO_COL AND E.CO_EMP_RESP = G.CO_EMP   '+
               ' AND  EM.CO_EMP = F.CO_EMP ' +
               ' AND   E.ORG_CODIGO_ORGAO = UB.ORG_CODIGO_ORGAO and E.CO_USUARIO_BIBLIOT = UB.CO_USUARIO_BIBLIOT ' +
               ' AND F.ORG_CODIGO_ORGAO = AI.ORG_CODIGO_ORGAO and F.CO_ISBN_ACER = AI.CO_ISBN_ACER and F.CO_ACERVO_AQUISI = AI.CO_ACERVO_AQUISI ' +
               ' AND F.CO_ACERVO_ITENS = AI.CO_ACERVO_ITENS ' +
               ' AND   E.DT_RESERVA BETWEEN ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM + ' 23:59:59') +
               ' AND F.CO_SITU_ITEM_RESERVA = ' + QuotedStr('A') +
               ' AND F.CO_EMP = ' + strP_CO_EMP;


      if strP_CO_AREACON <> 'T' then
        SqlString := SqlString + ' AND A.CO_AREACON = ' + strP_CO_AREACON;

      if strP_CO_CLAS <> 'T' then
        SqlString := SqlString + ' AND A.CO_CLAS_ACER = ' + strP_CO_CLAS;

      if strP_CO_ISBN_ACER <> 'T' then
        SqlString := SqlString + ' AND A.CO_CLAS_ACER = ' + strP_CO_ISBN_ACER;

      if strP_CO_USU <> 'T' then
        SqlString := SqlString + ' AND E.CO_USUARIO_BIBLIOT = ' + strP_CO_USU;

      if strP_CO_TP_USU <> 'T' then
        SqlString := SqlString + ' AND E.TP_USU_BIB = ' + QuotedStr(strP_CO_TP_USU);

      SqlString := SqlString +
               '  ORDER BY E.DT_RESERVA, '+
               '           B.NO_AREACON,   '+
               '           A.NO_ACERVO     ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelObrasReservadas.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      DM.Conn.ConnectionString := retornaConexao(strP_CNPJ_INSTI);
      DM.Conn.Open;
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

        Relatorio.strP_CO_AREACON := strP_CO_AREACON;
        Relatorio.strP_CO_CLAS := strP_CO_CLAS;
        Relatorio.strP_DT_INI := strP_DT_INI;
        Relatorio.strP_DT_FIM := strP_DT_FIM;

        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
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
  DLLRelObrasReservadas;

end.
