library WR_RelAniversarioProfessor;

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
  U_FrmRelAniversarioProfessor in 'Relatorios\U_FrmRelAniversarioProfessor.pas' {FrmRelAniversarioProfessor};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelAniversarioProfessor(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP, strP_MES, strP_CO_DEPTO:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelAniversarioProfessor;
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

      SqlString := 'select c.no_col,c.co_mat_col,c.co_sexo_col,c.dt_nasc_col,' +
              'c.nu_tele_resi_col,c.nu_tele_celu_col,c.co_emai_col,fu.no_fun,de.no_depto,'+
              'e.sigla from tb03_colabor c '+
              'left join tb15_funcao fu on c.co_fun = fu.co_fun ' +
              'left join tb14_depto de on c.co_depto = de.co_depto ' +
              'left join tb25_empresa e on c.co_emp = e.co_emp ' +
              'where c.dt_nasc_col is not null ' +
              'and c.fla_professor = ' + quotedStr('S');

      if strP_MES <> 'T' then
      begin
        SqlString := SqlString + ' and month(c.dt_nasc_col) = ' + strP_MES;
      end;

      if strP_CO_EMP <> nil then
      begin
        SqlString := SqlString + ' and c.co_emp = ' + strP_CO_EMP;
      end;

      if strP_CO_DEPTO <> 'T' then
      begin
        SqlString := SqlString + ' and c.co_depto = ' + strP_CO_DEPTO;
      end;

      SqlString := SqlString + ' group by e.sigla,c.co_emp,month(c.dt_nasc_col),day(c.dt_nasc_col),c.no_col,c.co_mat_col,c.co_sexo_col,c.dt_nasc_col,c.nu_tele_resi_col,c.nu_tele_celu_col,c.co_emai_col,fu.no_fun,de.no_depto';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelAniversarioProfessor.Create(Nil);

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
  DLLRelAniversarioProfessor;

end.
