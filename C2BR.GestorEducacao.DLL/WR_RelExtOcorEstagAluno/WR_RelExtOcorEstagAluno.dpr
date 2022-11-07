library WR_RelExtOcorEstagAluno;

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
  U_FrmRelExtOcorEstagAluno in 'Relatorios\U_FrmRelExtOcorEstagAluno.pas' {FrmRelExtOcorEstagAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelExtOcorEstagAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_ID_EFETI_ESTAGIO, strP_TP_OCORR: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelExtOcorEstagAluno;
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

      SqlString := 'SET LANGUAGE PORTUGUESE     '+
               ' Select  OCE.DT_OCORR,          '+
               '         OCE.TP_AVAL,           '+
               '         OCE.DE_OCORR,          '+
               '         OCE.DE_OBS_OCORR,      '+
               '         OCE.ORIGEM_OCORR,      '+
               '         OCE.DT_CADASTRO,       '+
               '         CE.ID_CANDID_ESTAG,    '+
               '         CE.NO_CANDID_ESTAG,    '+
               '         CE.TP_CANDID_ESTAG,    '+
               '         OE.DE_VAGA_OFERT_ESTAG, '+
               '         CE.CO_ALU,CE.CO_EMP_ALU,'+
               '         CE.CO_COL,CE.CO_EMP_COL'+
               ' From TB219_CANDID_OFERTAS CO   '+
               ' JOIN TB218_CANDID_ESTAGIO CE on CO.ID_CANDID_ESTAG = CE.ID_CANDID_ESTAG'+
               ' JOIN TB188_OFERT_ESTAG OE on CO.CO_OFERT_ESTAG = OE.CO_OFERT_ESTAG'+
               ' JOIN TB221_EFETI_ESTAGIO EE on EE.ID_CANDID_OFERTAS = CO.ID_CANDID_OFERTAS'+
               ' JOIN TB222_OCORR_ESTAGIO OCE on OCE.ID_EFETI_ESTAGIO = EE.ID_EFETI_ESTAGIO'+
               ' Where OE.CO_EMP = '+ strP_CO_EMP +
               '   AND OCE.ORIGEM_OCORR = '+ QuotedStr(strP_TP_OCORR) +
               ' AND OCE.ID_EFETI_ESTAGIO = ' + strP_ID_EFETI_ESTAGIO;

     SqlString := SqlString + ' ORDER BY CE.ID_CANDID_ESTAG, OE.DE_VAGA_OFERT_ESTAG';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelExtOcorEstagAluno.Create(Nil);

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

        if strP_TP_OCORR = 'E' then
          Relatorio.LblTituloRel.Caption := 'EXTRATO DE OCORRÊNCIAS - ESTÁGIO / EMPRESA'
        else
          Relatorio.LblTituloRel.Caption := 'EXTRATO DE OCORRÊNCIAS - ESTÁGIO / ESCOLA';

        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        //Relatorio.QuickRep1.ShowProgress := False;
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
  DLLRelExtOcorEstagAluno;

end.
