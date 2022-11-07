library WR_RelGradeAlunos;

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
  U_FrmRelGradeAlunos in 'Relatorios\U_FrmRelGradeAlunos.pas' {FrmRelGradeAlunos};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelGradeAlunos(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelGradeAlunos;
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

       SqlString := ' SET LANGUAGE PORTUGUESE ' +
               ' SELECT mm.co_alu_cad,a.nu_nis,a.no_alu,a.co_sexo_alu,a.dt_nasc_alu,cid.no_cidade,bai.no_bairro,c.no_cur,ct.co_sigla_turma as no_tur,t.co_tur,c.co_cur,r.no_resp,r.nu_tele_resi_resp, ' +
               '        DEFICIENCIA = (CASE A.TP_DEF                                   '+
               '                      WHEN '+ '''' +'N'+ '''' +' THEN '+ '''' +'Nenhuma'+ ''''+
               '                      WHEN '+ '''' +'A'+ '''' +' THEN '+ '''' +'Auditiva'+ '''' +
               '                      WHEN '+ '''' +'V'+ '''' +' THEN '+ '''' +'Visual'+ '''' +
               '                      WHEN '+ '''' +'F'+ '''' +' THEN '+ '''' +'F�sica'+ '''' +
               '                      WHEN '+ '''' +'M'+ '''' +' THEN '+ '''' +'Mental'+ '''' +
               '                      WHEN '+ '''' +'I'+ '''' +' THEN '+ '''' +'M�ltiplas'+ '''' +
               '                      WHEN '+ '''' +'O'+ '''' +' THEN '+ '''' +'Outra'+ '''' +
               '                      ELSE '+ '''' + '''' +
               '                    END), ' +
               '        PARENTESCO = (CASE A.CO_GRAU_PAREN_RESP                                   '+
               '                      WHEN '+ '''' +'PM'+ '''' +' THEN '+ '''' +'Pai/M�e'+ ''''+
               '                      WHEN '+ '''' +'AV'+ '''' +' THEN '+ '''' +'Av�/Av�'+ '''' +
               '                      WHEN '+ '''' +'IR'+ '''' +' THEN '+ '''' +'Irm�o(�)'+ '''' +
               '                      WHEN '+ '''' +'TI'+ '''' +' THEN '+ '''' +'Tio(a)'+ '''' +
               '                      WHEN '+ '''' +'PR'+ '''' +' THEN '+ '''' +'Primo(a)'+ '''' +
               '                      WHEN '+ '''' +'CN'+ '''' +' THEN '+ '''' +'Cunhado(a)'+ '''' +
               '                      WHEN '+ '''' +'TU'+ '''' +' THEN '+ '''' +'Tutor(a)'+ '''' +
               '                      WHEN '+ '''' +'OU'+ '''' +' THEN '+ '''' +'Outros'+ '''' +
               '                      ELSE '+ '''' + '''' +
               '                    END), mo.de_modu_cur, mm.co_ano_mes_mat  ' +
               ' FROM tb07_aluno a ' +
               ' JOIN tb08_matrcur mm ON mm.co_alu = a.co_alu ' +
               ' JOIN tb44_modulo mo on mo.co_modu_cur = mm.co_modu_cur ' +
               ' JOIN tb01_curso c ON c.co_cur = mm.co_cur ' +
               ' JOIN tb06_turmas t ON t.co_tur = mm.co_tur and mm.co_cur = t.co_cur ' +
               ' JOIN tb129_cadturmas ct ON t.co_tur = ct.co_tur ' +
               ' JOIN tb108_responsavel r ON r.co_resp = a.co_resp ' +
               ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = A.CO_BAIRRO ' +
               ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = A.CO_CIDADE ' +
               ' AND   a.CO_EMP = ' + strP_CO_EMP +
               ' AND   mm.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_MES_MAT) +
               ' AND   mm.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

               if (strP_CO_CUR <> 'T') then
                  SqlString := SqlString + ' AND   mm.CO_CUR = ' + strP_CO_CUR;
                  
               if (strP_CO_TUR <> 'T') then
                  SqlString := SqlString + ' AND   mm.CO_TUR = ' + strP_CO_TUR;


      SqlString := SqlString + ' Order by mm.co_cur,mm.co_tur,a.NO_ALU';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelGradeAlunos.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SQLString;
      //ShowMessage(SQLString);
      Relatorio.QryRelatorio.Open;

      if Relatorio.QryRelatorio.IsEmpty then
      begin
        // Retorna -1 para Sem Registros na Consulta.
        intReturn := -1;
      end
      else
      begin
        // Atualiza Campos do Relat�rio Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        Relatorio.QRLParametros.Caption := strParamRel;
        Relatorio.QRLParam.Caption := 'Unidade: ' + Relatorio.QryCabecalhoRel.FieldByName('NO_FANTAS_EMP').AsString +
        ' - Ano Refer�ncia: ' + Relatorio.QryRelatorioco_ano_mes_mat.AsString +
        ' - M�dulo: ' + Relatorio.QryRelatoriode_modu_cur.AsString;
        //'Ano de refer�ncia: ' + edtAno.Text + ' - M�dulo: ' + edtModulo.Text + ' - ' + Sys_DescricaoTipoCurso + ': ' + edtCurso.Text +
        //' - Turma: ' + edtTurma.Text + ' - Turno: ' + QryPesqTurmaTURNO.AsString + ' - UE: ';

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLNumPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
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
  DLLRelGradeAlunos;

end.
