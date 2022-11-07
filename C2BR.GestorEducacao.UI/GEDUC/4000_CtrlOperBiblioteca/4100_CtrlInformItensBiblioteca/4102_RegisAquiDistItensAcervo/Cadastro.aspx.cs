//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: DADOS CADASTRAIS DE ACERVO BIBLIOGRÁFICO
// OBJETIVO: REGISTRO DE AQUISIÇÃO E DISBRUIÇÃO DE ITENS DE ACERVO BIBLIOGRÁFICOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4102_RegisAquiDistItensAcervo
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variáveis

        static int intCoAquisi = 0;

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
            if (IsPostBack) return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                int intRetorno;

                HabilitaDesabilitaCampos(true);

//------------> Faz a verificação do último número da aquisição e soma mais um
                int ocorTb203  = TB203_ACERVO_AQUISICAO.RetornaTodosRegistros().Count();

                if (ocorTb203 > 0)
                {
                    var resultadoMax = TB203_ACERVO_AQUISICAO.RetornaTodosRegistros().Max( a => a.CO_ACERVO_AQUISI ).ToString();
                    intCoAquisi = int.TryParse(resultadoMax, out intRetorno) ? intRetorno : 0;
                }
                else
                    intCoAquisi = 0;
                
                txtDataCad.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNumAquisicao.Text = (intCoAquisi + 1).ToString("000000");

                if (lblNumAcervAquisi.Text == "")
                    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "SetCurrentSelectedTab(1);", true);
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao) || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                HabilitaDesabilitaCampos(false);
            
            CarregaUnidade();
            CarregaUnidadeBiblioteca();
            CarregaFornecedores();
            CarregaUfs();
            CarregaAcervo();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if ((lblNumAcervAquisi.Text != "") && (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)) && (grdItensAquisi.Rows.Count < 1))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Aquisição já cadastrada.");
                return;
            }

            if ((lblNumAcervAquisi.Text != "") && (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao)) && (grdItensAquisi.Rows.Count > 0))
            {
                AuxiliPagina.RedirecionaParaPaginaSucesso("Itens da aquisição cadastrados com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                return;
            }

            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coForn = ddlFornecedor.SelectedValue != "" ? int.Parse(ddlFornecedor.SelectedValue) : 0;
            decimal decimalRetorno = 0;
            
            DateTime dataRetorno;

            TB203_ACERVO_AQUISICAO tb203 = RetornaEntidade();

            if (tb203.CO_ACERVO_AQUISI == 0)
            {
                tb203.DT_CADASTRO = DateTime.Now;
                tb203.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                tb203.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                tb203.TB41_FORNEC = TB41_FORNEC.RetornaPelaChavePrimaria(coForn);
            }

            tb203.TP_ACERVO_AQUISI = ddlTipo.SelectedValue;

//--------> TAB de CONTROLE DE AQUISIÇÃO

//--------> Informações da Nota Fiscal
            tb203.NU_NOTA_ACERVO_AQUISI = Decimal.TryParse(txtNumNota.Text, out decimalRetorno) ? decimalRetorno : 0;
            tb203.NU_NOTA_SERIE_ACERVO_AQUISI = txtSerieNota.Text;
            tb203.CO_NOTA_ESTA_ACERVO_AQUISI = ddlUF.SelectedValue;
            tb203.DT_NOTA_ACERVO_AQUISI = DateTime.TryParse(txtDataNota.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;

//--------> Informações do Empenho
            tb203.NU_EMPENHO_ACERVO_AQUISI = Decimal.TryParse(txtNumEmpenho.Text, out decimalRetorno) ? decimalRetorno : 0;
            tb203.DT_EMPENHO_ACERVO_AQUISI = DateTime.TryParse(txtDataEmpenho.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb203.NO_EMPENHO_ORGAO_ACERVO_AQUISI = txtEmpenhoOrgao.Text;

///--------> Informações do Documento de Controle
            tb203.NU_DOCTO_ACERVO_AQUISI = Decimal.TryParse(txtNumDocto.Text, out decimalRetorno) ? decimalRetorno : 0;
            tb203.DT_DOCTO_ACERVO_AQUISI = DateTime.TryParse(txtDataDocto.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb203.TP_DOCTO_ACERVO_AQUISI = ddlTipoDocto.SelectedValue;
            tb203.NO_DOCTO_ORGAO_ACERVO_AQUISI = txtDoctoOrgao.Text;

            tb203.DE_OBS_ACERVO_AQUISI = txtObsAcervo.Text;

            int intResult = TB203_ACERVO_AQUISICAO.SaveOrUpdate(tb203, true);

            if (intResult > 0)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    int intRetorno;
                    
                    AuxiliPagina.EnvioMensagemSucesso(this, "Registro de Aquisição Cadastrado com Sucesso! Cadastre os Itens da Aquisição");

                    var resultadoMax = TB203_ACERVO_AQUISICAO.RetornaTodosRegistros().Max( a => a.CO_ACERVO_AQUISI ).ToString();
                    intCoAquisi = int.TryParse(resultadoMax, out intRetorno) ? intRetorno : 0;
                    lblNumAcervAquisi.Text = ddlTipo.SelectedValue.ToString() + "|" + intCoAquisi.ToString("00000");

//----------------> Faz o redirecionamento para a a TAB de CONTROLE DE ITENS
                    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "SetCurrentSelectedTab(1);", true);                                        
                }
                else
                {
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Item editado com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    return;
                }
            }
            else
            {
                AuxiliPagina.RedirecionaParaNenhumaPagina("Não foi Possível Cadastrar a Aquisição!", RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
                return;
            }

            CurrentPadraoCadastros.CurrentEntity = tb203;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB203_ACERVO_AQUISICAO tb203 = RetornaEntidade();

            if (tb203 != null)
            {
                tb203.TB03_COLABORReference.Load();
                tb203.TB25_EMPRESAReference.Load();
                tb203.TB41_FORNECReference.Load();

                ddlFornecedor.SelectedValue = tb203.TB41_FORNEC.CO_FORN.ToString();
                ddlUnidade.SelectedValue = tb203.TB25_EMPRESA.CO_EMP.ToString();
                ddlTipo.SelectedValue = tb203.TP_ACERVO_AQUISI.ToString();
                txtNumAquisicao.Text = tb203.TP_ACERVO_AQUISI.ToString() + "|" + tb203.CO_ACERVO_AQUISI.ToString("000000");
                txtNumNota.Text = tb203.NU_NOTA_ACERVO_AQUISI.ToString();
                txtSerieNota.Text = tb203.NU_NOTA_SERIE_ACERVO_AQUISI != null ? tb203.NU_NOTA_SERIE_ACERVO_AQUISI.ToString() : "";
                ddlUF.SelectedValue = tb203.CO_NOTA_ESTA_ACERVO_AQUISI != null ? tb203.CO_NOTA_ESTA_ACERVO_AQUISI.ToString() : "";
                txtDataNota.Text = tb203.DT_NOTA_ACERVO_AQUISI != null ? tb203.DT_NOTA_ACERVO_AQUISI.Value.ToString("dd/MM/yyyy") : "";
                txtNumEmpenho.Text = tb203.NU_EMPENHO_ACERVO_AQUISI.ToString();
                txtDataEmpenho.Text = tb203.DT_EMPENHO_ACERVO_AQUISI != null ? tb203.DT_EMPENHO_ACERVO_AQUISI.Value.ToString("dd/MM/yyyy") : "";
                txtEmpenhoOrgao.Text = tb203.NO_EMPENHO_ORGAO_ACERVO_AQUISI != null ? tb203.NO_EMPENHO_ORGAO_ACERVO_AQUISI.ToString() : "";
                txtNumDocto.Text = tb203.NU_DOCTO_ACERVO_AQUISI.ToString();
                txtDataDocto.Text = tb203.DT_DOCTO_ACERVO_AQUISI != null ? tb203.DT_DOCTO_ACERVO_AQUISI.Value.ToString("dd/MM/yyyy") : "";
                ddlTipoDocto.SelectedValue = tb203.TP_DOCTO_ACERVO_AQUISI != null ? tb203.TP_DOCTO_ACERVO_AQUISI.ToString() : "";
                txtDoctoOrgao.Text = tb203.NO_DOCTO_ORGAO_ACERVO_AQUISI != null ? tb203.NO_DOCTO_ORGAO_ACERVO_AQUISI.ToString() : "";
                txtObsAcervo.Text = tb203.DE_OBS_ACERVO_AQUISI != null ? tb203.DE_OBS_ACERVO_AQUISI.ToString() : "";
                txtDataCad.Text = tb203.DT_CADASTRO.ToString("dd/MM/yyyy");

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao) || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                {
                    intCoAquisi = tb203.CO_ACERVO_AQUISI;
                    lblNumAcervAquisi.Text = ddlTipo.SelectedValue + "|" + tb203.CO_ACERVO_AQUISI.ToString("00000");
                    CarregaGrid();

                    if (grdItensAquisi.Rows.Count > 0)
	                {
                        var lstItensAcervo = (from iTb203 in TB203_ACERVO_AQUISICAO.RetornaTodosRegistros()
                                             join tb204 in TB204_ACERVO_ITENS.RetornaTodosRegistros() on iTb203.CO_ACERVO_AQUISI equals tb204.CO_ACERVO_AQUISI
                                             where iTb203.CO_ACERVO_AQUISI == intCoAquisi
                                             select new
                                             {
                                                 tb204.TB35_ACERVO.NO_ACERVO, tb204.CO_ISBN_ACER,
                                                 tb204.TB25_EMPRESA.CO_EMP, tb204.TB25_EMPRESA.NO_FANTAS_EMP,
                                                 tb204.CO_ESTADO_ACERVO_ITENS, tb204.NU_PAGINA_ACERVO_ITENS,
                                                 tb204.VL_ACERVO_ITENS, tb204.DE_LOCAL, tb204.DE_LOCAL_END1,
                                                 tb204.DE_LOCAL_END2, tb204.DE_LOCAL_END3
                                             }).FirstOrDefault();

                        if (lstItensAcervo != null)
                        {
                            txtQtdItens.Enabled = txtQtdPaginas.Enabled = false;
                            txtQtdItens.Text = grdItensAquisi.Rows.Count.ToString();
                            txtQtdPaginas.Text = lstItensAcervo.NU_PAGINA_ACERVO_ITENS != null ? lstItensAcervo.NU_PAGINA_ACERVO_ITENS.ToString() : "";
                            ddlUnidadeBiblioteca.Items.Clear();
                            ddlUnidadeBiblioteca.Items.Insert(0, new ListItem(lstItensAcervo.NO_FANTAS_EMP, lstItensAcervo.CO_EMP.ToString()));
                            ddlUnidadeBiblioteca.Enabled = false;
                            ddlAcervo.Items.Clear();
                            ddlAcervo.Items.Insert(0, new ListItem(lstItensAcervo.NO_ACERVO, lstItensAcervo.CO_ISBN_ACER.ToString()));
                            ddlAcervo.Enabled = false;
                            ddlEstadoConservacao.SelectedValue = lstItensAcervo.CO_ESTADO_ACERVO_ITENS;
                            ddlEstadoConservacao.Enabled = false;
                            txtValor.Text = lstItensAcervo.VL_ACERVO_ITENS != null ? lstItensAcervo.VL_ACERVO_ITENS.ToString() : "";
                            txtValor.Enabled = txtLocalEnd.Enabled = txtLocalEnd1.Enabled = txtLocalEnd2.Enabled = txtLocalEnd3.Enabled = false;
                            txtLocalEnd.Text = lstItensAcervo.DE_LOCAL != null ? lstItensAcervo.DE_LOCAL : "";
                            txtLocalEnd1.Text = lstItensAcervo.DE_LOCAL_END1 != null ? lstItensAcervo.DE_LOCAL_END1 : "";
                            txtLocalEnd2.Text = lstItensAcervo.DE_LOCAL_END2 != null ? lstItensAcervo.DE_LOCAL_END2 : "";
                            txtLocalEnd3.Text = lstItensAcervo.DE_LOCAL_END3 != null ? lstItensAcervo.DE_LOCAL_END3 : "";
                        }                        
	                }                    
                }
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB203_ACERVO_AQUISICAO</returns>
        private TB203_ACERVO_AQUISICAO RetornaEntidade()
        {
            TB203_ACERVO_AQUISICAO tb203 = TB203_ACERVO_AQUISICAO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb203 == null) ? new TB203_ACERVO_AQUISICAO() : tb203;
        }

        /// <summary>
        /// Método que habilita/desabilita campos informados
        /// </summary>
        /// <param name="enabled">Boolean habilita</param>
        void HabilitaDesabilitaCampos(bool enabled)
        {
            ddlFornecedor.Enabled = ddlUnidade.Enabled = ddlTipo.Enabled = btnAddItens.Enabled = enabled;
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid de Itens de Aquisição
        /// </summary>
        private void CarregaGrid()
        {
            var lstItensAcervo = from tb203 in TB203_ACERVO_AQUISICAO.RetornaTodosRegistros()
                                 join tb204 in TB204_ACERVO_ITENS.RetornaTodosRegistros() on tb203.CO_ACERVO_AQUISI equals tb204.CO_ACERVO_AQUISI
                                 where tb203.CO_ACERVO_AQUISI == intCoAquisi
                                 select new
                                 {
                                     tb204.TB35_ACERVO.NO_ACERVO, tb204.TB25_EMPRESA.NO_FANTAS_EMP, tb204.CO_ISBN_ACER, tb204.CO_ACERVO_ITENS,
                                     CO_ESTADO_ACERVO_ITENS = ((tb204.CO_ESTADO_ACERVO_ITENS == "O") ? "Ótimo" : ((tb204.CO_ESTADO_ACERVO_ITENS == "B") ? "Bom" : 
                                     ((tb204.CO_ESTADO_ACERVO_ITENS == "R") ? "Ruim" : ""))),
                                     tb204.NU_PAGINA_ACERVO_ITENS, tb204.CO_CTRL_INTERNO, tb204.VL_ACERVO_ITENS
                                 };

            if (lstItensAcervo.Count() > 0)
            {
                grdItensAquisi.PageSize = 10;
                grdItensAquisi.DataSource = lstItensAcervo;
                grdItensAquisi.DataBind();
            }
            else
            {
                grdItensAquisi.Dispose();
                grdItensAquisi.DataBind();
            }

            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "SetCurrentSelectedTab(0);", true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares da Biblioteca
        /// </summary>
        private void CarregaUnidadeBiblioteca()
        {
            ddlUnidadeBiblioteca.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                               join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                               where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                               select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidadeBiblioteca.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeBiblioteca.DataValueField = "CO_EMP";
            ddlUnidadeBiblioteca.DataBind();

            ddlUnidadeBiblioteca.SelectedValue = LoginAuxili.CO_EMP.ToString();

        }

        /// <summary>
        /// Método que carrega o dropdown de Acervos
        /// </summary>
        private void CarregaAcervo()
        {
            ddlAcervo.DataSource = (from tb35 in TB35_ACERVO.RetornaTodosRegistros()
                                    where tb35.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                    select new { tb35.NO_ACERVO, tb35.CO_ISBN_ACER }).OrderBy( a => a.NO_ACERVO );

            ddlAcervo.DataTextField = "NO_ACERVO";
            ddlAcervo.DataValueField = "CO_ISBN_ACER";
            ddlAcervo.DataBind();

            ddlAcervo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Fornecedores
        /// </summary>
        private void CarregaFornecedores()
        {
            ddlFornecedor.DataSource = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                                        where tb41.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                        select new { tb41.NO_FAN_FOR, tb41.CO_FORN }).OrderBy( f => f.NO_FAN_FOR );

            ddlFornecedor.DataTextField = "NO_FAN_FOR";
            ddlFornecedor.DataValueField = "CO_FORN";
            ddlFornecedor.DataBind();

            ddlFornecedor.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        public void CarregaUfs()
        {
            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        protected void grdItensAquisi_DataBound(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = grdItensAquisi.BottomPagerRow;

            if (gridViewRow != null)
            {
                DropDownList ddlListaPaginas = (DropDownList)gridViewRow.Cells[0].FindControl("ddlGrdPages");

                if (ddlListaPaginas != null)
                    for (int i = 0; i < grdItensAquisi.PageCount; i++)
                    {
                        int numeroPagina = i + 1;
                        ListItem lstItem = new ListItem(numeroPagina.ToString());

                        if (i == grdItensAquisi.PageIndex)
                            lstItem.Selected = true;

                        ddlListaPaginas.Items.Add(lstItem);
                    }
            }
        }

        protected void grdItensAquisi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrdItensAquisi(e.NewPageIndex);
        }

        protected void ddlGrdPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = grdItensAquisi.BottomPagerRow;

            if (gridViewRow != null)
            {
                DropDownList ddlListaPaginas = (DropDownList)gridViewRow.Cells[0].FindControl("ddlGrdPages");
                BindGrdItensAquisi(ddlListaPaginas.SelectedIndex);
            }
        }

        private void BindGrdItensAquisi(int indiceNovaPagina)
        {
            CarregaGrid();
            grdItensAquisi.PageIndex = indiceNovaPagina;
            grdItensAquisi.DataBind();
        }        

//====> Método que ao clicado executa o cadastro na tabela "TB204_ACERVO_ITENS"
        protected void imgAdd_Click(object sender, EventArgs e)
        {
            if (intCoAquisi == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Primeiro deve ser cadastrada uma Aquisição.");
                return;
            }

            if (ddlAcervo.SelectedValue == "") 
            {
                AuxiliPagina.EnvioMensagemErro(this, "Campo Acervo deve ser informado.");
                return;
            }

            if(ddlUnidadeBiblioteca.SelectedValue == "") 
            {
                AuxiliPagina.EnvioMensagemErro(this, "Campo Unidade/Biblioteca deve ser informado.");
                return;
            }

            if (txtValor.Text == "") 
            {
                AuxiliPagina.EnvioMensagemErro(this, "Campo Valor deve ser informado.");
                return;
            }

            if(txtQtdPaginas.Text == "") 
            {
                AuxiliPagina.EnvioMensagemErro(this, "Campo Quantidade de Páginas deve ser informado.");
                return;
            }

            if (txtQtdItens.Text == "") 
            {
                AuxiliPagina.EnvioMensagemErro(this, "Campo Quantidade deve ser informado.");
                return;
            }
            
            if (Page.IsValid)
            {
                decimal coAcervo = ddlAcervo.SelectedValue != "" ? decimal.Parse(ddlAcervo.SelectedValue) : 0;
                int coEmp = ddlUnidadeBiblioteca.SelectedValue != "" ? int.Parse(ddlUnidadeBiblioteca.SelectedValue) : 0;

                int inteResultadoMax;
                decimal decimalRetorno;

                var ocorTb204 = TB204_ACERVO_ITENS.RetornaTodosRegistros();

                if (ocorTb204.Count() > 0)
                    inteResultadoMax = ocorTb204.Max(r => r.CO_ACERVO_ITENS);
                else
                    inteResultadoMax = 0;

                for (int i = 0; i < int.Parse(txtQtdItens.Text); i++)
                {
                    TB204_ACERVO_ITENS tb204 = new TB204_ACERVO_ITENS();

                    inteResultadoMax = inteResultadoMax + 1;
                    tb204.TB35_ACERVO = TB35_ACERVO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO, coAcervo);
                    tb204.TB35_ACERVO.TB31_AREA_CONHECReference.Load();
                    tb204.TB35_ACERVO.TB32_CLASSIF_ACERReference.Load();

                    tb204.TB203_ACERVO_AQUISICAO = TB203_ACERVO_AQUISICAO.RetornaPelaChavePrimaria(intCoAquisi);
                    tb204.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb204.CO_ESTADO_ACERVO_ITENS = ddlEstadoConservacao.SelectedValue;
                    tb204.NU_PAGINA_ACERVO_ITENS = int.Parse(txtQtdPaginas.Text);
                    
//-----------------> Código de Barras do Iten
//-----------------> "Código da Unidade"  "Area de Conhecimento" "Classificaçao" "Código da Aquisição"
//-----------------> "9999.               99.                    99.             99999
//-----------------> Composição do Código de Barras(Código Interno) 9999.99.99.99999
                    tb204.CO_CTRL_INTERNO = tb204.TB25_EMPRESA.CO_EMP.ToString("0000") + "." + tb204.TB35_ACERVO.TB31_AREA_CONHEC.CO_AREACON.ToString("00") + "." +
                                            tb204.TB35_ACERVO.TB32_CLASSIF_ACER.CO_CLAS_ACER.ToString("00") + "." + inteResultadoMax.ToString("00000");

                    tb204.CO_ACERVO_ITENS = inteResultadoMax;

                    tb204.DE_OBS_ACERVO_ITENS = null;
                    tb204.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                    tb204.VL_ACERVO_ITENS = decimal.TryParse(txtValor.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;

//----------------> Situações do Iten - "M" Em Manutenção, "I" Inativo, "R" Reserva Técnica, "E" Emprestado, "D" Disponível
                    tb204.CO_SITU_ACERVO_ITENS = "D";
                    tb204.DT_SITU_ACERVO_ITENS = DateTime.Now;
                    tb204.DT_CADASTRO = DateTime.Now;
                    tb204.DE_LOCAL = txtLocalEnd.Text != "" ? txtLocalEnd.Text : null;
                    tb204.DE_LOCAL_END1 = txtLocalEnd1.Text != "" ? txtLocalEnd1.Text : null;
                    tb204.DE_LOCAL_END2 = txtLocalEnd2.Text != "" ? txtLocalEnd2.Text : null;
                    tb204.DE_LOCAL_END3 = txtLocalEnd3.Text != "" ? txtLocalEnd3.Text : null;

                    int result = TB204_ACERVO_ITENS.SaveOrUpdate(tb204, true);
                }

                CarregaGrid();
            }
        }              

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNumAquisicao.Text = ddlTipo.SelectedValue + "|" + (intCoAquisi + 1).ToString("000000");
        }
    }
}
