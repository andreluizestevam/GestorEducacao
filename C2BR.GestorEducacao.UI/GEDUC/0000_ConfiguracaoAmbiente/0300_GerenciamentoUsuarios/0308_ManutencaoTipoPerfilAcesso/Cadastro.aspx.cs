//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: MANUTENÇÃO DE TIPO DE PERFIL DE ACESSO.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections;
using System.Configuration;
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
using System.Data.Objects.DataClasses;
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0308_ManutencaoTipoPerfilAcesso
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis

        public static Hashtable hashModulo;
        public static int idModulo;

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
            if (!IsPostBack)
            {
                hashModulo = new Hashtable();
                TreeViewFuncTPA.Attributes.Add("onclick", "OnTreeClick(event)");
                CarregaUnidade();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    ddlUnidadeTPA.Enabled = false;
                    TreeViewFuncTPA.Enabled = chkAlteracaoTPA.Enabled = chkConsultaTPA.Enabled = chkExclusaoTPA.Enabled = chkInclusaoTPA.Enabled = true;
                    CarregaTreeView(0);
                }
                else
                {
                    TreeViewFuncTPA.Enabled = ddlUnidadeTPA.Enabled = chkAlteracaoTPA.Enabled = 
                    chkConsultaTPA.Enabled = chkExclusaoTPA.Enabled = chkInclusaoTPA.Enabled = false;
                    CarregaFormulario(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    ddlUnidadeTPA.Enabled = true;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario()
        {
//--------> Limpa o HashTable
            hashModulo.Clear();
            CarregaFormulario(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int resultado = 0;
            int idePerfilAcesso = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            int coEmp = int.Parse(ddlUnidadeTPA.SelectedValue);

            AdmPerfilAcesso admPerfilAcesso = RetornaEntidade(idePerfilAcesso);

            if (admPerfilAcesso == null)
            {
                admPerfilAcesso = new AdmPerfilAcesso();

                admPerfilAcesso.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                admPerfilAcesso.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                admPerfilAcesso.nomeTipoPerfilAcesso = txtNomePerfilTPA.Text;
                admPerfilAcesso.statusTipoPerfilAcesso = ddlStatusTPA.SelectedValue;

                AdmPerfilAcesso.SaveOrUpdate(admPerfilAcesso);

                idePerfilAcesso = (from admPerfAce in AdmPerfilAcesso.RetornaTodosRegistros()
                                   select new { admPerfAce.idePerfilAcesso }).OrderByDescending( a => a.idePerfilAcesso ).Take(1).FirstOrDefault().idePerfilAcesso;

//------------> Inclui na tabela de AdmPerfilModulo
                AdmPerfilModulo admPerfilModulo = new AdmPerfilModulo();
                GravaAdmPerfilModulo(admPerfilModulo, idePerfilAcesso);
                admPerfilAcesso = RetornaEntidade(idePerfilAcesso);

                CurrentPadraoCadastros.CurrentEntity = admPerfilAcesso;
            }
            else
            {
                //int idPerfAcess = Convert.ToInt32(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
                admPerfilAcesso.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                admPerfilAcesso.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                admPerfilAcesso.nomeTipoPerfilAcesso = txtNomePerfilTPA.Text;
                admPerfilAcesso.statusTipoPerfilAcesso = ddlStatusTPA.SelectedValue;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                {
                    var ocorPerfil = (from tb134 in TB134_USR_EMP.RetornaTodosRegistros()
                                      where tb134.AdmPerfilAcesso.idePerfilAcesso == idePerfilAcesso
                                      select new { tb134.IDE_USREMP, }).Count();

                    if (ocorPerfil > 0)
                    {
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Perfil selecionado não pode ser excluído!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                        return;
                    }
                    else
                        AdmPerfilAcesso.Delete(admPerfilAcesso, false);
                }
                else
                    resultado = AdmPerfilAcesso.SaveOrUpdate(admPerfilAcesso, true);

                List<AdmPerfilModulo> lstAdmPerfilModulo = (from admPerfilModulo in AdmPerfilModulo.RetornaTodosRegistros()
                                                            where admPerfilModulo.idePerfilAcesso == idePerfilAcesso
                                                            && admPerfilModulo.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                                            select admPerfilModulo).ToList();
                
//------------> Deleta os registros da tabela AdmPerfilModulo
                foreach (AdmPerfilModulo admPerfilModulo in lstAdmPerfilModulo)
                    AdmPerfilModulo.Delete(admPerfilModulo, false);

                AdmPerfilModulo.SaveChanges();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {                    
//----------------> Grava registros selecionados na tabela AdmPerfilModulo
                    AdmPerfilModulo admPerfilModulo = new AdmPerfilModulo();
                    GravaAdmPerfilModulo(admPerfilModulo, idePerfilAcesso);
                }
            }

            if (resultado > 0)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro adicionado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            else
                CurrentPadraoCadastros.CurrentEntity = admPerfilAcesso;
        }                        

        #endregion

        #region Métodos

//====> 
        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        /// <param name="idPerfilAcesso">Id do perfil de acesso</param>
        private void CarregaFormulario(int idPerfilAcesso)
        {
            var admPerfilAcesso = RetornaEntidade(idPerfilAcesso);

            CarregaTreeView(idPerfilAcesso);
            admPerfilAcesso.TB25_EMPRESAReference.Load();
            ddlUnidadeTPA.SelectedValue = admPerfilAcesso.TB25_EMPRESA != null ? admPerfilAcesso.TB25_EMPRESA.CO_EMP.ToString() : "";
            txtNomePerfilTPA.Text = admPerfilAcesso.nomeTipoPerfilAcesso;
            ddlStatusTPA.SelectedValue = admPerfilAcesso.statusTipoPerfilAcesso;
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="idPerfilAcesso">Id do perfil de acesso</param>
        /// <returns>Entidade AdmPerfilAcesso</returns>
        private AdmPerfilAcesso RetornaEntidade(int idPerfilAcesso)
        {
            if (idPerfilAcesso == 0)
                idPerfilAcesso = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            return AdmPerfilAcesso.RetornaPelaChavePrimaria(idPerfilAcesso, LoginAuxili.ORG_CODIGO_ORGAO);
        }

        /// <summary>
        /// Método que carrega as operaçoes do módulo
        /// </summary>
        /// <param name="inclusao">String inclusão</param>
        /// <param name="alteracao">String alteracao</param>
        /// <param name="exclusao">String exclusao</param>
        /// <param name="consulta">String consulta</param>
        /// <param name="ideAdmModulo">Id do módulo</param>
        private void CarregaPermissoesMod(string inclusao, string alteracao, string exclusao, string consulta, int ideAdmModulo)
        {            
//--------> Cria um Hashtable com as operações disponíveis para cada funcionalidade
            if (ideAdmModulo > 0)
            {
                string permissao = "";

                permissao = consulta == "S" ? permissao + "S;" : permissao;
                permissao = inclusao == "S" ? permissao + "I;" : permissao;
                permissao = alteracao == "S" ? permissao + "U;" : permissao;
                permissao = exclusao == "S" ? permissao + "D;" : permissao;

                if (hashModulo.ContainsKey(ideAdmModulo))
                    hashModulo[ideAdmModulo] = permissao;
                else
                    hashModulo.Add(ideAdmModulo, permissao);
            }
        }

        /// <summary>
        /// Método que verifica as permissões das operaçoes do módulo
        /// </summary>
        /// <param name="ideAdmModulo">Id do módulo</param>
        private void ConsultaPermissoesMod(int ideAdmModulo)
        {
            int idePerfilAcesso = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            string tiposAcesso;
            
//--------> Faz a verificação para saber se já existe no HashTable
            if (hashModulo.ContainsKey(ideAdmModulo))
            {
                tiposAcesso = hashModulo[ideAdmModulo].ToString();
                
//------------> Consulta
                chkConsultaTPA.Checked = tiposAcesso.Contains("S");

//------------> Insere
                chkInclusaoTPA.Checked = tiposAcesso.Contains("I");

//------------> Atualiza
                chkAlteracaoTPA.Checked = tiposAcesso.Contains("U");

//------------> Deleta
                chkExclusaoTPA.Checked = tiposAcesso.Contains("D");
            }
            else
            {                
//------------> Verifica permissão na base utilizada
                var admPerfilModulo = AdmPerfilModulo.RetornaPeloPerfilModulo(idePerfilAcesso, ideAdmModulo, LoginAuxili.ORG_CODIGO_ORGAO);

                if (ideAdmModulo > 0)
                {
                    if (admPerfilModulo != null)
                    {
                        chkExclusaoTPA.Checked = admPerfilModulo.flagItemAcessoDelet == "S";
                        chkInclusaoTPA.Checked = admPerfilModulo.flagItemAcessoInsert == "S";
                        chkConsultaTPA.Checked = admPerfilModulo.flagItemAcessoSelect == "S";
                        chkAlteracaoTPA.Checked = admPerfilModulo.flagItemAcessoUpdate == "S";
                    }
                    else
                        chkExclusaoTPA.Checked = chkInclusaoTPA.Checked = chkConsultaTPA.Checked = chkAlteracaoTPA.Checked = false;
                }
            }
        }
        
        /// <summary>
        /// Método que salva os dados na tabela AdmPerfilModulo
        /// </summary>
        /// <param name="admPerfilModulo">Entidade da tabela AdmPerfilModulo</param>
        /// <param name="idePerfilAcesso">Id do Perfil selecionado</param>
        /// <param name="ideAdmModulo">Id do Módulo selecionado</param>
        private void AddFuncModulo(AdmPerfilModulo admPerfilModulo, int idePerfilAcesso, int ideAdmModulo)
        {
            try
            {
                string tiposAcesso;

                admPerfilModulo = new AdmPerfilModulo();
                admPerfilModulo.ADMMODULO = ADMMODULO.RetornaPelaChavePrimaria(ideAdmModulo);
                admPerfilModulo.AdmPerfilAcesso = AdmPerfilAcesso.RetornaPelaChavePrimaria(idePerfilAcesso, LoginAuxili.ORG_CODIGO_ORGAO);
                admPerfilModulo.idePerfilAcesso = idePerfilAcesso;
                admPerfilModulo.ideAdmModulo = ideAdmModulo;
                admPerfilModulo.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                admPerfilModulo.flagItemAcessoInsert = admPerfilModulo.flagItemAcessoSelect = 
                admPerfilModulo.flagItemAcessoDelet = admPerfilModulo.flagItemAcessoUpdate = "N";

                if (hashModulo.ContainsKey(ideAdmModulo))
                {
                    tiposAcesso = hashModulo[ideAdmModulo].ToString();
                    string[] flaAcesso = tiposAcesso.Split(';');

                    foreach (string acesso in flaAcesso)
                    {
//--------------------> Consulta
                        if (acesso == "S")
                            admPerfilModulo.flagItemAcessoSelect = "S";
//--------------------> Insere
                        else if (acesso == "I")
                            admPerfilModulo.flagItemAcessoInsert = "S";
//--------------------> Atualiza
                        else if (acesso == "U")
                            admPerfilModulo.flagItemAcessoUpdate = "S";
//--------------------> Deleta
                        else if (acesso == "D")
                            admPerfilModulo.flagItemAcessoDelet = "S";
                    }
                }

                AdmPerfilModulo.SaveOrUpdate(admPerfilModulo);
            }
            catch
            {
                AuxiliPagina.EnvioMensagemErro(this, "Operação não foi realizada com sucesso!");
            }
        }

        /// <summary>
        /// Método que salva os itens selecionados no TreeView, na tabela AdmPerfilModulo
        /// </summary>
        /// <param name="admPerfilModulo">Entidade da tabela AdmPerfilModulo</param>
        /// <param name="idePerfilAcesso">Id do Perfil selecionado</param>
        private void GravaAdmPerfilModulo(AdmPerfilModulo admPerfilModulo, int idePerfilAcesso)
        {            
//--------> Varre o TreeView
            TreeNodeCollection nodeCollection = TreeViewFuncTPA.Nodes;
            foreach (TreeNode node in nodeCollection)
            {
                if (node.Checked)
                {
                    AddFuncModulo(admPerfilModulo, idePerfilAcesso, int.Parse(node.Value));
                    foreach (TreeNode nodeAvo in node.ChildNodes)
                    {
                        if (nodeAvo.Checked)
                        {
                            AddFuncModulo(admPerfilModulo, idePerfilAcesso, int.Parse(nodeAvo.Value));
                            foreach (TreeNode nodePai in nodeAvo.ChildNodes)
                            {
                                if (nodePai.Checked)
                                {
                                    AddFuncModulo(admPerfilModulo, idePerfilAcesso, int.Parse(nodePai.Value));
                                    foreach (TreeNode nodeFilho in nodePai.ChildNodes)
                                        if (nodeFilho.Checked)
                                            AddFuncModulo(admPerfilModulo, idePerfilAcesso, int.Parse(nodeFilho.Value));
                                }
                            }
                        }
                    }
                }
            }
        }        

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidadeTPA.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                        where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                        && tb25.CO_SIT_EMP == "A"
                                        select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidadeTPA.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeTPA.DataValueField = "CO_EMP";
            ddlUnidadeTPA.DataBind();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                ddlUnidadeTPA.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o TreeView de acordo com o perfil
        /// </summary>
        /// <param name="idePerfilAcesso">Id do Perfil selecionado</param>
        private void CarregaTreeView(int idePerfilAcesso)
        {
            TreeViewFuncTPA.Nodes.Clear();

            var admModuloRaiz = (from admModulo in ADMMODULO.RetornaTodosRegistros()
                                where admModulo.ADMMODULO2 == null
                                select new
                                {
                                    admModulo.ideAdmModulo, admModulo.nomModulo, admModulo.numOrdemMenu, admModulo.AdmPerfilModulo
                                }).OrderBy( a => a.numOrdemMenu );

            //Módulo Principal
            if (admModuloRaiz.Count() > 0)
            {
                foreach (var resultadoRaiz in admModuloRaiz)
                {                    
//----------------> Cria nós Raiz
                    TreeNode nodeRaiz = new TreeNode();
                    nodeRaiz.Text = resultadoRaiz.nomModulo;
                    nodeRaiz.Value = resultadoRaiz.ideAdmModulo.ToString();
                    nodeRaiz.ShowCheckBox = true;

                    if (idePerfilAcesso > 0)
                    {                        
//--------------------> Faz a verificação das permissões de acesso para cada funcionalidade
                        var admPerfilModulo = resultadoRaiz.AdmPerfilModulo.Where(admPerMod => admPerMod.idePerfilAcesso == idePerfilAcesso &&
                                        admPerMod.ideAdmModulo == resultadoRaiz.ideAdmModulo).FirstOrDefault();
                        if (admPerfilModulo != null)
                        {
                            CarregaPermissoesMod(admPerfilModulo.flagItemAcessoInsert, admPerfilModulo.flagItemAcessoUpdate, admPerfilModulo.flagItemAcessoDelet, admPerfilModulo.flagItemAcessoSelect, resultadoRaiz.ideAdmModulo);
                            nodeRaiz.Checked = true;
                        }
                    }

                    TreeViewFuncTPA.Nodes.Add(nodeRaiz);

                    var admModuloPai = (from admModulo in ADMMODULO.RetornaTodosRegistros()
                                        where admModulo.ADMMODULO2.ideAdmModulo == resultadoRaiz.ideAdmModulo
                                        select new
                                        {
                                            admModulo.ideAdmModulo, admModulo.nomModulo, admModulo.ADMMODULO2, admModulo.AdmPerfilModulo, admModulo.numOrdemMenu
                                        }).OrderBy( a => a.numOrdemMenu );

                    if (admModuloPai.Count() > 0)
                    {
                        foreach (var resultadoPai in admModuloPai)
                        {                            
//------------------------> Cria nós Pais
                            TreeNode nodePais = new TreeNode();
                            nodePais.Text = resultadoPai.nomModulo;
                            nodePais.Value = resultadoPai.ideAdmModulo.ToString();
                            nodePais.ShowCheckBox = true;

                            if (idePerfilAcesso > 0)
                            {
//----------------------------> Faz a verificação das permissões de acesso para cada funcionalidade                                
                                var admPerfilModulo = resultadoPai.AdmPerfilModulo.Where(admPerMod => admPerMod.idePerfilAcesso == idePerfilAcesso &&
                                     admPerMod.ideAdmModulo == resultadoPai.ideAdmModulo).FirstOrDefault();

                                if (admPerfilModulo != null)
                                {
                                    CarregaPermissoesMod(admPerfilModulo.flagItemAcessoInsert, admPerfilModulo.flagItemAcessoUpdate, admPerfilModulo.flagItemAcessoDelet, admPerfilModulo.flagItemAcessoSelect, resultadoPai.ideAdmModulo);
                                    nodePais.Checked = true;
                                }
                            }
                            var admModuloFilho = (from admModulo in ADMMODULO.RetornaTodosRegistros()
                                                  where admModulo.ADMMODULO2.ideAdmModulo == resultadoPai.ideAdmModulo
                                                  select new
                                                  {
                                                      admModulo.ideAdmModulo, admModulo.nomModulo, admModulo.ADMMODULO2, admModulo.AdmPerfilModulo, admModulo.numOrdemMenu
                                                  }).AsQueryable().OrderBy( a => a.numOrdemMenu );

                            if (admModuloFilho.Count() > 0)
                            {
                                foreach (var resultadoFilho in admModuloFilho)
                                {
//--------------------------------> Cria nós Netos
                                    TreeNode nodeFilhos = new TreeNode();
                                    nodeFilhos.Text = resultadoFilho.nomModulo;
                                    nodeFilhos.Value = resultadoFilho.ideAdmModulo.ToString();
                                    nodeFilhos.ShowCheckBox = true;

                                    if (idePerfilAcesso > 0)
                                    {
//------------------------------------> Faz a verificação das permissões de acesso para cada funcionalidade                                        
                                        var admPerfilModulo = resultadoFilho.AdmPerfilModulo.Where(admPerMod => admPerMod.idePerfilAcesso == idePerfilAcesso &&
                                             admPerMod.ideAdmModulo == resultadoFilho.ideAdmModulo).FirstOrDefault();

                                        if (admPerfilModulo != null)
                                        {
                                            CarregaPermissoesMod(admPerfilModulo.flagItemAcessoInsert, admPerfilModulo.flagItemAcessoUpdate, admPerfilModulo.flagItemAcessoDelet, admPerfilModulo.flagItemAcessoSelect, resultadoFilho.ideAdmModulo);
                                            nodeFilhos.Checked = true;
                                        }
                                    }
                                    nodePais.ChildNodes.Add(nodeFilhos);
                                }
                            }
                            nodeRaiz.ChildNodes.Add(nodePais);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Faz a checagem das permissões para passar como parâmetro para o método CarregaPermissoesMod
        /// </summary>
        private void ChecaPermissoes()
        {
            string checkInclusao = chkInclusaoTPA.Checked ? "S" : "N";
            string checkAlteracao = chkAlteracaoTPA.Checked ? "S" : "N";
            string checkExclusao = chkExclusaoTPA.Checked ? "S" : "N";
            string checkConsulta = chkConsultaTPA.Checked ? "S" : "N";

            CarregaPermissoesMod(checkInclusao, checkAlteracao, checkExclusao, checkConsulta, idModulo);
        }
        #endregion

        protected void TreeViewFuncTPA_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeViewFuncTPA.SelectedNode.Checked)
            {
                idModulo = int.Parse(TreeViewFuncTPA.SelectedNode.Value);

                ConsultaPermissoesMod(idModulo);
                TreeViewFuncTPA.SelectedNodeStyle.BackColor = System.Drawing.Color.Silver;
//------------> Seta o foco no nó selecionado
                ClientScript.RegisterStartupScript(this.GetType(), "selectNode", "var elem = document.getElementById('" + TreeViewFuncTPA.ClientID + "_SelectedNode'" + ");var node = document.getElementById(elem.value);node.scrollIntoView(true);elem.scrollLeft=0;", true);
            }
            else
                chkAlteracaoTPA.Checked = chkConsultaTPA.Checked = chkExclusaoTPA.Checked = chkInclusaoTPA.Checked = false;
        }

        protected void chkInclusaoTPA_CheckedChanged(object sender, EventArgs e)
        {
            ChecaPermissoes();
        }

        protected void chkAlteracaoTPA_CheckedChanged(object sender, EventArgs e)
        {
            ChecaPermissoes();
        }

        protected void chkExclusaoTPA_CheckedChanged(object sender, EventArgs e)
        {
            ChecaPermissoes();
        }

        protected void chkConsultaTPA_CheckedChanged(object sender, EventArgs e)
        {
            ChecaPermissoes();
        }
    }
}
