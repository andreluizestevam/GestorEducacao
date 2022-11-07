library WR_RelRelacAluno;

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
  U_FrmRelRelacAluno in 'Relatorios\U_FrmRelRelacAluno.pas' {FrmRelRelacAluno},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
// STATUS: OK
function DLLRelRelacAluno(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR,
 strP_CO_CUR, strP_CO_ANO_REFER, strP_CO_TUR, strP_CO_SIT_MAT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacAluno;
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

      SqlString := ' select distinct MM.CO_EMP, MM.CO_ALU, MM.CO_CUR, MM.CO_TUR, ' +
               '  A.NO_ALU, A.NU_TELE_RESI_ALU, MM.CO_ALU_CAD, A.NU_CPF_ALU, ' +
               '  CT.CO_SIGLA_TURMA as NO_TURMA, C.NO_CUR, R.NO_RESP, A.CO_SEXO_ALU, A.DT_NASC_ALU, MM.CO_SIT_MAT ' +
               ' from TB08_MATRCUR MM, TB07_ALUNO A, ' +
               '      TB06_TURMAS T, TB01_CURSO C, TB108_RESPONSAVEL R, TB129_CADTURMAS CT ' +
               ' where MM.CO_EMP = A.CO_EMP ' +
               ' and MM.CO_EMP = T.CO_EMP ' +
               ' and MM.CO_EMP = C.CO_EMP ' +
               ' and A.CO_ALU = MM.CO_ALU ' +
               ' and MM.CO_TUR = T.CO_TUR ' +
               ' and CT.CO_TUR = T.CO_TUR ' +
               ' and MM.CO_MODU_CUR = T.CO_MODU_CUR ' +
               ' and T.CO_MODU_CUR = CT.CO_MODU_CUR ' +
               ' and MM.CO_CUR = C.CO_CUR ' +
               ' and R.CO_RESP = A.CO_RESP ' +
               ' and MM.CO_EMP = ' + strP_CO_EMP;

    if strP_CO_MODU_CUR <> nil then
      SqlString := SqlString + ' AND  MM.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

    if strP_CO_CUR <> nil then
      SqlString := SqlString + ' AND  MM.CO_CUR = ' + strP_CO_CUR;

    if strP_CO_ANO_REFER <> nil then
      SqlString := SqlString + ' AND  MM.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REFER);

    if strP_CO_TUR <> nil then
      SqlString := SqlString + ' AND MM.CO_TUR = ' + strP_CO_TUR;

    if strP_CO_SIT_MAT <> 'U' then
      SqlString := SqlString + ' AND MM.CO_SIT_MAT =' + QuotedStr(strP_CO_SIT_MAT);

    SqlString := SqlString + ' order by A.NO_ALU ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacAluno.Create(Nil);

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
  DLLRelRelacAluno;

end.
