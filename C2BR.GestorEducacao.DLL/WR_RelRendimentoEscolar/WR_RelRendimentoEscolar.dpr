library WR_RelRendimentoEscolar;

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
  U_FrmRelRendimentoEscolar in 'Relatorios\U_FrmRelRendimentoEscolar.pas' {frmRelRendimentoEscolar};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelRendimentoEscolar(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_CO_ALU, strP_CO_ANO_REF: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRendimentoEscolar;
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

      SqlString := ' SELECT DISTINCT e.co_emp,e.no_fantas_emp as Empresa,a.co_alu,a.no_alu as Aluno,a.no_mae_alu as Mae, '+
                   ' a.no_pai_alu as Pai,h.co_ano_ref as Ano,c.co_cur,c.no_cur as Curso,t.co_peri_tur as Turno,ct.co_sigla_turma as Turma,'+
                   ' m.co_mat,CM.NO_RED_MATERIA as Materia,h.VL_NOTA_BIM1, h.VL_NOTA_BIM2,h.VL_NOTA_BIM3,h.VL_NOTA_BIM4,h.VL_MEDIA_FINAL,'+
                   ' h.VL_PROVA_FINAL,c.co_nivel_cur,a.nu_nis,mm.co_alu_cad,mm.co_sit_mat,m.qt_carg_hora_mat,h.qt_falta_bim1,h.qt_falta_bim2,'+
                   ' h.qt_falta_bim3,h.qt_falta_bim4, MM.CO_STA_APROV, MM.CO_STA_APROV_FREQ, r.no_resp,h.co_modu_cur,h.co_tur '+
                   ' FROM tb079_hist_aluno h'+
                   ' JOIN tb01_curso      c ON c.co_cur = h.co_cur and h.co_emp = c.co_emp'+
                   ' JOIN tb07_aluno      a ON a.co_alu = h.co_alu and h.co_emp = a.co_emp'+
                   ' JOIN tb08_matrcur mm on h.co_alu = mm.co_alu and h.co_emp = mm.co_emp and mm.co_ano_mes_mat = h.co_ano_ref '+
                   ' and mm.CO_CUR = h.CO_CUR ' +
                   ' JOIN tb06_turmas     t ON t.co_tur = h.co_tur and h.co_emp = t.co_emp'+
                   ' JOIN tb129_cadturmas     ct ON t.co_tur = ct.co_tur '+
                   ' JOIN tb02_materia    m ON m.co_mat = h.co_mat and h.co_emp = m.co_emp'+
                   ' JOIN tb25_empresa    e ON e.co_emp = h.co_emp'+
                   ' JOIN TB107_CADMATERIAS CM ON CM.ID_MATERIA = M.ID_MATERIA'+
                   ' left JOIN tb108_responsavel r on r.co_resp = a.co_resp ' +
                   ' WHERE h.co_emp = ' + strP_CO_EMP +
                   ' and h.co_ano_ref = ' + strP_CO_ANO_REF +
                   ' and h.co_alu = ' + strP_CO_ALU +
                   ' and MM.CO_SIT_MAT in ( ' + quotedStr('A') + ',' + quotedStr('F') + ',' + quotedStr('T') + ',' + quotedStr('X') + ')';

      SqlString := SqlString +  ' ORDER BY CM.NO_RED_MATERIA ';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelRendimentoEscolar.Create(Nil);

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
        Relatorio.codigoEmpresa := strP_CO_EMP;

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
  DLLRelRendimentoEscolar;

end.