library WR_RelFreqAluno;

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
  U_FrmRelFreqAluno in 'Relatorios\U_FrmRelFreqAluno.pas' {FrmRelFreqAluno};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelFreqAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR,
 strP_CO_TUR, strP_CO_MAT, strP_CO_ANO_REF, strP_CO_PARAM_FREQUE, strP_MES, strP_DE_MES, strP_CNPJ_INSTI: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelFreqAluno;
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

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelFreqAluno.Create(Nil);

      SqlString := ' SELECT DISTINCT (A.CO_ALU), M.CO_ANO_MES_MAT, A.NO_ALU, A.CO_SEXO_ALU, A.DT_NASC_ALU, '+
                    ' M.CO_ALU_CAD, C.NO_CUR,C.CO_PARAM_FREQ_TIPO, M.CO_MODU_CUR, C.CO_CUR, T.CO_TUR, CT.CO_SIGLA_TURMA as NO_TUR, T.CO_PERI_TUR, MOD.DE_MODU_CUR,DIR.NO_COL,CM.NO_RED_MATERIA '+
                    ' FROM TB07_ALUNO A '+
                    ' JOIN TB08_MATRCUR M ON M.CO_ALU = A.CO_ALU and M.CO_EMP = A.CO_EMP'+
                    ' LEFT JOIN TB132_FREQ_ALU F ON F.CO_ALU = A.CO_ALU '+
                    ' JOIN TB44_MODULO MOD ON MOD.CO_MODU_CUR = M.CO_MODU_CUR '+
                    ' JOIN TB01_CURSO C ON C.CO_CUR = M.CO_CUR '+
                    ' JOIN TB06_TURMAS T ON T.CO_TUR = M.CO_TUR AND T.CO_CUR = M.CO_CUR '+
                    ' JOIN TB129_CADTURMAS CT ON T.CO_TUR = CT.CO_TUR '+
                    ' JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    ' LEFT JOIN TB02_MATERIA MA on MA.CO_MAT = F.CO_MAT ' +
                    ' LEFT JOIN TB107_CADMATERIAS CM on CM.ID_MATERIA = MA.ID_MATERIA ' +
                    ' JOIN TB83_PARAMETRO PR on PR.CO_EMP = A.CO_EMP ' +
                    ' LEFT JOIN TB03_COLABOR DIR on PR.CO_DIR1 = DIR.CO_COL ' +
                    ' WHERE A.CO_EMP = ' + strP_CO_EMP +
                    '   AND YEAR(F.DT_FRE) = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   AND MONTH(F.DT_FRE) = ' + strP_MES +
                    '   AND M.CO_CUR = ' + strP_CO_CUR +
                    '   AND M.CO_TUR = ' + strP_CO_TUR +
                    '   AND M.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_PARAM_FREQUE = 'D' then
      begin
        SqlString := SqlString + ' AND F.CO_MAT is null ';
      end
      else
        if strP_CO_MAT <> nil then
        begin
          SqlString := SqlString + ' AND F.CO_MAT = ' + strP_CO_MAT;
        end;

      SqlString := SqlString + ' ORDER BY A.NO_ALU ';

      // Atualiza a Consulta de Detalhe do Relat�rio.
      Relatorio.QryRelatorio.Close;
      //Bar�o do Rio Branco
      if strP_CNPJ_INSTI = '9014296000174' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEBARRIOBRA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Educandario de Maria
      else if strP_CNPJ_INSTI = '4120476000117' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEEDUCAMARIA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola ABC
      else if strP_CNPJ_INSTI = '7002950000102' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEESCABC;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola Reino Encantado
      else if strP_CNPJ_INSTI = '122135000120' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEESCREINOENCANT;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //ETB
      else if strP_CNPJ_INSTI = '3960623000102' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEETB;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Objetivo Esplanada
      else if strP_CNPJ_INSTI = '4776952000152' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEOBJESPLANADA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Colegio Especifico
      else if strP_CNPJ_INSTI = '10689657000161' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGECOESPECIFICO;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Col�gio Esplanada
      else if strP_CNPJ_INSTI = '4223948000167' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGECOESPLANADA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Isaac Newton
      else if strP_CNPJ_INSTI = '32908634000133' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEISACNEWTON;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola Modelo
      else if strP_CNPJ_INSTI = '11558593000122' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEMODELO;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Conex�o Aquarela
      else if strP_CNPJ_INSTI = '11489849000133' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=conexao@aquarela;Persist Security Info=True;User ID=conexao@aquarela;'+
  'Initial Catalog=BDPGECONAQU;Data Source=(local);Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      else
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end;
  
      DM.Conn.Open;
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
        // Atualiza Campos do Relat�rio Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.co_disciplina := StrToInt(strP_CO_MAT);
        Relatorio.QrlAnoBase.Caption := strP_CO_ANO_REF;
        Relatorio.QrlMesRef.Caption := UpperCase(strP_DE_MES);
        Relatorio.QrlMes.Caption := strP_MES;
        Relatorio.codigoEmpresa := strP_CO_EMP;

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
  DLLRelFreqAluno;

end.