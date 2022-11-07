//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE PATRIMONIO
// SUBMÓDULO: REGISTRO DE ITENS DE PATRIMÔNIO
// OBJETIVO: REGISTRO DE ITENS DE PATRIMÔNIO
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
using System.Text;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6310_CtrlManutencaoItensPatrimonio.F6311_RegistroItemPatrimonio
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

//--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
            CurrentCadastroMasterPage.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentCadastroMasterPage_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDadosFornecedor();
                if (txtCodPatrimonio.Text == "")
                {
                    CarregaUnidadeOrigem();
                    CarregaUnidadeAtual();
                }
                CarregarDepartamentos();
                CarregaTipoPatrimonio();

                if (CurrentCadastroMasterPage.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    txtDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentCadastroMasterPage_OnCarregaFormulario()
        {
            CarregaFormulario();
            CarregaUnidadeAtual();
            CarregaUnidadeOrigem();
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
        {
            string strTpPatr = ddlTipoPatrimonio.SelectedValue;
            int idSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            int coForn = ddlDadosFornecedor.SelectedValue != "" ? int.Parse(ddlDadosFornecedor.SelectedValue) : 0;
            int coDeptoAtual = ddlDeptoAtual.SelectedValue != "" ? int.Parse(ddlDeptoAtual.SelectedValue) : 0;
            int coDeptoOrigem = ddlDeptoOrigem.SelectedValue != "" ? int.Parse(ddlDeptoOrigem.SelectedValue) : 0;

            TB212_ITENS_PATRIMONIO tb212 = RetornaEntidade();

            if (tb212 == null)
            {
                tb212 = new TB212_ITENS_PATRIMONIO();
                tb212.COD_PATR = GerarCodigoPatrimonio();
            }

            tb212.NU_PATR_ANT = txtNumeroPatrimonio.Text != "" ? decimal.Parse(txtNumeroPatrimonio.Text) : tb212.NU_PATR_ANT;
            tb212.NU_PROCESSO = txtNumeroProcesso.Text != "" ? decimal.Parse(txtNumeroProcesso.Text) : tb212.NU_PROCESSO;
            tb212.TP_PATR = strTpPatr;
            tb212.TP_AQUISICAO = ddlTipoAquisicao.SelectedValue;
            tb212.TP_FRM_AQUISICAO = ddlFormaAquisicao.SelectedValue;
            tb212.TB261_SUBGRUPO = TB261_SUBGRUPO.RetornaPelaChavePrimaria(idSubGrupo);
            tb212.NU_PROCESSO_ADMINISTRATIVO = txtNumeroProcessoAdministrativo.Text != "" ? decimal.Parse(txtNumeroProcessoAdministrativo.Text) : tb212.NU_PROCESSO_ADMINISTRATIVO;
            tb212.NU_EMPENHO = txtNumeroEmpenho.Text != "" ? decimal.Parse(txtNumeroEmpenho.Text) : tb212.NU_EMPENHO;
            tb212.NU_DOTACAO_ORCAMENTARIA = txtDotacaoOrcamentaria.Text != "" ? txtDotacaoOrcamentaria.Text : tb212.NU_DOTACAO_ORCAMENTARIA;
            tb212.TB41_FORNEC = TB41_FORNEC.RetornaPelaChavePrimaria(coForn);
            tb212.NOM_PATR = txtTitulo.Text;
            tb212.DE_PATR = txtDescPatrimonio.Text;
            tb212.NU_NOT_FISC = decimal.Parse(txtNotaFiscal.Text);
            tb212.DT_EMIS_NF = txtDataEmissaoNF.Text != "" ? DateTime.Parse(txtDataEmissaoNF.Text) : tb212.DT_EMIS_NF;
            tb212.DT_FIM_GARANT = txtDataFimGarantia.Text != "" ? DateTime.Parse(txtDataFimGarantia.Text) : tb212.DT_FIM_GARANT;
            tb212.VL_AQUIS = decimal.Parse(txtVlrAquisicao.Text);
            tb212.PE_TX_DEPR_ANU = txtVlrDepreciacao.Text != "" ? decimal.Parse(txtVlrDepreciacao.Text) : tb212.PE_TX_DEPR_ANU;            
            tb212.TB14_DEPTO1 = TB14_DEPTO.RetornaPelaChavePrimaria(coDeptoAtual);
            tb212.CO_EMP = int.Parse(ddlUnidadePatrimonio.SelectedValue);
            tb212.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(coDeptoOrigem);
            tb212.CO_ESTADO = ddlEstadoConservacao.SelectedValue;
            tb212.CO_STATUS = ddlStatusPatrimonio.SelectedValue;
            tb212.DT_CADASTRO = DateTime.Now;
            tb212.NO_RESP = LoginAuxili.NOME_USU_LOGADO;
            
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                if (tb212.TP_PATR == "1")
                {
                    TB216_PATR_MOVEL tb216 = RetornaEntidadeMovel(tb212.COD_PATR);
                    TB216_PATR_MOVEL.Delete(tb216);
                }
                else
                {
                    TB217_PATR_IMOVEL tb217 = RetornaEntidadeImovel(tb212.COD_PATR);
                    TB217_PATR_IMOVEL.Delete(tb217);
                }

                CurrentCadastroMasterPage.CurrentEntity = tb212;
            }
            else
            {
                if (strTpPatr == "1")
                    PreencherEntidadeMovel(tb212.COD_PATR);
                else if (strTpPatr == "2")
                    PreencherEntidadeImovel(tb212.COD_PATR);
                else
                    CurrentCadastroMasterPage.CurrentEntity = tb212;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB212_ITENS_PATRIMONIO tb212 = RetornaEntidade(decimal.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));

            if (tb212 != null)
            {
                hdnCodPatr.Value = tb212.COD_PATR.ToString();
                CarregaItensPatrimonio(tb212);
                MostrarDetalhesPatrimonio(int.Parse(tb212.TP_PATR));

                if (tb212.TP_PATR == "1")
                {
                    TB216_PATR_MOVEL tb216 = RetornaEntidadeMovel(tb212.COD_PATR);
                    CarregaPatrimonioMovel(tb216);
                }
                else if (tb212.TP_PATR == "2")
                {
                    TB217_PATR_IMOVEL tb217 = RetornaEntidadeImovel(tb212.COD_PATR);
                    CarregaPatrimonioImovel(tb217);
                }

                ddlTipoPatrimonio.Enabled = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="codPatr">Id do patrimônio</param>
        /// <returns>Entidade TB212_ITENS_PATRIMONIO</returns>
        private TB212_ITENS_PATRIMONIO RetornaEntidade(decimal codPatr)
        {
            return TB212_ITENS_PATRIMONIO.RetornaPelaChavePrimaria(codPatr);
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="codPatr">Id do patrimônio</param>
        /// <returns>Entidade TB216_PATR_MOVEL</returns>
        private TB216_PATR_MOVEL RetornaEntidadeMovel(decimal codPatr)
        {
            return TB216_PATR_MOVEL.RetornaPelaChavePrimaria(codPatr);
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <param name="codPatr">Id do patrimônio</param>
        /// <returns>Entidade TB217_PATR_IMOVEL</returns>
        private TB217_PATR_IMOVEL RetornaEntidadeImovel(decimal codPatr)
        {
            return TB217_PATR_IMOVEL.RetornaPelaChavePrimaria(codPatr);
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB212_ITENS_PATRIMONIO</returns>
        private TB212_ITENS_PATRIMONIO RetornaEntidade()
        {
            if (QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id) != null)
                return TB212_ITENS_PATRIMONIO.RetornaPelaChavePrimaria(decimal.Parse(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id)));
            else
              return TB212_ITENS_PATRIMONIO.RetornaPelaChavePrimaria(0);
        }

        /// <summary>
        /// Método que apresenta o painel de detalhes do patrimônio de acordo com o tipo
        /// </summary>
        /// <param name="tipoPatrimonio">Tipo de patrimônio</param>
        private void MostrarPanelDetalhe(int tipoPatrimonio)
        {
            pnlDadosMovel.Visible = tipoPatrimonio == 1;            
        }

        /// <summary>
        /// Método que carrega informações do Patrimônio Móvel selecionado
        /// </summary>
        /// <param name="tb216">Entidade TB216_PATR_MOVEL</param>
        private void CarregaPatrimonioMovel(TB216_PATR_MOVEL tb216)
        {
            tb216.TB97_CORReference.Load();

            txtNrPlaca.Text = tb216.NU_PLACA.Trim();
            txtNrChassi.Text = tb216.CO_CHASSI.Trim();
            ddlCor.SelectedValue = tb216.TB97_COR.CO_COR.ToString();
            txtAno.Text = tb216.CO_ANO.ToString();
            txtModelo.Text = tb216.NO_MODELO;
            txtKilometragem.Text = tb216.NU_QUILOMETRAGEM.ToString();
        }

        /// <summary>
        /// Método que carrega informações do Patrimônio Imóvel selecionado
        /// </summary>
        /// <param name="tb217">Entidade TB217_PATR_IMOVEL</param>
        private void CarregaPatrimonioImovel(TB217_PATR_IMOVEL tb217)
        {
            tb217.TB180_TIPO_ESTAD_CONSERVReference.Load();
            tb217.TB904_CIDADEReference.Load();

            ddlUF.SelectedValue = tb217.CO_UF;
            CarregaCidades();
            ddlCidade.SelectedValue = tb217.TB904_CIDADE.CO_CIDADE.ToString();
            CarregaBairros();
            ddlBairro.SelectedValue = tb217.CO_BAIRRO.ToString();
            txtNrRegCartorio.Text = tb217.NU_REG_CART.ToString();
            txtNrEscritura.Text = tb217.NU_ESCRITURA.ToString();
            txtLogradouro.Text = tb217.NO_LOGRADOURO;
            txtNrLogradouro.Text = tb217.NU_LOGRADOURO.ToString();
            txtComplemento.Text = tb217.NO_COMPLEMENTO;
            txtMetragem.Text = tb217.NU_METRAGEM.ToString();
            txtCaracteristicas.Text = tb217.NO_CARAC_PATR;
            ddlTipoEdificio.SelectedValue = tb217.TB179_TIPO_EDIFI != null ? tb217.TB179_TIPO_EDIFI.CO_SIGLA_TIPO_EDIFI : ddlTipoEdificio.SelectedValue;
        }

        /// <summary>
        /// Método que carrega informações dos Itens de Patrimônio selecionado
        /// </summary>
        /// <param name="tb212">Entidade TB212_ITENS_PATRIMONIO</param>
        private void CarregaItensPatrimonio(TB212_ITENS_PATRIMONIO tb212)
        {
            tb212.TB14_DEPTO1Reference.Load();
            tb212.TB14_DEPTOReference.Load();
            tb212.TB261_SUBGRUPOReference.Load();
            tb212.TB261_SUBGRUPO.TB260_GRUPOReference.Load();

            txtCodPatrimonio.Text = tb212.COD_PATR.ToString();
            txtDataCadastro.Text = tb212.DT_CADASTRO.ToString("dd/MM/yyyy");
            ddlUnidadePatrimonio.SelectedValue = tb212.CO_EMP.ToString();
            ddlTipoPatrimonio.SelectedValue = tb212.TP_PATR;
            txtNumeroPatrimonio.Text = tb212.NU_PATR_ANT.ToString();
            txtNumeroProcesso.Text = tb212.NU_PROCESSO.ToString();
            txtNotaFiscal.Text = tb212.NU_NOT_FISC.ToString();
            txtTitulo.Text = tb212.NOM_PATR.ToString();
            CarregaGrupo();
            CarregaSubGrupo();
            ddlGrupo.SelectedValue = tb212.TB261_SUBGRUPO.TB260_GRUPO.ID_GRUPO.ToString();
            ddlSubGrupo.SelectedValue = tb212.TB261_SUBGRUPO.ID_SUBGRUPO.ToString();
            txtDataEmissaoNF.Text = tb212.DT_EMIS_NF != null ? tb212.DT_EMIS_NF.Value.ToString("dd/MM/yyyy") : "";
            txtDataFimGarantia.Text = tb212.DT_FIM_GARANT != null ? tb212.DT_FIM_GARANT.Value.ToString("dd/MM/yyyy") : "";
            txtVlrAquisicao.Text = tb212.VL_AQUIS.ToString();
            txtVlrDepreciacao.Text = tb212.PE_TX_DEPR_ANU.ToString();
            ddlDeptoAtual.SelectedValue = tb212.TB14_DEPTO.CO_DEPTO.ToString();
            ddlDeptoOrigem.SelectedValue = tb212.TB14_DEPTO1.CO_DEPTO.ToString();
            ddlEstadoConservacao.SelectedValue = tb212.CO_ESTADO;
            ddlStatusPatrimonio.SelectedValue = tb212.CO_STATUS;
            txtDescPatrimonio.Text = tb212.DE_PATR;
            ddlTipoAquisicao.SelectedValue = tb212.TP_AQUISICAO;
            ddlFormaAquisicao.SelectedValue = tb212.TP_FRM_AQUISICAO;
            txtNumeroProcessoAdministrativo.Text = tb212.NU_PROCESSO_ADMINISTRATIVO.ToString();
            txtNumeroEmpenho.Text = tb212.NU_EMPENHO.ToString();
            txtDotacaoOrcamentaria.Text = tb212.NU_DOTACAO_ORCAMENTARIA;
        }

        /// <summary>
        /// Método que apresenta detalhes do Patrimônio de acordo com o tipo
        /// </summary>
        /// <param name="intTipoPatrimonio">Id do tipo de patrimônio</param>
        private void MostrarDetalhesPatrimonio(int intTipoPatrimonio)
        {
            if (intTipoPatrimonio == 1)
            {
                lblDetalhesPatr.Visible = pnlDadosImovel.Visible = false;
                pnlDadosMovel.Visible = true;
                HabilitarCamposImovel(false);
                HabilitarCamposMovel(true);
            }
            else if (intTipoPatrimonio == 2)
            {
                lblDetalhesPatr.Visible = pnlDadosMovel.Visible = false;
                pnlDadosImovel.Visible = true;                
                HabilitarCamposMovel(false);
                HabilitarCamposImovel(true);
            }
            else
            {
                pnlDadosImovel.Visible = pnlDadosMovel.Visible = false;
                lblDetalhesPatr.Visible = true;
                HabilitarCamposMovel(false);
                HabilitarCamposImovel(false);                
                lblDetalhesPatr.Text = intTipoPatrimonio == -1 ? "O tipo do patrimônio deve ser selecionado para o carregamento dos detalhes." : "O tipo de patrimônio selecionado não possui detalhe.";
            }
        }

        /// <summary>
        /// Método que preenche entidade do Patrimônio Móvel
        /// </summary>
        /// <param name="codPatr">Id do patrimônio</param>
        private void PreencherEntidadeMovel(decimal codPatr)
        {
            int coCor = ddlCor.SelectedValue != "" ? int.Parse(ddlCor.SelectedValue) : 0;

            TB216_PATR_MOVEL tb216 = RetornaEntidadeMovel(codPatr);

            if (tb216 == null)
            {
                tb216 = new TB216_PATR_MOVEL();
                tb216.CO_PATR_MOVEL = codPatr;
            }

            tb216.NU_PLACA = txtNrPlaca.Text;
            tb216.CO_CHASSI = txtNrChassi.Text;
            tb216.TB97_COR = TB97_COR.RetornaPelaChavePrimaria(coCor);
            tb216.CO_ANO = txtAno.Text;
            tb216.NO_MODELO = txtModelo.Text;
            tb216.NU_QUILOMETRAGEM = txtKilometragem.Text != "" ? decimal.Parse(txtKilometragem.Text) : tb216.NU_QUILOMETRAGEM;

            CurrentCadastroMasterPage.CurrentEntity = tb216;
        }

        /// <summary>
        /// Método que Habilita campos do Patrimônio Móvel
        /// </summary>
        /// <param name="flagHabilita">Flag de habilitação</param>
        private void HabilitarCamposMovel(bool flagHabilita)
        {
            txtNrPlaca.Enabled = txtNrChassi.Enabled = ddlCor.Enabled = txtAno.Enabled = txtModelo.Enabled = txtKilometragem.Enabled =
            rfvNrPlaca.Enabled = rfvNrChassi.Enabled = rfvCor.Enabled = rfvAno.Enabled = rfvModelo.Enabled = true;

            if (flagHabilita)
                CarregaCores();            
        }

        /// <summary>
        /// Método que Habilita campos do Patrimônio Imóvel
        /// </summary>
        /// <param name="flagHabilita">Flag de habilitação</param>
        private void HabilitarCamposImovel(bool flagHabilita)
        {
            txtNrRegCartorio.Enabled = txtNrEscritura.Enabled = txtLogradouro.Enabled = txtNrLogradouro.Enabled = txtComplemento.Enabled =
            ddlUF.Enabled = ddlCidade.Enabled = ddlBairro.Enabled = txtMetragem.Enabled = txtCaracteristicas.Enabled = ddlTipoEdificio.Enabled =
            rfvLogradouro.Enabled = rfvNrLogradouro.Enabled = rfvUF.Enabled = rfvCidade.Enabled = rfvBairro.Enabled = flagHabilita;

            if (flagHabilita)
            {
                CarregaUfs(ddlUF);
                CarregaCidades();
                CarregaTipoEdificio();
            }            
        }

        /// <summary>
        /// Método que preenche entidade do Patrimônio Imóvel
        /// </summary>
        /// <param name="codPatr">Id do patrimônio</param>
        private void PreencherEntidadeImovel(decimal codPatr)
        {
            TB217_PATR_IMOVEL tb217 = RetornaEntidadeImovel(codPatr);

            if (tb217 == null)
            {
                tb217 = new TB217_PATR_IMOVEL();
                tb217.COD_PATR_IMOVEL = codPatr;
            }

            tb217.NU_REG_CART = txtNrRegCartorio.Text != "" ? decimal.Parse(txtNrRegCartorio.Text) : tb217.NU_REG_CART;
            tb217.NU_ESCRITURA = txtNrEscritura.Text != "" ? decimal.Parse(txtNrEscritura.Text) : tb217.NU_ESCRITURA;
            tb217.NO_LOGRADOURO = txtLogradouro.Text;
            tb217.NU_LOGRADOURO = int.Parse(txtNrLogradouro.Text);
            tb217.NO_COMPLEMENTO = txtComplemento.Text;
            tb217.CO_UF = ddlUF.SelectedItem.Text;
            tb217.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(int.Parse(ddlCidade.SelectedValue));
            tb217.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
            tb217.NU_METRAGEM = txtMetragem.Text != "" ? int.Parse(txtMetragem.Text) : tb217.NU_METRAGEM;
            tb217.NO_CARAC_PATR = txtCaracteristicas.Text;
            tb217.TB179_TIPO_EDIFI = TB179_TIPO_EDIFI.RetornaPelaChavePrimaria(ddlTipoEdificio.SelectedValue);

            CurrentCadastroMasterPage.CurrentEntity = tb217;
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDown de UF</param>
        private void CarregaUfs(DropDownList ddl)
        {
            ddl.DataSource = TB74_UF.RetornaTodosRegistros();
            ddl.DataTextField = "CODUF";
            ddl.DataValueField = "CODUF";
            ddl.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Cores
        /// </summary>
        private void CarregaCores()
        {
            ddlCor.DataSource = TB97_COR.RetornaTodosRegistros();

            ddlCor.DataTextField = "DES_COR";
            ddlCor.DataValueField = "CO_COR";
            ddlCor.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Enabled = ddlCidade.Items.Count > 0;
            ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;
                ddlBairro.Items.Clear();
                return;
            }

            ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Enabled = ddlBairro.Items.Count > 0;
            ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Edifício
        /// </summary>
        private void CarregaTipoEdificio()
        {
            ddlTipoEdificio.DataSource = TB179_TIPO_EDIFI.RetornaTodosRegistros();

            ddlTipoEdificio.DataTextField = "DE_TIPO_EDIFI";
            ddlTipoEdificio.DataValueField = "CO_SIGLA_TIPO_EDIFI";
            ddlTipoEdificio.DataBind();

            ddlTipoEdificio.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades de Origem
        /// </summary>
        private void CarregaUnidadeOrigem()
        {
            int coDeptoOrigem = ddlDeptoOrigem.SelectedValue != "" ? int.Parse(ddlDeptoOrigem.SelectedValue) : 0;

            ddlDeptoOrigem.Enabled = txtCodPatrimonio.Text == "";
            ddlUnidadePatrimonio.Enabled = false;            

            if (coDeptoOrigem != 0)
            {
                var tb14 = TB14_DEPTO.RetornaPelaChavePrimaria(coDeptoOrigem);

                tb14.TB25_EMPRESAReference.Load();

                int coEmp = tb14.TB25_EMPRESA.CO_EMP;

                var tb25 = (from lTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           where lTb25.CO_EMP == coEmp
                           select new { lTb25.CO_EMP, lTb25.NO_FANTAS_EMP }).FirstOrDefault();

                ddlUnidadePatrimonio.Items.Insert(0, new ListItem(tb25.NO_FANTAS_EMP, tb25.CO_EMP.ToString()));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown da Unidades Atual
        /// </summary>
        private void CarregaUnidadeAtual()
        {
            int coDeptoAtual = ddlDeptoAtual.SelectedValue != "" ? int.Parse(ddlDeptoAtual.SelectedValue) : 0;

            ddlUnidadeAtual.Enabled = false;

            if (coDeptoAtual != 0)
            {
                var tb14 = TB14_DEPTO.RetornaPelaChavePrimaria(coDeptoAtual);

                tb14.TB25_EMPRESAReference.Load();

                int coEmp = tb14.TB25_EMPRESA.CO_EMP;

                var tb25 = (from lTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                            where lTb25.CO_EMP == coEmp
                            select new { lTb25.CO_EMP, lTb25.NO_FANTAS_EMP }).FirstOrDefault();

                ddlUnidadeAtual.Items.Insert(0, new ListItem(tb25.NO_FANTAS_EMP, tb25.CO_EMP.ToString()));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Fornecedor
        /// </summary>
        private void CarregaDadosFornecedor()
        {
            ddlDadosFornecedor.DataSource = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                                             select new { tb41.CO_FORN, tb41.NO_FAN_FOR }).OrderBy( f => f.NO_FAN_FOR );

            ddlDadosFornecedor.DataValueField = "CO_FORN";
            ddlDadosFornecedor.DataTextField = "NO_FAN_FOR";
            ddlDadosFornecedor.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo
        /// </summary>
        private void CarregaGrupo()
        {
            ddlGrupo.DataSource = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                                   where tb260.TP_GRUPO == "P"
                                   select new { tb260.ID_GRUPO, tb260.NOM_GRUPO });

            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataTextField = "NOM_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo
        /// </summary>
        private void CarregaSubGrupo()
        {
            int idGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            
            ddlSubGrupo.DataSource = (from tb261 in TB261_SUBGRUPO.RetornaTodosRegistros()
                                      where tb261.TB260_GRUPO.ID_GRUPO == idGrupo
                                      select new { tb261.ID_SUBGRUPO, tb261.NOM_SUBGRUPO });

            ddlSubGrupo.DataValueField = "ID_SUBGRUPO";
            ddlSubGrupo.DataTextField = "NOM_SUBGRUPO";
            ddlSubGrupo.DataBind();

            ddlSubGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Patrimônio
        /// </summary>
        private void CarregaTipoPatrimonio()
        {
            ddlTipoPatrimonio.DataSource = from tb291 in TB291_TIPO_PATRIM.RetornaTodosRegistros()
                                           select new { tb291.CO_TIPO_PATRIM, tb291.NO_TIPO_PATRIM };

            ddlTipoPatrimonio.DataTextField = "NO_TIPO_PATRIM";
            ddlTipoPatrimonio.DataValueField = "CO_TIPO_PATRIM";
            ddlTipoPatrimonio.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamentos
        /// </summary>
        private void CarregarDepartamentos()
        {
            var tb14 = TB14_DEPTO.RetornaTodosRegistros().OrderBy( d => d.CO_SIGLA_DEPTO );


            ddlDeptoAtual.DataSource = tb14;
            ddlDeptoOrigem.DataSource = tb14;

            ddlDeptoAtual.DataValueField = "CO_DEPTO";
            ddlDeptoAtual.DataTextField = "CO_SIGLA_DEPTO";
            ddlDeptoAtual.DataBind();
            ddlDeptoAtual.Items.Insert(0, new ListItem("Selecione", ""));

            ddlDeptoOrigem.DataValueField = "CO_DEPTO";
            ddlDeptoOrigem.DataTextField = "CO_SIGLA_DEPTO";
            ddlDeptoOrigem.DataBind();
            ddlDeptoOrigem.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que cria o Código do Patrimônio de acordo com o padrão definido
        /// </summary>
        /// <returns>Próximo número do patrimônio</returns>
        private long GerarCodigoPatrimonio()
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append(DateTime.Now.Year.ToString());
            strBuilder.Append(DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString());
            strBuilder.Append(RetornarSeqUnidade(ddlUnidadePatrimonio.SelectedValue.Length));
            strBuilder.Append(ddlTipoPatrimonio.SelectedValue);
            strBuilder.Append(RetornarProximoSeqPatrimonio());

            return int.Parse(strBuilder.ToString());
        }

//====> 
        /// <summary>
        /// Método que retorna o código da Unidade de acordo com o padrão definido
        /// </summary>
        /// <param name="intLengthCoEmp">Id da unidade</param>
        /// <returns>Id da unidade formatado "000"</returns>
        private string RetornarSeqUnidade(int intLengthCoEmp)
        {
            switch (intLengthCoEmp)
            {
                case 1:
                    return "000" + ddlUnidadePatrimonio.SelectedValue;
                case 2:
                    return "00" + ddlUnidadePatrimonio.SelectedValue;
                case 3:
                    return "0" + ddlUnidadePatrimonio.SelectedValue;
            }

            return ddlUnidadePatrimonio.SelectedValue;
        }

        /// <summary>
        /// Método que retorna próximo número sequencial do Patrimônio
        /// </summary>
        /// <returns>Retorna próximo sequencial do patrimônio formatado "0000"</returns>
        private string RetornarProximoSeqPatrimonio()
        {
            var tb212 = TB212_ITENS_PATRIMONIO.RetornaTodosRegistros();
            string strPatr = "0001";

            if (tb212.FirstOrDefault() != null)
            {
                DateTime dataMaior = TB212_ITENS_PATRIMONIO.RetornaTodosRegistros().Max(p => p.DT_CADASTRO);

                var varTb212 = (from lTb212 in TB212_ITENS_PATRIMONIO.RetornaTodosRegistros()
                                     where (lTb212.DT_CADASTRO == dataMaior)
                                     select new { lTb212.COD_PATR }).FirstOrDefault();

                if (varTb212 != null)
                {
                    strPatr = varTb212.COD_PATR.ToString().Substring(11, 4);
                    strPatr = (int.Parse(strPatr) + 1).ToString();
                    int intLengthStrPatr = strPatr.Length;

                    switch (intLengthStrPatr)
                    {
                        case 1:
                            return "000" + strPatr;
                        case 2:
                            return "00" + strPatr;
                        case 3:
                            return "0" + strPatr;
                        case 4:
                            return strPatr;
                    }
                }
            }
            return strPatr;
        }
        #endregion

        protected void ddlTipoPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarDetalhesPatrimonio(int.Parse(ddlTipoPatrimonio.SelectedValue));
            CarregaGrupo();
        }        

        protected void ddlUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }
        
        protected void ddlDeptoAtual_SelectedIndexChanged(object sender, EventArgs e) 
        {

            CarregaUnidadeAtual();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaSubGrupo();
        }

        protected void ddlDeptoOrigem_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaUnidadeOrigem();
        }
    }
}
