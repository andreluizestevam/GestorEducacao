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

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9400_CtrlSUS._9410_Registros._9411_CadastroDomicilio
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
                txtDtIni.Text = DateTime.Now.ToShortDateString();
                txtDtFim.Text = DateTime.Now.ToShortDateString();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new String[] { "ID_CADAS_DOMIC" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_CADAS_DOMIC_V",
                HeaderText = "DATA CADASTRO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_MICROAREA",
                HeaderText = "MICROÁREA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CADAS_DOMIC",
                HeaderText = "CÓDIGO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var dataIni = String.IsNullOrEmpty(txtDtIni.Text) ? DateTime.MinValue : DateTime.Parse(txtDtIni.Text);
            var dataFim = String.IsNullOrEmpty(txtDtFim.Text) ? DateTime.MinValue : DateTime.Parse(txtDtFim.Text);
            var res = (from tbs176 in TBS176_ESUS_CADAS_DOMIC.RetornaTodosRegistros()
                       where (txtcodDocum.Text != "" ? tbs176.CO_CADAS_DOMIC.Contains(txtcodDocum.Text) : txtcodDocum.Text == "")
                       && (txtMicroarea.Text != "" ? tbs176.CO_MICROAREA.Contains(txtMicroarea.Text) : txtMicroarea.Text == "")
                       && (tbs176.DT_CADAS_DOMIC >= dataIni) && (tbs176.DT_CADAS_DOMIC <= dataFim)
                       select new saida
                       {
                           ID_CADAS_DOMIC = tbs176.ID_CADAS_DOMIC,
                           CO_CADAS_DOMIC = tbs176.CO_CADAS_DOMIC,
                           DT_CADAS_DOMIC = tbs176.DT_CADAS_DOMIC,
                           CO_MICROAREA = tbs176.CO_MICROAREA,
                       }).OrderBy(w => w.DT_CADAS_DOMIC).ThenBy(w => w.CO_MICROAREA).ThenBy(w => w.CO_CADAS_DOMIC);

            CurrentPadraoBuscas.GridBusca.DataSource = (res.Count() > 0) ? res : null;
        }

        public class saida
        {
            public int ID_CADAS_DOMIC { get; set; }
            public string CO_CADAS_DOMIC { get; set; }
            public DateTime DT_CADAS_DOMIC { get; set; }
            public string DT_CADAS_DOMIC_V
            {
                get
                {
                    return DT_CADAS_DOMIC.ToString("dd/MM/yyyy");
                }
            }
            public string CO_MICROAREA { get; set; }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_CADAS_DOMIC"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamentos


        #endregion
    }
}
