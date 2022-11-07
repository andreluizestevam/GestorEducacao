using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._9000_ControleAtendimento._9200_CtrlVacinas._9299_Relatorios;

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9200_CtrlVacinas._9299_Relatorios
{
    public partial class DemonstrativoVacinacaoPorUnid : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string NomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/9000_ControleAtendimento/9200_CtrlVacinas/9299_Relatorios/DemonstrativoVacinacaoPorUnid.aspx");

            string infos, parametros;
            string dataIni = IniPeri.Text;
            string dataFim = FimPeri.Text;
            int coEmp = int.Parse(drpUnidadeVacinacao.SelectedValue);
            int campanha = int.Parse(drpCampanhaVacinacao.SelectedValue);
            int grupoRisco = int.Parse(drpGrupoRisco.SelectedValue);
            int faixaEtaria = int.Parse(drpFaixaEtaria.SelectedValue);
            string usuESus = drpUsuariosEsus.SelectedValue;

            parametros = "( Unidade : " + drpUnidadeVacinacao.SelectedItem.Text.ToUpper()
                        + " - Grupo : " + drpGrupoRisco.SelectedItem.Text.ToUpper()
                        + " - Faixa Etárea : " + drpFaixaEtaria.SelectedItem.Text.ToUpper()
                        + " - Campanha :" + drpCampanhaVacinacao.SelectedItem.Text
                        + " - Período: " + dataIni + " à " + dataFim + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptDemonstrativoVacinacaoPorUnidade rpt = new RptDemonstrativoVacinacaoPorUnidade();
            var lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coEmp, campanha, grupoRisco, faixaEtaria, usuESus, dataIni, dataFim, NomeFuncionalidade.ToUpper());

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

                CarregaUnidade(drpUnidadeVacinacao);
                CarregaCampanhaVacinacao();
            }
        }

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaCampanhaVacinacao()
        {
            var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                       select new { tbs339.NM_CAMPAN, tbs339.ID_CAMPAN }).OrderBy(x => x.NM_CAMPAN).ToList();

            drpCampanhaVacinacao.Items.Clear();

            if (res.Count > 0)
            {
                drpCampanhaVacinacao.DataValueField = "ID_CAMPAN";
                drpCampanhaVacinacao.DataTextField = "NM_CAMPAN";
                drpCampanhaVacinacao.DataSource = res;
                drpCampanhaVacinacao.DataBind();
            }

            drpCampanhaVacinacao.Items.Insert(0, new ListItem("Todas", "0"));
        }
    }
}