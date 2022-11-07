library WR_RelLinhaTranspPorAluno;

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
  U_FrmRelLinhaTranspPorAluno in 'Relatorios\U_FrmRelLinhaTranspPorAluno.pas' {FrmRelLinhaTranspPorAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelLinhaTranspPorAluno(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR,
 strP_CO_TUR, strP_CO_ALU: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelLinhaTranspPorAluno;
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

      SqlString :=  'SELECT DISTINCT APE.*,A.NO_ALU, LO.NO_LINHA_ONIBUS,LO.VL_TARIF_LINHA_ONIBUS, LO.DE_LINHA_ONIBUS ' +
                     ' FROM TB170_ALUNO_PASSE_ESCOLAR APE ' +
                     ' JOIN TB08_MATRCUR M ON M.CO_ALU = APE.CO_ALU and M.CO_EMP = APE.CO_EMP and M.CO_ANO_MES_MAT = APE.ANO_REF ' +
                     ' JOIN TB07_ALUNO A on A.co_alu = ape.co_alu and A.co_emp = ape.co_emp ' +
                     ' JOIN TB189_LINHA_ONIBUS LO on LO.CO_LINHA_ONIBUS = APE.CO_LINHA_ONIBUS ' +
                     ' WHERE APE.CO_EMP = ' + strP_CO_EMP +
                     ' AND APE.ANO_REF = ' + strP_ANO_REF +
                     ' AND M.CO_CUR = ' + strP_CO_CUR +
                     ' AND M.CO_TUR = ' + strP_CO_TUR +
                     ' AND M.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_ALU <> 'T' then
      begin
        SqlString := SqlString + ' AND APE.CO_ALU = ' + strP_CO_ALU;
      end;

      SqlString := SqlString + ' ORDER BY A.NO_ALU ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelLinhaTranspPorAluno.Create(Nil);

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
        Relatorio.QrlParametros.Caption := strParamRel;

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
  DLLRelLinhaTranspPorAluno;

end.
