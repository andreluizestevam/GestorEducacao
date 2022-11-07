//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: SAÚDE
// SUBMÓDULO: CONTROLE OPERACIONAL DE ITENS DE ESTOQUE
// OBJETIVO: CADASTRO DE ITENS DE ESTOQUE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 06/03/14 | Vinícius Reis              | Adaptação da tela de cadastro de itens 
//          |                            | no estoque 

using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

namespace C2BR.GestorEducacao.UI.GSAUD._4000_ControleOperacionalEstoque._4100_ControleOperacionalMateriais._4110_ControleOperacionalItensEstoque._4111_CadastroItemEstoque
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
                CarregaCategoria();
                CarregaCor();
                CarregaGrupo();
                CarregaSubGrupo();
                CarregaMarca();
                CarregaTamanho();
                CarregaUnidade(ddlUnidade);
                CarregaUnidade(ddlUnidFabric);
                CarregaUnidade(ddlUnidVenda);
                CarregaUnidade(ddlUnidCompra);
                CarregaTipoProduto();
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            TB90_PRODUTO tb90 = RetornaEntidade();

            //--------> Como o CO_EMP é campo chave, só é permitido inserí-lo quando for inclusão
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb90.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb90.TB260_GRUPO = ddlGrupo.SelectedValue != "" ? TB260_GRUPO.RetornaPelaChavePrimaria(int.Parse(ddlGrupo.SelectedValue)) : null;
            tb90.TB261_SUBGRUPO = ddlSubGrupo.SelectedValue != "" ? TB261_SUBGRUPO.RetornaPelaChavePrimaria(int.Parse(ddlSubGrupo.SelectedValue)) : null;
            tb90.CO_REFE_PROD = txtCodRef.Text;
            tb90.NO_PROD_RED = txtNReduz.Text;
            tb90.NO_PROD = txtNProduto.Text;
            tb90.DES_PROD = txtDescProduto.Text;
            tb90.TB124_TIPO_PRODUTO = ddlTipoProduto.SelectedValue != "" ? TB124_TIPO_PRODUTO.RetornaPelaChavePrimaria(int.Parse(ddlTipoProduto.SelectedValue)) : null;
            tb90.TB95_CATEGORIA = ddlCategoria.SelectedValue != "" ? TB95_CATEGORIA.RetornaPelaChavePrimaria(int.Parse(ddlCategoria.SelectedValue)) : null;
            tb90.FLA_IMPORTADO = ddlImportado.SelectedValue;
            tb90.TB89_UNIDADES = ddlUnidade.SelectedValue != "" ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(ddlUnidade.SelectedValue)) : null;
            tb90.TB93_MARCA = ddlMarca.SelectedValue != "" ? TB93_MARCA.RetornaPelaChavePrimaria(int.Parse(ddlMarca.SelectedValue)) : null;
            tb90.TB97_COR = ddlCor.SelectedValue != "" ? TB97_COR.RetornaPelaChavePrimaria(int.Parse(ddlCor.SelectedValue)) : null;
            tb90.TB98_TAMANHO = ddlTamanho.SelectedValue != "" ? TB98_TAMANHO.RetornaPelaChavePrimaria(int.Parse(ddlTamanho.SelectedValue)) : null;
            tb90.NU_PESO_PROD = txtPeso.Text != "" ? Convert.ToDecimal(txtPeso.Text) : 0;
            tb90.VL_UNIT_PROD = txtValor.Text != "" ? Convert.ToDecimal(txtValor.Text) : 0;
            tb90.NU_VOL_PROD = txtVolume.Text != "" ? Convert.ToDecimal(txtVolume.Text) : 0;
            tb90.NU_DUR_PROD = txtDuracao.Text != "" ? Convert.ToDecimal(txtDuracao.Text) : 0;
            tb90.DT_CADA_PROD = DateTime.Now;
            tb90.CO_SITU_PROD = ddlSituacao.SelectedValue;
            tb90.DT_ALT_REGISTRO = DateTime.Now;

            // Novos Campos - Saúde
            tb90.QT_QX = !string.IsNullOrEmpty(txtQtQx.Text) ? int.Parse(txtQtQx.Text) : new Nullable<int>();
            tb90.CO_BARRAS = !string.IsNullOrEmpty(txtCodigoBarras.Text) ? txtCodigoBarras.Text : "";
            tb90.CO_UNID_FAB = ddlUnidFabric.SelectedValue != "" ? int.Parse(ddlUnidFabric.SelectedValue) : new Nullable<int>();
            tb90.QT_UNID_FAB = !string.IsNullOrEmpty(txtQtdeFabric.Text) ? int.Parse(txtQtdeFabric.Text) : new Nullable<int>();
            tb90.CO_UNID_COMPRA = ddlUnidCompra.SelectedValue != "" ? int.Parse(ddlUnidCompra.SelectedValue) : new Nullable<int>();
            tb90.QT_UNID_COMPRA = !string.IsNullOrEmpty(txtQtdeCompra.Text) ? int.Parse(txtQtdeCompra.Text) : new Nullable<int>();
            tb90.CO_UNID_VENDA = ddlUnidVenda.SelectedValue != "" ? int.Parse(ddlUnidVenda.SelectedValue) : new Nullable<int>();
            tb90.QT_UNID_VENDA = !string.IsNullOrEmpty(txtQtdeVenda.Text) ? int.Parse(txtQtdeVenda.Text) : new Nullable<int>();
            tb90.QT_SEG_MIN = !string.IsNullOrEmpty(txtMinSeg.Text) ? int.Parse(txtMinSeg.Text) : new Nullable<int>();
            tb90.QT_SEG_MAX = !string.IsNullOrEmpty(txtMaxSeg.Text) ? int.Parse(txtMaxSeg.Text) : new Nullable<int>();

            tb90.QT_SALDO = !string.IsNullOrEmpty(txtSaldo.Text) ? int.Parse(txtSaldo.Text) : new Nullable<int>();
            tb90.QT_DIAS = !string.IsNullOrEmpty(txtDias.Text) ? int.Parse(txtDias.Text) : new Nullable<int>();
            tb90.QT_TRANSITO = !string.IsNullOrEmpty(txtTransito.Text) ? int.Parse(txtTransito.Text) : new Nullable<int>();
            tb90.QT_COMPRAS = !string.IsNullOrEmpty(txtCompras.Text) ? int.Parse(txtCompras.Text) : new Nullable<int>();
            tb90.QT_TOTAL = !string.IsNullOrEmpty(txtTotal.Text) ? int.Parse(txtTotal.Text) : new Nullable<int>();

            tb90.FL_FARM_POP = ckbFarmaciaPopular.Checked ? "S" : "N";

            tb90.CO_FABRICANTE = ddlFabricante.SelectedValue != "" ? int.Parse(ddlFabricante.SelectedValue) : new Nullable<int>();
            tb90.NU_CO_FABRICANTE = !string.IsNullOrEmpty(txtCodFabricante.Text) ? int.Parse(txtCodFabricante.Text) : new Nullable<int>();
            tb90.CO_FORNECEDOR = ddlFornecedor.SelectedValue != "" ? int.Parse(ddlFornecedor.SelectedValue) : new Nullable<int>();
            tb90.QT_DIAS_FORNECEDOR = !string.IsNullOrEmpty(txtDiasFornecedor.Text) ? int.Parse(txtDiasFornecedor.Text) : new Nullable<int>();
            tb90.CO_MS_ANVISA = !string.IsNullOrEmpty(txtCodMsAnvisa.Text) ? txtCodMsAnvisa.Text : null;
            tb90.NU_NCM = !string.IsNullOrEmpty(txtNumNCM.Text) ? txtNumNCM.Text : null;
            tb90.CO_TIPO_PSICO = ddlTipoPsico.SelectedValue != "" ? int.Parse(ddlTipoPsico.SelectedValue) : new Nullable<int>();
            tb90.NO_PRINCIPIO_ATIVO = !string.IsNullOrEmpty(txtPrincipioAtivo.Text) ? txtPrincipioAtivo.Text : "";
            tb90.CO_TIPO_PSICO = ddlTipoPsico.SelectedValue != "" ? int.Parse(ddlTipoPsico.SelectedValue) : new Nullable<int>();
            tb90.CO_COR_ALERTA = ddlCorAlerta.SelectedValue != "" ? int.Parse(ddlCorAlerta.SelectedValue) : new Nullable<int>();
            tb90.DE_OBSERVACAO = !string.IsNullOrEmpty(txtObservacao.Text) ? txtObservacao.Text : "";

            tb90.NU_POR_GRP = !string.IsNullOrEmpty(txtProcentagemGRP.Text) ? Convert.ToDecimal(txtProcentagemGRP.Text) : new Nullable<decimal>();
            tb90.NU_GRP = !string.IsNullOrEmpty(txtGRP.Text) ? Convert.ToDecimal(txtGRP.Text) : new Nullable<decimal>();
            tb90.CO_CLASSIFICACAO = ddlClassificacaoGRP.SelectedValue != "" ? int.Parse(ddlClassificacaoGRP.SelectedValue) : new Nullable<int>();
            tb90.CO_PIS_COFINS = ddlTributacaoPISCofins.SelectedValue != "" ? int.Parse(ddlTributacaoPISCofins.SelectedValue) : new Nullable<int>();

            tb90.FL_MANTER_MARGEM = ckbManterMargem.Checked ? "S" : "N";
            tb90.FL_MANTER_VENDA = ckbManterVenda.Checked ? "S" : "N";

            tb90.VL_CUSTO = !string.IsNullOrEmpty(txtValorCusto.Text) ? Convert.ToDecimal(txtValorCusto.Text) : new Nullable<decimal>();
            tb90.VL_VENDA = !string.IsNullOrEmpty(txtValorVenda.Text) ? Convert.ToDecimal(txtValorVenda.Text) : new Nullable<decimal>();
            tb90.VL_VENDA_MAX = !string.IsNullOrEmpty(txtValorVendaMax.Text) ? Convert.ToDecimal(txtValorVendaMax.Text) : new Nullable<decimal>();
            tb90.NU_POR_MARG = !string.IsNullOrEmpty(txtMarg.Text) ? Convert.ToDecimal(txtMarg.Text) : new Nullable<decimal>();
            tb90.NU_POR_DESC = !string.IsNullOrEmpty(txtDesc.Text) ? Convert.ToDecimal(txtDesc.Text) : new Nullable<decimal>();
            tb90.NU_POR_COMI = !string.IsNullOrEmpty(txtComi.Text) ? Convert.ToDecimal(txtComi.Text) : new Nullable<decimal>();
            tb90.NU_POR_PROM = !string.IsNullOrEmpty(txtProm.Text) ? Convert.ToDecimal(txtProm.Text) : new Nullable<decimal>();

            tb90.DT_VALIDADE = !string.IsNullOrEmpty(txtDataValidade.Text) ? Convert.ToDateTime(txtDataValidade.Text) : new Nullable<DateTime>();
            tb90.CO_CONVENIO = ddlConvenio.SelectedValue != "" ? int.Parse(ddlConvenio.SelectedValue) : new Nullable<int>();
            tb90.NO_CLAS_ABC = !string.IsNullOrEmpty(txtClasABC.Text) ? txtClasABC.Text : "";
            tb90.NO_ROTATIVO = !string.IsNullOrEmpty(txtRotativo.Text) ? txtRotativo.Text : "";

            int codImagem = upImagemProduto.GravaImagem();
            tb90.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);

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
                tb90.TB124_TIPO_PRODUTOReference.Load();
                tb90.TB89_UNIDADESReference.Load();
                tb90.TB260_GRUPOReference.Load();
                tb90.TB261_SUBGRUPOReference.Load();
                tb90.TB95_CATEGORIAReference.Load();
                tb90.TB93_MARCAReference.Load();
                tb90.TB97_CORReference.Load();
                tb90.TB98_TAMANHOReference.Load();
                tb90.ImageReference.Load();

                if (tb90.Image != null)
                    upImagemProduto.CarregaImagem(tb90.Image.ImageId);
                else
                    upImagemProduto.CarregaImagem(0);

                txtCodRef.Text = tb90.CO_REFE_PROD;
                txtDataCadastro.Text = tb90.DT_CADA_PROD.ToString("dd/MM/yyyy");
                txtDescProduto.Text = tb90.DES_PROD;
                txtDuracao.Text = Math.Round(tb90.NU_DUR_PROD.Value, 1).ToString();
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
                ddlGrupo.SelectedValue = tb90.TB260_GRUPO.ID_GRUPO.ToString();
                CarregaSubGrupo();
                ddlSubGrupo.SelectedValue = tb90.TB261_SUBGRUPO.ID_SUBGRUPO.ToString();

                // Novos campos módulo de Saúde
                txtQtQx.Text = tb90.QT_QX != null ? tb90.QT_QX.ToString() : string.Empty;
                txtCodigoBarras.Text = tb90.CO_BARRAS != null ? tb90.CO_BARRAS : string.Empty;
                ddlUnidFabric.SelectedValue = tb90.CO_UNID_FAB != null ? tb90.CO_UNID_FAB.ToString() : "0";
                txtQtdeFabric.Text = tb90.QT_UNID_FAB != null ? tb90.QT_UNID_FAB.ToString() : string.Empty;
                ddlUnidCompra.SelectedValue = tb90.CO_UNID_COMPRA != null ? tb90.CO_UNID_COMPRA.ToString() : "0";
                txtQtdeCompra.Text = tb90.QT_UNID_COMPRA != null ? tb90.QT_UNID_COMPRA.ToString() : string.Empty;
                ddlUnidVenda.SelectedValue = tb90.CO_UNID_VENDA != null ? tb90.CO_UNID_VENDA.ToString() : "0";
                txtQtdeVenda.Text = tb90.QT_UNID_VENDA != null ? tb90.QT_UNID_VENDA.ToString() : string.Empty;
                txtMinSeg.Text = tb90.QT_SEG_MIN != null ? tb90.QT_SEG_MIN.ToString() : string.Empty;
                txtMaxSeg.Text = tb90.QT_SEG_MAX != null ? tb90.QT_SEG_MAX.ToString() : string.Empty;
                txtSaldo.Text = tb90.QT_SALDO != null ? tb90.QT_SALDO.ToString() : string.Empty;
                txtDias.Text = tb90.QT_DIAS != null ? tb90.QT_DIAS.ToString() : string.Empty;
                txtTransito.Text = tb90.QT_TRANSITO != null ? tb90.QT_TRANSITO.ToString() : string.Empty;
                txtCompras.Text = tb90.QT_COMPRAS != null ? tb90.QT_COMPRAS.ToString() : string.Empty;
                txtTotal.Text = tb90.QT_TOTAL != null ? tb90.QT_TOTAL.ToString() : string.Empty;

                ckbFarmaciaPopular.Checked = tb90.FL_FARM_POP == "S" ? true : false;

                ddlFabricante.SelectedValue = tb90.CO_FABRICANTE != null ? tb90.CO_FABRICANTE.ToString() : "0";
                txtCodFabricante.Text = tb90.NU_CO_FABRICANTE != null ? tb90.NU_CO_FABRICANTE.ToString() : string.Empty;
                ddlFornecedor.SelectedValue = tb90.CO_FORNECEDOR != null ? tb90.CO_FORNECEDOR.ToString() : "0";
                txtDiasFornecedor.Text = tb90.NU_CO_FABRICANTE != null ? tb90.NU_CO_FABRICANTE.ToString() : string.Empty;
                txtCodMsAnvisa.Text = tb90.CO_MS_ANVISA != null ? tb90.CO_MS_ANVISA.ToString() : string.Empty;
                txtNumNCM.Text = tb90.NU_NCM != null ? tb90.NU_NCM.ToString() : string.Empty;
                ddlTipoPsico.SelectedValue = tb90.CO_TIPO_PSICO != null ? tb90.CO_TIPO_PSICO.ToString() : "0";
                txtPrincipioAtivo.Text = tb90.NO_PRINCIPIO_ATIVO != null ? tb90.NO_PRINCIPIO_ATIVO : string.Empty;
                ddlCorAlerta.SelectedValue = tb90.CO_COR_ALERTA != null ? tb90.CO_COR_ALERTA.ToString() : "0";
                txtObservacao.Text = tb90.DE_OBSERVACAO != null ? tb90.DE_OBSERVACAO : string.Empty;

                txtProcentagemGRP.Text = tb90.NU_POR_GRP != null ? tb90.NU_POR_GRP.ToString() : string.Empty;
                txtGRP.Text = tb90.NU_GRP != null ? tb90.NU_GRP.ToString() : string.Empty;
                ddlClassificacaoGRP.SelectedValue = tb90.CO_CLASSIFICACAO != null ? tb90.CO_CLASSIFICACAO.ToString() : "0";
                ddlTributacaoPISCofins.SelectedValue = tb90.CO_PIS_COFINS != null ? tb90.CO_PIS_COFINS.ToString() : "0";

                ckbManterMargem.Checked = tb90.FL_MANTER_MARGEM == "S" ? true : false;
                ckbManterVenda.Checked = tb90.FL_MANTER_VENDA == "S" ? true : false;
                txtValorCusto.Text = tb90.VL_CUSTO != null ? tb90.VL_CUSTO.ToString() : string.Empty;
                txtValorVenda.Text = tb90.VL_VENDA != null ? tb90.VL_VENDA.ToString() : string.Empty;
                txtValorVendaMax.Text = tb90.VL_VENDA_MAX != null ? tb90.VL_VENDA_MAX.ToString() : string.Empty;
                txtMarg.Text = tb90.NU_POR_MARG != null ? tb90.NU_POR_MARG.ToString() : string.Empty;
                txtDesc.Text = tb90.NU_POR_DESC != null ? tb90.NU_POR_DESC.ToString() : string.Empty;
                txtComi.Text = tb90.NU_POR_COMI != null ? tb90.NU_POR_COMI.ToString() : string.Empty;
                txtProm.Text = tb90.NU_POR_PROM != null ? tb90.NU_POR_PROM.ToString() : string.Empty;
                txtDataValidade.Text = tb90.DT_VALIDADE != null ? tb90.DT_VALIDADE.ToString() : string.Empty;
                ddlConvenio.SelectedValue = tb90.CO_CONVENIO != null ? tb90.CO_CONVENIO.ToString() : "0";
                txtClasABC.Text = tb90.NO_CLAS_ABC != null ? tb90.NO_CLAS_ABC.ToString() : string.Empty;
                txtRotativo.Text = tb90.NO_ROTATIVO != null ? tb90.NO_ROTATIVO.ToString() : string.Empty;

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
            TB90_PRODUTO produto = TB90_PRODUTO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id), LoginAuxili.CO_EMP);
            return (produto == null) ? new TB90_PRODUTO() : produto;
        }
        #endregion

        #region Carregamento DropDown

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
            ddlCor.DataSource = (from tb97 in TB97_COR.RetornaTodosRegistros()
                                 select new { tb97.DES_COR, tb97.CO_COR });

            ddlCor.DataTextField = "DES_COR";
            ddlCor.DataValueField = "CO_COR";
            ddlCor.DataBind();

            ddlCor.Items.Insert(0, new ListItem("Selecione", ""));
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
            ddlMarca.DataSource = (from tb93 in TB93_MARCA.RetornaTodosRegistros()
                                   select new { tb93.DES_MARCA, tb93.CO_MARCA });

            ddlMarca.DataTextField = "DES_MARCA";
            ddlMarca.DataValueField = "CO_MARCA";
            ddlMarca.DataBind();

            ddlMarca.Items.Insert(0, new ListItem("Selecione", ""));
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
        /// Método que carrega o dropdown de Unidade
        /// </summary>
        private void CarregaUnidade(DropDownList ddl)
        {
            ddl.DataSource = (from tb89 in TB89_UNIDADES.RetornaTodosRegistros()
                                     select new { tb89.NO_UNID_ITEM, tb89.CO_UNID_ITEM });

            ddl.DataTextField = "NO_UNID_ITEM";
            ddl.DataValueField = "CO_UNID_ITEM";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
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
        #endregion

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo();
        }
    }
}