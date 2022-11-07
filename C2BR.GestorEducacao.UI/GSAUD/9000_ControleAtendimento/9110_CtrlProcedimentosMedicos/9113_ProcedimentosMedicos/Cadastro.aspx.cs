//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: GERA GRADE ANUAL DE DISCIPLINA (MATÉRIA) DE SÉRIE/CURSO
// DATA DE CRIAÇÃO: 
//---------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//---------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR           | DESCRIÇÃO RESUMIDA
// ----------+--------------------------------+------------------------------------
// 27/10/2014| Maxwell Almeida                |  Criação da funcionalidade para Cadastro de Procedimentos Médicos
// 14/07/2016| Tayguara Acioli  TA.14/07/2016 |  Adicionei a flag de procedimento para profissional e procedimento para tecnico
// 06/10/2017| Diogo Gomes                    |  Criação da funcionalidade para adicionar KITs itens de estoque aos procedimentos
//
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
using System.Web.Script.Serialization;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9113_ProcedimentosMedicos
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
                CarregaProtocolo();
                CarregaGrupos();
                CarregaTipos();
                CarregaOperadoras();
                CarregaSubGrupos();
                CarregaClassificacao();
                CarregarTipoProduto();
                //Se for operação de inserção 
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    txtVlBase.Enabled = txtVlCusto.Enabled = txtVlRestitu.Enabled = true;
                    txtDtValores.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); CarregarGrdItensEstoque(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (CurrentPadraoCadastros.BarraCadastro.AcaoSolicitadaClique == CurrentPadraoCadastros.BarraCadastro.botaoDelete)
            {
                ExcluirProcedimento();
                return;
            }

            TBS356_PROC_MEDIC_PROCE tbs356 = RetornaEntidade();

            tbs356.NM_PROC_MEDI = txtNoProcedimento.Text;
            tbs356.TBS354_PROC_MEDIC_GRUPO = TBS354_PROC_MEDIC_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue));
            tbs356.TBS355_PROC_MEDIC_SGRUP = TBS355_PROC_MEDIC_SGRUP.RetornaPelaChavePrimaria(int.Parse(ddlSubGrupo.SelectedValue));
            tbs356.CO_PROC_MEDI = txtCodProcMedic.Text;
            tbs356.CO_TIPO_PROC_MEDI = ddlTipoProcedimento.SelectedValue;
            tbs356.QT_AUXI_PROC_MEDI = (!string.IsNullOrEmpty(txtQTAux.Text) ? decimal.Parse(txtQTAux.Text) : (decimal?)null);
            tbs356.FL_PROC_PROFISSIONAL_SAUDE = chkProfissionalSaude.Checked ? "S" : "N";
            tbs356.FL_PROC_TECNICO = chkTecnico.Checked ? "S" : "N";
            tbs356.FL_USO_EXTERNO = chkUsoExterno.Checked ? "S" : "N";
            tbs356.NM_REDUZ_PROC_MEDI = txtNomeRedu.Text;
            tbs356.TB426_PROTO_ACAO = !ddlProtocolo.SelectedValue.Equals("0") ? TB426_PROTO_ACAO.RetornaPelaChavePrimaria(int.Parse(ddlProtocolo.SelectedValue)) : null;
            tbs356.DE_COMPL_PROC_MEDI = txtComplemento.Text;

            tbs356.QT_SESSO_AUTOR = (!string.IsNullOrEmpty(txtQtSecaoAutorizada.Text) ? int.Parse(txtQtSecaoAutorizada.Text) : (int?)null);

            string Classificacao = null;
            foreach (ListItem listb in lstClassificacao.Items)
            {

                if (listb.Selected == true)
                {
                    Classificacao += listb.Value + ";";

                }
            }
            tbs356.CO_CLASS_FUNCI = Classificacao;
            //---------------------------------------------------------------------------------------------------------------------------------------
            tbs356.QT_ANES_PROC_MEDI = (!string.IsNullOrEmpty(txtQTAnest.Text) ? decimal.Parse(txtQTAnest.Text) : (decimal?)null);
            tbs356.DE_OBSE_PROC_MEDI = (!string.IsNullOrEmpty(txtObservacao.Text) ? txtObservacao.Text : null);
            tbs356.FL_AUTO_PROC_MEDI = (chkRequerAuto.Checked ? "S" : "N");

            //Agrupadora
            if (!string.IsNullOrEmpty(ddlAgrupadora.SelectedValue))
            {
                TBS356_PROC_MEDIC_PROCE tbs356ob = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlAgrupadora.SelectedValue));
                tbs356.ID_AGRUP_PROC_MEDI_PROCE = (!string.IsNullOrEmpty(ddlAgrupadora.SelectedValue) ? int.Parse(ddlAgrupadora.SelectedValue) : (int?)null);
                tbs356.CO_OPER_AGRUP = tbs356ob.CO_OPER;
            }
            else
            {
                tbs356.ID_AGRUP_PROC_MEDI_PROCE = (int?)null;
                tbs356.CO_OPER_AGRUP = null;
            }

            //Operadora
            tbs356.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper.SelectedValue)) : null);
            //tbs356.CO_OPER = (!string.IsNullOrEmpty(txtCodOper.Text) ? txtCodOper.Text : null);

            //Salva essas informações apenas quando a situação tiver sido alterada
            if (hidCoSitua.Value != ddlSituacao.SelectedValue)
            {
                tbs356.CO_COL_SITU_PROC_MEDIC = LoginAuxili.CO_COL;
                tbs356.CO_SITU_PROC_MEDI = ddlSituacao.SelectedValue;
                tbs356.DT_SITU_PROC_MEDI = DateTime.Now;
            }

            //Salva essas informações apenas quando for cadastro novo
            switch (tbs356.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs356.DT_CADAS_PROC = DateTime.Now;
                    tbs356.CO_COL_CADAS_PROC = LoginAuxili.CO_COL;
                    tbs356.CO_EMP_CADAS_PROC = LoginAuxili.CO_EMP;
                    tbs356.IP_CADAS_PROC = Request.UserHostAddress;
                    break;
            }

            //Se for operação de inserção, salva as informações de valores inseridas.
            #region Salva Valores
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                //Na operação de inserção, é preciso informar os valores
                if (string.IsNullOrEmpty(txtVlCusto.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Ao inserir um novo Procedimento, é preciso informar o Valor de Custo correspondente.");
                    return;
                }

                if (string.IsNullOrEmpty(txtVlBase.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Ao inserir um novo Procedimento, é preciso informar o Valor Base correspondente.");
                    return;
                }

                //Persiste as informações de valores
                TBS353_VALOR_PROC_MEDIC_PROCE tbs353 = new TBS353_VALOR_PROC_MEDIC_PROCE();
                tbs353.TBS356_PROC_MEDIC_PROCE = tbs356;
                tbs353.VL_CUSTO = decimal.Parse(txtVlCusto.Text);
                tbs353.VL_BASE = decimal.Parse(txtVlBase.Text);
                tbs353.VL_RESTI = (!string.IsNullOrEmpty(txtVlRestitu.Text) ? decimal.Parse(txtVlRestitu.Text) : (decimal?)null);
                tbs353.CO_COL_LANC = LoginAuxili.CO_COL;
                tbs353.CO_EMP_LANC = LoginAuxili.CO_EMP;
                tbs353.IP_LANC = Request.UserHostAddress;
                tbs353.DT_LANC = DateTime.Now;
                tbs353.FL_STATU = "A";
                TBS353_VALOR_PROC_MEDIC_PROCE.SaveOrUpdate(tbs353, true);
            }
            #endregion

            var lst = new List<EstoqueVM>();

            lst = ObterListaEstoqueViewState("lstEstoque"); //Lista faz o controle do que vai ser salvo
            var lstEstoqueExcluir = ObterListaEstoqueViewState("lstEstoqueExcluir"); // Lista faz o controle o que deve ser excluído
            if (lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    //verifica se possui ID, se não possuir significa que é registro novo então adiciona
                    if (item.ID_PROC_MEDI_ITENS == null || item.ID_PROC_MEDI_ITENS == 0)
                    {
                        var tb96 = TB96_ESTOQUE.RetornarUmRegistro(p => p.ID_ESTOQUE == item.ID_ITEM_ESTOQUE);

                        if (tb96 != null)
                        {
                            tb96.TB90_PRODUTOReference.Load();
                            var prod = tb96.TB90_PRODUTO;
                            prod.TB124_TIPO_PRODUTOReference.Load();
                            prod.TBS457_CLASS_TERAPReference.Load();
                            prod.TB95_CATEGORIAReference.Load();

                            var tbs463 = new TBS463_PROC_MEDIC_ITENS();
                            tbs463.CO_CATEGORIA = prod.TB95_CATEGORIA != null ? prod.TB95_CATEGORIA.CO_CATEG : default(int?);
                            tbs463.CO_CLASS_TERAPEU = prod.TBS457_CLASS_TERAP != null ? prod.TBS457_CLASS_TERAP.ID_CLASS_TERAP : default(int?);
                            tbs463.CO_CLASS_TRIBUT = prod.CO_CLASSIFICACAO;
                            tbs463.CO_COL = (int)tb96.CO_COL;
                            tbs463.CO_COL_EMP = (int)tb96.CO_EMP_COL;
                            tbs463.CO_PIS_COFINS = prod.CO_PIS_COFINS;
                            tbs463.CO_REF_ESTOQUE = Convert.ToInt32(prod.CO_REFE_PROD);
                            tbs463.CO_TIPO_PRODUTO = prod.TB124_TIPO_PRODUTO != null ? prod.TB124_TIPO_PRODUTO.CO_TIP_PROD : default(int?);
                            tbs463.CO_TIPO_PSIQUICO = prod.CO_TIPO_PSICO;
                            tbs463.CO_UNID_FAB = prod.CO_UNID_FAB;
                            tbs463.DT_CADAS = DateTime.Now;
                            tbs463.DE_OBSER = item.DE_OBSER;
                            tbs463.FL_ITEM_OBRIGA = item.Obrigatorio;
                            tbs463.TB96_ESTOQUE = tb96;
                            tbs463.IP_CADAS = Request.UserHostAddress;
                            tbs463.PRINC_ATIVO = prod.NO_PRINCIPIO_ATIVO;
                            tbs463.QTDE_BASIC = item.QTDE_BASE;
                            tbs463.QTDE_MAX = item.QTDE_MAX;
                            tbs463.QTDE_MIN = item.QTDE_MINIMA;
                            tbs463.VL_CUSTO = prod.VL_CUSTO;
                            tbs463.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs463.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs463.CO_SITUA = Extensoes.ABERTO;
                            tbs463.DT_SITUA = DateTime.Now;
                            tbs463.CO_COL_SITUA = LoginAuxili.CO_COL;
                            tbs463.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                            tbs463.NO_UNID_USO = item.UNIDADE_USO;
                            tbs463.CO_UNID_USO = item.COD_UNIDADE_USO;
                            //tbs463.VL_DESCONTO = prod.desc;
                            tbs463.VL_VENDA = prod.VL_VENDA;
                            tbs463.NO_PROD = prod.NO_PROD;
                            tbs463.CO_PROD = prod.CO_PROD;

                            tbs356.TBS463_PROC_MEDIC_ITENS.Add(tbs463);
                        }
                    }
                }
            }

            //exclui os itens que estão armazenados no ViewState
            if (lstEstoqueExcluir.Count > 0)
            {
                foreach (var ex in lstEstoqueExcluir)
                {
                    if (ex.ID_PROC_MEDI_ITENS != null && ex.ID_PROC_MEDI_ITENS > 0)
                    {
                        var tbs463 = TBS463_PROC_MEDIC_ITENS.RetornaPelaChavePrimaria((int)ex.ID_PROC_MEDI_ITENS);
                        if (tbs356 != null)
                        {
                            tbs463.CO_SITUA = Extensoes.CANCELADO; //exclusão lógica
                        }
                    }
                }
            }

            //CurrentPadraoCadastros.CurrentEntity = tbs356;
            TBS356_PROC_MEDIC_PROCE.SaveOrUpdate(tbs356, true);

            AuxiliPagina.RedirecionaParaPaginaSucesso("Alterações salvas com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        private void ExcluirProcedimento()
        {
            try
            {
                TBS356_PROC_MEDIC_PROCE tbs356 = RetornaEntidade();

                if (tbs356 != null && tbs356.ID_PROC_MEDI_PROCE != 0)
                {
                    foreach (var tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros().Where(v => v.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE))
                        TBS353_VALOR_PROC_MEDIC_PROCE.Delete(tbs353, true);

                    foreach (var tbs362 in TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros().Where(a => a.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE))
                        TBS362_ASSOC_PLANO_PROCE.Delete(tbs362, true);

                    TBS356_PROC_MEDIC_PROCE.Delete(tbs356, true);
                }
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao tentar concluir a ação, pode ser que este procedimento já tenha itens associados a ele e não será possivel excluir! Erro: " + e.Message);
            }
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações
        /// </summary>
        private void CarregaFormulario()
        {
            TBS356_PROC_MEDIC_PROCE tbs356 = RetornaEntidade();

            if (tbs356 != null)
            {
                tbs356.TBS354_PROC_MEDIC_GRUPOReference.Load();
                tbs356.TBS355_PROC_MEDIC_SGRUPReference.Load();
                tbs356.TB250_OPERAReference.Load();
                tbs356.TB426_PROTO_ACAOReference.Load();

                txtNomeRedu.Text = tbs356.NM_REDUZ_PROC_MEDI;
                txtNoProcedimento.Text = tbs356.NM_PROC_MEDI;
                txtCodProcMedic.Text = tbs356.CO_PROC_MEDI;
                txtComplemento.Text = tbs356.DE_COMPL_PROC_MEDI;
                CarregaGrupos();
                ddlGrupo.SelectedValue = tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO.ToString();
                CarregaSubGrupos();
                ddlSubGrupo.SelectedValue = tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP.ToString();



                //29/05/2015-----------------------------------------------------------------------------------------------------------------------------
                CarregaClassificacao();
                string Classificacao = null;
                Classificacao = tbs356.CO_CLASS_FUNCI;
                if (!string.IsNullOrEmpty(Classificacao))
                {
                    foreach (ListItem listb in lstClassificacao.Items)
                    {

                        if (Classificacao.Contains(listb.Value))
                        {
                            listb.Selected = true;

                        }
                    }
                }
                //---------------------------------------------------------------------------------------------------------------------------------------

                //txtCodOper.Text = tbs356.CO_OPER;
                if (tbs356.TB250_OPERA != null)
                {
                    ddlOper.SelectedValue = tbs356.TB250_OPERA.ID_OPER.ToString();
                    CarregaProcedimentosPadroesInstituicao(tbs356.TB250_OPERA.ID_OPER);
                    ddlAgrupadora.SelectedValue = tbs356.ID_AGRUP_PROC_MEDI_PROCE.ToString();
                }

                ddlTipoProcedimento.SelectedValue = tbs356.CO_TIPO_PROC_MEDI;
                chkRequerAuto.Checked = (tbs356.FL_AUTO_PROC_MEDI == "S" ? true : false);
                txtQTAux.Text = tbs356.QT_AUXI_PROC_MEDI.ToString();
                txtQTAnest.Text = tbs356.QT_ANES_PROC_MEDI.ToString();
                txtQtSecaoAutorizada.Text = tbs356.QT_SESSO_AUTOR.ToString();
                txtObservacao.Text = tbs356.DE_OBSE_PROC_MEDI;
                hidCoSitua.Value = ddlSituacao.SelectedValue = tbs356.CO_SITU_PROC_MEDI;
                chkTecnico.Checked = tbs356.FL_PROC_TECNICO == "S" ? true : false;
                chkProfissionalSaude.Checked = tbs356.FL_PROC_PROFISSIONAL_SAUDE == "S" ? true : false;
                chkUsoExterno.Checked = tbs356.FL_USO_EXTERNO == "S" ? true : false;

                //Carrega os valores atuais
                var res = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaPeloProcedimento(tbs356.ID_PROC_MEDI_PROCE);
                if (res != null)
                {
                    txtVlCusto.Text = res.VL_CUSTO.ToString("N2");
                    txtVlBase.Text = res.VL_BASE.ToString("N2");
                    txtVlRestitu.Text = (res.VL_RESTI.HasValue ? res.VL_RESTI.Value.ToString("N2") : "");
                    txtDtValores.Text = res.DT_LANC.ToString("dd/MM/yyyy");
                    txtVlRestitu.Enabled = txtVlCusto.Enabled = txtVlBase.Enabled = false;
                }
                ddlProtocolo.SelectedValue = tbs356.TB426_PROTO_ACAO != null ? tbs356.TB426_PROTO_ACAO.ID_PROTO_ACAO.ToString() : "0";
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS339_CAMPSAUDE</returns>
        private TBS356_PROC_MEDIC_PROCE RetornaEntidade()
        {
            TBS356_PROC_MEDIC_PROCE tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs356 == null) ? new TBS356_PROC_MEDIC_PROCE() : tbs356;
        }

        /// <summary>
        /// Carrega os Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupo, false);
        }

        /// <summary>
        /// Carrega so SubGrupos
        /// </summary>
        private void CarregaSubGrupos()
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, coGrupo, false);
        }

        /// <summary>
        /// Carrega as Operadoras de Planos de Saúde
        /// </summary>
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOper, false, false);
            ddlOper.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega Protocolos
        /// </summary>
        private void CarregaProtocolo()
        {
            var res = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                       where tb426.FL_SITUA.Equals("A")
                       && tb426.TP_PROTO_ACAO.Equals("PRO")
                       select new { tb426.NO_PROTO_ACAO, tb426.ID_PROTO_ACAO, tb426.CO_SIGLA_PROTO_ACAO });

            //Trata para mostrar a concatenação caso tenha sido parametrizado dessa maneira
            ddlProtocolo.DataTextField = "CO_SIGLA_PROTO_ACAO";
            ddlProtocolo.DataValueField = "ID_PROTO_ACAO";
            ddlProtocolo.DataSource = res;
            ddlProtocolo.DataBind();
            ddlProtocolo.Items.Insert(0, new ListItem("Nenhum", "0"));

        }

        /// <summary>
        /// Carrega os tipos de procedimentos
        /// </summary>
        private void CarregaTipos()
        {
            AuxiliCarregamentos.CarregaTiposProcedimentosMedicos(ddlTipoProcedimento, false);
        }

        /// <summary>
        /// Carrega  Classificação
        /// </summary>
        private void CarregaClassificacao()
        {
            lstClassificacao.Items.Clear();
            lstClassificacao.Items.Insert(0, new ListItem("Outros", "O"));
            lstClassificacao.Items.Insert(0, new ListItem("Terapeuta Ocupacional", "T"));
            lstClassificacao.Items.Insert(0, new ListItem("Nutricionista", "N"));
            lstClassificacao.Items.Insert(0, new ListItem("Odontólogo(a)", "D"));
            lstClassificacao.Items.Insert(0, new ListItem("Psicólogo", "P"));
            lstClassificacao.Items.Insert(0, new ListItem("Médico(a)", "M"));
            lstClassificacao.Items.Insert(0, new ListItem("Fonoaudiólogo(a)", "F"));
            lstClassificacao.Items.Insert(0, new ListItem("Fisioterapeuta", "I"));
            lstClassificacao.Items.Insert(0, new ListItem("Esteticista", "S"));
            lstClassificacao.Items.Insert(0, new ListItem("Enfermeiro(a)", "E"));
            //lstClassificacao
            //AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassificacao, false);
        }
        /// <summary>
        /// Carrega os procedimentos padrões da instituição
        /// </summary>
        private void CarregaProcedimentosPadroesInstituicao(int? operadora)
        {
            ddlAgrupadora.Items.Clear();

            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.TB250_OPERA != null && operadora.HasValue ? tbs356.TB250_OPERA.ID_OPER == operadora.Value : 0 == 0
                       select new { tbs356.ID_PROC_MEDI_PROCE, tbs356.NM_PROC_MEDI }).OrderBy(w => w.NM_PROC_MEDI).ToList();

            if (res != null)
            {
                ddlAgrupadora.DataTextField = "NM_PROC_MEDI";
                ddlAgrupadora.DataValueField = "ID_PROC_MEDI_PROCE";
                ddlAgrupadora.DataSource = res;
                ddlAgrupadora.DataBind();
            }
            ddlAgrupadora.Items.Insert(0, new ListItem("", ""));
        }

        private void AbrirModalKitItens()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbrirModalKitItens();",
                true
            );
        }

        private void CarregarTipoProduto()
        {
            var res = TB124_TIPO_PRODUTO.RetornaTodosRegistros();

            if (res != null)
            {
                ddlTipoItem.DataTextField = "DE_TIP_PROD";
                ddlTipoItem.DataValueField = "CO_TIP_PROD";
                ddlTipoItem.DataSource = res;
                ddlTipoItem.DataBind();

            }

            ddlTipoItem.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarGrupoItens()
        {

            var tipoProduto = ddlTipoItem.SelectedValue;
            var codTipoProduto = 0;
            var lstGrupo = new List<TB260_GRUPO>();

            //limpa os ddls
            ddlGrupoItens.Items.Clear();
            ddlGrupoItens.DataBind();
            CarregarSubgrupoItens(0);


            if (!string.IsNullOrEmpty(tipoProduto) && tipoProduto != "0")
            {
                int.TryParse(tipoProduto, out codTipoProduto);
                var res = TB90_PRODUTO.RetornaTodosRegistros().Where(p => p.TB124_TIPO_PRODUTO.CO_TIP_PROD == codTipoProduto && p.TB260_GRUPO != null).Select(p => p.TB260_GRUPO);

                var resAgrupado = res.GroupBy(p => p.ID_GRUPO).ToList();

                foreach (var item in resAgrupado)
                {
                    lstGrupo.Add(item.FirstOrDefault());
                }

                ddlGrupoItens.DataTextField = "NOM_GRUPO";
                ddlGrupoItens.DataValueField = "ID_GRUPO";
                ddlGrupoItens.DataSource = lstGrupo.OrderBy(p => p.NOM_GRUPO);
                ddlGrupoItens.DataBind();

                CarregarSubgrupoItens(codTipoProduto);

            }
            if (ddlGrupoItens.Items.FindByValue("") == null)
            {
                ddlGrupoItens.Items.Insert(0, new ListItem("Selecione", ""));
            }

        }

        private void CarregarSubgrupoItens(int idGrupo)
        {
            //limpa o campo
            ddlSubGrupoItens.Items.Clear();
            ddlSubGrupoItens.DataBind();

            if (idGrupo > 0)
            {
                var res = TB261_SUBGRUPO.RetornaTodosRegistros().Where(p => p.TB260_GRUPO.ID_GRUPO == idGrupo).ToList();

                if (res.Count > 0)
                {
                    ddlSubGrupoItens.DataTextField = "NOM_SUBGRUPO";
                    ddlSubGrupoItens.DataValueField = "ID_SUBGRUPO";
                    ddlSubGrupoItens.DataSource = res;
                    ddlSubGrupoItens.DataBind();
                }

            }

            if (ddlSubGrupoItens.Items.FindByValue("") == null)
            {
                ddlSubGrupoItens.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }


        /// <summary>
        /// Obtem o produtos pelo filtros de codigo do produto, grupo e subgrupo
        /// </summary>
        private List<TB90_PRODUTO> ObterProdutosFiltrados(int codTipoProduto, int codItemGrupo, int codSubItenGrupo)
        {

            bool filtro = false;

            if (codTipoProduto <= 0)
            {
                return new List<TB90_PRODUTO>();
            }

            var res = TB90_PRODUTO.RetornarRegistros(p => p.TB124_TIPO_PRODUTO != null && p.TB124_TIPO_PRODUTO.CO_TIP_PROD == codTipoProduto);

            if (res.Count() > 0)
            {
                filtro = true;
            }

            if (codItemGrupo > 0)
            {
                res = res.Where(p => p.TB260_GRUPO != null && p.TB260_GRUPO.ID_GRUPO== codItemGrupo);
                filtro = true;
            }

            if (codSubItenGrupo > 0)
            {
                res = res.Where(p => p.CO_SUBGRP_ITEM == codSubItenGrupo);
                filtro = true;
            }

            if (!filtro)
            {
                return new List<TB90_PRODUTO>();
            }

            return res.ToList();
        }

        private void CarregarUnidadeUso()
        {

            var res = TB89_UNIDADES.RetornaTodosRegistros();

            foreach (GridViewRow linha in grdPesqEstoque.Rows)
            {

                var ddl = ((DropDownList)linha.Cells[3].FindControl("ddlUnidadeUso"));

                ddl.DataTextField = "NO_UNID_ITEM";
                ddl.DataValueField = "CO_UNID_ITEM";
                ddl.DataSource = res;
                ddl.DataBind();

            }

        }

        private void LimparGrdItensEstoque()
        {

            grdItensEstoque.DataSource = null;
            grdItensEstoque.DataBind();

        }

        private void DesmarcaTodosGrdPesqEstoque()
        {
            foreach (GridViewRow linha in grdPesqEstoque.Rows)
            {
                ((CheckBox)linha.Cells[0].FindControl("chkItensEstoque")).Checked = false;

                var nomeItem = linha.Cells[1].Text;
                var nomeUnidadePadrao = linha.Cells[2].Text;

                var codUnidadeUso = ((DropDownList)linha.Cells[3].FindControl("ddlUnidadeUso"));
                var qtdeBasica = ((TextBox)linha.Cells[3].FindControl("txtQtdeBasica")).Text;
                var qtdeMinima = ((TextBox)linha.Cells[4].FindControl("txtQtdeMinima")).Text;
                var qtdeMaxima = ((TextBox)linha.Cells[5].FindControl("txtQtdeMaxima")).Text;

                nomeItem = string.Empty;
                nomeUnidadePadrao = string.Empty;
                qtdeBasica = string.Empty;
                qtdeMinima = string.Empty;
                qtdeMaxima = string.Empty;
            }
        }

        private void CarregarGrdItensEstoque()
        {
            var lstEstoqueVM = new List<EstoqueVM>();

            var idProc = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            var itens = TBS463_PROC_MEDIC_ITENS.RetornarRegistros(p => p.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProc && p.CO_SITUA == Extensoes.ABERTO);

            foreach (var item in itens)
            {
                item.TB96_ESTOQUEReference.Load();


                lstEstoqueVM.Add(new EstoqueVM
                {
                    //CO_UNID_ITEM = item.TB96_ESTOQUE.TB90_PRODUTO.TB89_UNIDADES.CO_UNID_ITEM,
                    COD_UNIDADE_USO = item.CO_UNID_USO.GetValueOrDefault(),
                    DE_OBRIGATORIO = item.FL_ITEM_OBRIGA ? "Sim" : "Não",
                    DE_OBSER = item.DE_OBSER,
                    ID_ITEM_ESTOQUE = item.TB96_ESTOQUE.ID_ESTOQUE,
                    ID_PROC_MEDI_ITENS = item.ID_PROC_MEDI_ITENS,
                    NOME_ITEM = item.NO_PROD, // TB96_ESTOQUE.TB90_PRODUTO.NO_PROD,
                    QTDE_BASE = VerificaValor((decimal)item.QTDE_BASIC),
                    QTDE_MAX = VerificaValor((decimal)item.QTDE_MAX),
                    QTDE_MINIMA = VerificaValor((decimal)item.QTDE_MIN),
                    //UNIDADE_PADRAO = item.TB96_ESTOQUE.TB90_PRODUTO.TB89_UNIDADES.CO_UNID_ITEM
                    UNIDADE_USO = item.NO_UNID_USO
                });
            }

            ViewState["lstEstoque"] = string.Empty;

            if (lstEstoqueVM.Count > 0)
            {
                var lstJS = new JavaScriptSerializer().Serialize(lstEstoqueVM);

                ViewState["lstEstoque"] = lstJS;
            }


            grdItensEstoque.DataSource = lstEstoqueVM;
            grdItensEstoque.DataBind();
        }

        private List<EstoqueVM> ObterItensEstoque()
        {

            var lst = new List<EstoqueVM>();
            foreach (GridViewRow linha in grdItensEstoque.Rows)
            {

                var idItemEstoque = ((HiddenField)linha.Cells[0].FindControl("hidIdItemEstoque")).Value;
                //var unidadePadrao = ((HiddenField)linha.Cells[0].FindControl("B")).Value;

                var nomeItem = linha.Cells[1].Text;
                var nomeUnidadePadrao = linha.Cells[2].Text;
                var nomeUnidadeUso = linha.Cells[3].Text;
                var codUnidadeUso = ((HiddenField)linha.Cells[0].FindControl("hidCodUnidadeUso")).Value;
                var qtdeBasica = linha.Cells[4].Text;
                var qtdeMinima = linha.Cells[5].Text;
                var qtdeMaxima = linha.Cells[6].Text;
                var ddlObrigatorio = linha.Cells[7].Text;
                var obs = linha.Cells[8].Text;


                int idItem = 0;
                //int codUnidPadrao = 0;
                int codUnidUso = 0;
                int quantidadeBasica = 0;
                int quantidadeMinima = 0;
                int quantidadeMaxima = 0;

                int.TryParse(idItemEstoque, out idItem);
                //int.TryParse(unidadePadrao, out codUnidPadrao);
                int.TryParse(codUnidadeUso, out codUnidUso);
                int.TryParse(qtdeBasica, out quantidadeBasica);
                int.TryParse(qtdeMinima, out quantidadeMinima);
                int.TryParse(qtdeMaxima, out quantidadeMaxima);

                lst.Add(new EstoqueVM
                {
                    ID_ITEM_ESTOQUE = idItem,
                    NOME_ITEM = nomeItem,
                    COD_UNIDADE_USO = codUnidUso,
                    //UNIDADE_PADRAO = codUnidPadrao,
                    QTDE_BASE = quantidadeBasica,
                    QTDE_MINIMA = quantidadeMinima,
                    QTDE_MAX = quantidadeMaxima,
                    Obrigatorio = bool.Parse(ddlObrigatorio),
                    Observacao = obs
                });
            }

            return lst;
        }

        /// <summary>
        /// Obtem itens que estão armazenados no ViewState em formato Json
        /// </summary>
        private List<EstoqueVM> ObterListaEstoqueViewState(string nome)
        {

            var lst = new List<EstoqueVM>();

            var lstEstoque = ViewState[nome] as string;

            if (!string.IsNullOrEmpty(lstEstoque))
            {
                lst = new JavaScriptSerializer().Deserialize<List<EstoqueVM>>(lstEstoque);
            }

            return lst;
        }

        /// <summary>
        /// verifica se o que está sendo passado possui valores nas casas decimais, para exibir no grid com ou sem casas decimais
        /// </summary>
        private decimal VerificaValor(decimal valor)
        {
            decimal original = valor;

            int valorBase = 1000;
            int result = (int)((valor - (int)valor) * valorBase);

            if (result == 0)
            {
                return (int)valor;
            }

            return Convert.ToDecimal(valor.ToString("F"));
        }

        /// <summary>
        /// Verifica se número é inteiro ou decimal
        /// </summary>
        /// <returns></returns>
        private bool VerificarNumero(string numero)
        {

            int test = 0;
            decimal test2 = 0;

            return (int.TryParse(numero, out test) || decimal.TryParse(numero, out test2));

        }

        /// <summary>
        /// Exibe um alert com uma mensagem
        /// </summary>
        /// <param name="msg">mensagem para exibição no alert</param>
        private void MsgValidação(string msg) {

            ScriptManager.RegisterStartupScript(
                    this.Page,
                    this.GetType(),
                    "Acao",
                    "alert('"+ msg +"')",
                    true
                );
        }

       
        private bool ValidarGrdPesqEstoque() {

            var valida = true;

            var lst = ObterListaEstoqueViewState("lstEstoque");

            foreach (GridViewRow linha in grdPesqEstoque.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("chkItensEstoque")).Checked)
                {
                    var idItemEstoque = ((HiddenField)linha.Cells[0].FindControl("hidIdPesqEstoque")).Value;
                    var unidadePadrao = ((HiddenField)linha.Cells[0].FindControl("hidPesqUnidadePadrao")).Value;
                    var nomeItem = linha.Cells[1].Text;
                    var nomeUnidadePadrao = linha.Cells[2].Text;

                    var nomeUnidadeUso = ((DropDownList)linha.Cells[3].FindControl("ddlUnidadeUso")).SelectedItem.Text;
                    var codUnidadeUso = ((DropDownList)linha.Cells[3].FindControl("ddlUnidadeUso")).SelectedValue;
                    var qtdeBase = ((TextBox)linha.Cells[4].FindControl("txtQtdeBase")).Text;
                    var qtdeMinima = ((TextBox)linha.Cells[5].FindControl("txtQtdeMinima")).Text;
                    var qtdeMaxima = ((TextBox)linha.Cells[6].FindControl("txtQtdeMaxima")).Text;
                    var ddlObrigatorio = ((DropDownList)linha.Cells[7].FindControl("ddlObrigatorio")).SelectedValue;
                    var txtObs = ((TextBox)linha.Cells[8].FindControl("txtObs")).Text;

                    int idItem = 0;

                    int.TryParse(idItemEstoque, out idItem);

                    if (lst.Any(p => p.ID_ITEM_ESTOQUE == idItem))
                    {
                        MsgValidação(string.Format("Já Existe um {0} na lista de kit adicionado.", nomeItem));
                        valida = false;
                        break;
                    }

                    if (string.IsNullOrEmpty(codUnidadeUso) || codUnidadeUso == "0")
                    {
                        MsgValidação("Você deve selecionar uma unidade de uso para " + nomeItem);
                        valida = false;
                        break;
                    }

                    if (string.IsNullOrEmpty(qtdeBase))
                    {

                        MsgValidação("Você deve selecionar uma qtde básica para " + nomeItem);
                        valida = false;
                        break;
                    }

                    if (string.IsNullOrEmpty(qtdeMinima))
                    {
                        MsgValidação("Você deve selecionar uma quantidade mínima para " + nomeItem);
                        valida = false;
                        break;
                    }
                    if (string.IsNullOrEmpty(qtdeMaxima))
                    {
                        MsgValidação("Você deve selecionar uma quantidade máxima para " + nomeItem);
                        valida = false;
                        break;
                    }

                    if (!VerificarNumero(qtdeBase))
                    {
                        MsgValidação(string.Format("O valor da quantidade base do {0} deve ser um número.", nomeItem));
                        valida = false;
                        break;
                    }
                    if (!VerificarNumero(qtdeMinima))
                    {
                        MsgValidação(string.Format("O valor da quantidade mínima do {0} deve ser um número.", nomeItem));
                        valida = false;
                        break;
                    }
                    if (!VerificarNumero(qtdeMaxima))
                    {
                        MsgValidação(string.Format("O valor da quantidade máxima do {0} deve ser um número.", nomeItem));
                        valida = false;
                        break;
                    }
                }
            }

            return valida;
        }


        #endregion

        #region Funções de Campo

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupos();
        }

        protected void ddlGrupoItens_OnSelectedIntexChanged(object sender, EventArgs e)
        {
            var itemSelecionado = ddlGrupoItens.SelectedValue;
            var codGrupoItem = 0;

            if (!string.IsNullOrEmpty(itemSelecionado) && itemSelecionado != "0")
            {
                int.TryParse(itemSelecionado, out codGrupoItem);
                CarregarSubgrupoItens(codGrupoItem);
            }
        }

        protected void ddlOper_ddlOperOnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlOper.SelectedValue))
                CarregaProcedimentosPadroesInstituicao(int.Parse(ddlOper.SelectedValue));
            //else
            //    txtCodOper.Text = "";
        }
        protected void ddlTipoItem_OnSelectedIntexChanged(object sender, EventArgs e)
        {


            CarregarGrupoItens();

        }
        protected void lnkBtnAdiocionarKit_OnClick(object sender, EventArgs e)
        {
            AbrirModalKitItens();
            ddlGrupoItens.Items.Insert(0, new ListItem("Selecione", ""));
            ddlSubGrupoItens.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void imgExcItem_OnClick(object sender, EventArgs e)
        {

            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;

            var lst = ObterListaEstoqueViewState("lstEstoque");
            int id = 0;

            var lstExcluidos = ObterListaEstoqueViewState("lstEstoqueExcluir");


            if (grdItensEstoque.Rows.Count != 0)
            {
                foreach (GridViewRow l in grdItensEstoque.Rows)
                {
                    img = (ImageButton)l.FindControl("imgExcItem");
                    var hidIdItemEstoque = ((HiddenField)l.Cells[0].FindControl("hidIdItemEstoque")).Value;
                    if (img.ClientID == atual.ClientID)
                    {
                        int.TryParse(hidIdItemEstoque, out id);
                        aux = l.RowIndex;

                        break;
                    }
                }
            }

            //add os excluídos no viewstate para serem salvos posteriormente
            lstExcluidos.Add(lst.FirstOrDefault(p => p.ID_ITEM_ESTOQUE == id));

            ViewState["lstEstoqueExcluir"] = string.Empty;

            if (lstExcluidos.Count > 0)
            {
                var lstExcluirJS = new JavaScriptSerializer().Serialize(lstExcluidos);

                ViewState["lstEstoqueExcluir"] = lstExcluirJS;
            }

            //remove o item da lista que será salva posteriomente
            lst.Remove(lst.FirstOrDefault(p => p.ID_ITEM_ESTOQUE == id));

            grdItensEstoque.DataSource = lst;
            grdItensEstoque.DataBind();

            ViewState["lstEstoque"] = string.Empty;

            if (lst.Count > 0)
            {
                var lstJS = new JavaScriptSerializer().Serialize(lst);

                ViewState["lstEstoque"] = lstJS;
            }

        }

        protected void imgbPesqItens_OnClick(object sender, EventArgs e)
        {

            var tipoProduto = ddlTipoItem.SelectedValue;
            var grupoItens = ddlGrupoItens.SelectedValue;
            var subgrupoItens = ddlSubGrupoItens.SelectedValue;

            var codTipoProduto = 0;
            var codItemGrupo = 0;
            var codSubItenGrupo = 0;

            int.TryParse(tipoProduto, out codTipoProduto);
            int.TryParse(grupoItens, out codItemGrupo);
            int.TryParse(subgrupoItens, out codSubItenGrupo);

            var produtosFiltrados = ObterProdutosFiltrados(codTipoProduto, codItemGrupo, codSubItenGrupo);

            var resEstoque = TB96_ESTOQUE.RetornaTodosRegistros();
            var lstEstoque = new List<dynamic>();

            foreach (var prod in produtosFiltrados)
            {
                var itemEstoque = resEstoque.Where(o => o.TB90_PRODUTO.CO_PROD == prod.CO_PROD).Select(f => new
                {

                    ID_ITEM_ESTOQUE = f.ID_ESTOQUE,
                    NOME_ITEM = (f.TB90_PRODUTO != null) ? f.TB90_PRODUTO.NO_PROD : string.Empty,
                    UNIDADE_PADRAO = (f.TB90_PRODUTO != null && f.TB90_PRODUTO.TB89_UNIDADES != null) ? f.TB90_PRODUTO.TB89_UNIDADES.NO_UNID_ITEM : string.Empty,
                    CO_UNID_ITEM = (f.TB90_PRODUTO != null && f.TB90_PRODUTO.TB89_UNIDADES != null) ? f.TB90_PRODUTO.TB89_UNIDADES.CO_UNID_ITEM : 0,
                });

                if (itemEstoque.Count() > 0)
                {
                    lstEstoque.AddRange(itemEstoque);
                }
            }

            grdPesqEstoque.DataSource = lstEstoque;
            grdPesqEstoque.DataBind();

            CarregarUnidadeUso();
        }



        protected void lnkAddItem_OnClick(object sender, EventArgs e)
        {
            var lst = ObterListaEstoqueViewState("lstEstoque");

            if (ValidarGrdPesqEstoque())
            {
          
            foreach (GridViewRow linha in grdPesqEstoque.Rows)
            {

                if (((CheckBox)linha.Cells[0].FindControl("chkItensEstoque")).Checked)
                {
                    var idItemEstoque = ((HiddenField)linha.Cells[0].FindControl("hidIdPesqEstoque")).Value;
                    var unidadePadrao = ((HiddenField)linha.Cells[0].FindControl("hidPesqUnidadePadrao")).Value;
                    var nomeItem = linha.Cells[1].Text;
                    var nomeUnidadePadrao = linha.Cells[2].Text;

                    var nomeUnidadeUso = ((DropDownList)linha.Cells[3].FindControl("ddlUnidadeUso")).SelectedItem.Text;
                    var codUnidadeUso = ((DropDownList)linha.Cells[3].FindControl("ddlUnidadeUso")).SelectedValue;
                    var qtdeBase = ((TextBox)linha.Cells[4].FindControl("txtQtdeBase")).Text;
                    var qtdeMinima = ((TextBox)linha.Cells[5].FindControl("txtQtdeMinima")).Text;
                    var qtdeMaxima = ((TextBox)linha.Cells[6].FindControl("txtQtdeMaxima")).Text;
                    var ddlObrigatorio = ((DropDownList)linha.Cells[7].FindControl("ddlObrigatorio")).SelectedValue;
                    var txtObs = ((TextBox)linha.Cells[8].FindControl("txtObs")).Text;
                    
                    decimal vBase = 0;
                    decimal vMin = 0;
                    decimal vMax = 0;
                    decimal.TryParse(qtdeBase, out vBase);
                    decimal.TryParse(qtdeMinima, out vMin);
                    decimal.TryParse(qtdeMaxima, out vMax);

                    var estoqueVM = new EstoqueVM();
                    estoqueVM.ID_ITEM_ESTOQUE = int.Parse(idItemEstoque);
                    estoqueVM.CO_UNID_ITEM = int.Parse(unidadePadrao);
                    estoqueVM.NOME_ITEM = nomeItem;
                    estoqueVM.UNIDADE_PADRAO = nomeUnidadePadrao;
                    estoqueVM.UNIDADE_USO = nomeUnidadeUso;
                    estoqueVM.COD_UNIDADE_USO = int.Parse(codUnidadeUso);
                    estoqueVM.QTDE_BASE = VerificaValor(vBase);
                    estoqueVM.QTDE_MINIMA = VerificaValor(vMin);
                    estoqueVM.QTDE_MAX = VerificaValor(vMax);
                    estoqueVM.DE_OBRIGATORIO = ddlObrigatorio == "true" ? "Sim" : "Não";
                    estoqueVM.Obrigatorio = bool.Parse(ddlObrigatorio);
                    estoqueVM.DE_OBSER = txtObs;

                    lst.Add(estoqueVM);
                }
            }

            if (lst.Count <= 0)
            {
                return;
            }

            grdItensEstoque.DataSource = lst;
            grdItensEstoque.DataBind();

            ViewState["lstEstoque"] = string.Empty;

            if (lst.Count > 0)
            {
                var lstJS = new JavaScriptSerializer().Serialize(lst);

                ViewState["lstEstoque"] = lstJS;
            }

            }
            ///DesmarcaTodosGrdPesqEstoque();
        }

        #endregion

        class EstoqueVM
        {
            public int? ID_PROC_MEDI_ITENS { get; set; }
            public int ID_ITEM_ESTOQUE { get; set; }
            public int CO_UNID_ITEM { get; set; }
            public string NOME_ITEM { get; set; }
            public string UNIDADE_PADRAO { get; set; }
            public string UNIDADE_USO { get; set; }
            public decimal? QTDE_BASE { get; set; }
            public decimal? QTDE_MINIMA { get; set; }
            public decimal? QTDE_MAX { get; set; }
            public bool Obrigatorio { get; set; }
            public string Observacao { get; set; }
            public int COD_UNIDADE_USO { get; set; }
            public string DE_OBRIGATORIO { get; set; }
            public string DE_OBSER { get; set; }

        }
    }
}