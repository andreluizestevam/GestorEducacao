//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: NEGOCIAÇÃO DE DÉBITOS (CONTAS A RECEBER)
// OBJETIVO: NEGOCIAÇÃO DE TÍTULOS DE RECEITAS/MENSALIDADES
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
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5600_CtrlNegociacoesDebitos.F5601_NegociacaoTituloReceita
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            CarregaUnidades();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EMP", "ID_CLIENTE_DOC", "TP_CLIENTE_DOC" };

            BoundField bf6 = new BoundField();
            bf6.DataField = "NO_IDENTIF";
            bf6.HeaderText = "Nome Cedente";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf6);

            BoundField bf9 = new BoundField();
            bf9.DataField = "DES_TP_CLIENTE";
            bf9.HeaderText = "Tipo";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf9);

            BoundField bf8 = new BoundField();
            bf8.DataField = "CEDENTE";
            bf8.HeaderText = "Cedente";
            bf8.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf8);
        }
        
        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coNomeFonte = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;
            int coUnidContr = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;

            if (ddlTipoFonte.SelectedValue == "A")
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                join tb07 in TB07_ALUNO.RetornaPelaUnid(LoginAuxili.CO_EMP) on tb47.CO_ALU equals tb07.CO_ALU
                                join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                                where tb47.IC_SIT_DOC == "A" && tb47.TP_CLIENTE_DOC == "A" && tb47.CO_ALU == coNomeFonte && tb47.CO_EMP == coEmp
                                && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                select new
                                {
                                    tb47.CO_EMP, NO_IDENTIF = tb108.NO_RESP, CEDENTE = tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, "."),
                                    tb47.TP_CLIENTE_DOC, DES_TP_CLIENTE = "Responsável", ID_CLIENTE_DOC = tb108.CO_RESP,
                                    tb47.CO_EMP_UNID_CONT
                                }).Distinct().OrderByDescending( c => c.NO_IDENTIF );

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            else if (ddlTipoFonte.SelectedValue == "O")
            {
                var resultado = (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                                join tb103 in TB103_CLIENTE.RetornaTodosRegistros() on tb47.CO_CLIENTE equals tb103.CO_CLIENTE
                                where tb47.IC_SIT_DOC == "A" && tb47.TP_CLIENTE_DOC == "O" && tb47.CO_CLIENTE == coNomeFonte
                                && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                select new
                                {
                                    tb47.CO_EMP, NO_IDENTIF = tb103.NO_FAN_CLI, ID_CLIENTE_DOC = tb103.CO_CLIENTE, tb47.TP_CLIENTE_DOC,
                                    DES_TP_CLIENTE = tb103.TP_CLIENTE == "F" ? "Pessoa Física" : "Pessoa Jurídica",
                                    CEDENTE = tb103.CO_CPFCGC_CLI.Length == 14 ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI.Length == 11 ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb103.CO_CPFCGC_CLI,
                                    tb47.CO_EMP_UNID_CONT
                                }).Distinct().OrderByDescending( c => c.NO_IDENTIF );

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            else
            {
                var resultado = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                                join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb47.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                                where tb47.IC_SIT_DOC == "A" && tb47.TP_CLIENTE_DOC == "A" && (coEmp != 0 ? tb47.CO_EMP == coEmp : coEmp == 0) 
                                && tb108.CO_RESP == coNomeFonte
                                && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                select new
                                {
                                    tb47.CO_EMP, NO_IDENTIF = tb07.NO_ALU, TP_CLIENTE_DOC = "A", DES_TP_CLIENTE = "Aluno", ID_CLIENTE_DOC = tb07.CO_ALU,
                                    CEDENTE = tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, "."),
                                    tb47.CO_EMP_UNID_CONT
                                }).Distinct().OrderByDescending( c => c.NO_IDENTIF );

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();

            queryStringKeys.Add(new KeyValuePair<string, string>("tpCliente", "TP_CLIENTE_DOC"));

            if (ddlTipoFonte.SelectedValue == "A")
            {
                queryStringKeys.Add(new KeyValuePair<string, string>("idCliente", ddlNomeFonte.SelectedValue));
                queryStringKeys.Add(new KeyValuePair<string, string>("coResp", "ID_CLIENTE_DOC"));
            }
            else
            {
                queryStringKeys.Add(new KeyValuePair<string, string>("idCliente", "ID_CLIENTE_DOC"));
                queryStringKeys.Add(new KeyValuePair<string, string>("coResp", ddlNomeFonte.SelectedValue));
            }

            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, ddlUnidade.SelectedValue));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento

//====> Método que carrega o DropDown de Unidades Escolares
        protected void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.Items.Insert(0, new ListItem("Todas", ""));

            if (ddlTipoFonte.SelectedValue == "R")
                CarregaResponsaveis();
            else if (ddlTipoFonte.SelectedValue == "A")
                CarregaAluno();
            else
                CarregaFontesReceita();
        }

//====> Método que carrega o DropDown de Alunos
        private void CarregaAluno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlNomeFonte.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                       select new { tb07.CO_ALU, tb07.NO_ALU });

            ddlNomeFonte.DataTextField = "NO_ALU";
            ddlNomeFonte.DataValueField = "CO_ALU";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que carrega o DropDown de Nome da Fonte com os Responsáveis
        private void CarregaResponsaveis()
        {
            ddlNomeFonte.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                       where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                       select new { tb108.CO_RESP, tb108.NO_RESP });

            ddlNomeFonte.DataTextField = "NO_RESP";
            ddlNomeFonte.DataValueField = "CO_RESP";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que carrega o DropDown de Nome da Fonte com os Clientes
        private void CarregaFontesReceita()
        {
            ddlNomeFonte.DataSource = (from tb103 in TB103_CLIENTE.RetornaTodosRegistros()
                                       select new { tb103.CO_CLIENTE, tb103.NO_FAN_CLI }).OrderBy( c => c.NO_FAN_CLI );

            ddlNomeFonte.DataTextField = "NO_FAN_CLI";
            ddlNomeFonte.DataValueField = "CO_CLIENTE";
            ddlNomeFonte.DataBind();

            ddlNomeFonte.Items.Insert(0, new ListItem("Selecione", ""));
        }

//====> Método que preenche informação de acordo com o Nome da Fonte selecionada
        private void CarregaCodigoFonte(string strTipoFonte)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coNomeFonte = ddlNomeFonte.SelectedValue != "" ? int.Parse(ddlNomeFonte.SelectedValue) : 0;

            txtCodigoFonte.Text = "";

            if (coNomeFonte == 0) 
                return;

            if (strTipoFonte == "A")
            {
                if (coEmp == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Deve ser informada a Unidade/Escola");
                    return;
                }
                else
                    txtCodigoFonte.Text = (coNomeFonte).ToString();
            }
            else if (strTipoFonte == "O")
            {
                TB103_CLIENTE tb103 = TB103_CLIENTE.RetornaPelaChavePrimaria(coNomeFonte);
                txtCodigoFonte.Text = tb103.TP_CLIENTE == "F" && tb103.CO_CPFCGC_CLI.Length == 11 ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                    (tb103.TP_CLIENTE == "J" && tb103.CO_CPFCGC_CLI.Length == 14 ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI);
            }
            else
            {
                TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coNomeFonte);
                txtCodigoFonte.Text = tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".");
            }
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFonte.SelectedValue == "A")
            {
                CarregaAluno();
            }            
        }

        protected void ddlTipoFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFonte.SelectedValue == "R")
            {
                CarregaResponsaveis();
                CarregaCodigoFonte("R");
            }
            else if (ddlTipoFonte.SelectedValue == "A")
            {
                CarregaAluno();
                CarregaCodigoFonte("A");
            }
            else
            {
                CarregaFontesReceita();
                CarregaCodigoFonte("O");
            }  
        }

        protected void ddlNomeFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCodigoFonte(ddlTipoFonte.SelectedValue);
        }      
    }
}