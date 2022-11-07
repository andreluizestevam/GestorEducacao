library WR_RelDistAluCarAno;

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
  U_FrmRelDistAluCarAno in 'Relatorios\U_FrmRelDistAluCarAno.pas' {FrmRelDistAluCarAno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelDistAluCarAno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDistAluCarAno;
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

      SqlString := ' SELECT DISTINCT mat.co_ano_mes_mat as Ano, ' +
'(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat ' +
' 	) as ' + quotedStr('Quantidade') + ',' +
' 	( SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_sexo_alu = ' + QuotedStr('M') +
' 			and m.co_sit_mat not in (' + QuotedStr('situacao') +
') 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat ' +
' 	) as ' + quotedStr('Homens') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu' +
' 		WHERE co_sexo_alu =' + quotedStr('F') +
' 			and m.co_sit_mat not in ('  + quotedStr('situacao') +
') 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat ' +
' 	) as ' + quotedStr('Mulheres') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_turn_mat = ' + quotedStr('M') +
' 			and co_sit_mat not in (' + quotedStr('situacao') +
') 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' + quotedStr('Manha') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_turn_mat = ' + quotedStr('V') +
' 			and co_sit_mat not in (' + quotedStr('situacao') +
') 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' + quotedStr('Tarde') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE co_turn_mat =' +  quotedStr('N') +
' 			and co_sit_mat not in (' + quotedStr('situacao') +
') 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('Noite') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_raca =' + quotedStr('B') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('Brancos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_raca = ' + quotedStr('N') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' + quotedStr('Pretos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_raca = ' +  quotedStr('A') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' +  quotedStr('Amarelos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_raca = ' + quotedStr('P') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat ' +
' 	) as ' + quotedStr('Pardos') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_raca = ' + quotedStr('I') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' + quotedStr('Indigena') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and (tp_raca = ' + quotedStr('X') + ' OR TP_RACA is null ) ' +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('Nao_Declarada') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and renda_familiar = 1 ' +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('R1') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and renda_familiar = 2 ' +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('R2') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and renda_familiar = 3 ' +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' + quotedStr('R3') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + QuotedStr('situacao') +
') 			and renda_familiar = 4 ' +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('R4') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + QuotedStr('situacao') +
') 			and renda_familiar = 5 ' +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('R5') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + QuotedStr('situacao') +
') 			and renda_familiar = 6 ' +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('R6') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_def = ' + quotedStr('N') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('Def_Nenhuma') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_def = ' + quotedStr('A') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('Def_Auditivo') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_def = ' + quotedStr('V') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('Def_Visual') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_def = ' + quotedStr('F') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' + quotedStr('Def_Fisica') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_def = ' + quotedStr('M') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('Def_Mental') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_def = ' + quotedStr('I') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' + quotedStr('Def_Multiplas') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and tp_def = ' + quotedStr('O') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat'+
' 	) as ' + quotedStr('Def_Outras') + ',' +
' 	(SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m ' +
' 		JOIN tb07_aluno a ON a.co_alu = m.co_alu ' +
' 		WHERE m.co_sit_mat not in (' + quotedStr('situacao') +
') 			and fla_bolsa_escola = ' + quotedStr('true') +
' 			and m.co_emp = codigoempresa ' +
' and m.co_ano_mes_mat = mat.co_ano_mes_mat' +
' 	) as ' + quotedStr('Bolsa_Escola') +
' FROM tb08_matrcur mat  ' +
' JOIN tb07_aluno alu ON alu.co_alu = mat.co_alu ' +
' WHERE mat.co_emp = codigoempresa' +
' and mat.co_sit_mat not in (' + quotedStr('situacao') +
') Group By mat.co_emp,mat.co_ano_mes_mat,mat.co_alu';

     SqlString := StringReplace(SqlString,'situacao','C',[rfReplaceAll]);
     SqlString := StringReplace(SqlString,'codigoempresa',strP_CO_EMP,[rfReplaceAll]);

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDistAluCarAno.Create(Nil);

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
  DLLRelDistAluCarAno;

end.
