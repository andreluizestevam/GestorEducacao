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
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0918_CadastroProtocoloCID
{
    public partial class Cadastro : System.Web.UI.Page
    {

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaCID();
                CarregaItens();
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
                if (ddlCID.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O código CID deve ser selecionado");
                    return;
                }

                if (string.IsNullOrEmpty(txtNome.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O nome do protocolo CID é requerido, favor informá-lo");
                    return;
                }

                TBS434_PROTO_CID tbs434 = RetornaEntidade();

                if (tbs434.ID_PROTO_CID == 0)
                {
                    tbs434.IP_CADAS = LoginAuxili.IP_USU;
                    tbs434.DT_CADAS = DateTime.Now;
                    tbs434.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs434.CO_COL_CADAS = LoginAuxili.CO_COL;
                }

                tbs434.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(int.Parse(ddlCID.SelectedValue));
                tbs434.NO_PROTO_CID = txtNome.Text.Trim().ToUpper();
                tbs434.DE_PROTO_CID = txtDescricao.Text;
                //tbs434.CO_TIPO = (int?)int.Parse(ddlTipo.SelectedValue);
                tbs434.FL_STATUS = ddlSituacao.SelectedValue;
                tbs434.DT_SITUA = DateTime.Now;
                tbs434.IP_SITUA = LoginAuxili.IP_USU;
                tbs434.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs434.CO_COL_SITUA = LoginAuxili.CO_COL;

                CurrentPadraoCadastros.CurrentEntity = tbs434;
                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Cadastrado com sucesso!");
                AuxiliPagina.RedirecionaParaPaginaBusca();
            }
            catch (Exception)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro de execução, entre em contato com o suporte para solucionar o problema");
            }
        }

        //====> Processo de Exclusão de Registros na Entidade do BD, após a ação de salvar    
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow li in grdItensProto.Rows)
            {
                TBS436_ITEM_PROTO_CID.Delete(TBS436_ITEM_PROTO_CID.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value)), true);
                TBS436_ITEM_PROTO_CID.SaveChanges();
            }

            TBS434_PROTO_CID.Delete(TBS434_PROTO_CID.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id)), true);
            TBS434_PROTO_CID.SaveChanges();

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

                //if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                //{
                //    foreach (GridViewRow li in grdItensProto.Rows)
                //    {
                //        TBS436_ITEM_PROTO_CID.Delete(TBS436_ITEM_PROTO_CID.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value)), true);
                //        TBS436_ITEM_PROTO_CID.SaveChanges();
                //    }

                //    TBS434_PROTO_CID.Delete(TBS434_PROTO_CID.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id)), true);
                //    TBS434_PROTO_CID.SaveChanges();
                //}

                if (ddlCID.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O código CID deve ser selecionado");
                    return;
                }

                if (string.IsNullOrEmpty(txtNome.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O nome do protocolo CID é requerido, favor informá-lo");
                    return;
                }

                TBS434_PROTO_CID tbs434 = RetornaEntidade();

                if (tbs434.ID_PROTO_CID == 0)
                {
                    tbs434.IP_CADAS = LoginAuxili.IP_USU;
                    tbs434.DT_CADAS = DateTime.Now;
                    tbs434.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs434.CO_COL_CADAS = LoginAuxili.CO_COL;
                }

                tbs434.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(int.Parse(ddlCID.SelectedValue));
                tbs434.NO_PROTO_CID = txtNome.Text.Trim().ToUpper();
                tbs434.DE_PROTO_CID = txtDescricao.Text;
                //tbs434.CO_TIPO = (int?)int.Parse(ddlTipo.SelectedValue);
                tbs434.FL_STATUS = ddlSituacao.SelectedValue;
                tbs434.DT_SITUA = DateTime.Now;
                tbs434.IP_SITUA = LoginAuxili.IP_USU;
                tbs434.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs434.CO_COL_SITUA = LoginAuxili.CO_COL;

                CurrentPadraoCadastros.CurrentEntity = tbs434;
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
            TBS434_PROTO_CID tbs434 = RetornaEntidade();

            tbs434.TB117_CODIGO_INTERNACIONAL_DOENCAReference.Load();

            CarregaItens();

            if (tbs434 != null)
            {
                hidCID.Value = tbs434.ID_PROTO_CID.ToString();
                ddlCID.SelectedValue = tbs434.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID.ToString();
                txtNome.Text = tbs434.NO_PROTO_CID;
                txtDescricao.Text = tbs434.DE_PROTO_CID;
                //ddlTipo.SelectedValue = tbs434.CO_TIPO.ToString();
                ddlSituacao.SelectedValue = tbs434.FL_STATUS;
                CarregaItens(tbs434.ID_PROTO_CID);
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        private TBS434_PROTO_CID RetornaEntidade()
        {
            TBS434_PROTO_CID tbs434 = TBS434_PROTO_CID.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs434 == null) ? new TBS434_PROTO_CID() : tbs434;
        }

        // Carrega DropDownList ddlCID
        private void CarregaCID()
        {
            var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                       where tb117.CO_SITUA_CID == "A"
                       select new
                       {
                           tb117.IDE_CID,
                           NOME = tb117.NO_CID
                       });

            ddlCID.Items.Clear();
            ddlCID.ClearSelection();

            if (res != null)
            {
                ddlCID.DataTextField = "NOME";
                ddlCID.DataValueField = "IDE_CID";
                ddlCID.DataSource = res;
                ddlCID.DataBind();
            }

            ddlCID.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega a lista de itens recebido como parâmetro 
        /// </summary>
        /// <param name="ID_ITEM_PROTO"></param>
        private void CarregaItens(int ID_ITEM_PROTO = 0)
        {
            var res = new List<saidaItensProto>();

            if (ID_ITEM_PROTO != 0)
            {
                res = (from tbs436 in TBS436_ITEM_PROTO_CID.RetornaTodosRegistros()
                       where (ID_ITEM_PROTO == tbs436.TBS434_PROTO_CID.ID_PROTO_CID)
                       select new saidaItensProto
                       {
                           ID_ITEM_PROTO = tbs436.ID_ITEM_PROTO,
                           NO_ITEM_PROTO = tbs436.NO_ITEM_PROTO,
                           DE_ITEM_PROTO = tbs436.DE_ITEM_PROTO,
                           TP_ITEM_PROTO = tbs436.TP_ITEM_PROTO,
                           CO_SITU = (tbs436.CO_SITU_ITEM_PROTO.Equals("A") ? true : false)
                       }).ToList();
            }

            res = res.OrderBy(w => w.NO_ITEM_PROTO).ToList();

            grdItensProto.DataSource = res;
            grdItensProto.DataBind();

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                foreach (var r in res.Where(x => x.ID_ITEM_PROTO == int.Parse(((HiddenField)li.FindControl("idItemProto")).Value)))
                {
                    ((DropDownList)li.FindControl("ddlTipoItemGrd")).SelectedValue = r.TP_ITEM_PROTO;
                }
            }
        }

        public class saidaItensProto
        {
            public int ID_ITEM_PROTO { get; set; }
            public string NO_ITEM_PROTO { get; set; }
            public string DE_ITEM_PROTO { get; set; }
            public string TP_ITEM_PROTO { get; set; }
            public bool CO_SITU { get; set; }
        }

        public void onClick_AddItem(object sender, EventArgs e)
        {
            TBS434_PROTO_CID tbs434 = RetornaEntidade();

            if (tbs434.ID_PROTO_CID != 0)
            {
                try
                {
                    TBS436_ITEM_PROTO_CID tbs436 = new TBS436_ITEM_PROTO_CID();
                    tbs436.TBS434_PROTO_CID = tbs434;
                    tbs436.NO_ITEM_PROTO = txtNomeItem.Text;
                    tbs436.DE_ITEM_PROTO = txtDescItem.Text;
                    tbs436.TP_ITEM_PROTO = ddlTipoItem.SelectedValue;
                    tbs436.CO_SITU_ITEM_PROTO = "A";
                    tbs436.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                    tbs436.IP_CAD_ITEM_PROTO = LoginAuxili.IP_USU;
                    tbs436.DT_CAD_ITEM_PROTO = DateTime.Now;
                    tbs436.CO_COL_SITUA_ITEM_PROTO = LoginAuxili.CO_COL;
                    tbs436.CO_EMP_SITUA_ITEM_PROTO = LoginAuxili.CO_EMP;
                    tbs436.IP_SITUA_ITEM_PROTO = LoginAuxili.IP_USU;
                    tbs436.DT_SITUA_ITEM_PROTO = DateTime.Now;

                    TBS436_ITEM_PROTO_CID.SaveOrUpdate(tbs436);
                }
                catch (Exception) { }
            }
            else
            {
                try
                {
                    if (ddlCID.SelectedValue == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O código CID deve ser selecionado");
                        return;
                    }

                    if (string.IsNullOrEmpty(txtNome.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O nome do protocolo CID é requerido, favor informá-lo");
                        return;
                    }

                    if (tbs434.ID_PROTO_CID == 0)
                    {
                        tbs434.IP_CADAS = LoginAuxili.IP_USU;
                        tbs434.DT_CADAS = DateTime.Now;
                        tbs434.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs434.CO_COL_CADAS = LoginAuxili.CO_COL;
                    }

                    tbs434.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(int.Parse(ddlCID.SelectedValue));
                    tbs434.NO_PROTO_CID = txtNome.Text.Trim().ToUpper();
                    tbs434.DE_PROTO_CID = txtDescricao.Text;
                    //tbs434.CO_TIPO = (int?)int.Parse(ddlTipo.SelectedValue);
                    tbs434.FL_STATUS = ddlSituacao.SelectedValue;
                    tbs434.DT_SITUA = DateTime.Now;
                    tbs434.IP_SITUA = LoginAuxili.IP_USU;
                    tbs434.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs434.CO_COL_SITUA = LoginAuxili.CO_COL;

                    TBS434_PROTO_CID.SaveOrUpdate(tbs434);

                    var result = (from Tbs434 in TBS434_PROTO_CID.RetornaTodosRegistros()
                                  select Tbs434).OrderByDescending(x => x.ID_PROTO_CID).FirstOrDefault();

                    TBS434_PROTO_CID TBS434 = TBS434_PROTO_CID.RetornaPelaChavePrimaria(result.ID_PROTO_CID);

                    TBS436_ITEM_PROTO_CID tbs436 = new TBS436_ITEM_PROTO_CID();
                    tbs436.TBS434_PROTO_CID = TBS434;
                    tbs436.NO_ITEM_PROTO = txtNomeItem.Text;
                    tbs436.DE_ITEM_PROTO = txtDescItem.Text;
                    tbs436.TP_ITEM_PROTO = ddlTipoItem.SelectedValue;
                    tbs436.CO_SITU_ITEM_PROTO = "A";
                    tbs436.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                    tbs436.IP_CAD_ITEM_PROTO = LoginAuxili.IP_USU;
                    tbs436.DT_CAD_ITEM_PROTO = DateTime.Now;
                    tbs436.CO_COL_SITUA_ITEM_PROTO = LoginAuxili.CO_COL;
                    tbs436.CO_EMP_SITUA_ITEM_PROTO = LoginAuxili.CO_EMP;
                    tbs436.IP_SITUA_ITEM_PROTO = LoginAuxili.IP_USU;
                    tbs436.DT_SITUA_ITEM_PROTO = DateTime.Now;

                    TBS436_ITEM_PROTO_CID.SaveOrUpdate(tbs436);
                }
                catch (Exception) { }
            }
            CarregaItens(tbs434.ID_PROTO_CID);
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

                    TBS436_ITEM_PROTO_CID.Delete(TBS436_ITEM_PROTO_CID.RetornaPelaChavePrimaria(idItem), true);
                    TBS436_ITEM_PROTO_CID.SaveChanges();
                }
            }
            int cid = !string.IsNullOrEmpty(hidCID.Value) ? int.Parse(hidCID.Value) : 0;
            CarregaItens(cid);
        }

        public void alterItemChk(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (chk.ClientID == ((CheckBox)li.FindControl("chkSituaItemGrd")).ClientID)
                {
                    TBS436_ITEM_PROTO_CID tbs436 = TBS436_ITEM_PROTO_CID.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tbs436.TBS434_PROTO_CIDReference.Load();
                    tbs436.TB03_COLABORReference.Load();

                    tbs436.NO_ITEM_PROTO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tbs436.DE_ITEM_PROTO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tbs436.TP_ITEM_PROTO = ((DropDownList)li.FindControl("ddlTipoItemGrd")).SelectedValue;
                    tbs436.CO_SITU_ITEM_PROTO = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tbs436.CO_COL_SITUA_ITEM_PROTO = LoginAuxili.CO_COL;
                    tbs436.CO_EMP_SITUA_ITEM_PROTO = LoginAuxili.CO_EMP;
                    tbs436.IP_SITUA_ITEM_PROTO = LoginAuxili.IP_USU;
                    tbs436.DT_SITUA_ITEM_PROTO = DateTime.Now;

                    TBS436_ITEM_PROTO_CID.SaveOrUpdate(tbs436);
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
                    TBS436_ITEM_PROTO_CID tbs436 = TBS436_ITEM_PROTO_CID.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tbs436.TBS434_PROTO_CIDReference.Load();
                    tbs436.TB03_COLABORReference.Load();

                    tbs436.NO_ITEM_PROTO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tbs436.DE_ITEM_PROTO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tbs436.TP_ITEM_PROTO = ((DropDownList)li.FindControl("ddlTipoItemGrd")).SelectedValue;
                    tbs436.CO_SITU_ITEM_PROTO = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tbs436.CO_COL_SITUA_ITEM_PROTO = LoginAuxili.CO_COL;
                    tbs436.CO_EMP_SITUA_ITEM_PROTO = LoginAuxili.CO_EMP;
                    tbs436.IP_SITUA_ITEM_PROTO = LoginAuxili.IP_USU;
                    tbs436.DT_SITUA_ITEM_PROTO = DateTime.Now;

                    TBS436_ITEM_PROTO_CID.SaveOrUpdate(tbs436);
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
                    TBS436_ITEM_PROTO_CID tbs436 = TBS436_ITEM_PROTO_CID.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tbs436.TBS434_PROTO_CIDReference.Load();
                    tbs436.TB03_COLABORReference.Load();

                    tbs436.NO_ITEM_PROTO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tbs436.DE_ITEM_PROTO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tbs436.TP_ITEM_PROTO = ((DropDownList)li.FindControl("ddlTipoItemGrd")).SelectedValue;
                    tbs436.CO_SITU_ITEM_PROTO = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tbs436.CO_COL_SITUA_ITEM_PROTO = LoginAuxili.CO_COL;
                    tbs436.CO_EMP_SITUA_ITEM_PROTO = LoginAuxili.CO_EMP;
                    tbs436.IP_SITUA_ITEM_PROTO = LoginAuxili.IP_USU;
                    tbs436.DT_SITUA_ITEM_PROTO = DateTime.Now;

                    TBS436_ITEM_PROTO_CID.SaveOrUpdate(tbs436);
                }
            }
        }

        public void alterItemDdl(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            foreach (GridViewRow li in grdItensProto.Rows)
            {
                if (ddl.ClientID == ((DropDownList)li.FindControl("ddlTipoItemGrd")).ClientID)
                {
                    TBS436_ITEM_PROTO_CID tbs436 = TBS436_ITEM_PROTO_CID.RetornaPelaChavePrimaria(int.Parse(((HiddenField)li.FindControl("idItemProto")).Value));
                    tbs436.TBS434_PROTO_CIDReference.Load();
                    tbs436.TB03_COLABORReference.Load();

                    tbs436.NO_ITEM_PROTO = ((TextBox)li.FindControl("txtNomeItemGrd")).Text;
                    tbs436.DE_ITEM_PROTO = ((TextBox)li.FindControl("txtDescItem")).Text;
                    tbs436.TP_ITEM_PROTO = ((DropDownList)li.FindControl("ddlTipoItemGrd")).SelectedValue;
                    tbs436.CO_SITU_ITEM_PROTO = ((CheckBox)li.FindControl("chkSituaItemGrd")).Checked ? "A" : "I";
                    tbs436.CO_COL_SITUA_ITEM_PROTO = LoginAuxili.CO_COL;
                    tbs436.CO_EMP_SITUA_ITEM_PROTO = LoginAuxili.CO_EMP;
                    tbs436.IP_SITUA_ITEM_PROTO = LoginAuxili.IP_USU;
                    tbs436.DT_SITUA_ITEM_PROTO = DateTime.Now;

                    TBS436_ITEM_PROTO_CID.SaveOrUpdate(tbs436);
                }
            }
        }

        #endregion
    }

}