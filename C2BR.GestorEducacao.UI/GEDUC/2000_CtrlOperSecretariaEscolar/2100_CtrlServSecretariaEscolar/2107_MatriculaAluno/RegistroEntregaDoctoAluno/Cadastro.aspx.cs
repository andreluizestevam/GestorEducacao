//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: REGISTRO DE ENTREGA DE DOCUMENTOS DO ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.RegistroEntregaDoctoAluno
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
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione um aluno.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

            if (IsPostBack) return;

            txtFuncionario.Text = LoginAuxili.NOME_USU_LOGADO;
            txtDataC.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = Convert.ToInt32(hdAluno.Value);

            TB120_DOC_ALUNO_ENT tb120;

//--------> Varre toda a grid de Documentos
            foreach (GridViewRow row in grdDocumentos.Rows)
            {
                int coTpDocMat  = (int)grdDocumentos.DataKeys[row.RowIndex].Values[0];               

                if (((CheckBox)row.Cells[0].FindControl("ckSelect")).Checked)
                {
                    tb120 = (from lTb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                             where lTb120.CO_EMP == LoginAuxili.CO_EMP && lTb120.CO_ALU == coAlu && lTb120.CO_TP_DOC_MAT == coTpDocMat
                             select lTb120).FirstOrDefault();

                    if (tb120 == null)
                    {
                        tb120 = new TB120_DOC_ALUNO_ENT();
                        var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                        tb120.CO_ALU = coAlu;
                        tb120.CO_TP_DOC_MAT = coTpDocMat;
                        tb120.CO_EMP = LoginAuxili.CO_EMP;
                        tb120.TB07_ALUNO = refAluno;
                        tb120.TB121_TIPO_DOC_MATRICULA = TB121_TIPO_DOC_MATRICULA.RetornaPelaChavePrimaria(coTpDocMat);
                        refAluno.TB25_EMPRESA1Reference.Load();
                        tb120.TB25_EMPRESA = refAluno.TB25_EMPRESA1;

                        TB120_DOC_ALUNO_ENT.SaveOrUpdate(tb120, false);
                    }
                }
                else
                {
                    tb120 = (from lTb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                             where lTb120.CO_EMP == LoginAuxili.CO_EMP && lTb120.CO_ALU == coAlu && lTb120.CO_TP_DOC_MAT == coTpDocMat
                             select lTb120).FirstOrDefault();

                    if (tb120 != null)
                        TB120_DOC_ALUNO_ENT.Delete(tb120, false);
                }
            }

            GestorEntities.CurrentContext.SaveChanges();

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Efetuado com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            string anoMatricula = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano);
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu);

            var alunoSelecionado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                    join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                    join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb06.CO_TUR
                                    join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb01.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                    where tb08.CO_ANO_MES_MAT == anoMatricula && tb08.CO_ALU == coAlu && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                    select new
                                    {
                                        tb08.CO_CUR, tb08.CO_TUR, tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU, tb01.NO_CUR, 
                                        tb06.TB129_CADTURMAS.NO_TURMA, tb44.CO_MODU_CUR, tb44.DE_MODU_CUR
                                    }).FirstOrDefault();

            if (alunoSelecionado != null)
            {
                txtAluno.Text = alunoSelecionado.NO_ALU;
                txtAno.Text = anoMatricula.ToString();
                txtModalidade.Text = alunoSelecionado.DE_MODU_CUR;
                txtSérie.Text = alunoSelecionado.NO_CUR;
                txtTurma.Text = alunoSelecionado.NO_TURMA;
                hdAluno.Value = alunoSelecionado.CO_ALU.ToString();
                hdCodMod.Value = alunoSelecionado.CO_MODU_CUR.ToString();
                hdSerie.Value = alunoSelecionado.CO_CUR.ToString();
                hdTurma.Value = alunoSelecionado.CO_TUR.ToString();

                CarregaGridDocumentos();
            }            
        }

        /// <summary>
        /// Método que carrega a grid de Documentos
        /// </summary>
        private void CarregaGridDocumentos()
        {
            int serie = hdSerie.Value == "" ? 0 : Convert.ToInt32(hdSerie.Value);
            int modalidade = hdCodMod.Value == "" ? 0 : Convert.ToInt32(hdCodMod.Value);
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu);


            if (serie != 0 && modalidade != 0)
            {
                grdDocumentos.DataKeyNames = new string[] { "CO_TP_DOC_MAT" };

                var res = (from tb121 in TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros()
                                            join tb208 in TB208_CURSO_DOCTOS.RetornaTodosRegistros() on tb121.CO_TP_DOC_MAT equals tb208.CO_TP_DOC_MAT
                                            where tb208.CO_EMP == LoginAuxili.CO_EMP && tb208.CO_CUR == serie && tb208.CO_MODU_CUR == modalidade
                                            select new Documentos
                                            {
                                                CO_TP_DOC_MAT = tb121.CO_TP_DOC_MAT,
                                                DE_TP_DOC_MAT = tb121.DE_TP_DOC_MAT,
                                                SIG_TP_DOC_MAT = tb121.SIG_TP_DOC_MAT
                                            }).Distinct().OrderBy(t => t.DE_TP_DOC_MAT);

                foreach (Documentos d in res)
                {
                    d.chkSel = TB120_DOC_ALUNO_ENT.RetornaTodosRegistros().Where(w => w.CO_TP_DOC_MAT == d.CO_TP_DOC_MAT && w.CO_ALU == coAlu).Any();
                }

                grdDocumentos.DataSource = res;
                grdDocumentos.DataBind();
            }
        }

        public class Documentos
        {
            public int CO_TP_DOC_MAT { get; set; }
            public string DE_TP_DOC_MAT { get; set; }
            public string SIG_TP_DOC_MAT { get; set; }
            public bool chkSel { get; set; }
        }
        #endregion
        
        protected void grdDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int coAlu = Convert.ToInt32(hdAluno.Value);

            CheckBox cbSelect = ((CheckBox)e.Row.FindControl("ckSelect"));

            HiddenField hfCoTpDocMat = (HiddenField)e.Row.FindControl("hdCoTpDoc");
            int coTpDocMat = 0;

            if (cbSelect == null && hfCoTpDocMat == null)
                return;

            coTpDocMat = Convert.ToInt32(hfCoTpDocMat.Value);

            TB120_DOC_ALUNO_ENT tb120 = (from lTb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                                         where lTb120.CO_EMP == LoginAuxili.CO_EMP && lTb120.CO_ALU == coAlu && lTb120.CO_TP_DOC_MAT == coTpDocMat
                                         select lTb120).FirstOrDefault();

            if (tb120 == null)
                return;

            if (tb120.CO_TP_DOC_MAT == coTpDocMat)
                cbSelect.Checked = true;
            else
                cbSelect.Checked = false;
        }
    }
}
