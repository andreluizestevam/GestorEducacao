library WR_RelDistAluCaract;

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
  U_FrmRelDistAluCaract in 'Relatorios\U_FrmRelDistAluCaract.pas' {FrmRelDistAluCaract};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelDistAluCaractBARRIOBRA(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ANO_REF:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDistAluCaract;
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

      SqlString := ' SELECT DISTINCT bai.co_bairro,NO_BAIRRO as Bairro, ' +
'(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' and a.co_bairro = bai.co_bairro ' +
' and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Quantidade') + ',' +
' 	( SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_sexo_alu = ' + QuotedStr('M') +
' 			and m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' and a.co_bairro = bai.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Homens') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu' +
' 		WHERE co_sexo_alu =' + quotedStr('F') +
' 			and m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Mulheres') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_turn_mat = ' + quotedStr('M') +
' 			and co_sit_mat not in (' + quotedStr('situacao') + ')' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Manha') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_turn_mat = ' + quotedStr('V') +
' 			and co_sit_mat not in (' + quotedStr('situacao') + ')' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Tarde') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_turn_mat =' +  quotedStr('N') +
' 			and co_sit_mat not in (' + quotedStr('situacao') + ')' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Noite') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca =' + quotedStr('B') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Brancos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca = ' + quotedStr('N') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Pretos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca = ' +  quotedStr('A') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' +  quotedStr('Amarelos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca = ' + quotedStr('P') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Pardos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_raca = ' + quotedStr('I') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Indigena') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			 and ( tp_raca = ' + quotedStr('X') + ' OR TP_RACA is null ) ' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Nao_Declarada') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 1 ' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R1') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 2 ' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R2') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 3 ' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R3') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 4 ' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R4') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 5 ' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R5') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and renda_familiar = 6 ' +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('R6') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('N') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Nenhuma') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('A') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Auditivo') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('V') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Visual') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('F') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Fisica') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('M') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Mental') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('I') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Multiplas') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and tp_def = ' + quotedStr('O') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y			and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Def_Outras') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' 			and fla_bolsa_escola = ' + quotedStr('true') +
' and a.co_bairro = alu.co_bairro ' +
' 			and m.co_emp = codigoempresa ' +
' --y 		and m.co_ano_mes_mat = ano_ref ' +
' 	) as ' + quotedStr('Bolsa_Escola') +
' FROM tb08_matrcur m ' +
' JOIN tb07_aluno alu ON alu.co_alu = m.co_alu ' +
' JOIN tb905_bairro bai ON alu.co_bairro = bai.co_bairro ' +
' WHERE m.co_emp = codigoempresa ' +
'	and m.co_sit_mat not in (' + quotedStr('situacao') + ')' +
' --y	and m.co_ano_mes_mat = ano_ref ' +
' Group By m.co_emp,alu.co_bairro,bai.co_bairro,bai.no_bairro,m.co_alu--y,m.co_ano_mes_mat,m.co_sit_mat ' +
' ORDER BY BAI.NO_BAIRRO';

      // Se escolher o ANO s� exibir� matr�culas finalizadas - Diego Nobre 19/02/2009
     // � necess�rio para quando houver mais de um ano cadastrado;
     // Caso seja escolhido um ano anterior ao em andamento s� exibir� as matr�culas finalizadas;
      if strP_CO_ANO_REF <> nil then
      begin
        SqlString := StringReplace(SqlString,'--y','',[rfReplaceAll]);
        SqlString := StringReplace(SqlString,'ano_ref',strP_CO_ANO_REF,[rfReplaceAll]);
        SqlString := StringReplace(SqlString,'situacao','C',[rfReplaceAll]);
      // Se o ano escolhido for o atual situa��o = A, sen�o o atual situa��o = F - Diego Nobre 19/02/2009
      {
        if trim(strP_CO_ANO_REF) = trim(FormatDateTime('yyyy', Now)) then
        begin
          SqlString := StringReplace(SqlString,'situacao','A',[rfReplaceAll]);
        end
        else
        begin
          SqlString := StringReplace(SqlString,'situacao','F',[rfReplaceAll]);
        end;
        }
      end
      else
      begin
        SqlString := StringReplace(SqlString,'situacao','C',[rfReplaceAll]);
        //SqlString := StringReplace(SqlString,'situacao','A',[rfReplaceAll]);
      end;

      SqlString := StringReplace(SqlString,'codigoempresa',strP_CO_EMP,[rfReplaceAll]);

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelDistAluCaract.Create(Nil);

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
        Relatorio.QRLParam.Caption := 'Unidade: ' + Relatorio.QryCabecalhoRel.FieldByName('NO_FANTAS_EMP').AsString +
        ' - Ano Refer�ncia: ' + strP_CO_ANO_REF;

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
  DLLRelDistAluCaractBARRIOBRA;

end.
