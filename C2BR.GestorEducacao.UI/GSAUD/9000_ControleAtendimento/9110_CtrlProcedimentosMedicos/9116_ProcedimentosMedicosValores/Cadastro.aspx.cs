// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: GERA GRADE ANUAL DE DISCIPLINA (MATÉRIA) DE SÉRIE/CURSO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+------------------------------------
// 27/10/2014| Maxwell Almeida            |  Criação da funcionalidade para alteração nos valores dos procedimentos
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9116_ProcedimentosMedicosValores
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
            if (!Page.IsPostBack)
            {
                //Se for operação de inserção 
                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    AuxiliPagina.RedirecionaParaPaginaErro("É preciso selecionar o Procedimento Médico!!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

                CarregaValores();
                txtDtValores.Text = DateTime.Now.ToString();
            }
            else
            {
                if (chkIncluNovoValor.Checked)
                    txtDtValores.Enabled = txtVlBase.Enabled = txtVlCusto.Enabled = txtVlRestitu.Enabled = true;
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TBS356_PROC_MEDIC_PROCE tbs356 = RetornaEntidade();

            //Insere o novo registro apenas se o checkbox correspondente estiver marcado
            if (chkIncluNovoValor.Checked)
            {
                #region Validações
                if (string.IsNullOrEmpty(txtDtValores.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Você marcou inserir novo registro, porém não informou a data do valor. Valor informar.");
                    txtDtValores.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtVlBase.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Você marcou inserir novo registro, porém não informou o Valor Base. Valor informar.");
                    txtVlBase.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtVlCusto.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Você marcou inserir novo registro, porém não informou o Valor de Custo. Valor informar.");
                    txtVlCusto.Focus();
                    return;
                }

                if(ddlSitu.SelectedValue == "A")
                    InativaValoresAntigos(); // Inativa os outros valores anteriores

                DateTime dtValores = DateTime.Parse(txtDtValores.Text);
                //Cria datetime apartir do campo, e insere hora minutos e milisegundos do momento atual na data;
                dtValores = dtValores.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddMilliseconds(DateTime.Now.Millisecond);

                #endregion
                TBS353_VALOR_PROC_MEDIC_PROCE TBS353 = new TBS353_VALOR_PROC_MEDIC_PROCE();
                TBS353.TBS356_PROC_MEDIC_PROCE = tbs356;
                TBS353.VL_CUSTO = (!string.IsNullOrEmpty(txtVlCusto.Text) ? decimal.Parse(txtVlCusto.Text) : 0);
                TBS353.VL_BASE = (!string.IsNullOrEmpty(txtVlBase.Text) ? decimal.Parse(txtVlBase.Text) : 0);
                TBS353.VL_RESTI = (!string.IsNullOrEmpty(txtVlRestitu.Text) ? decimal.Parse(txtVlRestitu.Text) : 0);
                TBS353.CO_COL_LANC = LoginAuxili.CO_COL;
                TBS353.CO_EMP_LANC = LoginAuxili.CO_EMP;
                TBS353.IP_LANC = Request.UserHostAddress;
                TBS353.DT_LANC = DateTime.Now;
                TBS353.DT_VALOR = dtValores;
                TBS353.FL_STATU = ddlSitu.SelectedValue;

                TBS353.DT_STATU = DateTime.Now;
                TBS353.CO_COL_STATU = LoginAuxili.CO_COL;
                TBS353.CO_EMP_LANC = LoginAuxili.CO_EMP;
                TBS353.IP_STATU = Request.UserHostAddress;

                //CurrentPadraoCadastros.CurrentEntity = tbs356;
                TBS353_VALOR_PROC_MEDIC_PROCE.SaveOrUpdate(TBS353, true);

                AuxiliPagina.RedirecionaParaPaginaSucesso("Alterações salvas com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações
        /// </summary>
        private void CarregaFormulario()
        {
            TBS356_PROC_MEDIC_PROCE tbs356 = RetornaEntidade();
            txtNoProcedimento.Text = tbs356.NM_PROC_MEDI;
            txtCodProcMedic.Text = tbs356.CO_PROC_MEDI;
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS339_CAMPSAUDE</returns>
        private TBS356_PROC_MEDIC_PROCE RetornaEntidade()
        {
            TBS356_PROC_MEDIC_PROCE tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs356 == null) ? new TBS356_PROC_MEDIC_PROCE() : tbs356;
        }

        /// <summary>
        /// Método responsável por inativar os registros de valores antigos 
        /// </summary>
        private void InativaValoresAntigos()
        {
            foreach (GridViewRow li in grdValores.Rows)
            {
                //Só executa esse bloco caso o registro esteja com status de ativo
                if (((HiddenField)li.Cells[0].FindControl("hidCoStatus")).Value == "A")
                {
                    //Instancia o objeto
                    int id = int.Parse(((HiddenField)li.Cells[0].FindControl("hidIdValor")).Value);

                    //Altera o status para inativo e persiste
                    TBS353_VALOR_PROC_MEDIC_PROCE tb = TBS353_VALOR_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(id);
                    tb.FL_STATU = "I";
                    tb.DT_STATU = DateTime.Now;
                    tb.CO_COL_STATU = LoginAuxili.CO_COL;
                    tb.CO_EMP_STATU = LoginAuxili.CO_EMP;
                    tb.IP_STATU = Request.UserHostAddress;
                    TBS353_VALOR_PROC_MEDIC_PROCE.SaveOrUpdate(tb, true);

                    break; // Interrompe o foreach pois existirá sempre apenas um registro ativo
                }
            }
        }

        /// <summary>
        /// Método responsável por carregar os valores referentes ao procedimento médico em questão
        /// </summary>
        private void CarregaValores()
        {
            int ID_PROC = RetornaEntidade().ID_PROC_MEDI_PROCE;

            var res = (from tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == ID_PROC
                       select new Saida
                       {
                           VL_CUSTO = tbs353.VL_CUSTO,
                           VL_BASE = tbs353.VL_BASE,
                           VL_RESTI = tbs353.VL_RESTI,
                           CO_STATUS = tbs353.FL_STATU,
                           DT = tbs353.DT_VALOR ?? tbs353.DT_LANC,
                           ID_VALOR = tbs353.ID_VALOR_PROC_MEDIC_PROCE,
                       }).OrderByDescending(w => w.DT).ToList();

            grdValores.DataSource = res;
            grdValores.DataBind();
        }

        public class Saida
        {
            public int ID_VALOR { get; set; }
            public decimal? VL_CUSTO { get; set; }
            public string VL_CUSTO_V
            {
                get
                {
                    return (this.VL_CUSTO.HasValue ? this.VL_CUSTO.Value.ToString("N2") : "");
                }
            }
            public decimal? VL_BASE { get; set; }
            public string VL_BASE_V
            {
                get
                {
                    return (this.VL_BASE.HasValue ? this.VL_BASE.Value.ToString("N2") : "");
                }
            }
            public decimal? VL_RESTI { get; set; }
            public string VL_RESTI_V
            {
                get
                {
                    return (this.VL_RESTI.HasValue ? this.VL_RESTI.Value.ToString("N2") : "");
                }
            }
            public string CO_STATUS { get; set; }

            public DateTime DT { get; set; }
            public string DT_LANC_V
            {
                get
                {
                    return this.DT.ToString("dd/MM/yyyy") + " " + this.DT.ToString("HH:mm");
                }
            }
            public bool SW_USO
            {
                get
                {
                    return (this.CO_STATUS == "A" ? true : false);
                }
            }
            public bool SW_INATIVO
            {
                get
                {
                    return (this.CO_STATUS != "A" ? true : false);
                }
            }
        }
        #endregion

        #region Funções de Campo

        protected void grdValores_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                //Trata para que, quando o Procedimento estiver inativo, a linha seja destacada em vermelho
                if (((HiddenField)e.Row.Cells[0].FindControl("hidCoStatus")).Value == "A")
                    e.Row.BackColor = System.Drawing.Color.DarkSeaGreen;
                else
                    e.Row.BackColor = System.Drawing.Color.LightSalmon;
            }
        }

        #endregion
    }
}