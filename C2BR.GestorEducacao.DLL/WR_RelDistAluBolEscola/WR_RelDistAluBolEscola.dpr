library WR_RelDistAluBolEscola;

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
  U_FrmRelDistAluBolEscola in 'Relatorios\U_FrmRelDistAluBolEscola.pas' {FrmRelDistAluBolEscola},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelDistAluBolEscola(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_BOLSA:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDistAluBolEscola;
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

      SQLString := ' select distinct a.nu_nis, a.no_alu, a.co_alu, a.co_emp, a.co_sexo_alu, a.dt_nasc_alu, R.NU_CPF_RESP, A.RENDA_FAMILIAR, '+
               ' 	RENDA = (CASE A.RENDA_FAMILIAR ' +
               '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('1 a 3 SM') +
               '	WHEN ' + QuotedStr('2') + ' THEN ' + QuotedStr('3 a 5 SM') +
               '	WHEN ' + QuotedStr('3') + ' THEN ' + QuotedStr('+5 SM') +
               '	WHEN ' + QuotedStr('4') + ' THEN ' + QuotedStr('Sem Renda') +
               '	END), ps.NO_REDUZ_PROGR_SOCIA ' +
               ' from tb07_aluno a ' +
               ' join tb08_matrcur mm on mm.co_alu = a.co_alu and mm.co_emp = a.co_emp '+
               ' join tb01_curso c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp '+
               ' join tb06_turmas t on t.co_tur = mm.co_tur and mm.co_emp = t.co_emp and t.co_cur = mm.co_cur'+
               ' join tb129_cadturmas tu on tu.co_tur = mm.co_tur and mm.co_emp = c.co_emp '+
               ' JOIN TB108_RESPONSAVEL R ON R.CO_RESP = A.CO_RESP '+
               ' join TB136_ALU_PROG_SOCIAIS aps on aps.co_alu = a.co_alu and aps.co_emp = a.co_emp ' +
               ' join TB135_PROG_SOCIAIS ps on ps.CO_IDENT_PROGR_SOCIA = aps.CO_IDENT_PROGR_SOCIA and ps.ORG_CODIGO_ORGAO = aps.ORG_CODIGO_ORGAO ' +
               ' where a.co_emp = ' + strP_CO_EMP +
               ' and mm.co_sit_mat not in ( '+ QuotedStr('C') + ')';

      if strP_BOLSA <> 'T' then
        SQLString := SQLString + ' and aps.CO_IDENT_PROGR_SOCIA = ' + quotedStr(strP_BOLSA);

      SQLString := SQLString + ' order by a.no_alu';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDistAluBolEscola.Create(Nil);

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

        if strP_BOLSA <> 'T' then
          Relatorio.QRLProgSocial.Caption := Relatorio.QryRelatorio.FieldByName('NO_REDUZ_PROGR_SOCIA').AsString
        else
          Relatorio.QRLProgSocial.Caption := 'Todos';

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
  DLLRelDistAluBolEscola;

end.
