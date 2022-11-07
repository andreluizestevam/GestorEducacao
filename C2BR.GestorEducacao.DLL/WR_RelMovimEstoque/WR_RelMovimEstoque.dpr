library WR_RelMovimEstoque;

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
  U_FrmRelMovimEstoque in 'Relatorios\U_FrmRelMovimEstoque.pas' {FrmRelMovimEstoque};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelMovimEstoque(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_TP_PROD, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMovimEstoque;
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
               ' SELECT MV.*, P.CO_GRUPO_ITEM,              '+
               '        G.NO_GRUPO_ITEM,                    '+
               '        P.CO_SUBGRP_ITEM,                   '+
               '        P.CO_PROD,                          '+
               '        P.CO_REFE_PROD,                     '+
               '        P.DES_PROD, P.NO_PROD_RED,          '+
               ' E.QT_SALDO_EST, TM.DE_TIPO_MOV,TM.FLA_TP_MOV '+
               ' FROM TB91_MOV_PRODUTO MV ' +
               ' JOIN TB90_PRODUTO P on P.CO_PROD = MV.CO_PROD '+
               ' JOIN TB96_ESTOQUE E on E.CO_PROD = MV.CO_PROD ' +
               ' JOIN TB93_TIPO_MOVIMENTO TM on TM.CO_TIPO_MOV = MV.CO_TIPO_MOV ' +
               ' join TB87_GRUPO_ITENS G on P.CO_GRUPO_ITEM = G.CO_GRUPO_ITEM '+
               ' WHERE MV.CO_EMP   = ' + strP_CO_EMP +
               ' AND MV.DT_MOV_PROD between ' + quotedStr(strP_DT_INI) + ' and ' + quotedStr(strP_DT_FIM);

      if strP_CO_TP_PROD <> 'T' then
        SqlString := SqlString + ' AND P.CO_TIP_PROD = ' + strP_CO_TP_PROD;

      SqlString := SqlString +
                   ' ORDER BY P.CO_GRUPO_ITEM,P.DES_PROD';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelMovimEstoque.Create(Nil);

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
  DLLRelMovimEstoque;

end.
