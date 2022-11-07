//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Web.Security;
using Resources;
using System.Configuration;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI
{
    public partial class Default : System.Web.UI.Page
    {
        //Public values here can be late-bound to javascript in the ASPX page.
        public int iWarningTimeoutInMilliseconds;
        public int iSessionTimeoutInMilliseconds;
        public string sTargetURLForSessionTimeout;
        public string situacaoValidacao;
        public string situacaoUsuario;
        public string situacaoDataValidade;
        public string enderecoModulo;
        Dictionary<string, string> chavesReg = AuxiliBaseApoio.chave(controleChaveInstituicao.ResourceManager);
        Dictionary<string, string> statusUsuario = AuxiliBaseApoio.chave(controleAvisoUsuario.ResourceManager);
        protected void page_Init()
        {
            ///If que verifica se foi parametrizado no web.config para mostrar aviso de chave de registro
            if (ConfigurationSettings.AppSettings.AllKeys.Where(f => f == "SistemaEmManutencao").Count() > 0)
            {
                Boolean manutencao = false;
                if (Boolean.TryParse(ConfigurationSettings.AppSettings["SistemaEmManutencao"].ToString(), out manutencao)
                    && manutencao)
                    AuxiliPagina.RedirecionaParaPaginaManutencao();
            }

            

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Apresentação de aviso de chave, atualização ou bem vindo.
            var usuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);
            
            
            #region Verifica se necessário avisar o usuário
            situacaoUsuario = "";
            if (usuario != null)
            {
                if (usuario.flaPrimeiroAcesso != null && usuario.flaPrimeiroAcesso != statusUsuario[controleAvisoUsuario.N])
                {
                    string sitUsuario = usuario.flaPrimeiroAcesso.ToString().Trim().ToUpper();
                    situacaoUsuario = sitUsuario;
                    if (sitUsuario == statusUsuario[controleAvisoUsuario.S] || sitUsuario == statusUsuario[controleAvisoUsuario.V])
                    {
                        usuario.flaPrimeiroAcesso = statusUsuario[controleAvisoUsuario.N];
                        ADMUSUARIO.SaveOrUpdate(usuario, true);
                    }
                }
            }
            #endregion
            #region Verifica se necessário aviso do sistema
            var inst = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            situacaoValidacao = "";
            if (inst != null)
            {
                if (inst.FL_SITUA_VALIDACAO != null && inst.FL_SITUA_VALIDACAO != "")
                {
                    string situacao = inst.FL_SITUA_VALIDACAO.ToString().Trim().ToUpper();
                    #region Verifica se necessário trancar sistema
                    ///If que verifica se foi especificado no web.config para mostrar a tela de manutenção para o cliente
                    if (ConfigurationSettings.AppSettings.AllKeys.Where(f => f == "DataValidadeIrregular").Count() > 0)
                    {
                        string dataValidacao = ConfigurationSettings.AppSettings["DataValidadeIrregular"].ToString();
                        DateTime dataValida;
                        if (dataValidacao != "" && DateTime.TryParse(dataValidacao, out dataValida))
                        {
                            if (dataValida.Date > DateTime.Now.Date)
                                situacao = chavesReg[controleChaveInstituicao.A];
                            else if (dataValida.Date <= DateTime.Now.Date)
                                situacao = chavesReg[controleChaveInstituicao.I];
                        }
                    }
                    #endregion
                    if (Session["mostrarValidacao"] == null || (Session["mostrarValidacao"] != "" && Session["mostrarValidacao"] != "false"))
                    {
                        situacaoValidacao = situacao;
                        if (situacao == chavesReg[controleChaveInstituicao.A])
                            Session["mostrarValidacao"] = "false";
                        if (situacao == chavesReg[controleChaveInstituicao.I])
                            Session["mostrarValidacao"] = "true";
                        if (situacao == chavesReg[controleChaveInstituicao.V] || situacao == chavesReg[controleChaveInstituicao.R])
                        {
                            if (situacao == chavesReg[controleChaveInstituicao.R])
                            {
                                inst.FL_SITUA_VALIDACAO = chavesReg[controleChaveInstituicao.N];
                                Session["mostrarValidacao"] = "false";
                            }
                            else
                            {
                                inst.FL_SITUA_VALIDACAO = chavesReg[controleChaveInstituicao.A];
                                Session["mostrarValidacao"] = "";
                            }
                            TB000_INSTITUICAO.SaveOrUpdate(inst, true);

                        }

                    }
                }

            }
            #endregion

            #region Verifica usuário online
            if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "O")
            {
                var modulo = (from mo in ADMMODULO.RetornaTodosRegistros()
                              where mo.nomURLModulo.Contains("GEDUC/2000_CtrlOperSecretariaEscolar/2100_CtrlServSecretariaEscolar/2113_PreMatriculaAluno/cadastro.aspx")
                              select mo).FirstOrDefault();
                enderecoModulo = string.Format("{0}?moduloId={1}&moduloNome={2}&moduloId={1}", modulo.nomURLModulo, modulo.ideAdmModulo, HttpContext.Current.Server.UrlEncode(modulo.nomModulo));
                HttpContext.Current.Session[Resources.SessoesHttp.IdModuloCorrente] = modulo.ideAdmModulo;
                ifrmData.Attributes["src"] = enderecoModulo;
            }
            #endregion

            #endregion

            //--------> In a real app, stuff these values into web.config.
            sTargetURLForSessionTimeout = "LogOut.aspx";
            int iNumberOfMinutesBeforeSessionTimeoutToWarnUser = 1;

            //--------> Get the sessionState timeout (from web.config).  If not set there explicitly, the default is 20 minutes.
            int iSessionTimeoutInMinutes = Session.Timeout;

            //--------> Compute our timeout values, one for client-side warning, one for client-side session termination.
            int iWarningTimeoutInMinutes = iSessionTimeoutInMinutes - iNumberOfMinutesBeforeSessionTimeoutToWarnUser;
            iWarningTimeoutInMilliseconds = iWarningTimeoutInMinutes * 60 * 1000;

            iSessionTimeoutInMilliseconds = iSessionTimeoutInMinutes * 60 * 1000;

            if (!IsPostBack)
            {
//------------> Don't show the warning message div tag until later.  Setting the property here so we can see the div at design-time.
                divSessionTimeoutWarning.Style.Add("display", "none;");

//------------> Informações de Usuário Logado
                if (LoginAuxili.ID_IMG_USU_LOGADO > 0)
                    imagemUsuario.ImageUrl = String.Format("/LerImagem.ashx?idimg={0}", LoginAuxili.ID_IMG_USU_LOGADO);
                else
                    imagemUsuario.ImageUrl = "../../../../Library/IMG/Gestor_SemImagem.png";
                
                lblNomeUsuario.Text = LoginAuxili.NOME_USU_LOGADO;

                if (!LoginAuxili.TIPO_USU.Equals("R") && !LoginAuxili.TIPO_USU.Equals("A"))
                {
                    if (LoginAuxili.CO_MAT_COL != null)
                        lblMatriculaUsuario.Text = String.Format("Mat. {0}", LoginAuxili.CO_MAT_COL.Insert(5, "-").Insert(2, "."));
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);
                }
                else if (LoginAuxili.TIPO_USU.Equals("A"))
                {
                    var dadosUsu = "NIRE ";
                    var tb08 = TB08_MATRCUR.RetornaPeloAluno(LoginAuxili.CO_RESP);
                    tb08.TB07_ALUNOReference.Load();
                    tb08.TB44_MODULOReference.Load();

                    dadosUsu += tb08.TB07_ALUNO.NU_NIRE + " - ";
                    dadosUsu += tb08.CO_ANO_MES_MAT + " - ";
                    dadosUsu += tb08.TB44_MODULO.DE_MODU_CUR;// +" - ";
                    //dadosUsu += tb08.TB44_MODULO.TB01_CURSO.LastOrDefault().NO_CUR + " - ";
                    //dadosUsu += tb08.TB44_MODULO.TB129_CADTURMAS.LastOrDefault().NO_TURMA;

                    lblMatriculaUsuario.Text = dadosUsu;
                }

                lblUnidadeFuncUsuario.Text = String.Format("Unidade Funcional: {0}", LoginAuxili.NO_FANTAS_EMP.Length > 30 ? LoginAuxili.NO_FANTAS_EMP.Substring(0, 30) : LoginAuxili.NO_FANTAS_EMP);
                lblUnidadeFuncUsuario.Text = String.Format("Unidade Funcional: {0}", Extensoes.Capitalize(LoginAuxili.NO_FANTAS_EMP));
//---------------------------------------

//------------> Informações da Unidade Escolar
                lblNomeOrgao.Text = LoginAuxili.ORG_NOME_ORGAO;
                lblUnidadeSelecionada.Text = LoginAuxili.NO_FANTAS_EMP_ALTERADA;                            
                lblCidadeUFUnidade.Text = LoginAuxili.NO_CIDADE_EMP + " - " + LoginAuxili.CO_UF_EMP;
                if (LoginAuxili.TELEFONE_EMP != null)
                    lblTelefoneUnidade.Text = LoginAuxili.TELEFONE_EMP.Insert(0, "(").Insert(3, ") ").Insert(9, "-");
                //else
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);
                if (LoginAuxili.ID_IMG_EMPRESA_LOGADA > 0)
                    imgOrgao.ImageUrl = String.Format("/LerImagem.ashx?idimg={0}", LoginAuxili.ID_IMG_EMPRESA_LOGADA);
                else
                    imgOrgao.ImageUrl = "../../../../Library/IMG/Gestor_SemImagem.png";
//---------------------------------------

//------------> Informações do Rodapé
                lblQtdeAcessoUsuario.Text = LoginAuxili.QTD_ACESSO_USU.ToString();                
                lblInforUltimoAcesso.Text = "Dia " + LoginAuxili.DATA_ULTIMO_ACESSO_USU.ToString("dd/MM/yyyy") + " - " + 
                                            LoginAuxili.DATA_ULTIMO_ACESSO_USU.ToString("hh") + "h " + LoginAuxili.DATA_ULTIMO_ACESSO_USU.ToString("mm") + "m";
                lblIpAcesso.Text = LoginAuxili.IP_ULTIMO_ACESSO_USU;
//---------------------------------------
            }
        }

        protected void btnContinueWorking_Click(object sender, EventArgs e)
        {
            //Do nothing.  But the Session will be refreshed as a result of this method being called, which is its purpose.
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/logout.aspx");
        }
    }
}
