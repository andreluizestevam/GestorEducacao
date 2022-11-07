//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CADASTRO INTERNACIONAL DE DOENÇAS.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 14/12/16 |   BRUNO VIEIRA LANDIM      | Criado nova funcionalidade

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
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0919_CadastroClassRisco
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }


        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            try
            {
                CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_CLASS_RISCO" };

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "TIPO",
                    HeaderText = "TIPO"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "NU_ORDEM",
                    HeaderText = "SEQ"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "NO_PRIOR",
                    HeaderText = "NOME"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "NO_COR",
                    HeaderText = "COR"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "NU_TEMPO",
                    HeaderText = "TEMPO"
                });

                CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
                {
                    DataField = "SITUACAO",
                    HeaderText = "SIT"
                });

            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro de execução, entre em contato com o suporte para solucionar o problema.");
            }
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            try
            {
                int tipo = int.Parse(ddlTipoClassRisco.SelectedValue);

                var resultado = (from tbs435 in TBS435_CLASS_RISCO.RetornaTodosRegistros()
                                 where (tipo != 0 ? tbs435.TP_CLASS_RISCO == tipo : 0 == 0)
                                 && (ddlSitua.SelectedValue != "T" ? tbs435.FL_SITUA == ddlSitua.SelectedValue : 0 == 0)
                                 select new
                                 {
                                     tbs435.ID_CLASS_RISCO,
                                     TIPO = tbs435.TP_CLASS_RISCO == 1 ? "Australiano" : (tbs435.TP_CLASS_RISCO == 2 ? "Canadense" : (tbs435.TP_CLASS_RISCO == 3 ? "Manchester" : (tbs435.TP_CLASS_RISCO == 4 ? "Americano" : (tbs435.TP_CLASS_RISCO == 5 ? "Pediatria" : (tbs435.TP_CLASS_RISCO == 6 ? "Obstetrícia" : "Instituição"))))),
                                     tbs435.NU_ORDEM,
                                     tbs435.NO_PRIOR,
                                     tbs435.NO_COR,
                                     tbs435.NU_TEMPO,
                                     SITUACAO = tbs435.FL_SITUA == "A" ? "Ativo" : "Inativo",
                                 }).OrderBy(c => c.TIPO).ThenBy(k => k.NU_ORDEM);

                int count = resultado.Count();

                if (count > 0)
                {
                    CurrentPadraoBuscas.GridBusca.DataSource = resultado;
                }
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro de execução, entre em contato com o suporte para solucionar o problema");
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_CLASS_RISCO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion
    }
}
