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
//14/12/2016| BRUNO VIEIRA LANDIM        | Criado nova funcionalidade
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
    public partial class DemonPreAtend : System.Web.UI.Page
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
                CarregaUnidade();
                CarregaPaciente();
                CarregaClassRisco();
                CarregaEspec();
                IniPeri.Text = DateTime.Now.ToString();
                FimPeri.Text = DateTime.Now.ToString();
            }
        }

        #endregion

        #region Carregamentos

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlPaciente.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecione ao menos um Paciente para emitir o demonstrativo");
                return;
            }

            //--------> Variáveis de parâmetro do Relatório
            string infos;
            int lRetorno;
            int rptUnidade, rptPaciente, rptClassRisco, rptEspec;
            string rptTipoEncam, NomeFuncionalidadeCadastrada; ;
            DateTime rptDtIni, rptDtFim;

            rptUnidade = int.Parse(ddlUnidade.SelectedValue);
            rptPaciente = int.Parse(ddlPaciente.SelectedValue);
            rptClassRisco = int.Parse(ddlClassRisco.SelectedValue);
            rptEspec = int.Parse(ddlEspec.SelectedValue);
            rptTipoEncam = ddlTipoEncam.SelectedValue;
            rptDtIni = DateTime.Parse(IniPeri.Text);
            rptDtFim = DateTime.Parse(FimPeri.Text);

            NomeFuncionalidadeCadastrada = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/3000_ControleInformacoesUsuario/3200_ControleAtendimentoUsuario/3299_Relatorios/DemonPreAtend.aspx");

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptDemonPreAtend rpt = new RptDemonPreAtend();
            lRetorno = rpt.InitReport("", infos, NomeFuncionalidadeCadastrada, rptUnidade, rptPaciente, rptClassRisco, rptEspec, rptTipoEncam, rptDtIni, rptDtFim);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaPaciente()
        {
            int unidade = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP);
            AuxiliCarregamentos.CarregaPacientes(ddlPaciente, unidade, false);
        }

        private void CarregaClassRisco()
        {
            var res = (from tbs435 in TBS435_CLASS_RISCO.RetornaTodosRegistros()
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs435.TP_CLASS_RISCO equals tb14.CO_CLASS_RISCO
                       where tb14.FL_AMBUL == "S"
                       select new
                       {
                           tbs435.ID_CLASS_RISCO,
                           tbs435.NO_PRIOR,
                           tbs435.NU_ORDEM
                       }).DistinctBy(e => e.NO_PRIOR).OrderBy(w => w.NU_ORDEM);

            ddlClassRisco.DataTextField = "NO_PRIOR";
            ddlClassRisco.DataValueField = "ID_CLASS_RISCO";
            ddlClassRisco.DataSource = res;
            ddlClassRisco.DataBind();

            ddlClassRisco.Items.Insert(0, new ListItem("Todos", "-1"));
        }

        private void CarregaEspec()
        {
            ddlEspec.Items.Clear();
            var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                       select new { tb63.CO_ESPECIALIDADE, tb63.NO_ESPECIALIDADE }).OrderBy(w => w.NO_ESPECIALIDADE);

            ddlEspec.DataTextField = "NO_ESPECIALIDADE";
            ddlEspec.DataValueField = "CO_ESPECIALIDADE";
            ddlEspec.DataSource = res;
            ddlEspec.DataBind();

            ddlEspec.Items.Insert(0, new ListItem("Todos", "-1"));
        }

        #endregion

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        { CarregaPaciente(); }

        protected void ddlPaciente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaClassRisco();
            CarregaEspec();
        }

        protected void imgBtnPesqPaciente_OnClick(object sender, EventArgs e)
        {
            int unidade = int.Parse(ddlUnidade.SelectedValue);

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_SITU_ALU != "I"
                       && (unidade == 0 ? 0 == 0 : tb07.CO_EMP == unidade)
                       && tb07.NO_ALU.Contains(txtPaciente.Text)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).ToList();

            if (res != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = res;
                ddlPaciente.DataBind();
            }

            ddlPaciente.Items.Insert(0, new ListItem("Selecione", ""));

            txtPaciente.Visible = false;
            ddlPaciente.Visible = true;
            imgBtnPesqPaciente.Visible = false;
            imgBtnDesfPaciente.Visible = true;
        }

        protected void imgBtnDesfPaciente_OnClick(object sender, EventArgs e)
        {
            txtPaciente.Visible = true;
            ddlPaciente.Visible = false;
            imgBtnPesqPaciente.Visible = true;
            imgBtnDesfPaciente.Visible = false;
        }
    }
}