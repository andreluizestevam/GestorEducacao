//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: INFORMAÇÕES DE CALENDÁRIO ESCOLAR
// OBJETIVO: MANUTENÇÃO DE CALENDARIO ESCOLAR
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
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2104_InfoCalendarioEscolar.ManutencaoCalendarioEscolar
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

            CarregaTiposCalendario();
            CarregaTiposDia();
            CarregaUnidades();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");

                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
                txtDataAtividade.Text = hdData.Value = dataAtual;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int idTipoCalen = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            TB157_CALENDARIO_ATIVIDADES tb157 = RetornaEntidade();

            tb157.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
            tb157.TB152_CALENDARIO_TIPO = TB152_CALENDARIO_TIPO.RetornaPelaChavePrimaria(idTipoCalen);
            tb157.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            tb157.CAL_ANO_REFER_CALEND = DateTime.Parse(txtDataAtividade.Text).Year;
            tb157.CAL_TIPO_DIA_CALEND = ddlTipoDiaCalendario.SelectedValue;
            tb157.CAL_NOME_ATIVID_CALEND = txtDescricao.Text;
            tb157.CAL_OBSE_ATIVID_CALEND = txtObservacao.Text;
            tb157.CAL_DATA_CALEND = DateTime.Parse(hdData.Value);
            
            CurrentPadraoCadastros.CurrentEntity = tb157;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB157_CALENDARIO_ATIVIDADES tb157 = RetornaEntidade();

            if (tb157 != null)
            {
                tb157.TB152_CALENDARIO_TIPOReference.Load();
                tb157.TB25_EMPRESAReference.Load();

                ddlTipo.SelectedValue = tb157.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN.ToString();
                ddlUnidade.SelectedValue = tb157.TB25_EMPRESA.CO_EMP.ToString();
                txtDataAtividade.Text = tb157.CAL_DATA_CALEND.ToString("dd/MM/yyyy");
                ddlTipoDiaCalendario.SelectedValue = tb157.CAL_TIPO_DIA_CALEND;
                txtDescricao.Text = tb157.CAL_NOME_ATIVID_CALEND;
                txtObservacao.Text = tb157.CAL_OBSE_ATIVID_CALEND;
                hdData.Value = tb157.CAL_DATA_CALEND.ToString("dd/MM/yyyy");
                totalizadores(txtDataAtividade.Text.Substring(3, 2), txtDataAtividade.Text.Substring(6, 4));
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB157_CALENDARIO_ATIVIDADES</returns>
        private TB157_CALENDARIO_ATIVIDADES RetornaEntidade()
        {
            TB157_CALENDARIO_ATIVIDADES tb157 = TB157_CALENDARIO_ATIVIDADES.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb157 == null) ? new TB157_CALENDARIO_ATIVIDADES() : tb157;
        }        
        #endregion        

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Dia
        /// </summary>
        private void CarregaTiposDia()
        {
            ddlTipoDiaCalendario.Load<TipoDiaCalendario>();
            ddlTipoDiaCalendario.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Calendário
        /// </summary>
        private void CarregaTiposCalendario()
        {
            ddlTipo.DataSource = (from tb152 in TB152_CALENDARIO_TIPO.RetornaTodosRegistros()
                                  where tb152.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                  select new { tb152.CAT_ID_TIPO_CALEN, tb152.CAT_NOME_TIPO_CALEN });

            ddlTipo.DataTextField = "CAT_NOME_TIPO_CALEN";
            ddlTipo.DataValueField = "CAT_ID_TIPO_CALEN";
            ddlTipo.DataBind();

            ddlTipo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
        }
        #endregion

        protected void txtDataAtividade_TextChanged(object sender, EventArgs e)
        {
            hdData.Value = txtDataAtividade.Text;
        }

        void onchange()
        { 
            hdData.Value = txtDataAtividade.Text;
        }
        private void totalizadores(string mes, string ano)
        {
            ltrsomatorio.Text = "<b>Legenda Tipo de Dias</b> </br>";
            System.Data.DataTable dt = new System.Data.DataTable();
            BusinessEntities.Auxiliar.SQLDirectAcess acesso = new BusinessEntities.Auxiliar.SQLDirectAcess();
            string SQL = "select count(*), " +                         
                         " case CAL_TIPO_DIA_CALEND when 'C' then 'Conselho de Classe' " +
                         "                          when 'U' then 'Útil/Letivo' " +
                         "                          when 'N' then 'Não Útil/Letivo' " +
                         "                          when 'F' then 'Feriado' " +
                         "                          when 'F' then 'Recesso Escolar' end as CAL_TIPO_DIA_CALEND " +
                         " from TB157_CALENDARIO_ATIVIDADES " +
                         " where CAL_ANO_REFER_CALEND = '" + ano + "' " +
                         " and MONTH(CAL_DATA_CALEND) = '" + mes + "' " +
                         " group by CAL_TIPO_DIA_CALEND, CAL_TIPO_DIA_CALEND";

            dt = acesso.retornacolunas(SQL);            
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if(Convert.ToInt16(dt.Rows[i][0].ToString()) > 1)
                    ltrsomatorio.Text = ltrsomatorio.Text + dt.Rows[i][1].ToString() + " - " + dt.Rows[i][0].ToString() + "  Dias </br>";
                else
                    ltrsomatorio.Text = ltrsomatorio.Text + dt.Rows[i][1].ToString() + " - " + dt.Rows[i][0].ToString() + "  Dia </br>";

            }
        }
    }
}
