//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: FOLHA MENSAL DE CHAMADA DE ALUNOS POR SÉRIE/TURMA E MATÉRIA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 23/05/2013| André Nobre Vinagre        | Adicionada a consulta alunos diferentes de cancelado
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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3499_Relatorios
{
    public partial class ListaFrequenciaDiaria : System.Web.UI.Page
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
                CarregaAnos();
                CarregaModalidade();
                txtData.Text = DateTime.Now.ToString();
            }
        }

        //====> Método que faz a chamada de outro método de acordo com o Tipo
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            PautaChamadaFrente();
        }

        //====> Processo de Geração do Relatório
        void PautaChamadaFrente()
        {
            TB01_CURSO tb01 = TB01_CURSO.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue), int.Parse(ddlModalidade.SelectedValue),
            int.Parse(ddlSerieCurso.SelectedValue));

            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR;
            string strP_CO_ANO_REF;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;

            string turn = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                           where tb06.CO_TUR == strP_CO_TUR
                           select tb06).FirstOrDefault().CO_PERI_TUR;
            string NomTurno;
            switch (turn)
            {
                case "M":
                    NomTurno = "Matutino";
                    break;
                case "V":
                    NomTurno = "Vespertino";
                    break;
                case "N":
                    NomTurno = "Noturno";
                    break;
                default:
                    NomTurno = "**";
                    break;
            }

            int coMat = (ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0);
            string noDisc = "";
            if (coMat != 0)
            {
                noDisc = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                          join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                          where tb02.CO_MAT == coMat
                          select tb107).FirstOrDefault().NO_MATERIA;
            }

            strParametrosRelatorio = "( " + ddlModalidade.SelectedItem.ToString() + " - " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).CO_SIGLA_TURMA + " / " + NomTurno + " - Ano " +
                ddlAnoRefer.SelectedItem.ToString().Trim() + (coMat != 0 ? " - Disciplina: " + noDisc + " )" : " )");

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            string NO_RELATORIO = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GEDUC/3000_CtrlOperacionalPedagogico/3400_CtrlFrequenciaEscolar/3499_Relatorios/ListaFrequenciaDiaria.aspx");
            RptListaFrequenciaDiaria rpt = new RptListaFrequenciaDiaria();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, txtData.Text, strINFOS, NO_RELATORIO, coMat);
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
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaAno(ddlAnoRefer, coEmp, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidade()
        {
            int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
            else
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, true);

            CarregaSerieCurso();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verfica se o usuário logado é professor.
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;
            int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, false);
            else
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, true);

            CarregaTurma();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma()
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);

            if (LoginAuxili.FLA_PROFESSOR != "S")
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, serie, false);
            else
                AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, true);

            CarregaDisciplina();
        }
        private void CarregaDisciplina()
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string ano = ddlAnoRefer.SelectedValue;
            AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, coEmp, modalidade, serie, ano, false, false);
            ddlDisciplina.Items.Insert(0, new ListItem("Nenhuma", ""));
        }

        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidade();
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }
    }
}
