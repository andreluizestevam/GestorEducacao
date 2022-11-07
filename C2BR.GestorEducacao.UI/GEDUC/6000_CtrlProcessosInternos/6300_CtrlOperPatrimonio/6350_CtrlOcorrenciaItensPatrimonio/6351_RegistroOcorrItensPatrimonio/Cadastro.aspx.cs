//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMÔNIO
// SUBMÓDULO: REGISTRO DE OCORRÊNCIA DE ITENS DE PATRIMÔNIO
// OBJETIVO: OCORRÊNCIAS DE ITENS DE PATRIMÔNIO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6350_CtrlOcorrenciaItensPatrimonio.F6351_RegistroOcorrItensPatrimonio
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
            if (!Page.IsPostBack)
            {
                CarregaUnidade();
                CarregaTipoOcorrencia();                
                CarregaPatrimonio();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");

                    txtDataCadastro.Text = txtDataOcorrencia.Text = dataAtual;
                    txtResponsavel.Text = LoginAuxili.NOME_USU_LOGADO;
                    ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                int idTipoOcorrPatr = ddlTipoOcorrencia.SelectedValue != "" ? int.Parse(ddlTipoOcorrencia.SelectedValue) : 0;
                decimal codPatr = ddlPatrimonio.SelectedValue != "" ? decimal.Parse(ddlPatrimonio.SelectedValue) : 0;

                TB230_OCORR_PATRIMONIO tb230 = RetornaEntidade();

                tb230.TB212_ITENS_PATRIMONIO = TB212_ITENS_PATRIMONIO.RetornaPelaChavePrimaria(codPatr);
                tb230.TB229_TIPO_OCOR_PATRIMONIO = TB229_TIPO_OCOR_PATRIMONIO.RetornaPelaChavePrimaria(idTipoOcorrPatr);
                tb230.DT_OCORR = Convert.ToDateTime(txtDataOcorrencia.Text);
                tb230.DT_SITU_OCORR = Convert.ToDateTime(txtDataOcorrencia.Text);
                tb230.DE_OCORR = txtOcorrencia.Text;
                tb230.DT_CADASTRO = Convert.ToDateTime(txtDataCadastro.Text);
                tb230.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);

                CurrentCadastroMasterPage.CurrentEntity = tb230;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB230_OCORR_PATRIMONIO tb230 = RetornaEntidade();

            if (tb230 != null)
            {
                tb230.TB212_ITENS_PATRIMONIOReference.Load();
                tb230.TB229_TIPO_OCOR_PATRIMONIOReference.Load();
                tb230.TB03_COLABORReference.Load();

                txtOcorrencia.Text = tb230.DE_OCORR;
                txtDataOcorrencia.Text = tb230.DT_OCORR.ToString("dd/MM/yyyy");
                txtDataCadastro.Text = tb230.DT_CADASTRO.ToString("dd/MM/yyyy");
                ddlUnidade.SelectedValue = tb230.TB212_ITENS_PATRIMONIO.CO_EMP.ToString();
                CarregaPatrimonio();
                ddlPatrimonio.SelectedValue = tb230.TB212_ITENS_PATRIMONIO.COD_PATR.ToString();
                txtResponsavel.Text = tb230.TB03_COLABOR.NO_COL;
                ddlTipoOcorrencia.SelectedValue = tb230.TB229_TIPO_OCOR_PATRIMONIO.ID_TIPO_OCORR_PATR.ToString();
                ddlUnidade.Enabled = ddlPatrimonio.Enabled = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB230_OCORR_PATRIMONIO</returns>
        private TB230_OCORR_PATRIMONIO RetornaEntidade()
        {
            TB230_OCORR_PATRIMONIO tb230 = TB230_OCORR_PATRIMONIO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

            return (tb230 == null) ? new TB230_OCORR_PATRIMONIO() : tb230;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = TB25_EMPRESA.RetornaPeloUsuario(LoginAuxili.CO_COL);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Ocorrência
        /// </summary>
        private void CarregaTipoOcorrencia()
        {
            ddlTipoOcorrencia.DataSource = TB229_TIPO_OCOR_PATRIMONIO.RetornaTodosRegistros();

            ddlTipoOcorrencia.DataTextField = "DE_TIPO_OCORR";
            ddlTipoOcorrencia.DataValueField = "ID_TIPO_OCORR_PATR";
            ddlTipoOcorrencia.DataBind();

            ddlTipoOcorrencia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Patrimônios
        /// </summary>
        protected void CarregaPatrimonio()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlPatrimonio.DataSource = (from tb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                        where tb212.CO_EMP == coEmp && tb212.CO_STATUS != "T"
                                        select new { tb212.DE_PATR, tb212.COD_PATR });

            ddlPatrimonio.DataTextField = "DE_PATR";
            ddlPatrimonio.DataValueField = "COD_PATR";
            ddlPatrimonio.DataBind();

            ddlPatrimonio.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        #region Validadores

        protected void cvDataOcorrencia_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (txtDataOcorrencia.Text != "")
            {
                if (DateTime.Parse(txtDataOcorrencia.Text) > DateTime.Parse(txtDataCadastro.Text))
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPatrimonio();
        }
    }
}
