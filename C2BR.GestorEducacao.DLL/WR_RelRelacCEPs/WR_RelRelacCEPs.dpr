library WR_RelRelacCEPs;

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
  U_Funcoes in '..\General\U_Funcoes.pas',
  U_FrmRelRelacCEPs in 'Relatorios\U_FrmRelRelacCEPs.pas' {FrmRelRelacCEPs};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelRelacCEPs(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strP_CO_IMPRESSAO:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacCEPs;
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

      SQLString := ' select distinct a.*,b.no_bairro,b.co_bairro,ci.CO_UF,ci.no_cidade, tl.CO_TIPO_LOGRA'+
                   '  from TB235_CEP a ' +
                   ' join TB240_TIPO_LOGRADOURO tl on tl.ID_TIPO_LOGRA = a.ID_TIPO_LOGRA ' +
                   '  left join tb905_bairro b on b.co_bairro = a.CO_BAIRR_CEP and b.co_cidade = a.CO_CIDAD_CEP ' +
                   '  left join tb904_cidade ci on ci.co_cidade = a.CO_CIDAD_CEP ' +
                   ' where 1 = 1 ';

      if strP_CO_UF <> 'T' then
      begin
        SQLString := SQLString + ' and ci.CO_UF = ' + quotedStr(strP_CO_UF);
      end;

      if strP_CO_CIDADE <> 'T' then
      begin
        SQLString := SQLString + ' and a.CO_CIDAD_CEP = ' + strP_CO_CIDADE;
      end;

      if strP_CO_BAIRRO <> 'T' then
      begin
        SQLString := SQLString + ' and a.CO_BAIRR_CEP = ' + strP_CO_BAIRRO;
      end;

      if strP_CO_IMPRESSAO = 'B' then
        SQLString := SQLString + ' order by b.no_bairro'
      else if strP_CO_IMPRESSAO = 'C' then
        SQLString := SQLString + ' order by a.co_cep'
      else
        SQLString := SQLString + ' order by a.no_ender_cep';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacCEPs.Create(Nil);

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
        
        if strP_CO_IMPRESSAO = 'B' then
          Relatorio.QRLParam.Caption := '( UF: ' + Relatorio.QryRelatorio.FieldByName('co_uf').AsString +' - Cidade: ' + Relatorio.QryRelatorio.FieldByName('no_cidade').AsString + ' - Classificação Relatório: Bairro )'
        else if strP_CO_IMPRESSAO = 'C' then
          Relatorio.QRLParam.Caption := '( UF: ' + Relatorio.QryRelatorio.FieldByName('co_uf').AsString +' - Cidade: ' + Relatorio.QryRelatorio.FieldByName('no_cidade').AsString + ' - Classificação Relatório: Nº CEP )'
        else
          Relatorio.QRLParam.Caption := '( UF: ' + Relatorio.QryRelatorio.FieldByName('co_uf').AsString +' - Cidade: ' + Relatorio.QryRelatorio.FieldByName('no_cidade').AsString + ' - Classificação Relatório: Endereço )';

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
  DLLRelRelacCEPs;

end.
