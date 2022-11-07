//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: ASSOCIAÇÃO DE DOCUMENTOS DE MATRÍCULA À SÉRIE/CURSO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3018_AssociacaoDoctoMatriculaSerie
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
                CarregaUnidade();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaGridDocumentos();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            TB208_CURSO_DOCTOS tb208;

//--------> Varre toda a grid de Documentos
            foreach (GridViewRow row in grdDocumentos.Rows)
            {
                HiddenField hfCoTpDocMat = ((HiddenField)row.Cells[2].FindControl("hdCoTpMat"));
                CheckBox ckSelect = ((CheckBox)row.Cells[0].FindControl("ckSelect"));
                int coTpDocMat = Convert.ToInt32(hfCoTpDocMat.Value);

                if (ckSelect.Checked == true)
                {
                    tb208 = (from lTb208 in TB208_CURSO_DOCTOS.RetornaTodosRegistros()
                             where lTb208.CO_EMP == LoginAuxili.CO_EMP && lTb208.CO_MODU_CUR == modalidade 
                             && lTb208.CO_CUR == serie && lTb208.CO_TP_DOC_MAT == coTpDocMat
                             select lTb208).FirstOrDefault();

                    if (tb208 == null)
                    {
                        tb208 = new TB208_CURSO_DOCTOS();

                        tb208.CO_CUR = serie;
                        tb208.CO_MODU_CUR = modalidade;
                        tb208.CO_EMP = coEmp;
                        tb208.CO_TP_DOC_MAT = coTpDocMat;

                        tb208.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, modalidade, serie);
                        tb208.TB121_TIPO_DOC_MATRICULA = TB121_TIPO_DOC_MATRICULA.RetornaPelaChavePrimaria(coTpDocMat);

                        TB208_CURSO_DOCTOS.SaveOrUpdate(tb208, true);
                    }
                }
                else
                {
                    tb208 = (from lTb208 in TB208_CURSO_DOCTOS.RetornaTodosRegistros()
                             where lTb208.CO_EMP == LoginAuxili.CO_EMP && lTb208.CO_MODU_CUR == modalidade 
                             && lTb208.CO_CUR == serie && lTb208.CO_TP_DOC_MAT == coTpDocMat
                             select lTb208).FirstOrDefault();

                    if (tb208 != null)
                        TB208_CURSO_DOCTOS.Delete(tb208, true);
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
            var tb208 = TB208_CURSO_DOCTOS.RetornaPeloCoEmpCoModuCurCoCur(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp), 
            QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur));

            if (tb208 != null)
            {
                CarregaUnidade();
                ddlUnidade.SelectedValue = tb208.FirstOrDefault().CO_EMP.ToString();
                ddlUnidade.Enabled = false;
                CarregaModalidades();
                ddlModalidade.SelectedValue = tb208.FirstOrDefault().CO_MODU_CUR.ToString();
                ddlModalidade.Enabled = false;
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb208.FirstOrDefault().CO_CUR.ToString();
                ddlSerieCurso.Enabled = false;

                foreach (GridViewRow linha in grdDocumentos.Rows)
                {
                    HiddenField hfCoTpDocMat = ((HiddenField)linha.Cells[2].FindControl("hdCoTpMat"));
                    CheckBox ckSelect = ((CheckBox)linha.Cells[0].FindControl("ckSelect"));
                    int coTpDocMat = Convert.ToInt32(hfCoTpDocMat.Value);

                    foreach (var iTb208 in tb208)
                    {
                        if (coTpDocMat == iTb208.CO_TP_DOC_MAT)
                            ckSelect.Checked = true;
                    }
                }    
            }                   
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaPeloUsuario(LoginAuxili.CO_COL)
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
        }

        /// <summary>
        /// Método que carrega a grid de Documentos
        /// </summary>
        private void CarregaGridDocumentos()
        {
            grdDocumentos.DataSource = TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros();
            grdDocumentos.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb01.CO_MODU_CUR == modalidade
                                        join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                        where tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                        select new { tb01.CO_CUR, tb01.CO_SIGL_CUR }).Distinct().OrderBy(c => c.CO_SIGL_CUR);

            ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void grdDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox ckSelect = ((CheckBox)e.Row.FindControl("ckSelect"));
            }
        }

    }
}
