using System;
using System.Web.UI;
using Microsoft.VisualBasic;

[assembly: WebResource("BoletoNet.Imagens.070.jpg", "image/jpg")]
namespace BoletoNet
{
    /// <author>  
    /// Eduardo Frare
    /// Stiven 
    /// Diogo
    /// Miamoto
    /// </author>    


    /// <summary>
    /// Classe referente ao banco Banco_BRB
    /// </summary>
    internal class Banco_BRB : AbstractBanco, IBanco
    {
        private int _dacBoleto = 0;

        internal Banco_BRB()
        {
            this.Codigo = 70;
            this.Digito = 1;
            this.Nome = "Banco_BRB";
        }

        #region IBanco Members

        public override void FormataCodigoBarra(Boleto boleto)
        {
            // Código de Barras
            //banco & moeda & fator & valor & carteira & nossonumero & dac_nossonumero & agencia & conta & dac_conta & "000"

            string banco = Utils.FormatCode(Codigo.ToString(), 3);
            int moeda = boleto.Moeda;
            //string digito = "";
            string valorBoleto = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
            valorBoleto = Utils.FormatCode(valorBoleto, 10);

            string fatorVencimento = FatorVencimento(boleto).ToString();
            string chave = boleto.CodigoBarra.Chave;


            boleto.CodigoBarra.Codigo =
                string.Format("{0}{1}{2}{3}{4}", banco, moeda,fatorVencimento,
                              valorBoleto, boleto.CodigoBarra.Chave);


            _dacBoleto = Banco_BRB.Mod11_CodigoBarra(boleto.CodigoBarra.Codigo, 9);

            boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + _dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
        }

        public override void FormataLinhaDigitavel(Boleto boleto)
        {
            string BBB = Utils.FormatCode(Codigo.ToString(), 3);
            int M = boleto.Moeda;
            string CCCCC1 = boleto.CodigoBarra.Chave.Substring(0, 5);
            int D1 = 0;

            string CCCCCCCCCC2 = boleto.CodigoBarra.Chave.Substring(5, 10);
            int D2 = 0;

            string CCCCCCCCCC3 = boleto.CodigoBarra.Chave.Substring(14, 10);
            int D3 = 0;

            D1 = Mod10(BBB + M + CCCCC1);
            string Grupo1 = string.Format("{0}.{1}{2} ", BBB + M + CCCCC1.Substring(0, 1), CCCCC1.Substring(1, 4), D1);

            D2 = Mod10(CCCCCCCCCC2);
            string Grupo2 = string.Format("{0}.{1}{2} ", CCCCCCCCCC2.Substring(0, 5), CCCCCCCCCC2.Substring(5, 5), D2);

            D3 = Mod10(CCCCCCCCCC3);
            string Grupo3 = string.Format("{0}.{1}{2} ", CCCCCCCCCC3.Substring(0, 5), CCCCCCCCCC3.Substring(5, 5), D3);

            string Grupo4 = string.Format("{0} {1}{2}", _dacBoleto, FatorVencimento(boleto).ToString(), Utils.FormatCode(boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", ""), 10));

            boleto.CodigoBarra.LinhaDigitavel = Grupo1 + Grupo2 + Grupo3 + Grupo4;
        }

        public override void FormataNossoNumero(Boleto boleto)
        {
            //boleto.NossoNumero = string.Format("{0}{1}{2}", boleto.Categoria, boleto.NossoNumero, Utils.FormatCode(Codigo.ToString(), 3) + boleto.CodigoBarra.Chave.Substring(22, 2));
            boleto.NossoNumero = boleto.NossoNumero.ToString();
        }

        public override void FormataNumeroDocumento(Boleto boleto)
        {
            boleto.NumeroDocumento = string.Format("{0}", boleto.NumeroDocumento);
        }

        public override void ValidaBoleto(Boleto boleto)
        {
            //Verifica se o nosso número é válido
            if (Utils.ToInt64(boleto.NossoNumero) == 0)
                throw new NotImplementedException("Nosso número inválido");

            //Verifica se o tamanho para o NossoNumero são 12 dígitos
            if (Convert.ToInt64(boleto.NossoNumero).ToString().Length > 12)
                throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 6 números.");
            else if (Convert.ToInt64(boleto.NossoNumero).ToString().Length < 12)
                boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 12);

            // ERA O UTILIZADO ANTES
            //if (Convert.ToInt32(boleto.NossoNumero).ToString().Length > 6)
            //    throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 6 números.");
            //else if (Convert.ToInt32(boleto.NossoNumero).ToString().Length < 6)
            //    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 6);

            //if (boleto.Carteira != "COB")
            //    throw new NotImplementedException("Carteira não implementada. Utilize a carteira COB.");

            //Atribui o nome do banco ao local de pagamento
            boleto.LocalPagamento += Nome + "";

            //Verifica se data do processamento é valida
            if (boleto.DataProcessamento.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.DataProcessamento = DateTime.Now;

            //Verifica se data do documento é valida
            if (boleto.DataDocumento.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.DataDocumento = DateTime.Now;

            FormataChave(boleto);
            FormataCodigoBarra(boleto);
            FormataLinhaDigitavel(boleto);
            FormataNossoNumero(boleto);
            FormataNumeroDocumento(boleto);
        }

        #endregion

        public void FormataChave(Boleto boleto)
        {
            string zeros = "000";
            string agencia = boleto.Cedente.ContaBancaria.Agencia;
            string conta = boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta;
            int categoria = 1;
            boleto.Categoria = categoria;
            string nossonumero = boleto.NossoNumero;
            string banco = Utils.FormatCode(Codigo.ToString(), 3);
            
            //Mod10 dentro da classe Banco_BRB pelas particularidades que ela tem.
            int d1 = Banco_BRB.Mod10(zeros + agencia + conta + categoria + nossonumero + banco);
            int d2 = Banco_BRB.Mod11_NossoNumero(zeros + agencia + conta + categoria + nossonumero + banco + d1, 7);

            if (d2 > 10)
            {
                d1 += 1;
                d2 -= 20;
            }

            boleto.CodigoBarra.Chave = zeros + agencia + conta + categoria + nossonumero + banco + d1 + d2;
        }

        internal static int Mod11_CodigoBarra(string value, int Base)
        {
            int Digito, Soma = 0, Peso = 2;

            for (int i = value.Length; i > 0; i--)
            {
                Soma = Soma + (Convert.ToInt32(Strings.Mid(value, i, 1)) * Peso);
                if (Peso == Base)
                    Peso = 2;
                else
                    Peso = Peso + 1;
            }

            if (((Soma % 11) == 0) || ((Soma % 11) == 10) || ((Soma % 11) == 1))
            {
                Digito = 1;
            }
            else
            {
                Digito = 11 - (Soma % 11);
            }
            
            return Digito;
        }

        internal static int Mod11_NossoNumero(string value, int Base)
        {

            int Digito, Soma = 0, Peso = 2;

            for (int i = value.Length; i > 0; i--)
            {
                Soma = Soma + (Convert.ToInt32(Strings.Mid(value, i, 1)) * Peso);
                if (Peso == Base)
                    Peso = 2;
                else
                    Peso = Peso + 1;
            }

             if ((Soma % 11) > 1)
            {
                Digito = 11 - (Soma % 11);
            }
            else if ((Soma % 11) == 1)
            {
                int d1 = Utils.ToInt32(Strings.Mid(value, value.Length, value.Length - 1));

                d1 += 1;

                if (d1 == 10)
                    d1 = 0;

                Digito = Banco_BRB.Mod11_NossoNumero(Strings.Mid(value, 1, value.Length - 1) + d1, 7);
                Digito += 20;               
            }
            else 
            {
                Digito = (Soma % 11);
            }

            return Digito;
  
        }


        internal new static int Mod10(string seq)
        {

            int Digito, Soma = 0, Peso = 2, res;

            for (int i = seq.Length; i > 0; i--)
            {
                res = (Convert.ToInt32(Strings.Mid(seq, i, 1)) * Peso);

                if (res > 9)
                    res = (res - 9);

                Soma += res;

                if (Peso == 2)
                    Peso = 1;
                else
                    Peso = Peso + 1;
            }

            Digito = ((10 - (Soma % 10)) % 10);

            return Digito;
        }

        #region Remessa
        /// <summary>
        /// HEADER do arquivo CNAB
        /// Gera o HEADER do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        public override string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa, int numeroRegistros)
        {
            try
            {
                string _header = " ";

                base.GerarHeaderRemessa(cedente, tipoArquivo, numeroArquivoRemessa, numeroRegistros);

                switch (tipoArquivo)
                {

                    case TipoArquivo.CNAB240:
                        _header = GerarHeaderRemessaCNAB240();
                        break;
                    case TipoArquivo.CNAB400:
                        _header = GerarHeaderRemessaCNAB400(cedente, numeroArquivoRemessa, numeroRegistros);
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _header;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do HEADER do arquivo de REMESSA.", ex);
            }
        }

        public string GerarHeaderRemessaCNAB240()
        {
            throw new NotImplementedException("Função não implementada.");
        }

        public string GerarHeaderRemessaCNAB400(Cedente cedente, int numeroArquivoRemessa, int numeroRegistros)
        {
            try
            {
                string _header;

                _header = "DCB0010750000000000";
                _header += DateTime.Now.ToString("yyyyMMdd");
                _header += DateTime.Now.ToString("HHmmss");
                // Descobrir como vou pegar o número de registros: header + registros
                //_header += "000001";
                _header += (numeroRegistros+1).ToString().PadLeft(6,'0');
                _header = Utils.SubstituiCaracteresEspeciais(_header);

                return _header;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
            }
        }

        public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _detalhe = " ";

                base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _detalhe = GerarDetalheRemessaCNAB240();
                        break;
                    case TipoArquivo.CNAB400:
                        _detalhe = GerarDetalheRemessaCNAB400(boleto, numeroRegistro, tipoArquivo);
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _detalhe;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", ex);
            }
        }

        public string GerarDetalheRemessaCNAB240()
        {
            throw new NotImplementedException("Função não implementada.");
        }

        public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);

                string _detalhe;
                //detalhe                           (tamanho,tipo) A= Alfanumerico, N= Numerico
                _detalhe = "01"; //Identificação do Registro

                //Identificação da Empresa Cedente no Banco (17, A)
                _detalhe += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 3, 3, '0', 0, true, true, true); //N da agencia(3)
                _detalhe += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 7, 7, '0', 0, true, true, true); //Conta Corrente(7)

                //Nº Inscrição do Sacado (14, N)
                string cpf_Cnpj = boleto.Sacado.CPFCNPJ.Replace("/", "").Replace(".", "").Replace("-", "");
                _detalhe += Utils.FitStringLength(cpf_Cnpj.TrimStart(' '), 14, 14, ' ', 0, true, true, false);

                //Nome do Sacado (35, A)
                _detalhe += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(' '), 35, 35, ' ', 0, true, true, false).ToUpper();

                //Endereço Completo (35, A)
                _detalhe += Utils.FitStringLength(boleto.Sacado.Endereco.End.TrimStart(' '), 35, 35, ' ', 0, true, true, false).ToUpper();
                //Endereço Cidade (15, A)
                _detalhe += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade.TrimStart(' '), 15, 15, ' ', 0, true, true, false).ToUpper();
                //Endereço UF (2, A)
                _detalhe += Utils.FitStringLength(boleto.Sacado.Endereco.UF.TrimStart(' '), 2, 2, ' ', 0, true, true, false).ToUpper();
                //Endereço CEP (8, A)
                _detalhe += Utils.FitStringLength(boleto.Sacado.Endereco.CEP, 8, 8, ' ', 0, true, true, false).ToUpper(); ;
                if (boleto.Sacado.CPFCNPJ.Length <= 11)
                    _detalhe += "1";  // Física
                else
                    _detalhe += "2"; // Jurídica

                //Identificação do Título na Empresa (11, A)
                _detalhe += Utils.FitStringLength(boleto.NumeroDocumento, 13, 13, '0', 0, true, true, true); //Numero (13)

                //Código Categoria Cobrança 1 - Sem Registro, 2 - Com Registro, 3 - Com Registro = Impressão pelo BRB
                //Verificar depois qual código é o correto
                _detalhe += "1";

                _detalhe += boleto.DataProcessamento.ToString("ddMMyyyy"); //Data da emissão do Título (8, N) DDMMAAAA

                //Tipo de Documento 25 - Recibo
                _detalhe += "25";

                //Código da natureza
                _detalhe += "0";

                //Código da condição pagto 0 - no vencimento, 1 - à vista e 2 - contra apresentação
                _detalhe += "1";

                //Código da moeda
                _detalhe += "02";

                //Número banco
                _detalhe += "070";

                //Agência Cobradora
                _detalhe += "0000";

                //Praça de Cobrança
                _detalhe += new string(' ', 30);

                _detalhe += boleto.DataVencimento.ToString("ddMMyyyy"); //Data de vencimento do Título (8, N) DDMMAAAA

                //Valor Título
                _detalhe += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 14, 14, '0', 0, true, true, true);

                //Formatação C999999BBBDD => C: Categoria de Cobrança(1ou2), 999999: último sequencial do cedente na categoria de cobrança  + 1
                //BBB: 070 e DD: de acordo com padrões do BRB
                int d1 = Mod10("1" + Utils.FitStringLength(boleto.NossoNumero, 6, 6, '0', 0, true, true, true) + "070");
                int d2 = Mod11("1" + Utils.FitStringLength(boleto.NossoNumero, 6, 6, '0', 0, true, true, true) + "070" + d1.ToString(),7);
                _detalhe += "1" + Utils.FitStringLength(boleto.NossoNumero, 6, 6, '0', 0, true, true, true) + "070" + d1.ToString() + d2.ToString();

                //Juros: Tipo + Valor
                if (boleto.JurosMora > 0)
                {
                    _detalhe += "50";
                    _detalhe += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 14, 14, '0', 0, true, true, true);
                }
                else
                {
                    _detalhe += "0000000000000000";
                }

                //Valor abatimento
                _detalhe += Utils.FitStringLength(boleto.Abatimento.ToString("0.00").Replace(",", ""), 14, 14, '0', 0, true, true, true);

                //Desconto: Tipo + Data limite + Valor
                if (boleto.ValorDesconto > 0)
                {
                    _detalhe += "53";
                    _detalhe += boleto.DataDesconto.ToString("ddMMyyyy"); //Data limite de desconto do Título (8, N) DDMMAAAA
                    _detalhe += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 14, 14, '0', 0, true, true, true);
                }
                else
                {
                    _detalhe += "000000000000000000000000";
                }

                //Código 1ª Instrução: 00(Sem instrução) (2)
                _detalhe += "00";
                //Prazo 1ª Instrução: 00(Sem instrução) (2)
                _detalhe += "00";

                //Código 2ª Instrução: 00(Sem instrução) (2)
                _detalhe += "00";
                //Prazo 2ª Instrução: 00(Sem instrução) (2)
                _detalhe += "00";

                //Taxa ref, a uma das das inst.
                _detalhe += "00000";

                //Emitente do título
                _detalhe += new string(' ', 40);

                //Mensagem Livre
                _detalhe += new string(' ', 40);

                //Branco
                _detalhe += new string(' ', 32);                      

                _detalhe = Utils.SubstituiCaracteresEspeciais(_detalhe);

                return _detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
            }
        }

        #endregion

        public override DetalheRetorno LerDetalheRetornoCNAB400(string registro)
        {
            try
            {
                DetalheRetorno detalhe = new DetalheRetorno(registro);

                //Tipo de Identificação do registro
                detalhe.IdentificacaoDoRegistro = Utils.ToInt32(registro.Substring(0, 1));

                //Tipo de inscrição
                detalhe.TipoInscricao = Utils.ToInt32(registro.Substring(1, 2));

                //CGC ou CPF
                detalhe.CgcCpf = registro.Substring(3, 14);

                //Conta Corrente
                detalhe.ContaCorrente = Utils.ToInt32(registro.Substring(20, 17));

                //Nosso Número
                detalhe.NossoNumero = registro.Substring(70, 12);

                //Seu Número
                detalhe.SeuNumero = registro.Substring(92, 13);

                //Instrução
                detalhe.Instrucao = Utils.ToInt32(registro.Substring(108, 2));

                //Número do documento
                detalhe.NumeroDocumento = registro.Substring(128, 12);

                //Código do Raterio
                detalhe.CodigoRateio = Utils.ToInt32(registro.Substring(140, 4));

                //Data Ocorrência no Banco
                int dataOcorrencia = Utils.ToInt32(registro.Substring(110, 8));
                detalhe.DataOcorrencia = Utils.ToDateTime(dataOcorrencia.ToString("##-##-####"));

                //Data Vencimento do Título
                int dataVencimento = Utils.ToInt32(registro.Substring(148, 8));
                detalhe.DataVencimento = Utils.ToDateTime(dataVencimento.ToString("##-##-####"));

                //Valor do Título
                decimal valorTitulo = Convert.ToInt64(registro.Substring(156, 13));
                detalhe.ValorTitulo = valorTitulo / 100;

                //Banco Cobrador
                detalhe.BancoCobrador = Utils.ToInt32(registro.Substring(169, 3));

                //Agência Cobradora
                detalhe.AgenciaCobradora = Utils.ToInt32(registro.Substring(172, 5));

                //Espécie Título
                detalhe.EspecieTitulo = registro.Substring(177, 2);

                //Despesas de cobrança para os Códigos de Ocorrência (Valor Despesa)
                decimal despeasaDeCobranca = Convert.ToUInt64(registro.Substring(179, 13));
                detalhe.DespeasaDeCobranca = despeasaDeCobranca / 100;

                //Outras despesas Custas de Protesto (Valor Outras Despesas)
                decimal outrasDespesas = Convert.ToUInt64(registro.Substring(192, 13));
                detalhe.OutrasDespesas = outrasDespesas / 100;

                //Juros Mora
                decimal juros = Convert.ToUInt64(registro.Substring(205, 13));
                detalhe.Juros = juros / 100;
                detalhe.JurosMora = juros / 100;

                // IOF
                decimal iof = Convert.ToUInt64(registro.Substring(218, 13));
                detalhe.IOF = iof / 100;

                //Abatimento Concedido sobre o Título (Valor Abatimento Concedido)
                decimal abatimento = Convert.ToUInt64(registro.Substring(231, 13));
                detalhe.Abatimentos = abatimento / 100;

                //Desconto Concedido (Valor Desconto Concedido)
                decimal desconto = Convert.ToUInt64(registro.Substring(244, 13));
                detalhe.Descontos = desconto / 100;

                //Valor Pago
                decimal valorPago = Convert.ToUInt64(registro.Substring(257, 13));
                detalhe.ValorPago = valorPago / 100;

                //Outros Débitos
                decimal outrosDebitos = Convert.ToUInt64(registro.Substring(270, 13));
                detalhe.OutrosDebitos = outrosDebitos / 100;

                //Outros Créditos
                decimal outrosCreditos = Convert.ToUInt64(registro.Substring(283, 13));
                detalhe.OutrosCreditos = outrosCreditos / 100;

                // Data de Liquidação
                int dataLiquidacao = Utils.ToInt32(registro.Substring(299, 8));
                detalhe.DataLiquidacao = Utils.ToDateTime(dataLiquidacao.ToString("##-##-####"));
                detalhe.DataCredito = Utils.ToDateTime(dataLiquidacao.ToString("##-##-####"));

                //Motivo de Rejeição
                detalhe.MotivosRejeicao = registro.Substring(364, 30);

                //Motivo de Rejeição
                detalhe.Sequencial = Utils.ToInt32(registro.Substring(394, 6));
                detalhe.NumeroSequencial = Utils.ToInt32(registro.Substring(394, 6));

                return detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
            }
        }
    }
}
