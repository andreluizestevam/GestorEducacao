library WR_RelFicPerfilInst;

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
  U_FrmRelFicPerfilInst in 'Relatorios\U_FrmRelFicPerfilInst.pas' {FrmRelFicPerfilInst};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelFicPerfilInst(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelFicPerfilInst;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString: string;
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

      SqlString := ' SELECT TE.NO_TIPOEMP, CI.NO_CLAS, CID.NO_CIDADE, BAI.NO_BAIRRO, NUC.NO_SIGLA_NUCLEO, '+
                   ' (SELECT DE_HISTORICO FROM TB39_HISTORICO '+
                   '  WHERE FLA_TIPO_HISTORICO = ' + QuotedStr('C') +
                   '  AND CO_HISTORICO = E.CO_HIST_MAT) CTAR_HISTMAT, '+
                   ' (SELECT DE_HISTORICO FROM TB39_HISTORICO '+
                   '  WHERE FLA_TIPO_HISTORICO = ' + QuotedStr('C') +
                   '  AND CO_HISTORICO = E.CO_HIST_BIB) CTAR_HISTBIB, '+
                   ' (SELECT DE_HISTORICO FROM TB39_HISTORICO '+
                   '  WHERE FLA_TIPO_HISTORICO = ' + QuotedStr('C') +
                   '  AND CO_HISTORICO = E.CO_HIST_SOL) CTAR_HISTSOL, '+
                   ' (SELECT DE_HISTORICO FROM TB39_HISTORICO '+
                   '  WHERE FLA_TIPO_HISTORICO = ' + QuotedStr('C') +
                   '  AND CO_HISTORICO = E.CO_HIST_INSC) CTAR_HISTINSC, '+
                   ' FUNCIONAMENTO = (CASE E.TP_HORA_FUNC '+
                   '				WHEN ' + QuotedStr('M') + ' THEN ' + QuotedStr('Manhã') +
                   ' 				WHEN ' + QuotedStr('T') + ' THEN ' + QuotedStr('Tarde') +
                   '				WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Noite') +
                   '				WHEN ' + QuotedStr('MT') + ' THEN ' + QuotedStr('Manhã/Tarde') +
                   '				WHEN ' + QuotedStr('MN') + ' THEN ' + QuotedStr('Manhã/Noite') +
                   ' 				WHEN ' + QuotedStr('TN') + ' THEN ' + QuotedStr('Tarde/Noite') +
                   '				WHEN ' + QuotedStr('MTN') + ' THEN ' + QuotedStr('Manhã/Tarde/Noite') +
                   '				END), '+
                   ' E.*, img.ImageStream as fotoEmpresa '+
                   ' FROM TB25_EMPRESA E '+
                   ' left join Image img on img.ImageId = E.FOTO_IMAGE_ID ' +
                   ' 	JOIN TB24_TPEMPRESA TE ON TE.CO_TIPOEMP = E.CO_TIPOEMP '+
                   ' 	JOIN TB162_CLAS_INST CI ON CI.CO_CLAS = E.CO_CLAS '+
                   ' 	JOIN TB904_CIDADE CID ON CID.CO_CIDADE = E.CO_CIDADE '+
                   ' 	JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = E.CO_BAIRRO '+
                   '  LEFT JOIN TB_NUCLEO_INST NUC ON NUC.CO_NUCLEO = E.CO_NUCLEO '+
                   ' WHERE E.CO_EMP = ' + strP_CO_EMP;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelFicPerfilInst.Create(Nil);

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
        Relatorio.codigoEmpresa := strP_CO_EMP;

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
  DLLRelFicPerfilInst;

end.
