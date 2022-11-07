//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE DE TABELAS DE DADOS DA SOLUÇÃO GOVSAÚDE
// SUBMÓDULO: TABELAS DE APOIO - ITENS DE SAÚDE
// DATA DE CRIAÇÃO: 27/10/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+------------------------------------
// 27/10/2014| Maxwell Almeida            |  Criação da funcionalidade para Cadastro de Vacinas
// 02/05/2021| Filipe Rodrigues Gomes     |  Evolução da funcionalidade
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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9200_CtrlVacinas._9210_CtrlCadastral._9201_CadastroVacinas
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
            if (!IsPostBack)
            {
                txtDtCadas.Text =
                txtDtSitua.Text = DateTime.Now.ToString();

                CarregaUnidades();
                CarregaPaisesProcedencia();
                CarregaLaboratoriosFornecedores();
                CarregaViaAplicacao();
                CarregaLocalAplicacao();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome da Vacina é Requerida");
                return;
            }

            TBS345_VACINA tbs345 = RetornaEntidade();

            tbs345.NM_VACINA = txtNome.Text;
            tbs345.CO_SIGLA_VACINA = (!string.IsNullOrEmpty(txtSigla.Text) ? txtSigla.Text : null);
            tbs345.DE_VACINA = (!string.IsNullOrEmpty(txtDescricao.Text) ? txtDescricao.Text : null);
            tbs345.CO_IP_CADAS = Request.UserHostAddress;

            tbs345.TB41_FORNEC = !string.IsNullOrEmpty(drpLaboratorio.SelectedValue) ? TB41_FORNEC.RetornaPelaChavePrimaria(int.Parse(drpLaboratorio.SelectedValue)) : null;
            tbs345.ST_ORIG_FABRIC = !string.IsNullOrEmpty(drpFabricacao.SelectedValue) ? drpFabricacao.SelectedValue : null;
            tbs345.TB299_PAISES = !string.IsNullOrEmpty(drpProcedencia.SelectedValue) ? TB299_PAISES.RetornaPelaChavePrimaria(drpProcedencia.SelectedValue) : null;
            tbs345.NU_COD_BARR_LABOR = txtCodigoBarrasLaboratorio.Text;
            tbs345.NU_COD_BARR_ESTOQ = txtCodigoBarrasEstoque.Text;
            tbs345.NU_COD_BARR_ARMAZ = txtCodigoBarrasArmazenagem.Text;

            tbs345.QT_DOSES = !string.IsNullOrEmpty(txtQtDoses.Text) ? decimal.Parse(txtQtDoses.Text) : (decimal?)null;
            tbs345.NU_CAPCID_ML = !string.IsNullOrEmpty(txtCapacidade.Text) ? int.Parse(txtCapacidade.Text) : (int?)null;
            tbs345.CD_ANVISA = txtCodigoANVISA.Text;
            tbs345.CD_MSSUS = txtCodigoMSSUS.Text;
            tbs345.QT_DOSES_ARMZEN = !string.IsNullOrEmpty(txtQtdDosesArmazenagem.Text) ? decimal.Parse(txtQtdDosesArmazenagem.Text) : (decimal?)null;
            tbs345.DE_EMPILHAMENTO = txtEmpilhamento.Text;
            tbs345.DE_TEMP_MEDIA = txtTemperaturaMedia.Text;
            tbs345.DE_OBSERVACOES = txtObservacoes.Text;

            tbs345.TB89_UNIDADES = !string.IsNullOrEmpty(drpTipoEmbalagem.SelectedValue) ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(drpTipoEmbalagem.SelectedValue)) : null;
            tbs345.TB89_UNIDADES1 = !string.IsNullOrEmpty(drpUnidadeItem.SelectedValue) ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(drpUnidadeItem.SelectedValue)) : null;
            tbs345.TB89_UNIDADES2 = !string.IsNullOrEmpty(drpTipoArmazenagem.SelectedValue) ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(drpTipoArmazenagem.SelectedValue)) : null;
            tbs345.TB89_UNIDADES3 = !string.IsNullOrEmpty(drpUnidadeArmazenagem.SelectedValue) ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(drpUnidadeArmazenagem.SelectedValue)) : null;

            tbs345.TBS476_VIA_APLIC = !string.IsNullOrEmpty(drpViaAplicacao.SelectedValue) ? TBS476_VIA_APLIC.RetornaPelaChavePrimaria(int.Parse(drpViaAplicacao.SelectedValue)) : null;
            tbs345.TBS477_LOCAL_APLIC = !string.IsNullOrEmpty(drpLocalAplicacao.SelectedValue) ? TBS477_LOCAL_APLIC.RetornaPelaChavePrimaria(int.Parse(drpLocalAplicacao.SelectedValue)) : null;

            tbs345.QT_DOSES_APLIC = !string.IsNullOrEmpty(drpQtdDosesAplicacao.SelectedValue) ? int.Parse(drpQtdDosesAplicacao.SelectedValue) : (int?)null;
            tbs345.DE_INTRVL_APLIC = txtIntervaloAplicacao.Text;

            int codImagem = upImageVacina.GravaImagem();
            tbs345.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);

            //Salva essas informações apenas quando a situação tiver sido alterada
            if (hidCoSitua.Value != drpSituacao.SelectedValue)
            {
                tbs345.CO_SITUA_VACINA = drpSituacao.SelectedValue;
                tbs345.CO_COL_SITUA = LoginAuxili.CO_COL;
            }

            //Salva essas informações apenas quando for cadastro novo
            switch (tbs345.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs345.DT_CADAS = DateTime.Now;
                    tbs345.CO_COL_CADAS = LoginAuxili.CO_COL;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs345;
        }

        protected void drpLaboratorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoLaboratorio.Text = TB41_FORNEC.RetornaPelaChavePrimaria(int.Parse(((DropDownList)sender).SelectedValue)).CO_CPFCGC_FORN;
        }

        protected void drpViaAplicacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaLocalAplicacao();
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações
        /// </summary>
        private void CarregaFormulario()
        {
            upImageVacina.CarregaImagem(0);

            TBS345_VACINA tbs345 = RetornaEntidade();

            if (tbs345 != null)
            {
                tbs345.TB41_FORNECReference.Load();
                tbs345.TB299_PAISESReference.Load();
                tbs345.TB89_UNIDADESReference.Load();
                tbs345.TB89_UNIDADES1Reference.Load();
                tbs345.TB89_UNIDADES2Reference.Load();
                tbs345.TB89_UNIDADES3Reference.Load();
                tbs345.TBS476_VIA_APLICReference.Load();
                tbs345.TBS477_LOCAL_APLICReference.Load();

                hidCoSitua.Value = drpSituacao.SelectedValue = tbs345.CO_SITUA_VACINA;
                txtNome.Text = tbs345.NM_VACINA;
                txtSigla.Text = tbs345.CO_SIGLA_VACINA;
                txtDescricao.Text = tbs345.DE_VACINA;
                txtDtCadas.Text = tbs345.DT_CADAS.ToString();
                txtDtSitua.Text = tbs345.DT_SITUA.ToString();

                drpLaboratorio.SelectedValue = tbs345.TB41_FORNEC != null ? tbs345.TB41_FORNEC.CO_FORN.ToString() : "";
                txtCodigoLaboratorio.Text = tbs345.TB41_FORNEC != null ? tbs345.TB41_FORNEC.CO_CPFCGC_FORN : "";
                drpFabricacao.SelectedValue = !string.IsNullOrEmpty(tbs345.ST_ORIG_FABRIC) ? tbs345.ST_ORIG_FABRIC : "";
                drpProcedencia.SelectedValue = tbs345.TB299_PAISES != null ? tbs345.TB299_PAISES.CO_ISO_PAISES.ToString() : "";
                txtCodigoBarrasLaboratorio.Text = tbs345.NU_COD_BARR_LABOR;
                txtCodigoBarrasEstoque.Text = tbs345.NU_COD_BARR_ESTOQ;
                txtCodigoBarrasArmazenagem.Text = tbs345.NU_COD_BARR_ARMAZ;

                txtQtDoses.Text = tbs345.QT_DOSES.ToString() ?? "";
                txtCapacidade.Text = tbs345.NU_CAPCID_ML.ToString() ?? "";
                txtCodigoANVISA.Text = tbs345.CD_ANVISA;
                txtCodigoMSSUS.Text = tbs345.CD_MSSUS;
                txtQtdDosesArmazenagem.Text = tbs345.QT_DOSES_ARMZEN.ToString() ?? "";
                txtEmpilhamento.Text = tbs345.DE_EMPILHAMENTO;
                txtTemperaturaMedia.Text = tbs345.DE_TEMP_MEDIA;
                txtObservacoes.Text = tbs345.DE_OBSERVACOES;

                drpTipoEmbalagem.SelectedValue = tbs345.TB89_UNIDADES != null ? tbs345.TB89_UNIDADES.CO_UNID_ITEM.ToString() : "";
                drpUnidadeItem.SelectedValue = tbs345.TB89_UNIDADES1 != null ? tbs345.TB89_UNIDADES1.CO_UNID_ITEM.ToString() : "";
                drpTipoArmazenagem.SelectedValue = tbs345.TB89_UNIDADES2 != null ? tbs345.TB89_UNIDADES2.CO_UNID_ITEM.ToString() : "";
                drpUnidadeArmazenagem.SelectedValue = tbs345.TB89_UNIDADES3 != null ? tbs345.TB89_UNIDADES3.CO_UNID_ITEM.ToString() : "";

                drpViaAplicacao.SelectedValue = tbs345.TBS476_VIA_APLIC != null ? tbs345.TBS476_VIA_APLIC.ID_VIA_APLIC.ToString() : "";

                CarregaLocalAplicacao();
                if (tbs345.TBS477_LOCAL_APLIC != null && drpLocalAplicacao.Items.FindByValue(tbs345.TBS477_LOCAL_APLIC.ID_LOCAL_APLIC.ToString()) != null)
                    drpLocalAplicacao.SelectedValue = tbs345.TBS477_LOCAL_APLIC.ID_LOCAL_APLIC.ToString();

                drpQtdDosesAplicacao.SelectedValue = tbs345.QT_DOSES_APLIC.HasValue ? tbs345.QT_DOSES_APLIC.Value.ToString() : "";
                txtIntervaloAplicacao.Text = tbs345.DE_INTRVL_APLIC;

                upImageVacina.ImagemLargura = 70;
                upImageVacina.ImagemAltura = 85;

                if (tbs345.Image != null)
                    upImageVacina.CarregaImagem(tbs345.Image.ImageId);
                else
                    upImageVacina.CarregaImagem(0);
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS339_CAMPSAUDE</returns>
        private TBS345_VACINA RetornaEntidade()
        {
            TBS345_VACINA tbs345 = TBS345_VACINA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs345 == null) ? new TBS345_VACINA() : tbs345;
        }

        /// <summary>
        /// Carrega os fornecedores
        /// </summary>
        private void CarregaLaboratoriosFornecedores()
        {
            var res = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                       where tb41.TB24_TPEMPRESA.CO_TIPOEMP == 34
                       select new { tb41.DE_RAZSOC_FORN, tb41.CO_FORN }).ToList().OrderBy(w => w.DE_RAZSOC_FORN);

            drpLaboratorio.DataTextField = "DE_RAZSOC_FORN";
            drpLaboratorio.DataValueField = "CO_FORN";
            drpLaboratorio.DataSource = res;
            drpLaboratorio.DataBind();

            drpLaboratorio.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Procedencia
        /// </summary>
        private void CarregaPaisesProcedencia()
        {
            drpProcedencia.DataSource = TB299_PAISES.RetornaTodosRegistros();

            drpProcedencia.DataTextField = "NO_PAISES";
            drpProcedencia.DataValueField = "CO_ISO_PAISES";
            drpProcedencia.DataBind();

            drpProcedencia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega os dropdowns de Unidade
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidadesMedidas(drpTipoEmbalagem, false, true, "V");
            AuxiliCarregamentos.CarregaUnidadesMedidas(drpUnidadeItem, false, true, "V");
            AuxiliCarregamentos.CarregaUnidadesMedidas(drpTipoArmazenagem, false, true, "V");
            AuxiliCarregamentos.CarregaUnidadesMedidas(drpUnidadeArmazenagem, false, true, "V");
        }

        private void CarregaViaAplicacao()
        {
            drpViaAplicacao.DataSource = TBS476_VIA_APLIC.RetornaTodosRegistros();

            drpViaAplicacao.DataTextField = "NO_VIA_APLIC";
            drpViaAplicacao.DataValueField = "ID_VIA_APLIC";
            drpViaAplicacao.DataBind();

            drpViaAplicacao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaLocalAplicacao()
        {
            drpLocalAplicacao.Items.Clear();
            
            var viaAplicacao = !string.IsNullOrEmpty(drpViaAplicacao.SelectedValue) ? int.Parse(drpViaAplicacao.SelectedValue) : 0;

            var res = (from tbs477 in TBS477_LOCAL_APLIC.RetornaTodosRegistros()
                       where (viaAplicacao != 0 ? tbs477.TBS476_VIA_APLIC.ID_VIA_APLIC == viaAplicacao : tbs477.TBS476_VIA_APLIC == null)
                       || tbs477.TBS476_VIA_APLIC == null
                       select new { tbs477.NO_LOCAL_APLIC, tbs477.ID_LOCAL_APLIC }).ToList().OrderBy(w => w.ID_LOCAL_APLIC);

            drpLocalAplicacao.DataTextField = "NO_LOCAL_APLIC";
            drpLocalAplicacao.DataValueField = "ID_LOCAL_APLIC";
            drpLocalAplicacao.DataSource = res;
            drpLocalAplicacao.DataBind();

            drpLocalAplicacao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion
    }
}