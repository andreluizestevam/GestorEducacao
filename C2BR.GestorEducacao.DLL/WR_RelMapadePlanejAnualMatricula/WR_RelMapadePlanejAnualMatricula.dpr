library WR_RelMapadePlanejAnualMatricula;

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
  U_FrmRelMapadePlanejAnualMatricula in 'Relatorios\U_FrmRelMapadePlanejAnualMatricula.pas' {FrmRelMapadePlanejAnualMatricula};

// Controle Administrativo > Controle de Funcionários
// Relatório: EMISSÃO DO MAPA DE PLANEJAMENTO DE MATRÍCULAS
// STATUS: OK
function DLLRelMapadePlanejAnualMatricula(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_DPTO_CUR , strP_CO_ANO_REF: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapadePlanejAnualMatricula;
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
      SqlString := 'select distinct cur.no_cur,cur.co_dpto_cur, cur.co_cur, cur.co_modu_cur, ' +
                  'dpto.SG_DPTO_CUR from tb01_curso cur ' +
                  ' JOIN tb77_dpto_curso dpto ON dpto.co_dpto_cur = cur.co_dpto_cur and dpto.co_emp = cur.co_emp ' +
                  ' where cur.co_emp = ' + strP_CO_EMP +
                  ' and cur.co_modu_cur = ' + strP_CO_MODU_CUR +
                  ' and cur.co_dpto_cur = ' + strP_CO_DPTO_CUR +
                  ' order by cur.no_cur ';


      // Cria uma instância do Relatório.
      Relatorio := TFrmRelMapadePlanejAnualMatricula.Create(Nil);

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

        Relatorio.QRLParametros.Caption := strParamRel;
        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.anoBase := strP_CO_ANO_REF;

        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
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
  DLLRelMapadePlanejAnualMatricula;

end.
