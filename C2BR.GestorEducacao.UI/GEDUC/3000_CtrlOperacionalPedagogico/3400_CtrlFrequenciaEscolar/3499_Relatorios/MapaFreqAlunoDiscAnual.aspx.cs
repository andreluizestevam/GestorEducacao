//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: RELAÇÃO RESUMO DE FREQUÊNCIA POR SÉRIE/TURMA OU ALUNO NO ANO LETIVO
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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3499_Relatorios
{
    public partial class MapaFreqAlunoDiscAnual : System.Web.UI.Page
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

                if (ddlTipo.SelectedValue.ToString() == "S")
                {
                    liTurma.Visible = liModalidade.Visible = liSerie.Visible = true;
                    liAluno.Visible = false;
                    CarregaModalidade();
                    CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
                    CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
                }
                else
                {
                    liTurma.Visible = liModalidade.Visible = liSerie.Visible = false;
                    liAluno.Visible = true;
                    CarregaAluno();
                }
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_MAT, strP_CO_PARAM_FREQ, strP_CO_PARAM_FREQ_TIPO;

//--------> Inicializa as variáveis
            strParametrosRelatorio = null;
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_ANO_REF = null;
            strP_CO_ALU = null;
            strP_CO_MAT = null;
            strP_CO_PARAM_FREQ = null;
            strP_CO_PARAM_FREQ_TIPO = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            int coEmp = int.Parse(ddlUnidade.SelectedValue);

            strP_CO_EMP = coEmp.ToString();                        
            
            if (liAluno.Visible)
            {
                strP_CO_ALU = ddlAluno.SelectedValue;
                strP_CO_MODU_CUR = "0";
                strP_CO_CUR = "0";
                strP_CO_TUR = "0";                
            }
            else
            {
                strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
                strP_CO_CUR = ddlSerieCurso.SelectedValue;
                strP_CO_TUR = ddlTurma.SelectedValue;                
                strP_CO_ALU = "0";
            }
            
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;

            if (!liAluno.Visible)
            {
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                strP_CO_PARAM_FREQ = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, modalidade, serie).CO_PARAM_FREQUE.ToString();
                TB01_CURSO curso = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, modalidade, serie);
                strP_CO_PARAM_FREQ_TIPO = curso != null && curso.CO_PARAM_FREQ_TIPO != null ? curso.CO_PARAM_FREQ_TIPO : "M";
            }
            else
            {
                int coAlu = ddlAluno.Items.Count > 0 ? int.Parse(ddlAluno.SelectedValue)  : 0;
                
                var tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 join tb01 in TB01_CURSO.RetornaTodosRegistros() on lTb08.CO_CUR equals tb01.CO_CUR
                                 where lTb08.CO_ALU == coAlu && lTb08.TB25_EMPRESA.CO_EMP == coEmp && lTb08.CO_ANO_MES_MAT == ddlAnoRefer.SelectedValue
                                 select new
                                 {
                                     lTb08.CO_CUR, lTb08.CO_TUR, lTb08.TB44_MODULO.CO_MODU_CUR, tb01.CO_PARAM_FREQ_TIPO, tb01.CO_PARAM_FREQUE
                                 }).First();

                strP_CO_CUR = tb08.CO_CUR.ToString();
                strP_CO_MODU_CUR = tb08.CO_MODU_CUR.ToString();
                strP_CO_TUR = tb08.CO_TUR.ToString();
                strP_CO_PARAM_FREQ = tb08.CO_PARAM_FREQUE;
                strP_CO_PARAM_FREQ_TIPO = tb08.CO_PARAM_FREQ_TIPO != null ? tb08.CO_PARAM_FREQ_TIPO : "M";
            }
            
            strP_CO_MAT = "T";

            if (liAluno.Visible == false)
            {
                strParametrosRelatorio += "( Unidade: " + ddlUnidade.SelectedItem.ToString();
                strParametrosRelatorio += " - Ano Referência: " + ddlAnoRefer.SelectedItem.ToString().Trim();
                strParametrosRelatorio += " - Tipo de Relatório: " + ddlTipo.SelectedItem.ToString();
                strParametrosRelatorio += " - Módulo: " + ddlModalidade.SelectedItem.ToString();
                strParametrosRelatorio += " - Série: " + ddlSerieCurso.SelectedItem.ToString();
                strParametrosRelatorio += " - Turma: " + ddlTurma.SelectedItem.ToString() + " )";
            }
            else
                strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Ano Referência: " + ddlAnoRefer.SelectedItem.ToString().Trim() +
                   " - Aluno: " + ddlAluno.SelectedItem.ToString() + " )";

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GEDUC/3000_CtrlOperacionalPedagogico/3400_CtrlFrequenciaEscolar/3499_Relatorios/MapaFreqAlunoDiscAnual.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptMapaFreqAlunoDiscAnual rtp = new RptMapaFreqAlunoDiscAnual();
            lRetorno = rtp.InitReport(strParametrosRelatorio, LoginAuxili.CO_EMP, strINFOS, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_MAT, strP_CO_PARAM_FREQ, strP_CO_PARAM_FREQ_TIPO, NO_RELATORIO);
            Session["Report"] = rtp;
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
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            string anoMesMat = ddlAnoRefer.SelectedValue;

            if (anoMesMat != "")
            {
                ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       where tb08.CO_ANO_MES_MAT == anoMesMat && (tb08.CO_SIT_MAT.Equals("A") || tb08.CO_SIT_MAT.Equals("F"))                                       
                                       select new { tb08.TB07_ALUNO.NO_ALU, tb08.TB07_ALUNO.CO_ALU }).OrderBy( m => m.NO_ALU );

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();
            }
            else
                ddlAluno.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidade()
        {            
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();                                              
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
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

            ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp)
                                        join tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp) on tb43.CO_CUR equals tb01.CO_CUR
                                        where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                        && tb43.CO_ANO_GRADE == anoGrade
                                        select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( g => g.NO_CUR );

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();         
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
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
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            if (ddlTipo.SelectedValue.ToString() == "S")
            {
                liTurma.Visible = liModalidade.Visible = liSerie.Visible = true;
                liAluno.Visible = false;
                CarregaModalidade();
                CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
                CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            }
            else
            {
                liTurma.Visible = liModalidade.Visible = liSerie.Visible = false;
                liAluno.Visible = true;
                CarregaAluno();
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue.ToString() == "S")
            {
                liTurma.Visible = liModalidade.Visible = liSerie.Visible = true;
                liAluno.Visible = false;
                CarregaModalidade();
                CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
                CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            }
            else
            {
                liTurma.Visible = liModalidade.Visible = liSerie.Visible = false;
                liAluno.Visible = true;
                CarregaAluno();
            }
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue == "S")
            {
                liTurma.Visible = liModalidade.Visible = liSerie.Visible = true;
                liAluno.Visible = false;
                CarregaModalidade();
                CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
                CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            }
            else
            {
                liTurma.Visible = liModalidade.Visible = liSerie.Visible = false;
                liAluno.Visible = true;
                CarregaAluno();
            }

        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
        }  
    }
}
