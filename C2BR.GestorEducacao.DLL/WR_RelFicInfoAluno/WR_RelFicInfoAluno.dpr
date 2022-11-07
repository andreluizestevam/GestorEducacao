library WR_RelFicInfoAluno;

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
  U_FrmRelFicInfoAluno in 'Relatorios\U_FrmRelFicInfoAluno.pas' {FrmRelFicInfoAluno};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelFicInfoAluno(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_CO_ALU:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelFicInfoAluno;
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

      SqlString := ' SELECT B.NO_BAIRRO, C.NO_CIDADE, R.NO_RESP, R.CO_SEXO_RESP, R.DT_NASC_RESP, R.DE_GRAU_PAREN, R.DES_EMAIL_RESP, '+
               ' R.NU_TELE_RESI_RESP, R.NU_TELE_CELU_RESP, R.NU_TELE_COME_RESP, R.DE_ENDE_RESP, R.NU_ENDE_RESP, '+
               ' R.DE_COMP_RESP, R.CO_ESTA_RESP, R.CO_CEP_RESP,R.NU_CPF_RESP, I.NO_INST_ESP, D.DE_ESCOLA_ANTERIOR, D.CO_ULT_ANO_ESC_ANT, D.DE_CIDADE_ESC_ANT, D.CO_UF_ESC_ANT, A.*,'+
               ' (SELECT C.NO_CIDADE FROM TB108_RESPONSAVEL RE '+
               '    JOIN TB904_CIDADE C ON C.CO_CIDADE = RE.CO_CIDADE '+
               '		WHERE RE.CO_RESP = R.CO_RESP '+
               '	) CIDADERESP, '+
               ' (SELECT B.NO_BAIRRO FROM TB108_RESPONSAVEL RE '+
               '		JOIN TB905_BAIRRO B ON B.CO_BAIRRO = RE.CO_BAIRRO '+
               '		WHERE RE.CO_RESP = R.CO_RESP '+
               '	) BAIRRORESP, ima.ImageStream, E.sigla, tb.DE_TIPO_BOLSA as descBolsa, tb.CO_SITUA_TIPO_BOLSA as FLA_ATIVA'+
               '	FROM TB07_ALUNO A '+
               ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
               ' 	JOIN TB905_BAIRRO B ON B.CO_BAIRRO = A.CO_BAIRRO '+
               '	JOIN TB904_CIDADE C ON C.CO_CIDADE = A.CO_CIDADE '+
               '	JOIN TB108_RESPONSAVEL R ON R.CO_RESP = A.CO_RESP '+
               '	LEFT JOIN TB164_INST_ESP I ON I.CO_INST_ESP = A.CO_INST '+
               '	LEFT JOIN TB125_DADOSESCOLAR_ALU D ON D.CO_ALU = A.CO_ALU '+
               '  LEFT JOIN TB148_TIPO_BOLSA tb on tb.CO_TIPO_BOLSA = A.CO_TIPO_BOLSA ' +
               ' left join image ima on ima.imageId = a.imageId ' +
               //' LEFT JOIN TB08_MATRCUR MM on MM.CO_ALU = A.CO_ALU AND MM.CO_EMP = A.CO_EMP ' +
               //' LEFT JOIN TB01_CURSO cu on cu.co_cur = mm.co_cur and mm.co_emp = cu.co_emp and mm.co_modu_cur = cu.co_modu_cur ' +
               //' LEFT JOIN TB06_turmas tu on tu.co_cur = mm.co_cur and mm.co_emp = tu.co_emp and mm.co_tur = tu.co_tur ' +
               //' LEFT JOIN TB129_cadturmas ct on tu.co_tur = ct.co_tur ' +
               '	WHERE A.CO_ALU = ' + strP_CO_ALU;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelFicInfoAluno.Create(Nil);

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
  DLLRelFicInfoAluno;

end.
