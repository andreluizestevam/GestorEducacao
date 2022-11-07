//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: CONTROLE DE MENSAGENS SMS
// OBJETIVO: EMISSÃO DA RELAÇÃO DE MENSAGENS SMS
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
using C2BR.GestorEducacao.Reports._0000_ConfiguracaoAmbiente._0300_GerenciamentoUsuarios;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0399_Relatorios
{
    public partial class RelacMensagensSMS : System.Web.UI.Page
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
                CarregaDropDown();
                CarregaColaborador();
                CarregaDestinatario();
            }
        }

        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            string parametros, infos;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int coUnid = 0;
            parametros = "( Unidade : " + ddlUnidade.SelectedItem.Text + " - Colaborador : " + ddlColaborador.SelectedItem.Text + " - Tipo Contato : " + ddlTpContato.SelectedItem.Text + " - Nome Destinatario  : " + ddlNomeDestinatario.SelectedItem.Text + " - Status : " + ddlStatus.SelectedItem.Text + " - Perìodo : " + txtDataInicial.Text + " a " + txtDataFinal.Text + ")";

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            coUnid = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            int CodNomeColaborador = (ddlColaborador.SelectedValue != "" ? int.Parse(ddlColaborador.SelectedValue) : 0);
            string CodTipoContato = ddlTpContato.SelectedValue;
            int CodDestinatario = (ddlNomeDestinatario.SelectedValue != "" ? int.Parse(ddlNomeDestinatario.SelectedValue) : 0);
            //string Status = (ddlStatus.SelectedValue != "" ? int.Parse(ddlStatus.SelectedValue) : 0);
            string Status = ddlStatus.SelectedValue;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region

            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GEDUC/0000_ConfiguracaoAmbiente/0300_GerenciamentoUsuarios/0399_Relatorios/RelacMensagensSMS.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptRelacMensagensSMS rpt = new RptRelacMensagensSMS();

            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coUnid, CodNomeColaborador, Status, CodTipoContato, CodDestinatario, txtOutroDestinatario.Text, txtDataInicial.Text, txtDataFinal.Text, NO_RELATORIO.ToUpper());

            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamento DropDown

        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        public void CarregaColaborador()
        {
            int coUnid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaMedicos(ddlColaborador, coUnid, true, true);

        }

        public void CarregaDestinatario()
        {
            if (ddlTpContato.SelectedValue != "0")
            {
                string TipoContato = ddlTpContato.SelectedValue;
                string s = "";
                switch (TipoContato)
                {
                    case "A":
                        txtOutroDestinatario.Visible = false;
                        ddlNomeDestinatario.Visible = true;
                        var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   select new
                                   {
                                       tb07.NO_ALU,
                                       tb07.CO_ALU,
                                   }).OrderBy(w => w.NO_ALU).ToList();

                        ddlNomeDestinatario.DataTextField = "NO_ALU";
                        ddlNomeDestinatario.DataValueField = "CO_ALU";
                        ddlNomeDestinatario.DataSource = res;
                        ddlNomeDestinatario.DataBind();
                        ddlNomeDestinatario.Items.Insert(0, new ListItem("Todos", "0"));
                        break;
                    case "R":
                        txtOutroDestinatario.Visible = false;
                        ddlNomeDestinatario.Visible = true;
                        var resultadoResponsavel = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                                    select new
                                                    {
                                                        tb108.NO_RESP,
                                                        tb108.CO_RESP,
                                                    }).OrderBy(w => w.NO_RESP).ToList();

                        ddlNomeDestinatario.DataTextField = "NO_RESP";
                        ddlNomeDestinatario.DataValueField = "CO_RESP";
                        ddlNomeDestinatario.DataSource = resultadoResponsavel;
                        ddlNomeDestinatario.DataBind();
                        ddlNomeDestinatario.Items.Insert(0, new ListItem("Todos", "0"));
                        break;
                    case "F":
                        txtOutroDestinatario.Visible = false;
                        ddlNomeDestinatario.Visible = true;
                        var resultadoFuncionario = (from tb108 in TB03_COLABOR.RetornaTodosRegistros().Where(q => q.FLA_PROFESSOR != "S")
                                                    select new
                                                    {
                                                        tb108.NO_COL,
                                                        tb108.CO_FUN,
                                                    }).OrderBy(w => w.NO_COL).ToList();

                        ddlNomeDestinatario.DataTextField = "NO_COL";
                        ddlNomeDestinatario.DataValueField = "CO_FUN";
                        ddlNomeDestinatario.DataSource = resultadoFuncionario;
                        ddlNomeDestinatario.DataBind();
                        ddlNomeDestinatario.Items.Insert(0, new ListItem("Todos", "0"));


                        break;
                    case "P":
                        txtOutroDestinatario.Visible = false;
                        ddlNomeDestinatario.Visible = true;
                        var resultadoProfessor = (from tb108 in TB03_COLABOR.RetornaTodosRegistros().Where(q => q.FLA_PROFESSOR == "S")
                                                  select new
                                                  {
                                                      tb108.NO_COL,
                                                      tb108.CO_FUN,
                                                  }).OrderBy(w => w.NO_COL).ToList();

                        ddlNomeDestinatario.DataTextField = "NO_COL";
                        ddlNomeDestinatario.DataValueField = "CO_FUN";
                        ddlNomeDestinatario.DataSource = resultadoProfessor;
                        ddlNomeDestinatario.DataBind();
                        ddlNomeDestinatario.Items.Insert(0, new ListItem("Todos", "0"));

                        break;
                    case "O":
                        txtOutroDestinatario.Visible = true;
                        ddlNomeDestinatario.Visible = false;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ddlNomeDestinatario.Items.Insert(0, new ListItem("Todos", "0"));
            }

        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaDropDown()
        {
            CarregaUnidades();
        }

        #endregion

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            CarregaColaborador();
        }

        protected void ddlTpContato_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            CarregaDestinatario();
        }
    }
}
