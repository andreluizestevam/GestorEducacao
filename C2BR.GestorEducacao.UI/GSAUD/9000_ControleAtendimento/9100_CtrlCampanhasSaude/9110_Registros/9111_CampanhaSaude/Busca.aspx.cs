//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 03/03/14 | Maxwell Almeida            | Criação da funcionalidade para busca Campanhas de Vacinação


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

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9111_CampanhaSaude
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
                CarregaClass();
                CarregaComp();
                CarregaSituacao();
                Carregatipos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new String[] { "ID_CAMPAN" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_CAMPAN",
                HeaderText = "CAMPANHA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_INICI_CAMPAN_V",
                HeaderText = "DATA INI"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_TERMI_CAMPAN_V",
                HeaderText = "DATA TER"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {

            var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                       where (txtNomeCampanha.Text != "" ? tbs339.NM_CAMPAN.Contains(txtNomeCampanha.Text) : txtNomeCampanha.Text == "")
                          && (ddlTipoCamp.SelectedValue != "0" ? tbs339.CO_TIPO_CAMPAN == ddlTipoCamp.SelectedValue : 0 == 0)
                          && (ddlCompetencia.SelectedValue != "0" ? tbs339.CO_COMPE_TIPO_CAMPAN == ddlCompetencia.SelectedValue : 0 == 0)
                          && (ddlClassCamp.SelectedValue != "0" ? tbs339.CO_CLASS_CAMPAN == ddlClassCamp.SelectedValue : 0 == 0)
                          && tbs339.CO_SITUA_TIPO_CAMPAN == ddlSituacao.SelectedValue
                       select new saida
                       {
                          ID_CAMPAN = tbs339.ID_CAMPAN,
                          NM_CAMPAN = tbs339.NM_CAMPAN,
                          DT_INICI_CAMPAN = tbs339.DT_INICI_CAMPAN,
                          DT_TERMI_CAMPAN = tbs339.DT_TERMI_CAMPAN,

                       }).OrderBy(w => w.NM_CAMPAN);

            CurrentPadraoBuscas.GridBusca.DataSource = (res.Count() > 0) ? res : null;
        }

        public class saida
        {
            public int ID_CAMPAN { get; set; }
            public string NM_CAMPAN { get; set; }
            public DateTime DT_INICI_CAMPAN { get; set; }
            public string DT_INICI_CAMPAN_V
            {
                get
                {
                    return DT_INICI_CAMPAN.ToString("dd/MM/yyyy");
                }
            }
            public DateTime DT_TERMI_CAMPAN { get; set; }
            public string DT_TERMI_CAMPAN_V
            {
                get
                {
                    return DT_TERMI_CAMPAN.ToString("dd/MM/yyyy");
                }
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_CAMPAN"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega os tipos de campanha
        /// </summary>
        private void Carregatipos()
        {
            AuxiliCarregamentos.CarregaTiposCampanhaSaude(ddlTipoCamp, true);
        }

        /// <summary>
        /// Carrega as competencias de campanha
        /// </summary>
        private void CarregaComp()
        {
            AuxiliCarregamentos.CarregaCompetenciasCampanhaSaude(ddlCompetencia, true);
        }

        /// <summary>
        /// Carrega as classificações de campanha
        /// </summary>
        private void CarregaClass()
        {
            AuxiliCarregamentos.CarregaClassificacoesCampanhaSaude(ddlClassCamp, true);
        }

        /// <summary>
        /// Carrega as situações de campanha
        /// </summary>
        private void CarregaSituacao()
        {
            AuxiliCarregamentos.CarregaSituacaoCampanhaSaude(ddlSituacao, false);

        }

        #endregion
    }
}
