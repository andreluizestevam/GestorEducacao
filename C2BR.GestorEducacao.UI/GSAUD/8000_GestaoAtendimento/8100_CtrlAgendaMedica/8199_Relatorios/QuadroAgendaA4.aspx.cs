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
//25/04/2014    Maxwell Almeida             Criação da página de Filtro de Relatórios e do Relatório em Si.

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
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3300_Consultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class QuadroAgendaA4 : System.Web.UI.Page
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
                if (LoginAuxili.FLA_USR_DEMO)
                    IniPeri.Text = LoginAuxili.DATA_INICIO_USU_DEMO.ToShortDateString();

                carregaUnidades();
                carregaUnidadesContrato();
                carregaEspecialidade();
                carregaClassificaoProfissional();
                carregaMedicos();
                carregaUnidadesDaConsulta();
                carregaLocal();
                carregaEspecialidadeConsulta();
            }
        }

        #endregion

        #region Carregamentos

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

            string parametros;
            string infos;
            int coEmp, lRetorno;
            //string noMedic = "";
            //string noSitu = "";
            //string noStatus = "";
            //int coEmp, lRetorno, unidade, deptoLocal, Espec, Medico, materia, anoRef;
            //string dataIni, dataFim, Status, situacao, noUnidade, noDeptoLocal, noEspec, noMedico, Periodo;

            coEmp = LoginAuxili.CO_EMP;

            int UnidadeCadastro = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int UnidadeContrato = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            int Especialidade = ddlEspec.SelectedValue != "" ? int.Parse(ddlEspec.SelectedValue) : 0;
            string ClassificacaoProfissional = ddlClassificacaoProfissional.SelectedValue != "" ? ddlClassificacaoProfissional.SelectedValue : "0";
            int ProfissionalSaude = ddlMedico.SelectedValue != "" ? int.Parse(ddlMedico.SelectedValue) : 0;
            int UnidadeConsulta = ddlUnidConsulta.SelectedValue != "" ? int.Parse(ddlUnidConsulta.SelectedValue) : 0;
            int DeptLocal = ddlDeptLocal.SelectedValue != "" ? int.Parse(ddlDeptLocal.SelectedValue) : 0;
            int EspecialidadeConsulta = ddlEspecialidadeConsulta.SelectedValue != "" ? int.Parse(ddlEspecialidadeConsulta.SelectedValue) : 0;
            string Status = ddlStatus.SelectedValue != "" ? ddlStatus.SelectedValue : "";
            string Situacao = ddlSituacao.SelectedValue != "" ? ddlSituacao.SelectedValue : "";
            string dataIni = IniPeri.Text;

            DateTime dt = DateTime.Parse(IniPeri.Text);
            var qtdDias = 4;

            if (CheckBoxSabado.Checked)
                qtdDias++;
            if (CheckBoxDomingo.Checked)
                qtdDias++;

            string dataFim = dataFim = dt.AddDays(qtdDias).ToString();
            string noStatus = "";
            string noSituacao = "";
            //bool VerValor = CheckBoxVerValor.Checked;
            int CodUnidContrato = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;

            switch (Situacao)
            {
                case "0":
                    noSituacao = "0";
                    break;

                case "A":
                    noSituacao = "Em Aberto";
                    break;

                case "C":
                    noSituacao = "Cancelado";
                    break;

                case "R":
                    noSituacao = "Realizado";
                    break;
            }

            switch (Status)
            {
                case "0":
                    noStatus = "0";
                    break;

                case "C":
                    noStatus = "Confirmado";
                    break;

                case "N":
                    noStatus = "Não Confirmado";
                    break;

                case "I":
                    noStatus = "Indisponível";
                    break;
            }

            
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region

            var NO_RELATORIO = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/QuadroAgendaA4.aspx");

            var res = TB03_COLABOR.RetornaTodosRegistros().Where(a => a.CO_COL == ProfissionalSaude).SingleOrDefault();

            parametros = "( Profissional : " + res.CO_MAT_COL + " - " + ddlMedico.SelectedItem + " - Classificação: " + ddlClassificacaoProfissional.SelectedItem + " - Período de " + DateTime.Parse(IniPeri.Text).ToString("dd/MM/yy") + " à " + DateTime.Parse(dataFim).ToString("dd/MM/yy") + " )";
            #endregion

            if (!chkModelLocalProced.Checked)
            {
                RptQuadroAgendamentosA4 fpcb = new RptQuadroAgendamentosA4();
                lRetorno = fpcb.InitReport(parametros, infos, coEmp, UnidadeCadastro, UnidadeContrato, Especialidade, ClassificacaoProfissional, ProfissionalSaude, UnidadeConsulta, DeptLocal, EspecialidadeConsulta, noSituacao, noStatus, dataIni, dataFim, CheckBoxSabado.Checked, CheckBoxDomingo.Checked, false, drpLivre.SelectedValue, NO_RELATORIO.ToUpper());
                Session["Report"] = fpcb;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            else
            {
                bool pasta = true;
                RptQuadroAgendamentos2A4 fpcb = new RptQuadroAgendamentos2A4();
                lRetorno = fpcb.InitReport(parametros, infos, coEmp, UnidadeCadastro, UnidadeContrato, Especialidade, ClassificacaoProfissional, ProfissionalSaude, UnidadeConsulta, DeptLocal, EspecialidadeConsulta, noSituacao, noStatus, dataIni, dataFim, CheckBoxSabado.Checked, CheckBoxDomingo.Checked, false, drpLivre.SelectedValue, NO_RELATORIO.ToUpper(), pasta);
                Session["Report"] = fpcb;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
        }

        #region Contexo da Consulta

        /// <summary>
        /// Carrega as Unidades Cadastro
        /// </summary>
        private void carregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as Unidades Contrato
        /// </summary>
        private void carregaUnidadesContrato()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidContrato, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega as Especialidades
        /// </summary>
        private void carregaEspecialidade()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, coEmp, null, true);
        }

        /// <summary>
        /// Carrega as Classificação Profissional
        /// </summary>
        private void carregaClassificaoProfissional()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassificacaoProfissional, true, LoginAuxili.CO_EMP);
        }

        /// <summary>
        /// Carrega as unidades de Saude
        /// </summary>
        protected void CarregaProfissionalSaude()
        {
            ddlMedico.Items.Clear();
            int UnidadeDeCadastro = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int UnidadeDeContrato = ddlUnidContrato.SelectedValue != "" ? int.Parse(ddlUnidContrato.SelectedValue) : 0;
            int Especialidade = ddlEspec.SelectedValue != "" ? int.Parse(ddlEspec.SelectedValue) : 0;
            string ProgramacoaClassificacao = ddlClassificacaoProfissional.SelectedValue != "" ? ddlClassificacaoProfissional.SelectedValue : "";
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FLA_PROFESSOR == "S"
                        && (UnidadeDeCadastro != 0 ? tb03.CO_EMP == UnidadeDeCadastro : 0 == 0)
                        && (UnidadeDeContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidadeDeContrato : 0 == 0)
                         && (ProgramacoaClassificacao != "0" ? tb03.CO_CLASS_PROFI == ProgramacoaClassificacao : 0 == 0)
                         && (Especialidade != 0 ? tb03.CO_ESPEC == UnidadeDeContrato : 0 == 0)

                       select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();

            if (res.Count > 0)
            {
                ddlMedico.DataValueField = "CO_COL";
                ddlMedico.DataTextField = "NO_COL";
                ddlMedico.DataSource = res;
                ddlMedico.DataBind();
            }
            else
            {
                ddlMedico.Items.Clear();
            }
        }

        /// <summary>
        /// Carrega os colaboradores médicos
        /// </summary>
        private void carregaMedicos()
        {
            int unid = int.Parse(ddlUnidade.SelectedValue);
            string ProgramacoaClassificacao = ddlClassificacaoProfissional.SelectedValue != "" ? ddlClassificacaoProfissional.SelectedValue : "";
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros().Where(a => a.FLA_PROFESSOR == "S")
                       where unid != 0 ? tb03.CO_EMP == unid : 0 == 0 && (ProgramacoaClassificacao != "0" ? tb03.CO_CLASS_PROFI == ProgramacoaClassificacao : 0 == 0)
                       select new { tb03.NO_COL, tb03.CO_COL, tb03.CO_EMP });

            ddlMedico.DataTextField = "NO_COL";
            ddlMedico.DataValueField = "CO_COL";
            ddlMedico.DataSource = res;
            ddlMedico.DataBind();
            //CarregaProfissionalSaude();

        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Carrega as Unidades da consulta
        /// </summary>
        private void carregaUnidadesDaConsulta()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidConsulta, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }
        /// <summary>
        /// Carrega as Local Consulta
        /// </summary>
        private void carregaLocal()
        {
            AuxiliCarregamentos.CarregaDepartamentos(ddlDeptLocal, LoginAuxili.CO_EMP, true);
        }

        private void carregaEspecialidadeConsulta()
        {

            ddlEspecialidadeConsulta.Items.Clear();
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecialidadeConsulta, LoginAuxili.CO_EMP, null, true);
            ddlEspecialidadeConsulta.Items.Insert(0, new ListItem("Todos", "0"));



        }

        #endregion

        /// <summary>
        /// Carrega os Departamentos
        /// </summary>
        private void carregaDepartamentos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaDepartamentos(ddlDeptLocal, coEmp, true);
        }

        #endregion

        #region Funções de Campo

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaMedicos();
        }

        protected void ddlUnidContrato_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaMedicos();
        }

        protected void lblEspec_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaMedicos();
        }

        protected void ddlClassificacaoProfissional_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaMedicos();
        }

        protected void IniPeri_OnTextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(IniPeri.Text))
            //{
            //    DateTime dt = DateTime.Parse(IniPeri.Text);
            //    FimPeri.Text = (dt.AddDays(7).ToString());
            //}
        }

        #endregion
    }
}