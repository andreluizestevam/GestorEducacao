library WR_RelTarefIncons;

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
  U_FrmRelTarefIncons in 'Relatorios\U_FrmRelTarefIncons.pas' {FrmRelTarefIncons};

function DLLRelTarefIncons(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_RESP, strP_CO_SOLIC:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelTarefIncons;
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

      SQLString := 'set language portuguese select tar.*, res.no_col as responsavel, res.co_mat_col as matres, sol.no_col as solicitante, sol.co_mat_col as matsol, emp.no_fantas_emp, sagend.DE_SITU_TAREF_AGEND, ' +
                    ' pagend.DE_PRIOR_TAREF_AGEND, emp.sigla as siglaResp, empSol.sigla as siglaSol  ' +
                    ' from tb137_tarefas_agenda tar ' +
                    ' join tb03_colabor res on tar.co_col = res.co_col and res.co_emp = tar.co_emp ' +
                    ' join tb03_colabor sol on sol.co_col = tar.co_funci_solic_taref_agend and sol.co_emp = tar.co_emp_solic_taref_agend ' +
                    ' join tb25_empresa emp on emp.co_emp = tar.co_emp ' +
                    ' join TB139_SITU_TAREF_AGEND sagend on sagend.CO_SITU_TAREF_AGEND = tar.CO_SITU_TAREF_AGEND ' +
                    ' join TB140_PRIOR_TAREF_AGEND pagend on pagend.CO_PRIOR_TAREF_AGEND = tar.CO_PRIOR_TAREF_AGEND' +
                    ' join tb25_empresa empSol on empSol.co_emp = tar.co_emp_solic_taref_agend ' +
                    ' where tar.co_emp = ' + strP_CO_EMP +
                    ' and tar.DT_LIMIT_TAREF_AGEND < ' + QuotedStr(FormatDateTime('dd/MM/yyyy',Now) + ' 00:00:00') + 
                    ' and tar.CO_COL = ' + strP_CO_RESP +
                    ' and tar.CO_SITU_TAREF_AGEND not in (' + quotedStr('CO') + ',' + quotedStr('CR') + ',' + quotedStr('TF') + ')';

        if strP_CO_SOLIC <> 'T' then
        begin
          SQLString := SQLString + ' and tar.co_funci_solic_taref_agend = ' + strP_CO_SOLIC;
        end;

        SQLString := SQLString + ' order by tar.CO_CHAVE_UNICA_TAREF,tar.DT_COMPR_TAREF_AGEND';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelTarefIncons.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;

        Relatorio.QrlParametros.Caption := strParamRel;

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
  DLLRelTarefIncons;

end.
