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
// 19/02/2015| Maxwell Almeida            |  Criação da funcionalidade para Cadastro de ocorrências salvas
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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasAlunos._3643_CadasOcorrSalvas
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
                CarregaCategoria();
                CarregaClassificacao();
                txtDtCadas.Text = DateTime.Now.ToString();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TBE196_OCORR_DISCI tbe196 = RetornaEntidade();

            tbe196.CO_CATEG = ddlCateg.SelectedValue;
            tbe196.CO_SIGLA_OCORR = txtSigla.Text;
            tbe196.DE_OCORR = txtDescri.Text;
            tbe196.TB150_TIPO_OCORR = TB150_TIPO_OCORR.RetornaPelaChavePrimaria(ddlClassifi.SelectedValue);

            //Salva essas informações apenas quando a situação tiver sido alterada
            if (hidCoSitua.Value != ddlSituacao.SelectedValue)
            {
                tbe196.DT_SITU = DateTime.Now;
                tbe196.CO_SITU = ddlSituacao.SelectedValue;
                tbe196.CO_COL_SITU = LoginAuxili.CO_COL;
                tbe196.CO_EMP_SITU = LoginAuxili.CO_EMP;
                tbe196.IP_SITU = Request.UserHostAddress;
            }

            //Salva essas informações apenas quando for cadastro novo
            switch (tbe196.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbe196.DT_CADAS = DateTime.Now;
                    tbe196.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbe196.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbe196.IP_CADAS = Request.UserHostAddress;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tbe196;
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações
        /// </summary>
        private void CarregaFormulario()
        {
            TBE196_OCORR_DISCI tb196 = RetornaEntidade();

            if (tb196 != null)
            {
                tb196.TB150_TIPO_OCORRReference.Load();

                hidCoSitua.Value = ddlSituacao.SelectedValue = tb196.CO_SITU;
                txtSigla.Text = tb196.CO_SIGLA_OCORR;
                txtDescri.Text = tb196.DE_OCORR;
                ddlCateg.SelectedValue = tb196.CO_CATEG;
                ddlClassifi.SelectedValue = tb196.TB150_TIPO_OCORR.CO_SIGL_OCORR;
                txtDtCadas.Text = tb196.DT_CADAS.ToString();
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBE196_OCORR_DISCI</returns>
        private TBE196_OCORR_DISCI RetornaEntidade()
        {
            TBE196_OCORR_DISCI tbs355 = TBE196_OCORR_DISCI.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs355 == null) ? new TBE196_OCORR_DISCI() : tbs355;
        }

        /// <summary>
        /// Carrega os grupos
        /// </summary>
        private void CarregaCategoria()
        {
            AuxiliCarregamentos.CarregaCategoriaOcorrencias(ddlCateg, false);
        }

        /// <summary>
        /// Carreg as classificações
        /// </summary>
        private void CarregaClassificacao()
        {
            AuxiliCarregamentos.CarregaTiposOcorrencias(ddlClassifi, false, ddlCateg.SelectedValue);
        }

        #endregion

        #region Funções de Campo

        protected void ddlCateg_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaClassificacao();
        }

        #endregion
    }
}