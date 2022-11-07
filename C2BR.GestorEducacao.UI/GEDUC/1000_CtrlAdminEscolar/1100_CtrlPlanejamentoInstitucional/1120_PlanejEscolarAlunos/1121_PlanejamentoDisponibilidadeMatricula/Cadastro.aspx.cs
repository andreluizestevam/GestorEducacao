//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: CADASTRAMENTO DE GRUPO DE CONTAS CONTÁBIL
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1121_PlanejamentoDisponibilidadeMatricula
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
            if (IsPostBack) return;

            TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

            if (tb000 != null)
            {
                txtInstituicao.Text = tb000.ORG_NOME_ORGAO;
                txtTipoCtrlPlaneFinan.Text = "Unidade Escolar";

                var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                            where iTb25.CO_EMP == LoginAuxili.CO_EMP
                            select new { iTb25.NO_FANTAS_EMP, iTb25.sigla, iTb25.CO_CPFCGC_EMP }).First();

                txtUnidadeEscolar.Text = tb25.NO_FANTAS_EMP;
                txtCodIdenticacao.Text = tb25.sigla.ToUpper();
                txtCNPJ.Text = tb25.CO_CPFCGC_EMP;
            }

            CarregaModalidades();
            CarregaSerieCurso();
            txtAnoRefer.Text = DateTime.Now.Year.ToString();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            tb155_plan_matr tb155 = RetornaEntidade();

            int serie = int.Parse(ddlSerieCurso.SelectedValue);
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int intRetorno = 0;

            if (tb155 == null)
            {
                var planejMatri = (from tb155pM in tb155_plan_matr.RetornaTodosRegistros()
                                   where tb155pM.co_ano_ref.Equals(txtAnoRefer.Text)
                                    && tb155pM.co_cur.Equals(serie)
                                    && tb155pM.co_modu_cur == modalidade
                                   select tb155pM);

                if (planejMatri.Count() > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    AuxiliPagina.EnvioMensagemErro(this, MensagensErro.DadosExistentes);

                tb155 = new tb155_plan_matr();

                tb155.co_ano_ref = txtAnoRefer.Text;
                tb155.co_modu_cur = modalidade;
                tb155.co_cur = serie;
            }

            tb155.vl_plan_mes1 = int.TryParse(txtValorJanei.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes2 = int.TryParse(txtValorFever.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes3 = int.TryParse(txtValorMarco.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes4 = int.TryParse(txtValorAbril.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes5 = int.TryParse(txtValorMaio.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes6 = int.TryParse(txtValorJunho.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes7 = int.TryParse(txtValorJulho.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes8 = int.TryParse(txtValorAgost.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes9 = int.TryParse(txtValorSetem.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes10 = int.TryParse(txtValorOutub.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes11 = int.TryParse(txtValorNovem.Text, out intRetorno) ? intRetorno : 0;
            tb155.vl_plan_mes12 = int.TryParse(txtValorDezem.Text, out intRetorno) ? intRetorno : 0;

            CurrentPadraoCadastros.CurrentEntity = tb155;
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
                txtAnoRefer.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = false;

                txtAnoRefer.Text = tb155.co_ano_ref.ToString();
                ddlModalidade.SelectedValue = tb155.co_modu_cur.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb155.co_cur.ToString();
                txtValorJanei.Text = tb155.vl_plan_mes1.ToString();
                txtValorFever.Text = tb155.vl_plan_mes2.ToString();
                txtValorMarco.Text = tb155.vl_plan_mes3.ToString();
                txtValorAbril.Text = tb155.vl_plan_mes4.ToString();
                txtValorMaio.Text = tb155.vl_plan_mes5.ToString();
                txtValorJunho.Text = tb155.vl_plan_mes6.ToString();
                txtValorJulho.Text = tb155.vl_plan_mes7.ToString();
                txtValorAgost.Text = tb155.vl_plan_mes8.ToString();
                txtValorSetem.Text = tb155.vl_plan_mes9.ToString();
                txtValorOutub.Text = tb155.vl_plan_mes10.ToString();
                txtValorNovem.Text = tb155.vl_plan_mes11.ToString();
                txtValorDezem.Text = tb155.vl_plan_mes12.ToString();
                CalculaValores();
               /* txtQtdeAulasRealJAN.Text = tb155.vl_real_mes1.ToString();
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
                txtQtdeAulasRealDEZ.Text = tb155.vl_real_mes12.ToString();*/
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

        /// <summary>
        /// Método que preenche valor dos campos Total, Total semestre 1 e Total semestre 2
        /// </summary>
        private void CalculaValores()
        {
            int valorSemes1, valorSemes2 = 0;
            int retornaInt = 0;

            valorSemes1 = int.TryParse(txtValorJanei.Text, out retornaInt) ? retornaInt : 0;
            valorSemes1 = valorSemes1 + (int.TryParse(txtValorFever.Text.Replace("_",""), out retornaInt) ? retornaInt : 0);
            valorSemes1 = valorSemes1 + (int.TryParse(txtValorMarco.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);
            valorSemes1 = valorSemes1 + (int.TryParse(txtValorAbril.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);
            valorSemes1 = valorSemes1 + (int.TryParse(txtValorMaio.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);
            valorSemes1 = valorSemes1 + (int.TryParse(txtValorJunho.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);

            valorSemes2 = int.TryParse(txtValorJulho.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0;
            valorSemes2 = valorSemes2 + (int.TryParse(txtValorAgost.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);
            valorSemes2 = valorSemes2 + (int.TryParse(txtValorSetem.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);
            valorSemes2 = valorSemes2 + (int.TryParse(txtValorOutub.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);
            valorSemes2 = valorSemes2 + (int.TryParse(txtValorNovem.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);
            valorSemes2 = valorSemes2 + (int.TryParse(txtValorDezem.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);

            if (valorSemes1 > 0)
            {
                txtValorTotalSemes1.Text = valorSemes1.ToString("");
            }

            if (valorSemes2 > 0)
            {
                txtValorTotalSemes2.Text = valorSemes2.ToString("");
            }

            if (valorSemes1 + valorSemes2 > 0)
            {
                txtTotalMensal.Text = (valorSemes1 + valorSemes2).ToString("");
            }
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

        protected void txtValorJanei_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorFever_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorMarco_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorAbril_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorMaio_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorJunho_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorJulho_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorAgost_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorSetem_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorOutub_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorNovem_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void txtValorDezem_TextChanged(object sender, EventArgs e)
        {
            CalculaValores();
        }

        protected void lnkAtualizaValor_Click(object sender, EventArgs e)
        {
            CalculaValores();
        }
    }
}