using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8210_RecepcaoDeAvaliacao;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8210_RecepcaoDeAvaliacao._8219_Relatorios
{
    public partial class ExtAgendamento : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaPaciente();
                CarregaOperadora();
                CarregaCategoria();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            try
            {


                int coEmp, lRetorno, CoUnidade, CoPaciente, CoOperadora, CoPlano, CoCategoria;
                string dataIni, dataFim, Periodo, infos, Situacao, Tipo, parametros, Titulo;

                coEmp = LoginAuxili.CO_EMP;
                CoUnidade = int.Parse(ddlUnidade.SelectedValue);
                CoPaciente = int.Parse(ddlPaciente.SelectedValue);
                CoOperadora = int.Parse(ddlOperadora.SelectedValue);
                CoPlano = int.Parse(ddlPlano.SelectedValue);
                CoCategoria = int.Parse(ddlCategoria.SelectedValue);
                dataIni = IniPeri.Text;
                dataFim = FimPeri.Text;
                Situacao = ddlSituacao.SelectedValue;
                Tipo = ddlTipo.SelectedValue;

                Periodo = DateTime.Parse(dataIni).ToString("dd/MM/yy") + " à " + DateTime.Parse(dataFim).ToString("dd/MM/yy");
                Titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8210_RecepcaoDeAvaliacao/8219_Relatorios/ExtAgendamento.aspx");
                parametros = "( Unidade: " 
                    + ((!string.IsNullOrEmpty(ddlUnidade.SelectedValue)) && (ddlUnidade.SelectedValue != "0") ?
                    TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)).sigla.ToUpper() : "TODOS")
                    + " - Paciente: " + ddlPaciente.SelectedItem.Text.ToUpper()
                    + " - Operadora: " 
                    + ((!string.IsNullOrEmpty(ddlOperadora.SelectedValue)) && (ddlOperadora.SelectedValue != "0") ?
                    TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperadora.SelectedValue)).NM_SIGLA_OPER.ToUpper() : "TODOS")
                    + " - Plano: " 
                    + ((!string.IsNullOrEmpty(ddlPlano.SelectedValue)) && (ddlPlano.SelectedValue != "0") ?
                    TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlano.SelectedValue)).NM_SIGLA_PLAN.ToUpper() : "TODOS")
                    + " - Categoria: "
                    + ((!string.IsNullOrEmpty(ddlCategoria.SelectedValue)) && (ddlCategoria.SelectedValue != "0") ?
                    TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCategoria.SelectedValue)).NM_SIGLA_CATEG.ToUpper() : "TODOS")
                    + "  - Situações: " + ddlSituacao.SelectedItem + " - Período: " + Periodo + " )";
                infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);


                RptExtAgendamento fpcb = new RptExtAgendamento();
                lRetorno = fpcb.InitReport(parametros, infos, coEmp, CoUnidade, CoPaciente, CoOperadora, CoPlano, CoCategoria, dataIni, dataFim, Situacao, Tipo, Titulo);
                Session["Report"] = fpcb;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro  ao realizar operação" + ex.Message);
            }
        }

        #region Carregamentos

        protected void CarregaUnidade()
        {
            try
            {
                AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar Unidade " + ex.Message);

            }


        }
        protected void CarregaPaciente()
        {
            try
            {
                AuxiliCarregamentos.CarregaPacientes(ddlPaciente, LoginAuxili.CO_EMP, true);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar Paciente" + ex.Message);

            }

        }
        protected void CarregaOperadora()
        {
            try
            {
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar Operadora" + ex.Message);

            }

        }
        protected void CarregaPlano()
        {
            try
            {
                string IdOperadora = ddlOperadora.SelectedValue == "" ? "" : ddlOperadora.SelectedValue;
                AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, IdOperadora, true);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar Plano de saúde" + ex.Message);

            }

        }
        protected void CarregaCategoria()
        {
            try
            {
                AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCategoria, ddlPlano, true);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar Categoria de  pano de saúde " + ex.Message);

            }

        }

        protected void ddlddlOperadora_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregaPlano();
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar plano de saúde" + ex.Message);

            }
        }

        protected void ddlPlano_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregaCategoria();
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar Categoria " + ex.Message);

            }
        }

        #endregion
    }
}