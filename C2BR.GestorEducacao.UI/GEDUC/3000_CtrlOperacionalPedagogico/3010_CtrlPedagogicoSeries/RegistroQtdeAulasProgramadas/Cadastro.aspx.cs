//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: REGISTRO DE QUANTIDADE DE AULAS PROGRAMADAS POR SÉRIE/CURSO E MATÉRIA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.RegistroQtdeAulasProgramadas
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

            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaMaterias();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                ddlAno.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlMateria.Enabled = true;
                ddlAno.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFE1");
            }
            else
                CarregaFormulario();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAnoRef = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            int intRetorno = 0;

            TB_QTDE_AULAS tbQtdAulas = RetornaEntidade();

            if (tbQtdAulas == null)
            {
                tbQtdAulas = new TB_QTDE_AULAS();
                tbQtdAulas.CO_EMP = LoginAuxili.CO_EMP;
                tbQtdAulas.CO_ANO_REF = coAnoRef;
                tbQtdAulas.CO_MODU_CUR = modalidade;
                tbQtdAulas.CO_CUR = serie;
                tbQtdAulas.CO_MAT = materia;                
            }

            tbQtdAulas.QT_AULAS_PROG_JAN = int.TryParse(txtValorJanei.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_FEV = int.TryParse(txtValorFever.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_MAR = int.TryParse(txtValorMarco.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_ABR = int.TryParse(txtValorAbril.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_MAI = int.TryParse(txtValorMaio.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_JUN = int.TryParse(txtValorJunho.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_JUL = int.TryParse(txtValorJulho.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_AGO = int.TryParse(txtValorAgost.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_SET = int.TryParse(txtValorSetem.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_OUT = int.TryParse(txtValorOutub.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_NOV = int.TryParse(txtValorNovem.Text, out intRetorno) ? intRetorno : 0;
            tbQtdAulas.QT_AULAS_PROG_DEZ = int.TryParse(txtValorDezem.Text, out intRetorno) ? intRetorno : 0;
           /* tbQtdAulas.QT_AULAS_REAL_JAN = int.TryParse(txtQtdeAulasRealJAN.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_FEV = int.TryParse(txtQtdeAulasRealFEV.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_MAR = int.TryParse(txtQtdeAulasRealMAR.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_ABR = int.TryParse(txtQtdeAulasRealABR.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_MAI = int.TryParse(txtQtdeAulasRealMAI.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_JUN = int.TryParse(txtQtdeAulasRealJUN.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_JUL = int.TryParse(txtQtdeAulasRealJUL.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_AGO = int.TryParse(txtQtdeAulasRealAGO.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_SET = int.TryParse(txtQtdeAulasRealSET.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_OUT = int.TryParse(txtQtdeAulasRealOUT.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_NOV = int.TryParse(txtQtdeAulasRealNOV.Text, out intRetorno) ? (int?)intRetorno : null;
            tbQtdAulas.QT_AULAS_REAL_DEZ = int.TryParse(txtQtdeAulasRealDEZ.Text, out intRetorno) ? (int?)intRetorno : null;*/

            CurrentPadraoCadastros.CurrentEntity = tbQtdAulas;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB_QTDE_AULAS tbQtdAulas = RetornaEntidade();

            if (tbQtdAulas != null)
            {
                ddlAno.Text = tbQtdAulas.CO_ANO_REF.ToString();
                ddlModalidade.SelectedValue = tbQtdAulas.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tbQtdAulas.CO_CUR.ToString();
                CarregaMaterias();
                ddlMateria.SelectedValue = tbQtdAulas.CO_MAT.ToString();

                txtValorJanei.Text = tbQtdAulas.QT_AULAS_PROG_JAN.ToString();
                txtValorFever.Text = tbQtdAulas.QT_AULAS_PROG_FEV.ToString();
                txtValorMarco.Text = tbQtdAulas.QT_AULAS_PROG_MAR.ToString();
                txtValorAbril.Text = tbQtdAulas.QT_AULAS_PROG_ABR.ToString();
                txtValorMaio.Text = tbQtdAulas.QT_AULAS_PROG_MAI.ToString();
                txtValorJunho.Text = tbQtdAulas.QT_AULAS_PROG_JUN.ToString();
                txtValorJulho.Text = tbQtdAulas.QT_AULAS_PROG_JUL.ToString();
                txtValorAgost.Text = tbQtdAulas.QT_AULAS_PROG_AGO.ToString();
                txtValorSetem.Text = tbQtdAulas.QT_AULAS_PROG_SET.ToString();
                txtValorOutub.Text = tbQtdAulas.QT_AULAS_PROG_OUT.ToString();
                txtValorNovem.Text = tbQtdAulas.QT_AULAS_PROG_NOV.ToString();
                txtValorDezem.Text = tbQtdAulas.QT_AULAS_PROG_DEZ.ToString();
               /* txtQtdeAulasRealJAN.Text = tbQtdAulas.QT_AULAS_REAL_JAN.ToString();
                txtQtdeAulasRealFEV.Text = tbQtdAulas.QT_AULAS_REAL_FEV.ToString();
                txtQtdeAulasRealMAR.Text = tbQtdAulas.QT_AULAS_REAL_MAR.ToString();
                txtQtdeAulasRealABR.Text = tbQtdAulas.QT_AULAS_REAL_ABR.ToString();
                txtQtdeAulasRealMAI.Text = tbQtdAulas.QT_AULAS_REAL_MAI.ToString();
                txtQtdeAulasRealJUN.Text = tbQtdAulas.QT_AULAS_REAL_JUN.ToString();
                txtQtdeAulasRealJUL.Text = tbQtdAulas.QT_AULAS_REAL_JUL.ToString();
                txtQtdeAulasRealAGO.Text = tbQtdAulas.QT_AULAS_REAL_AGO.ToString();
                txtQtdeAulasRealSET.Text = tbQtdAulas.QT_AULAS_REAL_SET.ToString();
                txtQtdeAulasRealOUT.Text = tbQtdAulas.QT_AULAS_REAL_OUT.ToString();
                txtQtdeAulasRealNOV.Text = tbQtdAulas.QT_AULAS_REAL_NOV.ToString();
                txtQtdeAulasRealDEZ.Text = tbQtdAulas.QT_AULAS_REAL_DEZ.ToString();*/
                CalculaValores();
                ddlAno.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlMateria.Enabled = false;
            }       
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB_QTDE_AULAS</returns>
        private TB_QTDE_AULAS RetornaEntidade()
        {
            return TB_QTDE_AULAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur), 
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Ano),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoMat));
        }

        /// <summary>
        /// Método que preenche valor dos campos Total, Total semestre 1 e Total semestre 2
        /// </summary>
        private void CalculaValores()
        {
            int valorSemes1, valorSemes2 = 0;
            int retornaInt = 0;

            valorSemes1 = int.TryParse(txtValorJanei.Text, out retornaInt) ? retornaInt : 0;
            valorSemes1 = valorSemes1 + (int.TryParse(txtValorFever.Text.Replace("_", ""), out retornaInt) ? retornaInt : 0);
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
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, modalidade, serie)
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy( m => m.NO_MATERIA );

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "CO_MAT";
            ddlMateria.DataBind();

            if (ddlMateria.Items.Count <= 0) ddlMateria.Items.Clear();

            ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
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
