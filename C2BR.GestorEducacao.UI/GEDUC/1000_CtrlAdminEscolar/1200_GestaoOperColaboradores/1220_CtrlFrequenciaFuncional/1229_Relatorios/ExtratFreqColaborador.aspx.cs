﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE FREQUÊNCIA
// OBJETIVO: EXTRATO DE FREQUÊNCIA DO COLABORADOR (FUNCIONÁRIO)
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1229_Relatorios
{
    public partial class ExtratFreqColaborador : System.Web.UI.Page
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
                CarregaSituacao();
                CarregaDropDown();
                CarregaFuncionarios();
                verificaProfessor();
            }
        }

        //====> Método que faz a chamada de outro método de acordo com o Tipo de Presença
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            RelGerMapaFrequenciaFuncionario();
        }

        //====> Processo de Geração do Relatório
        void RelGerMapaFrequenciaFuncionario()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno, codCol, codEmp;

            //--------> Variáveis de parâmetro do Relatório
            DateTime dtInicio, dtFim;
            string strINFOS;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codCol = int.Parse(ddlFuncionarios.SelectedValue);
            dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptExtratoFreqFuncional rpt = new RptExtratoFreqFuncional();
            lRetorno = rpt.InitReport(codEmp, codCol, dtInicio, dtFim, strINFOS);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Carrega a situação de colaboradores
        /// </summary>
        private void CarregaSituacao()
        {
            ddlSituacaoColab.Items.Clear();
            ddlSituacaoColab.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(statusColaborador.ResourceManager));
            ddlSituacaoColab.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaDropDown()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Verifica se o usuário logado é professor
        /// </summary>
        private void verificaProfessor()
        {
            if (LoginAuxili.FLA_PROFESSOR == "S")
            {
                ddlTipoColaborador.Items.Clear();
                ddlTipoColaborador.Items.Insert(0, new ListItem("Professor", "S"));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string flaCol = ddlTipoColaborador.SelectedValue != "T" ? ddlTipoColaborador.SelectedValue : "";
            string situ = ddlSituacaoColab.SelectedValue;

            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                var lst = from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                          where (situ != "0" ? tb03.CO_SITU_COL == situ : 0 == 0)
                          select new
                          {
                              Nome = tb03.NO_COL + ((flaCol == "")
                                ? " - " + ((tb03.FLA_PROFESSOR == "N") ? "Funcionário" : "Professor") : ""),
                              tb03.CO_COL,
                              tb03.FLA_PROFESSOR
                          };


                if (flaCol != string.Empty)
                    ddlFuncionarios.DataSource = lst.Where(x => x.FLA_PROFESSOR == flaCol).OrderBy(c => c.Nome);
                else
                    ddlFuncionarios.DataSource = lst.OrderBy(c => c.Nome);

                ddlFuncionarios.DataTextField = "Nome";
                ddlFuncionarios.DataValueField = "CO_COL";
                ddlFuncionarios.DataBind();
                ddlFuncionarios.Items.Insert(0, new ListItem("Selecione", ""));

                ddlFuncionarios.Enabled = ddlFuncionarios.Items.Count > 0;
            }
            else
            {
                var lst = from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                          where tb03.CO_COL == LoginAuxili.CO_COL
                          && (situ != "0" ? tb03.CO_SITU_COL == situ : 0 == 0)
                          select new
                          {
                              Nome = tb03.NO_COL + ((flaCol == "")
                                ? " - " + ((tb03.FLA_PROFESSOR == "N") ? "Funcionário" : "Professor") : ""),
                              tb03.CO_COL,
                              tb03.FLA_PROFESSOR
                          };


                if (flaCol != string.Empty)
                    ddlFuncionarios.DataSource = lst.Where(x => x.FLA_PROFESSOR == flaCol).OrderBy(c => c.Nome);
                else
                    ddlFuncionarios.DataSource = lst.OrderBy(c => c.Nome);

                ddlFuncionarios.DataTextField = "Nome";
                ddlFuncionarios.DataValueField = "CO_COL";
                ddlFuncionarios.DataBind();
                //ddlFuncionarios.Items.Insert(0, new ListItem("Selecione", ""));

                ddlFuncionarios.Enabled = ddlFuncionarios.Items.Count > 0;
            }
        }

        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }

        protected void ddlTipoColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();

        }

        protected void ddlSituacaoColab_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncionarios();
        }
    }
}