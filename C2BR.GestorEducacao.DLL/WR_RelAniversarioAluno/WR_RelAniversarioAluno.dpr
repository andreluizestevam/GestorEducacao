library WR_RelAniversarioAluno;

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
  U_FrmRelAniversarioAluno in 'Relatorios\U_FrmRelAniversarioAluno.pas' {FrmRelAniversarioAluno},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelAniversarioAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_MES:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAniversarioAluno;
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

      SqlString := ' select month(a.dt_nasc_alu) as mes,a.nu_nis, a.nu_tele_resi_alu,a.no_alu,a.co_sexo_alu,a.dt_nasc_alu,mm.co_alu_cad,c.no_cur,C.CO_SIGL_CUR,ct.co_sigla_turma as no_tur,r.no_resp,r.nu_tele_resi_resp' +
               ' from tb07_aluno a join tb08_matrcur mm on mm.co_alu = a.co_alu' +
               ' join tb01_curso c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp ' +
               ' join tb06_turmas t on t.co_tur = mm.co_tur and t.co_emp = mm.co_emp and t.co_cur = mm.co_cur ' +
               ' join tb129_cadturmas ct on t.co_tur = ct.co_tur' +
               ' join tb108_responsavel r on r.co_resp = a.co_resp' +
               ' And mm.CO_EMP = ' + strP_CO_EMP +
               ' And year(mm.CO_ANO_MES_MAT) = ' + QuotedStr(strP_CO_ANO_MES_MAT) +
               ' AND mm.NU_SEM_LET =  ' + IntToStr(1) +
               ' and a.dt_nasc_alu is not null';

      if strP_CO_MODU_CUR <> nil then
      begin
        SQLString := SQLString + ' and mm.co_modu_cur = ' + strP_CO_MODU_CUR;
      end;

      if strP_CO_CUR <> 'T' then
      begin
        SQLString := SQLString + ' and mm.co_cur = ' + strP_CO_CUR;
      end;

      if strP_MES <> 'T' then
      begin
        SqlString := SqlString + ' And month(a.DT_NASC_ALU) = ' + strP_MES;
      end;

      if strP_CO_TUR <> 'T' then
      begin
        SQLString := SQLString + ' and mm.co_tur = ' + strP_CO_TUR;
      end;

      SqlString := SqlString + ' Order by month(a.dt_nasc_alu),day(a.dt_nasc_alu), a.no_alu, c.no_cur ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelAniversarioAluno.Create(Nil);

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
  DLLRelAniversarioAluno;

end.
