//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE TÍTULOS DE RECEITAS/RECURSOS - GERAL
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 10/06/2013| Victor Martins Machado     | Foi inserido nos filtros do relatório a combo de Responsável financeiro
//           |                            | do aluno, que só é apresentado quando é selecionado na combo de
//           |                            | 'Pesquisado por' o valor de 'Responsável'.
//           |                            | 

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
using C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios
{
    public partial class RelatoPosicaoRecursosReceitas : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();

                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaResponsavel();
                CarregaAluno();
                CarregaClientes();
                CarregaAgrupadores();

                liResponsavel.Visible = false;
                VerificaTipoUnid();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_NU_DOC, strINFOS, strP_IC_SIT_DOC, strP_CO_ANO_MES_MAT, strUniContr, strPesqPor;
            int strP_CO_EMP, strP_CO_TUR, strP_CO_MODU_CUR, strP_CO_CUR,
                strP_CO_ALU, strP_CO_EMP_REF, strP_CO_AGRUP, strP_CO_CLIENTE, strP_CO_RESP;
            DateTime strP_DT_INI, strP_DT_FIM;

            //--------> Inicializa as variáveis
            strP_NU_DOC = null;
            strP_CO_ANO_MES_MAT = null;
            strUniContr = null;
            strPesqPor = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strParametrosRelatorio = "";
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_NU_DOC = txtNumDoc.Text;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strUniContr = ddlUnidContrato.SelectedValue;
            strPesqPor = ddlPesqPor.SelectedValue;
            strP_CO_MODU_CUR = ddlTipoPessoa.SelectedValue == "A" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            strP_CO_CUR = ddlTipoPessoa.SelectedValue == "A" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            strP_CO_TUR = ddlTipoPessoa.SelectedValue == "A" ? int.Parse(ddlTurma.SelectedValue) : 0;
            strP_CO_ANO_MES_MAT = ddlTipoPessoa.SelectedValue == "A" ? ddlAnoRefer.SelectedValue : "T";
            strP_CO_ALU = ddlTipoPessoa.SelectedValue == "A" ? int.Parse(ddlAlunos.SelectedValue) : 0;
            strP_CO_CLIENTE = ddlTipoPessoa.SelectedValue == "C" ? int.Parse(ddlClientes.SelectedValue) : 0;
            strP_IC_SIT_DOC = ddlStaDocumento.SelectedValue;
            strP_DT_INI = DateTime.Parse(txtDataPeriodoIni.Text);
            strP_DT_FIM = DateTime.Parse(txtDataPeriodoFim.Text);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strP_CO_RESP = int.Parse(ddlResponsavel.SelectedValue);

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            string siglaUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(strP_CO_EMP_REF).sigla;
            string siglaUnidContr = strUniContr != "T" ? TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(strUniContr)).sigla : "Todas";
            string descPesqu = ddlPesqPor.SelectedValue == "V" ? "Data Vencto" : ddlPesqPor.SelectedItem.ToString();

            //Monta parâmetro exclusivo para escolas em string.
            string paramEscolas = (LoginAuxili.CO_TIPO_UNID == "PGE" ? "- Ano Ref.: " + ddlAnoRefer.SelectedItem.ToString()
                + " - Modal.: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() +
                " - Turma: " + ddlTurma.SelectedItem.ToString() : "");

            strParametrosRelatorio = "( Unidade: " + siglaUnid + "- Unid. Contrato: " + siglaUnidContr + "- Buscar Por: " + descPesqu + paramEscolas + " - Documentos: " + ddlStaDocumento.SelectedItem.ToString()
                + " - Período: " + DateTime.Parse(txtDataPeriodoIni.Text).ToString("dd/MM/yy") + " à " + DateTime.Parse(txtDataPeriodoFim.Text).ToString("dd/MM/yy") + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " )";

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GEDUC/5000_CtrlFinanceira/5200_CtrlReceitas/5299_Relatorios/RelatoPosicaoRecursosReceitas.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptRelatoPosicaoRecursosReceitas fpcb = new RptRelatoPosicaoRecursosReceitas();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF,
                strUniContr, strPesqPor, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_ALU, strP_CO_CLIENTE, strP_IC_SIT_DOC, strP_DT_INI, strP_DT_FIM, strP_NU_DOC, strP_CO_AGRUP, chkIncluiCancel.Checked, strINFOS, strP_CO_RESP, NO_RELATORIO);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Verifica o tipo da unidade e faz as devidas alterações
        /// </summary>
        private void VerificaTipoUnid()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    lblNomeAluPac.Text = "Paciente";

                    ddlTipoPessoa.Items.Clear();
                    ddlTipoPessoa.Items.Insert(0, new ListItem("Cliente", "C"));
                    ddlTipoPessoa.Items.Insert(0, new ListItem("Paciente", "A"));
                    divItensEscola.Visible = false;
                    break;
                case "PGE":
                default:
                    lblNomeAluPac.Text = "Aluno(a)";

                    ddlTipoPessoa.Items.Clear();
                    ddlTipoPessoa.Items.Insert(0, new ListItem("Cliente", "C"));
                    ddlTipoPessoa.Items.Insert(0, new ListItem("Aluno", "A"));
                    divItensEscola.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();


            ddlUnidContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                         where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidContrato.DataValueField = "CO_EMP";
            ddlUnidContrato.DataBind();

            ddlUnidContrato.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, serie, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaAno(ddlAnoRefer, coEmp, true);
            //ddlAnoRefer.Items.Insert(0, new ListItem("Todos", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de responsáveis
        /// </summary>
        private void CarregaResponsavel()
        {
            AuxiliCarregamentos.CarregaResponsaveis(ddlResponsavel, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coResp = int.Parse(ddlResponsavel.SelectedValue);
            int modalidade = ddlModalidade.SelectedValue != "0" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = (!string.IsNullOrEmpty(ddlTurma.SelectedValue) ? int.Parse(ddlTurma.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAlunos, coEmp, modalidade, serie, turma, ddlAnoRefer.SelectedValue, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.DataSource = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                       where tb315.TP_AGRUP_RECDESP == "R" || tb315.TP_AGRUP_RECDESP == "T"
                                       select new { tb315.ID_AGRUP_RECDESP, tb315.DE_SITU_AGRUP_RECDESP });

            ddlAgrupador.DataTextField = "DE_SITU_AGRUP_RECDESP";
            ddlAgrupador.DataValueField = "ID_AGRUP_RECDESP";
            ddlAgrupador.DataBind();

            ddlAgrupador.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Clientes
        /// </summary>
        private void CarregaClientes()
        {
            ddlClientes.DataSource = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                      select new { tb103.CO_CLIENTE, tb103.NO_FAN_CLI });

            ddlClientes.DataTextField = "NO_FAN_CLI";
            ddlClientes.DataValueField = "CO_CLIENTE";
            ddlClientes.DataBind();

            ddlClientes.Items.Insert(0, new ListItem("Todos", "0"));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaAluno();
        }

        protected void ddlTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPessoa.SelectedValue == "A")
            {
                liAluno.Visible = liAnoRefer.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
                liCliente.Visible = false;
            }
            else
            {
                liAluno.Visible = liAnoRefer.Visible = liModalidade.Visible = liSerie.Visible = liTurma.Visible = false;
                liCliente.Visible = true;
            }
        }

        protected void ddlStaDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStaDocumento.SelectedValue == "T")
            {
                chkIncluiCancel.Visible = true;
            }
            else
            {
                chkIncluiCancel.Visible = false;
                chkIncluiCancel.Checked = false;
            }
        }

        protected void ddlPesqPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPesqPor.SelectedValue != "R")
            {
                liResponsavel.Visible = false;
                ddlResponsavel.SelectedValue = "0";
                CarregaAluno();
            }
            else
            {
                liResponsavel.Visible = true;
                ddlResponsavel.SelectedValue = "0";
                CarregaAluno();
            }
        }

        protected void ddlResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }
    }
}
