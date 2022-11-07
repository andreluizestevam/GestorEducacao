using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Runtime.InteropServices;
using System.IO;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores._1240_ContrDeDeclaracoes;

namespace C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores._1240_ContrDeDeclaracoes
{
    public partial class DeclaracoesFuncionais : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

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
                CarregarUnidade();
                CarregarTipoColaborador();
                CarregaFuncionarios();
            }
        }

        //====> Método que faz a chamada de outro método de acordo com o Tipo de presença
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            var codEmpRef = int.Parse(ddlUnidade.SelectedValue);
            var codCol = int.Parse(ddlFuncionarios.SelectedValue);
            var tipoDeclaracao = drpTipoDeclaracao.SelectedValue;

            var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                       from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                       where tb009.TP_DOCUM == "DE" && tb009.CO_SIGLA_DOCUM == tipoDeclaracao
                       //&& tb009.TB25_EMPRESA.CO_EMP == codEmp
                       select new
                       {
                           Pagina = tb010.NU_PAGINA,
                           Titulo = tb009.NM_TITUL_DOCUM,
                           Texto = tb010.AR_DADOS
                       }).OrderBy(x => x.Pagina);

            if (lst != null && lst.Where(x => x.Pagina == 1).Any())
            {
                RptDeclaracoesFuncionais rpt = new RptDeclaracoesFuncionais();
                var lRetorno = rpt.InitReport(LoginAuxili.CO_EMP, codCol, "DE", tipoDeclaracao);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Arquivo não cadastrado na tabela de Arquivo RTF - Sigla DEA, Tipo de documento Declaração");
                return;
            }
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }

        protected void ddlTipoColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }

        #endregion

        #region Carregamento DropDown

        private void CarregarUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Verifica o tipo da unidade e altera informacoes especificas
        /// </summary>
        private void CarregarTipoColaborador()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGE":
                    ddlTipoColaborador.Items.Clear();
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Professores", "P"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Funcionários", "N"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Todos", ""));
                    break;
                case "PGS":
                    ddlTipoColaborador.Items.Clear();
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Profissionais Saúde", "S"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Funcionários", "N"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Todos", ""));
                    break;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaFuncionarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string flaCol = ddlTipoColaborador.SelectedValue;

            var lst = from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where (coEmp != 0 ? tb03.CO_EMP == coEmp : 0 == 0)
                      && (flaCol != "" ? tb03.FLA_PROFESSOR == flaCol : true)
                      && tb03.CO_SITU_COL != "DEM"
                      select new
                      {
                          Nome = tb03.NO_COL + (flaCol == "" ? " - " + ((tb03.FLA_PROFESSOR == "N") ? "Funcionário" : "Professor") : ""),
                          tb03.CO_COL
                      };

            ddlFuncionarios.DataSource = lst.OrderBy(c => c.Nome);
            ddlFuncionarios.DataTextField = "Nome";
            ddlFuncionarios.DataValueField = "CO_COL";
            ddlFuncionarios.DataBind();

            ddlFuncionarios.Items.Insert(0, new ListItem("Todos", ""));
        }

        #endregion
    }
}