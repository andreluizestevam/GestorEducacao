//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: REGISTRO DE OCORRÊNCIAS DE SAÚDE (ATESTADOS MÉDICOS) DO COLABORADOR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1223_LancamentoFrequencia
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

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                string now = DateTime.Now.ToString("dd/MM/yyyy");

                CarregaUnidades();                
                ddlUnidade.Enabled = ddlColaborador.Enabled = true;
                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
                CarregaColaboradores();
                txtDataCadastro.Text = now;
            }
            else
            {
                txtDataFrequ.Enabled = false;
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            DateTime dtFreq = DateTime.Parse(txtDataFrequ.Text);

            if (dtFreq > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data da Freqüência não pode ser superior a data atual.");
                return;
            }

            if (ddlTipoFreq.SelectedValue != "F")
            {
                if ((txtHora.Text.Replace(":", "") == ""))
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Hora deve ser informada.");
                    return;
                }
                else
                {
                    if (!AuxiliValidacao.ValidaHora(txtHora.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Hora informada é inválida.");
                        return;
                    }
                }                
            }


            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                var ocorrFalta = (from iTb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                                  where iTb199.CO_COL == coCol
                                  && iTb199.DT_FREQ == dtFreq
                                  select iTb199).FirstOrDefault();

                if (ocorrFalta != null)
                {
                    if (ddlTipoFreq.SelectedValue == "F")
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Falta não pode ser cadastrada, pois existe registro para a data informada.");
                        return;
                    }
                    else
                    {
                        if (ocorrFalta.FLA_PRESENCA == "N")
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Presença não pode ser cadastrada, pois existe falta na data informada.");
                            return;
                        }
                    }
                    
                }
            }
            /*else
            {
                var ocorrFalta = (from iTb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                                  where iTb199.CO_COL == coCol && iTb199.FLA_PRESENCA == "N"
                                  && iTb199.DT_FREQ == dtFreq
                                  select iTb199).FirstOrDefault();

                if (ocorrFalta != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Já existe falta cadastrada para esse funcionário e essa data.");
                    return;
                }
            }*/

            TB199_FREQ_FUNC tb199 = RetornaEntidade();

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb199.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(coCol);
                tb199.DT_FREQ = dtFreq;
                tb199.HR_FREQ = (string.IsNullOrEmpty(txtHora.Text.Replace(":", ""))) ? 0 : int.Parse(txtHora.Text.Replace(":", ""));
                tb199.CO_SEQ_FREQ = this.GetCodSeq(coCol, dtFreq);
            }
            
            tb199.TB03_COLABOR1 = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);            
            tb199.CO_EMP_ATIV = LoginAuxili.CO_EMP;
            tb199.TP_FREQ = ddlTipoFreq.SelectedValue;
            tb199.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coUnid);
            tb199.FLA_PRESENCA = (ddlTipoFreq.SelectedValue == "F") ? "N" : "S";
            tb199.DE_JUSTI_FALTA = (string.IsNullOrEmpty(txtMotivo.Text)) ? null : txtMotivo.Text;
            tb199.FL_JUSTI_FALTA = (string.IsNullOrEmpty(txtMotivo.Text)) ? "N" : "S";
            tb199.DT_CADASTRO = DateTime.Now;
            tb199.STATUS = "A";

            CurrentPadraoCadastros.CurrentEntity = tb199;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB199_FREQ_FUNC tb199 = RetornaEntidade();

            if (tb199 != null)
            {
                tb199.TB03_COLABORReference.Load();
                var tb03 = tb199.TB03_COLABOR;
                tb03.TB25_EMPRESA1Reference.Load();
                ddlUnidade.Items.Insert(0, new ListItem(tb03.TB25_EMPRESA1.NO_FANTAS_EMP, tb03.TB25_EMPRESA1.CO_EMP.ToString()));
                CarregaColaboradores();
                ddlColaborador.SelectedValue = tb199.CO_COL.ToString();
                ddlTipoFreq.SelectedValue = tb199.TP_FREQ; 
                ddlTipoFreq.Enabled = txtHora.Enabled = tb199.FLA_PRESENCA == "S";
                txtDataFrequ.Text = tb199.DT_FREQ.ToString("dd/MM/yyyy");                
                txtHora.Text = tb199.HR_FREQ.ToString().PadLeft(4, '0').Insert(2, ":");
                txtMotivo.Text = tb199.DE_JUSTI_FALTA;
                txtMotivo.Enabled = tb199.FLA_PRESENCA == "N";
                txtDataCadastro.Text = tb199.DT_CADASTRO.ToString("dd/MM/yyyy");
                txtDataFrequ.Enabled = ddlColaborador.Enabled = txtHora.Enabled = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB199_FREQ_FUNC</returns>
        private TB199_FREQ_FUNC RetornaEntidade()
        {
            DateTime dataCalendario;
            dataCalendario = DateTime.TryParse(QueryStringAuxili.RetornaQueryStringPelaChave("data"), out dataCalendario) ? dataCalendario : DateTime.Now.AddDays(1);
            TB199_FREQ_FUNC tb199 = TB199_FREQ_FUNC.RetornaPelaChavePrimaria(
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("emp"),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("col"),
                dataCalendario,
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("hora"),
                QueryStringAuxili.RetornaQueryStringComoIntPelaChave("seq"));
            return (tb199 == null) ? new TB199_FREQ_FUNC() : tb199;
        }

        private int GetCodSeq(int codCol, DateTime dtFreq)
        {
            var resultado = (from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                             where tb199.TB03_COLABOR.CO_COL == codCol && tb199.DT_FREQ == dtFreq && tb199.CO_EMP_ATIV == LoginAuxili.CO_EMP
                             select tb199).OrderBy(f => f.CO_SEQ_FREQ).ToList();

            if (resultado.Count > 0)
                return resultado.Last().CO_SEQ_FREQ + 1;

            return 1;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaColaboradores()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                         select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(c => c.NO_COL);

            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
        }

        protected void ddlTipoFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFreq.SelectedValue == "")
                return;
            else if (ddlTipoFreq.SelectedValue == "F")
            {
                txtHora.Enabled = false;
                txtHora.Text = "";
                txtMotivo.Enabled = true;
            }
            else
            {
                txtMotivo.Enabled = false;
                txtMotivo.Text = null;
                txtHora.Enabled = true;
            }
        }
    }
}
