library WR_RelRelacAlunoTurma;

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
  U_FrmRelRelacAlunoTurma in 'Relatorios\U_FrmRelRelacAlunoTurma.pas' {FrmRelRelacAlunoTurma};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelRelacAlunoTurma(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR,
 strP_CO_CUR, strP_CO_ANO_REFER, strP_CO_TUR, strP_CO_SIT_MAT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacAlunoTurma;
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

      SqlString := ' Select DISTINCT  MT.CO_ALU_CAD, mt.co_turn_mat, A.NO_ALU, A.TP_RACA, A.TP_DEF, A.CO_SEXO_ALU , A.DT_NASC_ALU, MT.DT_CAD_MAT, MT.CO_SIT_MAT, MT.CO_TUR, MT.CO_CUR,m.de_modu_cur,'+
                   '	(SELECT CT.CO_SIGLA_TURMA FROM TB129_CADTURMAS CT '+
                   '		 WHERE MT.CO_TUR = CT.CO_TUR AND MT.CO_MODU_CUR = CT.CO_MODU_CUR )AS TURMA,                    '+
                   '                                                              '+
                   '		(SELECT CU.NO_CUR FROM TB01_CURSO AS CU                   '+
                   '		 WHERE MT.CO_CUR = CU.CO_CUR AND CU.CO_MODU_CUR = MT.CO_MODU_CUR )AS SERIE                     '+
                   '   FROM TB07_ALUNO A                                          '+
                   '             JOIN TB08_MATRCUR MT ON               '+
                   '             MT.CO_ALU = A.CO_ALU AND                         '+
                   '             MT.CO_EMP = A.CO_EMP                             '+
                   ' JOIN TB44_MODULO M ON MT.CO_MODU_CUR = M.CO_MODU_CUR '+
                  // '			INNER JOIN TB48_GRADE_ALUNO GA ON                       '+
                  // '             GA.CO_ALU = MT.CO_ALU AND                        '+
                  // '             GA.CO_EMP = MT.CO_EMP AND                        '+
                  // '             GA.CO_CUR = MT.CO_CUR AND                        '+
                   '       AND      MT.CO_EMP = ' + strP_CO_EMP;

    if strP_CO_ANO_REFER <> nil then
        SqlString := SqlString + ' AND  MT.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REFER);

    if strP_CO_MODU_CUR <> nil then
        SqlString := SqlString + ' AND  MT.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

    if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' AND  MT.CO_CUR = ' + strP_CO_CUR;

    if strP_CO_TUR <> 'T' then
      SqlString := SqlString + ' AND MT.CO_TUR = ' + strP_CO_TUR;

    if strP_CO_SIT_MAT <> 'U' then
      SqlString := SqlString + ' AND MT.CO_SIT_MAT =' + QuotedStr(strP_CO_SIT_MAT);

    SqlString := SqlString + ' ORDER BY MT.CO_CUR, TURMA , A.NO_ALU ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacAlunoTurma.Create(Nil);

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
        Relatorio.QRLParametros.Caption := strParamRel;

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
  DLLRelRelacAlunoTurma;

end.
