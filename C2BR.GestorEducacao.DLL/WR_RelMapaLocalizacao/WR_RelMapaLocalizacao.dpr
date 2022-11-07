library WR_RelMapaLocalizacao;

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
  U_FrmRelMapaLocalizacao in 'Relatorios\U_FrmRelMapaLocalizacao.pas' {FrmRelMapaLocalizacao};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaLocalizacao(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_DE_EMP:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaLocalizacao;
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
               ' SELECT esp.tp_espec,c.co_curform, c.co_espec, E.SIGLA, C.CO_COL, C.CO_MAT_COL, C.NO_COL, ci.NO_CIDADE,b.no_bairro, ' +
               ' C.CO_ESTA_ENDE_COL, C.NU_TELE_RESI_COL, C.NU_TELE_CELU_COL, ' +
               ' C.CO_INST, C.NU_CARGA_HORARIA, TC.NO_TPCON, ESP.DE_ESPEC AS CURFORM, ' +
               ' SITUACAO = (CASE C.CO_SITU_COL ' +
               '      WHEN ' + QuotedStr('ATI') + ' THEN ' + QuotedStr('Atividade Interna') +
               '      WHEN ' + QuotedStr('ATE') + ' THEN ' + QuotedStr('Atividade Externa') +
               '      WHEN ' + QuotedStr('FCE') + ' THEN ' + QuotedStr('Cedido') +
               '      WHEN ' + QuotedStr('FES') + ' THEN ' + QuotedStr('Estagiário') +
               '      WHEN ' + QuotedStr('LFR') + ' THEN ' + QuotedStr('Licença Funcional') +
               '      WHEN ' + QuotedStr('LME') + ' THEN ' + QuotedStr('Licença Médica') +
               '      WHEN ' + QuotedStr('LMA') + ' THEN ' + QuotedStr('Licença Maternidade') +
               '      WHEN ' + QuotedStr('SUS') + ' THEN ' + QuotedStr('Suspenso') +
               '      WHEN ' + QuotedStr('TRE') + ' THEN ' + QuotedStr('Treinamento') +
               '      WHEN ' + QuotedStr('FER') + ' THEN ' + QuotedStr('Férias') +
               '          	END), ' +
               ' GR.NO_INST as ESPECIALIZAÇÃO ' +
               {' ESPECIALIZAÇÃO = (CASE ESP.TP_ESPEC ' +
               '  WHEN ' + QuotedStr('MB') + ' THEN ' + QuotedStr('MBA') +
               '  WHEN ' + QuotedStr('PG') + ' THEN ' + QuotedStr('Pós-Graduação') +
               '  WHEN ' + QuotedStr('PD') + ' THEN ' + QuotedStr('Pós-Doutorado') +
               '  WHEN ' + QuotedStr('SU') + ' THEN ' + QuotedStr('Superior') +
               '  WHEN ' + QuotedStr('ES') + ' THEN ' + QuotedStr('Especialização') +
               '  WHEN ' + QuotedStr('ME') + ' THEN ' + QuotedStr('Mestrado') +
               '  WHEN ' + QuotedStr('DO') + ' THEN ' + QuotedStr('Doutorado') +
               '  WHEN ' + QuotedStr('TE') + ' THEN ' + QuotedStr('Técnico') +
               '  WHEN ' + QuotedStr('OU') + ' THEN ' + QuotedStr('Outro') +
               '  END) ' +      }
               ' FROM TB03_COLABOR C ' +
               '      LEFT JOIN TB18_GRAUINS GR on GR.CO_INST = C.CO_INST ' +
               '    	LEFT JOIN TB20_TIPOCON TC ON TC.CO_TPCON = C.CO_TPCON ' +
               '      LEFT JOIN TB25_EMPRESA E ON E.CO_EMP = C.CO_EMP ' +
               '      LEFT join tb904_cidade ci on c.co_cidade = ci.co_cidade '+
               '      LEFT join tb905_bairro b on c.co_bairro = b.co_bairro ' +
               '      LEFT JOIN TB100_ESPECIALIZACAO ESP ON ESP.CO_ESPEC = C.CO_ESPEC ' +
               ' WHERE C.FLA_PROFESSOR = ' + QuotedStr('S');

      if strP_CO_EMP <> nil then
        SqlString := SqlString + ' AND C.CO_EMP = ' + strP_CO_EMP;

      SqlString := SqlString + ' ORDER BY C.NO_COL,C.CO_EMP';
      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaLocalizacao.Create(Nil);

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

        Relatorio.QRLDescUnidade.Caption := strP_DE_EMP;
        Relatorio.codigoEmpresa := strP_CO_EMP;

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
  DLLRelMapaLocalizacao;

end.
