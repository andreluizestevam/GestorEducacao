library WR_RelAluPorInsEsp;

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
  U_FrmRelAluPorInsEsp in 'Relatorios\U_FrmRelAluPorInsEsp.pas' {FrmRelAluPorInsEsp};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelAluPorInsEsp(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_INST_ESP, strP_CO_ANO_MES_MAT:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAluPorInsEsp;
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

      SQLString := 'select c.co_cur,mo.de_modu_cur,c.no_cur, a.nu_nis, a.no_alu,a.nu_tele_resi_alu,a.de_ende_alu,a.nu_ende_alu,a.tp_def,a.de_comp_alu,a.co_sexo_alu,mm.co_alu_cad,a.dt_nasc_alu,'+
               'ie.co_inst_esp,ie.no_inst_esp,res.no_resp,res.nu_tele_celu_resp,ct.co_sigla_turma as no_tur,b.no_bairro, CID.NO_CIDADE'+
               ' from tb165_alu_inst_esp rie '+
               'join tb07_aluno a on a.co_alu = rie.co_alu ' +
               'join tb164_inst_esp ie on ie.co_inst_esp = rie.co_inst_esp '+
               'join tb08_matrcur mm on mm.co_alu = rie.co_alu '+
               'join tb01_curso c on c.co_cur = mm.co_cur '+
               'join tb06_turmas t on t.co_tur = mm.co_tur '+
               'join tb129_cadturmas ct on t.co_tur = ct.co_tur '+
               'join tb44_modulo mo on mo.co_modu_cur = mm.co_modu_cur ' +
               'join tb905_bairro b on b.co_bairro = a.co_bairro '+
               'JOIN TB904_CIDADE CID ON CID.CO_CIDADE = A.CO_CIDADE '+
               'join tb108_responsavel res on res.co_resp = a.co_resp '+
               'where mm.co_emp = ' + strP_CO_EMP +
               ' and mm.co_ano_mes_mat = ' + strP_CO_ANO_MES_MAT +
               ' and mm.co_sit_mat = ' + quotedStr('A');

      if strP_CO_MODU_CUR <> nil then
      begin
        SqlString := SqlString + ' and mm.co_modu_cur = ' + strP_CO_MODU_CUR;
      end;

      if strP_CO_CUR <> 'T' then
      begin
        SqlString := SqlString + ' and mm.co_cur = ' + strP_CO_CUR;
      end;

      if strP_CO_TUR <> 'T' then
      begin
        SqlString := SqlString + ' and mm.co_tur = ' + strP_CO_TUR;
      end;

      if strP_CO_INST_ESP <> 'T' then
      begin
        SqlString := SqlString + ' and rie.co_inst_esp = ' + strP_CO_INST_ESP;
      end;

      SqlString := SqlString + ' group by a.nu_nis,cid.no_cidade,c.co_cur,c.no_cur,a.no_alu,a.co_sexo_alu,a.de_ende_alu,a.tp_def,'+
                             'a.de_comp_alu,a.nu_ende_alu,a.dt_nasc_alu,mm.co_alu_cad,ie.no_inst_esp,ie.co_inst_esp,a.nu_tele_resi_alu,res.no_resp,res.nu_tele_celu_resp,ct.co_sigla_turma,b.no_bairro,mo.de_modu_cur '+
                             ' ORDER BY ie.no_inst_esp,A.NO_ALU ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelAluPorInsEsp.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SQLString;
      //ShowMessage(SQLString);
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
  DLLRelAluPorInsEsp;

end.
