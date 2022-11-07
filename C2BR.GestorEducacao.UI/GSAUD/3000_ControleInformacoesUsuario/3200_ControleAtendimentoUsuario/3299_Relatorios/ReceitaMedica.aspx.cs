﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//15/05/2014| Maxwell Almeida            | Criação da página 
//          |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios
{
    public partial class ReceitaMedica : System.Web.UI.Page
    {
                  public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaPacientes();
                CarregaGrid(0);
            }
        }

        #endregion

        #region Carregamentos

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if(string.IsNullOrEmpty(hidCoAtend.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecione ao menos um atendimento para emitir o receituário");
                return;
            }

            //--------> Variáveis de parâmetro do Relatório
            string infos;
            int lRetorno, coAtend;

            coAtend = int.Parse(hidCoAtend.Value);

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptReceitaMedicaR rpt = new RptReceitaMedicaR();
            lRetorno = rpt.InitReport(infos, LoginAuxili.CO_EMP, coAtend);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        protected void CarregaPacientes()
        {
            var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                       join tb330 in TBS330_RECEI_ATEND_MEDIC.RetornaTodosRegistros() on tbs219.ID_ATEND_MEDIC equals tb330.ID_ATEND_MEDIC
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                       select new { tb07.NO_ALU, tb07.CO_ALU }).Distinct().OrderBy(w => w.NO_ALU).ToList();

            ddlPaciente.DataTextField = "NO_ALU";
            ddlPaciente.DataValueField = "CO_ALU";
            ddlPaciente.DataSource = res;
            ddlPaciente.DataBind();

            ddlPaciente.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega a grid de Atendimentos
        /// </summary>
        protected void CarregaGrid(int CO_ALU)
        {
            var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                       join tbs330 in TBS330_RECEI_ATEND_MEDIC.RetornaTodosRegistros() on tbs219.ID_ATEND_MEDIC equals tbs330.ID_ATEND_MEDIC
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs219.CO_EMP_CADAS equals tb25.CO_EMP
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs330.CO_COL_MEDIC equals tb03.CO_COL
                       where tbs219.CO_ALU == CO_ALU
                       select new Saida
                       {
                           Data = tbs330.DT_RECEI,
                           UNID = tb25.NO_FANTAS_EMP,
                           NO_COL = tb03.NO_COL,
                           ID_ATEND = tbs219.ID_ATEND_MEDIC,
                       }).DistinctBy(w => w.ID_ATEND).OrderBy(w => w.Data).ToList();

            grdAtendimentos.DataSource = res;
            grdAtendimentos.DataBind();
        }

        public class Saida
        {
            public DateTime? Data { get; set; }
            public string Data_V
            {
                get
                {
                    return (this.Data.HasValue ? this.Data.Value.ToString("dd/MM/yy") : " - ");
                }
            }
            public string UNID { get; set; }
            public string NO_COL { get; set; }
            public int ID_ATEND { get; set; }
        }

        #endregion

        #region Funções de Campo

        protected void chkSelectCamp_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdAtendimentos.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAtendimentos.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkSelectCamp");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                            hidCoAtend.Value = (((HiddenField)linha.Cells[0].FindControl("hidCoAtend")).Value);
                        else
                            hidCoAtend.Value = "";
                    }
                }
            }
        }

        public void ddlPaciente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid(!string.IsNullOrEmpty(ddlPaciente.SelectedValue) ? int.Parse(ddlPaciente.SelectedValue) : 0);
        }

        #endregion
    }
}