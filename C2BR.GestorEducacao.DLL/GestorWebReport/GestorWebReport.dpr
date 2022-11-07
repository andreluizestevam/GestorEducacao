library GestorWebReport;

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
  U_FrmRelInformacaoCurso in 'Relatorios\U_FrmRelInformacaoCurso.pas' {FrmRelInformacaoCurso},
  U_FrmRelBolEscAlu in 'Relatorios\U_FrmRelBolEscAlu.pas' {FrmRelBolEscAlu},
  QrAngLbl in '..\General\QrAngLbl.pas',
  QrAConst in '..\General\Qraconst.pas',
  U_FrmRelPlanRealizCentroCusto in 'Relatorios\U_FrmRelPlanRealizCentroCusto.pas' {FrmRelPlanRealizCentroCusto},
  U_FrmRelPlanRealizado in 'Relatorios\U_FrmRelPlanRealizado.pas' {FrmRelPlanRealizado},
  U_FrmRelRelacFuncionario in 'Relatorios\U_FrmRelRelacFuncionario.pas' {FrmRelRelacFuncionario},
  U_Funcoes in '..\General\U_Funcoes.pas',
  U_FrmRelAniversarioFuncionario in 'Relatorios\U_FrmRelAniversarioFuncionario.pas' {FrmRelAniversarioFuncionario},
  U_FrmRelGerMapaFrequenciaFuncionario in 'Relatorios\U_FrmRelGerMapaFrequenciaFuncionario.pas' {FrmRelGerMapaFrequenciaFuncionario},
  U_FrmRelGerMapaFrequenciaProfessor in 'Relatorios\U_FrmRelGerMapaFrequenciaProfessor.pas' {FrmRelGerMapaFrequenciaProfessor},
  U_FrmRelExtratoFreqLivre in 'Relatorios\U_FrmRelExtratoFreqLivre.pas' {FrmRelExtratoFreqLivre},
  U_FrmRelExtratoFreqFunc in 'Relatorios\U_FrmRelExtratoFreqFunc.pas' {FrmRelExtratoFreqFunc},
  U_FrmRelAvaliacaoModelo in 'Relatorios\U_FrmRelAvaliacaoModelo.pas' {FrmRelAvaliacaoModelo},
  U_FrmRelAvaliacao in 'Relatorios\U_FrmRelAvaliacao.pas' {FrmRelAvaliacao},
  U_FrmRelListagemProva in 'Relatorios\U_FrmRelListagemProva.pas' {FrmRelListagemProva},
  U_FrmRelCanhoto in 'Relatorios\U_FrmRelCanhoto.pas' {FrmRelCanhoto};

// STATUS: Pendente Tela de Parâmetros  
function DLLRelInformacaoCurso(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelInformacaoCurso;
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
      SqlString := 'Select C.*, D.NO_DPTO_CUR, CC.NO_COOR_CUR as NO_SUBDPTO_CUR, I.*,MO.DE_MODU_CUR ' +
            'From TB01_CURSO C ' +
            'LEFT JOIN TB77_DPTO_CURSO D on C.CO_EMP = D.CO_EMP AND C.CO_DPTO_CUR = D.CO_DPTO_CUR ' +
            'LEFT JOIN TB68_COORD_CURSO CC on C.CO_EMP = CC.CO_EMP AND C.CO_DPTO_CUR = CC.CO_DPTO_CUR AND C.CO_SUB_DPTO_CUR = CC.CO_COOR_CUR ' +
            'LEFT JOIN TB19_INFOR_CURSO I on C.CO_CUR = I.CO_CUR AND C.CO_EMP = I.CO_EMP ' +
            'JOIN TB44_MODULO MO on C.CO_MODU_CUR = MO.CO_MODU_CUR ' +
            'Where C.CO_EMP = ' + strP_CO_EMP;

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelInformacaoCurso.Create(Nil);

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
        
        // Atualiza Campos do Relatório Diretamente.
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);

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

// Controle Administrativo > Controle de Funcionários
// Relatório: EMISSÃO DO MAPA DE PLANEJAMENTO FINANCEIRO (CENTRO DE CUSTO)
// STATUS: OK
function DLLRelPlanRealizCentroCusto(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_TP_CONTA, strP_TP_RELATORIO, strP_CO_ANO_INI, strP_CO_ANO_FIM, strP_CO_DEPTO : PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelPlanRealizCentroCusto;
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
      SqlString:=' SELECT A.*, P.*,(P.VL_PLAN_MES1 + ' +
            ' P.VL_PLAN_MES2 + P.VL_PLAN_MES3 + P.VL_PLAN_MES4 + ' +
            ' P.VL_PLAN_MES5 + P.VL_PLAN_MES6 + P.VL_PLAN_MES7 + ' +
            ' P.VL_PLAN_MES8 + P.VL_PLAN_MES9 + P.VL_PLAN_MES10 + ' +
            ' P.VL_PLAN_MES11 + P.VL_PLAN_MES12) as VL_PLAN_TOTAL,' +
            ' (P.VL_REAL_MES_1 + P.VL_REAL_MES_2 + P.VL_REAL_MES_3 + P.VL_REAL_MES_4 + ' +
            'P.VL_REAL_MES_5 + P.VL_REAL_MES_6 + P.VL_REAL_MES_7 + ' +
            'P.VL_REAL_MES_8 + P.VL_REAL_MES_9 + P.VL_REAL_MES_10 + ' +
            'P.VL_REAL_MES_11 + P.VL_REAL_MES_12) as VL_REAL_TOTAL, PC.DE_CONTA_PC, PC.CO_GRUP_CTA, PC.CO_SGRUP_CTA, PC.CO_CONTA_PC ' +
            ' FROM TB099_CENTRO_CUSTO A, TB112_PLANCUSTO P, TB56_PLANOCTA PC, TB53_GRP_CTA GC ' +
            ' WHERE P.CO_CENT_CUSTO  = A.CO_CENT_CUSTO' +
            ' AND PC.CO_SEQU_PC = P.CO_SEQU_PC ' +
            ' AND PC.CO_GRUP_CTA = GC.CO_GRUP_CTA ' +
            ' AND P.CO_EMP = ' + strP_CO_EMP +
            ' AND GC.TP_GRUP_CTA = ' + quotedStr(strP_TP_CONTA);

      if strP_CO_ANO_INI <> Nil then
        SqlString := SqlString + ' AND P.CO_ANO_REF >= '+ strP_CO_ANO_INI;

      if strP_CO_ANO_FIM <> Nil then
        SqlString := SqlString + ' AND P.CO_ANO_REF <= '+ strP_CO_ANO_FIM;

      if strP_CO_DEPTO <> 'T' then
        SqlString := SqlString + ' AND A.CO_DEPTO = '+ strP_CO_DEPTO;

      SqlString := SqlString + ' ORDER BY A.NU_CTA_CENT_CUSTO, P.CO_ANO_REF ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelPlanRealizCentroCusto.Create(Nil);

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

        // Atualiza Campos do Relatório Diretamente.
        Relatorio.QRLParametros.Caption := strParamRel;
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        //Preenche alguns campos do relatorio
        if strP_TP_RELATORIO = 'A' then
        begin
          Relatorio.LblTituloRel.Caption := 'MAPA DE PLANEJAMENTO FINANCEIRO POR CENTRO DE CUSTO - ANALÍTICO';
        end
        else
        begin
          Relatorio.LblTituloRel.Caption := 'MAPA DE DIFERENÇA DO PLANEJAMENTO FINANCEIRO - POR CENTRO DE CUSTO';
        end;

        { Carrega as globais do relatório }
        if strP_TP_CONTA = 'D' then
          Relatorio.vDespesa := true
        else
          Relatorio.vDespesa := false;

        if strP_TP_RELATORIO = 'D' then
          Relatorio.vSintetico := true
        else
          Relatorio.vSintetico := false;

        if strP_CO_ANO_INI <> nil then
          Relatorio.AnoIni := strP_CO_ANO_INI
        else
          Relatorio.AnoIni := '';

        if strP_CO_ANO_FIM <> nil then
          Relatorio.AnoFim := strP_CO_ANO_FIM
        else
          Relatorio.AnoFim := '';

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

// Controle Administrativo > Controle de Funcionários
// Relatório: EMISSÃO DO MAPA DE PLANEJAMENTO FINANCEIRO (CONTA CONTÁBIL)
// STATUS: OK
function DLLFrmRelPlanRealizado(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_TP_CONTA , strP_TP_RELATORIO, strP_CO_ANO_INI, strP_CO_ANO_FIM : PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelPlanRealizado;
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
      SqlString:=' SELECT A.*, B.DE_GRUP_CTA, B.TP_GRUP_CTA, P.*,(P.VL_PLAN_MES1 + ' +
             ' P.VL_PLAN_MES2 + P.VL_PLAN_MES3 + P.VL_PLAN_MES4 + ' +
             ' P.VL_PLAN_MES5 + P.VL_PLAN_MES6 + P.VL_PLAN_MES7 + ' +
             ' P.VL_PLAN_MES8 + P.VL_PLAN_MES9 + P.VL_PLAN_MES10 + ' +
             ' P.VL_PLAN_MES11 + P.VL_PLAN_MES12) as VL_PLAN_TOTAL,' +
             ' (P.VL_REAL_MES_1 + P.VL_REAL_MES_2 + P.VL_REAL_MES_3 + P.VL_REAL_MES_4 + ' +
             'P.VL_REAL_MES_5 + P.VL_REAL_MES_6 + P.VL_REAL_MES_7 + ' +
             'P.VL_REAL_MES_8 + P.VL_REAL_MES_9 + P.VL_REAL_MES_10 + ' +
             'P.VL_REAL_MES_11 + P.VL_REAL_MES_12) as VL_REAL_TOTAL, ' +
             '       SG.DE_SGRUP_CTA                                     '+
             ' FROM TB56_PLANOCTA A, TB53_GRP_CTA B, TB111_PLANEJ_FINAN P, TB54_SGRP_CTA SG  '+
             ' WHERE A.CO_GRUP_CTA = B.CO_GRUP_CTA                       '+
             ' AND   A.CO_GRUP_CTA = SG.CO_GRUP_CTA                      '+
             ' AND   A.CO_SGRUP_CTA = SG.CO_SGRUP_CTA                    '+
             ' AND   P.CO_SEQU_PC  = A.CO_SEQU_PC                        '+
             ' AND   P.CO_EMP      = '+ strP_CO_EMP     +
             ' AND   b.TP_GRUP_CTA = '+ quotedStr(strP_TP_CONTA);

      if strP_CO_ANO_INI <> Nil then
        SqlString := SqlString + ' AND P.CO_ANO_REF >= '+ strP_CO_ANO_INI;

      if strP_CO_ANO_FIM <> Nil then
        SqlString := SqlString + ' AND P.CO_ANO_REF <= '+ strP_CO_ANO_FIM;

      SqlString := SqlString + ' ORDER BY P.CO_ANO_REF ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelPlanRealizado.Create(Nil);

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
        
        // Atualiza Campos do Relatório Diretamente.
        Relatorio.QRLParametros.Caption := strParamRel;
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        //Preenche alguns campos do relatorio
        if strP_TP_RELATORIO = 'A' then
        begin
          Relatorio.LblTituloRel.Caption := 'MAPA DE PLANEJAMENTO FINANCEIRO POR CONTA CONTÁBIL - ANALÍTICO';
        end
        else
        begin
          Relatorio.LblTituloRel.Caption := 'MAPA DE DIFERENÇA DO PLANEJAMENTO FINANCEIRO - POR CONTA CONTÁBIL';
        end;

        { Carrega as globais do relatório }
        if strP_TP_CONTA = 'D' then
          Relatorio.TipoVisualizacao := 1
        else
          Relatorio.TipoVisualizacao := 0;

        if strP_TP_RELATORIO = 'A' then
          Relatorio.TipoRelatorio := 0
        else
          Relatorio.TipoRelatorio := 1;

        if strP_CO_ANO_INI <> nil then
          Relatorio.AnoInicio := strP_CO_ANO_INI
        else
          Relatorio.AnoInicio := '';

        if strP_CO_ANO_FIM <> nil then
          Relatorio.AnoFim := strP_CO_ANO_FIM
        else
          Relatorio.AnoFim := '';

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

// Controle Administrativo > Controle de Funcionários
// Relatório: EMISSÃO DA RELAÇÃO DE FUNCIONÁRIOS (PARAMETRIZADA)
// STATUS: OK
function DLLRelRelacFuncionario(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP , strP_FLA_PROFESSOR,
strP_CO_FUN, strP_CO_INST, strP_TP_DEF, strP_CO_SEXO_COL, strP_CO_UF, strP_CO_CIDADE, strP_CO_BAIRRO : PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacFuncionario;
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
      SqlString := 'select f.no_fun, c.CO_EMP, c.CO_COL, c.DT_NASC_COL, c.NO_COL, c.CO_MAT_COL, c.DT_INIC_ATIV_COL,c.NU_TELE_CELU_COL,c.CO_SEXO_COL, ' +
            ' CID.NO_CIDADE, BAI.NO_BAIRRO, ' +
               ' DEFICIENCIA = (CASE TP_DEF '+
               '                 		WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Nenhuma') +
               '                		WHEN ' + QuotedStr('A') + ' THEN ' + QuotedStr('Auditivo') +
               '                		WHEN ' + QuotedStr('V') + ' THEN ' + QuotedStr('Visual') +
               '                		WHEN ' + QuotedStr('F') + ' THEN ' + QuotedStr('Físico') +
               '                 		WHEN ' + QuotedStr('M') + ' THEN ' + QuotedStr('Mental') +
               '                		WHEN ' + QuotedStr('I') + ' THEN ' + QuotedStr('Múltiplas') +
               '                		WHEN ' + QuotedStr('O') + ' THEN ' + QuotedStr('Outros') +
               '                   ELSE ' + QuotedStr('Sem Registro') +
               '                END), '+
               ' CATEGORIA = (CASE FLA_PROFESSOR '+
               '                   WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Professor') +
               '                   WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Funcionário') +
               '                   END), '+
               ' D.NO_DEPTO, G.NO_INST, TC.NO_TPCON '+
               ' from TB03_COLABOR c '+
               ' join TB15_funcao f ON c.co_fun = f.co_fun '+
               ' join TB14_DEPTO D ON D.CO_DEPTO = C.CO_DEPTO '+
               ' left join TB18_GRAUINS G ON G.CO_INST = C.CO_INST '+
               ' JOIN TB20_TIPOCON TC ON TC.CO_TPCON = C.CO_TPCON '+
               ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = C.CO_CIDADE '+
               ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = C.CO_BAIRRO '+
               ' WHERE 1 = 1 ';

      // Unidade
      if strP_CO_EMP <> Nil then
      begin
        SqlString := SqlString + ' AND C.CO_EMP = ' + strP_CO_EMP;
      end;

      //Categoria
      if strP_FLA_PROFESSOR <> 'T' then
      begin
        SqlString := SqlString + ' AND C.FLA_PROFESSOR = ' + QuotedStr(strP_FLA_PROFESSOR);
      end;

      // Função
      if strP_CO_FUN <> 'T' then
      begin
        SqlString := SqlString + ' AND C.CO_FUN = ' + strP_CO_FUN;
      end;

      // Grau de Instrução
      if strP_CO_INST <> 'T'  then
      begin
        SqlString := SqlString + ' AND C.CO_INST = ' + strP_CO_INST;
      end;

      // Deficiência
      if strP_TP_DEF <> 'T' then
      begin
        SqlString := SqlString + ' AND C.TP_DEF = ' + QuotedStr(strP_TP_DEF);
      end;

      // Sexo
      if strP_CO_SEXO_COL <> 'T' then
      begin
        SqlString := SqlString + ' AND C.CO_SEXO_COL = ' + quotedStr(strP_CO_SEXO_COL);
      end;

      // UF
      if strP_CO_UF <> 'T' then
      begin
        SqlString := SqlString + ' AND C.CO_ESTA_ENDE_COL = ' + quotedStr(strP_CO_UF);
      end;

      // Cidade
      if strP_CO_CIDADE <> 'T' then
      begin
        SqlString := SqlString + ' AND C.CO_CIDADE = ' + strP_CO_CIDADE;
      end;

      // Bairro
      if strP_CO_BAIRRO <> 'T' then
      begin
        SqlString := SqlString + ' AND C.CO_BAIRRO = ' + strP_CO_BAIRRO;
      end;

      SqlString := SqlString + ' order by c.NO_COL ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelRelacFuncionario.Create(Nil);

      //Preenche alguns campos do relatorio

      { Carrega as globais do relatório }

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

// Controle Administrativo > Controle de Funcionários
// Relatório: EMISSÃO DA RELAÇÃO DE FUNCIONÁRIOS ANIVERSARIANTES
// STATUS: OK
function DLLRelAniversarioFuncionario(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_FLA_PROFESSOR, strP_MES: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAniversarioFuncionario;
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
      SqlString := 'select c.no_col,c.co_mat_col,c.co_sexo_col,c.dt_nasc_col,' +
              'c.nu_tele_resi_col,c.nu_tele_celu_col,c.co_emai_col,fu.no_fun,de.no_depto,'+
              'e.sigla from tb03_colabor c '+
              'left join tb15_funcao fu on c.co_fun = fu.co_fun ' +
              'left join tb14_depto de on c.co_depto = de.co_depto ' +
              'left join tb25_empresa e on c.co_emp = e.co_emp ' +
              'where c.dt_nasc_col is not null ' +
              'and c.fla_professor = ' + quotedStr('N');

      if strP_MES <> '0' then
      begin
        SqlString := SqlString + ' and month(c.dt_nasc_col) = ' + strP_MES;
      end;

      if strP_CO_EMP <> nil then
      begin
        SqlString := SqlString + ' and c.co_emp = ' + strP_CO_EMP;
      end;

      SqlString := SqlString + ' order by month(c.dt_nasc_col),day(c.dt_nasc_col),c.no_col,c.co_emp';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelAniversarioFuncionario.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.QrlUnidade.Caption := UpperCase(strParamRel);
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

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DO EXTRATO DE FREQÜÊNCIA DO FUNCIONÁRIO/PROFESSOR (Ponto Padrão)
// STATUS: OK
function DLLRelGerMapaFrequenciaFuncionario(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_FLA_PROFESSOR, strP_CO_COL, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelGerMapaFrequenciaFuncionario;
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
      SqlString := 'SET LANGUAGE PORTUGUESE SELECT DISTINCT f.dt_freq,f.co_emp, c.no_col, c.co_mat_col,c.co_col, f.FLA_PRESENCA,' +
                   'FREQUENCIA = (CASE f.FLA_PRESENCA '+
                   '                     WHEN '+ QuotedStr('S') + ' THEN ' + QuotedStr('Presença')   +
                   '                     WHEN '+ QuotedStr('N') + ' THEN ' + QuotedStr('Falta')   +
                   '                    END) ' +
                   ' FROM TB199_FREQ_FUNC f ' +
                   'JOIN TB03_COLABOR c on c.co_col = f.co_col and c.co_emp = f.co_emp ' +
		               ' WHERE f.co_emp = ' + strP_CO_EMP +
			             ' AND f.co_col = ' + strP_CO_COL +
			             ' AND f.DT_FREQ BETWEEN ' + QuotedStr(strP_DT_INI) + ' AND ' + QuotedStr(strP_DT_FIM) +
                   'Order By f.DT_FREQ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelGerMapaFrequenciaFuncionario.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;
        
        if strP_FLA_PROFESSOR = 'N' then
          Relatorio.LblTituloRel.Caption := 'EXTRATO DE FREQUÊNCIA - FUNCIONÁRIO'
        else
          Relatorio.LblTituloRel.Caption := 'EXTRATO DE FREQUÊNCIA - PROFESSOR';

        Relatorio.QRLPeriodo.Caption := strP_DT_INI + ' à ' + strP_DT_FIM;
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

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DO EXTRATO DE FREQÜÊNCIA DO FUNCIONÁRIO/PROFESSOR (Ponto Livre)
// STATUS: OK
function DLLRelGerMapaFrequenciaProfessor(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_FLA_PROFESSOR, strP_CO_COL, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelGerMapaFrequenciaProfessor;
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
    SqlString := 'SET LANGUAGE PORTUGUESE SELECT DISTINCT f.dt_freq,f.co_emp, c.no_col, c.co_mat_col,c.co_col, f.FLA_PRESENCA, f.TP_FREQ, f.HR_FREQ, ' +
                 'FREQUENCIA = (CASE f.TP_FREQ '+
                 '                     WHEN '+ QuotedStr('E') + ' THEN ' + QuotedStr('Entrada')   +
                 '                     WHEN '+ QuotedStr('S') + ' THEN ' + QuotedStr('Saída')   +
                 '                    END), e.no_fantas_emp, ' +
                 'FINALIZADO = (CASE f.FLA_FINALIZADO '+
                 '                     WHEN '+ QuotedStr('S') + ' THEN ' + QuotedStr('Sim')   +
                 '                     WHEN '+ QuotedStr('N') + ' THEN ' + QuotedStr('Não')   +
                 '                    END) ' +
                 ' FROM TB199_FREQ_FUNC f ' +
                 'JOIN TB03_COLABOR c on c.co_col = f.co_col and c.co_emp = f.co_emp ' +
                 'join tb25_empresa e on e.co_emp = f.co_emp_ativ ' +
                 ' WHERE f.co_emp = ' + strP_CO_EMP +
                 ' AND f.co_col = ' + strP_CO_COL +
                 ' AND f.DT_FREQ BETWEEN ' + QuotedStr(strP_DT_INI) + ' AND ' + QuotedStr(strP_DT_FIM) +
                 'Order By f.DT_FREQ,f.HR_FREQ';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelGerMapaFrequenciaProfessor.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;
        
        if strP_FLA_PROFESSOR = 'N' then
          Relatorio.LblTituloRel.Caption := 'EXTRATO DE FREQUÊNCIA DO FUNCIONÁRIO'
        else
          Relatorio.LblTituloRel.Caption := 'EXTRATO DE FREQUÊNCIA DO PROFESSOR';

        Relatorio.QRLPeriodo.Caption := strP_DT_INI + ' à ' + strP_DT_FIM;
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

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DO DEMONSTRATIVO ANUAL DE FREQÜÊNCIA DE FUNCIONÁRIO/PROFESSOR (Ponto Livre)
// STATUS: OK - Trabalhando
function DLLRelExtratoFreqLivre(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_COL, strP_ANO_REFER: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelExtratoFreqLivre;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath : string;
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

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelExtratoFreqLivre.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      //Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := StringReplace(Relatorio.QryRelatorio.SQL.Text,'CODIGOEMPRESA',strP_CO_EMP,[rfIgnoreCase,rfReplaceAll]);
      Relatorio.QryRelatorio.SQL.Text := StringReplace(Relatorio.QryRelatorio.SQL.Text,'CODIGOCOLABORADOR',strP_CO_COL,[rfIgnoreCase,rfReplaceAll]);
      Relatorio.QryRelatorio.SQL.Text := StringReplace(Relatorio.QryRelatorio.SQL.Text,'ANOESCOLHIDO',strP_ANO_REFER,[rfIgnoreCase,rfReplaceAll]);
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
        
        Relatorio.QrlAno.Caption := strP_ANO_REFER;
        Relatorio.codigoEmpresa := strP_CO_EMP;
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

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DO DEMONSTRATIVO ANUAL DE FREQÜÊNCIA DE FUNCIONÁRIO/PROFESSOR (Ponto Padrão)
//
function DLLRelExtratoFreqFunc(strSessionID, strIdentFunc, strPathReportGenerate, strP_CO_EMP, strP_CO_COL, strP_ANO_REFER, strP_FLA_PROFESSOR: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelExtratoFreqFunc;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString : string;
begin

  intReturn := 0;
  PDFFilt := Nil;
  Relatorio := Nil;

  Try
    Try
      FilePath := '' + strPathReportGenerate + 'RelExtratoFreqFunc.pdf';
      PDFFilt := TQRPDFDocumentFilter.Create(FilePath);
      PDFFilt.AddFontMap( 'WebDings:ZapfDingBats' );
      PDFFilt.TextOnTop := true;
      PDFFilt.LeftMargin := 0;
      PDFFilt.topMargin := 0;
      PDFFilt.CompressionOn := true;
      PDFFilt.Concatenating := true;

      // Monta a Consulta do Relatório.
      SQlString := 'Select C.*, F.NO_FUN ' +
                   'From TB03_COLABOR C ' +
                   ' LEFT JOIN TB15_FUNCAO F on C.CO_FUN = F.CO_FUN ' +
                   'Where c.co_emp = ' + strP_CO_EMP +
                   ' And CO_COL = ' + strP_CO_COL;

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelExtratoFreqFunc.Create(Nil);

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
        
        Relatorio.QrlAno.Caption := strP_ANO_REFER;
        Relatorio.codigoEmpresa := strP_CO_EMP;

        if strP_FLA_PROFESSOR = 'N' then
          Relatorio.LblTituloRel.Caption := 'DEMONSTRATIVO ANUAL DE FREQÜÊNCIA FUNCIONÁRIO'
        else
          Relatorio.LblTituloRel.Caption := 'DEMONSTRATIVO ANUAL DE FREQÜÊNCIA PROFESSOR';

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

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO FORMULÁRIO DE AVALIAÇÃO (MODELO)
// STATUS: OK - Pendente
function DLLRelAvaliacaoModelo(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_NUM_PESQ: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAvaliacaoModelo;
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

      SqlString := 'SELECT T.*, TA.NO_TIPO_AVAL, TA.DE_OBJE_AVAL, TA.DE_OBSE_AVAL, AM.NU_AVAL_MASTER, mo.DE_MODU_CUR, c.NO_CUR, ct.NO_TURMA,'+
               'cm.NO_MATERIA, year(DT_CADASTRO) as ANO_REFER, month(DT_CADASTRO) as MES_REFER, AM.FLA_PUBLICO_ALVO ' +
               'FROM TB72_TIT_QUES_AVAL T ' +
               'JOIN TB73_TIPO_AVAL TA on T.CO_TIPO_AVAL =  TA.CO_TIPO_AVAL ' +
               'JOIN TB201_AVAL_MASTER AM on TA.CO_TIPO_AVAL = AM.CO_TIPO_AVAL ' +
               'left join tb44_modulo mo on mo.co_modu_cur = AM.co_modu_cur ' +
               'left join tb01_curso c on c.co_cur = AM.CO_SERIE_CUR and c.co_modu_cur = AM.co_modu_cur and c.co_emp = am.co_emp ' +
               'left join tb129_cadturmas ct on ct.co_tur = AM.co_tur and ct.co_emp = am.co_emp ' +
               'left join tb02_materia ma on ma.co_mat = AM.co_mat and ma.co_cur = AM.CO_SERIE_CUR and ma.co_modu_cur = AM.co_modu_cur and ma.co_emp = am.co_emp ' +
               'left join tb107_cadmaterias cm on ma.id_materia = cm.id_materia ' +
               'WHERE AM.CO_EMP = ' + strP_CO_EMP +
               ' and AM.NU_AVAL_MASTER = ' + strP_NUM_PESQ;


      // Cria uma instância do Relatório.
      Relatorio := TFrmRelAvaliacaoModelo.Create(Nil);

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

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DA AVALIAÇÃO DE ATIVIDADES
// STATUS: OK
function DLLRelAvaliacao(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_TIPO_AVAL, strP_CO_EMP, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAvaliacao;
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

      SqlString := 'SET LANGUAGE PORTUGUESE ' +
               ' select DISTINCT A.*, C.NO_CUR, CT.NO_TURMA AS NO_TUR, CM.NO_MATERIA, CO.NO_COL, TA.NO_TIPO_AVAL, ' +
               '        TA.DE_OBJE_AVAL, TA.DE_OBSE_AVAL, MO.DE_MODU_CUR ' +
               ' from TB78_PESQ_AVAL A ' +
               ' LEFT JOIN TB44_MODULO MO on MO.CO_MODU_CUR = A.CO_MODU_CUR ' +
               ' LEFT JOIN TB01_CURSO C on A.CO_CUR = C.CO_CUR ' +
               ' LEFT JOIN TB06_TURMAS T on A.CO_TUR = T.CO_TUR ' +
               ' LEFT JOIN TB129_CADTURMAS CT on T.CO_TUR = CT.CO_TUR ' +
               ' LEFT JOIN TB02_MATERIA M on A.CO_MAT = M.CO_MAT ' +
               ' LEFT JOIN TB107_CADMATERIAS CM on M.ID_MATERIA = CM.ID_MATERIA ' +
               ' LEFT JOIN TB03_COLABOR CO on A.CO_COL = CO.CO_COL AND CO.CO_EMP = A.CO_EMP ' +
               ' JOIN TB73_TIPO_AVAL TA on A.CO_TIPO_AVAL =  TA.CO_TIPO_AVAL ' +
               ' LEFT JOIN TB70_ITEM_AVAL IA on A.CO_TIPO_AVAL =  IA.CO_TIPO_AVAL AND IA.CO_EMP = A.CO_EMP AND A.CO_PESQ_AVAL = IA.CO_PESQ_AVAL ' +
               ' WHERE A.CO_EMP = ' + strP_CO_EMP +
               ' AND (A.DT_AVAL BETWEEN ' + quotedStr(strP_DT_INI) + ' AND ' + QuotedStr(strP_DT_FIM) + ')';

      if strP_CO_TIPO_AVAL <> nil then
        SqlString := SqlString + ' And TA.CO_TIPO_AVAL = ' + strP_CO_TIPO_AVAL;

      if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' And A.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_TUR <> 'T' then
        SqlString := SqlString + ' AND A.CO_TUR = ' + strP_CO_TUR;

      if strP_CO_MAT <> 'T' then
        SqlString := SqlString + ' And A.CO_MAT = ' + strP_CO_MAT;

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelAvaliacao.Create(Nil);

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

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DA LISTAGEM DE PESQUISA/AVALIAÇÃO
// STATUS: OK
function DLLRelListagemProva(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_CO_ANO_MES_MAT, strP_CO_BIMESTRE: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelListagemProva;
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

      SqlString := ' SELECT DISTINCT(MM.CO_ALU_CAD),                    '+
               '        A.NO_ALU,                                       '+
               '        N.VL_NOTA,                                      '+
               '        C.PE_CERT_CUR,                                  '+
               '        C.QT_AULA_CUR,                                  '+
               '        FALTA = SUM( CASE F.CO_FLAG_FREQ_ALUNO          '+
               '                      WHEN '+ '''' +'N'+ '''' +' THEN 1 '+
               '                             ELSE 0                     '+
               '                        END),                           '+
               '        C.PE_FALT_CUR                                   '+
               ' FROM TB49_NOTA_ATIV_ALUNO N ' +
               ' JOIN TB07_ALUNO A on N.CO_ALU = A.CO_ALU and N.CO_EMP_ALU = A.CO_EMP ' +
               ' JOIN TB08_MATRCUR MM on N.CO_CUR = MM.CO_CUR AND N.CO_MODU_CUR = MM.CO_MODU_CUR ' +
               ' AND N.CO_ALU = MM.CO_ALU ' +
               ' JOIN TB02_MATERIA MA on N.CO_EMP_MAT = MA.CO_EMP AND N.CO_CUR = MA.CO_CUR AND N.CO_MODU_CUR = MA.CO_MODU_CUR ' +
               ' AND N.ID_MATERIA = MA.ID_MATERIA ' +
               ' JOIN TB01_CURSO C on N.CO_EMP_CUR = C.CO_EMP AND N.CO_CUR = C.CO_CUR ' +
               ' LEFT JOIN TB132_FREQ_ALU F on N.CO_EMP_ALU = F.CO_EMP_ALU ' +
               ' AND N.CO_ALU = F.CO_ALU                           AND N.CO_CUR = F.CO_CUR ' +
               ' AND N.CO_TUR = F.CO_TUR                                 AND MA.CO_MAT = F.CO_MAT ' +
               ' WHERE N.CO_CUR = ' + strP_CO_CUR +
               '   AND N.CO_TUR = ' + strP_CO_TUR  +
               '   AND MA.CO_MAT = ' + strP_CO_MAT +
               '   AND MM.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_MES_MAT) +
               '   AND N.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
               '   AND N.CO_BIMESTRE =  ' + strP_CO_BIMESTRE +
               '   AND N.CO_EMP_ALU = ' + strP_CO_EMP +
               ' GROUP BY MM.CO_ALU_CAD,                                 '+
               '        A.NO_ALU,                                       '+
               '        N.VL_NOTA,                                      '+
               '        C.QT_AULA_CUR,                                  '+
               '        C.PE_CERT_CUR,                                  '+
               '        C.PE_FALT_CUR                                   '+
               '  ORDER BY A.NO_ALU                                     ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelListagemProva.Create(Nil);

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
        Relatorio.QRLParametros.Caption := strParamRel;

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

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO CANHOTO DE PESQUISA/AVALIAÇÃO
// STATUS: OK
function DLLRelCanhoto(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_CO_ANO_MES_MAT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelCanhoto;
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

      SqlString := ' SELECT Distinct            '+
               '        M.CO_ALU_CAD,       '+
               '        A.NO_ALU,           '+
               '        M.CO_SIT_MAT as CO_SITU_MTR,      '+
               '         SITUACAO = (CASE M.CO_SIT_MAT                                   '+
               '                      WHEN '+ '''' +'A'+ '''' +' THEN '+ '''' +'Em Aberto'+ ''''+
               '                      WHEN '+ '''' +'C'+ '''' +' THEN '+ '''' +'Cancelado'+ ''''+
               '                      WHEN '+ '''' +'T'+ '''' +' THEN '+ '''' +'Trancado'+ '''' +
               '                      WHEN '+ '''' +'F'+ '''' +' THEN '+ '''' +'Finalizado'+ '''' +
               '                      WHEN '+ '''' +'X'+ '''' +' THEN '+ '''' +'Transferido'+ '''' +
               '                      WHEN '+ '''' +'D'+ '''' +' THEN '+ '''' +'Desistente'+ '''' +
               '                      ELSE '+ '''' + '''' +
               '                     END)                                                '+
               ' FROM TB48_GRADE_ALUNO G,   '+
               '      TB07_ALUNO A,         '+
               '      TB06_TURMAS T,        '+
               '      TB01_CURSO C,         '+
               '      TB08_MATRCUR M     '+
               ' WHERE G.CO_EMP = A.CO_EMP  '+
               '   AND G.CO_EMP = T.CO_EMP  '+
               '   AND G.CO_EMP = C.CO_EMP  '+
               '   AND G.CO_ALU = A.CO_ALU  '+
               '   AND G.CO_TUR = T.CO_TUR  '+
               '   AND G.CO_CUR = C.CO_CUR  '+
               '   AND G.CO_EMP = M.CO_EMP                                               '+
               '   AND G.CO_CUR = M.CO_CUR                                               '+
               '   AND G.CO_ALU = M.CO_ALU                                               '+
               '   AND G.CO_CUR = ' + strP_CO_CUR +
               '   AND G.CO_TUR = ' + strP_CO_TUR +
               '   AND G.CO_MAT = ' + strP_CO_MAT +
               '   AND G.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
               '   AND G.CO_ANO_MES_MAT = ' + strP_CO_ANO_MES_MAT +
               '   AND G.NU_SEM_LET = 1 ' +
               '   AND G.CO_EMP = ' + strP_CO_EMP+
               '  ORDER BY A.NO_ALU         ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelCanhoto.Create(Nil);

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
        Relatorio.QrlParametro.Caption := strParamRel;

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

function DLLRelBolEscAlu(strSessionID, strIdentFunc, strPathReportGenerate: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelBolEscAlu;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath : string;
  SqlString : string;
begin

  intReturn := 0;
  PDFFilt := Nil;
  Relatorio := Nil;

  Try
    Try

      FilePath := '' + strPathReportGenerate + strSessionID + '_RelBolEscAlu.pdf';
      PDFFilt := TQRPDFDocumentFilter.Create(FilePath);
      PDFFilt.AddFontMap( 'WebDings:ZapfDingBats' );
      PDFFilt.TextOnTop := true;
      PDFFilt.LeftMargin := 0;
      PDFFilt.topMargin := 0;
      PDFFilt.CompressionOn := true;
      PDFFilt.Concatenating := true;

      // Monta a Consulta do Relatório.
      SqlString := 'select alu.no_alu [aluno], alu.no_pai_alu [pai], '+
            'alu.no_mae_alu [mae], alu.nu_nis [nis], ' +
            'alu.dt_nasc_alu [datanasc], ' +
            'cast((cast((getdate() - alu.dt_nasc_alu) as integer)/365.25)as integer) [idade], ' +
            'alu.co_sexo_alu [sexo], alu.im_foto_alu [foto], ' +
            'mat.co_alu_cad [matricula], mat.co_turn_mat [turno], mat.co_ano_mes_mat [anoletivo], ' +
            'mat.co_cur [cocur], mat.co_tur [cotur],mat.co_modu_cur [comoducur], mat.co_alu [coalu], mat.co_sta_aprov [aprovado], ' +
            'cadtur.co_sigla_turma [turma], ' +
            'modu.de_modu_cur [modulo], ' +
            'cur.no_cur [serie], ' +
            'res.no_resp [responsavel], ' +
            'res.nu_cpf_resp [cpfresponsavel] ' +
            'from tb07_aluno alu ' +
            'join tb08_matrcur mat on mat.co_alu = alu.co_alu and mat.co_emp = alu.co_emp ' +
            'join tb129_cadturmas cadtur on mat.co_tur = cadtur.co_tur ' +
            'join tb44_modulo modu on mat.co_modu_cur = modu.co_modu_cur ' +
            'join tb01_curso cur on cur.co_cur = mat.co_cur ' +
            'join tb108_responsavel res on res.co_resp = alu.co_resp ' +
            'where mat.co_alu = 103 ' +
            'and mat.co_emp = 187 ' +
            'and mat.co_ano_mes_mat = 2009 ' +
            'and mat.co_cur = 16 ' +
            'and mat.co_tur = 13 ' +
            'and mat.co_modu_cur = 1';

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelBolEscAlu.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SqlString;
      Relatorio.QryRelatorio.Open;

      // Atualiza Campos do Relatório Diretamente.
      Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

      // Prepara o Relatório e Gera o PDF.
      Relatorio.QuickRep1.Prepare;
      Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);

      // Retorna 1 para o Relatório Gerado com Sucesso.
      intReturn := 1;
    Except
      on E : Exception do
        intReturn := 0;
        //ShowMessage(E.ClassName + ' error raised, with message : ' + E.Message);
    end;

  Finally
    Relatorio.QuickRep1.QRPrinter.Free;
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
  DLLRelInformacaoCurso,
  DLLRelBolEscAlu,
  DLLRelPlanRealizCentroCusto,
  DLLFrmRelPlanRealizado,
  DLLRelRelacFuncionario,
  DLLRelAniversarioFuncionario,
  DLLRelGerMapaFrequenciaFuncionario,
  DLLRelGerMapaFrequenciaProfessor,
  DLLRelExtratoFreqLivre,
  DLLRelExtratoFreqFunc,
  DLLRelAvaliacaoModelo,
  DLLRelAvaliacao,
  DLLRelListagemProva,
  DLLRelCanhoto;

end.
