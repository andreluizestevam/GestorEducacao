//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: ASSOCIA USUÁRIO A UNIDADE EDUCACIONAL E PERFIL DE ACESSO.
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0302_AssociaUsuUnidEducPerfilAcesso
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
            if (!Page.IsPostBack)
            {
                CarregaTipoUsuario();
                CarregaUnidade();
                CarregaPerfil();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    ddlUnidadeAssoc.Enabled = ddlUsuarioAssoc.Enabled = false;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int ideAdmUsuario = int.Parse(ddlUsuarioAssoc.SelectedValue);
            int idPerfilAcesso = int.Parse(ddlPerfilAssoc.SelectedValue);
            int coEmp = int.Parse(ddlUnidadeAssoc.SelectedValue);
            string flOrigem = cbOrigem.Checked == true ? "S" : "N";
            
//--------> Verifica se perfil escolhido já está associado ao usuário/unidade selecionados            
            int ocorPerfil = (from tb134 in TB134_USR_EMP.RetornaTodosRegistros()
                              where tb134.ADMUSUARIO.ideAdmUsuario == ideAdmUsuario && tb134.AdmPerfilAcesso.idePerfilAcesso == idPerfilAcesso
                              && tb134.TB25_EMPRESA.CO_EMP == coEmp && tb134.FLA_STATUS == "A"
                              select new { tb134.IDE_USREMP }).Count();

            if (ocorPerfil > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.EnvioMensagemErro(this, "Perfil selecionado, com Status Ativo, já está associado ao Usuário e Unidade");
            else
            {
                TB134_USR_EMP tb134 = RetornaEntidade();
                var ctx = GestorEntities.CurrentContext;
                var varTb134 = (from lTb134 in ctx.TB134_USR_EMP
                                where lTb134.ADMUSUARIO.ideAdmUsuario == ideAdmUsuario
                                select lTb134);
                if (cbOrigem.Enabled && flOrigem == "S")
                {
                    foreach (TB134_USR_EMP iTb134 in varTb134)
                    {
                        iTb134.FLA_ORIGEM = "N";
                        if (iTb134.IDE_USREMP == tb134.IDE_USREMP)
                            iTb134.FLA_ORIGEM = flOrigem;
                    }
                    ctx.SaveChanges();
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {                    
                    tb134.ADMUSUARIO = ADMUSUARIO.RetornaPelaChavePrimaria(ideAdmUsuario);
                    tb134.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb134.FLA_ORIGEM = flOrigem;
                }

                tb134.AdmPerfilAcesso = AdmPerfilAcesso.RetornaPelaChavePrimaria(idPerfilAcesso, LoginAuxili.ORG_CODIGO_ORGAO);
                tb134.FLA_STATUS = ddlStatusAssoc.SelectedValue;

                CurrentPadraoCadastros.CurrentEntity = tb134;
                
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB134_USR_EMP tb134 = RetornaEntidade();

            if (tb134 != null)
            {
                tb134.TB25_EMPRESAReference.Load();
                tb134.ADMUSUARIOReference.Load();
                tb134.AdmPerfilAcessoReference.Load();

                drpTipoUsu.SelectedValue = tb134.ADMUSUARIO.TipoUsuario;
                CarregaUsuario();
                if (ddlUsuarioAssoc.Items.FindByValue(tb134.ADMUSUARIO.ideAdmUsuario.ToString()) != null)
                    ddlUsuarioAssoc.SelectedValue = tb134.ADMUSUARIO.ideAdmUsuario.ToString();

                ddlUnidadeAssoc.SelectedValue = tb134.TB25_EMPRESA.CO_EMP.ToString();
                ddlPerfilAssoc.SelectedValue = tb134.AdmPerfilAcesso.idePerfilAcesso.ToString();
                ddlStatusAssoc.SelectedValue = tb134.FLA_STATUS;
                cbOrigem.Checked = tb134.FLA_ORIGEM == "S" ? true : false;
                cbOrigem.Enabled = !cbOrigem.Checked;
            }
            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB134_USR_EMP</returns>
        private TB134_USR_EMP RetornaEntidade()
        {
            TB134_USR_EMP tb134 = TB134_USR_EMP.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb134 == null) ? new TB134_USR_EMP() : tb134;
        }
        #endregion        

        #region Carregamento DropDown
        
        /// <summary>
        /// Método que carrega o DropDown de Usuários
        /// </summary>
        private void CarregaUsuario()
        {
            var tpUsu = drpTipoUsu.SelectedValue;
            int coEmp = ddlUnidadeAssoc.SelectedValue != "" ? int.Parse(ddlUnidadeAssoc.SelectedValue) : 0;
            
            ddlUsuarioAssoc.Items.Clear();

            if (string.IsNullOrEmpty(tpUsu))
                return;

            if (tpUsu == "A")
            {
                ddlUsuarioAssoc.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                              join tb07 in TB07_ALUNO.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb07.CO_ALU
                                              where admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                                              && (coEmp == 0 ? 0 == 0 : tb07.CO_EMP == coEmp)
                                              select new { admUsuario.ideAdmUsuario, NO_COL = tb07.NO_ALU }).OrderBy(a => a.NO_COL);
            }
            else if (tpUsu == "R")
            {
                ddlUsuarioAssoc.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                              join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb108.CO_RESP
                                              where admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                                              && (coEmp == 0 ? 0 == 0 : tb108.CO_INST == LoginAuxili.ORG_CODIGO_ORGAO)
                                              && admUsuario.TipoUsuario == "R"
                                              select new { admUsuario.ideAdmUsuario, NO_COL = tb108.NO_RESP }).OrderBy(a => a.NO_COL);
            }
            else
            {
                ddlUsuarioAssoc.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                              join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                              where admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                                              && (coEmp == 0 ? 0 == 0 : tb03.CO_EMP == coEmp)
                                              select new { admUsuario.ideAdmUsuario, NO_COL = tb03.NO_COL }).OrderBy(a => a.NO_COL);
            }

            ddlUsuarioAssoc.DataTextField = "NO_COL";
            ddlUsuarioAssoc.DataValueField = "ideAdmUsuario";
            ddlUsuarioAssoc.DataBind();

            ddlUsuarioAssoc.Items.Insert(0, new ListItem("Selecione", ""));
            ddlUsuarioAssoc.SelectedValue = "";
        }

        /// <summary>
        /// Método que carrega o DropDown de Unidades Escolares
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidadeAssoc.Items.Clear();
            ddlUnidadeAssoc.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                          where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                          select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidadeAssoc.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeAssoc.DataValueField = "CO_EMP";
            ddlUnidadeAssoc.DataBind();

            ddlUnidadeAssoc.Items.Insert(0, new ListItem("Selecione", ""));
            ddlUnidadeAssoc.SelectedValue = "";
        }

        /// <summary>
        /// Método que carrega o DropDown de Perfis de Acesso
        /// </summary>
        private void CarregaPerfil()
        {
            int coEmp = ddlUnidadeAssoc.SelectedValue == "" ? 0 : int.Parse(ddlUnidadeAssoc.SelectedValue);
            ddlPerfilAssoc.Items.Clear();
            ddlPerfilAssoc.DataSource = (from admPerfAcesso in AdmPerfilAcesso.RetornaTodosRegistros()
                                         where admPerfAcesso.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         && (coEmp == 0 ? 0==0 : admPerfAcesso.TB25_EMPRESA.CO_EMP == coEmp)
                                         && admPerfAcesso.idePerfilAcesso != 32
                                         select new { admPerfAcesso.nomeTipoPerfilAcesso, admPerfAcesso.idePerfilAcesso }).OrderBy( a => a.nomeTipoPerfilAcesso );

            ddlPerfilAssoc.DataTextField = "nomeTipoPerfilAcesso";
            ddlPerfilAssoc.DataValueField = "idePerfilAcesso";
            ddlPerfilAssoc.DataBind();

            ddlPerfilAssoc.Items.Insert(0, new ListItem("Selecione", ""));
            ddlPerfilAssoc.SelectedValue = "";
        }

        private void CarregaTipoUsuario()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    drpTipoUsu.Items.Clear();
                    drpTipoUsu.Items.Insert(0, new ListItem("Outro", "O"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Profissional Saúde", "S"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Funcionário", "F"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Estagiário(a)", "E"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Selecione", ""));

                    break;
                case "PGE":
                default:
                    drpTipoUsu.Items.Clear();
                    drpTipoUsu.Items.Insert(0, new ListItem("Outro", "O"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Aluno", "A"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Responsável", "R"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Professor", "S"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Funcionário", "F"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Estagiário(a)", "E"));
                    drpTipoUsu.Items.Insert(0, new ListItem("Selecione", ""));
                    break;
            }
        }
        #endregion

        #region Eventos de componentes

        protected void ddlUnidadeAssoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaPerfil();
        }

        //protected void ddlUsuarioAssoc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (((DropDownList)sender).SelectedValue != "")
        //        CarregaUnidade();
        //}

        protected void drpTipoUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuario();
        }

        #endregion
    }
}
