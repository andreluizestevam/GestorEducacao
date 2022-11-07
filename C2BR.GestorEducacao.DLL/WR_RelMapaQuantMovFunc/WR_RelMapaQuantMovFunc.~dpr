library WR_RelMapaQuantMovFunc;

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
  U_FrmRelMapaQuantMovFunc in 'Relatorios\U_FrmRelMapaQuantMovFunc.pas' {FrmRelMapaQuantMovFunc};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
// STATUS: OK
function DLLRelMapaQuantMovFunc(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_REF, strP_ANO_REFER: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaQuantMovFunc;
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

      SqlString := ' SELECT DISTINCT E.CO_EMP, E.NO_FANTAS_EMP  '+
                   'FROM TB25_EMPRESA E ';

      if strP_CO_EMP_REF <> 'T' then
        SqlString := SqlString + ' WHERE E.CO_EMP = ' + strP_CO_EMP_REF;

      SQLString := SQLString + ' ORDER BY E.NO_FANTAS_EMP ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaQuantMovFunc.Create(Nil);

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
        //Relatorio.codigoEmpresa := strP_CO_EMP_REF;
        Relatorio.QRLAno5.Caption := strP_ANO_REFER;
        Relatorio.QRLAno4.Caption := IntToStr(strtoint(strP_ANO_REFER) - 1);
        Relatorio.QRLAno3.Caption := IntToStr(strtoint(strP_ANO_REFER) - 2);
        Relatorio.QRLAno2.Caption := IntToStr(strtoint(strP_ANO_REFER) - 3);
        Relatorio.QRLAno1.Caption := IntToStr(strtoint(strP_ANO_REFER) - 4);

        if strP_CO_EMP_REF <> 'T' then
          Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString + ' )'
        else
          Relatorio.QRLParam.Caption := '( Unidade: Todas )';

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
  DLLRelMapaQuantMovFunc;

end.
