﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: ASSOCIAÇÃO DE ALUNO A PAI/MÃE OU RESPONSÁVEL DE ALUNO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3614_AssociacaoAlunoPaiMaeRespon
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
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione um aluno ou responsável.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);

            if (!IsPostBack)
            {
                CarregaAluno();
                CarregaResponsaveis();
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Alteração de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coResp = ddlResponsavel.SelectedValue != "" ? int.Parse(ddlResponsavel.SelectedValue) : 0;
//--------> Só é permitida a edição
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                TB07_ALUNO tb07 = RetornaEntidade();

                if(ddlResponsavel.SelectedValue != "")
                    tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);

                tb07.DT_ALT_REGISTRO = DateTime.Now;

                CurrentPadraoCadastros.CurrentEntity = tb07;
            }
            else
                AuxiliPagina.RedirecionaParaPaginaBusca();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB07_ALUNO tb07 = RetornaEntidade();

            if (tb07 != null)
            {
                ddlAluno.SelectedValue = tb07.CO_ALU.ToString();
                CarregaCodigoAndResponsavel();
                ddlResponsavel.SelectedValue = tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.CO_RESP.ToString() : "";                
            }            
        }

//====> 
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB07_ALUNO</returns>
        private TB07_ALUNO RetornaEntidade()
        {
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb07 == null) ? new TB07_ALUNO() : tb07;
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                  select new { tb07.CO_ALU, tb07.NO_ALU });

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            CarregaCodigoAndResponsavel();
        }

        /// <summary>
        /// Método que carrega o dropdown de Responsáveis
        /// </summary>
        private void CarregaResponsaveis()
        {
            ddlResponsavel.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                        select new { tb108.CO_RESP, tb108.NO_RESP });

            ddlResponsavel.DataTextField = "NO_RESP";
            ddlResponsavel.DataValueField = "CO_RESP";
            ddlResponsavel.DataBind();

            ddlResponsavel.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que preenche o campo código e Responsável do Aluno
        /// </summary>
        private void CarregaCodigoAndResponsavel()
        {
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            tb07.TB108_RESPONSAVELReference.Load();
            txtCodigo.Text = tb07.NU_NIRE.ToString();
            txtResponsavelAtual.Text = tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL.NO_RESP : "";
        }
        #endregion    
    
        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoAndResponsavel();
        }
    }
}