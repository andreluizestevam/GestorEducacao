library WR_RelAnaliticoResAvaliacao;

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
  U_FrmRelAnaliticoResAvaliacao in 'Relatorios\U_FrmRelAnaliticoResAvaliacao.pas' {FrmRelAnaliticoResAvaliacao};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
// STATUS: OK
function DLLRelAnaliticoResAvaliacao(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_TIPO_AVAL, strP_CO_PESQ_AVAL,
 strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_DT_INI, strP_DT_FIM, strP_CO_COL,
 strP_NO_CUR, strP_NO_TUR, strP_NO_MAT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAnaliticoResAvaliacao;
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

      SqlString := 'SELECT T.*, TA.NO_TIPO_AVAL '+
               'FROM TB72_TIT_QUES_AVAL T, TB73_TIPO_AVAL TA ' +
               'WHERE T.CO_TIPO_AVAL =  TA.CO_TIPO_AVAL ';

      if strP_CO_TIPO_AVAL <> '' then
        SqlString := SqlString + ' And TA.CO_TIPO_AVAL = ' + strP_CO_TIPO_AVAL;

      // Cria uma instância do Relatório.
      Relatorio := TFrmRelAnaliticoResAvaliacao.Create(Nil);

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
        Relatorio.QrlNrPesq.Caption := strP_CO_PESQ_AVAL;
        Relatorio.codCurso := strP_CO_CUR;
        Relatorio.QrlNoCurso.Caption := strP_NO_CUR;
        Relatorio.CodTurma := strP_CO_TUR;
        Relatorio.QrlNoTurma.Caption := strP_NO_TUR;
        Relatorio.codDisciplina := strP_CO_MAT;
        Relatorio.QrlNoDisciplina.Caption := strP_NO_MAT;
        Relatorio.QrlProfessor.Caption := strP_CO_COL;
        Relatorio.codModulo := strP_CO_MODU_CUR;
        Relatorio.QrlDataInicial.Caption := strP_DT_INI;
        Relatorio.QrlDataFinal.Caption := strP_DT_FIM;
        Relatorio.codigoEmpresa := strP_CO_EMP;

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
  DLLRelAnaliticoResAvaliacao;

end.
