unit U_Funcoes;

interface
uses
  Windows, Messages, SysUtils,bsutils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, Db, ADODB, DBTables, DBGrids, DBCtrls, ExtCtrls, Gauges, Math, QUICKRPT,
  Qrctrls, QRPrntr, Mask, printers, Registry, DBitypes, WinProcs, COMObj, DateUtil,
  Excel97, FileCtrl, U_FrmRelTemplate, gbCobranca, ShellApi;

Type
  PHWND = ^HWND;

  { Funções }
  function fDVNuInternoValido(Numero : String) : Boolean;
  function NumeroCEIValido( var sCEI:String ) : Boolean;
  function fvalidaDVProcesso( Numero : String) : Boolean;
  function WindowsTempDir : String;
  function Direita(Campo: String; Tamanho : Integer) : String;
  function fMesMMM( mes : integer):String;
  function Critica_CEI(var Numero : String) : Boolean;
  function fDifAnoMes( sAnoMesFinal, sAnoMesInicial : String): Integer;
  function Critica_CGC(CGC: String) : Boolean;
  function Critica_CPF(CPF: String) : Boolean;
  function EnumWndProc( Hwnd: THandle; Found: PHWND) : Bool; Export; stdcall;
  function PISValido( nuPis:String ) : Boolean;
  function fAnoMesToDateTime( sAnoMes:String; iDia:Word) : TDateTime;
  function fAnoMesMM( sAnoMes:String; iMeses : Integer) : String;
  function DVNuNosso( var NumeroNosso : String) : Boolean;
  function RetNuNosso( var NumeroNosso : String) : string;
  function fFloatAsStr( Valor : Real; Precisao : Word ) : String;
  function fArred( Base : Extended; Casas:Word ) : Extended;
  function fPadl( sCaracter:String; iTamanho:Integer ) : String;
  function fPadr( sCaracter:String; iTamanho:Integer ) : String;
  function DiasNoMes( iMes, iAno : Word ) : Word;
  function StrZero(iNumero: integer; iTamanho: integer; sCaracter: char): string;
  function Ano( Data:TdateTime ) : Word;
  function Dia( Data:TdateTime ) : Word;
  function Mes( Data:TdateTime ) : Word;
  function Extenso(Valor: extended): string;
  function PoeZERO(Campo :String;Tamanho :Integer):String;
  function ConverteFloat(const S: string): Extended;
  function SegParaMin(Hora : integer) : String;
  function MinParaSeg(Hora : String) : integer;
  function SegParaHora(Hora : integer) : String;
  function FormataTempo(Tempo: string): string;
  function Replica(pStr: string; pLen: integer; pCar: string): string;
  function ReplicaDireita(pStr: string; pLen: integer; pCar: string): string;
  function GetWindowsDir: TFileName;
  function GetSystemDir: TFileName;
  function GetTempDir: String;
  function LogUser : String;
  function kfVersionInfo: String;
  function GetPiece(Node: string; Delimiter: string; FirstPiece: integer): string;
  function GetPieces(Node: string; Delimiter: string; FirstPiece: integer; LastPiece: integer): string;
  function ExtractTempDir : String;
  function RetiraString(const s: string; s1: string): string;
  function MinToHor(Secs: LongInt): String;
  function DataServidor(): TDateTime;
  function RecuperaMsgErroExclusao(Msg: String): String;
  function GeraProximoCodigo(nmCampo, nmTabela: String) : integer;
  function GeraProximoCodigoSimples(nmCampo, nmTabela: String) : integer;
  function IsValid(Ident: string;Modo:integer): Boolean;
  function RecuperaEstoqueAtual(CodProduto: Integer): Double;
  function ReplaceString(Text, NewChar, OldChar: String): String;
  function fCheckEmail(Email : String): Boolean;
  function Crypt(Action, Src, Key : String) : String;
  function DiaSemana(Data: TDateTime): String;
  function RecuperaCodigoAluno(CodigoMatricula: String): Integer;
  function RecuperaNomeUsuarioLogado(): String;
  function ImprimeDeclaracao(ADocument: String; TipoParam, DadosTipoParam, TipoDeclaracao: String): Boolean;
  function GetDataExtensoRelatorio(Data: TDateTime): String;
  function GetDataExtenso(Data: TDateTime): String;
  function GetDataExtensoReduzida(Data: TDateTime): String;
  function RodaScriptSqlBD: Boolean;
  function CalculaPercentualValor(ValorParaCalculo, ValorDoPercentual: Double): Double;
  function RecuperaUltSeqDocumentoAluno(CodAluno, CodCurso: Integer): Integer;
  function LastDayOfPrevMonthData(Data: TDateTime): TDateTime;
  function VerificaGerarMensUltDiaMes: Boolean;
  function GeraSenha(): String;
  function VerificaDocumentoPago(CodigoEmpresa: Integer; NumeroDocumento: Integer; TipoDoc: String): String;
  function VerificaFormaGerarBoletoAluno(CodigoEmpresa: Integer): String;
  function ArredondaNotaFinalAluno(Valor: Double): Double;
  function MontaStringConexaoDB(dadosConexao : TStringList;comCripto : Boolean):String;
  { Procedures }
  procedure TrocarImpressora(var Relatorio : TQuickRep);
  procedure TrocaDataHoradoSistema( DataHora:TDateTime );
  procedure TrocaTABporENTER(Sender: TObject; var Key: Char);
  procedure DataFutura( Data : TDateTime );
  procedure CorrigirArquivo( ArqOrigem, ArqDestino : String);
  procedure GetBuildInfo(var V1, V2, V3, V4: Word);
  procedure CriaArquivoTexto(NomeArquivo: String; Tipo: String; Texto: String);
  procedure GeraArquivoTexto(DataSet: TDataSet; NomeArq: String);
  procedure deletefilestmp(NomeArquivo: String);
  procedure MoveForm;
  procedure ImprimDBGrind(Grid: TDBGrid; TituloRel: String; PosPapel: Integer);
  procedure RecuperaParametrosDaEscola(CodCurso: String);
  procedure RecuperaParametrosDaEmpresa;
  procedure RecuperaPermissaoUsuario;

implementation

uses U_DataModuleSGE;

{*****************************************************************************
*  Autor: Gelson
*  Arredonda nota do aluno
*****************************************************************************}
function ArredondaNotaFinalAluno(Valor: Double): Double;
var
  Fac: Double;
begin
  Fac := StrToFloat('0,' + GetPiece(FloatToStr(Valor),',',2));

  if (Fac = 0.50) or (Fac = 0.00) then
    Result := Valor
  else
  begin
     if (Fac < 0.50) then
       Result := Valor + (0.50 - Fac)
     else Result := Valor + (1 - Fac)
  end;
end;

{*****************************************************************************
*  Autor: Gelson
*  Verifica a forma de gerar o boleto para o aluno
*****************************************************************************}
function VerificaFormaGerarBoletoAluno(CodigoEmpresa: Integer): String;
var
  QrySqlConsulta: TADOQuery;
begin
  { Cria query }
  QrySqlConsulta := TADOQuery.Create(DataModuleSGE);
  QrySqlConsulta.Connection := DataModuleSGE.Conn;
  with QrySqlConsulta do
  begin
    Close;
    Sql.Clear;
    Sql.Text := ' Select FLA_GERBOLEMPCUR From TB83_PARAMETRO ' +
                ' Where CO_EMP = ' + IntToStr(CodigoEmpresa);
    Open;

    Result := FieldByName('FLA_GERBOLEMPCUR').AsString;
      
    Close;
  end;
  
  QrySqlConsulta := nil;
  QrySqlConsulta.Free;

end;

{*****************************************************************************
*  Autor: Gelson
*  Verifica se o documento foi pago
*  Variavél de Retorno: Q-> Pagamento efetuado
*                       A -> Pagamento não efetuado
*                       N -> Documento não Cadastrado
*                       C -> Documento Cancelado
*****************************************************************************}
function VerificaDocumentoPago(CodigoEmpresa: Integer; NumeroDocumento: Integer; TipoDoc: String): String;
var
  QrySqlConsulta: TADOQuery;
  NumDoc: String;
begin

  if TipoDoc = 'V' then
    NumDoc := IntToStr(NumeroDocumento) + '/V'
  else NumDoc := IntToStr(NumeroDocumento);


  { Cria query }
  QrySqlConsulta := TADOQuery.Create(DataModuleSGE);
  QrySqlConsulta.Connection := DataModuleSGE.Conn;
  with QrySqlConsulta do
  begin
    Close;
    Sql.Clear;
    Sql.Text := ' Select NU_DOC, IC_SIT_DOC From TB47_CTA_RECEB ' +
                ' Where CO_EMP = ' + IntToStr(CodigoEmpresa) +
                ' and NU_DOC = ' + QuotedStr(NumDoc);
    Open;

    if IsEmpty then
      Result := 'N';

    if FieldByName('IC_SIT_DOC').AsString = 'Q' then
      Result := 'Q';

    if FieldByName('IC_SIT_DOC').AsString = 'A' then
      Result := 'A';

    if FieldByName('IC_SIT_DOC').AsString = 'C' then
      Result := 'C';
      
    Close;
  end;
  
  QrySqlConsulta := nil;
  QrySqlConsulta.Free;
end;

{ Funcção para gerar senha }
function GeraSenha(): String;
var
  i: integer;
  const Str = '1234567890ABCDEF5678GH90IJKLMNOPQ123RSTUV3456WXYZ';
        Max = 8;
begin
  for i := 1 to Max do
  begin
    Randomize;
    Result := Result + Str[Random(Length(Str)) + 1];
  end;
end;

{ Verifica se e para gerar parcelas no último dia do mês }
function VerificaGerarMensUltDiaMes: Boolean;
var
  QrySqlAux: TADOQuery;
begin
  { Cria query's auxiliares e aponta a conexão }
  QrySqlAux := TADOQuery.Create(DataModuleSGE);
  QrySqlAux.Connection := DataModuleSGE.Conn;
  Result := False;

  with QrySqlAux do
  begin
    Sql.Clear;
    Sql.Text := 'select FLA_GERMENULTDIAMES ' +
                ' from TB83_Parametro ' +
                ' where co_emp = ' + IntToStr(Sys_CodigoEmpresaAtiva);
    Open;

    if FieldByName('FLA_GERMENULTDIAMES').AsString = 'S' then
      Result := True;
    Close;
  end;

  QrySqlAux := nil;
  QrySqlAux.Free;
end;

{ Retornar o último dia de uma data }
function LastDayOfPrevMonthData(Data: TDateTime): TDateTime;
var
  D: TDateTime;
  Year, Month, Day: Word;
begin
  D := Data;
  DecodeDate(D, Year, Month, Day);
  Day := DaysPerMonth(Year, Month);
  Result := EncodeDate(Year, Month, Day);
end;

{********************************************************************************
* Recupera a quantidade de matrícula que o aluno já tem.  Para gerar o número do
* documento do contas a receber 
*********************************************************************************}
function RecuperaUltSeqDocumentoAluno(CodAluno, CodCurso: Integer): Integer;
var
  QrySqlAux: TADOQuery;
begin
  { Cria query's auxiliares e aponta a conexão }
  QrySqlAux := TADOQuery.Create(DataModuleSGE);
  QrySqlAux.Connection := DataModuleSGE.Conn;

  with QrySqlAux do
  begin
    Close;
    Sql.Clear;
    Sql.Text := 'select  max(nu_doc) nu_doc ' +
                ' from TB47_CTA_RECEB ' +
                ' where co_alu = ' + IntToStr(CodAluno) +
                '   and co_cur = ' + IntToStr(CodCurso) +
                '   and DE_COM_HIST <> ' + QuotedStr('SOLICITAÇÃO.') + 
                '   and co_emp = ' + IntToStr(Sys_CodigoEmpresaAtiva) +
                ' for browse ';
    Open;

    if FieldByName('nu_doc').IsNull then
      Result := 0
    else
    begin
      Result := StrToInt(GetPiece(QrySqlAux.FieldByName('nu_doc').AsString, '.', 3));
    end;
    
    Close;
  end;

  QrySqlAux := nil;
  QrySqlAux.Free;
end;

function CalculaPercentualValor(ValorParaCalculo, ValorDoPercentual: Double): Double;
begin
  if ((ValorParaCalculo = 0) or (ValorDoPercentual = 0))  then
    Result := 0;

  Result := (ValorParaCalculo * ValorDoPercentual) / 100;
end;

function RodaScriptSqlBD: Boolean;
var
  QrySqlScript: TADOQuery;
  StringCon: TStringList;
  StringConBD: String;
  Arq: String;
begin
  Result := True;
  try
    { Recupera o tipo de conexão }
    StringCon := TStringList.Create;
    StringCon.LoadFromFile(ExtractFilePath(Application.ExeName) + 'cfgsae.ini');

    { Tipo de conexão }
    StringConBD := StringCon[0];

    { Nome do servidor }
    Sys_NomeServidorBD := Crypt('D',StringConBD, '1999666777888AXDVXSD9642A526842D');

    { Cria query auxiliar aponta a conexão }
    QrySqlScript := TADOQuery.Create(DataModuleSGE);
    QrySqlScript.ConnectionString := Sys_NomeServidorBD;

    { Verifica se o arquivo existe e roda o script do banco }
    if FileExists(ExtractFilePath(Application.ExeName) + 'sae.sql') then
    begin
      Arq := ExtractFilePath(Application.ExeName) + 'saeok.sql';
      DeleteFile(PChar(Arq));
      QrySqlScript.Close;
      QrySqlScript.SQL.LoadFromFile(ExtractFilePath(Application.ExeName) + 'sae.sql');
      QrySqlScript.ExecSQL;
      RenameFile(ExtractFilePath(Application.ExeName) + 'sae.sql', ExtractFilePath(Application.ExeName) + 'saeok.sql');
    end;

    QrySqlScript := nil;
    QrySqlScript.Free;
  except
    Result := False;
    QrySqlScript := nil;
    QrySqlScript.Free;
  end;

end;

function GetDataExtensoReduzida(Data: TDateTime): String;
var
  XDia: Integer;
  Meses: array[1..12] of String;
  Dia, Mes, Ano: Word;
begin
  Data := Trunc(Data);
  Meses[1]:= 'Jan';
  Meses[2]:= 'Fev';
  Meses[3]:= 'Mar';
  Meses[4]:= 'Abr';
  Meses[5]:= 'Mai';
  Meses[6]:= 'Jun';
  Meses[7]:= 'Jul';
  Meses[8]:= 'Ago';
  Meses[9]:= 'Set';
  Meses[10]:= 'Out';
  Meses[11]:= 'Nov';
  Meses[12]:= 'Dez';

  DecodeDate(Data, Ano, Mes, Dia);
  XDia := DayOfWeek(Data);
  Result := IntToStr(Dia) + Meses[Mes] + IntToStr(Ano);
end;

function GetDataExtensoRelatorio(Data: TDateTime): String;
var
  Meses: array[1..12] of String;
  Dia, Mes, Ano: Word;
begin
  Data := Trunc(Data);

  Meses[1]:= 'Janeiro';
  Meses[2]:= 'Fevereiro';
  Meses[3]:= 'Março';
  Meses[4]:= 'Abril';
  Meses[5]:= 'Maio';
  Meses[6]:= 'Junho';
  Meses[7]:= 'Julho';
  Meses[8]:= 'Agosto';
  Meses[9]:= 'Setembro';
  Meses[10]:= 'Outubro';
  Meses[11]:= 'Novembro';
  Meses[12]:= 'Dezembro';

  DecodeDate(Data, Ano, Mes, Dia);
  Result := IntToStr(Dia) + ' de '+ Meses[Mes] + ' de ' + IntToStr(Ano);
end;


function GetDataExtenso(Data: TDateTime): String;
var
  XDia: Integer;
  DiaSemana: array[1..7] of String;
  Meses: array[1..12] of String;
  Dia, Mes, Ano: Word;
begin
  Data := Trunc(Data);

  DiaSemana[1]:= 'Dom';
  DiaSemana[2]:= 'Seg';
  DiaSemana[3]:= 'Ter';
  DiaSemana[4]:= 'Qua';
  DiaSemana[5]:= 'Qui';
  DiaSemana[6]:= 'Sex';
  DiaSemana[7]:= 'Sáb';
  Meses[1]:= 'Jan';
  Meses[2]:= 'Fev';
  Meses[3]:= 'Mar';
  Meses[4]:= 'Abr';
  Meses[5]:= 'Mai';
  Meses[6]:= 'Jun';
  Meses[7]:= 'Jul';
  Meses[8]:= 'Ago';
  Meses[9]:= 'Set';
  Meses[10]:= 'Out';
  Meses[11]:= 'Nov';
  Meses[12]:= 'Dez';

  DecodeDate(Data, Ano, Mes, Dia);
  XDia := DayOfWeek(Data);
  Result := DiaSemana[XDia] + ', ' + IntToStr(Dia) + ' de '+ Meses[Mes] + ' de ' + IntToStr(Ano);
end;


{ Recupera o nome do usuario logado no sistema }
function RecuperaNomeUsuarioLogado(): String;
var
  QrySqlUL: TADOQuery;
begin
  { Cria query's auxiliares e aponta a conexão }
  QrySqlUL := TADOQuery.Create(DataModuleSGE);
  QrySqlUL.Connection := DataModuleSGE.Conn;

  if Sys_TipoUsuarioSistema = 'C' then
    with QrySqlUL do
    begin
      Close;
      Sql.Clear;
      Sql.Text := 'Select NO_COL from TB03_COLABOR ' +
                  'where CO_COL = ' + Sys_CodigoUsuario +
                  ' AND CO_EMP = ' + IntToStr(Sys_CodigoEmpresaAtiva);
      Open;
      Result := FieldByName('NO_COL').AsString;
      Close;
    end;

  if Sys_TipoUsuarioSistema = 'A' then
    with QrySqlUL do
    begin
      Close;
      Sql.Clear;
      Sql.Text := 'Select NO_ALU from TB07_ALUNO ' +
                  'where CO_ALU = ' + Sys_CodigoUsuario +
                  ' AND CO_EMP = ' + IntToStr(Sys_CodigoEmpresaAtiva);
      Open;
      Result := FieldByName('NO_ALU').AsString;
      Close;
    end;

  QrySqlUL := nil;
  QrySqlUL.Free;
end;


function ImprimeDeclaracao(ADocument: String; TipoParam, DadosTipoParam, TipoDeclaracao: String): Boolean;
var
  WordApp: OLEVariant;
  Search1, Replace1: String;
  CaminhoDoc, Declaracao, Declaracaotmp: String;
  i: Integer;
begin
  Result := False;
  CaminhoDoc := ExtractFilePath(Application.ExeName) + '\Declaracoes\';
  Declaracaotmp := CaminhoDoc + 'tmp_' + ADocument;
  Declaracao := CaminhoDoc + ADocument;
  
  { Delete o arquivo temporario por segurança caso exista }
  DeleteFile(PChar(Declaracaotmp));

  { Verifica se o arquivo.doc existe }
  if not FileExists(Declaracao) then
  begin
    ShowMessage('Documento modelo de declaração não encontrado.');
    Exit;
  end;

  { Create the OLE Object }
  try
    WordApp := CreateOLEObject('Word.Application');
  except
    on E: Exception do
    begin
      E.Message := 'Word não instalado nesse computador.';
      raise;
      exit;
    end;
  end;

  try
    WordApp.Visible := False;
    WordApp.Documents.Open(Declaracao);
    i := 1;

    while GetPiece(TipoParam, '-', i) <> '' do
    begin
      Search1 := GetPiece(TipoParam, '-', i);
      Replace1 := GetPiece(DadosTipoParam, '##', i);
      Replace1 := ReplaceString(Replace1, '', '#'); 

      WordApp.Selection.Find.ClearFormatting;
      WordApp.Selection.Find.Text := Search1;
      WordApp.Selection.Find.Replacement.Text := Replace1;
      WordApp.Selection.Find.Forward := True;
      WordApp.Selection.Find.Wrap := 1;
      WordApp.Selection.Find.Format := False;
      WordApp.Selection.Find.MatchCase := True;
      WordApp.Selection.Find.MatchWholeWord := False;
      WordApp.Selection.Find.MatchWildcards := True;
      WordApp.Selection.Find.MatchSoundsLike := False;
      WordApp.Selection.Find.MatchAllWordForms := False;
      WordApp.Selection.Find.Execute(Replace := 2);

      i := i + 1;
    end;

    WordApp.ActiveDocument.SaveAs(Declaracaotmp);
    
    { Se fez com sucesso }
    Result := True;
//    WordApp.Documents.ReardOly := True;
    WordApp.Visible := True;
//    WordApp.PrintPreview;
  finally
    { Quit Word }
//    WordApp.Quit;
  end;

  { Delete o arquivo temporario depois que imprime }
  DeleteFile(PChar(Declaracaotmp));  
end;


{ Recupera o código chave da tabela do aluno }
function RecuperaCodigoAluno(CodigoMatricula: String): Integer;
var
  QryCodigoAluno: TADOQuery;
begin
  { Cria query's auxiliares e aponta a conexão }
  QryCodigoAluno := TADOQuery.Create(DataModuleSGE);
  QryCodigoAluno.Connection := DataModuleSGE.Conn;

  with QryCodigoAluno do
  begin
    Close;
    Sql.Clear;
    Sql.Text := 'Select CO_ALU from TB80_MASTERMATR ' +
                'where CO_ALU_CAD = ' + '''' + CodigoMatricula + '''' +
                ' AND CO_EMP = ' + IntToStr(Sys_CodigoEmpresaAtiva);
    Open;
    Result := QryCodigoAluno.FieldByName('CO_ALU').AsInteger;
    Close;
  end;
  QryCodigoAluno := nil;
  QryCodigoAluno.Free;
end;

{ Retorna o dia da semana em Extenso de uma determinada data }
function DiaSemana(Data: TDateTime): String;

const
  Dias : Array[1..7] of String[07] = ('DOMINGO', 'SEGUNDA', 'TERCA','QUARTA','QUINTA', 'SEXTA','SABADO');
begin
  Result := Dias[DayOfWeek(Data)];
end;

{ Rotina de Cryptografia. obs: Usando no sistema de segurança para conecta com o BD }
function Crypt(Action, Src, Key : String) : String;
var
   KeyLen    : Integer;
   KeyPos    : Integer;
   offset    : Integer;
   dest      : string;
   SrcPos    : Integer;
   SrcAsc    : Integer;
   TmpSrcAsc : Integer;
   Range     : Integer;
begin
     dest:='';
     KeyLen:=Length(Key);
     KeyPos:=0;
     SrcPos:=0;
     SrcAsc:=0;
     Range:=256;
     if Action = UpperCase('E') then
     begin
          Randomize;
          offset:=Random(Range);
          dest:=format('%1.2x',[offset]);
          for SrcPos := 1 to Length(Src) do
          begin
               SrcAsc:=(Ord(Src[SrcPos]) + offset) MOD 255;
               if KeyPos < KeyLen then KeyPos:= KeyPos + 1 else KeyPos:=1;
               SrcAsc:= SrcAsc xor Ord(Key[KeyPos]);
               dest:=dest + format('%1.2x',[SrcAsc]);
               offset:=SrcAsc;
          end;
     end;
     if Action = UpperCase('D') then
     begin
          offset:=StrToInt('$'+ copy(src,1,2));
          SrcPos:=3;
          repeat
                SrcAsc:=StrToInt('$'+ copy(src,SrcPos,2));
                if KeyPos < KeyLen Then KeyPos := KeyPos + 1 else KeyPos := 1;
                TmpSrcAsc := SrcAsc xor Ord(Key[KeyPos]);
                if TmpSrcAsc <= offset then
                     TmpSrcAsc := 255 + TmpSrcAsc - offset
                else
                     TmpSrcAsc := TmpSrcAsc - offset;
                dest := dest + chr(TmpSrcAsc);
                offset:=srcAsc;
                SrcPos:=SrcPos + 2;
          until SrcPos >= Length(Src);
     end;
     Crypt:=dest;
end;

function fCheckEmail(Email : String): Boolean;
var {sintaxe: nome@provedor.com.br ou outros}
  s: String;
  EPos: Integer;
begin
  EPos:= pos('@',Email);
  if Epos > 1 then
  begin
    s:= copy(EMail,Epos+1,Length(Email));
    if (pos('.',s)> 1) and (pos('.',s)< length(s)) then
    Result := true
  else Result := False;
  end
  else
    Result := False;
end;

function ReplaceString(Text, NewChar, OldChar: String): String;
var
  Cont : Integer;
begin
  if NewChar = OldChar then
  begin
    Result := Text;
    exit;
  end;

  cont := Pos(OldChar,Text);

  while Cont > 0 do
  begin
    Delete(Text,Cont,Length(OldChar));
    Insert(NewChar,Text,Cont);
    cont := Pos(OldChar,Text);
  end;

  Result := Text;
end;

{***********************************************************************************
*                               Procedures
*  Procedures usadas no sistema.
*
***********************************************************************************}

{**********************************************************************************
* Recupera as permissões do usuário para alteração em alguns modulos do sistema.
***********************************************************************************}
procedure RecuperaPermissaoUsuario;
var
  QrySqlUL: TADOQuery;
begin
  Sys_FlaPermBolsaCadastroAluno := False;
  Sys_FlaPermAlteraMenMatGradeAberta := False;
  Sys_FlaPermBloqueiaAluno := False;

  { Cria query's auxiliares e aponta a conexão }
  QrySqlUL := TADOQuery.Create(DataModuleSGE);
  QrySqlUL.Connection := DataModuleSGE.Conn;
  with QrySqlUL do
  begin
    Close;
    Sql.Clear;
    Sql.Text := 'Select * from admusuario ' +
                'where ideadmusuario = ' + Sys_CodigoUsuarioSistema;
    Open;

    if not IsEmpty then
    begin
      if FieldByName('fla_alt_bolsaaluno').AsString = 'S' then
        Sys_FlaPermBolsaCadastroAluno := True;

      if FieldByName('fla_alt_valormensalidademga').AsString = 'S' then
        Sys_FlaPermAlteraMenMatGradeAberta := True;

      if FieldByName('fla_bloq_alunocadastro').AsString = 'S' then
        Sys_FlaPermBloqueiaAluno := True;
    end;

    Close;
  end;

  QrySqlUL := nil;
  QrySqlUL.Free;
end;

procedure ImprimDBGrind(Grid: TDBGrid; TituloRel: String; PosPapel: Integer);
var
  i, CurrentLeft, CurrentTop: integer;
  BMark: TBookmark;
begin
{
  Application.CreateForm(TFrmRelTemplate, FrmRelTemplate);
  FrmRelTemplate.QuickRep1.Dataset:= Grid.DataSource.DataSet;

  if PosPapel = 1 then
    FrmRelTemplate.QuickRep1.Page.Orientation := poPortrait
  else FrmRelTemplate.QuickRep1.Page.Orientation := poLandscape;

  if not FrmRelTemplate.QuickRep1.Bands.HasColumnHeader then
    FrmRelTemplate.QuickRep1.Bands.HasColumnHeader:=true;

  if not FrmRelTemplate.QuickRep1.Bands.HasDetail then
    FrmRelTemplate.QuickRep1.Bands.HasDetail:=true;

  FrmRelTemplate.QuickRep1.Bands.ColumnHeaderBand.Height:=Abs(Grid.TitleFont.Height) + 10;
  FrmRelTemplate.QuickRep1.Bands.DetailBand.Height:=Abs(Grid.Font.Height) + 10;

  CurrentLeft := 12;
  CurrentTop := 6;

  //Record where the user stopped in the DBGrid
  BMark:=Grid.DataSource.DataSet.GetBookmark;

  //Don't let the grid flicker while the report is running
  Grid.DataSource.DataSet.DisableControls;
  try
    for i := 0 to Grid.FieldCount - 1 do
    begin
      if i <> 0 then
        CurrentLeft := CurrentLeft + 15;

      if (CurrentLeft + FrmRelTemplate.Canvas.TextWidth(Grid.Columns[i].Title.Caption)) >
        (FrmRelTemplate.QuickRep1.Bands.ColumnHeaderBand.Width) then
      begin
        CurrentLeft := 12;
        CurrentTop := CurrentTop + FrmRelTemplate.Canvas.TextHeight('A') + 6;
        FrmRelTemplate.QuickRep1.Bands.ColumnHeaderBand.Height := FrmRelTemplate.QuickRep1.Bands.ColumnHeaderBand.Height +
          (FrmRelTemplate.Canvas.TextHeight('A') + 10);
        FrmRelTemplate.QuickRep1.Bands.DetailBand.Height := FrmRelTemplate.QuickRep1.Bands.DetailBand.Height +
          (FrmRelTemplate.Canvas.TextHeight('A') + 10);
      end;

      //Create Header with QRLabels
      with TQRLabel.Create(FrmRelTemplate.QuickRep1.Bands.ColumnHeaderBand) do
      begin
        Parent := FrmRelTemplate.QuickRep1.Bands.ColumnHeaderBand;
        Color := FrmRelTemplate.QuickRep1.Bands.ColumnHeaderBand.Color;
        Left := CurrentLeft;
        Top := CurrentTop;
        Caption:= Grid.Columns[i].Title.Caption;
      end;

      //Create Detail with QRDBText
      with TQRDbText.Create(FrmRelTemplate.QuickRep1.Bands.DetailBand) do
      begin
        Parent := FrmRelTemplate.QuickRep1.Bands.DetailBand;
        Color := FrmRelTemplate.QuickRep1.Bands.DetailBand.Color;
        Left := CurrentLeft;
        Top := CurrentTop;
        Alignment:=Grid.Columns[i].Alignment;
        AutoSize:=false;
        AutoStretch:=true;
        Width:=Grid.Columns[i].Width;
        Dataset:= FrmRelTemplate.QuickRep1.Dataset;
        DataField:=Grid.Fields[i].FieldName;
        CurrentLeft:=CurrentLeft + (Grid.Columns[i].Width) + 15;
      end;
    end;

    //After all, call the QuickRep preview method
    FrmRelTemplate.QuickRep1.Bands.ColumnHeaderBand.Frame.DrawBottom := True;
    FrmRelTemplate.LblTituloRel.Left := 1;
    FrmRelTemplate.LblTituloRel.Width := FrmRelTemplate.PageHeaderBand1.Width;
    FrmRelTemplate.LblTituloRel.Caption := TituloRel;
    FrmRelTemplate.QuickRep1.PreviewModal; //or Preview if you prefer

  finally
    with Grid.DataSource.DataSet do
    begin
      GotoBookmark(BMark);
      FreeBookmark(BMark);
      EnableControls;
      FrmRelTemplate.Free;
    end;
  end;     }
  ShowMessage('Indisponível no momento.');
end;

{ Recupera o estoque atual do produto }
function RecuperaEstoqueAtual(CodProduto: Integer): Double;
begin
  with DataModuleSGE.QrySql do
  begin
    Close;
    Sql.Text := 'Select * from TB96_ESTOQUE where CO_PROD = ' + IntToStr(CodProduto);
    Open;
    Result := FieldByName('QT_SALDO_EST').AsFloat;
    Close;
  end;
end;

{Função para validar somente numeros ou caracteres em uma String}
function IsValid(Ident: string;Modo:integer): Boolean;
const
  Alpha = ['A'..'Z', 'a'..'z', '_'];
  AlphaNumeric = ['0'..'9'];
var
  I: Integer;
  Value: set of char;
begin
  case modo of
    1: Value := Alpha;
    2: Value := AlphaNumeric;
  end;
  Result := False;
  if (Length(Ident) = 0) or not (Ident[1] in Value) then
  begin
    Exit;
  end;
  for I := 2 to Length(Ident) do
  begin
    if not (Ident[I] in Value) then
    begin
      Exit;
    end;
  end;
  Result := True;
end;

function GeraProximoCodigoSimples(nmCampo, nmTabela: String) : integer;
var
  QrySqlRec: TADOQuery;
begin
  QrySqlRec := TADOQuery.Create(DataModuleSGE);
  QrySqlRec.Connection := DataModuleSGE.Conn;

  with QrySqlRec do
  begin
    Close;
    SQL.Clear;
    SQL.ADD('SELECT MAX(' + NmCampo + ') FROM ' + nmTabela);
    Open;
    result := Fields[0].AsInteger + 1;
    Close;
  end;

  QrySqlRec := nil;
  QrySqlRec.Free;

end;

{ Gera os Números Automáticos }
function GeraProximoCodigo(nmCampo, nmTabela: String) : integer;
var
  QrySqlRec: TADOQuery;
begin
  QrySqlRec := TADOQuery.Create(DataModuleSGE);
  QrySqlRec.Connection := DataModuleSGE.Conn;

  with QrySqlRec do
  begin
    Close;
    SQL.Clear;
    SQL.ADD('SELECT MAX(' + NmCampo + ') FROM ' + nmTabela + ' where CO_EMP = ' + IntToStr(Sys_CodigoEmpresaAtiva));
    Open;
    result := Fields[0].AsInteger + 1;
    Close;
  end;
  
  QrySqlRec := nil;
  QrySqlRec.Free;
end;

{ Recupera o nome da tabela que do erro dados na hora da exclusão e retorna uma mensagem }
function RecuperaMsgErroExclusao(Msg: String): String;
var
 Ini, Ini2: integer;
 Res1, Res2: String;
 VarPesq, VarPesq1: String;
begin
  Result := '';
  
  VarPesq := 'table ' + '''';
  VarPesq := '''' + 'TB';
  Ini := pos(VarPesq, Msg);

  VarPesq := Copy(Msg, Ini+1, Length(Msg));
  Res1 := IntToStr(Ini);
  
  VarPesq1 := '''';
  Ini2 := pos(VarPesq1, VarPesq);
  VarPesq1 := Copy(VarPesq, 1, Ini2-1);
  Res2 := VarPesq1;

  with DataModuleSGE.QrySql do
  begin
    Close;
    Sql.Clear;
    Sql.Text := 'Select * from TB999_TABSISTEMA where NO_TABSISTEMA = ' + '''' + Res2 + '''';
    Open;
    if not IsEmpty then
    begin
      Result := FieldByName('DE_TABSISTEMA').AsString;
    end;
    Close;
  end;

end;


{ Recupera a data atual do servidor }
function DataServidor(): TDateTime;
begin
  with DataModuleSGE.QryDataAtualServidor do
  begin
    Close;
    Sql.Clear;
    Sql.Text := 'Select GetDate() DataAtual';
    Open;
    Result := FieldByName('DataAtual').AsDateTime;
    Close;
  end;
end;

{ Transforma minutos em horas e minutos }
function MinToHor(Secs: LongInt): String;
  var
   Hrs, Min: Word;
begin
  Hrs := Secs Div 60;
  Min := Secs Mod 60;
  Result := Format('%d:%d', [Hrs, Min]);
end;

{ Retira um caracter de uma string }
function RetiraString(const s: string; s1: string): string;
begin
  Result := s;
  while Pos(s1, Result) > 0 do
    Delete(Result, Pos(s1, Result), 1);
end;

procedure MoveForm;
begin
//  DeleteMenu(GetSystemMenu(Handle, False), SC_MOVE, MF_BYCOMMAND);
end;

procedure deletefilestmp(NomeArquivo: String);
var
  SearchRec: TSearchRec;
  Result: Integer;
begin
  Result := FindFirst(ExtractTempDir + NomeArquivo, faAnyFile, SearchRec);
  while result = 0 do
  begin
    SetFileAttributesA(PChar(ExtractTempDir+SearchRec.Name), 6);
    DeleteFile(PChar(ExtractTempDir+SearchRec.Name));
    Result := FindNext(SearchRec);
 end;
end;

{*************************************************************************
Gera Arquivo Texto a partir do DataSert
*************************************************************************}
procedure GeraArquivoTexto(DataSet: TDataSet; NomeArq: String);
var
  Registros: TStringList;
  Linha, Espacos, Diretorio: String;
  TamanhoMaximo, i, j: Integer;
begin
  TamanhoMaximo := 0;
  Diretorio := '';
  Registros := TStringList.Create;
  DataSet.Open;
  DataSet.First;
  try
    while not DataSet.Eof do
    begin
      Linha := '';
      for i := 0 to DataSet.FieldCount - 1 do
      begin
        Espacos := '';
        TamanhoMaximo := DataSet.Fields[i].DisplayWidth;

        for j := Length(DataSet.Fields[i].AsString) to TamanhoMaximo do
          Espacos := Espacos + ' ';
        Linha := Linha + DataSet.Fields[i].AsString + Espacos;
      end;
      Registros.Add(Linha);
      DataSet.Next;
    end;
    Diretorio := ExtractFilePath(Application.ExeName) + 'ArquivosTexto';

    if not DirectoryExists(Diretorio) then
      forceDirectories(Diretorio);

    Registros.SaveToFile(Diretorio + '\' + NomeArq + '.txt');
    Application.MessageBox(PChar('Tabela ' + DataSet.Name + ' Exportada.'), 'Avivo',MB_OK);
  finally
    Registros.Free;
    DataSet.Close;
  end;
end;

procedure CriaArquivoTexto(NomeArquivo: String; Tipo: String; Texto: String);
var
  F : TextFile;
begin
  System.Assign(F, ChangeFileExt(NomeArquivo, '.' + Tipo));
  try
    If FileExists(ChangeFileExt(NomeArquivo, '.' + Tipo)) then
    begin
      Append(F);
    end
    else begin
      Rewrite(F);
    end;
    WriteLn(F, Texto);
  finally
    CloseFile(F);
  end;
end;

{*******************************************************************************
*  Retorna o diretório temp do usuário
*******************************************************************************}
function ExtractTempDir : String;
var Buffer : Array[0..144] of Char;
begin
  GetTempPath(144,Buffer);
  Result := StrPas(Buffer);
end;

function GetPiece(Node: string; Delimiter: string; FirstPiece: integer): string;
begin
 Result := GetPieces(Node, Delimiter, FirstPiece, FirstPiece);
// Result := ReplaceString(Result, Delimiter, '');
end;

function GetPieces(Node: string; Delimiter: string; FirstPiece: integer; LastPiece: integer): string;
type
  pieces = array[0..100] of byte;
var
   i , numpcs: integer;
  s1 : string;
  p1, p2: pieces;
begin
i := 1;
numpcs := 0;
s1 := node;
Result := '';
p1[0] := 0;
p2[0] := 0;
while i > 0 do
  begin
    i := pos(Delimiter, s1);
    numpcs := numpcs + 1;
    p1[numpcs] := i;
    p2[numpcs] := i;
    if numpcs > 1 then p2[numpcs] := p2[numpcs] + p2[numpcs - 1];
    s1 := copy(s1,i+1,length(s1)-i);
  end;
p1[numpcs] := length(s1) + 1;
p2[numpcs] := length(s1) + p2[numpcs - 1] + 1;
if (FirstPiece > 0) and (FirstPiece <= LastPiece) then
  begin;
  if LastPiece > numpcs then LastPiece := numpcs;
  Result := copy(node,p2[FirstPiece - 1] + 1 , p2[LastPiece] - p2[FirstPiece - 1] - 1);
  end;
end;

{ Recupera a versão do aplicativo }
procedure GetBuildInfo(var V1, V2, V3, V4: Word);
var
  VerInfoSize, VerValueSize, Dummy : DWORD;
  VerInfo : Pointer;
  VerValue : PVSFixedFileInfo;
begin
  VerInfoSize := GetFileVersionInfoSize(PChar(ParamStr(0)), Dummy);
  GetMem(VerInfo, VerInfoSize);
  GetFileVersionInfo(PChar(ParamStr(0)), 0, VerInfoSize, VerInfo);
  VerQueryValue(VerInfo, '\', Pointer(VerValue), VerValueSize);
  With VerValue^ do
  begin
    V1 := dwFileVersionMS shr 16;
    V2 := dwFileVersionMS and $FFFF;
    V3 := dwFileVersionLS shr 16;
    V4 := dwFileVersionLS and $FFFF;
  end;
  FreeMem(VerInfo, VerInfoSize);
end;

{ Retorna a Versão }
function kfVersionInfo: String;
var
  V1,       // Major Version
  V2,       // Minor Version
  V3,       // Release
  V4: Word; // Build Number
begin
  GetBuildInfo(V1, V2, V3, V4);
  Result := IntToStr(V1) + '.'
            + IntToStr(V2) + '.'
            + IntToStr(V3) + '.'
            + IntToStr(V4);
end;

{Retorna o nome do usuário logado na rede }
function LogUser : String;
var
  lpBuffer: Array[0..20] of Char;
  nSize: dWord;
  Achou: boolean;
  erro: dWord;
begin
  nSize := 120;
  Achou := GetUserName(lpBuffer, nSize);
  if (Achou) then
   result   := lpBuffer
  else
  begin
    Erro   :=GetLastError();
    result :=IntToStr(Erro);
  end;
end;

{ Recupera o path do diretório temporario }
function GetTempDir: String;
var
  TempDir: Array[0..255] of Char;
begin
  GetTempPath(255, @TempDir);
  Result := StrPas(TempDir);
end;

{ Recupera a data que o arquivo foi modificado }
function GetModifyDate(FileName: String): TDateTime;
var
  h: THandle;
  Struct: TOFSTRUCT;
  lastwrite: integer;
  t: TDateTime;
begin
  h := OpenFile(PChar(FileName), Struct, OF_SHARE_DENY_NONE);
  try
    if h<>HFILE_ERROR then
    begin
      lastwrite:=FileGetDate(h);
      Result:= FileDateToDateTime(lastwrite);
    end;
  finally
    _lclose(h);
  end;
end;

function Replica(pStr: string; pLen: integer; pCar: string): string;
var
  I : integer;
  s : string;
begin
  pStr := Trim(pStr);
  s := '';
  for I := 1 to (pLen - Length(pStr)) do
  begin
    s := s + pCar;
  end;
  replica := s + pStr;
end;

function ReplicaDireita(pStr: string; pLen: integer; pCar: string): string;
var
  I : integer;
  s : string;
begin
  pStr := Trim(pStr);
  s := '';
  for I := 1 to (pLen - Length(pStr)) do
  begin
    s := pCar + s;
  end;
  ReplicaDireita := pStr + s;
end;


function MinParaSeg(Hora : String) : integer;
begin
  result := StrToInt(copy(Hora,1,3))*60 + StrToInt(Copy(Hora,4,2));
end;

function SegParaMin(Hora : integer) : String;
begin
  result := formatFloat('000',(Hora div 60)) +
            formatFloat('00', (Hora mod 60));
end;

function SegParaHora(Hora : integer) : String;
begin
  result := formatFloat('000',(Hora div 3600)) +
            formatFloat('00' ,((Hora mod 3600) div 60)) +
            formatFloat('00' ,(((Hora mod 3600) mod 60) mod 60));
end;

function FormataTempo(Tempo: string): string;
begin
 result:= (Copy(tempo,1,3)+'h'+copy(tempo,4,2)+'´'+ copy(tempo,6,2)+'"');
end;

function StrZero(iNumero: integer; iTamanho: integer; sCaracter: char): string;
begin
   Result := StringOfChar(sCaracter,(iTamanho-Length(InttoStr(iNumero))))+IntToStr(iNumero);
end;

function Dia(Data: TdateTime ) : Word;
{ Objetivo.....: Retorna o dia de uma data
  Entrada......: DATA : TDateTime - data para obtenção do dia
  Saída........: Result : Word - dia da data passada }
var d,m,a : Word;
begin
   DecodeDate( Data, a, m, d);
   Result:= d;
end;
 
function Mes( Data:TdateTime ) : Word;
{ Objetivo.....: Retorna o Mês de uma data
  Entrada......: DATA : TDateTime - data para obtenção do dia
  Saída........: Result : Word - mês da data passada }
var d,m,a : Word;
begin
   DecodeDate( Data, a, m, d);
   Result:= m;
end;

function Ano( Data:TdateTime ) : Word;
{ Objetivo.....: Retorna o ano de uma data
  Entrada......: DATA : TDateTime - data para obtenção do dia
  Saída........: Result : Word - ano da data passada }
var d,m,a : Word;
begin
   DecodeDate( Data, a, m, d);
   Result:= a;
end;



function DiasNoMes( iMes, iAno : Word ) : Word;
const
  DiaMes: array[1..12] of Integer =
    (31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);	{ Meses normais }
Var
  Bissexto : Boolean;
begin
  // Verifica BisSexto
 Bissexto:=(iAno mod 4 = 0)     { anos divisiveis por 4 são... }
      and ((iAno mod 100 <> 0)  { ...exceto anos de séculos... }
        or (iAno mod 400 = 0)); { ...a não ser que seja divisível por 400 }
 Result := DiaMes[iMes];
 if ((iMes=2) and Bissexto) then Inc(Result);
end;

function fPadl( sCaracter:String; iTamanho:Integer ) : String;
{Objetivo..: Retornar um string alinhado a esquerda
 Entrada...: Entrada : String - String a ser alinhado
             itamanho: Word   - Tamanho de retorno
 Saída.....: Result  : String  - Valor de entrada alinhado à esquerda }
begin
   sCaracter:=Copy(sCaracter,1,iTamanho);
   Result := sCaracter+StringOfChar(' ',(iTamanho-Length(sCaracter)));
end;

function fPadr( sCaracter:String; iTamanho:Integer ) : String;
{Objetivo..: Retornar um string alinhado à direita
 Entrada...: Entrada : String - String a ser alinhado
             itamanho: Word   - Tamanho de retorno
 Saída.....: Result  : String  - Valor de entrada alinhado à direita }
begin
   sCaracter:=Copy(sCaracter,1,iTamanho);
   Result := StringOfChar(' ',(iTamanho-Length(sCaracter)))+sCaracter;
end;

function fArred( Base : Extended; Casas:Word ) : Extended;
{Objetivo..: Retornar o valor (Base) arredondado para N (Casas) decimais
 Entrada...: Base : Extended    - Valor base
             Casas : Word       - Numero de casas
 Saída.....: Result  : Extended - com o valor arredondado}
var
 i : Comp;
 e : Extended; 
begin
   i:=Int (Base * Power(10,Casas));
   e:=Frac(Base * Power(10,Casas));
   Result := i + Int( e * 2 );
   Result := Result / Power(10,Casas);
end;

function fFloatAsStr( Valor : Real; Precisao : Word ) : String;
{Objetivo...: Obter um valor Real no formato string com um ponto'.' separador
              de decimais em vez de ','.
              Foi criado para resolver problemas de gravação de dados no oracle
 Entrada....: Valor : Real - Valor base para a conversão
 Saída......: Result : String - String com o 'Valor' convertido e ',' como
              separador decimal}
Var 
 Posicao: Word;
Begin
 Result:=FormatFloat( '0.'+StringOfChar('0',Precisao), Valor);
 Posicao:=Pos(',',Result);
 if Posicao <> 0 then
    Result[Posicao]:='.';
end;

Procedure DataFutura( Data : TDateTime );
begin
 If Data > Now then
  Begin
    Showmessage('Não pode ser maior que a data corrente.'+#13#10+
    'Data informada ('+DateToStr(Data)+') > '+
    'data do sistema ('+DateToStr(Now)+')');
    Raise EAbort.Create('');
  end  
end;    

function DVNuNosso( var NumeroNosso : String) : Boolean;
{Objetivo...: Calcula o dígito verificador para as guias de 12 dígitos
 Entrada....: var NumeroNosso : String - Numero com 12 posições. Caso o digito
              seja inválido ele será substituido pelo dígito correto.
 Saída......: result : Boolean - indica se o digito estava correto(TRUE) ou não.
}
const Multiplicadores : array[1..11] of word = (7,8,9,2,3,4,5,6,7,8,9);
var itemp1, iDV : Word;
    sDV : String;
begin
 Result:=False;
 iDV:=0;
 For iTemp1:=1 to 11 do
     iDV:=iDV+ Multiplicadores[Itemp1] * StrToInt(NumeroNosso[Itemp1]);
     
 iDV:=(iDV mod 11);          // módulo 11
 if iDV = 10 then sDV:='X' else sDV:=IntToStr(iDV);
 
 if NumeroNosso[12] = sDV then Result:=True; // Digito correto
 
 NumeroNosso:=Copy(NumeroNosso, 1, 11)+sDV;
end;

function RetNuNosso( var NumeroNosso : String) : String;
{Objetivo...: Calcula o dígito verificador para as guias de 12 dígitos
 Entrada....: var NumeroNosso : String - Numero com 12 posições. Caso o digito
              seja inválido ele será substituido pelo dígito correto.
 Saída......: result : NumeroNosso com o DV correto.
}
const Multiplicadores : array[1..11] of word = (7,8,9,2,3,4,5,6,7,8,9);
var itemp1, iDV : Word;
    sDV : String;
begin
 Result:='';
 iDV:=0;
 For iTemp1:=1 to 11 do
     iDV:=iDV+ Multiplicadores[Itemp1] * StrToInt(NumeroNosso[Itemp1]);

 iDV:=(iDV mod 11);          // módulo 11
 if iDV = 10 then sDV:='X' else sDV:=IntToStr(iDV);

 Result:=Copy(NumeroNosso, 1, 11)+sDV; // Digito correto
end;

function fAnoMesMM( sAnoMes:String; iMeses : Integer) : String;
{Objetivo....: Somar ou subtrair meses a uma data tipo AAAAMM
 Entrada.....: sAnoMes : String - com a data no formato AAAAMM
               iMeses  : Word   - número de meses que deve ser somado ou 
                                  subtraido à data em questão
 Saída.......: String - com a data resultante da operação no formato AAAAMM
}
var
   Mes, Ano, Meses : Integer;
begin
 Meses:=(StrToInt(Copy(sAnoMes,1,4))*12) + StrToInt(Copy(sAnoMes,5,2));
 Meses:=(StrToInt(Copy(sAnoMes,1,4))*12) + StrToInt(Copy(sAnoMes,5,2))+iMeses;
 If ((Meses mod 12) = 0) then
  begin
    Ano:=Round(Int(Meses / 12))-1;
    Mes:=12;
  end
 else
  begin
    Ano:=Round(Int(Meses / 12));
    Mes:=(Meses mod 12);
  end;
 Result:=StrZero(Ano,4,'0')+StrZero(Mes,2,'0');
end;


function fAnoMesToDateTime( sAnoMes:String; iDia:Word) : TDateTime;
{Objetivo....: Somar ou subtrair meses a uma data tipo AAAAMM
 Entrada.....: sAnoMes : String - com a data no formato AAAAMM
               iDia    : Word   - Dia do mês a ser usado da conversão
 Saída.......: TDateTime - com a data resultante da operação
}
begin
 Result:=EncodeDate(StrToInt(Copy(sAnoMes,1,4)), 
                    StrToInt(Copy(sAnoMes,5,2)), 
                    iDia);
end;

function Extenso(Valor: extended): string;
var Centavos, Centena, Milhar, Milhao, Bilhao, Texto: string;
const Unidades: array[1..9] of string = ('um','dois','três','quatro','cinco','seis',
      'sete','oito','nove');
      Dez: array[1..9] of string = ('onze', 'doze', 'treze', 'quatorze', 'quinze',
      'dezesseis', 'dezessete', 'dezoito', 'dezenove');
      Dezenas: array[1..9] of string = ('dez', 'vinte', 'trinta', 'quarenta',
      'cinquenta', 'sessenta', 'setenta', 'oitenta', 'noventa');
      Centenas: array[1..9] of string = ('cento', 'duzentos', 'trezentos', 'quatrocentos',
      'quinhentos', 'seiscentos', 'setecentos', 'oitocentos', 'novecentos');

 function Ifs(Expressao: boolean; CasoVerdadeiro, CasoFalso: string): string;
 begin
    if Expressao then Result := CasoVerdadeiro else Result := CasoFalso;
 end;

 function MiniExtenso(Valor: shortstring): string;
 var Unidade, Dezena, Centena: string;
 begin
    if (Valor[2]='1') and (Valor[3]<>'0') then
    begin
       Unidade := Dez[StrtoInt(Valor[3])];
       Dezena  := '';
    end
    else
    begin
       if Valor[2] <> '0' then Dezena  := Dezenas[StrtoInt(Valor[2])];
       if Valor[3] <> '0' then Unidade := Unidades[StrtoInt(Valor[3])];
    end;

    if (Valor[1]='1') and (Unidade = '') and (Dezena = '') then
       Centena := 'cem'
    else
       if Valor[1] <> '0' then
          Centena := Centenas[StrtoInt(Valor[1])]
       else
          Centena := '';

    Result := Centena + Ifs((Centena<>'') and ((Dezena<>'') or (Unidade<>'')),' e ', '') +
              Dezena + Ifs((Dezena<>'')and(Unidade<>''),' e ', '') + Unidade;
 end;

{Agora sim começa a função em si}
begin
   if Valor = 0 then
   begin
      Result := '';
      Exit;
   end;
   Texto := FormatFloat('000000000000.00',Valor);
   Centavos := MiniExtenso('0' + Copy(texto, 14, 2));
   Centena := MiniExtenso(Copy(Texto, 10, 3));

   Milhar := MiniExtenso(Copy(Texto, 7, 3));
   if Milhar <> '' then Milhar := Milhar + ' mil';

   Milhao := MiniExtenso(Copy(Texto, 4,3));
   if Milhao <> '' then Milhao := Milhao + Ifs(Copy(Texto, 4,3)='001', ' milhão', ' milhões');

   Bilhao := MiniExtenso(Copy(Texto, 1,3));
   if Bilhao <> '' then Bilhao := Bilhao + Ifs(Copy(Texto, 1,3)='001', ' bilhão', ' bilhões');

   if (Bilhao<>'') and (Milhao + Milhar + Centena='') then
      Result := Bilhao + ' de reais'
   else if (Milhao<>'') and (Milhar+Centena = '') then
      Result := Milhao + ' de reais'
   else
      Result := Bilhao + Ifs((Bilhao<>'') and (Milhao+Milhar+Centena<>''),
                Ifs((Pos(' e ', Bilhao)>0) or (Pos(' e ', Milhao+Milhar+Centena)>0),', ',' e '), '') +
                Milhao + Ifs((Milhao<>'') and (Milhar+Centena<>''),
                Ifs((Pos(' e ', Milhao)>0) or (Pos(' e ', Milhar + Centena)>0),', ',' e '),'') +
                Milhar + Ifs((Milhar<>'') and (Centena<>''), Ifs(Pos(' e ', Centena)>0,', ',' e '),'') +
                Centena + Ifs(Int(Valor)=1, ' real', ' reais');
      if Centavos <> '' then
         Result := Result + ' e ' + Centavos + Ifs(Copy(Texto,14,2)='01',' centavo',' centavos');

   Result := UpperCase(Copy(Result,1,1)) + Copy(Result,2,Length(Result)); // + '.';

end;

Function PISValido( nuPis:String ) : Boolean;
{Objetivo..: Validar o Dígito Verificador do PIS
 Entrada...: nuPIS : String - Número a ser validado
 Entrada...: Result : Boolean - Indica (true) DV válido (False) DV inválido
}
const aDiv : array [1..10] of Word = (3,2,9,8,7,6,5,4,3,2);
var   iTemp : Word; 
      Resto : Integer;
begin
 Result:=False;
 Resto :=0;
 For iTemp:=1 to 10 do Resto :=Resto + StrToInt(nuPis[Itemp])*aDiv[Itemp];
 Resto:=(Resto mod 11);
 Resto:=11 - Resto;
 If (Resto >9 ) then Resto:=0;
 If (StrToInt(nuPis[11]) = Resto) then Result:=True;
end;

procedure RecuperaParametrosDaEmpresa;
var
  Ano, Mes, Dia: Word;
  SqlString: String;
begin
  { Recupera a configuração padrão para a secretaria }
  DecodeDate(Now, Ano, Mes, Dia);
  with DataModuleSGE.QrySql do
  begin
    Close;
    Sql.Clear;
    Sql.text := 'Select * ' +
                'From TB25_EMPRESA WHERE CO_EMP = ' + IntToStr(Sys_CodigoEmpresaAtiva);
    Open;

    if not IsEmpty then
    begin
      Sys_CodigoHistoricoBiblioteca := FieldByName('CO_HIST_BIB').AsInteger;
      Sys_CodigoLancCtaReceberBiblioteca := FieldByName('CO_CTABIB_EMP').AsInteger;
      Sys_CodigoCentroCustoBiblioteca := FieldByName('CO_CENT_CUSBIB').AsInteger;

      { Gera matrícula automatica }
      Sys_FlaGeraMatriculaAuto := True;
      if FieldByName('FLA_GERA_MAT_AUTO').AsString = 'N' then
        Sys_FlaGeraMatriculaAuto := False;

      Sys_CodigoHistoricoCR := FieldByName('CO_HIST_MAT').AsInteger;
      Sys_CodigoLancCtaContabilCR := FieldByName('CO_CTAMAT_EMP').AsInteger;
      Sys_CodigoCentroCustoCR := FieldByName('CO_CENT_CUSMAT').AsInteger;

      { Solicitações }
      Sys_CodigoHistoricoSolicitacoes := FieldByName('CO_HIST_SOL').AsInteger;
      Sys_CodigoLancCtaContabilSolicitacoes := FieldByName('CO_CTSOL_EMP').AsInteger;
      Sys_CodigoCentroCustoSolicitacoes := FieldByName('CO_CENT_CUSSOL').AsInteger;

      { Vestibular }
      Sys_CodigoHistoricoLancVestibular := FieldByName('CO_HIST_INSC').AsInteger;
      Sys_CodigoLancCtaContabilVestibular := FieldByName('CO_CTAINS_EMP').AsInteger;
      Sys_CodigoCentroCustoVestibular := FieldByName('CO_CENT_CUSINSC').AsInteger;

      { Dados do boleto bancario }
      Sys_DadosBoletoBancario := True;
      if FieldByName('IDEBANCO').AsString = '' then
        Sys_DadosBoletoBancario := False;
    end;
  end;

  Sys_FlaPeriodoReservaMat := True;
  Sys_FlaPeriodoInscMat := True;
  Sys_FlaPeriodoMatricula := True;
  Sys_FlaPeriodoTranMat := True;
  Sys_FlaPeriodoTransfMat := True;
  Sys_FlaPeriodoManGradeAluno := True;
  Sys_FlaTrancaMatFin := True;
  Sys_FlaPeriodoAlteracaoMat := True;

  if DataModuleSGE.QrySql.FieldByName('TP_DT_CTSEC').AsString = 'E' then
  begin
    { Recupera as datas de controle }
    with DataModuleSGE.QrySql3 do
    begin
      Close;
      Sql.Clear;
      Sql.text := 'Select * ' +
                  'From TB82_DTCT_EMP WHERE CO_EMP = ' + IntToStr(Sys_CodigoEmpresaAtiva);
      Open;

      if FieldByName('DT_INI_RES').AsString <> '' then
        { Critica parâmetro de reserva }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_RES').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_RES').AsDateTime)) then
          Sys_FlaPeriodoReservaMat := True
        else Sys_FlaPeriodoReservaMat := False;

       if FieldByName('DT_INI_INSC').AsString <> '' then
        { Critica parâmetro de inscrição }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_INSC').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_INSC').AsDateTime)) then
          Sys_FlaPeriodoInscMat := True
        else Sys_FlaPeriodoInscMat := False;

       if FieldByName('DT_INI_MAT').AsString <> '' then
        { Critica parâmetro de reserva }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_MAT').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_MAT').AsDateTime)) then
          Sys_FlaPeriodoMatricula := True
        else Sys_FlaPeriodoMatricula := False;

       if FieldByName('DT_INI_TRAN').AsString <> '' then
        { Critica parâmetro de reserva }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_TRAN').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_TRAN').AsDateTime)) then
          Sys_FlaPeriodoTranMat := True
        else Sys_FlaPeriodoTranMat := False;

        if FieldByName('DT_INI_ALTMAT').AsString <> '' then
        { Critica parâmetro de reserva }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_ALTMAT').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_ALTMAT').AsDateTime)) then
          Sys_FlaPeriodoAlteracaoMat := True
        else Sys_FlaPeriodoAlteracaoMat := False;
      Close;
    end;
  end;
end;


procedure RecuperaParametrosDaEscola(CodCurso: String);
var
  Ano, Mes, Dia: Word;
  SqlString, TipoCritDataContole: String;
  CodEmpresa: Integer;
begin

  { Recupera a configuração padrão para a secretaria }
  DecodeDate(Now, Ano, Mes, Dia);
  with DataModuleSGE.QrySql do
  begin
    Close;
    Sql.Clear;
    Sql.text := 'Select CO_EMP, TP_DT_CTSEC ' +
                'From TB25_EMPRESA WHERE FLA_UNID_ATIVA = ' + '''' + 'S' + '''';
    Open;

    CodEmpresa := FieldByName('CO_EMP').AsInteger;

    if (FieldByName('TP_DT_CTSEC').AsString = 'N') or (FieldByName('TP_DT_CTSEC').AsString = '') then
    begin
      Sys_FlaPeriodoReservaMat := True;
      Sys_FlaPeriodoInscMat := True;
      Sys_FlaPeriodoMatricula := True;
      Sys_FlaPeriodoTranMat := True;
      Sys_FlaPeriodoTransfMat := True;
      Sys_FlaPeriodoManGradeAluno := True;
      Sys_FlaTrancaMatFin := True;
      Sys_FlaPeriodoAlteracaoMat := True;
      Close;
      Exit;
    end;

    TipoCritDataContole := FieldByName('TP_DT_CTSEC').AsString;

    if TipoCritDataContole = 'E' then
    begin
      Close;
      Sql.text := 'Select * From TB82_DTCT_EMP Where CO_EMP = ' + IntToStr(CodEmpresa);
    end;

    if TipoCritDataContole = 'C' then
    begin
      if CodCurso <> '' then
      begin
        Close;
        Sql.text := 'Select * From TB81_DATA_CTRL Where CO_CUR = ' + CodCurso;
      end;
    end;

    Open;

      if FieldByName('DT_INI_RES').AsString <> '' then
        { Critica parâmetro de reserva }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_RES').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_RES').AsDateTime)) then
          Sys_FlaPeriodoReservaMat := True
        else Sys_FlaPeriodoReservaMat := False;

       if FieldByName('DT_INI_INSC').AsString <> '' then
        { Critica parâmetro de inscrição }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_INSC').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_INSC').AsDateTime)) then
          Sys_FlaPeriodoInscMat := True
        else Sys_FlaPeriodoInscMat := False;

       if FieldByName('DT_INI_MAT').AsString <> '' then
        { Critica parâmetro de reserva }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_MAT').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_MAT').AsDateTime)) then
          Sys_FlaPeriodoMatricula := True
        else Sys_FlaPeriodoMatricula := False;

       if FieldByName('DT_INI_TRAN').AsString <> '' then 
        { Critica parâmetro de reserva }
        if (Trunc(DataServidor) >= Trunc(FieldByName('DT_INI_TRAN').AsDateTime)) and
           (Trunc(DataServidor) <= Trunc(FieldByName('DT_FIM_TRAN').AsDateTime)) then
          Sys_FlaPeriodoTranMat := True
        else Sys_FlaPeriodoTranMat := False;

    Close;
  end;
end;

procedure TrocaTABporENTER(Sender: TObject; var Key: Char);
{Objetivo...: Trocar a tecla ENTER pela tecla TAB nos forms desejados.
              Para que esta procedure funcione, passe a propriedade (KeyPreview)
              do form para true e no evento OnKeyPress insira o seguinte código:
              
              TrocaTABporENTER(Sender, Key);
              Ex.:
                   procedure TFormXXX.FormKeyPress(Sender: TObject; var Key: Char);
                   begin
                     TrocaTABporENTER(Sender, Key);
                   end;
 Entrada....: Sender e Key passadas pelo OnKeyPreview
}
begin
{ if Key = #13 then
   if not (Screen.ActiveControl is TDBGrid) then
    begin
      Key := #0;
      (Sender as TControl).Perform(WM_NEXTDLGCTL, 0, 0);
    end
   else
     with TDBGrid(Screen.ActiveControl) do
       if selectedindex < (fieldcount -1) then
         selectedindex := selectedindex +1
       else
        begin
         selectedindex := 0;
         with DataSource.DataSet do
          if (state = dsInsert) then
             Insert
          else
           begin
             Next;
             if EOF then Insert;
           end;
        end; }
//
 if Key = #13 then
  begin
    Key := #0;
    if not (Screen.ActiveControl is TDBGrid) then
      (Sender as TControl).Perform(WM_NEXTDLGCTL, 0, 0)
     else
       with TDBGrid(Screen.ActiveControl) do
        if selectedindex < (fieldcount -1) then
          selectedindex := selectedindex +1
        else
         begin
          selectedindex := 0;
          with DataSource.DataSet do
           if (state = dsInsert) then
              Append
           else
            begin
              Next;
              if EOF then Append;
            end;
         end;
  end;

end;

function EnumWndProc( Hwnd: THandle; Found: PHWND) : Bool; Export; stdcall;
{Objetivo..: Verificar se uma aplicação já se encontra em execução

 Obs.......: Para usa esta função:
             Troque na linha "if AnsiCompareText (ClassName, 'TF_fMenu')=0 then"
             o string 'TF_fMenu' pelo nome da class da janela principal de sua
             aplicação;
             Use um código semelhante ao código abaixo no projeto:
             
             Var
               OldHwnd: Thandle;
             begin
              OldHwnd := 0;
              EnumWindows (@EnumWndProc, LongInt (@OldHwnd));
              If OldHwnd = 0 then
               begin
                 Application.Initialize;
                 Application.CreateForm(TForm1, Form1);
                 Application.Run;
               end
              else
               SetForegroundWindow(OldHwnd);
             end.
}
var
   ClassName, ModuleName, WinModuleName : String;
   WinInstance: THandle;
begin
   Result:=True;
   SetLength (ClassName, 100);
   GetClassName (Hwnd, Pchar(ClassName), Length (ClassName));
   ClassName:=Pchar(ClassName);
   if AnsiCompareText (ClassName, 'TF_fMenu') = 0 then
    begin
      SetLength (ModuleName, 200);
      SetLength (WinModuleName, 200);
      GetModuleFileName(HInstance, PChar(ModuleName), Length(ModuleName));
      WinInstance := GetWindowLong (Hwnd, gwl_HInstance);
      GetModuleFileName( WinInstance, PChar (WinModuleName), Length (WinModuleName));
      if AnsiCompareText(ModuleName, WinModuleName) = 0 then
       begin
         Found^ := Hwnd;
         Result := False;
       end;
    end;
end;


function Critica_CGC(CGC: String) : Boolean;
var
  Num : array[1..15] of Integer;
  Resto, Soma, DV, DV2 : Integer;
begin
  Result := True;
  if CGC = '' then
    exit;
  Result := False;
  // Cada variável Num armazena um dígito do CGC
  Num[1] := StrToInt(Copy(CGC, 1, 1));
  Num[2] := StrToInt(Copy(CGC, 2, 1));
  Num[3] := StrToInt(Copy(CGC, 3, 1));
  Num[4] := StrToInt(Copy(CGC, 4, 1));
  Num[5] := StrToInt(Copy(CGC, 5, 1));
  Num[6] := StrToInt(Copy(CGC, 6, 1));
  Num[7] := StrToInt(Copy(CGC, 7, 1));
  Num[8] := StrToInt(Copy(CGC, 8, 1));
  Num[9] := StrToInt(Copy(CGC, 9, 1));
  Num[10] := StrToInt(Copy(CGC, 10, 1));
  Num[11] := StrToInt(Copy(CGC, 11, 1));
  Num[12] := StrToInt(Copy(CGC, 12, 1));
  Num[13] := StrToInt(Copy(CGC, 13, 1));
  Num[14] := StrToInt(Copy(CGC, 14, 1));
  // ***
  Soma := Num[1] * 5 + Num[2] * 4 + Num[3] * 3 + Num[4] * 2 + Num[5] * 9 +
          Num[6] * 8 + Num[7] * 7 + Num[8] * 6 + Num[9] * 5 + Num[10] * 4 +
          Num[11] * 3 + Num[12] * 2;
  Resto := Soma - (11 * Trunc(Int(Soma / 11)));
  // ***
  if (Resto = 0) Or (Resto = 1) Then
     DV := 0
  else
     DV := 11 - Resto;
  // ***
  if DV = Num[13] then
  begin
     Soma := Num[1] * 6 + Num[2] * 5 + Num[3] * 4 + Num[4] * 3 + Num[5] * 2 +
             Num[6] * 9 + Num[7] * 8 + Num[8] * 7 + Num[9] * 6 + Num[10] * 5 +
             Num[11] * 4 + Num[12] * 3 + DV * 2;
     Resto := Soma - (11 * Trunc(Int(Soma / 11)));
     // ***
     if (Resto = 0) or (Resto = 1) then
       DV2 := 0
     else
       DV2 := 11 - Resto;
     // ***
     if (DV2 = Num[14]) Then
       Result := True;
  end;
end;


function fDifAnoMes( sAnoMesFinal, sAnoMesInicial : String): Integer;
{Objetivo...: Obter o número de meses entre uma data final e a data inicial
 Entrada....: sAnoMesFinal   : String  - Data final   no formato AAAAMM
              sAnoMesInicial : String  - Data inicial no formato AAAAMM
 Saída......: Result         : Integer - Número de meses entre uma data e outra
}
begin
 // Número de meses do AnoMes Final
 Result:=StrToInt(Copy(sAnoMesFinal  ,1,4))*12+
         StrToInt(Copy(sAnoMesFinal  ,5,6));
 // menos o número de meses do AnoMes Inicial
 Result:=Result - 
         (StrToInt(Copy(sAnoMesInicial,1,4))*12+
          StrToInt(Copy(sAnoMesInicial,5,6)));
end;

function Critica_CPF(CPF: String) : Boolean;
var
  Num : array[1..11] of Integer;
  Resto, Soma, DV, DV2 : Integer;
begin
  Result := True;
 { if CPF = '' then
    exit;
    CPF := ReplaceString(CPF,'','.');
    CPF := ReplaceString(CPF,'',',');
    CPF := ReplaceString(CPF,'','-');
    CPF := ReplaceString(CPF,'','/');
  Result := False;
  // Cada variável Num armazena um dígito do CGC
  Num[1] := StrToInt(Copy(CPF, 1, 1));
  Num[2] := StrToInt(Copy(CPF, 2, 1));
  Num[3] := StrToInt(Copy(CPF, 3, 1));
  Num[4] := StrToInt(Copy(CPF, 4, 1));
  Num[5] := StrToInt(Copy(CPF, 5, 1));
  Num[6] := StrToInt(Copy(CPF, 6, 1));
  Num[7] := StrToInt(Copy(CPF, 7, 1));
  Num[8] := StrToInt(Copy(CPF, 8, 1));
  Num[9] := StrToInt(Copy(CPF, 9, 1));
  Num[10] := StrToInt(Copy(CPF, 10, 1));
  Num[11] := StrToInt(Copy(CPF, 11, 1));
  // ***
  Soma := Num[1] * 10 + Num[2] * 9 + Num[3] * 8 + Num[4] * 7 + Num[5] * 6 +
          Num[6] * 5 + Num[7] * 4 + Num[8] * 3 + Num[9] * 2;
  Resto := Soma - (11 * Trunc(Int(Soma / 11)));
  // ***
  if (Resto = 0) or (Resto = 1) then
    DV := 0
  else
    DV := 11 - Resto;
  // ***
  if (DV = Num[10]) then
  begin
    Soma := Num[1] * 11 + Num[2] * 10 + Num[3] * 9 + Num[4] * 8 + Num[5] * 7 +
            Num[6] * 6 + Num[7] * 5 + Num[8] * 4 + Num[9] * 3 + Num[10] * 2;
    Resto := Soma - (11 * Trunc(Int(Soma / 11)));
    // ***
    if (Resto = 0) or (Resto = 1) then
      DV2 := 0
    else
      DV2 := 11 - Resto;
    // ***
    if (DV2 = Num[11]) then
      Result := True;
  end;        }
end;

procedure TrocaDataHoradoSistema( DataHora:TDateTime );
var
   lpstDataHora : TSystemTime;
begin
   DateTimeToSystemTime(DataHora, lpstDataHora);
   SetLocalTime(lpstDataHora);
end;

function Critica_CEI(var Numero : String) : Boolean;
var Soma, Resto, DV : Integer;
begin
 Soma:=
       (StrToInt(Numero[ 6]) * 9) +
       (StrToInt(Numero[ 7]) * 8) +
       (StrToInt(Numero[ 8]) * 7) +
       (StrToInt(Numero[ 9]) * 6) +
       (StrToInt(Numero[10]) * 5) +
       (StrToInt(Numero[11]) * 4) +
       (StrToInt(Numero[12]) * 3) +
       (StrToInt(Numero[13]) * 2) ;

  Resto:=Soma Mod 11;
  if ((Resto=0) or (Resto=1)) then
     Dv:=0
  else
     Dv:=11-Resto;

  If (StrToInt(Numero[14]) <> Dv) then
     Result:=False
  else
     Result:=True;

  Numero:=Copy(Numero,1,13)+IntToStr(DV);
end;

function fMesMMM( mes : integer):String;
Const
 MesesMMM : array[1..13] of String[3] = ('Jan','Fev','Mar','Abr','Mai','Jun','Jul','Ago','Set','Out','Nov','Dez','13o');
begin
 Result:=MesesMMM[mes];
end;

function Direita(Campo: String; Tamanho : Integer) : String;
Begin
   Result := StringofChar(' ',(Tamanho - Length(Campo)))+Campo;
End;

// Retorna o diretório temp do windows - Gelson
function WindowsTempDir : String;
var sWinDir: String;
    iLength: Integer;
begin
 // Initialize Variable
 iLength := 50;
 setLength(sWinDir, iLength);
 iLength := GetWindowsDirectory(PChar(sWinDir), iLength);
 setLength(sWinDir, iLength);
 Result:=sWinDir+ '\TEMP';
end;

procedure TrocarImpressora(var Relatorio : TQuickRep);
var PrintDialog1 : TPrintDialog;
    temp : String;
    posicao, i : Word;
begin
 i:=Printer.Printers.Count;
 PrintDialog1:= TPrintDialog.Create(Application);
 PrintDialog1.Execute;
 if (i=Printer.Printers.Count) then
    Relatorio.PrinterSettings.PrinterIndex:=Printer.PrinterIndex
 else  // o código abaixo serve para resolver um problema no Windows NT 4
  begin
    temp:=Printer.Printers[Printer.Printerindex];
    posicao:=Pos('ON NE', upperCase(temp));
    if posicao >0 then
     begin
       temp := copy(temp, 1, posicao-2);
       i:=0;
       While i < (Printer.Printers.Count-1) do
        begin
           if (Printer.Printers[i] = temp) then Break;
           Inc(i);
        end;
     end;                              
    Relatorio.PrinterSettings.PrinterIndex:=i;
 end;
end;

function fvalidaDVProcesso( Numero : String) : Boolean;
(*
Função....: fvalidaDVProcesso
Objetivo..: Validar o dígito verificador de um processo
Entrada...: Numero : string[15]
Saída.....: Boolean indicando True - Dígito válido | False - Dígito ou número inválido
*)
var
  nDig1, nDig2, sNumGerado : String;

  function CalculaDigito( sProc : String; nIndice:Word ) : String;
  var
   nSoma, nMax : Word;
  begin
   nSoma := 0;
   nMax:=nIndice;
   While nIndice >=2 do
    begin
      nSoma    := nSoma + StrToInt(Copy(sProc, (nMax-nIndice)+1, 1)) * nIndice;
      nIndice  := nIndice - 1;
    end;
      nSoma    := 11 - (nSoma mod 11);
      if nSoma = 10 then
         nSoma := 0
      else
       if nSoma= 11 then
          nSoma:= 1;
   Result:=IntToStr(nSoma);
  end;

begin
 Result:=False;
 Try
   sNumGerado :=Copy(Numero,1,13);
   nDig1 := CalculaDIgito(sNumGerado, 14);
   sNumGerado := sNumGerado + nDig1;
   nDig2 := CalculaDIgito(sNumGerado, 15);
   sNumGerado := sNumGerado + nDig2;
   if Copy(Numero,14,2) = Copy(sNumgerado,14,2) then
      Result:=True;
 except    
 end;
end;                                

procedure CorrigirArquivo( ArqOrigem, ArqDestino : String);
(*
 Este cópia de arquivo corrige o fim de linha quando apenas o carriage-return separar uma linha de outra.
*)
 var
  F1, F2: TextFile;
  Linha: String;
  Ch1, Ch2 : Char;
begin
 Ch1:=#0;
 Ch2:=#0;
 AssignFile(F1, ArqOrigem );  // abrir origem
 Reset(F1);                   // para leitura
 AssignFile(F2, ArqDestino);  // abrir destino
 Rewrite(F2);                 // para escrita
 while not Eof(F1) do         // até o final do arquivo
 begin
   Read(F1, Ch2);      // ler no origem
   if (Ch2=#10) and (Ch1<>#13) then  Write(F2, #13);     // gravar no destino
   Write(F2, Ch2);     // gravar no destino
   Ch1:=Ch2;
 end;
 CloseFile(F2);
 CloseFile(F1);
end;

function PoeZERO(Campo :String;Tamanho :Integer):String;   // Prencher Com Zero a esquerda...
Var
 Dif, i : Integer;
 comp : String;
begin
Comp :='';
Dif  := 0;
I    := 0;
If Length(Campo) < Tamanho Then
 begin
  Dif := Tamanho - Length(Campo);
   For i := 1 to Dif do
       Comp := Comp + '0';
 end;
 Result := Comp + Campo;

End;
{**************************************************************************
 Descrição	: esta função é uma ampliação da funcão strToFloat do Delphi,
   uma vez que retorna o valor numérico de uma string que representa um
   número de ponto flutuante. A diferença é que ela aceita strings com
   pontos (###.###,##, p.e.). No caso de erro na conversão, o valor
   retornado é 0 (zero).
 Parâmetros	: S - string com o número de ponto flutuante [ ENTRADA ];
 Retorno	: Valor da string contento um número de ponto flutuante.
**************************************************************************}
function ConverteFloat(const S: string): Extended;
var temp: string;
    i: smallint;
begin
	temp := '';
   for i := 1 to length(S) do
      if S[i] <> '.' then temp := temp + S[i];
	try
   	result := strToFloat(temp);
   except
   	on EConvertError do result := 0;
   end;
end;

function NumeroCEIValido( var sCEI:String ) : Boolean;
(*
  Função para a validação do dígito verificador do CEI
  Retorna: True - DV válido
           False - DV inválido
*)
Const Multiplicadores = '74185216374';
Var iSoma, iUltimaSoma, a : Word;
    sSoma : String[4];
    sUltimaSoma : String[2];
    sDigito : String[1];
begin
 iSoma:=0;
 Try
  For a:=1 to 11 do
   begin                              
      iSoma := iSoma + StrToInt(sCEI[a]) * StrToInt(Multiplicadores[a]);
   end;
  sSoma := IntToStr(iSoma);
  sSoma := StringOfChar('0',(4-Length(sSoma))) + sSoma;
  iUltimaSoma := StrToInt(sSoma[3]) + StrToInt(sSoma[4]);
  sUltimaSoma := IntToStr( iUltimaSoma );
  sUltimaSoma := StringOfChar('0',(2-Length(sUltimaSoma))) + sUltimaSoma;
  if sUltimaSoma[2] = '0' then
     sDigito := '0'
  else
     sDigito:= IntToStr( 10 - StrToInt(sUltimaSoma[2]) );
  if (sDigito[1] <> sCEI[12]) then
     Result := False
  else
     Result := True;
  sCEI[12]:=sDigito[1];

 except
  Result := False;
 end;
end;

function fDVNuInternoValido(Numero : String) : Boolean;
(*
  Valida o número sequencial do FNDE
  não corrige o dígito
*)
var Soma, Resto, DV : Integer;
begin
Try
 Soma:=
       (StrToInt(Numero[ 6]) * 9) +
       (StrToInt(Numero[ 7]) * 8) +
       (StrToInt(Numero[ 8]) * 7) +
       (StrToInt(Numero[ 9]) * 6) +
       (StrToInt(Numero[10]) * 5) +
       (StrToInt(Numero[11]) * 4) +
       (StrToInt(Numero[12]) * 3) +
       (StrToInt(Numero[13]) * 2) ;

  Resto:=Soma Mod 11;
  if ((Resto=0) or (Resto=1)) then
     Dv:=0
  else
     Dv:=11-Resto;

  If (StrToInt(Numero[14]) <> Dv) then
     Result:=False
  else
     Result:=True;

  Numero:=Copy(Numero,1,13)+IntToStr(DV);
except
  Result:=False;
end;
end;

{ Retorna o diretório do windows system32 - Gelson }
function GetSystemDir: TFileName;
var
  SysDir: array [0..MAX_PATH-1] of char;
begin
  SetString(Result, SysDir, GetSystemDirectory(SysDir, MAX_PATH));
  if Result = '' then
    raise Exception.Create(SysErrorMessage(GetLastError));
end;

{ Retorna o diretório do windows - Gelson }
function GetWindowsDir: TFileName;
var
  WinDir: array [0..MAX_PATH-1] of char;
begin
  SetString(Result, WinDir, GetWindowsDirectory(WinDir, MAX_PATH));
  if Result = '' then
    raise Exception.Create(SysErrorMessage(GetLastError));
end;

function MontaStringConexaoDB(dadosConexao : TStringList;comCripto : Boolean):String;
begin

//
end;

end.

