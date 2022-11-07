library WR_RelPosicaoEstoque;

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
  U_FrmRelPosicaoEstoque in 'Relatorios\U_FrmRelPosicaoEstoque.pas' {FrmRelPosicaoEstoque};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelPosicaoEstoque(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_TIP_PROD, strP_DE_TIP_PROD, strP_CO_GRUPO_ITEM, strP_CO_SUBGRP_ITEM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelPosicaoEstoque;
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
      SqlString := ' SET LANGUAGE PORTUGUESE                    '+
               ' SELECT P.CO_GRUPO_ITEM,                    '+
               '        G.NO_GRUPO_ITEM,                    '+
               '        P.CO_SUBGRP_ITEM,                   '+
               '        S.NO_SUBGRP_ITEM,                   '+
               '        P.CO_PROD,                          '+
               '        P.CO_REFE_PROD,                     '+
               '        P.DES_PROD, P.NO_PROD,              '+
               '        U.SG_UNIDADE,                       '+
               '        M.DES_MARCA,                        '+
               '        T.DES_TAMANHO, T.NO_SIGLA,          '+
               '        C.DES_COR,                          '+
               '        E.QT_SALDO_EST,                     '+
               '        E.QT_RES_EST,                       '+
               '        E.QT_EST_MIN,                       '+
               '        E.QT_EST_SEG,                       '+
               '        E.QT_EST_MAX                        '+
               ' FROM TB90_PRODUTO P                       '+
               '      JOIN TB87_GRUPO_ITENS G on P.CO_GRUPO_ITEM = G.CO_GRUPO_ITEM'+
               '      JOIN TB88_SUBGRUPO_ITENS S on P.CO_GRUPO_ITEM = S.CO_GRUPO_ITEM AND P.CO_SUBGRP_ITEM = S.CO_SUBGRP_ITEM '+
               '      LEFT JOIN TB89_UNIDADES U on P.CO_UNID_ITEM = U.CO_UNID_ITEM'+
               '      LEFT JOIN TB93_MARCA M on P.CO_MARCA = M.CO_MARCA'+
               '      LEFT JOIN TB98_TAMANHO T on P.CO_TAMANHO = T.CO_TAMANHO'+
               '      LEFT JOIN TB97_COR C on P.CO_COR = C.CO_COR'+
               '      JOIN TB96_ESTOQUE E on P.CO_PROD = E.CO_PROD'+
               ' WHERE E.CO_EMP = ' + strP_CO_EMP +
               ' AND   P.CO_TIP_PROD   = ' + strP_CO_TIP_PROD;

      if strP_CO_GRUPO_ITEM <> 'T' then
        SqlString := SqlString + ' AND P.CO_GRUPO_ITEM = ' + strP_CO_GRUPO_ITEM;

      if strP_CO_SUBGRP_ITEM <> 'T' then
        SqlString := SqlString + ' AND P.CO_SUBGRP_ITEM = ' + strP_CO_SUBGRP_ITEM;

      SqlString := SqlString +
                   '  ORDER BY P.CO_GRUPO_ITEM,                 '+
                   '           P.CO_SUBGRP_ITEM,                '+
                   '           P.DES_PROD                       ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelPosicaoEstoque.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;
        
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.QrlTipoProduto.Caption := strP_DE_TIP_PROD;

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
  DLLRelPosicaoEstoque;

end.
