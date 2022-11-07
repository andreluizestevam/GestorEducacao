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
// 14/12/16 |   BRUNO VIEIRA LANDIM      |  Criado funcionalidade    

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0919_CadastroClassRisco
{
    public partial class Cadastro : System.Web.UI.Page
    {

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.ToString();
                populateDdlMultiColor();
                //colorManipulation();
                ddlCor.Items.Insert(0, "Nenhum");
                ddlCor.Width = 115;
            }
        }

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
            try
            {
                if (ddlTipoClassRisco.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O tipo de classificação de risco deve ser selecionado");
                    return;
                }

                if (string.IsNullOrEmpty(txtNome.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O nome da prioridade de classificação de risco é requerido, favor informá-lo");
                    return;
                }

                TBS435_CLASS_RISCO tbs435 = RetornaEntidade();

                tbs435.TP_CLASS_RISCO = int.Parse(ddlTipoClassRisco.SelectedValue);
                tbs435.CO_COR = txtCodCor.Text;
                tbs435.NO_COR = txtNomeCor.Text;
                tbs435.NO_PRIOR = txtNome.Text.Trim();
                tbs435.CO_PRIOR = String.IsNullOrEmpty(txtSigla.Text) ? txtNome.Text.Substring(0,3).ToUpper() : txtSigla.Text;
                tbs435.NU_TEMPO = int.Parse(txtTempo.Text);
                tbs435.DE_CLASS_RISCO = txtDescricao.Text;
                tbs435.NU_ORDEM = int.Parse(txtOrdem.Text);
                tbs435.FL_SITUA = ddlSitua.Text;
                tbs435.DT_SITUA = DateTime.Parse(txtData.Text);

                CurrentPadraoCadastros.CurrentEntity = tbs435;
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro de execução, entre em contato com o suporte para solucionar o problema");
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TBS435_CLASS_RISCO tbs435 = RetornaEntidade();

            if (tbs435 != null)
            {
                hidCID.Value = tbs435.ID_CLASS_RISCO.ToString();
                ddlTipoClassRisco.SelectedValue = tbs435.TP_CLASS_RISCO.ToString(); 
                txtNome.Text = tbs435.NO_PRIOR;
                txtSigla.Text = tbs435.CO_PRIOR;
                txtOrdem.Text = tbs435.NU_ORDEM.ToString();
                txtTempo.Text = tbs435.NU_TEMPO.ToString();
                ddlCor.SelectedValue = tbs435.NO_COR;
                txtNomeCor.Text = tbs435.NO_COR;
                txtCodCor.Text = tbs435.CO_COR;
                txtDescricao.Text = tbs435.DE_CLASS_RISCO;
                ddlSitua.SelectedValue = tbs435.FL_SITUA;
                txtData.Text = tbs435.DT_SITUA.ToString();

                viewCor.Attributes.Add("style", "background:" + txtCodCor.Text + ";width:30px;height:11px;margin-top:13px;border-width:1px 1px 1px 1px; border-style:solid; border-color:Gray;");
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        private TBS435_CLASS_RISCO RetornaEntidade()
        {
            TBS435_CLASS_RISCO tbs435 = TBS435_CLASS_RISCO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs435 == null) ? new TBS435_CLASS_RISCO() : tbs435;
        }

        private void populateDdlMultiColor()
        {
            ddlCor.DataSource = finalColorList();
            ddlCor.DataBind();
        }

        private List<string> finalColorList()
        {
            string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
            string[] systemEnvironmentColors =
                new string[(
                typeof(System.Drawing.SystemColors)).GetProperties().Length];

            int index = 0;

            foreach (MemberInfo member in (
                typeof(System.Drawing.SystemColors)).GetProperties())
            {
                systemEnvironmentColors[index++] = member.Name;
            }

            List<string> finalColorList = new List<string>();

            foreach (string color in allColors)
            {
                if (Array.IndexOf(systemEnvironmentColors, color) < 0)
                {
                    finalColorList.Add(color);
                }
            }
            return finalColorList;
        }

        //private void colorManipulation()
        //{
        //    int row;
            
        //    for (row = 0; row < ddlCor.Items.Count; row++)
        //    {
        //        ddlCor.Items[row].Attributes.Add("style", "background-color:" + ddlCor.Items[row].Value);
        //    }

        //    ddlCor.BackColor = Color.FromName(ddlCor.SelectedItem.Text);
        //    ddlCor.Items[0].Attributes.Add("style", "background-color:" +"transparent;");
        //}

        protected void ddlCor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCor.SelectedIndex == 0)
            {
                //ddlCor.Items.FindByValue(ddlCor.SelectedValue).Selected = true;
                txtCodCor.Text = "";
                //colorManipulation();
                txtNomeCor.Enabled = true;
                txtCodCor.Enabled = true; 
            }
            else
            {
                //ddlCor.BackColor = Color.FromName(ddlCor.SelectedItem.Text);
                //colorManipulation();
                //ddlCor.Items.FindByValue(ddlCor.SelectedValue).Selected = true;
                viewCor.Attributes.Add("style", "background:" + ddlCor.SelectedItem.Value + ";width:30px;height:11px;margin-top:13px;border-width:1px 1px 1px 1px; border-style:solid; border-color:Gray;");
                Color argb = Color.FromName(ddlCor.SelectedItem.Text);
                txtCodCor.Text = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb((argb.A), (argb.R), (argb.G), (argb.B)));
                txtNomeCor.Text = ddlCor.SelectedItem.Value;
                txtNomeCor.Enabled = false;
                txtCodCor.Enabled = false; 
            }
        }

        protected void txtCodCor_OnTextChanged(object sender, EventArgs e)
        {
            //ddlCor.BackColor = Color.FromName(ddlCor.SelectedItem.Text);
            //colorManipulation();
            viewCor.Attributes.Add("style", "background:" + txtCodCor.Text + ";width:30px;height:11px;margin-top:13px;border-width:1px 1px 1px 1px; border-style:solid; border-color:Gray;");
            ddlCor.SelectedIndex = 0;
            string argb = ddlCor.BackColor.A.ToString() + ", " + ddlCor.BackColor.R.ToString() + ", " + ddlCor.BackColor.G.ToString() + ", " + ddlCor.BackColor.B.ToString();
            //ddlCor.Items.FindByValue(ddlCor.SelectedValue).Selected = true;
        }

        #endregion
    }


}