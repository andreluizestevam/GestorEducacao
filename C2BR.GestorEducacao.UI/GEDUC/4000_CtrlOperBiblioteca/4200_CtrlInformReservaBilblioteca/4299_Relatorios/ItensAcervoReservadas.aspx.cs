//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: RESERVA DE EMPRÉSTIMO DE ACERVO
// OBJETIVO: EMISSÃO DA RELAÇÃO DE RESERVAS DE ITEM DE ACERVO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 10/02/14 | Vinicius Reis              | Correção da validação do selectedvalue da ddlNome
//          |                            | ao mudar o tipo de usuário

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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4200_CtrlInformReservaBilblioteca.F4299_Relatorios
{
    public partial class ItensAcervoReservadas : System.Web.UI.Page
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
                CarregaDropDown();
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_AREACON, strP_CO_CLAS, strP_CO_ISBN_ACER, strP_CO_TP_USU, strP_CO_USU, strP_DT_INI, strP_DT_FIM;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelObrasReservadas");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_AREACON = null;
            strP_CO_CLAS = null;
            strP_CO_ISBN_ACER = null;
            strP_CO_TP_USU = null;
            strP_CO_USU = null;
            strP_DT_INI = null;
            strP_DT_FIM = null;


//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_AREACON = ddlAreaInteresse.SelectedValue;
            strP_CO_CLAS = ddlClassificacao.SelectedValue;
            strP_CO_ISBN_ACER = ddlObra.SelectedValue;
            strP_CO_TP_USU = ddlTipo.SelectedValue;
            strP_CO_USU = ddlNome.SelectedValue;
            strP_DT_INI = txtDataPeriodoIni.Text;
            strP_DT_FIM = txtDataPeriodoFim.Text;

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelObrasReservadas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_AREACON, strP_CO_CLAS, strP_CO_ISBN_ACER, strP_CO_TP_USU, strP_CO_USU, strP_DT_INI, strP_DT_FIM, LoginAuxili.ORG_NUMERO_CNPJ.ToString());

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        }              
        #endregion   

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares e Áreas de Interesse
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

            ddlAreaInteresse.DataSource = TB31_AREA_CONHEC.RetornaTodosRegistros();

            ddlAreaInteresse.DataTextField = "NO_AREACON";
            ddlAreaInteresse.DataValueField = "CO_AREACON";
            ddlAreaInteresse.DataBind();

            ddlAreaInteresse.Items.Insert(0, new ListItem("Todos", "T"));

            CarregaClassificacao();
            CarregaUsuarios();
        }

        /// <summary>
        /// Método que carrega o dropdown de Usuários
        /// </summary>
        private void CarregaUsuarios()
        {
            int coEmp = (ddlNome.SelectedValue != "T" && ddlNome.SelectedValue != "") ? int.Parse(ddlNome.SelectedValue) : 0;

            if (ddlTipo.SelectedValue == "A")
            {
                ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                      where tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb205.TP_USU_BIB == "A"
                                      && tb205.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == coEmp
                                      select new { tb205.NO_USU_BIB, tb205.CO_USUARIO_BIBLIOT }).OrderBy( u => u.NO_USU_BIB );
            }
            else if (ddlTipo.SelectedValue == "P")
            {
                ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                      where tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb205.TP_USU_BIB == "P"
                                      && tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp
                                      select new { tb205.NO_USU_BIB, tb205.CO_USUARIO_BIBLIOT }).OrderBy( u => u.NO_USU_BIB );
            }
            else if (ddlTipo.SelectedValue == "F")
            {
                ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                      where tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb205.TP_USU_BIB == "F"
                                      && tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp
                                      select new { tb205.NO_USU_BIB, tb205.CO_USUARIO_BIBLIOT }).OrderBy(u => u.NO_USU_BIB);
            }
            else if (ddlTipo.SelectedValue == "O")
            {
                ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                      where tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb205.TP_USU_BIB == "O"
                                      select new { tb205.NO_USU_BIB, tb205.CO_USUARIO_BIBLIOT }).OrderBy( u => u.NO_USU_BIB );
            }
            else
            {
                ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                      where tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                      select new { tb205.NO_USU_BIB, tb205.CO_USUARIO_BIBLIOT }).OrderBy( u => u.NO_USU_BIB );
            }

            ddlNome.DataTextField = "NO_USU_BIB";
            ddlNome.DataValueField = "CO_USUARIO_BIBLIOT";
            ddlNome.DataBind();

            ddlNome.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Classificações de Acervo
        /// </summary>
        void CarregaClassificacao()
        {
            int coAreaCon = ddlAreaInteresse.SelectedValue != "T" ? int.Parse(ddlAreaInteresse.SelectedValue) : 0;

            ddlClassificacao.Items.Clear();

            if (coAreaCon != 0)
            {
                ddlClassificacao.DataSource = TB32_CLASSIF_ACER.RetornaPelaAreaConhecimento(coAreaCon);

                ddlClassificacao.DataTextField = "NO_CLAS_ACER";
                ddlClassificacao.DataValueField = "CO_CLAS_ACER";
                ddlClassificacao.DataBind();
            }

            ddlClassificacao.Items.Insert(0, new ListItem("Todos", "T"));

            CarregaObras();
        }

        /// <summary>
        /// Método que carrega o dropdown de Obras de Acervo
        /// </summary>
        void CarregaObras()
        {
            int coAreaCon = ddlAreaInteresse.SelectedValue != "T" ? int.Parse(ddlAreaInteresse.SelectedValue) : 0;
            int coClasAcer = ddlClassificacao.SelectedValue != "T" ? int.Parse(ddlClassificacao.SelectedValue) : 0;

            ddlObra.Items.Clear();

            ddlObra.DataSource = (from tb35 in TB35_ACERVO.RetornaTodosRegistros()
                                  where tb35.TB31_AREA_CONHEC.CO_AREACON == coAreaCon && tb35.TB32_CLASSIF_ACER.CO_CLAS_ACER == coClasAcer
                                  && tb35.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                  select new { tb35.NO_ACERVO, tb35.CO_ISBN_ACER }).OrderBy( a => a.NO_ACERVO );

            ddlObra.DataTextField = "NO_ACERVO";
            ddlObra.DataValueField = "CO_ISBN_ACER";
            ddlObra.DataBind();

            ddlObra.Items.Insert(0, new ListItem("Todos", "T"));
        }
        #endregion

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuarios();
        }

        protected void ddlAreaInteresse_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaClassificacao();
        }

        protected void ddlClassificacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaObras();
        }

        protected void ddlNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuarios();
        }                
    }
}
