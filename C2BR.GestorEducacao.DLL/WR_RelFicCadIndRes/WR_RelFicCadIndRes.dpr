library WR_RelFicCadIndRes;

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
  U_FrmRelFicCadIndRes in 'Relatorios\U_FrmRelFicCadIndRes.pas' {FrmRelFicCadIndRes};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelFicCadIndRes(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_CO_RESP, strP_CNPJ_INSTI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelFicCadIndRes;
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

      SqlString := ' select distinct(r.co_resp),r.no_resp,r.co_sexo_resp,r.dt_nasc_resp,CID.NO_CIDADE,BAI.NO_BAIRRO,r.nu_cpf_resp,r.de_grau_paren,r.nu_tele_resi_resp,r.des_email_resp, ' +
               ' grau.no_inst as GRAUINSTRUCAO , '+
               ' PARENTESCO = (CASE R.DE_GRAU_PAREN '+
               '				 WHEN ' + QuotedStr('PM') + ' THEN ' + QuotedStr('Pai/Mãe') +
               '				 WHEN ' + QuotedStr('TI') + ' THEN ' + QuotedStr('Tio(a)') +
               '				 WHEN ' + QuotedStr('AV') + ' THEN ' + QuotedStr('Avô/Avó') +
               '				 WHEN ' + QuotedStr('PR') + ' THEN ' + QuotedStr('Primo(a)') +
               '				 WHEN ' + QuotedStr('CN') + ' THEN ' + QuotedStr('Cunhado(a)') +
               '				 WHEN ' + QuotedStr('TU') + ' THEN ' + QuotedStr('Tutor(a)') +
               '				 WHEN ' + QuotedStr('IR') + ' THEN ' + QuotedStr('Irmão(ã)') +
               '				 WHEN ' + QuotedStr('OU') + ' THEN ' + QuotedStr('Outros') +
               '				 END), '+
               'r.co_rg_resp,r.co_org_rg_resp,r.co_esta_rg_resp,r.de_ende_resp,r.nu_ende_resp,r.de_comp_resp,r.co_esta_resp,r.co_cep_resp,r.nu_tele_celu_resp,a.co_emp,'+
               'r.no_empr_resp,r.no_setor_resp,r.no_funcao_resp,r.des_email_emp,r.nu_tele_come_resp,a.no_alu,a.dt_nasc_alu,a.tp_def,a.nu_nis,a.co_sexo_alu,e.sigla,a.co_alu'+
               ' from TB108_responsavel r '+
               ' left join tb07_aluno a on a.co_RESP = r.co_RESP '+
               ' left join tb18_grauins grau on grau.co_inst = r.co_inst ' +
               ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = R.CO_BAIRRO '+
               ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = R.CO_CIDADE '+
               ' left join tb25_empresa e on a.co_emp = e.co_emp'+
               ' where r.co_resp = ' + strP_CO_RESP +
               ' order by a.no_alu';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelFicCadIndRes.Create(Nil);

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
  DLLRelFicCadIndRes;

end.
