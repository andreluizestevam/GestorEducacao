library WR_RelMapaPerFilOcupAluno;

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
  U_Funcoes in '..\General\U_Funcoes.pas',
  U_FrmRelMapaPerFilOcupAluno in 'Relatorios\U_FrmRelMapaPerFilOcupAluno.pas' {FrmRelMapaPerFilOcupAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaPerFilOcupAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_ANO_REF:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaPerFilOcupAluno;
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

      SQLString := ' select distinct upv.*, e.sigla'+
                   ' from TB246_UNIDADE_PERFIL_VAGAS upv ' +
                   ' join TB25_empresa e on e.CO_EMP = upv.CO_EMP ' +
                   ' where 1 = 1 ';

      if strP_CO_EMP_REF <> 'T' then
      begin
        SQLString := SQLString + ' and upv.CO_EMP = ' + strP_CO_EMP_REF;
      end;

      if strP_CO_ANO_REF <> 'T' then
      begin
        SQLString := SQLString + ' and upv.CO_ANO_PERFIL_VAGAS = ' + strP_CO_ANO_REF;
      end;

      SQLString := SQLString + ' order by e.sigla, upv.CO_ANO_PERFIL_VAGAS';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaPerFilOcupAluno.Create(Nil);

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
        
        if strP_CO_EMP_REF <> 'T' then
          Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('sigla').AsString
        else
          Relatorio.QRLParam.Caption := '( Unidade: Todas';

        if strP_CO_ANO_REF <> 'T' then
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Ano Referência: ' + strP_CO_ANO_REF + ' )'
        else
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Ano Referência: Todas )';

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
  DLLRelMapaPerFilOcupAluno;

end.
