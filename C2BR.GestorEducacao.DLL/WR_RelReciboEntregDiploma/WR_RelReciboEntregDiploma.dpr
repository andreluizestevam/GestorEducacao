library WR_RelReciboEntregDiploma;

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
  U_FrmRelReciboEntregDiploma in 'Relatorios\U_FrmRelReciboEntregDiploma.pas' {FrmRelReciboEntregDiploma};

// Controle Administrativo > Controle Frequencia Funcionário
// Relatório: EMISSÃO DA CURVA ABC DE FREQÜÊNCIA DE FUNCIONÁRIOS ***Função*** (Ponto Padrão/Livre)
// STATUS: OK
function DLLRelReciboEntregDiploma(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_ID_ENTR_DOCUM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelReciboEntregDiploma;
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

      // Monta a Consulta do Relatório.
      SqlString := 'Select U.*,a.no_alu, a.nu_nire, ed.DT_ENTREGA, ed.TP_USU, ed.CO_USU as usuEnt, ed.NO_RESP_ENTR_DOCUM,' +
                   'ed.NU_TELE_RESP_ENTR_DOCUM, m.DE_MODU_CUR, c.NO_CUR, sd.TP_USU_SOLIC, sd.CO_USU as usuSol, sd.NO_RESP_SOLIC_DIPLOMA, ' +
                   'sd.CO_RG_RESP_SOLIC_DIPLOMA, sd.NU_TELE_RESP_SOLIC_DIPLOMA, ed.CO_RG_RESP_ENTR_DOCUM, sd.CO_EMP_ALU, sd.DT_SOLIC ' +
                   'from TB214_ENTR_DOCUMENTO ed ' +
                   'JOIN TB211_SOLIC_DIPLOMA sd on sd.ID_SOLIC_DIPLOMA = ed.ID_SOLIC_DIPLOMA ' +
                   'JOIN TB16_DIPLOMA U on ed.ID_SOLIC_DIPLOMA = U.ID_SOLIC_DIPLOMA ' +
                   'JOIN TB44_MODULO m on m.co_modu_cur = sd.co_modu_cur ' +
                   'JOIN tb01_curso c on c.co_emp = sd.co_emp_alu and c.co_modu_cur = sd.co_modu_cur and c.co_cur = sd.co_cur ' +
                   'join tb07_aluno a on a.co_emp = sd.co_emp_alu and a.co_alu = sd.co_alu ' +
                   'where ed.ID_ENTR_DOCUM = ' + strP_ID_ENTR_DOCUM;

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelReciboEntregDiploma.Create(Nil);

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
  DLLRelReciboEntregDiploma;

end.
