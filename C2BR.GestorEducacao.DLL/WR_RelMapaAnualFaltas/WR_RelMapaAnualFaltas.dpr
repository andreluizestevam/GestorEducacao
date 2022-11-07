library WR_RelMapaAnualFaltas;

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
  U_FrmRelMapaAnualFaltas in 'Relatorios\U_FrmRelMapaAnualFaltas.pas' {FrmRelMapaAnualFaltas};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelMapaAnualFaltas(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR,
 strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_MAT: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaAnualFaltas;
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
               ' SELECT distinct c.no_cur,c.co_cur,c.co_param_freque,c.co_param_freq_tipo,a.no_alu,f.CO_ALU FROM tb48_GRADE_ALUNO f '+
               ' JOIN tb01_curso c ON c.co_cur = f.co_cur '+
               ' JOIN tb07_aluno a ON a.co_alu = f.co_alu '+
               ' where f.CO_EMP = '+ strP_CO_EMP +
               ' AND   f.CO_CUR = '+ strP_CO_CUR +
               ' AND   f.CO_TUR = '+ strP_CO_TUR +
               ' AND f.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
               ' AND   f.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF);

      if strP_CO_MAT <> nil then
        SqlString := SqlString + ' and f.co_mat = ' + strP_CO_MAT;

      if strP_CO_ALU <> 'T' then
        SqlString := SqlString + ' AND f.CO_ALU = ' + strP_CO_ALU;

      SqlString := SqlString + ' Order by a.no_alu,f.co_alu';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelMapaAnualFaltas.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
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
        // Atualiza Campos do Relat�rio Diretamente
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.AnoCurso := strP_CO_ANO_REF;
        Relatorio.codigoEmpresa := strP_CO_EMP;
        Relatorio.QRLParametros.Caption := strParamRel;
        if strP_CO_MAT <> nil then
          Relatorio.co_materia := strToInt(strP_CO_MAT);

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relat�rio Gerado com Sucesso.
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
  
  //Relat�rios
  DLLRelMapaAnualFaltas;

end.
