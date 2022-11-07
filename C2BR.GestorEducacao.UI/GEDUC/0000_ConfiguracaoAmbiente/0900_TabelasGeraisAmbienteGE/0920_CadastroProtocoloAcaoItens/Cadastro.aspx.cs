//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CADASTRO INTERNACIONAL DE DOENÇAS.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 09/12/16 |   BRUNO VIEIRA LANDIM      |  Criado funcionalidade    

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
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0920_CadastroProtocoloAcaoItens
{
    public partial class Cadastro : System.Web.UI.Page
    {

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaPrioridadeTarefa(ddlPriorItem);
                CarregaItens();
                txtData.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            //CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão e Alteração de Registros na Entidade do BD, após a ação de salvar
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlTipo.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O tipo de protocolo deve ser selecionado");
                    return;
                }

                if (string.IsNullOrEmpty(txtNome.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O nome do protocolo é requerido, por favor informá-lo");
                    return;
                }

                if (string.IsNullOrEmpty(txtSigla.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A SIGLA do protocolo é requerido, por favor informá-la");
                    return;
                }

                var res = TB426_PROTO_ACAO.RetornaTodosRegistros().Where(x => x.CO_SIGLA_PROTO_ACAO.Contains(txtSigla.Text)).ToList();

                if (res.Count > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A SIGLA informada já consta nos registro, por favor insira uma diferente");
                    return;
                }

                TB426_PROTO_ACAO tb426 = RetornaEntidade();

                if (tb426.ID_PROTO_ACAO == 0)
                {
                    tb426.DT_CAD = DateTime.Now;
                    tb426.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                }

                tb426.NO_PROTO_ACAO = txtNome.Text.Trim().ToUpper();
                tb426.DE_PROTO_ACAO = txtDescricao.Text;
                tb426.FL_SITUA = ddlSituacao.SelectedValue;
                tb426.CO_SIGLA_PROTO_ACAO = txtSigla.Text;
                tb426.TP_SISTEMA = LoginAuxili.CO_TIPO_UNID;
                tb426.TP_PROTO_ACAO = ddlTipo.SelectedValue;
                tb426.DT_SITUA = Convert.ToDateTime(txtData.Text);
                tb426.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tb426.CO_COL_SITUA = LoginAuxili.CO_COL;

                TB426_PROTO_ACAO.SaveOrUpdate(tb426, true);
                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro de execução, entre em contato com o suporte para solucionar o problema");
            }
        }

        //====> Processo de Exclusão de Registros na Entidade do BD, após a ação de salvar    
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow li in grdItensProto.Rows)
            {
                TB427_PROTO_ACAO_ITENS.Delete(TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value)), true);
                TB427_PROTO_ACAO_ITENS.SaveChanges();
            }

            TB426_PROTO_ACAO.Delete(TB426_PROTO_ACAO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id)), true);
            TB426_PROTO_ACAO.SaveChanges();

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Excluido com sucesso!");
            AuxiliPagina.RedirecionaParaPaginaBusca();
        }

        //====> Processo de Redirecionamento para tela de Busca
        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            AuxiliPagina.RedirecionaParaPaginaBusca();
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            try
            {
                if (ddlTipo.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O tipo de protocolo deve ser selecionado");
                    return;
                }

                if (string.IsNullOrEmpty(txtNome.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O nome do protocolo é requerido, favor informá-lo");
                    return;
                }

                TB426_PROTO_ACAO tb426 = RetornaEntidade();

                if (tb426.ID_PROTO_ACAO == 0)
                {
                    tb426.DT_CAD = DateTime.Now;
                    tb426.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                }

                tb426.NO_PROTO_ACAO = txtNome.Text.Trim().ToUpper();
                tb426.DE_PROTO_ACAO = txtDescricao.Text;
                tb426.FL_SITUA = ddlSituacao.SelectedValue;
                tb426.CO_SIGLA_PROTO_ACAO = txtSigla.Text;
                tb426.TP_SISTEMA = LoginAuxili.CO_TIPO_UNID;
                tb426.TP_PROTO_ACAO = ddlTipo.SelectedValue;
                tb426.DT_SITUA = Convert.ToDateTime(txtData.Text);
                tb426.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tb426.CO_COL_SITUA = LoginAuxili.CO_COL;

                CurrentPadraoCadastros.CurrentEntity = tb426;
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro de execução, entre em contato com o suporte para solucionar o problema");
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB426_PROTO_ACAO tb426 = RetornaEntidade();

            tb426.TB03_COLABORReference.Load();

            CarregaItens();

            if (tb426 != null)
            {
                hidProto.Value = tb426.ID_PROTO_ACAO.ToString();
                ddlTipo.SelectedValue = tb426.TP_PROTO_ACAO;
                txtNome.Text = tb426.NO_PROTO_ACAO;
                txtDescricao.Text = tb426.DE_PROTO_ACAO;
                ddlSituacao.SelectedValue = tb426.FL_SITUA;
                txtSigla.Text = tb426.CO_SIGLA_PROTO_ACAO;
                txtData.Text = tb426.DT_SITUA.ToString();
                CarregaItens(tb426.ID_PROTO_ACAO);
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        private TB426_PROTO_ACAO RetornaEntidade()
        {
            TB426_PROTO_ACAO tbs426 = TB426_PROTO_ACAO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs426 == null) ? new TB426_PROTO_ACAO() : tbs426;
        }

        private void CarregaPrioridadeTarefa(DropDownList ddl)
        {
            ddl.DataSource = TB140_PRIOR_TAREF_AGEND.RetornaTodosRegistros();
            ddl.DataValueField = "CO_PRIOR_TAREF_AGEND";
            ddl.DataTextField = "DE_PRIOR_TAREF_AGEND";
            ddl.DataBind();
        }

        /// <summary>
        /// Carrega a lista de itens recebido como parâmetro 
        /// </summary>
        /// <param name="ID_ITEM_PROTO"></param>
        private void CarregaItens(int ID_PROTO_ACAO = 0)
        {
            var res = new List<saidaItensProto>();

            if (ID_PROTO_ACAO != 0)
            {
                res = (from tb427 in TB427_PROTO_ACAO_ITENS.RetornaTodosRegistros()
                       where (ID_PROTO_ACAO == tb427.TB426_PROTO_ACAO.ID_PROTO_ACAO)
                       select new saidaItensProto
                       {
                           ID_ITEM_PROTO = tb427.ID_PROTO_ACAO_ITENS,
                           NO_ITEM_PROTO = tb427.NO_ACAO,
                           DE_ITEM_PROTO = tb427.DE_ACAO,
                           OBS_ITEM_PROTO = tb427.DE_OBSER_ACAO,
                           REFER_ITEM_PROTO = tb427.CO_REFER_ACAO,
                           QTD_ITEM_PROTO = (tb427.QT_EXEC_ACAO == null ? 0 : tb427.QT_EXEC_ACAO),
                           PRIO_ITEM_PROTO = tb427.TB140_PRIOR_TAREF_AGEND.CO_PRIOR_TAREF_AGEND,
                           CO_SITU = (tb427.CO_SITU.Equals("A") ? true : false),
                           SEQ_ITEM = (tb427.SEQ_ITEM != null ? tb427.SEQ_ITEM : 0)
                       }).ToList();
            }

            res = res.OrderBy(w => w.NO_ITEM_PROTO).ToList();

            grdItensProto.DataSource = res;
            grdItensProto.DataBind();

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                DropDownList ddl = ((DropDownList)li.FindControl("ddlPriorItem"));
                CarregaPrioridadeTarefa(ddl);

                foreach (var r in res.Where(x => x.ID_ITEM_PROTO == int.Parse(((HiddenField)li.FindControl("idItemProto")).Value)))
                {
                    ((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue = r.PRIO_ITEM_PROTO;
                }
            }
        }

        public class saidaItensProto
        {
            public int ID_ITEM_PROTO { get; set; }
            public string NO_ITEM_PROTO { get; set; }
            public string DE_ITEM_PROTO { get; set; }
            public string OBS_ITEM_PROTO { get; set; }
            public string REFER_ITEM_PROTO { get; set; }
            public int? QTD_ITEM_PROTO { get; set; }
            public int? SEQ_ITEM { get; set; }
            public string PRIO_ITEM_PROTO { get; set; }
            public bool CO_SITU { get; set; }
        }

        public void onClick_AddItem(object sender, EventArgs e)
        {
            TB426_PROTO_ACAO tb426 = RetornaEntidade();

            if (String.IsNullOrEmpty(txtNomeItem.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page,"Insira um nome para o item.");
                txtNomeItem.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtReferItem.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Insira uma referência para o item.");
                txtReferItem.Focus();
                return;
            }

            if (tb426.ID_PROTO_ACAO != 0)
            {
                try
                {
                    var res = TB427_PROTO_ACAO_ITENS.RetornaTodosRegistros().Where(x => x.CO_REFER_ACAO.Contains(txtReferItem.Text)).ToList();

                    if (res.Count > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A referência do item já consta nos registros,por favor informe uma referência diferente.");
                        txtReferItem.Focus();
                        return;
                    }

                    TB427_PROTO_ACAO_ITENS tb427 = new TB427_PROTO_ACAO_ITENS();
                    tb427.TB426_PROTO_ACAO = tb426;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(ddlPriorItem.SelectedValue);
                    tb427.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.CO_REFER_ACAO = txtReferItem.Text;
                    tb427.CO_SITU = "A";
                    tb427.DE_ACAO = txtDescItem.Text;
                    tb427.DE_OBSER_ACAO = txtObsItem.Text;
                    tb427.NO_ACAO = txtNomeItem.Text;
                    tb427.QT_EXEC_ACAO = String.IsNullOrEmpty(txtQtdItem.Text) ? 1 : int.Parse(txtQtdItem.Text);
                    tb427.DT_CAD = DateTime.Now;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.SEQ_ITEM = String.IsNullOrEmpty(txtSeqItem.Text) ? 0 : int.Parse(txtSeqItem.Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427, true);
                }
                catch (Exception) { }
            }
            else
            {
                try
                {
                    if (ddlTipo.SelectedValue == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O tipo deve ser selecionado");
                        ddlTipo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtNome.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O nome do protocolo de ação é requerido, favor informá-lo");
                        txtNome.Focus();
                        return;
                    }

                    if (tb426.ID_PROTO_ACAO == 0)
                    {
                        tb426.DT_CAD = DateTime.Now;
                        tb426.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                    }

                    tb426.NO_PROTO_ACAO = txtNome.Text.Trim().ToUpper();
                    tb426.DE_PROTO_ACAO = txtDescricao.Text;
                    tb426.FL_SITUA = ddlSituacao.SelectedValue;
                    tb426.CO_SIGLA_PROTO_ACAO = txtSigla.Text;
                    tb426.TP_SISTEMA = LoginAuxili.CO_TIPO_UNID;
                    tb426.TP_PROTO_ACAO = ddlTipo.SelectedValue;
                    tb426.DT_SITUA = Convert.ToDateTime(txtData.Text);
                    tb426.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tb426.CO_COL_SITUA = LoginAuxili.CO_COL;

                    TB426_PROTO_ACAO.SaveOrUpdate(tb426, true);

                    var result = (from Tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                  select Tb426).OrderByDescending(x => x.ID_PROTO_ACAO).FirstOrDefault();

                    TB426_PROTO_ACAO TB426 = TB426_PROTO_ACAO.RetornaPelaChavePrimaria(result.ID_PROTO_ACAO);


                    if (String.IsNullOrEmpty(txtNomeItem.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Insira um nome para o item.");
                        txtNomeItem.Focus();
                        return;
                    }

                    TB427_PROTO_ACAO_ITENS tb427 = new TB427_PROTO_ACAO_ITENS();
                    tb427.TB426_PROTO_ACAO = TB426;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(ddlPriorItem.SelectedValue);
                    tb427.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.CO_REFER_ACAO = txtReferItem.Text;
                    tb427.CO_SITU = "A";
                    tb427.DE_ACAO = txtDescItem.Text;
                    tb427.DE_OBSER_ACAO = txtObsItem.Text;
                    tb427.NO_ACAO = txtNomeItem.Text;
                    tb427.QT_EXEC_ACAO = String.IsNullOrEmpty(txtQtdItem.Text) ? 0 : int.Parse(txtQtdItem.Text);
                    tb427.DT_CAD = DateTime.Now;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.SEQ_ITEM = String.IsNullOrEmpty(txtSeqItem.Text) ? 0 : int.Parse(txtSeqItem.Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427, true);
                }
                catch (Exception) { }
            }
            CarregaItens(tb426.ID_PROTO_ACAO);
        }

        public void imgExcItem_OnClick(object sender, EventArgs e)
        {
            int idItem;
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                img = (ImageButton)li.FindControl("imgExcItem");

                if (img.ClientID == atual.ClientID)
                {
                    idItem = int.Parse(((HiddenField)li.FindControl("idItemProto")).Value);

                    TB427_PROTO_ACAO_ITENS.Delete(TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(idItem), true);
                    TB427_PROTO_ACAO_ITENS.SaveChanges();
                }
            }
            CarregaItens(int.Parse(hidProto.Value));
        }

        public void alterItemChk(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (chk.ClientID == ((CheckBox)li.FindControl("chkSituaItemGrd")).ClientID)
                {
                    TB427_PROTO_ACAO_ITENS tb427 = TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tb427.TB140_PRIOR_TAREF_AGENDReference.Load();
                    tb427.TB03_COLABORReference.Load();
                    tb427.TB426_PROTO_ACAOReference.Load();
                    tb427.NO_ACAO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tb427.DE_ACAO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue);
                    tb427.CO_SITU = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.CO_REFER_ACAO = ((TextBox)li.FindControl("txtReferItemGrd")).Text;
                    tb427.DE_OBSER_ACAO = ((TextBox)li.FindControl("txtObsItem")).Text;
                    tb427.QT_EXEC_ACAO = int.Parse(((TextBox)li.FindControl("txtQtdItemGrd")).Text);
                    tb427.SEQ_ITEM = int.Parse(((TextBox)li.FindControl("txtSeqItem")).Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427);
                }
            }
        }

        public void alterItemTxtNome(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (txt.ClientID == ((TextBox)li.FindControl("txtNomeItemGrd")).ClientID)
                {
                    TB427_PROTO_ACAO_ITENS tb427 = TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tb427.TB140_PRIOR_TAREF_AGENDReference.Load();
                    tb427.TB03_COLABORReference.Load();
                    tb427.TB426_PROTO_ACAOReference.Load();
                    tb427.NO_ACAO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tb427.DE_ACAO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue);
                    tb427.CO_SITU = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.CO_REFER_ACAO = ((TextBox)li.FindControl("txtReferItemGrd")).Text;
                    tb427.DE_OBSER_ACAO = ((TextBox)li.FindControl("txtObsItem")).Text;
                    tb427.QT_EXEC_ACAO = int.Parse(((TextBox)li.FindControl("txtQtdItemGrd")).Text);
                    tb427.SEQ_ITEM = int.Parse(((TextBox)li.FindControl("txtSeqItem")).Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427);
                }
            }
        }

        public void alterItemTxtDesc(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (txt.ClientID == ((TextBox)li.FindControl("txtDescItem")).ClientID)
                {
                    TB427_PROTO_ACAO_ITENS tb427 = TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tb427.TB140_PRIOR_TAREF_AGENDReference.Load();
                    tb427.TB03_COLABORReference.Load();
                    tb427.TB426_PROTO_ACAOReference.Load();
                    tb427.NO_ACAO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tb427.DE_ACAO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue);
                    tb427.CO_SITU = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.CO_REFER_ACAO = ((TextBox)li.FindControl("txtReferItemGrd")).Text;
                    tb427.DE_OBSER_ACAO = ((TextBox)li.FindControl("txtObsItem")).Text;
                    tb427.QT_EXEC_ACAO = int.Parse(((TextBox)li.FindControl("txtQtdItemGrd")).Text);
                    tb427.SEQ_ITEM = int.Parse(((TextBox)li.FindControl("txtSeqItem")).Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427);
                }
            }
        }

        public void alterItemTxtRefer(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (txt.ClientID == ((TextBox)li.FindControl("txtReferItemGrd")).ClientID)
                {
                    TB427_PROTO_ACAO_ITENS tb427 = TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tb427.TB140_PRIOR_TAREF_AGENDReference.Load();
                    tb427.TB03_COLABORReference.Load();
                    tb427.TB426_PROTO_ACAOReference.Load();
                    tb427.NO_ACAO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tb427.DE_ACAO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue);
                    tb427.CO_SITU = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.CO_REFER_ACAO = ((TextBox)li.FindControl("txtReferItemGrd")).Text;
                    tb427.DE_OBSER_ACAO = ((TextBox)li.FindControl("txtObsItem")).Text;
                    tb427.QT_EXEC_ACAO = int.Parse(((TextBox)li.FindControl("txtQtdItemGrd")).Text);
                    tb427.SEQ_ITEM = int.Parse(((TextBox)li.FindControl("txtSeqItem")).Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427);
                }
            }
        }

        public void alterItemTxtQtd(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (txt.ClientID == ((TextBox)li.FindControl("txtQtdItemGrd")).ClientID)
                {
                    TB427_PROTO_ACAO_ITENS tb427 = TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tb427.TB140_PRIOR_TAREF_AGENDReference.Load();
                    tb427.TB03_COLABORReference.Load();
                    tb427.TB426_PROTO_ACAOReference.Load();
                    tb427.NO_ACAO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tb427.DE_ACAO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue);
                    tb427.CO_SITU = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.CO_REFER_ACAO = ((TextBox)li.FindControl("txtReferItemGrd")).Text;
                    tb427.DE_OBSER_ACAO = ((TextBox)li.FindControl("txtObsItem")).Text;
                    tb427.QT_EXEC_ACAO = int.Parse(((TextBox)li.FindControl("txtQtdItemGrd")).Text);
                    tb427.SEQ_ITEM = int.Parse(((TextBox)li.FindControl("txtSeqItem")).Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427);
                }
            }
        }

        public void alterItemTxtObs(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (txt.ClientID == ((TextBox)li.FindControl("txtObsItem")).ClientID)
                {
                    TB427_PROTO_ACAO_ITENS tb427 = TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tb427.TB140_PRIOR_TAREF_AGENDReference.Load();
                    tb427.TB03_COLABORReference.Load();
                    tb427.TB426_PROTO_ACAOReference.Load();
                    tb427.NO_ACAO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tb427.DE_ACAO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue);
                    tb427.CO_SITU = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.CO_REFER_ACAO = ((TextBox)li.FindControl("txtReferItemGrd")).Text;
                    tb427.DE_OBSER_ACAO = ((TextBox)li.FindControl("txtObsItem")).Text;
                    tb427.QT_EXEC_ACAO = int.Parse(((TextBox)li.FindControl("txtQtdItemGrd")).Text);
                    tb427.SEQ_ITEM = int.Parse(((TextBox)li.FindControl("txtSeqItem")).Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427);
                }
            }
        }

        public void alterItemTxtSeq(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (txt.ClientID == ((TextBox)li.FindControl("txtSeqItem")).ClientID)
                {
                    TB427_PROTO_ACAO_ITENS tb427 = TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tb427.TB140_PRIOR_TAREF_AGENDReference.Load();
                    tb427.TB03_COLABORReference.Load();
                    tb427.TB426_PROTO_ACAOReference.Load();
                    tb427.NO_ACAO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tb427.DE_ACAO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue);
                    tb427.CO_SITU = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.CO_REFER_ACAO = ((TextBox)li.FindControl("txtReferItemGrd")).Text;
                    tb427.DE_OBSER_ACAO = ((TextBox)li.FindControl("txtObsItem")).Text;
                    tb427.QT_EXEC_ACAO = int.Parse(((TextBox)li.FindControl("txtQtdItemGrd")).Text);
                    tb427.SEQ_ITEM = int.Parse(((TextBox)li.FindControl("txtSeqItem")).Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427);
                }
            }
        }

        public void alterItemDdl(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (ddl.ClientID == ((DropDownList)li.FindControl("ddlPriorItem")).ClientID)
                {
                    TB427_PROTO_ACAO_ITENS tb427 = TB427_PROTO_ACAO_ITENS.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tb427.TB140_PRIOR_TAREF_AGENDReference.Load();
                    tb427.TB03_COLABORReference.Load();
                    tb427.TB426_PROTO_ACAOReference.Load();
                    tb427.NO_ACAO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tb427.DE_ACAO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tb427.TB140_PRIOR_TAREF_AGEND = TB140_PRIOR_TAREF_AGEND.RetornaPelaChavePrimaria(((DropDownList)li.FindControl("ddlPriorItem")).SelectedValue);
                    tb427.CO_SITU = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tb427.CO_COL_SITU = LoginAuxili.CO_COL;
                    tb427.CO_EMP_SITU = LoginAuxili.CO_EMP;
                    tb427.DT_SITUA = DateTime.Now;
                    tb427.CO_REFER_ACAO = ((TextBox)li.FindControl("txtReferItemGrd")).Text;
                    tb427.DE_OBSER_ACAO = ((TextBox)li.FindControl("txtObsItem")).Text;
                    tb427.QT_EXEC_ACAO = int.Parse(((TextBox)li.FindControl("txtQtdItemGrd")).Text);
                    tb427.SEQ_ITEM = int.Parse(((TextBox)li.FindControl("txtSeqItem")).Text);

                    TB427_PROTO_ACAO_ITENS.SaveOrUpdate(tb427);
                }
            }
        }

        #endregion
    }

}