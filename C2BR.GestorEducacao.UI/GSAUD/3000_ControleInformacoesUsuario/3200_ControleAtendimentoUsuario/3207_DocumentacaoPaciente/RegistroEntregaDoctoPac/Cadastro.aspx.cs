using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3207_DocumentacaoPaciente._RegistroEntregaDoctoPac
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarPacientes();
                CarregaOperadoras();
                CarregaGridDocumentos();
            }
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            var pac = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            var opr = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;

            foreach (GridViewRow row in grdDocumentos.Rows)
            {
                int coTpDocMat  = (int)grdDocumentos.DataKeys[row.RowIndex].Values[0];

                var tb120 = (from lTb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                             where lTb120.CO_EMP == LoginAuxili.CO_EMP && lTb120.CO_ALU == pac && lTb120.CO_TP_DOC_MAT == coTpDocMat
                             select lTb120).FirstOrDefault();

                if (((CheckBox)row.Cells[0].FindControl("ckSelect")).Checked)
                {
                    if (tb120 == null)
                    {
                        tb120 = new TB120_DOC_ALUNO_ENT();
                        var refAluno = TB07_ALUNO.RetornaPeloCoAlu(pac);
                        tb120.CO_ALU = pac;
                        tb120.CO_TP_DOC_MAT = coTpDocMat;
                        tb120.CO_EMP = LoginAuxili.CO_EMP;
                        tb120.TB07_ALUNO = refAluno;
                        tb120.TB121_TIPO_DOC_MATRICULA = TB121_TIPO_DOC_MATRICULA.RetornaPelaChavePrimaria(coTpDocMat);
                        refAluno.TB25_EMPRESA1Reference.Load();
                        tb120.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                        tb120.TB250_OPERA = TB250_OPERA.RetornaPelaChavePrimaria(opr);

                        TB120_DOC_ALUNO_ENT.SaveOrUpdate(tb120, false);
                    }
                }
                else
                {
                    if (tb120 != null)
                        TB120_DOC_ALUNO_ENT.Delete(tb120, false);
                }
            }

            GestorEntities.CurrentContext.SaveChanges();

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Efetuado com Sucesso", Request.Url.AbsoluteUri);
        }

        #endregion

        #region Métodos

        private void CarregarPacientes()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlPaciente, LoginAuxili.CO_EMP, false, true);
        }

        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false, true, false, true, false);
        }

        protected void ddlPaciente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlPaciente.SelectedValue))
            {
                var pac = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlPaciente.SelectedValue));

                pac.TB250_OPERAReference.Load();

                if (pac != null && pac.TB250_OPERA != null)
                    ddlOperadora.SelectedValue = pac.TB250_OPERA.ID_OPER.ToString();

                CarregaGridDocumentos();
            }
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlOperadora.SelectedValue) && !String.IsNullOrEmpty(ddlPaciente.SelectedValue))
                CarregaGridDocumentos();
        }

        /// <summary>
        /// Método que carrega a grid de Documentos
        /// </summary>
        private void CarregaGridDocumentos()
        {
            var pac = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            var opr = ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0;

            grdDocumentos.DataKeyNames = new string[] { "CO_TP_DOC_MAT" };

            var res = (from tb121 in TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros()
                       join tb402 in TBS402_OPER_DOCTOS.RetornaTodosRegistros() on tb121.CO_TP_DOC_MAT equals tb402.CO_TP_DOC_MAT
                       join tb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros().Where(t => t.CO_ALU == pac && t.TB250_OPERA.ID_OPER == opr) on tb121.CO_TP_DOC_MAT equals tb120.CO_TP_DOC_MAT into docs
                       from tb120 in docs.DefaultIfEmpty()
                       where tb402.ID_OPER == opr
                       select new Documentos
                       {
                           CO_TP_DOC_MAT = tb121.CO_TP_DOC_MAT,
                           DE_TP_DOC_MAT = tb121.DE_TP_DOC_MAT,
                           SIG_TP_DOC_MAT = tb121.SIG_TP_DOC_MAT,
                           chkSel = docs.FirstOrDefault() != null
                       }).Distinct().OrderBy(t => t.DE_TP_DOC_MAT);
            
            grdDocumentos.DataSource = res;
            grdDocumentos.DataBind();
        }

        public class Documentos
        {
            public int CO_TP_DOC_MAT { get; set; }
            public string DE_TP_DOC_MAT { get; set; }
            public string SIG_TP_DOC_MAT { get; set; }
            public bool chkSel { get; set; }
        }
        #endregion
    }
}
