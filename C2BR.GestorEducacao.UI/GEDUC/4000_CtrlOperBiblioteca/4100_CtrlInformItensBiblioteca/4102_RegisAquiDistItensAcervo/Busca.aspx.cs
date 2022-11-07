//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: DADOS CADASTRAIS DE ACERVO BIBLIOGRÁFICO
// OBJETIVO: REGISTRO DE AQUISIÇÃO E DISBRUIÇÃO DE ITENS DE ACERVO BIBLIOGRÁFICOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4102_RegisAquiDistItensAcervo
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

        protected void Page_Load()
        {
            if (IsPostBack) return;

            CarregaUnidade();
            CarregaFornecedores();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ACERVO_AQUISI" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FAN_FOR",
                HeaderText = "Fornecedor"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_ACERVO_AQUISI",
                HeaderText = "Tipo Aquisição"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FANTAS_EMP",
                HeaderText = "Empresa"
            });

            BoundField bf8 = new BoundField();
            bf8.DataField = "ACERVO_AQUISI";
            bf8.HeaderText = "Nº Aquisição";
            bf8.ItemStyle.CssClass = "direita";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf8);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coForn = ddlFornecedor.SelectedValue != "" ? int.Parse(ddlFornecedor.SelectedValue) : 0;
            string tpAcerAquisi = ddlTipo.SelectedValue != "" ? ddlTipo.SelectedValue : "";
            
            var resultado = (from tb203 in TB203_ACERVO_AQUISICAO.RetornaTodosRegistros()
                            where tb203.TB25_EMPRESA.CO_EMP == coEmp && tb203.TB41_FORNEC.CO_FORN == coForn
                            && (tpAcerAquisi != "" ? tb203.TP_ACERVO_AQUISI == tpAcerAquisi : tpAcerAquisi == "")
                            select new
                            {
                                tb203.TB41_FORNEC.NO_FAN_FOR, tb203.CO_ACERVO_AQUISI, tb203.DT_CADASTRO, tb203.TB25_EMPRESA.NO_FANTAS_EMP, ACERVO_AQUISI = tb203.CO_ACERVO_AQUISI,
                                TP_ACERVO_AQUISI = ((tb203.TP_ACERVO_AQUISI == "C") ? "Compra" : ((tb203.TP_ACERVO_AQUISI == "D") ? "Doação" :
                                ((tb203.TP_ACERVO_AQUISI == "T") ? "Transferência" : ((tb203.TP_ACERVO_AQUISI == "O") ? "Outros" : ""))))                                   
                            }).OrderBy( a => a.NO_FAN_FOR ).ThenBy( a => a.CO_ACERVO_AQUISI );
            
            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;           
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_ACERVO_AQUISI"));

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
        }

//====> Método que carrega o DropDown de Fornecedores
        private void CarregaFornecedores()
        {
            ddlFornecedor.DataSource = (from tb41 in TB41_FORNEC.RetornaTodosRegistros()
                                        where tb41.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                        select new { tb41.NO_FAN_FOR, tb41.CO_FORN }).OrderBy( f => f.NO_FAN_FOR );

            ddlFornecedor.DataTextField = "NO_FAN_FOR";
            ddlFornecedor.DataValueField = "CO_FORN";
            ddlFornecedor.DataBind();

            ddlFornecedor.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion
    }
}
