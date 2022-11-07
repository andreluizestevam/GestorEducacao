//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: DADOS/PARAMETRIZAÇÃO UNIDADES DE ENSINO
// OBJETIVO: EMISSÃO DO MAPA DE PERFIL DE DESEMPENHO DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

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
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios
{
    public partial class MapaPerfiDesempAluno : System.Web.UI.Page
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
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaMaterias();
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            int strP_CO_EMP, lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string parametros, infos,  strP_CO_EMP_REF, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_ID_MATERIA, strP_NOTA_MIN, strP_NOTA_MAX;            

//--------> Inicializa as variáveis
            strP_CO_EMP_REF = null;
            strP_CO_ANO_REF = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_ID_MATERIA = null;
            strP_NOTA_MAX = null;
            strP_NOTA_MIN = null;
            infos = null;
            parametros = null;
//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
           
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = ddlUnidade.SelectedValue;
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_ID_MATERIA = ddlMateria.SelectedValue;
            strP_NOTA_MAX = txtNotaMax.Text != "" ? txtNotaMax.Text : "T";
            strP_NOTA_MIN = txtNotaMin.Text != "" ? txtNotaMin.Text : "T";
            //string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP).sigla;

            parametros = "Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Ano Referência: " + ddlAnoRefer.SelectedItem.ToString()
               + " - Modalidade: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() +
               " - Turma: " + ddlTurma.SelectedItem.ToString();
            parametros += (txtNotaMin.Text != "" && txtNotaMax.Text !="") ? " - Faixa de Notas: " + txtNotaMin.Text + " até " + txtNotaMax.Text : "";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptMapaPerfilDesempAluno rpt = new RptMapaPerfilDesempAluno();

            lRetorno = rpt.InitReport(parametros, strP_CO_EMP,strP_CO_EMP_REF, strP_CO_ANO_REF, strP_CO_CUR, strP_CO_TUR,strP_CO_MODU_CUR, strP_ID_MATERIA, strP_NOTA_MIN, strP_NOTA_MAX,infos);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);       
        }       
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", "T"));
        } 

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlTurma.Items.Clear();

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR }).OrderBy(t => t.NO_TURMA);

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }

            ddlTurma.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Ano de Referencia
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "T" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAnoRefer.Items.Clear();

            if (coEmp != 0)
            {
                ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                          where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                          select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending( g => g.CO_ANO_MES_MAT );

                ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
                ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
                ddlAnoRefer.DataBind();   
            }

            ddlAnoRefer.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades Escolares
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {            
            int coEmp = ddlUnidade.SelectedValue != "T" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;            
            string anoGrade = ddlAnoRefer.SelectedValue;

            ddlSerieCurso.Items.Clear();

            if (anoGrade != "T")
            {
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp)
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp) on tb43.CO_CUR equals tb01.CO_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();    
            }            

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int coEmp = ddlUnidade.SelectedValue != "T" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;

            ddlMateria.Items.Clear();

            if (turma != 0)
            {
                ddlMateria.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                         where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_EMP == coEmp
                                         && tb43.CO_ANO_GRADE == anoGrade
                                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).OrderBy( m => m.NO_MATERIA );

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "ID_MATERIA";
                ddlMateria.DataBind();                
            }

            ddlMateria.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModalidade.Items.Count > 0)
            {
                CarregaSerieCurso();
                CarregaTurma();
                CarregaMaterias();
            }
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
        }
    }
}
