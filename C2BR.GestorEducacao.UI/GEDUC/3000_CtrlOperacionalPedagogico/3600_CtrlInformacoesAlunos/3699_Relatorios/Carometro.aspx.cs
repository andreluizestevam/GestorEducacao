//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS POR SÉRIE/TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//           |                            | 

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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios
{
    public partial class Carometro : System.Web.UI.Page
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
                //CarregaUnidades();
                liAnoRefer.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaSituacao();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório

            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR;
            string infos, parametros/*,strP_CO_ANO_REFER,strTipo*/;
            int strP_CO_EMP_REF, strP_CO_EMP;
            string strP_CO_ANO_MES_MAT;

            //--------> Inicializa as variáveis
            // strP_CO_ANO_REFER = null;
            strP_CO_ANO_MES_MAT = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) :  0;
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_TUR = ddlTurma.SelectedValue != "" ?  int.Parse(ddlTurma.SelectedValue) : 0;

            //string DE_MODU = TB44_MODULO.RetornaPelaChavePrimaria(strP_CO_MODU_CUR).DE_MODU_CUR;
            //string NO_CUR = TB01_CURSO.RetornaPelaChavePrimaria(strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR).NO_CUR;
            //string NO_TUR = TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).NO_TURMA;
            string situacao = ddlSitua.SelectedValue;

            // strP_CO_ANO_REFER = txtAnoBase.Text;

            RptCarometro rpt = new RptCarometro();

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Ano: " + strP_CO_ANO_MES_MAT + " - Modalidade: " + ddlModalidade.SelectedItem + " - Curso: " + ddlSerieCurso.SelectedItem + " - Turma: " + ddlTurma.SelectedItem + ")";

            //RptCarometro rpt = new RptCarometro();
            lRetorno = rpt.InitReport(infos, parametros, strP_CO_ANO_MES_MAT, situacao, LoginAuxili.CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usário logado é professor.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlTurma.Enabled = true;

                    ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                           where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                           select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                    ddlTurma.DataTextField = "NO_TURMA";
                    ddlTurma.DataValueField = "CO_TUR";
                    ddlTurma.DataBind();
                    ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
                }
                else
                {
                    int ano = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;
                    ddlTurma.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                           join t in TB129_CADTURMAS.RetornaTodosRegistros() on rm.CO_TUR equals t.CO_TUR
                                           where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                           && rm.CO_MODU_CUR == modalidade
                                           && rm.CO_CUR == serie
                                           && rm.CO_ANO_REF == ano
                                           select new
                                           {
                                               t.NO_TURMA,
                                               rm.CO_TUR,
                                               t.CO_SIGLA_TURMA
                                           }).Distinct();

                    ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    ddlTurma.DataValueField = "CO_TUR";
                    ddlTurma.DataBind();

                    ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
                }                
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       select new
                       {
                           tb08.CO_ANO_MES_MAT
                       }).OrderByDescending(o => o.CO_ANO_MES_MAT).Distinct();

            ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";

            ddlAnoRefer.DataSource = res;
            ddlAnoRefer.DataBind();

        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataBind();
                ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
            }
            else
            {
                int ano = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;

                ddlModalidade.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                            join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                                            where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                             && rm.CO_ANO_REF == ano
                                            select new
                                            {
                                                mo.DE_MODU_CUR,
                                                rm.CO_MODU_CUR
                                            }).Distinct();


                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataBind();
                ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            ddlSerieCurso.Items.Clear();
            /*=========================================================
             * Esta linha foi comentada por que o relatório precisa
             * utilizar a serie/curso da unidade corrente, e não
             * da unidade de contrato.
             *=========================================================
             * Victor Martins Machado - 06/08/2013
             *========================================================*/
            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coEmp = LoginAuxili.CO_EMP;
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;

            if (modalidade != 0)
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                                join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                                where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade && tb01.CO_EMP == coEmp
                                                select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataBind();
                    ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
                }
                else
                {
                    int ano = ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0;
                    ddlSerieCurso.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                                join c in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals c.CO_CUR
                                                join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on rm.CO_CUR equals tb43.CO_CUR
                                                where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                                && rm.CO_MODU_CUR == modalidade
                                                && rm.CO_ANO_REF == ano
                                                select new
                                                {
                                                    c.NO_CUR,
                                                    rm.CO_CUR
                                                }).Distinct();

                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataBind();

                    ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", "0"));
                }

            }
            else
            {
                ddlSerieCurso.Items.Clear();
                ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
            }

        }

        private void CarregaSituacao()
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       select new ComboSituacao
                       {
                           coSit = tb08.CO_SIT_MAT
                       }).Distinct().OrderBy(o => o.coSit);

            ddlSitua.DataTextField = "Situ";
            ddlSitua.DataValueField = "coSit";

            ddlSitua.DataSource = res;
            ddlSitua.DataBind();

            ddlSitua.Items.Insert(0, new ListItem("Todos", "0"));
        }

        public class ComboSituacao
        {
            public string coSit { get; set; }
            public string Situ
            {
                get
                {
                    string s = "";

                    switch (this.coSit)
                    {
                        case "A":
                            s = "Matriculado";
                            break;
                        case "F":
                            s = "Finalizado";
                            break;
                        case "R":
                            s = "Pré-Matriculado";
                            break;
                        case "T":
                            s = "Transferido";
                            break;
                        case "C":
                            s = "Cancelado";
                            break;
                        case "X":
                            s = "Transferência Externa";
                            break;
                    }

                    return s;
                }
            }
        }
        #endregion

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }
    }
}