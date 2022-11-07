library WR_RelSolicRealiz;

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
  U_frmRelSolicRealiz in 'Relatorios\U_frmRelSolicRealiz.pas' {frmRelSolicRealiz};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelSolicRealiz(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,
 strP_CO_TIPO_SOLI, strP_DT_INI, strP_DT_FIM, strP_SITU: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TfrmRelSolicRealiz;
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

      SQLString := 'SET LANGUAGE PORTUGUESE ' +
               ' select tb65.co_tipo_soli, tb66.no_tipo_soli, ' +
               '        tb07.no_alu, TB07.NU_NIS, tb01.no_cur, TB01.CO_SIGL_CUR,TU.CO_SIGLA_TURMA as NO_TURMA, tb64.co_soli_aten, tb64.mes_soli_aten, tb64.ano_soli_aten, ' +
               '        tb64.dt_soli_aten, tb64.dt_prev_entr, tb65.dt_entr_soli, ' +
               '        tb65.va_soli_aten,tb65.co_col_ent_sol, tb64.co_isen_taxa, tb65.co_situ_soli, ' +
               '        tb08.co_alu_cad, TB64.DE_OBS_SOLI, COL.CO_MAT_COL, TB64.LOCALIZACAO, TB65.CO_COL_ENT_SOL,tb64.NU_DCTO_SOLIC ' +
               'from tb07_aluno as tb07                                    ' +
               ' inner join tb64_solic_atend as tb64 on tb07.co_alu = tb64.co_alu and tb07.co_emp = tb64.co_emp_alu ' +
               ' inner join tb08_matrcur as tb08 on tb08.co_alu = tb07.co_alu and tb08.co_cur = tb64.co_cur and tb08.co_tur = tb64.co_tur' +
               ' inner join tb01_curso as tb01 on tb01.co_cur = tb08.co_cur                                                               ' +
               ' inner join tb65_hist_solicit  as tb65 on tb65.co_soli_aten = tb64.co_soli_aten                                           ' +
               ' inner join tb66_tipo_solic as tb66 on tb66.co_tipo_soli = tb65.co_tipo_soli                                              ' +
               ' JOIN TB06_TURMAS TUR ON TUR.CO_TUR = TB08.CO_TUR and TUR.CO_CUR = TB08.CO_CUR                                            ' +
               ' JOIN TB129_CADTURMAS TU ON TU.CO_TUR = TUR.CO_TUR ' +
               ' JOIN TB03_COLABOR COL ON COL.CO_COL = TB64.CO_COL and COL.CO_EMP = TB64.CO_EMP ' +
                //'      JOIN TB03_COLABOR COL_ENT ON COL_ENT.CO_COL = TB65.CO_COL  and COL_ENT.CO_EMP = TB65.CO_EMP ' +
               ' where tb07.co_emp = '+ strP_CO_EMP +
               ' and tb08.co_sit_mat not in(' + quotedStr('C') + ')';

       if strP_CO_TIPO_SOLI <> 'T'  then
         SQLString := SQLString + ' and tb65.co_tipo_soli = ' + strP_CO_TIPO_SOLI;

       if strP_SITU <> 'S' then
        SQLString := SQLString + ' and tb65.co_situ_soli = ' + quotedStr(strP_SITU);

       if (strP_DT_INI <> nil) and (strP_DT_FIM <> nil) then
       begin
        SQLString := SQLString + ' and tb64.dt_soli_aten between ' + quotedStr(strP_DT_INI) + ' and ' + quotedStr(strP_DT_FIM);
       end;

      SQLString := SQLString + ' group by tb65.co_tipo_soli, tb66.no_tipo_soli, tb07.no_alu, TB07.NU_NIS, tb01.no_cur, TB01.CO_SIGL_CUR, TU.CO_SIGLA_TURMA, ' +
                   ' tb64.co_soli_aten, tb64.mes_soli_aten, tb64.ano_soli_aten, tb64.dt_soli_aten, tb64.dt_prev_entr, tb65.dt_entr_soli, ' +
                   ' tb65.va_soli_aten, tb64.co_isen_taxa, tb65.co_situ_soli, tb08.co_alu_cad, TB64.DE_OBS_SOLI, COL.CO_MAT_COL, TB64.LOCALIZACAO, TB65.CO_COL_ENT_SOL,tb65.co_col_ent_sol, tb64.NU_DCTO_SOLIC ';

      // Cria uma instância do Relatório.

      Relatorio := TfrmRelSolicRealiz.Create(Nil);

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
        Relatorio.QRLPeriodo.Caption := 'Período de: ' + strP_DT_INI + ' à ' + strP_DT_FIM;
        Relatorio.codigoEmpresa := strP_CO_EMP;

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
  DLLRelSolicRealiz;

end.
