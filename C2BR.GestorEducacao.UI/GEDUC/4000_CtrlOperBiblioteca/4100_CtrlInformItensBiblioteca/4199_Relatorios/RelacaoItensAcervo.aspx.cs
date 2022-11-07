//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: DADOS CADASTRAIS DE ACERVO BIBLIOGRÁFICO
// OBJETIVO: LISTAGEM DOS ITENS DE ACERVO BIBLIOGRÁFICOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F1999_Relatorios
{
    public partial class RelacaoItensAcervo : System.Web.UI.Page
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
            string strP_CO_EMP, strP_CO_AREACON, strP_DE_AREACON, strP_CO_EDITORA, strP_DE_EDITORA, strP_CO_CLAS_ACER, strP_DE_CLAS_ACER, strP_CO_AUTOR, strP_DE_AUTOR;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelItensAcervo");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_AREACON = null;
            strP_DE_AREACON = null;
            strP_CO_EDITORA = null;
            strP_DE_EDITORA = null;
            strP_CO_CLAS_ACER = null;
            strP_DE_CLAS_ACER = null;
            strP_CO_AUTOR = null;
            strP_DE_AUTOR = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_AREACON = ddlAreaInteresse.SelectedValue;
            strP_DE_AREACON = ddlAreaInteresse.SelectedItem.ToString();
            strP_CO_EDITORA = ddlEditora.SelectedValue;
            strP_DE_EDITORA = ddlEditora.SelectedItem.ToString();
            strP_CO_CLAS_ACER = ddlClassificacao.SelectedValue;
            strP_DE_CLAS_ACER = ddlClassificacao.SelectedItem.ToString();
            strP_CO_AUTOR = ddlAutor.SelectedValue;
            strP_DE_AUTOR = ddlAutor.SelectedItem.ToString();           

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelItensAcervo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_AREACON, strP_DE_AREACON, strP_CO_EDITORA, strP_DE_EDITORA, strP_CO_CLAS_ACER, strP_DE_CLAS_ACER, strP_CO_AUTOR, strP_DE_AUTOR, LoginAuxili.ORG_NUMERO_CNPJ.ToString());            

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();
        }      
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares, Áreas de Interesse, Editora e Autor
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

            ddlAreaInteresse.DataSource = TB31_AREA_CONHEC.RetornaTodosRegistros();

            ddlAreaInteresse.DataTextField = "NO_AREACON";
            ddlAreaInteresse.DataValueField = "CO_AREACON";
            ddlAreaInteresse.DataBind();

            ddlAreaInteresse.Items.Insert(0, new ListItem("Todos", "T"));

            CarregaClassificacao();

            ddlEditora.DataSource = TB33_EDITORA.RetornaTodosRegistros();

            ddlEditora.DataTextField = "NO_EDITORA";
            ddlEditora.DataValueField = "CO_EDITORA";
            ddlEditora.DataBind();

            ddlEditora.Items.Insert(0, new ListItem("Todos", "T"));

            ddlAutor.DataSource = TB34_AUTOR.RetornaTodosRegistros();

            ddlAutor.DataTextField = "NO_AUTOR";
            ddlAutor.DataValueField = "CO_AUTOR";
            ddlAutor.DataBind();

            ddlAutor.Items.Insert(0, new ListItem("Todos", "T"));
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
        } 
        #endregion

        protected void ddlAreaInteresse_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaClassificacao();
        } 
    }
}
