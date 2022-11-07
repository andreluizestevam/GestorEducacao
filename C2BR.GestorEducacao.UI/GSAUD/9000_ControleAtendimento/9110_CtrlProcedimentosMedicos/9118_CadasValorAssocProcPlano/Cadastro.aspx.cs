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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9118_CadasValorAssocProcPlano
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
                    txtDtValores.Enabled = txtVlBase.Enabled = ddlRefer.Enabled = ddlSitu.Enabled = true;
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TBS362_ASSOC_PLANO_PROCE tbs362 = RetornaEntidade();
            tbs362.TBS356_PROC_MEDIC_PROCEReference.Load();

            //Insere o novo registro apenas se o checkbox correspondente estiver marcado
            if (chkIncluNovoValor.Checked)
            {
                #region Validações
                if (string.IsNullOrEmpty(txtDtValores.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Você marcou inserir novo registro, porém não informou a data da condição. Favor informar.");
                    txtDtValores.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ddlRefer.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Tipo de Referência deve ser informado");
                    ddlRefer.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtVlBase.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Você marcou inserir novo registro, porém não informou o Valor Base. Favor informar.");
                    txtVlBase.Focus();
                    return;
                }

                if (ddlSitu.SelectedValue == "A")
                    InativaValoresAntigos(); // Inativa os outros valores anteriores

                DateTime dtValores = DateTime.Parse(txtDtValores.Text);
                //Cria datetime apartir do campo, e insere hora minutos e milisegundos do momento atual na data;
                dtValores = dtValores.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddMilliseconds(DateTime.Now.Millisecond);

                #endregion

                TBS361_CONDI_PLANO_SAUDE tbs361 = new TBS361_CONDI_PLANO_SAUDE();

                tbs361.CO_REFER_TIPO = ddlRefer.SelectedValue;
                tbs361.VL_CONTE_REFER = decimal.Parse(txtVlBase.Text);
                tbs361.CO_COL_CONDI = LoginAuxili.CO_COL;
                tbs361.CO_EMP_CONDI = LoginAuxili.CO_EMP;
                tbs361.DT_CONDI = DateTime.Now;
                tbs361.IP_CONDI = Request.UserHostAddress;
                tbs361.CO_COL_STATU = LoginAuxili.CO_COL;
                tbs361.CO_EMP_STATU = LoginAuxili.CO_EMP;
                tbs361.DT_STATU = DateTime.Now;
                tbs361.FL_STATU = ddlSitu.SelectedValue;
                tbs361.IP_STATU = Request.UserHostAddress;
                tbs361.TBS362_ASSOC_PLANO_PROCE = tbs362;

                TBS361_CONDI_PLANO_SAUDE.SaveOrUpdate(tbs361, true);

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
            TBS362_ASSOC_PLANO_PROCE tbs362 = RetornaEntidade();
            tbs362.TBS356_PROC_MEDIC_PROCEReference.Load();
            txtNoProcedimento.Text = tbs362.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI;
            txtCodProcMedic.Text = tbs362.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI;
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS339_CAMPSAUDE</returns>
        private TBS362_ASSOC_PLANO_PROCE RetornaEntidade()
        {
            TBS362_ASSOC_PLANO_PROCE tbs362 = TBS362_ASSOC_PLANO_PROCE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs362 == null) ? new TBS362_ASSOC_PLANO_PROCE() : tbs362;
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
                    TBS361_CONDI_PLANO_SAUDE tb = TBS361_CONDI_PLANO_SAUDE.RetornaPelaChavePrimaria(id);
                    tb.FL_STATU = "I";
                    tb.CO_COL_STATU = LoginAuxili.CO_COL;
                    tb.CO_EMP_STATU = LoginAuxili.CO_EMP;
                    tb.DT_STATU = DateTime.Now;
                    tb.IP_STATU = Request.UserHostAddress;
                    TBS361_CONDI_PLANO_SAUDE.SaveOrUpdate(tb, true);

                    break; // Interrompe o foreach pois existirá sempre apenas um registro ativo
                }
            }
        }

        /// <summary>
        /// Método responsável por carregar os valores referentes ao procedimento médico em questão
        /// </summary>
        private void CarregaValores()
        {
            int ID_ASSOC_PLANO_PROCE = RetornaEntidade().ID_ASSOC_PLANO_PROCE;

            var res = (from tbs361 in TBS361_CONDI_PLANO_SAUDE.RetornaTodosRegistros()
                       where tbs361.TBS362_ASSOC_PLANO_PROCE.ID_ASSOC_PLANO_PROCE == ID_ASSOC_PLANO_PROCE
                       select new Saida
                       {
                           VL_CONDI = tbs361.VL_CONTE_REFER,
                           CO_STATUS = tbs361.FL_STATU,
                           DT = tbs361.DT_CONDI,
                           ID_CONDI = tbs361.ID_CONDI_PLANO_SAUDE,
                           REFER = tbs361.CO_REFER_TIPO,
                       }).OrderByDescending(w => w.DT).ToList();

            grdValores.DataSource = res;
            grdValores.DataBind();
        }

        public class Saida
        {
            public DateTime DT { get; set; }
            public string DT_LANC_V
            {
                get
                {
                    return this.DT.ToString("dd/MM/yyyy") + " " + this.DT.ToString("HH:mm");
                }
            }
            public int ID_CONDI { get; set; }
            public string REFER { get; set; }
            public string DE_REFER
            {
                get
                {
                    switch (this.REFER.ToUpper())
                    {
                        case "P":
                            return "Porcentagem";
                        case "V":
                            return "Valor";
                        default:
                            return " - ";
                    }
                }
            }
            public decimal VL_CONDI { get; set; }
            public string VL_CONDI_V
            {
                get
                {
                    //Trata para mostrar o conteúdo de referência de acordo com o tipo de referência (35% ou R$35,00)
                    return (this.REFER.ToUpper() == "P" ? this.VL_CONDI + "%" : "R$ " + this.VL_CONDI.ToString("N2"));
                }
            }
            public string CO_STATUS { get; set; }
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