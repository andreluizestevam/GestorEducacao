//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: FORMULÁRIO DE PESQUISA INSTITUCIONAL PARA AVALIAÇÃO
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
    public partial class FormAvaliacaoModelo : System.Web.UI.Page
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
                CarregaNumPesquisa();
                CarregaCampos(ddlNumPesquisa.Items.Count > 0 ? int.Parse(ddlNumPesquisa.SelectedValue) : 0);
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_NUM_PESQ;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelAvaliacaoModelo");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_NUM_PESQ = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP.ToString();
            strP_NUM_PESQ = ddlNumPesquisa.SelectedValue;

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelAvaliacaoModelo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_NUM_PESQ);

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
        private void CarregaDropDown()
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
        /// Método que carrega o dropdown de Número de Pesquisa
        /// </summary>
        private void CarregaNumPesquisa()
        {
             var avaMas = (from tb201 in TB201_AVAL_MASTER.RetornaTodosRegistros()
                           where tb201.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                           select new
                           {
                               tb201.NU_AVAL_MASTER, tb201.DT_CADASTRO, tb201.CO_TIPO_AVAL
                           }).ToList();

             ddlNumPesquisa.DataSource = (from np in avaMas
                                          select new
                                          {
                                              np.NU_AVAL_MASTER,
                                              numero = np.DT_CADASTRO.Year.ToString() + "." + np.DT_CADASTRO.Month.ToString("00") + "." +
                                              np.CO_TIPO_AVAL.ToString("000") + "." + np.NU_AVAL_MASTER.ToString("0000")
                                          }).OrderBy( p => p.NU_AVAL_MASTER );

            ddlNumPesquisa.DataTextField = "numero";
            ddlNumPesquisa.DataValueField = "NU_AVAL_MASTER";
            ddlNumPesquisa.DataBind();            
        }

        /// <summary>
        /// Método que carrega os campos de acordo com o número de pesquisa
        /// </summary>
        /// <param name="numeroPesquisa">Número da pesquisa</param>
        private void CarregaCampos(int numeroPesquisa)
        {
            if (ddlNumPesquisa.Items.Count > 0)
            {
                TB201_AVAL_MASTER tb201 = TB201_AVAL_MASTER.RetornaPelaChavePrimaria(numeroPesquisa);

                ddlTipoAvaliacao.DataSource = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                               where tb73.CO_TIPO_AVAL == tb201.CO_TIPO_AVAL
                                               select new { tb73.CO_TIPO_AVAL, tb73.NO_TIPO_AVAL }).OrderBy( p => p.NO_TIPO_AVAL );

                ddlTipoAvaliacao.DataTextField = "NO_TIPO_AVAL";
                ddlTipoAvaliacao.DataValueField = "CO_TIPO_AVAL";
                ddlTipoAvaliacao.DataBind();

                CarregaModalidades(tb201.CO_MODU_CUR != null ? tb201.CO_MODU_CUR.Value : 0);
                CarregaSerieCurso(tb201.CO_SERIE_CUR != null ? tb201.CO_SERIE_CUR.Value : 0);
                CarregaTurma(tb201.CO_TUR != null ? tb201.CO_TUR.Value : 0);
                CarregaMaterias(tb201.CO_MAT != null ? tb201.CO_MAT.Value : 0);

                ddlPublicoAlvo.SelectedValue = tb201.FLA_PUBLICO_ALVO;
                ddlIdent.SelectedValue = tb201.FLA_IDENT;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Turma
        /// </summary>
        /// <param name="turma">Id da turma</param>
        private void CarregaTurma(int turma)
        {
            if (turma > 0)
            {
                ddlTurma.DataSource = (from tb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                                       where tb129.CO_TUR == turma
                                       select new { tb129.NO_TURMA, tb129.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        /// <param name="modalidade">Id da modalidade</param>
        private void CarregaModalidades(int modalidade)
        {
            ddlModalidade.Items.Clear();

            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.CO_MODU_CUR == modalidade);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Série
        /// </summary>
        /// <param name="serie">Id da série</param>
        private void CarregaSerieCurso(int serie)
        {
            ddlSerieCurso.Items.Clear();

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                           where tb01.CO_CUR == serie
                                           select new { tb01.CO_CUR, tb01.NO_CUR });

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        /// <param name="materia">Id da matéria</param>
        private void CarregaMaterias(int materia)
        {
            ddlMateria.Items.Clear();

            ddlMateria.DataSource = from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                    join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                    where tb02.CO_MAT == materia
                                    select new { tb02.CO_MAT, tb107.NO_MATERIA };

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "CO_MAT";
            ddlMateria.DataBind();
        }
        #endregion        

        protected void ddlNumPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCampos(ddlNumPesquisa.Items.Count > 0 ? int.Parse(ddlNumPesquisa.SelectedValue) : 0);
        }
    }
}
