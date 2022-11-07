//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: REGISTRO DE SOLICITAÇÃO DE MATERIAL
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.RegistroSolicitacaoMaterial
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
//            int coAlu = Convert.ToInt32(hdAluno.Value);

//            TB114_FARDMAT tb114;

////--------> Varre toda a grid de Material
//            foreach (GridViewRow linha in grdMaterial.Rows)
//            {
//                HiddenField hfCoProd = ((HiddenField)linha.Cells[2].FindControl("hdCoProd"));
//                int coProd = Convert.ToInt32(hfCoProd.Value);

//                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
//                {
//                    tb114 = (from lTb114 in TB114_FARDMAT.RetornaTodosRegistros()
//                             where lTb114.CO_EMP == LoginAuxili.CO_EMP && lTb114.CO_ALU == coAlu && lTb114.CO_PROD == coProd
//                             select lTb114).FirstOrDefault();

//                    if (tb114 == null)
//                    {
//                        tb114 = new TB114_FARDMAT();
//                        var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
//                        tb114.CO_ALU = coAlu;
//                        tb114.CO_PROD = coProd;
//                        tb114.CO_EMP = LoginAuxili.CO_EMP;
//                        tb114.TB07_ALUNO = refAluno;
//                        tb114.TB90_PRODUTO = TB90_PRODUTO.RetornaPelaChavePrimaria(coProd, LoginAuxili.CO_EMP);
//                        tb114.DT_PEDIDO = DateTime.Now;
//                        tb114.TIPO = "M";
//                        refAluno.TB25_EMPRESA1Reference.Load();
//                        tb114.TB25_EMPRESA = refAluno.TB25_EMPRESA1;

//                        TB114_FARDMAT.SaveOrUpdate(tb114, false);
//                    }
//                }
//                else
//                {
//                    tb114 = (from lTb114 in TB114_FARDMAT.RetornaTodosRegistros()
//                             where lTb114.CO_EMP == LoginAuxili.CO_EMP && lTb114.CO_ALU == coAlu && lTb114.CO_PROD == coProd
//                             select lTb114).FirstOrDefault();

//                    if (tb114 != null)
//                        TB114_FARDMAT.Delete(tb114, false);
//                }
//            }

//            GestorEntities.CurrentContext.SaveChanges();

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
                CarregaValor();
            }            
        }

        /// <summary>
        /// Método que carrega o grid Materiais
        /// </summary>
        private void CarregaGridDocumentos()
        {
            grdMaterial.DataSource = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                                      where tb90.TB124_TIPO_PRODUTO.CO_TIP_PROD == 1 && tb90.CO_SITU_PROD == "A" && tb90.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                      select new
                                      {
                                          tb90.NO_PROD_RED, tb90.CO_PROD, tb90.TB95_CATEGORIA.DES_CATEG, tb90.VL_UNIT_PROD, tb90.CO_REFE_PROD
                                      }).OrderBy( p => p.NO_PROD_RED );
            grdMaterial.DataBind();
        }

        /// <summary>
        /// Método que atualiza o campo de valor "txtValor" com os valores totais dos itens selecionados
        /// </summary>
        private void CarregaValor()
        {
            decimal decimalValorTotal = 0;

//--------> Varre toda a grid de Material
            foreach (GridViewRow linha in grdMaterial.Rows)
            {
                HiddenField hdVlrAtiv = ((HiddenField)linha.Cells[2].FindControl("hdVlr"));

                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    decimalValorTotal += Convert.ToDecimal(hdVlrAtiv.Value);
            }

            txtValor.Text = decimalValorTotal.ToString();
        }        

        #endregion

        protected void grdDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //int coAlu = Convert.ToInt32(hdAluno.Value);

            //CheckBox cbSelect = ((CheckBox)e.Row.FindControl("ckSelect"));

            //HiddenField hfCoProd = (HiddenField)e.Row.FindControl("hdCoProd");
            //int coProd = 0;

            //if (cbSelect == null && hfCoProd == null)
            //    return;

            //coProd = Convert.ToInt32(hfCoProd.Value);

            //TB114_FARDMAT tb114 = (from lTb114 in TB114_FARDMAT.RetornaTodosRegistros()
            //                       where lTb114.CO_EMP == LoginAuxili.CO_EMP && lTb114.CO_ALU == coAlu && lTb114.CO_PROD == coProd
            //                       select lTb114).FirstOrDefault();

            //if (tb114 == null)
            //    return;

            //if (tb114.CO_PROD == coProd)
            //    cbSelect.Checked = true;
            //else
            //    cbSelect.Checked = false;

        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            CarregaValor();
        }
    }
}
