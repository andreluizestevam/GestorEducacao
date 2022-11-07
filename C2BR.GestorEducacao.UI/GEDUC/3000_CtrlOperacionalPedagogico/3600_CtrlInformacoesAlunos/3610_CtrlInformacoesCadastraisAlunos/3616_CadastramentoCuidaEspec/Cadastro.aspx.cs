//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: CADASTRAMENTO DE MÚLTIPLOS ENDEREÇOS DO ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3616_CadastramentoCuidaEspec
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
                CarregaAlunos();
                CarregaUnidadesMedidas();
                CarregaUf();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    txtDtReceita.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }
        
//====> Chamada do método de preenchimento do formulário da funcionalidade
        private void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        private void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int intRetorno;
            DateTime dataRetorno;

            TB293_CUIDAD_SAUDE tb293 = RetornaEntidade();

            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb293.TB07_ALUNO = refAluno;
            refAluno.TB25_EMPRESA1Reference.Load();
            tb293.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
            tb293.TP_CUIDADO_SAUDE = ddlTpCui.SelectedValue;
            tb293.TP_APLICAC_CUIDADO = ddlTpApli.SelectedValue;
            tb293.HR_APLICAC_CUIDADO = txtHrAplic.Text.Replace(":", "") != "" ? txtHrAplic.Text : null;
            tb293.NM_REMEDIO_CUIDADO = txtDescCEA.Text;
            tb293.DE_OBSERV_CUIDADO = txtObsCEA.Text != "" ? txtObsCEA.Text : null;
            tb293.DE_DOSE_REMEDIO_CUIDADO = int.TryParse(this.txtQtdeCEA.Text, out intRetorno) ? (int?)intRetorno : null;
            tb293.NM_MEDICO_CUIDADO = txtNomeMedCEA.Text != "" ? txtNomeMedCEA.Text : null;
            tb293.NR_CRM_MEDICO_CUIDADO = txtNumCRMCEA.Text != "" ? txtNumCRMCEA.Text : null;
            tb293.CO_UF_MEDICO = ddlUFCEA.SelectedValue != "" ? ddlUFCEA.SelectedValue : null;
            tb293.NR_TELEF_MEDICO = txtTelCEA.Text.Replace("(", "").Replace(")", "").Replace("-", "") != "" ? txtTelCEA.Text.Replace("(", "").Replace(")", "").Replace("-", "") : null;
            tb293.FL_RECEITA_CUIDADO = ddlRecCEA.SelectedValue;
            tb293.TB89_UNIDADES = ddlUniCEA.SelectedValue != "" ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(ddlUniCEA.SelectedValue)) : null;
            tb293.DT_RECEITA_INI = DateTime.TryParse(this.txtDataPeriodoIni.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb293.DT_RECEITA_FIM = DateTime.TryParse(this.txtDataPeriodoFim.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb293.DT_RECEITA_CUIDADO = DateTime.TryParse(this.txtDtReceita.Text, out dataRetorno) ? (DateTime?)dataRetorno : DateTime.Now;
            tb293.CO_STATUS_MEDICAC = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tb293;

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            if (!IsPostBack)
            {
                TB293_CUIDAD_SAUDE tb293 = RetornaEntidade();

                if (tb293 != null)
                {
                    tb293.TB07_ALUNOReference.Load();
                    tb293.TB89_UNIDADESReference.Load();

                    ddlAluno.SelectedValue = tb293.TB07_ALUNO.CO_ALU.ToString();
                    ddlTpCui.SelectedValue = tb293.TP_CUIDADO_SAUDE;
                    ddlTpApli.SelectedValue = tb293.TP_APLICAC_CUIDADO;
                    txtHrAplic.Text = tb293.HR_APLICAC_CUIDADO != null ? tb293.HR_APLICAC_CUIDADO : "";
                    txtDescCEA.Text = tb293.NM_REMEDIO_CUIDADO;
                    txtObsCEA.Text = tb293.DE_OBSERV_CUIDADO != null ? tb293.DE_OBSERV_CUIDADO : "";
                    txtQtdeCEA.Text = tb293.DE_DOSE_REMEDIO_CUIDADO != null ? tb293.DE_DOSE_REMEDIO_CUIDADO.ToString() : "";
                    txtNomeMedCEA.Text = tb293.NM_MEDICO_CUIDADO != null ? tb293.NM_MEDICO_CUIDADO : "";
                    txtNumCRMCEA.Text = tb293.NR_CRM_MEDICO_CUIDADO != null ? tb293.NR_CRM_MEDICO_CUIDADO : "";
                    ddlUFCEA.SelectedValue = tb293.CO_UF_MEDICO != "" ? tb293.CO_UF_MEDICO : "";
                    txtTelCEA.Text = tb293.NR_TELEF_MEDICO != null ? tb293.NR_TELEF_MEDICO : "";
                    ddlRecCEA.SelectedValue = tb293.FL_RECEITA_CUIDADO;
                    ddlUniCEA.SelectedValue = tb293.TB89_UNIDADES != null ? tb293.TB89_UNIDADES.CO_UNID_ITEM.ToString() : "";
                    txtDataPeriodoIni.Text = tb293.DT_RECEITA_INI != null ? tb293.DT_RECEITA_INI.Value.ToString("dd/MM/yyyy") : "";
                    txtDataPeriodoFim.Text = tb293.DT_RECEITA_FIM != null ? tb293.DT_RECEITA_FIM.Value.ToString("dd/MM/yyyy") : "";
                    txtDtReceita.Text = tb293.DT_RECEITA_CUIDADO != null ? tb293.DT_RECEITA_CUIDADO.Value.ToString("dd/MM/yyyy") : "";
                    ddlSituacao.SelectedValue =  tb293.CO_STATUS_MEDICAC;
                }
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB293_CUIDAD_SAUDE</returns>
        private TB293_CUIDAD_SAUDE RetornaEntidade()
        {
            TB293_CUIDAD_SAUDE tb293 = TB293_CUIDAD_SAUDE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb293 == null) ? new TB293_CUIDAD_SAUDE() : tb293;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAlunos()
        {
            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                   where tb07.CO_SITU_ALU.ToUpper() == "A"
                                   select new { tb07.CO_ALU, tb07.NO_ALU, }).OrderBy( a => a.NO_ALU );

            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataTextField  = "NO_ALU";
            ddlAluno.DataBind();
            
            ddlAluno.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades de Medida
        /// </summary>
        private void CarregaUnidadesMedidas()
        {
            ddlUniCEA.DataSource = TB89_UNIDADES.RetornaTodosRegistros().Where(u => (u.FL_CATEG_UNID == "T") || (u.FL_CATEG_UNID == "S")).OrderBy(u => u.SG_UNIDADE);

            ddlUniCEA.DataTextField = "SG_UNIDADE";
            ddlUniCEA.DataValueField = "CO_UNID_ITEM";
            ddlUniCEA.DataBind();

            ddlUniCEA.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de UF
        /// </summary>
        private void CarregaUf()
        {
            ddlUFCEA.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUFCEA.DataTextField = "CODUF";
            ddlUFCEA.DataValueField = "CODUF";
            ddlUFCEA.DataBind();

            ddlUFCEA.Items.Insert(0, new ListItem("", ""));
        }
        #endregion   
    }
}