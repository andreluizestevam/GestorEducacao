//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (FINANCEIRO)
// OBJETIVO: PLANO DE CONTAS CONTÁBIL
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 22/04/2013| André Nobre Vinagre        | Adicionado o subgrupo 2 de conta contábil no parametro e relatorio
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 29/04/2013| André Nobre Vinagre        | Retirada a mascara de grupo, subgrupo e subgrupo 2 para o cliente
//           |                            | colégio específico
//           |                            |

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
using C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1119_Relatorios
{
    public partial class RelacPlanoContas : System.Web.UI.Page
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
                CarregaGrupo();
                CarregaSubgrupo();
                CarregaSubGrupo2();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string strParametrosRelatorio, strINFOS;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string strP_CO_GRUP_CTA, strP_CO_SGRUP_CTA, strP_CO_SGRUP2_CTA, strP_TIPO, strP_CO_EMP;
            

            //--------> Inicializa as variáveis
            strP_CO_GRUP_CTA = null;
            strP_CO_SGRUP_CTA = null;
            strP_CO_SGRUP2_CTA = null;
            strP_TIPO = null;
            strINFOS = null;
            strP_CO_EMP = null;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_TIPO = ddlTipo.SelectedValue;
            strP_CO_GRUP_CTA = ddlGrupo.SelectedValue;
            strP_CO_SGRUP_CTA = ddlSubgrupo.SelectedValue;
            strP_CO_SGRUP2_CTA = ddlSubgrupo2.SelectedValue;
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
           
            strParametrosRelatorio = "Unidade: " + ddlUnidade.SelectedItem.ToString();

            RptRelacPlanoContas fpcb = new RptRelacPlanoContas();
            lRetorno = fpcb.InitReport(LoginAuxili.CO_EMP, strParametrosRelatorio, strP_CO_EMP, strP_TIPO,
                                          strP_CO_GRUP_CTA, strP_CO_SGRUP_CTA, strP_CO_SGRUP2_CTA, strINFOS);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion

        #region Carregamento DropDown

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

            ddlTipo.Items.Insert(0, new ListItem("Todas", "0"));
            ddlTipo.Items.Add(new ListItem("1 - Ativo", "A"));
            ddlTipo.Items.Add(new ListItem("2 - Passivo", "P"));
            ddlTipo.Items.Add(new ListItem("3 - Receita", "C"));
            ddlTipo.Items.Add(new ListItem("4 - Custo e Despesa", "D"));
            ddlTipo.Items.Add(new ListItem("5 - Investimento", "I"));
            ddlTipo.Items.Add(new ListItem("6 - Título", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grupo de Conta
        /// </summary>
        private void CarregaGrupo()
        {
            var result = (from tb53 in TB53_GRP_CTA.RetornaTodosRegistros()
                          where tb53.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                   && tb53.TP_GRUP_CTA == ddlTipo.SelectedValue
                          select new { tb53.DE_GRUP_CTA, tb53.CO_GRUP_CTA, tb53.NR_GRUP_CTA }).ToList();

            ddlGrupo.DataSource = (from res in result
                                  select new
                                  {
                                      DE_GRUP_CTA = res.NR_GRUP_CTA.ToString().PadLeft(2, '0') + " - " + res.DE_GRUP_CTA,
                                      res.CO_GRUP_CTA
                                  }).OrderBy(p => p.DE_GRUP_CTA);

            ddlGrupo.DataTextField = "DE_GRUP_CTA";
            ddlGrupo.DataValueField = "CO_GRUP_CTA";
            ddlGrupo.DataBind();

            ddlGrupo.Items.Insert(0, new ListItem("Todas", "T"));
        }

        /// <summary>
        /// Método que carrega o dropdown de SubGrupo de Conta
        /// </summary>
        private void CarregaSubgrupo()
        {
            int coGrupCta = ddlGrupo.SelectedValue != "T" ? int.Parse(ddlGrupo.SelectedValue) : 0;

            ddlSubgrupo.Items.Clear();

            if (coGrupCta != 0)
            {
                var result = (from tb54 in TB54_SGRP_CTA.RetornaTodosRegistros()
                              where tb54.CO_GRUP_CTA == coGrupCta
                              select new { tb54.DE_SGRUP_CTA, tb54.CO_SGRUP_CTA, tb54.NR_SGRUP_CTA }).ToList();

                ddlSubgrupo.DataSource = (from res in result
                                         select new
                                         {
                                             DE_SGRUP_CTA = res.NR_SGRUP_CTA.ToString().PadLeft(3, '0') + " - " + res.DE_SGRUP_CTA,
                                             res.CO_SGRUP_CTA
                                         }).OrderBy(p => p.DE_SGRUP_CTA);

                ddlSubgrupo.DataTextField = "DE_SGRUP_CTA";
                ddlSubgrupo.DataValueField = "CO_SGRUP_CTA";
                ddlSubgrupo.DataBind();
            }

            ddlSubgrupo.Items.Insert(0, new ListItem("Todas", "T"));
        }

        //====> Método que carrega o DropDown de SubGrupo2 de Contas
        private void CarregaSubGrupo2()
        {
            int coGrupCta = ddlGrupo.SelectedValue != "T" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSGrupCta = ddlSubgrupo.SelectedValue != "T" ? int.Parse(ddlSubgrupo.SelectedValue) : 0;

            ddlSubgrupo2.DataSource = (from tb055 in TB055_SGRP2_CTA.RetornaTodosRegistros()
                                       where tb055.TB54_SGRP_CTA.CO_GRUP_CTA == coGrupCta && tb055.TB54_SGRP_CTA.CO_SGRUP_CTA == coSGrupCta
                                       select new { tb055.DE_SGRUP2_CTA, tb055.CO_SGRUP2_CTA });

            ddlSubgrupo2.DataTextField = "DE_SGRUP2_CTA";
            ddlSubgrupo2.DataValueField = "CO_SGRUP2_CTA";
            ddlSubgrupo2.DataBind();

            ddlSubgrupo2.Items.Insert(0, new ListItem("Todas", "T"));
        }
        #endregion

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrupo();
            CarregaSubgrupo();
            CarregaSubGrupo2();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubgrupo();
        }

        protected void ddlSubgrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupo2();
        }
    }
}
