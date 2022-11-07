library WR_RelPosicReserva;

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
  UFrmRelPosicReserva in 'Relatorios\UFrmRelPosicReserva.pas' {FrmRelPosicReserva};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelPosicReserva(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP,strP_DT_INI, strP_DT_FIM: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelPosicReserva;
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
               ' SELECT DISTINCT B.NO_CUR,                               '+
               '        Aberto = SUM(CASE A.CO_SIT_RESMAT                '+
               '                    WHEN ' + '''' + 'A' + ''''+ ' THEN 1 '+
               '                    ELSE 0                               '+
               '                   END),                                 '+
               '        Habili = SUM(CASE A.CO_SIT_RESMAT                '+
               '                    WHEN ' + '''' + 'H' + ''''+ ' THEN 1 '+
               '                    ELSE 0                               '+
               '                   END),                                 '+
               '        Matric = SUM(CASE A.CO_SIT_RESMAT                '+
               '                    WHEN ' + '''' + 'M' + ''''+ ' THEN 1 '+
               '                    ELSE 0                               '+
               '                   END),                                 '+
               '        Cancel = SUM(CASE A.CO_SIT_RESMAT                '+
               '                    WHEN ' + '''' + 'C' + ''''+ ' THEN 1 '+
               '                    ELSE 0                               '+
               '                   END)                                  '+
               ' FROM TB01_CURSO B                                        '+
				       ' LEFT JOIN TB52_RESERVMAT A ON B.CO_CUR = A.CO_CUR AND A.CO_EMP = B.CO_EMP '+
               ' AND   A.DT_RESMAT BETWEEN ' + quotedStr(strP_DT_INI) + ' AND   ' + QuotedStr(strP_DT_FIM) +
               ' WHERE   B.CO_EMP = ' + strP_CO_EMP +
               ' GROUP BY  B.NO_CUR  ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelPosicReserva.Create(Nil);

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
        Relatorio.QrlPeriodo.Caption := 'Período de: ' + strP_DT_INI + ' à ' + strP_DT_FIM;

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
  DLLRelPosicReserva;

end.
