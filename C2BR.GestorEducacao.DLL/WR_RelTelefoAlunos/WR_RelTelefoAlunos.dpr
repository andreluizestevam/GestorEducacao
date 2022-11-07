library WR_RelTelefoAlunos;

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
  U_FrmRelTelefoAlunos in 'Relatorios\U_FrmRelTelefoAlunos.pas' {FrmRelTelefoAlunos};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelTelefoAlunos(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ALU:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelTelefoAlunos;
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

      SqlString := 'set language portuguese select AE.*, AL.NO_ALU, EM.sigla, TE.CO_TIPO_TELEFONE '+
                   'from TB242_ALUNO_TELEFONE AE ' +
                   'join TB07_ALUNO AL on AL.CO_EMP = AE.CO_EMP and AL.CO_ALU = AE.CO_ALU ' +
                   'join TB25_EMPRESA EM on EM.CO_EMP = AE.CO_EMP ' +
                   'join TB239_TIPO_TELEFONE TE on TE.ID_TIPO_TELEFONE = AE.ID_TIPO_TELEFONE' +
                   ' where 1 = 1';

      if strP_CO_ALU <> 'T' then
        SqlString := SqlString + ' and AE.CO_ALU = ' + strP_CO_ALU;

      if strP_CO_EMP <> 'T' then
        SqlString := SqlString + ' and AE.CO_EMP = ' + strP_CO_EMP;

      SqlString := SqlString +
                   '  order by AL.NO_ALU, EM.sigla';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelTelefoAlunos.Create(Nil);

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

        if strP_CO_ALU <> 'T' then
          Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('sigla').AsString + ' - Aluno: ' + Relatorio.QryRelatorio.FieldByName('NO_ALU').AsString + ' )'
        else
          Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('sigla').AsString + ' - Aluno: Todos )';

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
  DLLRelTelefoAlunos;

end.
