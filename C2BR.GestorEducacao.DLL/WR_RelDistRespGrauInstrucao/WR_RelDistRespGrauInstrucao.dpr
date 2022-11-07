library WR_RelDistRespGrauInstrucao;

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
  U_FrmRelDistRespGrauInstrucao in 'Relatorios\U_FrmRelDistRespGrauInstrucao.pas' {FrmRelDistRespGrauInstrucao},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelDistRespGrauInstrucao(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR,
                          strP_CO_CUR, strP_CO_TUR, strP_CO_GRAU_INST, strP_TP_DEF:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDistRespGrauInstrucao;
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

      SQLString := 'select distinct r.*,a.no_alu,a.tp_def , a.co_alu ,a.nu_cpf_alu,a.dt_nasc_alu,a.co_sexo_alu, ' +
               ' CASE a.co_grau_paren_resp ' +
               '    WHEN ' + QuotedStr('PM') + ' THEN ' + QuotedStr('Pai/Mãe') +
               '    WHEN ' + QuotedStr('TI') + ' THEN ' + QuotedStr('Tio(a)') +
               '    WHEN ' + QuotedStr('AV') + ' THEN ' + QuotedStr('Avô/Avó') +
               '    WHEN ' + QuotedStr('PR') + ' THEN ' + QuotedStr('Primo(a)') +
               '    WHEN ' + QuotedStr('CN') + ' THEN ' + QuotedStr('Cunhado(a)') +
               '    WHEN ' + QuotedStr('TU') + ' THEN ' + QuotedStr('Tutor(a)') +
               '    WHEN ' + QuotedStr('IR') + ' THEN ' + QuotedStr('Irmão(ã)') +
               '    WHEN ' + QuotedStr('OU') + ' THEN ' + QuotedStr('Outros') +
               ' END AS GrauParen, ' +
               'c.no_cur,C.CO_SIGL_CUR,tu.CO_SIGLA_TURMA,mm.co_alu_cad as MATRICULA, gi.NO_INST, ' +
               '(select count(Distinct(r1.nu_cpf_resp)) from TB108_responsavel r1  ' +
               ' join tb07_aluno a on a.co_RESP = r1.co_RESP ' +
               ' join tb08_matrcur mm on mm.co_alu = a.co_alu and mm.co_emp = a.co_emp ' +
               ' join tb01_curso c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp ' +
               ' join tb06_turmas t on t.co_tur = mm.co_tur and mm.co_emp = c.co_emp ' +
               ' join tb129_cadturmas tu on tu.co_tur = t.co_tur and tu.co_modu_cur = t.co_modu_cur ' +
               ' where a.co_emp =  ' + strP_CO_EMP +
					     ' and mm.co_sit_mat = '+ QuotedStr('A') +
               ' and r1.CO_INST = r.CO_INST ';

      if strP_CO_MODU_CUR <> nil then
        SqlString := SqlString + ' AND MM.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' AND MM.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_TUR <> 'T' then
      begin
        SQLString := SQLString + ' and mm.co_tur = ' + strP_CO_TUR;
      end;

      if strP_CO_GRAU_INST <> 'T'  then
        SQLString := SQLString + ' and r.CO_INST = ' + strP_CO_GRAU_INST;

      if strP_TP_DEF <> 'T' then
        SQLString := SQLString + ' and a.TP_DEF = ' + QuotedStr(strP_TP_DEF) +  ' or a.TP_DEF is Null ';

      //FIM DO TESTE

       SQLString := SQLString + ' )TotResp ' +
       'from tb108_responsavel r ' +
       'join tb07_aluno a on a.co_resp = r.co_resp ' +
       'join tb08_matrcur mm on mm.co_alu = a.co_alu and mm.co_emp = a.co_emp '+
       'join tb01_curso c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp '+
       'join tb06_turmas t on t.co_tur = mm.co_tur and mm.co_emp = c.co_emp '+
       'join tb129_cadturmas tu on tu.co_tur = mm.co_tur and mm.co_emp = c.co_emp '+
       'left join tb18_grauins gi on gi.co_inst = r.co_inst '+
       'where a.co_emp = ' + strP_CO_EMP +
       ' and mm.co_sit_mat = '+ QuotedStr('A');

      if strP_CO_MODU_CUR <> nil then
        SqlString := SqlString + ' AND MM.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' AND MM.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_TUR <> 'T' then
      begin
        SQLString := SQLString + ' and mm.co_tur = ' + strP_CO_TUR;
      end;

      if strP_CO_GRAU_INST <> 'T' then
        SQLString := SQLString + ' and r.CO_INST = ' + strP_CO_GRAU_INST;

      if strP_TP_DEF <> 'T' then
        SQLString := SQLString + ' and a.TP_DEF = ' + QuotedStr(strP_TP_DEF) +  ' or a.TP_DEF is Null ';

      SQLString := SQLString + ' order by NO_RESP, gi.NO_INST';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDistRespGrauInstrucao.Create(Nil);

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

exports
  DllGetClassObject,
  DllCanUnloadNow,
  DllRegisterServer,
  DllUnregisterServer,
  
  //Relatórios
  DLLRelDistRespGrauInstrucao;

end.
