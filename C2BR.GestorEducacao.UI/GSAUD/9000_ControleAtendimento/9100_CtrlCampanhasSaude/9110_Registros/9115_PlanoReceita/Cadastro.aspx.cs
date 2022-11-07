//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: GERA GRADE ANUAL DE DISCIPLINA (MATÉRIA) DE SÉRIE/CURSO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+------------------------------------
// 17/10/2014| Maxwell Almeida            |  Criação da funcionalidade para registro de Receitas de Campanhas de Saúde
//           |                            | 
//           |                            |  

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9115_PlanoReceita
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
                CarregaCampanha(0);
                CarregaHistorico();
                CarregaFornecedores(ddlTipoPessoa.SelectedValue);
                txtDtLanct.Text = DateTime.Now.ToString();
                CarregaGrid(0);
                RecarregaCampos();
                this.grdCampSaude.Columns[0].Visible = false;
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //Efetua as devidas validações
            #region Validações

            if (string.IsNullOrEmpty(ddlCampanha.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar uma Campanha de Saúde para prosseguir");
                ddlCampanha.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlTipoLancamento.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Tipo de Lançamento");
                ddlTipoLancamento.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlHistorico.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Histórico");
                ddlHistorico.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDtLanct.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data do Lançamento");
                txtDtLanct.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlFornecedor.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Fornecedor");
                ddlFornecedor.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtValor.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o R$ Valor do Lançamento");
                txtValor.Focus();
                return;
            }

            if (decimal.Parse(txtValor.Text) == 0) // não aceita valor 0
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não pode ser lançada receita/despesa com valor 0");
                txtValor.Focus();
                return;
            }

            #endregion

            //Persiste os dados em um novo registro
            TBS346_PLANO_FINAN_CAMPAN tbs346 = new TBS346_PLANO_FINAN_CAMPAN();
            tbs346.TBS339_CAMPSAUDE = TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCampanha.SelectedValue));
            tbs346.TP_LANCA = ddlTipoLancamento.SelectedValue;
            tbs346.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(int.Parse(ddlHistorico.SelectedValue));
            tbs346.DT_LANC = DateTime.Parse(txtDtLanct.Text);
            tbs346.VL_LANC = decimal.Parse(txtValor.Text);
            tbs346.NU_DOC = (!string.IsNullOrEmpty(txtNuDocto.Text) ? txtNuDocto.Text : null);
            tbs346.NU_CONTRA = (!string.IsNullOrEmpty(txtNuContrato.Text) ? txtNuContrato.Text : null);
            tbs346.DT_DOC = (!string.IsNullOrEmpty(txtDtDocto.Text) ? DateTime.Parse(txtDtDocto.Text) : (DateTime?)null);
            tbs346.TB41_FORNEC = TB41_FORNEC.RetornaPelaChavePrimaria(int.Parse(ddlFornecedor.SelectedValue));
            tbs346.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs346.DT_CADAS = DateTime.Now;
            tbs346.CO_IP_CADAS = Request.UserHostAddress;
            TBS346_PLANO_FINAN_CAMPAN.SaveOrUpdate(tbs346, true);

            HttpContext.Current.Session.Add("CO_EMP_CR", ddlUnidade.SelectedValue);
            HttpContext.Current.Session.Add("ID_CAMPAN_CR", ddlCampanha.SelectedValue);

            AuxiliPagina.RedirecionaParaPaginaSucesso("Lançamento Realizado com Sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        /// <summary>
        /// Recarrega os campos logo depois de ter salvo
        /// </summary>
        private void RecarregaCampos()
        {
            string coEmp = "";
            string idCamp = "";
            //Verifica se não está vazio antes de usar
            if (HttpContext.Current.Session["CO_EMP_CR"] != null)
            {
                coEmp = HttpContext.Current.Session["CO_EMP_CR"].ToString();
            }

            //Verifica se não está vazio antes de usar
            if (HttpContext.Current.Session["ID_CAMPAN_CR"] != null)
            {
                idCamp = HttpContext.Current.Session["ID_CAMPAN_CR"].ToString();
            }

            //Atribui as informações cabíveis
            ddlUnidade.SelectedValue = coEmp;
            CarregaCampanha(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            ddlCampanha.SelectedValue = idCamp;

            //Limpa as variáveis de sessão usadas
            HttpContext.Current.Session.Remove("CO_EMP_CR");
            HttpContext.Current.Session.Remove("ID_CAMPAN_CR");

            //Carrega a grid de informações
            CarregaGrid(ddlCampanha.SelectedValue != "" ? int.Parse(ddlCampanha.SelectedValue) : 0);
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false, false, false);
            ddlUnidade.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega as Campanhas
        /// </summary>
        /// <param name="CO_EMP"></param>
        private void CarregaCampanha(int CO_EMP)
        {
            var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                       where (CO_EMP != 0 ? tbs339.CO_EMP_LOCAL_CAMPAN == CO_EMP : tbs339.CO_EMP_LOCAL_CAMPAN == null)
                       && tbs339.CO_SITUA_TIPO_CAMPAN == "A"
                       select new { tbs339.ID_CAMPAN, tbs339.NM_CAMPAN }).OrderBy(w => w.NM_CAMPAN).ToList();

            ddlCampanha.DataTextField = "NM_CAMPAN";
            ddlCampanha.DataValueField = "ID_CAMPAN";
            ddlCampanha.DataSource = res;
            ddlCampanha.DataBind();

            ddlCampanha.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega os tipos disponíveis para histórico de receita
        /// </summary>
        private void CarregaHistorico()
        {
            var res = (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                       where tb39.FLA_TIPO_HISTORICO == ddlTipoLancamento.SelectedValue
                       select new { tb39.DE_HISTORICO, tb39.CO_HISTORICO }).OrderBy(w => w.DE_HISTORICO).ToList();

            ddlHistorico.DataTextField = "DE_HISTORICO";
            ddlHistorico.DataValueField = "CO_HISTORICO";
            ddlHistorico.DataSource = res;
            ddlHistorico.DataBind();

            ddlHistorico.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega os fornecedores
        /// </summary>
        private void CarregaFornecedores(string TipoPessoaFoJ)
        {
            var res = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                       where tb41.TP_FORN == TipoPessoaFoJ
                       && tb41.CO_SIT_FORN == "A"
                       select new { tb41.DE_RAZSOC_FORN, tb41.CO_FORN }).ToList().OrderBy(w => w.DE_RAZSOC_FORN);

            ddlFornecedor.DataTextField = "DE_RAZSOC_FORN";
            ddlFornecedor.DataValueField = "CO_FORN";
            ddlFornecedor.DataSource = res;
            ddlFornecedor.DataBind();

            ddlFornecedor.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega a Grid de Histórico
        /// </summary>
        private void CarregaGrid(int ID_CAMPAN)
        {
            var res = (from tbs346 in TBS346_PLANO_FINAN_CAMPAN.RetornaTodosRegistros()
                       join tb39 in TB39_HISTORICO.RetornaTodosRegistros() on tbs346.TB39_HISTORICO.CO_HISTORICO equals tb39.CO_HISTORICO
                       join tb41 in TB41_FORNEC.RetornaTodosRegistros() on tbs346.TB41_FORNEC.CO_FORN equals tb41.CO_FORN
                       where tbs346.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN
                       select new SaidaGrid
                       {
                           DATA = tbs346.DT_LANC,
                           TIPO = tbs346.TP_LANCA,
                           NO_HISTORICO = tb39.DE_HISTORICO,
                           NO_ORIGEM = tb41.DE_RAZSOC_FORN,
                           VALOR = tbs346.VL_LANC,

                           DATA_DOCTO = tbs346.DT_DOC,
                           NU_DOC = tbs346.NU_DOC,
                       }).OrderBy(w => w.DATA).ThenBy(w => w.TIPO).ThenBy(w => w.VALOR).ToList();

            grdCampSaude.DataSource = res;
            grdCampSaude.DataBind();

            CalculaValores();
            updCampanha.Update();
        }

        /// <summary>
        /// Calcula o Saldo resultado das receitas e despesas apresentadas na grid
        /// </summary>
        private void CalculaValores()
        {
            decimal saldo = 0;
            foreach (GridViewRow li in grdCampSaude.Rows)
            {
                string tipoLanc = (((HiddenField)li.Cells[0].FindControl("hidTipo")).Value);
                decimal valor = decimal.Parse((((HiddenField)li.Cells[0].FindControl("hidValor")).Value));

                //Calcula o Saldo de acordo com os tipos dos lançamentos e os valores
                saldo += (tipoLanc == "C" ? +valor : -valor);
            }

            //Tratamento que altera a cor do campo saldo total, onde quando positivo, ficará azul, quando negativo, ficará vermelho, e quando = 0, transparente.
            if (saldo < 0)
            {
                txtSaldoP.Visible = txtSaldo.Visible = false;
                txtSaldoN.Visible = true;
            }
            else if (saldo > 0)
            {
                txtSaldoN.Visible = txtSaldo.Visible = false;
                txtSaldoP.Visible = true;
            }
            else if (saldo == 0)
            {
                txtSaldoN.Visible = txtSaldoP.Visible = false;
                txtSaldo.Visible = true;
            }

            txtSaldo.Text = txtSaldoN.Text = txtSaldoP.Text = saldo.ToString("N2");
        }

        public class SaidaGrid
        {
            public DateTime DATA { get; set; }
            public string DATA_V
            {
                get
                {
                    return this.DATA.ToString("dd/MM/yy");
                }
            }
            public string TIPO { get; set; }
            public string TIPO_V
            {
                get
                {
                    string s = "";
                    switch (this.TIPO)
                    {
                        case "C":
                            s = "Receita";
                            break;
                        case "D":
                            s = "Despesa";
                            break;
                    }
                    return s;
                }
            }
            public string NO_HISTORICO { get; set; }
            public string NO_ORIGEM { get; set; }
            public decimal VALOR { get; set; }
            public string VALOR_V
            {
                get
                {
                    return (this.TIPO == "C" ? this.VALOR.ToString("N2") : "-" + this.VALOR.ToString("N2"));
                }
            }
            public bool SW_VALOR_POSITIVO
            {
                get
                {
                    if (this.TIPO == "C")
                        return true;
                    else
                        return false;
                }
            }
            public bool SW_VALOR_NEGATIVO
            {
                get
                {
                    if (this.TIPO == "C")
                        return false;
                    else
                        return true;
                }
            }

            public DateTime? DATA_DOCTO { get; set; }
            public string DATA_DOCTO_V
            {
                get
                {
                    return (this.DATA_DOCTO.HasValue ? this.DATA_DOCTO.Value.ToString("dd/MM/yy") : " - ");
                }
            }
            public string NU_DOC { get; set; }
            public string NU_DOC_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.NU_DOC) ? this.NU_DOC : " - ");
                }
            }

        }

        #endregion

        #region Funcoes de Campo

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCampanha((ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0));

            //Limpa a grid
            grdCampSaude.DataSource = null;
            grdCampSaude.DataBind();

            //limpa o saldo
            txtSaldo.Text = txtSaldoN.Text = txtSaldoP.Text = "";
                
            //Normaliza a visualização do saldo
            txtSaldo.Visible = true;
            txtSaldoN.Visible = txtSaldoP.Visible = false;
        }

        protected void ddlTipoLancamento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaHistorico();
        }

        protected void ddlCampanha_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid(ddlCampanha.SelectedValue != "" ? int.Parse(ddlCampanha.SelectedValue) : 0);
        }

        protected void imgPesq_OnClick(object sender, EventArgs e)
        {
            //Faz a pesquisa pelo CPF quando for o caso 
            if (ddlTipoPessoa.SelectedValue == "F")
            {
                //Verifica se não está vazio
                if (!string.IsNullOrEmpty(txtCPF.Text))
                {
                    string cpf = txtCPF.Text.Replace(".", "").Replace("-", "").Trim();
                    var res = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                               where tb41.CO_CPFCGC_FORN == cpf
                               select new { tb41.CO_FORN }).FirstOrDefault();

                    //Verifca se existe e seleciona o correspondente no devido campo
                    if (res != null)
                    {
                        ddlFornecedor.SelectedValue = res.CO_FORN.ToString();
                    }
                    else
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Fornecedor não encontrado com este CPF");
                }
                else
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPF à ser pesquisado");
            }
                //Faz por CNPJ quando for o caso
            else
            {
                //Verifica se não está vazio
                if (!string.IsNullOrEmpty(txtCPNJ.Text))
                {
                    string cnpj = txtCPNJ.Text.Replace(".", "").Replace("-", "").Replace("/", "");
                    var res = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                               where tb41.CO_CPFCGC_FORN == cnpj
                               select new { tb41.CO_FORN }).FirstOrDefault();

                    //Verifca se existe e seleciona o correspondente no devido campo
                    if (res != null)
                    {
                        ddlFornecedor.SelectedValue = res.CO_FORN.ToString();
                    }
                    else
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Fornecedor não encontrado com este CNPJ");
                }
                else
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPNJ à ser pesquisado");
            }

        }

        protected void ddlTipoPessoa_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPessoa.SelectedValue == "J")
            {
                liCNPJ.Visible = true;
                liCPF.Visible = false;
            }
            else
            {
                liCNPJ.Visible = false;
                liCPF.Visible = true;
            }
            CarregaFornecedores(ddlTipoPessoa.SelectedValue);
        }

        #endregion
    }
}