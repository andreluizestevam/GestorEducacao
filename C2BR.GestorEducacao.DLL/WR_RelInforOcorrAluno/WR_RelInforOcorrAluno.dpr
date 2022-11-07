library WR_RelInforOcorrAluno;

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
  U_FrmRelInforOcorrAluno in 'Relatorios\U_FrmRelInforOcorrAluno.pas' {FrmRelInforOcorrAluno};

// Controle Administrativo > Controle Frequencia Funcion�rio
// Relat�rio: EMISS�O DA CURVA ABC DE FREQ��NCIA DE FUNCION�RIOS ***Fun��o*** (Ponto Padr�o/Livre)
// STATUS: OK
function DLLRelInforOcorrAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_ALU, strP_CO_ALU, strP_CO_SIGL_OCORR, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelInforOcorrAluno;
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

      // Monta a Consulta do Relat�rio.
      SqlString := 'set language portuguese select oa.DT_OCORR, oa.DE_OCORR, e.sigla, e.no_fantas_emp, alu.no_alu, tpOco.DE_TIPO_OCORR from TB191_OCORR_ALUNO oa ' +
                   ' join tb07_aluno alu on alu.co_emp = oa.co_emp and alu.co_alu = oa.co_alu ' +
                   ' join tb25_empresa e on e.co_emp = oa.co_emp ' +
                   ' join TB150_TIPO_OCORR tpOco on tpOco.CO_SIGL_OCORR = oa.CO_SIGL_OCORR ' +
                   'where oa.CO_EMP = ' + strP_CO_EMP_ALU +
                   ' and oa.CO_ALU = ' + strP_CO_ALU +
                   ' and oa.DT_OCORR between ' + QuotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM) ;

      if strP_CO_SIGL_OCORR <> 'T' then
      begin
        SqlString := SqlString + ' and oa.CO_SIGL_OCORR = ' + QuotedStr(strP_CO_SIGL_OCORR);
      end;

      SQLString := SqlString + ' order by oa.DT_OCORR';

      // Cria uma inst�ncia do Relat�rio.
      Relatorio := TFrmRelInforOcorrAluno.Create(Nil);

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

        if strP_CO_SIGL_OCORR <> 'T' then
          Relatorio.QRLParam.Caption := 'Unidade: ' + Relatorio.QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString +
          ' - Aluno: ' + Relatorio.QryRelatorio.FieldByName('NO_ALU').AsString + ' - Per�odo: ' + strP_DT_INI + ' at� ' + strP_DT_FIM
        else
          Relatorio.QRLParam.Caption := 'Unidade: ' + Relatorio.QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString +
          ' - Aluno: ' + Relatorio.QryRelatorio.FieldByName('NO_ALU').AsString + ' - Tipo Ocorr�ncia: Todas - Per�odo: ' + strP_DT_INI + ' at� ' + strP_DT_FIM;

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
  DLLRelInforOcorrAluno;

end.
