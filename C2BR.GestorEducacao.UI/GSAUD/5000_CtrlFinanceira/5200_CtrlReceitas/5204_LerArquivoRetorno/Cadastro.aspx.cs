//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: FINANCEIRO
// SUBMÓDULO: LER ARQUIVO RETORNO
// OBJETIVO: LER ARQUIVO RETORNO
// DATA DE CRIAÇÃO: 
//-------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 19/03/2013 | Caio Barbosa Mendonça      | Criar função de retorno para formato CNAB 400
// -----------+----------------------------+-------------------------------------
// 31/05/2013 | André Nobre Vinagre        | Corrigida a questão do nosso número pego no
//            |                            | arquivo de retorno
//            |                            | 

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
namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas._5204_LerArquivoRetorno
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
            if (IsPostBack)
                return;

            divGrid.Visible = false;
        }

        #endregion

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            if (LoginAuxili.CO_EMP == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Referência a sessão expirada. Por favor efetue o login!");
                return;
            }

            if (fuMain.PostedFile.ContentLength == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Arquivo retorno do banco não selecionado!");
                return;
            }

            string conteudo = string.Empty;


            if (ddlcnab.SelectedValue.ToString() == "400")
            {
                Retorno400();
            }
            else
            {
                Retorno240();
            }
        }


        // Retorno no formato 400
        private bool Retorno400()
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fuMain.PostedFile.InputStream.CopyTo(ms);
                    ms.Position = 0;

                    StreamReader stream = new StreamReader(ms, System.Text.Encoding.UTF8);
                    string linha = "";
                    int i = 1;
                    int banco = 0;

                    while (((linha = stream.ReadLine()) != null) && (i == 1))
                    {
                        string bb = linha.Substring(76, 3);
                        banco = int.Parse(bb);
                        i = 2;
                    }

                    ms.Position = 0;

                    Banco bco = new Banco(banco);
                    ArquivoRetornoCNAB400 cnab400 = new ArquivoRetornoCNAB400();
                    cnab400.LerArquivoRetorno(bco, ms);

                    if (cnab400 == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Arquivo não processado!");
                        return false;
                    }

                    grdResumo.DataSource = null;

                    List<RetornoGrid> lst = new List<RetornoGrid>();
                    List<TB321_ARQ_RET_BOLETO> lstRet = new List<TB321_ARQ_RET_BOLETO>();

                    foreach (DetalheRetorno detalhe in cnab400.ListaDetalhe)
                    {

                        /*
                         * Informações do detalhe - BRADESCO
                          
                            -- Tipo de Inscrição Empresa
                            detalhe.CodigoInscricao
                            -- Nº Inscrição da Empresa
                            detalhe.NumeroInscricao

                            -- Identificação da Empresa Cedente no Banco
                            detalhe.Agencia
                            detalhe.Conta
                            detalhe.DACConta

                            -- Nº Controle do Participante
                            detalhe.NumeroControle
                            -- Identificação do Título no Banco
                            detalhe.NossoNumero
                            detalhe.DACNossoNumero
                            -- Carteira
                            detalhe.Carteira
                            -- Identificação de Ocorrência
                            detalhe.CodigoOcorrencia

                            -- Descrição da ocorrência
                            detalhe.DescricaoOcorrencia

                            -- Número do Documento
                            detalhe.NumeroDocumento
                            -- Identificação do Título no Banco
                            detalhe.IdentificacaoTitulo

                            -- Valor do Título
                            detalhe.ValorTitulo
                            -- Banco Cobrador
                            detalhe.CodigoBanco
                            -- Agência Cobradora
                            detalhe.AgenciaCobradora
                            -- Espécie do Título
                            detalhe.Especie
                            -- Despesas de cobrança para os Códigos de Ocorrência (Valor Despesa)
                            detalhe.ValorDespesa
                            -- Outras despesas Custas de Protesto (Valor Outras Despesas)
                            detalhe.ValorOutrasDespesas
                            -- IOF
                            detalhe.IOF
                            -- Abatimento Concedido sobre o Título (Valor Abatimento Concedido)
                            detalhe.ValorAbatimento
                            -- Desconto Concedido (Valor Desconto Concedido)
                            detalhe.Descontos
                            -- Valor Pago
                            detalhe.ValorPago
                            -- Juros Mora
                            detalhe.JurosMora
                            -- Outros Créditos
                            detalhe.OutrosCreditos
                            -- Motivo do Código de Ocorrência 19 (Confirmação de Instrução de Protesto)
                            detalhe.MotivoCodigoOcorrencia

                            -- Data Ocorrência no Banco
                            detalhe.DataOcorrencia
                            -- Data Vencimento do Título
                            detalhe.DataVencimento
                            -- Data do Crédito
                            detalhe.DataCredito

                            -- Origem Pagamento
                            detalhe.OrigemPagamento

                            -- Motivos das Rejeições para os Códigos de Ocorrência
                            detalhe.MotivosRejeicao
                            -- Número do Cartório
                            detalhe.NumeroCartorio
                            -- Número do Protocolo
                            detalhe.NumeroProtocolo
                            -- Nome do Sacado
                            detalhe.NomeSacado = "";

                            detalhe.NumeroSequencial
                          
                         
                         */

                        // O Bradesco aceita a letra P no nosso número
                        //string nossoNum = long.Parse(detalhe.NossoNumero.Substring(0, 12)).ToString();
                        //São 11 caracteres que serão pegos quando o banco é bradesco
                        //string nossoNum = detalhe.NossoNumero.Substring(0, 11);
                        string nossoNum = banco == 237 ? long.Parse(detalhe.NossoNumero.Substring(0, 11)).ToString() :
                            banco == 341 ? detalhe.NossoNumero.ToString().PadLeft(8, '0') : long.Parse(detalhe.NossoNumero).ToString();
                        string conta = detalhe.Conta.ToString().PadLeft(8,'0');

                        TB321_ARQ_RET_BOLETO tb321 = RetornaEntidade(nossoNum);
                        TB321_ARQ_RET_BOLETO ret;

                        if (tb321 == null)
                        {
                            ret = new TB321_ARQ_RET_BOLETO();

                            ret.DT_CREDITO = detalhe.DataCredito;
                            if (ret.DT_CREDITO.ToString() == "01/01/0001 00:00:00")
                            {
                                ret.DT_CREDITO = Convert.ToDateTime("01/01/1950 00:00:00");
                            }
                            ret.NU_LOTE_ARQUI = Convert.ToDecimal(detalhe.NumeroSequencial);
                            ret.DT_VENCIMENTO = detalhe.DataVencimento;
                            if (ret.DT_VENCIMENTO.ToString() == "01/01/0001 00:00:00")
                            {
                                ret.DT_VENCIMENTO = ret.DT_CREDITO;
                            }
                            ret.NU_NOSSO_NUMERO = nossoNum;
                            ret.VL_JUROS = detalhe.JurosMora;
                            ret.VL_PAGO = detalhe.ValorPago;
                            ret.VL_TARIFAS = detalhe.ValorOutrasDespesas;
                            ret.VL_TITULO = detalhe.ValorTitulo;
                            ret.VL_DESCTO = detalhe.Descontos + detalhe.ValorAbatimento;
                            ret.CO_EMP = LoginAuxili.CO_EMP;
                            //ret.IDEBANCO = detalhe.CodigoBanco.ToString().PadLeft(3, '0');
                            ret.IDEBANCO = banco.ToString().PadLeft(3,'0');
                            ret.CO_AGENCIA = detalhe.Agencia;
                            //ret.DI_AGENCIA = detalhe.DACAgenciaCobradora.ToString();
                            ret.CO_CONTA = (int.Parse(conta.Substring(0, 7))).ToString();
                            ret.CO_DIG_CONTA = detalhe.DACConta.ToString();
                            //nossoNum = long.Parse(nossoNum).ToString();

                            var tbs47 = (from iTb045 in TB045_NOS_NUM.RetornaTodosRegistros()
                                            join itbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                                                on iTb045.NU_DOC equals itbs47.NU_DOC
                                        where iTb045.CO_NOS_NUM.Equals(nossoNum)
                                        select itbs47).FirstOrDefault();

                            if (tbs47 != null)
                            {
                                if (tbs47.IC_SIT_DOC == "Q" || tbs47.IC_SIT_DOC == "P")
                                    ret.FL_STATUS = "Q";
                                else if (tbs47.IC_SIT_DOC == "A")
                                    ret.FL_STATUS = "P";
                                else
                                    ret.FL_STATUS = "C";
                                ret.NU_DCTO_RECEB = tbs47.NU_DOC;
                            }
                            else
                            {
                                ret.FL_STATUS = "I";
                            }
                        }
                        else
                        {
                            if (tb321.FL_STATUS == "B")
                            {
                                if (tb321.NU_LOTE_ARQUI != Convert.ToDecimal(detalhe.NumeroSequencial))
                                {
                                    ret = new TB321_ARQ_RET_BOLETO();

                                    ret.DT_CREDITO = detalhe.DataCredito;
                                    if (ret.DT_CREDITO.ToString() == "01/01/0001 00:00:00")
                                    {
                                        ret.DT_CREDITO = Convert.ToDateTime("01/01/1950 00:00:00");
                                    }
                                    ret.NU_LOTE_ARQUI = Convert.ToDecimal(detalhe.NumeroSequencial);
                                    ret.DT_VENCIMENTO = detalhe.DataVencimento;
                                    if (ret.DT_VENCIMENTO.ToString() == "01/01/0001 00:00:00")
                                    {
                                        ret.DT_VENCIMENTO = ret.DT_CREDITO;
                                    }
                                    ret.NU_NOSSO_NUMERO = nossoNum;
                                    ret.VL_JUROS = detalhe.JurosMora;
                                    ret.VL_PAGO = detalhe.ValorPago;
                                    ret.VL_TARIFAS = detalhe.ValorOutrasDespesas;
                                    ret.VL_TITULO = detalhe.ValorTitulo;
                                    ret.VL_DESCTO = detalhe.Descontos + detalhe.ValorAbatimento;
                                    ret.CO_EMP = LoginAuxili.CO_EMP;
                                    //ret.IDEBANCO = detalhe.CodigoBanco.ToString().PadLeft(3, '0');
                                    ret.IDEBANCO = banco.ToString().PadLeft(3, '0');
                                    ret.CO_AGENCIA = detalhe.Agencia;
                                    //ret.DI_AGENCIA = detalhe.DACAgenciaCobradora.ToString();
                                    ret.CO_CONTA = detalhe.Conta.ToString();
                                    ret.CO_DIG_CONTA = detalhe.DACConta.ToString();
                                    ret.FL_STATUS = "D";
                                }
                                else
                                {
                                    ret = tb321;
                                }
                            }
                            else
                            {
                                if (tb321.NU_LOTE_ARQUI != Convert.ToDecimal(detalhe.NumeroSequencial))
                                {
                                    ret = new TB321_ARQ_RET_BOLETO();

                                    ret.DT_CREDITO = detalhe.DataCredito;
                                    if (ret.DT_CREDITO.ToString() == "01/01/0001 00:00:00")
                                    {
                                        ret.DT_CREDITO = Convert.ToDateTime("01/01/1950 00:00:00");
                                    }
                                    ret.NU_LOTE_ARQUI = Convert.ToDecimal(detalhe.NumeroSequencial);
                                    ret.DT_VENCIMENTO = detalhe.DataVencimento;
                                    if (ret.DT_VENCIMENTO.ToString() == "01/01/0001 00:00:00")
                                    {
                                        ret.DT_VENCIMENTO = ret.DT_CREDITO;
                                    }
                                    ret.NU_NOSSO_NUMERO = nossoNum;
                                    ret.VL_JUROS = detalhe.JurosMora;
                                    ret.VL_PAGO = detalhe.ValorPago;
                                    ret.VL_TARIFAS = detalhe.ValorOutrasDespesas;
                                    ret.VL_TITULO = detalhe.ValorTitulo;
                                    ret.VL_DESCTO = detalhe.Descontos + detalhe.ValorAbatimento;
                                    ret.CO_EMP = LoginAuxili.CO_EMP;
                                    //ret.IDEBANCO = detalhe.CodigoBanco.ToString().PadLeft(3, '0');
                                    ret.IDEBANCO = banco.ToString().PadLeft(3, '0');
                                    ret.CO_AGENCIA = detalhe.Agencia;
                                    //ret.DI_AGENCIA = detalhe.DACAgenciaCobradora.ToString();
                                    ret.CO_CONTA = detalhe.Conta.ToString();
                                    ret.CO_DIG_CONTA = detalhe.DACConta.ToString();
                                    ret.FL_STATUS = "D";
                                }
                                else
                                {
                                    ret = tb321;

                                    ret.DT_CREDITO = detalhe.DataCredito;
                                    if (ret.DT_CREDITO.ToString() == "01/01/0001 00:00:00")
                                    {
                                        ret.DT_CREDITO = Convert.ToDateTime("01/01/1950 00:00:00");
                                    }
                                    ret.NU_LOTE_ARQUI = Convert.ToDecimal(detalhe.NumeroSequencial);
                                    ret.DT_VENCIMENTO = detalhe.DataVencimento;
                                    if (ret.DT_VENCIMENTO.ToString() == "01/01/0001 00:00:00")
                                    {
                                        ret.DT_VENCIMENTO = ret.DT_CREDITO;
                                    }
                                    ret.NU_NOSSO_NUMERO = nossoNum;
                                    ret.VL_JUROS = detalhe.JurosMora;
                                    ret.VL_PAGO = detalhe.ValorPago;
                                    ret.VL_TARIFAS = detalhe.ValorOutrasDespesas;
                                    ret.VL_TITULO = detalhe.ValorTitulo;
                                    ret.VL_DESCTO = detalhe.Descontos + detalhe.Abatimentos;
                                    ret.CO_EMP = LoginAuxili.CO_EMP;
                                    //ret.IDEBANCO = detalhe.CodigoBanco.ToString().PadLeft(3, '0');
                                    ret.IDEBANCO = banco.ToString().PadLeft(3, '0');
                                    ret.CO_AGENCIA = detalhe.Agencia;                                    
                                    //ret.DI_AGENCIA = detalhe.DACAgenciaCobradora.ToString();
                                    ret.CO_CONTA = detalhe.Conta.ToString();
                                    ret.CO_DIG_CONTA = detalhe.DACConta.ToString();

                                    var tbs47 = (from iT045 in TB045_NOS_NUM.RetornaTodosRegistros()
                                                    join itbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                                                        on iT045.NU_DOC equals itbs47.NU_DOC
                                                where iT045.CO_NOS_NUM.Contains(nossoNum)
                                                select itbs47).FirstOrDefault();

                                    if (tbs47 != null)
                                    {
                                        if (tbs47.IC_SIT_DOC == "Q" || tbs47.IC_SIT_DOC == "P")
                                            ret.FL_STATUS = "Q";
                                        else if (tbs47.IC_SIT_DOC == "A")
                                            ret.FL_STATUS = "P";
                                        else
                                            ret.FL_STATUS = "C";

                                        ret.NU_DCTO_RECEB = tbs47.NU_DOC;
                                    }
                                    else
                                    {
                                        ret.FL_STATUS = "I";
                                    }
                                }
                            }
                        }

                        ret.CO_TIPO_REGIS_CNAB = "400";

                        lstRet.Add(ret);

                        RetornoGrid retGrd = new RetornoGrid();

                        retGrd.DtVencimento = ret.DT_VENCIMENTO.ToString("dd/MM/yy");
                        retGrd.Valor = ret.VL_TITULO.ToString("###,###.00");
                        retGrd.NossoNumero = nossoNum;
                        lst.Add(retGrd);
                    }

                    foreach (var retorno in lstRet)
                    {
                        GestorEntities.SaveOrUpdate(retorno, false);
                    }

                    GestorEntities.SaveChanges(RefreshMode.ClientWins, null);

                    divGrid.Visible = liObser.Visible = liResumo.Visible = true;

                    if (lst.Count() > 0)
                    {
                        grdResumo.DataBind();
                    }

                    grdResumo.DataSource = lst;
                    grdResumo.DataBind();
                }

                return true;
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Erro desconhecido.\n" + ex.ToString());

                return false;
            }
        }


        // Retorno no formato 240
        private bool Retorno240()
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fuMain.PostedFile.InputStream.CopyTo(ms);
                    ms.Position = 0;

                    StreamReader stream = new StreamReader(ms, System.Text.Encoding.UTF8);
                    string linha = "";
                    int i = 1;
                    int banco = 0;

                    while (((linha = stream.ReadLine()) != null) && (i == 1))
                    {
                        banco = int.Parse(linha.Substring(0, 3));
                        i = 2;
                    }

                    ms.Position = 0;

                    Banco bco = new Banco(banco);                    
                    ArquivoRetornoCNAB240 cnab240 = new ArquivoRetornoCNAB240();
                    cnab240.LerArquivoRetorno(bco, ms);

                    if (cnab240 == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Arquivo não processado!");
                        return false;
                    }

                    grdResumo.DataSource = null;

                    List<RetornoGrid> lst = new List<RetornoGrid>();
                    List<TB321_ARQ_RET_BOLETO> lstRet = new List<TB321_ARQ_RET_BOLETO>();

                    foreach (DetalheRetornoCNAB240 detalhe in cnab240.ListaDetalhes)
                    {
                        if (detalhe.SegmentoT.DataVencimento < DateTime.Parse("01/01/1950"))
                        {
                            //AuxiliPagina.EnvioMensagemErro(this, "Arquivo Retorno apresenta títulos sem data de vencimento.");
                            //return false;
                            detalhe.SegmentoT.DataVencimento = DateTime.Parse("01/01/1900");
                        }

                        string nossoNum = "";
                        string strBanco = detalhe.SegmentoT.CodigoBanco.ToString().PadLeft(3, '0');
                        
                        if (strBanco == "033")
                        {
                            //Os títulos do conexão aquarela só foi encontrado quando fiz essa substring com 12 posições, mas o nosso número no santander 
                            //pega 13 posições
                            nossoNum = long.Parse(detalhe.SegmentoT.NossoNumero.Substring(0, 12)).ToString();
                        }
                        else
                            nossoNum = detalhe.SegmentoT.NossoNumero.Replace(" ", "");
                        

                        TB321_ARQ_RET_BOLETO tb321 = RetornaEntidade(nossoNum);
                        TB321_ARQ_RET_BOLETO ret;

                        if (tb321 == null)
                        {
                            ret = new TB321_ARQ_RET_BOLETO();

                            ret.DT_CREDITO = detalhe.SegmentoU.DataCredito;
                            ret.NU_LOTE_ARQUI = detalhe.SegmentoT.CodigoLote;
                            ret.DT_VENCIMENTO = detalhe.SegmentoT.DataVencimento;                            
                            ret.NU_NOSSO_NUMERO = nossoNum;
                            ret.VL_JUROS = detalhe.SegmentoU.JurosMultaEncargos;
                            ret.VL_PAGO = detalhe.SegmentoU.ValorPagoPeloSacado;
                            ret.VL_TARIFAS = detalhe.SegmentoT.ValorTarifas;
                            ret.VL_TITULO = detalhe.SegmentoT.ValorTitulo;
                            ret.VL_DESCTO = detalhe.SegmentoU.ValorDescontoConcedido + detalhe.SegmentoU.ValorAbatimentoConcedido;
                            ret.CO_EMP = LoginAuxili.CO_EMP;
                            ret.IDEBANCO = strBanco;
                            ret.CO_AGENCIA = detalhe.SegmentoT.Agencia;
                            ret.DI_AGENCIA = detalhe.SegmentoT.DigitoAgencia;
                            ret.CO_CONTA = detalhe.SegmentoT.Conta.ToString();
                            ret.CO_DIG_CONTA = detalhe.SegmentoT.DigitoConta;

                            var tbs47 = (from iTb045 in TB045_NOS_NUM.RetornaTodosRegistros()
                                        join itbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros() on iTb045.NU_DOC equals itbs47.NU_DOC
                                        //Se for banco do Brasil verifica se o nosso numero gerado pelo arquivo retorno contem o nosso numero existente no titulo
                                        //Alinhar com Córdova uma melhor solução
                                        where (ret.IDEBANCO == "001" ? ret.NU_NOSSO_NUMERO.Contains(itbs47.CO_NOS_NUM.Trim()) : iTb045.CO_NOS_NUM.Contains(nossoNum))
                                        select itbs47).FirstOrDefault();

                            if (tbs47 != null)
                            {
                                if (tbs47.IC_SIT_DOC == "Q" || tbs47.IC_SIT_DOC == "P")
                                    ret.FL_STATUS = "Q";
                                else if (tbs47.IC_SIT_DOC == "A")
                                    ret.FL_STATUS = "P";
                                else
                                    ret.FL_STATUS = "C";
                                ret.NU_DCTO_RECEB = tbs47.NU_DOC;
                            }
                            else
                            {
                                ret.FL_STATUS = "I";
                            }
                        }
                        else
                        {
                            if (tb321.FL_STATUS == "B")
                            {
                                if (tb321.NU_LOTE_ARQUI != detalhe.SegmentoT.CodigoLote)
                                {
                                    ret = new TB321_ARQ_RET_BOLETO();

                                    ret.DT_CREDITO = detalhe.SegmentoU.DataCredito;
                                    ret.NU_LOTE_ARQUI = detalhe.SegmentoT.CodigoLote;
                                    ret.DT_VENCIMENTO = detalhe.SegmentoT.DataVencimento;
                                    ret.NU_NOSSO_NUMERO = nossoNum;
                                    ret.VL_JUROS = detalhe.SegmentoU.JurosMultaEncargos;
                                    ret.VL_PAGO = detalhe.SegmentoU.ValorPagoPeloSacado;
                                    ret.VL_TARIFAS = detalhe.SegmentoT.ValorTarifas;
                                    ret.VL_TITULO = detalhe.SegmentoT.ValorTitulo;
                                    ret.VL_DESCTO = detalhe.SegmentoU.ValorDescontoConcedido + detalhe.SegmentoU.ValorAbatimentoConcedido;
                                    ret.CO_EMP = LoginAuxili.CO_EMP;
                                    ret.IDEBANCO = detalhe.SegmentoT.CodigoBanco.ToString().PadLeft(3, '0');
                                    ret.CO_AGENCIA = detalhe.SegmentoT.Agencia;
                                    ret.DI_AGENCIA = detalhe.SegmentoT.DigitoAgencia;
                                    ret.CO_CONTA = detalhe.SegmentoT.Conta.ToString();
                                    ret.CO_DIG_CONTA = detalhe.SegmentoT.DigitoConta;
                                    ret.FL_STATUS = "D";
                                }
                                else
                                {
                                    ret = tb321;
                                }
                            }
                            else
                            {
                                if (tb321.NU_LOTE_ARQUI != detalhe.SegmentoT.CodigoLote)
                                {
                                    ret = new TB321_ARQ_RET_BOLETO();

                                    ret.DT_CREDITO = detalhe.SegmentoU.DataCredito;
                                    ret.NU_LOTE_ARQUI = detalhe.SegmentoT.CodigoLote;
                                    ret.DT_VENCIMENTO = detalhe.SegmentoT.DataVencimento;
                                    ret.NU_NOSSO_NUMERO = nossoNum;
                                    ret.VL_JUROS = detalhe.SegmentoU.JurosMultaEncargos;
                                    ret.VL_PAGO = detalhe.SegmentoU.ValorPagoPeloSacado;
                                    ret.VL_TARIFAS = detalhe.SegmentoT.ValorTarifas;
                                    ret.VL_TITULO = detalhe.SegmentoT.ValorTitulo;
                                    ret.VL_DESCTO = detalhe.SegmentoU.ValorDescontoConcedido + detalhe.SegmentoU.ValorAbatimentoConcedido;
                                    ret.CO_EMP = LoginAuxili.CO_EMP;
                                    ret.IDEBANCO = detalhe.SegmentoT.CodigoBanco.ToString().PadLeft(3, '0');
                                    ret.CO_AGENCIA = detalhe.SegmentoT.Agencia;
                                    ret.DI_AGENCIA = detalhe.SegmentoT.DigitoAgencia;
                                    ret.CO_CONTA = detalhe.SegmentoT.Conta.ToString();
                                    ret.CO_DIG_CONTA = detalhe.SegmentoT.DigitoConta;
                                    ret.FL_STATUS = "D";
                                }
                                else
                                {
                                    ret = tb321;

                                    ret.DT_CREDITO = detalhe.SegmentoU.DataCredito;
                                    ret.NU_LOTE_ARQUI = detalhe.SegmentoT.CodigoLote;
                                    ret.DT_VENCIMENTO = detalhe.SegmentoT.DataVencimento;
                                    ret.NU_NOSSO_NUMERO = nossoNum;
                                    ret.VL_JUROS = detalhe.SegmentoU.JurosMultaEncargos;
                                    ret.VL_PAGO = detalhe.SegmentoU.ValorPagoPeloSacado;
                                    ret.VL_TARIFAS = detalhe.SegmentoT.ValorTarifas;
                                    ret.VL_TITULO = detalhe.SegmentoT.ValorTitulo;
                                    ret.VL_DESCTO = detalhe.SegmentoU.ValorDescontoConcedido + detalhe.SegmentoU.ValorAbatimentoConcedido;
                                    ret.CO_EMP = LoginAuxili.CO_EMP;
                                    ret.IDEBANCO = detalhe.SegmentoT.CodigoBanco.ToString().PadLeft(3, '0');
                                    ret.CO_AGENCIA = detalhe.SegmentoT.Agencia;
                                    ret.DI_AGENCIA = detalhe.SegmentoT.DigitoAgencia;
                                    ret.CO_CONTA = detalhe.SegmentoT.Conta.ToString();
                                    ret.CO_DIG_CONTA = detalhe.SegmentoT.DigitoConta;

                                    var tbs47 = (from iTb045 in TB045_NOS_NUM.RetornaTodosRegistros()
                                                    join itbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                                                        on iTb045.NU_DOC equals itbs47.NU_DOC
                                                where itbs47.CO_NOS_NUM.Contains(nossoNum)
                                                select itbs47).FirstOrDefault();

                                    if (tbs47 != null)
                                    {
                                        if (tbs47.IC_SIT_DOC == "Q" || tbs47.IC_SIT_DOC == "P")
                                            ret.FL_STATUS = "Q";
                                        else if (tbs47.IC_SIT_DOC == "A")
                                            ret.FL_STATUS = "P";
                                        else
                                            ret.FL_STATUS = "C";

                                        ret.NU_DCTO_RECEB = tbs47.NU_DOC;
                                    }
                                    else
                                    {
                                        ret.FL_STATUS = "I";
                                    }
                                }
                            }
                        }

                        ret.CO_TIPO_REGIS_CNAB = "240";

                        lstRet.Add(ret);

                        RetornoGrid retGrd = new RetornoGrid();

                        retGrd.DtVencimento = ret.DT_VENCIMENTO.ToString("dd/MM/yy");
                        retGrd.Valor = ret.VL_TITULO.ToString("###,###.00");
                        retGrd.NossoNumero = nossoNum;
                        lst.Add(retGrd);
                    }

                    foreach (var retorno in lstRet)
                    {
                        GestorEntities.SaveOrUpdate(retorno, false);
                    }

                    GestorEntities.SaveChanges(RefreshMode.ClientWins, null);

                    divGrid.Visible = liObser.Visible = liResumo.Visible = true;

                    if (lst.Count() > 0)
                    {
                        grdResumo.DataBind();
                    }

                    grdResumo.DataSource = lst;
                    grdResumo.DataBind();
                }

                return true;
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Erro desconhecido.\n" + ex.ToString());

                return false;
            }
        }


        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB321_ARQ_RET_BOLETO</returns>
        private TB321_ARQ_RET_BOLETO RetornaEntidade(string nossoNumero)
        {
            return TB321_ARQ_RET_BOLETO.RetornaPeloNossoNumero(nossoNumero);
        }
    }    
    

    public class RetornoGrid
    {
        public string NossoNumero { get; set; }
        public string DtVencimento { get; set; }
        public string Valor { get; set; }
    }
}