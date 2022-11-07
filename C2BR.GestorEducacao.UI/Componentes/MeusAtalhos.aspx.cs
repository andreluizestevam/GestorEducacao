//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: MEUS ATALHOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class MeusAtalhos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaRepeaterModulos();
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Carrega o repeater dos meus atalhos
        /// </summary>
        public void CarregaRepeaterModulos()
        {
            var resultado = (from admModulo in GestorEntities.CurrentContext.ADMMODULO
                             where admModulo.CO_FLAG_INFOR_GEREN.ToLower() == "s" && admModulo.flaStatus == "A"
                             select admModulo).OrderByDescending( a => a.nomModulo ).ToList();

            rptModuloItens.DataSource = (from result in resultado
                                         select new
                                         {
                                             result.ideAdmModulo, result.nomURLModulo, result.nomModulo, result.nomModulo_GEREN, result.nomDescricao,
                                             result.nomDescricaoGEREN, result.nomItemMenu, result.ADMMODULO2, result.numOrdemMenu,
                                             moduloId = result.ideAdmModulo, Icon = RetornaTipoIconeModulo(result)
                                         }).OrderBy( r => r.Icon );

            rptModuloItens.DataBind();
        }

        /// <summary>
        /// Retorna a string do tipo de icone do atalho de acordo com a tabela ADMMODULO informada
        /// </summary>
        /// <param name="admModulo">Tabela ADMMODULO</param>
        /// <returns>String do tipo de icone do atalho</returns>
        string RetornaTipoIconeModulo(ADMMODULO admModulo)
        {
            string strTipoIconeModulo = "/Navegacao/Icones/";

            if (!String.IsNullOrEmpty(admModulo.CO_FLAG_TIPO_ICONE_GEREN))
                if (admModulo.CO_FLAG_TIPO_ICONE_GEREN.ToLower().Equals("c"))
                    strTipoIconeModulo += "Gestor_IconesDiversos_Cadastros.png";
                else if (admModulo.CO_FLAG_TIPO_ICONE_GEREN.ToLower().Equals("r"))
                    strTipoIconeModulo += "Gestor_IconesDiversos_Relatorios.png";

            return strTipoIconeModulo;
        }
        #endregion

        protected void rptModuloItens_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            AuxiliRelatorioTemporario.ExecutaRelatorio(this.Page, e.CommandName);
        }             
    }
}