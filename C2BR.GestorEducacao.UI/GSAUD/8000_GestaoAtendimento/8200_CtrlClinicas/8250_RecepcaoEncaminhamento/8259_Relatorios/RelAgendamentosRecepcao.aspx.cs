using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8220_RecepcaoEncaminhamento;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8250_RecepcaoEncaminhamento._8259_Relatorios
{
    public partial class RelAgendamentosRecepcao : System.Web.UI.Page
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
                CarregaClassificacoesFuncionais();
                CarregaProfissional();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            int coEmp, lRetorno, CoUnidade, CoProfissional;
            string dataIni, dataFim, Periodo, infos, parametros, Titulo, CoClassFuncional;

            coEmp = LoginAuxili.CO_EMP;
            CoUnidade = int.Parse(ddlUnidade.SelectedValue);
            CoClassFuncional = ddlClassFuncional.SelectedValue;
            CoProfissional = Convert.ToInt32(ddlProfissional.SelectedValue);
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;
            var Unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).sigla;


            Periodo = Convert.ToDateTime(dataIni).ToString("dd/MM/yy") + " à " + Convert.ToDateTime(dataFim).ToString("dd/MM/yy");
            Titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8250_RecepcaoEncaminhamento/8259_Relatorios/RelAgendamentosRecepcao.aspx");
            parametros = "( Unidade: " + Unidade.ToUpper() + " - Classificação : " + ddlClassFuncional.SelectedItem.Text.ToUpper() + " - Tipo : " + ddlStatus.SelectedItem.Text.ToUpper() +
                " - Profissional : " + ddlProfissional.SelectedItem.Text.ToUpper()  + " - Período: " + Periodo.ToUpper() + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptRecepcaoEncam fpcb = new RptRecepcaoEncam();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, CoUnidade, CoClassFuncional, CoProfissional, ddlStatus.SelectedValue, dllOrdem.SelectedValue, dataIni, dataFim, Titulo);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #region Carregamentos

        protected void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        protected void CarregaClassificacoesFuncionais()
        {
            try
            {
                AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFuncional, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar classificação " + ex.Message);

            }
           
        }

        protected void CarregaProfissional()
        {
            try
            {
                AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, LoginAuxili.CO_EMP, true, ddlClassFuncional.SelectedValue, true);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar profissional de saúde" + ex.Message);

            }
           
        }

        protected void ddlClassFuncional_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregaProfissional();
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao carregar classificação funcional " + ex.Message);

            }
        }


        #endregion
    }
}