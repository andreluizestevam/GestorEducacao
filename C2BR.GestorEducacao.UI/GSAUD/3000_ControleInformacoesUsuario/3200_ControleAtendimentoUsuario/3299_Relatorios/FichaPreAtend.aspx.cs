//=============================================================================
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
//29/11/2014| Maxwell Almeida            | Criação da página para emissão da ficha de pré-atendimento
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
using System.Data.Objects;
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
    public partial class FichaPreAtend : System.Web.UI.Page
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
                IniPeri.Text = DateTime.Now.ToString();
                FimPeri.Text = DateTime.Now.ToString();
                CarregaGrid("");
            }
        }

        #endregion

        #region Carregamentos

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (string.IsNullOrEmpty(hidCoPreAtend.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecione ao menos um Pré-Atendimento para emitir a Ficha");
                return;
            }

            //--------> Variáveis de parâmetro do Relatório
            string infos;
            int lRetorno, coAtend;

            coAtend = int.Parse(hidCoPreAtend.Value);

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptFichaPreAtend rpt = new RptFichaPreAtend();
            lRetorno = rpt.InitReport("", infos, LoginAuxili.CO_EMP, coAtend, "");
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega a grid de Atendimentos
        /// </summary>
        protected void CarregaGrid(string NO_ALU)
        {
            DateTime dtFim = DateTime.Parse(FimPeri.Text);
            DateTime dtIni = DateTime.Parse(IniPeri.Text);
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            var res = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs194.CO_EMP equals tb25.CO_EMP
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs194.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs194.NO_USU.Contains(NO_ALU)
                       && (coEmp != 0 ? tbs194.CO_EMP == coEmp : 0 == 0)
                       && ((EntityFunctions.TruncateTime(tbs194.DT_PRE_ATEND) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs194.DT_PRE_ATEND) <= EntityFunctions.TruncateTime(dtFim)))
                       select new Saida
                       {
                           Data = tbs194.DT_PRE_ATEND,
                           UNID = tb25.NO_FANTAS_EMP,
                           ID_PRE_ATEND = tbs194.ID_PRE_ATEND,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           NO_RESP = tbs194.NO_RESP,

                           SENHA = tbs194.NU_SENHA_ATEND,
                           CO_PRE = tbs194.CO_PRE_ATEND,
                           NO_ALU = tbs194.NO_USU,
                       }).DistinctBy(w => w.ID_PRE_ATEND).OrderBy(w => w.Data).ToList();

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
                    return (this.Data.HasValue ? this.Data.Value.ToString("dd/MM/yy") + " " + this.Data.Value.ToString("HH:mm") : " - ");
                }
            }
            public string UNID { get; set; }
            public string NO_ESPEC { get; set; }
            public string NO_RESP { get; set; }
            public string NO_ALU { get; set; }
            public int ID_PRE_ATEND { get; set; }
            public string SENHA { get; set; }
            public string CO_PRE { get; set; }
            public string CO_PRE_V
            {
                get
                {
                    return AuxiliFormatoExibicao.FormataCodigosServicosSaude(this.CO_PRE);
                }
            }
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
                            hidCoPreAtend.Value = (((HiddenField)linha.Cells[0].FindControl("hidCoPreAtend")).Value);
                        else
                            hidCoPreAtend.Value = "";
                    }
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void imgPesqGridAgenda_OnClick(object sender, EventArgs e)
        {
            CarregaGrid(txtNomePacie.Text);
        }

        #endregion
    }
}