library WR_RelMapaCaracteristicaMatricula;

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
  U_FrmRelMapaCaracteristicaMatricula in 'Relatorios\U_FrmRelMapaCaracteristicaMatricula.pas' {FrmRelMapaCaracteristicaMatricula};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelMapaCaracteristicaMatricula(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ANO_REF: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaCaracteristicaMatricula;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath: string;
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

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelMapaCaracteristicaMatricula.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Text := StringReplace(Relatorio.QryRelatorio.SQL.Text,'substitui',strP_CO_EMP,[rfReplaceAll]);
      Relatorio.QryRelatorio.SQL.Text := StringReplace(Relatorio.QryRelatorio.SQL.Text,'anoSelecionado',strP_CO_ANO_REF,[rfReplaceAll]);
      Relatorio.QryRelatorio.Open;

      if Relatorio.QryRelatorio.IsEmpty then
      begin
        // Retorna -1 para Sem Registros na Consulta.
        intReturn := -1;
      end
      else
      begin
        // Atualiza Campos do Relat�rio Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.QRLParam.Caption := 'Unidade: ' + Relatorio.QryCabecalhoRel.FieldByName('NO_FANTAS_EMP').AsString +
        ' - Ano Refer�ncia: ' + strP_CO_ANO_REF;

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
  DLLRelMapaCaracteristicaMatricula;

end.
