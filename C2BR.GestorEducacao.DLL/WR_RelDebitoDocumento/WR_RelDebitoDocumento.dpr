library WR_RelDebitoDocumento;

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
  U_FrmRelDebitoDocumento in 'Relatorios\U_FrmRelDebitoDocumento.pas' {FrmRelDebitoDocumento};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelDebitoDocumento(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CNPJ_INSTI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDebitoDocumento;
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

      {SqlString := 'SET LANGUAGE PORTUGUESE ' +
                   'SELECT DISTINCT ALU.CO_ALU,MM.CO_ALU_CAD,ALU.NU_NIS,ALU.NO_ALU,I.DE_TP_DOC_MAT,C.NO_CUR,CT.NO_TURMA [NO_TUR]' +
                   ' FROM TB120_DOC_ALUNO_ENT D,' +
                   ' TB121_TIPO_DOC_MATRICULA I,' +
                   ' TB01_CURSO C, TB08_MATRCUR MM, TB07_ALUNO ALU,TB06_TURMAS T, tb129_cadturmas ct' +
                   ' WHERE ALU.CO_EMP *= D.CO_EMP' +
                   '  AND  ALU.CO_ALU *= D.CO_ALU' +
                   ' AND  ALU.CO_EMP = C.CO_EMP' +
                   '  AND ALU.CO_ALU = MM.CO_ALU' +
                   ' AND ALU.CO_EMP = MM.CO_EMP' +
                   '  AND  MM.CO_CUR = C.CO_CUR' +
                   '  AND  MM.CO_CUR = T.CO_CUR' +
                   ' AND MM.CO_TUR = T.CO_TUR' +
                   '  AND MM.CO_EMP = T.CO_EMP' +
                   '  AND MM.CO_SIT_MAT  = ' + quotedStr('A') +
                   '  AND  D.CO_TP_DOC_MAT =* I.CO_TP_DOC_MAT ' +
                   ' AND T.CO_TUR = CT.CO_TUR' +
                   '  AND ALU.CO_EMP = ' + strP_CO_EMP +
                   '  AND MM.CO_ANO_MES_MAT = ' + strP_CO_ANO_REF +
                   ' AND  I.CO_TP_DOC_MAT NOT IN( SELECT Z.CO_TP_DOC_MAT ' +
                                                'FROM TB120_DOC_ALUNO_ENT Z ' +
                                                'WHERE  ALU.CO_EMP = Z.CO_EMP ' +
                                                'AND    ALU.CO_ALU = Z.CO_ALU )'; }
      SqlString := 'SET LANGUAGE PORTUGUESE ' +
                   'SELECT DISTINCT ALU.CO_ALU,MM.CO_ALU_CAD,ALU.NU_NIS,ALU.NO_ALU,I.DE_TP_DOC_MAT,C.NO_CUR,CT.NO_TURMA [NO_TUR]' +
                   ' FROM TB120_DOC_ALUNO_ENT D, TB208_CURSO_DOCTOS CD,' +
                   ' TB121_TIPO_DOC_MATRICULA I,' +
                   ' TB01_CURSO C, TB08_MATRCUR MM, TB07_ALUNO ALU,TB06_TURMAS T, tb129_cadturmas ct' +
                   ' WHERE ALU.CO_EMP *= D.CO_EMP' +
                   '  AND  ALU.CO_ALU *= D.CO_ALU' +
                   ' AND  ALU.CO_EMP = C.CO_EMP' +
                   '  AND ALU.CO_ALU = MM.CO_ALU' +
                   ' AND ALU.CO_EMP = MM.CO_EMP' +
                   '  AND  MM.CO_CUR = C.CO_CUR' +
                   '  AND  MM.CO_CUR = T.CO_CUR' +
                   ' AND MM.CO_TUR = T.CO_TUR' +
                   '  AND MM.CO_EMP = T.CO_EMP' +
                   '  AND MM.CO_SIT_MAT  = ' + quotedStr('A') +
                   '  AND  D.CO_TP_DOC_MAT =* I.CO_TP_DOC_MAT ' +
                   ' AND T.CO_TUR = CT.CO_TUR' +
                   '  AND ALU.CO_EMP = ' + strP_CO_EMP +
                   '  AND MM.CO_ANO_MES_MAT = ' + strP_CO_ANO_REF +
                   ' AND I.CO_TP_DOC_MAT = CD.CO_TP_DOC_MAT ' +
                   ' AND CD.CO_MODU_CUR = MM.CO_MODU_CUR ' +
                   ' AND CD.CO_CUR = MM.CO_CUR ' +
                   ' AND CD.CO_EMP = MM.CO_EMP ' +
                   ' AND CD.CO_TP_DOC_MAT NOT IN( SELECT Z.CO_TP_DOC_MAT ' +
												                       ' FROM TB120_DOC_ALUNO_ENT Z ' +
                                               ' WHERE  ALU.CO_EMP = Z.CO_EMP ' +
                                               ' AND    ALU.CO_ALU = Z.CO_ALU)';

      if strP_CO_MODU_CUR <> 'T' then
      begin
        SQLString := SQLString + ' and mm.co_modu_cur = ' + strP_CO_MODU_CUR;
      end;

      if strP_CO_CUR <> 'T' then
      begin
        SQLString := SQLString + ' and mm.co_cur = ' + strP_CO_CUR;
      end;

      if strP_CO_TUR <> 'T' then
      begin
        SQLString := SQLString + ' and mm.co_tur = ' + strP_CO_TUR;
      end;

      if strP_CO_ALU <> 'T' then
        SqlString := SqlString + ' and alu.co_alu = ' + strP_CO_ALU;

      SqlString := SqlString +
                   '  order by ALU.NO_ALU,I.DE_TP_DOC_MAT ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDebitoDocumento.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      //Barão do Rio Branco
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
      //Colégio Esplanada
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
      //Conexão Aquarela
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

exports
  DllGetClassObject,
  DllCanUnloadNow,
  DllRegisterServer,
  DllUnregisterServer,
  
  //Relatórios
  DLLRelDebitoDocumento;

end.
