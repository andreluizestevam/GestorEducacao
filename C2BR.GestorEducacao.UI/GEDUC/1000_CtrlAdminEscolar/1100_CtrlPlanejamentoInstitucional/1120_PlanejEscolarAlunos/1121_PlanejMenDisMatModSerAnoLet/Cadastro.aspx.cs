//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (CENÁRIO ALUNOS)
// OBJETIVO: PLANEJAMENTO MENSAL DE DISPONIBILIDADE DE MATRÍCULAS POR MODALIDADE/SÉRIE POR ANO LETIVO
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
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1121_PlanejMenDisMatModSerAnoLet
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis
        
        int anoReferQS = 0;

        #endregion

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            anoReferQS = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("ano");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaModalidades();
                CarregaSerieCurso();
                txtAno.Text = DateTime.Now.Year.ToString();
                txtAno.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = true;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            tb155_plan_matr tb155 = RetornaEntidade();
            int serie = Convert.ToInt32(ddlSerieCurso.SelectedValue);
            int modalidade = Convert.ToInt32(ddlModalidade.SelectedValue);
            int intRetorno = 0;

            var planejMatri = (from tb155pM in tb155_plan_matr.RetornaTodosRegistros()
                               where tb155pM.co_ano_ref.Equals(txtAno.Text)
                                && tb155pM.co_cur.Equals(serie)
                                && tb155pM.co_modu_cur == modalidade
                               select tb155pM);

            if (planejMatri.Count() > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.EnvioMensagemErro(this, MensagensErro.DadosExistentes);
            else
            {
                if (tb155 == null)
                {
                    tb155 = new tb155_plan_matr();

                    tb155.co_ano_ref = txtAno.Text;
                    tb155.co_modu_cur = modalidade;
                    tb155.co_cur = serie;                    
                }

                tb155.vl_plan_mes1 = int.TryParse(txtQtdeAulasProgJAN.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes2 = int.TryParse(txtQtdeAulasProgFEV.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes3 = int.TryParse(txtQtdeAulasProgMAR.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes4 = int.TryParse(txtQtdeAulasProgABR.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes5 = int.TryParse(txtQtdeAulasProgMAI.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes6 = int.TryParse(txtQtdeAulasProgJUN.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes7 = int.TryParse(txtQtdeAulasProgJUL.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes8 = int.TryParse(txtQtdeAulasProgAGO.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes9 = int.TryParse(txtQtdeAulasProgSET.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes10 = int.TryParse(txtQtdeAulasProgOUT.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes11 = int.TryParse(txtQtdeAulasProgNOV.Text, out intRetorno) ? intRetorno : 0;
                tb155.vl_plan_mes12 = int.TryParse(txtQtdeAulasProgDEZ.Text, out intRetorno) ? intRetorno : 0;

                CurrentPadraoCadastros.CurrentEntity = tb155;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            tb155_plan_matr tb155 = RetornaEntidade();

            if (tb155 != null)
            {
                txtAno.Text = tb155.co_ano_ref.ToString();
                ddlModalidade.SelectedValue = tb155.co_modu_cur.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb155.co_cur.ToString();
                txtQtdeAulasProgJAN.Text = tb155.vl_plan_mes1.ToString();
                txtQtdeAulasProgFEV.Text = tb155.vl_plan_mes2.ToString();
                txtQtdeAulasProgMAR.Text = tb155.vl_plan_mes3.ToString();
                txtQtdeAulasProgABR.Text = tb155.vl_plan_mes4.ToString();
                txtQtdeAulasProgMAI.Text = tb155.vl_plan_mes5.ToString();
                txtQtdeAulasProgJUN.Text = tb155.vl_plan_mes6.ToString();
                txtQtdeAulasProgJUL.Text = tb155.vl_plan_mes7.ToString();
                txtQtdeAulasProgAGO.Text = tb155.vl_plan_mes8.ToString();
                txtQtdeAulasProgSET.Text = tb155.vl_plan_mes9.ToString();
                txtQtdeAulasProgOUT.Text = tb155.vl_plan_mes10.ToString();
                txtQtdeAulasProgNOV.Text = tb155.vl_plan_mes11.ToString();
                txtQtdeAulasProgDEZ.Text = tb155.vl_plan_mes12.ToString();
                txtQtdeAulasRealJAN.Text = tb155.vl_real_mes1.ToString();
                txtQtdeAulasRealFEV.Text = tb155.vl_real_mes2.ToString();
                txtQtdeAulasRealMAR.Text = tb155.vl_real_mes3.ToString();
                txtQtdeAulasRealABR.Text = tb155.vl_real_mes4.ToString();
                txtQtdeAulasRealMAI.Text = tb155.vl_real_mes5.ToString();
                txtQtdeAulasRealJUN.Text = tb155.vl_real_mes6.ToString();
                txtQtdeAulasRealJUL.Text = tb155.vl_real_mes7.ToString();
                txtQtdeAulasRealAGO.Text = tb155.vl_real_mes8.ToString();
                txtQtdeAulasRealSET.Text = tb155.vl_real_mes9.ToString();
                txtQtdeAulasRealOUT.Text = tb155.vl_real_mes10.ToString();
                txtQtdeAulasRealNOV.Text = tb155.vl_real_mes11.ToString();
                txtQtdeAulasRealDEZ.Text = tb155.vl_real_mes12.ToString();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade tb155_plan_matr</returns>
        private tb155_plan_matr RetornaEntidade()
        {
            return tb155_plan_matr.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur),
                                                       QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano));
        }        
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.NO_CUR }).OrderBy(c => c.NO_CUR);

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();          
        }
    }
}
