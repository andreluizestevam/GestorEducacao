//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
// DATA DE CRIAÇÃO: 
//------------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR              | DESCRIÇÃO RESUMIDA
// ---------+-----------------------------------+-------------------------------------
//29/11/2014| Maxwell Almeida                   | Criação da página para emissão da Guia de Consultas
//01/07/2016| Tayguara Acioli   TA.01/07/2016   | Adicionei a pesquisa fonética. 

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
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class GuiaConsulta : System.Web.UI.Page
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

        //TA.01/07/2016 início
        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var result = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                          where (tb07.NO_ALU.Contains(textPaciente.Text))
                       select new { tb07.NO_ALU, tb07.CO_ALU }).Distinct().OrderBy(w => w.NO_ALU).ToList();

            if (result != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = result;
                ddlPaciente.DataBind();
            }

            ddlPaciente.Items.Insert(0, new ListItem("Selecione", ""));

            OcultarPesquisa(true);

        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            textPaciente.Visible =
            imgPesqPacNome.Visible = !ocultar;
            ddlPaciente.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }
        //TA.01/07/2016 fim

        #endregion

        #region Carregamentos

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (string.IsNullOrEmpty(hidCoConsul.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecione ao menos uma Consulta para emitir a Guia.");
                return;
            }

            //--------> Variáveis de parâmetro do Relatório
            string infos;
            int lRetorno, coAtend;

            coAtend = int.Parse(hidCoConsul.Value);

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptGuiaConsulta rpt = new RptGuiaConsulta();
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
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
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
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs174.CO_ALU == CO_ALU
                       select new Saida
                       {
                           Data = tbs174.DT_AGEND_HORAR,
                           UNID = tb25.NO_FANTAS_EMP,
                           ID_AGENDA = tbs174.ID_AGEND_HORAR,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           NO_COL = tb03.NO_COL,
                       }).DistinctBy(w => w.ID_AGENDA).OrderBy(w => w.Data).ToList();

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
            public string NO_ESPEC { get; set; }
            public string NO_COL { get; set; }
            public int ID_AGENDA { get; set; }
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
                            hidCoConsul.Value = (((HiddenField)linha.Cells[0].FindControl("hidCoPreAtend")).Value);
                        else
                            hidCoConsul.Value = "";
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