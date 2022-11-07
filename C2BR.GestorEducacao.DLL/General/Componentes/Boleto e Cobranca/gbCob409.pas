unit gbCob409;

interface

uses
   classes, SysUtils, gbCobranca
   {$IFDEF VER140}
      , MaskUtils, contnrs
   {$ELSE}
      {$IFDEF VER130}
         , Mask, contnrs
      {$ELSE}
         , Mask
      {$ENDIF}
   {$ENDIF}
   ;

const
   CodigoBanco = '409';
   NomeBanco = 'UNIBANCO';

type

   TgbBanco409 = class(TPersistent)
   published
      function  GetNomeBanco   : string; {Retorna o nome do banco}
      function  GetCampoLivreCodigoBarra(ATitulo: TgbTitulo) : string; {Retorna o conteúdo da parte variável do código de barras}
      function  CalcularDigitoNossoNumero(ATitulo: TgbTitulo) : string; {Calcula o dígito do NossoNumero, conforme critérios definidos por cada banco}
      procedure FormatarBoleto(ATitulo: TgbTitulo; var AAgenciaCodigoCedente, ANossoNumero, ACarteira, AEspecieDocumento: string); {Define o formato como alguns valores serão apresentados no boleto }
{$IFNDEF VER120}
      function  LerRetorno(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean; {Lê o arquivo retorno recebido do banco}
      function  GerarRemessa(var ACobranca: TgbCobranca; var Remessa: TStringList) : boolean; {Gerar arquivo remessa para enviar ao banco}
{$ENDIF}
   end;


implementation


function TgbBanco409.GetNomeBanco : string;
begin
   Result := NomeBanco;
end;

function TgbBanco409.CalcularDigitoNossoNumero(ATitulo: TgbTitulo) : string;
var
   ANossoNumero,
   ADigitoNossoNumero : string;
begin
   Result := '0';

   ANossoNumero := Formatar(ATitulo.NossoNumero, 11, false, '0');
   ADigitoNossoNumero := Modulo11(ANossoNumero, 9);

   Result  := ADigitoNossoNumero;
end;

function TgbBanco409.GetCampoLivreCodigoBarra(ATitulo: TgbTitulo) : string;
var
   AAgencia,
   ADigitoAgencia,
   ANossoNumero,
   ADigitoNossoNumero,
   AVencimento,
   ASuperDigito,
   ACodigoCedente: string;
begin

   {
    A primeira parte do código de barras será calculada automaticamente.
    Ela é composta por:
    Código do banco (3 posições)
    Código da moeda = 9 (1 posição)
    Dígito do código de barras (1 posição) - Será calculado e incluído pelo componente
    Fator de vencimento (4 posições) - Obrigatório a partir de 03/07/2000
    Valor do documento (10 posições) - Sem vírgula decimal e com ZEROS à esquerda

    A segunda parte do código de barras é um campo livre, que varia de acordo
    com o banco. Esse campo livre será calculado por esta função (que você deverá
    alterar de acordo com as informações fornecidas pelo banco).
   }

   {Segunda parte do código de barras - Campo livre - Varia de acordo com o banco}

   with ATitulo do begin
      ACodigoCedente := Formatar(Cedente.CodigoCedente + Cedente.DigitoCodigoCedente, 7, False, '0');

      AVencimento    := FormatDateTime('yymmdd', DataVencimento);

      AAgencia       := Formatar(Cedente.ContaBancaria.CodigoAgencia, 4, False, '0');
      ADigitoAgencia := Cedente.ContaBancaria.DigitoAgencia;

      ANossoNumero   := Formatar(NossoNumero, 10, False, '0');
      ADigitoNossoNumero   := DigitoNossoNumero;

      ASuperDigito   := Modulo11('1' + Formatar(NossoNumero, 10, False, '0') + ADigitoNossoNumero, 9);
   end;

   Result  := '04' + AVencimento + AAgencia + ADigitoAgencia + ANossoNumero + ADigitoNossoNumero + ASuperDigito;
end;

procedure TgbBanco409.FormatarBoleto(ATitulo: TgbTitulo; var AAgenciaCodigoCedente, ANossoNumero, ACarteira, AEspecieDocumento: string);
var ASuperDigito   : String;
begin
   with ATitulo do begin
      ASuperDigito   := Modulo11('1' + Formatar(NossoNumero,10,false,'0') + DigitoNossoNumero, 9);

      AAgenciaCodigoCedente := Cedente.ContaBancaria.CodigoAgencia + '-' + Cedente.ContaBancaria.DigitoAgencia + '/' + Cedente.ContaBancaria.NumeroConta + '-' + Cedente.ContaBancaria.DigitoConta;
      ANossoNumero := '1/' + Formatar(NossoNumero,10,false,'0') + '-' + DigitoNossoNumero + '/' + ASuperDigito;
      ACarteira := Formatar(Carteira,3);
      AEspecieDocumento := '';

//      Cedente.ContaBancaria.Banco.UsoBanco   := 'CVT5539-5';
   end;
end;

{$IFNDEF VER120}

function TgbBanco409.GerarRemessa(var ACobranca: TgbCobranca; var Remessa: TStringList) : boolean;
begin
   Result := FALSE;
   Raise Exception.CreateFmt('Geração de arquivo remessa não está disponível para o banco %s - %s',[CodigoBanco, NomeBanco]);
end;

function TgbBanco409.LerRetorno(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean;
begin
   Result := FALSE;
   Raise Exception.CreateFmt('Processamento de arquivo retorno não está disponível para o banco %s - %s',[CodigoBanco, NomeBanco]);
end;

{$ENDIF}

initialization
RegisterClass(TgbBanco409);

end.
