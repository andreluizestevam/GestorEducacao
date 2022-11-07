library WR_RelItensAcervo;

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
  U_FrmRelItensAcervo in 'Relatorios\U_FrmRelItensAcervo.pas' {FrmRelItensAcervo};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelItensAcervo(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_AREACON, strP_DE_AREACON, strP_CO_EDITORA, strP_DE_EDITORA, strP_CO_CLAS_ACER, strP_DE_CLAS_ACER, strP_CO_AUTOR, strP_DE_AUTOR, strP_CNPJ_INSTI: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelItensAcervo;
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
               '  Select A.CO_AREACON,                  '+
               '         A.CO_CLAS_ACER,                '+
               '         B.NO_AREACON,                  '+
               '         A.NO_ACERVO,                   '+
               '         A.CO_ISBN_ACER,                '+
               '         E.NO_AUTOR,                    '+
               '         C.NO_CLAS_ACER,                '+
               '         D.NO_EDITORA,                  '+
               '         AI.CO_CTRL_INTERNO,AI.DE_LOCAL,             '+
               'situ =(CASE A.CO_SITU ' +
               'WHEN ' + quotedStr('A') + ' THEN '+  quotedStr('Ativo') +
               ' WHEN ' +  quotedStr('I') + ' THEN' + quotedStr('Inativo') +
               ' END),' +
               'estado =(CASE AI.CO_ESTADO_ACERVO_ITENS ' +
               'WHEN ' + quotedStr('O') + ' THEN '+  quotedStr('Ótimo') +
               ' WHEN ' +  quotedStr('B') + ' THEN' + quotedStr('Bom') +
               ' WHEN ' + quotedStr('R') + 'THEN ' + quotedStr('Ruim') +
               ' END), EM.sigla' +
               '  From TB35_ACERVO A,                   '+
               '       TB31_AREA_CONHEC B,              '+
               '       TB32_CLASSIF_ACER C,             '+
               '       TB33_EDITORA D,                  '+
               '       TB34_AUTOR E,                    '+
               '       TB204_ACERVO_ITENS AI,            '+
               '       TB25_EMPRESA EM '+
               '  Where A.CO_AREACON = B.CO_AREACON     '+
               '  AND   A.CO_CLAS_ACER = C.CO_CLAS_ACER '+
               '  AND   A.ORG_CODIGO_ORGAO = AI.ORG_CODIGO_ORGAO '+
               '  AND   A.CO_ISBN_ACER = AI.CO_ISBN_ACER'+
               '  AND AI.CO_EMP = EM.CO_EMP ' +
               '  AND   A.CO_EDITORA = D.CO_EDITORA     '+
               '  AND   A.CO_AUTOR = E.CO_AUTOR         '+
               '  AND C.CO_AREACON = A.CO_AREACON '+
               '  AND   A.CO_SITU = '+ '''' +'A'+ ''''   ;

      if strP_CO_AREACON <> 'T' then
        SqlString := SqlString + ' AND A.CO_AREACON = ' + strP_CO_AREACON;

      if strP_CO_EDITORA <> 'T' then
        SqlString := SqlString + ' AND A.CO_EDITORA = ' + strP_CO_EDITORA;

      if strP_CO_CLAS_ACER <> 'T' then
        SqlString := SqlString + ' AND A.CO_CLAS_ACER = ' + strP_CO_CLAS_ACER;

      if strP_CO_AUTOR <> 'T' then
        SqlString := SqlString + ' AND A.CO_AUTOR = ' + strP_CO_AUTOR;


      SqlString := SqlString +
                   '  ORDER BY B.NO_AREACON,C.NO_CLAS_ACER,  '+
                   '           A.NO_ACERVO    ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelItensAcervo.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.FOTO_IMAGE_ID,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;
        
        Relatorio.QrlParametros.Caption := '( Área Interesse: ' + strP_DE_AREACON + ' - Classificação: ' + strP_DE_CLAS_ACER +
        ' - Editora: ' + strP_DE_EDITORA + ' - Autor: ' + strP_DE_AUTOR + ' )';

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
  DLLRelItensAcervo;

end.
