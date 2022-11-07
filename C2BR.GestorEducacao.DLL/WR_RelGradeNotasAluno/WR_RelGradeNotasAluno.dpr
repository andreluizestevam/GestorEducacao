library WR_RelGradeNotasAluno;

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
  U_FrmRelGradeNotasAluno in 'Relatorios\U_FrmRelGradeNotasAluno.pas' {FrmRelGradeNotasAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelGradeNotasAluno(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelGradeNotasAluno;
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

      SqlString := ' Select distinct(A.NO_ALU), A.CO_ALU,A.NU_NIS, MM.CO_ALU_CAD,MM.CO_SIT_MAT   '+
               ' from TB079_HIST_ALUNO H, TB07_ALUNO A, TB08_MATRCUR MM, TB02_MATERIA MA    '+
               ' where H.CO_EMP = MM.CO_EMP                                                 '+
               '   AND H.CO_CUR = MM.CO_CUR                                                 '+
               '   AND H.CO_ALU = MM.CO_ALU                                                 '+
               '   AND H.CO_EMP = A.CO_EMP                                                  '+
               '   AND H.CO_ALU = A.CO_ALU                                                  '+
               '   AND H.CO_EMP = MA.CO_EMP                                                 '+
               '   AND H.CO_MAT = MA.CO_MAT                                                 '+
               '   AND MM.CO_SIT_MAT not in (' + QuotedStr('C')+ ')' +
               '	 AND H.CO_EMP = '+ strP_CO_EMP  +
               '   AND H.CO_ANO_REF = '+ QuotedStr(strP_CO_ANO_REF);

      if strP_CO_MODU_CUR <> 'T' then
      begin
        SQLString := SQLString + ' and h.co_modu_cur = ' + strP_CO_MODU_CUR;
      end;

      if strP_CO_CUR <> 'T' then
      begin
        SQLString := SQLString + ' and h.co_cur = ' + strP_CO_CUR;
      end;

      if strP_CO_TUR <> 'T' then
      begin
        SQLString := SQLString + ' and h.co_tur = ' + strP_CO_TUR;
      end;

      if strP_CO_ALU <> 'T' then
        SqlString := SqlString + ' and a.co_alu = ' + strP_CO_ALU;

      SqlString := SqlString + ' ORDER BY A.NO_ALU ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelGradeNotasAluno.Create(Nil);

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
        // Atualiza Campos do Relatório Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.codigoEmpresa := strP_CO_EMP;
        Relatorio.QRLParametros.Caption := strParamRel;

        Relatorio.CodigoModulo := strP_CO_MODU_CUR;
        Relatorio.CodigoCurso := strP_CO_CUR;
        Relatorio.CodigoTurma := strP_CO_TUR;
        Relatorio.AnoCurso := strP_CO_ANO_REF;
        Relatorio.NumSemestre := 1;

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
  DLLRelGradeNotasAluno;

end.
