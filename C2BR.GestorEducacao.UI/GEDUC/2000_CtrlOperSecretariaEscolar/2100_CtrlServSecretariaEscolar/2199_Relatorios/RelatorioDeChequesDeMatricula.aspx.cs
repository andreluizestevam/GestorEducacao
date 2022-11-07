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
using System.Data.Objects;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2199_Relatorios
{
    public partial class RelatorioDeChequesDeMatricula : System.Web.UI.Page
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
            int coEmpLog = LoginAuxili.CO_EMP;

            //--------> Inicializa as variáveis
            coAno = ddlAno.SelectedValue;
            parametros += "( Ano: " + coAno;

            coEmp = int.Parse(ddlEmp.SelectedValue);
            parametros += coEmp != 0 ? " - Unidade: " + TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).sigla : " - Unidade: Todos";

            coCol = int.Parse(ddlCol.SelectedValue);
            parametros += coCol != 0 ? " - Colaborador: " + TB03_COLABOR.RetornaPeloCoCol(coCol).NO_COL : " - Colaborador: Todos";

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
            //string EntityFunctions.TruncateTime();
            parametros += " Período: de " + dtIni.ToString("dd/MM/yyyy") + " até " + dtFim.ToString("dd/MM/yyyy") + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //RptMoviFinanMatric rpt = new RptMoviFinanMatric();
            //lRetorno = rpt.InitReport(parametros, infos, coEmp, coCol, coMod, coCur, coTur, coAno, dtIni, dtFim);

            RptRelatorioDeChequesDeMatricula rpt2 = new RptRelatorioDeChequesDeMatricula();
            lRetorno = rpt2.InitReport(parametros, infos, coEmp, coEmpLog, coCol, coMod, coCur, coTur, coAno, dtIni, dtFim);
            Session["Report"] = rpt2;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }


        #region Carregamento

        /// <summary>
        /// Método que carrega os anos na combo ddlAno
        /// </summary>
        private void CarregaAno()
        {
            var res = (from m in TB08_MATRCUR.RetornaTodosRegistros()
                       select new AnoSaida
                       {
                           coAno = m.CO_ANO_MES_MAT
                       }).Distinct().OrderByDescending(o => o.coAno);

            ddlAno.DataTextField = "CO_ANO";
            ddlAno.DataValueField = "CO_ANO";

            ddlAno.DataSource = res;
            ddlAno.DataBind();
        }

        public class AnoSaida
        {
            public string coAno { get; set; }
            public string CO_ANO
            {
                get
                {
                    return coAno.Trim();
                }
            }
        }

        /// <summary>
        /// Método que carrega as unidades na combo ddlEmp
        /// </summary>
        private void CarregaUnidade()
        {
            var res = (from emp in TB25_EMPRESA.RetornaTodosRegistros()
                       select new
                       {
                           emp.CO_EMP,
                           emp.NO_FANTAS_EMP
                       });

            ddlEmp.DataTextField = "NO_FANTAS_EMP";
            ddlEmp.DataValueField = "CO_EMP";

            ddlEmp.DataSource = res;
            ddlEmp.DataBind();

            ddlEmp.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega os colaboradores, que já fizeram uma matrícula, na combo ddlCol
        /// </summary>
        private void CarregaColaborador()
        {
            var res = (from m in TB08_MATRCUR.RetornaTodosRegistros()
                       join c in TB03_COLABOR.RetornaTodosRegistros() on m.CO_COL equals c.CO_COL
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
            var res = (from m in TB08_MATRCUR.RetornaTodosRegistros()
                       join mo in TB44_MODULO.RetornaTodosRegistros() on m.TB44_MODULO.CO_MODU_CUR equals mo.CO_MODU_CUR
                       select new
                       {
                           mo.CO_MODU_CUR,
                           mo.DE_MODU_CUR
                       }).Distinct();

            ddlMod.DataTextField = "DE_MODU_CUR";
            ddlMod.DataValueField = "CO_MODU_CUR";

            ddlMod.DataSource = res;
            ddlMod.DataBind();

            ddlMod.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega os cursos/séries, que possuam matrícula, na combo ddlSer
        /// </summary>
        private void CarregaSerie()
        {
            int coMod = int.Parse(ddlMod.SelectedValue);

            var res = (from m in TB08_MATRCUR.RetornaTodosRegistros()
                       join c in TB01_CURSO.RetornaTodosRegistros() on m.CO_CUR equals c.CO_CUR
                       where (coMod != 0 ? m.TB44_MODULO.CO_MODU_CUR == coMod : 0 == 0)
                       select new
                       {
                           c.NO_CUR,
                           c.CO_CUR
                       }).Distinct().OrderBy(w => w.NO_CUR);

            ddlSer.DataTextField = "NO_CUR";
            ddlSer.DataValueField = "CO_CUR";

            ddlSer.DataSource = res;
            ddlSer.DataBind();

            ddlSer.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega as turmas, que possuam matrícula, na combo ddlTur
        /// </summary>
        private void CarregaTurma()
        {
            int coMod = int.Parse(ddlMod.SelectedValue);
            int coCur = int.Parse(ddlSer.SelectedValue);

            var res = (from m in TB08_MATRCUR.RetornaTodosRegistros()
                       join t in TB129_CADTURMAS.RetornaTodosRegistros() on m.CO_TUR equals t.CO_TUR
                       where (coMod != 0 ? m.TB44_MODULO.CO_MODU_CUR == coMod : 0 == 0)
                       && (coCur != 0 ? m.CO_CUR == coCur : 0 == 0)
                       select new
                       {
                           t.CO_TUR,
                           t.NO_TURMA
                       }).Distinct();

            ddlTur.DataTextField = "NO_TURMA";
            ddlTur.DataValueField = "CO_TUR";

            ddlTur.DataSource = res;
            ddlTur.DataBind();

            ddlTur.Items.Insert(0, new ListItem("Todos", "0"));
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
