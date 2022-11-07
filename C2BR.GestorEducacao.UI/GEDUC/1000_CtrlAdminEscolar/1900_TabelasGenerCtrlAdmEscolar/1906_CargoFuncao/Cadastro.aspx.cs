//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
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
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1906_CargoFuncao
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
            if (Page.IsPostBack) return;

            CarregaGrupoCBO();
            VerificarTipoUnidade();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB15_FUNCAO tb15 = RetornaEntidade();
            tb15.NO_FUN = txtNO_FUN.Text;

            if (!chkCO_FLAG_CLASSI_ADMINI.Checked && !chkCO_FLAG_CLASSI_MAGIST.Checked && !chkCO_FLAG_CLASSI_NUCLEO.Checked && !chkCO_FLAG_CLASSI_OPERAC.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Tipo de Função deve ser informado");
                return;
            }

            tb15.CO_CBO_FUN = txtCBOFuncao.Text;
            tb15.CO_FLAG_CLASSI_ADMINI = chkCO_FLAG_CLASSI_ADMINI.Checked;
            tb15.CO_FLAG_CLASSI_MAGIST = chkCO_FLAG_CLASSI_MAGIST.Checked;
            tb15.CO_FLAG_CLASSI_NUCLEO = chkCO_FLAG_CLASSI_NUCLEO.Checked;
            tb15.CO_FLAG_CLASSI_OPERAC = chkCO_FLAG_CLASSI_OPERAC.Checked;
            tb15.TB316_CBO_GRUPO = TB316_CBO_GRUPO.RetornaPelaChavePrimaria(ddlGrupoCBO.SelectedValue);

            CurrentPadraoCadastros.CurrentEntity = tb15;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB15_FUNCAO tb15 = RetornaEntidade();

            if (tb15 != null)
            {
                tb15.TB316_CBO_GRUPOReference.Load();

                //txtCO_FUN.Text = tb15.CO_FUN.ToString();
                txtNO_FUN.Text = tb15.NO_FUN;
                txtCBOFuncao.Text = tb15.CO_CBO_FUN != null ? tb15.CO_CBO_FUN : "";
                chkCO_FLAG_CLASSI_ADMINI.Checked = tb15.CO_FLAG_CLASSI_ADMINI != null ? tb15.CO_FLAG_CLASSI_ADMINI.Value : false;
                chkCO_FLAG_CLASSI_MAGIST.Checked = tb15.CO_FLAG_CLASSI_MAGIST != null ? tb15.CO_FLAG_CLASSI_MAGIST.Value : false;
                chkCO_FLAG_CLASSI_NUCLEO.Checked = tb15.CO_FLAG_CLASSI_NUCLEO != null ? tb15.CO_FLAG_CLASSI_NUCLEO.Value : false;
                chkCO_FLAG_CLASSI_OPERAC.Checked = tb15.CO_FLAG_CLASSI_OPERAC != null ? tb15.CO_FLAG_CLASSI_OPERAC.Value : false;
                ddlGrupoCBO.SelectedValue = tb15.TB316_CBO_GRUPO != null ? tb15.TB316_CBO_GRUPO.CO_CBO_GRUPO : "";
                ddlGrupoCBO.Enabled = false;
                txtCBOFuncao.Enabled = false;

            }
            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB15_FUNCAO</returns>
        private TB15_FUNCAO RetornaEntidade()
        {
            TB15_FUNCAO tb15 = TB15_FUNCAO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb15 == null) ? new TB15_FUNCAO() : tb15;
        }
        #endregion       
 
        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Grupo CBO
        /// </summary>
        private void CarregaGrupoCBO()
        {
            ddlGrupoCBO.DataSource = (from tb316 in TB316_CBO_GRUPO.RetornaTodosRegistros()
                                      select new 
                                      { 
                                          tb316.CO_CBO_GRUPO, 
                                          DESC = tb316.CO_CBO_GRUPO + " - " + tb316.DE_CBO_GRUPO
                                      }).OrderBy(e => e.DESC);

            ddlGrupoCBO.DataValueField = "CO_CBO_GRUPO";
            ddlGrupoCBO.DataTextField = "DESC";
            ddlGrupoCBO.DataBind();

            ddlGrupoCBO.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        /// <summary>
        /// Verifica o tipo da unidade e altera informacoes especificas
        /// </summary>
        private void VerificarTipoUnidade()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGE":
                    chkCO_FLAG_CLASSI_MAGIST.Text = "Magistério";
                    break;
                case "PGS":
                    chkCO_FLAG_CLASSI_MAGIST.Text = "Saúde";

                    break;

            }
        }
    }
}