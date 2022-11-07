//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: FORMULÁRIO DE RESULTADOS DE PESQUISA INSTITUCIONAL
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1339_Relatorios
{
    public partial class MapaResultadoAvaliacao : System.Web.UI.Page
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
                CarregaNumPesq();
                CarregaFuncionarios();
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
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_TIPO_AVAL, strP_CO_PESQ_AVAL, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, 
                    strP_CO_MAT, strP_DT_INI, strP_DT_FIM, strP_CO_COL, strP_NO_CUR, strP_NO_TUR, strP_NO_MAT;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelAnaliticoResAvaliacao");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis         
            strP_CO_TIPO_AVAL = null;
            strP_CO_PESQ_AVAL = null;
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;
            strP_CO_MAT = null;
            strP_DT_INI = null;
            strP_DT_FIM = null;
            strP_CO_COL = null;
            strP_NO_CUR = null;
            strP_NO_TUR = null;
            strP_NO_MAT = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_TIPO_AVAL = ddlTipoAvaliacao.SelectedValue;
            strP_CO_PESQ_AVAL = ddlNumPesq.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_CO_MAT = ddlMateria.SelectedValue;
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;
            strP_CO_COL = ddlFuncionarios.SelectedValue;
            strP_NO_CUR =  ddlSerieCurso.SelectedItem.ToString();
            strP_NO_TUR =  ddlTurma.SelectedItem.ToString();
            strP_NO_MAT = ddlMateria.SelectedItem.ToString();

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelAnaliticoResAvaliacao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_TIPO_AVAL, strP_CO_PESQ_AVAL, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_DT_INI, strP_DT_FIM, strP_CO_COL, strP_NO_CUR, strP_NO_TUR, strP_NO_MAT);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        }       
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Tipo de Avaliação
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlTipoAvaliacao.DataSource = TB73_TIPO_AVAL.RetornaTodosRegistros();
            ddlTipoAvaliacao.DataTextField = "NO_TIPO_AVAL";
            ddlTipoAvaliacao.DataValueField = "CO_TIPO_AVAL";
            ddlTipoAvaliacao.DataBind();
        } 

        /// <summary>
        /// Método que carrega o dropdown de Números de Pesquisa
        /// </summary>
        private void CarregaNumPesq()
        {
            int coTipoAval = ddlTipoAvaliacao.SelectedValue != "" ? int.Parse(ddlTipoAvaliacao.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlNumPesq.DataSource = (from tb78 in TB78_PESQ_AVAL.RetornaTodosRegistros()
                                     where tb78.TB73_TIPO_AVAL.CO_TIPO_AVAL == coTipoAval && tb78.CO_EMP == coEmp
                                    select new { tb78.CO_PESQ_AVAL }).OrderBy( p => p.CO_PESQ_AVAL );

            ddlNumPesq.DataTextField = "CO_PESQ_AVAL";
            ddlNumPesq.DataValueField = "CO_PESQ_AVAL";
            ddlNumPesq.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlFuncionarios.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy( c => c.NO_COL );

            ddlFuncionarios.DataTextField = "NO_COL";
            ddlFuncionarios.DataValueField = "CO_COL";
            ddlFuncionarios.DataBind();

            ddlFuncionarios.Items.Insert(0, new ListItem(" ", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turma
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

                ddlTurma.Items.Insert(0, new ListItem(" ", "T"));
            }
            else
                ddlTurma.Items.Insert(0, new ListItem(" ", "T"));
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

            ddlModalidade.Items.Insert(0, new ListItem(" ", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {            
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.Items.Clear();

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp)
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(coEmp) on tb43.CO_CUR equals tb01.CO_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();

                ddlSerieCurso.Items.Insert(0, new ListItem(" ", "T"));
            }
            else
                ddlSerieCurso.Items.Insert(0, new ListItem(" ", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "T" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "T" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlMateria.Items.Clear();

            if (modalidade != 0)
            {
                ddlMateria.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                         where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.CO_CUR == serie
                                         join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy( g => g.NO_MATERIA );

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "CO_MAT";
                ddlMateria.DataBind();

                ddlMateria.Items.Insert(0, new ListItem(" ", "T"));
            }
            else
                ddlMateria.Items.Insert(0, new ListItem(" ", "T"));
        }
        #endregion

        protected void ddlTipoAvaliacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaNumPesq();
        }

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

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
            CarregaNumPesq();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }
    }
}
