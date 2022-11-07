library WR_RelDemonstrativoReserva;

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
  U_FrmRelDemonstrativoReserva in 'Relatorios\U_FrmRelDemonstrativoReserva.pas' {FrmRelDemonstrativoReserva};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
// STATUS: OK
function DLLRelDemonstrativoReserva(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR,
 strP_CO_CUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDemonstrativoReserva;
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
                 'SELECT c.no_cur,m.de_modu_cur,b.no_bairro,d.no_cidade,RE.NU_TELE_RESI_RESP,r.NU_TEL_RESP,re.no_resp as no_responsavel,re.nu_cpf_resp as nu_cpf_responsavel,re.co_sexo_resp,'+
                 'r.*, a.no_alu as no_aluno, a.DT_NASC_ALU as dt_nascimento, a.co_sexo_alu as sexo_alu FROM TB052_RESERV_MATRI r ' +
                 'LEFT JOIN tb108_responsavel re ON re.co_resp = r.co_resp '+
                 'LEFT JOIN tb07_ALUNO a ON a.co_alu = r.co_alu and a.co_emp = r.co_emp_alu '+
                 'JOIN tb01_curso c ON c.co_cur = r.co_cur '+
                 'JOIN tb44_modulo m ON m.co_modu_cur = r.co_modu_cur '+
                 'left JOIN tb905_bairro b ON b.co_bairro = r.CO_BAIRRO_ALU and b.co_cidade = r.CO_CIDADE_ALU '+
                 'left JOIN tb904_cidade d ON d.co_cidade = r.CO_CIDADE_ALU '+
                 ' WHERE r.co_emp1 = '+ strP_CO_EMP +
                 ' and c.co_emp = r.co_emp1 ' +
                 ' and c.co_modu_cur = ' + strP_CO_MODU_CUR +
                 ' AND   r.DT_CADASTRO BETWEEN ' + quotedStr(strP_DT_INI) + ' AND   ' + QuotedStr(strP_DT_FIM);

    if strP_CO_CUR <> nil then
      SqlString := SqlString + ' AND r.CO_CUR = ' + strP_CO_CUR;

    if strP_CO_SIT_MAT <> 'T' then
      SqlString := SqlString + ' AND r.CO_SIT_RESMAT = ' + quotedStr(strP_CO_SIT_MAT);

    SqlString := SqlString + ' ORDER BY c.no_cur,r.no_alu';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDemonstrativoReserva.Create(Nil);

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
  DLLRelDemonstrativoReserva;

end.
