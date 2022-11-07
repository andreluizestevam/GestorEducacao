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
//20/01/2015| Maxwell Almeida            | Criação da página 
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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3290_RelatoriosFinanceiros
{
    public partial class ExtratoFinanceiroAtend : System.Web.UI.Page
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
                AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
                AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecialidade, LoginAuxili.CO_EMP, null, true);

                IniPeri.Text = DateTime.Now.ToString();
                FimPeri.Text = DateTime.Now.ToString();
                CarregaGrid();
            }
        }

        #endregion

        #region Carregamentos

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (string.IsNullOrEmpty(hidCoAtend.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecione ao menos um atendimento para emitir o receituário");
                return;
            }

            string parametros;
            string infos;
            int coUnid, lRetorno, coPac, coEspec;
            string dataIni, dataFim, noUnidade, noPaci, noEspeci;

            coUnid = int.Parse(ddlUnidade.SelectedValue);
            coEspec = int.Parse(ddlEspecialidade.SelectedValue);
            coPac = int.Parse(ddlPaciente.SelectedValue);
            dataIni = IniPeri.Text;
            dataFim = FimPeri.Text;

            noUnidade = ddlUnidade.SelectedItem.Text;
            noEspeci = ddlEspecialidade.SelectedItem.Text;
            noPaci = ddlPaciente.SelectedItem.Text;

            parametros = "( Unidade: " + noUnidade.ToUpper() + " - Especialidade: " + noEspeci.ToUpper() + " - Paciente: " + noPaci.ToUpper() + " - Período: " + IniPeri.Text + " à " + FimPeri.Text + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptExtratoFinanceiroAtendimento fpcb = new RptExtratoFinanceiroAtendimento();
            lRetorno = fpcb.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnid, coEspec, coPac, IniPeri.Text, FimPeri.Text, int.Parse(hidCoAtend.Value));
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        protected void CarregaPacientes()
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tb07.CO_ALU equals tbs219.CO_ALU
                       select new { tb07.NO_ALU, tb07.CO_ALU }).Distinct().OrderBy(w => w.NO_ALU).ToList();

            ddlPaciente.DataTextField = "NO_ALU";
            ddlPaciente.DataValueField = "CO_ALU";
            ddlPaciente.DataSource = res;
            ddlPaciente.DataBind();

            ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));
        }

         /// <summary>
        /// Carrega a grid de Atendimentos
        /// </summary>
        protected void CarregaGrid()
        {
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int coAlu = (!string.IsNullOrEmpty(ddlPaciente.SelectedValue) ? int.Parse(ddlPaciente.SelectedValue) : 0);
            int coEsp = (!string.IsNullOrEmpty(ddlEspecialidade.SelectedValue) ? int.Parse(ddlEspecialidade.SelectedValue) : 0);
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeri.Text) ? DateTime.Parse(IniPeri.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeri.Text) ? DateTime.Parse(FimPeri.Text) : DateTime.Now);

            var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs219.CO_EMP_CADAS equals tb25.CO_EMP
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs219.CO_COL equals tb03.CO_COL
                       where (coEmp != 0 ? tbs219.CO_EMP == coEmp : 0 == 0)
                       && (coAlu != 0 ? tbs219.CO_ALU == coAlu : 0 == 0)
                       && (coEsp != 0 ? tbs219.CO_ESPECIALIDADE == coEsp : 0 == 0)
                       && (tbs219.DT_ATEND_CADAS >= dtIni)
                       && (tbs219.DT_ATEND_CADAS <= dtFim)
                       select new Saida
                       {
                           Data = tbs219.DT_ATEND_CADAS,
                           UNID = tb25.NO_FANTAS_EMP,
                           NO_COL = tb03.NO_COL,
                           ID_ATEND = tbs219.ID_ATEND_MEDIC,
                           NO_ALU = tb07.NO_ALU,
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
            public string NO_ALU { get; set; }
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

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void ddlPaciente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void ddlEspecialidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void IniPeri_OnTextChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void FimPeri_OnTextChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        #endregion
    }
}