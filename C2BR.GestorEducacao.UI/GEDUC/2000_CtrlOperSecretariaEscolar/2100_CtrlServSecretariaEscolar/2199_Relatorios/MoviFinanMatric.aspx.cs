//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: RELAÇÃO DE INFORMAÇÕES GERAIS DO ALUNO
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
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar;
using C2BR.GestorEducacao.Reports.GSAUD;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2199_Relatorios
{
    public partial class MoviFinanMatric : System.Web.UI.Page
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
                CarregaAno();
                CarregaUnidade();
                CarregaColaborador();
                CarregaModalidade();
                CarregaSerie();
                CarregaTurma();
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            #region Validações

            if (txtDtIni.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de início do período de busca não foi informado.");
                return;
            }

            if (txtDtFim.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de término do período de busca não foi informado.");
                return;
            }

            #endregion

            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int coEmp;
            int coCol;
            int coMod;
            int coCur;
            int coTur;
            string coAno;
            DateTime dtIni;
            DateTime dtFim;
            string infos = "";
            string parametros = "";

            //--------> Inicializa as variáveis
            coAno = ddlAno.SelectedValue;
            parametros += "( Ano: " + coAno;

            coEmp = int.Parse(ddlEmp.SelectedValue);
            parametros +=coEmp != 0 ? " - Unidade: " + TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).sigla : " - Unidade: Todos";

            coCol = int.Parse(ddlCol.SelectedValue);
            parametros +=  coCol != 0 ? " - Colaborador: " + TB03_COLABOR.RetornaPeloCoCol(coCol).NO_COL : " - Colaborador: Todos";

            coMod = int.Parse(ddlMod.SelectedValue);
            parametros += coMod != 0 ? " - Modalidade: " + TB44_MODULO.RetornaPelaChavePrimaria(coMod).CO_SIGLA_MODU_CUR : " - Modalidade: Todos";

            coCur = int.Parse(ddlSer.SelectedValue);
            parametros += coCur != 0 ? " - Série/Curso: " + TB01_CURSO.RetornaPeloCoCur(coCur).NO_CUR : " - Série/Curso: Todos";

            coTur = int.Parse(ddlTur.SelectedValue);
            parametros += coTur != 0 ? " - Turma: " + TB129_CADTURMAS.RetornaPelaChavePrimaria(coTur).NO_TURMA : " - Turma: Todos";


            if (!DateTime.TryParse(txtDtIni.Text, out dtIni))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de início do período de busca informada é uma data inválida.");
                return;
            }

            if (!DateTime.TryParse(txtDtFim.Text, out dtFim))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de término do período de busca informada é uma data inválida.");
                return;
            }

            parametros += " Período: de " + dtIni.ToString("dd/MM/yyyy") + " até " + dtFim.ToString("dd/MM/yyyy") + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptMoviFinanMatric rpt = new RptMoviFinanMatric();
            lRetorno = rpt.InitReport(parametros, infos, coEmp, coCol, coMod, coCur, coTur, coAno, dtIni, dtFim);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }


        #region Carregamento

        /// <summary>
        /// Método que carrega os anos na combo ddlAno
        /// </summary>
        private void CarregaAno()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Método que carrega as unidades na combo ddlEmp
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlEmp, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega os colaboradores, que já fizeram uma matrícula, na combo ddlCol
        /// </summary>
        private void CarregaColaborador()
        {
            int coEmp = ddlEmp.SelectedValue != "" ? int.Parse(ddlEmp.SelectedValue) : 0;
            var res = (from m in TB08_MATRCUR.RetornaTodosRegistros()
                       join c in TB03_COLABOR.RetornaTodosRegistros() on m.CO_COL equals c.CO_COL
                       where m.CO_EMP == coEmp
                       select new
                       {
                           c.NO_COL,
                           c.CO_COL
                       }).Distinct().OrderBy(o => o.NO_COL);

            ddlCol.DataTextField = "NO_COL";
            ddlCol.DataValueField = "CO_COL";

            ddlCol.DataSource = res;
            ddlCol.DataBind();

            ddlCol.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega as modalidades, que possuem matrícula, na combo ddlMod
        /// </summary>
        private void CarregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlMod, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega os cursos/séries, que possuam matrícula, na combo ddlSer
        /// </summary>
        private void CarregaSerie()
        {
            int coMod = ddlMod.SelectedValue != "" ? int.Parse(ddlMod.SelectedValue) : 0;
            int coEmp = ddlEmp.SelectedValue != "" ? int.Parse(ddlEmp.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSer, coMod, coEmp, true); 
        }

        /// <summary>
        /// Método que carrega as turmas, que possuam matrícula, na combo ddlTur
        /// </summary>
        private void CarregaTurma()
        {
            int coMod = ddlMod.SelectedValue != "" ? int.Parse(ddlMod.SelectedValue) : 0;
            int coCur = ddlSer.SelectedValue != "" ? int.Parse(ddlSer.SelectedValue) : 0;
            int coEmp = ddlEmp.SelectedValue != "" ? int.Parse(ddlEmp.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTur, coEmp, coMod, coCur, true);
        }

        #endregion

        #region Métodos de Campos

        /// <summary>
        /// Método que carrega as Séries e Turmas quando uma modalidade é selecionada
        /// </summary>
        protected void ddlMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerie();
            CarregaTurma();
        }

        /// <summary>
        /// Método que carrega a Turma quando uma série é seleionada
        /// </summary>
        protected void ddlSer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        #endregion
    }
}