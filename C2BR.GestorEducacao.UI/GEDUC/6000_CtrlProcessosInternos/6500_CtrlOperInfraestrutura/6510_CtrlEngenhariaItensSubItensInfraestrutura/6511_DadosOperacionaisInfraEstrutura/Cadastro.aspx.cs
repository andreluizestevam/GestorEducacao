//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE DE MANUTENÇÃO DE INFRA
// SUBMÓDULO: REGISTRO DE NECESSIDADES DE MANUTENÇÃO
// OBJETIVO: DADOS OPERACIONAIS DE INFRAESTRUTURA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6510_CtrlEngenhariaItensSubItensInfraestrutura.F6511_DadosOperacionaisInfraEstrutura
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
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                    AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, Selecione uma Unidade/Escola.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

                //if (QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id) == 0)
                //    AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, Selecione uma Unidade/Escola.", this.AppRelativeVirtualPath.Replace("Cadastro.aspx", "Busca.aspx?moduloNome=" + Request.QueryString["moduloNome"].ToString() + "&"), C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

                CarregaTipoUnidade();
                CarregaTipoOcupacao();
                CarregaTipoTerreno();
                CarregaTipoDelimitacaoTerreno();
                
                lblNomeUnidade.Text = lblNomeUnidade2.Text = TB25_EMPRESA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id)).NO_FANTAS_EMP;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (Page.IsValid)
            {
                decimal decimalRetorno;
                int intRetorno;

                TB160_PERFIL_UNIDADE tb160 = RetornaEntidade();

                if (tb160 == null)
                {
                    tb160 = new TB160_PERFIL_UNIDADE();
                    tb160.CO_EMP = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
                }

                tb160.TB182_TIPO_UNIDA = TB182_TIPO_UNIDA.RetornaPelaChavePrimaria(ddlTipoUnidade.SelectedValue);
                tb160.TB176_TIPO_OCUPA = TB176_TIPO_OCUPA.RetornaPelaChavePrimaria(ddlTipoOcupacao.SelectedValue);
                tb160.CO_FLAG_BIBLI = ddlFlagBiblioteca.SelectedValue;
                tb160.CO_FLAG_ENERG = ddlFlagEnergia.SelectedValue;
                tb160.CO_FLAG_ESPOR = ddlFlagEsporte.SelectedValue;
                tb160.CO_FLAG_ESTAC = ddlFlagEstacionamento.SelectedValue;
                tb160.TB177_TIPO_TERRE = TB177_TIPO_TERRE.RetornaPelaChavePrimaria(ddlTipoTerreno.SelectedValue);
                tb160.TB178_TIPO_DELIM_TERRE = TB178_TIPO_DELIM_TERRE.RetornaPelaChavePrimaria(ddlTipoDelimitTerreno.SelectedValue);
                tb160.QT_TERRE_AREA_TOTAL = decimal.TryParse(txtAreaTotal.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb160.QT_TERRE_AREA_CONST = decimal.TryParse(txtAreaConstruida.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb160.QT_TERRE_AREA_LIVRE = decimal.TryParse(txtAreaLivre.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;                
                tb160.QT_TERRE_AREA_VERDE = decimal.TryParse(txtAreaVerde.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb160.DE_TERRE_LATITU_NUMER = txtLatitude.Text != "" ? txtLatitude.Text : null;
                tb160.DE_TERRE_LATITU_SIGLA = ddlSiglaLatitude.SelectedValue != "" ? ddlSiglaLatitude.SelectedValue : null;
                tb160.DE_TERRE_LONGI_NUMER = txtLongitude.Text != "" ? txtLongitude.Text : null;
                tb160.DE_TERRE_LONGI_SIGLA = ddlSiglaLongitude.SelectedValue != "" ? ddlSiglaLongitude.SelectedValue : null;
                tb160.DE_TERRE_OBSER = txtObservacao.Text != "" ? txtObservacao.Text : null;
                tb160.DE_TERRE_AVALI = txtAvaliacaoTerreno.Text != "" ? txtAvaliacaoTerreno.Text : null;
                tb160.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb160.CO_EMP);
                tb160.TB25_EMPRESA.TB172_SERVICOReference.Load();
                if (tb160.TB25_EMPRESA.TB172_SERVICO == null)
                {
                    tb160.TB25_EMPRESA.TB172_SERVICO = new TB172_SERVICO();
                    tb160.TB25_EMPRESA.TB172_SERVICO.CO_EMP = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
                }
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_ATIVI_COMUN = ddlFlagAtividadeComunitaria.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_COLET_LIXO = ddlFlagColetaLixo.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_CLINI = ddlFlagClinica.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_ODONT = ddlFlagOdonto.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_VIGIA = ddlFlagVigia.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_ARMAR = ddlFlagArmario.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_MEREN = ddlFlagMerenda.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.QT_SERVI_MEREN_CAPAC = int.TryParse(txtCapacidadeMerenda.Text, out intRetorno) ? (int?)intRetorno : null;
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_TRANSP_ESCOL = ddlFlagTranspEscolar.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.QT_SERVI_TRANSP_ESCOL = int.TryParse(txtQtdTranspEscolar.Text, out intRetorno) ? (int?)intRetorno : null;
                tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_TRANSP_FUNCI = ddlFlagTranspFuncional.SelectedValue;
                tb160.TB25_EMPRESA.TB172_SERVICO.QT_SERVI_TRANSP_FUNCI = int.TryParse(txtQtdTranspFuncional.Text, out intRetorno) ? (int?)intRetorno : null;
                
                CurrentPadraoCadastros.CurrentEntity = tb160;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB160_PERFIL_UNIDADE tb160 = RetornaEntidade();

            if (tb160 != null)
            {
                tb160.TB182_TIPO_UNIDAReference.Load();
                tb160.TB176_TIPO_OCUPAReference.Load();
                tb160.TB177_TIPO_TERREReference.Load();
                tb160.TB178_TIPO_DELIM_TERREReference.Load();
                tb160.TB25_EMPRESAReference.Load();
                tb160.TB25_EMPRESA.TB172_SERVICOReference.Load();

                ddlTipoUnidade.SelectedValue = tb160.TB182_TIPO_UNIDA.CO_SIGLA_TIPO_UNIDA;
                ddlTipoOcupacao.SelectedValue = tb160.TB176_TIPO_OCUPA.CO_SIGLA_TIPO_OCUPA;
                ddlFlagBiblioteca.SelectedValue = tb160.CO_FLAG_BIBLI;
                ddlFlagEnergia.SelectedValue = tb160.CO_FLAG_ENERG;
                ddlFlagEsporte.SelectedValue = tb160.CO_FLAG_ESPOR;
                ddlFlagEstacionamento.SelectedValue = tb160.CO_FLAG_ESTAC;
                ddlTipoTerreno.SelectedValue = tb160.TB177_TIPO_TERRE.CO_SIGLA_TIPO_TERRE;
                ddlTipoDelimitTerreno.SelectedValue = tb160.TB178_TIPO_DELIM_TERRE.CO_SIGLA_TIPO_DELIM_TERRE;
                txtAreaConstruida.Text = tb160.QT_TERRE_AREA_CONST != null ? Convert.ToDecimal(tb160.QT_TERRE_AREA_CONST).ToString("#,###.00") : "";
                txtAreaLivre.Text = tb160.QT_TERRE_AREA_LIVRE != null ? Convert.ToDecimal(tb160.QT_TERRE_AREA_LIVRE).ToString("#,###.00") : "";
                txtAreaTotal.Text = tb160.QT_TERRE_AREA_TOTAL != null ? Convert.ToDecimal(tb160.QT_TERRE_AREA_TOTAL).ToString("#,###.00") : "";
                txtAreaVerde.Text = tb160.QT_TERRE_AREA_VERDE != null ? Convert.ToDecimal(tb160.QT_TERRE_AREA_VERDE).ToString("#,###.00") : "";
                txtAvaliacaoTerreno.Text = tb160.DE_TERRE_AVALI;
                txtLatitude.Text = tb160.DE_TERRE_LATITU_NUMER;
                ddlSiglaLatitude.SelectedValue = tb160.DE_TERRE_LATITU_SIGLA;
                txtLongitude.Text = tb160.DE_TERRE_LONGI_NUMER;
                ddlSiglaLongitude.SelectedValue = tb160.DE_TERRE_LONGI_SIGLA;
                txtObservacao.Text = tb160.DE_TERRE_OBSER;
                ddlFlagAtividadeComunitaria.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_ATIVI_COMUN;
                ddlFlagColetaLixo.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_COLET_LIXO;
                ddlFlagClinica.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_CLINI;
                ddlFlagOdonto.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_ODONT;
                ddlFlagTranspEscolar.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_TRANSP_ESCOL;
                txtQtdTranspEscolar.Text = tb160.TB25_EMPRESA.TB172_SERVICO.QT_SERVI_TRANSP_ESCOL.ToString();
                ddlFlagTranspFuncional.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_TRANSP_FUNCI;
                txtQtdTranspFuncional.Text = tb160.TB25_EMPRESA.TB172_SERVICO.QT_SERVI_TRANSP_FUNCI.ToString();
                ddlFlagMerenda.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_MEREN;
                txtCapacidadeMerenda.Text = tb160.TB25_EMPRESA.TB172_SERVICO.QT_SERVI_MEREN_CAPAC.ToString();
                ddlFlagVigia.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_VIGIA;
                ddlFlagArmario.SelectedValue = tb160.TB25_EMPRESA.TB172_SERVICO.CO_SERVI_ARMAR;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB160_PERFIL_UNIDADE</returns>
        private TB160_PERFIL_UNIDADE RetornaEntidade()
        {
            return TB160_PERFIL_UNIDADE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Unidade
        /// </summary>
        private void CarregaTipoUnidade()
        {
            ddlTipoUnidade.DataSource = (from tb182 in TB182_TIPO_UNIDA.RetornaTodosRegistros()
                                         select new { tb182.DE_TIPO_UNIDA, tb182.CO_SIGLA_TIPO_UNIDA });

            ddlTipoUnidade.DataTextField = "DE_TIPO_UNIDA";
            ddlTipoUnidade.DataValueField = "CO_SIGLA_TIPO_UNIDA";
            ddlTipoUnidade.DataBind();

            ddlTipoUnidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Ocupação
        /// </summary>
        private void CarregaTipoOcupacao()
        {
            ddlTipoOcupacao.DataSource = (from tb176 in TB176_TIPO_OCUPA.RetornaTodosRegistros()
                                          select new { tb176.CO_SIGLA_TIPO_OCUPA, tb176.DE_TIPO_OCUPA });

            ddlTipoOcupacao.DataTextField = "DE_TIPO_OCUPA";
            ddlTipoOcupacao.DataValueField = "CO_SIGLA_TIPO_OCUPA";
            ddlTipoOcupacao.DataBind();

            ddlTipoOcupacao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Terreno
        /// </summary>
        private void CarregaTipoTerreno()
        {
            ddlTipoTerreno.DataSource = (from tb177 in TB177_TIPO_TERRE.RetornaTodosRegistros()
                                         select new { tb177.CO_SIGLA_TIPO_TERRE, tb177.DE_TIPO_TERRE });

            ddlTipoTerreno.DataTextField = "DE_TIPO_TERRE";
            ddlTipoTerreno.DataValueField = "CO_SIGLA_TIPO_TERRE";
            ddlTipoTerreno.DataBind();

            ddlTipoTerreno.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo Delimitação do Terreno
        /// </summary>
        private void CarregaTipoDelimitacaoTerreno()
        {
            ddlTipoDelimitTerreno.DataSource = (from tb178 in TB178_TIPO_DELIM_TERRE.RetornaTodosRegistros()
                                                select new { tb178.CO_SIGLA_TIPO_DELIM_TERRE, tb178.DE_TIPO_DELIM_TERRE });

            ddlTipoDelimitTerreno.DataTextField = "DE_TIPO_DELIM_TERRE";
            ddlTipoDelimitTerreno.DataValueField = "CO_SIGLA_TIPO_DELIM_TERRE";
            ddlTipoDelimitTerreno.DataBind();

            ddlTipoDelimitTerreno.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion
    }
}