library WR_RelComprReserMatric;

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
  U_FrmRelComprReserMatric in 'Relatorios\U_FrmRelComprReserMatric.pas' {FrmRelComprReserMatric};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelComprReserMatric(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_NU_RESERVA:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelComprReserMatric;
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

      SqlString := 'select R.*, e1.no_fantas_emp as noEsc1, e2.no_fantas_emp as noEsc2, e3.no_fantas_emp as noEsc3,'+
                   'mo.de_modu_cur, cu.no_cur, co.no_col, co.co_mat_col, b.no_bairro, c.no_cidade, c.CO_UF, br.no_bairro as no_bairro_resp, ' +
                   'cr.no_cidade as no_cidade_resp, cr.co_uf as co_uf_resp, e1.CO_UF_EMP, ce.NO_CIDADE as cidadeUnid ' +
                   ' from TB052_RESERV_MATRI R ' +
                   ' join tb25_empresa e1 on e1.co_emp = R.co_emp1' +
                   ' left join tb25_empresa e2 on e2.co_emp = R.co_emp2' +
                   ' left join tb25_empresa e3 on e3.co_emp = R.co_emp3' +
                   ' join tb44_modulo mo on mo.co_modu_cur = R.co_modu_cur ' +
                   ' join tb01_curso cu on cu.co_emp = R.co_emp1 and cu.co_modu_cur = R.co_modu_cur and cu.co_cur = R.co_cur ' +
                   ' join tb03_colabor co on co.co_col = R.CO_COL_RESP_CAD and co.co_emp = R.CO_EMP_COL_RESP_CAD ' +
                   ' LEFT JOIN TB904_CIDADE C on R.CO_CIDADE_ALU = C.CO_CIDADE ' +
                   ' LEFT JOIN TB905_BAIRRO B on B.CO_CIDADE = R.CO_CIDADE_ALU and B.CO_BAIRRO = R.CO_BAIRRO_ALU ' +
                   ' LEFT JOIN TB904_CIDADE cr on R.CO_CIDADE_RESP = cr.CO_CIDADE ' +
                   ' LEFT JOIN TB905_BAIRRO br on br.CO_CIDADE = R.CO_CIDADE_RESP and br.CO_BAIRRO = R.CO_BAIRRO_RESP ' +
                   ' LEFT JOIN TB904_CIDADE ce on e1.CO_CIDADE = ce.CO_CIDADE ' +
                   ' where R.CO_EMP1 = ' + strP_CO_EMP +
                   ' and R.NU_RESERVA = ' + quotedStr(strP_NU_RESERVA);

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelComprReserMatric.Create(Nil);

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
  DLLRelComprReserMatric;

end.
