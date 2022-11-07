library WR_RelAluPasEsc;

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
  U_FrmRelAluPasEsc in 'Relatorios\U_FrmRelAluPasEsc.pas' {FrmRelAluPasEsc};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelAluPasEsc(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_LIN_ONI, strP_CNPJ_INST:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAluPasEsc;
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

      SQLString := ' select a.*,b.no_bairro,e.coduf,ci.no_cidade,c.no_cur,C.CO_SIGL_CUR,tu.co_sigla_turma [no_tur],mm.co_alu_cad,r.no_resp,r.nu_tele_celu_resp from tb07_aluno a ' +
                '  left join tb74_UF e on e.coduf = a.co_esta_alu ' +
                '  join tb08_matrcur mm on mm.co_alu = a.co_alu and mm.co_emp = a.co_emp ' +
                '  join tb01_curso c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp ' +
                '  join tb108_responsavel r on r.co_resp = a.co_resp and mm.co_emp = c.co_emp ' +
                '  join tb06_turmas t on t.co_tur = mm.co_tur and mm.co_emp = t.co_emp ' +
                '  join tb129_cadturmas tu on tu.co_tur = mm.co_tur and mm.co_emp = c.co_emp ' +
                '  left join tb905_bairro b on b.co_bairro = a.co_bairro and b.co_cidade = a.co_cidade and b.co_uf = a.co_esta_alu ' +
                '  left join tb904_cidade ci on ci.co_cidade = a.co_cidade and b.co_uf = a.co_esta_alu ' +
                '  where a.co_emp = ' + strP_CO_EMP +
                '  and mm.co_ano_mes_mat = ' + QuotedStr(strP_CO_ANO_MES_MAT) +
                '  and mm.co_sit_mat = '+ QuotedStr('A');

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

        SQLString := SQLString + ' order by a.no_alu';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelAluPasEsc.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
    {  if strP_CNPJ_INST = '11489849000133' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=10.0.88.2\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      else begin }
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BARAORIOBRANCO;Data Source=10.0.88.2\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
    //  end;
      DM.Conn.Open;
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

        Relatorio.QrlParametros.Caption := strParamRel;
        //'Ano referência: ' + edtAno.Text + ' - Módulo: ' + cbModulo.Text + ' - ' + Sys_DescricaoTipoCurso + ': ' + cbSerie.Text +
        //' - Turma: ' + cbTurma.Text + ' - Linha de ônibus: ' + cbLinOni.Text;

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
  DLLRelAluPasEsc;

end.
