//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: REGISTRO DE OCORRÊNCIAS FUNCIONAIS DE COLABORADORES
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1208_AgendaContato
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
            {
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_AGEND_CONTAT" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_CONTAT",
                HeaderText = "Tipo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CONTAT",
                HeaderText = "Contato"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "UNIDADE",
                HeaderText = "Unidade"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tb306 in TB306_AGEND_CONTATO.RetornaTodosRegistros()
                             join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb306.CO_EMP_CONTAT equals tb25.CO_EMP into sr
                             from x in sr.DefaultIfEmpty()
                             where (ddlTipoContato.SelectedValue != "" ? tb306.TP_CONTAT == ddlTipoContato.SelectedValue : ddlTipoContato.SelectedValue == "")
                             && tb306.CO_USUA_CONTAT == LoginAuxili.CO_COL
                               select new saida
                               {
                                   ID_AGEND_CONTAT = tb306.ID_AGEND_CONTAT,
                                   NO_CONTAT = tb306.NO_CONTAT,
                                   CO_TP_CONTAT = tb306.TP_CONTAT,
                                   UNIDADE = x != null ? x.NO_FANTAS_EMP : ""
                               }).OrderBy(o => o.NO_CONTAT);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        public class saida
        {
            public int ID_AGEND_CONTAT { get; set; }
            public string NO_CONTAT { get; set; }
            public string CO_TP_CONTAT { get; set; }
            public string TP_CONTAT
            {
                get
                {
                    if (LoginAuxili.CO_TIPO_UNID == "PGS")
                    {
                        return CO_TP_CONTAT == "A" ? "Paciente" : CO_TP_CONTAT == "R" ? "Responsável" :
                                   CO_TP_CONTAT == "F" ? "Funcionário" : CO_TP_CONTAT == "P" ? "Profi. Saúde" : "Outros";
                    }
                    else
                    {
                        return CO_TP_CONTAT == "A" ? "Aluno" : CO_TP_CONTAT == "R" ? "Responsável" :
                                CO_TP_CONTAT == "F" ? "Funcionário" : CO_TP_CONTAT == "P" ? "Professor" : "Outros";
                    }
                }
            }
            public string UNIDADE { get; set; }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_AGEND_CONTAT"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
