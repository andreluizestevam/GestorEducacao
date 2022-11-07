library WR_RelEvasaoEscolar;

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
  U_FrmRelEvasaoEscolar in 'Relatorios\U_FrmRelEvasaoEscolar.pas' {FrmRelEvasaoEscolar};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelEvasaoEscolar(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR,
 strP_CO_MAT, strP_DDL_SEL, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelEvasaoEscolar;
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

    if strP_DDL_SEL = 'S' then
    begin
      SqlString := ' SET LANGUAGE PORTUGUESE ' +
                   ' select distinct b.no_cur,b.co_cur,b.CO_PARAM_FREQ_TIPO,b.SEQ_IMPRESSAO,b.CO_SIGL_CUR [no_red_materia] ' +
                   ' from TB01_CURSO b '+
                   ' where b.co_emp = '+ strP_CO_EMP +
                   ' and b.CO_MODU_CUR = '+ strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
      begin
        SqlString := SqlString + ' and b.co_cur = ' + strP_CO_CUR;
      end;

      SqlString := SqlString + ' order by b.SEQ_IMPRESSAO ';

    end;

    if strP_DDL_SEL = 'M' then
    begin
      SqlString := ' SET LANGUAGE PORTUGUESE ' +
                   ' select distinct b.no_cur ,b.co_cur,b.CO_PARAM_FREQ_TIPO,b.SEQ_IMPRESSAO, b.CO_SIGL_CUR [no_red_materia] ' +
                   ' from TB01_CURSO b '+
                   ' join tb43_grd_curso gc on b.co_cur = gc.co_cur and b.co_emp = gc.co_emp and b.co_modu_cur = gc.co_modu_cur ' +
                   ' join tb02_materia m on m.co_mat = gc.co_mat ' +
                   ' join tb107_cadmaterias cm on m.id_materia = cm.id_materia ' +
                   ' where gc.co_emp = '+ strP_CO_EMP +
                   ' and gc.CO_MODU_CUR = '+ strP_CO_MODU_CUR +
                   ' and b.co_param_freq_tipo = ' + quotedStr('M');

      if strP_CO_MAT <> 'T' then
        SqlString := SqlString + ' and cm.id_materia = ' + strP_CO_MAT;

        SqlString := SqlString + ' order by b.SEQ_IMPRESSAO ';
    end;

    if strP_DDL_SEL = 'G' then
    begin
      SqlString := ' SET LANGUAGE PORTUGUESE ' +
                 ' select distinct b.no_cur,b.co_cur,b.CO_PARAM_FREQ_TIPO,b.SEQ_IMPRESSAO,cm.no_red_materia,m.co_mat ' +
                 ' from TB01_CURSO b '+
                 ' join tb43_grd_curso gc on b.co_cur = gc.co_cur and b.co_emp = gc.co_emp and b.co_modu_cur = gc.co_modu_cur ' +
                 ' join tb02_materia m on m.co_mat = gc.co_mat and m.co_cur = gc.co_cur and gc.co_modu_cur = m.co_modu_cur ' +
                 ' join tb107_cadmaterias cm on m.id_materia = cm.id_materia ' +
                 ' where gc.co_emp = '+ strP_CO_EMP +
                 ' and gc.CO_MODU_CUR = '+ strP_CO_MODU_CUR +
                 ' and b.co_param_freq_tipo = ' + quotedStr('M');

      if strP_CO_MAT <> 'T' then
        SqlString := SqlString + ' and cm.id_materia = ' + strP_CO_MAT;

      SqlString := SqlString + ' order by b.no_cur ';
    end;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelEvasaoEscolar.Create(Nil);

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
        Relatorio.codigoEmpresa := strP_CO_EMP;
        Relatorio.dtInicial := strP_DT_INI;
        Relatorio.dtFinal := strP_DT_FIM;

        if strP_DDL_SEL = 'G' then
        begin
          Relatorio.QRLParam1.Caption := 'MATÉRIA';
          Relatorio.QRDBTParam1.DataField := 'NO_RED_MATERIA';
        end
        else
        begin
          Relatorio.QRLParam1.Caption := 'SÉRIE';
          Relatorio.QRDBTParam1.DataField := 'NO_CUR';
        end;

        if strP_DDL_SEL = 'M' then
        begin
          if strP_CO_MAT <> 'T' then
            Relatorio.id_materia := strP_CO_MAT
          else
            Relatorio.id_materia := '0';
          Relatorio.LblTituloRel.Caption := 'MAPA ESTATÍSTICO DE EVASÃO ESCOLAR - MATÉRIA';
          Relatorio.QrlParametros.Caption := strParamRel;
        end;

        if strP_DDL_SEL = 'S' then
        begin
          Relatorio.id_materia := '0';
          Relatorio.LblTituloRel.Caption := 'MAPA ESTATÍSTICO DE EVASÃO ESCOLAR - SÉRIE';
          Relatorio.QrlParametros.Caption := strParamRel;
        end;

        if strP_DDL_SEL = 'G' then
        begin
          if strP_CO_MAT <> 'T' then
            Relatorio.id_materia := strP_CO_MAT
          else
            Relatorio.id_materia := '0';
          Relatorio.LblTituloRel.Caption := 'MAPA ESTATÍSTICO DE EVASÃO ESCOLAR - TODAS AS MATÉRIAS';
          Relatorio.QrlParametros.Caption := strParamRel;
        end;

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
  DLLRelEvasaoEscolar;

end.
