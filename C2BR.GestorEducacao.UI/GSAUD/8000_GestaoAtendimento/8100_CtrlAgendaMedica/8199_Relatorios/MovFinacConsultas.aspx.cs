using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3300_Consultas;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;


namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class MovFinacConsultas : System.Web.UI.Page
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

                CarregaUnidadeCadastro();
                CarregaUnidadeContrato();
                CarregaEspecialidade();
                CarregaClassificacoes();
                CarregaUnidadeDeConsulta();

            }
        }

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string NomeFuncionalidade = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/MovFinacConsultas.aspx");
            int UnidadeDeCadastro = dllUnidadeCadastro.SelectedValue != "" ? int.Parse(dllUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
            int UnidadeConsulta = ddlUnidadeDeConsulta.SelectedValue != "" ? int.Parse(ddlUnidadeDeConsulta.SelectedValue) : 0;
            int Especialidade = ddlEspecialidade.SelectedValue != "" ? int.Parse(ddlEspecialidade.SelectedValue) : 0;
            string ProgramacoaClassificacao = ddlProgramacoaProfissional.SelectedValue != "" ? ddlProgramacoaProfissional.SelectedValue : "";
            int ProfissionalSaude = ddlProfissionalSaude.SelectedValue != "" ? int.Parse(ddlProfissionalSaude.SelectedValue) : 0;
           
            string infos, parametros;
            int lRetorno, coEmp = LoginAuxili.CO_EMP;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
         
            parametros = "( Unidade Consulta: " + ddlUnidadeDeConsulta.SelectedItem.Text + "( Unidade Cadastro: " + dllUnidadeCadastro.SelectedItem.Text + " - Especialidade: " + ddlEspecialidade.SelectedItem.Text + " - Classificacão Profissional: " + ddlProgramacoaProfissional.SelectedItem.Text + " - Profissional Saúde: " + ddlProfissionalSaude.SelectedItem.Text + " )";

            RptMovFinacConsultas fpcb = new RptMovFinacConsultas();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, UnidadeConsulta, UnidadeDeCadastro, UnidadeDeContrato, Especialidade, ProgramacoaClassificacao, ProfissionalSaude, txtDtIni.Text, txtDtFim.Text, NomeFuncionalidade.ToUpper());
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #region CarregaDropDownList
        /// <summary>
        /// Carrega as unidades de cadastro
        /// </summary>
        protected void CarregaUnidadeCadastro()
        {
            AuxiliCarregamentos.CarregaUnidade(dllUnidadeCadastro, LoginAuxili.ORG_CODIGO_ORGAO, true);

        }
        /// <summary>
        /// Carrega as unidades de cadastro
        /// </summary>
        protected void CarregaUnidadeContrato()
        {

            AuxiliCarregamentos.CarregaUnidade(ddlUnidadeContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as unidades de cadastro
        /// </summary>
        protected void CarregaUnidadeDeConsulta()
        {

            AuxiliCarregamentos.CarregaUnidade(ddlUnidadeDeConsulta, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as unidades de Especialidade
        /// </summary>
        protected void CarregaEspecialidade()
        {

            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecialidade, LoginAuxili.CO_EMP, null, true);
        }
        /// <summary>
        /// Carrega as unidades de Classificacoes
        /// </summary>
        protected void CarregaClassificacoes()
        {

            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlProgramacoaProfissional, true);
        }
        /// <summary>
        /// Carrega as unidades de Saude
        /// </summary>
        protected void CarregaProfissionalSaude()
        {   
            ddlProfissionalSaude.Items.Clear();
            int UnidadeDeCadastro = dllUnidadeCadastro.SelectedValue != "" ? int.Parse(dllUnidadeCadastro.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
            int Especialidade = ddlEspecialidade.SelectedValue != "" ? int.Parse(ddlEspecialidade.SelectedValue) : 0;
            string ProgramacoaClassificacao = ddlProgramacoaProfissional.SelectedValue != "" ? ddlProgramacoaProfissional.SelectedValue : "";
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FLA_PROFESSOR == "S"
                        && (UnidadeDeCadastro != 0 ? tb03.CO_EMP == UnidadeDeCadastro : 0 == 0)
                        && (UnidadeDeContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidadeDeContrato : 0 == 0)
                        && (ProgramacoaClassificacao != "0" ? tb03.CO_CLASS_PROFI == ProgramacoaClassificacao : 0 == 0)
                         && (Especialidade != 0 ? tb03.CO_ESPEC == UnidadeDeContrato : 0 == 0)

                       select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();

            if (res.Count > 0)
            {
                ddlProfissionalSaude.DataValueField = "CO_COL";
                ddlProfissionalSaude.DataTextField = "NO_COL";
                ddlProfissionalSaude.DataSource = res;
                ddlProfissionalSaude.DataBind();
                ddlProfissionalSaude.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlProfissionalSaude.Items.Clear();
                ddlProfissionalSaude.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }



        #endregion

        protected void dllUnidadeCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionalSaude();
           
        }

        protected void ddlUnidadeContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionalSaude();
        }

        protected void ddlEspecialidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionalSaude();
          
        }

        protected void ddlProgramacoaProfissional_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionalSaude();
            
        }

    }
}