//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONFIGURAÇÃO DE MÓDULOS E FUNCIONALIDADES
// OBJETIVO: CADASTRO COMO FAZER
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0203_CadastroComoFazer
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaModulosPai();
                CarregaGrupoInformacao();
                CarregaFuncionalidade();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TBPROX_PASSOS comoFazer;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                comoFazer = new TBPROX_PASSOS();
                int idModulo = int.Parse(ddlFuncionalidadeCF.SelectedValue);
                comoFazer.ADMMODULO = ADMMODULO.RetornaPelaChavePrimaria(idModulo);
            }
            else
                comoFazer = TBPROX_PASSOS.RetornaPelaChavePrimaria(QueryStringAuxili.QueryStringValorInt(QueryStrings.Id));

            comoFazer.NO_DESCRICAO = txtDescCF.Text;
            comoFazer.CO_ORDEM_MENU = int.Parse(txtOMCF.Text);
            
            if (ddlTipoLinkCF.SelectedValue == "E")
            {
                comoFazer.CO_FLAG_LINK = "S";
                comoFazer.DE_URL_EXTERNA = txtNomUrlExternoCF.Text;
            }
            else
                comoFazer.CO_FLAG_LINK = "N";

            if (ddlTipoLinkCF.SelectedValue == "I")
            {
                int idModulo = int.Parse(ddlProxFuncionalidadeCF.SelectedValue);
                comoFazer.ADMMODULO1 = ADMMODULO.RetornaPelaChavePrimaria(idModulo);
            }
            else
                comoFazer.ADMMODULO1 = null;

            comoFazer.DE_ITEM_REFER = txtDeItemReferCF.Text;
            comoFazer.CO_STATUS = ddlStatusCF.SelectedValue;                                    
            comoFazer.CO_FLAG_REFER_VALID = "S";

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                TBPROX_PASSOS.Delete(comoFazer, false);
                GestorEntities.CurrentContext.SaveChanges();
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Excluído com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
            else
            {
                TBPROX_PASSOS.SaveOrUpdate(comoFazer, true);
                CarregaGrid();
            }            
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TBPROX_PASSOS comoFazer = TBPROX_PASSOS.RetornaPelaChavePrimaria(QueryStringAuxili.QueryStringValorInt(QueryStrings.Id));

            if (comoFazer != null)
            {
                comoFazer.ADMMODULOReference.Load();
                comoFazer.ADMMODULO.ADMMODULO2Reference.Load();
                comoFazer.ADMMODULO1Reference.Load();
                CarregaModulosPai();
                var admModu = ADMMODULO.RetornaPelaChavePrimaria(comoFazer.ADMMODULO.ADMMODULO2.ideAdmModulo);
                admModu.ADMMODULO2Reference.Load();
                ddlModuloPaiCF.SelectedValue = admModu.ADMMODULO2.ideAdmModulo.ToString();
                CarregaGrupoInformacao();
                ddlGrupoInforCF.SelectedValue = comoFazer.ADMMODULO.ADMMODULO2.ideAdmModulo.ToString();
                CarregaFuncionalidade();    
                ddlFuncionalidadeCF.SelectedValue = comoFazer.ADMMODULO.ideAdmModulo.ToString();
                CarregaGrid();
                txtDescCF.Text = comoFazer.NO_DESCRICAO;
                txtOMCF.Text = comoFazer.CO_ORDEM_MENU.ToString();

                if (comoFazer.CO_FLAG_LINK == "S")
                {
                    ddlTipoLinkCF.SelectedValue = "E";
                    liNomeURLLinkExternoCF.Visible = true;
                    liLinkFuncINternaCF.Visible = false;
                    txtNomUrlExternoCF.Text = comoFazer.DE_URL_EXTERNA;
                }
                else
                {
                    if (comoFazer.CO_FLAG_LINK == "N" && comoFazer.ADMMODULO1 != null)
                    {
                        ddlTipoLinkCF.SelectedValue = "I";
                        liNomeURLLinkExternoCF.Visible = false;
                        liLinkFuncINternaCF.Visible = true;
                        comoFazer.ADMMODULO1Reference.Load();
                        CarregaFuncInterna();
                        ddlProxFuncionalidadeCF.SelectedValue = comoFazer.ADMMODULO1.ideAdmModulo.ToString();
                    }
                    else
                        ddlTipoLinkCF.SelectedValue = "N";
                }

                txtDeItemReferCF.Text = comoFazer.DE_ITEM_REFER;
                ddlStatusCF.SelectedValue = comoFazer.CO_STATUS;
                                  
//------------> Varre a grid de Como Fazer para encontrar o ID da funcionalidade escolhida e alterar seu estilo
                foreach (GridViewRow linha in grdBuscaCF.Rows)
                {
                    int idComoFazer = Convert.ToInt32(grdBuscaCF.DataKeys[linha.RowIndex].Values[0]);

                    if (idComoFazer == QueryStringAuxili.QueryStringValorInt(QueryStrings.Id))
                        linha.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183);
                }
            }            
        }

        #endregion    
    
        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Módulo Pai
        /// </summary>
        private void CarregaModulosPai()
        {
            ddlModuloPaiCF.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                          where admModulo.ADMMODULO2 == null
                                          select new { admModulo.nomModulo, admModulo.ideAdmModulo }).OrderBy(a => a.nomModulo);

            ddlModuloPaiCF.DataTextField = "nomModulo";
            ddlModuloPaiCF.DataValueField = "ideAdmModulo";
            ddlModuloPaiCF.DataBind();

            ddlModuloPaiCF.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupos de Informação
        /// </summary>
        private void CarregaGrupoInformacao()
        {
            int ideModuloPai = ddlModuloPaiCF.SelectedValue != "" ? int.Parse(ddlModuloPaiCF.SelectedValue) : 0;

            ddlGrupoInforCF.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                         where admModulo.ADMMODULO2.ideAdmModulo == ideModuloPai
                                         select new { admModulo.nomModulo, admModulo.ideAdmModulo }).OrderBy( a => a.nomModulo );

            ddlGrupoInforCF.DataTextField = "nomModulo";
            ddlGrupoInforCF.DataValueField = "ideAdmModulo";
            ddlGrupoInforCF.DataBind();

            ddlGrupoInforCF.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionalidades
        /// </summary>
        private void CarregaFuncionalidade()
        {
            int idModuloPai = ddlGrupoInforCF.SelectedValue != "" ? int.Parse(ddlGrupoInforCF.SelectedValue) : 0;

            ddlFuncionalidadeCF.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                              where idModuloPai != 0 ? admModulo.ADMMODULO2.ideAdmModulo == idModuloPai : idModuloPai == 0
                                              && admModulo.nomURLModulo != null && admModulo.nomURLModulo != ""
                                              select new { admModulo.nomModulo, admModulo.ideAdmModulo, admModulo.numOrdemMenu }).OrderBy( a => a.nomModulo );

            ddlFuncionalidadeCF.DataTextField = "nomModulo";
            ddlFuncionalidadeCF.DataValueField = "ideAdmModulo";
            ddlFuncionalidadeCF.DataBind();

            ddlFuncionalidadeCF.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionalidade Interna
        /// </summary>
        private void CarregaFuncInterna()
        {
            ddlProxFuncionalidadeCF.DataSource = (from admModulo in ADMMODULO.RetornaTodosRegistrosStatus()
                                                  where admModulo.nomURLModulo != null && admModulo.nomURLModulo != ""
                                                  select new { admModulo.nomModulo, admModulo.ideAdmModulo, admModulo.numOrdemMenu }).OrderBy(a => a.nomModulo).ThenBy(a => a.numOrdemMenu);

            ddlProxFuncionalidadeCF.DataTextField = "nomModulo";
            ddlProxFuncionalidadeCF.DataValueField = "ideAdmModulo";
            ddlProxFuncionalidadeCF.DataBind();

            ddlProxFuncionalidadeCF.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega a grid de Como Fazer
        /// </summary>
        private void CarregaGrid()
        {
            int idModuloCF = ddlFuncionalidadeCF.SelectedValue != "" ? int.Parse(ddlFuncionalidadeCF.SelectedValue) : 0;

            if (idModuloCF == 0)
                return;

            grdBuscaCF.DataKeyNames = new string[] { "CO_PROXIPASSOS" };
            grdBuscaCF.DataSource = (from comoFazer in TBPROX_PASSOS.RetornaTodosRegistros()
                                     where comoFazer.ADMMODULO.ideAdmModulo == idModuloCF
                                     select new
                                     {
                                         comoFazer.CO_PROXIPASSOS, comoFazer.NO_DESCRICAO, comoFazer.DE_ITEM_REFER,
                                         comoFazer.CO_FLAG_LINK, comoFazer.CO_ORDEM_MENU, comoFazer.CO_STATUS
                                     }).OrderBy( c => c.CO_ORDEM_MENU );

            grdBuscaCF.DataBind();
        }
        #endregion

        protected void ddlModuloPaiCF_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupoInformacao();
            CarregaFuncionalidade();
        }

        protected void ddlGrupoInforCF_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionalidade();
        }

        protected void ddlTipoLinkCF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoLinkCF.SelectedValue == "N")
                liNomeURLLinkExternoCF.Visible =  liLinkFuncINternaCF.Visible = false;
            else
                if (ddlTipoLinkCF.SelectedValue == "E")
                {
                    liNomeURLLinkExternoCF.Visible = true;
                    liLinkFuncINternaCF.Visible = false;
                }
                else
                    if (ddlTipoLinkCF.SelectedValue == "I")
                    {
                        liNomeURLLinkExternoCF.Visible = false;
                        liLinkFuncINternaCF.Visible = true;
                        CarregaFuncInterna();
                    }
        }

        protected void ddlFuncionalidadeCF_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

    }
}
