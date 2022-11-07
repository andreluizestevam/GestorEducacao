library WR_RelDemonstrativoMatricula;

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
  U_FrmRelDemonstrativoMatricula in 'Relatorios\U_FrmRelDemonstrativoMatricula.pas' {FrmRelDemonstrativoMatricula};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
// STATUS: OK
function DLLRelDemonstrativoMatricula(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR,
 strP_CO_CUR, strP_CO_TUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDemonstrativoMatricula;
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

      SqlString := ' SET LANGUAGE PORTUGUESE                  '+
               'SELECT c.no_cur,m.de_modu_cur,r.*,ci.no_cidade,b.no_bairro,a.nu_cpf_alu,a.no_alu,' +
               'a.dt_nasc_alu,a.nu_tele_resi_alu,a.co_sexo_alu,re.NU_CPF_RESP, re.NU_TELE_CELU_RESP, re.NO_RESP, re.CO_SEXO_RESP ' +
               ' FROM tb08_matrcur r ' +
               'JOIN tb07_aluno a ON a.co_alu = r.co_alu ' +
               'JOIN tb108_responsavel re ON re.co_resp = a.co_resp ' +
               'JOIN tb01_curso c ON c.co_cur = r.co_cur ' +
               'JOIN tb44_modulo m ON m.co_modu_cur = r.co_modu_cur ' +
               'JOIN tb904_cidade ci ON ci.co_cidade = a.co_cidade ' +
               'JOIN tb905_bairro b ON b.co_bairro = a.co_bairro and b.co_cidade = a.co_cidade ' +
               ' WHERE r.co_emp = '+ strP_CO_EMP +
               ' and c.co_emp = r.co_emp ' +
               ' AND   r.DT_EFE_MAT between ' + quotedStr(strP_DT_INI + ' 00:00:00') + ' AND  ' + quotedStr(strP_DT_FIM +' 23:59:59');

      if strP_CO_MODU_CUR <> nil then
        SqlString := SqlString + ' AND r.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> nil then
          SqlString := SqlString + ' AND r.CO_CUR = ' + strP_CO_CUR;

      if strP_CO_TUR <> nil then
        SqlString := SqlString + ' AND r.CO_TUR = ' + strP_CO_TUR;

      if strP_CO_SIT_MAT <> 'U' then
        SqlString := SqlString + ' AND r.CO_SIT_MAT = ' + QuotedStr(strP_CO_SIT_MAT);

      SqlString := SqlString + ' ORDER BY c.no_cur,a.no_alu';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDemonstrativoMatricula.Create(Nil);

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
        Relatorio.qrlParametros.Caption := strParamRel;

        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLNumPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relatório Gerado com Sucesso.
        intReturn := 1;
      end;

    Except
      on E : Exception do
        //intReturn := 0;
        ShowMessage(E.ClassName + ' error raised, with message : ' + E.Message);
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
  DLLRelDemonstrativoMatricula;

end.
