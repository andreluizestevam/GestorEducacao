//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: CORREÇÃO DOS DADOS DA MATRÍCULA
// DATA DE CRIAÇÃO: 24/05/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.CorrecaoMatriculaAluno
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
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaFormulario();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }


        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = int.Parse(hidCoAlu.Value);
            int coCur = int.Parse(hidCoCur.Value);
            string coAno = hidCoAno.Value;
            string nuSem = hidNuSem.Value;

            //TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, coCur, coAno, nuSem);
            TB08_MATRCUR tb08 = RetornaEntidade();

            if (tb08 != null)
            {
                tb08.CO_TUR = tb08.CO_TUR;

                int coResp = ddlRespN.SelectedValue != "" ? int.Parse(ddlRespN.SelectedValue) : 0;
                if (coResp != 0)
                {
                    tb08.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);
                }

                int coBols = ddlBolsaN.SelectedValue != "" ? int.Parse(ddlBolsaN.SelectedValue) : 0;
                if (coBols != 0)
                {
                    tb08.TB148_TIPO_BOLSA = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(coBols);
                }

                if (GestorEntities.SaveOrUpdate(tb08) < 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item");
                    return;
                }
                else
                {
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Matrícula editada com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
            }
        }

        void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {
            //TB07_ALUNO tb07 = RetornaEntidade();

            //if (tb07 != null)
            //{
            //    tb07.TB164_INST_ESP.Clear();
            //    tb07.TB136_ALU_PROG_SOCIAIS.Clear();

            //    if (GestorEntities.Delete(tb07) <= 0)
            //        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
            //}
            AuxiliPagina.EnvioMensagemErro(this, "Não é possível excluir uma matrícula.");
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB08_MATRCUR tb08 = RetornaEntidade();

            //===> CARREGA AS REFERÊNCIAS DA TABELA TB08_MATRCUR
            tb08.TB07_ALUNOReference.Load();
            tb08.TB44_MODULOReference.Load();
            tb08.TB148_TIPO_BOLSAReference.Load();
            tb08.TB108_RESPONSAVELReference.Load();

            //===> ALIMENTA OS CAMPOS HIDDEN UTILIZADOS NA GRAVAÇÃO DOS DADOS
            hidCoAlu.Value = tb08.TB07_ALUNO.CO_ALU.ToString();
            hidCoCur.Value = tb08.CO_CUR.ToString();
            hidCoAno.Value = tb08.CO_ANO_MES_MAT;
            hidNuSem.Value = tb08.NU_SEM_LET;

            //===> CARREGA OS DADOS DOS ALUNOS
            txtNire.Text = tb08.TB07_ALUNO.NU_NIRE.ToString();
            txtNomeAlu.Text = tb08.TB07_ALUNO.NO_ALU;

            //===> CARREGA OS DADOS DE MODALIDADE, SÉRIE E TURMA
            string deModu = tb08.TB44_MODULO.DE_MODU_CUR;
            string noCur = TB01_CURSO.RetornaTodosRegistros().Where(w => w.CO_CUR == tb08.CO_CUR).FirstOrDefault().NO_CUR;
            string noTur = TB129_CADTURMAS.RetornaTodosRegistros().Where(w => w.CO_TUR == tb08.CO_TUR).FirstOrDefault().NO_TURMA;

            lblInfo.InnerText = "(" + deModu + " - " + noCur + " - " + noTur + ")";

            //===> CARREGA OS DADOS DE BOLSA
            if (tb08.TB148_TIPO_BOLSA != null)
            {
                ddlTipoBolsa.SelectedValue = tb08.TB148_TIPO_BOLSA.TP_GRUPO_BOLSA;
                CarregaBolsa();
                CarregaBolsaN();
                ddlBolsa.SelectedValue = tb08.TB148_TIPO_BOLSA.CO_TIPO_BOLSA.ToString();
                TB148_TIPO_BOLSA tb148 = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(tb08.TB148_TIPO_BOLSA.CO_TIPO_BOLSA.ToString()));

                txtValorBolsa.Text = tb148.VL_TIPO_BOLSA.Value.ToString("N2");
                if (tb148.FL_TIPO_VALOR_BOLSA == "P")
                {
                    chkValorPerc.Checked = true;
                }
                else
                {
                    chkValorPerc.Checked = false;
                }
            }
            else
            {
                CarregaBolsa();
                CarregaBolsaN();
            }

            //===> CARREGA OS DADOS DO RESPONSÁVEL FINANCEIRO
            CarregaResponsavel();
            CarregaResponsavelN();
            ddlResp.SelectedValue = tb08.TB108_RESPONSAVEL.CO_RESP.ToString();
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB07_ALUNO</returns>4
        private TB08_MATRCUR RetornaEntidade()
        {
            //TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id)); int CO_ALU, int CO_CUR, string CO_ANO_MES_MAT, string NU_SEM_LET
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            int coCur = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            string Ano = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano);
            string nuSem = QueryStringAuxili.RetornaQueryStringPelaChave("nuSem");

            TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, coCur, Ano, nuSem);
            return (tb08 == null) ? new TB08_MATRCUR() : tb08;
        }

        #endregion

        #region Carregamento

        private void CarregaBolsa()
        {
            var tb = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(c => c.TP_GRUPO_BOLSA == ddlTipoBolsa.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            //==> CARREGA BOLSA ATUAL
            ddlBolsa.DataSource = tb;
            ddlBolsa.DataTextField = "NO_TIPO_BOLSA";
            ddlBolsa.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsa.DataBind();
            ddlBolsa.Items.Insert(0, new ListItem("Nenhuma", ""));
        }

        private void CarregaBolsaN()
        {
            var tb = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(c => c.TP_GRUPO_BOLSA == ddlTipoBolsaN.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            //===> CARREGA NOVA BOLSA
            ddlBolsaN.DataSource = tb;
            ddlBolsaN.DataTextField = "NO_TIPO_BOLSA";
            ddlBolsaN.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsaN.DataBind();
            ddlBolsaN.Items.Insert(0, new ListItem("Nenhuma", ""));
        }

        private void CarregaResponsavel()
        {
            var tb108 = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            //===> CARREGA O RESPONSÁVEL ATUAL
            ddlResp.DataSource = tb108;
            ddlResp.DataTextField = "NO_RESP";
            ddlResp.DataValueField = "CO_RESP";
            ddlResp.DataBind();
            ddlResp.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaResponsavelN()
        {
            var tb108 = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            //===> CARREGA O NOVO RESPONSÁVEL
            ddlRespN.DataSource = tb108;
            ddlRespN.DataTextField = "NO_RESP";
            ddlRespN.DataValueField = "CO_RESP";
            ddlRespN.DataBind();
            ddlRespN.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Validadores

        #endregion

        protected void ddlTipoBolsa_SelectedIndexChange(object sender, EventArgs e)
        {
            CarregaBolsa();
        }

        protected void ddlTipoBolsaN_SelectedIndexChange(object sender, EventArgs e)
        {
            CarregaBolsaN();
        }

        protected void ddlBolsaN_SelectedIndexChange(object sender, EventArgs e)
        {
            if (ddlBolsaN.SelectedValue != "N")
            {
                TB148_TIPO_BOLSA tb148 = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(ddlBolsaN.SelectedValue));

                txtValorBolsaN.Text = tb148.VL_TIPO_BOLSA.Value.ToString("N2");
                if (tb148.FL_TIPO_VALOR_BOLSA == "P")
                {
                    chkValorPercN.Checked = true;
                }
                else
                {
                    chkValorPercN.Checked = false;
                }
            }
            else
            {
                txtValorBolsaN.Text = "";
                chkValorPercN.Checked = false;
            }
        }

        protected void ddlBolsa_SelectedIndexChange(object sender, EventArgs e)
        {
            if (ddlBolsa.SelectedValue != "N")
            {
                TB148_TIPO_BOLSA tb148 = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(ddlBolsa.SelectedValue));

                txtValorBolsa.Text = tb148.VL_TIPO_BOLSA.Value.ToString("N2");
                if (tb148.FL_TIPO_VALOR_BOLSA == "P")
                {
                    chkValorPerc.Checked = true;
                }
                else
                {
                    chkValorPerc.Checked = false;
                }
            }
            else
            {
                txtValorBolsa.Text = "";
                chkValorPerc.Checked = false;
            }
        }
    }
}
