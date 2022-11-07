//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: REGISTRO DE OCORRÊNCIAS DE SAÚDE (ATESTADOS MÉDICOS) DO COLABORADOR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1206_RegistroOcorrenciaSaudeColaborador
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
            
            CarregaCID();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string now = DateTime.Now.ToString("dd/MM/yyyy");

                CarregaUnidades();
                ddlUnidade.Enabled = ddlColaborador.Enabled = true;
                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
                CarregaColaboradores();
                CarregaResponsavel(LoginAuxili.CO_COL);
                txtDataCadastro.Text = txtDataEntrega.Text = now;
            }
            else
            {
                txtDataConsulta.Enabled = txtDiasLicenca.Enabled = false;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int coColResp = ddlResponsavel.SelectedValue != "" ? int.Parse(ddlResponsavel.SelectedValue) : 0;
            int ideCID = ddlCodigoDoenca.SelectedValue != "" ? int.Parse(ddlCodigoDoenca.SelectedValue) : 0;

            if (DateTime.Parse(txtDataConsulta.Text) > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data da consulta não pode ser superior a data atual.");
                return;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                DateTime dtInicio = DateTime.Parse(txtDataConsulta.Text);
                DateTime dtFim = dtInicio.AddDays(int.Parse(txtDiasLicenca.Text) - 1);

                var ocoPres = (from iTb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                               where iTb199.DT_FREQ >= dtInicio && iTb199.DT_FREQ <= dtFim
                               && iTb199.TB03_COLABOR.CO_COL == coCol && iTb199.FLA_PRESENCA == "S"
                               select iTb199);

                if (ocoPres.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Colaborador já apresenta registros para intervalo de data informado.");
                    return;
                }                

                for (int i = 0; i <= dtFim.Subtract(dtInicio).Days; i++)
                {         
                    if ((dtInicio.AddDays(i).DayOfWeek != DayOfWeek.Saturday) && (dtInicio.AddDays(i).DayOfWeek != DayOfWeek.Sunday))
	                {
                        TB199_FREQ_FUNC tb199 = new TB199_FREQ_FUNC();

                        tb199.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(coCol);
                        tb199.TB03_COLABOR1 = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                        tb199.DT_FREQ = dtInicio.AddDays(i);
                        tb199.HR_FREQ = int.Parse(DateTime.Now.ToString("HH:mm").Replace(":", ""));
                        tb199.CO_SEQ_FREQ = 1;
                        tb199.CO_EMP_ATIV = LoginAuxili.CO_EMP;
                        tb199.TP_FREQ = "F";
                        tb199.FL_JUSTI_FALTA = "S";
                        tb199.DE_JUSTI_FALTA = "ATESTADO - CRM: " + txtCrm.Text + " - Data Consulta: " + txtDataConsulta.Text + " )";
                        tb199.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coUnid);
                        tb199.FLA_PRESENCA = "N";
                        tb199.DT_CADASTRO = DateTime.Now;
                        tb199.STATUS = "A";

                        TB199_FREQ_FUNC.SaveOrUpdate(tb199, true);
	                }
                }
            }

            TB143_ATEST_MEDIC tb143 = RetornaEntidade();

            if (tb143.ORG_CODIGO_ORGAO == 0) 
            {
                tb143.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            }

            tb143.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(coColResp);
            tb143.CO_USU = coCol;
            tb143.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb143.NO_MEDIC = txtMedico.Text;
            tb143.NU_CRM_MEDIC = txtCrm.Text;
            tb143.DE_HOSPI_CONSU = txtHospital.Text;
            tb143.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(ideCID);
            tb143.DE_DOENC = txtDoenca.Text;
            tb143.DT_CONSU = DateTime.Parse(txtDataConsulta.Text);
            tb143.QT_DIAS_LICEN = txtDiasLicenca.Text != "" ?(int?)int.Parse(txtDiasLicenca.Text) : null;
            tb143.DE_OBS = txtObservacao.Text;
            tb143.DT_ENTRE_ATEST = DateTime.Parse(txtDataEntrega.Text);
            tb143.DT_CADAS = DateTime.Parse(txtDataCadastro.Text);            
            tb143.TP_USU = "F";            

            CurrentPadraoCadastros.CurrentEntity = tb143;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB143_ATEST_MEDIC tb143 = RetornaEntidade();

            if (tb143 != null)
            {
                tb143.TB03_COLABORReference.Load();
                tb143.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();
                var tb03 = TB03_COLABOR.RetornaPeloCoCol(tb143.CO_USU);
                tb03.TB25_EMPRESA1Reference.Load();
                ddlUnidade.Items.Insert(0, new ListItem(tb03.TB25_EMPRESA1.NO_FANTAS_EMP,tb03.TB25_EMPRESA1.CO_EMP.ToString()));
                CarregaColaboradores();
                ddlColaborador.SelectedValue = tb143.CO_USU.ToString();
                txtMedico.Text = tb143.NO_MEDIC;
                txtCrm.Text = tb143.NU_CRM_MEDIC;
                txtHospital.Text = tb143.DE_HOSPI_CONSU;
                ddlCodigoDoenca.SelectedValue = tb143.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID.ToString();
                txtDoenca.Text = tb143.DE_DOENC;
                txtDataConsulta.Text = tb143.DT_CONSU.ToString("dd/MM/yyyy");
                txtDiasLicenca.Text = tb143.QT_DIAS_LICEN.ToString();
                txtObservacao.Text = tb143.DE_OBS;
                txtDataEntrega.Text = tb143.DT_ENTRE_ATEST.ToString("dd/MM/yyyy");
                txtDataCadastro.Text = tb143.DT_CADAS.ToString("dd/MM/yyyy");
                CarregaResponsavel(tb143.TB03_COLABOR.CO_COL);
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB143_ATEST_MEDIC</returns>
        private TB143_ATEST_MEDIC RetornaEntidade()
        {
            TB143_ATEST_MEDIC tb143 = TB143_ATEST_MEDIC.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id),
                LoginAuxili.ORG_CODIGO_ORGAO);
            return (tb143 == null) ? new TB143_ATEST_MEDIC() : tb143;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidades() 
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();                        
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaColaboradores() 
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaborador Responsável
        /// </summary>
        /// <param name="coCol">Id do funcionário</param>
        private void CarregaResponsavel(int coCol)
        {
            ddlResponsavel.DataSource = (from c in TB03_COLABOR.RetornaTodosRegistros()
                                         where c.CO_COL.Equals(coCol)
                                         select new { c.NO_COL, c.CO_COL }).OrderBy(c => c.NO_COL);

            ddlResponsavel.DataTextField = "NO_COL";
            ddlResponsavel.DataValueField = "CO_COL";
            ddlResponsavel.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Codigo de Doença
        /// </summary>
        private void CarregaCID()
        {
            var cid = (from c in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                       select new
                       {
                           doenca = String.Concat(c.CO_CID, " - ", c.NO_CID),
                           c.IDE_CID
                       }).OrderBy(p => p.doenca);

            ddlCodigoDoenca.DataSource = cid;
            ddlCodigoDoenca.DataTextField = "doenca";
            ddlCodigoDoenca.DataValueField = "IDE_CID";
            ddlCodigoDoenca.DataBind();
        }

        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }
    }
}
