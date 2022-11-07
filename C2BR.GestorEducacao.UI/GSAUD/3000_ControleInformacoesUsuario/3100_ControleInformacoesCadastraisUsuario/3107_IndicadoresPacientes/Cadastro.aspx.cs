//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE PAIS OU RESPONSÁVEIS 
// OBJETIVO: CADASTRAMENTO DE PAIS OU RESPONSÁVEIS DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 09/03/2014| Julio Gleisson Rodrigues   |  Copia da Tela \GEDUC\3000_CtrlOperacionalPedagogico\
//           |                            |  3700_CtrlInformacoesResponsaveis\3710_CtrlInformacoesCadastraisResponsaveis
//           |                            | 

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Artem.Web.UI.Controls;
using System.Web;
using System.Data;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3107_IndicadoresPacientes
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
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
                CarregaClassificacoesFuncionais();
                CarregaUfs();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }
        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(txtnome.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Nome");
                return;
            }
            TBS377_INDIC_PACIENTES tbs377 = RetornaEntidade();
            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            tbs377.NU_CPF = txtCPF.Text.Replace(".", "").Replace(".", "").Replace("-", "");
            tbs377.NM_PROFI_INDIC = txtnome.Text;
            tbs377.CO_CLASS_FUNC = (!string.IsNullOrEmpty(ddlClassificacoesFuncionais.SelectedValue) ? ddlClassificacoesFuncionais.SelectedValue : null);
            tbs377.CO_SIGLA_ENTID_PROFI = (!string.IsNullOrEmpty(ddlSiglaEntidProfi.SelectedValue) ? ddlSiglaEntidProfi.SelectedValue : null);
            tbs377.NU_ENTID_PROFI = (!string.IsNullOrEmpty(txtNrEntidProfi.Text) ? txtNrEntidProfi.Text : null);
            tbs377.CO_UF_ENTID_PROFI = (!string.IsNullOrEmpty(ddlUfEntidProfi.SelectedValue) ? ddlUfEntidProfi.SelectedValue : null);
            tbs377.DT_EMISS_ENTID_PROFI  = txtDtEmissEntidProfi.Text == "" ? null:   tbs377.DT_EMISS_ENTID_PROFI = Convert.ToDateTime(txtDtEmissEntidProfi.Text);
            tbs377.CO_SITUA = ddlSitu.SelectedValue;
           

            if (hidSituacao.Value != ddlSitu.SelectedValue)
            {
                //tbs377.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs377.DT_SITUA = DateTime.Now;
                tbs377.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs377.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs377.IP_SITUA = Request.UserHostAddress;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tbs377.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs377.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs377.DT_CADAS = DateTime.Now;
                    tbs377.IP_CADAS = Request.UserHostAddress;
                    tbs377.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    break;
            }
            CurrentPadraoCadastros.CurrentEntity = tbs377;
        }

        #region Carregamento DropDown



        private void CarregaFormulario()
        {


            try
            {
                TBS377_INDIC_PACIENTES tbs377 = RetornaEntidade();

                if (tbs377 != null)
                {

                    hidSituacao.Value = tbs377.CO_SITUA;
                    txtnome.Text = tbs377.NM_PROFI_INDIC;
                    txtCPF.Text =  tbs377.NU_CPF;
                    ddlSitu.SelectedValue = tbs377.CO_SITUA;
                    ddlClassificacoesFuncionais.SelectedValue = tbs377.CO_CLASS_FUNC.ToString();
                    ddlSiglaEntidProfi.SelectedValue = tbs377.CO_SIGLA_ENTID_PROFI.ToString();
                    txtNrEntidProfi.Text = tbs377.NU_ENTID_PROFI;
                    ddlUfEntidProfi.SelectedValue = tbs377.CO_UF_ENTID_PROFI.ToString();
                    txtDtEmissEntidProfi.Text = tbs377.DT_EMISS_ENTID_PROFI.ToString();
                   
                }
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao caregar  formulário" + " - " + ex.Message);
                return;
            }

        }


        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDownList UF</param>
        private void CarregaUfs()
        {
            AuxiliCarregamentos.CarregaUFs(ddlUfEntidProfi, false);
        }

        #endregion

        /// <summary>
        /// Retorna a entidade e contexto, quando houver
        /// </summary>
        /// <returns></returns>
        /// 
        private TBS377_INDIC_PACIENTES RetornaEntidade()
        {
            TBS377_INDIC_PACIENTES tbs377 = TBS377_INDIC_PACIENTES.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs377 == null) ? new TBS377_INDIC_PACIENTES() : tbs377;
        }


        private void CarregaClassificacoesFuncionais()
        {

            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassificacoesFuncionais, false, LoginAuxili.CO_EMP);

        }
    }
}