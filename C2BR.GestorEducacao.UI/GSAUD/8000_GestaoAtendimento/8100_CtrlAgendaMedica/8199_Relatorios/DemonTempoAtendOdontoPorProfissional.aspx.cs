using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class DemonTempoAtendOdontoPorProfissional : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }
        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            if(String.IsNullOrEmpty(IniPeri.Text)){
                AuxiliPagina.EnvioMensagemErro(this.Page, "Insira uma data para início do período");
                IniPeri.Focus();
                return;
            }

            if (String.IsNullOrEmpty(FimPeri.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Insira uma data para o final do período");
                FimPeri.Focus();
                return;
            }

            if (String.IsNullOrEmpty(horaInicial.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Insira um horário para o início do período");
                horaInicial.Focus();
                return;
            }

            if (String.IsNullOrEmpty(horaFinal.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Insira um horário para o final do período");
                horaFinal.Focus();
                return;
            }

            string NomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/DemonTempoAtendPorProfissional.aspx");

            string infos, parametros, tb;
            int lRetorno, coEmp = LoginAuxili.CO_EMP;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            string dataIni = IniPeri.Text;
            string dataFim = FimPeri.Text;
            string horaIni = horaInicial.Text;
            string horaFim = horaFinal.Text;
            int CoUnidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int CoLocal = int.Parse(ddlLocal.SelectedValue);
            string Espec = ddlEspec.SelectedValue;
            int profissional = int.Parse(ddlProfissional.SelectedValue);
            string Periodo = dataIni + " " + horaIni + " à " + dataFim + " " + horaFim;

            if (CoUnidade != 0)
            {
                tb = TB25_EMPRESA.RetornaPelaChavePrimaria(CoUnidade).sigla;
            }
            else
            {
                tb = "Todos";
            }

            parametros = "( Unidade : " + tb.ToUpper()
                + " -  Local: " + ddlLocal.SelectedItem.Text.ToUpper()
                + " -  Especialidade: " + ddlEspec.SelectedItem.Text.ToUpper()
                + " -  Profissional: " + ddlProfissional.SelectedItem.Text.ToUpper()
                + "  - Período: " + Periodo.ToUpper() + " )";
            RptDemonTempoAtendOdontoProProfi fpcb = new RptDemonTempoAtendOdontoProProfi();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, CoUnidade, CoLocal, Espec, profissional, dataIni, dataFim, horaIni, horaFim, NomeFuncionalidade.ToUpper());

            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LoginAuxili.FLA_USR_DEMO)
                {
                    IniPeri.Text = LoginAuxili.DATA_INICIO_USU_DEMO.ToShortDateString();
                    FimPeri.Text = LoginAuxili.DATA_FINAL_USU_DEMO.ToShortDateString();
                }

                CarregaUnidade();
                CarregaLocal();
                CarregaEspecialidade();
                CarregaProfissionaisSaude();
            }
        }

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            CarregaProfissionaisSaude();
        }

        protected void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        protected void CarregaProfissionaisSaude()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, int.Parse(ddlUnidade.SelectedValue), true, ddlEspec.SelectedValue,false,int.Parse(ddlLocal.SelectedValue));
        }

        protected void CarregaEspecialidade()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlEspec, true, int.Parse(ddlUnidade.SelectedValue));
        }

        protected void CarregaLocal()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where !tb14.CO_SITUA_DEPTO.Equals("N")
                       && tb14.FL_CONSU.Equals("S")
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO }).OrderBy(w => w.NO_DEPTO).ToList();

            if (res.Count > 0)
            {
                ddlLocal.DataValueField = "CO_DEPTO";
                ddlLocal.DataTextField = "NO_DEPTO";
                ddlLocal.DataSource = res;
                ddlLocal.DataBind();
            }

            ddlLocal.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void ddlLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionaisSaude();
        }

        protected void ddlProfissional_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlEspec_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionaisSaude();
        }
    }
}