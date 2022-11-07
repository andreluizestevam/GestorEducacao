//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 15/07/16 | Filipe Rodrigues           | Criação da funcionalidade para Cadastro de comissionamentos


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
using System.Data;
using Resources;

namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5105_Comissao
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
                CarregarProfissionais();
                CarregaGrupos();
                CarregaSubGrupos();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
            {
                CurrentPadraoCadastros.CurrentEntity = RetornaEntidade();
                return;
            }

            if (Page.IsValid)
            {
                foreach (GridViewRow li in grdComissoes.Rows)
                {
                    var chk = (CheckBox)li.FindControl("chkSelectProc");
                    var exist = bool.Parse(((HiddenField)li.FindControl("hidExistente")).Value);

                    if (chk.Checked || exist)
                    {
                        string idCom = ((HiddenField)li.FindControl("hidIdComiss")).Value;

                        TBS410_COMISSAO tbs410 = new TBS410_COMISSAO();

                        if (!String.IsNullOrEmpty(idCom) && idCom != "0")
                            tbs410 = TBS410_COMISSAO.RetornaPelaChavePrimaria(int.Parse(idCom));

                        if (chk.Checked)
                        {
                            var pcAvaliacao = (CheckBox)li.FindControl("chkPcAvaliacao");
                            var vlAvaliacao = (TextBox)li.FindControl("txtVlAvaliacao");
                            var pcCobranca = (CheckBox)li.FindControl("chkPcCobranca");
                            var vlCobranca = (TextBox)li.FindControl("txtVlCobranca");
                            var pcContrato = (CheckBox)li.FindControl("chkPcContrato");
                            var vlContrato = (TextBox)li.FindControl("txtVlContrato");
                            var pcIndPac = (CheckBox)li.FindControl("chkPcIndPac");
                            var vlIndPac = (TextBox)li.FindControl("txtVlIndPac");
                            var pcIndProc = (CheckBox)li.FindControl("chkPcIndProc");
                            var vlIndProc = (TextBox)li.FindControl("txtVlIndProc");
                            var pcPlanejamento = (CheckBox)li.FindControl("chkPcPlanejamento");
                            var vlPlanejamento = (TextBox)li.FindControl("txtVlPlanejamento");

                            if (tbs410.ID_COMISS == 0)
                            {
                                string idProc = ((HiddenField)li.FindControl("hidIdProced")).Value;

                                tbs410.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlProfissional.SelectedValue));
                                tbs410.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                                tbs410.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(idProc));

                                //Dados de cadastro
                                tbs410.DT_CADAS = DateTime.Now;
                                tbs410.CO_COL_CADAS = LoginAuxili.CO_COL;
                                tbs410.CO_EMP_COL_CADAS = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                                tbs410.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                                tbs410.IP_CADAS = Request.UserHostAddress;
                            }

                            tbs410.PC_AVALIA = pcAvaliacao.Checked ? "S" : "N";
                            tbs410.VL_AVALIA = !String.IsNullOrEmpty(vlAvaliacao.Text) ? decimal.Parse(vlAvaliacao.Text) : (decimal?)null;
                            tbs410.PC_COBRAN = pcCobranca.Checked ? "S" : "N";
                            tbs410.VL_COBRAN = !String.IsNullOrEmpty(vlCobranca.Text) ? decimal.Parse(vlCobranca.Text) : (decimal?)null;
                            tbs410.PC_CONTRT = pcContrato.Checked ? "S" : "N";
                            tbs410.VL_CONTRT = !String.IsNullOrEmpty(vlContrato.Text) ? decimal.Parse(vlContrato.Text) : (decimal?)null;
                            tbs410.PC_INDC_PAC = pcIndPac.Checked ? "S" : "N";
                            tbs410.VL_INDC_PAC = !String.IsNullOrEmpty(vlIndPac.Text) ? decimal.Parse(vlIndPac.Text) : (decimal?)null;
                            tbs410.PC_INDC_PROC = pcIndProc.Checked ? "S" : "N";
                            tbs410.VL_INDC_PROC = !String.IsNullOrEmpty(vlIndProc.Text) ? decimal.Parse(vlIndProc.Text) : (decimal?)null;
                            tbs410.PC_PLANEJ = pcPlanejamento.Checked ? "S" : "N";
                            tbs410.VL_PLANEJ = !String.IsNullOrEmpty(vlPlanejamento.Text) ? decimal.Parse(vlPlanejamento.Text) : (decimal?)null;
                        }

                        tbs410.CO_SITUA = chk.Checked ? "A" : "I";
                        tbs410.DT_SITUA = DateTime.Now;
                        tbs410.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs410.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                        tbs410.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs410.IP_SITUA = Request.UserHostAddress;

                        TBS410_COMISSAO.SaveOrUpdate(tbs410, true);
                    }
                }

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Dados Salvos com Sucesso!");
            }
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TBS410_COMISSAO tbs410 = RetornaEntidade();

            if (tbs410 != null)
            {
                /*drpPaciente.SelectedValue = tbs410.CO_ALU.ToString();
                ddlTipo.SelectedValue = tbs410.TP_OCORR;
                txtTitulo.Text = tbs410.NO_OCORR;
                txtOcorrencia.Text = tbs410.DE_OCORR;
                txtAcao.Text = tbs410.DE_ACAO_OCORR;
                ddlSituacao.SelectedValue = tbs410.CO_SITU;
                //Desabilitando os que não podem ser alterados
                drpPaciente.Enabled = false;
                imgbVoltarPesq.Visible = false;*/
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns></returns>
        private TBS410_COMISSAO RetornaEntidade()
        {
            return TBS410_COMISSAO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        private void CarregarProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, LoginAuxili.CO_EMP, false, "0", true, 0, false);
        }

        /// <summary>
        /// Carrega os Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupo, true);
        }

        /// <summary>
        /// Carrega so SubGrupos
        /// </summary>
        private void CarregaSubGrupos()
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, coGrupo, true);
        }

        public void CarregarComissoes()
        {
            int profissional = int.Parse(ddlProfissional.SelectedValue);
            int grup = int.Parse(ddlGrupo.SelectedValue);
            int sgrup = int.Parse(ddlSubGrupo.SelectedValue);
            var situa = ddlSituacao.SelectedValue;

            var procs = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                         where (grup != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grup : 0 == 0)
                         && (sgrup != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == sgrup : 0 == 0)
                         && (situa == "0" || situa == "S" ? true : false)
                         select new listaComissoes
                         {
                             GRUPO = tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                             SUBGRUPO = tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                             PROCED = tbs356.NM_PROC_MEDI,
                             CO_PROCED = tbs356.CO_PROC_MEDI,
                             ID_PROCED = tbs356.ID_PROC_MEDI_PROCE,
                             STATUS = "Sem Uso",
                             Existente = false
                         }).ToList();

            var coms = (from tbs410 in TBS410_COMISSAO.RetornaTodosRegistros()
                        where tbs410.TB03_COLABOR.CO_COL == profissional
                        && (grup != 0 ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grup : 0 == 0)
                        && (sgrup != 0 ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == sgrup : 0 == 0)
                        && (situa != "0" && situa != "S" ? tbs410.CO_SITUA == situa : (situa == "0" ? true : false))
                        select new listaComissoes
                        {
                            ID_COMISS = tbs410.ID_COMISS,
                            GRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                            SUBGRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                            PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                            CO_PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI,
                            ID_PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,

                            PC_AVALIACAO = tbs410.PC_AVALIA == "S" ? true : false,
                            VL_AVALIACAO = tbs410.VL_AVALIA,
                            PC_COBRANCA = tbs410.PC_COBRAN == "S" ? true : false,
                            VL_COBRANCA = tbs410.VL_COBRAN,
                            PC_CONTRATO = tbs410.PC_CONTRT == "S" ? true : false,
                            VL_CONTRATO = tbs410.VL_CONTRT,
                            PC_IND_PACIENTE = tbs410.PC_INDC_PAC == "S" ? true : false,
                            VL_IND_PACIENTE = tbs410.VL_INDC_PAC,
                            PC_IND_PROCEDIMENTO = tbs410.PC_INDC_PROC == "S" ? true : false,
                            VL_IND_PROCEDIMENTO = tbs410.VL_INDC_PROC,
                            PC_PLANEJAMENTO = tbs410.PC_PLANEJ == "S" ? true : false,
                            VL_PLANEJAMENTO = tbs410.VL_PLANEJ,

                            STATUS = tbs410.CO_SITUA == "A" ? "Ativo" : "Inativo",
                            Existente = tbs410.CO_SITUA == "A"
                        }).ToList();

            var res = coms;

            foreach (var i in procs)
                if (res.FirstOrDefault(c => c.ID_PROCED == i.ID_PROCED) == null)
                    res.Add(i);

            grdComissoes.DataSource = res.OrderBy(c => c.PROCED);
            grdComissoes.DataBind();
        }

        private class listaComissoes
        {
            public int ID_COMISS { get; set; }
            public string GRUPO { get; set; }
            public string SUBGRUPO { get; set; }
            public string PROCED { get; set; }
            public string CO_PROCED { get; set; }
            public int ID_PROCED { get; set; }
            public string PROCEDIMENTO
            {
                get
                {
                    return CO_PROCED + " - " + PROCED;
                }
            }

            public string STATUS { get; set; }
            public bool Existente { get; set; }

            public bool PC_AVALIACAO { get; set; }
            public decimal? VL_AVALIACAO { get; set; }
            public bool PC_COBRANCA { get; set; }
            public decimal? VL_COBRANCA { get; set; }
            public bool PC_CONTRATO { get; set; }
            public decimal? VL_CONTRATO { get; set; }
            public bool PC_IND_PACIENTE { get; set; }
            public decimal? VL_IND_PACIENTE { get; set; }
            public bool PC_IND_PROCEDIMENTO { get; set; }
            public decimal? VL_IND_PROCEDIMENTO { get; set; }
            public bool PC_PLANEJAMENTO { get; set; }
            public decimal? VL_PLANEJAMENTO { get; set; }
        }

        #endregion

        #region Funções de Campo

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupos();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarComissoes();
        }

        protected void chkTodosProcs_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;

            foreach (GridViewRow li in grdComissoes.Rows)
            {
                var chk = (CheckBox)li.FindControl("chkSelectProc");

                chk.Checked = atual.Checked;
            }
        }

        #endregion
    }
}