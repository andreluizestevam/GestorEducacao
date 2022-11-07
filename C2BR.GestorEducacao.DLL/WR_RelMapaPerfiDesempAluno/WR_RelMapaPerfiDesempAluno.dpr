library WR_RelMapaPerfiDesempAluno;

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
  U_Funcoes in '..\General\U_Funcoes.pas',
  U_FrmRelMapaPerfiDesempAluno in 'Relatorios\U_FrmRelMapaPerfiDesempAluno.pas' {FrmRelMapaPerfiDesempAluno};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelMapaPerfiDesempAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_ANO_REF, strP_CO_MODU_CUR,
                                    strP_CO_CUR, strP_CO_TUR, strP_ID_MATERIA, strP_NOTA_MIN, strP_NOTA_MAX:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaPerfiDesempAluno;
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

      SQLString := ' select distinct upd.*, e.sigla, m.CO_SIGLA_MODU_CUR, c.CO_SIGL_CUR, ct.CO_SIGLA_TURMA, cm.NO_SIGLA_MATERIA,'+
                   '(isnull(upd.NR_MEDIA_BIM1_DESEMP,0) + isnull(upd.NR_MEDIA_BIM2_DESEMP,0) + isnull(upd.NR_MEDIA_BIM3_DESEMP,0) + isnull(upd.NR_MEDIA_BIM4_DESEMP,0))/4 as media ' +
                   ' from TB247_UNIDADE_PERFIL_DESEMPENHO upd ' +
                   ' join TB25_empresa e on e.CO_EMP = upd.CO_EMP ' +
                   ' join tb44_modulo m on m.co_modu_cur = upd.CO_MOD ' +
                   ' join tb01_curso c on c.co_emp = upd.co_emp and c.co_modu_cur = upd.CO_MOD and c.co_cur = upd.co_cur ' +
                   ' join tb06_turmas t on t.co_emp = upd.co_emp and t.co_modu_cur = upd.CO_MOD and t.co_cur = upd.co_cur and t.co_tur = upd.co_tur ' +
                   ' join tb129_cadturmas ct on ct.co_tur = t.co_tur ' +
                   ' join tb107_cadmaterias cm on cm.co_emp = upd.co_emp and cm.id_materia = upd.id_materia ' +
                   ' where 1 = 1 ';

      if strP_CO_EMP_REF <> 'T' then
      begin
        SQLString := SQLString + ' and upd.CO_EMP = ' + strP_CO_EMP_REF;
      end;

      if strP_CO_ANO_REF <> 'T' then
      begin
        SQLString := SQLString + ' and upd.NR_ANO = ' + strP_CO_ANO_REF;
      end;

      if strP_CO_MODU_CUR <> 'T' then
      begin
        SQLString := SQLString + ' and upd.CO_MOD = ' + strP_CO_MODU_CUR;
      end;

      if strP_CO_CUR <> 'T' then
      begin
        SQLString := SQLString + ' and upd.CO_CUR = ' + strP_CO_CUR;
      end;

      if strP_CO_TUR <> 'T' then
      begin
        SQLString := SQLString + ' and upd.CO_TUR = ' + strP_CO_TUR;
      end;

      if strP_ID_MATERIA <> 'T' then
      begin
        SQLString := SQLString + ' and upd.ID_MATERIA = ' + strP_ID_MATERIA;
      end;

      if strP_NOTA_MIN <> 'T' then
      begin
        SQLString := SQLString + ' and (isnull(upd.NR_MEDIA_BIM1_DESEMP,0) + isnull(upd.NR_MEDIA_BIM2_DESEMP,0) + isnull(upd.NR_MEDIA_BIM3_DESEMP,0) + isnull(upd.NR_MEDIA_BIM4_DESEMP,0))/4  >= ' + strP_NOTA_MIN;
      end;

      if strP_NOTA_MAX <> 'T' then
      begin
        SQLString := SQLString + ' and (isnull(upd.NR_MEDIA_BIM1_DESEMP,0) + isnull(upd.NR_MEDIA_BIM2_DESEMP,0) + isnull(upd.NR_MEDIA_BIM3_DESEMP,0) + isnull(upd.NR_MEDIA_BIM4_DESEMP,0))/4 <= ' + strP_NOTA_MAX;
      end;

      SQLString := SQLString + ' order by e.sigla, upd.NR_ANO, m.CO_SIGLA_MODU_CUR, c.CO_SIGL_CUR, ct.CO_SIGLA_TURMA, cm.NO_SIGLA_MATERIA';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelMapaPerfiDesempAluno.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
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
        // Atualiza Campos do Relat�rio Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        
        if strP_CO_EMP_REF <> 'T' then
          Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('sigla').AsString
        else
          Relatorio.QRLParam.Caption := '( Unidade: Todas';

        if strP_CO_ANO_REF <> 'T' then
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Ano Refer�ncia: ' + strP_CO_ANO_REF + ' )'
        else
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Ano Refer�ncia: Todas';

        if strP_CO_MODU_CUR <> 'T' then
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Modalidade: ' + Relatorio.QryRelatorio.FieldByName('CO_SIGLA_MODU_CUR').AsString + ' )'
        else
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Modalidade: Todas';

        if strP_CO_CUR <> 'T' then
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - S�rie: ' + Relatorio.QryRelatorio.FieldByName('CO_SIGL_CUR').AsString + ' )'
        else
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - S�rie: Todas';

        if strP_CO_TUR <> 'T' then
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Turma: ' + Relatorio.QryRelatorio.FieldByName('CO_SIGLA_TURMA').AsString + ' )'
        else
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Turma: Todas';

        if strP_ID_MATERIA <> 'T' then
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Disciplina: ' + Relatorio.QryRelatorio.FieldByName('NO_SIGLA_MATERIA').AsString + ' )'
        else
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Disciplina: Todas )';

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relat�rio Gerado com Sucesso.
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
  
  //Relat�rios
  DLLRelMapaPerfiDesempAluno;

end.