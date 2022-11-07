//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: CADASTRAMENTO GERAL DE TÍTULOS DE RECEITAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5207_CancelarTituReceita
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return ((PadraoCadastros)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            string nomeEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).NO_FANTAS_EMP;
            txtNomeEmp.Text = nomeEmp;

            CarregaResponsavel();
            CarregaAgrupador();
            CarregaUnidadeContrato();
            VerificaTipoUnid();
        }

        #endregion

        #region Carregamento

        //====> Método que carrega o DropDown Nome da Fonte com os Alunos
        private void CarregaResponsavel()
        {
            ddlResponsavel.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                         where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         select new { tb108.CO_RESP, tb108.NO_RESP }).OrderBy(a => a.NO_RESP);

            ddlResponsavel.DataTextField = "NO_RESP";
            ddlResponsavel.DataValueField = "CO_RESP";
            ddlResponsavel.DataBind();

            ddlResponsavel.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        //====> Método que carrega o DropDown Nome da Fonte com os Alunos
        private void CarregaAlunos()
        {
            int coResp = int.Parse(ddlResponsavel.SelectedValue);
            if (coResp == 0)
            {
                ddlAluno.DataSource = null;
                return;
            }

            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP)
                                   where tb07.TB108_RESPONSAVEL.CO_RESP == coResp
                                   select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy(a => a.NO_ALU);

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        //====> Método que carrega o DropDown de Unidades de Contrato
        private void CarregaAgrupador()
        {
            ddlAgrupador.DataSource = from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                      where tb315.TP_AGRUP_RECDESP == "R"
                                      select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP };

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Todos", "0"));
        }

        //====> Método que carrega o DropDown de Unidades de Contrato
        private void CarregaUnidadeContrato()
        {
            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Verifica o tipo da unidade e faz as devidas alterações
        /// </summary>
        private void VerificaTipoUnid()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    lblAluPaci.Text = "Paciente";
                    break;
                case "PGE":
                default:
                    lblAluPaci.Text = "Aluno(a)";
                    break;
            }
        }


        #endregion

        protected void ddlResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (ddlResponsavel.SelectedValue == "" || ddlResponsavel.SelectedValue == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione o responsável.");
                return;
            }
            if (ddlAluno.SelectedValue == "" || ddlAluno.SelectedValue == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione o aluno.");
                return;
            }
            int agrupador = int.Parse(ddlAgrupador.SelectedValue);
            int undContrato = int.Parse(ddlUnidadeContrato.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue);

            DateTime dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            DateTime dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);

            var lst = from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                      where tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                      && tb47.IC_SIT_DOC == "A" && tb47.CO_ALU == coAlu
                      && (agrupador != 0 ? tb47.CO_AGRUP_RECDESP == agrupador : agrupador == 0)
                      && (undContrato != 0 ? tb47.CO_EMP_UNID_CONT == undContrato : undContrato == 0)
                      select new Dummy
                      {
                          Checked = false,
                          NU_DOC = tb47.NU_DOC,
                          NU_PAR = tb47.NU_PAR,
                          VR_PAR_DOC = tb47.VR_PAR_DOC,
                          DT_VEN_DOC = tb47.DT_VEN_DOC,
                          DE_HISTORICO = tb47.TB39_HISTORICO.DE_HISTORICO
                      };

            this.grdResumo.DataSource = lst.ToList();
            this.grdResumo.DataBind();

            divGrid.Visible = liResumo.Visible = true;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool editado = false;

            // Varre toda a grid de Pesquisa
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                // Verifica se linha está checada
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    string NuDoc = linha.Cells[1].Text;
                    int NuPar = int.Parse(linha.Cells[2].Text);

                    // Faz a adicão do item
                    var tit = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, NuDoc, NuPar);
                    tit.IC_SIT_DOC = "C";
                    tit.DT_SITU_DOC = DateTime.Now;

                    if (GestorEntities.SaveOrUpdate(tit) < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar um dos títulos.");
                        return;
                    }

                    editado = true;
                }
            }

            if (!editado)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione ao menos um título para alterar o status.");
                return;
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Status Alterado(s) com Sucesso!", Request.Url.AbsoluteUri);
        }
    }

    public class Dummy
    {
        public string NU_DOC { get; set; }
        public int NU_PAR { get; set; }
        public decimal VR_PAR_DOC { get; set; }
        public DateTime DT_VEN_DOC { get; set; }
        public string DE_HISTORICO { get; set; }
        public bool Checked { get; set; }
    }
}