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
// 09/12/2014| Maxwell Almeida           | Funcionalidade para associação de procedimentos médicos à Unidades CRIADA

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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9117_AssocProcPlanoSaude
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
                CarregaPlanos();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    AuxiliPagina.RedirecionaParaPaginaErro("Funcionalidade apenas de alteração, não disponível inclusão.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
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
            int ID_PLAN = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            //--------> Varre o TreeView
            TreeNodeCollection nodeCollection = TreeViewFuncTPA.Nodes;
            foreach (TreeNode node in nodeCollection)
            {
                //AddFuncModulo(admPerfilModulo, idePerfilAcesso, int.Parse(node.Value));
                foreach (TreeNode nodeAvo in node.ChildNodes)
                {
                    //AddFuncModulo(admPerfilModulo, idePerfilAcesso, int.Parse(nodeAvo.Value));
                    foreach (TreeNode nodePai in nodeAvo.ChildNodes)
                    {
                        int id_proc_filho = int.Parse(nodePai.Value);
                        //Verifica se já está associado
                        bool temAssociadoItPr = TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros().Where(
                        w => w.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE ==
                           id_proc_filho && w.TB251_PLANO_OPERA.ID_PLAN == ID_PLAN).Any();

                        if (nodePai.Checked)
                        {
                            //Caso esteja checkado mas ainda não exista a associação, registra a associação
                            if (!temAssociadoItPr)
                            {
                                TBS362_ASSOC_PLANO_PROCE tbs362 = TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros().Where(w => w.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == id_proc_filho && w.TB251_PLANO_OPERA.ID_PLAN == ID_PLAN).FirstOrDefault() ?? new TBS362_ASSOC_PLANO_PROCE();
                                tbs362.TB251_PLANO_OPERA = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(ID_PLAN);
                                tbs362.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(id_proc_filho);
                                tbs362.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs362.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs362.DT_CADAS = DateTime.Now;
                                tbs362.IP_CADAS = Request.UserHostAddress;
                                tbs362.CO_COL_STATU = LoginAuxili.CO_COL;
                                tbs362.CO_EMP_STATU = LoginAuxili.CO_EMP;
                                tbs362.DT_STATU = DateTime.Now;
                                tbs362.FL_STATU = "A";
                                tbs362.IP_STATU = Request.UserHostAddress;
                                TBS362_ASSOC_PLANO_PROCE.SaveOrUpdate(tbs362, true);
                            }
                        }
                        else
                        {
                            //Caso não esteja marcado, mas possua associação, instancia e deleta a associação
                            if (temAssociadoItPr)
                            {
                                TBS362_ASSOC_PLANO_PROCE tbs362 = TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros().Where(w => w.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == id_proc_filho && w.TB251_PLANO_OPERA.ID_PLAN == ID_PLAN).FirstOrDefault();
                                tbs362.CO_COL_STATU = LoginAuxili.CO_COL;
                                tbs362.CO_EMP_STATU = LoginAuxili.CO_EMP;
                                tbs362.DT_STATU = DateTime.Now;
                                tbs362.FL_STATU = "I";
                                tbs362.IP_STATU = Request.UserHostAddress;
                                TBS362_ASSOC_PLANO_PROCE.SaveOrUpdate(tbs362, true);
                            }
                        }
                    }
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Alterações realizadas com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        #endregion

        #region Métodos

        //====> 
        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        /// <param name="idPerfilAcesso">Id do perfil de acesso</param>
        private void CarregaFormulario(int ID_PLANO)
        {
            ddlPlano.SelectedValue = ID_PLANO.ToString();
            CarregaTreeView(ID_PLANO);
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaPlanos()
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, "0", false, true, true);
        }

        /// <summary>
        /// Método que carrega o TreeView de acordo com os procedimentos da instituição
        /// </summary>
        /// <param name="ID_PLANO">Id do Perfil selecionado</param>
        private void CarregaTreeView(int ID_PLANO)
        {
            var dadPlan = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(ID_PLANO);
            dadPlan.TB250_OPERAReference.Load();
            int ID_OPER = dadPlan.TB250_OPERA.ID_OPER;

            TreeViewFuncTPA.Nodes.Clear();

            var gruposProcMed = (from tbs354 in TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros()
                                 where tbs354.FL_SITUA_PROC_MEDIC_GRUPO == "A"
                                 select new
                                 {
                                     tbs354.ID_PROC_MEDIC_GRUPO,
                                     tbs354.NM_PROC_MEDIC_GRUPO,
                                 }).OrderBy(a => a.NM_PROC_MEDIC_GRUPO);

            //grupo de procedimentos
            if (gruposProcMed.Count() > 0)
            {
                //Percorre cada item do tree view
                foreach (var resultadoRaiz in gruposProcMed)
                {
                    //----------------> Cria nós Raiz
                    TreeNode nodeRaiz = new TreeNode();
                    nodeRaiz.Text = resultadoRaiz.NM_PROC_MEDIC_GRUPO;
                    nodeRaiz.Value = resultadoRaiz.ID_PROC_MEDIC_GRUPO.ToString();
                    nodeRaiz.ShowCheckBox = true;

                    if (ID_PLANO > 0)
                    {
                        //--------------------> Faz a verificação se tem alguma associação de procedimentos desse grupo com este plano
                        bool temAssociado = TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros().Where(
                            w => w.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO ==
                                resultadoRaiz.ID_PROC_MEDIC_GRUPO && w.TB251_PLANO_OPERA.ID_PLAN == ID_PLANO
                                  && w.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER == ID_OPER
                                  && w.FL_STATU == "A").Any();

                        if (temAssociado)
                            nodeRaiz.Checked = true;
                    }

                    TreeViewFuncTPA.Nodes.Add(nodeRaiz);

                    // Lista os nós subgrupos
                    var subGruposProc = (from tbs355 in TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros()
                                         where tbs355.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == resultadoRaiz.ID_PROC_MEDIC_GRUPO
                                         && tbs355.FL_SITUA_PROC_MEDIC_GRUP == "A"
                                         select new
                                         {
                                             tbs355.ID_PROC_MEDIC_SGRUP,
                                             tbs355.NM_PROC_MEDIC_SGRUP,
                                         }).AsQueryable().OrderBy(a => a.NM_PROC_MEDIC_SGRUP);

                    if (subGruposProc.Count() > 0)
                    {
                        foreach (var resultadoPai in subGruposProc)
                        {
                            //------------------------> Cria nós subGrupos
                            TreeNode nodePais = new TreeNode();
                            nodePais.Text = resultadoPai.NM_PROC_MEDIC_SGRUP;
                            nodePais.Value = resultadoPai.ID_PROC_MEDIC_SGRUP.ToString();
                            nodePais.ShowCheckBox = true;

                            if (ID_PLANO > 0)
                            {
                                //--------------------> Faz a verificação se tem algum item filho do subGrupo em questão;
                                bool temAssociadoSbGr = TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros().Where(
                                    w => w.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP ==
                                        resultadoPai.ID_PROC_MEDIC_SGRUP && w.TB251_PLANO_OPERA.ID_PLAN == ID_PLANO
                                        && w.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER == ID_OPER
                                        && w.FL_STATU == "A").Any();          

                                if (temAssociadoSbGr)
                                    nodePais.Checked = true;
                            }

                            var itemProceMedic = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                                  where tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == resultadoPai.ID_PROC_MEDIC_SGRUP
                                                  && tbs356.TB250_OPERA.ID_OPER == ID_OPER
                                                  && tbs356.CO_SITU_PROC_MEDI == "A"
                                                  select new
                                                  {
                                                      tbs356.ID_PROC_MEDI_PROCE,
                                                      tbs356.NM_PROC_MEDI,
                                                  }).AsQueryable().OrderBy(a => a.NM_PROC_MEDI);

                            if (itemProceMedic.Count() > 0)
                            {
                                foreach (var resultadoFilho in itemProceMedic)
                                {
                                    //--------------------------------> Cria nós de procedimentos
                                    TreeNode nodeFilhos = new TreeNode();
                                    nodeFilhos.Text = resultadoFilho.NM_PROC_MEDI;
                                    nodeFilhos.Value = resultadoFilho.ID_PROC_MEDI_PROCE.ToString();
                                    nodeFilhos.ShowCheckBox = true;

                                    if (ID_PLANO > 0)
                                    {
                                        //--------------------> Faz a verificação se tem algum item associado em questão;
                                        bool temAssociadoItPr = TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros().Where(
                                            w => w.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE ==
                                                resultadoFilho.ID_PROC_MEDI_PROCE && w.TB251_PLANO_OPERA.ID_PLAN == ID_PLANO
                                                  && w.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER == ID_OPER
                                                  && w.FL_STATU == "A").Any();

                                        if (temAssociadoItPr)
                                            nodeFilhos.Checked = true;
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

        #endregion

        protected void TreeViewFuncTPA_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeViewFuncTPA.SelectedNode.Checked)
            {
                idModulo = int.Parse(TreeViewFuncTPA.SelectedNode.Value);

                TreeViewFuncTPA.SelectedNodeStyle.BackColor = System.Drawing.Color.Silver;
                //------------> Seta o foco no nó selecionado
                ClientScript.RegisterStartupScript(this.GetType(), "selectNode", "var elem = document.getElementById('" + TreeViewFuncTPA.ClientID + "_SelectedNode'" + ");var node = document.getElementById(elem.value);node.scrollIntoView(true);elem.scrollLeft=0;", true);
            }
        }
    }
}