//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES SIMPLIFICADA
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

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios
{
    public partial class AlunosAniverSimples : System.Web.UI.Page
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
                CarregaUnidades();
                //CarregaAnos();
                CarregaModalidade();
                CarregaSerieCurso();
                CarregaTurma();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int strP_CO_EMP_UNID_CONT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR;
            DateTime? dtInicio;
            DateTime? dtFim;
            string infos, parametros, strMesRef;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP_UNID_CONT = ddlUnidade.SelectedValue != "" ?  int.Parse(ddlUnidade.SelectedValue) : 0;
            strP_CO_MODU_CUR =  ddlModalidade.SelectedValue != "" ?  int.Parse(ddlModalidade.SelectedValue) : 0;
            strP_CO_CUR = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            strP_CO_TUR =  ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            dtInicio = txtPeriodoDe.Text != "" ? (DateTime?)DateTime.Parse(txtPeriodoDe.Text) : null;
            dtFim = txtPeriodoAte.Text != "" ? (DateTime?)DateTime.Parse(txtPeriodoAte.Text) : null;
            strMesRef = ddlMesRef.SelectedValue;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            if (ddlTipoAluno.SelectedValue == "M" || ddlTipoAluno.SelectedValue == "T")
            {
                parametros = "( Unid. Contrato: " + ddlUnidade.SelectedItem + " - Modalidade: " + ddlModalidade.SelectedItem  + " - Série: " + ddlSerieCurso.SelectedItem +
                    " - Turma: " + ddlTurma.SelectedItem  + " - Tipo Aluno: " + ddlTipoAluno.SelectedItem + " - Sexo: " + ddlSexo.SelectedItem +
                    " - Período: " + (txtPeriodoDe.Text != "" ? txtPeriodoDe.Text : "XXX") + " à " + (txtPeriodoAte.Text != "" ? txtPeriodoAte.Text : "XXX") + " - Mês Ref.: " + ddlMesRef.SelectedItem + " )";
            }
            else
                parametros = "( Tipo Aluno: " + ddlTipoAluno.SelectedItem + " - Sexo: " + ddlSexo.SelectedItem + " - Período: " + (txtPeriodoDe.Text != "" ? txtPeriodoDe.Text : "XXX") + " à " + (txtPeriodoAte.Text != "" ? txtPeriodoAte.Text : "XXX") + " - Mês Ref.: " + ddlMesRef.SelectedItem + " )";

            RptAlunosAniverSimples rpt = new RptAlunosAniverSimples();
            lRetorno = rpt.InitReport(parametros, LoginAuxili.CO_EMP, strP_CO_EMP_UNID_CONT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, dtInicio, dtFim, strMesRef, ddlSexo.SelectedValue, ddlTipoAluno.SelectedValue, infos);
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
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidade()
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
                DateTime dataAno = DateTime.Now;
                int ano = dataAno.Year;
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
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                                join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                                where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb01.CO_EMP == coEmp
                                                select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataBind();
                    ddlSerieCurso.Items.Insert(0, new ListItem("Todas", "0"));
                }
                else
                {
                    DateTime dataAno = DateTime.Now;
                    int ano = dataAno.Year;
                    ddlSerieCurso.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                                join c in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals c.CO_CUR
                                                join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on rm.CO_CUR equals tb43.CO_CUR
                                                where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                                && rm.CO_ANO_REF == ano
                                                && rm.CO_MODU_CUR == modalidade
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
                ddlTurma.Items.Clear();
                ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
            } 
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaTurma()
        {
            ddlTurma.Items.Clear();
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int serie = (ddlModalidade.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);

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
                    DateTime dataAno = DateTime.Now;
                    int ano = dataAno.Year;
                    ddlTurma.DataSource = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                                           join t in TB129_CADTURMAS.RetornaTodosRegistros() on rm.CO_TUR equals t.CO_TUR
                                           where rm.CO_COL_RESP == LoginAuxili.CO_COL
                                           && rm.CO_ANO_REF == ano
                                           && rm.CO_MODU_CUR == modalidade
                                           && rm.CO_CUR == serie
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
        #endregion

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
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