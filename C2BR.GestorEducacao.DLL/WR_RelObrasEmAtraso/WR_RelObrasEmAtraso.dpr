library WR_RelObrasEmAtraso;

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
  U_FrmRelObrasEmAtraso in 'Relatorios\U_FrmRelObrasEmAtraso.pas' {FrmRelObrasEmAtraso},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelObrasEmAtraso(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_AREACON, strP_DE_ARECON, strP_DT_INI, strP_DT_FIM, strP_CNPJ_INSTI: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelObrasEmAtraso;
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
      SqlString := ' SET LANGUAGE PORTUGUESE                '+
               '   Select A.CO_AREACON,                 '+
               '        B.NO_AREACON,                   '+
               '        A.NO_ACERVO,                    '+
               '        D.NO_AUTOR,                     '+
               '        C.NO_EDITORA,                   '+
               '        E.*,              '+
               '        F.DT_PREV_DEVO_ACER,            '+
               '        F.DT_REAL_DEVO_ACER,            '+
               '        F.VL_MULT_ATRA_ACER,            '+
               '        G.NO_COL, UB.TP_USU_BIB,UB.CO_USUARIO_BIBLIOT,UB.NO_USU_BIB,'+
               'tpUsuario =(CASE UB.TP_USU_BIB ' +
               'WHEN ' + quotedStr('A') + ' THEN '+  quotedStr('Aluno') +
               ' WHEN ' +  quotedStr('P') + ' THEN' + quotedStr('Professor') +
               ' WHEN ' + quotedStr('F') + 'THEN ' + quotedStr('Funcionário') +
               ' WHEN ' + quotedStr('O') + 'THEN ' + quotedStr('Outros') +
		           ' END), AI.CO_CTRL_INTERNO ' +
               ' From TB35_ACERVO A,                    '+
               '      TB31_AREA_CONHEC B,               '+
               '      TB33_EDITORA C,                   '+
               '      TB34_AUTOR D,                     '+
               '      TB36_EMPR_BIBLIOT  E,             '+
               '      TB123_EMPR_BIB_ITENS F,           '+
               '      TB03_COLABOR G,                   '+
               '      TB205_USUARIO_BIBLIOT UB,         '+
               '      TB204_ACERVO_ITENS AI             '+
               ' Where A.CO_AREACON = B.CO_AREACON      '+
               ' AND   A.CO_EDITORA = C.CO_EDITORA      '+
               ' AND   A.CO_AUTOR = D.CO_AUTOR          '+
               ' AND   A.CO_ISBN_ACER = F.CO_ISBN_ACER        '+
               ' AND   A.ORG_CODIGO_ORGAO = F.ORG_CODIGO_ORGAO ' +
               ' AND   F.CO_NUM_EMP = E.CO_NUM_EMP  '+
               ' AND   E.CO_COL_EMPR_ACER = G.CO_COL AND E.CO_EMP_COL_EMPR_ACER = G.CO_EMP   '+
               ' AND   E.ORG_CODIGO_ORGAO_USU = UB.ORG_CODIGO_ORGAO and E.CO_USUARIO_BIBLIOT = UB.CO_USUARIO_BIBLIOT ' +
               ' AND F.ORG_CODIGO_ORGAO = AI.ORG_CODIGO_ORGAO and F.CO_ISBN_ACER = AI.CO_ISBN_ACER and F.CO_ACERVO_AQUISI = AI.CO_ACERVO_AQUISI ' +
               ' and F.CO_ACERVO_ITENS = AI.CO_ACERVO_ITENS ' +
               ' AND   F.DT_REAL_DEVO_ACER IS NULL      '+
               ' AND   F.DT_PREV_DEVO_ACER < GetDate() '+
               ' AND   F.DT_PREV_DEVO_ACER BETWEEN ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM) +
               ' AND E.CO_EMP = ' + strP_CO_EMP;

      if strP_CO_AREACON <> 'T' then
        SqlString := SqlString + ' AND A.CO_AREACON = ' + strP_CO_AREACON;

      SqlString := SqlString +
                   '  ORDER BY A.NO_ACERVO,    '+
                   '           B.NO_AREACON,   '+
                   '           E.DT_EMPR_BIBLIOT  ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelObrasEmAtraso.Create(Nil);

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

        Relatorio.QrlParametros.Caption := '( Área Conhecimento: ' + strP_DE_ARECON + ' - Período: ' + strP_DT_INI + ' à ' + strP_DT_FIM + ' )';

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
  DLLRelObrasEmAtraso;

end.
