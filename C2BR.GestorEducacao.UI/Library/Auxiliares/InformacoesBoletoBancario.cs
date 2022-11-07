//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resources;
using System.Web.UI;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public static class BoletoBancarioHelpers
    {
        /// <summary>
        /// Método que retorna o boleto em uma nova página no browser
        /// </summary>
        /// <param name="page">Página</param>
        public static void GeraBoletos(Page page, Boolean saude = false)
        {
            if (saude)
                page.RegisterStartupScript("", "<script language ='Javascript'>var win=window.open('../../../../../../Library/Componentes/ApresentaBoletoSaude.aspx','true');</script>");
            else
                page.RegisterStartupScript("", "<script language ='Javascript'>var win=window.open('../../../../../../Library/Componentes/ApresentaBoleto.aspx','true');</script>");
            //page.RegisterStartupScript("", "<script language ='Javascript'>var win=window.open('~/Library/Componentes/ApresentaBoleto.aspx','true');</script>");
        }
    }

    public struct InformacoesBoletoBancario
    {
        #region Chaves do Título

        /// <summary>
        /// Código da unidade
        /// </summary>
        public int CO_EMP { get; set; }

        /// <summary>
        /// Número do documento
        /// </summary>
        public string NU_DOC { get; set; }

        /// <summary>
        /// Número da parcela
        /// </summary>
        public int NU_PAR { get; set; }

        /// <summary>
        /// Data de cadastro do documento
        /// </summary>
        public DateTime DT_CAD_DOC { get; set; }
        #endregion

        #region Dados do Boleto Bancário

        /// <summary>
        /// Código do Banco
        /// </summary>
        public string CodigoBanco { get; set; }

        /// <summary>
        /// Data de Vencimento do Boleto
        /// </summary>
        public DateTime Vencimento { get; set; }

        /// <summary>
        /// Valor do Boleto
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Valor do Desconto do Boleto
        /// </summary>
        public decimal Desconto { get; set; }

        /// <summary>
        /// Nosso Número
        /// </summary>
        public string NossoNumero { get; set; }

        /// <summary>
        /// Número do Documento
        /// </summary>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Número da Carteira
        /// </summary>
        public string Carteira { get; set; }

        /// <summary>
        /// Observação do Canhoto no Carnê
        /// </summary>
        public string ObsCanhoto { get; set; }
        #endregion

        #region Dados do Cedente

        /// <summary>
        /// CPF/CNPJ do Cedente
        /// </summary>
        public string CpfCnpjCedente { get; set; }

        /// <summary>
        /// Número do Convênio com o Banco
        /// </summary>
        public int NumeroConvenio { get; set; }

        /// <summary>
        /// Código do Cedente
        /// </summary>
        public string CodigoCedente { get; set; }

        /// <summary>
        /// Nome do Cedente
        /// </summary>
        public string NomeCedente { get; set; }

        /// <summary>
        /// Número da Agência do Cedente
        /// </summary>
        public string Agencia { get; set; }

        /// <summary>
        /// Número da Conta do Cedente
        /// </summary>
        public string Conta { get; set; }

        /// <summary>
        /// Instruções de Cobrança
        /// </summary>
        public string Instrucoes { get; set; }
        #endregion

        #region Dados do Sacado

        /// <summary>
        /// CPF/CNPJ do Sacado
        /// </summary>
        public string CpfCnpjSacado { get; set; }

        /// <summary>
        /// Nome do Sacado
        /// </summary>
        public string NomeSacado { get; set; }

        /// <summary>
        /// Endereço (Logradouro, Número e Complemento) do Sacado
        /// </summary>
        public string EnderecoSacado { get; set; }

        /// <summary>
        /// Bairro do Sacado
        /// </summary>
        public string BairroSacado { get; set; }

        /// <summary>
        /// Cidade do Sacado
        /// </summary>
        public string CidadeSacado { get; set; }

        /// <summary>
        /// CEP do Sacado
        /// </summary>
        public string CepSacado { get; set; }

        /// <summary>
        /// UF do Sacado
        /// </summary>
        public string UfSacado { get; set; }
        #endregion
    }
}
