library WR_RelHistoricoEscolar;

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
  U_FrmRelHistoricoEscolar in 'Relatorios\U_FrmRelHistoricoEscolar.pas' {FrmRelHistoricoEscolar};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelHistoricoEscolar(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_CO_ALU, strP_CO_MODU_CUR: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelHistoricoEscolar;
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

      SqlString := 'select distinct c.NO_CUR,c.QT_AULA_CUR, c.CO_CUR,ha.CO_ANO_REF,c.SEQ_IMPRESSAO,c.CO_MODU_CUR ' +
               'from TB01_CURSO c ' +
               'inner join TB079_HIST_ALUNO ha on ha.CO_CUR = c.CO_CUR and ha.CO_EMP = c.CO_EMP and ha.CO_MODU_CUR = c.CO_MODU_CUR ' +
               ' inner join TB08_MATRCUR mm on mm.CO_CUR = c.CO_CUR and mm.CO_EMP = c.CO_EMP and mm.CO_MODU_CUR = c.CO_MODU_CUR and ha.co_alu = mm.co_alu ' +
               'where c.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
               'and  (ha.CO_ALU = ' + strP_CO_ALU + ' and mm.co_sit_mat = ' + quotedStr('F') +
                ' ) union ' +
                'select distinct c.NO_CUR,c.QT_AULA_CUR, c.CO_CUR,het.CO_ANO_REF,c.SEQ_IMPRESSAO,c.CO_MODU_CUR ' +
                'from TB01_CURSO c ' +
                'inner join TB130_HIST_EXT_ALUNO het on c.CO_CUR = het.CO_CUR and het.CO_EMP = c.CO_EMP and het.CO_MODU_CUR = c.CO_MODU_CUR ' +
                'where c.CO_MODU_CUR = ' + strP_CO_MODU_CUR +
                'and  (het.CO_ALU = ' + strP_CO_ALU +
                ' ) order by SEQ_IMPRESSAO,CO_ANO_REF';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelHistoricoEscolar.Create(Nil);

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
        Relatorio.codigoEmpresa := strP_CO_EMP;

        with Relatorio.qryAluno do
        begin
          Close;
          Sql.Clear;
          Sql.Text := 'select nu_nis,co_alu,no_alu,co_sexo_alu,dt_nasc_alu,CO_NACI_ALU,DE_NATU_ALU,DE_NACI_ALU,CO_UF_NATU_ALU, ' +
                      'CO_RG_ALU,CO_ORG_RG_ALU,CO_ESTA_RG_ALU,DT_EMIS_RG_ALU,NO_PAI_ALU,NO_MAE_ALU from tb07_aluno ' +
                      'where co_alu = ' + strP_CO_ALU;
          Open;
        end;

        with DM.QrySql do
        begin
          Close;
          SQL.Clear;
          SQL.Text := 'select c.no_cur,c.NO_ENSINO_FUND_ANTERIOR from tb01_curso c ' +
                      'where c.seq_impressao = ' + IntToStr(Relatorio.QryRelatorioSEQ_IMPRESSAO.AsInteger + 1);
          Open;
          Last;
          if not IsEmpty then
          begin
            Relatorio.QRLTitCertificado.Caption := 'CERTIFICAMOS QUE O ALUNO(A) PODERÁ MATRICULAR-SE NA ' + UpperCase(FieldByName('NO_ENSINO_FUND_ANTERIOR').AsString) +
              ' DO ENSINO FUNDAMENTAL DE 8 ANOS OU NO ' + UpperCase(FieldByName('no_cur').AsString) +' DO ENSINO FUNDAMENTAL DE 9 ANOS';
          end;
        end;

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
  DLLRelHistoricoEscolar;

end.
