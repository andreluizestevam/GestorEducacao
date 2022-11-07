//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: DADOS OPERACIONAIS DE CONTRATO
// OBJETIVO: CADASTRAMENTO DE TEMPO/HORÁRIO DE AULA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Collections.Generic;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0104_CadastramentoTempoAula
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
            if (IsPostBack) return;

            CarregaModalidade();
            CarregaSerieCurso();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int tempoAula = ddlTempoAula.SelectedValue != "" ? int.Parse(ddlTempoAula.SelectedValue) : 0;
            int horaIni, horaFim;

            if( (modalidade == 0) || (serie == 0) || (tempoAula == 0) || (ddlTurnoTA.SelectedValue == ""))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Campos devem ser todos preenchidos.");
                return;
            }

            horaIni = int.Parse(txtHrInicioTA.Text.Replace(":", ""));
            horaFim = int.Parse(txtHrTerminoTA.Text.Replace(":", ""));

            if (horaIni > horaFim)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Horário de Término deve ser maior que Horário de Início");
                return;
            }

            TB131_TEMPO_AULA tb131 = (from lTb131 in TB131_TEMPO_AULA.RetornaTodosRegistros()
                                      where lTb131.CO_CUR == serie && lTb131.CO_EMP == LoginAuxili.CO_EMP && lTb131.CO_MODU_CUR == modalidade
                                      && lTb131.NR_TEMPO == tempoAula && lTb131.TP_TURNO == ddlTurnoTA.SelectedValue
                                      select lTb131).FirstOrDefault();            

            if (tb131 == null)
            {
                tb131 = new TB131_TEMPO_AULA();

                tb131.CO_EMP = LoginAuxili.CO_EMP;
                tb131.CO_MODU_CUR = modalidade;
                tb131.CO_CUR = serie;
                tb131.NR_TEMPO = tempoAula;
                tb131.TP_TURNO = ddlTurnoTA.SelectedValue;
            }

            tb131.HR_INICIO = txtHrInicioTA.Text;
            tb131.HR_TERMI = txtHrTerminoTA.Text;

            CurrentPadraoCadastros.CurrentEntity = tb131;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {                                               
            TB131_TEMPO_AULA tb131 = RetornaEntidade();

            if (tb131 != null)
            {
                ddlModalidade.SelectedValue = tb131.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb131.CO_CUR.ToString();
                ddlTempoAula.SelectedValue = tb131.NR_TEMPO.ToString();
                ddlTurnoTA.SelectedValue = tb131.TP_TURNO;
                txtHrInicioTA.Text = tb131.HR_INICIO;
                txtHrTerminoTA.Text = tb131.HR_TERMI;

                ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTempoAula.Enabled = ddlTurnoTA.Enabled = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB131_TEMPO_AULA</returns>
        private TB131_TEMPO_AULA RetornaEntidade()
        {
            int modalidade, serie, nrTempo;
            string tpTurno;

            modalidade = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoModuCur);
            serie = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCur);
            nrTempo = QueryStringAuxili.RetornaQueryStringComoIntPelaChave("nTempo");
            tpTurno = QueryStringAuxili.RetornaQueryStringPelaChave("tpTurno");

            return TB131_TEMPO_AULA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie, nrTempo, tpTurno);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de modalidade
        /// </summary>
        private void CarregaModalidade()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecine", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de série
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ? DateTime.Now.Year.ToString() : "";

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where anoGrade != "" ? tb43.CO_ANO_GRADE == anoGrade : anoGrade == ""
                                            && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecine", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }        
    }
}