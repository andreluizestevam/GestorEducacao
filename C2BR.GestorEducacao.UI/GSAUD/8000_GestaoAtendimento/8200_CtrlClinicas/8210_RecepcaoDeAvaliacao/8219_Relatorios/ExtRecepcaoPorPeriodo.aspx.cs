using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8210_RecepcaoDeAvaliacao;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;


namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8210_RecepcaoDeAvaliacao._8219_Relatorios
{
    public partial class ExtRecepcaoPorPeriodo : System.Web.UI.Page
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

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = res;
                ddlPaciente.DataBind();
            }

            ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));

            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNomePacPesq.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            ddlPaciente.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            int coEmp, lRetorno, CoUnidade, CoPaciente, CoOperadora, CoPlano, CoCategoria;
            string dataIni, dataFim, Periodo, OrdenadoPor, infos, parametros,procedimentos, Titulo;

            coEmp = LoginAuxili.CO_EMP;
            CoUnidade = int.Parse(ddlUnidade.SelectedValue);
            CoPaciente = int.Parse(ddlPaciente.SelectedValue);
            CoOperadora = int.Parse(ddlOperadora.SelectedValue);
            CoPlano = int.Parse(ddlPlano.SelectedValue);
            CoCategoria = int.Parse(ddlCategoria.SelectedValue);
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;
            OrdenadoPor = dllOrdenadoPor.SelectedValue;
            procedimentos = ddlProcedimentos.SelectedValue;
            Periodo = dataIni + " à " + dataFim;
            var Unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).sigla;
            Titulo = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8200_CtrlClinicas/8210_RecepcaoDeAvaliacao/8219_Relatorios/ExtRecepcaoPorPeriodo.aspx");
            parametros = "( Unidade: " + Unidade.ToUpper() + " - Paciente: " + ddlPaciente.SelectedItem.Text.ToUpper() +
                " - Operadora: " + ddlOperadora.SelectedItem.Text.ToUpper() + " - Plano: " + ddlPlano.SelectedItem.Text.ToUpper() + " - Categoria: " + ddlCategoria.SelectedItem.Text.ToUpper() + " - Ordenado Por: " + dllOrdenadoPor.SelectedItem.Text.ToUpper() + " - Procedimentos : " + ddlProcedimentos.SelectedItem.Text.ToUpper() + "  - Período: " + Periodo.ToUpper() + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);


            RptExtRecepcaoPorPeriodo fpcb = new RptExtRecepcaoPorPeriodo();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, CoUnidade, CoPaciente, CoOperadora, CoPlano, CoCategoria, dataIni, dataFim, OrdenadoPor, procedimentos, Titulo);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #region Carregamentos

        protected void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }
        protected void CarregaPaciente()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlPaciente, LoginAuxili.CO_EMP, true);
        }
        protected void CarregaOperadora()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);
        }
        protected void CarregaPlano()
        {
            string IdOperadora = ddlOperadora.SelectedValue == "" ? "" : ddlOperadora.SelectedValue;
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, IdOperadora, true);
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