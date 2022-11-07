library WR_RelHistEscAvalProg;

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
  U_FrmRelHistEscAvalProg in 'Relatorios\U_FrmRelHistEscAvalProg.pas' {FrmRelHistEscAvalProg},
  U_FrmRelHistEscAvalProgP2 in 'Relatorios\U_FrmRelHistEscAvalProgP2.pas' {FrmRelHistEscAvalProgP2},
  U_FrmRelHistEscAvalProgP3 in 'Relatorios\U_FrmRelHistEscAvalProgP3.pas' {FrmRelHistEscAvalProgP3},
  U_FrmRelHistEscAvalProgP4 in 'Relatorios\U_FrmRelHistEscAvalProgP4.pas' {FrmRelHistEscAvalProgP4};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelHistEscAvalProg(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelHistEscAvalProg;
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

      SqlString := ' SELECT A.CO_ALU, A.NO_ALU, A.DT_NASC_ALU, A.NO_PAI_ALU, A.NO_MAE_ALU, A.DES_OBS_ALU, '+
               ' E.NO_FANTAS_EMP, M.CO_ANO_MES_MAT, '+
               ' C.CO_SIGL_CUR, C.CO_SIGL_REFER, C.QT_CARG_HORA_CUR, CID.NO_CIDADE, E.CO_UF_EMP '+
               ' FROM TB07_ALUNO A '+
               ' JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
               ' JOIN TB08_MATRCUR M ON M.CO_ALU = A.CO_ALU AND M.CO_EMP = A.CO_EMP '+
               ' JOIN TB01_CURSO C ON C.CO_CUR = M.CO_CUR AND C.CO_EMP = M.CO_EMP '+
               ' JOIN TB904_CIDADE CID ON CID.CO_CIDADE = E.CO_CIDADE '+
               ' JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = E.CO_BAIRRO '+
               ' WHERE M.CO_ALU = ' + strP_CO_ALU +
               '   AND M.CO_ANO_MES_MAT = ' + strP_CO_ANO_REF +
               ' AND M.CO_EMP = ' + strP_CO_EMP;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelHistEscAvalProg.Create(Nil);

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

        Relatorio.QrlNoAlu.Caption := Relatorio.QryRelatorio.FieldByName('NO_ALU').AsString;
        Relatorio.QrlNoEmpresa.Caption := Relatorio.QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString;
        Relatorio.QrlAno.Caption := Relatorio.QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString;

        Relatorio.QrlTotCHA.Caption := Relatorio.QryRelatorio.FieldByName('QT_CARG_HORA_CUR').AsString;
        Relatorio.QrlCserie.Caption := Relatorio.QryRelatorio.FieldByName('CO_SIGL_CUR').AsString;
        Relatorio.QrlCciclo.Caption := Relatorio.QryRelatorio.FieldByName('CO_SIGL_REFER').AsString;
        Relatorio.QrlCano.Caption := Relatorio.QryRelatorio.FieldByName('CO_ANO_MES_MAT').AsString;
        Relatorio.QrlCempresa.Caption := Relatorio.QryRelatorio.FieldByName('NO_FANTAS_EMP').AsString;
        Relatorio.QrlCcidade.Caption := Relatorio.QryRelatorio.FieldByName('NO_CIDADE').AsString;
        Relatorio.QrlCuf.Caption := Relatorio.QryRelatorio.FieldByName('CO_UF_EMP').AsString;

        // Prepara o Relatório e Gera o PDF.
        //Relatorio.QuickRep1.Prepare;
        Relatorio.QuickRep1.Prepare;
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);;
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
  DLLRelHistEscAvalProg;

end.
