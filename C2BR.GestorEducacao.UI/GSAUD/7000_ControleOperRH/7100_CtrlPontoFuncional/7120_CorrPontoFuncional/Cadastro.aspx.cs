//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE FREQÜÊNCIA 
// OBJETIVO: CORREÇÃO DE FREQÜÊNCIA/PONTO DO COLABORADOR
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
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections.Specialized;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7120_CorrPontoFuncional
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaUnidadesFrequencia();
                ddlUnidadeFreq.SelectedValue = LoginAuxili.CO_EMP.ToString();
                CarregaColaboradores();
                CarregaAno();
            }
        }

        //====> Processo de Alteração e Inserção de Registros na Entidade do BD, após a ação de salvar
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro() { SalvaAtualizaGrid(); }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que monta a grid de Frequência
        /// </summary>
        private void MontaGridFrequencias()
        {
            int coEmp = ddlUnidadeFreq.SelectedValue != "" ? int.Parse(ddlUnidadeFreq.SelectedValue) : 0;
            int coCol = ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int mes = ddlMes.SelectedValue != "" ? int.Parse(ddlMes.SelectedValue) : 0;

            var resultado = (from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros().ToList()
                             where tb199.CO_EMP == coEmp && tb199.STATUS == "A" && tb199.CO_COL == coCol
                             && ((tb199.DT_FREQ.Year == ano || ano == 0) && (tb199.DT_FREQ.Month == mes || mes == 0))
                             select new
                             {
                                 DATA = tb199.DT_FREQ.ToString("dd/MM/yyyy"),
                                 TIPO = tb199.TP_FREQ == "E" ? "Entrada" : "Saída",
                                 HORA = tb199.HR_FREQ.ToString("00:00"),
                                 MOTIVO_CANCELAMENTO = tb199.DE_MOTIV_CANC_FREQ_FUNC,
                                 tb199.CO_EMP,
                                 tb199.CO_COL,
                                 tb199.CO_SEQ_FREQ
                             });

            grdFreqs.DataKeyNames = new string[] { "CO_EMP", "CO_COL", "DATA", "HORA", "CO_SEQ_FREQ" };

            grdFreqs.DataSource = (resultado.Count() > 0) ? resultado : null;
            grdFreqs.DataBind();
        }

        /// <summary>
        /// Método que Salva/Atualiza uma Frequência
        /// </summary>
        private void SalvaAtualizaGrid()
        {
            foreach (GridViewRow rows in grdFreqs.Rows)
            {
                TextBox txtMotivo = (TextBox)grdFreqs.Rows[rows.RowIndex].FindControl("txtMotivoCancelamento");
                TextBox txthora = (TextBox)grdFreqs.Rows[rows.RowIndex].FindControl("txtHoraFreq");
                int horaAtual = int.Parse(txthora.Text.Replace(":", ""));

                string strTipo;
                int coEmp, coCol, coSeqFreq, hora;
                DateTime data;

                coEmp = int.Parse(grdFreqs.DataKeys[rows.RowIndex].Values[0].ToString());
                coCol = int.Parse(grdFreqs.DataKeys[rows.RowIndex].Values[1].ToString());
                data = DateTime.Parse(grdFreqs.DataKeys[rows.RowIndex].Values[2].ToString());
                hora = int.Parse(grdFreqs.DataKeys[rows.RowIndex].Values[3].ToString().Replace(":", ""));
                coSeqFreq = int.Parse(grdFreqs.DataKeys[rows.RowIndex].Values[4].ToString());
                strTipo = rows.Cells.GetCellValue("Tipo").Equals("Entrada") ? "E" : "S";

                TB199_FREQ_FUNC frequencia = TB199_FREQ_FUNC.RetornaPelaChavePrimaria(coEmp, coCol, data, hora, coSeqFreq);

                if (frequencia.HR_FREQ != horaAtual)
                {
                    frequencia.DE_MOTIV_CANC_FREQ_FUNC = txtMotivo.Text.Trim();
                    frequencia.STATUS = "M";

                    TB199_FREQ_FUNC.SaveOrUpdate(frequencia, true);

                    AdicionaNovoRegistro(coCol, coEmp, coSeqFreq, strTipo, horaAtual, data);
                }
            }

            string strMensagem;
            strMensagem = " Alteração efetuada com sucesso!";
            AuxiliPagina.RedirecionaParaPaginaMensagem(strMensagem, this.AppRelativeVirtualPath, RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
        }

        //====> 
        /// <summary>
        /// Método que adiciona um novo registro de frequencia na TB199_FREQ_FUNC
        /// </summary>
        /// <param name="CO_COL">Id do funcionário</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_SEQ_FREQ">Id da frequência</param>
        /// <param name="TP_FREQ">Tipo de frequência</param>
        /// <param name="HR_FREQ">Hora da frequência</param>
        /// <param name="DT_FREQ">Data da frequência</param>
        public void AdicionaNovoRegistro(int CO_COL, int CO_EMP, int CO_SEQ_FREQ, string TP_FREQ, int HR_FREQ, DateTime DT_FREQ)
        {
            TB199_FREQ_FUNC tb199 = new TB199_FREQ_FUNC();

            tb199.CO_EMP = LoginAuxili.CO_UNID_FUNC;
            tb199.CO_COL = LoginAuxili.CO_COL;
            tb199.DT_FREQ = DT_FREQ;
            tb199.HR_FREQ = HR_FREQ;
            tb199.CO_SEQ_FREQ = CO_SEQ_FREQ;
            tb199.CO_EMP_ATIV = LoginAuxili.CO_EMP;
            tb199.TP_FREQ = TP_FREQ;
            var tb03 = TB03_COLABOR.RetornaPeloCoCol(CO_COL);
            tb199.TB03_COLABOR = tb03;
            tb03.TB25_EMPRESA1Reference.Load();
            tb199.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.TB25_EMPRESA1.CO_EMP);
            //tb199.TB03_COLABOR1 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
            tb199.TB03_COLABOR1 = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            tb199.FLA_PRESENCA = "S";
            tb199.DT_CADASTRO = DateTime.Now;
            tb199.STATUS = "A";

            if (tb199 != null)
            {
                if (tb199.EntityState == System.Data.EntityState.Added)
                    TB199_FREQ_FUNC.SaveOrUpdate(tb199, true);
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades de Frequencia
        /// </summary>
        private void CarregaUnidadesFrequencia()
        {
            ddlUnidadeFreq.Items.Clear();

            ddlUnidadeFreq.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                         join tb198 in TB198_USR_UNID_FREQ.RetornaTodosRegistros() on tb25.CO_EMP equals tb198.TB25_EMPRESA.CO_EMP
                                         select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidadeFreq.DataValueField = "CO_EMP";
            ddlUnidadeFreq.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeFreq.DataBind();

            ddlUnidadeFreq.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Colaboradores
        /// </summary>
        private void CarregaColaboradores()
        {
            int coEmp = ddlUnidadeFreq.SelectedValue != "" ? int.Parse(ddlUnidadeFreq.SelectedValue) : 0;

            ddlColaborador.DataSource = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                         join tb198 in TB198_USR_UNID_FREQ.RetornaTodosRegistros() on tb03.CO_COL equals tb198.CO_COL
                                         where tb198.TB25_EMPRESA.CO_EMP == coEmp
                                         select new { tb03.CO_COL, tb03.NO_COL }).Distinct().OrderBy(c => c.NO_COL);

            ddlColaborador.DataValueField = "CO_COL";
            ddlColaborador.DataTextField = "NO_COL";
            ddlColaborador.DataBind();

            ddlColaborador.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Ano
        /// </summary>
        private void CarregaAno()
        {
            int coEmp = ddlUnidadeFreq.SelectedValue != "" ? int.Parse(ddlUnidadeFreq.SelectedValue) : 0;

            ddlAno.DataSource = (from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                                 where tb199.CO_EMP == coEmp
                                 select new { ANO_FREQ = tb199.DT_FREQ.Year }).Distinct().OrderByDescending(f => f.ANO_FREQ);

            ddlAno.DataValueField = "ANO_FREQ";
            ddlAno.DataTextField = "ANO_FREQ";
            ddlAno.DataBind();
        }
        #endregion

        protected void grdFreqs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                TextBox txtMotivo = (TextBox)grdFreqs.Rows[e.RowIndex].FindControl("txtMotivoCancelamento");

                //--------> Faz a verificação para saber se foi informado o motivo do cancelamendo da frequência
                if (txtMotivo.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Informe um motivo para o cancelamento da frequência.");
                    txtMotivo.Focus();
                    return;
                }
                else
                {
                    int coEmp, coCol, coSeqFreq, hora;
                    DateTime data;

                    coEmp = int.Parse(grdFreqs.DataKeys[e.RowIndex].Values[0].ToString());
                    coCol = int.Parse(grdFreqs.DataKeys[e.RowIndex].Values[1].ToString());
                    data = DateTime.Parse(grdFreqs.DataKeys[e.RowIndex].Values[2].ToString());
                    hora = int.Parse(grdFreqs.DataKeys[e.RowIndex].Values[3].ToString().Replace(":", ""));
                    coSeqFreq = int.Parse(grdFreqs.DataKeys[e.RowIndex].Values[4].ToString());

                    TB199_FREQ_FUNC frequencia = TB199_FREQ_FUNC.RetornaPelaChavePrimaria(coEmp, coCol, data, hora, coSeqFreq);
                    frequencia.DE_MOTIV_CANC_FREQ_FUNC = txtMotivo.Text.Trim();
                    frequencia.STATUS = "C";

                    if (TB199_FREQ_FUNC.SaveOrUpdate(frequencia, true) > 0)
                        MontaGridFrequencias();
                }
            }
        }

        protected void ddlUnidadeFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaColaboradores();
            CarregaAno();
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            MontaGridFrequencias();
        }

    }
}