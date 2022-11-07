library WR_RelMatricEfetivadas;

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
  U_FrmRelMatricEfetivadas in 'Relatorios\U_FrmRelMatricEfetivadas.pas' {FrmRelMatricEfetivadas};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
// STATUS: OK
function DLLRelMatricEfetivadas(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP,strP_ANO_REFER,strP_CO_MODU_CUR,strP_DES_MODU_CUR, strP_CO_CUR: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMatricEfetivadas;
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

      SqlString := ' SELECT DISTINCT B.NO_CUR, B.CO_CUR,  '+
               '        COUNT(A.CO_ALU) TOTAL             '+
               'FROM TB01_CURSO B                         '+
               'left join TB08_MATRCUR A on b.co_cur = a.co_cur and b.co_emp = a.co_emp and b.co_modu_cur = a.co_modu_cur '+
               'WHERE A.CO_SIT_MAT NOT IN (' + QuotedStr('C') + ')' +
               ' AND   A.CO_EMP = '+ strP_CO_EMP +
               ' AND A.CO_ANO_MES_MAT = ' + strP_ANO_REFER;

      if strP_CO_MODU_CUR <> nil then
        SqlString := SqlString + ' AND A.CO_MODU_CUR = ' + strP_CO_MODU_CUR;

      if strP_CO_CUR <> 'T' then
        SqlString := SqlString + ' AND A.CO_CUR = ' + strP_CO_CUR;

        SQLString := SQLString + ' GROUP BY B.NO_CUR, B.CO_CUR ORDER BY B.NO_CUR';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMatricEfetivadas.Create(Nil);

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
        Relatorio.codigoEmpresa := strP_CO_EMP;
        Relatorio.QRLDescModulo.Caption := strP_DES_MODU_CUR;
        Relatorio.QRLAno5.Caption := strP_ANO_REFER;
        Relatorio.QRLAno4.Caption := IntToStr(strtoint(strP_ANO_REFER) - 1);
        Relatorio.QRLAno3.Caption := IntToStr(strtoint(strP_ANO_REFER) - 2);
        Relatorio.QRLAno2.Caption := IntToStr(strtoint(strP_ANO_REFER) - 3);
        Relatorio.QRLAno1.Caption := IntToStr(strtoint(strP_ANO_REFER) - 4);

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
  DLLRelMatricEfetivadas;

end.
