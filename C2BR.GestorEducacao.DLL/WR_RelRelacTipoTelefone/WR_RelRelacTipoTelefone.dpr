library WR_RelRelacTipoTelefone;

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
  U_FrmRelRelacTipoTelefone in 'Relatorios\U_FrmRelRelacTipoTelefone.pas' {FrmRelRelacTipoTelefone};

function DLLRelRelacTipoTelefone(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_ID_TIPO_TELEFONE, strP_CO_SITU:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacTipoTelefone;
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

      SqlString := 'select TP.* ' +
                  'from TB239_TIPO_TELEFONE TP'+
                  ' where 1 = 1 ';

      if strP_ID_TIPO_TELEFONE <> 'T' then
        SQLString := SQLString + ' and TP.ID_TIPO_TELEFONE = ' + strP_ID_TIPO_TELEFONE;

      if strP_CO_SITU <> 'T' then
        SQLString := SQLString + ' and TP.CO_SITUACAO = ' + QuotedStr(strP_CO_SITU);

      SQLString := SQLString + ' order by TP.NM_TIPO_TELEFONE';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelRelacTipoTelefone.Create(Nil);

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

        if strP_ID_TIPO_TELEFONE <> 'T' then
        begin
          if strP_CO_SITU <> 'T' then
          begin
            if strP_CO_SITU <> 'I' then
              Relatorio.QRLParam.Caption := '( Tipo Telefone: ' + Relatorio.QryRelatorio.FieldByName('NM_TIPO_TELEFONE').AsString + ' - Situa��o: Ativo )'
            else
              Relatorio.QRLParam.Caption := '( Tipo Telefone: ' + Relatorio.QryRelatorio.FieldByName('NM_TIPO_TELEFONE').AsString + ' - Situa��o: Inativo )';
          end
          else
            Relatorio.QRLParam.Caption := '( Tipo Telefone: ' + Relatorio.QryRelatorio.FieldByName('NM_TIPO_TELEFONE').AsString + ' - Situa��o: Todas )';
        end
        else
        begin
          if strP_CO_SITU <> 'T' then
          begin
            if strP_CO_SITU <> 'I' then
              Relatorio.QRLParam.Caption := '( Tipo Telefone: Todas - Situa��o: Ativo )'
            else
              Relatorio.QRLParam.Caption := '( Tipo Telefone: Todas - Situa��o: Inativo )';
          end
          else
            Relatorio.QRLParam.Caption := '( Tipo Telefone: Todas - Situa��o: Todas )';
        end;

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
  DLLRelRelacTipoTelefone;

end.