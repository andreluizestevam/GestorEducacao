unit gbCob399;

interface

uses
   classes, SysUtils, Variants, gbCobranca
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
   CodigoBanco = '399';
   NomeBanco = 'HSBC';

type

   TgbBanco399 = class(TPersistent)
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


function TgbBanco399.GetNomeBanco : string;
begin
   Result := NomeBanco;
end;

function TgbBanco399.CalcularDigitoNossoNumero(ATitulo: TgbTitulo) : string;
var
   ANossoNumero,
   {ACodigoCedente,
   ADataVencimento, }
   ADigitoNossoNumero{, ADigitoNossoNumero1} : string;
begin
   Result := '0';

   ANossoNumero := Formatar(ATitulo.NossoNumero,10,false,'0');
   ADigitoNossoNumero := Modulo11(ANossoNumero,7);

   if (StrToInt(ADigitoNossoNumero) = 10) or (StrToInt(ADigitoNossoNumero) = 11) then
      ADigitoNossoNumero := '0'
   else
      ADigitoNossoNumero := (ADigitoNossoNumero);

   Result := ADigitoNossoNumero;

   {ANossoNumero := Formatar(ATitulo.NossoNumero,10,false,'0');
   ACodigoCedente := Formatar(ATitulo.Cedente.CodigoCedente,5,false,'0');
   ADigitoNossoNumero1 := Modulo11(ANossoNumero,7);
   if ATitulo.DataVencimento <> 0  then
   begin
      ADataVencimento := FormatDateTime('ddmmyy',ATitulo.DataVencimento);
      ADigitoNossoNumero := Modulo11(IntToStr(StrToInt(ANossoNumero + ADigitoNossoNumero1 + '4') + StrToInt(ACodigoCedente) + StrToInt(ADataVencimento)));
      ADigitoNossoNumero := ADigitoNossoNumero1 + '4' + ADigitoNossoNumero;
   end
   else
   begin
      ADigitoNossoNumero := Modulo11(IntToStr(StrToInt(ANossoNumero + ADigitoNossoNumero1 + '5') + StrToInt(ACodigoCedente)));
      ADigitoNossoNumero := ADigitoNossoNumero1 + '5' + ADigitoNossoNumero;
   end;
   Result := ADigitoNossoNumero;    }
end;

function TgbBanco399.GetCampoLivreCodigoBarra(ATitulo: TgbTitulo) : string;
var
   ADiaDoAno,
   AUltimoDigitoDoAno,
   ADataVencimentoJuliano,
   ANossoNumero,
   ACodigoCedente: string;
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
      ACodigoCedente := Cedente.ContaBancaria.CodigoAgencia + Cedente.ContaBancaria.NumeroConta + Cedente.ContaBancaria.DigitoConta;
      ANossoNumero   := Formatar(NossoNumero,10,false,'0') + DigitoNossoNumero;
      {
       O preenchimento da data de vencimento em formato juliano, somente deve
       ser feito, se e somente se, para tipo identificador "4" e com retorno
       dos 3 d�gitos no arquivo magn�tico e demonstrativo de liquida��o
       (condi��o cadastral).

       Data de vencimento informada atrav�s de m�s juliano.  Exemplo:
       -  001  -  corresponde a 01 de janeiro
       -  042  -  corresponde a 11 de fevereiro

       A �ltima posi��o dessas deve ser utilizada para representar o ano. Exemplo:
       -  0 a 7  -  corresponde: 2000 a 2007
       -  8 e 9  -  corresponde: 1998 e 1999

       Para o tipo identificador 5 ou tipo de identificador 4 sem o retorno dos
       3 d�gitos, a data de vencimento no formato juliano deve ser zerada (0000).
      }
//      ADataVencimentoJuliano := Formatar( IntToStr(1 + Trunc(Now - EncodeDate(StrToInt(FormatDateTime('yyyy',Now)),1,1))) + FormatDateTime('y',Now) ,4,false,'0');
      if DataVencimento = 0 then
         ADataVencimentoJuliano := '0000'
      else
      begin
          ADiaDoAno := Formatar(IntToStr(Trunc(DataVencimento - EncodeDate(StrToInt(FormatDateTime('yyyy',DataVencimento))-1,12,31))),3,false,'0');
          AUltimoDigitoDoAno := FormatDateTime('y',DataVencimento);
          ADataVencimentoJuliano := ADiaDoAno + AUltimoDigitoDoAno;
      end;
   end;

   Result := ANossoNumero  + ACodigoCedente + '00' + '1'; { + ADataVencimentoJuliano + '2'}
end;

procedure TgbBanco399.FormatarBoleto(ATitulo: TgbTitulo; var AAgenciaCodigoCedente, ANossoNumero, ACarteira, AEspecieDocumento: string);
begin
   with ATitulo do
   begin
      AAgenciaCodigoCedente := Cedente.ContaBancaria.CodigoAgencia + '-' + Cedente.ContaBancaria.CodigoAgencia + Cedente.ContaBancaria.NumeroConta + Cedente.ContaBancaria.DigitoConta;
      ANossoNumero := Formatar(NossoNumero,10,false,'0') + '-' + DigitoNossoNumero;
      ACarteira := 'CSB'; //Formatar(Carteira,3);
      AEspecieDocumento := 'PD';
   end;
end;

{$IFNDEF VER120}

function TgbBanco399.GerarRemessa(var ACobranca: TgbCobranca; var Remessa: TStringList) : boolean;
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

      Remessa.Add('01REMESSA01'+Formatar('COBRANCA',15)+'0'+Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.CodigoAgencia,4,false,'0')+'55'+Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.CodigoAgencia,4,false,'0')+Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.NumeroConta,5,false,'0')+Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.DigitoConta,2,false,'0')+'  '+Formatar(Titulos[NumeroRegistro].Cedente.Nome,30,true,' ')+Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Codigo,3,false,'0')+Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Nome,15,true,' ')+FormatDateTime('ddmmyy',DataArquivo)+'01600BPI  '+'COBHSBC'+Formatar('',277)+'000001');

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

         Registro := '1';
         Registro := Registro + Formatar(ACedenteTipoInscricao,2,false,'0');
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.NumeroCPFCGC,14,false,'0');
         Registro := Registro + '0';
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.CodigoAgencia,4,false,'0');
         Registro := Registro + '55';
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.CodigoAgencia,4,false,'0');
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.NumeroConta,5,false,'0');
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.DigitoConta,2,false,'0');
         Registro := Registro + Formatar('',27);
         Registro := Registro + Formatar(Titulos[NumeroRegistro].NossoNumero,10,false,'0');
         Registro := Registro + Formatar(Titulos[NumeroRegistro].DigitoNossoNumero,1,false,'0');
         //Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataDesconto);
         //Registro := Registro + FormatCurr('00000000000',Titulos[NumeroRegistro].ValorDesconto * 100);
         Registro := Registro + '000000';      // Desconto
         Registro := Registro + '00000000000'; // Desconto
         Registro := Registro + '000000';      // Desconto
         Registro := Registro + '00000000000'; // Desconto
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Carteira,1,false,'0');
         // Tipo de ocorr�ncia
         case Titulos[NumeroRegistro].TipoOcorrencia of
            toRemessaRegistrar : ATipoOcorrencia := '01';
            toRemessaBaixar : ATipoOcorrencia := '02';
            toRemessaConcederAbatimento : ATipoOcorrencia := '04';
            toRemessaCancelarAbatimento : ATipoOcorrencia := '05';
            toRemessaAlterarVencimento : ATipoOcorrencia := '06';
            toRemessaAlterarNumeroControle : ATipoOcorrencia := '08';
            toRemessaProtestar : ATipoOcorrencia := '09';
            toRemessaCancelarInstrucaoProtesto : ATipoOcorrencia := '10';
            toRemessaDispensarJuros : ATipoOcorrencia := '11'
         else
            Raise Exception.CreateFmt('Ocorr�ncia inv�lida em remessa - Nosso n�mero: %s / Seu n�mero: %s',[Titulos[NumeroRegistro].NossoNumero,Titulos[NumeroRegistro].SeuNumero]);
         end;
         Registro := Registro + ATipoOcorrencia;
         Registro := Registro + Formatar(Titulos[NumeroRegistro].SeuNumero,10);
         Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataVencimento);
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorDocumento * 100);
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Cedente.ContaBancaria.Banco.Codigo,3,false,'0');
         Registro := Registro + Formatar('',5,false,'0');
         // Esp�cie do t�tulo
         case Titulos[NumeroRegistro].EspecieDocumento of
            edOutros : AEspecieDocumento := '98';
            edDuplicataMercantil : AEspecieDocumento := '01';
            edDuplicataServico : AEspecieDocumento := '10';
            edNotaPromissoria : AEspecieDocumento := '02';
            edNotaSeguro : AEspecieDocumento := '03';
            edRecibo : AEspecieDocumento := '05'
         else
            AEspecieDocumento := '99';
         end;
         Registro := Registro + AEspecieDocumento;
         {Identifica��o de aceite do t�tulo}
         case Titulos[NumeroRegistro].AceiteDocumento of
            adSim : Registro := Registro + 'A';
            adNao : Registro := Registro + 'N';
         end;
         Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataProcessamento);
         // Instru��es
         if (Titulos[NumeroRegistro].DataProtesto <> null)  and (Titulos[NumeroRegistro].DataProtesto > Titulos[NumeroRegistro].DataVencimento) then
            Registro := Registro + '77' // Protestar ap�s 5� dia �til
         else
            Registro := Registro + '00'; // N�o protestar
         Registro := Registro + '00';    // 2� Instru��o
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorMoraJuros * 100);
         //Registro := Registro + FormatDateTime('ddmmyy',Titulos[NumeroRegistro].DataDesconto);
         Registro := Registro + '      ';
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorDesconto * 100);
         Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorIOF * 100);
         if Titulos[NumeroRegistro].ValorAbatimento > 0 then
            Registro := Registro + FormatCurr('0000000000000',Titulos[NumeroRegistro].ValorAbatimento * 100)
         else
            Registro := Registro + Formatar(' ',13);
         Registro := Registro + Formatar(ASacadoTipoInscricao,2,false,'0');
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.NumeroCPFCGC,14,true,'0');
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Nome,40);
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Rua+' '+Titulos[NumeroRegistro].Sacado.Endereco.Numero+' '+Titulos[NumeroRegistro].Sacado.Endereco.Complemento,38);
         Registro := Registro + Formatar(' ',2);
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Bairro,12);
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.CEP,8,true,'0');
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Cidade,15);
         Registro := Registro + Formatar(Titulos[NumeroRegistro].Sacado.Endereco.Estado,2);
         Registro := Registro + Formatar(' ',39);
         Registro := Registro + 'E';

         if (Titulos[NumeroRegistro].DataProtesto <> null)  and (Titulos[NumeroRegistro].DataProtesto > Titulos[NumeroRegistro].DataVencimento) then
            Registro := Registro + '07' // Qtde dias para protestar
         else
            Registro := Registro + '  ';

         Registro := Registro + '9';
         Registro := Registro + Formatar(IntToStr(NumeroRegistro+2),6,false,'0');
         Remessa.Add(Registro);
         NumeroRegistro := NumeroRegistro + 1;
      end;

      { GERAR REGISTRO TRAILER DA REMESSA }

      Remessa.Add('9'+Formatar('',393,true,' ')+Formatar(IntToStr(NumeroRegistro+2),6,false,'0'));
   end;

   Result := TRUE;
end;

function TgbBanco399.LerRetorno(var ACobranca: TgbCobranca; Retorno: TStringList) : boolean;
var
   ACodigoBanco,
   ANomeCedente,
   ATipoInscricao : string;
   NumeroRegistro : integer;
   ATitulo : TgbTitulo;
begin
   NumeroRegistro := 0;
   ATitulo := TgbTitulo.Create(nil);
   try
      with ACobranca do
      begin
         Titulos.Clear; {Zera o conjunto de t�tulos, antes de incluir os t�tulos do arquivo retorno}

         if Retorno.Count <= 0 then
            Raise Exception.Create('O retorno est� vazio. N�o h� dados para processar');

         {Ver se o arquivo � mesmo RETORNO DE COBRAN�A}
         if Copy(Retorno.Strings[NumeroRegistro],1,19) <> '02RETORNO01COBRANCA' then
            Raise Exception.Create(NomeArquivo+' n�o � um arquivo de retorno de cobran�a');

         { L� registro HEADER}
         ACodigoBanco := Copy(Retorno.Strings[NumeroRegistro],77,3);
         if ACodigoBanco <> CodigoBanco then
            Raise Exception.CreateFmt('Este n�o � um retorno de cobran�a do banco %s - %s',[CodigoBanco,NomeBanco]);

         ANomeCedente := Trim(Copy(Retorno.Strings[NumeroRegistro],47,30));
         if StrToInt(Copy(Retorno.Strings[NumeroRegistro],99,2)) <= 69 then
            DataArquivo := EncodeDate(StrToInt('20'+Copy(Retorno.Strings[NumeroRegistro],99,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],97,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],95,2)))
         else
            DataArquivo := EncodeDate(StrToInt('19'+Copy(Retorno.Strings[NumeroRegistro],99,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],97,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],95,2)));

         NumeroArquivo := StrToInt(Trim(Copy(Retorno.Strings[NumeroRegistro],389,5)));
         if Formatar(Copy(Retorno.Strings[NumeroRegistro],120,6),6,false,'0') <> '000000' then
            if StrToInt(Copy(Retorno.Strings[NumeroRegistro],124,2)) <= 69 then
               ATitulo.DataCredito := EncodeDate(StrToInt('20'+Copy(Retorno.Strings[NumeroRegistro],124,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],122,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],120,2)))
            else
               ATitulo.DataCredito := EncodeDate(StrToInt('19'+Copy(Retorno.Strings[NumeroRegistro],124,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],122,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],120,2)));

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
                  ContaBancaria.Banco.Codigo := ACodigoBanco;
                  ContaBancaria.CodigoAgencia := Copy(Retorno.Strings[NumeroRegistro],19,4);
                  Nome := ANomeCedente;
               end; {with ACedente}
               NumeroDocumento := Copy(Retorno.Strings[NumeroRegistro],117,10);

               {Tipo de ocorr�ncia}
               OcorrenciaOriginal := Copy(Retorno.Strings[NumeroRegistro],109,2);
               case StrToInt(OcorrenciaOriginal) of
                  02: TipoOcorrencia := toRetornoRegistroConfirmado;
                  03: TipoOcorrencia := toRetornoRegistroRecusado;
                  06: TipoOcorrencia := toRetornoLiquidado;
                  07: TipoOcorrencia := toRetornoLiquidadoPorConta;
                  09: TipoOcorrencia := toRetornoBaixado;
                  10: TipoOcorrencia := toRetornoBaixado;
                  11: TipoOcorrencia := toRetornoTituloEmSer;
                  12: TipoOcorrencia := toRetornoAbatimentoConcedido;
                  13: TipoOcorrencia := toRetornoAbatimentoCancelado;
                  14: TipoOcorrencia := toRetornoVencimentoAlterado;
                  15: TipoOcorrencia := toRetornoLiquidadoEmCartorio;
                  16: TipoOcorrencia := toRetornoLiquidado;
                  17: TipoOcorrencia := toRetornoEncaminhadoACartorio;
                  18: TipoOcorrencia := toRetornoRecebimentoInstrucaoProtestar;
                  21: TipoOcorrencia := toRetornoDadosAlterados;
                  22: TipoOcorrencia := toRetornoProtestado;
                  23: TipoOcorrencia := toRetornoProtestoSustado;
                  27: TipoOcorrencia := toRetornoDadosAlterados;
                  31: TipoOcorrencia := toRetornoLiquidado;
                  32: TipoOcorrencia := toRetornoLiquidadoEmCartorio;
                  33: TipoOcorrencia := toRetornoLiquidadoPorConta;
                  36: TipoOcorrencia := toRetornoLiquidado;
                  37: TipoOcorrencia := toRetornoBaixaPorProtesto;
                  38: TipoOcorrencia := toRetornoLiquidadoSemRegistro;
                  39: TipoOcorrencia := toRetornoLiquidadoSemRegistro;
                  49: TipoOcorrencia := toRetornoVencimentoAlterado;
                  69: TipoOcorrencia := toRetornoDespesasProtesto;
               else
                  TipoOcorrencia := toRetornoOutrasOcorrencias;
               end; {case StrToInt(ATipoOcorrencia)}

               if (TipoOcorrencia = toRetornoRegistroRecusado) or (TipoOcorrencia = toRetornoComandoRecusado) then
               begin
                  MotivoRejeicaoComando := Copy(Retorno.Strings[NumeroRegistro],302,2);
                  case StrToInt(MotivoRejeicaoComando) of
                      00 : MotivoRejeicaoComando := '00 - Ocorrencia aceita';
                      01 : MotivoRejeicaoComando := '01 - Valor do desconto n�o informado/inv�lido.';
                      02 : MotivoRejeicaoComando := '02 - Inexist�ncia de ag�ncia do HSBC na pra�a do sacado.';
                      03 : MotivoRejeicaoComando := '03 - CEP do sacado incorreto ou inv�lido.';
                      04 : MotivoRejeicaoComando := '04 - Cadastro do cedente n�o aceita banco correspondente.';
                      05 : MotivoRejeicaoComando := '05 - Tipo de moeda inv�lido.';
                      06 : MotivoRejeicaoComando := '06 - Prazo de protesto indefinido/inv�lido.';
                      07 : MotivoRejeicaoComando := '07 - Data do vencimento inv�lida.';
                      08 : MotivoRejeicaoComando := '08 - Nosso  n�mero(n�mero  banc�rio)  utilizado  n�o  possui vincula��o com a conta cobran�a.';
                      09 : MotivoRejeicaoComando := '09 - Taxa mensal de mora acima do permitido (170%).';
                      10 : MotivoRejeicaoComando := '10 - Taxa de multa acima do permitido (10% ao m�s).';
                      11 : MotivoRejeicaoComando := '11 - Data limite de desconto inv�lida.';
                      12 : MotivoRejeicaoComando := '12 - C�digo da ag�ncia deposit�ria do HSBC informado � inv�lido.';
                      13 : MotivoRejeicaoComando := '13 - Taxa de multa inv�lida.';
                      14 : MotivoRejeicaoComando := '14 - Valor di�rio da multa n�o informado.';
                      15 : MotivoRejeicaoComando := '15 - Quantidade de dias ap�s vencimento para incid�ncia da multa n�o informada.';
                      16 : MotivoRejeicaoComando := '16 - Outras irregularidades.';
                      17 : MotivoRejeicaoComando := '17 - Data de in�cio da multa inv�lida.';
                      18 : MotivoRejeicaoComando := '18 - Nosso n�mero (n�mero banc�rio) j�  existente  para  outro t�tulo.';
                      19 : MotivoRejeicaoComando := '19 - Valor do t�tulo inv�lido.';
                      20 : MotivoRejeicaoComando := '20 - Aus�ncia do nome do sacado.';
                      21 : MotivoRejeicaoComando := '21 - T�tulo sem border�.';
                      22 : MotivoRejeicaoComando := '22 - N�mero da conta do cedente n�o cadastrado.';
                      23 : MotivoRejeicaoComando := '23 - Instru��o n�o permitida para t�tulo em garantia de opera��o.';
                      24 : MotivoRejeicaoComando := '24 - Condi��o  de  desconto  n�o  permitida  para  titulo  em  garantia de Opera��o.';
                      25 : MotivoRejeicaoComando := '25 - Utilizada mais de uma instru��o de multa.';
                      26 : MotivoRejeicaoComando := '26 - Aus�ncia do endere�o do sacado.';
                      27 : MotivoRejeicaoComando := '27 - Aus�ncia do CEP do sacado.';
                      28 : MotivoRejeicaoComando := '28 - Aus�ncia do CPF/CNPJ do sacado em t�tulo com instru��o de protesto.';
                      29 : MotivoRejeicaoComando := '29 - Ag�ncia cedente informada inv�lida.';
                      30 : MotivoRejeicaoComando := '30 - N�mero da conta do cedente inv�lido.';
                  else
                     MotivoRejeicaoComando := MotivoRejeicaoComando + ' - Outros motivos'
                  end; {case MotivoRejeicaoComando of}
               end; {if TipoOcorrencia...}

               if StrToInt(Copy(Retorno.Strings[NumeroRegistro],115,2)) <= 69 then
                  DataOcorrencia := EncodeDate(StrToInt('20'+Copy(Retorno.Strings[NumeroRegistro],115,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],113,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],111,2)))
               else
                  DataOcorrencia := EncodeDate(StrToInt('19'+Copy(Retorno.Strings[NumeroRegistro],115,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],113,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],111,2)));

               if StrToInt(Copy(Retorno.Strings[NumeroRegistro],115,2)) <= 69 then
                  DataRecebimento := EncodeDate(StrToInt('20'+Copy(Retorno.Strings[NumeroRegistro],115,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],113,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],111,2)))
               else
                  DataRecebimento := EncodeDate(StrToInt('19'+Copy(Retorno.Strings[NumeroRegistro],115,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],113,2)),StrToInt(Copy(Retorno.Strings[NumeroRegistro],111,2)));

               ValorDocumento  := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],153,13))/100;
               ValorAbatimento := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],228,13))/100;
               ValorDesconto   := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],241,13))/100;
               ValorMoraJuros  := StrToFloat(Copy(Retorno.Strings[NumeroRegistro],267,13))/100;

               {Dados que variam de acordo com o banco}

               {Nosso n�mero SEM D�GITO}
               NossoNumero := Copy(Retorno.Strings[NumeroRegistro],63,10);
               Carteira    := Copy(Retorno.Strings[NumeroRegistro],108,1);
               Cedente.ContaBancaria.CodigoAgencia := Copy(Retorno.Strings[NumeroRegistro],25,4);
               Cedente.ContaBancaria.NumeroConta   := Copy(Retorno.Strings[NumeroRegistro],29,5);
               Cedente.ContaBancaria.DigitoConta   := Copy(Retorno.Strings[NumeroRegistro],34,2);
               ValorDespesaCobranca := StrToFloat(Trim(Copy(Retorno.Strings[NumeroRegistro],176,13)))/100;
            end; {with ATitulo}
            {Insere o t�tulo}
            Titulos.Add(ATitulo);
         end;
      end;

      ATitulo.Free;
      Result := TRUE
   except
      ATitulo.Free;
      Result := FALSE;
      Raise; {Propaga o erro}
   end;
end;

{$ENDIF}

initialization
RegisterClass(TgbBanco399);

end.
