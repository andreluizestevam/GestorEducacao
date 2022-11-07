//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE OPERACIONAL DE REGIÕES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE EQUIPES DE REGIÕES DE ENSINO ESCOLAR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3800_CtrlEncerramentoLetivo.F3810_CtrlRegioesEnsino.F3812_CadastramentoRegiaoEquipeEnsino
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
                string dataAtual = DateTime.Now.Date.ToString("dd/MM/yyyy");                
                CarregaDropDown();
                txtDataS.Text = txtDataCadastro.Text = dataAtual;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB_EQUIPE_NUCLEO_INST tbEquiNucInst = RetornaEntidade();

            if (tbEquiNucInst == null)
                tbEquiNucInst = new TB_EQUIPE_NUCLEO_INST();

//--------> Faz a validação para saber se data inicial é maior que data final
            if (txtDataF.Text != "")
            {
                if (Convert.ToDateTime(txtDataI.Text) > Convert.ToDateTime(txtDataF.Text))
                    AuxiliPagina.EnvioMensagemErro(this, "Data Final da Atividade não pode ser menor que a Data Inicial");
            }
            else
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    int coCol = ddlFuncionario.SelectedValue != "" ? int.Parse(ddlFuncionario.SelectedValue) : 0;
                    int coNucleo = ddlNucleo.SelectedValue != "" ? int.Parse(ddlNucleo.SelectedValue) : 0;
                    int coFun = ddlFuncao.SelectedValue != "" ? int.Parse(ddlFuncao.SelectedValue) : 0;

                    if (coNucleo == 0 || coCol == 0 || coFun == 0 )
                        AuxiliPagina.EnvioMensagemErro(this, "Núcleo, Função e Funcionário são campos obrigatórios");
                    else
                    {
                        tbEquiNucInst.CO_NUCLEO = coNucleo;
                        tbEquiNucInst.CO_FUN = coFun;
                        tbEquiNucInst.DT_CADASTRO = DateTime.Now.Date;
                        tbEquiNucInst.CO_COL = coCol;
                        tbEquiNucInst.CO_EMP_COL = TB03_COLABOR.RetornaPeloCoCol(coCol).CO_EMP;
                        tbEquiNucInst.TB_NUCLEO_INST = TB_NUCLEO_INST.RetornaPeloCoNucleo(coNucleo);
                    }
                }

            tbEquiNucInst.NU_TELEFONE = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tbEquiNucInst.DT_INICIO_ATIV = Convert.ToDateTime(txtDataI.Text).Date;
            if (txtDataF.Text != "")
                tbEquiNucInst.DT_FIM_ATIV = Convert.ToDateTime(txtDataF.Text).Date;
            tbEquiNucInst.DT_STATUS = Convert.ToDateTime(txtDataS.Text).Date;
            tbEquiNucInst.CO_STATUS = ddlSituacao.SelectedValue.ToString();
            
            CurrentPadraoCadastros.CurrentEntity = tbEquiNucInst;
        }       

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB_EQUIPE_NUCLEO_INST tbEquiNucInst = RetornaEntidade();

            if (tbEquiNucInst != null)
            {
                ddlFuncionario.SelectedValue = tbEquiNucInst.CO_COL.ToString();
                ddlNucleo.SelectedValue = tbEquiNucInst.CO_NUCLEO.ToString();
                ddlFuncao.SelectedValue = tbEquiNucInst.CO_FUN.ToString();
                txtDataCadastro.Text = tbEquiNucInst.DT_CADASTRO.ToString("dd/MM/yyyy");
                txtDataI.Text = tbEquiNucInst.DT_INICIO_ATIV.ToString("dd/MM/yyyy");
                if (!String.IsNullOrEmpty(tbEquiNucInst.DT_FIM_ATIV.ToString()))
                    txtDataF.Text = tbEquiNucInst.DT_FIM_ATIV.Value.ToString("dd/MM/yyyy");

                txtDataS.Text = tbEquiNucInst.DT_STATUS.ToString("dd/MM/yyyy");
                ddlSituacao.SelectedValue = tbEquiNucInst.CO_STATUS.ToString();
                txtTelefone.Text = tbEquiNucInst.NU_TELEFONE.ToString();

                ddlFuncionario.Enabled = ddlNucleo.Enabled = false;                
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB_EQUIPE_NUCLEO_INST</returns>
        private TB_EQUIPE_NUCLEO_INST RetornaEntidade()
        {
            return TB_EQUIPE_NUCLEO_INST.RetornaPeloCoEquipNucleo(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Função, Núcleo e Funcionário
        /// </summary>
        private void CarregaDropDown()
        {
            ddlFuncao.DataSource = TB15_FUNCAO.RetornaTodosRegistros();

            ddlFuncao.DataTextField = "NO_FUN";
            ddlFuncao.DataValueField = "CO_FUN";
            ddlFuncao.DataBind();

            ddlFuncao.Items.Insert(0, new ListItem("Selecione", ""));

            ddlNucleo.DataSource = TB_NUCLEO_INST.RetornaTodosRegistros();

            ddlNucleo.DataTextField = "DE_NUCLEO";
            ddlNucleo.DataValueField = "CO_NUCLEO";
            ddlNucleo.DataBind();

            ddlNucleo.Items.Insert(0, new ListItem("Selecione", ""));

            ddlFuncionario.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlFuncionario.DataTextField = "NO_COL";
            ddlFuncionario.DataValueField = "CO_COL";
            ddlFuncionario.DataBind();

            ddlFuncionario.Items.Insert(0, new ListItem("Selecione", ""));

        }
        #endregion
    }
}
