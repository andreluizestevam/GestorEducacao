//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PESQUISAS/ENQUETES DE SATISFAÇÃO
// OBJETIVO: GERA A PESQUISA INSTITUCIONAL
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
using System.Data.Sql;
using System.Data.Objects;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1300_ServicosApoioAdministrativo._1330_CtrlPesquisaInstitucional._1334_GeraPesquisaInstSaude
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
                TreeViewQuest.Attributes.Add("onclick", "OnTreeClick(event)");

                CarregaUnidades();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    
                    CarregaColabs();
                    CarregaAvaliacoes();
                    ddlResponsavel.SelectedValue = LoginAuxili.CO_COL.ToString();

                    txtDataStatus.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }

                int codTipoAval = ddlAvaliacao.SelectedValue != "" ? int.Parse(ddlAvaliacao.SelectedValue) : 0;

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    CarregaTreeView(codTipoAval);
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario()
        {
            if (!IsPostBack)
            {
                
                
                CarregaColabs();
                CarregaAvaliacoes();
                CarregaFormulario();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

           

            //--------> Faz a verificação para saber se as questões foram selecionadas
            if (TreeViewQuest.CheckedNodes.Count == 0)
            {
                ServerValidateEventArgs eArgs = new ServerValidateEventArgs("", false);
                AuxiliPagina.EnvioMensagemErro(this, "Usuário deve selecionar as questões");
                return;
            }

            if (Page.IsValid)
            {
                TB201_AVAL_MASTER tb201 = RetornaEntidade();

                if (tb201 == null)
                {
                    tb201 = new TB201_AVAL_MASTER();

                    tb201.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    tb201.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb201.DT_CADASTRO = DateTime.Now;
                }

                tb201.NM_TITU_AVAL = txtTituloAvaliacao.Text;
                tb201.FLA_PUBLICO_ALVO = ddlPublicoAlvo.SelectedValue;
                tb201.FLA_IDENT = ddlIdentificada.SelectedValue;             
                tb201.CO_COL = int.Parse(ddlResponsavel.SelectedValue);
                tb201.STATUS = ddlStatus.SelectedValue;
                tb201.DT_STATUS = DateTime.Now;

                tb201.CO_TIPO_AVAL = int.Parse(ddlAvaliacao.SelectedValue);

                List<TB202_AVAL_DETALHE> tb202 = (from lTb202 in TB202_AVAL_DETALHE.RetornaTodosRegistros()
                                                  where lTb202.NU_AVAL_MASTER.Equals(tb201.NU_AVAL_MASTER)
                                                  select lTb202).ToList();

                foreach (TB202_AVAL_DETALHE linha in tb202)
                    TB202_AVAL_DETALHE.Delete(linha, false);

                TB202_AVAL_DETALHE.SaveChanges();

                //------------> Faz a verificação de todos os itens da TreeView de questões
                foreach (TreeNode node in TreeViewQuest.Nodes)
                {
                    if (node.Checked)
                    {
                        //--------------------> Faz a verificação de itens selecionados na TreeView de questões e adiciona na BD
                        foreach (TreeNode child in node.ChildNodes)
                        {
                            if (child.Checked)
                            {
                                TB202_AVAL_DETALHE questao = new TB202_AVAL_DETALHE();

                                int coTipoAval = int.Parse(ddlAvaliacao.SelectedValue);
                                int coTituAval = int.Parse(node.Value);
                                int nuQuestAval = int.Parse(child.Value);

                                questao.CO_TIPO_AVAL = coTipoAval;
                                questao.CO_TITU_AVAL = coTituAval;
                                questao.NU_QUEST_AVAL = nuQuestAval;
                                questao.NU_AVAL_MASTER = tb201.NU_AVAL_MASTER;

                                TB202_AVAL_DETALHE.SaveOrUpdate(questao, true);
                            }
                        }
                    }
                }

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                else
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro cadastrado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB201_AVAL_MASTER tb201 = RetornaEntidade();

            if (tb201 != null)
            {
                tb201.TB25_EMPRESAReference.Load();

                txtTituloAvaliacao.Text = tb201.NM_TITU_AVAL;
                ddlPublicoAlvo.SelectedValue = tb201.FLA_PUBLICO_ALVO;
                ddlIdentificada.SelectedValue = tb201.FLA_IDENT;
                ddlUnidade.SelectedValue = tb201.TB25_EMPRESA.CO_EMP.ToString();
              
               
               
                txtDataCadastro.Text = tb201.DT_CADASTRO.ToString("dd/MM/yyyy");
                ddlResponsavel.SelectedValue = tb201.CO_COL.ToString();
                ddlStatus.SelectedValue = tb201.STATUS;
                txtDataStatus.Text = tb201.DT_STATUS.ToString("dd/MM/yyyy");
                ddlAvaliacao.SelectedValue = tb201.CO_TIPO_AVAL.ToString();
                CarregaTreeView(int.Parse(ddlAvaliacao.SelectedValue));

                List<TB202_AVAL_DETALHE> lstTb202 = (from lTb202 in TB202_AVAL_DETALHE.RetornaTodosRegistros()
                                                     where lTb202.NU_AVAL_MASTER.Equals(tb201.NU_AVAL_MASTER)
                                                     select lTb202).ToList();

                foreach (TB202_AVAL_DETALHE lTb202 in lstTb202)
                {
                    string strNodePath = string.Format("{0}/{1}", lTb202.CO_TITU_AVAL.ToString(), lTb202.NU_QUEST_AVAL.ToString());

                    TreeNode node = TreeViewQuest.FindNode(strNodePath);
                    node.Checked = true;
                    node.Parent.Checked = true;
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB201_AVAL_MASTER</returns>
        private TB201_AVAL_MASTER RetornaEntidade()
        {
            return TB201_AVAL_MASTER.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        /// <summary>
        /// Método que carrega a TreeView de Questões
        /// </summary>
        /// <param name="coTipoAval">Id do tipo de avaliação</param>
        private void CarregaTreeView(int coTipoAval)
        {
            TreeViewQuest.Nodes.Clear();

            List<TB72_TIT_QUES_AVAL> lstTb72 = (from lTb72 in TB72_TIT_QUES_AVAL.RetornaTodosRegistros().Include(typeof(TB71_QUEST_AVAL).Name)
                                                where lTb72.CO_TIPO_AVAL == coTipoAval
                                                select lTb72).ToList();

            foreach (var lTb72 in lstTb72)
            {
                //------------> Fazendo a criação do nó Pai
                TreeNode trRoot = new TreeNode();
                trRoot.Text = lTb72.NO_TITU_AVAL;
                trRoot.Value = lTb72.CO_TITU_AVAL.ToString();
                trRoot.ShowCheckBox = true;
                trRoot.SelectAction = TreeNodeSelectAction.None;

                TreeViewQuest.Nodes.Add(trRoot);

                foreach (var tb71 in lTb72.TB71_QUEST_AVAL)
                {
                    trRoot.ChildNodes.Add(new TreeNode
                    {
                        Text = tb71.DE_QUES_AVAL,
                        Value = tb71.NU_QUES_AVAL.ToString(),
                        ShowCheckBox = true,
                        SelectAction = TreeNodeSelectAction.None
                    });
                }
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Avaliação
        /// </summary>
        private void CarregaAvaliacoes()
        {
            ddlAvaliacao.DataSource = (from tb73 in TB73_TIPO_AVAL.RetornaTodosRegistros()
                                       select new { tb73.CO_TIPO_AVAL, tb73.NO_TIPO_AVAL });

            ddlAvaliacao.DataTextField = "NO_TIPO_AVAL";
            ddlAvaliacao.DataValueField = "CO_TIPO_AVAL";
            ddlAvaliacao.DataBind();

            ddlAvaliacao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores Responsáveis
        /// </summary>
        private void CarregaColabs()
        {
            ddlResponsavel.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                         select new { tb03.CO_COL, tb03.NO_COL }).OrderBy(c => c.NO_COL);

            ddlResponsavel.DataTextField = "NO_COL";
            ddlResponsavel.DataValueField = "CO_COL";
            ddlResponsavel.DataBind();

            ddlResponsavel.Items.Insert(0, new ListItem("Selecione", ""));
        }


        #endregion

        protected void ddlAvaliacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coTipoAval = ddlAvaliacao.SelectedValue != "" ? int.Parse(ddlAvaliacao.SelectedValue) : 0;

            CarregaTreeView(coTipoAval);
        }
    }
}