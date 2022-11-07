//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE FREQUÊNCIA
// OBJETIVO: Emissão de demonstrativo financeiro de plantões
// DATA DE CRIAÇÃO: 06/10/2014
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
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios
{
    public partial class DemonConsultMedic : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

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
                CarregaUnidade(ddlUnidade);
                CarregaEspecialidades();
                CarregaDepartament();
            }
        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis de parâmetro do Relatório
            string infos, parametros;
            int lRetorno, coUnidConsul, coEspecConsul, coDepto, coOrdenacao;
            string noUnidadeConsul, noEspecialidade, noPeriodo, noOrdenacao;
            //--------> Inserção de valores nas variáveis
            coUnidConsul = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            coEspecConsul = ddlEspecPlant.SelectedValue != "" ? int.Parse(ddlEspecPlant.SelectedValue) : 0;
            coDepto = ddlLocal.SelectedValue != "" ? int.Parse(ddlLocal.SelectedValue) : 0;
            coOrdenacao = int.Parse(ddlOrdenadoPor.SelectedValue);
            bool VerValor = CheckBoxVerValor.Checked;
            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            noUnidadeConsul = (coUnidConsul != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coUnidConsul).sigla : "Todos");
            noEspecialidade = (coEspecConsul != 0 ? TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(o => o.CO_ESPECIALIDADE == coEspecConsul).FirstOrDefault().NO_ESPECIALIDADE : "Todos");
            noOrdenacao = ddlOrdenadoPor.SelectedItem.Text;

            DateTime dtini = DateTime.Parse(txtIniPeri.Text);
            DateTime dtfim = DateTime.Parse(txtFimPeri.Text);

            parametros = "( Unid: " + noUnidadeConsul.ToUpper() + " - Especialidade: " + noEspecialidade.ToUpper()
                + " - Classificado por " + ddlClassif.SelectedItem.Text + " - Relatório ordenado por " + noOrdenacao
                + " - " + ddlTipoOrdem.SelectedItem.Text + " )";

            noPeriodo = " - Período: " + dtini.ToString("dd/MM/yy") + " à " + dtfim.ToString("dd/MM/yy");
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            #region

            string NO_RELATORIO = "";
            NO_RELATORIO = AuxiliGeral.RetornaNomeFuncionalidadeCadastrada("GSAUD/8000_GestaoAtendimento/8100_CtrlAgendaMedica/8199_Relatorios/DemonConsultMedic.aspx");

            #endregion

            RptDemonConsulMedic rpt = new RptDemonConsulMedic();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, noPeriodo, coUnidConsul, coDepto, coEspecConsul, ddlClassif.SelectedValue, int.Parse(ddlOrdenadoPor.SelectedValue), ddlTipoOrdem.SelectedValue, txtIniPeri.Text, txtFimPeri.Text, (chkGraficos.Checked ? true : false), (chkRelatorio.Checked ? true : false), VerValor, NO_RELATORIO.ToUpper());
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidade(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os Departamentos
        /// </summary>
        private void CarregaDepartament()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocal, coEmp, true);
        }

        /// <summary>
        /// Carrega as Especiliadades
        /// </summary>
        private void CarregaEspecialidades()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecPlant, coEmp, null, true);
        }

        protected void ddlClassif_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlClassif.SelectedValue)
            {
                case "U":
                    ddlOrdenadoPor.Items.Clear();
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MAP - Média Atendimento por Profissional", "15"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MDE - Média Atendimento por Especialidade", "14"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MAD - Média Atendimento Diário", "13"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCO - Qtde Outros", "12"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCV - Qtde Vacinas", "11"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCC - Qtde Cirugias", "10"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCE - Qtde Exames", "9"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCP - Qtde Procedimentos", "8"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCR - Qtde Consultas Retorno", "7"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCN - Qtde Consultas Normais", "6"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAM - Qtde Atendimentos Movimentados", "5"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAC - Qtde Atendimentos Cancelados", "4"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAR - Qtde Atendimentos Realizados", "3"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAP - Qtde Atendimentos Planejados", "2"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("Unidade", "1"));

                    break;
                case "P":
                    ddlOrdenadoPor.Items.Clear();
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MAP - Média Atendimento por Profissional", "15"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MDE - Média Atendimento por Especialidade", "14"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MAD - Média Atendimento Diário", "13"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCO - Qtde Outros", "12"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCV - Qtde Vacinas", "11"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCC - Qtde Cirugias", "10"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCE - Qtde Exames", "9"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCP - Qtde Procedimentos", "8"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCR - Qtde Consultas Retorno", "7"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCN - Qtde Consultas Normais", "6"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAM - Qtde Atendimentos Movimentados", "5"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAC - Qtde Atendimentos Cancelados", "4"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAR - Qtde Atendimentos Realizados", "3"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAP - Qtde Atendimentos Planejados", "2"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("Profisional", "1"));
                    break;
                case "E":
                    ddlOrdenadoPor.Items.Clear();
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MAP - Média Atendimento por Profissional", "15"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MDE - Média Atendimento por Especialidade", "14"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MAD - Média Atendimento Diário", "13"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCO - Qtde Outros", "12"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCV - Qtde Vacinas", "11"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCC - Qtde Cirugias", "10"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCE - Qtde Exames", "9"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCP - Qtde Procedimentos", "8"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCR - Qtde Consultas Retorno", "7"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCN - Qtde Consultas Normais", "6"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAM - Qtde Atendimentos Movimentados", "5"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAC - Qtde Atendimentos Cancelados", "4"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAR - Qtde Atendimentos Realizados", "3"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAP - Qtde Atendimentos Planejados", "2"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("Especialidade", "1"));
                    break;
                case "B":
                    ddlOrdenadoPor.Items.Clear();
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MAP - Média Atendimento por Profissional", "15"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MDE - Média Atendimento por Especialidade", "14"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("MAD - Média Atendimento Diário", "13"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCO - Qtde Outros", "12"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCV - Qtde Vacinas", "11"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCC - Qtde Cirugias", "10"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCE - Qtde Exames", "9"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCP - Qtde Procedimentos", "8"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCR - Qtde Consultas Retorno", "7"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QCN - Qtde Consultas Normais", "6"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAM - Qtde Atendimentos Movimentados", "5"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAC - Qtde Atendimentos Cancelados", "4"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAR - Qtde Atendimentos Realizados", "3"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("QAP - Qtde Atendimentos Planejados", "2"));
                    ddlOrdenadoPor.Items.Insert(0, new ListItem("Cidade/Bairro", "1"));
                    break;
            }
        }
    }
}
