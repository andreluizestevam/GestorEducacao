library WR_RelBoletimAluno;

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
  U_FrmRelBoltetimAluno in 'Relatorios\U_FrmRelBoltetimAluno.pas' {FrmRelBoletimAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelBoletimAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_DE_MODU_CUR, strP_CO_CUR, strP_DE_CUR, strP_CO_TUR, strP_DE_TUR:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelBoletimAluno;
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

      SqlString := ' select distinct COL.NO_COL, COL.CO_MAT_COL, CID.NO_CIDADE, a.no_alu,a.co_alu,mc.co_alu_cad,c.no_cur,c.co_cur,cm.no_red_materia as no_materia,cm.no_sigla_materia,m.co_mat,mc.co_ano_mes_mat, mc.CO_STA_APROV, mc.CO_STA_APROV_FREQ from tb08_matrcur mc ' +
               ' join tb07_aluno a on a.co_alu = mc.co_alu ' +
               ' join tb43_grd_curso gc on gc.co_modu_cur = mc.co_modu_cur and gc.CO_ANO_GRADE = mc.co_ano_mes_mat and gc.co_cur = mc.co_cur and gc.co_emp = mc.co_emp ' +
               ' join tb01_curso c on c.co_cur = mc.co_cur ' +
               ' join tb02_materia m on m.CO_MAT = gc.co_mat and c.co_cur = gc.co_cur and c.co_modu_cur = gc.co_modu_cur ' +
               ' join tb107_cadmaterias cm on cm.id_materia = m.id_materia ' +
               ' join tb06_turmas t on t.co_tur = mc.co_tur and t.co_cur = mc.co_cur ' +
               ' join tb129_cadturmas ct on t.co_tur = ct.co_tur and t.co_modu_cur = ct.co_modu_cur ' +
               ' JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
               ' JOIN TB03_COLABOR COL ON COL.CO_COL = E.CO_DIR and col.co_emp = e.co_emp '+
               ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = E.CO_CIDADE '+
               ' where mc.CO_EMP = ' + strP_CO_EMP +
               ' and a.co_emp = mc.co_emp ' +
               ' AND mc.co_sit_mat in (' + quotedstr('A') + ',' + quotedstr('P') + ',' + quotedstr('F') +')' +
               ' AND   mc.CO_CUR = ' + strP_CO_CUR +
               //' AND   mm.CO_CUR = ' + QryPesquisaCursoCO_CUR.AsString +
               ' AND   mc.CO_TUR = '+ strP_CO_TUR       +
               ' AND mc.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
               ' and mc.co_ano_mes_mat = ' + QuotedStr(strP_CO_ANO_REF);

      if strP_CO_ALU <> 'T' then
      begin
        SqlString := SqlString + ' AND a.CO_ALU = ' + strP_CO_ALU;
      end;

      SqlString := SqlString + ' Order by a.NO_ALU';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelBoletimAluno.Create(Nil);

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

        Relatorio.numMat := 0;
        
        with Relatorio.qryMateria do
        begin

          Close;
          Parameters.parambyname('P_CO_EMP').Value := StrToInt(strP_CO_EMP);
          Parameters.parambyname('P_CO_CUR').Value := StrToInt(strP_CO_CUR);
          Parameters.ParamByName('P_CO_MODU_CUR').Value := StrToInt(strP_CO_MODU_CUR);
          Open;
          First;
          
          while Not Eof do
          begin
            Relatorio.materia[Relatorio.numMat] := fieldByName('co_mat').asinteger;
            Relatorio.numMat := Relatorio.numMat + 1;
            Next;
          end;
        end;
        Relatorio.CodigoCurso := strP_CO_CUR;
        Relatorio.CodigoTurma := strP_CO_TUR;
        Relatorio.AnoCurso := strP_CO_ANO_REF;
        Relatorio.CodigoModulo := strP_CO_MODU_CUR;
        Relatorio.QRLSerie.Caption := strP_DE_CUR;
        Relatorio.QRLTurma.Caption := strP_DE_TUR;
        Relatorio.QRLModulo.Caption := strP_DE_MODU_CUR;
        Relatorio.QRLAno.Caption := strP_CO_ANO_REF;

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
  DLLRelBoletimAluno;

end.
