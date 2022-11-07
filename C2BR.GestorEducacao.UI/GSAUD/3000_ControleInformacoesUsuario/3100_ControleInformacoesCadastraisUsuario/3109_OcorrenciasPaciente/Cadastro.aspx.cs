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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3109_OcorrenciasPaciente
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
                CarregarTiposOcorrencia();
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

            if (Page.IsValid)
            {
                TBS408_OCORR_PACIE tbs408 = RetornaEntidade();

                if (tbs408 == null)
                {
                    tbs408 = new TBS408_OCORR_PACIE();

                    tbs408.CO_ALU = int.Parse(drpPaciente.SelectedValue);
                    tbs408.CO_EMP_OCORR = LoginAuxili.CO_EMP;

                    //Dados de cadastro
                    tbs408.DT_CADAS = DateTime.Now;
                    tbs408.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs408.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                    tbs408.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs408.IP_CADAS = Request.UserHostAddress;
                }

                tbs408.DT_OCORR = DateTime.Now;
                tbs408.TP_OCORR = ddlTipo.SelectedValue;
                tbs408.NO_OCORR = txtTitulo.Text.Trim();
                tbs408.DE_OCORR = txtOcorrencia.Text;
                tbs408.DE_ACAO_OCORR = txtAcao.Text.Trim();
                tbs408.CO_SITU = ddlSituacao.SelectedValue;

                CurrentPadraoCadastros.CurrentEntity = tbs408;
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TBS408_OCORR_PACIE tbs408 = RetornaEntidade();

            if (tbs408 != null)
            {

                CarregarPacientes();
                OcultarPesquisa(true);
                drpPaciente.SelectedValue = tbs408.CO_ALU.ToString();
                ddlTipo.SelectedValue = tbs408.TP_OCORR;
                txtTitulo.Text = tbs408.NO_OCORR;
                txtOcorrencia.Text = tbs408.DE_OCORR;
                txtAcao.Text = tbs408.DE_ACAO_OCORR;
                ddlSituacao.SelectedValue = tbs408.CO_SITU;
                //Desabilitando os que não podem ser alterados
                drpPaciente.Enabled = false;
                imgbVoltarPesq.Visible = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns></returns>
        private TBS408_OCORR_PACIE RetornaEntidade()
        {
            return TBS408_OCORR_PACIE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregarPacientes()
        {
            drpPaciente.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                       where tb07.NO_ALU.Contains(txtNomePacPesq.Text)
                                       select new { tb07.CO_ALU, tb07.NO_ALU });

            drpPaciente.DataTextField = "NO_ALU";
            drpPaciente.DataValueField = "CO_ALU";
            drpPaciente.DataBind();

            drpPaciente.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarTiposOcorrencia()
        {
            AuxiliCarregamentos.CarregaTiposOcorrencia(ddlTipo, false);
        }

        #endregion

        #region Funções de Campo

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            CarregarPacientes();
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
            drpPaciente.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        #endregion
    }
}