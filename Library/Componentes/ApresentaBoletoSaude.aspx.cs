//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoletoNet;
using Microsoft.VisualBasic;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Componentes
{
    public partial class ApresentaBoletoSaude : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (InformacoesBoletoBancario boleto in (List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente])
            {
                string codigoBarras = string.Empty;

                try
                {
                    codigoBarras = GeraBoletoBancario(boleto);

                    if (codigoBarras != null)
                    {
                        //--------------------> Faz a atualização do Nosso Número e do Código de Barras do Título
                        AtualizaNossoNumeroCodigoBarras(boleto, codigoBarras);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message + "<br/><br/>"); // + msg);
                }
            }

            //--------> Faz a remoção dos boletos da sessão
            Session.Remove(SessoesHttp.BoletoBancarioCorrente);
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Gera o Boleto Bancário
        /// </summary>
        /// <param name="boleto">Entidade InformacoesBoletoBancario</param>
        /// <returns></returns>
        private string GeraBoletoBancario(InformacoesBoletoBancario boleto)
        {
            //--------> Faz a remoção do Dígito da Agência
            string DigAgencia = "0";
            string Agencia = "0";
            if (boleto.Agencia.IndexOf("-") > -1)
            {
                int s = boleto.Agencia.IndexOf("-") + 1;
                int tam = Strings.Len(boleto.Agencia);
                DigAgencia = Strings.Right(boleto.Agencia, tam - s);
                int dif = tam - (s - 1);
                //------------> Inclui o traço.
                Agencia = Strings.Left(boleto.Agencia, tam - dif);
            }

            //--------> Faz a remoção do Dígito da Conta
            string DigConta = "";
            int Conta = 0;
            if (boleto.Conta.IndexOf("-") > -1)
            {
                int s2 = boleto.Conta.IndexOf("-") + 1;
                int tam2 = Strings.Len(boleto.Conta);
                DigConta = Strings.Right(boleto.Conta, tam2 - s2);
                int dif2 = tam2 - (s2 - 1);
                //------------> Inclui o traço.
                Conta = Convert.ToInt32(Strings.Left(boleto.Conta, tam2 - dif2));
            }

            //--------> Faz a remoção do Dígito da Cedente
            if (boleto.CodigoCedente.IndexOf("-") > -1)
            {
                int s3 = boleto.CodigoCedente.IndexOf("-") + 1;
                int tam3 = Strings.Len(boleto.CodigoCedente);
                int dif3 = tam3 - (s3 - 1);
                //------------> Inclui o traço.
                boleto.CodigoCedente = Strings.Left(boleto.CodigoCedente, tam3 - dif3);
            }

            //--------> Faz a validação do Banco
            switch (boleto.CodigoBanco)
            {
                case "001":
                    #region Banco do Brasil
                    //Banco do Brasil.

                    //Carteira com 2 caracteres.
                    //If Len(boleto.Carteira) <> 2 Then
                    //Response.Write("A Carteira deve conter 2 dígitos."]
                    //Exit Sub
                    //End If

                    //Agência com 4 caracteres.
                    if (Strings.Len(Agencia) > 4)
                    {
                        Response.Write("A Agência deve conter até 4 dígitos.");
                        return null;
                    }

                    //Conta com 8 caracteres.
                    if (Strings.Len(Conta) > 8)
                    {
                        Response.Write("A Conta deve conter até 8 dígitos.");
                        return null;
                    }

                    //Cedente com 8 caracteres.
                    if (Strings.Len(boleto.CodigoCedente) > 8)
                    {
                        Response.Write("O Código do Cedente deve conter até 8 dígitos.");
                        return null;
                    }

                    //Nosso Número deve ser 11 ou 17 dígitos.
                    if (Strings.Len(boleto.NossoNumero) != 11 & Strings.Len(boleto.NossoNumero) != 10) //& Strings.Len(boleto.NossoNumero) != 17)
                    {
                        Response.Write("O Nosso Número deve ter 10 ou 11 dígitos dependendo da Carteira.");
                        return null;
                    }

                    break;
                //Se Carteira 18 então NossoNumero são 17 dígitos.
                //If boleto.Carteira = "18" Then
                // If Len(boleto.NossoNumero) <> 17 Then
                // Response.Write("O Nosso Número deve ter 17 dígitos para Carteira 18."]
                // Exit Sub
                // End If
                //Else
                // 'Senão, então NossoNumero 11 dígitos.
                // If Len(boleto.NossoNumero) <> 11 Then
                // Response.Write("O Nosso Número deve ter 11 dígitos para Carteira diferente que 18."]
                // Exit Sub
                // End If
                //End If
                    #endregion

                case "033":
                    //Santander.
                    break;

                case "070":
                    //Banco BRB.
                    break;

                case "104":
                    //Caixa Econômica Federal.
                    break;

                case "237":
                    //Banco Bradesco.
                    break;

                case "275":
                    //Banco Real.

                    //Cedente 
                    if (!string.IsNullOrEmpty(Request["CodigoCedente"]))
                    {

                    }

                    //Cobrança registrada 7 dígitos.
                    //Cobrança sem registro até 13 dígitos.
                    if (Strings.Len(boleto.NossoNumero) < 7 & Strings.Len(boleto.NossoNumero) < 13)
                    {
                        Response.Write("O Nosso Número deve ser entre 7 e 13 caracteres.");
                        return null;
                    }

                    //Carteira
                    if (boleto.Carteira != "00" & boleto.Carteira != "20" & boleto.Carteira != "31" & boleto.Carteira != "42" & boleto.Carteira != "47" & boleto.Carteira != "85" & boleto.Carteira != "57")
                    {
                        Response.Write("A Carteira deve ser 00,20,31,42,47,57 ou 85.");
                        return null;
                    }

                    //00'- Carteira do convênio
                    //20' - Cobrança Simples
                    //31' - Cobrança Câmbio
                    //42' - Cobrança Caucionada
                    //47' - Cobr. Caucionada Crédito Imobiliário
                    //85' - Cobrança Partilhada
                    //57 - última implementação ?

                    //Agência 4 dígitos.
                    if (Strings.Len(Agencia) > 4)
                    {
                        Response.Write("A Agência deve conter até 4 dígitos.");
                        return null;
                    }

                    //Número da conta 6 dígitos.
                    if (Strings.Len(Conta) > 6)
                    {
                        Response.Write("A Conta Corrente deve conter até 6 dígitos.");
                        return null;
                    }

                    break;
                case "291":
                    //Banco BCN.
                    break;

                case "341":
                    //Banco Itaú.
                    break;

                case "347":
                    //Banco Sudameris.
                    break;

                case "356":
                    //Banco Real.

                    //Cedente 
                    if (!string.IsNullOrEmpty(Request["CodigoCedente"]))
                    {
                    }
                    //?

                    //Cobrança registrada 7 dígitos.
                    //Cobrança sem registro até 13 dígitos.
                    if (Strings.Len(boleto.NossoNumero) < 7 & Strings.Len(boleto.NossoNumero) < 13)
                    {
                        Response.Write("O Nosso Número deve ser entre 7 e 13 caracteres.");
                        return null;
                    }

                    //Carteira
                    if (boleto.Carteira != "00" & boleto.Carteira != "20" & boleto.Carteira != "31" & boleto.Carteira != "42" & boleto.Carteira != "47" & boleto.Carteira != "85" & boleto.Carteira != "57")
                    {
                        Response.Write("A Carteira deve ser 00,20,31,42,47,57 ou 85.");
                        return null;
                    }

                    //00'- Carteira do convênio
                    //20' - Cobrança Simples
                    //31' - Cobrança Câmbio
                    //42' - Cobrança Caucionada
                    //47' - Cobr. Caucionada Crédito Imobiliário
                    //85' - Cobrança Partilhada

                    //Agência 4 dígitos.
                    if (Strings.Len(Agencia) > 4)
                    {
                        Response.Write("A Agência deve conter até 4 dígitos.");
                        return null;
                    }

                    //Número da conta 6 dígitos.
                    if (Strings.Len(Conta) > 6)
                    {
                        Response.Write("A Conta Corrente deve conter até 6 dígitos.");
                        return null;
                    }
                    break;
                case "409":
                    //Banco Unibanco.
                    break;

                case "422":
                    //Banco Safra.
                    break;

                default:
                    break;
            }

            //--------> Informa os dados do cedente
            Cedente c = new Cedente(boleto.CpfCnpjCedente, boleto.NomeCedente, Agencia.ToString(), DigAgencia.ToString(), Conta.ToString(), DigConta);

            //--------> Dependendo da carteira, é necessário informar o código do cedente (o banco que fornece)
            c.Codigo = Convert.ToInt32(boleto.CodigoCedente);

            c.Convenio = boleto.NumeroConvenio;

            //--------> Dados para preenchimento do boleto (data de vencimento, valor, carteira e nosso número)
            Boleto b = new Boleto(boleto.Vencimento, boleto.Valor, boleto.Carteira, boleto.NossoNumero, c);

            //--------> Dependendo da carteira, é necessário o número do documento
            b.NumeroDocumento = boleto.NumeroDocumento;

            //--------> Informa os dados do sacado
            b.Sacado = new Sacado(boleto.CpfCnpjSacado, boleto.NomeSacado);
            b.Sacado.Endereco.End = boleto.EnderecoSacado;
            b.Sacado.Endereco.Bairro = boleto.BairroSacado;
            b.Sacado.Endereco.Cidade = boleto.CidadeSacado;
            b.Sacado.Endereco.CEP = boleto.CepSacado;
            b.Sacado.Endereco.UF = boleto.UfSacado;

            //--------> Instrução.
            switch (boleto.CodigoBanco)
            {
                case "001":
                    //Banco do Brasil.
                    Instrucao_BancoBrasil i1 = new Instrucao_BancoBrasil(Convert.ToInt32(boleto.CodigoBanco));
                    i1.Descricao = boleto.Instrucoes;
                    // "Não Receber após o vencimento"
                    b.Instrucoes.Add(i1);
                    break;
                case "033":
                    //Santander.
                    Instrucao_Santander i2 = new Instrucao_Santander(Convert.ToInt32(boleto.CodigoBanco));
                    i2.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i2);
                    break;
                case "070":
                    //Banco BRB.
                    Instrucao i3 = new Instrucao(Convert.ToInt32(boleto.CodigoBanco));
                    i3.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i3);
                    break;
                case "104":
                    //Caixa Econômica Federal.
                    Instrucao_Caixa i4 = new Instrucao_Caixa(Convert.ToInt32(boleto.CodigoBanco));
                    i4.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i4);
                    break;
                case "237":
                    //Banco Bradesco.
                    Instrucao_Bradesco i5 = new Instrucao_Bradesco(Convert.ToInt32(boleto.CodigoBanco));
                    i5.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i5);
                    break;
                case "275":
                    //Banco Real.
                    Instrucao i6 = new Instrucao(Convert.ToInt32(boleto.CodigoBanco));
                    i6.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i6);
                    break;
                case "291":
                    //Banco BCN.
                    Instrucao i7 = new Instrucao(Convert.ToInt32(boleto.CodigoBanco));
                    i7.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i7);
                    break;
                case "341":
                    //Banco Itaú.
                    Instrucao_Itau i8 = new Instrucao_Itau(Convert.ToInt32(boleto.CodigoBanco));
                    i8.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i8);
                    break;
                case "347":
                    //Banco Sudameris.
                    Instrucao i9 = new Instrucao(Convert.ToInt32(boleto.CodigoBanco));
                    i9.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i9);
                    break;
                case "356":
                    //Banco Real.
                    //Dim i10 As New Instrucao(CInt(boleto.CodigoBanco))
                    Instrucao i10 = new Instrucao(1);
                    i10.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i10);
                    break;
                case "409":
                    //Banco Unibanco.
                    Instrucao i11 = new Instrucao(Convert.ToInt32(boleto.CodigoBanco));
                    i11.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i11);
                    break;
                case "422":
                    //Banco Safra.
                    Instrucao i12 = new Instrucao(Convert.ToInt32(boleto.CodigoBanco));
                    i12.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i12);
                    break;
                default:
                    //Instrução de teste Santander.
                    Instrucao_Santander i13 = new Instrucao_Santander(Convert.ToInt32(boleto.CodigoBanco));
                    i13.Descricao = boleto.Instrucoes;
                    //"Não Receber após o vencimento"
                    b.Instrucoes.Add(i13);
                    break;
            }

            //--------> Espécie do Documento - [R] Recibo
            switch (boleto.CodigoBanco)
            {
                case "001":
                    //Banco do Brasil.
                    b.EspecieDocumento = new EspecieDocumento_BancoBrasil(2);
                    break;

                //Espécie.
                //Cheque = 1, //CH – CHEQUE
                //DuplicataMercantil = 2, //DM – DUPLICATA MERCANTIL
                //DuplicataMercantilIndicacao = 3, //DMI – DUPLICATA MERCANTIL P/ INDICAÇÃO
                //DuplicataServico = 4, //DS – DUPLICATA DE SERVIÇO
                //DuplicataServicoIndicacao = 5, //DSI – DUPLICATA DE SERVIÇO P/ INDICAÇÃO
                //DuplicataRural = 6, //DR – DUPLICATA RURAL
                //LetraCambio = 7, //LC – LETRA DE CAMBIO
                //NotaCreditoComercial = 8, //NCC – NOTA DE CRÉDITO COMERCIAL
                //NotaCreditoExportacao = 9, //NCE – NOTA DE CRÉDITO A EXPORTAÇÃO
                //NotaCreditoIndustrial = 10, //NCI – NOTA DE CRÉDITO INDUSTRIAL
                //NotaCreditoRural = 11, //NCR – NOTA DE CRÉDITO RURAL
                //NotaPromissoria = 12, //NP – NOTA PROMISSÓRIA
                //NotaPromissoriaRural = 13, //NPR –NOTA PROMISSÓRIA RURAL
                //TriplicataMercantil = 14, //TM – TRIPLICATA MERCANTIL
                //TriplicataServico = 15, //TS – TRIPLICATA DE SERVIÇO
                //NotaSeguro = 16, //NS – NOTA DE SEGURO
                //Recibo = 17, //RC – RECIBO
                //Fatura = 18, //FAT – FATURA
                //NotaDebito = 19, //ND – NOTA DE DÉBITO
                //ApoliceSeguro = 20, //AP – APÓLICE DE SEGURO
                //MensalidadeEscolar = 21, //ME – MENSALIDADE ESCOLAR
                //ParcelaConsorcio = 22, //PC – PARCELA DE CONSÓRCIO
                //Outros = 23 //OUTROS

                case "033":
                    //Santander.
                    b.EspecieDocumento = new EspecieDocumento_Santander(17);
                    break;
                case "070":
                    //Banco BRB.
                    //b.EspecieDocumento = new EspecieDocumento(17);
                    b.EspecieDocumento = new EspecieDocumento(70);
                    break;
                case "104":
                    //Caixa Econômica Federal.
                    b.EspecieDocumento = new EspecieDocumento_Caixa(17);
                    break;
                case "237":
                    //Banco Bradesco.
                    b.EspecieDocumento = new EspecieDocumento_Bradesco(5);
                    break;
                case "275":
                    //Banco Real.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                case "291":
                    //Banco BCN.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                case "341":
                    //Banco Itaú.
                    b.EspecieDocumento = new EspecieDocumento_Itau(99);
                    break;
                case "347":
                    //Banco Sudameris.
                    b.EspecieDocumento = new EspecieDocumento_Sudameris(17);
                    break;
                case "356":
                    //Banco Real.
                    break;
                //b.EspecieDocumento = New EspecieDocumento_BancoBrasil(17)
                //b.EspecieDocumento = New EspecieDocumento_Itau(99)
                case "409":
                    //Banco Unibanco.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                case "422":
                    //Banco Safra.
                    b.EspecieDocumento = new EspecieDocumento(17);
                    break;
                default:
                    //Banco de teste Santander.
                    b.EspecieDocumento = new EspecieDocumento_Santander(17);
                    break;
            }
            
            BoletoBancario bb = new BoletoBancario();
            bb.CodigoBanco = Convert.ToInt16(boleto.CodigoBanco);
            //--------> 33 '-> Referente ao código do Santander
            bb.Boleto = b;
            //--------> bb.MostrarCodigoCarteira = True
            bb.Boleto.Valida();

            bb.ObsCarne = boleto.ObsCanhoto;
            bb.CnpjCedente = boleto.CpfCnpjCedente;

            /**
             * Esta parte do código foi adaptada para consultar nas tabelas TB25_EMPRESA ou TB000_INSTITUICAO,
             *  dependendo da configuração da instituição (por unidade ou por instituição),o tipo do layout do 
             *  boleto. Os tipo atuais são:
             *     >> Modelo 1 - O layout apresentado é o de carnê;
             *     >> Modelo 2 - O layout apresentado é o de boleto comum com o recibo do sacado;
             *     >> Modelo 3 - O layout apresentado é o de boleto comun com recibo do sacado e comprovante
             *                   de entrega;
             *     >> Modelo 4 - O layout apresentado é o de boleto comum com recibo do sacado e o valor do desconto
             *                   nas instruções.
             * 
             * Esta alteração foi executada por Victor Martins Machado, no dia 13/05/2013.
             * */
            #region Valida layout do boleto gerado
            TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB000_INSTITUICAOReference.Load();
            tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();
            //TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);

            if (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL == "I")
            {
                // Apresenta o boleto de acordo com a instituição
                switch (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_BOLETO_BANC)
                {
                    case "N":
                        // Carnê
                        bb.MostrarComprovanteEntrega = false;
                        bb.OcultarReciboSacado = true;
                        bb.FormatoCarne = true;
                        b.ValorDesconto = boleto.Desconto;
                        break;
                    case "C":
                        // Com recibo do sacado
                        bb.MostrarComprovanteEntrega = false;
                        bb.OcultarReciboSacado = false;
                        bb.FormatoCarne = false;
                        b.ValorDesconto = boleto.Desconto;
                        break;
                    case "M":
                        // Com recibo do sacado e o valor do desconto nas instruções
                        bb.MostrarComprovanteEntrega = false;
                        bb.OcultarReciboSacado = false;
                        bb.FormatoCarne = false;
                        break;
                    case "S":
                        // Com recibo do sacado e comprovante de entrega
                        bb.MostrarComprovanteEntrega = true;
                        bb.OcultarReciboSacado = false;
                        bb.FormatoCarne = false;
                        b.ValorDesconto = boleto.Desconto;
                        break;
                    default:
                        // O padrão é como carnê
                        bb.MostrarComprovanteEntrega = false;
                        bb.OcultarReciboSacado = true;
                        bb.FormatoCarne = true;
                        b.ValorDesconto = boleto.Desconto;
                        break;
                }
            }
            else
            {
                // Apresenta o boleto de acordo com a unidade
                switch (tb25.TP_BOLETO_BANC)
                {
                    case "N":
                        // Carnê
                        bb.MostrarComprovanteEntrega = false;
                        bb.OcultarReciboSacado = true;
                        bb.FormatoCarne = true;
                        b.ValorDesconto = boleto.Desconto;
                        break;
                    case "C":
                        // Com recibo do sacado
                        bb.MostrarComprovanteEntrega = false;
                        bb.OcultarReciboSacado = false;
                        bb.FormatoCarne = false;
                        b.ValorDesconto = boleto.Desconto;
                        break;
                    case "M":
                        // Com recibo do sacado e o valor do desconto nas instruções
                        bb.MostrarComprovanteEntrega = false;
                        bb.OcultarReciboSacado = false;
                        bb.FormatoCarne = false;
                        break;
                    case "S":
                        // Com recibo do sacado e comprovante de entrega
                        bb.MostrarComprovanteEntrega = true;
                        bb.OcultarReciboSacado = false;
                        bb.FormatoCarne = false;
                        b.ValorDesconto = boleto.Desconto;
                        break;
                    default :
                        // O padrão é como carnê
                        bb.MostrarComprovanteEntrega = false;
                        bb.OcultarReciboSacado = true;
                        bb.FormatoCarne = true;
                        b.ValorDesconto = boleto.Desconto;
                        break;
                }
            }
            #endregion

            bb.OcultarInstrucoes = true;
            //panelDados.Visible = false;
            //panelBoleto.Controls.Clear();
            //if (panelBoleto.Controls.Count == 0)
            //{

            panelBoleto.Controls.Add(bb);

            //}

            //03399.08063 49800.000330 32007.101028 8 41680000065640 -> correta
            //03399.08063 49800.000330 32007.101028 8 41680000065640
            //03399.08063 49800.000330 32007.101028 1 41680000065640
            //03399.08063 49800.003334 20071.301012 6 41680000065640
            //03399.08063 49800.000330 32007.101028 1 41680000065640

            //03399.08063 49800.003334 20071.301020 4 41680000065640
            //03399.08063 49800.003334 20071.301020 4 41680000065640

            //Gerar remessa.
            //Dim rdr As System.IO.Stream
            //Dim arquivo As New ArquivoRemessa(TipoArquivo.CNAB400)
            //arquivo.GerarArquivoRemessa(boleto.CodigoCedente, b.Banco, _
            // b.Cedente, b, rdr, 1)
            //Response.Write(rdr.ToString())

            //--------> Se quiser pegar o número que aparece na parte superior do boleto: 04192.16239 00723.547238 77956.041840 2 52940000020000              
            //bb.Boleto.CodigoBarra.LinhaDigitavel

            //return bb.Boleto.CodigoBarra.Codigo;
            return bb.Boleto.CodigoBarra.LinhaDigitavel.Replace(" ", "").Replace(".", "");
        }

        /// <summary>
        /// Faz o Incremento do Nosso Número da Unidade cedente e atualiza o código de barras no registro do Contas a Receber (tb47_CTA_RECEB)
        /// </summary>
        /// <param name="boleto">Entidade InformacoesBoletoBancario</param>
        /// <param name="codigoBarras">Código de barras</param>
        private void AtualizaNossoNumeroCodigoBarras(InformacoesBoletoBancario boleto, string codigoBarras)
        {
            try
            {
                //------------> Recebe título com campos informados
                TBS47_CTA_RECEB c = TBS47_CTA_RECEB.RetornaPelaChavePrimaria(boleto.CO_EMP, boleto.NU_DOC, boleto.NU_PAR, boleto.DT_CAD_DOC);

                //------------> Recebe o registro da tabela TB045, tabela de nosso número.
                TB045_NOS_NUM n = TB045_NOS_NUM.RetornaPeloTitulo(c);

                //------------> Faz a verificação para saber se código de barra é vazio (Primeira Impressão), se sim, atualiza o Nosso Número
                //if (c.CO_BARRA_DOC == "")
                //{
                c.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                c.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                c.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();

                if (String.IsNullOrEmpty(c.CO_NOS_NUM))
                {

                    TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(c.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

                    long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                    int casas = u.CO_PROX_NOS_NUM.Length;
                    string mask = string.Empty;
                    foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                    u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                    GestorEntities.SaveOrUpdate(u);
                    //TB25_EMPRESA u = TB25_EMPRESA.RetornaPelaChavePrimaria(boleto.CO_EMP);
                    //long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                    //int casas = u.CO_PROX_NOS_NUM.Length;
                    //string mask = string.Empty;
                    //foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                    //u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                    //GestorEntities.SaveOrUpdate(u);
                    //}


                    c.CO_NOS_NUM = boleto.NossoNumero;     
                }

                c.CO_BARRA_DOC = codigoBarras;
                GestorEntities.SaveOrUpdate(c);

                //===> Valida se existe registro na tabela de nosso número para o título em questão
                if (n != null)
                {
                    //===> Atualiza a linha digitável na tabela de nossos números
                    n.CO_BARRA_DOC = codigoBarras;
                    GestorEntities.SaveOrUpdate(n);
                }
            }
            catch (Exception)
            {
                // Response.Write("Falha ao atualizar o Nosso Número e Código de Barras");
            }
        }
        #endregion
    }
}
