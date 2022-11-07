//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: EMISSÃO DA FICHA DE INFORMAÇÕES DA SÉRIES/CURSOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3019_Relatorios
{
    public partial class FicInforSerie : System.Web.UI.Page
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
                CarregaDepartamentos();
                CarregaCoordenacoes();
                CarregaModalidade();
                CarregaSerieCurso();
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_DPTO_CUR, strP_CO_SUB_DPTO_CUR;
            string strP_CO_SITU, strP_CO_NIVEL_CUR;


//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_NIVEL_CUR = ddlNivel.SelectedValue;
            strP_CO_DPTO_CUR = int.Parse(ddlDepartamento.SelectedValue);
            strP_CO_SUB_DPTO_CUR = int.Parse(ddlCoordenacao.SelectedValue);
            strP_CO_SITU = ddlSituacao.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "( Série Referência: " + ddlSerieCurso.SelectedItem.ToString() + " )";

            RptFicInforSerie rpt = new RptFicInforSerie();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.CO_EMP, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_NIVEL_CUR, 
                ddlNivel.SelectedItem.ToString(), strP_CO_DPTO_CUR, strP_CO_SUB_DPTO_CUR, strP_CO_SITU, strINFOS);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }                      
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Modalidades
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            //ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            //ddlModalidade.DataTextField = "DE_MODU_CUR";
            //ddlModalidade.DataValueField = "CO_MODU_CUR";
            //ddlModalidade.DataBind();

            ddlNivel.DataSource = TB133_CLASS_CUR.RetornaTodosRegistros();

            ddlNivel.DataTextField = "NO_CLASS_CUR";
            ddlNivel.DataValueField = "CO_SIGLA_CLASS_CUR";
            ddlNivel.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamentos
        /// </summary>
        private void CarregaDepartamentos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlDepartamento.DataSource = (from tb77 in TB77_DPTO_CURSO.RetornaTodosRegistros()
                                          where tb77.CO_EMP == coEmp
                                          select new { tb77.NO_DPTO_CUR, tb77.CO_DPTO_CUR });

            ddlDepartamento.DataTextField = "NO_DPTO_CUR";
            ddlDepartamento.DataValueField = "CO_DPTO_CUR";
            ddlDepartamento.DataBind();

            ddlDepartamento.Enabled = ddlDepartamento.Items.Count > 0;
        }

        /// <summary>
        /// Método que carrega o dropdown de Coordenação
        /// </summary>
        private void CarregaCoordenacoes()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coDptoCur = ddlDepartamento.SelectedValue != "" ? int.Parse(ddlDepartamento.SelectedValue) : 0;

            ddlCoordenacao.DataSource = (from tb68 in TB68_COORD_CURSO.RetornaTodosRegistros()
                                         where tb68.CO_EMP == coEmp && tb68.CO_DPTO_CUR == coDptoCur 
                                         select new { tb68.NO_COOR_CUR, tb68.CO_COOR_CUR }).OrderBy( c => c.NO_COOR_CUR );

            ddlCoordenacao.DataTextField = "NO_COOR_CUR";
            ddlCoordenacao.DataValueField = "CO_COOR_CUR";
            ddlCoordenacao.DataBind();

            ddlCoordenacao.Enabled = ddlCoordenacao.Items.Count > 0;
        }
       
        /// <summary>
        /// Método que carrega o dropdown de Coordenação, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidade()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            }
            else
            {
                int ano = DateTime.Now.Year;
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, true);
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int depto = ddlDepartamento.SelectedValue != "" ? int.Parse(ddlDepartamento.SelectedValue) : 0;
            int coord = ddlCoordenacao.SelectedValue != "" ? int.Parse(ddlCoordenacao.SelectedValue) : 0;
            string strClasCur = ddlNivel.SelectedValue.Trim();

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, true);
            }
            else
            {
                int ano = DateTime.Now.Year;
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, true);
            }

            //ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
            //                            where tb01.CO_MODU_CUR == modalidade && tb01.CO_NIVEL_CUR == strClasCur
            //                            && tb01.CO_DPTO_CUR == depto && tb01.CO_SUB_DPTO_CUR == coord
            //                            select new { tb01.CO_SIGL_CUR, tb01.CO_CUR }).OrderBy(c => c.CO_SIGL_CUR);

            //ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
            //ddlSerieCurso.DataValueField = "CO_CUR";
            //ddlSerieCurso.DataBind();

            //ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaDepartamentos();
            CarregaCoordenacoes();
            CarregaSerieCurso();
        }

        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlCoordenacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCoordenacoes();
            CarregaSerieCurso();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }
    }
}
