library WR_RelPlanoAula;

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
  U_FrmRelPlanoAula in 'Relatorios\U_FrmRelPlanoAula.pas' {FrmRelPlanoAula};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelPlanoAula(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_ID_MATERIA, strP_DT_INI, strP_DT_FIM, strP_TP_ATIV, strP_CO_COL:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelPlanoAula;
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

      SqlString := ' set language portuguese '+
               ' select pla.dt_prev_pla, pla.qt_carg_hora_pla,cu.no_cur,ct.co_sigla_turma as no_turma,cm.no_materia,co.no_col,pla.de_tema_aula, '+
               ' pla.de_obje_aula,pla.fla_executada_ativ,pla.co_situ_pla,pla.HR_INI_AULA_PLA,pla.HR_FIM_AULA_PLA,pla.nu_temp_pla, pla.FLA_HOMOLOG '+
               ' from tb17_plano_aula pla ' +
               ' left join TB119_ATIV_PROF_TURMA apt on pla.co_ativ_prof_tur = apt.co_ativ_prof_tur and pla.co_emp = apt.co_emp'+
               ' join tb03_colabor co on pla.co_col = co.co_col and pla.co_emp = co.co_emp'+
               ' join tb01_curso cu on pla.co_cur = cu.co_cur and pla.co_emp = cu.co_emp'+
               ' join tb06_turmas tu on pla.co_tur = tu.co_tur and pla.co_emp = tu.co_emp'+
               ' join tb129_cadturmas ct on ct.co_tur = tu.co_tur and ct.co_modu_cur = tu.co_modu_cur ' +
               ' join tb02_materia m on pla.co_mat = m.co_mat and pla.co_emp = m.co_emp'+
               ' join tb107_cadmaterias cm on m.id_materia = cm.id_materia and m.co_emp = cm.co_emp'+
               ' where pla.co_cur = ' + strP_CO_CUR +
               ' and pla.co_tur = ' + strP_CO_TUR +
               ' and pla.co_emp = ' + strP_CO_EMP +
               ' and pla.co_modu_cur = ' + strP_CO_MODU_CUR +
               ' and pla.dt_prev_pla BETWEEN ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM);

  if strP_ID_MATERIA <> 'T' then
  begin
    SqlString := SqlString + ' and cm.id_materia = ' + strP_ID_MATERIA;
  end;

  if strP_CO_COL <> 'T' then
  begin
    SqlString := SqlString + ' and pla.CO_COL = ' + strP_CO_COL;
  end;

  if strP_TP_ATIV <> 'T' then
  begin
    SqlString := SqlString + ' and pla.FLA_HOMOLOG = ' + QuotedStr(strP_TP_ATIV);
  end;

  SqlString := SqlString + ' order by pla.dt_prev_pla, pla.nu_temp_pla';
      // Cria uma instância do Relatório.

      Relatorio := TFrmRelPlanoAula.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SQLString;
      ShowMessage(SQLString);
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

        if strP_TP_ATIV <> 'T' then
          Relatorio.QRLTpAtiv.Enabled := false
        else
          Relatorio.QRLTpAtiv.Enabled := true;

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
  DLLRelPlanoAula;

end.
