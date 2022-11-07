using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3300_Consultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8250_RecepcaoEncaminhamento._8259_Relatorios
{
    public partial class TemposAtendimento : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string NomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8250_RecepcaoEncaminhamento/8259_Relatorios/TemposAtendimento.aspx");

            string infos, parametros, local, classFunci;
            int profissional = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;

            parametros = "( Profissional: " + ddlProfissional.SelectedItem.Text.ToUpper()
                        + " - Classif. Funcional: " + ddlClassFunci.SelectedItem.Text
                        + " - Data: " + txtData.Text + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            local = ddlLocal.SelectedValue;
            classFunci = ddlClassFunci.SelectedValue;

            if (ddlModelo.SelectedValue.Equals("M2"))
            {

                RptTempoAtendimento rpt = new RptTempoAtendimento();
                var lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, profissional, txtData.Text, NomeFuncionalidade.ToUpper(), int.Parse(local), classFunci);

                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }

            if (ddlModelo.SelectedValue.Equals("M1"))
            {

                RptTempoAtendimento2 rpt = new RptTempoAtendimento2();
                var lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, profissional, txtData.Text, NomeFuncionalidade.ToUpper(), int.Parse(local), classFunci);

                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = DateTime.Now;

                if (LoginAuxili.FLA_USR_DEMO)
                    data = LoginAuxili.DATA_INICIO_USU_DEMO;

                txtData.Text = data.ToShortDateString();

                CarregaUnidade(ddlUnidadeCadastro);
                CarregaUnidade(ddlUnidadeContrato);
                CarregaClassificacoes();
                CarregaDepartamento();
                CarregaProfissional();
            }
        }

        private void CarregaUnidade(DropDownList drop)
        {
            AuxiliCarregamentos.CarregaUnidade(drop, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaProfissional()
        {
            int UnidadeDeCadastro = ddlUnidadeCadastro.SelectedValue != "" ? int.Parse(ddlUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
            string coClass = ddlClassFunci.SelectedValue;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FLA_PROFESSOR == "S"
                        && (UnidadeDeCadastro != 0 ? tb03.CO_EMP == UnidadeDeCadastro : 0 == 0)
                        && (UnidadeDeContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidadeDeContrato : 0 == 0)
                        && (coClass != "0" ? tb03.CO_CLASS_PROFI.Equals(coClass) : 0 == 0)
                       select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();

            ddlProfissional.Items.Clear();

            if (res.Count > 0)
            {
                ddlProfissional.DataValueField = "CO_COL";
                ddlProfissional.DataTextField = "NO_COL";
                ddlProfissional.DataSource = res;
                ddlProfissional.DataBind();
            }

            ddlProfissional.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega os departamentos
        /// </summary>
        private void CarregaDepartamento()
        {
            int coEmp = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       join tb174 in TB174_DEPTO_TIPO.RetornaTodosRegistros() on tb14.CO_TIPO_DEPTO equals tb174.ID_DEPTO_TIPO
                       where coEmp != 0 ? tb14.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0
                       && tb174.CO_CLASS_TIPO_LOCAL.Equals("TEC")
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO, DE_DEPTO = (tb14.CO_SIGLA_DEPTO + " - " + tb14.NO_DEPTO) }).OrderBy(x => x.NO_DEPTO);

            ddlLocal.DataTextField = "DE_DEPTO";
            ddlLocal.DataValueField = "CO_DEPTO";
            ddlLocal.DataSource = res;
            ddlLocal.DataBind();
            ddlLocal.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega as classificações
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFunci, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
            // AuxiliCarregamentos.CarregaTiposAgendamento(ddlClassFunc, true, false, false, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }

        protected void ddlClassFunci_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissional();
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