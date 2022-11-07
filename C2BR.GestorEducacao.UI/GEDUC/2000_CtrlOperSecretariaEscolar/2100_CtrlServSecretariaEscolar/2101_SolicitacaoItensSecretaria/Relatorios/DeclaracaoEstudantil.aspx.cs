//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: COMPROVANTE DE MATRÍCULA NOVA DE ALUNO
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
//using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar;
//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.Relatorios
{
    public partial class DeclaracaoEstudantil : System.Web.UI.Page
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
                CarregaAluno();
                DiasAula.Text = "Segunda à Sexta";
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            ////--------> Variáveis obrigatórias para gerar o Relatório
            //string codAlunoCad, Obs, horIni, horFim, DAula, horario;
            //int lRetorno;

            ////--------> Variáveis de parâmetro do Relatório
            //int codEmp = 0;

            ////--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            //codEmp = int.Parse(ddlUnidade.SelectedValue);
            //codAlunoCad = ddlAlunos.SelectedValue;
            //Obs = txtObs.Text;
            //horIni = horarioInicio.Text;
            //horFim = horarioFim.Text;
            //horario = "Horário de Aula: " + horarioInicio.Text + " até " + horarioFim.Text;
            //DAula = DiasAula.Text;

            //RptDeclEstudantil rpt = new RptDeclEstudantil();
            //lRetorno = rpt.InitReport(codEmp, codAlunoCad, Obs,  DAula, horario);
            //Session["Report"] = rpt;
            //Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
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

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.TB25_EMPRESA.CO_EMP == coEmp
                                 select new { tb08.CO_ANO_MES_MAT }).Distinct().OrderByDescending(m => m.CO_ANO_MES_MAT);

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

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
            int modalidade;

            if (!String.IsNullOrEmpty(coModuCur.ToString()))
                modalidade = Convert.ToInt32(coModuCur);
            else
                modalidade = int.Parse(ddlModalidade.SelectedValue);

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp)
                                        where tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.NO_CUR, tb01.CO_CUR }).OrderBy(c => c.NO_CUR);

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int unidade = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string coAnoMesMat = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : "0";

            ddlAlunos.Items.Clear();

            if (serie != 0)
            {
                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where tb08.TB25_EMPRESA.CO_EMP == unidade && tb08.CO_CUR == serie && tb08.CO_ANO_MES_MAT == coAnoMesMat
                                        && tb08.TB44_MODULO.CO_MODU_CUR == modalidade && tb08.CO_SIT_MAT == "A"
                                        select new { tb08.CO_ALU_CAD, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU_CAD";
                ddlAlunos.DataBind();
            }
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso(Convert.ToInt32(ddlModalidade.SelectedValue));
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso(null);
            CarregaAluno();
        }
    }

    public class ContratoDetalhe
    {
        public bool HideLogo { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public int Pagina { get; set; }
        public string Texto { get; set; }
    }
}
