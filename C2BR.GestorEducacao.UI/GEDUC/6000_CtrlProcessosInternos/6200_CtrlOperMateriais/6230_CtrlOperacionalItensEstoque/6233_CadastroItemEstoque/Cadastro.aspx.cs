//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE ESTOQUE
// SUBMÓDULO: CONTROLE OPERACIONAL DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DE ITENS DE ESTOQUE
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
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6230_CtrlOperacionalItensEstoque.F6233_CadastroItemEstoque
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //------------> Tamanho da imagem de Produto
                ///Define altura e largura da imagem do funcionário
                upImagemProdu.ImagemLargura = 160;
                upImagemProdu.ImagemAltura = 110;

                CarregaCategoria();
                CarregaCor();
                CarregaGrupo();
                CarregaSubGrupo();
                CarregaMarca();
                CarregaTamanho();
                CarregaUnidade();
                CarregaTipoProduto();
                CarregaFornecedores();
                //CarregarClasseTerapeutica();
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()  
        {
            /*
            if (string.IsNullOrEmpty(txtNReduz.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Você deve inserir um nome reduzido");
                return;
            }
            */
            if (string.IsNullOrEmpty(txtCodRef.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Voce deve inserir um código de Referência.");
                return;
            }

            if (string.IsNullOrEmpty(ddlGrupo.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Você deve selecionar um grupo");
                return;
            }

            if (txtObservacao.Text.Count() > 300)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O campo observação nao pode possuir mais de 300 caracteres.");
                return;
            }


            TB90_PRODUTO tb90 = RetornaEntidade();

            //--------> Como o CO_EMP é campo chave, só é permitido inserí-lo quando for inclusão
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb90.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            int codImagem = upImagemProdu.GravaImagem();

            //Informações padrões
            tb90.NO_PROD = txtNProduto.Text;
            tb90.DES_PROD = txtDescProduto.Text;
            tb90.NO_PROD_RED = txtNReduz.Text;
            tb90.CO_REFE_PROD = txtCodRef.Text;  
            tb90.TB260_GRUPO = ddlGrupo.SelectedValue != "" ? TB260_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue)) : null;
            tb90.TB261_SUBGRUPO = ddlSubGrupo.SelectedValue != "" ? TB261_SUBGRUPO.RetornaPelaChavePrimaria(int.Parse(ddlSubGrupo.SelectedValue)) : null;
            tb90.TB124_TIPO_PRODUTO = ddlTipoProduto.SelectedValue != "" ? TB124_TIPO_PRODUTO.RetornaPelaChavePrimaria(int.Parse(ddlTipoProduto.SelectedValue)) : null;
            tb90.TB95_CATEGORIA = ddlCategoria.SelectedValue != "" ? TB95_CATEGORIA.RetornaPelaChavePrimaria(int.Parse(ddlCategoria.SelectedValue)) : null;
            tb90.FLA_IMPORTADO = ddlImportado.SelectedValue;

            //Salva data de cadastro somente se for o caso
            switch (tb90.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tb90.DT_CADA_PROD = DateTime.Now;
                    break;
            }

            tb90.CO_SITU_PROD = ddlSituacao.SelectedValue;
            tb90.DT_ALT_REGISTRO = DateTime.Now;
            tb90.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);

            //Características
            tb90.TB89_UNIDADES = ddlUnidade.SelectedValue != "" ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)) : null;
            tb90.QT_QX = (!string.IsNullOrEmpty(txtQtCx.Text) ? int.Parse(txtQtCx.Text) : (int?)null);
            tb90.CO_BARRAS = (!string.IsNullOrEmpty(txtCodBarr.Text) ? txtCodBarr.Text : null);
            tb90.TB93_MARCA = ddlMarca.SelectedValue != "" ? TB93_MARCA.RetornaPelaChavePrimaria(int.Parse(ddlMarca.SelectedValue)) : null;
            tb90.TB97_COR = ddlCor.SelectedValue != "" ? TB97_COR.RetornaPelaChavePrimaria(int.Parse(ddlCor.SelectedValue)) : null;
            tb90.TB98_TAMANHO = ddlTamanho.SelectedValue != "" ? TB98_TAMANHO.RetornaPelaChavePrimaria(int.Parse(ddlTamanho.SelectedValue)) : null;
            tb90.NU_PESO_PROD = txtPeso.Text != "" ? Convert.ToDecimal(txtPeso.Text) : 0;
            tb90.NU_VOL_PROD = txtVolume.Text != "" ? Convert.ToDecimal(txtVolume.Text) : 0;

            if (ddlClasseTerapeutica.Visible)
            {
                tb90.TBS457_CLASS_TERAP = !string.IsNullOrEmpty(ddlClasseTerapeutica.SelectedValue) ? TBS457_CLASS_TERAP.RetornaPelaChavePrimaria(int.Parse(ddlClasseTerapeutica.SelectedValue)) : null; 
            }

            //Quantidades
            tb90.CO_UNID_FAB = (ddlUnidFab.SelectedValue != "" ? int.Parse(ddlUnidFab.SelectedValue) : (int?)null);
            tb90.QT_UNID_FAB = (!string.IsNullOrEmpty(txtQtdeFab.Text) ? int.Parse(txtQtdeFab.Text) : (int?)null);
            tb90.CO_UNID_COMPRA = (ddlUnidComp.SelectedValue != "" ? int.Parse(ddlUnidComp.SelectedValue) : (int?)null);
            tb90.QT_UNID_COMPRA = (!string.IsNullOrEmpty(txtQtdeComp.Text) ? int.Parse(txtQtdeComp.Text) : (int?)null);
            tb90.CO_UNID_VENDA = (ddlUnidComp.SelectedValue != "" ? int.Parse(ddlUnidComp.SelectedValue) : (int?)null);
            tb90.QT_UNID_VENDA = (!string.IsNullOrEmpty(txtQtdeVend.Text) ? int.Parse(txtQtdeVend.Text) : (int?)null);

            //Segurança
            tb90.QT_SEG_MIN = (!string.IsNullOrEmpty(txtSegMin.Text) ? int.Parse(txtSegMin.Text) : (int?)null);
            tb90.QT_SEG_MAX = (!string.IsNullOrEmpty(txtSegMax.Text) ? int.Parse(txtSegMax.Text) : (int?)null);
            
            //Outras Informações
            tb90.FL_FARM_POP = (chkFarmPopul.Checked ? "S" : "N");
            tb90.CO_FABRICANTE = ddlFabricante.SelectedValue != "" ? int.Parse(ddlFabricante.SelectedValue) : (int?)null;
            tb90.NU_CO_FABRICANTE = (!string.IsNullOrEmpty(txtCodFabricante.Text) ? int.Parse(txtCodFabricante.Text) : (int?)null);
            tb90.CO_FORNECEDOR = ddlFornecedor.SelectedValue != "" ? int.Parse(ddlFornecedor.SelectedValue) : (int?)null;
            tb90.QT_DIAS_FORNECEDOR = (!string.IsNullOrEmpty(txtDiasEntrForne.Text) ? int.Parse(txtDiasEntrForne.Text) : (int?)null);
            tb90.CO_MS_ANVISA = (!string.IsNullOrEmpty(txtCodAnvisa.Text) ? txtCodAnvisa.Text : null);
            tb90.NU_NCM = (!string.IsNullOrEmpty(txtNNCM.Text) ? txtNNCM.Text : null);
            //tb90.CO_TIPO_PSICO = ddlTipoPsico.SelectedValue != "" ? int.Parse(ddlTipoPsico.SelectedValue) : (int?)null;
            tb90.NO_PRINCIPIO_ATIVO = (!string.IsNullOrEmpty(txtPrinAtiv.Text) ? txtPrinAtiv.Text : null);
            tb90.CO_COR_ALERTA = (ddlCorAlerta.SelectedValue != "" ? int.Parse(ddlCorAlerta.SelectedValue) : (int?)null);
            tb90.DE_OBSERVACAO = (!string.IsNullOrEmpty(txtObservacao.Text) ? txtObservacao.Text : null);
            tb90.FL_PSICO = ddlTipoPsico.SelectedValue;

            //Dados Tributários
            tb90.NU_POR_GRP = (!string.IsNullOrEmpty(txtPerTribuGrp.Text) ? decimal.Parse(txtPerTribuGrp.Text) : (decimal?)null);
            tb90.NU_GRP = (!string.IsNullOrEmpty(txtGrpTribu.Text) ? decimal.Parse(txtGrpTribu.Text) : (decimal?)null);
            tb90.CO_CLASSIFICACAO = ddlClassTribu.SelectedValue != "" ? int.Parse(ddlClassTribu.SelectedValue) : (int?)null;
            tb90.CO_PIS_COFINS = ddlTribPISConfins.SelectedValue != "" ? int.Parse(ddlTribPISConfins.SelectedValue) : (int?)null;

            //Dados Comerciais
            tb90.FL_MANTER_MARGEM = chkManterMargem.Checked ? "S" : "N";
            tb90.FL_MANTER_VENDA = chkManterValVenda.Checked ? "S" : "N";
            tb90.VL_CUSTO = (!string.IsNullOrEmpty(txtValor.Text) ? decimal.Parse(txtValor.Text) : (decimal?)null);
            tb90.VL_VENDA = (!string.IsNullOrEmpty(txtValVenda.Text) ? decimal.Parse(txtValVenda.Text) : (decimal?)null);
            tb90.VL_VENDA_MAX = (!string.IsNullOrEmpty(txtValVendMax.Text) ? decimal.Parse(txtValVendMax.Text) : (decimal?)null);
            tb90.NU_POR_MARG = (!string.IsNullOrEmpty(txtPerMarg.Text) ? decimal.Parse(txtPerMarg.Text) : (decimal?)null);
            //tb90.NU_POR_DESC = (!string.IsNullOrEmpty(txtPerDescMarg.Text) ? decimal.Parse(txtPerDescMarg.Text) : (decimal?)null);
            tb90.NU_POR_COMI = (!string.IsNullOrEmpty(txtPerComi.Text) ? decimal.Parse(txtPerComi.Text) : (decimal?)null);
            tb90.NU_POR_DESC_COMI = (!string.IsNullOrEmpty(txtPerDescComi.Text) ? decimal.Parse(txtPerDescComi.Text) : (decimal?)null);
            tb90.NU_POR_PROM = (!string.IsNullOrEmpty(txtPerProm.Text) ? decimal.Parse(txtPerProm.Text) : (decimal?)null);
            tb90.DT_VALIDADE = (!string.IsNullOrEmpty(txtValidade.Text) ? DateTime.Parse(txtValidade.Text) : (DateTime?)null);
            tb90.CO_CONVENIO = ddlConvenio.SelectedValue != "" ? int.Parse(ddlConvenio.SelectedValue) : (int?)null;
            tb90.NO_CLAS_ABC = ddlCLasABC.SelectedValue;
            tb90.NO_ROTATIVO = (!string.IsNullOrEmpty(txtRotativ.Text) ? txtRotativ.Text : null);
            tb90.FL_FATUR_ITEM = chkFaturado.Checked ? "S" : "N";

            tb90.VL_UNIT_PROD = txtValor.Text != "" ? Convert.ToDecimal(txtValor.Text) : 0;
            tb90.NU_DUR_PROD = txtDuracao.Text != "" ? Convert.ToDecimal(txtDuracao.Text) : 0;
            string strNomeUsuario = LoginAuxili.NOME_USU_LOGADO;
            tb90.NOM_USUARIO = strNomeUsuario.ToString().Substring(0, (strNomeUsuario.Length > 30 ? 29 : strNomeUsuario.Length));


            CurrentCadastroMasterPage.CurrentEntity = tb90;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB90_PRODUTO tb90 = RetornaEntidade();

            if (tb90 != null)
            {
                tb90.ImageReference.Load();
                tb90.TB124_TIPO_PRODUTOReference.Load();
                tb90.TB89_UNIDADESReference.Load();
                tb90.TB260_GRUPOReference.Load();
                tb90.TB261_SUBGRUPOReference.Load();
                tb90.TB95_CATEGORIAReference.Load();
                tb90.TB93_MARCAReference.Load();
                tb90.TB97_CORReference.Load();
                tb90.TB98_TAMANHOReference.Load();

                if (tb90.Image != null)
                    upImagemProdu.CarregaImagem(tb90.Image.ImageId);
                else
                    upImagemProdu.CarregaImagem(0);

                txtCodRef.Text = tb90.CO_REFE_PROD;
                txtDataCadastro.Text = tb90.DT_CADA_PROD.ToString("dd/MM/yyyy");
                txtDescProduto.Text = tb90.DES_PROD;
                txtCodBarr.Text = tb90.CO_BARRAS;
                txtQtCx.Text = tb90.QT_QX.ToString();
                txtDuracao.Text = tb90.NU_DUR_PROD.HasValue ? Math.Round(tb90.NU_DUR_PROD.Value,1).ToString() : "";
                txtNProduto.Text = tb90.NO_PROD;
                txtNReduz.Text = tb90.NO_PROD_RED;
                txtPeso.Text = tb90.NU_PESO_PROD.ToString();
                txtValor.Text = tb90.VL_UNIT_PROD.ToString();
                txtVolume.Text = tb90.NU_VOL_PROD.ToString();
                ddlCategoria.SelectedValue = tb90.TB95_CATEGORIA != null ? tb90.TB95_CATEGORIA.CO_CATEG.ToString() : "";
                ddlCor.SelectedValue = tb90.TB97_COR != null ? tb90.TB97_COR.CO_COR.ToString() : "";
                ddlImportado.SelectedValue = tb90.FLA_IMPORTADO;
                ddlMarca.SelectedValue = tb90.TB93_MARCA != null ? tb90.TB93_MARCA.CO_MARCA.ToString() : "";
                ddlSituacao.SelectedValue = tb90.CO_SITU_PROD;
                ddlTamanho.SelectedValue = tb90.TB98_TAMANHO != null ? tb90.TB98_TAMANHO.CO_TAMANHO.ToString() : "";
                ddlTipoProduto.SelectedValue = tb90.TB124_TIPO_PRODUTO != null ? tb90.TB124_TIPO_PRODUTO.CO_TIP_PROD.ToString() : "";
                ddlUnidade.SelectedValue = tb90.TB89_UNIDADES.CO_UNID_ITEM.ToString();
                ddlGrupo.SelectedValue =  tb90.TB260_GRUPO != null ? tb90.TB260_GRUPO.ID_GRUPO.ToString() : "";
                CarregaSubGrupo();
                ddlSubGrupo.SelectedValue = tb90.TB261_SUBGRUPO != null ? tb90.TB261_SUBGRUPO.ID_SUBGRUPO.ToString() : "";
                //Quantidades
                ddlUnidFab.SelectedValue = tb90.CO_UNID_FAB.ToString();
                txtQtdeFab.Text = tb90.QT_UNID_FAB.ToString();
                ddlUnidComp.SelectedValue = tb90.CO_UNID_COMPRA.ToString();
                txtQtdeComp.Text = tb90.QT_UNID_COMPRA.ToString();
                ddlUnidComp.SelectedValue = tb90.CO_UNID_VENDA.ToString();
                txtQtdeVend.Text = tb90.QT_UNID_VENDA.ToString();
                tb90.TBS457_CLASS_TERAPReference.Load();

                if (tb90.TBS457_CLASS_TERAP != null)
                {
                    OcultarPesquisa(true);

                    ddlClasseTerapeutica.SelectedValue = tb90.TBS457_CLASS_TERAP.ID_CLASS_TERAP.ToString();
                }


                //ddlClasseTerapeutica.SelectedValue = tb90.TBS457_CLASS_TERAP != null ? tb90.TBS457_CLASS_TERAP.ID_CLASS_TERAP.ToString() : "0";
                ddlTipoPsico.SelectedValue = tb90.FLA_IMPORTADO != null ? tb90.FLA_IMPORTADO : string.Empty;
                chkFaturado.Checked = tb90.FL_FATUR_ITEM == "S" ? true : false;


                //Segurança
                txtSegMin.Text = tb90.QT_SEG_MIN.ToString();
                txtSegMax.Text = tb90.QT_SEG_MAX.ToString();

                //Outras Informações
                chkFarmPopul.Checked = tb90.FL_FARM_POP == "S" ? true : false;
                ddlFabricante.SelectedValue = tb90.CO_FABRICANTE.ToString();
                txtCodFabricante.Text = tb90.NU_CO_FABRICANTE.ToString();
                ddlFornecedor.SelectedValue = tb90.CO_FORNECEDOR.ToString();
                txtDiasEntrForne.Text = tb90.QT_DIAS_FORNECEDOR.ToString();
                txtCodAnvisa.Text = !string.IsNullOrEmpty(tb90.CO_MS_ANVISA) ? tb90.CO_MS_ANVISA : "";
                txtNNCM.Text = !string.IsNullOrEmpty(tb90.NU_NCM) ? tb90.NU_NCM : "";
                ddlTipoPsico.SelectedValue = tb90.CO_TIPO_PSICO.HasValue ? tb90.CO_TIPO_PSICO.Value.ToString() : "";
                txtPrinAtiv.Text = tb90.NO_PRINCIPIO_ATIVO;
                ddlCorAlerta.SelectedValue = tb90.CO_COR_ALERTA.HasValue ?  tb90.CO_COR_ALERTA.Value.ToString() : "";
                txtObservacao.Text = tb90.DE_OBSERVACAO;

                //Dados Tributários
                txtPerTribuGrp.Text = tb90.NU_POR_GRP.HasValue ? tb90.NU_POR_GRP.Value.ToString() : "";
                txtGrpTribu.Text = tb90.NU_GRP.ToString();
                ddlClassTribu.SelectedValue = tb90.CO_CLASSIFICACAO.ToString();
                ddlTribPISConfins.SelectedValue = tb90.CO_PIS_COFINS.ToString();
                
                //Dados Comerciais
                chkManterMargem.Checked = tb90.FL_MANTER_MARGEM == "S" ? true : false;
                chkManterValVenda.Checked = tb90.FL_MANTER_VENDA == "S" ? true : false;
                txtValor.Text = tb90.VL_CUSTO.ToString();
                txtValVenda.Text = tb90.VL_VENDA.ToString();
                txtValVendMax.Text = tb90.VL_VENDA_MAX.ToString();
                txtPerMarg.Text = tb90.NU_POR_MARG.ToString();
                //txtPerDescMarg.Text = tb90.NU_POR_DESC.ToString();
                txtPerComi.Text = tb90.NU_POR_COMI.ToString();
                txtPerDescComi.Text = tb90.NU_POR_DESC_COMI.ToString();
                txtPerProm.Text = tb90.NU_POR_PROM.ToString();
                txtValidade.Text = tb90.DT_VALIDADE.ToString();
                ddlConvenio.SelectedValue = tb90.CO_CONVENIO.ToString();
                ddlCLasABC.SelectedValue = tb90.NO_CLAS_ABC;
                txtRotativ.Text = tb90.NO_ROTATIVO;

//------------> Faz a recuperação da quantidade do produto em Estoque
                var varQtdEstoque = (from tb96 in TB96_ESTOQUE.RetornaTodosRegistros()
                                  where tb96.TB90_PRODUTO.CO_PROD == tb90.CO_PROD && tb96.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                  select new { tb96.QT_SALDO_EST }).FirstOrDefault();

                if (varQtdEstoque != null)
                    txtQtdEstoque.Text = varQtdEstoque.QT_SALDO_EST.ToString();
                else
                    txtQtdEstoque.Text = "0";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB90_PRODUTO</returns>
        private TB90_PRODUTO RetornaEntidade()
        {
            int coprod = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            TB90_PRODUTO produto = TB90_PRODUTO.RetornaTodosRegistros().Where(w=>w.CO_PROD == coprod).FirstOrDefault();
            return (produto == null) ? new TB90_PRODUTO() : produto;
        }
        #endregion

        #region Carregamento DropDown

        private void CarregarClasseTerapeutica(string termo){

            ddlClasseTerapeutica.DataSource = TBS457_CLASS_TERAP.RetornarRegistros(p => p.DE_CLASS_TERAP.Contains(termo)).Select(x => new { x.ID_CLASS_TERAP, x.DE_CLASS_TERAP });
            ddlClasseTerapeutica.DataTextField = "DE_CLASS_TERAP";
            ddlClasseTerapeutica.DataValueField = "ID_CLASS_TERAP";
            ddlClasseTerapeutica.DataBind();

            ddlClasseTerapeutica.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega os fornecedores
        /// </summary>
        private void CarregaFornecedores()
        {
            var res = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                       select new { tb41.DE_RAZSOC_FORN, tb41.CO_FORN }).ToList().OrderBy(w => w.DE_RAZSOC_FORN);

            ddlFornecedor.DataTextField = "DE_RAZSOC_FORN";
            ddlFornecedor.DataValueField = "CO_FORN";
            ddlFornecedor.DataSource = res;
            ddlFornecedor.DataBind();

            ddlFornecedor.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Categoria
        /// </summary>
        private void CarregaCategoria()
        {
            ddlCategoria.DataSource = (from tb95 in TB95_CATEGORIA.RetornaTodosRegistros()
                                       select new { tb95.DES_CATEG, tb95.CO_CATEG });

            ddlCategoria.DataTextField = "DES_CATEG";
            ddlCategoria.DataValueField = "CO_CATEG";
            ddlCategoria.DataBind();

            ddlCategoria.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Cor
        /// </summary>
        private void CarregaCor()
        {
            AuxiliCarregamentos.CarregaCores(ddlCor, false);
            AuxiliCarregamentos.CarregaCores(ddlCorAlerta, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo
        /// </summary>
        private void CarregaGrupo() 
        {
            ddlGrupo.DataSource = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                                    where tb260.TP_GRUPO == "E"
                                    select new { tb260.ID_GRUPO, tb260.NOM_GRUPO });

            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubGrupo() 
        {
            int idGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubGrupo.DataSource = (from tb261 in TB261_SUBGRUPO.RetornaTodosRegistros()
                                      where (idGrupo != 0 ? tb261.TB260_GRUPO.ID_GRUPO == idGrupo : idGrupo == 0)
                                      select new { tb261.ID_SUBGRUPO, tb261.NOM_SUBGRUPO });

            ddlSubGrupo.DataValueField = "ID_SUBGRUPO";
            ddlSubGrupo.DataTextField = "NOM_SUBGRUPO";
            ddlSubGrupo.DataBind();

            ddlSubGrupo.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Marca
        /// </summary>
        private void CarregaMarca()
        {
            var res = (from tb93 in TB93_MARCA.RetornaTodosRegistros()
                                   select new { tb93.DES_MARCA, tb93.CO_MARCA });

            ddlMarca.DataTextField = "DES_MARCA";
            ddlMarca.DataValueField = "CO_MARCA";
            ddlMarca.DataSource = res;
            ddlMarca.DataBind();

            ddlMarca.Items.Insert(0, new ListItem("Selecione", ""));


            ddlFabricante.DataTextField = "DES_MARCA";
            ddlFabricante.DataValueField = "CO_MARCA";
            ddlFabricante.DataSource = res;
            ddlFabricante.DataBind();

            ddlFabricante.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tamanho
        /// </summary>
        private void CarregaTamanho()
        {
            ddlTamanho.DataSource = (from tb98 in TB98_TAMANHO.RetornaTodosRegistros()
                                     select new { tb98.DES_TAMANHO, tb98.CO_TAMANHO });

            ddlTamanho.DataTextField = "DES_TAMANHO";
            ddlTamanho.DataValueField = "CO_TAMANHO";
            ddlTamanho.DataBind();

            ddlTamanho.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega os dropdowns de Unidade
        /// </summary>
        private void CarregaUnidade()
        {  
            AuxiliCarregamentos.CarregaUnidadesMedidas(ddlUnidade, false);
            AuxiliCarregamentos.CarregaUnidadesMedidas(ddlUnidFab, false, false);
            AuxiliCarregamentos.CarregaUnidadesMedidas(ddlUnidComp, false, false);
            AuxiliCarregamentos.CarregaUnidadesMedidas(ddlUnidVend, false, false);

            ddlUnidFab.Items.Insert(0, new ListItem("", ""));
            ddlUnidComp.Items.Insert(0, new ListItem("", ""));
            ddlUnidVend.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Produto
        /// </summary>
        private void CarregaTipoProduto()
        {
            ddlTipoProduto.DataSource = (from tb124 in TB124_TIPO_PRODUTO.RetornaTodosRegistros()
                                         select new { tb124.DE_TIP_PROD, tb124.CO_TIP_PROD });

            ddlTipoProduto.DataTextField = "DE_TIP_PROD";
            ddlTipoProduto.DataValueField = "CO_TIP_PROD";
            ddlTipoProduto.DataBind();

            ddlTipoProduto.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNomeClassTerap.Visible =
            imgbPesqClassTerap.Visible = !ocultar;
            ddlClasseTerapeutica.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        #endregion

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
        }

        /// <summary>
        /// Excecuta a pesquisa para a classe terapêutica
        /// </summary>
        protected void imgbPesqClassTerap_OnClick(object sender, EventArgs e) {

            if (string.IsNullOrEmpty(txtNomeClassTerap.Text))
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Você deve inserir algum termo para ser pesquisado.");
                return;
            }

            CarregarClasseTerapeutica(txtNomeClassTerap.Text);
            OcultarPesquisa(true);

        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e) {
            OcultarPesquisa(false);
        }
    }
}
