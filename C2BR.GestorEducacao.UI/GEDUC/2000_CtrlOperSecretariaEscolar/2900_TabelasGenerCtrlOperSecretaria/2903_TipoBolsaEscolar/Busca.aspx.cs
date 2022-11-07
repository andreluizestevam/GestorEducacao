//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: TABELAS DE APOIO OPERACIONAL SECRETARIA
// OBJETIVO: TIPOS DE BOLSA ESCOLAR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2903_TipoBolsaEscolar
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

            CarregaAgrupador();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_TIPO_BOLSA" };

            CurrentPadraoBuscas.GridBusca.PageSize = 18;

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TipoBolsa",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Grupo_Bolsa",
                HeaderText = "Agrupador"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPO_BOLSA",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FL_TIPO_VALOR_BOLSA",
                HeaderText = "TP Valor"
            });

            BoundField bf1 = new BoundField();
            bf1.DataField = "VL_TIPO_BOLSA";
            bf1.HeaderText = "Valor Descto";
            bf1.DataFormatString = "{0:N}";
            bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            BoundField bf3 = new BoundField();
            bf3.DataField = "DT_CADAS_TIPO_BOLSA";
            bf3.HeaderText = "Dt Cadas";
            bf3.DataFormatString = "{0:dd/MM/yyyy}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);

            BoundField bf2 = new BoundField();
            bf2.DataField = "DT_SITUA_TIPO_BOLSA";
            bf2.HeaderText = "Dt Status";
            bf2.DataFormatString = "{0:dd/MM/yyyy}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Status",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coAgrup = ddlGrupoBolsa.SelectedValue != "" ? int.Parse(ddlGrupoBolsa.SelectedValue) : 0;

            var resultado = (from tb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                             where (txtDE_TIPO_BOLSA.Text != "" ? tb148.NO_TIPO_BOLSA.Contains(txtDE_TIPO_BOLSA.Text) : txtDE_TIPO_BOLSA.Text == "")
                             && (coAgrup != 0 ? tb148.TB317_AGRUP_BOLSA.ID_AGRUP_BOLSA == coAgrup : coAgrup == 0)
                             && (ddlTipo.SelectedValue != "T" ? tb148.TP_GRUPO_BOLSA == ddlTipo.SelectedValue : ddlTipo.SelectedValue == "T")
                             && (ddlSituacao.SelectedValue != "T" ? tb148.CO_SITUA_TIPO_BOLSA == ddlSituacao.SelectedValue : ddlSituacao.SelectedValue == "T")
                             && tb148.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new
                             {
                                 TipoBolsa = tb148.TP_GRUPO_BOLSA == "B" ? "Bolsa" : "Convênio", tb148.NO_TIPO_BOLSA, Grupo_Bolsa = tb148.TB317_AGRUP_BOLSA.NO_AGRUP_BOLSA, tb148.CO_TIPO_BOLSA,
                                 Status = (tb148.CO_SITUA_TIPO_BOLSA.Equals("A") ? "Ativo" : "Inativo"), tb148.DT_CADAS_TIPO_BOLSA, tb148.DT_SITUA_TIPO_BOLSA,
                                 FL_TIPO_VALOR_BOLSA = tb148.FL_TIPO_VALOR_BOLSA == "P" ? "%" : "R$", tb148.VL_TIPO_BOLSA 
                             }).OrderBy(t => t.TipoBolsa).ThenBy(c => c.Grupo_Bolsa).ThenBy(c => c.NO_TIPO_BOLSA);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_TIPO_BOLSA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Caregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Grupos
        /// </summary>
        private void CarregaAgrupador()
        {
            ddlGrupoBolsa.DataSource = TB317_AGRUP_BOLSA.RetornaTodosRegistros().Where(c => c.CO_SITUA_AGRUP_BOLSA == "A" && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlGrupoBolsa.DataTextField = "NO_AGRUP_BOLSA";
            ddlGrupoBolsa.DataValueField = "ID_AGRUP_BOLSA";
            ddlGrupoBolsa.DataBind();

            ddlGrupoBolsa.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
