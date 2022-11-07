//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: LANÇAMENTO DE ENTRADA E SAÍDA DE ITENS DE ESTOQUE
// OBJETIVO: REGISTRO DE MOVIMENTAÇÃO DE ESTOQUE.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//  13/02   | Samira Lira                | Acrescentar local e pesquisa de produto

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6250_LanctoEntradaSaidaItensEstoque.F6251_RegistroMovimentacaoEstoque
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
                CarregaTipoMov();
                CarregarLocal();
                txtDataMovimento.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                //------------> Se for edição, desabilita o campo txtQMov -> quantidade de produto movimentado
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                    txtQMov.Enabled = false;
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            try
            {
                int coProd = ddlProduto.SelectedValue != "" ? int.Parse(ddlProduto.SelectedValue) : 0;
                int coTipoMov = ddlTipoMovimento.SelectedValue != "" ? int.Parse(ddlTipoMovimento.SelectedValue) : 0;
                int coDepto = !string.IsNullOrEmpty(ddlLocal.SelectedValue) ? int.Parse(ddlLocal.SelectedValue) : 0;
                RegistroLog log = new RegistroLog();

                //TB91_MOV_PRODUTO tb91_aux = RetornaEntidade();
                TB91_MOV_PRODUTO tb91 = new TB91_MOV_PRODUTO();
                
                TB93_TIPO_MOVIMENTO tb93 = TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(coTipoMov);
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    tb91.TB93_TIPO_MOVIMENTO = tb93;

                    //tb91.CO_PROD = coProd;
                    //tb91.CO_EMP = LoginAuxili.CO_EMP;
                    tb91.TB90_PRODUTO = TB90_PRODUTO.RetornaPelaChavePrimaria(coProd, LoginAuxili.CO_EMP);
                    tb91.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb91.DT_MOV_PROD = DateTime.Now;
                }

                tb91.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(coDepto);
                tb91.NU_DOC_PROD = txtNDoc.Text;
                tb91.QT_MOV_PROD = decimal.Parse(txtQMov.Text);
                tb91.DE_MOV_PROD = txtDesc.Text;
                tb91.DT_ALT_REGISTRO = DateTime.Now;
                tb91.CO_COL = LoginAuxili.CO_COL;
                tb91.CO_EMP_COL = LoginAuxili.CO_EMP;

                int coFor = ddlFornec.SelectedValue != "" ? int.Parse(ddlFornec.SelectedValue) : 0;
                if (tb93.CO_SIGLA == "ERAC")
                {
                    if (coFor != 0)
                    {
                        tb91.CO_FORN = coFor;
                    }
                }

                int coEmpTrans = ddlUnidTrans.SelectedValue != "" ? int.Parse(ddlUnidTrans.SelectedValue) : 0;
                //int coDpto = ddlDepto.SelectedValue != "" ? int.Parse(ddlDepto.SelectedValue) : 0;
                if (tb93.CO_SIGLA == "STI" || tb93.CO_SIGLA == "SUAI")
                {
                    if (coEmpTrans != 0 && coDepto != 0)
                    {
                        tb91.CO_EMP_TRANS_ESTOQ = coEmpTrans;
                        //tb91.CO_DEPTO = coDpto;
                    }
                }

                if (tb93.CO_SIGLA == "STE")
                {
                    if (coEmpTrans != 0)
                    {
                        tb91.CO_EMP_TRANS_ESTOQ = coEmpTrans;
                    }
                }

                if (tb93.CO_SIGLA == "ECSE")
                {
                    tb91.FL_INVEN = chkFlInv.Checked ? "S" : "N";
                }

                string strNomeUsuario = LoginAuxili.NOME_USU_LOGADO;

                tb91.NOM_USUARIO = strNomeUsuario.ToString().Substring(0, (strNomeUsuario.Length > 30 ? 29 : strNomeUsuario.Length));

                TB96_ESTOQUE tb96 = TB96_ESTOQUE.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coProd);
                bool flagInclusao = false;

                if (tb96 == null)
                {
                    tb96 = new TB96_ESTOQUE();
                    //if (tb96.TB25_EMPRESA != null)

                    // Tentativa
                    //tb96.TB25_EMPRESA = new TB25_EMPRESA();
                    //tb96.TB25_EMPRESA.CO_EMP = LoginAuxili.CO_EMP;
                    //tb96.TB90_PRODUTO = new TB90_PRODUTO();
                    //tb96.TB90_PRODUTO.CO_PROD = coProd;
                    //tb96.CO_EMP = LoginAuxili.CO_EMP;
                    //tb96.CO_PROD = coProd;
                    
                    tb96.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    // Erro
                    //tb96.TB25_EMPRESA.CO_EMP = LoginAuxili.CO_EMP;

                    tb96.TB90_PRODUTO = TB90_PRODUTO.RetornaPelaChavePrimaria(coProd, LoginAuxili.CO_EMP);
                    // Erro
                    //tb96.TB90_PRODUTO.CO_PROD = coProd;

                    tb96.NOM_USUARIO = strNomeUsuario.ToString().Substring(0, (strNomeUsuario.Length > 30 ? 29 : strNomeUsuario.Length));
                    tb96.DT_ALT_REGISTRO = DateTime.Now;
                    tb96.CO_COL = LoginAuxili.CO_COL;
                    tb96.CO_EMP_COL = LoginAuxili.CO_EMP;
                    flagInclusao = true;
                }


                string strFlaTpMov = TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(coTipoMov).FLA_TP_MOV;

                //--------> Só pode adicionar ou remover se não for edição, já que na edição não é permitido alterar a quantidade de registros
                if (!QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    //------------> Quando for exclusão do registro ele remove do estoque a quantidade correspondente (se foi uma adição)
                    if (strFlaTpMov == "S" || QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                    {
                        decimal dcmQtdeMovto = decimal.Parse(txtQMov.Text);
                        decimal dcmQtdeTotal = tb96.QT_SALDO_EST - dcmQtdeMovto;
                        tb96.QT_SALDO_EST = dcmQtdeTotal;
                    }
                    else
                    {
                        //----------------> Faz a adição no estoque da quantidade informada
                        decimal dcmQtdeMovto = decimal.Parse(txtQMov.Text);
                        decimal dcmQtdeTotal = tb96.QT_SALDO_EST + dcmQtdeMovto;
                        tb96.QT_SALDO_EST = dcmQtdeTotal;
                    }
                }

                TB96_ESTOQUE.SaveOrUpdate(tb96, true);
                string acao = flagInclusao ? RegistroLog.ACAO_GRAVAR : RegistroLog.ACAO_EDICAO;
                log.RegistroLOG(tb96, acao);

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    TB91_MOV_PRODUTO.SaveOrUpdate(tb91, true);
                    log.RegistroLOG(tb91, RegistroLog.ACAO_GRAVAR);
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Salvo com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
                else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    CurrentPadraoCadastros.CurrentEntity = tb91;
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message); ;
            }

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB91_MOV_PRODUTO tb91 = RetornaEntidade();

            if (tb91 != null)
            {
                txtDataMovimento.Text = tb91.DT_MOV_PROD.ToString("dd/MM/yyyy");
                txtDesc.Text = tb91.DE_MOV_PROD;
                txtNDoc.Text = tb91.NU_DOC_PROD.ToString();
                txtQMov.Text = Convert.ToInt32(tb91.QT_MOV_PROD).ToString();

                int coMov = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

                var varTb91 = (from lTb91 in TB91_MOV_PRODUTO.RetornaTodosRegistros()
                               where lTb91.CO_MOV == coMov && lTb91.CO_PROD == tb91.CO_PROD
                               select lTb91).FirstOrDefault();
                varTb91.TB93_TIPO_MOVIMENTOReference.Load();
                varTb91.TB14_DEPTOReference.Load();
                varTb91.TB90_PRODUTOReference.Load();

                //------------> Faz a recuperação da quantidade do produto selecionado no Estoque
                decimal dcmQtdeSaldoEstoque = (from tb96 in TB96_ESTOQUE.RetornaTodosRegistros()
                                               where tb96.TB90_PRODUTO.CO_PROD == tb91.CO_PROD && tb96.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                               select new { tb96.QT_SALDO_EST }).FirstOrDefault().QT_SALDO_EST;

                txtQEstoque.Text = dcmQtdeSaldoEstoque.ToString();
                ddlTipoMovimento.SelectedValue = varTb91.TB93_TIPO_MOVIMENTO.CO_TIPO_MOV.ToString();

                CarregarProdutos(varTb91.TB90_PRODUTO.NO_PROD);
                ddlProduto.SelectedValue = varTb91.TB90_PRODUTO.CO_PROD.ToString();
                OcultarPesquisa(true);
                ddlLocal.SelectedValue = varTb91.TB14_DEPTO.CO_DEPTO.ToString();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB91_MOV_PRODUTO</returns>
        private TB91_MOV_PRODUTO RetornaEntidade()
        {
            TB91_MOV_PRODUTO tb91 = TB91_MOV_PRODUTO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb91 == null) ? new TB91_MOV_PRODUTO() : tb91;
        }

        protected void mostraCampos(string sigla)
        {
            liDepto.Visible = false;
            liUnidTRans.Visible = false;
            liFornec.Visible = false;

            if (sigla == "ERAC")
            {
                CarregaFornecedor();
                liFornec.Visible = true;
            }

            if (sigla == "STI" || sigla == "SUAI")
            {
                CarregaUnidadeTransf(sigla);
                liUnidTRans.Visible = true;
                CarregaDepartamento();
                liDepto.Visible = true;
            }

            if (sigla == "STE")
            {
                CarregaUnidadeTransf(sigla);
                liUnidTRans.Visible = true;
            }

            if (sigla == "ECSE")
            {
                chkFlInv.Visible = true;
                lblFlInv.Visible = true;
            }
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtProduto.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            ddlProduto.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Movimento
        /// </summary>
        private void CarregaTipoMov()
        {
            ddlTipoMovimento.DataSource = (from tb93 in TB93_TIPO_MOVIMENTO.RetornaTodosRegistros()
                                           select new { tb93.CO_TIPO_MOV, tb93.DE_TIPO_MOV });

            ddlTipoMovimento.DataTextField = "DE_TIPO_MOV";
            ddlTipoMovimento.DataValueField = "CO_TIPO_MOV";
            ddlTipoMovimento.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Produtos
        /// </summary>
        private void CarregaProduto()
        {
            ddlProduto.DataSource = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                                     where tb90.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                     select new { tb90.CO_PROD, tb90.NO_PROD_RED }).OrderBy(p => p.NO_PROD_RED);

            ddlProduto.DataTextField = "NO_PROD_RED";
            ddlProduto.DataValueField = "CO_PROD";
            ddlProduto.DataBind();

            ddlProduto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarLocal()
        {
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocal, LoginAuxili.CO_EMP, false);
            ddlLocal.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que preenche o campo txtQEstoque com a quantidade do produto informado no Estoque
        /// </summary>
        private void CarregaQtdEstoque()
        {
            int coProd = ddlProduto.SelectedValue != "" ? int.Parse(ddlProduto.SelectedValue) : 0;

            var varTb96 = TB96_ESTOQUE.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coProd);

            if (varTb96 != null)
                txtQEstoque.Text = varTb96.QT_SALDO_EST.ToString();
            else
                txtQEstoque.Text = "0";
        }

        private void CarregaUnidadeTransf(string sigla)
        {
            if (sigla == "STI" || sigla == "SUAI")
            {
                ddlUnidTrans.Items.Clear();
                ddlUnidTrans.Items.Insert(0, new ListItem(TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).NO_FANTAS_EMP, LoginAuxili.CO_EMP.ToString()));
                ddlUnidTrans.Enabled = false;
            }

            if (sigla == "STE")
            {
                ddlUnidTrans.Items.Clear();

                var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           where tb25.CO_EMP != LoginAuxili.CO_EMP
                           select new
                           {
                               tb25.NO_FANTAS_EMP,
                               tb25.CO_EMP
                           }).ToList();

                ddlUnidTrans.DataTextField = "NO_FANTAS_EMP";
                ddlUnidTrans.DataValueField = "CO_EMP";

                ddlUnidTrans.DataSource = res;
                ddlUnidTrans.DataBind();

                ddlUnidTrans.Enabled = true;
            }
        }

        private void CarregaDepartamento()
        {
            int coEmp = int.Parse(ddlUnidTrans.SelectedValue);

            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.TB25_EMPRESA.CO_EMP == coEmp
                       select new
                       {
                           tb14.CO_DEPTO,
                           tb14.NO_DEPTO
                       });

            ddlDepto.DataTextField = "NO_DEPTO";
            ddlDepto.DataValueField = "CO_DEPTO";

            ddlDepto.DataSource = res;
            ddlDepto.DataBind();
        }

        private void CarregaFornecedor()
        {
            var res = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                       select new
                       {
                           tb41.NO_FAN_FOR,
                           tb41.CO_FORN
                       });

            ddlFornec.DataTextField = "NO_FAN_FOR";
            ddlFornec.DataValueField = "CO_FORN";

            ddlFornec.DataSource = res;
            ddlFornec.DataBind();
        }
        #endregion

        protected void ddlProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaQtdEstoque();
        }

        protected void ddlTipoMovimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostraCampos(TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(int.Parse(ddlTipoMovimento.SelectedValue)).CO_SIGLA);
        }

        protected void imgbPesqProduto_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(true);

            CarregarProdutos(txtProduto.Text);
        }

        private void CarregarProdutos(string noProduto)
        {
            ddlProduto.DataSource = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                                     where tb90.NO_PROD.Contains(noProduto) && tb90.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                     select new { tb90.CO_PROD, tb90.NO_PROD_RED }).OrderBy(p => p.NO_PROD_RED);

            ddlProduto.DataTextField = "NO_PROD_RED";
            ddlProduto.DataValueField = "CO_PROD";
            ddlProduto.DataBind();

            ddlProduto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }
    }
}
