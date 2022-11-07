library WR_RelMapaPerfilSalaAulaAluno;

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
  U_FrmRelMapaPerfilSalaAulaAluno in 'Relatorios\U_FrmRelMapaPerfilSalaAulaAluno.pas' {FrmRelMapaPerfilSalaAulaAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaPerfilSalaAulaAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_TIPO_SALA:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaPerfilSalaAulaAluno;
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

      SQLString := ' select distinct upv.*, e.NO_FANTAS_EMP'+
                   ' from TB248_UNIDADE_SALAS_AULA upv ' +
                   ' join TB25_empresa e on e.CO_EMP = upv.CO_EMP ' +
                   ' where 1 = 1 ';

      if strP_CO_EMP_REF <> 'T' then
      begin
        SQLString := SQLString + ' and upv.CO_EMP = ' + strP_CO_EMP_REF;
      end;

      if strP_CO_TIPO_SALA <> 'T' then
      begin
        SQLString := SQLString + ' and upv.CO_TIPO_SALA_AULA = ' + QuotedStr(strP_CO_TIPO_SALA);
      end;

      SQLString := SQLString + ' order by e.NO_FANTAS_EMP, upv.CO_TIPO_SALA_AULA, upv.CO_IDENTI_SALA_AULA';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaPerfilSalaAulaAluno.Create(Nil);

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
          Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString
        else
          Relatorio.QRLParam.Caption := '( Unidade: Todas';

        if strP_CO_TIPO_SALA <> 'T' then
        begin
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Tipo de Sala de Aula: ';

          if strP_CO_TIPO_SALA = 'A' then
            Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Aula )'
          else if strP_CO_TIPO_SALA = 'L' then
            Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Laboratório )'
          else if strP_CO_TIPO_SALA = 'E' then
            Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Estudo )'
          else if strP_CO_TIPO_SALA = 'M' then
            Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Monitoria )'
          else
            Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + 'Outros )';
        end
        else
          Relatorio.QRLParam.Caption := Relatorio.QRLParam.Caption + ' - Tipo de Sala de Aula: Todas )';

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
  DLLRelMapaPerfilSalaAulaAluno;

end.
