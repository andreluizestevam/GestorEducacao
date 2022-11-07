library WR_RelExtSolicitAluno;

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
  U_FrmRelExtSolicitAluno in 'Relatorios\U_FrmRelExtSolicitAluno.pas' {FrmRelExtSolicitAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelExtSolicitAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,
 strP_CO_TIPO_SOLI, strP_CO_ALU, strP_DT_INI, strP_DT_FIM, strP_SITU: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelExtSolicitAluno;
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

      SqlString := 'SET LANGUAGE PORTUGUESE                 '+
               ' Select  SA.DE_OBS_SOLI,               '+
               '         A.NU_NIS,                      '+
               '         SA.CO_SOLI_ATEN,               '+
               '         SA.MES_SOLI_ATEN,              '+
               '         SA.ANO_SOLI_ATEN,              '+
               '         SA.CO_CUR,                     '+
               '         CT.CO_SIGLA_TURMA as NO_TUR,  '+
               '         SA.DT_SOLI_ATEN,               '+
               '         SA.DT_PREV_ENTR,               '+
               '         SA.NU_DCTO_SOLIC,              '+
               '         B.VA_SOLI_ATEN,                '+
               '         B.DT_FIM_SOLI,                 '+
               '         A.NO_ALU, C.NO_CUR, C.CO_SIGL_CUR, '+
               '         B.CO_TIPO_SOLI,                '+
               '         TS.NO_TIPO_SOLI,               '+
               '         B.CO_SITU_SOLI CO_SIT_SOLI,    '+
               '         M.CO_ALU_CAD                   '+
               ' From TB64_SOLIC_ATEND SA,              '+
               '      TB66_TIPO_SOLIC TS,               '+
               '      TB65_HIST_SOLICIT B,              '+
               '      TB07_ALUNO A,                     '+
               '      TB01_CURSO C,                     '+
               '      TB08_MATRCUR M,                   '+
               '      TB06_TURMAS TUR,                  '+
               '      TB129_CADTURMAS CT                '+
               ' Where SA.CO_ALU = A.CO_ALU             '+
               '   AND SA.CO_CUR = C.CO_CUR             '+
               '   AND SA.CO_TUR = TUR.CO_TUR           '+
               '   AND CT.CO_TUR = TUR.CO_TUR           '+
               '   AND SA.CO_SOLI_ATEN = B.CO_SOLI_ATEN '+
               '   AND TS.CO_TIPO_SOLI = B.CO_TIPO_SOLI '+
               '   AND A.CO_EMP = '+ strP_CO_EMP +
               '   AND C.CO_EMP = '+ strP_CO_EMP +
               '   AND B.CO_EMP = '+ strP_CO_EMP +
               '   AND SA.CO_SIT = ' + QuotedStr('A')    +
               '   AND A.CO_ALU = '+ strP_CO_ALU +
               '   AND M.CO_EMP = '+ strP_CO_EMP +
               '   AND M.CO_ALU = A.CO_ALU              '+
               '   AND M.CO_CUR = C.CO_CUR              ' +
               ' AND M.CO_CUR = TUR.CO_CUR';


      //Se houver data
      if (strP_DT_INI <> nil) and (strP_DT_FIM <> nil) then
        SqlString := SqlString + '   AND SA.DT_SOLI_ATEN BETWEEN ' + quotedStr(strP_DT_INI) + ' and ' + quotedStr(strP_DT_FIM);

      if strP_CO_TIPO_SOLI <> 'T' then
      begin
        SqlString := SqlString + ' AND B.CO_TIPO_SOLI = ' + strP_CO_TIPO_SOLI;
      end;

      if strP_SITU <> 'S' then
      begin
        SqlString := SqlString + ' AND B.CO_SITU_SOLI = ' + QuotedStr(strP_SITU);
      end;

     SqlString := SqlString + ' ORDER BY SA.DT_SOLI_ATEN, B.CO_TIPO_SOLI, SA.CO_CUR ';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelExtSolicitAluno.Create(Nil);

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
        Relatorio.QRLPeriodo.Caption := 'Período de: ' + strP_DT_INI + ' à ' + strP_DT_FIM;

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
  DLLRelExtSolicitAluno;

end.
