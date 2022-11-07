//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PROGRAMAS SOCIAIS
// OBJETIVO: CADASTRAMENTO DE PROGRAMAS SOCIAIS
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
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1600_ProgramasSociais.F1601_CadasProgrSociais
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
                CarregaTipoProgramaConvenio();
                CarregaUnidade();

                int coEmp;

                if (int.TryParse(ddlUnidadeResponsavel.SelectedValue, out coEmp) && coEmp > 0)
                    CarregaColaborador(coEmp, ddlColaboradorResponsavel);
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataCadastro.Text = txtDataSituacao.Text = dataAtual;
                txtInstituicao.Text = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO).ORG_NOME_ORGAO;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                decimal retornaDecimal;
                double retornaDouble;
                int retornaInt;

                TB135_PROG_SOCIAIS tb135 = RetornaEntidade();

                // Verifica se existe algum aluno vinculado ao Programa Social
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                {
                    int count = (from tb136 in TB136_ALU_PROG_SOCIAIS.RetornaTodosRegistros()
                                 where tb136.CO_IDENT_PROGR_SOCIA == tb135.CO_IDENT_PROGR_SOCIA
                                 select new
                                 {
                                     tb136.CO_IDENT_PROGR_SOCIA
                                 }).Count();

                    if (count > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Existem pessoas vinculadas ao Programa/Convênio.");
                        return;
                    }
                    else
                    {
                        CurrentCadastroMasterPage.CurrentEntity = tb135;
                        return;
                    }
                }

                if (tb135 == null)
                {
                    tb135 = new TB135_PROG_SOCIAIS();

                    tb135.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    tb135.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb135.DT_CADAS_PROG_SOCIA = DateTime.Now;
                }

                tb135.NO_PROGR_SOCIA = txtNomeProgramaSocial.Text;
                tb135.NO_REDUZ_PROGR_SOCIA = txtNomeReduzidoProgramaSocial.Text;
                tb135.CO_SIGLA_PROGR_SOCIA = txtSigla.Text.Trim().ToUpper();
                tb135.TP_PROGR_SOCIA = ddlTipoBeneficio.SelectedValue;
                tb135.PE_FREQU_PROGR_SOCIA = txtPercentFrequencia.Text != "" && decimal.TryParse(txtPercentFrequencia.Text, out retornaDecimal) ? retornaDecimal : (decimal?)null;
                tb135.VL_RENDA_FAMIL_PROGR_SOCIA = txtRendaFamiliar.Text != "" && double.TryParse(txtRendaFamiliar.Text, out retornaDouble) ? retornaDouble : (double?)null;
                tb135.QT_DEPEN_PROGR_SOCIA = txtQtDependentes.Text != "" && int.TryParse(txtQtDependentes.Text, out retornaInt) ? retornaInt : (int?)null;
                tb135.NU_CONTR_PROGR_SOCIA = txtNumeroContrato.Text != "" && int.TryParse(txtNumeroContrato.Text, out retornaInt) ? retornaInt : (int?)null;
                tb135.NU_CONVE_PROGR_SOCIA = txtNumeroConvenio.Text != "" && int.TryParse(txtNumeroConvenio.Text, out retornaInt) ? retornaInt : (int?)null;
                tb135.DT_VALID_CONVE_PROG_SOCIA = txtDataValidadePrograma.Text != "" ? DateTime.Parse(txtDataValidadePrograma.Text) : (DateTime?)null;
                tb135.CO_STATUS_PROGR_SOCIA = ddlSituacao.SelectedValue;
                tb135.DT_STATUS_PROGR_SOCIA = DateTime.Now;
                tb135.NO_ORGAO_PROG = txtNomeOrgaoGestor.Text != "" ? txtNomeOrgaoGestor.Text : null;
                tb135.CO_SIGLA_ORGAO_PROG = txtSiglaOrgaoGestor.Text != "" ? txtSiglaOrgaoGestor.Text : null;
                tb135.NO_RESP_ORGAO_PROG = txtResponsavelOrgaoGestor.Text != "" ? txtResponsavelOrgaoGestor.Text : null;
                tb135.NU_TELE_RESP_ORGAO_PROG = txtTelRespOrgaoGestor.Text != "" ? txtTelRespOrgaoGestor.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "") : null;
                tb135.CO_EMAI_RESP_ORGAO_PROG = txtEmailRespOrgaoGestor.Text != "" ? txtEmailRespOrgaoGestor.Text : null;
                tb135.NO_DEPTO_RESP_ORGAO_PROG = txtDeptoRespOrgaoGestor.Text != "" ? txtDeptoRespOrgaoGestor.Text : null;
                tb135.DE_OBJETIVO = txtObjetivo.Text != "" ? txtObjetivo.Text : null;
                tb135.DT_CONTRATO = DateTime.Parse(txtDataContrato.Text);
                tb135.DT_PREV = DateTime.Parse(txtDataPrevisao.Text);
                tb135.DT_TERM = txtDataTermino.Text != "" ? DateTime.Parse(txtDataTermino.Text) : (DateTime?)null;

                tb135.TB215_PROGR_TIPO_SOCEDU = TB215_PROGR_TIPO_SOCEDU.RetornaPelaChavePrimaria(int.Parse(ddlTipoProgramaConvenio.SelectedValue));
                tb135.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlColaboradorResponsavel.SelectedValue));

                CurrentCadastroMasterPage.CurrentEntity = tb135;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB135_PROG_SOCIAIS tb135 = RetornaEntidade();

            if (tb135 != null)
            {
                tb135.TB000_INSTITUICAOReference.Load();
                tb135.TB03_COLABORReference.Load();
                tb135.TB25_EMPRESAReference.Load();
                tb135.TB215_PROGR_TIPO_SOCEDUReference.Load();

                txtInstituicao.Text = tb135.TB000_INSTITUICAO.ORG_NOME_ORGAO;
                txtDataCadastro.Text = tb135.DT_CADAS_PROG_SOCIA.ToString("dd/MM/yyyy");

                txtNomeProgramaSocial.Text = tb135.NO_PROGR_SOCIA;
                txtNomeReduzidoProgramaSocial.Text = tb135.NO_REDUZ_PROGR_SOCIA;
                txtSigla.Text = tb135.CO_SIGLA_PROGR_SOCIA;
                ddlTipoBeneficio.SelectedValue = tb135.TP_PROGR_SOCIA;
                txtPercentFrequencia.Text = tb135.PE_FREQU_PROGR_SOCIA.HasValue ? tb135.PE_FREQU_PROGR_SOCIA.ToString() : "";
                txtRendaFamiliar.Text = tb135.VL_RENDA_FAMIL_PROGR_SOCIA.HasValue ? string.Format("{0:N2}", tb135.VL_RENDA_FAMIL_PROGR_SOCIA) : "";
                txtQtDependentes.Text = tb135.QT_DEPEN_PROGR_SOCIA.HasValue ? tb135.QT_DEPEN_PROGR_SOCIA.ToString() : "";
                txtNumeroContrato.Text = tb135.NU_CONTR_PROGR_SOCIA.HasValue ? tb135.NU_CONTR_PROGR_SOCIA.ToString() : "";
                txtNumeroConvenio.Text = tb135.NU_CONVE_PROGR_SOCIA.HasValue ? tb135.NU_CONVE_PROGR_SOCIA.ToString() : "";
                txtDataValidadePrograma.Text = tb135.DT_VALID_CONVE_PROG_SOCIA.HasValue ? tb135.DT_VALID_CONVE_PROG_SOCIA.Value.ToString("dd/MM/yyyy") : "";
                ddlSituacao.SelectedValue = tb135.CO_STATUS_PROGR_SOCIA;
                txtDataSituacao.Text = tb135.DT_STATUS_PROGR_SOCIA.ToString("dd/MM/yyyy");
                txtNomeOrgaoGestor.Text = tb135.NO_ORGAO_PROG;
                txtSiglaOrgaoGestor.Text = tb135.CO_SIGLA_ORGAO_PROG;
                txtResponsavelOrgaoGestor.Text = tb135.NO_RESP_ORGAO_PROG;
                txtTelRespOrgaoGestor.Text = tb135.NU_TELE_RESP_ORGAO_PROG;
                txtEmailRespOrgaoGestor.Text = tb135.CO_EMAI_RESP_ORGAO_PROG;
                txtDeptoRespOrgaoGestor.Text = tb135.NO_DEPTO_RESP_ORGAO_PROG;
                txtObjetivo.Text = tb135.DE_OBJETIVO;
                txtDataContrato.Text = tb135.DT_CONTRATO.ToString("dd/MM/yyyy");
                txtDataPrevisao.Text = tb135.DT_PREV.ToString("dd/MM/yyyy");
                txtDataTermino.Text = tb135.DT_TERM.HasValue ? tb135.DT_TERM.Value.ToString("dd/MM/yyyy") : "";

                ddlTipoProgramaConvenio.SelectedValue = tb135.TB215_PROGR_TIPO_SOCEDU.CO_PROGR_TP_SOCEDU.ToString();
                ddlUnidadeResponsavel.SelectedValue = tb135.TB03_COLABOR.CO_EMP.ToString();
                ddlColaboradorResponsavel.SelectedValue = tb135.TB03_COLABOR.CO_COL.ToString();
            }        
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB135_PROG_SOCIAIS</returns>
        private TB135_PROG_SOCIAIS RetornaEntidade()
        {
            return TB135_PROG_SOCIAIS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Programa de Convênio
        /// </summary>
        private void CarregaTipoProgramaConvenio()
        {
            ddlTipoProgramaConvenio.DataSource = (from tb215 in TB215_PROGR_TIPO_SOCEDU.RetornaTodosRegistros()
                                                  where tb215.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                                  select new { tb215.CO_PROGR_TP_SOCEDU, tb215.NO_PROGR_TP_SOCEDU }).OrderBy( p => p.NO_PROGR_TP_SOCEDU );

            ddlTipoProgramaConvenio.DataValueField = "CO_PROGR_TP_SOCEDU";
            ddlTipoProgramaConvenio.DataTextField = "NO_PROGR_TP_SOCEDU";
            ddlTipoProgramaConvenio.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidade
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidadeResponsavel.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                                where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                                select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidadeResponsavel.DataValueField = "CO_EMP";
            ddlUnidadeResponsavel.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeResponsavel.DataBind();

            ddlUnidadeResponsavel.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        /// <param name="unidade">Id da unidade</param>
        /// <param name="ddlColabor">Drop Down de colaborador</param>
        private void CarregaColaborador(int unidade, DropDownList ddlColabor)
        {
            ddlColabor.Items.Clear();

            ddlColabor.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                       where tb03.TB25_EMPRESA1.CO_EMP == unidade
                                       select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlColabor.DataValueField = "CO_COL";
            ddlColabor.DataTextField = "NO_COL";
            ddlColabor.DataBind();
        }
        #endregion

        protected void ddlUnidadeResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp;

            if (int.TryParse(ddlUnidadeResponsavel.SelectedValue, out coEmp) && coEmp > 0)
                CarregaColaborador(coEmp, ddlColaboradorResponsavel);
        }      
    }
}