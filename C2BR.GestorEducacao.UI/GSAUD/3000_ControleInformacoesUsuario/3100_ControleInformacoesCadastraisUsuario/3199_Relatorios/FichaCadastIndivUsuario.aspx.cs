//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE PAIS OU RESPONSÁVEIS
// OBJETIVO: INFORMAÇÕES GERAIS DE PAIS/RESPONSÁVEL DE ALUNO
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
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario;


namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3199_Relatorios
{
    public partial class FichaCadastIndivUsuario : System.Web.UI.Page
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
                CarregaResponsavel();
            }
        }

//====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno, intP_CO_ALU;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_RESP, parametros, infos;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelFicCadIndRes");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_RESP = null;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_RESP = ddlResponsaveis.SelectedValue;
            intP_CO_ALU = ddlUsuario.SelectedValue != "" ? int.Parse(ddlUsuario.SelectedValue) : 0;
            parametros = "";
            infos = "";

            if (intP_CO_ALU == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Usuário não informado.");
                return;
            }

            RptRelInfGeraisUsuario rpt = new RptRelInfGeraisUsuario();
            lRetorno = rpt.InitReport(parametros, int.Parse(strP_CO_EMP), infos, strP_CO_EMP, intP_CO_ALU);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            //lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            //lRetorno = lIRelatorioWeb.RelFicCadIndRes(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_RESP, LoginAuxili.ORG_NUMERO_CNPJ.ToString());

            //string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            //Session["URLRelatorio"] = strURL;
            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            //varRelatorioWeb.Close();
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

            ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega os responsáveis na combo
        /// </summary>
        private void CarregaResponsavel()
        {
            ddlResponsaveis.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                          select new ComboResponsavel
                                          { 
                                              coResp = tb108.CO_RESP, 
                                              noResp = tb108.NO_RESP,
                                              cpfResp = tb108.NU_CPF_RESP
                                          });

            ddlResponsaveis.DataTextField = "nome";
            ddlResponsaveis.DataValueField = "coResp";
            ddlResponsaveis.DataBind();

            ddlResponsaveis.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega os usuários na combo
        /// </summary>
        private void CarregaUsuario()
        {
            int coResp = ddlResponsaveis.SelectedValue != "" ? int.Parse(ddlResponsaveis.SelectedValue) : 0;

            if (coResp != 0)
            {
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.TB108_RESPONSAVEL.CO_RESP == coResp
                           select new ComboUsuario
                           {
                               noAlu = tb07.NO_ALU,
                               nuNirs = tb07.NU_NIRE,
                               coAlu = tb07.CO_ALU
                           });

                ddlUsuario.DataTextField = "nome";
                ddlUsuario.DataValueField = "coAlu";

                ddlUsuario.DataSource = res;
                ddlUsuario.DataBind();

                ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlUsuario.Items.Clear();
            }
        }

        // Classe que formata a saída dos responsáveis na combo
        public class ComboResponsavel
        {
            public string cpfResp { get; set; }
            public string noResp { get; set; }
            public int coResp { get; set; }
            public string nome
            {
                get
                {
                    if (this.cpfResp != "")
                    {
                        return Convert.ToUInt64(this.cpfResp).ToString(@"000\.000\.000\-00") + " - " + this.noResp;
                    }
                    else
                    {
                        return "***.***.***-**" + " - " + this.noResp;
                    }
                }
            }
        }

        // Classe que formata a saída das informações na combo de usuário
        public class ComboUsuario
        {
            public string noAlu { get; set; }
            public int nuNirs { get; set; }
            public int coAlu { get; set; }
            public string nome
            {
                get
                {
                    return this.nuNirs.ToString().PadLeft(11, '0') + " - " + this.noAlu;
                }
            }
        }
        #endregion

        protected void ddlResponsaveis_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuario();
        }
    }
}