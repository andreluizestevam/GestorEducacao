library WR_RelAtivExtraAluno;

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
  U_FrmRelAtivExtraAluno in 'Relatorios\U_FrmRelAtivExtraAluno.pas' {FrmRelAtivExtraAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelAtivExtraAluno(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ATIV_EXTRA:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAtivExtraAluno;
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

      SqlString := 'select distinct a.no_alu, a.nu_nire, a.co_sexo_alu, a.dt_nasc_alu, mo.de_modu_cur, c.no_cur,ct.co_sigla_turma [no_turma],ata.DT_CAD_ATIV,ata.QT_MES_ATIV,'+
                   'ae.DES_ATIV_EXTRA from tb08_matrcur mm ' +
                   ' join tb07_aluno a on a.co_alu = mm.co_alu'+
                   ' join tb106_ativextra_aluno ata on ata.co_alu = a.co_alu'+
                   ' join tb105_atividades_extras ae on ae.CO_ATIV_EXTRA = ata.CO_ATIV_EXTRA'+
                   ' join tb44_modulo mo on mo.co_modu_cur = mm.co_modu_cur'+
                   ' join tb01_curso c on c.co_cur = mm.co_cur and c.co_emp = mm.co_emp'+
                   ' join tb06_turmas t on t.co_tur = mm.co_tur and t.co_cur = mm.co_cur and t.co_emp = mm.co_emp'+
                   ' join tb129_cadturmas ct on t.co_tur = ct.co_tur'+
                   ' where mm.co_emp = ' + strP_CO_EMP +
                   ' and mm.co_ano_mes_mat = ' + strP_CO_ANO_REF;

      if strP_CO_MODU_CUR <> 'T' then
        SQLString := SQLString +  ' and mm.co_modu_cur = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SQLString := SQLString +  ' and mm.co_modu_cur = ' + strP_CO_CUR;

      if strP_CO_TUR <> 'T' then
        SQLString := SQLString +  ' and mm.co_modu_cur = ' + strP_CO_TUR;

      if strP_CO_ATIV_EXTRA <> 'T' then
        SQLString := SQLString +  ' and ata.CO_ATIV_EXTRA = ' + strP_CO_ATIV_EXTRA;

      SQLString := SQLString + ' order by ae.des_ativ_extra, a.no_alu';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelAtivExtraAluno.Create(Nil);

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

exports
  DllGetClassObject,
  DllCanUnloadNow,
  DllRegisterServer,
  DllUnregisterServer,
  
  //Relatórios
  DLLRelAtivExtraAluno;

end.
