//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: INFORMAÇÕES DE CALENDÁRIO ESCOLAR
// OBJETIVO: MANUTENÇÃO DE CALENDARIO ESCOLAR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2104_InfoCalendarioEscolar.ManutencaoCalendarioEscolar
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
            if (IsPostBack) return;
            CarregaAnos();
            CarregaMeses();
            CarregaTipos();
            CarregaUnidades();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            ddlAnoReferencia.SelectedValue = DateTime.Now.Year.ToString();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CAL_ID_ATIVI_CALEND" };
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CAL_DATA_CALEND",
                HeaderText = "Data",
                DataFormatString = "{0:dd/MM/yyyy}"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CAL_TIPO_DIA_CALEND",
                HeaderText = "Dia"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CAL_NOME_ATIVID_CALEND",
                HeaderText = "Atividade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TIPO",
                HeaderText = "Origem"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CAT_NOME_TIPO_CALEN",
                HeaderText = "Calendário"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int anoReferCalend = int.Parse(ddlAnoReferencia.SelectedValue);
            int coEmp = int.Parse(ddlUnidade.SelectedValue);

            int mesSelec = 0;
            int idTipoCalend = 0;
            int.TryParse(ddlMesReferencia.SelectedValue, out mesSelec);
            int.TryParse(ddlTipo.SelectedValue, out idTipoCalend);

            var resultado = (from tb157 in TB157_CALENDARIO_ATIVIDADES.RetornaTodosRegistros()
                            where tb157.CAL_ANO_REFER_CALEND == anoReferCalend && tb157.TB25_EMPRESA.CO_EMP == coEmp &&
                            (idTipoCalend == 0 || tb157.TB152_CALENDARIO_TIPO.CAT_ID_TIPO_CALEN == idTipoCalend) &&
                            tb157.TB152_CALENDARIO_TIPO.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                            select new 
                            {
                                tb157.CAL_ID_ATIVI_CALEND, tb157.CAL_DATA_CALEND, tb157.CAL_NOME_ATIVID_CALEND,
                                TIPO = tb157.TB25_EMPRESA == null ? "Instituição" : "Unidade", tb157.TB152_CALENDARIO_TIPO.CAT_NOME_TIPO_CALEN,
                                CAL_TIPO_DIA_CALEND = tb157.CAL_TIPO_DIA_CALEND == "U" ? "Útil/Letivo" :
                                    (tb157.CAL_TIPO_DIA_CALEND == "N" ? "Não Útil/Letivo" :
                                    (tb157.CAL_TIPO_DIA_CALEND == "F" ? "Feriado" :
                                    (tb157.CAL_TIPO_DIA_CALEND == "R" ? "Recesso Escolar" : "Conselho de Classe")))
                            }).OrderBy(c => c.CAL_DATA_CALEND).ToList();

            var resultado2 = (from result in resultado
                              where (mesSelec == 0 || result.CAL_DATA_CALEND.Month == mesSelec)
                              select new
                              {
                                result.CAL_DATA_CALEND, result.CAL_ID_ATIVI_CALEND, result.CAL_NOME_ATIVID_CALEND, result.CAT_NOME_TIPO_CALEN,
                                result.TIPO, result.CAL_TIPO_DIA_CALEND
                              }).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado2.Count() > 0) ? resultado2 : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CAL_ID_ATIVI_CALEND"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Anos de Referência
        private void CarregaAnos() 
        {
            ddlAnoReferencia.Items.Clear();

            for (int a = DateTime.Now.Year - 15; a < DateTime.Now.Year + 5; a++)
                ddlAnoReferencia.Items.Add(new ListItem(a.ToString(), a.ToString()));
        }

//====> Método que carrega o DropDown de Meses de Referência
        private void CarregaMeses()
        {
            ddlMesReferencia.Items.Clear();

            ddlMesReferencia.Items.Add(new ListItem("Janeiro", "1"));
            ddlMesReferencia.Items.Add(new ListItem("Fevereiro", "2"));
            ddlMesReferencia.Items.Add(new ListItem("Março", "3"));
            ddlMesReferencia.Items.Add(new ListItem("Abril", "4"));
            ddlMesReferencia.Items.Add(new ListItem("Maio", "5"));
            ddlMesReferencia.Items.Add(new ListItem("Junho", "6"));
            ddlMesReferencia.Items.Add(new ListItem("Julho", "7"));
            ddlMesReferencia.Items.Add(new ListItem("Agosto", "8"));
            ddlMesReferencia.Items.Add(new ListItem("Setembro", "9"));
            ddlMesReferencia.Items.Add(new ListItem("Outubro", "10"));
            ddlMesReferencia.Items.Add(new ListItem("Novembro", "11"));
            ddlMesReferencia.Items.Add(new ListItem("Dezembro", "12"));

            ddlMesReferencia.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Tipos de Calendário
        private void CarregaTipos()
        {
            ddlTipo.DataSource = (from c in TB152_CALENDARIO_TIPO.RetornaTodosRegistros()
                                  where c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                  select new { c.CAT_ID_TIPO_CALEN, c.CAT_NOME_TIPO_CALEN }).OrderBy(r => r.CAT_NOME_TIPO_CALEN);

            ddlTipo.DataTextField = "CAT_NOME_TIPO_CALEN";
            ddlTipo.DataValueField = "CAT_ID_TIPO_CALEN";
            ddlTipo.DataBind();

            ddlTipo.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
        }        
        #endregion
    }
}
