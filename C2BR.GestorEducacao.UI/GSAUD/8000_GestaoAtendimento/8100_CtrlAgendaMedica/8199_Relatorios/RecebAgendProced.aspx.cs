using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class RecebAgendProced : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                CarregaUnidade(ddlUnidadeCadastro);
                CarregaUnidade(ddlUnidadeContrato);
                CarregaLocal();
                CarregaProfissional();
                IniPeri.Text =
                FimPeri.Text = data.ToShortDateString();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/RecebAgendProced.aspx");

            string infos, parametros;
            int lRetorno, coEmp = LoginAuxili.CO_EMP;
            int profiss = int.Parse(ddlProfissional.SelectedValue);
            int local = int.Parse(ddlLocal.SelectedValue);

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "( Unidade : " + ddlUnidadeCadastro.SelectedItem.Text.ToUpper()
                + " - Local : " + ddlLocal.SelectedItem.Text.ToUpper()
                + " - Profissional : " + ddlProfissional.SelectedItem.Text.ToUpper()
                + " - Período: " + IniPeri.Text + " à " + FimPeri.Text + " )";

            RptRecebAgendProced rpt = new RptRecebAgendProced();
            lRetorno = rpt.InitReport(titulo.ToUpper(), infos, parametros, coEmp, profiss, local,IniPeri.Text, FimPeri.Text, chkProcedimento.Checked);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaLocal()
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.CO_SITUA_DEPTO.Equals("A")
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO }).OrderBy(w => w.NO_DEPTO).ToList();
            ddlLocal.Items.Clear();

            if (res.Count > 0)
            {
                ddlLocal.DataValueField = "CO_DEPTO";
                ddlLocal.DataTextField = "NO_DEPTO";
                ddlLocal.DataSource = res;
                ddlLocal.DataBind();
            }
            ddlLocal.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CarregaProfissional()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FLA_PROFESSOR == "S"
                        && (UnidadeDeCadastro != 0 ? tb03.CO_EMP == UnidadeDeCadastro : 0 == 0)
                        && (UnidadeDeContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidadeDeContrato : 0 == 0)
                       select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();

            ddlProfissional.Items.Clear();

            if (res.Count > 0)
            {
                ddlProfissional.DataValueField = "CO_COL";
                ddlProfissional.DataTextField = "NO_COL";
                ddlProfissional.DataSource = res;
                ddlProfissional.DataBind();
                ddlProfissional.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
                ddlProfissional.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void dllUnidadeCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissional();
        }

        protected void ddlUnidadeContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissional();
        }
    }
}