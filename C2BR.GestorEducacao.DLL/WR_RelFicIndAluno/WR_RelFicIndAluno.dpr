library WR_RelFicIndAluno;

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
  U_FrmRelFicIndAluno in 'Relatorios\U_FrmRelFicIndAluno.pas' {FrmRelFicIndAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelFicIndAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_CO_ALU, strP_CO_MODU_CUR,strP_DE_MODU_CUR, strP_CO_CUR, strP_NO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CNPJ_INSTI: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelFicIndAluno;
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
               'SELECT DISTINCT n.*,a.no_alu,c.no_cur,ct.co_sigla_turma as no_tur,m.co_mat,MAT.NO_MATERIA,a.dt_nasc_alu,mm.co_alu_cad' +
               ',a.no_mae_alu,a.no_pai_alu,a.co_alu,c.co_cur,a.NU_NIS, a.NU_NIRE,c.CO_NIVEL_CUR,c.MED_FINAL_CUR,' +
               'A.DE_NACI_ALU,A.DE_NATU_ALU,A.CO_NACI_ALU,A.CO_UF_NATU_ALU,mm.CO_STA_APROV ' +
               'FROM tb079_hist_aluno n ' +
               'JOIN tb07_aluno a ON a.co_alu = n.co_alu ' +
               'JOIN tb01_curso c ON c.co_cur = n.co_cur ' +
               'JOIN tb06_turmas t ON t.co_tur = n.co_tur ' +
               'JOIN tb129_cadturmas ct ON ct.co_tur = t.co_tur and ct.co_modu_cur = t.co_modu_cur ' +
               'JOIN tb02_materia m ON m.co_mat = n.co_mat ' +
               'JOIN TB107_CADMATERIAS MAT ON MAT.ID_MATERIA = M.ID_MATERIA ' +
               'JOIN tb079_hist_aluno h ON h.co_alu = n.co_alu ' +
               'JOIN tb08_matrcur mm ON mm.co_alu = n.co_alu and n.co_cur = mm.co_cur and mm.co_ano_mes_mat = n.CO_ANO_REF ' +
               ' where n.CO_EMP = ' + strP_CO_EMP +
               ' AND a.co_emp = n.co_emp '+
               ' AND   n.CO_CUR = ' + strP_CO_CUR +
               ' AND   n.CO_TUR = '+ strP_CO_TUR      +
               ' AND n.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
               ' AND   n.CO_ANO_REF = ' + QuotedStr(strP_CO_ANO_MES_MAT);

      if strP_CO_ALU <> nil then
        SqlString := SqlString + ' AND n.CO_ALU = ' + strP_CO_ALU;

      SqlString := SqlString + ' Order by a.NO_ALU ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelFicIndAluno.Create(Nil);

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
        Relatorio.codigoEmpresa := strP_CO_EMP;

        Relatorio.AnoCurso := strP_CO_ANO_MES_MAT;
        Relatorio.NumSemestre := 1;

        Relatorio.NomeCurso := strP_NO_CUR;
        Relatorio.NomeModulo := strP_DE_MODU_CUR;

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
  DLLRelFicIndAluno;

end.
