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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3499_Relatorios
{
    public partial class PautaChamadaSerieTurma : System.Web.UI.Page
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
                CarregaSerieCurso(null);
                CarregaTurma(null);
                CarregaMaterias(null);
            }
        }

//====> Método que faz a chamada de outro método de acordo com o Tipo
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlTipo.SelectedValue.ToString() == "A")
            {
                PautaChamadaFrente();
                PautaChamadaVerso();
            }
            else if (ddlTipo.SelectedValue.ToString() == "F")
                PautaChamadaFrente();
            else
                PautaChamadaVerso();
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
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_MES;
            string strP_CO_ANO_REF;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;            
            strP_MES = int.Parse(ddlMesReferencia.SelectedValue);

            strParametrosRelatorio = "( "+ ddlModalidade.SelectedItem.ToString() + " - " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + (strP_CO_TUR != 0 ? ddlTurma.SelectedItem.Text : "             ") + " - Ano Letivo: " + ddlAnoRefer.SelectedItem.ToString().Trim()
                + " - Mês: " + (strP_MES != 0 ? ddlMesReferencia.SelectedItem.ToString() : "             ");

            if (tb01.CO_PARAM_FREQ_TIPO != "M")
            {
                strP_CO_MAT = 0;
                strParametrosRelatorio = strParametrosRelatorio + " - Matéria: ***** )";
            }
            else
            {
                strP_CO_MAT = int.Parse(ddlMateria.SelectedValue);
                strParametrosRelatorio = strParametrosRelatorio + " - Matéria: " + (strP_CO_MAT != 0 ? ddlMateria.SelectedItem.ToString() : "             ") + " )";

            }
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GEDUC/3000_CtrlOperacionalPedagogico/3400_CtrlFrequenciaEscolar/3499_Relatorios/PautaChamadaSerieTurma.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptPautaChamadaSerieTurma rpt = new RptPautaChamadaSerieTurma();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_MES, strINFOS, NO_RELATORIO);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

//====> Processo de Geração do Relatório
        void PautaChamadaVerso()
        {
            TB01_CURSO tb01 = TB01_CURSO.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue), int.Parse(ddlModalidade.SelectedValue),
                        int.Parse(ddlSerieCurso.SelectedValue));

            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_MES;
            string strP_CO_ANO_REF, strP_PROF_RESP = "";

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            strP_MES = int.Parse(ddlMesReferencia.SelectedValue);

            strParametrosRelatorio = "( " + ddlModalidade.SelectedItem.ToString() + " - " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + (strP_CO_TUR != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(strP_CO_TUR).CO_SIGLA_TURMA : "             ") + 
                " - Ano Letivo: " + ddlAnoRefer.SelectedItem.ToString().Trim() + " - Mês: " + (strP_MES != 0 ? ddlMesReferencia.SelectedItem.ToString() : "             ");

            if (tb01.CO_PARAM_FREQ_TIPO != "M")
            {
                strP_CO_MAT = 0;
                strParametrosRelatorio = strParametrosRelatorio + " - Matéria: ***** )";
                strP_PROF_RESP = "( Professor Responsável: ***** )";
            }
            else
            {
                strP_CO_MAT = int.Parse(ddlMateria.SelectedValue);
                strParametrosRelatorio = strParametrosRelatorio + " - Matéria: " + (strP_CO_MAT != 0 ? ddlMateria.SelectedItem.ToString() : "             ") + " )";

                int anoRefer = int.Parse(strP_CO_ANO_REF);
                var tbResponMat = (from tbRespMat in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                   join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbRespMat.CO_COL_RESP equals tb03.CO_COL
                                   where (tbRespMat.CO_ANO_REF == anoRefer) && (tbRespMat.CO_MODU_CUR == strP_CO_MODU_CUR) && (tbRespMat.CO_CUR == strP_CO_CUR)
                                   && (tbRespMat.CO_TUR == strP_CO_TUR)
                                   && (tbRespMat.CO_MAT == strP_CO_MAT)
                                   select new
                                   {
                                       CO_MAT_COL = tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "."),
                                       tb03.NO_COL
                                   }).FirstOrDefault();

                if (tbResponMat != null)
                {
                    strP_PROF_RESP = "( Professor Responsável: " + tbResponMat.CO_MAT_COL + " " + tbResponMat.NO_COL + " )";
                }
                else
                {
                    strP_PROF_RESP = "( Professor Responsável: ***** )";
                }
            }
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptPautaChamadaSerieTurmaVerso rpt = new RptPautaChamadaSerieTurmaVerso();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_PROF_RESP, strINFOS, int.Parse(ddlMesReferencia.SelectedValue));
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
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            if (coEmp != 0)
            {
                ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                          where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                          select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending( g => g.CO_ANO_MES_MAT );

                ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
                ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
                ddlAnoRefer.DataBind();
            }
            else
                ddlAnoRefer.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidade()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                //ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

                //ddlModalidade.DataTextField = "DE_MODU_CUR";
                //ddlModalidade.DataValueField = "CO_MODU_CUR";
                //ddlModalidade.DataBind();
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            }
            else
            {
                int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, true);
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verfica se o usuário logado é professor.
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso(int? coModuCur)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;
            int modalidade;

            if (!String.IsNullOrEmpty(coModuCur.ToString()))
                modalidade = Convert.ToInt32(coModuCur);
            else
                modalidade = int.Parse(ddlModalidade.SelectedValue);

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp)
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp) on tb43.CO_CUR equals tb01.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            && tb43.CO_ANO_GRADE == anoGrade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(g => g.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
            }
            else
            {
                int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade, LoginAuxili.CO_COL, ano, true);
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma(int? coModuCur)
        {
            int modalidade;
            if (!String.IsNullOrEmpty(coModuCur.ToString()))
                modalidade = Convert.ToInt32(coModuCur);
            else
                modalidade = int.Parse(ddlModalidade.SelectedValue);

            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
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
                    int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                    AuxiliCarregamentos.CarregaTurmasProfeResp(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, LoginAuxili.CO_COL, ano, true);
                }
            }
            else
                ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias, verifica se o usuário logado é professor.
        /// </summary>
        /// <param name="codMod">Id da modalidade</param>
        private void CarregaMaterias(int? codMod)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;

            if (serie != 0)
            {
                string tipoFreq = TB01_CURSO.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue), int.Parse(ddlModalidade.SelectedValue),
                        int.Parse(ddlSerieCurso.SelectedValue)).CO_PARAM_FREQ_TIPO;

                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    if (tipoFreq == "M")
                    {
                        liMateria.Visible = true;
                        ddlMateria.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                                 where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                                                 && tb43.CO_ANO_GRADE == anoGrade && tb43.CO_EMP == coEmp
                                                 join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                                 join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                                 select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

                        ddlMateria.DataTextField = "NO_MATERIA";
                        ddlMateria.DataValueField = "CO_MAT";
                        ddlMateria.DataBind();
                        ddlMateria.Items.Insert(0, new ListItem("Todas", "0"));
                    }
                    else
                    {
                        liMateria.Visible = false;
                        ddlMateria.Items.Clear();
                    }
                }
                else
                {
                    if (tipoFreq == "M")
                    {
                        liMateria.Visible = true;
                        //ddlMateria.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                        //                         where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                        //                         && tb43.CO_ANO_GRADE == anoGrade && tb43.CO_EMP == coEmp
                        //                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                        //                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                        //                         select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

                        //ddlMateria.DataTextField = "NO_MATERIA";
                        //ddlMateria.DataValueField = "CO_MAT";
                        //ddlMateria.DataBind();

                        int ano = (ddlAnoRefer.SelectedValue != "" ? int.Parse(ddlAnoRefer.SelectedValue) : 0);
                        AuxiliCarregamentos.CarregaMateriasProfeRespon(ddlMateria, LoginAuxili.CO_COL, modalidade, serie, ano,true);
                    }
                    else
                    {
                        liMateria.Visible = false;
                        ddlMateria.Items.Clear();
                    }
                }
                
            }
            else
                ddlMateria.Items.Clear();
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaMaterias(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidade();
            CarregaSerieCurso(null);
            CarregaTurma(null);
            CarregaMaterias(null);
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaMaterias(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaMaterias(int.Parse(ddlModalidade.SelectedValue));
        }
    }
}
