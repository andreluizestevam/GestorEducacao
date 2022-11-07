//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: MANUTENÇÃO DE USUÁRIOS DO SISTEMA.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//10/11/2021|FABRICIO SOARES DOS SANTOS  | CORREÇÃO DO CO_EMP SENDO RECEBIDO PELA PÁGINA E CORREÇÃO NO CarregaColaborador()  

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Data.Sql;



//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0304_ResetarSenhaUsuario
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

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
            if (!IsPostBack)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    AuxiliPagina.RedirecionaParaPaginaMensagem("Funcionalidade apenas de alteração, deve ser selecionado um usuário.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

                ///Define altura e largura da imagem do funcionário
                upImagemColab.ImagemLargura = 90;
                upImagemColab.ImagemAltura = 122;

                //upImagemColab.Visible = false;
                upImagemColab.ImagemLargura = 86;
                upImagemColab.ImagemAltura = 105;
                upImagemColab.MostraProcurar = false;
                txtInstituicao.Text = LoginAuxili.ORG_NOME_ORGAO;
                CarregaUnidades();
                CarregaColaborador();
                txtDtSituacaoMUS.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                //string url = HttpContext.Current.Request.Url.AbsoluteUri;
                //string url = Request.Url.GetLeftPart(UriPartial.Authority);
                string strppath = HttpContext.Current.Request.Url.PathAndQuery;
                string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strppath, "/");
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            string tipo = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.CoMat);
            if (tipo.Equals("Responsável"))
            {
                //--------> Retorna as informaçoes do usuário informado
                ADMUSUARIO admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
                string cpfCol = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.CO_RESP == admUsuario.CodUsuario).FirstOrDefault().NU_CPF_RESP;
                string strSenhaMD5 = "";

                strSenhaMD5 = LoginAuxili.GerarMD5(cpfCol);
                admUsuario.desSenha = strSenhaMD5;

                CurrentPadraoCadastros.CurrentEntity = admUsuario;
            }
            else
            {
                //--------> Retorna as informaçoes do usuário informado
                ADMUSUARIO admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
                string cpfCol = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == admUsuario.CodUsuario).FirstOrDefault().NU_CPF_COL;
                string strSenhaMD5 = "";

                strSenhaMD5 = LoginAuxili.GerarMD5(cpfCol);
                admUsuario.desSenha = strSenhaMD5;

                CurrentPadraoCadastros.CurrentEntity = admUsuario;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            string tipo = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.CoMat);
            if (tipo.Equals("Responsável"))
            {
                //--------> Carrega informaçoes do usuário (funcionário) e lança nos respectivos campos do formulário
                TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol));
                tb108.ImageReference.Load();
                if (tb108 != null)
                {
                    //if (tb03.Image != null)
                    //{
                    //    imgColMUS.Visible = true;
                    //    imgColMUS.ImageUrl = "~/LerImagem.ashx?idimg=" + tb03.Image.ImageId;
                    //}


                    if (tb108.Image != null)
                        upImagemColab.CarregaImagem(tb108.Image.ImageId);
                    else
                        upImagemColab.CarregaImagem(0);




                    CarregaUnidades();
                    ddlUnidadeMUS.SelectedValue = LoginAuxili.CO_EMP.ToString();
                    ddlColMUS.SelectedValue = tb108.CO_RESP.ToString();
                    txtApelidoMUS.Text = tb108.NO_APELIDO_RESP;
                    txtEmailMUS.Text = tb108.DES_EMAIL_RESP != null ? tb108.DES_EMAIL_RESP : "";
                    txtCelularMUS.Text = tb108.NU_TELE_CELU_RESP != null ? tb108.NU_TELE_CELU_RESP : "";
                    txtTelefoneMUS.Text = tb108.NU_TELE_RESI_RESP != null ? tb108.NU_TELE_RESI_RESP : "";

                    txtDepartamentoMUS.Text = "Responsável Financeiro";

                    txtFuncaoMUS.Text = "Responsável";

                    //------------> Carrega informaçoes do usuário e lança nos respectivos campos do formulário
                    var admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

                    if (admUsuario != null)
                    {
                        txtLoginMUS.Text = admUsuario.desLogin;
                        ddlTpUsuMUS.SelectedValue = admUsuario.TipoUsuario;
                        ddlUsuaCaixaMUS.SelectedValue = admUsuario.FLA_MANUT_CAIXA == null ? "N" : admUsuario.FLA_MANUT_CAIXA;
                        ddlTpUsuMUS.Enabled = false;
                        ddlClaUsuMUS.SelectedValue = admUsuario.ClassifUsuario;
                        txtQtdSMSMes.Text = admUsuario.QT_SMS_MAXIM_USR.ToString();
                    }
                }
            }
            else
            {
                //--------> Carrega informaçoes do usuário (funcionário) e lança nos respectivos campos do formulário
                var tb03 = RetornaEntidade();
                tb03.ImageReference.Load();
                if (tb03 != null)
                {
                    //if (tb03.Image != null)
                    //{
                    //    imgColMUS.Visible = true;
                    //    imgColMUS.ImageUrl = "~/LerImagem.ashx?idimg=" + tb03.Image.ImageId;
                    //}


                    if (tb03.Image != null)
                        upImagemColab.CarregaImagem(tb03.Image.ImageId);
                    else
                        upImagemColab.CarregaImagem(0);




                    CarregaUnidades();
                    ddlUnidadeMUS.SelectedValue = tb03.CO_EMP.ToString();
                    ddlColMUS.SelectedValue = tb03.CO_COL.ToString();
                    txtApelidoMUS.Text = tb03.NO_APEL_COL;
                    txtEmailMUS.Text = tb03.CO_EMAIL_FUNC_COL != null ? tb03.CO_EMAIL_FUNC_COL : "";
                    txtCelularMUS.Text = tb03.NU_TELE_CELU_COL != null ? tb03.NU_TELE_CELU_COL : "";
                    txtTelefoneMUS.Text = tb03.NU_TELE_RESI_COL != null ? tb03.NU_TELE_RESI_COL : "";

                    var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                               where tb14.CO_DEPTO == tb03.CO_DEPTO
                               select new { tb14.NO_DEPTO }).FirstOrDefault();

                    if (res != null)
                    {
                        txtDepartamentoMUS.Text = res.NO_DEPTO;
                    }

                    txtFuncaoMUS.Text = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                                         where tb15.CO_FUN == tb03.CO_FUN
                                         select new { tb15.NO_FUN }).FirstOrDefault().NO_FUN;

                    //------------> Carrega informaçoes do usuário e lança nos respectivos campos do formulário
                    var admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));

                    if (admUsuario != null)
                    {
                        txtLoginMUS.Text = admUsuario.desLogin;
                        ddlTpUsuMUS.SelectedValue = admUsuario.TipoUsuario;
                        ddlUsuaCaixaMUS.SelectedValue = admUsuario.FLA_MANUT_CAIXA == null ? "N" : admUsuario.FLA_MANUT_CAIXA;
                        ddlTpUsuMUS.Enabled = false;
                        ddlClaUsuMUS.SelectedValue = admUsuario.ClassifUsuario;
                        txtQtdSMSMes.Text = admUsuario.QT_SMS_MAXIM_USR.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="coCol">Id do funcionário</param>
        private TB03_COLABOR RetornaEntidade()
        {
            return TB03_COLABOR.RetornaPeloCoCol(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que preenche o DropDown de funcionário de acordo com os parâmetros necessários
        /// </summary>
        private void CarregaColaborador()
        {
            string tipo = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.CoMat);
            if (tipo.Equals("Responsável"))
            {
                int? coCol = Convert.ToInt32(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol));

                int coEmp = ddlUnidadeMUS.SelectedValue != "" ? int.Parse(ddlUnidadeMUS.SelectedValue) : 0;

                if (coCol == null || coCol == 0)
                {
                    string strFlaProfessor = ddlTpUsuMUS.SelectedValue == "P" ? "S" : "N";

                    var resultado = from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                    join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb108.CO_RESP
                                    where admUsuario.CO_EMP == coEmp
                                    select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP };

                    ddlColMUS.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                            where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP.ToUpper() }).Except(resultado).OrderBy(c => c.NO_COL).ToList();

                    ddlColMUS.DataTextField = "NO_COL";
                    ddlColMUS.DataValueField = "CO_COL";
                    ddlColMUS.DataBind();

                    ddlColMUS.Items.Insert(0, new ListItem("Selecione", ""));
                }
                else
                {
                    ddlColMUS.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                            where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { CO_COL = tb108.CO_RESP, NO_COL = tb108.NO_RESP.ToUpper() }).OrderBy(c => c.NO_COL).ToList();

                    ddlColMUS.DataTextField = "NO_COL";
                    ddlColMUS.DataValueField = "CO_COL";
                    ddlColMUS.DataBind();

                    ddlColMUS.SelectedValue = coCol.ToString();
                }
            }
            else
            {
                int? coCol = Convert.ToInt32(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol));

                int coEmp = ddlUnidadeMUS.SelectedValue != "" ? int.Parse(ddlUnidadeMUS.SelectedValue) : 0;

                if (coCol == null || coCol == 0)
                {
                    string strFlaProfessor = ddlTpUsuMUS.SelectedValue == "P" ? "S" : "N";

                    var resultado = from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                    join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                    where admUsuario.CO_EMP == coEmp
                                    select new { tb03.CO_COL, tb03.NO_COL };

                    ddlColMUS.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                            where tb03.CO_EMP == coEmp && tb03.FLA_PROFESSOR == strFlaProfessor
                                            select new { tb03.CO_COL, NO_COL = tb03.NO_COL.ToUpper() }).Except(resultado).OrderBy(c => c.NO_COL).ToList();

                    ddlColMUS.DataTextField = "NO_COL";
                    ddlColMUS.DataValueField = "CO_COL";
                    ddlColMUS.DataBind();

                    ddlColMUS.Items.Insert(0, new ListItem("Selecione", ""));
                }
                else
                {
                    ddlColMUS.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                            where tb03.CO_EMP == coEmp
                                            select new { tb03.CO_COL, NO_COL = tb03.NO_COL.ToUpper() }).OrderBy(c => c.NO_COL).ToList();

                    ddlColMUS.DataTextField = "NO_COL";
                    ddlColMUS.DataValueField = "CO_COL";
                    ddlColMUS.DataBind();

                    ddlColMUS.SelectedValue = coCol.ToString();
                }
            }
        }

        /// <summary>
        /// Método que carrega dropdown das Unidade Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidadeMUS.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                        join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                        where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                        select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidadeMUS.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeMUS.DataValueField = "CO_EMP";
            ddlUnidadeMUS.DataBind();

            int? coCol= Convert.ToInt32(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol));

            if (coCol == null || coCol == 0)
            {             
                ddlUnidadeMUS.SelectedValue = LoginAuxili.CO_EMP.ToString();
                CarregaColaborador();
            }
            else
            {
                var coEmp = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                              where tb03.CO_COL == coCol
                           select new { tb03.CO_EMP }).FirstOrDefault().CO_EMP;
                ddlUnidadeMUS.SelectedValue = coEmp.ToString();
                CarregaColaborador();
            }

            if ((ddlUnidadeMUS.SelectedValue != "") && (txtQtdSMSMes.Enabled))
            {
                int coEmpRefer = int.Parse(ddlUnidadeMUS.SelectedValue);

                var tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                if (tb149.TP_CTRLE_MENSA_SMS == "U")
                {
                    var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmpRefer);

                    if (tb83.FL_ENVIO_SMS != null)
                    {
                        txtQtdSMSMes.Enabled = tb83.FL_ENVIO_SMS == "S";
                    }
                }
                else
                {
                    txtQtdSMSMes.Enabled = tb149.FL_ENVIO_SMS == "S";
                }

            }

        }
        #endregion
    }
}
