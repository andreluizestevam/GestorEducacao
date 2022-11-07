library WR_RelMapaDistResp;

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
  U_FrmRelMapaDistResp in 'Relatorios\U_FrmRelMapaDistResp.pas' {FrmRelMapaDistResp};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaDistResp(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR :PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaDistResp;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString: string;
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

      SQLString :=  ' select  ' +
                    '(Select Count(QTDALU.CO_ALU)                              ' +
                    '    From TB08_matrcur QTDALU                              ' +
                    '    Where QTDALU.CO_CUR = c.CO_CUR                        ' +
                    '  and QTDALU.CO_TUR = t.CO_TUR                            ' +
                    '  and QTDALU.CO_MODU_CUR = mo.CO_MODU_CUR                            ' +
                    ' and QTDALU.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    ' and QTDALU.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    ' ) QTDALU,                                                ' +
                    ' (Select Count(distinct(QTDRESP.CO_RESP))                 ' +
                    '     From TB07_ALUNO QTDRESP                              ' +
                    '   join TB08_matrcur mm on mm.co_alu = QTDRESP.co_alu     ' +
                    '     Where mm.CO_CUR = c.CO_CUR                           ' +
                    '        AND mm.CO_TUR = t.CO_TUR                          ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '    and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDRESP,                                             ' +
                    ' (Select Count(distinct(FEMININO.CO_RESP))                          ' +
                    '     From TB07_ALUNO FEMININO                             ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = FEMININO.CO_RESP  ' +
                    ' join TB08_matrcur mm on mm.co_alu = FEMININO.co_alu             ' +
                    '     Where resp.CO_SEXO_RESP = ' + QuotedStr('F')                  +
                    '		    AND mm.CO_CUR = c.CO_CUR                                  ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                  ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    '    and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   ) FEMININO,                                                   ' +
                    '  (Select Count(distinct(MASCULINO.CO_RESP))                               ' +
                    '     From TB07_ALUNO MASCULINO                                   ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = MASCULINO.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = MASCULINO.co_alu            ' +
                    '    Where resp.CO_SEXO_RESP = ' + QuotedStr('M')                   +
                    '    AND mm.CO_CUR = c.CO_CUR                                     ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                  ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '      and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) MASCULINO,                                                  ' +
                    ' (Select Count(distinct(QTDPAIS.CO_RESP))                                  ' +
                    '     from TB07_ALUNO  QTDPAIS                                    ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDPAIS.CO_RESP   ' +
                    '     join TB08_matrcur mm on mm.co_alu = QTDPAIS.co_alu          ' +
                    '     Where QTDPAIS.CO_GRAU_PAREN_RESP = '+ QuotedStr('PM')                 +
                    '      AND mm.CO_CUR = c.CO_CUR                                   ' +
                    '      AND mm.CO_TUR = t.CO_TUR                                   ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDPAIS,                                                    ' +
                    ' (Select Count(distinct(QTDTIOS.CO_RESP))                                  ' +
                    '     From TB07_ALUNO QTDTIOS                                     ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDTIOS.CO_RESP   ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDTIOS.co_alu              ' +
                    '     Where QTDTIOS.CO_GRAU_PAREN_RESP =   ' + QuotedStr('TI')   +
                    '        AND mm.CO_CUR = c.CO_CUR                                 ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                  ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDTIOS,                                                    ' +
                    ' (Select Count(distinct(QTDAVOS.CO_RESP))                                  ' +
                    ' From TB07_ALUNO QTDAVOS                                         ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDAVOS.CO_RESP   ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDAVOS.co_alu              ' +
                    ' Where QTDAVOS.CO_GRAU_PAREN_RESP = ' + QuotedStr('AV')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                     ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                  ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDAVOS,                                                    ' +
                    ' (Select Count(distinct(QTDPRIMOS.CO_RESP))                                ' +
                    '     From TB07_ALUNO QTDPRIMOS                                   ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDPRIMOS.CO_RESP ' +
                    '     join TB08_matrcur mm on mm.co_alu = QTDPRIMOS.co_alu        ' +
                    '     Where QTDPRIMOS.CO_GRAU_PAREN_RESP =   ' + QuotedStr('PR')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                     ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                  ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDPRIMOS,                                                  ' +
                    '(Select Count(distinct(QTDTUTORES.CO_RESP))                                ' +
                    '     From TB07_ALUNO QTDTUTORES                                  ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDTUTORES.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDTUTORES.co_alu            ' +
                    '     Where QTDTUTORES.CO_GRAU_PAREN_RESP =   ' + QuotedStr('TU')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                      ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                   ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '  ) QTDTUTORES,                                                   ' +
                    ' (Select Count(distinct(QTDCUNHADOS.CO_RESP))                               ' +
                    '     From TB07_ALUNO QTDCUNHADOS                                  ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDCUNHADOS.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDCUNHADOS.co_alu            ' +
                    '     Where QTDCUNHADOS.CO_GRAU_PAREN_RESP = ' + QuotedStr('CN')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                       ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                    ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDCUNHADOS,                                                  ' +
                    ' (Select Count(distinct(QTDIRMAOS.CO_RESP))                                  ' +
                    '     From TB07_ALUNO QTDIRMAOS                                     ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDIRMAOS.CO_RESP   ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDIRMAOS.co_alu              ' +
                    '     Where QTDIRMAOS.CO_GRAU_PAREN_RESP =   ' + QuotedStr('IR')   +
                    '   AND mm.CO_CUR = c.CO_CUR                                        ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                    ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDIRMAOS,                                                    ' +
                    ' (Select Count(distinct(QTDOUTROS.CO_RESP))                                  ' +
                    '     From TB07_ALUNO QTDOUTROS                                     ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDOUTROS.CO_RESP   ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDOUTROS.co_alu              ' +
                    '     Where QTDOUTROS.CO_GRAU_PAREN_RESP =   ' + QuotedStr('OU')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                       ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                    ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    ' )   QTDOUTROS,                                                    ' +
                    ' (Select Count(distinct(QTDFUNDAMENTAL.CO_RESP))                             ' +
                    '     From TB07_ALUNO QTDFUNDAMENTAL                                ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDFUNDAMENTAL.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDFUNDAMENTAL.co_alu          ' +
                    ' join tb18_grauins grau on resp.co_inst = grau.co_inst ' +
                    '     Where grau.CO_CONTROL_PESQUISA = ' + QuotedStr('F')   +
                    '     AND mm.CO_CUR = c.CO_CUR                                       ' +
                    '      AND mm.CO_TUR = t.CO_TUR                                      ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '  ) QTDFUNDAMENTAL,                                                 ' +
                    ' (Select Count(distinct(QTDMEDIO.CO_RESP))                                    ' +
                    '     From TB07_ALUNO QTDMEDIO                                       ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDMEDIO.CO_RESP     ' +
                    '   join TB08_matrcur mm on mm.co_alu = QTDMEDIO.co_alu              ' +
                    ' join tb18_grauins grau on resp.co_inst = grau.co_inst ' +
                    '     Where grau.CO_CONTROL_PESQUISA = ' + QuotedStr('M')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                        ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                     ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '    ) QTDMEDIO,                                                      ' +
                    ' (Select Count(distinct(QTDESPECIALIZACAO.CO_RESP))                           ' +
                    '     From TB07_ALUNO QTDESPECIALIZACAO                              ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDESPECIALIZACAO.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDESPECIALIZACAO.co_alu       ' +
                    ' join tb18_grauins grau on resp.co_inst = grau.co_inst ' +
                    '     Where grau.CO_CONTROL_PESQUISA = ' + QuotedStr('E')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                        ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                     ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDESPECIALIZACAO,                                             ' +
                    ' (Select Count(distinct(QTDGRADUACAO.CO_RESP))                                ' +
                    '     From TB07_ALUNO QTDGRADUACAO                                   ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDGRADUACAO.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDGRADUACAO.co_alu            ' +
                    ' join tb18_grauins grau on resp.co_inst = grau.co_inst ' +
                    '     Where grau.CO_CONTROL_PESQUISA = ' + QuotedStr('G')   +
                    '   AND mm.CO_CUR = c.CO_CUR                                         ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                     ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '    ) QTDGRADUACAO,                                                  ' +
                    ' (Select Count(distinct(QTDPOSGRADUACAO.CO_RESP))                             ' +
                    '     From TB07_ALUNO QTDPOSGRADUACAO                                ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDPOSGRADUACAO.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDPOSGRADUACAO.co_alu         ' +
                    ' join tb18_grauins grau on resp.co_inst = grau.co_inst ' +
                    '     Where grau.CO_CONTROL_PESQUISA = ' + QuotedStr('P')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                        ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                     ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '    ) QTDPOSGRADUACAO,                                               ' +
                    ' (Select Count(distinct(QTDMESTRADO.CO_RESP))                                 ' +
                    '     From TB07_ALUNO QTDMESTRADO                                    ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDMESTRADO.CO_RESP  ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDMESTRADO.co_alu             ' +
                    ' join tb18_grauins grau on resp.co_inst = grau.co_inst ' +
                    '     Where grau.CO_CONTROL_PESQUISA = ' + QuotedStr('T')   +
                    '   AND mm.CO_CUR = c.CO_CUR                                         ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                     ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '   ) QTDMESTRADO,                                                    ' +
                    ' (Select Count(distinct(QTDDOUTORADO.CO_RESP))                                ' +
                    '     From TB07_ALUNO QTDDOUTORADO                                   ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDDOUTORADO.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDDOUTORADO.co_alu            ' +
                    ' join tb18_grauins grau on resp.co_inst = grau.co_inst ' +
                    '     Where grau.CO_CONTROL_PESQUISA = ' + QuotedStr('D')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                        ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                     ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    ' ) QTDDOUTORADO,                                                   ' +
                    ' (Select Count(distinct(QTDALFABETIZADO.CO_RESP))                             ' +
                    '    From TB07_ALUNO QTDALFABETIZADO                                 ' +
                    ' join TB108_RESPONSAVEL resp on resp.CO_RESP = QTDALFABETIZADO.CO_RESP ' +
                    ' join TB08_matrcur mm on mm.co_alu = QTDALFABETIZADO.co_alu         ' +
                    ' join tb18_grauins grau on resp.co_inst = grau.co_inst ' +
                    '     Where grau.CO_CONTROL_PESQUISA = ' + QuotedStr('A')   +
                    '    AND mm.CO_CUR = c.CO_CUR                                        ' +
                    '       AND mm.CO_TUR = t.CO_TUR                                     ' +
                    '        AND mm.CO_MODU_CUR = mo.CO_MODU_CUR               ' +
                    ' and mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    '   and mm.co_sit_mat not in (' + quoTedSTR('C') + ')'     +
                    '    ) QTDALFABETIZADO,                                               ' +
                    '  c.no_cur,c.co_cur,t.co_tur,tu.co_sigla_turma [no_tur],mo.co_modu_cur,mo.de_modu_cur from tb08_matrcur MM ' +
                    '  join tb01_curso c on c.co_cur = MM.co_cur                         ' +
                    '  join tb44_modulo mo on mm.co_modu_cur = mo.co_modu_cur ' +
                    '  join tb06_turmas t on t.co_tur = MM.co_tur                        ' +
                    '  join tb129_cadturmas tu on tu.co_tur = MM.co_tur                        ' +
                    '  where MM.co_emp = '  + strP_CO_EMP +
                    '  and co_sit_mat not in (' + quoTedSTR('C') + ')';

      if strP_CO_MODU_CUR <> nil then
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

      SQLString := SQLString + ' group by c.no_cur,c.co_cur,t.co_tur,tu.co_sigla_turma,mo.co_modu_cur, mo.de_modu_cur' ;
      SQLString := SQLString + ' order by c.no_cur,tu.co_sigla_turma' ;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaDistResp.Create(Nil);

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
        Relatorio.LblTituloRel.Caption := 'MAPA DE DISTRIBUIÇÃO DE RESPONSÁVEL POR CARACTERÍSTICAS - SÉRIE/TURMA';

        Relatorio.QRLParam.Caption := 'Unidade: ' + Relatorio.QryCabecalhoRel.FieldByName('NO_FANTAS_EMP').AsString +
        ' - Ano Referência: ' + strP_CO_ANO_REF + ' - Modalidade: ' + Relatorio.QryRelatorio.FieldByName('DE_MODU_CUR').AsString;

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
  DLLRelMapaDistResp;

end.
