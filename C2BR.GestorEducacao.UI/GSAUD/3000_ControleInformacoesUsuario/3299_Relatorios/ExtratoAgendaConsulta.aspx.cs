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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios
{
    public partial class ExtratoAgendaConsulta : System.Web.UI.Page
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
            string dataFim = FimPeri.Text;
            string noStatus = "";
            string noSituacao = "";
            bool VerValor = CheckBoxVerValor.Checked;
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


            parametros = "";//"( Unidade: " + noUnidade.ToUpper() + " - Departamento: " + noDeptoLocal.ToUpper() + " - Médico: " + noMedico.ToUpper() + "" + " - Período: " + Periodo.ToUpper() + " - Status: " + noStatus + " - Situação: " + noSitu + " )";
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region

            string NO_RELATORIO = "";
            NO_RELATORIO = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/3000_ControleInformacoesUsuario/3200_ControleAtendimentoUsuario/3299_Relatorios/ExtratoAgendaConsulta.aspx");

            #endregion

            RptExtratoConsultas fpcb = new RptExtratoConsultas();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, UnidadeCadastro, UnidadeContrato, Especialidade, ClassificacaoProfissional, ProfissionalSaude, UnidadeConsulta, DeptLocal, EspecialidadeConsulta, noSituacao, noStatus, dataIni, dataFim, VerValor,NO_RELATORIO.ToUpper());
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
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
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassificacaoProfissional, true);
        }

        /// <summary>
        /// Carrega as unidades de Saude
        /// </summary>
        protected void CarregaProfissionalSaude()
        {
            //ddlMedico.Items.Clear();
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
                ddlMedico.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                ddlMedico.Items.Clear();
                ddlMedico.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        /// <summary>
        /// Carrega os colaboradores médicos
        /// </summary>
        private void carregaMedicos()
        {
            int unid = int.Parse(ddlUnidade.SelectedValue);

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where unid != 0 ? tb03.CO_EMP == unid : 0 == 0
                       select new { tb03.NO_COL, tb03.CO_COL, tb03.CO_EMP });

            ddlMedico.DataTextField = "NO_COL";
            ddlMedico.DataValueField = "CO_COL";
            ddlMedico.DataSource = res;
            ddlMedico.DataBind();

            ddlMedico.Items.Insert(0, new ListItem("Todos", "0"));
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
            AuxiliCarregamentos.CarregaDepartamentos(ddlDeptLocal, LoginAuxili.CO_EMP , true);
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

        #endregion
    }
}