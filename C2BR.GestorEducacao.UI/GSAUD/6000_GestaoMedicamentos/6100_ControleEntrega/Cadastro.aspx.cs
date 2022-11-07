//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: Registro de Atendimento do Usuário
// DATA DE CRIAÇÃO: 08/03/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._6000_GestaoMedicamentos._6100_ControleEntrega;

namespace C2BR.GestorEducacao.UI.GSAUD._6000_GestaoMedicamentos._6100_ControleEntrega
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["URLRelatorio"] != null)
            {
                AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                //----------------> Limpa a var de sessão com o url do relatório.
                Session.Remove("URLRelatorio");
                //----------------> Limpa a ref da url utilizada para carregar o relatório.
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                isreadonly.SetValue(this.Request.QueryString, true, null);
            }

            if (!IsPostBack)
            {
                CarregaResponsavel();
                CarregaUsuario();
            }
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Método que carrega as informações de solicitações na grid grdSoli
        /// </summary>
        protected void CarregaGridSolicitacoes()
        {
            int coResp = ddlResponsavel.SelectedValue != "" ? int.Parse(ddlResponsavel.SelectedValue) : 0;
            int coAlu = ddlUsuario.SelectedValue != "" ? int.Parse(ddlUsuario.SelectedValue) : 0;

            if (coResp != 0 && coAlu != 0)
            {
                var res = (from tb092 in TB092_RESER_MEDIC.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb092.CO_EMP equals tb25.CO_EMP
                           where tb092.CO_RESP == coResp
                            && tb092.CO_ALU == coAlu
                           select new GridSolicitacoes
                           {
                               idReser = tb092.ID_RESER_MEDIC,
                               coReser = tb092.CO_RESER_MEDIC,
                               dtReser = tb092.DT_RESER_MEDIC,
                               siglaEmp = tb25.sigla,
                               noInstSoli = tb092.NO_INST_SOLI,
                               noMediSoli = tb092.NO_MEDI_SOLI,
                               crmMediSoli = tb092.NU_MEDI_SOLI_CRM,
                               ufMediSoli = tb092.CO_MEDI_SOLI_UF
                           });

                grdSoli.DataSource = res;
                grdSoli.DataBind();
            }
        }

        /// <summary>
        /// Classe que formata a saída da consulta das solicitações
        /// </summary>
        public class GridSolicitacoes
        {
            public int idReser { get; set; }
            public string noInstSoli { get; set; }
            public string noMediSoli { get; set; }
            public string crmMediSoli { get; set; }
            public string ufMediSoli { get; set; }
            public string crm_uf
            {
                get
                {
                    return this.crmMediSoli + "/" + this.ufMediSoli;
                }
            }
            public string siglaEmp { get; set; }
            public string coReser { get; set; }
            public string coReserMedic
            {
                get
                {
                    return Convert.ToInt64(this.coReser).ToString(@"0000\.000\.0000000");
                }
            }
            public DateTime dtPrev { get; set; }
            public string dtPrevMedic
            {
                get
                {
                    return this.dtPrev.ToString("dd/MM/yyyy");
                }
            }
            public DateTime dtReser { get; set; }
            public string dtReserMedic
            {
                get
                {
                    return this.dtReser.ToString("dd/MM/yyyy");
                }
            }
        }

        /// <summary>
        /// Método que carrega a grid de items da solicitação
        /// </summary>
        /// <param name="idReser">ID da solicitação</param>
        protected void CarregaItemsSolicitacao(int idReser = 0)
        {
            if (idReser != 0)
            {
                var res = (from tb094 in TB094_ITEM_RESER_MEDIC.RetornaTodosRegistros()
                           join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tb094.TB90_PRODUTO.CO_PROD equals tb90.CO_PROD
                           where tb094.TB092_RESER_MEDIC.ID_RESER_MEDIC == idReser
                           select new GridItemsSolicitacao
                           {
                               idItemReser = tb094.ID_ITEM_RESER_MEDIC,
                               coEmpItem = tb094.CO_EMP,
                               noProd = tb094.TB90_PRODUTO.NO_PROD,
                               qtMes1 = tb094.QT_MES1,
                               qtMes2 = tb094.QT_MES2,
                               qtMes3 = tb094.QT_MES3,
                               qtMes4 = tb094.QT_MES4,
                               qtEntrega1 = tb094.QT_ENTR_MES1,
                               qtEntrega2 = tb094.QT_ENTR_MES2,
                               qtEntrega3 = tb094.QT_ENTR_MES3,
                               qtEntrega4 = tb094.QT_ENTR_MES4
                           });

                grdItemSoli.DataSource = res;
                grdItemSoli.DataBind();
            }
        }

        public class GridItemsSolicitacao
        {
            public int idItemReser { get; set; }
            public int coEmpItem { get; set; }
            public string noProd { get; set; }
            public decimal qtMes1 { get; set; }
            public decimal qtMes2 { get; set; }
            public decimal qtMes3 { get; set; }
            public decimal qtMes4 { get; set; }
            public decimal qtTotal
            {
                get
                {
                    decimal t = 0;

                    t = this.qtMes1 + this.qtMes2 + this.qtMes3 + this.qtMes4;

                    return t;
                }
            }
            public decimal qtEntrega1 { get; set; }
            public decimal qtEntrega2 { get; set; }
            public decimal qtEntrega3 { get; set; }
            public decimal qtEntrega4 { get; set; }
            public decimal qtSaldo1
            {
                get
                {
                    return this.qtMes1 - this.qtEntrega1;
                }
            }
            public decimal qtSaldo2
            {
                get
                {
                    return this.qtMes2 - this.qtEntrega2;
                }
            }
            public decimal qtSaldo3
            {
                get
                {
                    return this.qtMes3 - this.qtEntrega3;
                }
            }
            public decimal qtSaldo4
            {
                get
                {
                    return this.qtMes4 - this.qtEntrega4;
                }
            }
        }

        /// <summary>
        /// Método que carrega os responsáveis da combo ddlResponsavel
        /// </summary>
        protected void CarregaResponsavel()
        {
            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                       select new
                       {
                           tb108.NO_RESP,
                           tb108.CO_RESP
                       }).OrderBy(o => o.NO_RESP);

            ddlResponsavel.DataTextField = "NO_RESP";
            ddlResponsavel.DataValueField = "CO_RESP";

            ddlResponsavel.DataSource = res;
            ddlResponsavel.DataBind();

            ddlResponsavel.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega os usuários na combo ddlUsuario, de acordo com o responsável selecionado
        /// </summary>
        protected void CarregaUsuario()
        {
            int coResp = ddlResponsavel.SelectedValue != "" ? int.Parse(ddlResponsavel.SelectedValue) : 0;

            if (coResp != 0)
            {
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.TB108_RESPONSAVEL.CO_RESP == coResp
                           select new
                           {
                               tb07.CO_ALU,
                               tb07.NO_ALU
                           }).OrderBy(o => o.NO_ALU);

                ddlUsuario.DataTextField = "NO_ALU";
                ddlUsuario.DataValueField = "CO_ALU";

                ddlUsuario.DataSource = res;
                ddlUsuario.DataBind();

                ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlUsuario.Items.Clear();
                ddlUsuario.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        #endregion

        #region Funções de campo

        /// <summary>
        /// Método executado quando um responsável é selecionado
        /// </summary>
        protected void ddlResponsavel_SelectedChanged(object sender, EventArgs e)
        {
            CarregaUsuario();
        }

        /// <summary>
        /// Método executado quando um usuário é selecionado
        /// </summary>
        protected void ddlUsuario_SelectedChanged(object sender, EventArgs e)
        {
            CarregaGridSolicitacoes();
        }

        /// <summary>
        /// Método executando quando um checkbox da grid de solicitações é checkado
        /// </summary>
        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = ((CheckBox)sender);
            int idReser = 0;

            #region Desmarca os checkbox já marcados
            int numeroLinha = -1;
            if (grdSoli.Rows.Count > 0)
            {
                foreach (GridViewRow linha in grdSoli.Rows)
                {
                    CheckBox marcado = (CheckBox)linha.FindControl("ckSelect");
                    if (marcado.ClientID != atual.ClientID)
                    {
                        marcado.Checked = false;
                    }
                    else
                    {
                        numeroLinha = linha.RowIndex;
                        // Pega o ID da solicitação
                        idReser = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidIdReser")).Value);
                    }
                }
            }
            #endregion

            hidIdReserSel.Value = idReser.ToString();
            CarregaItemsSolicitacao(idReser);
        }

        /// <summary>
        /// Método executado pelo botão EFETIVAR
        /// </summary>
        protected void btnEfeEntre_Click(object sender, EventArgs e)
        {
            TB109_DETAL_ENTRE tb109;
            TB094_ITEM_RESER_MEDIC tb094;
            int idItemReser = 0;
            int idReser = 0;
            decimal qtEntre = 0;
            int coEmpItemReser = 0;
            int coEmpEntre = LoginAuxili.CO_EMP;
            int coColEntre = LoginAuxili.CO_COL;
            int mesRef = 0;
            DateTime dtEntre = DateTime.Now;
            string stm1 = "";
            string stm2 = "";
            string stm3 = "";
            string stm4 = "";

            RegistroLog log = new RegistroLog();

            foreach (GridViewRow linha in grdItemSoli.Rows)
            {
                qtEntre = ((TextBox)linha.Cells[9].FindControl("txtQtdEntre")).Text != "" ? decimal.Parse(((TextBox)linha.Cells[9].FindControl("txtQtdEntre")).Text.Replace('.', ',')) : 0;
                if (qtEntre != 0)
                {
                    idItemReser = int.Parse(((HiddenField)linha.Cells[9].FindControl("hidIdItemReser")).Value);
                    mesRef = int.Parse(((DropDownList)linha.Cells[10].FindControl("ddlMesRef")).SelectedValue);
                    qtEntre = decimal.Parse(((TextBox)linha.Cells[9].FindControl("txtQtdEntre")).Text.Replace('.', ','));
                    coEmpItemReser = int.Parse(((HiddenField)linha.Cells[9].FindControl("hidCoEmpItem")).Value);


                    #region Atualiza o item da solicitação
                    tb094 = TB094_ITEM_RESER_MEDIC.RetornaTodosRegistros().Where(w => w.ID_ITEM_RESER_MEDIC == idItemReser).FirstOrDefault();
                    tb094.TB092_RESER_MEDICReference.Load();
                    tb094.TB90_PRODUTOReference.Load();
                    idReser = tb094.TB092_RESER_MEDIC.ID_RESER_MEDIC;
                    switch (mesRef)
                    {
                        case 1:
                            tb094.QT_ENTR_MES1 = tb094.QT_ENTR_MES1 + qtEntre;
                            break;
                        case 2:
                            tb094.QT_ENTR_MES2 = tb094.QT_ENTR_MES2 + qtEntre;
                            break;
                        case 3:
                            tb094.QT_ENTR_MES3 = tb094.QT_ENTR_MES3 + qtEntre;
                            break;
                        case 4:
                            tb094.QT_ENTR_MES4 = tb094.QT_ENTR_MES4 + qtEntre;
                            break;
                    }
                    #endregion

                    stm1 = tb094.QT_MES1 == tb094.QT_ENTR_MES1 ? "T" : tb094.QT_ENTR_MES1 != 0 ? "P" : "A";
                    stm2 = tb094.QT_MES2 == tb094.QT_ENTR_MES2 ? "T" : tb094.QT_ENTR_MES2 != 0 ? "P" : "A";
                    stm3 = tb094.QT_MES3 == tb094.QT_ENTR_MES3 ? "T" : tb094.QT_ENTR_MES3 != 0 ? "P" : "A";
                    stm4 = tb094.QT_MES4 == tb094.QT_ENTR_MES4 ? "T" : tb094.QT_ENTR_MES4 != 0 ? "P" : "A";

                    if (stm1 == "A" && stm2 == "A" && stm3 == "A" && stm4 == "A")
                    {
                        tb094.ST_ITEM_RESER = "A";
                    }
                    else
                    {
                        if (stm1 == "T" && stm2 == "T" && stm3 == "T" && stm4 == "T")
                        {
                            tb094.ST_ITEM_RESER = "T";
                        }
                        else
                        {
                            tb094.ST_ITEM_RESER = "P";
                        }
                    }

                    #region Grava na tabela de registro de entrega

                    tb109 = new TB109_DETAL_ENTRE();
                    tb109.TB094_ITEM_RESER_MEDIC = tb094;
                    tb109.CO_EMP = tb094.CO_EMP;
                    tb109.CO_EMP_ENTRE = coEmpEntre;
                    tb109.QT_ENTREGA = qtEntre;
                    tb109.MES_REF = mesRef;
                    tb109.CO_COL_ENTRE = coColEntre;
                    tb109.DT_ENTRE = dtEntre;

                    #endregion

                    #region Grava a movimentação do estoque

                    int tpMov = TB93_TIPO_MOVIMENTO.RetornaPelaSigla("SEM").CO_TIPO_MOV;

                    int resMov = AuxiliGeral.movimentaEstoque(tb094.TB90_PRODUTO.CO_PROD, LoginAuxili.CO_EMP, qtEntre, tpMov, "", DateTime.Now);

                    #endregion
                }
            }

            #region Verifica as entregas

            TB092_RESER_MEDIC tb092 = TB092_RESER_MEDIC.RetornaTodosRegistros().Where(w => w.ID_RESER_MEDIC == idReser).FirstOrDefault();

            string stItem = TB092_RESER_MEDIC.RetornaStatus(tb092);

            tb092.ST_RESER_MEDIC = stItem;

            TB092_RESER_MEDIC.SaveOrUpdate(tb092, false);

            log.RegistroLOG(tb092, RegistroLog.ACAO_EDICAO);
            #endregion


            tb094 = new TB094_ITEM_RESER_MEDIC();
            log.RegistroLOG(tb094, RegistroLog.ACAO_EDICAO);

            tb109 = new TB109_DETAL_ENTRE();
            log.RegistroLOG(tb109, RegistroLog.ACAO_GRAVAR);

            

            AuxiliPagina.EnvioMensagemSucesso(this, "Entrega efetivada com sucesso!");

            CarregaItemsSolicitacao(idReser);
        }

        /// <summary>
        /// Método executao pelo botão EXTRATO
        /// </summary>
        protected void btnExtEntre_Click(object sender, EventArgs e)
        {
            string parametros = "";
            string infos = "";
            int coEmp, lRetorno, idReser, coUsuario, coResp;

            coEmp = LoginAuxili.CO_EMP;
            idReser = int.Parse(hidIdReserSel.Value);
            coUsuario = int.Parse(ddlUsuario.SelectedValue);
            coResp = int.Parse(ddlResponsavel.SelectedValue);


            RptExtratoEntrega fpcb = new RptExtratoEntrega();
            lRetorno = fpcb.InitReport(parametros, coEmp, infos, "E", idReser, coUsuario, coResp, null, null, null, null, null);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Método executado pelo botão GUIA
        /// </summary>
        protected void btnGuiEntre_Click(object sender, EventArgs e)
        {
            string parametros = "";
            string infos = "";
            int coEmp, lRetorno, idReser, coUsuario, coResp;

            coEmp = LoginAuxili.CO_EMP;
            idReser = int.Parse(hidIdReserSel.Value);
            coUsuario = int.Parse(ddlUsuario.SelectedValue);
            coResp = int.Parse(ddlResponsavel.SelectedValue);


            RptExtratoEntrega fpcb = new RptExtratoEntrega();
            lRetorno = fpcb.InitReport(parametros, coEmp, infos, "G", idReser, coUsuario, coResp, null, null, null, null, null);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion
    }
}