library WR_RelMinhaBibliot;

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
  U_FrmRelMinhaBibliot in 'Relatorios\U_FrmRelMinhaBibliot.pas' {FrmRelMinhaBibliot};

function DLLRelMinhaBibliot(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_COL:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMinhaBibliot;
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

      SqlString := 'select E.*,AI.CO_CTRL_INTERNO,A.NO_ACERVO,EI.CO_ISBN_ACER,UB.TP_USU_BIB,EI.DT_REAL_DEVO_ACER,UB.NO_USU_BIB,EI.DT_PREV_DEVO_ACER,'+
                   'co.no_col, co.co_mat_col from TB36_EMPR_BIBLIOT E ' +
                   'join TB123_EMPR_BIB_ITENS EI on EI.CO_NUM_EMP = E.CO_NUM_EMP ' +
                   'join TB205_USUARIO_BIBLIOT UB on UB.ORG_CODIGO_ORGAO = E.ORG_CODIGO_ORGAO_USU and UB.CO_USUARIO_BIBLIOT = E.CO_USUARIO_BIBLIOT ' +
                   'join TB204_ACERVO_ITENS AI on AI.ORG_CODIGO_ORGAO = EI.ORG_CODIGO_ORGAO and AI.CO_ISBN_ACER = EI.CO_ISBN_ACER ' +
                   ' AND AI.CO_ACERVO_AQUISI = EI.CO_ACERVO_AQUISI and AI.CO_ACERVO_ITENS = EI.CO_ACERVO_ITENS ' +
                   'join TB35_ACERVO A on A.CO_ISBN_ACER = AI.CO_ISBN_ACER ' +
                   ' join tb03_colabor co on co.co_col = UB.CO_COL and co.co_emp = UB.CO_EMP_COL ' +
                   ' where UB.CO_EMP_COL = ' + strP_CO_EMP_COL +
                   ' and UB.co_col = ' + strP_CO_COL;

      SqlString := SqlString +
                   '  order by A.NO_ACERVO ';

      // Cria uma inst�ncia do Relat�rio.

      Relatorio := TFrmRelMinhaBibliot.Create(Nil);

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
        // Atualiza Campos do Relat�rio Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        //Relatorio.QRLParametros.Caption := strParamRel;

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
  DLLRelMinhaBibliot;

end.