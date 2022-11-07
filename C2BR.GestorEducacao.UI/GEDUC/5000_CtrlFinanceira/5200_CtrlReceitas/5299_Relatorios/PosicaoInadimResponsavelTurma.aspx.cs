﻿//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: POSIÇÃO DE INADIMPLÊNCIA DE TÍTULOS DE RECEITAS DE ALUNOS 
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
    public partial class PosicaoInadimResponsavelTurma : System.Web.UI.Page
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
                CarregaAgrupadores();
            }
        }

        /// <summary>
        /// Método que faz a chamada de outro método de acordo com o Tipo
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            if (ddlResponsavel.SelectedValue == "" && ddlTpInadimplencia.SelectedValue == "F")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione o responsável.");
                return;
            }
            if (ddlTpInadimplencia.SelectedValue.ToString() == "F")
                RelInadimplencia();
            else if (ddlTpInadimplencia.SelectedValue.ToString() == "S")
                RelInadimplenciaSerTur();
            else if (ddlTpInadimplencia.SelectedValue.ToString() == "RD")
                RelInadimplenciaResponDoc();
            else
                RelInadimplenciaPorResp();
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        private void RelInadimplenciaPorResp()
        {
            ///Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            DateTime strDt_Ini, strDt_Fim;
            int lRetorno;

            ///Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_AGRUP;

            ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP_REF = LoginAuxili.CO_EMP;
            strP_CO_EMP = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);

            strDt_Ini = DateTime.Parse(txtPeriodoDe.Text);
            strDt_Fim = DateTime.Parse(txtPeriodoAte.Text);

            if (strDt_Fim < strDt_Ini)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A data de FIM do período não pode ser menor que a data de INICIO.");
                return;
            }

            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Unidade de Contrato: " + ddlUnidadeContrato.SelectedItem.ToString() 
                + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " - Período: "  + strDt_Ini.ToString("dd/MM/yyyy") + " á " + strDt_Fim.ToString("dd/MM/yyyy") +  " )";

            RptPosicInadimPorRespon fpcb = new RptPosicInadimPorRespon();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_AGRUP, strINFOS, strDt_Ini, strDt_Fim);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        private void RelInadimplencia()
        {
            ///Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            strParametrosRelatorio = "";

            DateTime strDt_Ini, strDt_Fim;

            ///Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_RESP, strP_CO_AGRUP;

            ///Inicializa as variáveis
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
            strP_CO_RESP = int.Parse(ddlResponsavel.SelectedValue);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            strDt_Ini = DateTime.Parse(txtPeriodoDe.Text);
            strDt_Fim = DateTime.Parse(txtPeriodoAte.Text);

            if (strDt_Fim < strDt_Ini)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A data de FIM do período não pode ser menor que a data de INICIO.");
                return;
            }

            var tb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                        where iTb108.CO_RESP == strP_CO_RESP
                        select new
                        {
                            iTb108.NO_RESP,iTb108.NU_CPF_RESP, iTb108.NU_TELE_CELU_RESP, iTb108.NU_TELE_RESI_RESP
                        }).FirstOrDefault();

            if (tb108 != null)
            {
                strParametrosRelatorio = "Responsável: " + tb108.NO_RESP + " ( CPF: " + (tb108.NU_CPF_RESP.Length == 11 ? tb108.NU_CPF_RESP : "00000000000").Insert(3, ".").Insert(7, ".").Insert(11, "-");

                if (!String.IsNullOrEmpty(tb108.NU_TELE_CELU_RESP) || !String.IsNullOrEmpty(tb108.NU_TELE_RESI_RESP))
                {
                    strParametrosRelatorio = strParametrosRelatorio + " - Telefone(s): ";

                    if (!String.IsNullOrEmpty(tb108.NU_TELE_CELU_RESP))
                    {
                        strParametrosRelatorio = strParametrosRelatorio + tb108.NU_TELE_CELU_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " C ";
                    }

                    if (!String.IsNullOrEmpty(tb108.NU_TELE_RESI_RESP))
                    {
                        strParametrosRelatorio = strParametrosRelatorio + tb108.NU_TELE_RESI_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " R";
                    }
                }
            }

            strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Unidade de Contrato: " + ddlUnidadeContrato.SelectedItem.ToString()
                + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " - Período: " + strDt_Ini.ToString("dd/MM/yyyy") + " á " + strDt_Fim.ToString("dd/MM/yyyy") + " )";

            RptPosicInadimFichaRespon fpcb = new RptPosicInadimFichaRespon();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_RESP, strP_CO_AGRUP, strINFOS, strDt_Ini, strDt_Fim);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        private void RelInadimplenciaResponDoc()
        {
            ///Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            strParametrosRelatorio = "";

            DateTime strDt_Ini, strDt_Fim;

            ///Variáveis de parâmetro do Relatório
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_RESP, strP_CO_AGRUP;

            ///Inicializa as variáveis
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
            strP_CO_RESP = int.Parse(ddlResponsavel.SelectedValue);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            strDt_Ini = DateTime.Parse(txtPeriodoDe.Text);
            strDt_Fim = DateTime.Parse(txtPeriodoAte.Text);

            if (strDt_Fim < strDt_Ini)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A data de FIM do período não pode ser menor que a data de INICIO.");
                return;
            }

            var tb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                         where iTb108.CO_RESP == strP_CO_RESP
                         select new
                         {
                             iTb108.NO_RESP,
                             iTb108.NU_CPF_RESP,
                             iTb108.NU_TELE_CELU_RESP,
                             iTb108.NU_TELE_RESI_RESP
                         }).FirstOrDefault();

            if (tb108 != null)
            {
                strParametrosRelatorio = "Responsável: " + tb108.NO_RESP + " ( CPF: " + tb108.NU_CPF_RESP.Insert(3, ".").Insert(7, ".").Insert(11, "-");

                if (!String.IsNullOrEmpty(tb108.NU_TELE_CELU_RESP) || !String.IsNullOrEmpty(tb108.NU_TELE_RESI_RESP))
                {
                    strParametrosRelatorio = strParametrosRelatorio + " - Telefone(s): ";

                    if (!String.IsNullOrEmpty(tb108.NU_TELE_CELU_RESP))
                    {
                        strParametrosRelatorio = strParametrosRelatorio + tb108.NU_TELE_CELU_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " C ";
                    }

                    if (!String.IsNullOrEmpty(tb108.NU_TELE_RESI_RESP))
                    {
                        strParametrosRelatorio = strParametrosRelatorio + tb108.NU_TELE_RESI_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " R";
                    }
                }
            }

            strParametrosRelatorio = "( Unidade: " + ddlUnidade.SelectedItem.ToString() + " - Unidade de Contrato: " + ddlUnidadeContrato.SelectedItem.ToString()
                + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " - Período: " + strDt_Ini.ToString("dd/MM/yyyy") + " á " + strDt_Fim.ToString("dd/MM/yyyy") + " )";

            RptPosicInadimFichaResponDoc fpcb = new RptPosicInadimFichaResponDoc();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_RESP, strP_CO_AGRUP, strINFOS, strDt_Ini, strDt_Fim);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        private void RelInadimplenciaSerTur()
        {

            ///Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            ///Variáveis de parâmetro do Relatório
            DateTime strDt_Ini, strDt_Fim;
            int strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_AGRUP;

            ///Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = LoginAuxili.CO_EMP;
            strP_CO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strP_CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
            strP_CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
            strP_CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strP_CO_TUR = int.Parse(ddlTurma.SelectedValue);
            strP_CO_AGRUP = int.Parse(ddlAgrupador.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            strDt_Ini = DateTime.Parse(txtPeriodoDe.Text);
            strDt_Fim = DateTime.Parse(txtPeriodoAte.Text);

            if (strDt_Fim < strDt_Ini)
            {
                AuxiliPagina.EnvioMensagemErro(this, "A data de FIM do período não pode ser menor que a data de INICIO.");
                return;
            }

            strParametrosRelatorio = "(  Modalidade: " + ddlModalidade.SelectedItem.ToString() + " - Curso: " + ddlSerieCurso.SelectedItem.ToString()
            + " -Turma: " + ddlTurma.SelectedItem.ToString() + " - Período: " + txtPeriodoDe.Text + " à " + txtPeriodoAte.Text + " - Agrupador: " + ddlAgrupador.SelectedItem.ToString() + " )";

            RptPosicInadimPorSerieTurma fpcb = new RptPosicInadimPorSerieTurma();
            lRetorno = fpcb.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_EMP_UNID_CONT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_AGRUP, strINFOS, strDt_Ini, strDt_Fim);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }                      

        #endregion

        #region Métodos

        /// <summary>
        /// Método que controla a visibilidade dos campos
        /// </summary>
        private void ControlaVisibilidade()
        {
            if (ddlTpInadimplencia.SelectedValue == "F")
            {
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = false;
                liResponsavel.Visible = ddlResponsavel.Visible = true;

                ddlResponsavel.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                             where tb108.NO_RESP != null && tb108.NO_RESP != ""
                                             select new { tb108.CO_RESP, tb108.NO_RESP });

                ddlResponsavel.DataTextField = "NO_RESP";
                ddlResponsavel.DataValueField = "CO_RESP";
                ddlResponsavel.DataBind();
                ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
                ddlResponsavel.SelectedValue = "0";
            }
            else if (ddlTpInadimplencia.SelectedValue == "RD")
            {
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = false;
                liResponsavel.Visible = ddlResponsavel.Visible = true;

                ddlResponsavel.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                             where tb108.NO_RESP != null && tb108.NO_RESP != ""
                                             select new { tb108.CO_RESP, tb108.NO_RESP });

                ddlResponsavel.DataTextField = "NO_RESP";
                ddlResponsavel.DataValueField = "CO_RESP";
                ddlResponsavel.DataBind();
                ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
                ddlResponsavel.SelectedValue = "0";
            }
            else if (ddlTpInadimplencia.SelectedValue == "S")
            {
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = true;
                liResponsavel.Visible = ddlResponsavel.Visible = false;

                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
            }
            else
                liModalidade.Visible = liSerie.Visible = liTurma.Visible = liResponsavel.Visible = ddlResponsavel.Visible = false;
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO));
            if (ddlUnidade.Items.FindByValue(LoginAuxili.CO_EMP.ToString()) != null)
                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUnidadeContrato.Items.Clear();
            ddlUnidadeContrato.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO, todos:true));
            ddlUnidadeContrato.SelectedValue = "-1";

            ControlaVisibilidade();
        }  

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int coEmp = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int coCur = (!string.IsNullOrEmpty(ddlSerieCurso.SelectedValue) ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            int coModu = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, coModu, coCur, true);
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
            int coEmp = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int coModu = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, coModu, coEmp, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Agrupadores
        /// </summary>
        private void CarregaAgrupadores()
        {
            ddlAgrupador.Items.Clear();
            ddlAgrupador.Items.AddRange(AuxiliBaseApoio.AgrupadoresDDL("R", todos:true));
            ddlAgrupador.SelectedValue = "-1";
        }
        #endregion

        #region Eventos de componentes da pagina

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlTpInadimplencia.SelectedValue == "S")
            {
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
            }
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
        }

        protected void ddlTpInadimplencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaVisibilidade();
        }

        #endregion
    }
}
