//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: HISTÓRICO ESCOLAR DE ALUNOS 
// OBJETIVO: EMISSÃO DO HISTÓRICO ESCOLAR (PADRÃO)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 03/06/2013 | André Nobre Vinagre        | Alterei o dropdown de alunos para listar da tabela
//            |                            | de historico externo e da tabela de matrícula
//            |                            | 

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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3040_CtrlHistoricoEscolar;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3040_CtrlHistoricoEscolar.F3049_Relatorios
{
    public partial class HistoEscolPadrao : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
                CarregaTipoModalidades();
                CarregaAluno();

                CarregaModalidades();
                CarregaSeries();
                CarregaTurmas();
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            ///Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            ///Variáveis de parâmetro do Relatório
            int strP_CO_EMP_REF, strP_CO_ALU;
            string strP_CO_CLASS_CUR, str_INFOR_COMPLE, strParametrosRelatorio, strINFOS, str_TITULO;

            ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_ALU = int.Parse(ddlAlunos.SelectedValue);
            strP_CO_CLASS_CUR = ddlModalidade.SelectedValue;
            str_INFOR_COMPLE = txtInforComplementares.Text;
            strParametrosRelatorio = "";
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            str_TITULO = "HISTÓRICO ESCOLAR DO " + ddlModalidade.SelectedItem.ToString().ToUpper();

            RptHistorEscolarPadrao rpt = new RptHistorEscolarPadrao();
            lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, LoginAuxili.CO_EMP, strP_CO_EMP_REF, strP_CO_CLASS_CUR, strP_CO_ALU, strINFOS, str_TITULO, str_INFOR_COMPLE);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }      
        #endregion

        #region Carregamento dos DropDownList da página

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Modalidades
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO));
            if (ddlUnidade.Items.FindByValue(LoginAuxili.CO_EMP.ToString()) != null)
                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Carrega os tipos de classificação de modalidades de ensino regular
        /// </summary>
        private void CarregaTipoModalidades()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoClassificacaoEnsino.ResourceManager, true));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int moda = (!string.IsNullOrEmpty(ddlModal.SelectedValue) ? int.Parse(ddlModal.SelectedValue) : 0);
            int serie = (!string.IsNullOrEmpty(ddlSerie.SelectedValue) ? int.Parse(ddlSerie.SelectedValue) : 0);
            int Turma = (!string.IsNullOrEmpty(ddlTurma.SelectedValue) ? int.Parse(ddlTurma.SelectedValue) : 0);

            string nivelCur = ddlModalidade.SelectedValue;

            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                    join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                    where tb08.CO_EMP == coEmp && tb01.CO_NIVEL_CUR.Contains(nivelCur)
                                    && (moda != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == moda : 0 == 0)
                                    && (serie != 0 ? tb08.CO_CUR == serie : 0 == 0)
                                    && (Turma != 0 ? tb08.CO_TUR == Turma : 0 == 0)
                                    select new ListaAlunos { NomeAluno = tb08.TB07_ALUNO.NO_ALU, CodigoAluno = tb08.TB07_ALUNO.CO_ALU }).Distinct().OrderBy(a => a.NomeAluno).ToList();

            var resultadoHist = (from tb130 in TB130_HIST_EXT_ALUNO.RetornaTodosRegistros()
                                 join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb130.CO_CUR equals tb01.CO_CUR
                                 join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb130.CO_ALU equals tb07.CO_ALU
                                 where tb130.CO_EMP == coEmp && tb01.CO_NIVEL_CUR.Contains(nivelCur)
                                    && (moda != 0 ? tb130.CO_MODU_CUR == moda : 0 == 0)
                                    && (serie != 0 ? tb130.CO_CUR == serie : 0 == 0)
                                 select new ListaAlunos { NomeAluno = tb07.NO_ALU, CodigoAluno = tb07.CO_ALU }).Distinct().OrderBy(a => a.NomeAluno).ToList().Except(resultado);

            foreach (var iRes in resultadoHist)
            {
                resultado.Add(new ListaAlunos { NomeAluno = iRes.NomeAluno, CodigoAluno = iRes.CodigoAluno });
            }

            ddlAlunos.DataSource = resultado;
            ddlAlunos.DataTextField = "NomeAluno";
            ddlAlunos.DataValueField = "CodigoAluno";
            ddlAlunos.DataBind();

            ddlAlunos.Enabled = ddlAlunos.Items.Count > 0;
        }  

        /// <summary>
        /// Carrega as modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModal, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as séries
        /// </summary>
        private void CarregaSeries()
        {
            int moda = (!string.IsNullOrEmpty(ddlModal.SelectedValue) ? int.Parse(ddlModal.SelectedValue) : 0);
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue)  ? int.Parse(ddlUnidade.SelectedValue) : 0);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerie, moda, coEmp, true);
        }

        /// <summary>
        /// Carrega as Turmas
        /// </summary>
        private void CarregaTurmas()
        {
            int moda = (!string.IsNullOrEmpty(ddlModal.SelectedValue) ? int.Parse(ddlModal.SelectedValue) : 0);
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int serie = (!string.IsNullOrEmpty(ddlSerie.SelectedValue) ? int.Parse(ddlSerie.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, moda, serie, true);
        }

        #endregion

        #region Eventos de componentes da página
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "")
                CarregaAluno();
        }

        protected void ddlModal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSeries();
            CarregaAluno();
        }

        protected void ddlSerie_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurmas();
            CarregaAluno();
        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        #endregion

        #region Classes
        /// <summary>
        /// Classe para listagem de alunos disponiveis para histórico
        /// </summary>
        public class ListaAlunos
        {
            public string NomeAluno { get; set; }
            public int CodigoAluno { get; set; }
        }
        #endregion
    }
}
