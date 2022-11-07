library WR_RelComprMatric;

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
  U_FrmRelComprMatric in 'Relatorios\U_FrmRelComprMatric.pas' {FrmRelComprMatric};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
//function DLLRelComprMatric(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ALU, strP_CO_MODU_CUR, strP_CO_CUR,
//                           strP_CO_ANO_MES_MAT:PChar): Integer; export; cdecl;
function DLLRelComprMatric(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ALU_CAD:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelComprMatric;
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

      SqlString := 'select R.CO_EMP,R.DT_EFE_MAT,R.CO_ALU_CAD,R.CO_ANO_MES_MAT, c.no_col, c.co_mat_col, m.de_modu_cur, cu.no_cur,ci.no_cidade as cidAluno, b.no_bairro as baiAluno,'+
                   'ci.CO_UF as UFAlu,ci.no_cidade as cidResp, b.no_bairro as baiResp, ci.CO_UF as UFResp, ct.no_turma,' +
                   'a.no_alu,a.DT_NASC_ALU,a.CO_SEXO_ALU,a.DE_ENDE_ALU,a.NU_ENDE_ALU,a.DE_COMP_ALU,a.NU_TELE_RESI_ALU,a.NU_TELE_CELU_ALU,' +
                   're.NO_RESP,re.NU_CPF_RESP,re.DE_ENDE_RESP,re.NU_ENDE_RESP,re.DE_COMP_RESP,re.NU_TELE_RESI_RESP,re.NU_TELE_CELU_RESP ' +
                   'from TB08_MATRCUR R ' +
                   'join tb07_aluno a on R.co_alu = a.co_alu ' +
                   'join tb108_responsavel re on re.co_resp = a.co_resp ' +
                   'left join tb03_colabor c on c.co_col = R.CO_COL and c.co_emp = R.CO_EMP ' +
                   'join tb44_modulo m on m.co_modu_cur = R.co_modu_cur ' +
                   'join tb01_curso cu on cu.co_cur = R.co_cur and cu.co_modu_cur = R.co_modu_cur and cu.co_emp = R.co_emp ' +
                   'join tb129_cadturmas ct on ct.co_tur = R.co_tur ' +
                   ' left join tb904_cidade ci on ci.co_cidade = A.CO_CIDADE ' +
                   ' left join tb905_bairro b on b.co_bairro = A.CO_BAIRRO and b.co_cidade = A.CO_CIDADE ' +
                   ' left join tb904_cidade ciR on ciR.co_cidade = re.CO_CIDADE ' +
                   ' left join tb905_bairro bR on bR.co_bairro = re.CO_BAIRRO and bR.co_cidade = re.CO_CIDADE ' +
                   ' where R.CO_EMP = ' + strP_CO_EMP +
                   ' and R.CO_ALU_CAD = ' + QuotedStr(strP_CO_ALU_CAD);

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelComprMatric.Create(Nil);

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
        //Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
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
  DLLRelComprMatric;

end.
