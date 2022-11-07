library WR_RelBolEscAluModelo2;

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
  U_FrmRelBolEscAluModelo2 in 'Relatorios\U_FrmRelBolEscAluModelo2.pas' {FrmRelBolEscAluModelo2};

// Controle Administrativo > Pesquisa de Opini�o / Avalia��es Institucionais
// Relat�rio: EMISS�O DO MAPA DE RESULTADO DE AVALIA��O
//
function DLLRelBolEscAluModelo2(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_HABIL_FOTO, strP_CO_BIMESTRE, strP_CNPJ_INSTI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelBolEscAluModelo2;
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

      SqlString := 'select '+
                   'alu.no_alu [aluno], alu.no_pai_alu [pai], '+
                   'alu.no_mae_alu [mae], alu.nu_nis [nis], '+
                   'alu.dt_nasc_alu [datanasc], '+
                   'cast((cast((getdate() - alu.dt_nasc_alu) as integer)/365.25)as integer) [idade], '+
                   'alu.co_sexo_alu [sexo], img.ImageStream,'+ //img.ImageStream [foto], '+
                   'alu.nu_nire [matricula], mat.co_turn_mat [turno], mat.co_ano_mes_mat [anoletivo], '+
                   'mat.co_cur [cocur], mat.co_tur [cotur],mat.co_modu_cur [comoducur], mat.co_alu [coalu], mat.co_sta_aprov [aprovado], mat.CO_STA_APROV_FREQ [aprovadoFreq],  '+
                   'cadtur.co_sigla_turma [turma], '+
                   'modu.de_modu_cur [modulo], '+
                   'cur.no_cur [serie], '+
                   'res.no_resp [responsavel], '+
                   'res.nu_cpf_resp [cpfresponsavel] ' +
                   'from '+
                   'tb07_aluno alu ' +
                   'join tb08_matrcur mat on mat.co_alu = alu.co_alu and mat.co_emp = alu.co_emp ' +
                   'join tb129_cadturmas cadtur on mat.co_tur = cadtur.co_tur ' +
                   'join tb44_modulo modu on mat.co_modu_cur = modu.co_modu_cur ' +
                   'join tb01_curso cur on cur.co_cur = mat.co_cur ' +
                   'join tb108_responsavel res on res.co_resp = alu.co_resp ' +
                   'left join Image img on img.ImageId = alu.ImageId ' +
                   'where mat.co_ano_mes_mat = ' + strP_CO_ANO_REF +
                   ' and mat.co_cur = ' + strP_CO_CUR +
                   ' and mat.co_tur = ' + strP_CO_TUR +
                   ' and mat.co_modu_cur = ' + strP_CO_MODU_CUR +
                   ' and mat.co_sit_mat <> '+QuotedStr('C') +
                   ' and mat.co_emp = ' + strP_CO_EMP;

      if strP_CO_ALU <> 'T' then
      begin
        SqlString := SqlString + ' and mat.co_alu = ' + strP_CO_ALU;
      end;
      // Cria uma inst�ncia do Relat�rio.

      SQLString := SQLString + ' order by alu.no_alu';

      Relatorio := TFrmRelBolEscAluModelo2.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relat�rio.
      Relatorio.QryRelatorio.Close;
      DM.Conn.ConnectionString := retornaConexao(strP_CNPJ_INSTI);
      DM.Conn.Open;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SQLString;
      Relatorio.QryRelatorio.Open;

      Relatorio.habilitaFoto := strP_HABIL_FOTO;
      Relatorio.bimestre := StrToInt(strP_CO_BIMESTRE);      

      if Relatorio.QryRelatorio.IsEmpty then
      begin
        // Retorna -1 para Sem Registros na Consulta.
        intReturn := -1;
      end
      else
      begin
        // Atualiza Campos do Relat�rio Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.codigoEmpresa := strP_CO_EMP;

        // Prepara o Relat�rio e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relat�rio Gerado com Sucesso.
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

  //Relat�rios
  DLLRelBolEscAluModelo2;

end.