library WR_RelRelacPlanoContas;

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
  U_FrmRelRelacPlanoContas in 'Relatorios\U_FrmRelRelacPlanoContas.pas' {FrmRelRelacPlanoContas};

function DLLRelRelacPlanoContas(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_GRUP_CTA, strP_CO_SGRUP_CTA:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacPlanoContas;
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

      SqlString := 'select gc.DE_GRUP_CTA, sc.DE_SGRUP_CTA, p.DE_CONTA_PC, p.CO_GRUP_CTA, p.CO_SGRUP_CTA ' +
                   'from TB56_PLANOCTA p ' +
                   'join TB53_GRP_CTA gc on gc.CO_GRUP_CTA = p.CO_GRUP_CTA ' +
                   'join TB54_SGRP_CTA sc on p.CO_SGRUP_CTA = sc.CO_SGRUP_CTA and p.CO_GRUP_CTA = sc.CO_GRUP_CTA ';

      if strP_CO_GRUP_CTA <> 'T' then
      begin
        SQLString := SQLString + ' and p.CO_GRUP_CTA = ' + strP_CO_GRUP_CTA;
      end;

      if strP_CO_SGRUP_CTA <> 'T' then
      begin
        SQLString := SQLString + ' and p.CO_SGRUP_CTA = ' + strP_CO_SGRUP_CTA;
      end;

      SQLString := SQLString + 'order by p.CO_GRUP_CTA, p.CO_SGRUP_CTA, p.DE_CONTA_PC';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacPlanoContas.Create(Nil);

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
  DLLRelRelacPlanoContas;

end.
