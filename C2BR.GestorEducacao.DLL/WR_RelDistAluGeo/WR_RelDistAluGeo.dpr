library WR_RelDistAluGeo;

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
  U_FrmRelDistAluGeo in 'Relatorios\U_FrmRelDistAluGeo.pas' {FrmRelDistAluGeo},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelDistAluGeo(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_UF,strP_CO_CIDADE, strP_CO_BAIRRO, strP_TP_REL:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelDistAluGeo;
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

      SQLString := ' select distinct a.co_alu,a.no_alu,a.nu_nis,a.co_sexo_alu,a.NU_TELE_RESI_ALU,a.TP_DEF,a.NU_TELE_CELU_ALU,b.no_bairro,b.co_bairro,e.coduf,ci.no_cidade,'+
                   'a.DT_NASC_ALU,a.DE_ENDE_ALU,a.NU_ENDE_ALU,a.DE_COMP_ALU,'+
                   'r.no_resp,r.nu_tele_celu_resp, a.co_emp ' +
                   '  from tb07_aluno a ' +
                   '  left join tb74_UF e on e.coduf = a.co_esta_alu ' +
                   '  join tb108_responsavel r on r.co_resp = a.co_resp ' +
                   '  left join tb905_bairro b on b.co_bairro = a.co_bairro and b.co_cidade = a.co_cidade and b.co_uf = a.co_esta_alu ' +
                   '  left join tb904_cidade ci on ci.co_cidade = a.co_cidade and b.co_uf = a.co_esta_alu ';


      if (strP_TP_REL = 'M') then
      begin
        SQLString := SQLString + ' join tb08_matrcur mm on mm.co_alu = a.co_alu and mm.co_emp = a.co_emp ' +
                    ' where a.co_emp = ' + strP_CO_EMP +
                    ' and mm.co_sit_mat not in ( '+ QuotedStr('C') + ')';
      end
      else if (strP_TP_REL = 'N') then
      begin
        SQLString := SQLString + 'where a.co_emp = ' + strP_CO_EMP + ' AND  a.CO_ALU NOT IN( SELECT Z.CO_ALU ' +
                                                ' FROM TB08_MATRCUR Z'+
                                                ' WHERE  A.CO_EMP = Z.CO_EMP'+
                                                ' AND    A.CO_ALU = Z.CO_ALU )';
      end
      else
      begin
        SQLString  := SQLString + 'where a.co_emp = ' + strP_CO_EMP;
      end;

      if strP_UF <> 'T' then
      begin
        SQLString := SQLString + ' and a.co_esta_alu = ' + quotedStr(strP_UF);
      end;

      if strP_CO_CIDADE <> 'T' then
      begin
        SQLString := SQLString + ' and a.co_cidade = ' + strP_CO_CIDADE;
      end;

      if strP_CO_BAIRRO <> 'T' then
      begin
        SQLString := SQLString + ' and a.co_bairro = ' + strP_CO_BAIRRO;
      end;

      SQLString := SQLString + ' order by b.co_bairro,e.coduf,a.no_alu,a.co_alu,a.nu_nis,a.co_sexo_alu,a.NU_TELE_RESI_ALU,a.TP_DEF,a.NU_TELE_CELU_ALU,b.no_bairro, ' +
      'ci.no_cidade,a.DT_NASC_ALU,a.DE_ENDE_ALU,a.NU_ENDE_ALU,a.DE_COMP_ALU,r.no_resp,r.nu_tele_celu_resp, a.co_emp';

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelDistAluGeo.Create(Nil);

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
        Relatorio.tpRelatorio := strP_TP_REL;

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
  DLLRelDistAluGeo;

end.
