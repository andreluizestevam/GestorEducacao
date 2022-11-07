library WR_RelGradeHorario;

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
  U_FrmRelGradeHorario in 'Relatorios\U_FrmRelGradeHorario.pas' {FrmRelGradeHorario};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelGradeHorario(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR,
 strP_CO_CUR, strP_CO_ANO_REFER, strP_CO_TUR: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelGradeHorario;
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

      SqlString := 'Select distinct G.CO_CUR, G.CO_TUR, G.NR_TEMPO, G.TP_TURNO, TA.HR_INICIO, TA.HR_TERMI,TA.TP_TURNO, ' +
	       ' C.NO_CUR, CT.NO_TURMA, D.CO_MODU_CUR, D.DE_MODU_CUR,G.CO_ANO_GRADE ' +
               ' From TB05_GRD_HORAR G,  TB01_CURSO C, TB06_TURMAS T, ' +
               '      TB43_GRD_CURSO GC, TB44_MODULO D, TB129_CADTURMAS CT, TB131_TEMPO_AULA TA ' +
               ' WHERE G.CO_EMP = C.CO_EMP AND G.CO_EMP = T.CO_EMP AND G.CO_EMP = GC.CO_EMP AND ' +
               '       G.CO_CUR = C.CO_CUR AND ' +
               '       G.CO_CUR = T.CO_CUR AND ' +
               '       G.CO_TUR = T.CO_TUR AND ' +
               '       CT.CO_TUR = T.CO_TUR AND ' +
               '       CT.CO_MODU_CUR = T.CO_MODU_CUR AND ' +
               '       G.CO_MODU_CUR = T.CO_MODU_CUR AND' +
               '       G.CO_CUR = GC.CO_CUR AND ' +
               '       G.CO_MAT = GC.CO_MAT AND ' +
               '       GC.CO_MODU_CUR = D.CO_MODU_CUR ' +
               ' AND TA.CO_EMP = G.CO_EMP AND TA.CO_MODU_CUR = G.CO_MODU_CUR AND TA.CO_CUR = G.CO_CUR AND TA.TP_TURNO = G.TP_TURNO ' +
               ' AND TA.NR_TEMPO = G.NR_TEMPO ' +
               ' and G.CO_EMP = ' + strP_CO_EMP;

    if strP_CO_ANO_REFER <> nil then
      SqlString := SqlString + ' AND  G.CO_ANO_GRADE = ' + QuotedStr(strP_CO_ANO_REFER);

    if strP_CO_MODU_CUR <> nil then
      SqlString := SqlString + ' AND  G.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

    if strP_CO_CUR <> nil then
      SqlString := SqlString + ' AND  G.CO_CUR = ' + strP_CO_CUR;

    if strP_CO_TUR <> nil then
      SqlString := SqlString + ' AND G.CO_TUR = ' + strP_CO_TUR;

    SqlString := SqlString + ' ORDER BY G.CO_CUR, G.CO_TUR, D.DE_MODU_CUR, G.NR_TEMPO, G.TP_TURNO ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelGradeHorario.Create(Nil);

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
        Relatorio.QRLParametros.Caption := strParamRel;
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
  DLLRelGradeHorario;

end.
