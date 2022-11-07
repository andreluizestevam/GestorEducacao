//==================================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: SÁÚDE
// SUBMÓDULO: Cadastramento de Família
// OBJETIVO: Cadastramento de Família
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+------------------------------------------
// 03/03/2014| Vinícius Reis              | Criação da tela de cadastro de famílias.
//           |                            | 
//           |                            | 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Configuration;
using Artem.Web.UI.Controls;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;

namespace C2BR.GestorEducacao.UI.GSAUD._2000_ControleInformacoesFamilia._2100_ControleInformacoesCadastraisFamilia._2101_CadastroFamilia
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);
        }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (CurrentPadraoCadastros.BarraCadastro.AcaoSolicitadaClique == CurrentPadraoCadastros.BarraCadastro.botaoDelete)
            {
                excluirFamilia();
                return;
            }

            if (!ValidaCamposFamilia())
                return;

            TB075_FAMILIA tb075 = RetornaEntidade();

            DateTime dataRetorno = DateTime.Now;
            decimal decimalRetorno = 0;
            int intRetorno = 0;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var count = TB075_FAMILIA.RetornaTodosRegistros().Count();

                string coFamilia = "F" + (count + 1).ToString();

                tb075.CO_FAMILIA = coFamilia;
            }


            tb075.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
            tb075.CO_CEP_FAM = txtCep.Text.Replace("-", "");
            tb075.CO_ESTA_FAM = ddlUf.SelectedValue != "" ? ddlUf.SelectedValue : null;
            tb075.CO_FONTE_ENERG = ddlEnergiaEletrica.SelectedValue != "" ? int.Parse(ddlEnergiaEletrica.SelectedValue) : new Nullable<int>();
            tb075.CO_GEORE_LATIT_FAM = !string.IsNullOrEmpty(txtLatitude.Text) ? txtLatitude.Text : "";
            tb075.CO_GEORE_LONGI_FAM = !string.IsNullOrEmpty(txtLongitude.Text) ? txtLongitude.Text : "";
            tb075.CO_MUNIC_IBGE = !string.IsNullOrEmpty(txtCodMun.Text) ? txtCodMun.Text : "";
            tb075.CO_ORIGE_AGUA = ddlAbastAgua.SelectedValue != "" ? int.Parse(ddlAbastAgua.SelectedValue) : new Nullable<int>();
            tb075.CO_REDE_ESGOT = ddlRedeEsgoto.SelectedValue != "" ? int.Parse(ddlRedeEsgoto.SelectedValue) : new Nullable<int>();
            tb075.CO_TIPO_COBER = ddlTipoCobertura.SelectedValue != "" ? int.Parse(ddlTipoCobertura.SelectedValue) : new Nullable<int>();
            tb075.CO_TIPO_DELIM_TERRE = ddlTipoDelim.SelectedValue != "" ? int.Parse(ddlTipoDelim.SelectedValue) : new Nullable<int>();
            tb075.CO_TIPO_OCUPA = ddlTipoOcup.SelectedValue != "" ? int.Parse(ddlTipoOcup.SelectedValue) : new Nullable<int>();
            tb075.CO_TIPO_TERRE = ddlTipoTerreno.SelectedValue != "" ? int.Parse(ddlTipoTerreno.SelectedValue) : new Nullable<int>();
            tb075.CO_TIPO_TERRE = ddlTipoTerreno.SelectedValue != "" ? int.Parse(ddlTipoTerreno.SelectedValue) : new Nullable<int>();
            tb075.DE_COMP_FAM = !string.IsNullOrEmpty(txtComplemento.Text) ? txtComplemento.Text : "";
            tb075.DE_ENDE_FAM = !string.IsNullOrEmpty(txtLogradouro.Text) ? txtLogradouro.Text : "";
            tb075.DE_OBS_FAM = !string.IsNullOrEmpty(txtObs.Text) ? txtObs.Text : "";
            tb075.DT_CAD_FAM = dataRetorno;
            tb075.DT_SITUACAO = DateTime.TryParse(txtDataSituacao.Text, out dataRetorno) ? (DateTime?)dataRetorno : new Nullable<DateTime>();
            tb075.FL_AREA_RISCO = chkAreaRisco.Checked;
            tb075.FL_STATUS = ddlSituacao.SelectedValue != "" ? ddlSituacao.SelectedValue : "";
            tb075.NO_RESP_FAM = !string.IsNullOrEmpty(txtNome.Text) ? txtNome.Text : "";
            tb075.NU_AREA_EDIF_FAM = decimal.TryParse(txtAreaEdificada.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb075.NU_AREA_TERRE_FAM = decimal.TryParse(txtAreaTerreno.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb075.NU_CPF_RESP_FAM = txtCpf.Text.Replace("-", "").Replace(".", "");
            tb075.NU_ENDE_FAM = int.TryParse(txtNumero.Text, out intRetorno) ? (int?)intRetorno : null;
            tb075.NU_TELE_RESI_FAM = txtTelResidencial.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            tb075.QT_BANHEIROS = int.TryParse(txtBanheiros.Text, out intRetorno) ? (int?)intRetorno : null;
            tb075.QT_PESSOA_0_05 = int.TryParse(txtQtd0005.Text, out intRetorno) ? (int?)intRetorno : null;
            tb075.QT_PESSOA_06_12 = int.TryParse(txtQtd0612.Text, out intRetorno) ? (int?)intRetorno : null;
            tb075.QT_PESSOA_13_18 = int.TryParse(txtQtd1318.Text, out intRetorno) ? (int?)intRetorno : null;
            tb075.QT_PESSOA_18M = int.TryParse(txtQtd18m.Text, out intRetorno) ? (int?)intRetorno : null;
            tb075.QT_QUARTOS = int.TryParse(txtQuartos.Text, out intRetorno) ? (int?)intRetorno : null;
            tb075.QT_RENDA_ATIVO = int.TryParse(txtQtdRenda.Text, out intRetorno) ? (int?)intRetorno : null;
            tb075.RENDA_FAM = ddlRendaFamiliar.SelectedValue != "" ? ddlRendaFamiliar.SelectedValue : "";
            tb075.CO_ESTA_ORIGEM = ddlOrigem.SelectedValue != "" ? ddlOrigem.SelectedValue : null;

            tb075.FL_AREA_RISCO = chkAreaRisco.Checked;

            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;
            int coArea = ddlArea.SelectedValue != "" ? int.Parse(ddlArea.SelectedValue) : 0;
            int coRegiao = ddlRegiao.SelectedValue != "" ? int.Parse(ddlRegiao.SelectedValue) : 0;
            int coSubArea = ddlSubArea.SelectedValue != "" ? int.Parse(ddlSubArea.SelectedValue) : 0;

            tb075.TB904_CIDADE = TB904_CIDADE.RetornaPelaChavePrimaria(coCidade);
            tb075.TB906_REGIAO = TB906_REGIAO.RetornaPelaChavePrimaria(coRegiao);
            tb075.TB907_AREA = TB907_AREA.RetornaPelaChavePrimaria(coArea);
            tb075.TB908_SUBAREA = TB908_SUBAREA.RetornaPelaChavePrimaria(coSubArea);

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                if (TB075_FAMILIA.Delete(tb075, true) > 0)
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Excluido com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                ///Quando for inclusão deve primeiro incluir o aluno para depois salvar os registros de Instituições de Apoio e Programas Sociais
                if (GestorEntities.SaveOrUpdate(tb075) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar registro.");
                    return;
                }
                CurrentPadraoCadastros.CurrentEntity = tb075;
            }
            else if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {

                if (TB075_FAMILIA.SaveOrUpdate(tb075, true) > 0)
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Alterado com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

                CurrentPadraoCadastros.CurrentEntity = tb075;
            }

        }

        private bool ValidaCamposFamilia()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                ///Faz a verificação de ocorrência de aluno pelo nome, sexo e data de nascimento ( quando inclusão )
                var ocorrFamilia = from lTb075 in TB075_FAMILIA.RetornaTodosRegistros()
                                   where lTb075.NO_RESP_FAM == txtNome.Text && lTb075.NU_CPF_RESP_FAM == txtCep.Text
                                   select new { lTb075.CO_FAMILIA };

                if (ocorrFamilia.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Responsável pela família já cadastrado no sistema.");
                    return false;
                }
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                string coFamilia = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id);

                ///Faz a verificação de ocorrência de aluno pelo nome, sexo e data de nascimento ( diferente do aluno informado na alteração )
                var ocorrFamilia = from lTb075 in TB075_FAMILIA.RetornaTodosRegistros()
                                   where lTb075.NO_RESP_FAM == txtNome.Text && lTb075.NU_CPF_RESP_FAM == txtCep.Text && lTb075.CO_FAMILIA != coFamilia
                                   select new { lTb075.CO_FAMILIA };

                if (ocorrFamilia.Count() > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Responsável pela família já cadastrado no sistema.");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtNome.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Campo Nome é obrigatório.");
                txtNome.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCep.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Campo CEP é obrigatório.");
                txtCep.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(ddlUf.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Campo UF é obrigatório.");
                ddlUf.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(ddlCidade.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Campo Cidade é obrigatório.");
                ddlCidade.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(ddlBairro.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Campo Bairro é obrigatório.");
                ddlBairro.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtLogradouro.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Campo Logradouro é obrigatório.");
                txtLogradouro.Focus();
                return false;
            }

            return true;
        }

        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            excluirFamilia();
        }

        private void excluirFamilia()
        {
            bool excluido = false;
            try
            {
                TB075_FAMILIA tb07 = RetornaEntidade();

                if (tb07 != null)
                {
                    if (GestorEntities.Delete(tb07) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                }
                excluido = true;
            }
            catch
            {
                excluido = false;
            }
            if (excluido)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Família excluída com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            else
                AuxiliPagina.RedirecionaParaPaginaErro("Não foi possível excluir a familia solicitado.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUfs(ddlUf);
                CarregaUfs(ddlOrigem);
                CarregaTipoOcupacao(ddlTipoOcup);
                CarregaTipoTerreno(ddlTipoTerreno);
                CarregaTipoDelimitacao(ddlTipoDelim);
                CarregaTipoCobertura(ddlTipoCobertura);
                CarregaRedeEsgoto(ddlRedeEsgoto);
                CarregaEnergiaEletrica(ddlEnergiaEletrica);
                CarregaAbastAgua(ddlAbastAgua);
                CarregaInstituicoes();
                CarregaProgramas();
                CarregaRegiao();
                CarregaArea();
                CarregaSubArea();

                //CarregaFormulario();
            }
        }

        /// <summary>
        /// Preenche os campos de endereço do responsável de acordo com o CEP, se o mesmo possuir registro na base de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesqCEP_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCep.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCep.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouro.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUf.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades(ddlCidade, ddlUf);
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros(ddlBairro, ddlCidade);
                    ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouro.Text = "";
                    ddlBairro.SelectedValue = "";
                    ddlCidade.SelectedValue = "";
                    ddlUf.SelectedValue = "";
                }
            }
        }
        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades(ddlCidade, ddlUf);
            CarregaBairros(ddlBairro, ddlCidade);
        }
        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros(ddlBairro, ddlCidade);
        }

        protected void ddlRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaArea();
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubArea();
        }

        #endregion

        #region Carregamentos

        private void CarregaFormulario()
        {

            TB075_FAMILIA tb075 = RetornaEntidade();

            if (tb075 != null)
            {
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
                {
                    tb075.TB904_CIDADEReference.Load();
                    tb075.TB906_REGIAOReference.Load();
                    tb075.TB907_AREAReference.Load();
                    tb075.TB908_SUBAREAReference.Load();
                }
                chkAreaRisco.Checked = tb075.FL_AREA_RISCO != null ? tb075.FL_AREA_RISCO.Value : false;
                ddlAbastAgua.SelectedValue = tb075.CO_ORIGE_AGUA != null ? tb075.CO_ORIGE_AGUA.Value.ToString() : "";
                ddlBairro.SelectedValue = tb075.CO_BAIRRO != null ? tb075.CO_BAIRRO.Value.ToString() : "";
                ddlEnergiaEletrica.SelectedValue = tb075.CO_FONTE_ENERG != null ? tb075.CO_FONTE_ENERG.Value.ToString() : "";
                ddlRedeEsgoto.SelectedValue = tb075.CO_REDE_ESGOT != null ? tb075.CO_REDE_ESGOT.Value.ToString() : "";
                ddlRendaFamiliar.SelectedValue = tb075.RENDA_FAM != null ? tb075.RENDA_FAM.ToString() : "";
                ddlTipoCobertura.SelectedValue = tb075.CO_TIPO_COBER != null ? tb075.CO_TIPO_COBER.Value.ToString() : "";
                ddlTipoDelim.SelectedValue = tb075.CO_TIPO_DELIM_TERRE != null ? tb075.CO_TIPO_DELIM_TERRE.Value.ToString() : "";
                ddlTipoOcup.SelectedValue = tb075.CO_TIPO_OCUPA != null ? tb075.CO_TIPO_OCUPA.Value.ToString() : "";
                ddlTipoTerreno.SelectedValue = tb075.CO_TIPO_TERRE != null ? tb075.CO_TIPO_TERRE.Value.ToString() : "";
                ddlUf.SelectedValue = tb075.CO_ESTA_FAM != null ? tb075.CO_ESTA_FAM.ToString() : "";
                ddlOrigem.SelectedValue = tb075.CO_ESTA_ORIGEM != null ? tb075.CO_ESTA_ORIGEM.ToString() : "";
                CarregaCidades(ddlCidade, ddlUf);
                ddlCidade.SelectedValue = tb075.TB904_CIDADE != null ? tb075.TB904_CIDADE.CO_CIDADE.ToString() : "";
                CarregaBairros(ddlBairro, ddlCidade);
                ddlRegiao.SelectedValue = tb075.TB906_REGIAO != null ? tb075.TB906_REGIAO.ID_REGIAO.ToString() : "";
                CarregaArea();
                ddlArea.SelectedValue = tb075.TB907_AREA != null ? tb075.TB907_AREA.ID_AREA.ToString() : "";
                CarregaSubArea();
                ddlSubArea.SelectedValue = tb075.TB908_SUBAREA != null ? tb075.TB908_SUBAREA.ID_SUBAREA.ToString() : "";
                ddlSituacao.SelectedValue = tb075.FL_STATUS != null ? tb075.FL_STATUS : "";

                txtCodigo.Text = tb075.CO_FAMILIA != null ? tb075.CO_FAMILIA : "";
                txtAreaEdificada.Text = tb075.NU_AREA_EDIF_FAM != null ? tb075.NU_AREA_EDIF_FAM.Value.ToString() : "";
                txtAreaTerreno.Text = tb075.NU_AREA_TERRE_FAM != null ? tb075.NU_AREA_TERRE_FAM.Value.ToString() : "";
                txtBanheiros.Text = tb075.QT_BANHEIROS != null ? tb075.QT_BANHEIROS.Value.ToString() : "";
                txtCep.Text = tb075.CO_CEP_FAM != null ? tb075.CO_CEP_FAM : "";
                txtCodMun.Text = tb075.CO_MUNIC_IBGE != null ? tb075.CO_MUNIC_IBGE : "";
                txtComplemento.Text = tb075.DE_COMP_FAM != null ? tb075.DE_COMP_FAM : "";
                txtCpf.Text = tb075.NU_CPF_RESP_FAM != null ? tb075.NU_CPF_RESP_FAM : "";
                txtDataCadastro.Text = tb075.DT_CAD_FAM != null ? tb075.DT_CAD_FAM.ToString("dd/MM/yyyy") : "";
                txtLogradouro.Text = tb075.DE_ENDE_FAM != null ? tb075.DE_ENDE_FAM : "";

                txtLatitude.Text = tb075.CO_GEORE_LATIT_FAM != null ? tb075.CO_GEORE_LATIT_FAM : "";
                txtLongitude.Text = tb075.CO_GEORE_LONGI_FAM != null ? tb075.CO_GEORE_LONGI_FAM : "";
                txtNome.Text = tb075.NO_RESP_FAM != null ? tb075.NO_RESP_FAM : "";
                txtNumero.Text = tb075.NU_ENDE_FAM != null ? tb075.NU_ENDE_FAM.Value.ToString() : "";
                txtObs.Text = tb075.DE_OBS_FAM != null ? tb075.DE_OBS_FAM : "";
                txtQtd0005.Text = tb075.QT_PESSOA_0_05 != null ? tb075.QT_PESSOA_0_05.Value.ToString() : "";
                txtQtd0612.Text = tb075.QT_PESSOA_06_12 != null ? tb075.QT_PESSOA_06_12.Value.ToString() : "";
                txtQtd1318.Text = tb075.QT_PESSOA_13_18 != null ? tb075.QT_PESSOA_13_18.Value.ToString() : "";
                txtQtd18m.Text = tb075.QT_PESSOA_18M != null ? tb075.QT_PESSOA_18M.Value.ToString() : "";
                txtQtdRenda.Text = tb075.QT_RENDA_ATIVO != null ? tb075.QT_RENDA_ATIVO.Value.ToString() : "";
                txtDataSituacao.Text = tb075.DT_SITUACAO != null ? tb075.DT_SITUACAO.Value.ToString("dd/MM/yyyy") : "";

                txtQuartos.Text = tb075.QT_QUARTOS != null ? tb075.QT_QUARTOS.Value.ToString() : "";
                txtTelResidencial.Text = tb075.NU_TELE_RESI_FAM != null ? tb075.NU_TELE_RESI_FAM : "";

                if (tb075.DE_ENDE_FAM != null)
                {
                    string descEnd = tb075.DE_ENDE_FAM + ",";
                    descEnd = tb075.NU_ENDE_FAM != null ? descEnd + tb075.NU_ENDE_FAM.ToString() + "," : descEnd;
                    carregaMapa(descEnd + ddlCidade.SelectedItem + " - " + tb075.CO_ESTA_FAM);
                }
                else
                    tbMap.Visible = false;
            }
        }

        private TB075_FAMILIA RetornaEntidade()
        {
            TB075_FAMILIA tb075 = TB075_FAMILIA.RetornaPeloCoFamilia(QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id));
            return (tb075 == null) ? new TB075_FAMILIA() : tb075;
        }

        private void carregaMapa(string endAluno)
        {
            if (AuxiliValidacao.IsConnected())
            {
                ///ATIVA MAP GOOGLE PARA ATIVAR COLOCAR COMO TRUE 
                tbMap.Visible = true;

                ///Chave GoogleMaps
                GMapa.Key = ConfigurationManager.AppSettings.Get(AppSettings.GoogleMapsKey);
                GMapa.Address = endAluno;
                GMapa.Markers.Clear();
                GMapa.Markers.Add(new GoogleMarker(endAluno));

                GMapa.Zoom = 15;
            }
        }

        //====> Método que carrega o DropDown de região
        private void CarregaRegiao()
        {
            ddlRegiao.DataSource = TB906_REGIAO.RetornaTodosRegistros().OrderBy(r => r.ID_REGIAO);

            ddlRegiao.DataTextField = "NM_REGIAO";
            ddlRegiao.DataValueField = "ID_REGIAO";
            ddlRegiao.DataBind();

            ddlRegiao.Items.Insert(0, new ListItem("", ""));
            UpdatePanel1.Update();
        }

        //====> Método que carrega o DropDown de Area
        private void CarregaArea()
        {
            int idRegiao = ddlRegiao.SelectedValue != "" ? int.Parse(ddlRegiao.SelectedValue) : 0;

            if (idRegiao != 0)
            {
                ddlArea.DataSource = TB907_AREA.RetornaPelaRegiao(idRegiao);

                ddlArea.DataTextField = "NM_AREA";
                ddlArea.DataValueField = "ID_AREA";
                ddlArea.DataBind();

                ddlArea.Enabled = ddlArea.Items.Count > 0;
                ddlArea.Items.Insert(0, new ListItem("", ""));
            }
            UpdatePanel1.Update();
        }

        //====> Método que carrega o DropDown de Area
        private void CarregaSubArea()
        {
            int idArea = ddlArea.SelectedValue != "" ? int.Parse(ddlArea.SelectedValue) : 0;

            if (idArea != 0)
            {
                ddlSubArea.DataSource = (from tb908 in TB908_SUBAREA.RetornaPelaArea(idArea)
                                         select new { tb908.NM_SUBAREA, tb908.ID_SUBAREA });

                ddlSubArea.DataTextField = "NM_SUBAREA";
                ddlSubArea.DataValueField = "ID_SUBAREA";
                ddlSubArea.DataBind();
                ddlSubArea.Enabled = ddlSubArea.Items.Count > 0;
                ddlSubArea.Items.Insert(0, new ListItem("", ""));
            }
            UpdatePanel1.Update();
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        /// <param name="ddlCidade">DropDown de cidade</param>
        /// <param name="ddlUF">DropDown de UF</param>
        private void CarregaCidades(DropDownList ddlCidade, DropDownList ddlUF)
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUF.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Enabled = ddlCidade.Items.Count > 0;
            ddlCidade.Items.Insert(0, "");
        }

        private void CarregaTipoOcupacao(DropDownList ddl)
        {
            ddl.DataSource = TB176_TIPO_OCUPA.RetornaTodosRegistros();

            ddl.DataTextField = "DE_TIPO_OCUPA";
            ddl.DataValueField = "CO_TIPO_OCUPA";
            ddl.DataBind();

            ddl.Enabled = ddl.Items.Count > 0;
            ddl.Items.Insert(0, "");
        }

        private void CarregaTipoTerreno(DropDownList ddl)
        {
            ddl.DataSource = TB177_TIPO_TERRE.RetornaTodosRegistros();

            ddl.DataTextField = "DE_TIPO_TERRE";
            ddl.DataValueField = "CO_TIPO_TERRE";
            ddl.DataBind();

            ddl.Enabled = ddl.Items.Count > 0;
            ddl.Items.Insert(0, "");
        }

        private void CarregaTipoDelimitacao(DropDownList ddl)
        {
            ddl.DataSource = TB178_TIPO_DELIM_TERRE.RetornaTodosRegistros();

            ddl.DataTextField = "DE_TIPO_DELIM_TERRE";
            ddl.DataValueField = "CO_TIPO_DELIM_TERRE";
            ddl.DataBind();

            ddl.Enabled = ddl.Items.Count > 0;
            ddl.Items.Insert(0, "");
        }

        private void CarregaTipoCobertura(DropDownList ddl)
        {
            ddl.DataSource = TB181_TIPO_COBER.RetornaTodosRegistros();

            ddl.DataTextField = "DE_TIPO_COBER";
            ddl.DataValueField = "CO_TIPO_COBER";
            ddl.DataBind();

            ddl.Enabled = ddl.Items.Count > 0;
            ddl.Items.Insert(0, "");
        }

        private void CarregaAbastAgua(DropDownList ddl)
        {
            ddl.DataSource = TB183_ORIGE_AGUA.RetornaTodosRegistros();

            ddl.DataTextField = "DE_ORIGE_AGUA";
            ddl.DataValueField = "CO_ORIGE_AGUA";
            ddl.DataBind();

            ddl.Enabled = ddl.Items.Count > 0;
            ddl.Items.Insert(0, "");
        }

        private void CarregaRedeEsgoto(DropDownList ddl)
        {
            ddl.DataSource = TB187_REDE_ESGOT.RetornaTodosRegistros();

            ddl.DataTextField = "DE_REDE_ESGOT";
            ddl.DataValueField = "CO_REDE_ESGOT";
            ddl.DataBind();

            ddl.Enabled = ddl.Items.Count > 0;
            ddl.Items.Insert(0, "");
        }

        private void CarregaEnergiaEletrica(DropDownList ddl)
        {
            ddl.DataSource = TB184_FONTE_ENERG.RetornaTodosRegistros();

            ddl.DataTextField = "DE_FONTE_ENERG";
            ddl.DataValueField = "CO_FONTE_ENERG";
            ddl.DataBind();

            ddl.Enabled = ddl.Items.Count > 0;
            ddl.Items.Insert(0, "");
        }

        private void CarregaInstituicoes()
        {
            List<InstituicoesApoio> lstInst = new List<InstituicoesApoio>();
            lstInst.Add(new InstituicoesApoio() { IdInstituicao = 1, MarcarLinha = false, NomeInstituicao = "AACD" });
            lstInst.Add(new InstituicoesApoio() { IdInstituicao = 2, MarcarLinha = true, NomeInstituicao = "ADEFIL" });
            lstInst.Add(new InstituicoesApoio() { IdInstituicao = 3, MarcarLinha = false, NomeInstituicao = "APAE" });

            lstInstituicoes.DataSource = lstInst;
            lstInstituicoes.DataBind();
        }

        private void CarregaProgramas()
        {
            List<ProgramasSociais> lstProg = new List<ProgramasSociais>();
            lstProg.Add(new ProgramasSociais() { IdPrograma = 1, MarcarLinha = false, NomePrograma = "Bolsa Família" });

            lstProgramas.DataSource = lstProg;
            lstProgramas.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairro
        /// </summary>
        /// <param name="ddlBairro">DropDown de bairro</param>
        /// <param name="ddlCidade">DropDown de cidade</param>
        private void CarregaBairros(DropDownList ddlBairro, DropDownList ddlCidade)
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;
                ddlBairro.Items.Clear();
                return;
            }
            else
            {
                ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataBind();

                ddlBairro.Enabled = ddlBairro.Items.Count > 0;
                ddlBairro.Items.Insert(0, "");
            }
        }

        private void CarregaUfs(DropDownList ddlUF)
        {
            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.Items.Insert(0, new ListItem("", ""));
        }

        #endregion
    }

    public class InstituicoesApoio
    {
        public int IdInstituicao { get; set; }
        public string NomeInstituicao { get; set; }
        public bool MarcarLinha { get; set; }
    }

    public class ProgramasSociais
    {
        public int IdPrograma { get; set; }
        public string NomePrograma { get; set; }
        public bool MarcarLinha { get; set; }
    }
}