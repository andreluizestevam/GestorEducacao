//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 23/06/16 | Filipe Rodrigues           | Criação da funcionalidade para Cadastro de Ocorrencias


//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using Resources;

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7950_CtrlCadastralParceiros._7952_OcorrenciasParceiros
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
                CarregarTiposOcorrenciaParceiros();
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                CurrentPadraoCadastros.CurrentEntity = RetornaEntidade();
                return;
            }

            TB422_REGIS_OCORR_PARCE tb422 = RetornaEntidade();
            if (Page.IsValid)
            {
                tb422 = new TB422_REGIS_OCORR_PARCE();
                tb422.TB421_PARCEIROS = TB421_PARCEIROS.RetornaPelaChavePrimaria(int.Parse(drpParceiro.SelectedValue));
                tb422.DT_OCORR = DateTime.Now;
                tb422.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                tb422.IP_CADAS_OCORR = Request.UserHostAddress;
                tb422.DT_OCORR = DateTime.Now;
                tb422.TP_OCORR = ddlTipo.SelectedValue;
                tb422.NO_OCORR = txtTitulo.Text.Trim();
                tb422.TX_OCORR = txtOcorrencia.Text;
                tb422.TX_ACAO_OCORR = txtAcao.Text.Trim();

                TB422_REGIS_OCORR_PARCE.SaveOrUpdate(tb422, true);

            }
            else if (tb422 != null)
            {
                //tb422 = new TB422_REGIS_OCORR_PARCE();
                tb422.DT_OCORR = DateTime.Now;
                tb422.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                tb422.IP_CADAS_OCORR = Request.UserHostAddress;
                tb422.DT_OCORR = DateTime.Now;
                if (ddlTipo.SelectedValue != null)
                {
                    tb422.TP_OCORR = ddlTipo.SelectedValue; 
                }                
                tb422.NO_OCORR = txtTitulo.Text.Trim();
                tb422.TX_OCORR = txtOcorrencia.Text;
                tb422.TX_ACAO_OCORR = txtAcao.Text.Trim();

                TB422_REGIS_OCORR_PARCE.SaveOrUpdate(tb422, true);
                AuxiliPagina.RedirecionaParaPaginaSucesso("Ocorrência alterada com sucesso.", Request.Url.AbsoluteUri);
            }
            else
            {
                AuxiliPagina.RedirecionaParaPaginaErro("Ocorreu alguma inconsistência.", Request.Url.AbsoluteUri);
            }

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB422_REGIS_OCORR_PARCE tb422 = RetornaEntidade();

            if (tb422 != null)
            {
                tb422.TB421_PARCEIROSReference.Load();
                tb422.TB03_COLABORReference.Load();

                OcultarPesquisa(true);
                drpParceiro.Visible = false;
                txtNomePacPesq.Visible = true;
                txtNomePacPesq.Enabled = false;
                txtNomePacPesq.Text = tb422.TB421_PARCEIROS.DE_RAZSOC_PARCE.ToString().ToUpper();
                ddlTipo.SelectedValue = tb422.TP_OCORR;
                txtTitulo.Text = tb422.NO_OCORR;
                txtOcorrencia.Text = tb422.TX_OCORR;
                txtAcao.Text = tb422.TX_ACAO_OCORR;
                //Desabilitando os que não podem ser alterados
                imgbVoltarPesq.Visible = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns></returns>
        private TB422_REGIS_OCORR_PARCE RetornaEntidade()
        {
            return TB422_REGIS_OCORR_PARCE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregarParceiros()
        {
            drpParceiro.DataSource = (from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                                      where tb421.DE_RAZSOC_PARCE.Contains(txtNomePacPesq.Text)
                                      select new { tb421.CO_PARCE, tb421.DE_RAZSOC_PARCE });

            drpParceiro.DataTextField = "DE_RAZSOC_PARCE";
            drpParceiro.DataValueField = "CO_PARCE";
            drpParceiro.DataBind();

            drpParceiro.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarTiposOcorrenciaParceiros()
        {
            AuxiliCarregamentos.CarregaTiposOcorrenciaParceiros(ddlTipo, false);
        }

        #endregion

        #region Funções de Campo

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            CarregarParceiros();
            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNomePacPesq.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            drpParceiro.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        #endregion
    }
}