//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: PERFIL DO USUARIO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas 
using System;
using System.Collections.Generic;
using System.Linq;
using Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq.Expressions;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.Security;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class MeuPerfil : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgColMP.Visible = false;
                imgColMP.Width = 86;
                imgColMP.Height = 105;
                CarregaUnidade();
                CarregaColaborador();
                divSucessMessage.Visible = false;
                CarregaFormulario(LoginAuxili.CO_COL, LoginAuxili.CO_UNID_FUNC);
                VerificaUnidadeLogada();
            }
        }                    
        #endregion

        #region Métodos

//====> Método de preenchimento do formulário da funcionalidade
        private void CarregaFormulario(int coCol, int coEmp)
        {
            var tb03 = RetornaEntidade(coCol, coEmp);

            if (tb03 != null)
            {                
//------------> Carregamento da imagem do usuário
                if (tb03.Image != null)
                {
                    imgColMP.Visible = true;
                    imgColMP.ImageUrl = "~/LerImagem.ashx?idimg=" + tb03.Image.ImageId;
                }

                ddlCol.SelectedValue = tb03.CO_COL.ToString();

                txtApelido.Text = tb03.NO_APEL_COL;
                txtEmail.Text = tb03.CO_EMAI_COL;

                if (tb03.NU_TELE_CELU_COL != null)
                    txtCelular.Text = tb03.NU_TELE_CELU_COL.ToString();

                if (tb03.NU_TELE_RESI_COL != null)
                    txtTelefone.Text = tb03.NU_TELE_RESI_COL.ToString();

                if (tb03.CO_DEPTO != null)
                {
                    var vTb14 = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                                 where tb14.CO_DEPTO == tb03.CO_DEPTO
                                 select new { tb14.NO_DEPTO }).FirstOrDefault();

                    txtDepartamento.Text = vTb14 != null ? vTb14.NO_DEPTO : "";
                }
                else
                    txtDepartamento.Text = "";

                var vTb15 = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                             where tb15.CO_FUN == tb03.CO_FUN
                             select new { tb15.NO_FUN }).FirstOrDefault();

                txtFuncao.Text = vTb15 != null ? vTb15.NO_FUN : "";

                var admUsuario = ADMUSUARIO.RetornaPelaUnidColabor(tb03.CO_EMP, tb03.CO_COL);

//------------> Se usuário existente, preenche as informações de acessos hora/dia
                if (admUsuario != null)
                {
                    txtDtSituacao.Text = admUsuario.DataStatus.ToString("dd/MM/yyyy");

                    if (admUsuario.HR_INIC_ACESSO != null)
                        txtHoraAcessoI.Text = int.Parse(admUsuario.HR_INIC_ACESSO).ToString("0000");

                    if (admUsuario.HR_FIM_ACESSO != null)
                        txtHoraAcessoF.Text = int.Parse(admUsuario.HR_FIM_ACESSO).ToString("0000");

                    foreach (ListItem lstDiaAcesso in cbDiaAcesso.Items)
                    {
                        if (lstDiaAcesso.Value == "SG")
                            lstDiaAcesso.Selected = admUsuario.FLA_ACESS_SEG == "S";

                        if (lstDiaAcesso.Value == "TR")
                            lstDiaAcesso.Selected = admUsuario.FLA_ACESS_TER == "S";

                        if (lstDiaAcesso.Value == "QR")
                            lstDiaAcesso.Selected = admUsuario.FLA_ACESS_QUA == "S";

                        if (lstDiaAcesso.Value == "QN")
                            lstDiaAcesso.Selected = admUsuario.FLA_ACESS_QUI == "S";

                        if (lstDiaAcesso.Value == "SX")
                            lstDiaAcesso.Selected = admUsuario.FLA_ACESS_SEX == "S";

                        if (lstDiaAcesso.Value == "SB")
                            lstDiaAcesso.Selected = admUsuario.FLA_ACESS_TER == "S";

                        if (lstDiaAcesso.Value == "DG")
                            lstDiaAcesso.Selected = admUsuario.FLA_ACESS_DOM == "S";
                    }

                    chkFreqManu.Checked = admUsuario.FLA_MANUT_PONTO == "S";
                    chkBibliReserva.Checked = admUsuario.FLA_MANUT_RESER_BIBLI != null ? admUsuario.FLA_MANUT_RESER_BIBLI.ToString() == "S" ? true : false : false;
                    chkSerEnvioSMS.Checked = admUsuario.FLA_SMS != null ? (admUsuario.FLA_SMS.ToString() == "S" ? true : false) : false;
                    ddlUsuaCaixa.SelectedValue = admUsuario.FLA_MANUT_CAIXA == null ? "N" : admUsuario.FLA_MANUT_CAIXA;
                    txtLogin.Text = admUsuario.desLogin;                
                    txtLogin.Enabled = false;
                }
            }
        }

//====> Método de retorno da entidade informada
        private TB03_COLABOR RetornaEntidade(int col, int emp)
        {
            return TB03_COLABOR.RetornaPelaChavePrimaria(emp, col);
        }

        /// <summary>
        /// Salva a nova senha na tabela de usuário informada
        /// </summary>
        /// <param name="admUsuario">Entidade ADMUSUARIO</param>
        private void SalvaNovaSenha(ADMUSUARIO admUsuario)
        {
            admUsuario.desSenha = LoginAuxili.GerarMD5(txtConfirmaNovaSenha.Text);
            ADMUSUARIO.SaveOrUpdate(admUsuario);

            lblMensagem.Text = "Nova Senha salva com Sucesso!";
            lblMensagem.Visible = true;
            divSucessMessage.Visible = true;
            divAlterarSenhaContent.Visible = false;
        }
        #endregion       
 
        #region Carregamento DropDown

        /// <summary>
        /// Verifica qual o tipo da unidade logada e providencia as devidas alterações na página
        /// </summary>
        private void VerificaUnidadeLogada()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    /*ddlClaUsu.Items.Insert(0, new ListItem("Outro", "O"));
                    ddlClaUsu.Items.Insert(0, new ListItem("Profissional Saúde", "P"));
                    ddlClaUsu.Items.Insert(0, new ListItem("Funcionário", "N"));
                    ddlClaUsu.Items.Insert(0, new ListItem("Estagiário(a)", "E"));
                    ddlClaUsu.Items.Insert(0, new ListItem("Selecione", ""));*/

                    chkGestorEducacao.Text = "Portal Saúde";
                    break;
                case "PGE":
                    /*ddlClaUsu.Items.Insert(0, new ListItem("Outro", "O"));
                    ddlClaUsu.Items.Insert(0, new ListItem("Profissional Saúde", "P"));
                    ddlClaUsu.Items.Insert(0, new ListItem("Funcionário", "N"));
                    ddlClaUsu.Items.Insert(0, new ListItem("Estagiário(a)", "E"));
                    ddlClaUsu.Items.Insert(0, new ListItem("Selecione", ""));*/

                    chkGestorEducacao.Text = "Portal Educação";
                    break;
            }
        }

        /// <summary>
        /// Carrega o dropdown de unidade
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.CO_EMP == LoginAuxili.CO_EMP
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            CarregaColaborador();
        }

        /// <summary>
        /// Carrega o dropdown de colaborador
        /// </summary>
        private void CarregaColaborador()
        {
            ddlCol.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                 where tb03.CO_COL == LoginAuxili.CO_COL
                                 select new { tb03.CO_COL, NO_COL = tb03.NO_COL.ToUpper() });

            ddlCol.DataTextField = "NO_COL";
            ddlCol.DataValueField = "CO_COL";
            ddlCol.DataBind();
        }
        #endregion

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string strSenhaMD5 = LoginAuxili.GerarMD5(txtSenhaAtual.Text);

            if (!Page.IsValid)
                return;

            ADMUSUARIO admUsuario = (from lAdmUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                     where lAdmUsuario.desSenha.Equals(strSenhaMD5) && lAdmUsuario.ideAdmUsuario.Equals(LoginAuxili.IDEADMUSUARIO)
                                     select lAdmUsuario).FirstOrDefault();

            if (admUsuario != null)
                SalvaNovaSenha(admUsuario);
            else
                AuxiliPagina.EnvioMensagemErro(this, "Senha Atual Incorreta. Por Favor preencha o campo Senha Atual e tente novamente.");
        }

        protected void btnMeusAcessos_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            new AuxiliRelatorioTemporario().RelMeusAcessos(this.Page, LoginAuxili.CO_EMP.ToString(), LoginAuxili.CO_UNID_FUNC.ToString(), LoginAuxili.CO_COL.ToString());
        }

        protected void btnMinhasMsgs_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            new AuxiliRelatorioTemporario().RelMinhasMsgs(this.Page, LoginAuxili.CO_EMP.ToString(), LoginAuxili.CO_UNID_FUNC.ToString(), LoginAuxili.CO_COL.ToString());
        }

        protected void btnMinhasInformacoes_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string strP_FLA_PROFESSOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL).FLA_PROFESSOR.ToString();
            string strTIPO_PRESENCA = strP_FLA_PROFESSOR == "S" ? TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)).TP_PONTO_PROF : TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)).TP_PONTO_FUNC;

            string strP_TP_PONTO = strTIPO_PRESENCA != null ? strTIPO_PRESENCA : "L";
            

            new AuxiliRelatorioTemporario().RelFicCadFuncionario(this.Page, LoginAuxili.CO_EMP.ToString(), LoginAuxili.CO_UNID_FUNC.ToString(), LoginAuxili.CO_COL.ToString(), strP_FLA_PROFESSOR, strP_TP_PONTO);
        }

        protected void btnMeusAtalhos_Click(object sender, EventArgs e)
        {
        }

        protected void btnMinhaBiblioteca_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            new AuxiliRelatorioTemporario().RelMinhaBibliot(this.Page, LoginAuxili.CO_EMP.ToString(), LoginAuxili.CO_UNID_FUNC.ToString(), LoginAuxili.CO_COL.ToString());
        }

        protected void btnMeusContat_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            new AuxiliRelatorioTemporario().RelAgendaContatos(this.Page, LoginAuxili.CO_EMP.ToString(), LoginAuxili.CO_COL.ToString());
        } 
    }
}