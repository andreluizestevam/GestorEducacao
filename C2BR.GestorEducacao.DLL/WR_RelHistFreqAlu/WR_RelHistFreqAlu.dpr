library WR_RelHistFreqAlu;

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
  U_FrmRelHistFreqAlu in 'Relatorios\U_FrmRelHistFreqAlu.pas' {FrmRelHistFreqAlu};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelHistFreqAlu(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR,
 strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_NO_ALU, strP_CO_PARAM_FREQUE, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelHistFreqAlu;
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
               ' select distinct FR.CO_CUR,cu.CO_PARAM_FREQ_TIPO, FR.CO_TUR,a.nu_nis,mm.co_alu_cad,PLA.dt_prev_pla,apt.HR_INI_ATIV,apt.HR_TER_ATIV, fr.dt_fre, PLA.nu_temp_pla,cu.no_cur,ct.no_turma as no_tur,'+
               'cm.no_materia,fr.DE_JUSTI_FREQ_ALUNO,fr.CO_FLAG_FREQ_ALUNO, PLA.DE_TEMA_AULA,PLA.DE_OBJE_AULA '+
               ' from TB132_FREQ_ALU fr '+
               'join TB119_ATIV_PROF_TURMA apt on apt.CO_ATIV_PROF_TUR = fr.CO_ATIV_PROF_TUR and fr.CO_EMP_ALU = apt.CO_EMP ' +
               'left join TB17_PLANO_AULA PLA ON PLA.CO_EMP = apt.CO_EMP and PLA.CO_PLA_AULA = apt.CO_PLA_AULA ' +
               ' left join TB08_MATRCUR mm  on fr.co_alu = mm.co_alu and fr.CO_EMP_ALU = mm.co_emp ' +
               ' join TB07_aluno a on fr.co_alu = a.co_alu and fr.CO_EMP_ALU = a.co_emp'+
               ' join tb01_curso cu on fr.co_cur = cu.co_cur and fr.CO_EMP_ALU = cu.co_emp'+
               ' join tb06_turmas tu on fr.co_tur = tu.co_tur and fr.CO_EMP_ALU = tu.co_emp'+
               ' join tb129_cadturmas ct on ct.co_tur = tu.co_tur '+
               ' left join tb02_materia m on fr.co_mat = m.co_mat and fr.CO_EMP_ALU = m.co_emp'+
               ' join tb107_cadmaterias cm on m.id_materia = cm.id_materia and m.co_emp = cm.co_emp'+
               ' where fr.co_cur = ' + strP_CO_CUR +
               ' and fr.co_tur = ' + strP_CO_TUR +
               ' and fr.CO_EMP_ALU = ' + strP_CO_EMP +
               ' and fr.co_cur = mm.co_cur and fr.co_tur = mm.co_tur' +
               ' and fr.co_modu_cur = ' + strP_CO_MODU_CUR +
               ' and mm.co_sit_mat in ( ' + QuotedStr('A') + ',' + QuotedStr('F') + ',' + QuotedStr('T') + ')' +
               ' and mm.co_ano_mes_mat = ' + strP_CO_ANO_REF +
               ' and fr.co_alu = ' + strP_CO_ALU +
               ' and fr.dt_fre BETWEEN ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM);

      SqlString := SqlString + ' order by fr.dt_fre';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelHistFreqAlu.Create(Nil);

      if strP_CO_PARAM_FREQUE = 'D' then
        SqlString := SqlString + ' and fr.co_mat is null';

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
        // Atualiza Campos do Relatório Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.QRLParametros.Caption := '(Período de '+ FormatDateTime('dd/MM/yy',StrToDate(strP_DT_INI)) + ' à ' + FormatDateTime('dd/MM/yy',StrToDate(strP_DT_FIM))+')';
        Relatorio.QRLAluno.Caption := 'Aluno: ' + UpperCase(strP_NO_ALU) + ' ';
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
  DLLRelHistFreqAlu;

end.
