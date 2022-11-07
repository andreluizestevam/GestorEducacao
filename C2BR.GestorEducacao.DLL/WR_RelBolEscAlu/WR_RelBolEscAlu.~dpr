{$A8,B-,C+,D+,E-,F-,G+,H+,I+,J-,K-,L+,M-,N+,O+,P+,Q-,R-,S-,T-,U-,V+,W-,X+,Y+,Z1}
{$MINSTACKSIZE $00004000}
{$MAXSTACKSIZE $00100000}
{$IMAGEBASE $00400000}
{$APPTYPE GUI}
{$WARN SYMBOL_DEPRECATED ON}
{$WARN SYMBOL_LIBRARY ON}
{$WARN SYMBOL_PLATFORM ON}
{$WARN UNIT_LIBRARY ON}
{$WARN UNIT_PLATFORM ON}
{$WARN UNIT_DEPRECATED ON}
{$WARN HRESULT_COMPAT ON}
{$WARN HIDING_MEMBER ON}
{$WARN HIDDEN_VIRTUAL ON}
{$WARN GARBAGE ON}
{$WARN BOUNDS_ERROR ON}
{$WARN ZERO_NIL_COMPAT ON}
{$WARN STRING_CONST_TRUNCED ON}
{$WARN FOR_LOOP_VAR_VARPAR ON}
{$WARN TYPED_CONST_VARPAR ON}
{$WARN ASG_TO_TYPED_CONST ON}
{$WARN CASE_LABEL_RANGE ON}
{$WARN FOR_VARIABLE ON}
{$WARN CONSTRUCTING_ABSTRACT ON}
{$WARN COMPARISON_FALSE ON}
{$WARN COMPARISON_TRUE ON}
{$WARN COMPARING_SIGNED_UNSIGNED ON}
{$WARN COMBINING_SIGNED_UNSIGNED ON}
{$WARN UNSUPPORTED_CONSTRUCT ON}
{$WARN FILE_OPEN ON}
{$WARN FILE_OPEN_UNITSRC ON}
{$WARN BAD_GLOBAL_SYMBOL ON}
{$WARN DUPLICATE_CTOR_DTOR ON}
{$WARN INVALID_DIRECTIVE ON}
{$WARN PACKAGE_NO_LINK ON}
{$WARN PACKAGED_THREADVAR ON}
{$WARN IMPLICIT_IMPORT ON}
{$WARN HPPEMIT_IGNORED ON}
{$WARN NO_RETVAL ON}
{$WARN USE_BEFORE_DEF ON}
{$WARN FOR_LOOP_VAR_UNDEF ON}
{$WARN UNIT_NAME_MISMATCH ON}
{$WARN NO_CFG_FILE_FOUND ON}
{$WARN MESSAGE_DIRECTIVE ON}
{$WARN IMPLICIT_VARIANTS ON}
{$WARN UNICODE_TO_LOCALE ON}
{$WARN LOCALE_TO_UNICODE ON}
{$WARN IMAGEBASE_MULTIPLE ON}
{$WARN SUSPICIOUS_TYPECAST ON}
{$WARN PRIVATE_PROPACCESSOR ON}
{$WARN UNSAFE_TYPE OFF}
{$WARN UNSAFE_CODE OFF}
{$WARN UNSAFE_CAST OFF}
library WR_RelBolEscAlu;

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
  U_FrmRelBolEscAlu in 'Relatorios\U_FrmRelBolEscAlu.pas' {FrmRelBolEscAlu};

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelBolEscAlu(strIdentFunc, strPathReportGenerate, strReportName, strP_CO_EMP,strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_HABIL_FOTO, strP_CNPJ_INSTI:PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelBolEscAlu;
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
      // Cria uma instância do Relatório.

      SQLString := SQLString + ' order by alu.no_alu';

      Relatorio := TFrmRelBolEscAlu.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      {
      //Barão do Rio Branco
      if strP_CNPJ_INSTI = '9014296000174' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEBARRIOBRA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Educandario de Maria
      else if strP_CNPJ_INSTI = '4120476000117' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEEDUCAMARIA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola ABC
      else if strP_CNPJ_INSTI = '7002950000102' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEESCABC;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola Reino Encantado
      else if strP_CNPJ_INSTI = '122135000120' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEESCREINOENCANT;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //ETB
      else if strP_CNPJ_INSTI = '3960623000102' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEETB;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Objetivo Esplanada
      else if strP_CNPJ_INSTI = '4776952000152' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEOBJESPLANADA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Colegio Especifico
      else if strP_CNPJ_INSTI = '10689657000161' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGECOESPECIFICO;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Colégio Esplanada
      else if strP_CNPJ_INSTI = '4223948000167' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGECOESPLANADA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Isaac Newton
      else if strP_CNPJ_INSTI = '32908634000133' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEISACNEWTON;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola Modelo
      else if strP_CNPJ_INSTI = '11558593000122' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEMODELO;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Conexão Aquarela
      else if strP_CNPJ_INSTI = '11489849000133' then
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=conexao@aquarela;Persist Security Info=True;User ID=conexao@aquarela;'+
  'Initial Catalog=BDPGECONAQU;Data Source=(local);Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      else
      begin
        DM.Conn.ConnectionString := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end;     }

      DM.Conn.ConnectionString := retornaConexao(strP_CNPJ_INSTI);
      DM.Conn.Open;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      //Relatorio.QryRelatorio.Connection.Open;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SQLString;
      Relatorio.QryRelatorio.Open;
      Relatorio.cnpjIntituicao := strP_CNPJ_INSTI;

      Relatorio.habilitaFoto := strP_HABIL_FOTO;

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
  DLLRelBolEscAlu;

end.
