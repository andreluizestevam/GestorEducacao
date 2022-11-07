//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: CADASTRAMENTO DE GRUPO DE CONTAS CONTÁBIL
// DATA DE CRIAÇÃO: 
//--------------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//--------------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR              | DESCRIÇÃO RESUMIDA
// -----------+-----------------------------------+-------------------------------------
// 05/07/2016   Tayguara Acioli     TA.05/07/2016   Alterei o nome de alguns tipos de conta, alterei a ordem que elas eram exibidas, 
//                                                  os nomes dos tipos ficam na BaseApoio tipoContaCatabil, alterei o value na página
//                                                  de cadastro também para não ter erros de cadastros errados, por causa da alteração
//                                                  A	Ativo                               A	Ativo
//                                                  P	Passivo                             B	Passivo
//                                                  C	Receita                     	    C	Entradas/Saídas e Compensações
//                                                  D	Custo e Despesa 	                D	Entradas e Custos
//                                                  I	Investimento                        E	Despesas
//                                                  T	Título 	                            F	Receita	
//                                                     

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1111_CadastramentoGrupoCtaContabil
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }
        private static Dictionary<string, string> tipoConta = AuxiliBaseApoio.chave(tipoContaCatabil.ResourceManager, true);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(!IsPostBack)
                CarregarTipo();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_GRUP_CTA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_GRUP_CTA",
                HeaderText = "TIPO CONTA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NR_GRUP_CTA",
                HeaderText = "CÓDIGO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_GRUP_CTA",
                HeaderText = "GRUPO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string tipoGrupo = (ddlTipo.SelectedValue != "" ? ddlTipo.SelectedValue : "-1");
            string nomeGrupo = (txtGrupo.Text.Trim() == "" ? "-1" : txtGrupo.Text);
            var resultado = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                             where (nomeGrupo == "-1" ? 0==0 : tb53.DE_GRUP_CTA.Contains(txtGrupo.Text))
                             && (tipoGrupo == "-1" ? 0 == 0 : tb53.TP_GRUP_CTA == tipoGrupo)
                             && tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             orderby tb53.TP_GRUP_CTA, tb53.TP_GRUP_CTA, tb53.NR_GRUP_CTA //TA.05/07/2016
                             select new listaGrupos
                             {
                                DE_GRUP_CTA = tb53.DE_GRUP_CTA,
                                CO_TP_GRUP = tb53.TP_GRUP_CTA,
                                CO_GRUP_CTA = tb53.CO_GRUP_CTA,
                                NR_GRUP_CTA = tb53.NR_GRUP_CTA
                             });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_GRUP_CTA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown
        /// <summary>
        /// Carrega os tipos de grupos para filtro
        /// </summary>
        private void CarregarTipo()
        {
            ddlTipo.Items.Clear();
            ddlTipo.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoContaCatabil.ResourceManager, todos:true));
        }
        #endregion

        #region Classes
        /// <summary>
        /// Classe para tratamento da lista de grupos do filtro.
        /// </summary>
        private class listaGrupos
        {
            public string DE_GRUP_CTA { get; set; }
            public string CO_TP_GRUP {
                set
                {
                    if(tipoConta.Where(f => f.Key == value).DefaultIfEmpty() != null)
                        this.TP_GRUP_CTA = tipoConta[value];
                    else
                        this.TP_GRUP_CTA = "Nenhuma";
                }
            }
            public string TP_GRUP_CTA { get; set; }
            public int CO_GRUP_CTA { get; set; }
            public int? NR_GRUP_CTA { get; set; }
        }
        #endregion
    }
}