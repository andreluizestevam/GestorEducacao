library WR_RelDemonstrativoInscricao;

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
  U_FrmRelDemonstrativoInscricao in 'Relatorios\U_FrmRelDemonstrativoInscricao.pas' {FrmRelDemonstrativoInscricao};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
// STATUS: OK
function DLLRelDemonstrativoInscricao(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR,
 strP_CO_CUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDemonstrativoInscricao;
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
        ' SELECT R.NU_TELE_RESI_RESP, R.NO_RESP, R.NU_CPF_RESP, R.CO_SEXO_RESP, R.DT_NASC_RESP, B.NO_BAIRRO, C.NO_CIDADE, U.DESCRICAOUF, CUR.NO_CUR, I.*,mo.de_modu_cur FROM tb46_inscricao i ' +
        ' JOIN tb108_responsavel r ON i.co_resp = r.co_resp ' +
        ' JOIN tb904_cidade c ON i.co_cidade = c.co_cidade ' +
        ' JOIN tb905_bairro b ON b.co_bairro = i.co_bairro and b.co_cidade = r.co_cidade ' +
        ' JOIN tb45_opcao_insc o ON o.nu_insc_alu = i.nu_insc_alu ' +
        ' JOIN tb74_UF u ON u.coduf = i.co_esta_alu and u.coduf = r.co_esta_resp ' +
        ' JOIN tb01_curso cur ON cur.co_cur = o.co_cur ' +
        ' JOIN tb44_modulo mo on mo.co_modu_cur = i.co_modu_cur ' +
        ' where cur.co_emp = ' + strP_CO_EMP +
        ' and i.co_emp = ' + strP_CO_EMP +
        ' and i.co_modu_cur = ' + strP_CO_MODU_CUR +
        ' and i.co_emp = o.co_emp ' +
        ' and i.co_alu = o.co_alu ' +
        ' and i.dt_insc_alu >= ' + quotedStr(strP_DT_INI) +
        ' and i.dt_insc_alu <= ' + QuotedStr(strP_DT_FIM);

      if strP_CO_CUR <> nil then
        SqlString := SqlString + ' and o.co_cur = ' + strP_CO_CUR;

      if strP_CO_SIT_MAT <> 'T' then
        SqlString := SqlString + ' AND i.CO_SITU_INS = ' + QuotedStr(strP_CO_SIT_MAT);

      SqlString := SqlString + ' ORDER BY cur.no_cur,i.no_alu';


      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDemonstrativoInscricao.Create(Nil);

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
  DLLRelDemonstrativoInscricao;

end.
