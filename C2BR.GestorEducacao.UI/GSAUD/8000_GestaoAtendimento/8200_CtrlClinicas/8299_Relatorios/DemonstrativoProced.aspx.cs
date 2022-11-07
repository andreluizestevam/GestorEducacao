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

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios
{
    public partial class DemonstrativoProced : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string NomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8299_Relatorios/DemonstrativoProced.aspx");

            string infos, parametros;
            string dataIni = IniPeri.Text;
            string dataFim = FimPeri.Text;
            string ordem = ddlOrdem.SelectedValue;
            int coEmp = int.Parse(ddlUnidadeCadastro.SelectedValue);
            int grupo = int.Parse(ddlGrupo.SelectedValue);
            int subgrupo = int.Parse(ddlSubGrupo.SelectedValue);

            parametros = "( Unidade : " + ddlUnidadeCadastro.SelectedItem.Text.ToUpper()
                        + " - Grupo : " + ddlGrupo.SelectedItem.Text.ToUpper()
                        + " - SubGrupo : " + ddlSubGrupo.SelectedItem.Text.ToUpper()
                        + " - Ordenado : " + ddlOrdem.SelectedItem.Text
                        + " - Período: " + dataIni + " à " + dataFim + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptDemonProced rpt = new RptDemonProced();
            var lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coEmp, grupo, subgrupo, dataIni, dataFim, ordem, chkCrescente.Checked, NomeFuncionalidade.ToUpper());

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dtIni = DateTime.Now;
                var dtFim = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                {
                    dtIni = LoginAuxili.DATA_INICIO_USU_DEMO;
                    dtFim = LoginAuxili.DATA_FINAL_USU_DEMO;
                }

                IniPeri.Text = dtIni.ToShortDateString();
                FimPeri.Text = dtFim.ToShortDateString();

                CarregaUnidade(ddlUnidadeCadastro);
                CarregaGrupo();
                CarregaSubGrupo();
            }
        }

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaGrupo()
        {
            var res = (from tbs354 in TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros()
                       where (tbs354.FL_SITUA_PROC_MEDIC_GRUPO.Equals("A"))
                       select new { tbs354.NM_PROC_MEDIC_GRUPO, tbs354.ID_PROC_MEDIC_GRUPO }).OrderBy(x => x.NM_PROC_MEDIC_GRUPO).ToList();

            ddlGrupo.Items.Clear();

            if (res.Count > 0)
            {
                ddlGrupo.DataValueField = "ID_PROC_MEDIC_GRUPO";
                ddlGrupo.DataTextField = "NM_PROC_MEDIC_GRUPO";
                ddlGrupo.DataSource = res;
                ddlGrupo.DataBind();
            }

            ddlGrupo.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CarregaSubGrupo()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            int Grupo = ddlGrupo.SelectedValue != "0" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            var res = (from tbs355 in TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros()
                       where (tbs355.FL_SITUA_PROC_MEDIC_GRUP.Equals("A"))
                       && (Grupo != 0 ? tbs355.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == Grupo : 0 == 0)
                       select new { tbs355.NM_PROC_MEDIC_SGRUP, tbs355.ID_PROC_MEDIC_SGRUP }).OrderBy(x => x.NM_PROC_MEDIC_SGRUP).ToList();

            ddlSubGrupo.Items.Clear();

            if (res.Count > 0)
            {
                ddlSubGrupo.DataValueField = "ID_PROC_MEDIC_SGRUP";
                ddlSubGrupo.DataTextField = "NM_PROC_MEDIC_SGRUP";
                ddlSubGrupo.DataSource = res;
                ddlSubGrupo.DataBind();
            }

            ddlSubGrupo.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void dllUnidadeCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
        }
    }
}