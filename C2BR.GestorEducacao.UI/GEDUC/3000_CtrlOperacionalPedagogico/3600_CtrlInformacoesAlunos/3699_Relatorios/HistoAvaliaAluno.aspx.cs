//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: RELAÇÃO DE AVALIAÇÕES GERAIS DO ALUNO
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios
{
    public partial class HistoAvaliaAluno : System.Web.UI.Page
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
                CarregaModalidades();
                CarregaSerieCurso(null);
                CarregaTurma(null);
                CarregaMaterias();
                CarregaAluno();
            }
        }

//====> Método que faz a chamada de outro método de acordo com o Aluno
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlAlunos.SelectedValue.ToString() == "T")
                RelMapaHistAlunoSerTur();
            else
                RelMapaHistAluno();
        }

//====> Processo de Geração do Relatório
        void RelMapaHistAluno()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_MAT, strP_CO_ALU, strP_TP_AVAL;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strParametrosRelatorio = null;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelMapaHistAluno");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_ANO_MES_MAT = null;
            strP_CO_MAT = null;
            strP_CO_ALU = null;
            strP_TP_AVAL = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;
            strP_CO_MAT = ddlMateria.SelectedValue;
            strP_CO_ALU = ddlAlunos.SelectedValue;
            strP_TP_AVAL = ddlTpAvaliacao.SelectedValue;

            strParametrosRelatorio = "Ano Referência: " + ddlAnoRefer.SelectedItem.ToString() + " - Tipo de Avaliação: " + ddlTpAvaliacao.SelectedItem.ToString() + " - Matéria: " + ddlMateria.SelectedItem.ToString();
            
            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelMapaHistAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_MAT, strP_CO_ALU, strP_TP_AVAL);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        }

//====> Processo de Geração do Relatório
        void RelMapaHistAlunoSerTur()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_MAT, strP_CO_ALU, strP_TP_AVAL;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strParametrosRelatorio = null;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelMapaHistAlunoSerTur");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_ANO_MES_MAT = null;
            strP_CO_MAT = null;
            strP_CO_ALU = null;
            strP_TP_AVAL = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_CO_ANO_MES_MAT = ddlAnoRefer.SelectedValue;
            strP_CO_MAT = ddlMateria.SelectedValue;
            strP_CO_ALU = ddlAlunos.SelectedValue;
            strP_TP_AVAL = ddlTpAvaliacao.SelectedValue;

            strParametrosRelatorio = "Ano Referência: " + ddlAnoRefer.SelectedItem.ToString() + " - Módulo: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() +
            " - Turma: " + ddlTurma.SelectedItem.ToString() + " - Tipo de Avaliação: " + ddlTpAvaliacao.SelectedItem.ToString() + " - Matéria: " + ddlMateria.SelectedItem.ToString();

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelMapaHistAlunoSerTur(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_MAT, strP_CO_ALU, strP_TP_AVAL);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
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

            int serie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();

                ddlTurma.Items.Insert(0, new ListItem("Todas", "T"));
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlTurma.Items.Insert(0, new ListItem("Todas", "T"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                      where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                      select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending( g => g.CO_ANO_MES_MAT );

            ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
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

            ddlSerieCurso.Items.Clear();

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_ANO_GRADE == anoGrade && tb01.CO_EMP == coEmp
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "T" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;

            ddlAlunos.Items.Clear();

            if (turma != 0)
            {
                ddlAlunos.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                        where tb48.TB44_MODULO.CO_MODU_CUR == modalidade && tb48.CO_CUR == serie
                                        && tb48.CO_ANO_MES_MAT == anoGrade && tb48.CO_TUR == turma
                                        select new { tb48.TB07_ALUNO.CO_ALU, tb48.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(a => a.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();

                ddlAlunos.Items.Insert(0, new ListItem("Todos", "T"));
            }
            else
            {
                ddlAlunos.Items.Insert(0, new ListItem("Todos", "T"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "T" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoGrade = ddlAnoRefer.SelectedValue;

            ddlMateria.Items.Clear();

            if (turma != 0)
            {
                ddlMateria.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                         where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie && tb43.CO_EMP == coEmp
                                         && tb43.CO_ANO_GRADE == anoGrade
                                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy(g => g.NO_MATERIA);

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "CO_MAT";
                ddlMateria.DataBind();

                ddlMateria.Items.Insert(0, new ListItem("Todos", "T"));
            }
            else
            {
                ddlMateria.Items.Insert(0, new ListItem("Todos", "T"));
            }
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaAluno();
            CarregaMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaAluno();
            CarregaMaterias();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso(null);
            CarregaTurma(null);
            CarregaMaterias();
            CarregaAluno();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(int.Parse(ddlModalidade.SelectedValue));
            CarregaTurma(int.Parse(ddlModalidade.SelectedValue));
            CarregaAluno();
            CarregaMaterias();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            CarregaMaterias();
        }
    }
}
