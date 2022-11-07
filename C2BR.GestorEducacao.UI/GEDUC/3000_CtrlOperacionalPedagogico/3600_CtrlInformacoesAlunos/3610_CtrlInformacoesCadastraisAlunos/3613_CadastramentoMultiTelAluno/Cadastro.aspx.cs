//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: CADASTRAMENTO DE MÚLTIPLOS TELEFONES DO ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3613_CadastramentoMultiTelAluno
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
                CarregaAlunos();
                CarregaTipoTelefone();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        private void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        private void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string strTelefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "");
            int idTipoTelefone = ddlTipoTelefone.SelectedValue != "" ? int.Parse(ddlTipoTelefone.SelectedValue) : 0;

            TB242_ALUNO_TELEFONE tb242 = RetornaEntidade();

            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb242.TB07_ALUNO = refAluno;
            refAluno.TB25_EMPRESA1Reference.Load();
            tb242.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
            tb242.TB239_TIPO_TELEFONE = TB239_TIPO_TELEFONE.RetornaPelaChavePrimaria(idTipoTelefone);
            tb242.NR_DDD = int.Parse(strTelefone.Substring(0, 2));
            tb242.NR_TELEFONE = int.Parse(strTelefone.Substring(2, 8));
            tb242.CO_SITUACAO = ddlSituacao.SelectedValue;
            tb242.DT_SITUACAO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb242;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB242_ALUNO_TELEFONE tb242 = RetornaEntidade();

            if (tb242 != null)
            {
                tb242.TB07_ALUNOReference.Load();
                tb242.TB239_TIPO_TELEFONEReference.Load();

                ddlAluno.SelectedValue = tb242.TB07_ALUNO.CO_ALU.ToString();
                ddlTipoTelefone.SelectedValue = tb242.TB239_TIPO_TELEFONE.ID_TIPO_TELEFONE.ToString();
                txtTelefone.Text = string.Concat(tb242.NR_DDD, tb242.NR_TELEFONE);
                ddlSituacao.SelectedValue = tb242.CO_SITUACAO; 
            }       
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB242_ALUNO_TELEFONE</returns>
        private TB242_ALUNO_TELEFONE RetornaEntidade()
        {
            TB242_ALUNO_TELEFONE tb242 = TB242_ALUNO_TELEFONE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb242 == null) ? new TB242_ALUNO_TELEFONE() : tb242;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAlunos()
        {
            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                   where tb07.CO_SITU_ALU.ToUpper() == "A"
                                   select new { tb07.CO_ALU, tb07.NO_ALU, }).OrderBy( a => a.NO_ALU );

            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem(string.Empty, string.Empty));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Telefone
        /// </summary>
        private void CarregaTipoTelefone()
        {
            ddlTipoTelefone.DataSource = (from tb239 in TB239_TIPO_TELEFONE.RetornaTodosRegistros()
                                          where tb239.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb239.CO_SITUACAO.ToUpper() == "A"
                                          select new { tb239.ID_TIPO_TELEFONE, tb239.NM_TIPO_TELEFONE });

            ddlTipoTelefone.DataValueField = "ID_TIPO_TELEFONE";
            ddlTipoTelefone.DataTextField  = "NM_TIPO_TELEFONE";
            ddlTipoTelefone.DataBind();

            ddlTipoTelefone.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}