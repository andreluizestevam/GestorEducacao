{
 
 Controlador da Conexao e tambem concetra todas as funcoes de tratamento de banco de dados

 @since	29/05/2008
 @author	Nataniel Fiuza <natan.fiuza@gmail.com>
 @version	1.0
 @package	database
 @copyright	UMTI - Unidade Minicipal de Tecnologia da Informacao

}
unit UTPersistente;

interface

uses
  ADODB,Sysutils,bsutils,dialogs;
type
  TPersistenteProtocol = ( tppMysql, tppMsSql );

type TPersistente = class(TObject)
     connection : TADOConnection;
     conectado  : boolean;
     protocolo  : TPersistenteProtocol;
   private
   public
    function openSql(query : string): TADOQuery;
    function getQuery(query : string): TADOQuery;
    function execSql(query : string): boolean;
    function FormatStringMysql(Valor : string): string;
    function FormatCurrencyMysql(Valor : string): string;
    function FormatStringMysqlLike(Valor,campo : string): string;
    function FormatDataMySQL(Valor : TDateTime): String;
    function FormatDataHoraMySQL(Valor: TDateTime): String;
    function FormatStringLike(Valor,campo : string): string; // Usa () no final e inicio da condicao like 
    function chave:string;
    function verificaConexao:Boolean;
    constructor CriarInstancia(conexao : TADOConnection);
    destructor RemoveInstancia;
end;

implementation

function TPersistente.FormatDataMySQL(Valor : TDateTime): String;
begin
    result:= FormatDateTime('yyyy-mm-dd',Valor);
end;

function TPersistente.FormatDataHoraMySQL(Valor : TDateTime): String;
begin
    result:= FormatDateTime('yyyy-mm-dd hh:nn:ss',Valor);
end;

function TPersistente.FormatStringMysql(Valor : string): string;
begin
    result:=chr(39)+replace(valor,chr(39),'\'+chr(39),false)+chr(39);
end;

function TPersistente.FormatCurrencyMysql(Valor : string): string;
var
  texto : string;
begin
  try
    texto := chr(39) + formatcurr('#######################################################0.##',strtocurr(valor))+chr(39);
    result := replace(texto,',','.', True);
  except
    result := chr(39) + replace(valor,chr(39),'\'+chr(39),false) + chr(39);
  end;
end;

function TPersistente.FormatStringMysqlLike(Valor,campo : string): string;
begin
// Correcao criada em 29/06/2006
// Cria o like ja com o campo e com varias opcoes
//
  result := campo+' LIKE '+chr(39)+''+replace(valor,chr(39),'\'+chr(39),false)+'%'+chr(39);
  result := result+' OR '+campo+' LIKE '+chr(39)+'%'+replace(valor,chr(39),'\'+chr(39),false)+''+chr(39);
  result := result+' OR '+campo+' LIKE '+chr(39)+'%'+replace(valor,chr(39),'\'+chr(39),false)+'%'+chr(39);
end;

function TPersistente.FormatStringLike(Valor,campo : string): string;
begin
// Correcao criada em 29/06/2006
// Cria o like ja com o campo e com varias opcoes
//
  result := ' ( '+campo+' LIKE '+chr(39)+''+replace(valor,chr(39),'\'+chr(39),false)+'%'+chr(39);
  result := result+' OR '+campo+' LIKE '+chr(39)+'%'+replace(valor,chr(39),'\'+chr(39),false)+''+chr(39);
  result := result+' OR '+campo+' LIKE '+chr(39)+'%'+replace(valor,chr(39),'\'+chr(39),false)+'%'+chr(39)+' )';
end;


function TPersistente.openSql(query : string):TADOQuery;
var
  Tabela : TADOQuery;
begin
  Tabela:=TADOQuery.create(nil);
  Tabela.Connection:=connection;
  Tabela.Close;
  Tabela.sql.clear;
  Tabela.sql.add(query);
  try
      Tabela.open;
  except
      Result:=nil;
      Tabela.close;
      Tabela.Free;
      exit;
  end;
  Result:=Tabela;
end;

function TPersistente.getQuery(query : string):TADOQuery;
var
  tabela : TADOQuery;
begin
  Tabela:=TADOQuery.create(nil);
  Tabela.Connection:=connection;
  Tabela.sql.clear;
  Tabela.sql.add(query);
  result := tabela;
end;

function TPersistente.verificaConexao:Boolean;
begin
  result := self.conectado;
end;

function TPersistente.execSql(query : string): boolean;
var
  Tabela : TADOQuery;
begin
  Tabela:=TADOQuery.create(nil);
  Tabela.Connection:=connection;
  Tabela.Close;
  Tabela.sql.clear;
  Tabela.sql.add(query);
  try
      Tabela.ExecSql;
  except
      Tabela.close;
      Tabela.Free;
      Result:=false;
      exit;
  end;
  Tabela.close;
  Tabela.Free;
  Result:=true;
end;

function TPersistente.chave:string;
var
  tempo:  ttimestamp;
begin
  Tempo:=DateTimeToTimeStamp(date+time);
  Randomize;
  Result:=IntTostr(Random(120))+IntToStr(tempo.date)+IntToStr(tempo.time)
end;

constructor TPersistente.CriarInstancia(conexao : TADOConnection);
begin
  inherited create;
  // Excluido o codigo usado no zeosdb - Nataniel Fiuza
    self.connection := conexao;

end;

destructor TPersistente.RemoveInstancia;
begin

end;

end.

