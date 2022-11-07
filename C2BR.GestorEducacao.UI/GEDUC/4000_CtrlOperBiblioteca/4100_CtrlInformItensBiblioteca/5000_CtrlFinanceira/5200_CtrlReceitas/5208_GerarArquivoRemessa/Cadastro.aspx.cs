//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: FINANCEIRO
// SUBMÓDULO: GERAR ARQUIVO REMESSA
// OBJETIVO: GERAR ARQUIVO REMESSA
// DATA DE CRIAÇÃO: 12/03/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 12/03/2013 | Caio Mendonça              | Criação das principais funcionalidades - btnGerar_Click, CarregaBoletos, CarregaUnidadeContrato,ddlUnidadeContrato_SelectedIndexChanged
// 19/03/2013 | Caio Mendonça              | Atualização da função de gerar arquivo, força download e só busca da tabela TB322
// 15/07/2013 | André Nobre Vinagre        | Ajustado o nome do arquivo criado para o banco bradesco de acordo com o padrão CNAB 400 - Bradesco
// 17/07/2013 | André Nobre Vinagre        | Preenchido os campos Data Emissão, Num Docto e Data Limite para concessão do desconto que estavam vazios
// 18/07/2013 | André Nobre Vinagre        | Implementados arquivo remessa para o BB, Caixa Econômica, Bradesco e Santander
//
//   

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.IO;
using BoletoNet;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5208_GerarArquivoRemessa
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidadeContrato();
                CarregaBoletos();
            }

        }

        #endregion


        protected void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginAuxili.CO_EMP == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Referência a sessão expirada. Por favor efetue o login!");
                    return;
                }

                TipoArquivo tiporemessa = new TipoArquivo();

                if (ddlcnab.SelectedValue.ToString() == "400")
                {
                    tiporemessa = TipoArquivo.CNAB400;
                }
                else
                {
                    tiporemessa = TipoArquivo.CNAB240;
                }

                ArquivoRemessa remessa = new ArquivoRemessa(tiporemessa);
                Boletos boletos = new Boletos();

                // Lista os status que irá buscar, criei assim para se quiser colocar outros status é mais fácil
                List<string> status = new List<string>();
                status.Add("Q");
                status.Add("P");

                // Pega id da configuração do boleto
                int idboleto = Convert.ToInt32(ddlBoleto.SelectedValue.ToString());

                Cedente cedente = null;

                IQueryable<TB322_ARQ_REM_BOLETO> rems = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                                         join tb322 in TB322_ARQ_REM_BOLETO.RetornaTodosRegistros() on tb47.CO_NOS_NUM equals tb322.NU_NOSSO_NUMERO
                                                         where tb47.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO == idboleto 
                                                                && tb47.IC_SIT_DOC == "A"
                                                                && tb322.CO_SITU == "A"
                                                                && tb322.FLA_ENVIO_BANCO == "N"
                                                                && tb322.CO_CARTEIRA == tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA
                                                                && tb322.IDEBANCO == tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.IDEBANCO
                                                                && tb322.CO_CONTA == tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA
                                                                && tb322.CO_AGENCIA == tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_AGENCIA
                                                                select tb322).OrderBy(x => x.NU_LOTE_ARQUI);

                if (rems.Count() != 0)
                {

                    IQueryable<TB47_CTA_RECEB> tb47s = TB47_CTA_RECEB.RetornaTodosRegistros().Where(t => t.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO == idboleto);

                    TB227_DADOS_BOLETO_BANCARIO tb227 = TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros().SingleOrDefault(t => t.ID_BOLETO == idboleto);

                    int codbanco = 0;

                    // Passa por todos os boletos
                    #region boletos

                    foreach (TB322_ARQ_REM_BOLETO rem in rems)
                    {

                        if (cedente == null)
                        {
                            // Cedente para geração do arquivo de remessa, apenas um cedente por remessa
                            cedente = new Cedente(
                                rem.CO_CPFCGC_EMP, // CPF
                                rem.NO_RAZSOC_EMP, // Razão Social
                                rem.CO_AGENCIA.ToString(), // Agência
                                rem.DI_AGENCIA, // Digito Agência
                                rem.CO_CONTA.Trim(), // Conta
                                rem.CO_DIG_CONTA.Trim() // Digito conta
                                );
                            cedente.Carteira = rem.CO_CARTEIRA;
                            cedente.Convenio = rem.NU_CONVENIO;
                            cedente.Codigo = rem.NU_CONVENIO;
                        }

                        // Criação do objeto de boleto
                        Boleto b = new Boleto(
                            rem.DT_VENCIMENTO, // vencimento
                            rem.VL_TITULO, // valor
                            rem.CO_CARTEIRA.Trim(), // carteira
                            rem.NU_NOSSO_NUMERO, // nosso número
                            cedente // cedente
                            );

                        //Juros -> 1 (Valor Dia)
                        b.JurosMora = Convert.ToDecimal(rem.VL_JUROS); // juros
                        //Multa => 2 (percentual)
                        b.ValorMulta = Convert.ToDecimal(rem.VL_MULTA);  // multa
                        //Desconto => 1 (Valor)
                        b.ValorDesconto = Convert.ToDecimal(rem.VL_DESCTO);  // desconto
                        b.OutrosAcrescimos = Convert.ToDecimal(rem.VL_OUTROS); // acrescimos, outros
                        b.ValorCobrado = Convert.ToDecimal(rem.VL_PAGO); // valor pago
                        b.DataDesconto = rem.DT_VENCIMENTO;
                        b.DataMulta = rem.DT_VENCIMENTO;
                        b.NumeroDocumento = rem.NU_DOC_TB47.Replace(".", "").Replace("/", "");
                        //Santander e bradesco utiliza esse campo
                        b.PercMulta = Convert.ToDecimal(rem.VL_MULTA);  // multa

                        var tb045 = (from iTb045 in TB045_NOS_NUM.RetornaTodosRegistros()
                                    where iTb045.CO_NOS_NUM == rem.NU_NOSSO_NUMERO
                                    select iTb045).FirstOrDefault();

                        if (tb045 != null)
	                    {
                            b.DataProcessamento = tb045.DT_NOS_NUM;
	                    }           
                        else
                            b.DataProcessamento = (DateTime)rem.DT_CAD_DOC_TB47;

                        switch (rem.IDEBANCO)
                        {
                            case "001":
                                //Banco do Brasil.
                                b.EspecieDocumento = new EspecieDocumento_BancoBrasil(17);
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
                                b.EspecieDocumento = new EspecieDocumento_BRB(25);
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

                        // Cria endereço para Sacado
                        Endereco end = new Endereco();
                        end.Bairro = rem.NO_BAIRRO; // Bairro
                        end.CEP = rem.CO_CEP; // Cep
                        end.Cidade = rem.NO_CIDADE; // Cidade
                        end.Complemento = rem.DE_COMP; // Complemento
                        end.End = rem.DE_ENDE; // Endereço
                        end.UF = rem.CO_UF;

                        Sacado sacado = new Sacado(
                            rem.NU_CPFCNPJ, // CPF/CNPJ
                            rem.NO_SACADO, // Nome
                            end // Endereço
                            );

                        // Coloca o sacado no boleto
                        b.Sacado = sacado;

                        // Adiciona o boleto na lista de boletos
                        boletos.Add(b);

                        // Número do convênio para remessa, apenas um convênio por remessa
                        remessa.NumeroConvenio = rem.NU_CONVENIO.ToString();
                        codbanco = Convert.ToInt32(rem.IDEBANCO);


                        ////--------> Informações do Boleto
                        //boleto.Carteira = rem.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                        //boleto.CodigoBanco = rem.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;
                        //boleto.NossoNumero = rem.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                        //boleto.NumeroDocumento = rem.NU_DOC + "-" + rem.NU_PAR;
                        //boleto.Valor = rem.VR_PAR_DOC;
                        //boleto.Vencimento = rem.DT_VEN_DOC;

                        ////--------> Informações do Cedente
                        //boleto.NumeroConvenio = rem.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                        //boleto.Agencia = rem.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                        //                 rem.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA;
                        //boleto.CodigoCedente = rem.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                        //boleto.Conta = rem.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim() + '-' + rem.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_DIG_CONTA.Trim();
                        //boleto.CpfCnpjCedente = tb25.CO_CPFCGC_EMP;
                        //boleto.NomeCedente = tb25.NO_RAZSOC_EMP; ;

                        ////--------> Informações do Sacado
                        //boleto.BairroSacado = varSacado.BAIRRO;
                        //boleto.CepSacado = varSacado.CEP;
                        //boleto.CidadeSacado = varSacado.CIDADE;
                        //boleto.CpfCnpjSacado = varSacado.CPFCNPJ;
                        //boleto.EnderecoSacado = varSacado.ENDERECO + " " + varSacado.NUMERO + " " + varSacado.COMPL;
                        //boleto.NomeSacado = varSacado.NOME;
                        //boleto.UfSacado = varSacado.UF;

                    }

                    #endregion

                    int cont = 0;
                    if (TB322_ARQ_REM_BOLETO.RetornaTodosRegistros().OrderByDescending(x => x.NU_DCTO_RECEB).FirstOrDefault().NU_DCTO_RECEB == null)
                    {
                        cont = 1;
                    }
                    else
                    {
                        cont = int.Parse(TB322_ARQ_REM_BOLETO.RetornaTodosRegistros().OrderByDescending(x => x.NU_DCTO_RECEB).FirstOrDefault().NU_DCTO_RECEB);
                        cont++;
                    }

                    // Arquivo de remessa
                    string nomea = "";

                    switch (codbanco)
                    {
                        case 1:
                            nomea = "BB" + DateTime.Now.Date.ToString("MMdd") + cont.ToString().PadLeft(2, '0') + ".rem";
                            break;
                        case 33:
                            nomea = "SA" + DateTime.Now.Date.ToString("ddMM") + cont.ToString().PadLeft(2, '0') + ".rem";
                            break;
                        case 104:
                            nomea = "CE" + DateTime.Now.Date.ToString("ddMM") + cont.ToString().PadLeft(2, '0') + ".rem";
                            break;
                        case 70:
                            nomea = "BR" + DateTime.Now.Date.ToString("ddMM") + cont.ToString().PadLeft(2, '0') + ".rem";
                            break;
                        case 237:
                            nomea = "CB" + DateTime.Now.Date.ToString("ddMM") + cont.ToString().PadLeft(2, '0') + ".rem";
                            break;
                        default:
                            nomea = "R_" + codbanco + "_" + DateTime.Now.Date.ToString("ddMMyy") + "_" + DateTime.Now.Hour.ToString() + "h_" + cont.ToString() + ".txt";
                            break;
                    }                  
                        
                    string nomearquivo = Server.MapPath("~/ArquivosRemessa/"+nomea);

                    Stream arquivo = File.OpenWrite(@nomearquivo);

                    // Variavel de banco
                    Banco banco = new Banco(codbanco);

                    // Gera arquivo de remessa, escrevendo no arquivo
                    remessa.GerarArquivoRemessa(remessa.NumeroConvenio, banco, cedente, boletos, arquivo, cont);    
                    

                    // Fecha arquivo
                    arquivo.Close();

                    IList<TB322_ARQ_REM_BOLETO> Lrem = rems.ToList();

                    foreach (TB322_ARQ_REM_BOLETO rem in Lrem)
                    {
                        rem.CO_SITU = "A";
                        rem.FLA_ENVIO_BANCO = "S";
                        rem.NU_DCTO_RECEB = cont.ToString();
                        rem.DT_ENVIO = DateTime.Now;
                        rem.CO_EMP_ENVIO = LoginAuxili.CO_EMP;
                        rem.CO_COL_ENVIO = LoginAuxili.CO_COL;
                        rem.NR_IP_ACESS_ENVIO = LoginAuxili.IP_USU;
                        rem.NO_ARQUIVO = nomea;
                        rem.CO_TIPO_REGIS_CNAB = ddlcnab.SelectedValue;
                        TB322_ARQ_REM_BOLETO.SaveOrUpdate(rem);
                    }

                    IList<TB47_CTA_RECEB> tb47ss = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                                         where tb47.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO == idboleto 
                                                         && tb47.IC_SIT_DOC == "A"
                                                                select tb47).ToList();

                    foreach (TB47_CTA_RECEB tb in tb47ss)
                    {
                        if (tb != null)
                        {
                            foreach (TB322_ARQ_REM_BOLETO rm in Lrem)
                            {
                                if (rm != null)
                                {
                                    if (!String.IsNullOrEmpty(tb.CO_NOS_NUM))
                                    {
                                        if (!String.IsNullOrEmpty(rm.NU_NOSSO_NUMERO))
                                        {
                                            if (tb.CO_NOS_NUM.Trim() == rm.NU_NOSSO_NUMERO)
                                            {
                                                tb.FLA_ENVIO_BANCO = "S";
                                                tb.FLA_ARQ_REMESSA = "S";
                                                TB47_CTA_RECEB.SaveOrUpdate(tb);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }


                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment; filename=" + nomea);
                    Response.WriteFile(@nomearquivo);
                    Response.ContentType = "txt";
                    Response.End();


                }

                else
                {

                    AuxiliPagina.EnvioMensagemSucesso(this, "Não há boletos para geração do arquivo de remessa.");

                }


                //Response.Clear();
                //Response.AddHeader("content-disposition", "attachment; filename=Remessa_" + codbanco + "_" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt");
                //Response.WriteFile(@nomearquivo);
                //Response.ContentType = "txt";
                //Response.End();


                //ClientScript.RegisterStartupScript(this.GetType(), "normal", "volta()");
                ////AuxiliPagina.EnvioMensagemSucesso(this, "Arquivo gerado com sucesso. C:/testeremessa/ArquivoRemessa.txt");

                //div2.Visible = false;
                //btnGerar.Visible = true;

            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this, ex.ToString());
                return;
            }


        }


        # region Funções dos dropdown's

        /// <summary>
        /// Método que carrega o dropdown de Boletos
        /// </summary>
        private void CarregaBoletos()
        {
            int coEmp = int.Parse(ddlUnidadeContrato.SelectedValue);

            var result = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                          select new { tb227.ID_BOLETO, tb227.TB224_CONTA_CORRENTE, 
                          TP_TAXA_BOLETO = (tb227.TP_TAXA_BOLETO == "M" ? "Matrícula" :
                              (tb227.TP_TAXA_BOLETO == "R" ? "Renovação" :
                                (tb227.TP_TAXA_BOLETO == "E" ? "Mensalidade" :
                                    (tb227.TP_TAXA_BOLETO == "A" ? "Atividades Extras" :
                                        (tb227.TP_TAXA_BOLETO == "B" ? "Biblioteca" :
                                            (tb227.TP_TAXA_BOLETO == "S" ? "Serv. de Secretaria" :
                                                (tb227.TP_TAXA_BOLETO == "D" ? "Serv. Diversos" :
                                                    (tb227.TP_TAXA_BOLETO == "N" ? "Negociação" : "Outros"
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                              )
                          )
                          }).ToList();

            var result2 = (from res in result
                           join tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros() on res.TB224_CONTA_CORRENTE.CO_CONTA equals tb225.CO_CONTA
                           where tb225.CO_EMP == coEmp && res.TB224_CONTA_CORRENTE.CO_AGENCIA == tb225.CO_AGENCIA
                           && tb225.IDEBANCO == res.TB224_CONTA_CORRENTE.IDEBANCO
                           select new
                           {
                               res.ID_BOLETO,
                               DESCRICAO = string.Format("BCO {0} - AGE {1} - CTA {2} - {3}", res.TB224_CONTA_CORRENTE.IDEBANCO,
                               res.TB224_CONTA_CORRENTE.CO_AGENCIA, res.TB224_CONTA_CORRENTE.CO_CONTA, res.TP_TAXA_BOLETO)
                           }).OrderBy(b => b.DESCRICAO);

            ddlBoleto.DataSource = result2;

            ddlBoleto.DataValueField = "ID_BOLETO";
            ddlBoleto.DataTextField = "DESCRICAO";
            ddlBoleto.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades de Contrato
        /// </summary>
        private void CarregaUnidadeContrato()
        {
            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        protected void ddlUnidadeContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBoletos();
        }

        #endregion

    }    
    
}