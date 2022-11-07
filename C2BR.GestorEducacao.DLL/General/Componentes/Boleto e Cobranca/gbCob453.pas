unit gbCob453;

interface

uses
   Classes, SysUtils, gbCobranca
   {$IFDEF VER150}
      , Variants, MaskUtils, contnrs, DateUtils
   {$ELSE}
      {$IFDEF VER140}
         , Variants, MaskUtils, contnrs, DateUtils
      {$ELSE}
         {$IFDEF VER130}
            , Mask, contnrs
         {$ELSE}
            , Mask
         {$ENDIF}
      {$ENDIF}
   {$ENDIF}
   ;

const
   CodigoBanco = '453';
   NomeBanco = 'Banco RURAL S.A';

type

   TgbBanco453 = class(TPersistent)
{$IFNDEF VER120}
      function VerificaOcorrenciaOriginal(sOcorrenciaOriginal: String): String;
      function VerificaMotivoRejeicaoComando(sMotivoRejeicaoComando: String): String;
//      function GerarRemessaCNAB240(var ACobranca: TgbCobranca; var Remessa: TStringList) : boolean;
      function GerarRemessaCNAB400(var ACobranca: TgbCobranca; var Remessa: TStringList) : boolean;
//      function LerRetornoCNAB240(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean;
      function LerRetornoCNAB400(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean;
{$ENDIF}
   published
      function  GetNomeBanco   : string; {Retorna o nome do banco}
      function  GetCampoLivreCodigoBarra(ATitulo: TgbTitulo) : string; {Retorna o conte�do da parte vari�vel do c�digo de barras}
      function  CalcularDigitoNossoNumero(ATitulo: TgbTitulo) : string; {Calcula o d�gito do NossoNumero, conforme crit�rios definidos por cada banco}
      procedure FormatarBoleto(ATitulo: TgbTitulo; var AAgenciaCodigoCedente, ANossoNumero, ACarteira, AEspecieDocumento: string); {Define o formato como alguns valores ser�o apresentados no boleto }
{$IFNDEF VER120}
      function  LerRetorno(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean; {L� o arquivo retorno recebido do banco}
      function  GerarRemessa(var ACobranca: TgbCobranca; var Remessa: TStringList) : boolean; {Gerar arquivo remessa para enviar ao banco}
{$ENDIF}
   end;


implementation


function TgbBanco453.GetNomeBanco : string;
begin
   Result := NomeBanco;
end;

function TgbBanco453.CalcularDigitoNossoNumero(ATitulo: TgbTitulo) : string;
var
   ACodigoAgencia,
   ANumeroConta,
   ADigitoConta,
   ANossoNumero,
   AComposicao,
   APesos: string;
   ASoma,
   AResto,
   AContador: integer;
begin
   Result := '0';

   ACodigoAgencia := Formatar(ATitulo.Cedente.ContaBancaria.CodigoAgencia,4,false,'0');
   ANumeroConta   := Formatar(ATitulo.Cedente.ContaBancaria.NumeroConta,9,false,'0');
   ADigitoConta   := Formatar(ATitulo.Cedente.ContaBancaria.DigitoConta,1,false,'0');
   ANossoNumero   := Formatar(ATitulo.NossoNumero,9,false,'0');
   AComposicao    := ACodigoAgencia + ANumeroConta + ADigitoConta + ANossoNumero;
   {
    Multiplicar os algarismos da composi��o, iniciando da direita para a esquerda
    pelos pesos: 3, 7, 9, 1, com exce��o do campo "D�gito da Conta", que deve ser
    multiplicado sempre por 1
   }
   APesos := '31973197319731319731973';

   ASoma := 0;
   for AContador := 1 to Length(AComposicao) do
      ASoma := ASoma + ( StrToInt(AComposicao[AContador]) * StrToInt(APesos[AContador]) );

   AResto := (ASoma mod 10);
   if AResto = 0 then
      Result := '0'
   else
      Result := IntToStr(10 - AResto);
end;

function TgbBanco453.GetCampoLivreCodigoBarra(ATitulo: TgbTitulo) : string;
var
   AAnoAtual,
   ACarteira,
   ANossoNumero,
   ANossoNumeroDg,
   ACodigoAgencia,
   ANumeroConta, ANumeroContaDg : string;
begin

   {
    A primeira parte do c�digo de barras ser� calculada automaticamente.
    Ela � composta por:
    C�digo do banco (3 posi��es)
    C�digo da moeda = 9 (1 posi��o)
    D�gito do c�digo de barras (1 posi��o) - Ser� calculado e inclu�do pelo componente
    Fator de vencimento (4 posi��es) - Obrigat�rio a partir de 03/07/2000
    Valor do documento (10 posi��es) - Sem v�rgula decimal e com ZEROS � esquerda

    A segunda parte do c�digo de barras � um campo livre, que varia de acordo
    com o banco. Esse campo livre ser� calculado por esta fun��o (que voc� dever�
    alterar de acordo com as informa��es fornecidas pelo banco).
   }

   {Segunda parte do c�digo de barras - Campo livre - Varia de acordo com o banco}

   with ATitulo do
   begin
      ACarteira      := Formatar(Carteira,2,false,'0');
      AAnoAtual      := FormatDateTime('yy',Now());
      ANossoNumero   := Formatar(NossoNumero,7,false,'0');
      ANossoNumeroDg := CalcularDigitoNossoNumero(ATitulo);
      ACodigoAgencia := Formatar(Cedente.ContaBancaria.CodigoAgencia,4,false,'0');
      ANumeroConta   := Formatar(Cedente.ContaBancaria.NumeroConta,9,false,'0');
      ANumeroContaDg := Formatar(Cedente.ContaBancaria.DigitoConta,1,false,'0');
   end;

//   Result := ACodigoAgencia + ACarteira + AAnoAtual + ANossoNumero + ANumeroConta + '0';
   Result := ACodigoAgencia + ANumeroConta+ANumeroContaDg+ANossoNumero+ANossoNumeroDg+'000';
end;

procedure TgbBanco453.FormatarBoleto(ATitulo: TgbTitulo; var AAgenciaCodigoCedente, ANossoNumero, ACarteira, AEspecieDocumento: string);
begin
   with ATitulo do
   begin
      AAgenciaCodigoCedente := Formatar(Cedente.ContaBancaria.CodigoAgencia,4,false,'0') + '.' + Formatar(Cedente.CodigoCedente,7,false,'0') + '.' + Cedente.DigitoCodigoCedente;
      ANossoNumero := Formatar(NossoNumero,7,false,'0') + '.' + DigitoNossoNumero;
      ACarteira := Formatar(Carteira,2);
      case EspecieDocumento of
         edApoliceSeguro                : AEspecieDocumento := 'AP';
         edCheque                       : AEspecieDocumento := 'CH';
         edDuplicataMercantil           : AEspecieDocumento := 'DM';
         edDuplicataMercantialIndicacao : AEspecieDocumento := 'DMI';
         edDuplicataRural               : AEspecieDocumento := 'DR';
         edDuplicataServico             : AEspecieDocumento := 'DS';
         edDuplicataServicoIndicacao    : AEspecieDocumento := 'DSI';
         edFatura                       : AEspecieDocumento := 'FAT';
         edLetraCambio                  : AEspecieDocumento := 'LC';
         edMensalidadeEscolar           : AEspecieDocumento := 'ME';
         edNotaCreditoComercial         : AEspecieDocumento := 'NCC';
         edNotaCreditoExportacao        : AEspecieDocumento := 'NCE';
         edNotaCreditoIndustrial        : AEspecieDocumento := 'NCI';
         edNotaCreditoRural             : AEspecieDocumento := 'NCR';
         edNotaDebito                   : AEspecieDocumento := 'ND';
         edNotaPromissoria              : AEspecieDocumento := 'NP';
         edNotaPromissoriaRural         : AEspecieDocumento := 'NPR';
         edNotaSeguro                   : AEspecieDocumento := 'NS';
         edParcelaConsorcio             : AEspecieDocumento := 'PC';
         edRecibo                       : AEspecieDocumento := 'RC';
         edTriplicataMercantil          : AEspecieDocumento := 'TM';
         edTriplicataServico            : AEspecieDocumento := 'TS'
      else
         AEspecieDocumento := '';
      end;
   end;
end;

{$IFNDEF VER120}

function TgbBanco453.GerarRemessaCNAB400(var ACobranca: TgbCobranca; var Remessa: TStringList) : boolean;
var
   ACedenteTipoInscricao, ASacadoTipoInscricao,
   ATipoOcorrencia,
   AEspecieDocumento,
   Registro : string;
   NumeroRegistro : integer;
begin
   Result := FALSE;
   NumeroRegistro := 0;
   Remessa.Clear;

   with ACobranca do
   begin

      { GERAR REGISTRO-HEADER DA REMESSA }

      Remessa.Add('0'+ // 1 a 1 - IDENTIFICA��O DO REGISTRO HEADER
                  '9'+ // 2 a 2 - TIPO DE OPERA��O - REMESSA
                  'REMESSA'+ // 3 a 9 - IDENTIFICA��O POR EXTENSO DO MOVIMENTO
                  '01'+ // 10 a 11 - IDENTIFICA��O DO TIPO DE SERVI�O
                  Formatar('COBRANCA',15)+ // 12 a 26 - IDENTIFICA��O POR EXTENSO DO TIPO DE SERVI�O
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.CodigoAgencia,4,false,'0')+ // 27 a 30 - AG�NCIA MANTENEDORA DA CONTA
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.NumeroConta,9,false,'0')+ // 31 a 39 - TIPO DE CONTA (2 D�GITOS ) + N�MERO DA CONTA CORRENTE DA EMPRESA (7 D�GITOS)
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.DigitoConta,1)+ // 40 a 40 - D�GITO DE AUTO CONFER�NCIA AG/CONTA EMPRESA
                  Formatar('',6)+ // 41 a 46 - BRANCOS
                  Formatar(Titulos[NumeroRegistro].Cedente.Nome,30,true,' ')+ // 47 a 76 - NOME POR EXTENSO DA "EMPRESA M�E"
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Codigo,3,false,'0')+ // 77 a 79 - N� DO BANCO NA C�MARA DE COMPENSA��O
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Nome,15,true,' ')+ // 80 a 94 - NOME POR EXTENSO DO BANCO COBRADOR
                  Formatar('',15) + // 95 a 109 - BRANCOS
                  'CD0V01' + // 110 a 115 - C�DIGO VERS�O
                  Formatar('',279) + // 116 a 394 - BRANCOS
                  '000001'); // 395 a 400 - N�MERO SEQ�ENCIAL DO REGISTRO NO ARQUIVO

      { GERAR TODOS OS REGISTROS DETALHE DA REMESSA }
      while NumeroRegistro <= (Titulos.Count - 1) do
      begin
         if Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Codigo,3,false,'0') <> Formatar(CodigoBanco,3,false,'0') then
            Raise Exception.CreateFmt('Titulo n�o pertence ao banco %s - %s',[CodigoBanco,NomeBanco]);

         case Titulos[NumeroRegistro].Cedente.TipoInscricao of
            tiPessoaFisica  : ACedenteTipoInscricao := '01';
            tiPessoaJuridica: ACedenteTipoInscricao := '02';
            tiOutro         : ACedenteTipoInscricao := '03';
         end;
         case Titulos[NumeroRegistro].Sacado.TipoInscricao of
            tiPessoaFisica  : ASacadoTipoInscricao := '01';
            tiPessoaJuridica: ASacadoTipoInscricao := '02';
            tiOutro         : ASacadoTipoInscricao := '03';
         end;

         Registro := '1'; // 1 a 1 - IDENTIFICA��O DO REGISTRO TRANSA��O
         Registro := Registro + Formatar('',16); // 2 a 17 - BRANCOS
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.CodigoAgencia,4,false,'0'); // 18 a 21 - AG�NCIA MANTENEDORA DA CONTA
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.NumeroConta,9,false,'0'); // 22 a 30 - TIPO DE CONTA (2 D�GITOS ) + N�MERO DA CONTA CORRENTE DA EMPRESA (7 D�GITOS)
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.DigitoConta,1); // 31 a 31 - D�GITO DE AUTO CONFER�NCIA AG/CONTA EMPRESA
         Registro := Registro + Formatar(IntToStr(DaysBetween(Titulos[NumeroRegistro].DataVencimento, Titulos[NumeroRegistro].DataProtesto)),2,false,'0'); // 32 A 33 - PRAZO PARA PROTESTO
         Registro := Registro + '00'; // 34 a 35 - C�DIGO DA MOEDA (00 = REAL)
         Registro := Registro + '2'; // 36 a 36 - TIPO MORA (2 = VALOR DA MORA SER� EXPRESSOEM REAIS)
         Registro := Registro + ' '; // 37 a 37 - ESPECIFICA SE EXISTE MENSAGEM PARA BLOQUETE (BRANCO - N�O EXISTE MENSAGEM)
         Registro := Registro + Formatar('',25); // 38 a 62 // USO DA EMPRESA
         Registro := Registro + Formatar(Titulos[NumeroRegistro].NossoNumero,7,false,'0'); // 63 a 69 - NOSSO N�MERO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].DigitoNossoNumero,1,false,'0'); // 70 a 70 - D�GITO VERIFICADOR DO NOSSO N�MERO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.CodigoCedente,9,false,'0'); // 71 a 79 - N�MERO DO CONTRATO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.DigitoCodigoCedente,1,false,'0'); // 80 a 80 - D�GITO VERIFICADOR DO N�MERO DO CONTRATO
         Registro := Registro + '  '; // 81 a 82 - BRANCOS
         Registro := Registro + '00'; // 83 a 84 - Identifica��o do tipo de inscri��o do sacador: 01 - CPF // 02 - CNPJ
         Registro := Registro + Formatar('',14,false,'0'); // 85 a 98 - N�mero de inscri��o (CPF ou CNPJ) do sacador
         Registro := Registro + Formatar('',9); // 99 a 107 - Brancos
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Carteira,1,false,'0');  // 108 a 108 - C�digo da carteira
         
         {Tipo de ocorr�ncia}
         case Titulos[NumeroRegistro].TipoOcorrencia of
            toRemessaRegistrar : ATipoOcorrencia := '01';
            toRemessaBaixar : ATipoOcorrencia := '02';
            toRemessaConcederAbatimento : ATipoOcorrencia := '04';
            toRemessaCancelarAbatimento : ATipoOcorrencia := '05';
            toRemessaAlterarVencimento : ATipoOcorrencia := '06';
            toRemessaAlterarNumeroControle: ATipoOcorrencia := '08';
            toRemessaProtestar : ATipoOcorrencia := '09';
            toRemessaCancelarInstrucaoProtesto : ATipoOcorrencia := '18';
            toRemessaAlterarNomeEnderecoSacado: ATipoOcorrencia := '31';
         else
            Raise Exception.CreateFmt('Ocorr�ncia inv�lida em remessa - Nosso n�mero: %s / Seu n�mero: %s',[Titulos[NumeroRegistro].NossoNumero,Titulos[NumeroRegistro].SeuNumero]);
         end;
         Registro := Registro + ATipoOcorrencia; // IDENTIFICA��O DA OCORR�NCIA  // 109 a 110 - Identifica��o do Servi�o

         Registro := Registro + Formatar(Titulos[NumeroRegistro].NumeroDocumento,10); // 111 a 120 - N� DO DOCUMENTO DE COBRAN�A (DUPL.,NP ETC.)
         Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataVencimento); // 121 a 126 - DATA DE VENCIMENTO DO T�TULO
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorDocumento * 100); // 127 a 139 - VALOR NOMINAL DO T�TULO
         Registro := Registro + Formatar('',7,false,'0'); // 140 a 146 - C�DIGO DO BANCO E AG�NCIA COBRADORA
         Registro := Registro + ' '; // 147 a 147 - Brancos

         {Esp�cie do t�tulo}
         if Titulos[NumeroRegistro].EmissaoBoleto = ebClienteEmite then
         begin
            case Titulos[NumeroRegistro].EspecieDocumento of
               edApoliceSeguro : AEspecieDocumento := '04';
               edDuplicataMercantil : AEspecieDocumento := '01';
               edDuplicataMercantialIndicacao : AEspecieDocumento := '01';
               edDuplicataRural: AEspecieDocumento := '01';
               edDuplicataServico : AEspecieDocumento := '02';
               edDuplicataServicoIndicacao : AEspecieDocumento := '02';
               edLetraCambio : AEspecieDocumento := '06';
               edNotaPromissoria : AEspecieDocumento := '03';
               edNotaPromissoriaRural : AEspecieDocumento := '03';
               edParcelaConsorcio : AEspecieDocumento := '08';
               edRecibo : AEspecieDocumento := '05';
            else
               AEspecieDocumento := '09';
            end;
         end
         else
         begin
            case Titulos[NumeroRegistro].EspecieDocumento of
               edApoliceSeguro : AEspecieDocumento := '24';
               edDuplicataMercantil : AEspecieDocumento := '21';
               edDuplicataMercantialIndicacao : AEspecieDocumento := '21';
               edDuplicataRural: AEspecieDocumento := '21';
               edDuplicataServico : AEspecieDocumento := '22';
               edDuplicataServicoIndicacao : AEspecieDocumento := '22';
               edLetraCambio : AEspecieDocumento := '26';
               edNotaPromissoria : AEspecieDocumento := '23';
               edNotaPromissoriaRural : AEspecieDocumento := '23';
               edParcelaConsorcio : AEspecieDocumento := '28';
               edRecibo : AEspecieDocumento := '25';
            else
               AEspecieDocumento := '29';
            end;
         end;
         Registro := Registro + AEspecieDocumento; // 149 a 150 - ESP�CIE DO T�TULO

         {Identifica��o de aceite do t�tulo // 150 a 150 - Identifia��o do aceite}
         case Titulos[NumeroRegistro].AceiteDocumento of
            adSim : Registro := Registro + 'A';
            adNao : Registro := Registro + 'N';
         end;

         Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataDocumento); // 151 a 156 - DATA DA EMISS�O DO T�TULO
         Registro := Registro + '0000'; // 157 a 160 - Instru��es
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorMoraJuros*100); // 161 a 173 - VALOR DE MORA POR DIA DE ATRASO
         
         if Titulos[NumeroRegistro].DataDesconto <> 0 then // 174 a 179 - DATA LIMITE PARA CONCESS�O DE DESCONTO
            Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataDesconto)
         else
            Registro := Registro + Formatar(' ',6,false,'0');

         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorDesconto*100); // 180 a 192 - VALOR DO DESCONTO A SER CONCEDIDO
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorIOF*100); // 193 a 205 - VALOR DO I.O.F. RECOLHIDO P/ NOTAS SEGURO
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorAbatimento*100); // 206 a 218 - VALOR DO ABATIMENTO A SER CONCEDIDO
         Registro := Registro + Formatar(ASacadoTipoInscricao,2,false,'0'); // 219 a 220 - IDENTIFICA��O DO TIPO DE INSCRI��O/SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.NumeroCPFCGC,14,false,'0'); // 221 a 234 - N� DE INSCRI��O DO SACADO  (CPF/CGC)
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Nome,40); // 235 a 274 - NOME DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Rua+' '+Titulos[NumeroRegistro].Sacado.Endereco.Numero+' '+Titulos[NumeroRegistro].Sacado.Endereco.Complemento,40); // 275 a 314 - RUA, N�MERO E COMPLEMENTO DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Bairro,12); // 315 a 326 - BAIRRO DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.CEP,8,true,'0'); // 327 a 334 - CEP DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Cidade,15,true); // 335 a 349 - CIDADE DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Estado,2,false); // 350 a 351 - UF DO SACADO
         Registro := Registro + '1'; // 352 a 352 - Indicador do Sacador {Indicando que n�o h� sacador}
         Registro := Registro + Formatar('',30,true,' '); // 353 a 394 - NOME DO SACADOR/AVALISTA
         Registro := Registro + Formatar(IntToStr(NumeroRegistro+2),6,false,'0'); // 395 a 400 - N� SEQ�ENCIAL DO REGISTRO NO ARQUIVO

         Remessa.Add(Registro);
         NumeroRegistro := NumeroRegistro + 1;
      end;

      { GERAR REGISTRO TRAILER DA REMESSA }

      Remessa.Add('9'+
                  Formatar('',393,true,' ')+
                  Formatar(IntToStr(NumeroRegistro+2),6,false,'0'));
   end;

   Result := TRUE;
end;

function TgbBanco453.GerarRemessa(var ACobranca: TgbCobranca; var Remessa: TStringList) : boolean;
begin
   case ACobranca.LayoutArquivo of
//      laCNAB240 : Result := GerarRemessaCNAB240(ACobranca, Remessa);
      laCNAB400 : Result := GerarRemessaCNAB400(ACobranca, Remessa);
   else
      Raise Exception.CreateFmt('S� est� dispon�vel a remessa no layout CNAB400 para o banco %s - %s',[CodigoBanco,NomeBanco]);
   end;
end;

function TgbBanco453.LerRetornoCNAB400(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean;
var
   ACodigoBanco,
   ANomeCedente,
   ATipoInscricao : string;
   ATipoOcorrencia : string;
   NumeroRegistro : integer;
   ATitulo : TgbTitulo;
begin
   NumeroRegistro := 0;
   ATitulo := TgbTitulo.Create(nil);

   TRY

      with ACobranca do
      begin
         Titulos.Clear; {Zera o conjunto de t�tulos, antes de incluir os t�tulos do arquivo retorno}

         if Retorno.Count <= 0 then
            Raise Exception.Create('O retorno est� vazio. N�o h� dados para processar');

         {Ver se o arquivo � mesmo RETORNO DE COBRAN�A}
         if Copy(Retorno.Strings[NumeroRegistro],1,19) <> '02RETORNO01COBRANCA' then
            Raise Exception.Create(NomeArquivo+' n�o � um arquivo de retorno de cobran�a');

         if length(Retorno[0]) <> 400 then
         begin
            LayoutArquivo := laOutro;
            Raise Exception.CreateFmt('Tamanho de registro diferente de 400 bytes. Tamanho = %d bytes',[length(Retorno[0])]);
         end;

         LayoutArquivo := laCNAB400;

         {Ver se o arquivo � mesmo RETORNO DE COBRAN�A}
         if Copy(Retorno.Strings[NumeroRegistro],1,19) <> '02RETORNO01COBRANCA' then
            Raise Exception.Create(NomeArquivo+' n�o � um arquivo de retorno de cobran�a com layout CNAB400');

         { L� registro HEADER}
         ACodigoBanco := Copy(Retorno.Strings[NumeroRegistro],77,3);
         if ACodigoBanco <> CodigoBanco then
            Raise Exception.CreateFmt('Este n�o � um retorno de cobran�a do banco %s - %s',[CodigoBanco,NomeBanco]);

         ANomeCedente := Trim(Copy(Retorno.Strings[NumeroRegistro],47,30));
         if StrToInt(Copy(Retorno.Strings[NumeroRegistro],99,2)) <= 69 then
            DataArquivo := EncodeDate(StrToInt('20'+
                           Copy(Retorno.Strings[NumeroRegistro],99,2)),
                           StrToInt(Copy(Retorno.Strings[NumeroRegistro],97,2)),
                           StrToInt(Copy(Retorno.Strings[NumeroRegistro],95,2)))
         else
            DataArquivo := EncodeDate(StrToInt('19'+
                           Copy(Retorno.Strings[NumeroRegistro],99,2)),
                           StrToInt(Copy(Retorno.Strings[NumeroRegistro],97,2)),
                           StrToInt(Copy(Retorno.Strings[NumeroRegistro],95,2)));

         NumeroArquivo := StrToInt(Trim(Copy(Retorno.Strings[NumeroRegistro],390,5)));

         {L� os registros DETALHE}
         {Processa at� o pen�ltimo registro porque o �ltimo cont�m apenas o TRAILLER}
         for NumeroRegistro := 1 to (Retorno.Count - 2) do
         begin
            {Confirmar se o tipo do registro � 1}
            if Copy(Retorno.Strings[NumeroRegistro],1,1) <> '1' then
               Continue; {N�o processa o registro atual}

            { Ler t�tulos do arquivo retorno}
            {Dados do titulo}
            with ATitulo do
            begin
               {Dados do cedente do t�tulo}
               with Cedente do
               begin
                  ATipoInscricao := Copy(Retorno.Strings[NumeroRegistro],2,2);
                  if ATipoInscricao = '01' then
                     TipoInscricao := tiPessoaFisica
                  else if ATipoInscricao = '02' then
                     TipoInscricao := tiPessoaJuridica
                  else
                     TipoInscricao := tiOutro;
                  NumeroCPFCGC := Copy(Retorno.Strings[NumeroRegistro],4,14);
                  ContaBancaria.Banco.Codigo := ACodigoBanco;
                  ContaBancaria.CodigoAgencia := Copy(Retorno.Strings[NumeroRegistro],18,4);
                  ContaBancaria.NumeroConta := Copy(Retorno.Strings[NumeroRegistro],22,9);
                  ContaBancaria.DigitoConta := Copy(Retorno.Strings[NumeroRegistro],31,1);
                  CodigoCedente := Cedente.ContaBancaria.NumeroConta;
                  DigitoCodigoCedente := Cedente.ContaBancaria.DigitoConta;
                  Nome := ANomeCedente;
               end; {with ACedente}

               SeuNumero := Copy(Retorno.Strings[NumeroRegistro],25,25);
               {Nosso n�mero SEM D�GITO}
               NossoNumero := Copy(Retorno.Strings[NumeroRegistro],63,7);
               Carteira := Copy(Retorno.Strings[NumeroRegistro],108,1);

               {Tipo de ocorr�ncia}

               ATipoOcorrencia := Copy(Retorno.Strings[NumeroRegistro],109,2);
               OcorrenciaOriginal := Copy(Retorno.Strings[NumeroRegistro],109,2);
               DescricaoOcorrenciaOriginal := VerificaOcorrenciaOriginal(OcorrenciaOriginal); //@w
               case StrToInt(ATipoOcorrencia) of
                  02 : TipoOcorrencia := toRetornoRegistroConfirmado;
                  03 : TipoOcorrencia := toRetornoRegistroRecusado;
                  06 : TipoOcorrencia := toRetornoLiquidado;
                  07 : TipoOcorrencia := toRetornoLiquidadoPorConta;
                  08 : TipoOcorrencia := toRetornoBaixaPorProtesto;
                  09 : TipoOcorrencia := toRetornoBaixado;
                  10 : TipoOcorrencia := toRetornoBaixaSolicitada;
//                  11 : TipoOcorrencia := toRetornoTituloEmSer;
                  12 : TipoOcorrencia := toRetornoAbatimentoConcedido;
                  13 : TipoOcorrencia := toRetornoAbatimentoCancelado;
                  14 : TipoOcorrencia := toRetornoVencimentoAlterado;
                  15 : TipoOcorrencia := toRetornoLiquidadoEmCartorio;
                  16 : TipoOcorrencia := toRetornoLiquidado;
                  17 : TipoOcorrencia := toRetornoNomeSacadoAlterado;
                  18 : TipoOcorrencia := toRetornoEnderecoSacadoAlterado;
                  19 : TipoOcorrencia := toRetornoEncaminhadoACartorio;
                  20 : TipoOcorrencia := toRetornoEnderecoSacadoAlterado; //CEP
                  21 : TipoOcorrencia := toRetornoEnderecoSacadoAlterado; //Cidade
                  22 : TipoOcorrencia := toRetornoEnderecoSacadoAlterado; //Estado
                  23 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de CGC ou CPF
                  24 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de DATA de EMISS�O
                  25 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de DATA DESCONTO
                  26 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de ACEITE
                  27 : TipoOcorrencia := toRetornoAcertoDepositaria; //Altera��o de DEPOSIT�RIA
                  28 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de INSTRU��O de COBRAN�A
                  29 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de ESP�CIE de DOCUMENTO
                  30 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de INDICADOR de REAPRESENTA��O
                  31 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de SEU N�MERO
                  32 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de EST�GIO de COBRAN�A
                  33 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de TABELA de TARIFAS
                  34 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o de DIAS BLOQUEIO PARA LIQUIDA��O
                  35 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o  de PRAZO PROTESTO
                  36 : TipoOcorrencia := toRetornoDadosAlterados; //Altera��o  de VALOR do DESCONTO
                  37 : TipoOcorrencia := toRetornoProtestoSustado;
                  38 : TipoOcorrencia := toRetornoDebitoTarifas;
                  39 : TipoOcorrencia := toRetornoDebitoTarifas;
                  40 : TipoOcorrencia := toRetornoTipoCobrancaAlterado; //Transfer�ncia  COBRAN�A GARANTIDA para CS.
                  41 : TipoOcorrencia := toRetornoTipoCobrancaAlterado; //T�tulo  descontado transferido para cobran�a simples c/d�bito em conta
                  44 : TipoOcorrencia := toRetornoDebitoTarifas; //Tarifa altera��o cadastral - t�tulo
                  45 : TipoOcorrencia := toRetornoDebitoTarifas; //Tarifa altera��o cadastral - cliente
                  46 : TipoOcorrencia := toRetornoDespesasProtesto;
               else
                  TipoOcorrencia := toRetornoOutrasOcorrencias;
               end;

               MotivoRejeicaoComando := Copy(Retorno.Strings[NumeroRegistro],334,61);
               DescricaoMotivoRejeicaoComando:=VerificaMotivoRejeicaoComando(MotivoRejeicaoComando);

               if StrToInt(Copy(Retorno.Strings[NumeroRegistro],115,2)) <= 69 then
                  DataOcorrencia := EncodeDate(StrToInt('20'+
                                    Copy(Retorno.Strings[NumeroRegistro],115,2)),
                                    StrToInt(Copy(Retorno.Strings[NumeroRegistro],113,2)),
                                    StrToInt(Copy(Retorno.Strings[NumeroRegistro],111,2)))
               else
                  DataOcorrencia := EncodeDate(StrToInt('19'+
                                    Copy(Retorno.Strings[NumeroRegistro],115,2)),
                                    StrToInt(Copy(Retorno.Strings[NumeroRegistro],113,2)),
                                    StrToInt(Copy(Retorno.Strings[NumeroRegistro],111,2)));

               NumeroDocumento := Copy(Retorno.Strings[NumeroRegistro],117,15);

               if StrToInt(Copy(Retorno.Strings[NumeroRegistro],151,2)) <= 69 then
                  DataVencimento := EncodeDate(StrToInt('20'+
                                    Copy(Retorno.Strings[NumeroRegistro],151,2)),
                                    StrToInt(Copy(Retorno.Strings[NumeroRegistro],149,2)),
                                    StrToInt(Copy(Retorno.Strings[NumeroRegistro],147,2)))
               else
                  DataVencimento := EncodeDate(StrToInt('19'+
                                    Copy(Retorno.Strings[NumeroRegistro],151,2)),
                                    StrToInt(Copy(Retorno.Strings[NumeroRegistro],149,2)),
                                    StrToInt(Copy(Retorno.Strings[NumeroRegistro],147,2)));

               ValorDocumento := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],153,13))/100;

               {Esp�cie do documento}
               if Trim(Copy(Retorno.Strings[NumeroRegistro],174,2)) = '' then
                  EspecieDocumento := edOutros
               else
                  case StrToInt(Copy(Retorno.Strings[NumeroRegistro],174,2)) of
                     01, 21 : EspecieDocumento := edDuplicataMercantil;
                     02, 22 : EspecieDocumento := edDuplicataServico;
                     03, 23 : EspecieDocumento := edNotaPromissoria;
                     04, 24 : EspecieDocumento := edApoliceSeguro;
                     05, 25 : EspecieDocumento := edRecibo;
                     06, 26 : EspecieDocumento := edLetraCambio;
                     08, 28 : EspecieDocumento := edParcelaConsorcio;
                  else
                     EspecieDocumento := edOutros;
                  end;

               ValorDespesaCobranca := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],176,13))/100;
               ValorIOF := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],215,13))/100;
               ValorAbatimento := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],228,13))/100;
               ValorDesconto := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],241,13))/100;
               ValorMoraJuros := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],267,13))/100;
               ValorOutrosCreditos := StrToFloat(Trim(Copy(Retorno.Strings[NumeroRegistro],280,13)))/100;

               if StrToInt(Copy(Retorno.Strings[NumeroRegistro],300,2)) <= 69 then
                  DataCredito := EncodeDate(StrToInt('20'+
                                 Copy(Retorno.Strings[NumeroRegistro],300,2)),
                                 StrToInt(Copy(Retorno.Strings[NumeroRegistro],298,2)),
                                 StrToInt(Copy(Retorno.Strings[NumeroRegistro],296,2)))
               else
                  DataCredito := EncodeDate(StrToInt('19'+
                                 Copy(Retorno.Strings[NumeroRegistro],300,2)),
                                 StrToInt(Copy(Retorno.Strings[NumeroRegistro],298,2)),
                                 StrToInt(Copy(Retorno.Strings[NumeroRegistro],296,2)));

            end; {with ATitulo}

            {Insere o t�tulo}
            Titulos.Add(ATitulo);
         end;
      end;

      ATitulo.Free;
      Result := TRUE
   EXCEPT
      ATitulo.Free;
      Result := FALSE;
      Raise; {Propaga o erro}
   END;
end;

function TgbBanco453.LerRetorno(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean;
var
   ACodigoBanco,
   ANomeCedente,
   ATipoInscricao : string;
   NumeroRegistro : integer;
   ATitulo : TgbTitulo;
begin
   NumeroRegistro := 0;
   ATitulo := TgbTitulo.Create(nil);

   TRY

      with ACobranca do
      begin
         Titulos.Clear; {Zera o conjunto de t�tulos, antes de incluir os t�tulos do arquivo retorno}

         if Retorno.Count <= 0 then
            Raise Exception.Create('O retorno est� vazio. N�o h� dados para processar');

         case length(Retorno[0]) of
            240 :
               begin
                  LayoutArquivo := laCNAB240;
//                Result := LerRetornoCNAB240(ACobranca, Retorno);
               end;
            400 :
               begin
                  LayoutArquivo := laCNAB400;
                  Result := LerRetornoCNAB400(ACobranca, Retorno);
               end
         else
            begin
               LayoutArquivo := laOutro;
               Raise Exception.CreateFmt('Tamanho de registro inv�lido: %d',[length(Retorno[0])]);
            end;
         end;
      end;

      ATitulo.Free;
      Result := TRUE
   EXCEPT
      ATitulo.Free;
      Result := FALSE;
      Raise; //Propaga o erro
   END;
end;


function TgbBanco453.VerificaOcorrenciaOriginal(sOcorrenciaOriginal: String): String;
begin
   if sOcorrenciaOriginal='  ' then begin
      Result:='';
      Exit;
   end;
   case StrToInt(sOcorrenciaOriginal) of
      02 : Result := '02 - ENTRADA CONFIRMADA';
      03 : Result := '03 - REGISTRO REJEITADO';
      05 : Result := '05 - SOLICITA��O ENCAMINHADA � AG�NCIA';
      06 : Result := '06 - LIQUIDA��O NORMAL';
      07 : Result := '07 - PAGAMENTO POR CONTA';
      08 : Result := '08 - BAIXA T�TULO PROTESTADO';
      09 : Result := '09 - BAIXA COMANDADA PELO BANCO';
      10 : Result := '10 - BAIXA COMANDADA PELA EMPRESA';
      12 : Result := '12 - ABATIMENTO CONCEDIDO';
      13 : Result := '13 - ABATIMENTO CANCELADO';
      14 : Result := '14 - VENCIMENTO ALTERADO';
      15 : Result := '15 - LIQUIDA��O EM CART�RIO';
      16 : Result := '16 - LIQUIDA��O POR COMPENSA��O';
      17 : Result := '17 - ALTERA��O DE NOME DO SACADO';
      18 : Result := '18 - ALTERA��O DE ENDERE�O DE COBRAN�A';
      19 : Result := '19 - ENVIADO PARA CART�RIO';
      20 : Result := '20 - ALTERA��O DE CEP';
      21 : Result := '21 - ALTERA��O DE CIDADE';
      22 : Result := '22 - ALTERA��O DE ESTADO';
      23 : Result := '23 - ALTERA��O DE CGC OU CPF';
      24 : Result := '24 - ALTERA��O DE DATA DE EMISS�O';
      25 : Result := '25 - ALTERA��O DE DATA DESCONTO';
      26 : Result := '26 - ALTERA��O DE ACEITE';
      27 : Result := '27 - ALTERA��O DE DEPOSIT�RIA';
      28 : Result := '28 - ALTERA��O DE INSTRU��O DE COBRAN�A';
      29 : Result := '29 - ALTERA��O DE ESP�CIE DE DOCUMENTO';
      30 : Result := '30 - ALTERA��O DE INDICADOR DE REAPRESENTA��O';
      31 : Result := '31 - ALTERA��O DE SEU N�MERO';
      32 : Result := '32 - ALTERA��O DE EST�GIO DE COBRAN�A';
      33 : Result := '33 - ALTERA��O DE TABELA DE TARIFAS';
      34 : Result := '34 - ALTERA��O  DE DIAS BLOQUEIO PARA LIQUIDA��O';
      35 : Result := '35 - ALTERA��O  DE PRAZO PROTESTO';
      36 : Result := '36 - ALTERA��O  DE VALOR DO DESCONTO';
      37 : Result := '37 - PROTESTO  SUSTADO';
      38 : Result := '38 - CR�DIT  NO C/C CONTA GARANTIDA';
      39 : Result := '39 - LIQ . COB.  GARANTIDA S/ CR�DITO NO C/C';
      40 : Result := '40 - TRANSFER�NCIA  COBRAN�A GARANTIDA PARA CS.';
      41 : Result := '41 - T�TULO  DESCONTADO TRANSFERIDO PARA COBRAN�A SIMPLES C/D�BITO EM CONTA';
      42 : Result := '42 - T�TULO  DESCONTADO LIQUIDADO COM D�BITO EM CONTA';
      43 : Result := '43 - BAIXA  COMANDADA PELO BANCO - CR�DITO DIRETO EM CONTA';
      44 : Result := '44 - TARIFA ALTERA��O CADASTRAL - T�TULO';
      45 : Result := '45 - TARIFA ALTERA��O CADASTRAL - CLIENTE';
      46 : Result := '46 - DESPESAS CART�RIO';
      99 : Result := 'ALTERA��ES  ESPEC�FICAS DA  AG.BANC�RIA - N�O CONSIDERAR';
   else
      Result:='';
   end;
end;


function TgbBanco453.VerificaMotivoRejeicaoComando(sMotivoRejeicaoComando: String): String;
var
   CampoErrado: Integer;
begin
   Result := '';
   CampoErrado := Pos('1',sMotivoRejeicaoComando);
   case CampoErrado of
      00 : Result := '';
      01 : Result := 'C�DIGO MOEDA INV�LIDO' ;
      02 : Result := 'C�DIGO MOEDA INV�LIDO PARA SEGURO - REAIS';
      03 : Result := 'C�DIGO MOEDA / MORA INCONSISTENTES';
      04 : Result := 'VALOR EM REAIS - MORA EM MOEDA';
      05 : Result := 'PROIBIDO PROTESTAR - T�TULO N�O VENCIDO';
      06 : Result := 'ESP�CIE DOCUMENTO INV�LIDA';
      07 : Result := 'ESP�CIE DOCUMENTO INEXISTENTE';
      08 : Result := 'TIPO DE OPERA��O INV. P/ SEGUROS C/ IOS';
      09 : Result := 'TIPO DE OPERA��O INEXISTENTE';
      10 : Result := 'CONTRATO PROIBIDO PARA ESTA CARTEIRA';
      11 : Result := 'FALTA N�MERO DO CONTRATO';
      12 : Result := 'PROIBIDO INFORMAR TIPO DE CONTA';
      13 : Result := 'TIPO DE CONTA DO CONTRATO INEXISTENTE';
      14 : Result := 'D�GITO DO CONTRATO N�O CONFERE';
      15 : Result := 'CONTRATO INEXISTENTE';
      16 : Result := 'DATA DA EMISS�O INV�LIDA';
      17 : Result := 'VALOR DO T�TULO INV�LIDO';
      18 : Result := 'DATA VENCIMENTO INV�LIDA';
      19 : Result := 'DATA DE VENCIMENTO ANTERIOR � EMISS�O';
      20 : Result := 'FALTA VENCIMENTO DESCONTO';
      21 : Result := 'DATA VENCIMENTO DESCONTO INV�LIDA';
      22 : Result := 'DATA DESCONTO POSTERIOR AO VENCIMENTO';
      23 : Result := 'FALTA VALOR DESCONTO';
      24 : Result := '';
      25 : Result := 'BANCO/AG�NCIA COBRADOR INEXISTENTE';
      26 : Result := '';
      27 : Result := 'VALOR/TAXA DE MORA MUITO ALTO';
      28 : Result := 'FALTA CEP, BANCO E AG�NCIA COBRADOR';
      29 : Result := 'FALTA NOME DO SACADO';
      30 : Result := 'FALTA ENDERE�O';
      31 : Result := 'FALTA CIDADE';
      32 : Result := 'FALTA ESTADO';
      33 : Result := 'ESTADO INV�LIDO';
      34 : Result := 'FALTA CPF/CGC DO SACADO';
      35 : Result := 'FALTA NUMERA��O - BLOQUETE CLIENTE';
      36 : Result := 'T�TULO PR�-NUMERADO J� EXISTENTE';
      37 : Result := 'D�GITO NOSSO N�MERO N�O CONFERE';
      38 : Result := 'SERVI�O INV�LIDO';
      39 : Result := 'REGISTRO 2 - MENSAGEM INV�LIDO OU INEXIST.';
      40 : Result := 'D�GITO DO CLIENTE /CONTRATO COM ERRO';
      41 : Result := '';
      42 : Result := 'T�TULO INEXISTENTE';
      43 : Result := 'T�TULO LIQUIDADO';
      44 : Result := 'T�TULO N�O PODE SER BAIXADO';
      45 : Result := 'VALOR NOMINAL INCORRETO';
      46 : Result := '';
      47 : Result := 'FALTA TIPO DE CONTA DO CONTRATO';
      48 : Result := 'CPF/CNPJ DO SACADOR COM ERRO';
      49 : Result := 'CPF/CNPJ DO SACADOR IGUAL CPF/CNPJ DO CEDENTE';
      50 : Result := 'CPF/CNPJ DO SACADOR IGUAL CPF/CNPJ DO SACADO';
      51 : Result := '';
      52 : Result := 'VALOR ABATIMENTO INV�LIDO';
      53 : Result := '';
      54 : Result := '';
      55 : Result := 'FALTA TIPO DE PESSOA PARA ALTERAR CGC/CPF';
      56 : Result := 'CPF/CNPJ DO SACADO COM ERRO';
      57 : Result := '';
      58 : Result := '';
      59 : Result := 'ACEITE INV�LIDO PARA ESP�CIE DOCUMENTO';
      60 : Result := 'ALTERA��O PRAZO PROTESTO INV�LIDO';
      61 : Result := 'DEPOSIT�RIA N�O CADASTRADA'
   else
      Result := 'MOTIVO N�O IDENTIFICADO - POSI��O ' + IntToStr(CampoErrado);
   end;
end;

{$ENDIF}

initialization
RegisterClass(TgbBanco453);

end.
