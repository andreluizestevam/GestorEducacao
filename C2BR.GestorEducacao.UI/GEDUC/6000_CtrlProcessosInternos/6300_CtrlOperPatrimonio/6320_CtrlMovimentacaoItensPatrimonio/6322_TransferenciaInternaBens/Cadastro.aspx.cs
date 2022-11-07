//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMÔNIO
// SUBMÓDULO: MOVIMENTAÇÃO DE ITENS DE PATRIMÔNIO
// OBJETIVO: TRANSFERÊNCIA INTERNA DE ITENS DE PATRIMÔNIO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6320_CtrlMovimentacaoItensPatrimonio.F6322_TransferenciaInternaBens
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
                CarregaUE();
                CarregaFuncionario();
                ddlUnidest.SelectedValue = LoginAuxili.CO_EMP.ToString();
                txtDtCad.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtRespMovi.Text = LoginAuxili.NOME_USU_LOGADO;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    ddlUnidade.Enabled = txtDataI.Enabled = false;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            decimal codPatr = ddlPatrimonio.SelectedValue != "" ? decimal.Parse(ddlPatrimonio.SelectedValue) : 0;
            int coDepto = ddlDepartamentoDes.SelectedValue != "" ? int.Parse(ddlDepartamentoDes.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coEmpDest = ddlUnidest.SelectedValue != "" ? int.Parse(ddlUnidest.SelectedValue) : 0;
            int coColRespPatri = ddlResponsavelP.SelectedValue != "" ? int.Parse(ddlResponsavelP.SelectedValue) : 0;
            int coDeptoOrigem = 0;

//--------> Retorna o Patrimônio selecionado, para efetuar a alteração do registro
            TB212_ITENS_PATRIMONIO tb212 = TB212_ITENS_PATRIMONIO.RetornaPelaChavePrimaria(codPatr);

            tb212.TB14_DEPTOReference.Load();

            coDeptoOrigem = tb212.TB14_DEPTO.CO_DEPTO;

            TB228_PATRI_HISTO_MOVIM tb228 = new TB228_PATRI_HISTO_MOVIM();

            tb212.TB14_DEPTOReference.Load();
            tb212.TB14_DEPTO1Reference.Load();
            tb212.CO_EMP = coEmp;

            tb212.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(coDepto);

            TB212_ITENS_PATRIMONIO.SaveOrUpdate(tb212, true);

            tb228.TB212_ITENS_PATRIMONIO = tb212;
            tb228.CO_STATUS = ddlStatus.SelectedValue;
            tb228.DT_CADASTRO = Convert.ToDateTime(txtDtCad.Text);
            tb228.DT_MOVIM_PATRI = Convert.ToDateTime(txtDataI.Text);
            tb228.DE_OBSER = txtObs.Text;
            tb228.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            tb228.CO_EMP_ORIGEM = coEmp;
            tb228.CO_DEPTO_ORIGEM = coDeptoOrigem;
            tb228.CO_COL_RESP_PATRI = coColRespPatri != 0 ? (int?)coColRespPatri : null;
            tb228.CO_EMP_RESP_PATRI = coEmp;
            tb228.CO_DEPTO_DESTI = coDepto;
            tb228.CO_EMP_DESTI = coEmpDest;

            TB228_PATRI_HISTO_MOVIM.SaveOrUpdate(tb228, false);

            GestorEntities.CurrentContext.SaveChanges();
            AuxiliPagina.RedirecionaParaPaginaSucesso("Item de Patrimônio Transferido com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            var tb228 = TB228_PATRI_HISTO_MOVIM.RetornaPelaChavePrimaria(QueryStringAuxili.QueryStringValorInt(QueryStrings.Id));

            if (tb228 != null)
            {
                tb228.TB212_ITENS_PATRIMONIOReference.Load();
                tb228.TB03_COLABORReference.Load();

                ddlUnidade.SelectedValue = tb228.CO_EMP_DESTI.ToString();
                ddlPatrimonio.SelectedValue = tb228.TB212_ITENS_PATRIMONIO.COD_PATR.ToString();
                CarregaDadosPatrimonio();
                ddlDepartamentoDes.SelectedValue = tb228.CO_DEPTO_ORIGEM.ToString();
                ddlUnidest.SelectedValue = tb228.CO_EMP_ORIGEM.ToString();
                ddlStatus.SelectedValue = tb228.CO_STATUS;
                txtDtCad.Text = tb228.DT_CADASTRO.ToString("dd/MM/yyyy");
                txtDataI.Text = tb228.DT_MOVIM_PATRI.ToString("dd/MM/yyyy");
                txtObs.Text = tb228.DE_OBSER;
                txtRespMovi.Text = tb228.TB03_COLABOR.NO_COL;
                ddlResponsavelP.SelectedValue = tb228.CO_COL_RESP_PATRI.ToString();
                ddlUnidest.Enabled = ddlDepartamentoDes.Enabled = ddlUnidade.Enabled = ddlPatrimonio.Enabled =
                ddlStatus.Enabled = txtDtCad.Enabled = txtDataI.Enabled = txtObs.Enabled = ddlResponsavelP.Enabled = txtRespMovi.Enabled = false;
            }            
        }        
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Patrimônio
        /// </summary>
        private void CarregaPatrimonio()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlPatrimonio.DataSource = (from lTb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                        where lTb212.CO_EMP == coEmp && lTb212.CO_STATUS != "T"
                                        select new { lTb212.DE_PATR, lTb212.COD_PATR });

            ddlPatrimonio.DataTextField = "DE_PATR";
            ddlPatrimonio.DataValueField = "COD_PATR";
            ddlPatrimonio.DataBind();

            ddlPatrimonio.Items.Insert(0, new ListItem("Selecione", ""));
        }

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

            ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));

            CarregaPatrimonio();
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

            txtDepA.Text = tb212.NO_DEPTO_ATUAL != null ? tb212.NO_DEPTO_ATUAL : tb212.NO_DEPTO_ORIGEM != null ? tb212.NO_DEPTO_ORIGEM : "";
            txtDepO.Text = tb212.NO_DEPTO_ORIGEM != null ? tb212.NO_DEPTO_ORIGEM : "";
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamentos
        /// </summary>
        private void CarregaDepto()
        {
            int coEmp = ddlUnidest.SelectedValue != "" ? int.Parse(ddlUnidest.SelectedValue) : LoginAuxili.CO_EMP;

            ddlDepartamentoDes.DataSource = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                             where tb14.TB25_EMPRESA.CO_EMP == coEmp
                                             select new { tb14.CO_DEPTO, tb14.NO_DEPTO });

            ddlDepartamentoDes.DataTextField = "NO_DEPTO";
            ddlDepartamentoDes.DataValueField = "CO_DEPTO";
            ddlDepartamentoDes.DataBind();

            ddlDepartamentoDes.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionário Responsável pelo Patrimônio
        /// </summary>
        private void CarregaFuncionario()
        {
            int coEmp = ddlUnidest.SelectedValue != "" ? int.Parse(ddlUnidest.SelectedValue) : LoginAuxili.CO_EMP;

            ddlResponsavelP.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                          join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                                          where tb03.TB25_EMPRESA1.CO_EMP == coEmp
                                          select new { tb03.CO_COL, NO_COL = tb03.NO_COL.ToUpper() + " - {" + tb14.NO_DEPTO + "}" }).OrderBy( c => c.NO_COL );

            ddlResponsavelP.DataTextField = "NO_COL";
            ddlResponsavelP.DataValueField = "CO_COL";
            ddlResponsavelP.DataBind();

            ddlResponsavelP.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades de Destino
        /// </summary>
        private void CarregaUE()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (coEmp != 0)
            {
                ddlUnidest.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                         select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

                ddlUnidest.DataTextField = "NO_FANTAS_EMP";
                ddlUnidest.DataValueField = "CO_EMP";
                ddlUnidest.DataBind();

                ddlUnidest.Items.Insert(0, new ListItem("Selecione", ""));

                ddlUnidest.SelectedValue = LoginAuxili.CO_EMP.ToString();
                CarregaDepto();
            }
        }
        #endregion

        protected void ddlUnidest_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepto();
            CarregaFuncionario();
        }

        protected void ddlPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosPatrimonio();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPatrimonio();
        }
    }
}
