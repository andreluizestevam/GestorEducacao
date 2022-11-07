//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: FICHA INDIVIDUAL COMPLETA DE INFORMAÇÕES DE FUNCIONÁRIO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 25/02/2015| Maxwell Almeida           | Inserida regra para alterar o label professor para profissional de saúde
//          |                            | quando a unidade logada for de saúde

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1299_Relatorios
{
    public partial class FichaFuncionario : System.Web.UI.Page
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
                CarregaDropDown();
                CarregaFuncionarios();
                VerificarTipoUnidade();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            string NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("/GEDUC/1000_CtrlAdminEscolar/1200_GestaoOperColaboradores/1299_RelatoriosFichaFuncionario.aspx");

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_COL, strP_CO_ANO_BASE;
            string strP_FLA_PROFESSOR, strINFOS;

            //--------> Inicializa as variáveis
            strP_CO_EMP = 0;
            strP_CO_COL = 0;
            strP_CO_ANO_BASE = 0;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            int strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidContrato.SelectedValue);
            strP_CO_COL = int.Parse(ddlFuncionarios.SelectedValue);
            strP_CO_ANO_BASE = int.Parse(ddlAnoBase.SelectedValue);

            if (ddlTipoColaborador.SelectedValue == "T")
                strP_FLA_PROFESSOR = (ddlFuncionarios.SelectedItem.Text.EndsWith("Professor") ? "S" : "N");
            else
                strP_FLA_PROFESSOR = ddlTipoColaborador.SelectedValue;

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptFichaColaborador rpt = new RptFichaColaborador();
            lRetorno = rpt.InitReport(strP_CO_EMP, strP_CO_COL, strP_CO_ANO_BASE, strP_FLA_PROFESSOR, strINFOS, strP_CO_EMP_UNID_CONT, NomeFuncionalidadeCadastrada.ToUpper());
            rpt.Titulo = "FICHA DE INFORMAÇÕES DO COLABORADOR";
            Session["Report"] = rpt;

            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e de Ano Base
        /// </summary>
        private void CarregaDropDown()
        {
            int intAno = DateTime.Now.Year;

            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);

            ddlAnoBase.Items.Clear();
            for (int i = intAno - 9; i <= intAno; i++)
                ddlAnoBase.Items.Add(new ListItem(i.ToString(), i.ToString()));

            ddlAnoBase.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coEmpLotacao = (ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0);
            string flaCol = ddlTipoColaborador.SelectedValue != "T" ? ddlTipoColaborador.SelectedValue : "";

            var lst = from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where (coEmpLotacao != 0 ? tb03.CO_EMP_UNID_CONT == coEmpLotacao : 0 == 0)
                      && (coEmp != 0 ? tb03.CO_EMP == coEmp : 0 == 0)
                      select new
                      {
                          Nome = tb03.NO_COL, 
                          tb03.CO_COL,
                          tb03.FLA_PROFESSOR
                      };


            if (flaCol != string.Empty)
                ddlFuncionarios.DataSource = lst.Where(x => x.FLA_PROFESSOR == flaCol).OrderBy(c => c.Nome);
            else
                ddlFuncionarios.DataSource = lst.OrderBy(c => c.Nome);

            ddlFuncionarios.DataTextField = "Nome";
            ddlFuncionarios.DataValueField = "CO_COL";
            ddlFuncionarios.DataBind();
            ddlFuncionarios.Items.Insert(0, new ListItem("Selecione", ""));

            ddlFuncionarios.Enabled = ddlFuncionarios.Items.Count > 0;
        }

        /// <summary>
        /// Verifica o tipo da unidade e altera informacoes especificas
        /// </summary>
        private void VerificarTipoUnidade()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGE":
                    ddlTipoColaborador.Items.Clear();
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Professores", "P"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Funcionários", "N"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Todos", "T"));
                    break;
                case "PGS":
                    ddlTipoColaborador.Items.Clear();
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Profissionais Saúde", "S"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Funcionários", "N"));
                    ddlTipoColaborador.Items.Insert(0, new ListItem("Todos", "T"));
                    break;

            }
        }

        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }

        protected void ddlUnidContrato_onselectedindexchanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }

        protected void ddlTipoColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }
    }
}
