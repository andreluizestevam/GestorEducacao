library WR_RelPautaChamadaFrente;

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
  U_FrmRelPautaChamadaFrente in 'Relatorios\U_FrmRelPautaChamadaFrente.pas' {FrmRelPautaChamadaFrente};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelPautaChamadaFrente(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR,
 strP_CO_TUR, strP_CO_ANO_REF, strP_CO_MAT, strP_NUM_MES, strP_DES_MES: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelPautaChamadaFrente;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString: string;
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

      if strP_CO_MAT <> nil then
      begin
        SqlString := 'Select distinct C.NO_CUR, CT.NO_TURMA, CM.NO_MATERIA,MO.DE_MODU_CUR,M.CO_MAT,C.CO_CUR,'+
                    'CM.NO_SIGLA_MATERIA,T.CO_TUR,G.CO_ANO_MES_MAT,MO.CO_MODU_CUR    '+
                    ' From TB48_GRADE_ALUNO G ' +
                    ' join TB01_CURSO C on C.co_cur = g.co_cur and c.co_emp = g.co_emp and c.co_modu_cur = g.co_modu_cur '+
                    ' left join tb02_materia m on G.CO_CUR = M.CO_CUR and G.CO_MAT = M.CO_MAT and G.CO_MODU_CUR = M.CO_MODU_CUR and g.co_emp = m.co_emp ' +
                    ' left join TB107_CADMATERIAS CM on M.ID_MATERIA = CM.ID_MATERIA and cm.co_emp = m.co_emp ' +
                    ' join TB44_MODULO MO on g.co_modu_cur = mo.co_modu_cur ' +
                    ' join TB06_TURMAS T on t.co_tur = g.co_tur and t.co_emp = g.co_emp and t.co_modu_cur = g.co_modu_cur ' +
                    ' join TB129_CADTURMAS CT on t.co_tur = ct.co_tur '+
                    ' WHERE G.CO_EMP = ' + strP_CO_EMP +
                    ' AND G.CO_CUR = ' + strP_CO_CUR +
                    ' AND G.CO_TUR = ' + strP_CO_TUR +
                    ' AND G.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    ' AND G.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
                    ' AND G.NU_SEM_LET = ' + IntToStr(1) +
                    ' and G.CO_MAT = ' + strP_CO_MAT;
      end
      else
      begin
        SqlString := 'Select top 1 C.NO_CUR, CT.NO_TURMA, CM.NO_MATERIA,MO.DE_MODU_CUR,M.CO_MAT,C.CO_CUR,'+
                    'CM.NO_SIGLA_MATERIA,T.CO_TUR,G.CO_ANO_MES_MAT,MO.CO_MODU_CUR    '+
                    ' From TB48_GRADE_ALUNO G ' +
                    ' join TB01_CURSO C on C.co_cur = g.co_cur and c.co_emp = g.co_emp and c.co_modu_cur = g.co_modu_cur '+
                    ' left join tb02_materia m on G.CO_CUR = M.CO_CUR and G.CO_MAT = M.CO_MAT and G.CO_MODU_CUR = M.CO_MODU_CUR and g.co_emp = m.co_emp ' +
                    ' left join TB107_CADMATERIAS CM on M.ID_MATERIA = CM.ID_MATERIA and cm.co_emp = m.co_emp ' +
                    ' join TB44_MODULO MO on g.co_modu_cur = mo.co_modu_cur ' +
                    ' join TB06_TURMAS T on t.co_tur = g.co_tur and t.co_emp = g.co_emp and t.co_modu_cur = g.co_modu_cur ' +
                    ' join TB129_CADTURMAS CT on t.co_tur = ct.co_tur '+
                    ' WHERE G.CO_EMP = ' + strP_CO_EMP +
                    ' AND G.CO_CUR = ' + strP_CO_CUR +
                    ' AND G.CO_TUR = ' + strP_CO_TUR +
                    ' AND G.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                    ' AND G.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
                    ' AND G.NU_SEM_LET = ' + IntToStr(1);
      end;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelPautaChamadaFrente.Create(Nil);

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

        if strP_CO_MAT <> nil then
        begin
          Relatorio.QRLMateria.Enabled := True;
        end
        else
          Relatorio.QRLMateria.Enabled := False;

        Relatorio.numMes := StrToInt(strP_NUM_MES);

        Relatorio.QRLMesReferencia.Caption := strP_DES_MES;
        Relatorio.QrlAluno1.Caption := '';
        Relatorio.QrlAluno2.Caption := '';
        Relatorio.QrlAluno3.Caption := '';
        Relatorio.QrlAluno4.Caption := '';
        Relatorio.QrlAluno5.Caption := '';
        Relatorio.QrlAluno6.Caption := '';
        Relatorio.QrlAluno7.Caption := '';
        Relatorio.QrlAluno8.Caption := '';
        Relatorio.QrlAluno9.Caption := '';
        Relatorio.QrlAluno10.Caption := '';
        Relatorio.QrlAluno11.Caption := '';
        Relatorio.QrlAluno12.Caption := '';
        Relatorio.QrlAluno13.Caption := '';
        Relatorio.QrlAluno14.Caption := '';
        Relatorio.QrlAluno15.Caption := '';
        Relatorio.QrlAluno16.Caption := '';
        Relatorio.QrlAluno17.Caption := '';
        Relatorio.QrlAluno18.Caption := '';
        Relatorio.QrlAluno19.Caption := '';
        Relatorio.QrlAluno20.Caption := '';
        Relatorio.QrlAluno21.Caption := '';
        Relatorio.QrlAluno22.Caption := '';
        Relatorio.QrlAluno23.Caption := '';
        Relatorio.QrlAluno24.Caption := '';
        Relatorio.QrlAluno25.Caption := '';
        Relatorio.QrlAluno26.Caption := '';
        Relatorio.QrlAluno27.Caption := '';
        Relatorio.QrlAluno28.Caption := '';
        Relatorio.QrlAluno29.Caption := '';
        Relatorio.QrlAluno30.Caption := '';
        Relatorio.QrlAluno31.Caption := '';
        Relatorio.QrlAluno32.Caption := '';
        Relatorio.QrlAluno33.Caption := '';
        Relatorio.QrlAluno34.Caption := '';
        Relatorio.QrlAluno35.Caption := '';
        Relatorio.QrlAluno36.Caption := '';
        Relatorio.QrlAluno37.Caption := '';
        Relatorio.QrlAluno38.Caption := '';
        Relatorio.QrlAluno39.Caption := '';
        Relatorio.QrlAluno40.Caption := '';
        Relatorio.QrlAluno41.Caption := '';
        Relatorio.QrlAluno42.Caption := '';
        Relatorio.QrlAluno43.Caption := '';
        Relatorio.QrlAluno44.Caption := '';
        Relatorio.QrlAluno45.Caption := '';

        with Relatorio.QryProfessor do
        begin
          Close;
          SQL.Clear;
          if strP_CO_MAT <> nil then
          begin
            SqlString := ' SELECT C.NO_COL,C.CO_MAT_COL    ' +
                   ' FROM TB_RESPON_MATERIA P, TB03_COLABOR C ' +
                   ' WHERE P.CO_COL_RESP = C.CO_COL          ' +
                   ' AND P.CO_EMP = C.CO_EMP                ' +
                   ' AND P.CO_EMP = ' + strP_CO_EMP +
                   ' AND P.CO_CUR = ' + strP_CO_CUR +
                   ' AND P.CO_TUR = ' + strP_CO_TUR +
                   ' AND P.CO_MAT = ' + strP_CO_MAT +
                   ' AND P.CO_MODU_CUR = ' + strP_CO_MODU_CUR;
          end
          else
          begin
            SqlString := ' SELECT C.NO_COL,C.CO_MAT_COL    ' +
                   ' FROM TB_RESPON_MATERIA P, TB03_COLABOR C ' +
                   ' WHERE P.CO_COL_RESP = C.CO_COL          ' +
                   ' AND P.CO_EMP = C.CO_EMP                ' +
                   ' AND P.CO_EMP = ' + strP_CO_EMP +
                   ' AND P.CO_CUR = ' + strP_CO_CUR +
                   ' AND P.CO_TUR = ' + strP_CO_TUR +
                   ' AND P.CO_MODU_CUR = ' + strP_CO_MODU_CUR;
          end;
          SQL.Text := SqlString;
          Open;
          if IsEmpty then
          begin
          end;
        end;
        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;

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
  DLLRelPautaChamadaFrente;

end.
