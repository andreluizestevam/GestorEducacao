//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMÔNIO
// SUBMÓDULO: MOVIMENTAÇÃO DE ITENS DE PATRIMÔNIO
// OBJETIVO: TRANSFERÊNCIA EXTERNA DE ITENS DE PATRIMÔNIO
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
using System.Data.Sql;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6320_CtrlMovimentacaoItensPatrimonio.F6323_TransferenciaExternaBens
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaPatrimonio();
                CarregaDadosPatrimonio();
                txtDtCad.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtRespMovi.Text = LoginAuxili.NOME_USU_LOGADO;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            decimal codPatr = ddlPatrimonio.SelectedValue != "" ? decimal.Parse(ddlPatrimonio.SelectedValue) : 0;

            TB231_PATRI_HISTO_MOVIM_EXTERNA tb231 = RetornaEntidade();

            if (tb231 == null)
                tb231 = new TB231_PATRI_HISTO_MOVIM_EXTERNA();

            var tb212 = TB212_ITENS_PATRIMONIO.RetornaPelaChavePrimaria(codPatr);

            tb212.CO_STATUS = "T";

            TB212_ITENS_PATRIMONIO.SaveOrUpdate(tb212);

            tb231.TB212_ITENS_PATRIMONIO = TB212_ITENS_PATRIMONIO.RetornaPelaChavePrimaria(codPatr);
            tb231.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            tb231.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            tb212.TB14_DEPTOReference.Load();
            tb231.TB14_DEPTO = tb212.TB14_DEPTO;
            tb231.NO_INST_DESTI = txtInstDest.Text;
            tb231.DT_MOVIM_PATRI_EXT = Convert.ToDateTime(txtDataI.Text);
            tb231.CO_STATUS = ddlStatus.SelectedValue;
            tb231.DE_OBS = txtObs.Text;
            tb231.DT_CADASTRO = Convert.ToDateTime(txtDtCad.Text);
            tb231.DT_STATUS = Convert.ToDateTime(txtDtCad.Text);
            
            CurrentCadastroMasterPage.CurrentEntity = tb231;
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB231_PATRI_HISTO_MOVIM_EXTERNA tb231 = RetornaEntidade();

            if (tb231 != null)
            {
                tb231.TB212_ITENS_PATRIMONIOReference.Load();
                tb231.TB03_COLABORReference.Load();

                ddlUnidade.SelectedValue = tb231.TB212_ITENS_PATRIMONIO.CO_EMP.ToString();
                CarregaPatrimonio();
                ddlPatrimonio.SelectedValue = tb231.TB212_ITENS_PATRIMONIO.COD_PATR.ToString();
                CarregaDadosPatrimonio();
                txtRespMovi.Text = tb231.TB03_COLABOR.NO_COL;
                txtObs.Text = tb231.DE_OBS;
                txtInstDest.Text = tb231.NO_INST_DESTI.ToString();
                txtDtCad.Text = tb231.DT_CADASTRO.ToString("dd/MM/yyyy");
                txtDataI.Text = tb231.DT_MOVIM_PATRI_EXT.ToString("dd/MM/yyyy");
                tb231.CO_STATUS = ddlStatus.SelectedValue;
                ddlUnidade.Enabled = ddlPatrimonio.Enabled = false;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB231_PATRI_HISTO_MOVIM_EXTERNA</returns>
        private TB231_PATRI_HISTO_MOVIM_EXTERNA RetornaEntidade()
        {
            return TB231_PATRI_HISTO_MOVIM_EXTERNA.RetornaPelaChavePrimaria(QueryStringAuxili.QueryStringValorInt(QueryStrings.Id));
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Patrimônio
        /// </summary>
        private void CarregaPatrimonio()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlPatrimonio.DataSource = (from tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                        where tb212.CO_EMP == coEmp && tb212.CO_STATUS != "T"
                                        select new { tb212.COD_PATR, tb212.DE_PATR });

            ddlPatrimonio.DataTextField = "DE_PATR";
            ddlPatrimonio.DataValueField = "COD_PATR";
            ddlPatrimonio.DataBind();

            ddlPatrimonio.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que preenche campos do formulário de acordo com o Patrimônio selecionado
        /// </summary>
        private void CarregaDadosPatrimonio()
        {
            decimal codPatr = ddlPatrimonio.SelectedValue != "" ? decimal.Parse(ddlPatrimonio.SelectedValue) : 0;

            var tb212 = (from lTb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                         where lTb212.COD_PATR == codPatr
                         select new { NO_DEPTO_ATUAL = lTb212.TB14_DEPTO.NO_DEPTO, NO_DEPTO_ORIGEM = lTb212.TB14_DEPTO1.NO_DEPTO }).FirstOrDefault();

            if (tb212 != null)
                txtDep.Text = tb212.NO_DEPTO_ATUAL != null ? tb212.NO_DEPTO_ATUAL : tb212.NO_DEPTO_ORIGEM != null ? tb212.NO_DEPTO_ORIGEM : "";
            else
                txtDep.Text = "";
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPatrimonio();            
        }

        protected void ddlPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosPatrimonio();
        }
    }
}
