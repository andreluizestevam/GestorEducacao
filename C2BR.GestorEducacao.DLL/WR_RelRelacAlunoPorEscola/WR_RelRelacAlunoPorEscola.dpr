library WR_RelRelacAlunoPorEscola;

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
  U_FrmRelRelacAlunoPorEscola in 'Relatorios\U_FrmRelRelacAlunoPorEscola.pas' {FrmRelRelacAlunoPorEscola};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
// STATUS: OK
function DLLRelRelacAlunoPorEscola(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_ANO_REFER: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelRelacAlunoPorEscola;
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

      SqlString := ' select distinct E.sigla,A.NO_MAE_ALU,' +
               '  A.NO_ALU, MM.CO_ALU_CAD, MO.CO_SIGLA_MODU_CUR, A.TP_DEF,' +
               '  CT.CO_SIGLA_TURMA, C.CO_SIGL_CUR, A.CO_SEXO_ALU, A.DT_NASC_ALU ' +
               ' from TB08_MATRCUR MM, TB07_ALUNO A, TB44_MODULO MO, ' +
               '      TB06_TURMAS T, TB01_CURSO C, TB129_CADTURMAS CT, TB25_EMPRESA E ' +
               ' where MM.CO_EMP = A.CO_EMP ' +
               ' and MM.CO_EMP = T.CO_EMP ' +
               ' and MM.CO_EMP = C.CO_EMP ' +
               ' and MM.CO_EMP = E.CO_EMP ' +
               ' and A.CO_ALU = MM.CO_ALU ' +
               ' and MM.CO_TUR = T.CO_TUR ' +
               ' and CT.CO_TUR = T.CO_TUR ' +
               ' and MO.CO_MODU_CUR = MM.CO_MODU_CUR ' +
               ' and MM.CO_MODU_CUR = T.CO_MODU_CUR ' +
               ' and T.CO_MODU_CUR = CT.CO_MODU_CUR ' +
               ' and MM.CO_CUR = C.CO_CUR ' +
               ' and MM.CO_ANO_MES_MAT = ' + quotedStr(strP_CO_ANO_REFER) + 
               ' AND MM.CO_SIT_MAT =' + QuotedStr('A');

      if strP_CO_EMP_REF <> 'T' then
        SQLString := SQLString + ' and MM.CO_EMP = ' + strP_CO_EMP_REF;

      SqlString := SqlString + ' order by A.NO_ALU ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelRelacAlunoPorEscola.Create(Nil);

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
        Relatorio.QRLParametros.Caption := 'Ano Referência: ' + strP_CO_ANO_REFER;

        if strP_CO_EMP_REF = 'T' then
          Relatorio.QRLParametros.Caption := Relatorio.QRLParametros.Caption + ' - Unidade: Todas'; 

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
  DLLRelRelacAlunoPorEscola;

end.
