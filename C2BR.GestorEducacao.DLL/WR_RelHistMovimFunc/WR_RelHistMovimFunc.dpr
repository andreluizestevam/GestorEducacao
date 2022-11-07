library WR_RelHistMovimFunc;

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
  U_FrmRelHistMovimFunc in 'Relatorios\U_FrmRelHistMovimFunc.pas' {FrmRelHistMovimFunc};

function DLLRelHistMovimFunc(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_COL, strP_DT_INI, strP_DT_FIM:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelHistMovimFunc;
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

      SqlString := 'set language portuguese ' +
                    ' select mf.CO_EMP_ORIGEM, c.CO_MAT_COL, c.NO_COL, mf.DT_CADAST as CADASTRO, mf.DT_INI_MOVIM_TRANSF_FUNCI as INICIO, ' +
                    ' mf.DT_FIM_MOVIM_TRANSF_FUNCI as FIM, mf.CO_TIPO_MOVIM, mf.CO_MOTIVO_AFAST as MOTIVO, ' +
                    ' e.NO_FANTAS_EMP as DESTINO_INT, mf.CO_TIPO_REMUN as REMUN, it.NO_INSTIT_TRANSF ' +
                    ' from TB286_MOVIM_TRANSF_FUNCI mf ' +
                    ' join TB03_COLABOR c on c.CO_COL = mf.CO_COL and c.CO_EMP = mf.CO_EMP_ORIGEM ' +
                    ' left join TB25_EMPRESA e on e.CO_EMP = mf.CO_EMP_DESTIN ' +
                    ' left join TB285_INSTIT_TRANSF it on it.ID_INSTIT_TRANSF = mf.ID_INSTIT_TRANSF ' +
                    ' where mf.DT_INI_MOVIM_TRANSF_FUNCI between ' + quotedStr(strP_DT_INI) + 'AND ' + quotedStr(strP_DT_FIM);

      if strP_CO_EMP_REF <> 'T' then
        SQLString := SQLString + ' and mf.CO_EMP_ORIGEM = ' + strP_CO_EMP_REF;

      if strP_CO_COL <> 'T' then
        SQLString := SQLString + ' and mf.CO_COL = ' + strP_CO_COL;

      SQLString := SQLString + ' order by mf.DT_CADAST';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelHistMovimFunc.Create(Nil);

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

        if strP_CO_EMP_REF <> 'T' then
        begin
          if strP_CO_COL <> 'T' then
            Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('DESTINO_INT').AsString + ' - Funcionário: Todos - Período: ' + strP_DT_INI + ' à ' + strP_DT_FIM  +  ' )'
          else
            Relatorio.QRLParam.Caption := '( Unidade: ' + Relatorio.QryRelatorio.FieldByName('DESTINO_INT').AsString + ' - Funcionário: ' + Relatorio.QryRelatorio.FieldByName('NO_COL').AsString + ' - Período: ' + strP_DT_INI + ' à ' + strP_DT_FIM  +  ' )';
        end
        else
          Relatorio.QRLParam.Caption := '( Unidade: Todas - Funcionário: Todos - Período: ' + strP_DT_INI + ' à ' + strP_DT_FIM  +  ' )';

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
  DLLRelHistMovimFunc;

end.
