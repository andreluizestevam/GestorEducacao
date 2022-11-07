library WR_RelFinalAlunos;

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
  U_FrmRelFinalAlunos in 'Relatorios\U_FrmRelFinalAlunos.pas' {FrmRelFinalAlunos};

// Controle Administrativo > Controle de Funcionários
// Relatório: EMISSÃO DA FICHA DE INFORMAÇÕES DE FUNCIONÁRIO
// STATUS: OK
function DLLRelFinalAlunos(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_Classificacao : PChar): Integer; export; cdecl;

var
  intReturn: Integer;
  Relatorio: TFrmRelFinalAlunos;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath : string;
  SqlString : string;
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

      // Monta a Consulta do Relatório.
      SqlString := ' Select distinct(A.NO_ALU),A.NU_NIS, A.CO_ALU, MM.CO_ALU_CAD,A.CO_SEXO_ALU,CT.CO_SIGLA_TURMA as NO_TURMA, H.CO_TUR, ' +
                ' SUM(H.VL_MEDIA_FINAL) as mediaFinal, SUM(H.VL_MEDIA_FINAL) testeVazio, MM.CO_STA_APROV, MM.CO_STA_APROV_FREQ ' +
                ' from TB079_HIST_ALUNO H, TB07_ALUNO A, TB08_MATRCUR MM, TB02_MATERIA MA, TB129_CADTURMAS CT ' +
                ' where H.CO_EMP = MM.CO_EMP ' +
                ' AND H.CO_CUR = MM.CO_CUR ' +
                ' AND H.CO_TUR = CT.CO_TUR ' +
                ' AND H.CO_ALU = MM.CO_ALU ' +
                ' AND H.CO_EMP = A.CO_EMP ' +
                ' AND H.CO_ALU = A.CO_ALU ' +
                ' AND H.CO_EMP = MA.CO_EMP ' +
                ' AND H.CO_MAT = MA.CO_MAT ' +
                ' AND NOT MM.CO_SIT_MAT IN (' + QuotedStr('C') + ', ' + QuotedStr('T') + ', ' + QuotedStr('R') + ', ' + QuotedStr('D') + ') ' +
                ' AND H.CO_EMP = ' + strP_CO_EMP  +
                ' AND H.CO_ANO_REF = ' + QuotedStr(strP_CO_ANO_REF) +
                ' AND H.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
                ' AND H.CO_CUR = ' + strP_CO_CUR;

                if strP_CO_TUR <> Nil Then
                  SQLString := SQLString + ' and h.co_tur = ' + strP_CO_TUR;

                if strP_Classificacao = 'N' then
                  SqlString := SqlString + ' group by (A.NO_ALU),A.NU_NIS, A.CO_ALU,MM.CO_STA_APROV, MM.CO_ALU_CAD,A.CO_SEXO_ALU,CT.CO_SIGLA_TURMA, H.CO_TUR, MM.CO_STA_APROV_FREQ ' +
                   ' ORDER BY A.NO_ALU '
                else
                  SqlString := SqlString + ' group by (A.NO_ALU),A.NU_NIS, A.CO_ALU,MM.CO_STA_APROV, MM.CO_ALU_CAD,A.CO_SEXO_ALU,CT.CO_SIGLA_TURMA, H.CO_TUR, MM.CO_STA_APROV_FREQ ' +
                    ' order by testeVazio desc, mediaFinal ';


      // Cria uma instância do Relatório.
      Relatorio := TFrmRelFinalAlunos.Create(Nil);

      //Preenche alguns campos do relatorio

      { Carrega as globais do relatório }

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SqlString;
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
        
        relatorio.QRLParametro.Caption := strParamRel;
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.CodigoCurso := strP_CO_CUR;
        Relatorio.CodigoModulo :=  strP_CO_MODU_CUR;
        Relatorio.CodigoEmpresa := strP_CO_EMP;
        Relatorio.AnoCurso := strP_CO_ANO_REF;
        Relatorio.TipoEnsino := 'ES';

        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relatório Gerado com Sucesso.
        intReturn := 1;
      end;

    Except
      on E : Exception do
        //intReturn := 0;
        ShowMessage(E.ClassName + ' error raised, with message : ' + E.Message);
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
  DLLRelFinalAlunos;

end.
