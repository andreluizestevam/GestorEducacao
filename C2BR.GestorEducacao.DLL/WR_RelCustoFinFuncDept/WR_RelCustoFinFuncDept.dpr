library WR_RelCustoFinFuncDept;

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
  U_FrmRelCustoFinFuncDept in 'Relatorios\U_FrmRelCustoFinFuncDept.pas' {FrmRelCustoFinFuncDept};

// Controle Administrativo > Controle de Funcionários
// Relatório: Emissão de Custo Financeiro (Salário) de Funcionários (Por Departamento)
// STATUS: OK.
function DLLRelCustoFinFuncDept(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_DEPTO, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelCustoFinFuncDept;
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

      // Monta a Consulta do Relatório.
      SqlString := ' select f.no_fun,C.CO_DEPTO,c.CO_MAT_COL,c.NO_COL,c.NU_CPF_COL,c.DT_INIC_ATIV_COL,c.CO_TPCAL,'+
            'c.VL_SALAR_COL,c.NU_CARGA_HORARIA,C.CO_EMP, CID.NO_CIDADE, BAI.NO_BAIRRO, '+
            ' DEFICIENCIA = (CASE TP_DEF '+
              ' WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Nenhuma') +
              ' WHEN ' + QuotedStr('A') + ' THEN ' + QuotedStr('Auditivo') +
              ' WHEN ' + QuotedStr('V') + ' THEN ' + QuotedStr('Visual') +
              ' WHEN ' + QuotedStr('F') + ' THEN ' + QuotedStr('Físico') +
              ' WHEN ' + QuotedStr('M') + ' THEN ' + QuotedStr('Mental') +
              ' WHEN ' + QuotedStr('I') + ' THEN ' + QuotedStr('Múltiplas') +
              ' WHEN ' + QuotedStr('O') + ' THEN ' + QuotedStr('Outros') +
              ' ELSE ' + QuotedStr('Sem Registro') +
            ' END), '+
            ' CATEGORIA = (CASE FLA_PROFESSOR '+
              ' WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Professor') +
              ' WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Funcionário') +
            ' END), '+
            ' D.NO_DEPTO, G.NO_INST, TC.NO_TPCON, EMP.NO_FANTAS_EMP, EMP.SIGLA '+
              { ' 	(SELECT SUM(C.VL_SALAR_COL) '+
               '	 FROM TB03_COLABOR C '+
               '	 WHERE C.CO_DEPTO = ' + IntToStr(codDepartamento[cbDepto.ItemIndex]) +
               '	) TOTALSALARIO '+ }
               ' from TB03_COLABOR c '+
               ' join TB15_funcao f ON c.co_fun = f.co_fun '+
               ' join TB14_DEPTO D ON D.CO_DEPTO = C.CO_DEPTO '+
               ' join TB18_GRAUINS G ON G.CO_INST = C.CO_INST '+
               ' JOIN TB20_TIPOCON TC ON TC.CO_TPCON = C.CO_TPCON '+
               ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = C.CO_CIDADE '+
               ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = C.CO_BAIRRO '+
               ' JOIN TB25_EMPRESA EMP ON EMP.CO_EMP = C.CO_EMP '+
               ' WHERE C.CO_DEPTO = D.CO_DEPTO ';

        if strP_CO_DEPTO <> 'T' then
          SqlString := SqlString + ' AND C.CO_DEPTO = ' + strP_CO_DEPTO;

        if strP_FLA_PROFESSOR <> 'T' then
          SqlString := SqlString + ' AND C.FLA_PROFESSOR = ' + QuotedStr(strP_FLA_PROFESSOR);

        if strP_CO_SEXO_COL <> 'T' then
          SqlString := SqlString + ' AND C.CO_SEXO_COL = ' + QuotedStr(strP_CO_SEXO_COL);

        if strP_tp_def <> 'T' then
          SqlString := SqlString + ' AND C.tp_def = ' + QuotedStr(strP_tp_def);

        SqlString := SqlString + ' GROUP BY D.NO_DEPTO,C.CO_DEPTO,EMP.NO_FANTAS_EMP,EMP.SIGLA,c.CO_MAT_COL,c.NO_COL,c.NU_CPF_COL,c.DT_INIC_ATIV_COL,c.CO_TPCAL,'+
                   'c.VL_SALAR_COL,C.CO_EMP,c.NU_CARGA_HORARIA,f.no_fun, CID.NO_CIDADE, BAI.NO_BAIRRO,'+
                   'C.tp_def,C.FLA_PROFESSOR,D.CO_SIGLA_DEPTO, G.NO_INST, TC.NO_TPCON ' +
                   'order by D.NO_DEPTO,C.NO_COL ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelCustoFinFuncDept.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
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
        // Atualiza Campos do Relatório Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;
        
        Relatorio.QrlParamRel.Caption := strParamRel;
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
  DLLRelCustoFinFuncDept;

end.
