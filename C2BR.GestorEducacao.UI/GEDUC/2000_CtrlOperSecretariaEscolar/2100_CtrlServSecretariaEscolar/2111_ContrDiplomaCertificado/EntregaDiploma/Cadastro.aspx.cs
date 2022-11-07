//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE DIPLOMAS E CERTIFICADOS
// OBJETIVO: ENTREGA DE DIPLOMAS
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
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2111_ContrDiplomaCertificado.EntregaDiploma
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis

        public string CodEnt
        {
            get
            {
                if (Session["codEnt"].ToString() == "")
                    return "";
                else
                    return Session["codEnt"].ToString();
            }
            set { Session["codEnt"] = value; }
        }

        #endregion

        #region Eventos
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

//--------> Valida se o evento é de exibição do relatório gerado.
            if (Request.QueryString["ApresentaRelatorio"] == "1")
            {
//------------> Limpa a var de sessão com o url do relatório.
                Session.Remove("URLRelatorio");
//------------> Limpa a ref da url utilizada para carregar o relatório.
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                this.Request.QueryString.Remove("ApresentaRelatorio");
                isreadonly.SetValue(this.Request.QueryString, true, null);
                AuxiliPagina.RedirecionaParaPaginaBusca();
            }

            CarregaModalidades();
            CarregaSerieCurso();
            CarregaColaborador();

            CarregaAluno(ddlAuloRecebimento);
            txtCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (ddlAuloRecebimento.SelectedValue == "" && ddlRespRec.SelectedValue == "" && txtNomeOutro.Text == String.Empty)
                return;

            int idSolicDiploma = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            TB214_ENTR_DOCUMENTO tb214 = new TB214_ENTR_DOCUMENTO();

            TB211_SOLIC_DIPLOMA tb211 = TB211_SOLIC_DIPLOMA.RetornaPelaChavePrimaria(idSolicDiploma);

            tb211.CO_STATUS = "E";

            TB211_SOLIC_DIPLOMA.SaveOrUpdate(tb211, true);

            tb214.TB211_SOLIC_DIPLOMA = TB211_SOLIC_DIPLOMA.RetornaPelaChavePrimaria(idSolicDiploma);
            tb214.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
            tb214.TP_USU = ddlTipoUsuRece.SelectedValue;

            if (ddlTipoUsuRece.SelectedValue == "A")
            {
                if (ddlAuloRecebimento.SelectedValue == "")
                    return;
                tb214.CO_USU = int.Parse(ddlAuloRecebimento.SelectedValue);
            }
            else if (ddlTipoUsuRece.SelectedValue == "R")
            {
                if (ddlRespRec.SelectedValue == "")
                    return;

                tb214.CO_USU = int.Parse(ddlRespRec.SelectedValue);
            }
            else
            {
                tb214.NO_RESP_ENTR_DOCUM = txtNomeOutro.Text;
                tb214.NU_TELE_RESP_ENTR_DOCUM = txtTelefone.Text;
                tb214.CO_RG_RESP_ENTR_DOCUM = txtRGOutro.Text;
                tb214.CO_USU = 0;
            }

            DateTime dataAtual = DateTime.Now.Date;
            tb214.DT_ENTREGA = dataAtual;

            TB214_ENTR_DOCUMENTO.SaveOrUpdate(tb214, true);
            
//--------> Gera o relatório
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_ID_ENTR_DOCUM;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelReciboEntregDiploma");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP.ToString();

            strP_ID_ENTR_DOCUM = (from lTb214 in TB214_ENTR_DOCUMENTO.RetornaTodosRegistros()
                                  where lTb214.DT_ENTREGA == dataAtual && tb214.TB211_SOLIC_DIPLOMA.ID_SOLIC_DIPLOMA == idSolicDiploma
                                  select new { lTb214.ID_ENTR_DOCUM }).FirstOrDefault().ID_ENTR_DOCUM.ToString();
            
            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelReciboEntregDiploma(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ID_ENTR_DOCUM);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            AuxiliPagina.EnvioMensagemSucesso(this, "Entrega Efetuada com Sucesso");
            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());

            varRelatorioWeb.Close();
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método que faz referência a outro método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            PreencheControles(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        /// <param name="idSolicDiploma">Id da solicitação do diploma</param>
        private void PreencheControles(int idSolicDiploma)
        {
            TB211_SOLIC_DIPLOMA tb211 = TB211_SOLIC_DIPLOMA.RetornaPelaChavePrimaria(idSolicDiploma);

            if (tb211 != null)
            {
                ddlModalidade.SelectedValue = tb211.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb211.CO_CUR.ToString();
                CarregaAlunoSerie(ddlAluno);
                tb211.TB07_ALUNOReference.Load();
                ddlAluno.SelectedValue = tb211.TB07_ALUNO.CO_ALU.ToString();
                tb211.TB03_COLABORReference.Load();
                ddlColaborador.SelectedValue = tb211.TB03_COLABOR.CO_COL.ToString();
                ddlTipoUsu.SelectedValue = tb211.TP_USU_SOLIC;
                CarregaNire();

                if (ddlTipoUsu.SelectedValue == "R")
                {
                    liResponsavel.Visible = true;
                    liAlunoSol.Visible = liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = false;
                    CarregaResponsavel(ddlResponsavel);
                    ddlResponsavel.SelectedValue = tb211.CO_USU.ToString();
                }
                else if (ddlTipoUsu.SelectedValue == "A")
                {
                    liResponsavel.Visible = liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = false;
                    liAlunoSol.Visible = true;
                    CarregaAluno(ddlAlunoSolicitante);
                    ddlAlunoSolicitante.SelectedValue = tb211.CO_USU.ToString();
                }
                else
                {
                    liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = true;
                    liResponsavel.Visible = liAlunoSol.Visible = false;
                    txtNomeS.Text = tb211.NO_RESP_SOLIC_DIPLOMA;
                    txtRGS.Text = tb211.CO_RG_RESP_SOLIC_DIPLOMA.ToString();
                    txtTelefoneS.Text = tb211.NU_TELE_RESP_SOLIC_DIPLOMA.ToString();
                }

                txtCadastro.Text = tb211.DT_CADASTRO.ToString("dd/MM/yyyy");

                ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlAluno.Enabled = ddlColaborador.Enabled = ddlTipoUsu.Enabled = false;
            }            
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        /// <param name="ddl">DropDown de aluno</param>
        private void CarregaAluno(DropDownList ddl)
        {
            ddl.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                              where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                              select new { NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(), tb08.CO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

            ddl.DataTextField = "NO_ALU";
            ddl.DataValueField = "CO_ALU";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        /// <param name="ddl">DropDown aluno</param>
        private void CarregaAlunoSerie(DropDownList ddl)
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0)
            {
                ddl.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_CUR == serie && tb08.CO_SIT_MAT == "F"
                                  select new { NO_ALU = tb08.TB07_ALUNO.NO_ALU.ToUpper(), tb08.CO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddl.DataTextField = "NO_ALU";
                ddl.DataValueField = "CO_ALU";
                ddl.DataBind();

                ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Responsaveis
        /// </summary>
        /// <param name="ddl">DropDown de responsável</param>
        private void CarregaResponsavel(DropDownList ddl)
        {
            ddl.DataSource = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                              join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                              where tb07.TB25_EMPRESA1.CO_EMP == LoginAuxili.CO_EMP
                              select new { NO_RESP = tb108.NO_RESP.ToUpper(), tb108.CO_RESP }).Distinct().OrderBy(r => r.NO_RESP);

            ddl.DataTextField = "NO_RESP";
            ddl.DataValueField = "CO_RESP";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaColaborador()
        {
            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                         select new { NO_COL = tb03.NO_COL.ToUpper(), tb03.CO_COL }).OrderBy(c => c.NO_COL);

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.SelectedValue = LoginAuxili.CO_COL.ToString();
        }

        /// <summary>
        /// Método que carrega o NIRE do Aluno selecionado
        /// </summary>
        private void CarregaNire()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            if (coAlu == 0)
                return;

            txtNire.Text = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                            where tb07.CO_ALU == coAlu
                            select new { tb07.NU_NIRE }).FirstOrDefault().NU_NIRE.ToString();
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunoSerie(ddlAluno);
        }

        protected void ddlTipoUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoUsu.SelectedValue == "R")
            {
                liResponsavel.Visible = true;
                liAlunoSol.Visible = liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = false;
                CarregaResponsavel(ddlResponsavel);
            }
            else if (ddlTipoUsu.SelectedValue == "A")
            {
                liResponsavel.Visible = liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = false;
                liAlunoSol.Visible = true;
                CarregaAluno(ddlAlunoSolicitante);
            }
            else
            {
                liOutroNomeSol.Visible = liOutroRGSol.Visible = liOutroFoneSol.Visible = true;
                liResponsavel.Visible = liAlunoSol.Visible = false;
            }            
        }

        protected void ddlTipoUsuRec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoUsuRece.SelectedValue == "R")
            {
                liResSol.Visible = true;
                liAlunoRe.Visible = liOutrosNome.Visible = liOutrosRG.Visible =  liOutrosTelefone.Visible = false;
                CarregaResponsavel(ddlRespRec);
            }
            else if (ddlTipoUsuRece.SelectedValue == "A")
            {
                liResSol.Visible = liOutrosNome.Visible = liOutrosRG.Visible = liOutrosTelefone.Visible = false;
                liAlunoRe.Visible = true;
                CarregaAluno(ddlAuloRecebimento);
            }
            else
            {
                liOutrosNome.Visible = liOutrosRG.Visible = liOutrosTelefone.Visible = true;
                liResSol.Visible = liAlunoRe.Visible = false;
            }
        }
        
        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaNire();                  
        }
    }
}