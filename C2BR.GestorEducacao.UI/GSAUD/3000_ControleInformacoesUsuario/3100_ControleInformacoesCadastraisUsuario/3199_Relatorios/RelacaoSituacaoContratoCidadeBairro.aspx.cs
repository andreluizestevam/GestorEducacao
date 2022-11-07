using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario;



namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3199_Relatorios
{
    public partial class RelacaoSituacaoContratoCidadeBairro : System.Web.UI.Page
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
                CarregaUnidades();
                Uf();
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int coEmp, coUndCont, coOrdem, Cidade, Bairro, Operadora;
           
            string sitMat, infos, parametros, Uf;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coEmp = LoginAuxili.CO_EMP;
            coUndCont = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);


            Operadora = ddlOperadora.SelectedValue == "" ? 0 : int.Parse(ddlOperadora.SelectedValue);
            Uf = ddlUF.SelectedValue == "" ? "0" : ddlUF.SelectedValue;
            Cidade = ddlCidade.SelectedValue == "T" ? 0 : int.Parse(ddlCidade.SelectedValue);
            Bairro = ddlBairro.SelectedValue == "T" ? 0 : int.Parse(ddlBairro.SelectedValue);

            coOrdem = int.Parse(ddlOrdemImpressao.SelectedValue);
            sitMat = ddlSituacao.SelectedValue;


            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            parametros = "(Unidade : " + ddlUnidade.SelectedItem.ToString() +
                         " - Operadora :  " + ddlOperadora.SelectedItem.ToString() +
                         " - UF : " + ddlUF.SelectedItem.ToString() +
                         " - Cidade : " + ddlCidade.SelectedItem.ToString() +
                         " - Bairro : "  + ddlBairro.SelectedItem.ToString() +
                         " - Situação: "  + ddlSituacao.SelectedItem.ToString() +
                         " - Ordenado Por:  " + ddlOrdemImpressao.SelectedItem.ToString() + ")";

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GSAUD/3000_ControleInformacoesUsuario/3100_ControleInformacoesCadastraisUsuario/3199_Relatorios/RelacaoSituacaoContratoCidadeBairro.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptRelacaoSituPorContratoCidadeBairro rpt = new RptRelacaoSituPorContratoCidadeBairro();
            lRetorno = rpt.InitReport(parametros, coEmp, coUndCont, sitMat, Operadora, Uf, Cidade, Bairro, coOrdem, infos, NO_RELATORIO);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown


        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void Uf()
        {
            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO.ToString();

        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "T" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;

                if (ddlCidade.Items.Count > 1)
                    ddlBairro.Items.Insert(0, new ListItem("Todos", "T"));
                else
                    ddlBairro.Items.Clear();

                return;
            }
            ddlBairro.Enabled = true;

            ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion

        protected void ddlUF_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaCidades();
            CarregaBairros();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }
    }
}