using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3207_DocumentacaoPaciente._AssociacaoDocOperadora
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
            if (!IsPostBack)
            {
                CarregaOperadoras();
                CarregaGridDocumentos();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (String.IsNullOrEmpty(ddlOperadora.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione uma operadora!");
                return;
            }

            int operadora = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;

            foreach (GridViewRow row in grdDocumentos.Rows)
            {
                HiddenField hfCoTpDocMat = ((HiddenField)row.Cells[2].FindControl("hdCoTpMat"));
                CheckBox ckSelect = ((CheckBox)row.Cells[0].FindControl("ckSelect"));
                int coTpDocMat = Convert.ToInt32(hfCoTpDocMat.Value);

                var tb402 = (from lTb402 in TBS402_OPER_DOCTOS.RetornaTodosRegistros()
                             where lTb402.ID_OPER == operadora && lTb402.CO_TP_DOC_MAT == coTpDocMat
                             select lTb402).FirstOrDefault();

                if (ckSelect.Checked == true)
                {
                    if (tb402 == null)
                    {
                        tb402 = new TBS402_OPER_DOCTOS();

                        tb402.TB250_OPERA = TB250_OPERA.RetornaPelaChavePrimaria(operadora);
                        tb402.TB121_TIPO_DOC_MATRICULA = TB121_TIPO_DOC_MATRICULA.RetornaPelaChavePrimaria(coTpDocMat);

                        TBS402_OPER_DOCTOS.SaveOrUpdate(tb402, true);
                    }
                }
                else
                {
                    if (tb402 != null)
                        TBS402_OPER_DOCTOS.Delete(tb402, true);
                }
            }

            GestorEntities.CurrentContext.SaveChanges();
            AuxiliPagina.RedirecionaParaPaginaSucesso("Associação Efetuada com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            var opr = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
        
            CarregaOperadoras();
            ddlOperadora.SelectedValue = opr.ToString();
            ddlOperadora.Enabled = false;

            MarcarItensGridDocumentos(opr);
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var opr = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;
            MarcarItensGridDocumentos(opr);
        }

        private void MarcarItensGridDocumentos(int opr)
        {
            var tb402 = TBS402_OPER_DOCTOS.RetornaTodosRegistros().Where(o => o.ID_OPER == opr);

            if (tb402 != null)
            {
                foreach (GridViewRow linha in grdDocumentos.Rows)
                {
                    HiddenField hfCoTpDocMat = ((HiddenField)linha.Cells[2].FindControl("hdCoTpMat"));
                    CheckBox ckSelect = ((CheckBox)linha.Cells[0].FindControl("ckSelect"));
                    int coTpDocMat = Convert.ToInt32(hfCoTpDocMat.Value);

                    ckSelect.Checked = false;

                    foreach (var i in tb402)
                        if (coTpDocMat == i.CO_TP_DOC_MAT)
                            ckSelect.Checked = true;
                }
            }
        }



        #endregion

        #region Carregamento
        
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false, true);
        }

        /// <summary>
        /// Método que carrega a grid de Documentos
        /// </summary>
        private void CarregaGridDocumentos()
        {
            grdDocumentos.DataSource = TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros();
            grdDocumentos.DataBind();
        }

        #endregion
    }
}
