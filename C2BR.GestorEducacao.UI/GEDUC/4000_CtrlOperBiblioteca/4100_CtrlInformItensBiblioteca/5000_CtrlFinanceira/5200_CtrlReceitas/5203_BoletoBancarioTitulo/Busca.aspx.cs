//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: BOLETO BANCÁRIO DE TÍTULOS DE RECEITAS/RECURSOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5203_BoletoBancarioTitulo
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
            if (!Page.IsPostBack)
                CarregaUnidade();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CODIGO", "TIPO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOME",
                HeaderText = "Nome do Aluno"
            });

            BoundField bf1 = new BoundField();
            bf1.DataField = "CPFCNPJ";
            bf1.HeaderText = "Código";
            bf1.ItemStyle.CssClass = "colunaNumerica";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TELEFONE",
                HeaderText = "Telefone"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "UF",
                HeaderText = "UF"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CIDADE",
                HeaderText = "Cidade"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strTpCliente = ddlTipo.SelectedValue;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coUnidContr = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;

            if (strTpCliente == "A")
            {
                var resultado = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros() 
                                 join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                                where (txtNome.Text != "" ? tb07.NO_ALU.Contains(txtNome.Text) : txtNome.Text == "")
                                && tb47.CO_EMP == coEmp
                                && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") && (tb47.TP_CLIENTE_DOC == "A" || tb47.TP_CLIENTE_DOC == "R")
                                select new
                                {
                                    tb07.TB25_EMPRESA1.CO_EMP, TIPO = "A", CODIGO = tb07.CO_ALU, NOME = tb07.NO_ALU, UF = tb07.CO_ESTA_ALU, tb07.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                    CPFCNPJ = tb07.NU_NIRE,
                                    TELEFONE = tb07.NU_TELE_RESI_ALU.Length >= 10 ? tb07.NU_TELE_RESI_ALU.Insert(6, "-").Insert(2, ") ").Insert(0, "(") : tb07.NU_TELE_RESI_ALU
                                }).Distinct().OrderBy(c => c.NOME);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            else
            {
                var resultado = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb103 in TB103_CLIENTE.RetornaTodosRegistros() on tb47.TB103_CLIENTE.CO_CLIENTE equals tb103.CO_CLIENTE
                                where (txtNome.Text != "" ? tb47.TB103_CLIENTE.NO_FAN_CLI.Contains(txtNome.Text) : txtNome.Text == "")
                                && tb47.CO_EMP == coEmp
                                && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                                && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") && (tb47.TP_CLIENTE_DOC == "O" || tb47.TP_CLIENTE_DOC == "R")
                                select new
                                {
                                    CO_EMP = coEmp,
                                    TIPO = "O",
                                    CODIGO = tb103.CO_CLIENTE,
                                    NOME = tb103.NO_FAN_CLI,
                                    UF = tb103.CO_UF_CLI,
                                    NO_CIDADE = tb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                    CPFCNPJ = (tb103.TP_CLIENTE == "F" && tb103.CO_CPFCGC_CLI.Length >= 11) ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
                                        ((tb103.TP_CLIENTE == "J" && tb103.CO_CPFCGC_CLI.Length >= 14) ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI),
                                    TELEFONE = tb103.CO_TEL1_CLI.Length >= 10 ? tb103.CO_TEL1_CLI.Insert(6, "-").Insert(2, ") ").Insert(0, "(") : tb103.CO_TEL1_CLI
                                }).OrderBy(c => c.NOME).Distinct();

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, ddlUnidade.SelectedValue));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CODIGO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("tp", "TIPO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
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
        }
        #endregion
    }
}