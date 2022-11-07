library WR_RelEndereAlunos;

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
  U_FrmRelEndereAlunos in 'Relatorios\U_FrmRelEndereAlunos.pas' {FrmRelEndereAlunos};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelEndereAlunos(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ALU:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelEndereAlunos;
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

      SqlString := 'set language portuguese select AE.*, AL.NO_ALU, BA.NO_BAIRRO, CI.NO_CIDADE, EM.NO_FANTAS_EMP, TE.CO_TIPO_ENDERECO, CI.CO_UF '+
                   'from TB241_ALUNO_ENDERECO AE ' +
                   'join TB07_ALUNO AL on AL.CO_EMP = AE.CO_EMP and AL.CO_ALU = AE.CO_ALU ' +
                   'join TB25_EMPRESA EM on EM.CO_EMP = AE.CO_EMP ' +
                   'join TB904_CIDADE CI on CI.CO_CIDADE = AE.CO_CIDADE ' +
                   'join TB905_BAIRRO BA on BA.CO_BAIRRO = AE.CO_BAIRRO AND BA.CO_BAIRRO = AE.CO_BAIRRO ' +
                   'join TB238_TIPO_ENDERECO TE on TE.ID_TIPO_ENDERECO = AE.ID_TIPO_ENDERECO' +
                   ' where 1 = 1';

      if strP_CO_ALU <> 'T' then
        SqlString := SqlString + ' and AE.CO_ALU = ' + strP_CO_ALU;

      if strP_CO_EMP <> 'T' then
        SqlString := SqlString + ' and AE.CO_EMP = ' + strP_CO_EMP;

      SqlString := SqlString +
                   '  order by AL.NO_ALU, EM.sigla';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelEndereAlunos.Create(Nil);

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

        if strP_CO_ALU <> 'T' then
          Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString + ' )'
        else
          Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString + ' - Aluno: Todos )';

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
  DLLRelEndereAlunos;

end.