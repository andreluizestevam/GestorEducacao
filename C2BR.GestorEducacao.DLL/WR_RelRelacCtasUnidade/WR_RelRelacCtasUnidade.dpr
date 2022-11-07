library WR_RelRelacCtasUnidade;

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
  U_FrmRelRelacCtasUnidade in 'Relatorios\U_FrmRelRelacCtasUnidade.pas' {FrmRelRelacCtasUnidade};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelRelacCtasUnidade(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_ORG_CODIGO_ORGAO, strP_CO_EMP_CTA:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacCtasUnidade;
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

      SqlString := 'select CU.*, E.NO_FANTAS_EMP, B.DESBANCO, A.DI_AGENCIA, CC.CO_DIG_CONTA, CC.NO_GER_CTA, CC.NU_TEL_GER_CTA, CC.DT_ABERT_CTA, CC.CO_STATUS, CC.FLAG_EMITE_BOLETO_BANC '+
                   'from TB225_CONTAS_UNIDADE CU ' +
                   'join TB25_EMPRESA E on E.CO_EMP = CU.CO_EMP ' +
                   'join TB29_BANCO B on B.IDEBANCO = CU.IDEBANCO ' +
                   'join TB30_AGENCIA A on A.IDEBANCO = CU.IDEBANCO and A.CO_AGENCIA = CU.CO_AGENCIA ' +
                   'join TB224_CONTA_CORRENTE CC on CC.IDEBANCO = CU.IDEBANCO and CC.CO_AGENCIA = CU.CO_AGENCIA and CC.CO_CONTA = CU.CO_CONTA ' +
                   ' where CC.ORG_CODIGO_ORGAO = ' + strP_ORG_CODIGO_ORGAO;

      if strP_CO_EMP_CTA <> 'T' then
        SqlString := SqlString + ' and CU.CO_EMP = ' + strP_CO_EMP;

      SqlString := SqlString +
                   '  order by E.NO_FANTAS_EMP, B.DESBANCO  ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacCtasUnidade.Create(Nil);

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
        //Relatorio.QRLParametros.Caption := strParamRel;

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
  DLLRelRelacCtasUnidade;

end.
