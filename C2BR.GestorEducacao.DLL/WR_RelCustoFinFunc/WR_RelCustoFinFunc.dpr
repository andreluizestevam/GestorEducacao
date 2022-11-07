library WR_RelCustoFinFunc;

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
  U_FrmRelCustoFinFunc in 'Relatorios\U_FrmRelCustoFinFunc.pas' {FrmRelCustoFinFunc};

// Controle Administrativo > Controle de Funcion�rios
// Relat�rio: Emiss�o de Custo Financeiro (Sal�rio) de Funcion�rios
// STATUS: OK 
function DLLRelCustoFinFunc(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_EMP_SELEC, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelCustoFinFunc;
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
      SqlString := 'select f.no_fun, c.CO_MAT_COL, c.NO_COL, c.NU_CPF_COL, c.DT_INIC_ATIV_COL, c.CO_TPCAL,' +
            ' c.VL_SALAR_COL, c.NU_CARGA_HORARIA,C.CO_EMP, CID.NO_CIDADE, BAI.NO_BAIRRO,' +
            ' DEFICIENCIA = (CASE TP_DEF' +
              ' WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Nenhuma') +
              ' WHEN ' + QuotedStr('A') + ' THEN ' + QuotedStr('Auditivo') +
              ' WHEN ' + QuotedStr('V') + ' THEN ' + QuotedStr('Visual') +
              ' WHEN ' + QuotedStr('F') + ' THEN ' + QuotedStr('F�sico') +
              ' WHEN ' + QuotedStr('M') + ' THEN ' + QuotedStr('Mental') +
              ' WHEN ' + QuotedStr('I') + ' THEN ' + QuotedStr('M�ltiplas') +
              ' WHEN ' + QuotedStr('O') + ' THEN ' + QuotedStr('Outros') +
              ' ELSE ' + QuotedStr('Sem Registro') +
              ' END),' +
            ' CATEGORIA = (CASE FLA_PROFESSOR' +
              ' WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Professor') +
              ' WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Funcion�rio') +
              ' END),' +
            ' D.CO_SIGLA_DEPTO, G.NO_INST, TC.NO_TPCON, EMP.NO_FANTAS_EMP' +
            ' from TB03_COLABOR c' +
            ' join TB15_funcao f ON c.co_fun = f.co_fun' +
            ' join TB14_DEPTO D ON D.CO_DEPTO = C.CO_DEPTO' +
            ' left join TB18_GRAUINS G ON G.CO_INST = C.CO_INST' +
            ' JOIN TB20_TIPOCON TC ON TC.CO_TPCON = C.CO_TPCON' +
            ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = C.CO_CIDADE' +
            ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = C.CO_BAIRRO' +
            ' JOIN TB25_EMPRESA EMP ON EMP.CO_EMP = C.CO_EMP' +
            ' WHERE C.CO_EMP = EMP.CO_EMP';

            // Par�metros da consulta.
            if strP_CO_EMP_SELEC <> 'T' then
              SqlString := SqlString + ' AND C.CO_EMP = ' + strP_CO_EMP_SELEC;
            if strP_FLA_PROFESSOR <> 'T' then
              SqlString := SqlString + ' AND C.FLA_PROFESSOR = ' + QuotedStr(strP_FLA_PROFESSOR);
            if strP_CO_SEXO_COL <> 'T' then
              SqlString := SqlString + ' AND C.CO_SEXO_COL = ' + QuotedStr(strP_CO_SEXO_COL);
            if strP_tp_def <> 'T' then
              SqlString := SqlString + ' AND C.tp_def = ' + QuotedStr(strP_tp_def);

            SqlString := SqlString + ' GROUP BY EMP.NO_FANTAS_EMP,c.CO_MAT_COL,c.NO_COL, c.NU_CPF_COL,c.DT_INIC_ATIV_COL,' +
            'c.CO_TPCAL,c.VL_SALAR_COL,C.CO_EMP,c.NU_CARGA_HORARIA,f.no_fun, CID.NO_CIDADE, ' +
            'BAI.NO_BAIRRO,C.tp_def,C.FLA_PROFESSOR,D.CO_SIGLA_DEPTO, G.NO_INST, TC.NO_TPCON' +
            ' order by EMP.NO_FANTAS_EMP,C.NO_COL';

      // Cria uma inst�ncia do Relat�rio.
      Relatorio := TFrmRelCustoFinFunc.Create(Nil);

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
        
        Relatorio.QrlParamRel.Caption := strParamRel;
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
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
  DLLRelCustoFinFunc;

end.
