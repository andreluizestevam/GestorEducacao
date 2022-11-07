library WR_RelFicCadFuncionario;

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
  U_FrmRelFicCadFuncionario in 'Relatorios\U_FrmRelFicCadFuncionario.pas' {FrmRelFicCadFuncionario};

// Controle Administrativo > Controle de Funcion�rios
// Relat�rio: EMISS�O DA FICHA DE INFORMA��ES DE FUNCION�RIO
// STATUS: OK
function DLLRelFicCadFuncionario(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP , strP_CO_COL, strP_CO_ANO_BASE, strP_FLA_PROFESSOR, strP_TP_PONTO : PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelFicCadFuncionario;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath : string;
  SqlString : string;
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

      // Monta a Consulta do Relat�rio.
      SqlString := ' Select I.NO_INST, ' +
               '        T.NO_TPCON, ' +
               '        D.NO_DEPTO, ' +
               '        ES.NO_ESPECIALIDADE as DE_ESPEC, c.*, ' +
               '        F.NO_FUN, ' +
               '        E.NO_RAZSOC_EMP, ' +
               '        E.DE_END_EMP, ' +
               '        BAI.NO_BAIRRO, ' + // Diego Nobre - 11/05/2009
               '        E.CO_CEP_EMP, ' +
               '        E.CO_UF_EMP, ' +
               '        CID.NO_CIDADE, ' + // Diego Nobre - 11/05/2009
               '        E.CO_TEL1_EMP, ' +
               ' CATEGORIA = (CASE C.FLA_PROFESSOR '+
               ' 	WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Professor') +
               '	WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Funcion�rio') +
               '			END), '+
               ' DEFICIENCIA = (CASE C.TP_DEF '+
               '	WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Nenhuma') +
               '	WHEN ' + QuotedStr('A') + ' THEN ' + QuotedStr('Auditiva') +
               ' 	WHEN ' + QuotedStr('V') + ' THEN ' + QuotedStr('Visual') +
               '	WHEN ' + QuotedStr('F') + ' THEN ' + QuotedStr('F�sica') +
               '	WHEN ' + QuotedStr('M') + ' THEN ' + QuotedStr('Mental') +
               '	WHEN ' + QuotedStr('I') + ' THEN ' + QuotedStr('M�ltiplas') +
               '	WHEN ' + QuotedStr('O') + ' THEN ' + QuotedStr('Outras') +
               '				END), '+
               ' SITUACAO = (CASE C.CO_SITU_COL '+
               '	WHEN ' + QuotedStr('ATI') + ' THEN ' + QuotedStr('Atividade Interna') +
               '	WHEN ' + QuotedStr('ATE') + ' THEN ' + QuotedStr('Atividade Externa') +
               ' 	WHEN ' + QuotedStr('FCE') + ' THEN ' + QuotedStr('Cedido') +
               '	WHEN ' + QuotedStr('FES') + ' THEN ' + QuotedStr('Estagi�rio') +
               '	WHEN ' + QuotedStr('LFR') + ' THEN ' + QuotedStr('Licen�a Funcional') +
               '	WHEN ' + QuotedStr('LME') + ' THEN ' + QuotedStr('Licen�a M�dica') +
               '	WHEN ' + QuotedStr('LMA') + ' THEN ' + QuotedStr('Licen�a Maternidade') +
               '	WHEN ' + QuotedStr('SUS') + ' THEN ' + QuotedStr('Suspenso') +
               '	WHEN ' + QuotedStr('TRE') + ' THEN ' + QuotedStr('Treinamento') +
               ' 	WHEN ' + QuotedStr('FER') + ' THEN ' + QuotedStr('F�rias') +
               '	END),ESTADOCIVIL = (CASE C.CO_ESTADO_CIVIL '+
               '	WHEN' + QuotedStr('O') + ' THEN ' + QuotedStr('Solteiro(a)') +
               '	WHEN ' + QuotedStr('C') + ' THEN ' + QuotedStr('Casado(a)') +
               '	WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Separado(a)') +
               '	WHEN ' + QuotedStr('D') + ' THEN ' + QuotedStr('Divorciado(a)') +
               '	WHEN ' + QuotedStr('V') + ' THEN ' + QuotedStr('Vi�vo(a)') +
               '	WHEN ' + QuotedStr('P') + ' THEN ' + QuotedStr('Companheiro(a)') +
               '	WHEN ' + QuotedStr('U') + ' THEN ' + QuotedStr('Uni�o Est�vel') +
               '		END),TP.NO_TPCAL, img.ImageStream '+
               ' From TB25_EMPRESA E ' +
               '      LEFT JOIN TB03_COLABOR C ' +
               '        ON C.CO_EMP = E.CO_EMP ' +
               '      LEFT JOIN TB21_TIPOCAL TP' +
               '        ON TP.CO_TPCAL = C.CO_TPCAL' +
               '      LEFT JOIN TB18_GRAUINS I ON I.CO_INST = C.CO_INST '+
               '      LEFT JOIN TB20_TIPOCON T ' +
               '        ON C.CO_TPCON = T.CO_TPCON ' +
               '      LEFT JOIN TB15_FUNCAO F ' +
               '        ON C.CO_FUN = F.CO_FUN ' +
               '      LEFT JOIN TB14_DEPTO D ' +
               '        ON C.CO_DEPTO = D.CO_DEPTO ' +
               '      LEFT JOIN TB63_ESPECIALIDADE ES ' +
               '        ON C.CO_ESPEC = ES.CO_ESPECIALIDADE ' +
               '      LEFT JOIN TB904_CIDADE CID ON CID.CO_CIDADE = C.CO_CIDADE '+
               '      LEFT JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = C.CO_BAIRRO '+
               '      LEFT JOIN Image img on img.ImageId = c.ImageId ' +
               ' Where C.CO_FUN = F.CO_FUN ' +
               '   And FLA_PROFESSOR = ' + QuotedStr(strP_FLA_PROFESSOR) +
               '   And C.CO_EMP = E.CO_EMP ' +
               '   And C.CO_EMP = ' + strP_CO_EMP +
               ' AND C.CO_COL = ' + strP_CO_COL;

      // Cria uma inst�ncia do Relat�rio.
      Relatorio := TFrmRelFicCadFuncionario.Create(Nil);

      //Preenche alguns campos do relatorio

      { Carrega as globais do relat�rio }

      // Atualiza a Consulta de Detalhe do Relat�rio.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SqlString;
      Relatorio.QryRelatorio.Open;

      if Relatorio.QryRelatorio.IsEmpty then
      begin
        // Retorna -1 para Sem Registros na Consulta.
        intReturn := -1;
      end
      else
      begin
        // Atualiza Campos do Relat�rio Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;
        
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.anoBase := strP_CO_ANO_BASE;
        Relatorio.codigoEmpresa := strP_CO_EMP;
        Relatorio.QRLDescFreq.Caption := Relatorio.QRLDescFreq.Caption + ' - ' + strP_CO_ANO_BASE;
        Relatorio.tipoPresenca := strP_TP_PONTO;

        if strP_FLA_PROFESSOR = 'S' then
          Relatorio.LblTituloRel.Caption := 'FICHA DE INFORMA��ES DO PROFESSOR'
        else
          Relatorio.LblTituloRel.Caption := 'FICHA DE INFORMA��ES DO FUNCION�RIO';

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relat�rio Gerado com Sucesso.
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
  
  //Relat�rios
  DLLRelFicCadFuncionario;

end.
