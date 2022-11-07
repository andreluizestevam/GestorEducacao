//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: BAIRRO
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
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0913_CadastroInformativos
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
                CarregaFuncionalidades();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                txtResponsavel.Text = LoginAuxili.NOME_USU_LOGADO;
                txtDtCadas.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (ddlTipoURL.SelectedValue == "E")
	        {
                if (txtURLExter.Text == "")
	            {
		            AuxiliPagina.EnvioMensagemErro(this.Page,"URL Externa deve ser informada.");
                    return;
	            }
            }
            else
            {
                if (ddlFuncioURL.SelectedValue == "")
	            {
		            AuxiliPagina.EnvioMensagemErro(this.Page,"Funcionalidade deve ser informada.");
                    return;
	            }
            }

            TB138_INFORMATIVOS tb138 = RetornaEntidade();

            tb138.TP_USUARIO = chkTpFunci.Checked ? "F" : "P";
            tb138.DE_TITUL_PUBLIC = txtTitulPublic.Text;
            tb138.DE_OBS_INFOR = txtObserInfor.Text;
            tb138.TB25_EMPRESA = chkAbrangUnidLog.Checked ? TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP) : null;
            tb138.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            tb138.DT_INICI_PUBLIC = DateTime.Parse(txtDtIniciPublic.Text);
            tb138.DT_FINAL_PUBLIC = DateTime.Parse(txtDtFinalPublic.Text);

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)) 
                tb138.DT_CADAS_INFOR = DateTime.Now;

            tb138.TP_TITUL_URL = ddlTipoURL.SelectedValue;
            tb138.NO_TITUL_URL = txtTitulURL.Text;

            if (ddlTipoURL.SelectedValue == "E")
	        {
		        tb138.IDEADMMODULO = null;
                tb138.CO_URL_EXT = txtURLExter.Text;
	        }
            else
            {
                tb138.IDEADMMODULO = int.Parse(ddlFuncioURL.SelectedValue);
                tb138.CO_URL_EXT = null;
            }
            

            CurrentPadraoCadastros.CurrentEntity = tb138;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB138_INFORMATIVOS tb138 = RetornaEntidade();

            if (tb138 != null)
            {
                tb138.TB25_EMPRESAReference.Load();
                tb138.TB03_COLABORReference.Load();


                chkTpFunci.Checked = tb138.TP_USUARIO == "F";
                chkTpProfe.Checked = tb138.TP_USUARIO == "P";
                txtTitulPublic.Text = tb138.DE_TITUL_PUBLIC;
                txtObserInfor.Text = tb138.DE_OBS_INFOR;
                chkAbrangUnidLog.Checked = tb138.TB25_EMPRESA != null;
                chkAbrangTodasUnid.Checked = tb138.TB25_EMPRESA == null;
                txtDtIniciPublic.Text = tb138.DT_INICI_PUBLIC.ToString("dd/MM/yyyy");
                txtDtFinalPublic.Text = tb138.DT_FINAL_PUBLIC.ToString("dd/MM/yyyy");
                txtDtCadas.Text = tb138.DT_CADAS_INFOR.ToString("dd/MM/yyyy");
                txtResponsavel.Text = tb138.TB03_COLABOR.NO_COL;
                ddlTipoURL.SelectedValue = tb138.TP_TITUL_URL;
                txtTitulURL.Text = tb138.NO_TITUL_URL;

                if (ddlTipoURL.SelectedValue == "E")
                {
                    liDdlFuncioURL.Visible = false;
                    liURLExter.Visible = true;
                    txtURLExter.Text = tb138.CO_URL_EXT;
                }
                else
                {
                    liDdlFuncioURL.Visible = true;
                    liURLExter.Visible = false;
                    CarregaFuncionalidades();
                    ddlFuncioURL.SelectedValue = tb138.IDEADMMODULO.ToString();
                }
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB138_INFORMATIVOS</returns>
        private TB138_INFORMATIVOS RetornaEntidade()
        {
            TB138_INFORMATIVOS tb138 = TB138_INFORMATIVOS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb138 == null) ? new TB138_INFORMATIVOS() : tb138;
        }

        #endregion       

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Funcionalidades
        /// </summary>
        private void CarregaFuncionalidades()
        {
            ddlFuncioURL.DataSource = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                      where admMod.nomURLModulo != null
                                      select new { admMod.ideAdmModulo, admMod.nomItemMenu }).OrderBy( a => a.nomItemMenu );

            ddlFuncioURL.DataTextField = "nomItemMenu";
            ddlFuncioURL.DataValueField = "ideAdmModulo";
            ddlFuncioURL.DataBind();

            ddlFuncioURL.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        protected void ddlTipoURL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoURL.SelectedValue == "E")
            {
                liDdlFuncioURL.Visible = false;
                liURLExter.Visible = true;
            }
            else
            {
                liDdlFuncioURL.Visible = true;
                liURLExter.Visible = false;
            }
        }

        protected void chkAbrangUnidLog_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAbrangUnidLog.Checked)
                chkAbrangTodasUnid.Checked = false;
            else
                chkAbrangTodasUnid.Checked = true;
        }

        protected void chkAbrangTodasUnid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAbrangTodasUnid.Checked)
                chkAbrangUnidLog.Checked = false;
            else
                chkAbrangUnidLog.Checked = true;
        }

        protected void chkTpFunci_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTpFunci.Checked)
                chkTpProfe.Checked = false;
            else
                chkTpProfe.Checked = true;
        }

        protected void chkTpProfe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTpProfe.Checked)
                chkTpFunci.Checked = false;
            else
                chkTpFunci.Checked = true;
        }
    }
}