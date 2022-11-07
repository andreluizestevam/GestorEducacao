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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;
using C2BR.GestorEducacao.Reports.GSAUD;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios
{
    public partial class FichaCadastIndiviAluno : System.Web.UI.Page
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
                txtUnidadeEscola.Text = LoginAuxili.NO_FANTAS_EMP;
                CarregaAnos();
               //CarregaUnidades();
                CarregaModalidade();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();
                ddlModalidade.Enabled = false;
                ddlSerieCurso.Enabled = false;
                ddlTurma.Enabled = false;
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
           // string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP;
            int strP_CO_ALU;
            string infos, parametros;
            String Situacao = ddlSituacao.SelectedValue;
            int Ano = int.Parse(ddlAno.SelectedValue);
            //var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            //IRelatorioWeb lIRelatorioWeb;

           /* strIDSessao = Session.SessionID.ToString();
            strIdentFunc = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelFicInfoAluno");*/

//--------> Criação da Pasta
           /* if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);*/

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            //strP_CO_ALU = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            //strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_ALU = int.Parse(ddlAlunos.SelectedValue);            

            //lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            /*lRetorno = lIRelatorioWeb.RelFicInfoAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();*/

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "";

            RptRelInfGeraisAluno rpt = new RptRelInfGeraisAluno();
            lRetorno = rpt.InitReport(parametros, LoginAuxili.CO_EMP, infos, strP_CO_EMP, strP_CO_ALU, Ano, Situacao);

            //TesteChart rpt = new TesteChart();
            //lRetorno = rpt.InitReport();

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
       /* private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }*/

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //int unidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            //string coAnoMesMat = dd.SelectedValue != "" ? ddlAno.SelectedValue : "0";
            string Situacao = ddlSituacao.SelectedValue != "" ? ddlSituacao.SelectedValue : "0";

            ddlAlunos.Items.Clear();
            if (Situacao == "Todas")
            {
                ddlAlunos.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                        select new { tb07.CO_ALU, tb07.NO_ALU }
                                        ).Distinct().OrderBy(m => m.NO_ALU);
                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();
            }
            else if (Situacao == "NMat")
            {
                var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }
                                );

                ddlAlunos.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                       select new { tb07.CO_ALU, tb07.NO_ALU}
                                        ).Except(resultado).Distinct().OrderBy(m => m.NO_ALU);
                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();

            }else{
                if (modalidade == 0 && (serie == 0 && turma == 0))
                {
                    ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                            select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                    ddlAlunos.DataTextField = "NO_ALU";
                    ddlAlunos.DataValueField = "CO_ALU";
                    ddlAlunos.DataBind();
                }
                else if (modalidade != 0 && (serie == 0 && turma == 0))
                {
                    ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                            where tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                                            select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                    ddlAlunos.DataTextField = "NO_ALU";
                    ddlAlunos.DataValueField = "CO_ALU";
                    ddlAlunos.DataBind();
                }
                else if (serie != 0 && turma == 0)
                {
                    ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                            where tb08.CO_CUR == serie
                                            select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                    ddlAlunos.DataTextField = "NO_ALU";
                    ddlAlunos.DataValueField = "CO_ALU";
                    ddlAlunos.DataBind();
                }
                else if (turma != 0)
                {
                    ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                            where tb08.CO_TUR == turma
                                            select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                    ddlAlunos.DataTextField = "NO_ALU";
                    ddlAlunos.DataValueField = "CO_ALU";
                    ddlAlunos.DataBind();
                }

            }

        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
           // CarregaAluno();
        }

       


        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
   
      
        private void CarregaAnos()
        {

            string ano = DateTime.Now.Year.ToString();

            
            ddlAno.DataTextField = ano;
            ddlAno.Items.Insert(0, new ListItem(ano, ano));
            ddlAno.DataBind();
        }

       
       
            /*int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAnoRefer.DataSource = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                      where tb48.TB25_EMPRESA.CO_EMP == coEmp
                                      select new { tb48.CO_ANO_MES_MAT }).Distinct().OrderByDescending( g => g.CO_ANO_MES_MAT );

            ddlAnoRefer.DataTextField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataValueField = "CO_ANO_MES_MAT";
            ddlAnoRefer.DataBind();*/
            

        

        private void CarregaModalidade()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
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

            ddlTurma.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();

        }

        protected void ddlSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSituacao.SelectedValue == "Todas")
            {
                ddlModalidade.Enabled = false;
                ddlSerieCurso.Enabled = false;
                ddlTurma.Enabled = false;
            }
            else if (ddlSituacao.SelectedValue == "NMat")
            {
                ddlModalidade.Enabled = false;
                ddlSerieCurso.Enabled = false;
                ddlTurma.Enabled = false;
            }
            else
            {
                ddlModalidade.Enabled = true;
                ddlSerieCurso.Enabled = true;
                ddlTurma.Enabled = true;
            }

            CarregaAluno();
        }

    }
}
