library WR_relCurvaABCFreq;

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
  U_FrmRelCurvaABCFreq in 'Relatorios\U_FrmRelCurvaABCFreq.pas' {FrmrelCurvaABCFreq};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLrelCurvaABCFreq(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_MODU_CUR, strP_DE_MODU_CUR, strP_CO_CUR, strP_DE_CUR,
 strP_CO_ANO_REF, strP_TP_PRESENCA, strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmrelCurvaABCFreq;
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

      SqlString := ' SET LANGUAGE PORTUGUESE ' +
                   ' SELECT DISTINCT B.CO_MAT, CM.NO_MATERIA,GC.CO_CUR,GC.CO_MODU_CUR, GC.CO_ANO_GRADE '+
                   ' FROM TB43_GRD_CURSO GC ' +
                   ' JOIN TB02_MATERIA B on GC.CO_EMP = B.CO_EMP and GC.CO_MAT = B.CO_MAT and GC.CO_CUR = B.CO_CUR '+
                   ' JOIN TB107_CADMATERIAS CM on B.ID_MATERIA = CM.ID_MATERIA' +
                   ' WHERE GC.CO_CUR = ' + strP_CO_CUR +
                   ' AND   GC.CO_EMP = '+ strP_CO_EMP +
                   ' AND GC.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
                   ' AND GC.CO_ANO_GRADE = ' + strP_CO_ANO_REF;

      // Cria uma instância do Relatório.

      Relatorio := TFrmrelCurvaABCFreq.Create(Nil);

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

        //Retorna o total da frequência
      with DM.QrySql do
      begin
        Close;
        SQL.Clear;

        SQL.Text :=  ' SET LANGUAGE PORTUGUESE ' +
                     ' SELECT  COUNT(A.ID_FREQ_ALUNO) QTD '+
                     ' FROM TB132_FREQ_ALU A '+
                     ' JOIN TB02_MATERIA B on A.CO_EMP_ALU = B.CO_EMP AND A.CO_MAT = B.CO_MAT AND A.CO_CUR = B.CO_CUR '+
                     ' JOIN TB107_CADMATERIAS CM on B.ID_MATERIA = CM.ID_MATERIA ' +
                     ' WHERE A.CO_CUR = ' + strP_CO_CUR +
                     ' AND A.CO_EMP_ALU = '+ strP_CO_EMP +
                     ' AND A.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
                     ' AND A.DT_FRE BETWEEN ' + quotedStr(strP_DT_INI) + ' and ' + QuotedStr(strP_DT_FIM) +
                     ' AND A.CO_FLAG_FREQ_ALUNO = ' + QuotedStr(strP_TP_PRESENCA) +
                     ' AND A.CO_ANO_REFER_FREQ_ALUNO = ' + strP_CO_ANO_REF;

        Open;

        if not IsEmpty then
        begin
          Relatorio.TotalFrequencia := FieldByName('QTD').AsInteger;
        end
        else
        begin
          Relatorio.TotalFrequencia := 0;
        end;
      end;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.codigoEmpresa := strP_CO_EMP;

        Relatorio.QRLDescCurso.Caption := strP_DE_CUR;
        Relatorio.QRLDesModulo.Caption := strP_DE_MODU_CUR;
        Relatorio.dtInicial := strP_DT_INI;
        Relatorio.dtFinal := strP_DT_FIM;

        Relatorio.tpPresenca := strP_TP_PRESENCA;

        Relatorio.QrlPeriodo.Caption := strP_DT_INI + ' à ' + strP_DT_FIM;

        if strP_TP_PRESENCA = 'S' then
          Relatorio.QRLTipo.Caption := 'Presença'
        else
          Relatorio.QRLTipo.Caption := 'Falta';

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
  DLLrelCurvaABCFreq;

end.
