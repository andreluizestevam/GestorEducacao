//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CADASTRO INTERNACIONAL DE DOENÇAS.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0905_CadastroInternacionalDoencaGeral
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

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (string.IsNullOrEmpty(txtSigla.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A sigla é requerida, favor informá-la");
                return;
            }

            if (string.IsNullOrEmpty(txtCID.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Código do CID é requerido, favor informá-lo");
                return;
            }

            TBS223_CID tbs223 = RetornaEntidade();

            tbs223.NO_CID_GRUPO = txtCID.Text.Trim().ToUpper();
            tbs223.DE_CID_GRUPO = txtDescricaoCID.Text;
            //tbs338.CO_SIGLA_CID = txtSigla.Text;
            tbs223.CO_SITUA_CID_GRUPO = ddlSituacao.SelectedValue;

            CurrentPadraoCadastros.CurrentEntity = tbs223;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TBS223_CID tbs223 = RetornaEntidade();

            if (tbs223 != null)
            {
                txtCID.Text = tbs223.NO_CID_GRUPO.ToString();
                txtDescricaoCID.Text = tbs223.DE_CID_GRUPO.ToString();
                //txtSigla.Text = tbs223.CO_SIGLA_CID;
                ddlSituacao.SelectedValue = tbs223.CO_SITUA_CID_GRUPO.ToString();
                hidCID.Value = tbs223.ID_CID_GRUPO.ToString();

                CarregaCIDAssociados();
            }                       
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS338_CID_GERAL</returns>
        private TBS223_CID RetornaEntidade()
        {
            TBS223_CID tbs223 = TBS223_CID.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs223 == null) ? new TBS223_CID() : tbs223;
        }

        /// <summary>
        /// Carrega os ISDA associados ao Tipo ISDA em contexto
        /// </summary>
        private void CarregaCIDAssociados()
        {
            int tipo = (!string.IsNullOrEmpty(hidCID.Value) ? int.Parse(hidCID.Value) : 0);
            var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                       where tb117.TBS223_CID.ID_CID_GRUPO == tipo
                       select new
                       {
                           tb117.NO_CID,
                           tb117.IDE_CID,
                           //tb117.CO_SIGLA_CID,
                       }).OrderBy(w => w.NO_CID).ToList();

            if (res.Count > 0)
                infoAgrup.Visible = true;

            grdISDA.DataSource = res;
            grdISDA.DataBind();
        }

        /// <summary>
        /// Método responsável por deletar a associação do item selecionado na grid
        /// </summary>
        //private void DeletaAssociacao()
        //{
        //    foreach (GridViewRow li in grdISDA.Rows)
        //    {
        //        if ((((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked) == true)
        //        {
        //            int isda = int.Parse((((HiddenField)li.Cells[0].FindControl("hidcoitem")).Value));
        //            TB117_CODIGO_INTERNACIONAL_DOENCA tb117 = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(isda);
        //            tb117.TBS223_CID.ID_CID_GRUPO = null;
        //            TB117_CODIGO_INTERNACIONAL_DOENCA.SaveOrUpdate(tb117, true);
        //        }
        //    }
        //    CarregaCIDAssociados();
        //}

        //protected void lnkApagaAssoci_OnClick(object sender, EventArgs e)
        //{
        //    DeletaAssociacao();
        //}

        #endregion
    }
}