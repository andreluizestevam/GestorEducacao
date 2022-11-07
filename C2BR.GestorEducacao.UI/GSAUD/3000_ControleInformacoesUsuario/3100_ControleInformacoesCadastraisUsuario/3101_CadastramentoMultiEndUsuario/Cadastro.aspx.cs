//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: CADASTRAMENTO DE MÚLTIPLOS ENDEREÇOS DO ALUNO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------


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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3101_CadastramentoMultiEndUsuario
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
                CarregaTipoEndereco();
                CarregaUF();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        private void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        private void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int idTipoEndereco = ddlTipoEndereco.SelectedValue != "" ? int.Parse(ddlTipoEndereco.SelectedValue) : 0;
            int coBairro = ddlBairro.SelectedValue != "" ? int.Parse(ddlBairro.SelectedValue) : 0;

            TB241_ALUNO_ENDERECO tb241 = RetornaEntidade();

            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb241.TB07_ALUNO = refAluno;
            refAluno.TB25_EMPRESA1Reference.Load();
            tb241.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
            tb241.TB238_TIPO_ENDERECO = TB238_TIPO_ENDERECO.RetornaPelaChavePrimaria(idTipoEndereco);
            tb241.DS_ENDERECO = txtLogradouro.Text;
            tb241.NR_ENDERECO = txtNumero.Text != "" ? (decimal?)decimal.Parse(txtNumero.Text) : null;
            tb241.DS_COMPLEMENTO = txtComplemento.Text != "" ? txtComplemento.Text : null;
            tb241.CO_CEP = txtCep.Text.Replace("-", "");
            tb241.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(coBairro);
            tb241.CO_SITUACAO = ddlSituacao.SelectedValue;
            tb241.FL_PRINCIPAL = ddlPrincipal.SelectedValue == "S";
            tb241.DT_SITUACAO = DateTime.Now;
            CurrentPadraoCadastros.CurrentEntity = tb241;

            if (tb241.FL_PRINCIPAL == true)
                AtualizarEnderecoPrincipal(tb241);
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            if (!IsPostBack)
            {
                TB241_ALUNO_ENDERECO tb241 = RetornaEntidade();

                if (tb241 != null)
                {
                    tb241.TB07_ALUNOReference.Load();
                    tb241.TB238_TIPO_ENDERECOReference.Load();
                    tb241.TB905_BAIRROReference.Load();

                    ddlAluno.SelectedValue = tb241.TB07_ALUNO.CO_ALU.ToString();
                    ddlTipoEndereco.SelectedValue = tb241.TB238_TIPO_ENDERECO.ID_TIPO_ENDERECO.ToString();
                    txtLogradouro.Text = tb241.DS_ENDERECO;
                    txtNumero.Text = tb241.NR_ENDERECO != null ? tb241.NR_ENDERECO.ToString() : "";
                    txtComplemento.Text = tb241.DS_COMPLEMENTO != null ? tb241.DS_COMPLEMENTO.ToString() : "";
                    txtCep.Text = tb241.CO_CEP;
                    ddlUf.SelectedValue = tb241.TB905_BAIRRO.CO_UF;
                    CarregaCidade();
                    ddlCidade.SelectedValue = tb241.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairro();
                    ddlBairro.SelectedValue = tb241.TB905_BAIRRO.CO_BAIRRO.ToString();
                    ddlSituacao.SelectedValue = tb241.CO_SITUACAO;
                    ddlPrincipal.SelectedValue = tb241.FL_PRINCIPAL ? "S" : "N";
                }
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB241_ALUNO_ENDERECO</returns>
        private TB241_ALUNO_ENDERECO RetornaEntidade()
        {
            TB241_ALUNO_ENDERECO tb241 = TB241_ALUNO_ENDERECO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb241 == null) ? new TB241_ALUNO_ENDERECO() : tb241;
        }

        /// <summary>
        /// Método que atualiza endereço principal do aluno
        /// </summary>
        /// <param name="pTb241">Entidade TB241_ALUNO_ENDERECO</param>
        private void AtualizarEnderecoPrincipal(TB241_ALUNO_ENDERECO pTb241)
        {
            IList<TB241_ALUNO_ENDERECO> lstTb241 = TB241_ALUNO_ENDERECO.RetornaPeloAluno(pTb241.TB07_ALUNO.CO_ALU, pTb241.TB07_ALUNO.CO_EMP).ToList();

            foreach (TB241_ALUNO_ENDERECO tb241 in lstTb241)
            {
                if (pTb241.ID_ALUNO_ENDERECO != tb241.ID_ALUNO_ENDERECO)
                {
                    if (tb241.FL_PRINCIPAL)
                    {
                        tb241.FL_PRINCIPAL = false;
                        TB241_ALUNO_ENDERECO.SaveOrUpdate(tb241, false);
                    }
                }
            }
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
                                   select new { tb07.CO_ALU, tb07.NO_ALU, }).OrderBy(a => a.NO_ALU);

            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Endereço
        /// </summary>
        private void CarregaTipoEndereco()
        {
            ddlTipoEndereco.DataSource = (from tb238 in TB238_TIPO_ENDERECO.RetornaTodosRegistros()
                                          where tb238.CD_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb238.CO_SITUACAO.ToUpper() == "A"
                                          select new { tb238.ID_TIPO_ENDERECO, tb238.NM_TIPO_ENDERECO });

            ddlTipoEndereco.DataValueField = "ID_TIPO_ENDERECO";
            ddlTipoEndereco.DataTextField = "NM_TIPO_ENDERECO";
            ddlTipoEndereco.DataBind();

            ddlTipoEndereco.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        private void CarregaUF()
        {
            ddlUf.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUf.DataValueField = "CODUF";
            ddlUf.DataTextField = "CODUF";
            ddlUf.DataBind();

            ddlUf.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidade()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUf.SelectedValue);

            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairro()
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Items.Clear();
                return;
            }

            ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Items.Insert(0, new ListItem("", ""));
        }
        #endregion        

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairro();
        }

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidade();
            CarregaBairro();
            ddlBairro.SelectedIndex = -1;
        }

    }
}