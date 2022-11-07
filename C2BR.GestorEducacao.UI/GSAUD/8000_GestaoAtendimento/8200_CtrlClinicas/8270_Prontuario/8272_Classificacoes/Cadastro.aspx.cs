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
// 14/04/2017| Filipe Rodrigues           |  Criação da funcionalidade para Cadastro de Classificações de Prontuarios Médicos
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
namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8270_Prontuario._8272_Classificacoes
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
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(txtNomeClassificacao.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O nome da classificação é obrigatório");
                return;
            }
            
            if (string.IsNullOrEmpty(txtSiglaClassificacao.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A sigla da classificação é obrigatória");
                return;
            }

            if (string.IsNullOrEmpty(ddlSituacao.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A situação da classificação é obrigatória");
                return;
            }

            TBS418_CLASS_PRONT tbs418 = RetornaEntidade();

            tbs418.NO_CLASS_PRONT = txtNomeClassificacao.Text;
            tbs418.NO_SIGLA_CLASS_PRONT = txtSiglaClassificacao.Text;
            tbs418.DE_CLASS_PRONT = txtDescricao.Text;

            var emp_col = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

            //Salva essas informações apenas quando for cadastro novo
            if (tbs418.ID_CLASS_PRONT == 0)
            {
                //Dados de quem cadastrou o atendimento
                tbs418.DT_CADAS = DateTime.Now;
                tbs418.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs418.CO_EMP_COL_CADAS = emp_col;
                tbs418.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs418.IP_CADAS = Request.UserHostAddress;
            }

            //Dados da situação do atendimento
            tbs418.CO_SITUA = ddlSituacao.SelectedValue;
            tbs418.DT_SITUA = DateTime.Now;
            tbs418.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs418.CO_EMP_COL_SITUA = emp_col;
            tbs418.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs418.IP_SITUA = Request.UserHostAddress;

            CurrentPadraoCadastros.CurrentEntity = tbs418;
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações
        /// </summary>
        private void CarregaFormulario()
        {
            TBS418_CLASS_PRONT tbs418 = RetornaEntidade();

            if (tbs418 != null)
            {
                txtNomeClassificacao.Text = tbs418.NO_CLASS_PRONT;
                txtSiglaClassificacao.Text = tbs418.NO_SIGLA_CLASS_PRONT;
                txtDescricao.Text = tbs418.DE_CLASS_PRONT;
                ddlSituacao.SelectedValue = tbs418.CO_SITUA;
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS418_CLASS_PRONT</returns>
        private TBS418_CLASS_PRONT RetornaEntidade()
        {
            TBS418_CLASS_PRONT tbs418 = TBS418_CLASS_PRONT.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs418 == null) ? new TBS418_CLASS_PRONT() : tbs418;
        }

        #endregion
    }
}