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
// 27/10/2014| Maxwell Almeida            |  Criação da funcionalidade para Cadastro de Grupos de Procedimentos Médicos
//           |                            | 
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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9111_Grupo
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
            CarregarAreas();
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(txtNomeGrupo.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Grupo é Requerido");
                return;
            }

            TBS354_PROC_MEDIC_GRUPO tbs354 = RetornaEntidade();

            tbs354.NM_PROC_MEDIC_GRUPO = txtNomeGrupo.Text;
            tbs354.NM_AREA_GRUPO = drpArea.SelectedValue;

            //Salva essas informações apenas quando a situação tiver sido alterada
            if (hidCoSitua.Value != ddlSituacao.SelectedValue)
            {
                tbs354.FL_SITUA_PROC_MEDIC_GRUPO = ddlSituacao.SelectedValue;
                tbs354.CO_COL_SITUA = LoginAuxili.CO_COL;
            }

            //Salva essas informações apenas quando for cadastro novo
            switch (tbs354.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs354.DT_CADAS = DateTime.Now;
                    tbs354.CO_COL_CADAS = LoginAuxili.CO_COL;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs354;
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações
        /// </summary>
        private void CarregaFormulario()
        {
            TBS354_PROC_MEDIC_GRUPO tbs354 = RetornaEntidade();

            if (tbs354 != null)
            {
                hidCoSitua.Value = ddlSituacao.SelectedValue = tbs354.FL_SITUA_PROC_MEDIC_GRUPO;
                drpArea.SelectedValue = tbs354.NM_AREA_GRUPO;
                txtNomeGrupo.Text = tbs354.NM_PROC_MEDIC_GRUPO;
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS339_CAMPSAUDE</returns>
        private TBS354_PROC_MEDIC_GRUPO RetornaEntidade()
        {
            TBS354_PROC_MEDIC_GRUPO tbs354 = TBS354_PROC_MEDIC_GRUPO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs354 == null) ? new TBS354_PROC_MEDIC_GRUPO() : tbs354;
        }

        private void CarregarAreas()
        {
            AuxiliCarregamentos.CarregaAreasGrupos(drpArea, false, AuxiliCarregamentos.ETiposClassificacoes.atendimento);
        }

        #endregion
    }
}