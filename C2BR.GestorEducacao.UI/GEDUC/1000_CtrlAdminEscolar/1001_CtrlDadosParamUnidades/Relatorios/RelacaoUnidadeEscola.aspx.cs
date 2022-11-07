//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: RELAÇÃO GERAL DAS UNIDADES DE ENSINO E DE APOIO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades;

namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios
{
    public partial class RelacaoUnidadeEscola : System.Web.UI.Page
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
                CarregaDropDown();
        }


        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlTipo.SelectedValue.ToString() == "U")
                RelUnidEsc();
            else if (ddlTipo.SelectedValue.ToString() == "N")
                RelUnidEscNuc();
            else
                RelUnidEscBairro();
        }

        //====> Processo de Geração do Relatório
        void RelUnidEsc()
        {
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int codEmp, codTpUnid;
            string infos;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codTpUnid = int.Parse(ddlTipoUnidade.SelectedValue);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptUnidadeEscolaTipo rpt = new RptUnidadeEscolaTipo();

            lRetorno = rpt.InitReport(codEmp, codTpUnid, infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        void RelUnidEscNuc()
        {
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int codEmp, codNucleo;
            string infos;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codNucleo = int.Parse(ddlNucleo.SelectedValue);
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptUnidadeEscolaNucleo rpt = new RptUnidadeEscolaNucleo();

            lRetorno = rpt.InitReport(codEmp, codNucleo, infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        void RelUnidEscBairro()
        {
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int codEmp, codCid, codBairro;
            string infos;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codCid = int.Parse(ddlCidade.SelectedValue);
            codBairro = (ddlBairro.SelectedValue == "T" ? 0 : int.Parse(ddlBairro.SelectedValue));
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptUnidadeEscolaBairro rpt = new RptUnidadeEscolaBairro();

            lRetorno = rpt.InitReport(codEmp, codCid, codBairro, infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        //====> Chamada do método de carregamento de dropdown de acordo com o tipo
        private void CarregaDropDown()
        {
            if (ddlTipo.SelectedValue.ToString() == "U")
                CarregaTiposUnidades();
            else if (ddlTipo.SelectedValue.ToString() == "N")
                CarregaNucleos();
            else
            {
                CarregaUF();
                CarregaCidades();
                CarregaBairros();
            }
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Núcleos
        /// </summary>
        private void CarregaNucleos()
        {
            liNucleo.Visible = true;
            liTipoUnidade.Visible = liUF.Visible = liBairro.Visible = liCidade.Visible = false;

            ddlNucleo.DataSource = from tbNucleoInst in TB_NUCLEO_INST.RetornaTodosRegistros()
                                   select new { tbNucleoInst.CO_NUCLEO, tbNucleoInst.DE_NUCLEO };

            ddlNucleo.DataTextField = "DE_NUCLEO";
            ddlNucleo.DataValueField = "CO_NUCLEO";
            ddlNucleo.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Unidade
        /// </summary>
        private void CarregaTiposUnidades()
        {
            liTipoUnidade.Visible = true;
            liNucleo.Visible = liUF.Visible = liBairro.Visible = liCidade.Visible = false;

            ddlTipoUnidade.DataSource = (from tb24 in TB24_TPEMPRESA.RetornaTodosRegistros()
                                         where tb24.CL_CLAS_EMP == "E"
                                         select new { tb24.CO_TIPOEMP, tb24.NO_TIPOEMP });

            ddlTipoUnidade.DataTextField = "NO_TIPOEMP";
            ddlTipoUnidade.DataValueField = "CO_TIPOEMP";
            ddlTipoUnidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        private void CarregaUF()
        {
            liUF.Visible = liBairro.Visible = liCidade.Visible = true;
            liTipoUnidade.Visible = liNucleo.Visible = false;

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

            ddlCidade.Enabled = ddlCidade.Items.Count > 0;
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;
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

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue.ToString() == "U")
                CarregaTiposUnidades();
            else if (ddlTipo.SelectedValue.ToString() == "N")
                CarregaNucleos();
            else
            {
                CarregaUF();
                CarregaCidades();
                CarregaBairros();
            }
        }

        protected void ddlUF_SelectedIndexChanged(object sender, EventArgs e)
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
