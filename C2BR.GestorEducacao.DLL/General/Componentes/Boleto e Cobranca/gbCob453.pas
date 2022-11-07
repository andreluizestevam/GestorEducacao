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
      function  GetCampoLivreCodigoBarra(ATitulo: TgbTitulo) : string; {Retorna o conteúdo da parte variável do código de barras}
      function  CalcularDigitoNossoNumero(ATitulo: TgbTitulo) : string; {Calcula o dígito do NossoNumero, conforme critérios definidos por cada banco}
      procedure FormatarBoleto(ATitulo: TgbTitulo; var AAgenciaCodigoCedente, ANossoNumero, ACarteira, AEspecieDocumento: string); {Define o formato como alguns valores serão apresentados no boleto }
{$IFNDEF VER120}
      function  LerRetorno(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean; {Lê o arquivo retorno recebido do banco}
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
    Multiplicar os algarismos da composição, iniciando da direita para a esquerda
    pelos pesos: 3, 7, 9, 1, com exceção do campo "Dígito da Conta", que deve ser
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

      Remessa.Add('0'+ // 1 a 1 - IDENTIFICAÇÃO DO REGISTRO HEADER
                  '9'+ // 2 a 2 - TIPO DE OPERAÇÃO - REMESSA
                  'REMESSA'+ // 3 a 9 - IDENTIFICAÇÃO POR EXTENSO DO MOVIMENTO
                  '01'+ // 10 a 11 - IDENTIFICAÇÃO DO TIPO DE SERVIÇO
                  Formatar('COBRANCA',15)+ // 12 a 26 - IDENTIFICAÇÃO POR EXTENSO DO TIPO DE SERVIÇO
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.CodigoAgencia,4,false,'0')+ // 27 a 30 - AGÊNCIA MANTENEDORA DA CONTA
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.NumeroConta,9,false,'0')+ // 31 a 39 - TIPO DE CONTA (2 DÍGITOS ) + NÚMERO DA CONTA CORRENTE DA EMPRESA (7 DÍGITOS)
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.DigitoConta,1)+ // 40 a 40 - DÍGITO DE AUTO CONFERÊNCIA AG/CONTA EMPRESA
                  Formatar('',6)+ // 41 a 46 - BRANCOS
                  Formatar(Titulos[NumeroRegistro].Cedente.Nome,30,true,' ')+ // 47 a 76 - NOME POR EXTENSO DA "EMPRESA MÃE"
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Codigo,3,false,'0')+ // 77 a 79 - Nº DO BANCO NA CÂMARA DE COMPENSAÇÃO
                  Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Nome,15,true,' ')+ // 80 a 94 - NOME POR EXTENSO DO BANCO COBRADOR
                  Formatar('',15) + // 95 a 109 - BRANCOS
                  'CD0V01' + // 110 a 115 - CÓDIGO VERSÃO
                  Formatar('',279) + // 116 a 394 - BRANCOS
                  '000001'); // 395 a 400 - NÚMERO SEQÜENCIAL DO REGISTRO NO ARQUIVO

      { GERAR TODOS OS REGISTROS DETALHE DA REMESSA }
      while NumeroRegistro <= (Titulos.Count - 1) do
      begin
         if Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Codigo,3,false,'0') <> Formatar(CodigoBanco,3,false,'0') then
            Raise Exception.CreateFmt('Titulo não pertence ao banco %s - %s',[CodigoBanco,NomeBanco]);

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

         Registro := '1'; // 1 a 1 - IDENTIFICAÇÃO DO REGISTRO TRANSAÇÃO
         Registro := Registro + Formatar('',16); // 2 a 17 - BRANCOS
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.CodigoAgencia,4,false,'0'); // 18 a 21 - AGÊNCIA MANTENEDORA DA CONTA
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.NumeroConta,9,false,'0'); // 22 a 30 - TIPO DE CONTA (2 DÍGITOS ) + NÚMERO DA CONTA CORRENTE DA EMPRESA (7 DÍGITOS)
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.DigitoConta,1); // 31 a 31 - DÍGITO DE AUTO CONFERÊNCIA AG/CONTA EMPRESA
         Registro := Registro + Formatar(IntToStr(DaysBetween(Titulos[NumeroRegistro].DataVencimento, Titulos[NumeroRegistro].DataProtesto)),2,false,'0'); // 32 A 33 - PRAZO PARA PROTESTO
         Registro := Registro + '00'; // 34 a 35 - CÓDIGO DA MOEDA (00 = REAL)
         Registro := Registro + '2'; // 36 a 36 - TIPO MORA (2 = VALOR DA MORA SERÁ EXPRESSOEM REAIS)
         Registro := Registro + ' '; // 37 a 37 - ESPECIFICA SE EXISTE MENSAGEM PARA BLOQUETE (BRANCO - NÃO EXISTE MENSAGEM)
         Registro := Registro + Formatar('',25); // 38 a 62 // USO DA EMPRESA
         Registro := Registro + Formatar(Titulos[NumeroRegistro].NossoNumero,7,false,'0'); // 63 a 69 - NOSSO NÚMERO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].DigitoNossoNumero,1,false,'0'); // 70 a 70 - DÍGITO VERIFICADOR DO NOSSO NÚMERO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.CodigoCedente,9,false,'0'); // 71 a 79 - NÚMERO DO CONTRATO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.DigitoCodigoCedente,1,false,'0'); // 80 a 80 - DÍGITO VERIFICADOR DO NÚMERO DO CONTRATO
         Registro := Registro + '  '; // 81 a 82 - BRANCOS
         Registro := Registro + '00'; // 83 a 84 - Identificação do tipo de inscrição do sacador: 01 - CPF // 02 - CNPJ
         Registro := Registro + Formatar('',14,false,'0'); // 85 a 98 - Número de inscrição (CPF ou CNPJ) do sacador
         Registro := Registro + Formatar('',9); // 99 a 107 - Brancos
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Carteira,1,false,'0');  // 108 a 108 - Código da carteira
         
         {Tipo de ocorrência}
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
            Raise Exception.CreateFmt('Ocorrência inválida em remessa - Nosso número: %s / Seu número: %s',[Titulos[NumeroRegistro].NossoNumero,Titulos[NumeroRegistro].SeuNumero]);
         end;
         Registro := Registro + ATipoOcorrencia; // IDENTIFICAÇÃO DA OCORRÊNCIA  // 109 a 110 - Identificação do Serviço

         Registro := Registro + Formatar(Titulos[NumeroRegistro].NumeroDocumento,10); // 111 a 120 - Nº DO DOCUMENTO DE COBRANÇA (DUPL.,NP ETC.)
         Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataVencimento); // 121 a 126 - DATA DE VENCIMENTO DO TÍTULO
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorDocumento * 100); // 127 a 139 - VALOR NOMINAL DO TÍTULO
         Registro := Registro + Formatar('',7,false,'0'); // 140 a 146 - CÓDIGO DO BANCO E AGÊNCIA COBRADORA
         Registro := Registro + ' '; // 147 a 147 - Brancos

         {Espécie do título}
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
         Registro := Registro + AEspecieDocumento; // 149 a 150 - ESPÉCIE DO TÍTULO

         {Identificação de aceite do título // 150 a 150 - Identifiação do aceite}
         case Titulos[NumeroRegistro].AceiteDocumento of
            adSim : Registro := Registro + 'A';
            adNao : Registro := Registro + 'N';
         end;

         Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataDocumento); // 151 a 156 - DATA DA EMISSÃO DO TÍTULO
         Registro := Registro + '0000'; // 157 a 160 - Instruções
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorMoraJuros*100); // 161 a 173 - VALOR DE MORA POR DIA DE ATRASO
         
         if Titulos[NumeroRegistro].DataDesconto <> 0 then // 174 a 179 - DATA LIMITE PARA CONCESSÃO DE DESCONTO
            Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataDesconto)
         else
            Registro := Registro + Formatar(' ',6,false,'0');

         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorDesconto*100); // 180 a 192 - VALOR DO DESCONTO A SER CONCEDIDO
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorIOF*100); // 193 a 205 - VALOR DO I.O.F. RECOLHIDO P/ NOTAS SEGURO
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorAbatimento*100); // 206 a 218 - VALOR DO ABATIMENTO A SER CONCEDIDO
         Registro := Registro + Formatar(ASacadoTipoInscricao,2,false,'0'); // 219 a 220 - IDENTIFICAÇÃO DO TIPO DE INSCRIÇÃO/SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.NumeroCPFCGC,14,false,'0'); // 221 a 234 - Nº DE INSCRIÇÃO DO SACADO  (CPF/CGC)
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Nome,40); // 235 a 274 - NOME DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Rua+' '+Titulos[NumeroRegistro].Sacado.Endereco.Numero+' '+Titulos[NumeroRegistro].Sacado.Endereco.Complemento,40); // 275 a 314 - RUA, NÚMERO E COMPLEMENTO DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Bairro,12); // 315 a 326 - BAIRRO DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.CEP,8,true,'0'); // 327 a 334 - CEP DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Cidade,15,true); // 335 a 349 - CIDADE DO SACADO
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Estado,2,false); // 350 a 351 - UF DO SACADO
         Registro := Registro + '1'; // 352 a 352 - Indicador do Sacador {Indicando que não há sacador}
         Registro := Registro + Formatar('',30,true,' '); // 353 a 394 - NOME DO SACADOR/AVALISTA
         Registro := Registro + Formatar(IntToStr(NumeroRegistro+2),6,false,'0'); // 395 a 400 - Nº SEQÜENCIAL DO REGISTRO NO ARQUIVO

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
      Raise Exception.CreateFmt('Só está disponível a remessa no layout CNAB400 para o banco %s - %s',[CodigoBanco,NomeBanco]);
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
         Titulos.Clear; {Zera o conjunto de títulos, antes de incluir os títulos do arquivo retorno}

         if Retorno.Count <= 0 then
            Raise Exception.Create('O retorno está vazio. Não há dados para processar');

         {Ver se o arquivo é mesmo RETORNO DE COBRANÇA}
         if Copy(Retorno.Strings[NumeroRegistro],1,19) <> '02RETORNO01COBRANCA' then
            Raise Exception.Create(NomeArquivo+' não é um arquivo de retorno de cobrança');

         if length(Retorno[0]) <> 400 then
         begin
            LayoutArquivo := laOutro;
            Raise Exception.CreateFmt('Tamanho de registro diferente de 400 bytes. Tamanho = %d bytes',[length(Retorno[0])]);
         end;

         LayoutArquivo := laCNAB400;

         {Ver se o arquivo é mesmo RETORNO DE COBRANÇA}
         if Copy(Retorno.Strings[NumeroRegistro],1,19) <> '02RETORNO01COBRANCA' then
            Raise Exception.Create(NomeArquivo+' não é um arquivo de retorno de cobrança com layout CNAB400');

         { Lê registro HEADER}
         ACodigoBanco := Copy(Retorno.Strings[NumeroRegistro],77,3);
         if ACodigoBanco <> CodigoBanco then
            Raise Exception.CreateFmt('Este não é um retorno de cobrança do banco %s - %s',[CodigoBanco,NomeBanco]);

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

         {Lê os registros DETALHE}
         {Processa até o penúltimo registro porque o último contém apenas o TRAILLER}
         for NumeroRegistro := 1 to (Retorno.Count - 2) do
         begin
            {Confirmar se o tipo do registro é 1}
            if Copy(Retorno.Strings[NumeroRegistro],1,1) <> '1' then
               Continue; {Não processa o registro atual}

            { Ler títulos do arquivo retorno}
            {Dados do titulo}
            with ATitulo do
            begin
               {Dados do cedente do título}
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
               {Nosso número SEM DÍGITO}
               NossoNumero := Copy(Retorno.Strings[NumeroRegistro],63,7);
               Carteira := Copy(Retorno.Strings[NumeroRegistro],108,1);

               {Tipo de ocorrência}

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
                  23 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de CGC ou CPF
                  24 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de DATA de EMISSÃO
                  25 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de DATA DESCONTO
                  26 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de ACEITE
                  27 : TipoOcorrencia := toRetornoAcertoDepositaria; //Alteração de DEPOSITÁRIA
                  28 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de INSTRUÇÃO de COBRANÇA
                  29 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de ESPÉCIE de DOCUMENTO
                  30 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de INDICADOR de REAPRESENTAÇÃO
                  31 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de SEU NÚMERO
                  32 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de ESTÁGIO de COBRANÇA
                  33 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de TABELA de TARIFAS
                  34 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração de DIAS BLOQUEIO PARA LIQUIDAÇÃO
                  35 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração  de PRAZO PROTESTO
                  36 : TipoOcorrencia := toRetornoDadosAlterados; //Alteração  de VALOR do DESCONTO
                  37 : TipoOcorrencia := toRetornoProtestoSustado;
                  38 : TipoOcorrencia := toRetornoDebitoTarifas;
                  39 : TipoOcorrencia := toRetornoDebitoTarifas;
                  40 : TipoOcorrencia := toRetornoTipoCobrancaAlterado; //Transferência  COBRANÇA GARANTIDA para CS.
                  41 : TipoOcorrencia := toRetornoTipoCobrancaAlterado; //Título  descontado transferido para cobrança simples c/débito em conta
                  44 : TipoOcorrencia := toRetornoDebitoTarifas; //Tarifa alteração cadastral - título
                  45 : TipoOcorrencia := toRetornoDebitoTarifas; //Tarifa alteração cadastral - cliente
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

               {Espécie do documento}
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

            {Insere o título}
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
         Titulos.Clear; {Zera o conjunto de títulos, antes de incluir os títulos do arquivo retorno}

         if Retorno.Count <= 0 then
            Raise Exception.Create('O retorno está vazio. Não há dados para processar');

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
               Raise Exception.CreateFmt('Tamanho de registro inválido: %d',[length(Retorno[0])]);
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
      05 : Result := '05 - SOLICITAÇÃO ENCAMINHADA À AGÊNCIA';
      06 : Result := '06 - LIQUIDAÇÃO NORMAL';
      07 : Result := '07 - PAGAMENTO POR CONTA';
      08 : Result := '08 - BAIXA TÍTULO PROTESTADO';
      09 : Result := '09 - BAIXA COMANDADA PELO BANCO';
      10 : Result := '10 - BAIXA COMANDADA PELA EMPRESA';
      12 : Result := '12 - ABATIMENTO CONCEDIDO';
      13 : Result := '13 - ABATIMENTO CANCELADO';
      14 : Result := '14 - VENCIMENTO ALTERADO';
      15 : Result := '15 - LIQUIDAÇÃO EM CARTÓRIO';
      16 : Result := '16 - LIQUIDAÇÃO POR COMPENSAÇÃO';
      17 : Result := '17 - ALTERAÇÃO DE NOME DO SACADO';
      18 : Result := '18 - ALTERAÇÃO DE ENDEREÇO DE COBRANÇA';
      19 : Result := '19 - ENVIADO PARA CARTÓRIO';
      20 : Result := '20 - ALTERAÇÃO DE CEP';
      21 : Result := '21 - ALTERAÇÃO DE CIDADE';
      22 : Result := '22 - ALTERAÇÃO DE ESTADO';
      23 : Result := '23 - ALTERAÇÃO DE CGC OU CPF';
      24 : Result := '24 - ALTERAÇÃO DE DATA DE EMISSÃO';
      25 : Result := '25 - ALTERAÇÃO DE DATA DESCONTO';
      26 : Result := '26 - ALTERAÇÃO DE ACEITE';
      27 : Result := '27 - ALTERAÇÃO DE DEPOSITÁRIA';
      28 : Result := '28 - ALTERAÇÃO DE INSTRUÇÃO DE COBRANÇA';
      29 : Result := '29 - ALTERAÇÃO DE ESPÉCIE DE DOCUMENTO';
      30 : Result := '30 - ALTERAÇÃO DE INDICADOR DE REAPRESENTAÇÃO';
      31 : Result := '31 - ALTERAÇÃO DE SEU NÚMERO';
      32 : Result := '32 - ALTERAÇÃO DE ESTÁGIO DE COBRANÇA';
      33 : Result := '33 - ALTERAÇÃO DE TABELA DE TARIFAS';
      34 : Result := '34 - ALTERAÇÃO  DE DIAS BLOQUEIO PARA LIQUIDAÇÃO';
      35 : Result := '35 - ALTERAÇÃO  DE PRAZO PROTESTO';
      36 : Result := '36 - ALTERAÇÃO  DE VALOR DO DESCONTO';
      37 : Result := '37 - PROTESTO  SUSTADO';
      38 : Result := '38 - CRÉDIT  NO C/C CONTA GARANTIDA';
      39 : Result := '39 - LIQ . COB.  GARANTIDA S/ CRÉDITO NO C/C';
      40 : Result := '40 - TRANSFERÊNCIA  COBRANÇA GARANTIDA PARA CS.';
      41 : Result := '41 - TÍTULO  DESCONTADO TRANSFERIDO PARA COBRANÇA SIMPLES C/DÉBITO EM CONTA';
      42 : Result := '42 - TÍTULO  DESCONTADO LIQUIDADO COM DÉBITO EM CONTA';
      43 : Result := '43 - BAIXA  COMANDADA PELO BANCO - CRÉDITO DIRETO EM CONTA';
      44 : Result := '44 - TARIFA ALTERAÇÃO CADASTRAL - TÍTULO';
      45 : Result := '45 - TARIFA ALTERAÇÃO CADASTRAL - CLIENTE';
      46 : Result := '46 - DESPESAS CARTÓRIO';
      99 : Result := 'ALTERAÇÕES  ESPECÍFICAS DA  AG.BANCÁRIA - NÃO CONSIDERAR';
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
      01 : Result := 'CÓDIGO MOEDA INVÁLIDO' ;
      02 : Result := 'CÓDIGO MOEDA INVÁLIDO PARA SEGURO - REAIS';
      03 : Result := 'CÓDIGO MOEDA / MORA INCONSISTENTES';
      04 : Result := 'VALOR EM REAIS - MORA EM MOEDA';
      05 : Result := 'PROIBIDO PROTESTAR - TÍTULO NÃO VENCIDO';
      06 : Result := 'ESPÉCIE DOCUMENTO INVÁLIDA';
      07 : Result := 'ESPÉCIE DOCUMENTO INEXISTENTE';
      08 : Result := 'TIPO DE OPERAÇÃO INV. P/ SEGUROS C/ IOS';
      09 : Result := 'TIPO DE OPERAÇÃO INEXISTENTE';
      10 : Result := 'CONTRATO PROIBIDO PARA ESTA CARTEIRA';
      11 : Result := 'FALTA NÚMERO DO CONTRATO';
      12 : Result := 'PROIBIDO INFORMAR TIPO DE CONTA';
      13 : Result := 'TIPO DE CONTA DO CONTRATO INEXISTENTE';
      14 : Result := 'DÍGITO DO CONTRATO NÃO CONFERE';
      15 : Result := 'CONTRATO INEXISTENTE';
      16 : Result := 'DATA DA EMISSÃO INVÁLIDA';
      17 : Result := 'VALOR DO TÍTULO INVÁLIDO';
      18 : Result := 'DATA VENCIMENTO INVÁLIDA';
      19 : Result := 'DATA DE VENCIMENTO ANTERIOR À EMISSÃO';
      20 : Result := 'FALTA VENCIMENTO DESCONTO';
      21 : Result := 'DATA VENCIMENTO DESCONTO INVÁLIDA';
      22 : Result := 'DATA DESCONTO POSTERIOR AO VENCIMENTO';
      23 : Result := 'FALTA VALOR DESCONTO';
      24 : Result := '';
      25 : Result := 'BANCO/AGÊNCIA COBRADOR INEXISTENTE';
      26 : Result := '';
      27 : Result := 'VALOR/TAXA DE MORA MUITO ALTO';
      28 : Result := 'FALTA CEP, BANCO E AGÊNCIA COBRADOR';
      29 : Result := 'FALTA NOME DO SACADO';
      30 : Result := 'FALTA ENDEREÇO';
      31 : Result := 'FALTA CIDADE';
      32 : Result := 'FALTA ESTADO';
      33 : Result := 'ESTADO INVÁLIDO';
      34 : Result := 'FALTA CPF/CGC DO SACADO';
      35 : Result := 'FALTA NUMERAÇÃO - BLOQUETE CLIENTE';
      36 : Result := 'TÍTULO PRÉ-NUMERADO JÁ EXISTENTE';
      37 : Result := 'DÍGITO NOSSO NÚMERO NÃO CONFERE';
      38 : Result := 'SERVIÇO INVÁLIDO';
      39 : Result := 'REGISTRO 2 - MENSAGEM INVÁLIDO OU INEXIST.';
      40 : Result := 'DÍGITO DO CLIENTE /CONTRATO COM ERRO';
      41 : Result := '';
      42 : Result := 'TÍTULO INEXISTENTE';
      43 : Result := 'TÍTULO LIQUIDADO';
      44 : Result := 'TÍTULO NÃO PODE SER BAIXADO';
      45 : Result := 'VALOR NOMINAL INCORRETO';
      46 : Result := '';
      47 : Result := 'FALTA TIPO DE CONTA DO CONTRATO';
      48 : Result := 'CPF/CNPJ DO SACADOR COM ERRO';
      49 : Result := 'CPF/CNPJ DO SACADOR IGUAL CPF/CNPJ DO CEDENTE';
      50 : Result := 'CPF/CNPJ DO SACADOR IGUAL CPF/CNPJ DO SACADO';
      51 : Result := '';
      52 : Result := 'VALOR ABATIMENTO INVÁLIDO';
      53 : Result := '';
      54 : Result := '';
      55 : Result := 'FALTA TIPO DE PESSOA PARA ALTERAR CGC/CPF';
      56 : Result := 'CPF/CNPJ DO SACADO COM ERRO';
      57 : Result := '';
      58 : Result := '';
      59 : Result := 'ACEITE INVÁLIDO PARA ESPÉCIE DOCUMENTO';
      60 : Result := 'ALTERAÇÃO PRAZO PROTESTO INVÁLIDO';
      61 : Result := 'DEPOSITÁRIA NÃO CADASTRADA'
   else
      Result := 'MOTIVO NÃO IDENTIFICADO - POSIÇÃO ' + IntToStr(CampoErrado);
   end;
end;

{$ENDIF}

initialization
RegisterClass(TgbBanco453);

end.
