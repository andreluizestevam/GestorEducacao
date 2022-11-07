library WR_RelMapaCaracteristicaMatriculaSerie;

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
  U_FrmRelMapaCaracteristicaMatricula in 'Relatorios\U_FrmRelMapaCaracteristicaMatricula.pas' {FrmRelMapaCaracteristicaMatricula};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelMapaCaracteristicaMatriculaSerie(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CNPJ_INSTI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaCaracteristicaMatricula;
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

      SqlString := ' SELECT NO_CUR as Curso, ct.CO_SIGLA_TURMA as Turma,' +
' 	COUNT(co_alu) as ' + quotedStr('Quantidade') + ',' +
' 	( SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_sexo_alu = ' + QuotedStr('M') +
' 			and m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Homens') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu' +
' 		WHERE co_sexo_alu =' + quotedStr('F') +
' 			and m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Mulheres') + ',' +
' 	(SELECT COUNT(CO_ALU) FROM tb08_matrcur ' +
' 		WHERE co_turn_mat = ' + quotedStr('M') +
' 			and co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and co_cur = cur.co_cur' +
'       and co_tur = tur.co_tur ' +
' 			and co_emp = codigoempresa ' +
' --y			and co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Manha') + ',' +
' 	(SELECT COUNT(CO_ALU) FROM tb08_matrcur ' +
' 		WHERE co_turn_mat = ' + quotedStr('V') +
' 			and co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and co_cur = cur.co_cur ' +
'       and co_tur = tur.co_tur ' +
' 			and co_emp = codigoempresa ' +
' --y			and co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Tarde') + ',' +
' 	(SELECT COUNT(CO_ALU) FROM tb08_matrcur ' +
' 		WHERE co_turn_mat =' +  quotedStr('N') +
' 			and co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and co_cur = cur.co_cur ' +
'       and co_tur = tur.co_tur ' +
' 			and co_emp = codigoempresa ' +
' --y			and co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Noite') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca =' + quotedStr('B') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Brancos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca = ' + quotedStr('N') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Pretos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca = ' +  quotedStr('A') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' +  quotedStr('Amarelos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca = ' + quotedStr('P') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Pardos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca = ' + quotedStr('I') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Indigena') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and (tp_raca = ' + quotedStr('X') + ' OR TP_RACA is null ) ' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Nao_Declarada') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 1 ' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R1') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 2 ' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R2') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 3 ' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R3') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 4 ' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R4') + ',' +
'   (SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 5 ' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R5') + ',' +
'   (SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 6 ' +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R6') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('N') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Nenhuma') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('A') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Auditivo') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('V') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Visual') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('F') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Fisica') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('M') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Mental') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('I') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Multiplas') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('O') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Outras') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and fla_bolsa_escola = ' + quotedStr('true') +
' 			and m.co_cur = cur.co_cur ' +
'       and m.co_tur = tur.co_tur ' +
' 			and m.co_emp = codigoempresa ' +
' --y 		and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Bolsa_Escola') + ', mo.de_modu_cur' +
' FROM tb08_matrcur m ' +
' JOIN tb44_modulo mo on mo.co_modu_cur = m.co_modu_cur ' +
' JOIN tb01_curso cur ON cur.co_cur = m.co_cur and cur.co_modu_cur = m.co_modu_cur ' +
' JOIN tb06_turmas tur ON tur.co_tur = m.co_tur ' +
' JOIN Tb129_cadturmas ct on ct.co_tur = tur.co_tur ' +
' WHERE m.co_emp = codigoempresa ' +
' --z and m.co_cur = codigocurso ' + #13 +
' --mo and m.co_modu_cur = modulocurso ' + #13 +
'	and m.co_sit_mat not in (' + quotedStr('situacao') + ') '  + #13 +
' --y and m.co_ano_mes_mat = ano_ref ' + #13 +
' Group By m.co_emp,cur.co_cur,cur.no_cur,tur.co_tur,ct.co_sigla_turma,cur.SEQ_IMPRESSAO, mo.de_modu_cur --y,m.co_ano_mes_mat,m.co_sit_mat ' + #13 +
' order by cur.SEQ_IMPRESSAO';

      SqlString := StringReplace(SqlString,'--mo','',[rfReplaceAll]);
      SqlString := StringReplace(SqlString,'modulocurso',strP_CO_MODU_CUR,[rfReplaceAll]);

      if strP_CO_CUR <> 'T' then
      begin
        SqlString := StringReplace(SqlString,'--z','',[rfReplaceAll]);
        SqlString := StringReplace(SqlString,'codigocurso',strP_CO_CUR,[rfReplaceAll]);
      end;

      if strP_CO_ANO_REF <> nil then
      begin
        SqlString := StringReplace(SqlString,'--y','',[rfReplaceAll]);
        SqlString := StringReplace(SqlString,'ano_ref',strP_CO_ANO_REF,[rfReplaceAll]);
      // Se o ano escolhido for o atual situa��o = A, sen�o o atual situa��o = F - Diego Nobre 19/02/2009
        SqlString := StringReplace(SqlString,'situacao','C',[rfReplaceAll]);
         {
        if trim(strP_CO_ANO_REF) = trim(FormatDateTime('yyyy', Now)) then
        begin
          SqlString := StringReplace(SqlString,'situacao','A',[rfReplaceAll]);
        end
        else
        begin
          SqlString := StringReplace(SqlString,'situacao','F',[rfReplaceAll]);
        end;   }
      end
      else
      begin
        SqlString := StringReplace(SqlString,'situacao','C',[rfReplaceAll]);
        //SqlString := StringReplace(SqlString,'situacao','A',[rfReplaceAll]);
      end;

      SqlString := StringReplace(SqlString,'codigoempresa',strP_CO_EMP,[rfReplaceAll]);

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelMapaCaracteristicaMatricula.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
      Relatorio.QryRelatorio.Close;
      DM.Conn.ConnectionString := retornaConexao(strP_CNPJ_INSTI);
      DM.Conn.Open;
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
        // Atualiza Campos do Relat�rio Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        Relatorio.QRLParam.Caption := 'Unidade: ' + Relatorio.QryCabecalhoRel.FieldByName('NO_FANTAS_EMP').AsString +
        ' - Ano Refer�ncia: ' + strP_CO_ANO_REF + 
        ' - Modalidade: ' + Relatorio.QryRelatorio.FieldByName('de_modu_cur').AsString;


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
  DLLRelMapaCaracteristicaMatriculaSerie;

end.
