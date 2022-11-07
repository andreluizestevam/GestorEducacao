﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PGS - Portal Gestor Saúde
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: --
// SUBMÓDULO: --
// OBJETIVO: --
// DATA DE CRIAÇÃO: 17/08/2016
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
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Data.Sql;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9306_ControleValoresreferencia
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
                CarregaOperadoras();
                CarregaProcedimentos();
                CarregaGrupos();
                CarregaSubgrupos();
                CarregaItensAvaliacao();

                txtObserRefer.Attributes.Add("maxlength", "200");
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (String.IsNullOrEmpty(ddlItemAval.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page,"É necessário informar o item de avaliação.");
                ddlItemAval.Focus();
                return;
            }

            var tbs415 = TBS415_EXAME_ITENS_REFER.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("item"));

            if (tbs415 == null)
            {
                tbs415 = new TBS415_EXAME_ITENS_REFER();
            }

            tbs415.TBS414_EXAME_ITENS_AVALI = TBS414_EXAME_ITENS_AVALI.RetornaPelaChavePrimaria(int.Parse(ddlItemAval.SelectedValue));

            tbs415.FL_SEXO_REFER = ddlSexo.SelectedValue;
            tbs415.NO_ITENS_REFER = txtVlrRefer.Text;
            tbs415.VL_INICI_REFER = txtVlrReferInicial.Text;
            tbs415.VL_FINAL_REFER = txtVlrReferFinal.Text;
            tbs415.TP_UNIDA_REFER = txtTpUnidRefer.Text;
            tbs415.DE_OBSER_REFER = txtObserRefer.Text;
            tbs415.FL_SITUA_REFER = ddlStatus.SelectedValue;
            tbs415.NU_ORDEM = !String.IsNullOrEmpty(txtNumOrdem.Text) ? int.Parse(txtNumOrdem.Text) : (int?)null;

            CurrentPadraoCadastros.CurrentEntity = tbs415;
        }

        #endregion

        #region Carregamento

        private void CarregaFormulario()
        {
            var tbs415 = TBS415_EXAME_ITENS_REFER.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave("item"));

            if (tbs415 != null)
            {
                tbs415.TBS414_EXAME_ITENS_AVALIReference.Load();
                tbs415.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGRReference.Load();
                tbs415.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPOReference.Load();
                tbs415.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCEReference.Load();
                tbs415.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.TB250_OPERAReference.Load();

                CarregaOperadoras();
                ddlOperadora.SelectedValue = tbs415.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER.ToString();
                ddlOperadora.Enabled = false;

                CarregaProcedimentos();
                ddlProcedimento.SelectedValue = tbs415.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE.ToString();
                ddlProcedimento.Enabled = false;

                CarregaGrupos();
                ddlGrupo.SelectedValue = tbs415.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.ID_GRUPO.ToString();
                ddlGrupo.Enabled = false;

                CarregaSubgrupos();
                ddlSubgrupo.SelectedValue = tbs415.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.ID_SUBGRUPO.ToString();
                ddlSubgrupo.Enabled = false;

                CarregaItensAvaliacao();
                ddlItemAval.SelectedValue = tbs415.TBS414_EXAME_ITENS_AVALI.ID_ITENS_AVALI.ToString();
                ddlItemAval.Enabled = false;

                txtVlrRefer.Text = tbs415.NO_ITENS_REFER;
                ddlSexo.SelectedValue = tbs415.FL_SEXO_REFER.ToString();
                txtVlrReferInicial.Text = tbs415.VL_INICI_REFER;
                txtVlrReferFinal.Text = tbs415.VL_FINAL_REFER;
                txtTpUnidRefer.Text = tbs415.TP_UNIDA_REFER;
                txtObserRefer.Text = tbs415.DE_OBSER_REFER;
                ddlStatus.SelectedValue = tbs415.FL_SITUA_REFER;
                txtNumOrdem.Text = tbs415.NU_ORDEM.HasValue ? tbs415.NU_ORDEM.Value.ToString() : "";
            }
        }

        public void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, false);
        }

        public void CarregaProcedimentos()
        {
            var idOper = !String.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0;

            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlProcedimento, idOper, false);
        }

        private void CarregaGrupos()
        {
            var idProc = !String.IsNullOrEmpty(ddlProcedimento.SelectedValue) ? int.Parse(ddlProcedimento.SelectedValue) : 0;

            ddlGrupo.DataSource = (from tbs412 in TBS412_EXAME_GRUPO.RetornaTodosRegistros()
                                   where tbs412.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProc
                                   select new
                                   {
                                       tbs412.ID_GRUPO,
                                       tbs412.NO_GRUPO_EXAME
                                   }).OrderBy(t => t.NO_GRUPO_EXAME);

            ddlGrupo.DataTextField = "NO_GRUPO_EXAME";
            ddlGrupo.DataValueField = "ID_GRUPO";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaSubgrupos()
        {
            var idGrupo = !String.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubgrupo.DataSource = (from tbs413 in TBS413_EXAME_SUBGR.RetornaTodosRegistros()
                                      where tbs413.TBS412_EXAME_GRUPO.ID_GRUPO == idGrupo
                                      select new
                                      {
                                          tbs413.ID_SUBGRUPO,
                                          tbs413.NO_SUBGR_EXAME
                                      }).OrderBy(t => t.NO_SUBGR_EXAME);

            ddlSubgrupo.DataTextField = "NO_SUBGR_EXAME";
            ddlSubgrupo.DataValueField = "ID_SUBGRUPO";
            ddlSubgrupo.DataBind();

            ddlSubgrupo.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaItensAvaliacao()
        {
            var idSubgrupo = !String.IsNullOrEmpty(ddlSubgrupo.SelectedValue) ? int.Parse(ddlSubgrupo.SelectedValue) : 0;

            ddlItemAval.DataSource = (from tbs414 in TBS414_EXAME_ITENS_AVALI.RetornaTodosRegistros()
                                      where tbs414.TBS413_EXAME_SUBGR.ID_SUBGRUPO == idSubgrupo
                                      select new
                                      {
                                          tbs414.ID_ITENS_AVALI,
                                          tbs414.NO_ITEM_AVALI
                                      }).OrderBy(t => t.NO_ITEM_AVALI);

            ddlItemAval.DataTextField = "NO_ITEM_AVALI";
            ddlItemAval.DataValueField = "ID_ITENS_AVALI";
            ddlItemAval.DataBind();

            ddlItemAval.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Métodos Componentes

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos();
            CarregaGrupos();
            CarregaSubgrupos();
            CarregaItensAvaliacao();
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupos();
            CarregaSubgrupos();
            CarregaItensAvaliacao();
        }

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupos();
            CarregaItensAvaliacao();
        }

        protected void ddlSubgrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaItensAvaliacao();
        }

        #endregion
    }
}
