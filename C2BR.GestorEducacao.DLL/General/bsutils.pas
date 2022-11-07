{************************************************************}
{  bsutils.pas                                               }
{  (c)1998 Business Software http://members.xoom.com/bsoft/  }
{ Collection of String / System / Math utility classes       }
{         for Borland Delphi(r) Programmers.                 }
{                                                            }
{ Author: Eddie Bond ebinfo@compuserve.com                   }
{************************************************************}

// FileName:            bsutils.zip
// Program:		BSUTILS collection of Pascal utility classes
// Ver:			1.0
// Date:		22 October 1998
// Copyright:		(c)1998 Business Software
// Web:			http://members.xoom.com/bsoft/
// Author:		Eddie Bond
// E-Mail:		ebinfo@compuserve.com
// Status:		FreeWare
// Restrictions:	None.
// Delphi:		32-bit versions
// Platform:		Windows 32-bit versions.
// Distribution:	Freely distribute ENTIRE package.
//
// NB.
// This source code is distributed by Business Software as FREEWARE
// with the author's permission.
// IT IS NOT PUBLIC DOMAIN!
// You may use these utilities in your applications, whether private
// or commercial, without payment or royalties.
// You may distribute this file in unadulterated and unmodified form,
// or include this file together, or as part of your own distributed
// project's source code provided that this header, and all comments
// remain attached and readable.
// you may 'cut and paste' program segments from this file, to incorporate
// into your own projects, but if you publish the source code you should
// show the following comment below the program segment:

  {from bsutils.pas
  (c)1998 Business Software http://members.xoom.com/bsoft/}

// NB This code is provided without warranty or support of any kind. You use
// this code entirely at your own risk.

{*************************************************************}
{                                                             }
{ CHECK OUT OUR SITE http://members.xoom.com/bsoft/ for more  }
{ DELPHI FREEWARE - SHAREWARE - ADVICE - DOWNLOADS            }
{                                                             }
{*************************************************************}

{===============================================================}

unit bsutils;
{$B-,H+}

interface


uses sysutils,windows,registry,DB,dbtables,BDE;

{================= String Utils =================}
function retornaConexao(value:string):string;


{================= String Utils =================}

function slash(value:string):string;
{ensures that value has '\' as last character (for directory strings)}

function capfirst(value:string):string;
{Capitalise first character of each word, lowercase remaining chars}
{example: capfirst('bOrLANd delPHi FOR windOWs') = 'Borland Delphi For Windows'}

function striptags(value:string):string;
{strip HTML tags from value}
{example: striptags('<TR><TD Align="center">Hello World</TD>') = 'Hello World'}

function replace(str,s1,s2:string;casesensitive:boolean):string;
{replace all incidences of s1 in str with s2}
{example: replace('We know what we want','we','I',false) = 'I Know what I want'}

function CopyFromChar(s:string;c:char;l:integer):string;
{copy l characters from string s starting at first incidence of c}
{example: Copyfromchar('Borland Delphi','a',3) = 'and'}


{================= System Utils =================}

function getwinsysdir:string;
{returns Windows System Path (inc drive)}
{example: getwinsysdir = 'C:\WINDOWS\SYSTEM\'}

function getwindir:string;
{returns windows directory path (inc Drive)}
{example: getwindir = 'C:\WINDOWS\'}

function getinstalldir:string;
{returns install directory of EXE using this library}
{example: getinstalldir = 'C:\PROGRAM FILES\BORLAND\DELPHI\DEMOS\'}

function getcurrentdir:string;
{returns current directory}
{example: getcurrentdir = 'D:\DELPHI PROJECTS\CLASSES\UTILS\'}

function getregvalue(root:integer;key,value:string):string;
{reads a registry value}
{example: getregvalue(HKEY_LOCAL_MACHINE,'network\logon\','username') = 'Eddie Bond'}

function getaliaspath(dbset:Tdataset):string;
{returns DOS path of an ACTIVE dataset's (TTable or TQuery) database alias}
{example getaliaspath(Table1) = 'C:\Program Files\Borland\Delphi\Demos\Data\'}

function getfiledate(filename:string):Tdatetime;
{returns a file's date in TDateTime format}


{================= Arithmetic Utils =================}

function StrToFloatDef(const s:string;def:Extended):Extended;
{converts S into a number. If S is invalid, returns the number passed in Def.}
{example: strtofloatdef('$10.25',0) = 0}

function VolSphere(radius:single):extended;
{volume of sphere of given radius}

function AreaSphere(radius:single):extended;
{surface area of sphere of given radius}

function VolCylinder(radius,height:single):extended;
{volume of cylinder of given radius and height}

function AreaCylinder(radius,height:single):extended;
{surface area of cylinder of given radius and height}

function MinExt(const A:array of Extended):Extended;
{returns minimum value of an array of extended}

function MaxExt(const A:array of Extended):Extended;
{returns maximum value of an array of extended}

function MinInteger(const A:array of Integer):Integer;
{returns minimum value of an array of integers}

function MaxInteger(const A:array of integer):Integer;
{returns maximum value of an array of integers}

function InverseSum(const a:array of single):single;
{solves formulae of type 1/r = 1/a + 1/b +...1/n (eg electrical resistance in parallel)}

{================= Financial Utils =================}

function MarkUp(profit:single):single;
{returns markup percentage required to return a profit of profit percent}
{example: MarkUp(25) = 20 }

function SellingPrice(net:double;markup:single):double;
{returns selling price after adding markup percent to net}
{example: SellingPrice(199.50,22.5) = 244.3875}

function NetPrice(gross:double;taxrate:single):double;
{returns the net value of an item of gross value containing tax at taxrate percent}
{example: NetPrice(199.99,17.5) = 170.204255319149}

function StringDecode(const S: string): string;

function StringUpper(s:string): string;

implementation


function retornaConexao(value:string):string;
begin             
      //Barão do Rio Branco
      if value = '9014296000174' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEBARRIOBRA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Educandario de Maria
      else if value = '4120476000117' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEEDUCAMARIA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola ABC
      else if value = '7002950000102' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEESCABC;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola Reino Encantado
      else if value = '122135000120' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEESCREINOENCANT;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //ETB
      else if value = '3960623000102' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEETB;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Objetivo Esplanada
      else if value = '4776952000152' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEOBJESPLANADA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Colegio Especifico
      else if value = '10689657000161' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGECOESPECIFICO;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Colégio Esplanada
      else if value = '4223948000167' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGECOESPLANADA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Isaac Newton
      else if value = '32908634000133' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEISACNEWTON;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola Modelo
      else if value = '11558593000122' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEMODELO;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Escola Reação
      else if value = '54108130000173' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGECOREACAO;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      //Conexão Aquarela
      else if value = '11489849000133' then
      begin
        result := 'Provider=SQLOLEDB.1;Password=conexao@aquarela;Persist Security Info=True;User ID=conexao@aquarela;'+
  'Initial Catalog=BDPGECONAQU;Data Source=(local);Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end
      else
      begin
        result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEMODELO;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';
      end;
                       {
      result := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=10.0.88.2\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False'; }
end;

function StringDecode(const S: string): string;
var
  Idx: Integer;
  retorno : string;
begin
  retorno := '';
  for Idx := 1 to Length(S) do
  begin
    if S[Idx] in [ '(',')','=', ':', '?', ';', '_', '.',',','[',']', '''','"','’','‘','·','/','\','¨' ] then
       retorno := retorno + ' '
    else
      retorno := retorno + S[Idx];
  end;
  Result:=retorno;
end;

function slash(value:string):string;
begin
if (value[length(value)]<>'\') then result:=value+'\' else result:=value;
end;

function capfirst(value:string):string;
var
i:integer;
s:string;
begin
s:=uppercase(value[1]);
for i:=2 to length(value) do
    if (ord(value[i-1])<33) then s:=s+uppercase(value[i]) else s:=s+lowercase(value[i]);
result:=s;
end;

function striptags(value:string):string;
var
i:integer;
s:string;
begin
i:=1;
s:='';
while i<=length(value) do
      begin
      if value[i]='<' then repeat inc(i) until (value[i]='>') else s:=s+value[i];
      inc(i);
      end;
result:=s;
end;

function replace(str,s1,s2:string;casesensitive:boolean):string;
var
i:integer;
s,t:string;
begin
s:='';
t:=str;
       repeat
       if casesensitive then i:=pos(s1,t) else i:=pos(lowercase(s1),lowercase(t));
       if i>0 then
          begin
          s:=s+Copy(t,1,i-1)+s2;
          t:=Copy(t,i+Length(s1),MaxInt);
          end
       else s:=s+t;
       until i<=0;
result:=s;
end;

function CopyFromChar(s:string;c:char;l:integer):string;
var i:integer;
begin
i:=pos(c,s);
result:=copy(s,i,l);
end;

function getwinsysdir:string;
var p:pchar;
    z:integer;
begin
z:=255;
getmem(p,z);
getsystemdirectory(p,z);
result:=slash(string(p));
freemem(p,z);
end;

function getwindir:string;
var p:pchar;
    z:integer;
begin
z:=255;
getmem(p,z);
getwindowsdirectory(p,z);
result:=slash(string(p));
freemem(p,z);
end;

function getcurrentdir:string;
var p:pchar;
    z:integer;
begin
z:=255;
getmem(p,z);
getcurrentdirectory(z,p);
result:=slash(string(p));
freemem(p,z);
end;


function getinstalldir:string;
begin
result:=slash(extractfiledir(paramstr(0)));
end;


function getregvalue(root:integer;key,value:string):string;
var
rg:Tregistry;
begin
rg:=Tregistry.create;
  try
    rg.rootkey:=root;
    if rg.OpenKey(key,false) then result:=rg.readString(value) else result:='';
  finally
    FreeAndNil(rg);
  end;
end;

function getaliaspath(dbset:Tdataset):string;
var
vDBDesc:DBDesc;
s:string;
begin
result:='';
if not (dbset.active) then exit;
if (dbset is TTable) then s:=(dbset as ttable).databasename;
if (dbset is TQuery) then s:=(dbset as tquery).databasename;
Check(DbiGetDatabaseDesc(PChar(s),@vDBDesc));
result:=slash(string(vDBDesc.szPhyName));
end;

function getfiledate(filename:string):Tdatetime;
begin
if fileexists(filename) then
   result:=filedatetodatetime(fileage(filename)) else result:=maxint;
end;


function strtofloatdef(const s:string;def:Extended):Extended;
begin
     try
     result:=strtofloat(s);
     except
     result:=def;
     end;
end;

function volsphere(radius:single):extended;
begin
result:=((4/3)*pi*radius*radius*radius);
end;

function areasphere(radius:single):extended;
begin
result:=(4*pi*radius*radius);
end;

function volcylinder(radius,height:single):extended;
begin
result:=(pi*radius*radius*height);
end;

function areacylinder(radius,height:single):extended;
begin
result:=(2*pi*radius*height);
end;

function MinExt(const A:array of Extended):Extended;
var
i:integer;
begin
Result:=A[Low(A)];
for i:=Low(A)+1 to High(A) do if A[i]<Result then Result:=A[I];
end;

function MaxExt(const A:array of Extended):Extended;
var
i:integer;
begin
Result:=A[Low(A)];
for i:=Low(A)+1 to High(A) do if A[i]>Result then Result:=A[I];
end;

function MinInteger(const A:array of Integer):Integer;
var
i:integer;
begin
Result:=A[Low(A)];
for i:=Low(A)+1 to High(A) do if A[i]<Result then Result:=A[I];
end;

function MaxInteger(const A:array of integer):Integer;
var
i:integer;
begin
Result:=A[Low(A)];
for i:=Low(A)+1 to High(A) do if A[i]>Result then Result:=A[I];
end;

function InverseSum(const a:array of single):single;
var i:integer;
begin
result:=0;
for i:=low(a) to high(a) do result:=result+(1/a[i]);
result:=(1/result);
end;

function MarkUp(profit:single):single;
begin
result:=(100-(10000/(100+profit)));
end;

function SellingPrice(net:double;markup:single):double;
begin
result:=net+(net*markup/100);
end;

function NetPrice(gross:double;taxrate:single):double;
begin
result:=gross-(gross*(taxrate)/(100+taxrate));
end;

function StringUpper(s:string): string;
var
  n : integer;
begin
   s:=replace(s,'ç','Ç',false);
   s := replace(s,'à','À',false);
   s := replace(s,'á','Á',false);
   s := replace(s,'â','Â',false);
   s := replace(s,'ã','Ã',false);
   s := replace(s,'ä','Ä',false);
   s := replace(s,'å','Å',false);

   s := replace(s,'è','È',false);
   s := replace(s,'é','É',false);
   s := replace(s,'ê','Ê',false);
   s := replace(s,'ë','Ë',false);


   s := replace(s,'ò','Ò',false);
   s := replace(s,'ó','Ó',false);
   s := replace(s,'ô','Ô',false);
   s := replace(s,'õ','Õ',false);
   s := replace(s,'ö','Ö',false);

   s := replace(s,'ù','Ù',false);
   s := replace(s,'ú','Ú',false);
   s := replace(s,'û','Û',false);
   s := replace(s,'ü','Ü',false);
   s := replace(s,'--',' ',false);
   s := replace(s,'í','Í',false);
   s := replace(s,'ì','Ì',false);
   s := replace(s,'î','Î',false);
   s := replace(s,'ï','Ï',false);
   s := replace(s,'¨',' ',false);
   s := replace(s,'`',' ',false);
   s := replace(s,'|',' ',false);
   s := replace(s,'~',' ',false);
   s := replace(s,'£',' ',false);
   s := replace(s,'§',' ',false);
   s := replace(s,'º',' ',false);
   s := replace(s,'»',' ',false);
   s := replace(s,'*',' ',false);
   s := replace(s,'´',' ',false);
   s := replace(s,chr(39),' ',false);
   s := replace(s,'©',' ',false);
   s := replace(s,'ª',' ',false);
   s := replace(s,'°',' ',false);
   s := replace(s,'"',' ',false);
   s := replace(s,'(',' ',false);
   s := replace(s,')',' ',false);
   s := replace(s,'=',' ',false);
   s := replace(s,'(',' ',false);
   s := replace(s,'?',' ',false);
   s := replace(s,':',' ',false);
   s := replace(s,';',' ',false);
   s := replace(s,'>','> ',false);
   s := replace(s,'<',' <',false);
   s := replace(s,'&nbsp;',' ',false);
   for n:=1 to 33 do begin
     s := replace(s,chr(n),' ',false);
   end;











s:=UpperCase(s);
Result := s;
end;

end.

