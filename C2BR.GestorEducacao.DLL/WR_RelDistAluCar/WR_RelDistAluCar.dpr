library WR_RelDistAluCar;

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
  U_FrmRelDistAluCar in 'Relatorios\U_FrmRelDistAluCar.pas' {FrmRelDistAluCar},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelDistAluCar(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strP_TP_RACA, strP_RENDA, strP_TP_DEF, strP_BOLSA, strP_PASSE:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDistAluCar;
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

      SqlString := ' SELECT a.co_alu, a.co_emp, a.nu_nis, a.no_alu, RE.nu_cpf_RESP, a.dt_nasc_alu, a.co_sexo_alu, a.tp_raca, a.tp_def, a.renda_familiar, a.co_esta_alu, c.no_cidade, b.no_bairro, ' +
                   ' 	BE = (Case a.FLA_BOLSA_ESCOLA '+
                   '			when ' + QuotedStr('False') + ' then ' + QuotedStr('Não') +
                   '			when ' + QuotedStr('True') + ' then ' + QuotedStr('Sim') +
                   '		  End), '+
                   '	PASSE = (Case a.FLA_PASSE_ESCOLA '+
                   '				when ' + QuotedStr('False') + ' then ' + QuotedStr('Não') +
                   '				when ' + QuotedStr('True') + ' then ' + QuotedStr('Sim') +
                   '			 End), a.co_modu_cur, a.co_cur, a.co_tur '+
                   ' from tb07_aluno a ' +
                   //' join tb08_matrcur mm on a.co_alu = mm.co_alu and a.co_emp = mm.co_emp ' +
                   ' JOIN tb905_bairro b on b.co_bairro = a.co_bairro and b.co_cidade = a.co_cidade and b.co_uf = a.co_esta_alu ' +
                   ' JOIN tb904_cidade c on c.co_cidade = a.co_cidade ' +
                   //' left JOIN tb01_curso cu on a.co_cur = cu.co_cur'+
                   //' left JOIN TB06_TURMAS TUR ON TUR.CO_TUR = a.CO_TUR and TUR.CO_CUR = cu.CO_CUR '+
                   //' left JOIN TB129_CADTURMAS CT ON CT.CO_TUR = TUR.CO_TUR ' +
                   ' JOIN TB108_RESPONSAVEL RE ON RE.CO_RESP = A.CO_RESP '+
                   ' WHERE a.co_emp = ' + strP_CO_EMP;
                   //' and mm.co_sit_mat not in (' + quotedStr('C') + ')';

      if strP_UF <> 'T' then
      begin
        SqlString := SqlString + ' and a.co_esta_alu = ' + quotedStr(strP_UF);
      end;

      if strP_CO_CIDADE <> 'T' then
      begin
        SQLString := SQLString + ' and a.co_cidade = ' + strP_CO_CIDADE;
      end;

      if strP_CO_BAIRRO <> 'T' then
      begin
        SQLString := SQLString + ' and a.co_bairro = ' + strP_CO_BAIRRO;
      end;

    // parâmetros para pegar legenda sobre etnia;

      if strP_TP_RACA <> 'T' then
        SqlString := SqlString + ' AND a.tp_raca = ' + strP_TP_RACA;

      if strP_RENDA <> 'T' then
        SqlString := SqlString + ' AND a.renda_familiar = ' + strP_RENDA;

      if strP_TP_DEF <> 'T' then
        SqlString := SqlString + ' AND a.tp_def = ' + strP_TP_DEF;

      if strP_BOLSA <> 'T' then
        SQLString := SQLString + ' and a.FLA_BOLSA_ESCOLA = ' + quotedStr(strP_BOLSA);

      if strP_PASSE <> 'T' then
        SQLString := SQLString + ' and a.FLA_PASSE_ESCOLA = ' + quotedStr(strP_PASSE);

      SqlString := SqlString + ' ORDER BY a.co_esta_alu, c.co_cidade, b.co_bairro,a.no_alu ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDistAluCar.Create(Nil);

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
        Relatorio.QRLParametros.Caption := strParamRel;

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
  DLLRelDistAluCar;

end.
