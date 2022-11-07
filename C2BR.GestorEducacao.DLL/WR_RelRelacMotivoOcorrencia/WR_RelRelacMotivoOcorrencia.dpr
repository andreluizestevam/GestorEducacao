library WR_RelRelacMotivoOcorrencia;

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
  U_FrmRelRelacMotivoOcorrencia in 'Relatorios\U_FrmRelRelacMotivoOcorrencia.pas' {FrmRelRelacMotivoOcorrencia};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelRelacMotivoOcorrencia(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CLASSIF, strP_CO_TP_OCORR, strP_CO_SITU:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacMotivoOcorrencia;
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

      SqlString := 'set language portuguese select AE.*, TP.DE_TIPO_OCORR '+
                   'from TB243_MOTIVO_OCORRENCIA AE ' +
                   'join TB150_TIPO_OCORR TP on TP.CO_SIGL_OCORR = AE.CO_SIGL_OCORR ' +
                   ' where TP.TP_USU = ' + QuotedStr(strP_CLASSIF);

      if strP_CO_TP_OCORR <> 'T' then
        SqlString := SqlString + ' and AE.CO_SIGL_OCORR = ' + QuotedStr(strP_CO_TP_OCORR);

      if strP_CO_SITU <> 'T' then
        SqlString := SqlString + ' and AE.CO_SITUA_MOTIV_OCORR = ' + QuotedStr(strP_CO_SITU);

      SqlString := SqlString +
                   '  order by TP.DE_TIPO_OCORR, AE.NO_MOTIV_OCORR';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacMotivoOcorrencia.Create(Nil);

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
        if strP_CLASSIF = 'A' then
          Relatorio.QRLParam.Caption := '( Classificação: Aluno - Tipo de Ocorrência: '
        else if strP_CLASSIF = 'F' then
          Relatorio.QRLParam.Caption := '( Classificação: Funcionário - Tipo de Ocorrência: '
        else if strP_CLASSIF = 'R' then
          Relatorio.QRLParam.Caption := '( Classificação: Responsável - Tipo de Ocorrência: '
        else
          Relatorio.QRLParam.Caption := '( Classificação: Outros - Tipo de Ocorrência: ';

        if strP_CO_TP_OCORR = 'T' then
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Todas - Situação: '
        else
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + Relatorio.QryRelatorio.FieldByName('DE_TIPO_OCORR').AsString + ' - Situação: ';

        if strP_CO_SITU = 'T' then
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Todas )'
        else
        begin
          if strP_CO_SITU = 'A' then
            Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Ativa )'
          else
            Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Inativa )';
        end;

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
  DLLRelRelacMotivoOcorrencia;

end.
