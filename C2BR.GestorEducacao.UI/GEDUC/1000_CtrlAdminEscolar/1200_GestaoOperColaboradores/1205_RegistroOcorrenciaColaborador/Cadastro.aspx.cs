//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: REGISTRO DE OCORRÊNCIAS FUNCIONAIS DE COLABORADORES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1205_RegistroOcorrenciaColaborador
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
            if (!Page.IsPostBack)
            {
                CarregaUnidades();
                CarregaTipoOcorrencia();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string now = DateTime.Now.ToString("dd/MM/yyyy");

                    txtDataCadastro.Text = txtDataOcorrencia.Text = now;
                    txtResponsavel.Text = LoginAuxili.NOME_USU_LOGADO;
                    ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
                }

                CarregaColaboradores();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;

            if (DateTime.Parse(txtDataOcorrencia.Text) > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Ocorrência não pode ser maior que data atual.");
                return;
            }

            var dtAtivTb03 = (from iTb03 in TB03_COLABOR.RetornaTodosRegistros()
                             where iTb03.CO_COL == coCol
                             select new { iTb03.DT_INIC_ATIV_COL, iTb03.DT_TERM_ATIV_COL }).FirstOrDefault();

            if (dtAtivTb03 != null)
            {
                if (DateTime.Parse(txtDataOcorrencia.Text) < dtAtivTb03.DT_INIC_ATIV_COL)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Ocorrência não pode ser menor que data de admissão.");
                    return;
                }

                if (dtAtivTb03.DT_TERM_ATIV_COL != null)
                {
                    if (DateTime.Parse(txtDataOcorrencia.Text) > dtAtivTb03.DT_TERM_ATIV_COL)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Ocorrência não pode ser maior que data de demissão.");
                        return;
                    }
                }                
            }

            if (DateTime.Parse(txtDataOcorrencia.Text) > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Ocorrência não pode ser maior que data atual.");
                return;
            }

            TB151_OCORR_COLABOR tb151 = RetornaEntidade();

            tb151.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb151.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            var tb03 = TB03_COLABOR.RetornaPeloCoCol(coCol);
            tb151.TB03_COLABOR = tb03;
            tb03.TB25_EMPRESA1Reference.Load();
            tb151.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.TB25_EMPRESA1.CO_EMP);
            tb151.TB150_TIPO_OCORR = TB150_TIPO_OCORR.RetornaPelaChavePrimaria(ddlTipoOcorrencia.SelectedValue);
            tb151.DT_OCORR = DateTime.Parse(txtDataOcorrencia.Text);  
            tb151.DE_OCORR = txtOcorrencia.Text;
            tb151.DT_CADASTRO = DateTime.Parse(txtDataCadastro.Text);
            tb151.TB03_COLABOR1 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

            CurrentPadraoCadastros.CurrentEntity = tb151;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB151_OCORR_COLABOR tb151 = RetornaEntidade();

            if (tb151 != null)
            {
                tb151.TB03_COLABORReference.Load();
                tb151.TB03_COLABOR1Reference.Load();
                tb151.TB150_TIPO_OCORRReference.Load();

                ddlUnidade.SelectedValue = tb151.TB03_COLABOR.CO_EMP.ToString();
                CarregaColaboradores();
                ddlColaborador.SelectedValue = tb151.TB03_COLABOR.CO_COL.ToString();
                ddlTipoOcorrencia.SelectedValue = tb151.TB150_TIPO_OCORR.CO_SIGL_OCORR;
                txtDataOcorrencia.Text = tb151.DT_OCORR.ToString("dd/MM/yyyy");
                txtOcorrencia.Text = tb151.DE_OCORR;
                txtDataCadastro.Text = tb151.DT_CADASTRO.ToString("dd/MM/yyyy");
                txtResponsavel.Text = tb151.TB03_COLABOR1.NO_COL;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB151_OCORR_COLABOR</returns>
        private TB151_OCORR_COLABOR RetornaEntidade()
        {
            TB151_OCORR_COLABOR tb151 = TB151_OCORR_COLABOR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb151 == null) ? new TB151_OCORR_COLABOR() : tb151;
        }
        #endregion

        #region Validadores

        protected void cvDataOcorrencia_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (txtDataOcorrencia.Text != "")
            {
                if (DateTime.Parse(txtDataOcorrencia.Text) < DateTime.Parse(txtDataCadastro.Text))
                {
                    e.IsValid = false;
                    AuxiliPagina.EnvioMensagemErro(this, "");
                }
            }
        }

        #endregion        

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o DropDown de Unidades
        /// </summary>
        protected void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o DropDown de Tipo de Ocorrência
        /// </summary>
        protected void CarregaTipoOcorrencia()
        {
            ddlTipoOcorrencia.DataSource = TB150_TIPO_OCORR.RetornaTodosRegistros().Where(t => t.TP_USU == "F").OrderBy(t => t.DE_TIPO_OCORR);

            ddlTipoOcorrencia.DataTextField = "DE_TIPO_OCORR";
            ddlTipoOcorrencia.DataValueField = "CO_SIGL_OCORR";
            ddlTipoOcorrencia.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        protected void CarregaColaboradores()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }
    }
}
