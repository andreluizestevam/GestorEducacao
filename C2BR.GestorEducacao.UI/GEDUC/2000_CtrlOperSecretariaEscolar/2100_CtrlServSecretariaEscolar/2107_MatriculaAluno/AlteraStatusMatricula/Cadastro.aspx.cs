//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: ALTERAÇÃO DE STATUS DE MATRÍCULA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//07/06/2013+ Thales p Andrade           + Quando o Aluno Estiver Cancelado ou Trancado
//          +                            + não pode alterar os Status
//----------+----------------------------+------------------------------------

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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.AlteraStatusMatricula
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

        //Recebe o Valor da matricula se ela e matricula: Matriculado Cancelado Ou trancado 
        string ValoMatric = QueryStringAuxili.RetornaQueryStringPelaChave("tipoMatri");
//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int serie = Convert.ToInt32(hdSerie.Value);
            int coAlu = Convert.ToInt32(hdAluno.Value);

            string anoSelecionado = txtAno.Text;

           

            TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, serie, anoSelecionado, "1");



            if (tb08 != null && ddlStatus.SelectedValue != "R" )
            {
                
                if (tb08.CO_SIT_MAT == "R")
                {
                    if(ddlStatus.SelectedValue == "A" && AuxiliGeral.AtivarPreMatricula(tb08.CO_ALU, tb08.CO_CUR, tb08.CO_ANO_MES_MAT, tb08.NU_SEM_LET))
                    {
                        AuxiliPagina.RedirecionaParaPaginaSucesso("A Pré-Matrícula foi ativada com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                        return;
                    }
                    else
                    {
                        AuxiliPagina.RedirecionaParaPaginaErro("Não foi possível alterar o status da Pré-Matrícula.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    }
                }
                else
                {
                    TB209_HISTO_MATRICULA tb209 = new TB209_HISTO_MATRICULA();

                    tb209.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb209.CO_ALU_CAD = tb08.CO_ALU_CAD;
                    tb209.CO_STA_ALT = tb08.CO_SIT_MAT;
                    tb209.CO_STA_ATUAL = ddlStatus.SelectedValue;
                    tb209.DE_OBS = txtOBS.Text;
                    tb209.DT_ALT = Convert.ToDateTime(txtDataC.Text);
                    tb209.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                    TB209_HISTO_MATRICULA.SaveOrUpdate(tb209, false);

                    tb08.CO_SIT_MAT = ddlStatus.SelectedValue;
                    TB08_MATRCUR.SaveOrUpdate(tb08, false);

                    GestorEntities.CurrentContext.SaveChanges();
                }
                AuxiliPagina.RedirecionaParaPaginaSucesso("Status Alterado com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
            else
            {
                if(ddlStatus.SelectedValue != "R" )
                    AuxiliPagina.RedirecionaParaPaginaErro("Matrícula do aluno não encontrada.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                else
                    AuxiliPagina.RedirecionaParaPaginaErro("Matrícula não pode ser alterada para Pré-Matrícula.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }                   
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
            int CoCur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            string nuSemLet = QueryStringAuxili.RetornaQueryStringPelaChave("nuSemLet");
            int CoModuCur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);

            var infoMatricAluno = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                   join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                   join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb06.CO_TUR
                                   where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                   && tb08.CO_ANO_MES_MAT == anoMatricula
                                   && tb08.TB44_MODULO.CO_MODU_CUR == CoModuCur
                                   && tb08.CO_ALU == coAlu 
                                   && tb08.CO_CUR == CoCur
                                   && tb08.NU_SEM_LET == nuSemLet
                                   select new
                                   {
                                       tb08.CO_CUR,
                                       tb08.CO_TUR,
                                       tb08.CO_ALU,
                                       tb08.TB07_ALUNO.NO_ALU,
                                       tb01.NO_CUR,
                                       tb06.TB129_CADTURMAS.NO_TURMA,
                                       tb08.TB44_MODULO.CO_MODU_CUR,
                                       tb08.TB44_MODULO.DE_MODU_CUR,
                                       tb08.CO_SIT_MAT,
                                       tb08.FLA_REMATRICULADO,
                                       
                                   }).FirstOrDefault();

            if (infoMatricAluno != null )
            {
                txtAluno.Text = infoMatricAluno.NO_ALU;
                txtAno.Text = anoMatricula.ToString();
                txtModalidade.Text = infoMatricAluno.DE_MODU_CUR;
                txtSérie.Text = infoMatricAluno.NO_CUR;
                txtTurma.Text = infoMatricAluno.NO_TURMA;
                hdAluno.Value = infoMatricAluno.CO_ALU.ToString();
                hdCodMod.Value = infoMatricAluno.CO_MODU_CUR.ToString();
                hdSerie.Value = infoMatricAluno.CO_CUR.ToString();
                hdTurma.Value = infoMatricAluno.CO_TUR.ToString();
                string situacaoMat = "";
                switch(infoMatricAluno.CO_SIT_MAT)
                {
                    case "A":
                    case "C":
                    case "T":
                    case "R":
                        situacaoMat = infoMatricAluno.CO_SIT_MAT;
                        break;
                }    
                ddlStatus.SelectedValue = situacaoMat;
            }
            if (ValoMatric == "Cancelado")
            {
                txtOBS.Enabled = false;
            }
        }
        #endregion
    }
}
