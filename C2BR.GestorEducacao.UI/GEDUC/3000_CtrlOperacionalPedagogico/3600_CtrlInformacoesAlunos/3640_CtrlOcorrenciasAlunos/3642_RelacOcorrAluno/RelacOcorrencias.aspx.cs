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
// 13/02/2015| Maxwell Almeida           | Criação do relatório para emissão da relação de Ocorrências

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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar;
using C2BR.GestorEducacao.Reports.GSAUD;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasAlunos._3642_RelacOcorrAluno
{
    public partial class RelacOcorrencias : System.Web.UI.Page
    {
        #region Eventos

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
                CarregaModalidades();
                CarregaCursos();
                CarregaTurmas();
                CarregaAlunos();
                CarregaTipoOcorrencia();
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
                txtDtIni.Focus();
                return;
            }

            if (txtDtFim.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de término do período de busca não foi informado.");
                txtDtFim.Focus();
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
            int coAlu;
            string tipoOcorr;
            string coAno;
            DateTime dtIni;
            DateTime dtFim;
            string infos = "";
            string parametros = "";

            //--------> Inicializa as variáveis
            coAno = ddlAno.SelectedValue;
            parametros += "( Ano: " + ddlAno.SelectedItem.Text;

            coMod = int.Parse(ddlModalidade.SelectedValue);
            parametros += coMod != 0 ? " - Modalidade: " + ddlModalidade.SelectedItem.Text : " - Modalidade: Todos";

            coCur = int.Parse(ddlCurso.SelectedValue);
            parametros += coCur != 0 ? " - Curso: " + ddlCurso.SelectedItem.Text : " - Curso: Todos";

            coTur = int.Parse(ddlTurma.SelectedValue);
            parametros += coTur != 0 ? " - Turma: " + ddlTurma.SelectedItem.Text : " - Turma: Todos";

            coAlu = int.Parse(ddlAluno.SelectedValue);
            parametros += coTur != 0 ? " - Aluno: " + ddlAluno.SelectedItem.Text : " - Aluno: Todos";

            tipoOcorr = ddlTipoOcorrencia.SelectedValue;
            parametros += tipoOcorr != "0" ? " - Tipo: " + ddlTipoOcorrencia.SelectedItem.Text : " - Tipo: Todos";

            #region Valida se as datas inseridas são válidas
            if (!DateTime.TryParse(txtDtIni.Text, out dtIni))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de início do período de busca informada é uma data inválida.");
                txtDtIni.Focus();
                return;
            }

            if (!DateTime.TryParse(txtDtFim.Text, out dtFim))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de término do período de busca informada é uma data inválida.");
                txtDtFim.Focus();
                return;
            }
            #endregion

            parametros += " - Período: de " + dtIni.ToString("dd/MM/yyyy") + " até " + dtFim.ToString("dd/MM/yyyy") + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GEDUC/3000_CtrlOperacionalPedagogico/3600_CtrlInformacoesAlunos/3640_CtrlOcorrenciasAlunos/3642_RelacOcorrAluno/RelacOcorrencias.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptRelacOcorrencias rpt = new RptRelacOcorrencias();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coMod, coCur, coTur, coAno, coAlu, dtIni, dtFim, NO_RELATORIO.ToUpper(), tipoOcorr);

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Ocorrência
        /// </summary>
        private void CarregaTipoOcorrencia()
        {
            ddlTipoOcorrencia.DataSource = TB150_TIPO_OCORR.RetornaTodosRegistros().Where(p => p.TP_USU.Equals("A")).OrderBy(t => t.DE_TIPO_OCORR);

            ddlTipoOcorrencia.DataTextField = "DE_TIPO_OCORR";
            ddlTipoOcorrencia.DataValueField = "CO_SIGL_OCORR";
            ddlTipoOcorrencia.DataBind();

            ddlTipoOcorrencia.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega os anos na combo ddlAno
        /// </summary>
        private void CarregaAno()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);
            ddlAno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega as modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os Cursos
        /// </summary>
        private void CarregaCursos()
        { 
            int CoModuCur = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            AuxiliCarregamentos.carregaSeriesCursos(ddlCurso, CoModuCur, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Carrega as Turmas
        /// </summary>
        private void CarregaTurmas()
        {
            int CoModuCur = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int curso = (!string.IsNullOrEmpty(ddlCurso.SelectedValue) ? int.Parse(ddlCurso.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, CoModuCur, curso, true);

        }

        /// <summary>
        /// Método responsável por mostrar todos os alunos ou aqueles resultantes dos filtros preenchidos
        /// </summary>
        private void CarregaAlunos()
        {
            int CoModuCur = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int curso = (!string.IsNullOrEmpty(ddlCurso.SelectedValue) ? int.Parse(ddlCurso.SelectedValue) : 0);
            int turma = (!string.IsNullOrEmpty(ddlTurma.SelectedValue) ? int.Parse(ddlTurma.SelectedValue) : 0);
            string ano = ddlAno.SelectedValue;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.TB07_ALUNO.CO_ALU into l1
                       from ls in l1.DefaultIfEmpty()
                       where (CoModuCur != 0 ? ls.TB44_MODULO.CO_MODU_CUR == CoModuCur : 0 == 0)
                       && (curso != 0 ? ls.CO_CUR == curso : 0 == 0)
                       && (turma != 0 ? ls.CO_TUR == turma : 0 == 0)
                       && (ano != "0" ? ls.CO_ANO_MES_MAT == ano : 0 == 0)
                       select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy(w => w.NO_ALU).ToList();

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataSource = res;
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Métodos de Campos

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCursos();
            CarregaAlunos();
        }

        protected void ddlCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurmas();
            CarregaAlunos();
        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
        }

        protected void ddlAno_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
        }

        #endregion
    }
}