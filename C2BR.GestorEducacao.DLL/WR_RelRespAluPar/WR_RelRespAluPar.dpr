library WR_RelRespAluPar;

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
  U_FrmRelRespAluPar in 'Relatorios\U_FrmRelRespAluPar.pas' {FrmRelRespAluPar};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelRespAluPar(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strP_CO_MODU_CUR,
                          strP_CO_CUR, strP_CO_TUR, strP_CO_GRAU_INST, strP_DE_GRAU_PAREN, strP_CNPJ_INSTI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRespAluPar;
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

      SqlString := ' select distinct(r.co_resp),r.no_resp,r.co_sexo_resp,r.dt_nasc_resp,CID.NO_CIDADE,BAI.NO_BAIRRO,r.nu_cpf_resp,r.nu_tele_resi_resp,r.des_email_resp, ' +
               'r.nu_tele_celu_resp,r.nu_tele_come_resp,'+
               ' gi.no_inst as GRAUINSTRUCAO, '+
               ' PARENTESCO = (CASE a.CO_GRAU_PAREN_RESP '+
               '				 WHEN ' + QuotedStr('PM') + ' THEN ' + QuotedStr('Pai/Mãe') +
               '				 WHEN ' + QuotedStr('TI') + ' THEN ' + QuotedStr('Tio(a)') +
               '				 WHEN ' + QuotedStr('AV') + ' THEN ' + QuotedStr('Avô/Avó') +
               '				 WHEN ' + QuotedStr('PR') + ' THEN ' + QuotedStr('Primo(a)') +
               '				 WHEN ' + QuotedStr('CN') + ' THEN ' + QuotedStr('Cunhado(a)') +
               '				 WHEN ' + QuotedStr('TU') + ' THEN ' + QuotedStr('Tutor(a)') +
               '				 WHEN ' + QuotedStr('IR') + ' THEN ' + QuotedStr('Irmão(ã)') +
               '				 WHEN ' + QuotedStr('OU') + ' THEN ' + QuotedStr('Outros') +
               '				 END) '+
               ' from TB108_responsavel r '+
               ' left join tb07_aluno a on a.co_resp = r.co_resp '+
               ' left join tb08_matrcur mm on mm.co_alu = a.co_alu and mm.co_emp = a.co_emp '+
               ' left join tb01_curso c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp '+
               ' left join tb06_turmas t on t.co_tur = mm.co_tur and mm.co_emp = c.co_emp '+
               ' left join tb18_grauins gi on gi.co_inst = r.co_inst ' +
               ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = R.CO_BAIRRO '+
               ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = R.CO_CIDADE '+
               ' where a.co_emp = ' + strP_CO_EMP;

      if strP_UF <> nil then
      begin
        SqlString := SqlString + ' and r.co_esta_resp = ' + quotedStr(strP_UF);
      end;

      if strP_CO_CIDADE <> 'T' then
      begin
        SQLString := SQLString + ' and r.co_cidade = ' + strP_CO_CIDADE;
      end;

      if strP_CO_BAIRRO <> 'T' then
      begin
        SQLString := SQLString + ' and r.co_bairro = ' + strP_CO_BAIRRO;
      end;

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

      if strP_CO_GRAU_INST <> 'T' then
        SQLString := SQLString + ' and r.CO_INST = ' + QuotedStr(strP_CO_GRAU_INST);

      if strP_DE_GRAU_PAREN <> 'T' then
        SQLString := SQLString + ' and a.CO_GRAU_PAREN_RESP = ' + QuotedStr(strP_DE_GRAU_PAREN);

      SQLString := SQLString + ' order by NO_RESP';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRespAluPar.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      DM.Conn.ConnectionString := retornaConexao(strP_CNPJ_INSTI);
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
        
        Relatorio.QrlTotal.Caption := intToStr(Relatorio.QryRelatorio.RecordCount);

        if strP_CO_TUR <> 'T' then
        begin
          Relatorio.CodigoTurma := strP_CO_TUR;
        end
        else
          Relatorio.CodigoTurma := '';

        if strP_CO_MODU_CUR <> 'T' then
        begin
          Relatorio.CodigoModulo := strP_CO_MODU_CUR;
        end
        else
          Relatorio.CodigoModulo := '';

        if strP_CO_CUR <> 'T' then
        begin
          Relatorio.CodigoCurso := strP_CO_CUR;
        end
        else
          Relatorio.CodigoCurso := '';

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.codigoEmpresa := strP_CO_EMP;
        Relatorio.QrlParam.Caption := strParamRel;

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
  DLLRelRespAluPar;

end.
